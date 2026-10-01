using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class RelicofRuin : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 40;
		base.Item.damage = 21;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 16;
		base.Item.useTime = 35;
		base.Item.useAnimation = 35;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.25f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item84;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ForbiddenAxeBlade>();
		base.Item.shootSpeed = 5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		int totalProjectiles = 12;
		float radians = (float)Math.PI * 2f / (float)totalProjectiles;
		for (int i = 0; i < totalProjectiles; i++)
		{
			Vector2 vector = Utils.RotatedBy(new Vector2(0f, 0f - base.Item.shootSpeed), (double)(radians * (float)i), default(Vector2));
			Projectile.NewProjectile(source, position, vector, type, damage, knockback, Main.myPlayer);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddRecipeGroup("AnyAdamantiteBar", 5).AddIngredient(3783, 2)
			.AddTile(101)
			.Register();
	}
}
