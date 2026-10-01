using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ChronomancersScythe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 45;
		base.Item.height = 45;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.damage = 159;
		base.Item.knockBack = 4f;
		base.Item.useAnimation = 25;
		base.Item.useTime = 5;
		base.Item.autoReuse = false;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item71;
		base.Item.mana = 10;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<ChronomancersScytheSwing>();
		base.Item.shootSpeed = 24f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			int p = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ChronomancersScytheHoldout>(), damage, knockback, Main.myPlayer, 0f, 0f, player.direction);
			float rot = -(float)Math.PI / 4f;
			Main.projectile[p].rotation = (float)player.direction * rot;
			return false;
		}
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if ((float)player.altFunctionUse == 2f)
		{
			base.Item.reuseDelay = 10;
			base.Item.channel = false;
			base.Item.useTurn = false;
		}
		else
		{
			base.Item.reuseDelay = 0;
			base.Item.channel = true;
			base.Item.useTurn = true;
		}
		return base.CanUseItem(player);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1306).AddIngredient(889).AddIngredient(3467, 10)
			.AddIngredient<EssenceofEleum>(6)
			.AddTile(134)
			.Register();
	}
}
