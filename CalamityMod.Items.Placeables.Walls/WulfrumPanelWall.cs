using CalamityMod.Items.Placeables.FurnitureWulfrum;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class WulfrumPanelWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.WulfrumPanelWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<WulfrumPanels>().AddTile(18).Register();
	}
}
