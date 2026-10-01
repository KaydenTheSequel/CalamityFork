using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class ManaDrainBlob : Particle
{
	public Player Owner;

	public override bool SetLifetime => false;

	public override string Texture => "CalamityMod/Particles/MicroBloom";

	public override bool UseAdditiveBlend => true;

	public override int FrameVariants => 2;

	public ManaDrainBlob(Player owner, Vector2 position, Vector2 speed, float scale, Color color)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Owner = owner;
		Position = position;
		Scale = Math.Min(scale, 1f);
		Color = color;
		Velocity = speed;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Variant = Main.rand.Next(2);
	}

	public override void Update()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		Scale -= 0.02f;
		if (Scale < 0.1f)
		{
			Kill();
		}
		Scale += 0.007f;
		if (Owner != null && Owner.active)
		{
			Vector2 speedDirection = Position - Owner.Center;
			float distanceToOwner = ((Vector2)(ref speedDirection)).Length();
			float distanceToOwnerTwisted = 100f - distanceToOwner;
			if (distanceToOwnerTwisted > 0f)
			{
				Scale -= distanceToOwnerTwisted * 0.0015f;
			}
			((Vector2)(ref speedDirection)).Normalize();
			float dustAcceleration = (1f - Scale) * -20f;
			speedDirection *= dustAcceleration;
			Velocity = (Velocity * 4f + speedDirection) / 5f;
			if (distanceToOwner > 16f && Main.rand.NextBool(7))
			{
				float velocityMultiplier = MathHelper.Lerp(0.05f, 1f, MathHelper.Clamp((distanceToOwner - 10f) / 40f, 0f, 1f));
				Vector2 position = Position;
				Vector2? velocity = Velocity * Main.rand.NextFloat(0.7f, 1.2f) * velocityMultiplier;
				float scale = Main.rand.NextFloat(1.2f, 1.8f);
				Dust dust = Dust.NewDustPerfect(position, 15, velocity, 100, default(Color), scale);
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
		Rotation += 0.04f * ((Velocity.X > 0f) ? 1f : (-1f));
	}
}
