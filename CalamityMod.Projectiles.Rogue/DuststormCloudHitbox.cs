using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(true)]
public class DuststormCloudHitbox : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = DuststormInABottle.CloudLifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 12;
	}

	public override void AI()
	{
		if (base.Projectile.scale < ((base.Projectile.ai[1] == 1f) ? DuststormInABottle.MaxSizeStealth : DuststormInABottle.MaxSize))
		{
			base.Projectile.scale += ((base.Projectile.ai[1] == 1f) ? DuststormInABottle.StealthGrowthRate : DuststormInABottle.GrowthRate);
			base.Projectile.ExpandHitboxBy(1f + ((base.Projectile.ai[1] == 1f) ? DuststormInABottle.StealthGrowthRate : DuststormInABottle.GrowthRate));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.Projectile.width, targetHitbox);
	}
}
