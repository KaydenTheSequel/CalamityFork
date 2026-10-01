using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class AgedFlamelightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<CagedFlamelightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AgedFlamelight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}
}
