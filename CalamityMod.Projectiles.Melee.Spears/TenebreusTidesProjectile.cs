using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class TenebreusTidesProjectile : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TenebreusTides>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.8f;

	public override Action<Projectile> EffectBeforeReelback => delegate
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + base.Projectile.velocity.X, base.Projectile.Center.Y + base.Projectile.velocity.Y, base.Projectile.velocity.X * 2.4f, base.Projectile.velocity.Y * 2.4f, ModContent.ProjectileType<TenebreusTidesWaterProjectile>(), (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.85f, base.Projectile.owner);
	};

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 46);
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
	}

	public override void ExtraBehavior()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 33, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 300);
		SwordSpam(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 300);
		SwordSpam(target.Center);
	}

	public void SwordSpam(Vector2 targetPos)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = 3;
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int i = 0; i < projAmt; i++)
		{
			int type = (Main.rand.NextBool() ? ModContent.ProjectileType<TenebreusTidesWaterSword>() : ModContent.ProjectileType<TenebreusTidesWaterSpear>());
			if (base.Projectile.owner == Main.myPlayer)
			{
				CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 1000f, 1400f, 80f, 900f, Main.rand.NextFloat(25f, 35f), type, base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
	}
}
