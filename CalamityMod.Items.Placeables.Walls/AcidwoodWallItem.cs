using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class AcidwoodWallItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<AcidwoodWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<Acidwood>().AddTile(18).Register();
	}
}
