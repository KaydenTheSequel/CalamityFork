using System.Collections.Generic;
using CalamityMod.Enums;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Fishing;
using CalamityMod.Items.Materials;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.World;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityMod.Items;

public class CalamityGlobalItemLoot : GlobalItem
{
	public override bool InstancePerEntity => false;

	private static IItemDropRule NewGoldenCrateBaitRule => ItemDropRule.SequentialRulesNotScalingWithLuck(2, ItemDropRule.NotScalingWithLuck(ModContent.ItemType<GrandMarquisBait>(), 3, 3, 7), ItemDropRule.NotScalingWithLuck(2676, 1, 3, 7));

	private static IItemDropRule UndergroundChestLootRule
	{
		get
		{
			int[] obj = new int[5] { 930, 5011, 49, 975, 0 };
			obj[4] = ModContent.ItemType<EnchantedKnifeStaff>();
			return new OneFromOptionsNotScaledWithLuckDropRule(4, 1, obj);
		}
	}

	public override void ModifyItemLoot(Item item, ItemLoot loot)
	{
		new Fraction(15, 100);
		switch (item.type)
		{
		case 3318:
			loot.DefineConditionalDropSet(DropHelper.NotRemix).Add(2273, 3);
			loot.DefineConditionalDropSet(DropHelper.Remix).Add(671, 3);
			loot.Add(ModContent.ItemType<CrownJewel>(), DropHelper.BagWeaponDropRateFraction);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		case 3319:
			loot.Add(ModContent.ItemType<DeathstareRod>(), DropHelper.BagWeaponDropRateFraction);
			loot.Add(ModContent.ItemType<TeardropCleaver>(), DropHelper.BagWeaponDropRateFraction);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		case 3320:
		{
			LeadingConditionRule mainRule = loot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.revenge));
			mainRule.Add(56, 1, 70, 90);
			mainRule.Add(86, 1, 20, 30);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 3321:
		{
			LeadingConditionRule mainRule2 = loot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.revenge));
			mainRule2.Add(880, 1, 70, 90);
			mainRule2.Add(1329, 1, 20, 30);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 5111:
		{
			loot.Remove(FindDeerclopsWeapons(loot));
			int[] deerclopsWeapons = new int[4] { 5095, 5117, 5118, 5119 };
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, deerclopsWeapons));
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 3322:
		{
			loot.Remove(FindQueenBeeWeapons(loot));
			int[] obj5 = new int[4] { 1123, 2888, 1121, 0 };
			obj5[3] = ModContent.ItemType<HardenedHoneycomb>();
			int[] queenBeeWeapons = obj5;
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, queenBeeWeapons));
			loot.Add(ModContent.ItemType<TheBee>(), DropHelper.BagWeaponDropRateFraction);
			loot.Add(209, 1, 8, 12);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 3324:
		{
			loot.Remove(FindWallOfFleshWeapons(loot));
			loot.Remove(FindWallOfFleshEmblems(loot));
			int[] obj3 = new int[8] { 426, 0, 434, 0, 514, 0, 4912, 0 };
			obj3[1] = ModContent.ItemType<Carnage>();
			obj3[3] = ModContent.ItemType<Meowthrower>();
			obj3[5] = ModContent.ItemType<BlackHawkRemote>();
			obj3[7] = ModContent.ItemType<BlastBarrel>();
			int[] wofWeapons = obj3;
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, wofWeapons));
			int[] obj4 = new int[5] { 490, 491, 489, 2998, 0 };
			obj4[4] = ModContent.ItemType<RogueEmblem>();
			int[] emblems = obj4;
			loot.Add(DropHelper.CalamityStyle(new Fraction(1, 4), emblems));
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 4957:
			loot.Add(520, 1, 15, 20);
			loot.Add(3111, 1, 15, 20);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		case 3325:
			loot.Remove(FindHallowedBars(loot));
			loot.AddIf(DropHelper.HallowedBarsCondition, 1225, 1, 20, 35);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		case 3326:
			loot.Remove(FindHallowedBars(loot));
			loot.AddIf(DropHelper.HallowedBarsCondition, 1225, 1, 20, 35);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		case 3327:
			loot.Remove(FindHallowedBars(loot));
			loot.AddIf(DropHelper.HallowedBarsCondition, 1225, 1, 20, 35);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		case 3328:
		{
			loot.Remove(FindPlanteraWeapons(loot));
			int[] planteraWeapons = new int[7] { 1259, 3018, 758, 1255, 1178, 788, 1155 };
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, planteraWeapons));
			loot.Add(ModContent.ItemType<BlossomFlux>(), 10);
			loot.Add(ModContent.ItemType<BloomStone>(), DropHelper.BagWeaponDropRateFraction);
			loot.Add(ModContent.ItemType<LivingShard>(), 1, 40, 50);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 3329:
		{
			loot.Remove(FindGolemItems(loot));
			int[] golemItems = new int[7] { 1297, 1122, 1258, 1295, 1296, 1248, 899 };
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, golemItems));
			loot.Add(ModContent.ItemType<AegisBlade>(), 10);
			loot.Add(ModContent.ItemType<EssenceofSunlight>(), 1, 10, 12);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 3860:
		{
			loot.Remove(FindBetsyWeapons(loot));
			int[] betsyWeapons = new int[4] { 3827, 3858, 3859, 3870 };
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, betsyWeapons));
			break;
		}
		case 3330:
		{
			RemoveDukeRules(loot);
			int[] obj2 = new int[7] { 2611, 2624, 2623, 2622, 2621, 0, 2609 };
			obj2[5] = ModContent.ItemType<DukesDecapitator>();
			int[] dukeItems = obj2;
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, dukeItems));
			loot.Add(ModContent.ItemType<BrinyBaron>(), 10);
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 4782:
		{
			RemoveEmpressRules(loot);
			int[] empressItems = new int[6] { 4923, 4953, 4952, 4715, 4914, 4823 };
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, empressItems));
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			break;
		}
		case 3332:
		{
			loot.Remove(FindMoonLordWeapons(loot));
			int[] obj = new int[10] { 3063, 3065, 3389, 3930, 1553, 3541, 3570, 3569, 3571, 0 };
			obj[9] = ModContent.ItemType<UtensilPoker>();
			int[] moonLordWeapons = obj;
			loot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, moonLordWeapons));
			loot.AddRevBagAccessories();
			loot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			loot.AddIf((DropAttemptInfo info) => (!info.player.Calamity().extraAccessoryML && !Main.masterMode) || (!info.player.extraAccessory && Main.masterMode), ModContent.ItemType<CelestialOnion>());
			break;
		}
		case 2334:
		case 3979:
			loot.Add(ModContent.ItemType<Kylie>(), 100);
			break;
		case 3980:
			RemoveHardmodeOresFromStandardCrates(loot);
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Mythril);
			break;
		case 2336:
			RemoveBaitFromGoldenCrates(loot);
			loot.Add(NewGoldenCrateBaitRule);
			loot.Add(UndergroundChestLootRule);
			break;
		case 3981:
			RemoveHardmodeOresFromStandardCrates(loot);
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Titanium);
			RemoveBaitFromGoldenCrates(loot);
			loot.Add(NewGoldenCrateBaitRule);
			loot.Add(UndergroundChestLootRule);
			break;
		case 4406:
			RemoveHardmodeOresFromBiomeCrates(loot);
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Biome);
			loot.Add(ModContent.ItemType<EssenceofEleum>(), 2, 2, 5);
			break;
		case 3985:
			RemoveHardmodeOresFromBiomeCrates(loot);
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Biome);
			loot.Add(ModContent.ItemType<EssenceofSunlight>(), 2, 2, 5);
			break;
		case 4407:
			loot.Add(ModContent.ItemType<TheComb>(), 8);
			break;
		case 4408:
			RemoveHardmodeOresFromBiomeCrates(loot);
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Biome);
			loot.Add(ModContent.ItemType<TheComb>(), 8);
			break;
		case 3982:
		case 3983:
		case 3984:
		case 3986:
		case 3987:
		case 4878:
		case 5003:
			RemoveHardmodeOresFromBiomeCrates(loot);
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Biome);
			break;
		case 1774:
			RemoveBatHookFromGoodieBag(loot);
			break;
		}
	}

	private static IItemDropRule FindDeerclopsWeapons(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 5095)
				{
					return o;
				}
			}
		}
		return null;
	}

	private static IItemDropRule FindQueenBeeWeapons(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 1123)
				{
					return o;
				}
			}
		}
		return null;
	}

	private static IItemDropRule FindWallOfFleshWeapons(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 426)
				{
					return o;
				}
			}
		}
		return null;
	}

	private static IItemDropRule FindWallOfFleshEmblems(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 490)
				{
					return o;
				}
			}
		}
		return null;
	}

	private static IItemDropRule FindHallowedBars(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (item is CommonDrop { itemId: 1225 } c)
			{
				return c;
			}
		}
		return null;
	}

	private static IItemDropRule FindPlanteraWeapons(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromRulesRule { options: var options } o))
			{
				continue;
			}
			for (int i = 0; i < options.Length; i++)
			{
				if (options[i] is CommonDrop { itemId: 758 })
				{
					return o;
				}
			}
		}
		return null;
	}

	private static IItemDropRule FindGolemItems(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromRulesRule { options: var options } o))
			{
				continue;
			}
			for (int i = 0; i < options.Length; i++)
			{
				if (options[i] is CommonDrop { itemId: 1258 })
				{
					return o;
				}
			}
		}
		return null;
	}

	private static IItemDropRule FindBetsyWeapons(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 3827)
				{
					return o;
				}
			}
		}
		return null;
	}

	private static void RemoveDukeRules(ItemLoot loot)
	{
		List<IItemDropRule> rules = loot.Get(includeGlobalDrops: false);
		IItemDropRule toRemove = null;
		foreach (IItemDropRule item in rules)
		{
			if (item is CommonDropNotScalingWithLuck { itemId: 2609 } c)
			{
				toRemove = c;
			}
		}
		if (toRemove != null)
		{
			loot.Remove(toRemove);
		}
		foreach (IItemDropRule item2 in rules)
		{
			if (!(item2 is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 2611)
				{
					toRemove = o;
				}
			}
		}
		if (toRemove != null)
		{
			loot.Remove(toRemove);
		}
	}

	private static void RemoveEmpressRules(ItemLoot loot)
	{
		List<IItemDropRule> rules = loot.Get(includeGlobalDrops: false);
		IItemDropRule toRemove = null;
		foreach (IItemDropRule item in rules)
		{
			if (item is CommonDropNotScalingWithLuck { itemId: 4823 } c)
			{
				toRemove = c;
			}
		}
		if (toRemove != null)
		{
			loot.Remove(toRemove);
		}
		foreach (IItemDropRule item2 in rules)
		{
			if (!(item2 is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 4923)
				{
					toRemove = o;
				}
			}
		}
		if (toRemove != null)
		{
			loot.Remove(toRemove);
		}
		foreach (IItemDropRule item3 in rules)
		{
			if (item3 is CommonDropNotScalingWithLuck { itemId: 4715 } c2)
			{
				toRemove = c2;
			}
		}
		if (toRemove != null)
		{
			loot.Remove(toRemove);
		}
	}

	private static IItemDropRule FindMoonLordWeapons(ItemLoot loot)
	{
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops: false))
		{
			if (!(item is OneFromOptionsNotScaledWithLuckDropRule { dropIds: var dropIds } o))
			{
				continue;
			}
			for (int i = 0; i < dropIds.Length; i++)
			{
				if (dropIds[i] == 3389)
				{
					return o;
				}
			}
		}
		return null;
	}

	private static void RemoveHardmodeOresFromStandardCrates(ItemLoot loot)
	{
		List<IItemDropRule> list = loot.Get(includeGlobalDrops: false);
		AlwaysAtleastOneSuccessDropRule mainRule = null;
		foreach (IItemDropRule item in list)
		{
			if (item is AlwaysAtleastOneSuccessDropRule a)
			{
				mainRule = a;
			}
		}
		if (mainRule == null)
		{
			return;
		}
		IItemDropRule[] rules = mainRule.rules;
		for (int i = 0; i < rules.Length; i++)
		{
			if (!(rules[i] is SequentialRulesNotScalingWithLuckRule { rules: var rules2 } oreRule))
			{
				continue;
			}
			for (int j = 0; j < rules2.Length; j++)
			{
				if (rules2[j] is SequentialRulesNotScalingWithLuckRule)
				{
					oreRule.chanceNumerator = 0;
					return;
				}
			}
		}
	}

	private static void RemoveHardmodeOresFromBiomeCrates(ItemLoot loot)
	{
		List<IItemDropRule> list = loot.Get(includeGlobalDrops: false);
		AlwaysAtleastOneSuccessDropRule mainRule = null;
		foreach (IItemDropRule item in list)
		{
			if (item is AlwaysAtleastOneSuccessDropRule a)
			{
				mainRule = a;
			}
		}
		if (mainRule == null)
		{
			return;
		}
		IItemDropRule[] rules = mainRule.rules;
		for (int i = 0; i < rules.Length; i++)
		{
			if (!(rules[i] is SequentialRulesNotScalingWithLuckRule { rules: var rules2 } oreRule))
			{
				continue;
			}
			for (int j = 0; j < rules2.Length; j++)
			{
				if (rules2[j] is OneFromRulesRule)
				{
					oreRule.chanceNumerator = 0;
				}
			}
		}
	}

	private static void RemoveBaitFromGoldenCrates(ItemLoot loot)
	{
		List<IItemDropRule> list = loot.Get(includeGlobalDrops: false);
		IItemDropRule toRemove = null;
		foreach (IItemDropRule item in list)
		{
			if (item is CommonDrop { itemId: 2676 } c)
			{
				toRemove = c;
			}
		}
		if (toRemove != null)
		{
			loot.Remove(toRemove);
		}
	}

	private static void RemoveBatHookFromGoodieBag(ItemLoot loot)
	{
		List<IItemDropRule> list = loot.Get(includeGlobalDrops: false);
		SequentialRulesNotScalingWithLuckRule rule1 = null;
		foreach (IItemDropRule item in list)
		{
			if (item is SequentialRulesNotScalingWithLuckRule s)
			{
				rule1 = s;
			}
		}
		if (rule1 == null)
		{
			return;
		}
		IItemDropRule[] rules = rule1.rules;
		for (int i = 0; i < rules.Length; i++)
		{
			if (rules[i] is CommonDropNotScalingWithLuck { itemId: 1800 } rule2)
			{
				rule2.chanceNumerator = 0;
				rule2.chanceDenominator = 1;
			}
		}
	}
}
