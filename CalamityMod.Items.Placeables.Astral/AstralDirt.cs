using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Astral;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

public class AstralDirt : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 2, 1);
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Astral.AstralDirt>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralDirtWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
