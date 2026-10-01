using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.DraedonStructures.CagedLights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures.CagedLights;

public class MiniCagedLablightItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<MiniAgedLablightItem>();
		base.Item.DefaultToPlaceableTile(ModContent.TileType<MiniCagedLablight>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient<DubiousPlating>().AddIngredient<MysteriousCircuitry>(2).AddIngredient<DraedonPowerCell>()
			.AddTile(16)
			.Register();
	}
}
