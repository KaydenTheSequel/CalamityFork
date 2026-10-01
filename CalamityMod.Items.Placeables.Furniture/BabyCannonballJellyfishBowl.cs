using CalamityMod.Items.Critters;
using CalamityMod.Tiles.Furniture;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class BabyCannonballJellyfishBowl : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<BabyCannonballJellyfishBowlTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BabyCannonballJellyfishItem>().AddIngredient(126).Register();
	}
}
