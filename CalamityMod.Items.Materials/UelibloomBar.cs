using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using CalamityMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "UeliaceBar" })]
public class UelibloomBar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 106;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.UelibloomBar>());
		base.Item.value = Item.sellPrice(0, 1, 40);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomOre>(4).AddTile(133).Register();
	}
}
