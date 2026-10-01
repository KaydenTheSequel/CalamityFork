using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NorfleetExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 500;
		base.Projectile.height = 500;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 2;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.ai[1] == 0f)
		{
			target.AddBuff(189, 300);
		}
		if (base.Projectile.ai[1] == 1f)
		{
			target.AddBuff(144, 300);
		}
		if (base.Projectile.ai[1] == 2f)
		{
			target.AddBuff(ModContent.BuffType<Plague>(), 300);
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void AI()
	{
		if (base.Projectile.ai[2] == 1f)
		{
			base.Projectile.friendly = false;
			base.Projectile.hostile = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 400f, targetHitbox);
	}
}
