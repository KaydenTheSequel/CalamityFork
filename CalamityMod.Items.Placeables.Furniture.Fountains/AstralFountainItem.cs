using CalamityMod.Tiles.Furniture.Fountains;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Fountains;

public class AstralFountainItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AstralFountainTile>());
		base.Item.value = Item.buyPrice(0, 4);
		base.Item.rare = 1;
	}
}
