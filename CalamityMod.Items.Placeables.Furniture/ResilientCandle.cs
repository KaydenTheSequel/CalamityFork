using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

[LegacyName(new string[] { "PurpleCandle" })]
public class ResilientCandle : ModItem, ILocalizedModType, IModType
{
	public static float DefenseRatioBonus = 0.1f;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DefenseRatioBonus.ToPercent(), (0.5f + DefenseRatioBonus).ToPercent(), (0.75f + DefenseRatioBonus).ToPercent(), (1f + DefenseRatioBonus).ToPercent());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PurpleCandle>());
		base.Item.value = Item.buyPrice(0, 25);
		base.Item.rare = 5;
	}
}
