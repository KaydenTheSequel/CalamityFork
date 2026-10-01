using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Threading;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic.TempParticleSystem;

public class TempParticleManager
{
	public delegate void ParticleUpdateFunctionDelegate(TempParticle tempParticle);

	public delegate void ParticleDrawFunctionDelegate(TempParticle tempParticle, SpriteBatch spriteBatch, Vector2 drawOffset);

	public readonly List<TempParticle> ActiveParticles;

	public readonly int MaxParticles;

	public readonly Asset<Texture2D> BaseParticleTexture;

	public readonly ParticleUpdateFunctionDelegate ParticleUpdateFunction;

	public readonly ParticleDrawFunctionDelegate ParticleDrawingOverride;

	private readonly Queue<TempParticle> ParticlePool;

	public TempParticleManager(int maxParticles, string baseParticleTexturePath, ParticleUpdateFunctionDelegate particleUpdateFunction = null, ParticleDrawFunctionDelegate particleDrawingOverride = null)
	{
		if (Main.dedServ)
		{
			return;
		}
		MaxParticles = maxParticles;
		BaseParticleTexture = ModContent.Request<Texture2D>(baseParticleTexturePath, (AssetRequestMode)2);
		ParticleUpdateFunction = particleUpdateFunction;
		ParticleDrawingOverride = particleDrawingOverride;
		ActiveParticles = new List<TempParticle>();
		if (ParticlePool == null)
		{
			ParticlePool = new Queue<TempParticle>();
			for (int i = 0; i < maxParticles; i++)
			{
				ParticlePool.Enqueue(new TempParticle());
			}
		}
	}

	public void SpawnParticle(Vector2 position, Vector2 velocity, Vector2 scale, Color drawColor, int lifetime, float rotation = 0f, float opacity = 1f, float parallaxStrength = 0f, int frameX = 0, int frameY = 0, int maxHorizontalFrames = 1, int maxVerticalFrames = 1, float extraData0 = 0f, float extraData1 = 0f, float extraData2 = 0f, float extraData3 = 0f, Vector2? storedPosition = null)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		if (ActiveParticles.Count < MaxParticles && ActiveParticles.Count < CalamityClientConfig.Instance.ParticleLimit && !Main.dedServ && ParticlePool.Count > 0)
		{
			TempParticle pooledParticle = ParticlePool.Dequeue();
			pooledParticle.SetBasicParticleData(BaseParticleTexture, position, (Vector2)(((_003F?)storedPosition) ?? Vector2.Zero), velocity, scale, drawColor, rotation, opacity, parallaxStrength, lifetime, frameX, frameY, maxHorizontalFrames, maxVerticalFrames, extraData0, extraData1, extraData2, extraData3);
			ActiveParticles.Add(pooledParticle);
		}
	}

	public void SpawnParticle(Vector2 position, Vector2 velocity, float scale, Color drawColor, int lifetime, float rotation = 0f, float opacity = 1f, float parallaxStrength = 0f, int frameX = 0, int frameY = 0, int maxHorizontalFrames = 1, int maxVerticalFrames = 1, float extraData0 = 0f, float extraData1 = 0f, float extraData2 = 0f, float extraData3 = 0f, Vector2? storedPosition = null)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		SpawnParticle(position, velocity, new Vector2(scale), drawColor, lifetime, rotation, opacity, parallaxStrength, frameX, frameY, maxHorizontalFrames, maxVerticalFrames, extraData0, extraData1, extraData2, extraData3, storedPosition);
	}

	public void Update()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (ActiveParticles.Count == 0)
		{
			return;
		}
		FastParallel.For(0, ActiveParticles.Count, (ParallelForAction)delegate(int start, int end, object context)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			for (int i = start; i < end; i++)
			{
				ActiveParticles[i].Time++;
				TempParticle tempParticle = ActiveParticles[i];
				tempParticle.Position += ActiveParticles[i].Velocity;
				ParticleUpdateFunction(ActiveParticles[i]);
			}
		}, (object)null);
		ActiveParticles.RemoveAll(delegate(TempParticle p)
		{
			if (p.Time >= p.Lifetime)
			{
				ParticlePool.Enqueue(p);
				return true;
			}
			return false;
		});
	}

	public void DrawAllParticles(SpriteBatch spriteBatch, Vector2? baseOffset = null)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (ActiveParticles.Count == 0)
		{
			return;
		}
		Vector2 drawOffset = (Vector2)(((_003F?)baseOffset) ?? (-Main.screenPosition));
		if (ParticleDrawingOverride != null)
		{
			foreach (TempParticle tempParticle in ActiveParticles)
			{
				ParticleDrawingOverride(tempParticle, spriteBatch, drawOffset);
			}
			return;
		}
		foreach (TempParticle tempParticle2 in ActiveParticles)
		{
			Vector2 drawPosition = tempParticle2.Position + drawOffset + tempParticle2.StoredPosition;
			Rectangle frame = BaseParticleTexture.Frame(tempParticle2.MaxHorizontalFrames, tempParticle2.MaxVerticalFrames, tempParticle2.FrameX, tempParticle2.FrameY);
			Vector2 origin = frame.Size() * 0.5f;
			spriteBatch.Draw(BaseParticleTexture.Value, drawPosition, (Rectangle?)frame, tempParticle2.DrawColor * tempParticle2.Opacity, tempParticle2.Rotation, origin, tempParticle2.Scale, (SpriteEffects)0, 0f);
		}
	}

	public void ReturnAll()
	{
		if (ActiveParticles.Count != 0)
		{
			ActiveParticles.RemoveAll(delegate(TempParticle p)
			{
				ParticlePool.Enqueue(p);
				return true;
			});
		}
	}
}
