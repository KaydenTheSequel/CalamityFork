using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class BoltParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public bool FadeIn;

	public float FadeInScale;

	public bool GlowCenter;

	public Vector2 Stretch;

	public float ShrinkSpeed;

	public bool Fliped;

	public bool GlowFade;

	public override int FrameVariants => 3;

	public override string Texture => "CalamityMod/Particles/Bolt2";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public BoltParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, Vector2 stretch, bool glowCenter = false, bool glowFade = false, bool fadeIn = false, float shrinkSpeed = 0f)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Stretch = new Vector2(0.5f, 1.6f);
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		AffectedByGravity = affectedByGravity;
		Scale = scale;
		Stretch = stretch;
		FadeInScale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		ShrinkSpeed = shrinkSpeed;
		GlowCenter = glowCenter;
		Variant = Main.rand.Next(3);
		Fliped = Main.rand.NextBool();
		FadeIn = fadeIn;
		if (FadeIn)
		{
			Scale = 0f;
		}
		GlowFade = glowFade;
	}

	public override void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		if (!FadeIn)
		{
			Scale *= 0.95f;
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
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
		Stretch.X *= 1f - 0.2f * ShrinkSpeed;
		Stretch.Y *= 1f + 0.2f * ShrinkSpeed;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = Stretch * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Rectangle frame = texture.Frame(1, 3, 0, Variant);
		Vector2 origin = frame.Size() * 0.5f;
		Color col = Color;
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)frame, col, Rotation, origin, scale, (SpriteEffects)(Fliped ? 1 : 0), 0f);
		if (GlowCenter)
		{
			spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)frame, Color.Lerp(col, Color.White, 0.8f) * (GlowFade ? Utils.GetLerpValue(Lifetime, 0f, Time, clamped: true) : 1f), Rotation, origin, scale * 0.8f, (SpriteEffects)(Fliped ? 1 : 0), 0f);
		}
	}
}
