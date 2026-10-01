using CalamityMod.Items.Materials;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class WulfrumPillar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.WulfrumPillar>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(8).AddIngredient<AnodizedWulfrumMetal>().AddIngredient<WulfrumMetalScrap>().AddTile(18)
			.Register();
	}
}
