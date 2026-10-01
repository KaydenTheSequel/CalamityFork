using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Ores;

public class AuricOre : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 119;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Ores.AuricOre>());
		base.Item.value = Item.sellPrice(0, 0, 60);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient<YharonSoulFragment>().AddCondition(Condition.NearShimmer).Register();
	}
}
