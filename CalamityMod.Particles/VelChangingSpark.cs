using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class VelChangingSpark : Particle
{
	public Color InitialColor;

	public bool FadeIn;

	public float FadeInScale;

	public bool GlowCenter;

	public string NewTexture;

	public float ExtraRotation;

	public Vector2 Stretch;

	public float ShrinkSpeed;

	public Vector2 EndVelocity;

	public float LerpRate;

	public bool AltVisual;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool UseAdditiveBlend => AltVisual;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public VelChangingSpark(Vector2 relativePosition, Vector2 startVelocity, Vector2 endVelocity, string texture, int lifetime, float scale, Color color, Vector2 stretch, bool useAddativeBlend = true, bool glowCenter = false, float extraRotation = 0f, bool fadeIn = false, float shrinkSpeed = 0f, float lerpRate = 0.1f)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Stretch = new Vector2(0.5f, 1.6f);
		LerpRate = 0.1f;
		AltVisual = true;
		base._002Ector();
		Position = relativePosition;
		Velocity = startVelocity;
		EndVelocity = endVelocity;
		NewTexture = texture;
		ExtraRotation = extraRotation;
		AffectedByLight = false;
		Scale = scale;
		Stretch = stretch;
		FadeInScale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		ShrinkSpeed = shrinkSpeed;
		AltVisual = useAddativeBlend;
		GlowCenter = glowCenter;
		FadeIn = fadeIn;
		if (FadeIn)
		{
			Scale = 0f;
		}
		LerpRate = lerpRate;
	}

	public override void Update()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		if (!FadeIn)
		{
			if ((float)Time / (float)Lifetime < 0.5f)
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
		if ((float)Time / (float)Lifetime < 0.8f)
		{
			Velocity *= 0.95f;
			EndVelocity *= 0.95f;
		}
		Velocity = new Vector2(MathHelper.Lerp(Velocity.X, EndVelocity.X, LerpRate), MathHelper.Lerp(Velocity.Y, EndVelocity.Y, LerpRate));
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f + ExtraRotation;
		Stretch.X *= 1f - 0.2f * ShrinkSpeed;
		Stretch.Y *= 1f + 0.2f * ShrinkSpeed;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = Stretch * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(NewTexture, (AssetRequestMode)2).Value;
		Color col = Color;
		if (AffectedByLight)
		{
			col = Lighting.GetColor((Position / 16f).ToPoint()).MultiplyRGB(Color);
		}
		float scaleMult = 1f;
		if (Main.zenithWorld && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
		{
			Texture2D joke = ModContent.Request<Texture2D>("CalamityMod/Particles/MammothParticle", (AssetRequestMode)2).Value;
			scaleMult = MathHelper.Lerp(texture.Size().X / joke.Size().X, texture.Size().Y / joke.Size().Y, 0.5f);
			texture = joke;
			AltVisual = true;
		}
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, col, Rotation, texture.Size() * 0.5f, scale * scaleMult, (SpriteEffects)0, 0f);
		if (GlowCenter)
		{
			spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color.Lerp(Color.Lerp(col, Color.White, 0.8f), Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0)), Rotation, texture.Size() * 0.5f, scale * 0.8f * scaleMult, (SpriteEffects)0, 0f);
		}
	}
}
