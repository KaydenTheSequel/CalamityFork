using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class ZenithThrone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.ZenithThrone>());
		base.Item.value = Item.sellPrice(0, 10);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4956).AddRecipeGroup(RecipeGroupID.IronBar, 10).AddTile(16)
			.Register();
	}
}
