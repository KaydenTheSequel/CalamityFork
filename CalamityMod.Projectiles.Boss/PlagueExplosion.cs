using System;
using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PlagueExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.15f, 0f);
		float projTimer = 25f;
		if (base.Projectile.ai[0] > 180f)
		{
			projTimer -= (base.Projectile.ai[0] - 180f) / 2f;
		}
		if (projTimer <= 0f)
		{
			projTimer = 0f;
			base.Projectile.Kill();
		}
		projTimer *= 0.7f;
		base.Projectile.ai[0] += 4f;
		for (int timerCounter = 0; (float)timerCounter < projTimer; timerCounter++)
		{
			float rando1 = (float)Main.rand.Next(-7, 8) * base.Projectile.scale;
			float rando2 = (float)Main.rand.Next(-7, 8) * base.Projectile.scale;
			float num = (float)Main.rand.Next(2, 6) * base.Projectile.scale;
			float randoAdjuster = (float)Math.Sqrt(rando1 * rando1 + rando2 * rando2);
			randoAdjuster = num / randoAdjuster;
			rando1 *= randoAdjuster;
			rando2 *= randoAdjuster;
			int greenPlague = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100);
			Main.dust[greenPlague].noGravity = true;
			Main.dust[greenPlague].position.X = base.Projectile.Center.X;
			Main.dust[greenPlague].position.Y = base.Projectile.Center.Y;
			Main.dust[greenPlague].position.X += Main.rand.Next(-10, 11);
			Main.dust[greenPlague].position.Y += Main.rand.Next(-10, 11);
			Main.dust[greenPlague].velocity.X = rando1;
			Main.dust[greenPlague].velocity.Y = rando2;
			Main.dust[greenPlague].scale = base.Projectile.scale * 0.35f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Plague>(), 90);
		}
	}
}
