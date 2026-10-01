using CalamityMod.Items.Placeables.FurnitureSacrilegious;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class OccultBrickWallItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<OccultBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<OccultBrickItem>().AddTile(18).Register();
	}
}
