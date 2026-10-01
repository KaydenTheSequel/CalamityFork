using System.Collections.Generic;
using CalamityMod.Buffs;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Buffs.Cooldowns;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Potions;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.DataStructures;
using CalamityMod.Items.Accessories;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class CalamityBuffSets
{
	public static SetFactory Factory = new SetFactory(BuffLoader.BuffCount, "CalamityMod/BuffID", Search);

	public static IdDictionary Search = IdDictionary.Create<BuffID, int>();

	public static bool[] BuffedByAmalgam;

	public static bool[] IsPersistentBuff;

	public static bool[] IsDebuff;

	public static bool[] IsSummonTagBuff;

	public static Dictionary<int, SummonTag> SummonTagDebuff;

	public static Dictionary<int, int> AlcoholStrength;

	static CalamityBuffSets()
	{
		SetFactory factory = Factory;
		int[] obj = new int[92]
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12, 13, 14, 15, 16, 17, 18, 25, 26,
			206, 207, 48, 71, 73, 74, 75, 76, 77, 78,
			79, 257, 104, 105, 106, 107, 108, 109, 110, 111,
			112, 113, 114, 115, 116, 117, 119, 120, 121, 122,
			123, 124, 192, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0
		};
		obj[53] = ModContent.BuffType<AnechoicCoatingBuff>();
		obj[54] = ModContent.BuffType<AstralInjectionBuff>();
		obj[55] = ModContent.BuffType<BaguetteBuff>();
		obj[56] = ModContent.BuffType<BloodfinBoost>();
		obj[57] = ModContent.BuffType<BoundingBuff>();
		obj[58] = ModContent.BuffType<CalciumBuff>();
		obj[59] = ModContent.BuffType<CeaselessHunger>();
		obj[60] = ModContent.BuffType<GravityNormalizerBuff>();
		obj[61] = ModContent.BuffType<Omniscience>();
		obj[62] = ModContent.BuffType<PhotosynthesisBuff>();
		obj[63] = ModContent.BuffType<ShadowBuff>();
		obj[64] = ModContent.BuffType<Soaring>();
		obj[65] = ModContent.BuffType<SulphurskinBuff>();
		obj[66] = ModContent.BuffType<WeaponImbueBrimstone>();
		obj[67] = ModContent.BuffType<WeaponImbueCrumbling>();
		obj[68] = ModContent.BuffType<WeaponImbueHolyFlames>();
		obj[69] = ModContent.BuffType<Zen>();
		obj[70] = ModContent.BuffType<Zerg>();
		obj[71] = ModContent.BuffType<BloodyMaryBuff>();
		obj[72] = ModContent.BuffType<CaribbeanRumBuff>();
		obj[73] = ModContent.BuffType<CinnamonRollBuff>();
		obj[74] = ModContent.BuffType<EverclearBuff>();
		obj[75] = ModContent.BuffType<EvergreenGinBuff>();
		obj[76] = ModContent.BuffType<PurpleHazeBuff>();
		obj[77] = ModContent.BuffType<FireballBuff>();
		obj[78] = ModContent.BuffType<GrapeBeerBuff>();
		obj[79] = ModContent.BuffType<MargaritaBuff>();
		obj[80] = ModContent.BuffType<MoonshineBuff>();
		obj[81] = ModContent.BuffType<MoscowMuleBuff>();
		obj[82] = ModContent.BuffType<RedWineBuff>();
		obj[83] = ModContent.BuffType<RumBuff>();
		obj[84] = ModContent.BuffType<ScrewdriverBuff>();
		obj[85] = ModContent.BuffType<StarBeamRyeBuff>();
		obj[86] = ModContent.BuffType<TequilaBuff>();
		obj[87] = ModContent.BuffType<TequilaSunriseBuff>();
		obj[88] = ModContent.BuffType<Trippy>();
		obj[89] = ModContent.BuffType<VodkaBuff>();
		obj[90] = ModContent.BuffType<WhiskeyBuff>();
		obj[91] = ModContent.BuffType<WhiteWineBuff>();
		BuffedByAmalgam = factory.CreateBoolSet(obj);
		IsPersistentBuff = Factory.CreateBoolSet(71, 73, 74, 75, 76, 77, 78, 79, ModContent.BuffType<WeaponImbueBrimstone>(), ModContent.BuffType<WeaponImbueCrumbling>(), ModContent.BuffType<WeaponImbueHolyFlames>(), ModContent.BuffType<BloodyMaryBuff>(), ModContent.BuffType<CaribbeanRumBuff>(), ModContent.BuffType<CinnamonRollBuff>(), ModContent.BuffType<EverclearBuff>(), ModContent.BuffType<EvergreenGinBuff>(), ModContent.BuffType<FireballBuff>(), ModContent.BuffType<GrapeBeerBuff>(), ModContent.BuffType<ManhattanBuff>(), ModContent.BuffType<MargaritaBuff>(), ModContent.BuffType<MoonshineBuff>(), ModContent.BuffType<MoscowMuleBuff>(), ModContent.BuffType<OldFashionedBuff>(), ModContent.BuffType<PurpleHazeBuff>(), ModContent.BuffType<RedWineBuff>(), ModContent.BuffType<RumBuff>(), ModContent.BuffType<ScrewdriverBuff>(), ModContent.BuffType<StarBeamRyeBuff>(), ModContent.BuffType<TequilaBuff>(), ModContent.BuffType<TequilaSunriseBuff>(), ModContent.BuffType<VodkaBuff>(), ModContent.BuffType<WhiskeyBuff>(), ModContent.BuffType<WhiteWineBuff>());
		SetFactory factory2 = Factory;
		int[] obj2 = new int[72]
		{
			20, 22, 23, 24, 30, 31, 32, 33, 35, 36,
			39, 44, 46, 47, 67, 68, 69, 70, 80, 144,
			148, 149, 156, 160, 164, 195, 196, 197, 203, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0
		};
		obj2[29] = ModContent.BuffType<SulphuricPoisoning>();
		obj2[30] = ModContent.BuffType<Shadowflame>();
		obj2[31] = ModContent.BuffType<Daybroken>();
		obj2[32] = ModContent.BuffType<BrimstoneFlames>();
		obj2[33] = ModContent.BuffType<BurningBlood>();
		obj2[34] = ModContent.BuffType<BrainRot>();
		obj2[35] = ModContent.BuffType<ElementalMix>();
		obj2[36] = ModContent.BuffType<GlacialState>();
		obj2[37] = ModContent.BuffType<GodSlayerInferno>();
		obj2[38] = ModContent.BuffType<AstralInfectionDebuff>();
		obj2[39] = ModContent.BuffType<HolyFlames>();
		obj2[40] = ModContent.BuffType<Irradiated>();
		obj2[41] = ModContent.BuffType<Plague>();
		obj2[42] = ModContent.BuffType<CrushDepth>();
		obj2[43] = ModContent.BuffType<HadopelagicPressure>();
		obj2[44] = ModContent.BuffType<RiptideDebuff>();
		obj2[45] = ModContent.BuffType<MarkedforDeath>();
		obj2[46] = ModContent.BuffType<HeavyBleeding>();
		obj2[47] = ModContent.BuffType<Laceration>();
		obj2[48] = ModContent.BuffType<AbsorberAffliction>();
		obj2[49] = ModContent.BuffType<ArmorCrunch>();
		obj2[50] = ModContent.BuffType<Crumbling>();
		obj2[51] = ModContent.BuffType<Vaporfied>();
		obj2[52] = ModContent.BuffType<Eutrophication>();
		obj2[53] = ModContent.BuffType<Dragonfire>();
		obj2[54] = ModContent.BuffType<VermillionFlux>();
		obj2[55] = ModContent.BuffType<AuricRebuke>();
		obj2[56] = ModContent.BuffType<StaticDischarge>();
		obj2[57] = ModContent.BuffType<Nightwither>();
		obj2[58] = ModContent.BuffType<Voidfrost>();
		obj2[59] = ModContent.BuffType<VulnerabilityHex>();
		obj2[60] = ModContent.BuffType<MiracleBlight>();
		obj2[61] = ModContent.BuffType<WhisperingDeath>();
		obj2[62] = ModContent.BuffType<FrozenLungs>();
		obj2[63] = ModContent.BuffType<FishAlert>();
		obj2[64] = ModContent.BuffType<HolyInferno>();
		obj2[65] = ModContent.BuffType<IcarusFolly>();
		obj2[66] = ModContent.BuffType<DoGExtremeGravity>();
		obj2[67] = ModContent.BuffType<PopoNoselessBuff>();
		obj2[68] = ModContent.BuffType<SearingLava>();
		obj2[69] = ModContent.BuffType<WeakBrimstoneFlames>();
		obj2[70] = ModContent.BuffType<Withered>();
		obj2[71] = ModContent.BuffType<ManaBurn>();
		IsDebuff = factory2.CreateBoolSet(obj2);
		SetFactory factory3 = Factory;
		int[] obj3 = new int[5] { 312, 311, 308, 314, 0 };
		obj3[4] = ModContent.BuffType<ProfanedCrystalWhipBuff>();
		IsSummonTagBuff = factory3.CreateBoolSet(obj3);
		SummonTagDebuff = new Dictionary<int, SummonTag>
		{
			{
				307,
				SummonTag.LeatherWhip
			},
			{
				326,
				SummonTag.SpinalTap
			},
			{
				340,
				SummonTag.CoolWhip
			},
			{
				313,
				SummonTag.Firecracker
			},
			{
				319,
				SummonTag.MorningStar
			},
			{
				316,
				SummonTag.Kaleidoscope
			},
			{
				310,
				SummonTag.DarkHarvest
			},
			{
				309,
				SummonTag.Durendal
			},
			{
				315,
				SummonTag.Snapthorn
			},
			{
				ModContent.BuffType<ProfanedCrystalWhipDebuff>(),
				ProfanedSoulCrystal.SummonTag
			}
		};
		AlcoholStrength = new Dictionary<int, int>
		{
			{ 25, 1 },
			{
				ModContent.BuffType<BloodyMaryBuff>(),
				1
			},
			{
				ModContent.BuffType<CaribbeanRumBuff>(),
				1
			},
			{
				ModContent.BuffType<CinnamonRollBuff>(),
				1
			},
			{
				ModContent.BuffType<EverclearBuff>(),
				2
			},
			{
				ModContent.BuffType<EvergreenGinBuff>(),
				1
			},
			{
				ModContent.BuffType<FireballBuff>(),
				1
			},
			{
				ModContent.BuffType<GrapeBeerBuff>(),
				1
			},
			{
				ModContent.BuffType<MargaritaBuff>(),
				1
			},
			{
				ModContent.BuffType<MoonshineBuff>(),
				1
			},
			{
				ModContent.BuffType<MoscowMuleBuff>(),
				1
			},
			{
				ModContent.BuffType<OldFashionedBuff>(),
				1
			},
			{
				ModContent.BuffType<PurpleHazeBuff>(),
				1
			},
			{
				ModContent.BuffType<RedWineBuff>(),
				1
			},
			{
				ModContent.BuffType<RumBuff>(),
				1
			},
			{
				ModContent.BuffType<ScrewdriverBuff>(),
				1
			},
			{
				ModContent.BuffType<StarBeamRyeBuff>(),
				1
			},
			{
				ModContent.BuffType<TequilaBuff>(),
				1
			},
			{
				ModContent.BuffType<TequilaSunriseBuff>(),
				1
			},
			{
				ModContent.BuffType<VodkaBuff>(),
				1
			},
			{
				ModContent.BuffType<WhiskeyBuff>(),
				1
			},
			{
				ModContent.BuffType<WhiteWineBuff>(),
				1
			}
		};
	}
}
