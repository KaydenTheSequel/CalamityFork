using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HydraHeadLaunch : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Hydra";

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 360;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (!(base.Projectile.ai[1] < 1f))
		{
			return null;
		}
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(base.Projectile.ai[0]);
		base.Projectile.ai[0] += 12f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GalvanicCorrosion>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GalvanicCorrosion>(), 60);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1] = 1f;
		base.Projectile.ExpandHitboxBy(180);
		base.Projectile.Damage();
		float randAngle = Main.rand.NextFloat(1f, 2f);
		for (int i = 0; i < 8; i++)
		{
			float angle = (float)Math.PI * randAngle - (float)i * ((float)Math.PI * 2f) / 8f;
			float f = (float)Math.PI * randAngle - (float)(i + 2) * ((float)Math.PI * 2f) / 8f;
			Vector2 start = angle.ToRotationVector2();
			Vector2 end = f.ToRotationVector2();
			for (int j = 0; j < 30; j++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.scale = 1.5f;
				dust.velocity = Vector2.Lerp(start, end, (float)j / 30f) * 12f;
				dust.color = Color.Orchid;
				dust.noGravity = true;
			}
		}
		SoundEngine.PlaySound(in CommonCalamitySounds.LargeWeaponFireSound, base.Projectile.Center);
	}
}
