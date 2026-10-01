using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.AstralSnow;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

public class AstralIce : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 664, 1);
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<AstralSnow>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.AstralSnow.AstralIce>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralIceWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
