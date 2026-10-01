using CalamityMod.Items.Placeables.FurnitureBotanic;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables;

public class UelibloomBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.UelibloomBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddRecipeGroup("AnyStoneBlock", 50).AddIngredient<UelibloomOre>().AddTile(133)
			.Register();
		CreateRecipe().AddIngredient<UelibloomBrickWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<BotanicPlatform>(2).DisableDecraft().Register();
	}
}
