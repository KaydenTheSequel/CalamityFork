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

public abstract class DragonsBreathMetaball2 : Metaball
{
	public class DragonsBreathParticle2
	{
		public Vector2 Center;

		public float Size;

		public DragonsBreathParticle2(Vector2 center, float size)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Center = center;
			Size = size;
		}

		public void Update()
		{
			Size = MathHelper.Clamp(Size - 0.1f, 0f, 200f) * 0.91f;
			if (Size < 20f)
			{
				Size = Size * 0.8f - 1f;
			}
		}
	}

	public List<DragonsBreathParticle2> Particles { get; private set; } = new List<DragonsBreathParticle2>();

	public override bool AnythingToDraw => Particles.Any();

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		}
	}

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.AfterProjectiles;

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((DragonsBreathParticle2 p) => p.Size <= 2f);
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

	public void SpawnParticle(Vector2 position, float size)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new DragonsBreathParticle2(position, size));
	}

	internal void DrawInstancesInternal(float opacity, float whiteSizeThreshold)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		float pureWhiteIntensity = 0.16f;
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Graphics/Metaballs/MetaballMessy", (AssetRequestMode)2).Value;
		foreach (DragonsBreathParticle2 particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			Color drawColor;
			if (whiteSizeThreshold >= 0f)
			{
				float pureWhiteInterpolant = Utils.GetLerpValue(0.8f * whiteSizeThreshold, whiteSizeThreshold, particle.Size, clamped: true) * pureWhiteIntensity;
				drawColor = Color.Lerp(EdgeColor, Color.White, pureWhiteInterpolant) * opacity;
			}
			else
			{
				drawColor = Color.Lerp(EdgeColor, Color.DarkOrange, Utils.GetLerpValue(60f, 100f, particle.Size, clamped: true) * 0.3f);
			}
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
