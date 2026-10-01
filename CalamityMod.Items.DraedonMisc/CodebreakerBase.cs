using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.Tiles.DraedonSummoner;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

public class CodebreakerBase : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<CodebreakerTile>());
		base.Item.rare = 2;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ChargingStationItem>().AddIngredient<MysteriousCircuitry>(20).AddIngredient<DubiousPlating>(35)
			.AddTile(16)
			.Register();
	}
}
