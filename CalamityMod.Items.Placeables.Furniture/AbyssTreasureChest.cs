using CalamityMod.Tiles.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class AbyssTreasureChest : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Abyss.AbyssTreasureChest>());
		base.Item.value = Item.sellPrice(0, 0, 10);
	}
}
