using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.AstralDesert;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

public class AstralSandstone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 3271, 1);
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<AstralSand>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.AstralDesert.AstralSandstone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralSandstoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
