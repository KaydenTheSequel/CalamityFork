using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class CalamityNPCTypeSets
{
	public static SetFactory Factory = new SetFactory(NPCLoader.NPCCount, "CalamityMod/NPCType", Search);

	public static IdDictionary Search = IdDictionary.Create<NPCID, int>();

	public static bool[] AngryBones = Factory.CreateBoolSet(31, 294, 295, 296);

	public static bool[] BoundTownNPC = Factory.CreateBoolSet(105, 106, 123, 376, 579, 354, 589);

	public static bool[] Hornet = Factory.CreateBoolSet(42, 231, 232, 233, 234, 235);

	public static bool[] Skeleton = Factory.CreateBoolSet(21, 201, 202, 203, 449, 450, 451, 452, 322, 323, 324, 77, 110, 481, 635);

	public static bool[] Zombie;

	public static List<int> AquaticScourge;

	public static List<int> Ares;

	public static List<int> AstrumDeus;

	public static List<int> DesertScourge;

	public static List<int> Destroyer;

	public static List<int> DevourerOfGods;

	public static List<int> EaterOfWorlds;

	public static List<int> Perforators;

	public static List<int> Ravager;

	public static List<int> SkeletronPrime;

	public static List<int> SlimeGod;

	public static List<int> StormWeaver;

	public static List<int> Thanatos;

	static CalamityNPCTypeSets()
	{
		SetFactory factory = Factory;
		int[] obj = new int[18]
		{
			3, 430, 132, 186, 432, 187, 433, 188, 434, 189,
			435, 200, 436, 223, 161, 431, 632, 0
		};
		obj[17] = ModContent.NPCType<BucketZombie>();
		Zombie = factory.CreateBoolSet(obj);
		int num = 4;
		List<int> list = new List<int>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<int> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = ModContent.NPCType<AquaticScourgeHead>();
		num2++;
		span[num2] = ModContent.NPCType<AquaticScourgeBody>();
		num2++;
		span[num2] = ModContent.NPCType<AquaticScourgeBodyAlt>();
		num2++;
		span[num2] = ModContent.NPCType<AquaticScourgeTail>();
		AquaticScourge = list;
		num2 = 5;
		List<int> list2 = new List<int>(num2);
		CollectionsMarshal.SetCount(list2, num2);
		Span<int> span2 = CollectionsMarshal.AsSpan(list2);
		num = 0;
		span2[num] = ModContent.NPCType<AresBody>();
		num++;
		span2[num] = ModContent.NPCType<AresGaussNuke>();
		num++;
		span2[num] = ModContent.NPCType<AresLaserCannon>();
		num++;
		span2[num] = ModContent.NPCType<AresPlasmaFlamethrower>();
		num++;
		span2[num] = ModContent.NPCType<AresTeslaCannon>();
		Ares = list2;
		num = 3;
		List<int> list3 = new List<int>(num);
		CollectionsMarshal.SetCount(list3, num);
		Span<int> span3 = CollectionsMarshal.AsSpan(list3);
		num2 = 0;
		span3[num2] = ModContent.NPCType<AstrumDeusHead>();
		num2++;
		span3[num2] = ModContent.NPCType<AstrumDeusBody>();
		num2++;
		span3[num2] = ModContent.NPCType<AstrumDeusTail>();
		AstrumDeus = list3;
		num2 = 3;
		List<int> list4 = new List<int>(num2);
		CollectionsMarshal.SetCount(list4, num2);
		Span<int> span4 = CollectionsMarshal.AsSpan(list4);
		num = 0;
		span4[num] = ModContent.NPCType<DesertScourgeHead>();
		num++;
		span4[num] = ModContent.NPCType<DesertScourgeBody>();
		num++;
		span4[num] = ModContent.NPCType<DesertScourgeTail>();
		DesertScourge = list4;
		num = 3;
		List<int> list5 = new List<int>(num);
		CollectionsMarshal.SetCount(list5, num);
		Span<int> span5 = CollectionsMarshal.AsSpan(list5);
		num2 = 0;
		span5[num2] = 134;
		num2++;
		span5[num2] = 135;
		num2++;
		span5[num2] = 136;
		Destroyer = list5;
		num2 = 3;
		List<int> list6 = new List<int>(num2);
		CollectionsMarshal.SetCount(list6, num2);
		Span<int> span6 = CollectionsMarshal.AsSpan(list6);
		num = 0;
		span6[num] = ModContent.NPCType<DevourerofGodsHead>();
		num++;
		span6[num] = ModContent.NPCType<DevourerofGodsBody>();
		num++;
		span6[num] = ModContent.NPCType<DevourerofGodsTail>();
		DevourerOfGods = list6;
		num = 3;
		List<int> list7 = new List<int>(num);
		CollectionsMarshal.SetCount(list7, num);
		Span<int> span7 = CollectionsMarshal.AsSpan(list7);
		num2 = 0;
		span7[num2] = 13;
		num2++;
		span7[num2] = 14;
		num2++;
		span7[num2] = 15;
		EaterOfWorlds = list7;
		num2 = 9;
		List<int> list8 = new List<int>(num2);
		CollectionsMarshal.SetCount(list8, num2);
		Span<int> span8 = CollectionsMarshal.AsSpan(list8);
		num = 0;
		span8[num] = ModContent.NPCType<PerforatorHeadLarge>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorBodyLarge>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorTailLarge>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorHeadMedium>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorBodyMedium>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorTailMedium>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorHeadSmall>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorBodySmall>();
		num++;
		span8[num] = ModContent.NPCType<PerforatorTailSmall>();
		Perforators = list8;
		num = 6;
		List<int> list9 = new List<int>(num);
		CollectionsMarshal.SetCount(list9, num);
		Span<int> span9 = CollectionsMarshal.AsSpan(list9);
		num2 = 0;
		span9[num2] = ModContent.NPCType<RavagerBody>();
		num2++;
		span9[num2] = ModContent.NPCType<RavagerClawLeft>();
		num2++;
		span9[num2] = ModContent.NPCType<RavagerClawRight>();
		num2++;
		span9[num2] = ModContent.NPCType<RavagerLegLeft>();
		num2++;
		span9[num2] = ModContent.NPCType<RavagerLegRight>();
		num2++;
		span9[num2] = ModContent.NPCType<RavagerHead>();
		Ravager = list9;
		num2 = 5;
		List<int> list10 = new List<int>(num2);
		CollectionsMarshal.SetCount(list10, num2);
		Span<int> span10 = CollectionsMarshal.AsSpan(list10);
		num = 0;
		span10[num] = 127;
		num++;
		span10[num] = 128;
		num++;
		span10[num] = 131;
		num++;
		span10[num] = 129;
		num++;
		span10[num] = 130;
		SkeletronPrime = list10;
		num = 5;
		List<int> list11 = new List<int>(num);
		CollectionsMarshal.SetCount(list11, num);
		Span<int> span11 = CollectionsMarshal.AsSpan(list11);
		num2 = 0;
		span11[num2] = ModContent.NPCType<EbonianPaladin>();
		num2++;
		span11[num2] = ModContent.NPCType<CrimulanPaladin>();
		num2++;
		span11[num2] = ModContent.NPCType<SplitEbonianPaladin>();
		num2++;
		span11[num2] = ModContent.NPCType<SplitCrimulanPaladin>();
		num2++;
		span11[num2] = ModContent.NPCType<SlimeGodCore>();
		SlimeGod = list11;
		num2 = 3;
		List<int> list12 = new List<int>(num2);
		CollectionsMarshal.SetCount(list12, num2);
		Span<int> span12 = CollectionsMarshal.AsSpan(list12);
		num = 0;
		span12[num] = ModContent.NPCType<StormWeaverHead>();
		num++;
		span12[num] = ModContent.NPCType<StormWeaverBody>();
		num++;
		span12[num] = ModContent.NPCType<StormWeaverTail>();
		StormWeaver = list12;
		num = 4;
		List<int> list13 = new List<int>(num);
		CollectionsMarshal.SetCount(list13, num);
		Span<int> span13 = CollectionsMarshal.AsSpan(list13);
		num2 = 0;
		span13[num2] = ModContent.NPCType<ThanatosHead>();
		num2++;
		span13[num2] = ModContent.NPCType<ThanatosBody1>();
		num2++;
		span13[num2] = ModContent.NPCType<ThanatosBody2>();
		num2++;
		span13[num2] = ModContent.NPCType<ThanatosTail>();
		Thanatos = list13;
	}
}
