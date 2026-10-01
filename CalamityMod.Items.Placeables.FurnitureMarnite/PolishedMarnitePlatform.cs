using CalamityMod.Tiles.FurnitureMarnite;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMarnite;

public class PolishedMarnitePlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMarnite.PolishedMarnitePlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<PolishedMarniteBlock>().Register();
	}
}
