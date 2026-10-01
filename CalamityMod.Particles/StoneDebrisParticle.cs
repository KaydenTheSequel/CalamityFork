using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class StoneDebrisParticle : Particle
{
	private float Spin;

	private float opacity;

	private Color originalColor;

	public override string Texture => "CalamityMod/Particles/StoneDebris";

	public override bool SetLifetime => true;

	public override int FrameVariants => 5;

	public StoneDebrisParticle(Vector2 position, Vector2 velocity, Color color, float scale, int lifeTime, float rotationSpeed = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		originalColor = color;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		opacity = ((base.LifetimeCompletion < 0.5f) ? 1f : ((float)Math.Sin(base.LifetimeCompletion * (float)Math.PI)));
		Color = originalColor * opacity;
		Velocity = Velocity * 0.95f + Vector2.UnitY * 0.1f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
	}
}
