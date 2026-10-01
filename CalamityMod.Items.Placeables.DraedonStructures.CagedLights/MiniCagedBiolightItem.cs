using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class MiniCagedBiolightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<MiniAgedBiolightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<MiniCagedBiolight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient<DubiousPlating>().AddIngredient<MysteriousCircuitry>(2).AddIngredient<Plagueplate>()
			.AddTile(16)
			.Register();
	}
}
