using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BlazingSun2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 130;
		base.Projectile.height = 130;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.Projectile.timeLeft = 30;
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft < 18)
		{
			base.Projectile.scale -= 0.05f;
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha += 10;
			}
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
			}
		}
		if (base.Projectile.timeLeft >= 30)
		{
			base.Projectile.scale += 0.6f;
		}
		base.Projectile.rotation -= 0.025f;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
