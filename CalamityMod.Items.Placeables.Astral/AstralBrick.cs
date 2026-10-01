using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

public class AstralBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.AstralBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient<AstralOre>().AddIngredient<AstralStone>().AddTile(17)
			.Register();
		CreateRecipe().AddIngredient<AstralBrickWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
