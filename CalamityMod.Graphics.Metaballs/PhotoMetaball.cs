using System.Collections.Generic;
using System.Linq;
using CalamityMod.Effects;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class PhotoMetaball : Metaball
{
	public class PhotoParticle
	{
		public Vector2 Center;

		public float Size;

		public int Time;

		public float OrginSize;

		public PhotoParticle(Vector2 center, float size)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Center = center;
			Size = size;
		}

		public void Update()
		{
			Time++;
			if (Time == 1)
			{
				OrginSize = Size;
			}
			Size = MathHelper.Clamp(Size - 0.8f, 0f, 1000f) * 0.96f;
			if (Size < OrginSize * 0.15f)
			{
				Size = 2f;
			}
		}
	}

	public Color sparkColor;

	public int Time;

	public static List<PhotoParticle> Particles { get; private set; } = new List<PhotoParticle>();

	public override bool AnythingToDraw => Particles.Any();

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		}
	}

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.AfterProjectiles;

	public override Color EdgeColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return sparkColor;
		}
	}

	public override void Update()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((PhotoParticle p) => p.Size <= 2f);
		if (Time % 10 == 0)
		{
			sparkColor = (Color)(Main.rand.Next(4) switch
			{
				0 => Color.Red, 
				1 => Color.MediumTurquoise, 
				2 => Color.Orange, 
				_ => Color.LawnGreen, 
			});
		}
	}

	public override void PrepareShaderForTarget(int layerIndex)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Asset<Effect> additiveMetaballEdgeShader = CalamityShaders.AdditiveMetaballEdgeShader;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		EffectParameter obj = additiveMetaballEdgeShader.Value.Parameters["screenArea"];
		if (obj != null)
		{
			obj.SetValue(screenSize);
		}
		EffectParameter obj2 = additiveMetaballEdgeShader.Value.Parameters["layerOffset"];
		if (obj2 != null)
		{
			obj2.SetValue(Vector2.Zero);
		}
		EffectParameter obj3 = additiveMetaballEdgeShader.Value.Parameters["singleFrameScreenOffset"];
		if (obj3 != null)
		{
			obj3.SetValue(Vector2.Zero);
		}
		additiveMetaballEdgeShader.Value.CurrentTechnique.Passes[0].Apply();
	}

	public static void SpawnParticle(Vector2 position, float size)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new PhotoParticle(position, size));
	}

	public override void DrawInstances()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		float opacity = 1f;
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/MetaballMessy", (AssetRequestMode)2).Value;
		foreach (PhotoParticle particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			float Interpolant = Utils.GetLerpValue(25f, 60f, particle.Size * 0.6f, clamped: true);
			Color drawColor = Color.Lerp(EdgeColor, Color.White * 0.9f, Interpolant).MultiplyRGBA(new Color(1f, 1f, 1f, opacity));
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
