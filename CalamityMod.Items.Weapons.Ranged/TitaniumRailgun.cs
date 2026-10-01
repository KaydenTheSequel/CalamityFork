using System;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TitaniumRailgun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 32;
		base.Item.damage = 370;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 26;
		base.Item.useAnimation = 26;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item77 with
		{
			Volume = SoundID.Item77.Volume * 0.7f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<TitaniumRailgunScope>();
		base.Item.shootSpeed = 16f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 60f * player.GetWeaponAttackSpeed(player.HeldItem));
		return false;
	}

	public override void UseItemFrame(Player player)
	{
		float armPointingDirection = player.itemRotation;
		if (player.direction < 0)
		{
			armPointingDirection += (float)Math.PI;
		}
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1198, 10).AddIngredient(502, 5).AddTile(134)
			.Register();
	}
}
