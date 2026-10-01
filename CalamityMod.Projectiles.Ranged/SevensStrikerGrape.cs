using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SevensStrikerGrape : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		base.Projectile.rotation += 0.1f * (float)base.Projectile.direction;
	}
}
