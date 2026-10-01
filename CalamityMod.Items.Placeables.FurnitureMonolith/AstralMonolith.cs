using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureMonolith;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMonolith;

public class AstralMonolith : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 9;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMonolith.AstralMonolith>());
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Wood;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolithWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<MonolithPlatform>(2).DisableDecraft().Register();
	}
}
