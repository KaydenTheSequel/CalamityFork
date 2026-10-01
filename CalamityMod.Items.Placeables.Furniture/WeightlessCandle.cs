using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

[LegacyName(new string[] { "BlueCandle" })]
public class WeightlessCandle : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.1f;

	public static double WingTimeBoost = 0.1;

	public static float AccelerationBoost = 0.1f;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<BlueCandle>());
		base.Item.value = Item.buyPrice(0, 25);
		base.Item.rare = 5;
	}
}
