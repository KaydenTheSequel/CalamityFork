using CalamityMod.Items.Placeables.FurnitureDriftwood;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.SunkenSeaCatches;

[LegacyName(new string[] { "SunkenCrate" })]
public class EutrophicCrate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.IsFishingCrate[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<EutrophicCrateTile>());
		base.Item.width = (base.Item.height = 32);
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = 2;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(new OneFromRulesRule(1, ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.Navystone>(), 1, 20, 50), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.EutrophicSand>(), 1, 20, 50), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Driftwood>(), 1, 20, 50)));
		itemLoot.Add(new OneFromRulesRule(1, ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.CyanCoral>(), 1, 10, 20), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.OrangeCoral>(), 1, 10, 20), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.LimeCoral>(), 1, 10, 20), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.MagentaCoral>(), 1, 10, 20), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.YellowCoral>(), 1, 10, 20)));
		itemLoot.Add(ModContent.ItemType<PrismShard>(), 2, 4, 10);
		itemLoot.AddBiomeCrateLootRules(hardMode: false);
	}
}
