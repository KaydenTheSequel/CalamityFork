using CalamityMod.Tiles.Furniture.BossRelics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.BossRelics;

public class GiantClamRelic : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.BossRelics.GiantClamRelic>());
		base.Item.width = 30;
		base.Item.height = 40;
		base.Item.rare = -13;
		base.Item.master = true;
		base.Item.value = Item.sellPrice(0, 1);
	}
}
