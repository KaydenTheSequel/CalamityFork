using CalamityMod.Tiles.FurnitureRunestone;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureRunestone;

public class RunestoneTerracotta : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureRunestone.RunestoneTerracotta>());
	}
}
