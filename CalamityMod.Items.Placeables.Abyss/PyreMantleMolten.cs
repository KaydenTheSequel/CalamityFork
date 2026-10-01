using CalamityMod.Tiles.Abyss;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Abyss;

public class PyreMantleMolten : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Abyss.PyreMantleMolten>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddIngredient(207).AddIngredient<PyreMantle>(25).AddTile(17)
			.Register();
	}
}
