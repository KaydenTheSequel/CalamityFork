using CalamityMod.Tiles.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class LaboratoryShelf : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.LaboratoryShelf>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<LaboratoryPlating>().Register();
		CreateRecipe().AddIngredient<RustedShelf>().Register();
	}
}
