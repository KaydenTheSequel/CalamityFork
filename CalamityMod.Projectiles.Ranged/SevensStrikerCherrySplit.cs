using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SevensStrikerCherrySplit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 150;
	}

	public override void AI()
	{
		base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		base.Projectile.velocity.Y += 0.2f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}
}
