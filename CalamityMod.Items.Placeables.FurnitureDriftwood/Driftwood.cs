using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureDriftwood;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureDriftwood;

public class Driftwood : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 9;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureDriftwood.Driftwood>());
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Wood;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DriftwoodWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
