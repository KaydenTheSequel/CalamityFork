using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "DraedonBar" })]
public class PerennialBar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 92;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.PerennialBar>());
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = 7;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialOre>(4).AddTile(133).Register();
	}
}
