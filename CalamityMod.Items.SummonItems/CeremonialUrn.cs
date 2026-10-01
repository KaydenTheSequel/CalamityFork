using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "EyeofExtinction" })]
public class CeremonialUrn : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 54;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AshesofAnnihilation>(5).AddIngredient<AshesofCalamity>(15).AddTile<SCalAltar>()
			.Register();
	}
}
