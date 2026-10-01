using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Abyss;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Abyss;

public class AbyssGravel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Abyss.AbyssGravel>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AbyssGravelWallItem>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
