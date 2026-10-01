using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class LabHologramProjectorItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<LabHologramProjector>());
		base.Item.value = Item.sellPrice(0, 0, 5);
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LaboratoryPlating>(20).AddIngredient<MysteriousCircuitry>(3).AddIngredient<DubiousPlating>(3)
			.AddIngredient<DraedonPowerCell>(8)
			.AddTile(16)
			.Register();
	}
}
