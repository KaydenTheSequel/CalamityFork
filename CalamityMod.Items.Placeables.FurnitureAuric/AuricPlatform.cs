using CalamityMod.Tiles.FurnitureAuric;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAuric;

public class AuricPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AuricPlatformTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<AuricPanel>().Register();
	}
}
