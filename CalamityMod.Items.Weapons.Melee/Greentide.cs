using System;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Greentide : ModItem, ILocalizedModType, IModType
{
	internal const float ShootSpeed = 32f;

	internal const float TeethSpread = 960f;

	internal const float HalvedTeethSpread = 480f;

	internal const int TotalRows = 2;

	internal const int TotalTeeth = 3;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 62;
		base.Item.damage = 81;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = (base.Item.useAnimation = 30);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<GreenWater>();
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 10f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		for (int i = -2; i <= 2; i++)
		{
			if (i == 0)
			{
				Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity * 1.2f, type, damage, knockback, player.whoAmI, 2f);
				projectile.penetrate = -1;
				projectile.timeLeft = 150;
			}
			else
			{
				Projectile.NewProjectile(source, position, velocity.RotatedBy(0.1f * (float)i).RotatedByRandom(0.05999999865889549) * (1f - (float)(Math.Abs(i) - 1) * 0.3f), type, (int)((float)damage * 0.5f), knockback / 3f, player.whoAmI, 1f);
			}
		}
		return false;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		NPC target2 = player.Calamity().mouseWorld.ClosestNPCAt(300f);
		if (target2 == null)
		{
			target2 = target;
		}
		Vector2 center = target.Center;
		Vector2 position = default(Vector2);
		((Vector2)(ref position))._002Ector(target2.Center.X, target2.Center.Y - 350f);
		Vector2 cachedPosition = position;
		Vector2 secondPosition = default(Vector2);
		((Vector2)(ref secondPosition))._002Ector(target2.Center.X, target2.Center.Y + 350f);
		Vector2 secondCachedPosition = secondPosition;
		Vector2 velocity = (center - position).SafeNormalize(Vector2.UnitY) * 32f;
		Vector2 cachedVelocity = velocity;
		Vector2 secondVelocity = (center - secondPosition).SafeNormalize(Vector2.UnitY) * 32f;
		Vector2 secondCachedVelocity = secondVelocity;
		int teethDamage = player.CalcIntDamage<MeleeDamageClass>((int)((float)base.Item.damage * 0.1f));
		float teethKnockback = base.Item.knockBack * 0.2f;
		int centralProjectile = 1;
		float teethXVelocityReduction = 0.9f;
		float minVelocityAdjustment = 0.8f;
		float maxVelocityAdjustment = 1f;
		for (int i = 0; i < 2; i++)
		{
			bool topTeeth = i == 0;
			for (int j = 0; j < 3; j++)
			{
				float velocityAdjustment = ((j == centralProjectile) ? minVelocityAdjustment : MathHelper.Lerp(minVelocityAdjustment, maxVelocityAdjustment, Math.Abs((float)j + 0.5f - (float)centralProjectile) / (float)centralProjectile));
				if (topTeeth)
				{
					position.X += MathHelper.Lerp(-144f, 144f, (float)j / 2f);
					velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(position, target2, 32f, 1) * velocityAdjustment;
					velocity.X *= teethXVelocityReduction;
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), position, velocity * 0.25f, ModContent.ProjectileType<GreenWater>(), teethDamage, teethKnockback, player.whoAmI, 0f, i, target2.Center.Y);
					position = cachedPosition;
					velocity = cachedVelocity;
				}
				else
				{
					secondPosition.X += MathHelper.Lerp(-144f, 144f, (float)j / 2f);
					secondVelocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(secondPosition, target2, 32f, 1) * velocityAdjustment;
					secondVelocity.X *= teethXVelocityReduction;
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), secondPosition, secondVelocity * 0.25f, ModContent.ProjectileType<GreenWater>(), teethDamage, teethKnockback, player.whoAmI, 0f, i, target2.Center.Y);
					secondPosition = secondCachedPosition;
					secondVelocity = secondCachedVelocity;
				}
			}
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = target.Center;
		Vector2 position = destination - Vector2.UnitY * (destination.Y - Main.screenPosition.Y + 80f);
		Vector2 cachedPosition = position;
		Vector2 secondPosition = cachedPosition + Vector2.UnitY * ((float)Main.screenHeight + 160f);
		Vector2 secondCachedPosition = secondPosition;
		Vector2 velocity = (destination - position).SafeNormalize(Vector2.UnitY) * 32f;
		Vector2 cachedVelocity = velocity;
		Vector2 secondVelocity = (destination - secondPosition).SafeNormalize(Vector2.UnitY) * 32f;
		Vector2 secondCachedVelocity = secondVelocity;
		int teethDamage = player.CalcIntDamage<MeleeDamageClass>((int)((float)base.Item.damage * 0.1f));
		float teethKnockback = base.Item.knockBack * 0.2f;
		int centralProjectile = 1;
		float teethXVelocityReduction = 0.9f;
		float minVelocityAdjustment = 0.8f;
		float maxVelocityAdjustment = 1f;
		for (int i = 0; i < 2; i++)
		{
			bool topTeeth = i == 0;
			for (int j = 0; j < 3; j++)
			{
				float velocityAdjustment = ((j == centralProjectile) ? minVelocityAdjustment : MathHelper.Lerp(minVelocityAdjustment, maxVelocityAdjustment, Math.Abs((float)j + 0.5f - (float)centralProjectile) / (float)centralProjectile));
				if (topTeeth)
				{
					position.X += MathHelper.Lerp(-480f, 480f, (float)j / 2f);
					velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(position, target, 32f, 1) * velocityAdjustment;
					velocity.X *= teethXVelocityReduction;
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), position, velocity, ModContent.ProjectileType<GreenWater>(), teethDamage, teethKnockback, player.whoAmI, 0f, i, target.Center.Y);
					position = cachedPosition;
					velocity = cachedVelocity;
				}
				else
				{
					secondPosition.X += MathHelper.Lerp(-480f, 480f, (float)j / 2f);
					secondVelocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(secondPosition, target, 32f, 1) * velocityAdjustment;
					secondVelocity.X *= teethXVelocityReduction;
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), secondPosition, secondVelocity, ModContent.ProjectileType<GreenWater>(), teethDamage, teethKnockback, player.whoAmI, 0f, i, target.Center.Y);
					secondPosition = secondCachedPosition;
					secondVelocity = secondCachedVelocity;
				}
			}
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dustVel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.9f, 1.5f);
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 102, dustVel.X, dustVel.Y);
		}
	}
}
