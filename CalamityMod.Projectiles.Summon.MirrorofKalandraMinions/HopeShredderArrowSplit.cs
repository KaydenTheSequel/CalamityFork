using CalamityMod.Items.Weapons.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;

public class HopeShredderArrowSplit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = MirrorofKalandra.Vile_SplitIFrames;
		base.Projectile.penetrate = 3;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}
}
