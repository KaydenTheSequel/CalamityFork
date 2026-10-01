using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class AgedFloodlightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<CagedFloodlightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AgedFloodlight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}
}
