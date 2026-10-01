using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Plates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Plates;

public class Navyplate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<Cinderplate>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Plates.Navyplate>());
		base.Item.value = Item.sellPrice(0, 0, 3);
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddIngredient(173, 25).AddIngredient<SeaPrism>().AddTile(77)
			.Register();
		CreateRecipe().AddIngredient<NavyplateWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
