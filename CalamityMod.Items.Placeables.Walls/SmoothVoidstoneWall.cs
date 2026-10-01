using CalamityMod.Items.Placeables.FurnitureVoid;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class SmoothVoidstoneWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.SmoothVoidstoneWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<SmoothVoidstone>().AddTile(18).Register();
	}
}
