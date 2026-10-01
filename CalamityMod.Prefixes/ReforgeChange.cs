using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Packets;
using CalamityMod.World;
using Terraria;
using Terraria.GameContent.Prefixes;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Prefixes;

public sealed class ReforgeChange : GlobalItem
{
	private static int storedPrefix = -1;

	[Obsolete("No longer used with the removal of the Simplify Accessory Reforge config")]
	public static int[] SimplifiedAccessoryPrefixes;

	[Obsolete("No longer used with the removal of the Simplify Accessory Reforge config")]
	public static int[][] AccessoryPrefixTiers;

	public static int[][] TerrarianPrefixTiers;

	public static int[][] MeleePrefixTiers;

	public static int[][] MeleeNoSpeedPrefixTiers;

	public static int[][] MeleeNoSpeedAlwaysCritPrefixTiers;

	public static int[][] ToolPrefixTiers;

	public static int[][] RangedPrefixTiers;

	public static int[][] MagicPrefixTiers;

	public static int[][] SummonPrefixTiers;

	public static int[][] RoguePrefixTiers;

	public override bool InstancePerEntity => false;

	public override void PreReforge(Item item)
	{
		storedPrefix = item.prefix;
	}

	public override int ChoosePrefix(Item item, UnifiedRandom rand)
	{
		if (storedPrefix == -1 && item.CountsAsClass<RogueDamageClass>() && (item.maxStack == 1 || item.AllowReforgeForStackableItem))
		{
			int prefix = CalamityUtils.RandomRoguePrefix();
			if (CalamityUtils.NegativeRoguePrefix(prefix) && !Main.rand.NextBool(3))
			{
				return 0;
			}
			return prefix;
		}
		if (Main.gameMenu)
		{
			return -1;
		}
		if (storedPrefix != -1 && !item.accessory && CalamityServerConfig.Instance.RemoveReforgeRNG)
		{
			return GetReworkedReforge(item, rand, storedPrefix);
		}
		return -1;
	}

	public override void PostReforge(Item item)
	{
		storedPrefix = -1;
		if (NPC.AnyNPCs(ModContent.NPCType<Bandit>()))
		{
			int value = item.value;
			Player p = Main.LocalPlayer;
			ItemLoader.ReforgePrice(item, ref value, ref p.discountAvailable);
			int stolen = value / 5;
			CalamityWorld.MoneyStolenByBandit += stolen;
			CalamityWorld.Reforges++;
			if (Main.netMode == 1)
			{
				BanditStolenMoneySyncPacket.Send(stolen);
			}
		}
	}

	internal static int GetReworkedReforge(Item item, UnifiedRandom rand, int currentPrefix)
	{
		int prefix = -1;
		bool supportsLegendary = PrefixLegacy.ItemSets.SwordsHammersAxesPicks[item.type] || (item.ModItem != null && item.ModItem.MeleePrefix());
		if ((item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>()) && (!item.CountsAsClass<MeleeRangedHybridDamageClass>() || supportsLegendary) && item.type != ModContent.ItemType<TheBurningSky>())
		{
			if (PrefixLegacy.ItemSets.ItemsThatCanHaveLegendary2[item.type])
			{
				prefix = IteratePrefix(rand, TerrarianPrefixTiers, currentPrefix);
			}
			else if (supportsLegendary)
			{
				int[][] tierListToUse = ((item.pick > 0 || item.axe > 0 || item.hammer > 0) ? ToolPrefixTiers : MeleePrefixTiers);
				prefix = IteratePrefix(rand, tierListToUse, currentPrefix);
			}
			else
			{
				bool has100Crit = Main.LocalPlayer.GetTotalCritChance(item.DamageType) >= 100f;
				prefix = IteratePrefix(rand, has100Crit ? MeleeNoSpeedAlwaysCritPrefixTiers : MeleeNoSpeedPrefixTiers, currentPrefix);
			}
		}
		else if (item.CountsAsClass<RangedDamageClass>() || item.type == ModContent.ItemType<TheBurningSky>())
		{
			prefix = IteratePrefix(rand, RangedPrefixTiers, currentPrefix);
		}
		else if (item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<MagicSummonHybridDamageClass>())
		{
			prefix = IteratePrefix(rand, MagicPrefixTiers, currentPrefix);
		}
		else if (item.CountsAsClass<SummonDamageClass>())
		{
			prefix = IteratePrefix(rand, SummonPrefixTiers, currentPrefix);
		}
		else if (item.CountsAsClass<ThrowingDamageClass>())
		{
			prefix = IteratePrefix(rand, RoguePrefixTiers, currentPrefix);
		}
		return prefix;
	}

	private static int GetPrefixTier(int[][] tiers, int currentPrefix)
	{
		for (int checkingTier = 0; checkingTier < tiers.Length; checkingTier++)
		{
			int[] tierList = tiers[checkingTier];
			for (int i = 0; i < tierList.Length; i++)
			{
				if (tierList[i] == currentPrefix)
				{
					return checkingTier;
				}
			}
		}
		return -1;
	}

