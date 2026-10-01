using CalamityMod.Items.Materials;
using CalamityMod.Tiles.FurnitureWulfrum;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum;

public class WulfrumWallMountedBulb : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.WulfrumWallMountedBulb>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient<AnodizedWulfrumMetal>().AddIngredient<WulfrumMetalScrap>().AddIngredient<EnergyCore>()
			.AddTile(283)
			.Register();
	}
}
