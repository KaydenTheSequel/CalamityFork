using CalamityMod.Items.Critters;
using CalamityMod.Tiles.Furniture;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class BabyFlakCrabCage : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<BabyFlakCrabCageTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2208).AddIngredient<BabyFlakCrabItem>().Register();
	}
}
