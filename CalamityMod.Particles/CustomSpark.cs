using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class CustomSpark : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public bool FadeIn;

	public float FadeInScale;

	public bool GlowCenter;

	public float GlowCenterScale;

	public float GlowOpacity;

	public string NewTexture;

	public float ExtraRotation;

	public Vector2 Stretch;

	public float ShrinkSpeed;

	public bool FlipHorizontal;

	public bool NoShrink;

	public float Spin;

	public bool AltVisual;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool UseAdditiveBlend => AltVisual;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public CustomSpark(Vector2 relativePosition, Vector2 velocity, string texture, bool affectedByGravity, int lifetime, float scale, Color color, Vector2 stretch, bool useAddativeBlend = true, bool glowCenter = false, float extraRotation = 0f, bool fadeIn = false, bool affectedByLight = false, float shrinkSpeed = 0f, float glowCenterScale = 1f, float glowOpacity = 1f, bool flipHorizontal = false, bool noShrink = false, float spin = 0f)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		GlowCenterScale = 1f;
		GlowOpacity = 1f;
		Stretch = new Vector2(0.5f, 1.6f);
		AltVisual = true;
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		NewTexture = texture;
		ExtraRotation = extraRotation;
		AffectedByGravity = affectedByGravity;
		AffectedByLight = affectedByLight;
		Scale = scale;
		Stretch = stretch;
		FadeInScale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		ShrinkSpeed = shrinkSpeed;
		AltVisual = useAddativeBlend;
		GlowCenter = glowCenter;
		GlowCenterScale = glowCenterScale;
		GlowOpacity = glowOpacity;
		FlipHorizontal = flipHorizontal;
		NoShrink = noShrink;
		FadeIn = fadeIn;
		if (FadeIn)
		{
			Scale = 0f;
		}
		Spin = spin;
	}

	public override void Update()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		if (!FadeIn)
		{
			if (!NoShrink)
			{
				Scale *= 0.95f;
			}
			Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		}
		else if ((float)Time / (float)Lifetime < 0.5f)
		{
			Scale = MathHelper.Lerp(Scale, FadeInScale, 0.2f);
		}
		else
		{
			Scale = MathHelper.Lerp(Scale, FadeInScale, -0.21f);
		}
		Velocity *= 0.95f;
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		ExtraRotation += Spin;
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f + ExtraRotation;
		Stretch.X *= 1f - 0.2f * ShrinkSpeed;
		Stretch.Y *= 1f + 0.2f * ShrinkSpeed;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = Stretch * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(NewTexture, (AssetRequestMode)2).Value;
		float scaleMult = 1f;
		if (Main.zenithWorld && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
		{
			Texture2D joke = ModContent.Request<Texture2D>("CalamityMod/Particles/MammothParticle", (AssetRequestMode)2).Value;
			scaleMult = MathHelper.Lerp(texture.Size().X / joke.Size().X, texture.Size().Y / joke.Size().Y, 0.5f);
			texture = joke;
			AltVisual = true;
		}
		Color col = Color;
		if (AffectedByLight)
		{
			col = Lighting.GetColor((Position / 16f).ToPoint()).MultiplyRGB(Color);
		}
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color.Lerp(col, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0)), Rotation, texture.Size() * 0.5f, scale * scaleMult, (SpriteEffects)(FlipHorizontal ? 1 : 0), 0f);
		if (GlowCenter)
		{
			spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color.Lerp(Color.Lerp(col, Color.White, 0.8f), Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0)) * GlowOpacity, Rotation, texture.Size() * 0.5f, scale * 0.8f * GlowCenterScale * scaleMult, (SpriteEffects)(FlipHorizontal ? 1 : 0), 0f);
		}
	}
}
