using CalamityMod.Tiles.FurnitureSilva;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureSilva;

public class SilvaPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureSilva.SilvaPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<SilvaCrystal>().Register();
	}
}
