using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class TimedSmokeParticle : Particle
{
	private float Opacity;

	private Color ColorStart;

	private Color ColorFade;

	private float Spin;

	public override string Texture => "CalamityMod/Particles/MediumSmoke";

	public override bool SetLifetime => true;

	public override int FrameVariants => 3;

	public TimedSmokeParticle(Vector2 position, Vector2 velocity, Color colorStart, Color colorFade, float scale, float opacity, int timeleft, float rotationSpeed = 0f)
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
		Scale = scale;
		Opacity = opacity;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Lifetime = timeleft;
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Velocity *= 0.85f;
		if (base.LifetimeCompletion < 0.84f)
		{
			Scale += 0.01f;
		}
		else
		{
			Scale *= 0.975f;
		}
		Velocity -= Vector2.UnitY * 0.08f;
		float opacityMult = 1f - (float)Math.Pow(base.LifetimeCompletion, 2.0);
		Color = Color.Lerp(ColorStart, ColorFade, opacityMult) * (Opacity * opacityMult);
	}
}
