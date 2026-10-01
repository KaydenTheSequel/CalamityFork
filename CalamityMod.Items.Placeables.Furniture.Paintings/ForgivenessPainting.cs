using CalamityMod.Tiles.Furniture.Paintings;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Paintings;

public class ForgivenessPainting : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<ForgivenessPaintingTile>());
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.value = Item.buyPrice(0, 15);
	}
}
