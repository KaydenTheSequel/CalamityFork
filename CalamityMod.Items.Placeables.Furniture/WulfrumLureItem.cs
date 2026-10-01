using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class WulfrumLureItem : ModItem, ILocalizedModType, IModType
{
	public static int SignalTime = 1800;

	public static int SpawnIntervals = 240;

	public static int MaxEnemiesPerWave = 3;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SignalTime.FramesToSeconds());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<WulfrumLure>());
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(5).AddIngredient<EnergyCore>().AddTile(16)
			.Register();
	}
}
