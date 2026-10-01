using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HadalUrnStarfishShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 60;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft <= 50)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 20f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 360);
	}
}
