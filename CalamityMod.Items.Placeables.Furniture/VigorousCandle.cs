using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

[LegacyName(new string[] { "PinkCandle" })]
public class VigorousCandle : ModItem, ILocalizedModType, IModType
{
	public static double PercentHealthPerSecond = 0.004;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PercentHealthPerSecond.ToPercent());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PinkCandle>());
		base.Item.value = Item.buyPrice(0, 25);
		base.Item.rare = 5;
	}
}
