using System;
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

public class RancorLavaMetaball : Metaball
{
	public class LavaParticle
	{
		public int Time;

		public float Size;

		public Vector2 Center;

		public LavaParticle(Vector2 center, float size)
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
			Size = MathHelper.Clamp(Size - 0.24f, 0f, 200f) * 0.9956f;
			if (Size < 15f)
			{
				Size = Size * 0.95f - 0.9f;
			}
		}
	}

	public static List<LavaParticle> Particles { get; private set; } = new List<LavaParticle>();

	public override bool AnythingToDraw => Particles.Any();

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.AfterProjectiles;

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		}
	}

	public override Color EdgeColor
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 56, 3);
		}
	}

	public override void ClearInstances()
	{
		Particles.Clear();
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
		Particles.Add(new LavaParticle(position, size));
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((LavaParticle p) => p.Size <= 3f);
	}

	public override void PrepareSpriteBatch(SpriteBatch spriteBatch)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, (Effect)null, Main.Transform);
	}

	public override void DrawInstances()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SmallGreyscaleCircle", (AssetRequestMode)2).Value;
		foreach (LavaParticle particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Color lavaColor = EdgeColor * 1.2f;
			((Color)(ref lavaColor)).B = (byte)(((Color)(ref lavaColor)).B + (int)(MathF.Cos(particle.Center.Y * 0.015f + Main.GlobalTimeWrappedHourly * 0.1f) * 3f));
			float brightnessInterpolant = Utils.GetLerpValue(10f, 2f, particle.Time, clamped: true) * 0.67f;
			lavaColor = Color.Lerp(lavaColor, Color.Wheat, brightnessInterpolant);
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, lavaColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
