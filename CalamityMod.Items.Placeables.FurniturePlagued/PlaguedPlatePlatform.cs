using CalamityMod.Tiles.FurniturePlaguedPlate;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurniturePlagued;

public class PlaguedPlatePlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurniturePlaguedPlate.PlaguedPlatePlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<PlaguedContainmentBrick>().Register();
	}
}
