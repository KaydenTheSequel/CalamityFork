using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class HellionFlowerSpearProjectile : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<HellionFlowerSpear>();

	public override float InitialSpeed => 3f;

	public override float ReelbackSpeed => 2.4f;

	public override float ForwardSpeed => 0.8f;

	public override Action<Projectile> EffectBeforeReelback => delegate
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 2f, ModContent.ProjectileType<HellionSpike>(), base.Projectile.damage, base.Projectile.knockBack * 0.85f, base.Projectile.owner);
	};

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, hit.Crit);
		target.AddBuff(70, 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, crit: true);
		target.AddBuff(70, 300);
	}

	private void OnHitEffects(Vector2 targetPos, bool crit)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!crit)
		{
			return;
		}
		IEntitySource source = base.Projectile.GetSource_FromThis();
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile petal = CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 800f, 800f, 0f, 800f, 10f, 221, (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack * 0.5f, base.Projectile.owner, clamped: true);
			if (petal.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				petal.DamageType = DamageClass.Melee;
				petal.localNPCHitCooldown = -1;
			}
		}
	}
}
