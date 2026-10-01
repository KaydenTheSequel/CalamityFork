using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class SeaPrismParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public bool FadeIn;

	public float FadeInScale;

	public string NewTexture;

	public float ActiveRotation;

	public float AddedRotation;

	public float SpeedReduction;

	public Vector2 Stretch;

	public float ShrinkSpeed;

	public bool AltVisual;

	public override string Texture => "CalamityMod/Particles/SeaPrisms";

	public override bool UseAdditiveBlend => AltVisual;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override int FrameVariants => 3;

	public SeaPrismParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, Vector2 stretch, bool useAddativeBlend = false, float activeRotation = 0f, float speedReduction = 0.95f, bool fadeIn = false, bool affectedByLight = true, float shrinkSpeed = 0f)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		SpeedReduction = 0.95f;
		Stretch = Vector2.One;
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		ActiveRotation = activeRotation;
		AffectedByGravity = affectedByGravity;
		AffectedByLight = affectedByLight;
		Scale = scale;
		Stretch = stretch;
		FadeInScale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		ShrinkSpeed = shrinkSpeed;
		SpeedReduction = speedReduction;
		AltVisual = useAddativeBlend;
		FadeIn = fadeIn;
		if (FadeIn)
		{
			Scale = 0f;
		}
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
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
		Velocity *= SpeedReduction;
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		float velRot = ((ActiveRotation == 0f) ? (Velocity.ToRotation() + (float)Math.PI / 2f) : ActiveRotation);
		Rotation = velRot + AddedRotation;
		AddedRotation += ActiveRotation;
		ActiveRotation *= 0.975f;
		Stretch.X *= 1f - 0.2f * ShrinkSpeed;
		Stretch.Y *= 1f + 0.2f * ShrinkSpeed;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = GeneralParticleHandler.GetTexture(Type);
		int frameWidth = 16;
		int frameHeight = 32;
		int frameSpacing = frameWidth + 2;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameSpacing * Variant, 0, frameWidth, frameHeight);
		Vector2 scale = Stretch * Scale;
		Color col = Color;
		if (AffectedByLight)
		{
			col = Lighting.GetColor((Position / 16f).ToPoint()).MultiplyRGB(Color);
		}
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)frame, col, Rotation, frame.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}
}
