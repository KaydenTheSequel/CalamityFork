using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class RainbowGlowSparkParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public bool QuickShrink;

	public bool Glowing;

	public float ShrinkSpeed;

	public Vector2 Squash;

	public float HueShift;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => true;

	public override string Texture => "CalamityMod/Particles/GlowSpark";

	public RainbowGlowSparkParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color initialColor, Vector2 squash, bool quickShrink = false, bool glow = true, float shrinkSpeed = 1f, float hueShift = 0f)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		ShrinkSpeed = 1f;
		Squash = new Vector2(0.5f, 1.6f);
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		AffectedByGravity = affectedByGravity;
		Scale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = initialColor);
		Squash = squash;
		QuickShrink = quickShrink;
		Glowing = glow;
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
		ShrinkSpeed = shrinkSpeed;
		HueShift = hueShift;
	}

	public override void Update()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		Scale *= 0.95f;
		Velocity *= 0.95f;
		if (QuickShrink)
		{
			if (ShrinkSpeed == 1f)
			{
				Squash.X *= 0.8f;
				Squash.Y *= 1.2f;
			}
			else
			{
				Squash.X *= 1f - 0.2f * ShrinkSpeed;
				Squash.Y *= 1f + 0.2f * ShrinkSpeed;
			}
		}
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		Color = Main.hslToRgb(Main.rgbToHsl(Color).X + HueShift, Main.rgbToHsl(Color).Y, Main.rgbToHsl(Color).Z);
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = Squash * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		float scaleMult = 1f;
		if (Main.zenithWorld && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
		{
			Texture2D joke = ModContent.Request<Texture2D>("CalamityMod/Particles/MammothParticle", (AssetRequestMode)2).Value;
			scaleMult = MathHelper.Lerp(texture.Size().X / joke.Size().X, texture.Size().Y / joke.Size().Y, 0.5f);
			texture = joke;
		}
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, texture.Size() * 0.5f, scale * scaleMult, (SpriteEffects)0, 0f);
		if (Glowing)
		{
			spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color.Lerp(Color.White, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0)), Rotation, texture.Size() * 0.5f, scale * new Vector2(0.45f, 1f) * scaleMult, (SpriteEffects)0, 0f);
		}
	}
}
