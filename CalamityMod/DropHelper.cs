using System;
using System.Collections.Generic;
using CalamityMod.Enums;
using CalamityMod.Items.Accessories;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityMod;

public static class DropHelper
{
	internal class LambdaDropRuleCondition : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		private readonly Func<DropAttemptInfo, bool> conditionLambda;

		private readonly bool visibleInUI;

		private readonly string description;

		internal LambdaDropRuleCondition(Func<DropAttemptInfo, bool> lambda, bool ui = true, string desc = null)
		{
			conditionLambda = lambda;
			visibleInUI = ui;
			description = desc;
		}

		public bool CanDrop(DropAttemptInfo info)
		{
			return conditionLambda(info);
		}

		public bool CanShowItemDropInUI()
		{
			return visibleInUI;
		}

		public string GetConditionDescription()
		{
			return description;
		}
	}

	internal class LambdaDropRuleCondition2 : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		private readonly Func<DropAttemptInfo, bool> conditionLambda;

		private readonly Func<bool> visibleInUI;

		private readonly string description;

		internal LambdaDropRuleCondition2(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, string desc = null)
		{
			conditionLambda = lambda;
			visibleInUI = ui;
			description = desc;
		}

		public bool CanDrop(DropAttemptInfo info)
		{
			return conditionLambda(info);
		}

		public bool CanShowItemDropInUI()
		{
			return visibleInUI();
		}

		public string GetConditionDescription()
		{
			return description;
		}
	}

	internal class LambdaDropRuleCondition3 : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		private readonly Func<DropAttemptInfo, bool> conditionLambda;

		private readonly Func<bool> visibleInUI;

		private readonly Func<string> description;

		internal LambdaDropRuleCondition3(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, Func<string> desc)
		{
			conditionLambda = lambda;
			visibleInUI = ui;
			description = desc;
		}

		public bool CanDrop(DropAttemptInfo info)
		{
			return conditionLambda(info);
		}

		public bool CanShowItemDropInUI()
		{
			return visibleInUI();
		}

		public string GetConditionDescription()
		{
			return description();
		}
	}

	public class AllOptionsAtOnceWithPityDropRule : IItemDropRule
	{
		public WeightedItemStack[] stacks;

		public Fraction dropRate;

		public bool usesLuck;

		public List<IItemDropRuleChainAttempt> ChainedRules { get; set; }

		public AllOptionsAtOnceWithPityDropRule(Fraction dropRate, bool luck, params WeightedItemStack[] stacks)
		{
			this.dropRate = dropRate;
			this.stacks = stacks;
			usesLuck = luck;
			ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		public AllOptionsAtOnceWithPityDropRule(Fraction dropRate, bool luck, params int[] itemIDs)
		{
			this.dropRate = dropRate;
			stacks = new WeightedItemStack[itemIDs.Length];
			for (int i = 0; i < stacks.Length; i++)
			{
				stacks[i] = itemIDs[i];
			}
			usesLuck = luck;
			ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			bool droppedAnything = false;
			WeightedItemStack[] array = stacks;
			for (int i = 0; i < array.Length; i++)
			{
				WeightedItemStack stack = array[i];
				bool rngRoll = (usesLuck ? (info.player.RollLuck(dropRate.denominator) < dropRate.numerator) : (info.rng.NextFloat() < (float)dropRate));
				droppedAnything |= rngRoll;
				if (rngRoll)
				{
					CommonCode.DropItem(info, stack.itemID, stack.ChooseQuantity(info.rng));
				}
			}
			if (!droppedAnything)
			{
				WeightedItemStack stack2 = info.rng.NextFromList(stacks);
				CommonCode.DropItem(info, stack2.itemID, stack2.ChooseQuantity(info.rng));
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			int numDrops = stacks.Length;
			float rawDropRate = dropRate;
			float dropRateAdjustedForParent = (rawDropRate + (float)(Math.Pow(1f - rawDropRate, numDrops) * (double)(1f / (float)numDrops))) * ratesInfo.parentDroprateChance;
			WeightedItemStack[] array = stacks;
			for (int i = 0; i < array.Length; i++)
			{
				WeightedItemStack stack = array[i];
				drops.Add(new DropRateInfo(stack.itemID, stack.minQuantity, stack.maxQuantity, dropRateAdjustedForParent, ratesInfo.conditions));
			}
			Chains.ReportDroprates(ChainedRules, rawDropRate, drops, ratesInfo);
		}
	}

	public class PerPlayerDropRule : CommonDrop
	{
		private const int DefaultDropProtectionTime = 18000;

		private int protectionTime;

		public PerPlayerDropRule(int itemID, int denominator, int minQuantity = 1, int maxQuantity = 1, int numerator = 1, int protectFrames = 18000)
			: base(itemID, denominator, minQuantity, maxQuantity, numerator)
		{
			protectionTime = protectFrames;
		}

		public PerPlayerDropRule(int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1)
			: base(itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator)
		{
			protectionTime = 18000;
		}

		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			ItemDropAttemptResult result = default(ItemDropAttemptResult);
			if (info.rng.Next(chanceDenominator) < chanceNumerator)
			{
				int stack = info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1);
				TryDropInternal(info, itemId, stack);
				result.State = ItemDropAttemptResultState.Success;
				return result;
			}
			result.State = ItemDropAttemptResultState.FailedRandomRoll;
			return result;
		}

		private void TryDropInternal(DropAttemptInfo info, int itemId, int stack)
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if (itemId <= 0 || itemId >= ItemLoader.ItemCount)
			{
				return;
			}
			if (Main.dedServ)
			{
				NPC npc = info.npc;
				int idx = Item.NewItem(npc.GetSource_Loot(), npc.Center, itemId, stack, noBroadcast: true, -1);
				if (idx < Main.maxItems)
				{
					Main.timeItemSlotCannotBeReusedFor[idx] = protectionTime;
					ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Player player = enumerator.Current;
						NetMessage.SendData(90, player.whoAmI, -1, null, idx);
					}
					Main.item[idx].active = false;
				}
			}
			else
			{
				CommonCode.DropItem(info, itemId, stack);
			}
		}
	}

	public const int NormalWeaponDropRateInt = 4;

	public const float NormalWeaponDropRateFloat = 0.25f;

	public static readonly Fraction NormalWeaponDropRateFraction = new Fraction(1, 4);

	public const int BagWeaponDropRateInt = 3;

	public const float BagWeaponDropRateFloat = 0.3333333f;

	public static readonly Fraction BagWeaponDropRateFraction = new Fraction(1, 3);

	public static string FirstKillText = CalamityUtils.GetTextValue("Condition.Drops.FirstKill");

	public static string MechBossText = CalamityUtils.GetTextValue("Condition.Drops.MechBoss");

	public static string CataclysmKilledLast = CalamityUtils.GetTextValue("Condition.Drops.CataclysmKilledLast");

	public static string CatastropheKilledLast = CalamityUtils.GetTextValue("Condition.Drops.CatastropheKilledLast");

	public static string CynosureText = CalamityUtils.GetTextValue("Condition.Drops.Cynosure");

	public static string ProvidenceEnragedText = CalamityUtils.GetTextValue("Condition.Drops.ProvidenceEnraged");

	public static string ProvidenceChallengeText = CalamityUtils.GetTextValue("Condition.Drops.ProvidenceChallenge");

	private static int[] AllLoadedItemIDs = null;

	public static IItemDropRuleCondition MythrilCondition = If((DropAttemptInfo info) => !CalamityServerConfig.Instance.EarlyHardmodeProgressionRework || NPC.downedMechBossAny);

	public static IItemDropRuleCondition AdamantiteCondition = If((Func<DropAttemptInfo, bool>)delegate
	{
		if (!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework)
		{
			return true;
		}
		return (NPC.downedMechBoss1 && NPC.downedMechBoss2) || (NPC.downedMechBoss2 && NPC.downedMechBoss3) || (NPC.downedMechBoss1 && NPC.downedMechBoss3);
	});

	public static IItemDropRuleCondition HallowedBarsCondition = If((Func<DropAttemptInfo, bool>)delegate
	{
		if (!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework)
		{
			return true;
		}
		return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
	});

	public static IItemDropRuleCondition GoldSetBonusGoldCondition = If(delegate(DropAttemptInfo info)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = info.npc;
		if (npc.IsAnEnemy(allowStatues: false))
		{
			return false;
		}
		Player player = info.player;
		if (player == null || !player.active)
		{
			player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
		}
		return player.Calamity().goldArmorGoldDrops;
	});

	public static IItemDropRuleCondition GoldSetBonusBossCondition = If(delegate(DropAttemptInfo info)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = info.npc;
		if (!npc.boss)
		{
			return false;
		}
		Player player = info.player;
		if (player == null || !player.active)
		{
			player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
		}
		return player.Calamity().goldArmorGoldDrops;
	});

	public static IItemDropRuleCondition TarragonSetBonusHeartCondition = If(delegate(DropAttemptInfo info)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = info.npc;
		if (npc.IsAnEnemy(allowStatues: false))
		{
			return false;
		}
		Player player = info.player;
		if (player == null || !player.active)
		{
			player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
		}
		return player.Calamity().tarraSet;
	});

	public static IItemDropRuleCondition Remix => Condition.RemixWorld.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied);

	public static IItemDropRuleCondition NotRemix => Condition.NotRemixWorld.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied);

	public static IItemDropRuleCondition GFB => Condition.ZenithWorld.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied);

	public static IItemDropRuleCondition RevNoMaster => CalamityConditions.InRevengeanceModeNotMasterMode.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied);

	public static IItemDropRuleCondition RevAndMaster => CalamityConditions.InRevengeanceModeOrMasterMode.ToDropCondition(ShowItemDropInUI.WhenConditionSatisfied);

	public static void BlockDrops(params int[] itemIDs)
	{
		foreach (int itemID in itemIDs)
		{
			NPCLoader.blockLoot.Add(itemID);
		}
	}

	public static void BlockEverything(params int[] exceptions)
	{
		if (AllLoadedItemIDs == null)
		{
			AllLoadedItemIDs = new int[ItemLoader.ItemCount];
			for (int i = 0; i < ItemLoader.ItemCount; i++)
			{
				AllLoadedItemIDs[i] = i;
			}
		}
		int[] withSomeExceptions = new int[ItemLoader.ItemCount];
		AllLoadedItemIDs.CopyTo(withSomeExceptions, 0);
		withSomeExceptions[58] = 678;
		withSomeExceptions[184] = 678;
		foreach (int itemID in exceptions)
		{
			withSomeExceptions[itemID] = 678;
		}
		BlockDrops(withSomeExceptions);
	}

	public static Item DropItemClone(IEntitySource src, Item item, Vector2 position, int stack = -1)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		int index = Item.NewItem(src, position, item.type, stack, noBroadcast: false, -1);
		Item theClone = (Main.item[index] = item.Clone());
		theClone.whoAmI = index;
		theClone.position = position;
		if (stack != -1)
		{
			theClone.stack = stack;
		}
		if (Main.netMode == 1)
		{
			NetMessage.SendData(21, -1, -1, null, index, 1f);
		}
		return theClone;
	}

	public static int FindClosestWormSegment(NPC wormHead, params int[] wormSegmentIDs)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		List<int> idsToCheck = new List<int>(wormSegmentIDs);
		Vector2 playerPos = Main.player[wormHead.target].Center;
		int r = wormHead.whoAmI;
		float minDist = 1000000f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (idsToCheck.Contains(n.type))
			{
				Vector2 val = n.Center - playerPos;
				float dist = ((Vector2)(ref val)).Length();
				if (dist < minDist)
				{
					minDist = dist;
					r = n.whoAmI;
				}
			}
		}
		return r;
	}

	public static LeadingConditionRule AddConditionalPerPlayer(this ILoot loot, Func<bool> lambda, int itemID, bool ui = true, string desc = null)
	{
		LeadingConditionRule lcr = new LeadingConditionRule(If(lambda, ui, desc));
		lcr.Add(PerPlayer(itemID));
		loot.Add(lcr);
		return lcr;
	}

	public static LeadingConditionRule AddConditionalPerPlayer(this ILoot loot, Func<DropAttemptInfo, bool> lambda, int itemID, bool ui = true, string desc = null)
	{
		LeadingConditionRule lcr = new LeadingConditionRule(If(lambda, ui, desc));
		lcr.Add(PerPlayer(itemID));
		loot.Add(lcr);
		return lcr;
	}

	public static DropBasedOnExpertMode NormalVsExpertQuantity(int itemID, int dropRateInt, int minNormal, int maxNormal, int minExpert, int maxExpert)
	{
		IItemDropRule ruleForNormalMode = ItemDropRule.Common(itemID, dropRateInt, minNormal, maxNormal);
		IItemDropRule expertRule = ItemDropRule.Common(itemID, dropRateInt, minExpert, maxExpert);
		return new DropBasedOnExpertMode(ruleForNormalMode, expertRule);
	}

	public static void AddRevBagAccessories(this ILoot loot)
	{
		LeadingConditionRule lcr = new LeadingConditionRule(If(() => CalamityWorld.revenge));
		lcr.Add(new OneFromOptionsDropRule(20, 1, ModContent.ItemType<Laudanum>(), ModContent.ItemType<HeartofDarkness>(), ModContent.ItemType<StressPills>()));
		loot.Add(lcr);
	}

	public static void AddBiomeCrateLootRules(this ILoot loot, bool hardMode = true)
	{
		if (hardMode)
		{
			loot.AddHardmodeOresToCrates(HardmodeCrateType.Biome);
		}
		else
		{
			IItemDropRule[] phmOres = new IItemDropRule[8]
			{
				ItemDropRule.NotScalingWithLuck(12, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(699, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(11, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(700, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(14, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(701, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(13, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(702, 1, 20, 35)
			};
			loot.Add(new OneFromRulesRule(7, phmOres));
			IItemDropRule[] phmBars = new IItemDropRule[6]
			{
				ItemDropRule.NotScalingWithLuck(22, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(704, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(21, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(705, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(19, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(706, 1, 6, 16)
			};
			loot.Add(new OneFromRulesRule(4, phmBars));
		}
		loot.Add(new OneFromRulesRule(4, ItemDropRule.NotScalingWithLuck(288, 1, 2, 4), ItemDropRule.NotScalingWithLuck(296, 1, 2, 4), ItemDropRule.NotScalingWithLuck(304, 1, 2, 4), ItemDropRule.NotScalingWithLuck(305, 1, 2, 4), ItemDropRule.NotScalingWithLuck(2322, 1, 2, 4), ItemDropRule.NotScalingWithLuck(2323, 1, 2, 4)));
		loot.Add(new OneFromRulesRule(2, ItemDropRule.NotScalingWithLuck(188, 1, 5, 17), ItemDropRule.NotScalingWithLuck(189, 1, 5, 17)));
		loot.Add(new OneFromRulesRule(2, ItemDropRule.NotScalingWithLuck(2676, 1, 2, 6), ItemDropRule.NotScalingWithLuck(2675, 1, 2, 6)));
		loot.Add(73, 4, 5, 12);
	}

	public static void AddHardmodeOresToCrates(this ILoot loot, HardmodeCrateType type)
	{
		LeadingConditionRule adamantiteLCR = loot.DefineConditionalDropSet(AdamantiteCondition);
		LeadingConditionRule mythrilLCR = new LeadingConditionRule(MythrilCondition);
		switch (type)
		{
		case HardmodeCrateType.Biome:
		{
			IItemDropRule[] phmOres3 = new IItemDropRule[8]
			{
				ItemDropRule.NotScalingWithLuck(12, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(699, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(11, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(700, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(14, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(701, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(13, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(702, 1, 20, 35)
			};
			IItemDropRule[] phmBars3 = new IItemDropRule[6]
			{
				ItemDropRule.NotScalingWithLuck(22, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(704, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(21, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(705, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(19, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(706, 1, 6, 16)
			};
			IItemDropRule[] hmOresThree2 = new IItemDropRule[6]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1104, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(365, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1105, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(366, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1106, 1, 20, 35)
			};
			IItemDropRule[] hmBarsThree2 = new IItemDropRule[6]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1184, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(382, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1191, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(391, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1198, 1, 5, 16)
			};
			SequentialRulesNotScalingWithLuckRule adamantiteDropsOres2 = new SequentialRulesNotScalingWithLuckRule(7, new OneFromRulesRule(2, hmOresThree2), new OneFromRulesRule(1, phmOres3));
			SequentialRulesNotScalingWithLuckRule adamantiteDropsBars2 = new SequentialRulesNotScalingWithLuckRule(4, new OneFromRulesRule(3, 2, hmBarsThree2), new OneFromRulesRule(1, phmBars3));
			IItemDropRule[] hmOresTwo3 = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1104, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(365, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1105, 1, 20, 35)
			};
			IItemDropRule[] hmBarsTwo3 = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1184, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(382, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1191, 1, 5, 16)
			};
			SequentialRulesNotScalingWithLuckRule mythrilDropsOres3 = new SequentialRulesNotScalingWithLuckRule(7, new OneFromRulesRule(2, hmOresTwo3), new OneFromRulesRule(1, phmOres3));
			SequentialRulesNotScalingWithLuckRule mythrilDropsBars3 = new SequentialRulesNotScalingWithLuckRule(4, new OneFromRulesRule(3, 2, hmBarsTwo3), new OneFromRulesRule(1, phmBars3));
			IItemDropRule[] hmOresOne3 = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1104, 1, 20, 35)
			};
			IItemDropRule[] hmBarsOne3 = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1184, 1, 5, 16)
			};
			SequentialRulesNotScalingWithLuckRule cobaltDropsOres3 = new SequentialRulesNotScalingWithLuckRule(7, new OneFromRulesRule(2, hmOresOne3), new OneFromRulesRule(1, phmOres3));
			SequentialRulesNotScalingWithLuckRule cobaltDropsBars3 = new SequentialRulesNotScalingWithLuckRule(4, new OneFromRulesRule(3, 2, hmBarsOne3), new OneFromRulesRule(1, phmBars3));
			adamantiteLCR.Add(adamantiteDropsOres2);
			adamantiteLCR.Add(adamantiteDropsBars2);
			adamantiteLCR.OnFailedConditions(mythrilLCR);
			mythrilLCR.Add(mythrilDropsOres3);
			mythrilLCR.Add(mythrilDropsBars3);
			mythrilLCR.OnFailedConditions(cobaltDropsOres3);
			mythrilLCR.OnFailedConditions(cobaltDropsBars3);
			break;
		}
		case HardmodeCrateType.Mythril:
		{
			IItemDropRule[] phmOres2 = new IItemDropRule[6]
			{
				ItemDropRule.NotScalingWithLuck(12, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(699, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(11, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(700, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(14, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(701, 1, 12, 21)
			};
			IItemDropRule[] phmBars2 = new IItemDropRule[6]
			{
				ItemDropRule.NotScalingWithLuck(20, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(703, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(22, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(704, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(21, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(705, 1, 4, 7)
			};
			IItemDropRule[] hmOresTwo2 = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(1104, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(365, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(1105, 1, 12, 21)
			};
			IItemDropRule[] hmBarsTwo2 = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(1184, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(382, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(1191, 1, 3, 7)
			};
			SequentialRulesNotScalingWithLuckRule mythrilDropsOres2 = new SequentialRulesNotScalingWithLuckRule(6, new OneFromRulesRule(2, hmOresTwo2), new OneFromRulesRule(1, phmOres2));
			SequentialRulesNotScalingWithLuckRule mythrilDropsBars2 = new SequentialRulesNotScalingWithLuckRule(4, new OneFromRulesRule(3, 2, hmBarsTwo2), new OneFromRulesRule(1, phmBars2));
			SequentialRulesNotScalingWithLuckRule mythrilDrops2 = new SequentialRulesNotScalingWithLuckRule(1, mythrilDropsOres2, mythrilDropsBars2);
			IItemDropRule[] hmOresOne2 = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(1104, 1, 12, 21)
			};
			IItemDropRule[] hmBarsOne2 = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(1184, 1, 3, 7)
			};
			SequentialRulesNotScalingWithLuckRule cobaltDropsOres2 = new SequentialRulesNotScalingWithLuckRule(6, new OneFromRulesRule(2, hmOresOne2), new OneFromRulesRule(1, phmOres2));
			SequentialRulesNotScalingWithLuckRule cobaltDropsBars2 = new SequentialRulesNotScalingWithLuckRule(4, new OneFromRulesRule(3, 2, hmBarsOne2), new OneFromRulesRule(1, phmBars2));
			SequentialRulesNotScalingWithLuckRule cobaltDrops2 = new SequentialRulesNotScalingWithLuckRule(1, cobaltDropsOres2, cobaltDropsBars2);
			mythrilLCR.Add(mythrilDrops2);
			mythrilLCR.OnFailedConditions(cobaltDrops2);
			loot.Add(mythrilLCR);
			break;
		}
		case HardmodeCrateType.Titanium:
		{
			IItemDropRule[] phmOres = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(14, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(701, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(13, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(702, 1, 25, 34)
			};
			IItemDropRule[] phmBars = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(21, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(705, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(19, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(706, 1, 8, 11)
			};
			IItemDropRule[] hmOresThree = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(365, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(1105, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(366, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(1106, 1, 25, 34)
			};
			IItemDropRule[] hmBarsThree = new IItemDropRule[4]
			{
				ItemDropRule.NotScalingWithLuck(382, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(1191, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(391, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(1198, 1, 8, 11)
			};
			SequentialRulesNotScalingWithLuckRule adamantiteDropsOres = new SequentialRulesNotScalingWithLuckRule(5, new OneFromRulesRule(2, hmOresThree), new OneFromRulesRule(1, phmOres));
			SequentialRulesNotScalingWithLuckRule adamantiteDropsBars = new SequentialRulesNotScalingWithLuckRule(3, new OneFromRulesRule(3, 2, hmBarsThree), new OneFromRulesRule(1, phmBars));
			SequentialRulesNotScalingWithLuckRule adamantiteDrops = new SequentialRulesNotScalingWithLuckRule(1, adamantiteDropsOres, adamantiteDropsBars);
			IItemDropRule[] hmOresTwo = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(365, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(1105, 1, 25, 34)
			};
			IItemDropRule[] hmBarsTwo = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(382, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(1191, 1, 8, 11)
			};
			SequentialRulesNotScalingWithLuckRule mythrilDropsOres = new SequentialRulesNotScalingWithLuckRule(5, new OneFromRulesRule(2, hmOresTwo), new OneFromRulesRule(1, phmOres));
			SequentialRulesNotScalingWithLuckRule mythrilDropsBars = new SequentialRulesNotScalingWithLuckRule(3, new OneFromRulesRule(3, 2, hmBarsTwo), new OneFromRulesRule(1, phmBars));
			SequentialRulesNotScalingWithLuckRule mythrilDrops = new SequentialRulesNotScalingWithLuckRule(1, mythrilDropsOres, mythrilDropsBars);
			IItemDropRule[] hmOresOne = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(1104, 1, 25, 34)
			};
			IItemDropRule[] hmBarsOne = new IItemDropRule[2]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(1184, 1, 8, 11)
			};
			SequentialRulesNotScalingWithLuckRule cobaltDropsOres = new SequentialRulesNotScalingWithLuckRule(5, new OneFromRulesRule(2, hmOresOne), new OneFromRulesRule(1, phmOres));
			SequentialRulesNotScalingWithLuckRule cobaltDropsBars = new SequentialRulesNotScalingWithLuckRule(3, new OneFromRulesRule(3, 2, hmBarsOne), new OneFromRulesRule(1, phmBars));
			SequentialRulesNotScalingWithLuckRule cobaltDrops = new SequentialRulesNotScalingWithLuckRule(1, cobaltDropsOres, cobaltDropsBars);
			adamantiteLCR.Add(adamantiteDrops);
			adamantiteLCR.OnFailedConditions(mythrilLCR);
			mythrilLCR.Add(mythrilDrops);
			mythrilLCR.OnFailedConditions(cobaltDrops);
			break;
		}
		}
	}

	private static int RecursivelyMutateDropRate(this IItemDropRule rule, int itemID, int newNumerator, int newDenominator)
	{
		if (rule is CommonDrop drop && drop.itemId == itemID)
		{
			drop.chanceNumerator = newNumerator;
			drop.chanceDenominator = newDenominator;
			return 1;
		}
		if (rule is ItemDropWithConditionRule conditionalDrop && conditionalDrop.itemId == itemID)
		{
			conditionalDrop.chanceNumerator = newNumerator;
			conditionalDrop.chanceDenominator = newDenominator;
			return 1;
		}
		if (rule is DropBasedOnExpertMode expertDrop)
		{
			int num = expertDrop.ruleForNormalMode.RecursivelyMutateDropRate(itemID, newNumerator, newDenominator);
			int expertChanges = expertDrop.ruleForExpertMode.RecursivelyMutateDropRate(itemID, newNumerator, newDenominator);
			return num + expertChanges;
		}
		if (rule is DropBasedOnMasterMode masterDrop)
		{
			int num2 = masterDrop.ruleForDefault.RecursivelyMutateDropRate(itemID, newNumerator, newDenominator);
			int masterChanges = masterDrop.ruleForMasterMode.RecursivelyMutateDropRate(itemID, newNumerator, newDenominator);
			return num2 + masterChanges;
		}
		return 0;
	}

	public static IItemDropRuleCondition If(Func<bool> lambda)
	{
		return new LambdaDropRuleCondition((DropAttemptInfo _) => lambda());
	}

	public static IItemDropRuleCondition If(Func<bool> lambda, bool ui = true, string desc = null)
	{
		return new LambdaDropRuleCondition(LambdaInfoWrapper, ui, desc);
		bool LambdaInfoWrapper(DropAttemptInfo _)
		{
			return lambda();
		}
	}

	public static IItemDropRuleCondition If(Func<bool> lambda, Func<bool> ui, string desc = null)
	{
		return new LambdaDropRuleCondition2(LambdaInfoWrapper, ui, desc);
		bool LambdaInfoWrapper(DropAttemptInfo _)
		{
			return lambda();
		}
	}

	public static IItemDropRuleCondition If(Func<bool> lambda, Func<bool> ui, Func<string> desc)
	{
		return new LambdaDropRuleCondition3(LambdaInfoWrapper, ui, desc);
		bool LambdaInfoWrapper(DropAttemptInfo _)
		{
			return lambda();
		}
	}

	public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda)
	{
		return new LambdaDropRuleCondition(lambda);
	}

	public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda, bool ui = true, string desc = null)
	{
		return new LambdaDropRuleCondition(lambda, ui, desc);
	}

	public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, string desc = null)
	{
		return new LambdaDropRuleCondition2(lambda, ui, desc);
	}

	public static IItemDropRuleCondition If(Func<DropAttemptInfo, bool> lambda, Func<bool> ui, Func<string> desc)
	{
		return new LambdaDropRuleCondition3(lambda, ui, desc);
	}

	public static IItemDropRuleCondition PostKS(bool ui = true)
	{
		return Condition.DownedKingSlime.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostDS(bool ui = true)
	{
		return CalamityConditions.DownedDesertScourge.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostEoC(bool ui = true)
	{
		return Condition.DownedEyeOfCthulhu.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostCrab(bool ui = true)
	{
		return CalamityConditions.DownedCrabulon.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostEvil1(bool ui = true)
	{
		return Condition.DownedEowOrBoc.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostHM(bool ui = true)
	{
		return CalamityConditions.DownedHiveMind.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostPerfs(bool ui = true)
	{
		return CalamityConditions.DownedPerforator.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostEvil2(bool ui = true)
	{
		return CalamityConditions.DownedHiveMindOrPerforator.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostQB(bool ui = true)
	{
		return Condition.DownedQueenBee.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostDeer(bool ui = true)
	{
		return Condition.DownedDeerclops.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostSkele(bool ui = true)
	{
		return Condition.DownedSkeletron.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostSG(bool ui = true)
	{
		return CalamityConditions.DownedSlimeGod.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition Hardmode(bool ui = true)
	{
		return Condition.Hardmode.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostQS(bool ui = true)
	{
		return Condition.DownedQueenSlime.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostCryo(bool ui = true)
	{
		return CalamityConditions.DownedCryogen.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostAS(bool ui = true)
	{
		return CalamityConditions.DownedAquaticScourge.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostBrim(bool ui = true)
	{
		return CalamityConditions.DownedBrimstoneElemental.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostDest(bool ui = true)
	{
		return Condition.DownedDestroyer.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostTwins(bool ui = true)
	{
		return Condition.DownedTwins.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostSP(bool ui = true)
	{
		return Condition.DownedSkeletronPrime.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition Post1Mech(bool ui = true)
	{
		return Condition.DownedMechBossAny.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition Post3Mechs(bool ui = true)
	{
		return Condition.DownedMechBossAll.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostCal(bool ui = true)
	{
		return CalamityConditions.DownedCalamitasClone.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostPlant(bool ui = true)
	{
		return Condition.DownedPlantera.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostCalPlant(bool ui = true)
	{
		return CalamityConditions.DownedCalamitasCloneOrPlantera.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostLevi(bool ui = true)
	{
		return CalamityConditions.DownedLeviathan.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostAureus(bool ui = true)
	{
		return CalamityConditions.DownedAstrumAureus.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostGolem(bool ui = true)
	{
		return Condition.DownedGolem.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostPBG(bool ui = true)
	{
		return CalamityConditions.DownedPlaguebringer.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostEoL(bool ui = true)
	{
		return Condition.DownedEmpressOfLight.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostFish(bool ui = true)
	{
		return Condition.DownedDukeFishron.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostRav(bool ui = true)
	{
		return CalamityConditions.DownedRavager.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostLC(bool ui = true)
	{
		return Condition.DownedCultist.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostAD(bool ui = true)
	{
		return CalamityConditions.DownedAstrumDeus.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostML(bool ui = true)
	{
		return Condition.DownedMoonLord.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostGuard(bool ui = true)
	{
		return CalamityConditions.DownedGuardians.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostBirb(bool ui = true)
	{
		return CalamityConditions.DownedBumblebird.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostProv(bool ui = true)
	{
		return CalamityConditions.DownedProvidence.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostSig(bool ui = true)
	{
		return CalamityConditions.DownedSignus.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostSW(bool ui = true)
	{
		return CalamityConditions.DownedStormWeaver.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostCV(bool ui = true)
	{
		return CalamityConditions.DownedCeaselessVoid.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostPolter(bool ui = true)
	{
		return CalamityConditions.DownedPolterghast.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostOD(bool ui = true)
	{
		return CalamityConditions.DownedOldDuke.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostDoG(bool ui = true)
	{
		return CalamityConditions.DownedDevourerOfGods.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostYharon(bool ui = true)
	{
		return CalamityConditions.DownedYharon.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostExos(bool ui = true)
	{
		return CalamityConditions.DownedExoMechs.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostSCal(bool ui = true)
	{
		return CalamityConditions.DownedSupremeCalamitas.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostAEW(bool ui = true)
	{
		return CalamityConditions.DownedPrimordialWyrm.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostClam(bool ui = true)
	{
		return CalamityConditions.DownedClam.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostClamHM(bool ui = true)
	{
		return CalamityConditions.DownedBuffedClam.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostGSS(bool ui = true)
	{
		return CalamityConditions.DownedGreatSandShark.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostBetsy(bool ui = true)
	{
		return CalamityConditions.DownedBetsy.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostT1AR(bool ui = true)
	{
		return CalamityConditions.DownedAcidRainT1.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRuleCondition PostT2AR(bool ui = true)
	{
		return CalamityConditions.DownedAcidRainT2.ToDropCondition((!ui) ? ShowItemDropInUI.Never : ShowItemDropInUI.Always);
	}

	public static IItemDropRule Add(this LeadingConditionRule mainRule, IItemDropRule chainedRule, bool hideLootReport = false)
	{
		return mainRule.OnSuccess(chainedRule, hideLootReport);
	}

	public static IItemDropRule Add(this LeadingConditionRule mainRule, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false)
	{
		return mainRule.OnSuccess(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity), hideLootReport);
	}

	public static IItemDropRule Add(this LeadingConditionRule mainRule, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false)
	{
		return mainRule.OnSuccess(new CommonDrop(itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator), hideLootReport);
	}

	public static IItemDropRule AddIf(this LeadingConditionRule mainRule, Func<bool> lambda, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false, string desc = null)
	{
		return mainRule.OnSuccess(ItemDropRule.ByCondition(If(lambda, ui: true, desc), itemID, dropRateInt, minQuantity, maxQuantity), hideLootReport);
	}

	public static IItemDropRule AddIf(this LeadingConditionRule mainRule, Func<bool> lambda, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false, string desc = null)
	{
		return mainRule.OnSuccess(ItemDropRule.ByCondition(If(lambda, ui: true, desc), itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator), hideLootReport);
	}

	public static IItemDropRule AddIf(this LeadingConditionRule mainRule, Func<DropAttemptInfo, bool> lambda, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false, string desc = null)
	{
		return mainRule.OnSuccess(ItemDropRule.ByCondition(If(lambda, ui: true, desc), itemID, dropRateInt, minQuantity, maxQuantity), hideLootReport);
	}

	public static IItemDropRule AddIf(this LeadingConditionRule mainRule, Func<DropAttemptInfo, bool> lambda, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false, string desc = null)
	{
		return mainRule.OnSuccess(ItemDropRule.ByCondition(If(lambda, ui: true, desc), itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator), hideLootReport);
	}

	public static IItemDropRule AddFail(this LeadingConditionRule mainRule, IItemDropRule chainedRule, bool hideLootReport = false)
	{
		return mainRule.OnFailedConditions(chainedRule, hideLootReport);
	}

	public static IItemDropRule AddFail(this LeadingConditionRule mainRule, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false)
	{
		return mainRule.OnFailedConditions(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity), hideLootReport);
	}

	public static IItemDropRule AddFail(this LeadingConditionRule mainRule, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false)
	{
		return mainRule.OnFailedConditions(new CommonDrop(itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator), hideLootReport);
	}

	public static IItemDropRule Add(this ILoot loot, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
	{
		return loot.Add(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity));
	}

	public static IItemDropRule Add(this ILoot loot, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1)
	{
		return loot.Add(new CommonDrop(itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator));
	}

	public static IItemDropRule AddIf(this ILoot loot, IItemDropRuleCondition cond, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
	{
		return loot.Add(ItemDropRule.ByCondition(cond, itemID, dropRateInt, minQuantity, maxQuantity));
	}

	public static IItemDropRule AddIf(this ILoot loot, IItemDropRuleCondition cond, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1)
	{
		return loot.Add(ItemDropRule.ByCondition(cond, itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator));
	}

	public static IItemDropRule AddIf(this ILoot loot, Func<bool> lambda, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool ui = true, string desc = null)
	{
		return loot.Add(ItemDropRule.ByCondition(If(lambda, ui, desc), itemID, dropRateInt, minQuantity, maxQuantity));
	}

	public static IItemDropRule AddIf(this ILoot loot, Func<bool> lambda, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1, bool ui = true, string desc = null)
	{
		return loot.Add(ItemDropRule.ByCondition(If(lambda, ui, desc), itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator));
	}

	public static IItemDropRule AddIf(this ILoot loot, Func<DropAttemptInfo, bool> lambda, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool ui = true, string desc = null)
	{
		return loot.Add(ItemDropRule.ByCondition(If(lambda, ui, desc), itemID, dropRateInt, minQuantity, maxQuantity));
	}

	public static IItemDropRule AddIf(this ILoot loot, Func<DropAttemptInfo, bool> lambda, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1, bool ui = true, string desc = null)
	{
		return loot.Add(ItemDropRule.ByCondition(If(lambda, ui, desc), itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator));
	}

	public static IItemDropRule AddNormalOnly(this ILoot loot, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
	{
		return loot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), itemID, dropRateInt, minQuantity, maxQuantity));
	}

	public static IItemDropRule AddNormalOnly(this ILoot loot, int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1)
	{
		return loot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator));
	}

	public static void AddNormalOnly(this ILoot loot, IItemDropRule rule)
	{
		loot.DefineNormalOnlyDropSet().Add(rule);
	}

	public static LeadingConditionRule DefineConditionalDropSet(this ILoot loot, IItemDropRuleCondition condition)
	{
		LeadingConditionRule rule = new LeadingConditionRule(condition);
		loot.Add(rule);
		return rule;
	}

	public static LeadingConditionRule DefineConditionalDropSet(this ILoot loot, Func<bool> lambda)
	{
		return loot.DefineConditionalDropSet(If(lambda));
	}

	public static LeadingConditionRule DefineConditionalDropSet(this ILoot loot, Func<DropAttemptInfo, bool> lambda)
	{
		return loot.DefineConditionalDropSet(If(lambda));
	}

	public static LeadingConditionRule DefineNormalOnlyDropSet(this ILoot loot)
	{
		return loot.DefineConditionalDropSet(new Conditions.NotExpert());
	}

	public static int ChangeDropRate(this ILoot loot, int itemID, int newNumerator, int newDenominator, bool includeGlobalDrops = false)
	{
		int numChanges = 0;
		foreach (IItemDropRule item in loot.Get(includeGlobalDrops))
		{
			item.RecursivelyMutateDropRate(itemID, newNumerator, newDenominator);
		}
		return numChanges;
	}

	public static int ChangeDropRate(this ILoot loot, int itemID, Fraction dropRate, bool includeGlobalDrops = false)
	{
		return loot.ChangeDropRate(itemID, dropRate.numerator, dropRate.denominator, includeGlobalDrops);
	}

	public static IItemDropRule CalamityStyle(Fraction dropRateForEachItem, params WeightedItemStack[] stacks)
	{
		return CalamityStyle(dropRateForEachItem, luck: true, stacks);
	}

	public static IItemDropRule CalamityStyle(Fraction dropRateForEachItem, bool luck, params WeightedItemStack[] stacks)
	{
		return new AllOptionsAtOnceWithPityDropRule(dropRateForEachItem, luck, stacks);
	}

	public static IItemDropRule CalamityStyle(Fraction dropRateForEachItem, params int[] itemIDs)
	{
		return CalamityStyle(dropRateForEachItem, luck: true, itemIDs);
	}

	public static IItemDropRule CalamityStyle(Fraction dropRateForEachItem, bool luck, params int[] itemIDs)
	{
		return new AllOptionsAtOnceWithPityDropRule(dropRateForEachItem, luck, itemIDs);
	}

	public static IItemDropRule PerPlayer(int itemID, int denominator = 1, int minQuantity = 1, int maxQuantity = 1, int numerator = 1)
	{
		return new PerPlayerDropRule(itemID, denominator, minQuantity, maxQuantity, numerator);
	}

	public static IItemDropRule PerPlayer(int itemID, Fraction dropRate, int minQuantity = 1, int maxQuantity = 1)
	{
		return PerPlayer(itemID, dropRate.denominator, minQuantity, maxQuantity, dropRate.numerator);
	}
}
