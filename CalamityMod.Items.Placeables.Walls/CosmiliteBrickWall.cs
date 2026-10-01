using CalamityMod.Items.Placeables.FurnitureCosmilite;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class CosmiliteBrickWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.CosmiliteBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<CosmiliteBrick>().AddTile(18).Register();
	}
}
