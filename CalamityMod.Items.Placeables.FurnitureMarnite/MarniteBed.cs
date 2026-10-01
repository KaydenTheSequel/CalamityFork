using CalamityMod.Tiles.FurnitureMarnite;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMarnite;

public class MarniteBed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMarnite.MarniteBed>());
		base.Item.value = Item.sellPrice(0, 0, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PolishedMarniteBlock>(4).AddIngredient(225, 5).AddTile(106)
			.Register();
	}
}
