using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class SulphurousSandstoneWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<SafeSulphurousSandstoneWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<SulphurousSandstone>().AddTile(18).Register();
	}
}
