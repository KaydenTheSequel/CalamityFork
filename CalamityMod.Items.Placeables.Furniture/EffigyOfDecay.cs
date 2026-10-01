using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class EffigyOfDecay : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<EffigyOfDecayPlaceable>());
		base.Item.value = Item.sellPrice(0, 0, 10);
		base.Item.rare = 1;
	}
}
