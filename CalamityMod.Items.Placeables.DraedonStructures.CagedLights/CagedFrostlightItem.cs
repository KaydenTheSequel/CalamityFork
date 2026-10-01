using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class CagedFrostlightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<AgedFrostlightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<CagedFrostlight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient<DubiousPlating>(2).AddIngredient<MysteriousCircuitry>().AddIngredient<Elumplate>()
			.AddTile(16)
			.Register();
	}
}
