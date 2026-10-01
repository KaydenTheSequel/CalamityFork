using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Potions;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags.MiscGrabBags;

public class SulphuricTreasure : ModItem, ILocalizedModType, IModType
{
	internal static readonly int[] SulphuricTreasurePotions;

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
		base.Item.rare = 2;
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
		LeadingConditionRule LCRsinglePlayer = new LeadingConditionRule(DropHelper.If(() => Main.netMode != 1));
		LeadingConditionRule LCRexpert = new LeadingConditionRule(DropHelper.If(() => Main.expertMode));
		IItemDropRule buffPotions = itemLoot.Add(new OneFromOptionsNotScaledWithLuckDropRule(10, 1, SulphuricTreasurePotions));
		IItemDropRule wormholePotion = ItemDropRule.ByCondition(DropHelper.If(() => Main.netMode == 1), 2997, 30);
		DropBasedOnExpertMode torches = DropHelper.NormalVsExpertQuantity(ModContent.ItemType<SulphurousTorch>(), 1, 4, 12, 5, 18);
		IItemDropRule ammo = ItemDropRule.NotScalingWithLuck(40, 1, 10, 20);
		IItemDropRule healPot = ItemDropRule.NotScalingWithLuck(188);
		IItemDropRule healPotExtra = ItemDropRule.NotScalingWithLuck(188, 3);
		DropBasedOnExpertMode bombs = DropHelper.NormalVsExpertQuantity(166, 1, 1, 4, 1, 7);
		DropBasedOnExpertMode coins = DropHelper.NormalVsExpertQuantity(72, 1, 2, 9, 6, 27);
		OneFromRulesRule otherDrops = new OneFromRulesRule(1, coins, torches, ammo, healPot, bombs, coins, coins);
		buffPotions.OnFailedRoll(wormholePotion).OnFailedRoll(otherDrops);
		buffPotions.OnFailedRoll(LCRsinglePlayer).OnSuccess(otherDrops);
		healPot.OnSuccess(LCRexpert).OnSuccess(healPotExtra);
	}

	static SulphuricTreasure()
	{
		int[] obj = new int[10] { 0, 2327, 291, 298, 4870, 2346, 305, 2323, 2345, 296 };
		obj[0] = ModContent.ItemType<SulphurskinPotion>();
		SulphuricTreasurePotions = obj;
	}
}
