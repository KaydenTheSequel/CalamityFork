using System;
using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SpikecragSpike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 6);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.aiStyle = 1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.velocity.Y += 0.1f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 120);
		if (Main.zenithWorld)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		}
	}
}
