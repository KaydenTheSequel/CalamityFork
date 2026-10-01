using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Tiles.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class RustyChest : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<RustyChestTile>());
		base.Item.value = Item.sellPrice(0, 0, 10);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<global::CalamityMod.Items.Placeables.Abyss.HardenedSulphurousSandstone>(8).AddRecipeGroup("IronBar", 2).AddTile(16)
			.Register();
	}
}
