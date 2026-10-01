using CalamityMod.Rarities;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class PowerCellFactoryItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(900.FramesToSeconds());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PowerCellFactory>());
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}
}
