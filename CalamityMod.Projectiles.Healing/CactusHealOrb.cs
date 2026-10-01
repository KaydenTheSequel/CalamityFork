using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class CactusHealOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.Y *= 0.98f;
		base.Projectile.HealingProjectile(15, base.Projectile.owner, 12f, 15f, autoHomes: false);
		int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 100, new Color(0, 200, 0), 1.5f);
		Dust obj = Main.dust[dusty];
		obj.noGravity = true;
		obj.position.X -= base.Projectile.velocity.X * 0.2f;
		obj.position.Y += base.Projectile.velocity.Y * 0.2f;
	}
}
