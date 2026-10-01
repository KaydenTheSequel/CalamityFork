using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class DesertProwlerSkullParticle : Particle
{
	private float Opacity;

	private Color ColorStart;

	private Color ColorFade;

	private float BaseOpacity;

	public CalamityUtils.CurveSegment UpsquashSegment;

	public CalamityUtils.CurveSegment DownsquashSegment;

	public CalamityUtils.CurveSegment BumpSquashSegment;

	public CalamityUtils.CurveSegment BumpSquash2Segment;

	public CalamityUtils.CurveSegment BumpSquash3Segment;

	public CalamityUtils.CurveSegment StaySegment;

	public override string Texture => "CalamityMod/Particles/DesertProwlerSkull";

	public override bool UseCustomDraw => true;

	public override bool UseHalfTransparency => true;

	internal float Squash => CalamityUtils.PiecewiseAnimation(1f - Opacity / BaseOpacity, UpsquashSegment, DownsquashSegment, BumpSquashSegment, BumpSquash2Segment, BumpSquash3Segment, StaySegment);

	public DesertProwlerSkullParticle(Vector2 position, Vector2 velocity, Color colorStart, Color colorFade, float scale, float opacity)
	{
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		UpsquashSegment = new CalamityUtils.CurveSegment(CalamityUtils.PolyOutEasing, 0f, 1f, 0.2f, 2);
		DownsquashSegment = new CalamityUtils.CurveSegment(CalamityUtils.PolyInEasing, 0.2f, 1.2f, -0.3f, 2);
		BumpSquashSegment = new CalamityUtils.CurveSegment(CalamityUtils.SineOutEasing, 0.3f, 0.9f, -0.05f);
		BumpSquash2Segment = new CalamityUtils.CurveSegment(CalamityUtils.SineInEasing, 0.6f, 0.85f, 0.15f);
		BumpSquash3Segment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.76f, 1f, 0.05f);
		StaySegment = new CalamityUtils.CurveSegment(CalamityUtils.LinearEasing, 0.9f, 1f, 0f);
		base._002Ector();
		Position = position;
		Velocity = velocity;
		ColorStart = colorStart;
		ColorFade = colorFade;
		Scale = scale;
		Opacity = opacity;
		BaseOpacity = opacity;
		Rotation = Main.rand.NextFloat((float)Math.PI / 8f) - (float)Math.PI / 16f;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		Velocity *= 0.97f;
		if (Opacity > 90f)
		{
			Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * 0.1f);
			Scale += 0.01f;
			Opacity -= 3f;
		}
		else
		{
			Scale *= 1.01f;
			Opacity -= 2f;
		}
		if (Opacity < 0f)
		{
			Kill();
		}
		Color = Color.Lerp(ColorStart, ColorFade, MathHelper.Clamp((255f - Opacity - 100f) / 80f, 0f, 1f)) * (Opacity / 255f);
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = GeneralParticleHandler.GetTexture(Type);
		Vector2 size = Scale * new Vector2(1f - (Squash - 1f) * 0.8f, 1f + (Squash - 1f) * 0.8f);
		spriteBatch.Draw(sprite, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, sprite.Size() / 2f, size, (SpriteEffects)0, 0f);
	}
}
