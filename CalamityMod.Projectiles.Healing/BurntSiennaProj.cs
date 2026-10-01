using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class BurntSiennaProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.95f;
		base.Projectile.HealingProjectile(3, (int)base.Projectile.ai[0], 6f, 15f, autoHomes: false);
		int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100);
		Dust obj = Main.dust[dusty];
		obj.noGravity = true;
		obj.position.X -= base.Projectile.velocity.X * 0.2f;
		obj.position.Y += base.Projectile.velocity.Y * 0.2f;
	}
}
