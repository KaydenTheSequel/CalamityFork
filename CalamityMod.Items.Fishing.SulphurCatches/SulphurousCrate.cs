using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Tools.SpawnBlocker;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Tiles.Abyss;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.SulphurCatches;

[LegacyName(new string[] { "AbyssalCrate" })]
public class SulphurousCrate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.IsFishingCrate[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SulphurousCrateTile>());
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
		itemLoot.Add(new OneFromRulesRule(1, ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.Abyss.SulphurousSand>(), 1, 20, 50), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.Abyss.SulphurousSandstone>(), 1, 20, 50), ItemDropRule.NotScalingWithLuck(ModContent.ItemType<global::CalamityMod.Items.Placeables.Abyss.HardenedSulphurousSandstone>(), 1, 20, 50)));
		itemLoot.Add(new OneFromOptionsNotScaledWithLuckDropRule(1, 1, ModContent.ItemType<BrokenWaterFilter>(), ModContent.ItemType<EffigyOfDecay>(), ModContent.ItemType<RustyBeaconPrototype>(), ModContent.ItemType<ScionsCurio>()));
		itemLoot.AddBiomeCrateLootRules(hardMode: false);
	}
}
