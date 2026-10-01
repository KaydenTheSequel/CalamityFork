using CalamityMod.Items.Placeables.Walls.DraedonStructures;
using CalamityMod.Tiles.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class HazardChevronPanels : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.HazardChevronPanels>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient<LaboratoryPanels>(10).AddIngredient(1075).AddIngredient(1097)
			.AddTile(18)
			.Register();
		CreateRecipe().AddIngredient<HazardChevronWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
