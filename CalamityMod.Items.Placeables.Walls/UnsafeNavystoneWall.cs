using CalamityMod.Walls.UnsafeWalls;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class UnsafeNavystoneWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
		ItemID.Sets.DrawUnsafeIndicator[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<NavystoneWall>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.UnsafeWalls.UnsafeNavystoneWall>());
	}
}
