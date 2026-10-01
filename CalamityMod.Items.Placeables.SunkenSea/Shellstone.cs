using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class Shellstone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<AridSoil>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.Shellstone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShellstoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
