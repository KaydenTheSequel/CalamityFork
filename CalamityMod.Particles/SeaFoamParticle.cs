using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class SeaFoamParticle : Particle
{
	public float Opacity;

	private Color ColorStart;

	private Color ColorFade;

	private float Spin;

	private float BaseScale;

	public override string Texture => "CalamityMod/Particles/SeaFoam";

	public override int FrameVariants => 3;

	public override bool UseAdditiveBlend => true;

	public SeaFoamParticle(Vector2 position, Vector2 velocity, Color colorStart, Color colorFade, float scale, float opacity, float rotationSpeed = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		ColorStart = colorStart;
		ColorFade = colorFade;
		BaseScale = scale;
		Opacity = opacity;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		Velocity *= 0.85f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Opacity -= 10f;
		Scale = BaseScale + Opacity / 255f;
		if (Opacity <= 0f)
		{
			Kill();
		}
		Color = Color.Lerp(ColorStart, ColorFade, MathHelper.Clamp((255f - Opacity - 100f) / 80f, 0f, 1f)) * (Opacity / 255f);
	}
}
