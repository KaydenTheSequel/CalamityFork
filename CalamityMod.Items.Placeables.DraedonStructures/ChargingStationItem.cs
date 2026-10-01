using CalamityMod.Rarities;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class ChargingStationItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<ChargingStation>());
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}
}
