using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Crags;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Crags;

public class ScorchedBone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Crags.ScorchedBone>());
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Wood;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScorchedBoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
