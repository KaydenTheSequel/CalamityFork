using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

[LegacyName(new string[] { "NavystoneWallSafe" })]
public class NavystoneWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<UnsafeNavystoneWall>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.NavystoneWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<Navystone>().AddTile(18).Register();
	}
}
