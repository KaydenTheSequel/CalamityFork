using CalamityMod.Items.Placeables.FurnitureExo;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class ExoPrismPanelWallItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<ExoPrismPanelWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<ExoPrismPanel>().AddTile(18).Register();
	}
}
