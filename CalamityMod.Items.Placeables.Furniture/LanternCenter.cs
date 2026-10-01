using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class LanternCenter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<LanternCenterTile>());
		base.Item.value = Item.buyPrice(0, 20);
		base.Item.rare = 3;
	}
}
