using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class AgedCinderlightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<CagedCinderlightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AgedCinderlight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}
}
