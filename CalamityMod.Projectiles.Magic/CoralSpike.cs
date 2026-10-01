using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CoralSpike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float ChargeCompletion => ref base.Projectile.ai[0];

	public static int DustPick
	{
		get
		{
			if (!Main.rand.NextBool())
			{
				if (!Main.rand.NextBool())
				{
					if (!Main.rand.NextBool())
					{
						return 280;
					}
					return 281;
				}
				return 282;
			}
			return 255;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 360;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 2f)
		{
			base.Projectile.Kill();
		}
		int dustOpacity = (int)(200f * (1f - ChargeCompletion));
		float dustScale = Main.rand.NextFloat(1f, 1.4f) * (0.6f + 0.4f * ChargeCompletion);
		Vector2 dustVelocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(0.4712389409542084) * 4f + Main.rand.NextVector2Circular(4f, 4f);
		Vector2 center = base.Projectile.Center;
		int dustPick = DustPick;
		Vector2? velocity = dustVelocity;
		float scale = dustScale;
		Dust.NewDustPerfect(center, dustPick, velocity, dustOpacity, default(Color), scale).noGravity = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		if (timeLeft != 0)
		{
			SoundStyle style = SoundID.Dig with
			{
				Volume = SoundID.Dig.Volume * 0.4f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		for (int i = 0; i < 3; i++)
		{
			float angle = Main.rand.NextFloat((float)Math.PI * 2f);
			GeneralParticleHandler.SpawnParticle(new UrchinSpikeParticle(base.Projectile.Center + angle.ToRotationVector2() * 2f, angle.ToRotationVector2() * 6f, angle + (float)Math.PI / 2f, Main.rand.NextFloat(1f, 1.3f), 1f, Main.rand.Next(10) + 25));
		}
		int dustCount = Main.rand.Next(7);
		for (int j = 0; j < dustCount; j++)
		{
			int dustOpacity = (int)(200f * (1f - ChargeCompletion));
			float dustScale = Main.rand.NextFloat(1f, 1.4f) * (0.6f + 0.4f * ChargeCompletion);
			Vector2 center = base.Projectile.Center;
			int dustPick = DustPick;
			Vector2? velocity = Main.rand.NextVector2Circular(7f, 7f);
			float scale = dustScale;
			Dust.NewDustPerfect(center, dustPick, velocity, dustOpacity, default(Color), scale).noGravity = true;
		}
	}
}
