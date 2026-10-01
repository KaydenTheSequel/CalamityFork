using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;

public class AncientSmoothNavystone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone.AncientSmoothNavystone>());
	}
}
