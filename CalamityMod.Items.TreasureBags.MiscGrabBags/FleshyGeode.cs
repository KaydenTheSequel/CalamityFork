using CalamityMod.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags.MiscGrabBags;

[LegacyName(new string[] { "FleshyGeodeT1" })]
public class FleshyGeode : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.TreasureBags";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 24;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.rare = 8;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.GoodieBags;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		LeadingConditionRule mainRule = itemLoot.DefineNormalOnlyDropSet();
		mainRule.Add(ModContent.ItemType<CryonicBar>(), 1, 1, 3);
		mainRule.Add(ModContent.ItemType<PerennialBar>(), 1, 1, 3);
		mainRule.Add(ModContent.ItemType<ScoriaBar>(), 1, 1, 3);
		mainRule.Add(ModContent.ItemType<EssenceofEleum>(), 1, 1, 2);
		mainRule.Add(ModContent.ItemType<EssenceofSunlight>(), 1, 1, 2);
		mainRule.Add(ModContent.ItemType<EssenceofHavoc>(), 1, 1, 2);
		mainRule.Add(ModContent.ItemType<LifeAlloy>(), 3);
		mainRule.Add(ModContent.ItemType<CoreofCalamity>(), 4);
		LeadingConditionRule mainRule2 = itemLoot.DefineConditionalDropSet(new Conditions.IsExpert());
		mainRule2.Add(ModContent.ItemType<CryonicBar>(), 1, 2, 3);
		mainRule2.Add(ModContent.ItemType<PerennialBar>(), 1, 2, 3);
		mainRule2.Add(ModContent.ItemType<ScoriaBar>(), 1, 2, 3);
		mainRule2.Add(ModContent.ItemType<EssenceofEleum>(), 1, 1, 3);
		mainRule2.Add(ModContent.ItemType<EssenceofSunlight>(), 1, 1, 3);
		mainRule2.Add(ModContent.ItemType<EssenceofHavoc>(), 1, 1, 3);
		mainRule2.Add(ModContent.ItemType<LifeAlloy>(), 2);
		mainRule2.Add(ModContent.ItemType<CoreofCalamity>(), 3);
	}
}
