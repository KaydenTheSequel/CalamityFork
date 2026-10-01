using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

[LegacyName(new string[] { "YellowCandle" })]
public class SpitefulCandle : ModItem, ILocalizedModType, IModType
{
	public static float ExtraChipDamageRatio = 0.07f;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((1f + ExtraChipDamageRatio).ToString(), ExtraChipDamageRatio.ToString());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<YellowCandle>());
		base.Item.value = Item.buyPrice(0, 25);
		base.Item.rare = 5;
	}
}
