using CalamityMod.Tiles.Furniture.BossTrophies;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Trophies;

[LegacyName(new string[] { "GreatSandSharkBanner" })]
public class GreatSandSharkTrophy : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<GreatSandSharkTrophyTile>());
		base.Item.width = (base.Item.height = 30);
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = 1;
	}
}
