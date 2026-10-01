using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "GrandStaffoftheNebulaMage" })]
public class NebulousCataclysm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 120;
		base.Item.height = 124;
		base.Item.damage = 530;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 70;
		base.Item.useTime = (base.Item.useAnimation = 30);
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<NebulousCataclysm_Held>();
		base.Item.shootSpeed = 5f;
	}

	public override void OnConsumeMana(Player player, int manaConsumed)
	{
		player.statMana += manaConsumed;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3476).AddIngredient(3542).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<NightmareFuel>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
