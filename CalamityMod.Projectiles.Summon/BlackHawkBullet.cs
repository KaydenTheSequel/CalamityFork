using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BlackHawkBullet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 0;
		base.Projectile.extraUpdates = 4;
		base.Projectile.scale = 1.18f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.AIType = 242;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}
}
