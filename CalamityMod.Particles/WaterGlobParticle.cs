using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class WaterGlobParticle : Particle
{
	private float Spin;

	public override string Texture => "CalamityMod/Particles/WaterGlob";

	public override bool SetLifetime => true;

	public override bool Important => true;

	public WaterGlobParticle(Vector2 position, Vector2 velocity, float scale, float rotationSpeed = 0f, int lifeTime = 50)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = Color.White * 0.5f;
		Scale = scale;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Lifetime = lifeTime;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		Color *= 0.985f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Velocity *= 0.85f;
		Scale *= 0.975f;
	}
}
