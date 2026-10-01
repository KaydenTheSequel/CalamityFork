using CalamityMod.Tiles.Furniture.Paintings;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Paintings;

public class CalamityCanvas2023 : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override string Texture => "CalamityMod/Items/Placeables/Furniture/Paintings/CalamityCanvas";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<CalamityCanvas2023Tile>());
		base.Item.width = 96;
		base.Item.height = 64;
		base.Item.value = Item.sellPrice(0, 0, 40);
	}
}
