using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CausticStaffProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 360;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 0, default(Color), 0.5f);
		Dust obj = Main.dust[fire];
		obj.velocity *= 0.1f;
		obj.scale = 1.3f;
		obj.noGravity = true;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.velocity.X *= 0.99f;
		if (base.Projectile.velocity.Y < 9f)
		{
			base.Projectile.velocity.Y += 0.085f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 6);
			dust.noGravity = true;
			dust.velocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(2f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Player obj = Main.player[base.Projectile.owner];
		Item heldItem = obj.HeldItem;
		if (CalamityUtils.ShouldTriggerSummonPenalty(obj, heldItem))
		{
			return;
		}
		int duration = Main.rand.Next(60, 181);
		switch ((int)base.Projectile.ai[0])
		{
		case 0:
			if (target.Calamity().markedForDeath)
			{
				target.AddBuff(ModContent.BuffType<MarkedforDeath>(), duration);
			}
			break;
		case 1:
			if (!target.ichor)
			{
				target.AddBuff(69, duration);
			}
			break;
		case 2:
			if (!target.venom)
			{
				target.AddBuff(70, duration);
			}
			break;
		case 3:
			if (!target.onFire2)
			{
				target.AddBuff(39, duration);
			}
			break;
		case 4:
			if (!target.onFire3)
			{
				target.AddBuff(323, duration);
			}
			break;
		}
	}
}
