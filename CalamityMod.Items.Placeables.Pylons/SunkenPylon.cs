using CalamityMod.Tiles.Pylons;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Pylons;

public class SunkenPylon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SunkenPylonTile>());
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 1;
	}
}
