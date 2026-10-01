using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class YinYoLight : ModProjectile, ILocalizedModType, IModType
{
	private const int speedTimerMax = 30;

	private int speedTimer;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "Terraria/Images/Item_528";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.5f;
		speedTimer--;
		if (speedTimer <= 0)
		{
			speedTimer = 30;
			Projectile projectile = base.Projectile;
			projectile.velocity *= -1f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
