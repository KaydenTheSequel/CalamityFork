using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.CraftingStations;

[LegacyName(new string[] { "SCalAltarItem" })]
public class AltarOfTheAccursedItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SCalAltarLarge>());
		base.Item.value = Item.sellPrice(0, 40);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlag>(30).AddIngredient<AuricBar>(5).AddIngredient<CoreofCalamity>()
			.AddTile(134)
			.Register();
	}
}
