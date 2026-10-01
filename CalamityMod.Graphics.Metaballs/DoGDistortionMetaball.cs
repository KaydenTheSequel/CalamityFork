using System.Collections.Generic;
using CalamityMod.Effects;
using CalamityMod.Enums;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Systems.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class DoGDistortionMetaball : Metaball
{
	public class DistortionParticle
	{
		public Vector2 Center;

		public Vector2 Velocity;

		public float Size;

		public bool Square;

		public float Rotation;

		public DistortionParticle(Vector2 center, Vector2 velocity, float size, bool square = false, float rotation = 0f)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			Center = center;
			Velocity = velocity;
			Size = size;
			Square = square;
			Rotation = rotation;
			base._002Ector();
		}

		public void Update()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			Center += Velocity;
			Velocity *= 0.96f;
			Size *= 0.91f;
		}
	}

	public static List<DistortionParticle> Particles { get; private set; } = new List<DistortionParticle>();

	public override GeneralDrawLayer DrawLayer => GeneralDrawLayer.AfterProjectiles;

	public override bool AnythingToDraw
	{
		get
		{
			if (Particles.Count <= 0)
			{
				return CalamityUtils.AnyProjectiles(ModContent.ProjectileType<DoGRiftCrack>());
			}
			return true;
		}
	}

	public override Color EdgeColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(136, 26, 186);
		}
	}

	public override IEnumerable<Texture2D> Layers
	{
		get
		{
			yield return ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		}
	}

	public static void SpawnParticle(Vector2 position, Vector2 velocity, float size, bool square = false, float rotation = 0f)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Particles.Add(new DistortionParticle(position, velocity, size, square, rotation));
	}

	public override void Update()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
		}
		Particles.RemoveAll((DistortionParticle p) => p.Size <= 2f);
	}

	public override void PrepareShaderForTarget(int layerIndex)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		Asset<Effect> metaballEdgeShader = CalamityShaders.MetaballEdgeShader;
		GraphicsDevice gd = ((Game)Main.instance).GraphicsDevice;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		Vector2 layerScrollOffset = Main.screenPosition / screenSize + CalculateManualOffsetForLayer(layerIndex);
		if (FixedToScreen)
		{
			layerScrollOffset = Vector2.Zero;
		}
		EffectParameter obj = metaballEdgeShader.Value.Parameters["layerSize"];
		if (obj != null)
		{
			obj.SetValue(((Texture2D)(object)DoGVisualsManager.DistortionForegroundContentsTarget.Target).Size());
		}
		EffectParameter obj2 = metaballEdgeShader.Value.Parameters["screenSize"];
		if (obj2 != null)
		{
			obj2.SetValue(screenSize);
		}
		EffectParameter obj3 = metaballEdgeShader.Value.Parameters["layerOffset"];
		if (obj3 != null)
		{
			obj3.SetValue(layerScrollOffset);
		}
		EffectParameter obj4 = metaballEdgeShader.Value.Parameters["edgeColor"];
		if (obj4 != null)
		{
			Color edgeColor = EdgeColor;
			obj4.SetValue(((Color)(ref edgeColor)).ToVector4());
		}
		EffectParameter obj5 = metaballEdgeShader.Value.Parameters["singleFrameScreenOffset"];
		if (obj5 != null)
		{
			obj5.SetValue((Main.screenLastPosition - Main.screenPosition) / screenSize);
		}
		gd.Textures[1] = (Texture)(object)DoGVisualsManager.DistortionForegroundContentsTarget.Target;
		gd.SamplerStates[1] = SamplerState.LinearWrap;
		metaballEdgeShader.Value.CurrentTechnique.Passes[0].Apply();
	}

	public override void DrawInstances()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		Texture2D metaballTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		Texture2D squareTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/Square", (AssetRequestMode)2).Value;
		foreach (DistortionParticle particle in Particles)
		{
			Texture2D textureToUse = (particle.Square ? squareTexture : metaballTexture);
			Vector2 drawPosition = particle.Center - Main.screenPosition;
			Vector2 origin = textureToUse.Size() * 0.5f;
			Vector2 scale = Vector2.One * particle.Size / textureToUse.Size();
			Main.spriteBatch.Draw(textureToUse, drawPosition, (Rectangle?)null, Color.White, particle.Rotation, origin, scale, (SpriteEffects)0, 0f);
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Projectile proj = enumerator2.Current;
			if (proj.type == ModContent.ProjectileType<DoGRiftCrack>())
			{
				Color lightColor = Color.White;
				proj.ModProjectile.PreDraw(ref lightColor);
			}
		}
	}
}
