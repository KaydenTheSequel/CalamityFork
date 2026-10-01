using CalamityMod.Tiles.Astral;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class AstralChest : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AstralChestLocked>());
		base.Item.value = Item.sellPrice(0, 0, 5);
	}
}
