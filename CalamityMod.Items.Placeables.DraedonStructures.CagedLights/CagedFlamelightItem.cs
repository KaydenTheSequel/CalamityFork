using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class CagedFlamelightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<AgedFlamelightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<CagedFlamelight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient<DubiousPlating>(2).AddIngredient<MysteriousCircuitry>().AddIngredient<Havocplate>()
			.AddTile(16)
			.Register();
	}
}
