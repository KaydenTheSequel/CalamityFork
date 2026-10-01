using System.Collections.Generic;
using System.Linq;
using CalamityMod.Effects;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class BloodBoilerMetaball : Metaball
{
	public class BloodBoilerParticle
	{
		public Vector2 Center;

		public float Size;

		public BloodBoilerParticle(Vector2 center, float size)
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

	public static List<BloodBoilerParticle> Particles { get; private set; } = new List<BloodBoilerParticle>();

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
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			Color red = Color.Red;
			((Color)(ref red)).A = 0;
			return red;
		}
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((BloodBoilerParticle p) => p.Size <= 2f);
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
		Particles.Add(new BloodBoilerParticle(position, size));
	}

	public override void DrawInstances()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		float pureRedIntensity = 0.15f;
		float opacity = 1f;
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SmallGreyscaleCircle", (AssetRequestMode)2).Value;
		foreach (BloodBoilerParticle particle in Particles)
		{
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = tex.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / tex.Size();
			float pureRedInterpolant = Utils.GetLerpValue(25f, 60f, particle.Size, clamped: true) * pureRedIntensity;
			Color drawColor = Color.Lerp((!ChildSafety.Disabled) ? Color.CornflowerBlue : EdgeColor, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed, pureRedInterpolant).MultiplyRGBA(new Color(1f, 1f, 1f, opacity));
			Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, (!ChildSafety.Disabled) ? Color.CornflowerBlue : drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
