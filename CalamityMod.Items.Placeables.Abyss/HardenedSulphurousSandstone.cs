using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Abyss;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Abyss;

public class HardenedSulphurousSandstone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<SulphurousSand>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Abyss.HardenedSulphurousSandstone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<HardenedSulphurousSandstoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
