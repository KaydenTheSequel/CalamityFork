using CalamityMod.Tiles.FurnitureStatigel;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStatigel;

public class StatigelPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureStatigel.StatigelPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<StatigelBlock>().Register();
	}
}
