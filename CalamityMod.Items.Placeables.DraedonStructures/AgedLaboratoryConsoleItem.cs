using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class AgedLaboratoryConsoleItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AgedLaboratoryConsole>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RustedPlating>(10).AddIngredient<MysteriousCircuitry>().AddIngredient<DraedonPowerCell>(8)
			.AddTile(16)
			.Register();
	}
}
