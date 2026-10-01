using System;
using CalamityMod.Buffs.StatDebuffs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ScourgeVenomCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 3600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 45;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.ai[0] < 219f)
		{
			if (base.Projectile.frame >= 4)
			{
				base.Projectile.frame = 0;
			}
		}
		else if (base.Projectile.owner == Main.myPlayer && base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		if (base.Projectile.alpha > 110)
		{
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha < 110)
			{
				base.Projectile.alpha = 110;
			}
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.1f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), base.Projectile.Calamity().stealthStrike ? 120 : 60);
		if (base.Projectile.ai[1] == 1f && base.Projectile.owner == Main.myPlayer)
		{
			target.AddBuff(70, 120);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), base.Projectile.Calamity().stealthStrike ? 120 : 60);
		if (base.Projectile.ai[1] == 1f && base.Projectile.owner == Main.myPlayer)
		{
			target.AddBuff(70, 120);
		}
	}
}
