using System.Collections.Generic;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class CalamityNPCSets
{
	public static SetFactory Factory = new SetFactory(NPCLoader.NPCCount, "CalamityMod/NPCID", Search);

	public static IdDictionary Search = IdDictionary.Create<NPCID, int>();

	public static bool[] CalamityNPCNotImmuneToConfused = Factory.CreateBoolSet(ModContent.NPCType<AeroSlime>(), ModContent.NPCType<AstralachneaGround>(), ModContent.NPCType<AstralachneaWall>(), ModContent.NPCType<BloomSlime>(), ModContent.NPCType<Bohldohr>(), ModContent.NPCType<CalamityEye>(), ModContent.NPCType<CrimulanBlightSlime>(), ModContent.NPCType<Cryon>(), ModContent.NPCType<CryoSlime>(), ModContent.NPCType<DespairStone>(), ModContent.NPCType<EbonianBlightSlime>(), ModContent.NPCType<FearlessGoldfishWarrior>(), ModContent.NPCType<HeatSpirit>(), ModContent.NPCType<MantisShrimp>(), ModContent.NPCType<OverloadedSoldier>(), ModContent.NPCType<PerennialSlime>(), ModContent.NPCType<RenegadeWarlock>(), ModContent.NPCType<Rimehound>(), ModContent.NPCType<Rotdog>(), ModContent.NPCType<Scryllar>(), ModContent.NPCType<ScryllarRage>(), ModContent.NPCType<StellarCulex>(), ModContent.NPCType<Stormlion>(), ModContent.NPCType<SuperDummyNPC>(), ModContent.NPCType<WulfrumGyrator>(), ModContent.NPCType<WulfrumRover>());

	public static bool[] ForceDrawDebuffDisplay = Factory.CreateBoolSet(488, 114, ModContent.NPCType<SuperDummyNPC>());

	public static bool[] BossSegmentThatDoesNotGenerateRageFaster = Factory.CreateBoolSet(ModContent.NPCType<DesertScourgeBody>(), ModContent.NPCType<DesertScourgeTail>(), ModContent.NPCType<AquaticScourgeBody>(), ModContent.NPCType<AquaticScourgeBodyAlt>(), ModContent.NPCType<AquaticScourgeTail>(), ModContent.NPCType<AstrumDeusBody>(), ModContent.NPCType<AstrumDeusTail>(), ModContent.NPCType<StormWeaverBody>(), ModContent.NPCType<StormWeaverTail>(), ModContent.NPCType<DevourerofGodsBody>(), ModContent.NPCType<DevourerofGodsTail>(), ModContent.NPCType<ThanatosBody1>(), ModContent.NPCType<ThanatosBody2>(), ModContent.NPCType<ThanatosTail>(), ModContent.NPCType<AresLaserCannon>(), ModContent.NPCType<AresTeslaCannon>(), ModContent.NPCType<AresPlasmaFlamethrower>(), ModContent.NPCType<AresGaussNuke>());

	public static bool[] ScalesHealthLikeBoss = Factory.CreateBoolSet(13, 14, 15, 36, 114, 135, 136, 128, 131, 130, 129, 246, 249, 248, 247, 372, 373, 396, 397, ModContent.NPCType<DarkEnergy>(), ModContent.NPCType<BrimstoneHeart>(), ModContent.NPCType<SoulSeeker>(), ModContent.NPCType<SoulSeekerSupreme>(), ModContent.NPCType<Cataclysm>(), ModContent.NPCType<SupremeCataclysm>(), ModContent.NPCType<Catastrophe>(), ModContent.NPCType<SupremeCatastrophe>(), ModContent.NPCType<SepulcherHead>(), ModContent.NPCType<SepulcherBody>(), ModContent.NPCType<SepulcherTail>(), ModContent.NPCType<SepulcherArm>(), ModContent.NPCType<SepulcherBodyEnergyBall>(), ModContent.NPCType<PrimordialWyrmBody>(), ModContent.NPCType<PrimordialWyrmBodyAlt>(), ModContent.NPCType<PrimordialWyrmHead>(), ModContent.NPCType<PrimordialWyrmTail>(), ModContent.NPCType<AquaticAberration>(), ModContent.NPCType<AnahitasIceShield>(), ModContent.NPCType<CryogenShield>(), ModContent.NPCType<OldDukeToothBall>(), ModContent.NPCType<SulphurousSharkron>(), ModContent.NPCType<DraconicSwarmer>(), ModContent.NPCType<AureusSpawn>(), ModContent.NPCType<Brimling>(), ModContent.NPCType<CrabShroom>(), ModContent.NPCType<DankCreeper>(), ModContent.NPCType<HiveBlob>(), ModContent.NPCType<DarkHeart>(), ModContent.NPCType<DesertNuisanceBody>(), ModContent.NPCType<DesertNuisanceHead>(), ModContent.NPCType<DesertNuisanceTail>(), ModContent.NPCType<DesertNuisanceBodyYoung>(), ModContent.NPCType<DesertNuisanceHeadYoung>(), ModContent.NPCType<DesertNuisanceTailYoung>(), ModContent.NPCType<PolterPhantom>(), ModContent.NPCType<PhantomFuckYou>(), ModContent.NPCType<KingSlimeJewelRuby>(), ModContent.NPCType<PlanterasFreeTentacle>(), ModContent.NPCType<PlagueHomingMissile>(), ModContent.NPCType<PlagueMine>(), ModContent.NPCType<ProfanedRocks>(), ModContent.NPCType<ProvSpawnDefense>(), ModContent.NPCType<ProvSpawnOffense>(), ModContent.NPCType<ProvSpawnHealer>(), ModContent.NPCType<RockPillar>(), ModContent.NPCType<FlamePillar>(), ModContent.NPCType<CosmicMine>(), ModContent.NPCType<CosmicLantern>(), ModContent.NPCType<ProfanedGuardianDefender>(), ModContent.NPCType<ProfanedGuardianHealer>(), ModContent.NPCType<CorruptSlimeSpawn>(), ModContent.NPCType<CorruptSlimeSpawn2>(), ModContent.NPCType<CrimsonSlimeSpawn>(), ModContent.NPCType<CrimsonSlimeSpawn2>(), ModContent.NPCType<PerforatorHeadLarge>(), ModContent.NPCType<PerforatorBodyLarge>(), ModContent.NPCType<PerforatorTailLarge>(), ModContent.NPCType<PerforatorHeadMedium>(), ModContent.NPCType<PerforatorBodyMedium>(), ModContent.NPCType<PerforatorTailMedium>(), ModContent.NPCType<PerforatorHeadSmall>(), ModContent.NPCType<PerforatorBodySmall>(), ModContent.NPCType<PerforatorTailSmall>(), ModContent.NPCType<EbonianPaladin>(), ModContent.NPCType<CrimulanPaladin>(), ModContent.NPCType<SplitEbonianPaladin>(), ModContent.NPCType<SplitCrimulanPaladin>(), ModContent.NPCType<SlimeGodCore>(), ModContent.NPCType<RavagerBody>(), ModContent.NPCType<RavagerClawLeft>(), ModContent.NPCType<RavagerClawRight>(), ModContent.NPCType<RavagerLegLeft>(), ModContent.NPCType<RavagerLegRight>(), ModContent.NPCType<RavagerHead>());

	public static bool[] DealsZeroContactDamage = Factory.CreateBoolSet(48, 498, 499, 500, 501, 502, 503, 504, 505, 506, 289, 259, 260, 206, 250, 541, 32, 24, 45, 379, 533, 285, 286, 122, 169, 268, 283, 284, 281, 282, 172, 110, 293, 291, 292, 109, 111, 29, 471, 215, 214, 216, 143, 145, 468, 251, 463, 381, 389, 382, 390, 520, 387, 347, 350, 420, 424, 406, 407, 411, 409, 426, 425, 429, 492, 392, 394, 395, 393, 139, 439, 246, 249, 400, 619, 263, 628, 564, 565, 576, 577, 555, 556, 557, 561, 562, 563, 572, 573, 570, 571, 574, 575, 568, 569, 578, 325, 327, 344, 345, 346, 523);

	public static bool[] NerfDamageInHardmode = Factory.CreateBoolSet(102, 175, 157, 163, 238, 242, 256, 103, 101, 77, 197, 78, 79, 80, 241, 532, 120, 630, 81, 94, 183, 179, 83, 177, 174, 95, 524, 525, 526, 527, 510, 84, 182, 93, 152, 261, 153, 154, 304, 85, 137, 138, 236, 237, 529, 528, 176, 205, 170, 180, 171, 75, 140, 631, 530, 531, 121, 141, 86, 133, 104, 155, 98, 82, 378, 243, 244, 542, 543, 544, 545, 472, 252, 213, 212, 662, 274, 276, 287, 288, 226, 277, 278, 280, 144, 460, 461, 467, 162, 462, 466, 253, 166, 469, 159, 158, 315, 329, 330, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 326, 352, 342, 351, 348, 349, 341, 343, 338, 339, 340, 621, 620, 587, 586);

	public static bool[] IsBuffedDungeonEnemy = Factory.CreateBoolSet(291, 292, 293, 290, 289, 287, 286, 285, 284, 283, 282, 281, 280, 279, 278, 277, 276, 275, 274, 273, 272, 271, 270, 269);

	public static bool[] IsBuffedPumpkinMoonEnemy = Factory.CreateBoolSet(305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 325, 326, 327, 328, 329, 330);

	public static bool[] IsBuffedFrostMoonEnemy = Factory.CreateBoolSet(338, 339, 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 352);

	public static bool[] IsBuffedSolarEclipseEnemy = Factory.CreateBoolSet(251, 253, 162, 166, 159, 158, 460, 461, 462, 463, 466, 467, 468, 469, 477, 478, 479);

	public static bool[] ResistSlowingDebuffsAndOtherSpecialEffects;

	public static bool[] DoCheckDeadRegardlessRealLife;

	public static bool[] DontCountAsEnemy;

	public static Dictionary<int, int> BossRushHealth;

	public static Dictionary<int, int> BossSpeedrunTimerID;

	static CalamityNPCSets()
	{
		SetFactory factory = Factory;
		int[] obj = new int[135]
		{
			50, 0, 4, 13, 14, 15, 266, 267, 222, 668,
			35, 36, 113, 114, 492, 657, 139, 125, 126, 127,
			128, 129, 131, 130, 262, 264, 0, 344, 346, 345,
			325, 327, 477, 245, 246, 249, 248, 247, 395, 394,
			393, 370, 372, 373, 636, 439, 454, 455, 456, 457,
			458, 459, 521, 396, 397, 398, 400, 558, 559, 560,
			564, 565, 566, 567, 568, 569, 570, 571, 572, 573,
			574, 575, 576, 577, 551, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
		};
		obj[1] = ModContent.NPCType<KingSlimeJewelRuby>();
		obj[26] = ModContent.NPCType<PlanterasFreeTentacle>();
		obj[75] = ModContent.NPCType<DesertNuisanceHead>();
		obj[76] = ModContent.NPCType<DesertNuisanceBody>();
		obj[77] = ModContent.NPCType<DesertNuisanceTail>();
		obj[78] = ModContent.NPCType<DesertNuisanceHeadYoung>();
		obj[79] = ModContent.NPCType<DesertNuisanceBodyYoung>();
		obj[80] = ModContent.NPCType<DesertNuisanceTailYoung>();
		obj[81] = ModContent.NPCType<GiantClam>();
		obj[82] = ModContent.NPCType<PerforatorHeadLarge>();
		obj[83] = ModContent.NPCType<PerforatorHeadMedium>();
		obj[84] = ModContent.NPCType<PerforatorHeadSmall>();
		obj[85] = ModContent.NPCType<PerforatorBodyLarge>();
		obj[86] = ModContent.NPCType<PerforatorBodyMedium>();
		obj[87] = ModContent.NPCType<PerforatorBodySmall>();
		obj[88] = ModContent.NPCType<PerforatorTailLarge>();
		obj[89] = ModContent.NPCType<PerforatorTailMedium>();
		obj[90] = ModContent.NPCType<PerforatorTailSmall>();
		obj[91] = ModContent.NPCType<EbonianPaladin>();
		obj[92] = ModContent.NPCType<CrimulanPaladin>();
		obj[93] = ModContent.NPCType<SplitEbonianPaladin>();
		obj[94] = ModContent.NPCType<SplitCrimulanPaladin>();
		obj[95] = ModContent.NPCType<EarthElemental>();
		obj[96] = ModContent.NPCType<CloudElemental>();
		obj[97] = ModContent.NPCType<CryogenShield>();
		obj[98] = ModContent.NPCType<AquaticScourgeHead>();
		obj[99] = ModContent.NPCType<AquaticScourgeBody>();
		obj[100] = ModContent.NPCType<AquaticScourgeBodyAlt>();
		obj[101] = ModContent.NPCType<AquaticScourgeTail>();
		obj[102] = ModContent.NPCType<CragmawMire>();
		obj[103] = ModContent.NPCType<Cataclysm>();
		obj[104] = ModContent.NPCType<Catastrophe>();
		obj[105] = ModContent.NPCType<SoulSeeker>();
		obj[106] = ModContent.NPCType<GreatSandShark>();
		obj[107] = ModContent.NPCType<AnahitasIceShield>();
		obj[108] = ModContent.NPCType<AureusSpawn>();
		obj[109] = ModContent.NPCType<PlaguebringerMiniboss>();
		obj[110] = ModContent.NPCType<PlagueHomingMissile>();
		obj[111] = ModContent.NPCType<PlagueMine>();
		obj[112] = ModContent.NPCType<RavagerClawLeft>();
		obj[113] = ModContent.NPCType<RavagerClawRight>();
		obj[114] = ModContent.NPCType<RavagerLegLeft>();
		obj[115] = ModContent.NPCType<RavagerLegRight>();
		obj[116] = ModContent.NPCType<RockPillar>();
		obj[117] = ModContent.NPCType<RavagerHead>();
		obj[118] = ModContent.NPCType<ProfanedGuardianDefender>();
		obj[119] = ModContent.NPCType<ProfanedGuardianHealer>();
		obj[120] = ModContent.NPCType<DraconicSwarmer>();
		obj[121] = ModContent.NPCType<ProvSpawnDefense>();
		obj[122] = ModContent.NPCType<ProvSpawnHealer>();
		obj[123] = ModContent.NPCType<ProvSpawnOffense>();
		obj[124] = ModContent.NPCType<BobbitWormHead>();
		obj[125] = ModContent.NPCType<Mauler>();
		obj[126] = ModContent.NPCType<ColossalSquid>();
		obj[127] = ModContent.NPCType<ReaperShark>();
		obj[128] = ModContent.NPCType<EidolonWyrmHead>();
		obj[129] = ModContent.NPCType<NuclearTerror>();
		obj[130] = ModContent.NPCType<OldDukeToothBall>();
		obj[131] = ModContent.NPCType<SulphurousSharkron>();
		obj[132] = ModContent.NPCType<SupremeCataclysm>();
		obj[133] = ModContent.NPCType<SupremeCatastrophe>();
		obj[134] = ModContent.NPCType<SoulSeekerSupreme>();
		ResistSlowingDebuffsAndOtherSpecialEffects = factory.CreateBoolSet(obj);
		DoCheckDeadRegardlessRealLife = Factory.CreateBoolSet(ModContent.NPCType<DevourerofGodsBody>(), ModContent.NPCType<DevourerofGodsTail>());
		DontCountAsEnemy = Factory.CreateBoolSet(488, ModContent.NPCType<SuperDummyNPC>());
		BossRushHealth = new Dictionary<int, int>
		{
			{ 50, 300000 },
			{ 1, 3600 },
			{ 535, 7200 },
			{ -3, 2700 },
			{ -8, 5400 },
			{ -7, 7200 },
			{ -9, 6300 },
			{ 147, 4500 },
			{ 225, 5400 },
			{ 244, 30000 },
			{ -4, 15000 },
			{
				ModContent.NPCType<KingSlimeJewelRuby>(),
				21000
			},
			{ 4, 450000 },
			{ 5, 6000 },
			{ 13, 15000 },
			{ 14, 15000 },
			{ 15, 15000 },
			{ 266, 100000 },
			{ 267, 10000 },
			{ 222, 315000 },
			{ 210, 3000 },
			{ 211, 2000 },
			{ -59, 10000 },
			{ 232, 7500 },
			{ -58, 5000 },
			{ 668, 315000 },
			{ 35, 150000 },
			{ 36, 60000 },
			{ 113, 450000 },
			{ 114, 450000 },
			{ 115, 10000 },
			{ 116, 5000 },
			{ 117, 5000 },
			{ 118, 5000 },
			{ 119, 5000 },
			{ 657, 200000 },
			{ 658, 6000 },
			{ 659, 6000 },
			{ 660, 5000 },
			{ 126, 150000 },
			{ 125, 125000 },
			{ 134, 600000 },
			{ 135, 600000 },
			{ 136, 600000 },
			{ 139, 10000 },
			{ 127, 160000 },
			{ 130, 54000 },
			{ 128, 45000 },
			{ 129, 45000 },
			{ 131, 38000 },
			{ 262, 160000 },
			{ 264, 5000 },
			{
				ModContent.NPCType<PlanterasFreeTentacle>(),
				5000
			},
			{ 245, 100000 },
			{ 246, 70000 },
			{ 247, 30000 },
			{ 248, 30000 },
			{ 636, 200000 },
			{ 370, 290000 },
			{ 439, 220000 },
			{ 454, 60000 },
			{ 455, 60000 },
			{ 456, 60000 },
			{ 457, 60000 },
			{ 458, 60000 },
			{ 459, 60000 },
			{ 521, 50000 },
			{ 398, 160000 },
			{ 397, 45000 },
			{ 396, 60000 },
			{ 401, 800 }
		};
		BossSpeedrunTimerID = new Dictionary<int, int>
		{
			{ 50, 1 },
			{
				ModContent.NPCType<DesertScourgeHead>(),
				2
			},
			{ 4, 3 },
			{
				ModContent.NPCType<Crabulon>(),
				4
			},
			{ 13, 5 },
			{ 14, 5 },
			{ 15, 5 },
			{ 266, 6 },
			{
				ModContent.NPCType<HiveMind>(),
				7
			},
			{
				ModContent.NPCType<PerforatorHive>(),
				8
			},
			{ 222, 9 },
			{ 35, 10 },
			{
				ModContent.NPCType<SlimeGodCore>(),
				11
			},
			{
				ModContent.NPCType<SplitEbonianPaladin>(),
				11
			},
			{
				ModContent.NPCType<SplitCrimulanPaladin>(),
				11
			},
			{ 113, 12 },
			{
				ModContent.NPCType<Cryogen>(),
				13
			},
			{ 125, 14 },
			{ 126, 14 },
			{
				ModContent.NPCType<AquaticScourgeHead>(),
				15
			},
			{ 134, 16 },
			{
				ModContent.NPCType<BrimstoneElemental>(),
				17
			},
			{ 127, 18 },
			{
				ModContent.NPCType<CalamitasClone>(),
				19
			},
			{ 262, 20 },
			{
				ModContent.NPCType<Leviathan>(),
				21
			},
			{
				ModContent.NPCType<Anahita>(),
				21
			},
			{
				ModContent.NPCType<AstrumAureus>(),
				22
			},
			{ 245, 23 },
			{
				ModContent.NPCType<PlaguebringerGoliath>(),
				24
			},
			{ 370, 25 },
			{
				ModContent.NPCType<RavagerBody>(),
				26
			},
			{ 439, 27 },
			{
				ModContent.NPCType<AstrumDeusHead>(),
				28
			},
			{ 398, 29 },
			{
				ModContent.NPCType<ProfanedGuardianCommander>(),
				30
			},
			{
				ModContent.NPCType<Dragonfolly>(),
				31
			},
			{
				ModContent.NPCType<Providence>(),
				32
			},
			{
				ModContent.NPCType<CeaselessVoid>(),
				33
			},
			{
				ModContent.NPCType<StormWeaverHead>(),
				34
			},
			{
				ModContent.NPCType<Signus>(),
				35
			},
			{
				ModContent.NPCType<Polterghast>(),
				36
			},
			{
				ModContent.NPCType<OldDuke>(),
				37
			},
			{
				ModContent.NPCType<DevourerofGodsHead>(),
				38
			},
			{
				ModContent.NPCType<Yharon>(),
				39
			},
			{
				ModContent.NPCType<SupremeCalamitas>(),
				40
			},
			{
				ModContent.NPCType<AresBody>(),
				41
			},
			{
				ModContent.NPCType<ThanatosHead>(),
				41
			},
			{
				ModContent.NPCType<Artemis>(),
				41
			},
			{
				ModContent.NPCType<Apollo>(),
				41
			},
			{ 657, 42 },
			{ 636, 43 },
			{ 668, 44 }
		};
	}
}
