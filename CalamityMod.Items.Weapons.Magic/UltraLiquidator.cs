using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class UltraLiquidator : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 70);
		base.Item.damage = 120;
		base.Item.knockBack = 5f;
		base.Item.useTime = 3;
		base.Item.reuseDelay = (base.Item.useAnimation = 15);
		base.Item.useLimitPerAnimation = 5;
		base.Item.mana = 25;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 16f;
		base.Item.shoot = ModContent.ProjectileType<LiquidBlade>();
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item9;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 30f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo projSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float speed = base.Item.shootSpeed;
		float xVec = (float)Main.mouseX + Main.screenPosition.X - playerPos.X;
		float yVec = (float)Main.mouseY + Main.screenPosition.Y - playerPos.Y;
		float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
		float lowerBoundOffset = 20f;
		float upperBoundOffset = 60f;
		Vector2 source = playerPos + f.ToRotationVector2() * MathHelper.Lerp(lowerBoundOffset, upperBoundOffset, Main.rand.NextFloat());
		for (int i = 0; i < 50; i++)
		{
			source = playerPos + f.ToRotationVector2() * MathHelper.Lerp(lowerBoundOffset, upperBoundOffset, Main.rand.NextFloat());
			if (Collision.CanHit(playerPos, 0, 0, source + (source - playerPos).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
			{
				break;
			}
			f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
		}
		Vector2 velocityReal = Main.MouseWorld - source;
		Vector2 upperVelocityLimit = Utils.SafeNormalize(new Vector2(xVec, yVec), Vector2.UnitY) * speed;
		velocityReal = velocityReal.SafeNormalize(upperVelocityLimit) * speed;
		velocityReal = Vector2.Lerp(velocityReal, upperVelocityLimit, 0.25f);
		Projectile.NewProjectile(projSource, source, velocityReal, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<InfernalRift>().AddIngredient(157).AddIngredient(3467, 5)
			.AddIngredient<GalacticaSingularity>(5)
			.AddTile(134)
			.Register();
	}
}
