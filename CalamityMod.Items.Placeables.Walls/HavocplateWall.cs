using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

[LegacyName(new string[] { "ChaosplateWall" })]
public class HavocplateWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.HavocplateWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<Havocplate>().AddTile(18).Register();
	}
}
