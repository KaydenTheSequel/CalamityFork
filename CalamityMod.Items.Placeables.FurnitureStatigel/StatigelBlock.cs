using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureStatigel;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStatigel;

public class StatigelBlock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureStatigel.StatigelBlock>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddIngredient<PurifiedGel>().AddTile(220).Register();
		CreateRecipe().AddIngredient<StatigelPlatform>(2).DisableDecraft().Register();
		CreateRecipe().AddIngredient<StatigelWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