	private static int IteratePrefix(UnifiedRandom rand, int[][] reforgeTiers, int currentPrefix)
	{
		int currentTier = GetPrefixTier(reforgeTiers, currentPrefix);
		int newTier = ((currentTier == reforgeTiers.Length - 1) ? currentTier : (currentTier + 1));
		return rand.Next(reforgeTiers[newTier]);
	}

	static ReforgeChange()
	{
		int[] obj = new int[9] { 65, 72, 68, 76, 80, 66, 0, 0, 0 };
		obj[6] = ModContent.PrefixType<Silent>();
		obj[7] = ModContent.PrefixType<Invigorating>();
		obj[8] = ModContent.PrefixType<Dauntless>();
		SimplifiedAccessoryPrefixes = obj;
		int[][] obj2 = new int[4][]
		{
			new int[4] { 62, 69, 73, 77 },
			new int[5] { 63, 70, 67, 74, 78 },
			new int[5] { 64, 71, 75, 79, 66 },
			null
		};
		int[] obj3 = new int[8] { 65, 72, 68, 76, 80, 0, 0, 0 };
		obj3[5] = ModContent.PrefixType<Silent>();
		obj3[6] = ModContent.PrefixType<Dauntless>();
		obj3[7] = ModContent.PrefixType<Invigorating>();
		obj2[3] = obj3;
		AccessoryPrefixTiers = obj2;
		TerrarianPrefixTiers = new int[4][]
		{
			new int[3] { 36, 38, 54 },
			new int[3] { 53, 57, 61 },
			new int[3] { 37, 60, 59 },
			new int[1] { 84 }
		};
		MeleePrefixTiers = new int[6][]
		{
			new int[8] { 36, 45, 51, 15, 14, 15, 38, 54 },
			new int[6] { 53, 57, 61, 42, 6, 12 },
			new int[5] { 46, 44, 1, 3, 5 },
			new int[4] { 2, 55, 4, 37 },
			new int[3] { 60, 43, 59 },
			new int[1] { 81 }
		};
		MeleeNoSpeedPrefixTiers = new int[4][]
		{
			new int[3] { 36, 38, 54 },
			new int[3] { 53, 57, 61 },
			new int[2] { 37, 60 },
			new int[1] { 59 }
		};
		MeleeNoSpeedAlwaysCritPrefixTiers = new int[4][]
		{
			new int[3] { 36, 38, 54 },
			new int[3] { 53, 57, 61 },
			new int[2] { 37, 60 },
			new int[2] { 59, 57 }
		};
		ToolPrefixTiers = new int[6][]
		{
			new int[6] { 36, 45, 51, 14, 38, 54 },
			new int[6] { 53, 57, 61, 42, 6, 12 },
			new int[5] { 46, 44, 1, 3, 5 },
			new int[4] { 2, 55, 4, 37 },
			new int[3] { 60, 43, 59 },
			new int[2] { 81, 15 }
		};
		RangedPrefixTiers = new int[6][]
		{
			new int[6] { 36, 45, 51, 25, 38, 54 },
			new int[5] { 53, 57, 61, 42, 19 },
			new int[5] { 46, 44, 18, 21, 55 },
			new int[3] { 37, 60, 16 },
			new int[4] { 59, 17, 20, 43 },
			new int[1] { 82 }
		};
		MagicPrefixTiers = new int[6][]
		{
			new int[6] { 36, 45, 51, 35, 38, 54 },
			new int[6] { 53, 57, 61, 42, 33, 52 },
			new int[5] { 46, 44, 27, 34, 55 },
			new int[3] { 37, 60, 26 },
			new int[3] { 59, 28, 43 },
			new int[1] { 83 }
		};
		SummonPrefixTiers = new int[6][]
		{
			new int[2] { 45, 35 },
			new int[5] { 38, 54, 42, 33, 52 },
			new int[3] { 53, 27, 34 },
			new int[4] { 37, 60, 26, 43 },
			new int[2] { 28, 59 },
			new int[2] { 83, 57 }
		};
		int[][] array = new int[6][];
		int[] obj4 = new int[7] { 36, 45, 51, 38, 54, 0, 0 };
		obj4[5] = ModContent.PrefixType<Radical>();
		obj4[6] = ModContent.PrefixType<Pointy>();
		array[0] = obj4;
		int[] obj5 = new int[6] { 53, 57, 61, 42, 0, 0 };
		obj5[4] = ModContent.PrefixType<Sharp>();
		obj5[5] = ModContent.PrefixType<Glorious>();
		array[1] = obj5;
		int[] obj6 = new int[6] { 46, 44, 55, 0, 0, 0 };
		obj6[3] = ModContent.PrefixType<Feathered>();
		obj6[4] = ModContent.PrefixType<Sleek>();
		obj6[5] = ModContent.PrefixType<Hefty>();
		array[2] = obj6;
		array[3] = new int[4]
		{
			37,
			60,
			ModContent.PrefixType<Mighty>(),
			ModContent.PrefixType<Serrated>()
		};
		array[4] = new int[4]
		{
			59,
			43,
			ModContent.PrefixType<Vicious>(),
			ModContent.PrefixType<Lethal>()
		};
		array[5] = new int[1] { ModContent.PrefixType<Flawless>() };
		RoguePrefixTiers = array;
	}
}
