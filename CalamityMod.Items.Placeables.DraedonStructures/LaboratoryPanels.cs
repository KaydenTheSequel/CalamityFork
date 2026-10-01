using CalamityMod.Items.Placeables.Walls.DraedonStructures;
using CalamityMod.Tiles.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class LaboratoryPanels : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.LaboratoryPanels>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddRecipeGroup("AnyStoneBlock", 25).AddRecipeGroup("IronBar").AddTile(283)
			.Register();
		CreateRecipe().AddIngredient<LaboratoryPanelWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
