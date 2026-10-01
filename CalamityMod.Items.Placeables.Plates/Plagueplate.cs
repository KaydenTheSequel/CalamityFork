using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Plates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Plates;

[LegacyName(new string[] { "PlagueContainmentCells" })]
public class Plagueplate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<Havocplate>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PlagueContainmentCells>());
		base.Item.value = Item.sellPrice(0, 0, 3);
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddIngredient(173, 25).AddIngredient<PlagueCellCanister>().AddTile(77)
			.Register();
		CreateRecipe().AddIngredient<PlagueContainmentCellsWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
