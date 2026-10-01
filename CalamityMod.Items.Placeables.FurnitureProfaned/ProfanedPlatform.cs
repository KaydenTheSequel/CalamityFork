using CalamityMod.Tiles.FurnitureProfaned;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureProfaned;

public class ProfanedPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureProfaned.ProfanedPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<ProfanedRock>().Register();
	}
}
