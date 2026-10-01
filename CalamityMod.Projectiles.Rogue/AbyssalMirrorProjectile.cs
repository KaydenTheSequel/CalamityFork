using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AbyssalMirrorProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 50;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft < 25)
		{
			base.Projectile.alpha += 10;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!target.friendly)
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 120);
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		}
	}
}
