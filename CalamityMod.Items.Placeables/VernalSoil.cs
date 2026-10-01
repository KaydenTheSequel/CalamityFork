using CalamityMod.Tiles;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables;

public class VernalSoil : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.VernalSoil>());
	}
}
