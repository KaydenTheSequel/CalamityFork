using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicPlatform" })]
public class AncientNavystonePlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystonePlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<AncientSmoothNavystone>().Register();
	}
}
