using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class DeathAshParticle
{
	internal static Dictionary<NPC, RenderTargetLease> PendingNPCsToDraw = new Dictionary<NPC, RenderTargetLease>();

	internal static BasicEffect basicShader = null;

	internal static VertexPositionColorTexture[] VertexCache = (VertexPositionColorTexture[])(object)new VertexPositionColorTexture[1024];

	internal static short[] IndexCache = new short[1536];

	public int Time;

	public int Lifetime;

	public int ID;

	public float Scale;

	public Color AshColor;

	public Vector2 Center;

	public Vector2 Velocity;

	public static Vector2 VelOverride;

	public static HashSet<DeathAshParticle> Ashes = new HashSet<DeathAshParticle>();

	public const int PrimitiveBatchSize = 256;

	public const int AshCountLimit = 45000;

	public Vector2 TopLeft
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return Center - Vector2.One * Scale * 3.5f;
		}
	}

	public Vector2 TopRight
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			return Center + new Vector2(1f, -1f) * Scale * 3.5f;
		}
	}

	public Vector2 BottomRight
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return Center + Vector2.One * Scale * 3.5f;
		}
	}

	public Vector2 BottomLeft
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			return Center + new Vector2(-1f, 1f) * Scale * 3.5f;
		}
	}

	public static BasicEffect BasicShader
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!Main.dedServ && basicShader == null)
			{
				basicShader = new BasicEffect(((Game)Main.instance).GraphicsDevice)
				{
					VertexColorEnabled = true,
					TextureEnabled = false
				};
			}
			return basicShader;
		}
	}

	public DeathAshParticle(int lifetime, float brightness, Vector2 spawnPosition)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Scale = 1f;
		base._002Ector();
		Time = 0;
		Lifetime = lifetime;
		ID = Ashes.Count;
		AshColor = Main.hslToRgb(0f, 0f, brightness * 0.67f);
		Scale = Main.rand.NextFloat(0.7f, 1.3f);
		Center = spawnPosition;
	}

	public static void PrepareRenderTargets()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		foreach (NPC npc in PendingNPCsToDraw.Keys)
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
			using (PendingNPCsToDraw[npc].Scope(preserveContents: true, Color.Transparent))
			{
				Vector2 oldPosition = npc.position;
				npc.oldPos = (Vector2[])(object)new Vector2[npc.oldPos.Length];
				npc.position = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
				try
				{
					Main.instance.DrawNPC(npc.whoAmI, behindTiles: true);
				}
				catch
				{
				}
				npc.position = oldPosition;
				npc.Opacity = 0f;
				Main.spriteBatch.End();
			}
		}
	}

	public static void CreateAshesFromNPC(NPC npc, Vector2 velocityOverride)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			VelOverride = velocityOverride;
			PendingNPCsToDraw[npc] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
		}
	}

	public static Dictionary<Vector2, Color> GetColorCacheFromTexture(Texture2D texture, Rectangle? frame = null, bool pruneForEfficency = false)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<Vector2, Color> colorCache = new Dictionary<Vector2, Color>();
		Color[] uncleanedCache = (Color[])(object)new Color[texture.Width * texture.Height];
		texture.GetData<Color>(uncleanedCache);
		int width = texture.Width;
		int height = texture.Height;
		if (!frame.HasValue)
		{
			frame = new Rectangle(0, 0, width, height);
		}
		int stride = 1;
		int totalFilledPixels = Enumerable.Count(uncleanedCache, (Color c) => ((Color)(ref c)).R != 0 || ((Color)(ref c)).G != 0 || ((Color)(ref c)).B != 0 || ((Color)(ref c)).A != 0);
		if (pruneForEfficency && totalFilledPixels > 16000)
		{
			stride = 2;
		}
		if (pruneForEfficency && totalFilledPixels > 38000)
		{
			stride = 3;
		}
		for (int i = 0; i < width; i += stride)
		{
			for (int j = 0; j < height; j += stride)
			{
				Color color = uncleanedCache[i + j * width];
				int num = i;
				Rectangle value = frame.Value;
				if (num < ((Rectangle)(ref value)).Left)
				{
					continue;
				}
				int num2 = i;
				value = frame.Value;
				if (num2 >= ((Rectangle)(ref value)).Right)
				{
					continue;
				}
				int num3 = j;
				value = frame.Value;
				if (num3 >= ((Rectangle)(ref value)).Top)
				{
					int num4 = j;
					value = frame.Value;
					if (num4 < ((Rectangle)(ref value)).Bottom && (((Color)(ref color)).R != 0 || ((Color)(ref color)).G != 0 || ((Color)(ref color)).B != 0 || ((Color)(ref color)).A != 0))
					{
						colorCache[new Vector2((float)(i - frame.Value.X), (float)(j - frame.Value.Y))] = color;
					}
				}
			}
		}
		return colorCache;
	}

	public static void FlushContentsAndCreateAshes()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		if (PendingNPCsToDraw.Count == 0)
		{
			return;
		}
		Rectangle frame = default(Rectangle);
		foreach (NPC npc in PendingNPCsToDraw.Keys)
		{
			RenderTarget2D temporaryTextureDrawTarget = PendingNPCsToDraw[npc].Target;
			((Rectangle)(ref frame))._002Ector(0, 0, ((Texture2D)temporaryTextureDrawTarget).Width, ((Texture2D)temporaryTextureDrawTarget).Height);
			Dictionary<Vector2, Color> colorsOnNPC = GetColorCacheFromTexture((Texture2D)(object)temporaryTextureDrawTarget, frame, pruneForEfficency: true);
			if (Ashes.Count + colorsOnNPC.Count > 45000)
			{
				break;
			}
			foreach (Vector2 drawOffset in colorsOnNPC.Keys)
			{
				int lifetime = Main.rand.Next(105, 145);
				Vector2 ashSpawnPosition = npc.position + drawOffset - new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
				Color color = colorsOnNPC[drawOffset];
				float brightness = (float)(((Color)(ref color)).R + ((Color)(ref color)).G + ((Color)(ref color)).B) / 765f;
				DeathAshParticle ash = new DeathAshParticle(lifetime, brightness, ashSpawnPosition)
				{
					Velocity = (Main.npc.IndexInRange(npc.realLife) ? Main.npc[npc.realLife].velocity : npc.velocity) * 0.7f
				};
				if (brightness > 0.05f)
				{
					Ashes.Add(ash);
				}
			}
			PendingNPCsToDraw[npc].Dispose();
		}
		PendingNPCsToDraw.Clear();
	}

	public static void DrawAll()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		FlushContentsAndCreateAshes();
		CalamityUtils.CalculatePerspectiveMatricies(out var effectView, out var effectProjection);
		BasicShader.View = effectView;
		BasicShader.Projection = effectProjection;
		int batchIndex = 0;
		Array.Clear(VertexCache, 0, VertexCache.Length);
		Array.Clear(IndexCache, 0, IndexCache.Length);
		((Effect)BasicShader).CurrentTechnique.Passes[0].Apply();
		foreach (DeathAshParticle ash in Ashes)
		{
			Color fadedAshColor = ash.AshColor * ash.Scale;
			VertexCache[batchIndex * 4] = new VertexPositionColorTexture(new Vector3(ash.TopLeft - Main.screenPosition, 0f), fadedAshColor, new Vector2(0f, 0f));
			VertexCache[batchIndex * 4 + 1] = new VertexPositionColorTexture(new Vector3(ash.TopRight - Main.screenPosition, 0f), fadedAshColor, new Vector2(1f, 0f));
			VertexCache[batchIndex * 4 + 2] = new VertexPositionColorTexture(new Vector3(ash.BottomRight - Main.screenPosition, 0f), fadedAshColor, new Vector2(1f, 1f));
			VertexCache[batchIndex * 4 + 3] = new VertexPositionColorTexture(new Vector3(ash.BottomLeft - Main.screenPosition, 0f), fadedAshColor, new Vector2(0f, 1f));
			IndexCache[batchIndex * 6] = (short)(batchIndex * 4);
			IndexCache[batchIndex * 6 + 1] = (short)(batchIndex * 4 + 1);
			IndexCache[batchIndex * 6 + 2] = (short)(batchIndex * 4 + 2);
			IndexCache[batchIndex * 6 + 3] = (short)(batchIndex * 4);
			IndexCache[batchIndex * 6 + 4] = (short)(batchIndex * 4 + 2);
			IndexCache[batchIndex * 6 + 5] = (short)(batchIndex * 4 + 3);
			batchIndex++;
			if (batchIndex >= 256)
			{
				((Game)Main.instance).GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>((PrimitiveType)0, VertexCache, 0, 1024, IndexCache, 0, 512);
				batchIndex = 0;
			}
		}
		if (batchIndex > 0)
		{
			((Game)Main.instance).GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>((PrimitiveType)0, VertexCache, 0, batchIndex * 4, IndexCache, 0, batchIndex * 2);
		}
	}

	public void Update()
	{
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		float brightness = (float)(((Color)(ref AshColor)).R + ((Color)(ref AshColor)).G + ((Color)(ref AshColor)).B) / 765f;
		Time++;
		Scale = MathHelper.Clamp(Scale - ((brightness < 0.1f) ? 0.08f : 0.008f), 0f, 1f);
		float dissipationFactor = Utils.GetLerpValue(6f, 16f, ((Vector2)(ref Velocity)).Length(), clamped: true);
		float velocityInterpolant = Utils.GetLerpValue(25f - dissipationFactor * 10f, 80f - dissipationFactor * 45f, Time, clamped: true);
		Vector2 idealVelocity = default(Vector2);
		((Vector2)(ref idealVelocity))._002Ector(Main.windSpeedCurrent * MathHelper.Lerp(0.8f, 1.2f, (float)Math.Sin(Center.Y / 50f + (float)ID)) * 20f, (float)Math.Sin(Main.time / 20.0 + (double)((float)ID * 0.01f)) * 3f - 1f);
		if (VelOverride != Vector2.Zero)
		{
			Vector2 useVel = VelOverride.RotateRandom(0.01f * (float)ID);
			((Vector2)(ref idealVelocity))._002Ector(useVel.X * MathHelper.Lerp(0.8f, 1.2f, (float)Math.Sin(Main.time / 20.0 + (double)ID)), useVel.Y * MathHelper.Lerp(0.8f, 1.2f, (float)Math.Sin(Main.time / 20.0 + (double)ID)));
		}
		Velocity = Vector2.Lerp(Velocity, idealVelocity, velocityInterpolant * 0.16f);
		Center += Velocity;
	}

	public static void UpdateAll()
	{
		if (Main.dedServ)
		{
			return;
		}
		foreach (DeathAshParticle ash in Ashes)
		{
			ash.Update();
		}
		Ashes.RemoveWhere((DeathAshParticle a) => a.Time >= a.Lifetime);
	}
}
