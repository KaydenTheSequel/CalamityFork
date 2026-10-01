using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
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
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Thanatos;
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
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod;

public static class NPCStats
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct EnemyStats
	{
		public static SortedDictionary<int, Tuple<GeneralImmunityStatus, int[]>> DebuffImmunities;
	}

	internal enum GeneralImmunityStatus
	{
		None,
		ImmuneToRegularBuffs,
		ImmuneToAllBuffs
	}

	private static readonly int[] slimeEnemyImmunities = new int[1] { 20 };

	private static readonly int[] iceEnemyImmunities = new int[3]
	{
		44,
		324,
		ModContent.BuffType<GlacialState>()
	};

	private static readonly int[] sulphurEnemyImmunities = new int[4]
	{
		20,
		70,
		ModContent.BuffType<SulphuricPoisoning>(),
		ModContent.BuffType<Irradiated>()
	};

	private static readonly int[] sunkenSeaEnemyImmunities = new int[2]
	{
		ModContent.BuffType<Eutrophication>(),
		ModContent.BuffType<PearlAura>()
	};

	private static readonly int[] abyssEnemyImmunities = new int[2]
	{
		ModContent.BuffType<CrushDepth>(),
		ModContent.BuffType<RiptideDebuff>()
	};

	private static readonly int[] cragEnemyImmunities = new int[3]
	{
		24,
		323,
		ModContent.BuffType<BrimstoneFlames>()
	};

	private static readonly int[] scalImmunities = new int[5]
	{
		24,
		323,
		ModContent.BuffType<BrimstoneFlames>(),
		ModContent.BuffType<VulnerabilityHex>(),
		ModContent.BuffType<TrueVulnerabilityHex>()
	};

	private static readonly int[] astralEnemyImmunities = new int[2]
	{
		20,
		ModContent.BuffType<AstralInfectionDebuff>()
	};

	private static readonly int[] plagueEnemyImmunities = new int[3]
	{
		20,
		70,
		ModContent.BuffType<Plague>()
	};

	private static readonly int[] holyEnemyImmunities;

	public static void SetDebuffImmunities(this NPC npc)
	{
		if (npc == null || EnemyStats.DebuffImmunities == null)
		{
			return;
		}
		if (EnemyStats.DebuffImmunities.TryGetValue(npc.type, out var buffSetTuple))
		{
			GeneralImmunityStatus gis = buffSetTuple.Item1;
			switch (gis)
			{
			case GeneralImmunityStatus.ImmuneToRegularBuffs:
				NPCID.Sets.ImmuneToRegularBuffs[npc.type] = true;
				break;
			case GeneralImmunityStatus.ImmuneToAllBuffs:
				NPCID.Sets.ImmuneToAllBuffs[npc.type] = true;
				break;
			}
			bool providingExceptions = gis != GeneralImmunityStatus.None;
			for (int i = 0; i < buffSetTuple.Item2.Length; i++)
			{
				int buffID = buffSetTuple.Item2[i];
				NPCID.Sets.SpecificDebuffImmunity[npc.type][buffID] = !providingExceptions;
			}
		}
		bool cal = npc.ModNPC != null && npc.ModNPC.Mod.Name.Equals(ModContent.GetInstance<CalamityMod>().Name);
		if (!CalamityNPCSets.CalamityNPCNotImmuneToConfused[npc.type] & cal)
		{
			NPCID.Sets.SpecificDebuffImmunity[npc.type][31] = true;
		}
		bool num = npc.type == 35 || npc.type == 548;
		bool isTownNPC = npc.townNPC || NPCID.Sets.ActsLikeTownNPC[npc.type];
		if (!(num | isTownNPC))
		{
			return;
		}
		NPCID.Sets.ImmuneToRegularBuffs[npc.type] = true;
		if (isTownNPC)
		{
			NPCID.Sets.SpecificDebuffImmunity[npc.type][103] = false;
			NPCID.Sets.SpecificDebuffImmunity[npc.type][137] = false;
			NPCID.Sets.SpecificDebuffImmunity[npc.type][119] = false;
			NPCID.Sets.SpecificDebuffImmunity[npc.type][120] = false;
			NPCID.Sets.SpecificDebuffImmunity[npc.type][320] = false;
			if (npc.townNPC && NPCID.Sets.ShimmerTownTransform[npc.type])
			{
				NPCID.Sets.SpecificDebuffImmunity[npc.type][353] = false;
			}
		}
	}

	internal static void LoadDebuffs()
	{
		Tuple<GeneralImmunityStatus, int[]> immuneToEverything = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.ImmuneToRegularBuffs, Array.Empty<int>());
		new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.ImmuneToAllBuffs, Array.Empty<int>());
		Tuple<GeneralImmunityStatus, int[]> slime = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, slimeEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> ice = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, iceEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> sulphur = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, sulphurEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> sunkenSea = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, sunkenSeaEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> abyss = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, abyssEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> crags = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, cragEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> scal = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, scalImmunities);
		Tuple<GeneralImmunityStatus, int[]> astral = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, astralEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> plague = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, plagueEnemyImmunities);
		Tuple<GeneralImmunityStatus, int[]> holy = new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, holyEnemyImmunities);
		SortedDictionary<int, Tuple<GeneralImmunityStatus, int[]>> sortedDictionary = new SortedDictionary<int, Tuple<GeneralImmunityStatus, int[]>>();
		sortedDictionary.Add(ModContent.NPCType<KingSlimeJewelRuby>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<HiveMind>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BrainRot>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorHive>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorHeadSmall>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorBodySmall>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorTailSmall>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorHeadMedium>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorBodyMedium>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorTailMedium>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorHeadLarge>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorBodyLarge>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(ModContent.NPCType<PerforatorTailLarge>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<BurningBlood>() }));
		sortedDictionary.Add(668, ice);
		sortedDictionary.Add(ModContent.NPCType<SlimeGodCore>(), slime);
		sortedDictionary.Add(ModContent.NPCType<EbonianPaladin>(), slime);
		sortedDictionary.Add(ModContent.NPCType<SplitEbonianPaladin>(), slime);
		sortedDictionary.Add(ModContent.NPCType<CrimulanPaladin>(), slime);
		sortedDictionary.Add(ModContent.NPCType<SplitCrimulanPaladin>(), slime);
		sortedDictionary.Add(ModContent.NPCType<CorruptSlimeSpawn>(), slime);
		sortedDictionary.Add(ModContent.NPCType<CorruptSlimeSpawn2>(), slime);
		sortedDictionary.Add(ModContent.NPCType<CrimsonSlimeSpawn>(), slime);
		sortedDictionary.Add(ModContent.NPCType<CrimsonSlimeSpawn2>(), slime);
		sortedDictionary.Add(ModContent.NPCType<Cryogen>(), ice);
		sortedDictionary.Add(ModContent.NPCType<CryogenShield>(), ice);
		sortedDictionary.Add(ModContent.NPCType<AquaticScourgeHead>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<AquaticScourgeBody>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<AquaticScourgeBodyAlt>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<AquaticScourgeTail>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<BrimstoneElemental>(), crags);
		sortedDictionary.Add(ModContent.NPCType<Brimling>(), crags);
		sortedDictionary.Add(ModContent.NPCType<CalamitasClone>(), crags);
		sortedDictionary.Add(ModContent.NPCType<Cataclysm>(), crags);
		sortedDictionary.Add(ModContent.NPCType<Catastrophe>(), crags);
		sortedDictionary.Add(ModContent.NPCType<SoulSeeker>(), crags);
		sortedDictionary.Add(262, new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { 70 }));
		sortedDictionary.Add(264, new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { 70 }));
		sortedDictionary.Add(ModContent.NPCType<PlanterasFreeTentacle>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { 70 }));
		sortedDictionary.Add(ModContent.NPCType<Anahita>(), ice);
		sortedDictionary.Add(ModContent.NPCType<AnahitasIceShield>(), ice);
		sortedDictionary.Add(ModContent.NPCType<AstrumAureus>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AureusSpawn>(), astral);
		sortedDictionary.Add(ModContent.NPCType<PlaguebringerGoliath>(), plague);
		sortedDictionary.Add(ModContent.NPCType<PlagueMine>(), plague);
		sortedDictionary.Add(ModContent.NPCType<PlagueHomingMissile>(), plague);
		sortedDictionary.Add(636, holy);
		sortedDictionary.Add(ModContent.NPCType<RavagerHead2>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<FlamePillar>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<AstrumDeusHead>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AstrumDeusBody>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AstrumDeusTail>(), astral);
		sortedDictionary.Add(398, new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<Nightwither>() }));
		sortedDictionary.Add(397, new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<Nightwither>() }));
		sortedDictionary.Add(396, new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<Nightwither>() }));
		sortedDictionary.Add(401, new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<Nightwither>() }));
		sortedDictionary.Add(ModContent.NPCType<ProfanedGuardianCommander>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProfanedGuardianDefender>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProfanedGuardianHealer>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProfanedRocks>(), holy);
		sortedDictionary.Add(ModContent.NPCType<Dragonfolly>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<VermillionFlux>() }));
		sortedDictionary.Add(ModContent.NPCType<DraconicSwarmer>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<VermillionFlux>() }));
		sortedDictionary.Add(ModContent.NPCType<WildBumblebirb>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<VermillionFlux>() }));
		sortedDictionary.Add(ModContent.NPCType<Providence>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProvSpawnOffense>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProvSpawnDefense>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProvSpawnHealer>(), holy);
		sortedDictionary.Add(ModContent.NPCType<CeaselessVoid>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<DarkEnergy>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<StormWeaverHead>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[2]
		{
			144,
			ModContent.BuffType<StaticDischarge>()
		}));
		sortedDictionary.Add(ModContent.NPCType<StormWeaverBody>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[2]
		{
			144,
			ModContent.BuffType<StaticDischarge>()
		}));
		sortedDictionary.Add(ModContent.NPCType<StormWeaverTail>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[2]
		{
			144,
			ModContent.BuffType<StaticDischarge>()
		}));
		sortedDictionary.Add(ModContent.NPCType<Signus>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[1] { ModContent.BuffType<WhisperingDeath>() }));
		sortedDictionary.Add(ModContent.NPCType<CosmicLantern>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<CosmicMine>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<Polterghast>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[3]
		{
			ModContent.BuffType<Nightwither>(),
			ModContent.BuffType<Voidfrost>(),
			ModContent.BuffType<WhisperingDeath>()
		}));
		sortedDictionary.Add(ModContent.NPCType<PolterPhantom>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<PhantomFuckYou>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<PolterghastHook>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<OldDuke>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<OldDukeToothBall>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<SulphurousSharkron>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<DevourerofGodsBody>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<DevourerofGodsTail>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<Yharon>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[2]
		{
			24,
			ModContent.BuffType<Dragonfire>()
		}));
		sortedDictionary.Add(ModContent.NPCType<ThanatosHead>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<ThanatosBody1>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<ThanatosBody2>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<ThanatosTail>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<SupremeCalamitas>(), scal);
		sortedDictionary.Add(ModContent.NPCType<SupremeCatastrophe>(), scal);
		sortedDictionary.Add(ModContent.NPCType<SupremeCataclysm>(), scal);
		sortedDictionary.Add(ModContent.NPCType<SoulSeekerSupreme>(), scal);
		sortedDictionary.Add(ModContent.NPCType<BrimstoneHeart>(), scal);
		sortedDictionary.Add(ModContent.NPCType<SepulcherHead>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<SepulcherBody>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<SepulcherBodyEnergyBall>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<SepulcherTail>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<PrimordialWyrmHead>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<PrimordialWyrmBody>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<PrimordialWyrmBodyAlt>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<PrimordialWyrmTail>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<AcidEel>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<BloodwormFleeing>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<BloodwormNormal>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<CragmawMire>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<BabyFlakCrab>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<FlakCrab>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<GammaSlime>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<IrradiatedSlime>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<NuclearTerror>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<NuclearToad>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Orthocera>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Radiator>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Skyfin>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<SulphurousSkater>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Trilobite>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<AquaticUrchin>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<AnthozoanCrab>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<BelchingCoral>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Toxicatfish>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Sulflounder>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Gnasher>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Mauler>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<MicrobialCluster>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<Trasher>(), sulphur);
		sortedDictionary.Add(ModContent.NPCType<BlindedAngler>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<Clam>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<EutrophicRay>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<GhostBell>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<GiantClam>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<PrismBack>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<SeaSerpent1>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<SeaSerpent2>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<SeaSerpent3>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<SeaSerpent4>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<SeaSerpent5>(), sunkenSea);
		sortedDictionary.Add(ModContent.NPCType<BabyCannonballJellyfish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<CannonballJellyfish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<Bloatfish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<BobbitWormHead>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<BoxJellyfish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<ChaoticPuffer>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<Cuttlefish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<DevilFish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<DevilFishAlt>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<GiantSquid>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<GulperEelHead>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<GulperEelBody>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<GulperEelBodyAlt>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<GulperEelTail>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<Laserfish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<LuminousCorvina>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<MirageJelly>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<MorayEel>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<OarfishHead>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<OarfishBody>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<SlabCrab>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<OarfishTail>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<ToxicMinnow>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<Viperfish>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<EidolonWyrmHead>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<EidolonWyrmBody>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<EidolonWyrmBodyAlt>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<EidolonWyrmTail>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<ColossalSquid>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<ReaperShark>(), abyss);
		sortedDictionary.Add(ModContent.NPCType<HeatSpirit>(), crags);
		sortedDictionary.Add(ModContent.NPCType<Scryllar>(), crags);
		sortedDictionary.Add(ModContent.NPCType<ScryllarRage>(), crags);
		sortedDictionary.Add(ModContent.NPCType<DespairStone>(), crags);
		sortedDictionary.Add(ModContent.NPCType<InfernalCongealment>(), crags);
		sortedDictionary.Add(ModContent.NPCType<RenegadeWarlock>(), crags);
		sortedDictionary.Add(ModContent.NPCType<CalamityEye>(), crags);
		sortedDictionary.Add(ModContent.NPCType<SoulSlurper>(), crags);
		sortedDictionary.Add(ModContent.NPCType<Aries>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AstralachneaGround>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AstralachneaWall>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AstralProbe>(), astral);
		sortedDictionary.Add(ModContent.NPCType<AstralSlime>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Atlas>(), astral);
		sortedDictionary.Add(ModContent.NPCType<SightseerSpitter>(), astral);
		sortedDictionary.Add(ModContent.NPCType<FusionFeeder>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Hadarian>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Astraglomerate>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Glomerling>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Mantis>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Nova>(), astral);
		sortedDictionary.Add(ModContent.NPCType<SightseerCollider>(), astral);
		sortedDictionary.Add(ModContent.NPCType<StellarCulex>(), astral);
		sortedDictionary.Add(ModContent.NPCType<Plagueshell>(), plague);
		sortedDictionary.Add(ModContent.NPCType<Viruling>(), plague);
		sortedDictionary.Add(ModContent.NPCType<Melter>(), plague);
		sortedDictionary.Add(ModContent.NPCType<PestilentSlime>(), plague);
		sortedDictionary.Add(ModContent.NPCType<PlagueChargerLarge>(), plague);
		sortedDictionary.Add(ModContent.NPCType<PlagueCharger>(), plague);
		sortedDictionary.Add(ModContent.NPCType<PlaguebringerMiniboss>(), plague);
		sortedDictionary.Add(ModContent.NPCType<ScornEater>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ImpiousImmolator>(), holy);
		sortedDictionary.Add(ModContent.NPCType<ProfanedEnergyBody>(), holy);
		sortedDictionary.Add(ModContent.NPCType<Sunskater>(), holy);
		sortedDictionary.Add(ModContent.NPCType<Eidolist>(), immuneToEverything);
		sortedDictionary.Add(ModContent.NPCType<Frogfish>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[2] { 20, 70 }));
		sortedDictionary.Add(ModContent.NPCType<CloudElemental>(), new Tuple<GeneralImmunityStatus, int[]>(GeneralImmunityStatus.None, new int[2]
		{
			144,
			ModContent.BuffType<StaticDischarge>()
		}));
		sortedDictionary.Add(ModContent.NPCType<CrimulanBlightSlime>(), slime);
		sortedDictionary.Add(ModContent.NPCType<EbonianBlightSlime>(), slime);
		sortedDictionary.Add(ModContent.NPCType<Rimehound>(), ice);
		sortedDictionary.Add(ModContent.NPCType<Cryon>(), ice);
		sortedDictionary.Add(ModContent.NPCType<CryoSlime>(), ice);
		sortedDictionary.Add(ModContent.NPCType<IceClasper>(), ice);
		sortedDictionary.Add(ModContent.NPCType<AuroraSpirit>(), ice);
		EnemyStats.DebuffImmunities = sortedDictionary;
	}

	internal static void UnloadDebuffs()
	{
		EnemyStats.DebuffImmunities = null;
	}

	static NPCStats()
	{
		int[] obj = new int[4] { 24, 323, 0, 189 };
		obj[2] = ModContent.BuffType<HolyFlames>();
		holyEnemyImmunities = obj;
	}
}
