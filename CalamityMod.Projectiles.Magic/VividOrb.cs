using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VividOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		CalamityUtils.MagnetSphereHitscan(base.Projectile, 300f, 6f, 24f, 5, ModContent.ProjectileType<VividBolt>(), 1.0, attackMultiple: true);
	}
}
