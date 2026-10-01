using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AMR2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/AMRShot";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 10;
		base.Projectile.scale = 1.18f;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}
}
