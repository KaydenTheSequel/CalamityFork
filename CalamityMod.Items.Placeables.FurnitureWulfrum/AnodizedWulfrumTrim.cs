using CalamityMod.Items.Materials;
using CalamityMod.Tiles.FurnitureWulfrum;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum;

public class AnodizedWulfrumTrim : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.AnodizedWulfrumTrim>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddRecipeGroup("AnyStoneBlock", 25).AddIngredient<AnodizedWulfrumMetal>().AddTile(283)
			.Register();
	}
}
