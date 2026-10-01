using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.FurnitureBasalt;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureBasalt;

public class BasaltSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureBasalt.BasaltSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Basalt>().AddTile(18).DisableDecraft()
			.Register();
	}
}
