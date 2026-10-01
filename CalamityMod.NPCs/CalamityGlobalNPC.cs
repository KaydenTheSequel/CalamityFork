using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using CalamityMod.Balancing;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Events;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Armor.PlagueReaper;
using CalamityMod.Items.Fishing;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Typeless;
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
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.Deconstructors;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.DraedonLabThings;
using CalamityMod.NPCs.ExoMechs;
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
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Packets;
using CalamityMod.Particles;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using CalamityMod.Systems.Collections;
using CalamityMod.Tiles.FurnitureAuric;
using CalamityMod.Tiles.Ores;
using CalamityMod.UI;
using CalamityMod.UI.DebuffSystem;
using CalamityMod.UI.VanillaBossBars;
using CalamityMod.Walls.DraedonStructures;
using CalamityMod.World;
using CalamityMod.World.Planets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Utils;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.UI.Chat;
using Terraria.Utilities;

namespace CalamityMod.NPCs;

public class CalamityGlobalNPC : GlobalNPC
{
	public static SortedDictionary<int, int> BossKillTimes;

	public bool unbreakableDR;

	public bool? VulnerableToHeat;

	public bool? VulnerableToCold;

	public bool? VulnerableToSickness;

	public bool? VulnerableToElectricity;

	public bool? VulnerableToWater;

	public const float BaseDoTDamageMult = 1f;

	public const float VulnerableToDoTDamageMult = 2f;

	public const float VulnerableToDoTDamageMult_Worms_SlimeGod = 1.5f;

	public const float ResistantToDoTDamageMult = 0.5f;

	public StatModifier TypelessDebuffMultiplier = new StatModifier();

	public StatModifier HeatDebuffMultiplier = new StatModifier();

	public StatModifier ColdDebuffMultiplier = new StatModifier();

	public StatModifier SicknessDebuffMultiplier = new StatModifier();

	public StatModifier WaterDebuffMultiplier = new StatModifier();

	public StatModifier ElectricDebuffMultiplier = new StatModifier();

	public StatModifier ActiveTypelessDebuffMultiplier = new StatModifier();

	public StatModifier ActiveHeatDebuffMultiplier = new StatModifier();

	public StatModifier ActiveColdDebuffMultiplier = new StatModifier();

	public StatModifier ActiveSicknessDebuffMultiplier = new StatModifier();

	public StatModifier ActiveWaterDebuffMultiplier = new StatModifier();

	public StatModifier ActiveElectricDebuffMultiplier = new StatModifier();

	public bool IncreasedColdEffects_EskimoSet;

	public bool IncreasedColdEffects_CryoStone;

	public bool IncreasedElectricityEffects_Unused;

	public bool IncreasedHeatEffects_Fireball;

	public bool IncreasedHeatEffects_CinnamonRoll;

	public int IncreasedHeatEffects_FireBoots;

	public bool IncreasedSicknessEffects_ToxicHeart;

	public bool IncreasedWaterEffects_Amulet1;

	public bool IncreasedWaterEffects_Amulet2;

	public bool IncreasedSicknessAndWaterEffects_EvergreenGin;

	public bool IncreasedSicknessAndWaterEffects_CorrosiveSpine;

	public bool IncreasedDebuffEffects_Amalgam;

	public const int biomeEnrageTimerMax = 300;

	public float velocityPriorToPhaseSwap;

	public const float velocityPriorToPhaseSwapIncrement = 0.1f;

	public bool canBreakPlayerDefense;

	public int miscDefenseLoss;

	public const float CatchUpDistance200Tiles = 3200f;

	public const float CatchUpDistance350Tiles = 5600f;

	private const float BossZenDistance = 6400f;

	private const double DesertEnemyStatMultiplier = 0.75;

	public const double EarlyHardmodeProgressionReworkFirstMechStatMultiplier_Classic = 0.8;

	public const double EarlyHardmodeProgressionReworkSecondMechStatMultiplier_Classic = 0.9;

	public const double EarlyHardmodeProgressionReworkFirstMechStatMultiplier_Expert = 0.9;

	public const double EarlyHardmodeProgressionReworkSecondMechStatMultiplier_Expert = 0.95;

	private const double NPCValueMultiplier_ClassicCalamity = 1.5;

	private const double NPCValueMultiplier_ExpertVanilla = 2.5;

	private const double NPCValueMultiplier_ExpertCalamity = 1.5;

	public const int maxPlayerImmunities = 256;

	public int[] dashImmunityTime = new int[256];

	public bool ProvidesProximityRage = true;

	internal const int maxAIMod = 4;

	public float[] newAI = new float[4];

	public int killTimeTimer;

	public bool SplittingWorm;

	public bool CanHaveBossHealthBar;

	public bool ShouldCloseHPBar;

	public const int slowingDebuffResistanceMin = 1800;

	public int debuffResistanceTimer;

	public bool vaporfied;

	public bool timeDistortion;

	public bool glacialState;

	public bool galvanicCorrosion;

	public bool temporalSadness;

	public bool eutrophication;

	public bool webbed;

	public bool electrified;

	public bool pearlAura;

	public float manaBurn;

	public float manaBurnPeak;

	public float playerManaBurnIntensity;

	public bool burningBlood;

	public bool brainRot;

	public bool heavyBleeding;

	public bool laceration;

	public bool elementalMix;

	public bool markedForDeath;

	public bool absorberAffliction;

	public bool irradiated;

	public double irradiatedContactBoost = 1.5;

	public bool brimstoneFlames;

	public bool demonicFlames;

	public int demonicFlamesBonusDamage;

	public bool holyFlames;

	public bool plague;

	public bool armorCrunch;

	public bool crumble;

	public int antlionCloudDebuffTimer;

	public bool scionsCurioEffected;

	public int warbannerBurnTime;

	public int warbannerBurnTimer;

	public int warbannerBurnStacks;

	public int warbannerBurnDamage;

	public Vector2 warbannerBurnDirection;

	public float warbannerBurnIntensity;

	public bool warbannerBurnMarked;

	public bool warbannerBurnHideEffects;

	public const int veriumDoomTime = 90;

	public int veriumDoomTimer;

	public int veriumDoomStacks;

	public bool veriumDoomMarked;

	public bool laserBurnMarked;

	public int laserBurnType;

	public int laserBurnDamage;

	public const int laserBurnTime = 300;

	public int laserBurnTimer;

	public int laserBurnStacks;

	public bool hyperiusMarked;

	public int hyperiusDamage;

	public static int hyperiusOverflowTime = 100;

	public int hyperiusOverflowTimer = hyperiusOverflowTime;

	public const float HyperiusLifePercentThreshold = 0.07f;

	public int hyperiusFxTimer;

	public int glaiveShredTimer;

	public int blazingStarShredTimer;

	public int cursorFocus;

	public const int cursorFocusMax = 300;

	public int demonSwordImpales;

	public int impalePacketTimer;

	public bool pacified;

	public int somaShredStacks;

	public int somaShredApplicator = -1;

	public int somaShredFalloff = 320;

	public bool crushDepth;

	public bool riptide;

	public bool hadopelagicPressure;

	public bool godSlayerInferno;

	public bool dragonFire;

	public bool vermillionFlux;

	public bool auricRebuke;

	public bool staticDischarge;

	public bool miracleBlight;

	public bool astralInfection;

	public bool whisperingDeath;

	public bool nightwither;

	public int shocked;

	public bool voidfrost;

	public bool shellfishStaffDebuff;

	public bool snapClamDebuff;

	public bool sulphurPoison;

	public int ladHearts;

	public bool relicOfResilienceWeakness;

	public bool sagePoison;

	public int sagePoisonDamage;

	public bool vulnerabilityHex;

	public bool trueVulnerabilityHex;

	public bool banishingFire;

	public bool wither;

	public int ashesOnDeath;

	public static int[] bobbitWormBottom = new int[5];

	public static int hiveMind = -1;

	public static int perfHive = -1;

	public static int slimeGodPurple = -1;

	public static int slimeGodRed = -1;

	public static int slimeGod = -1;

	public static int laserEye = -1;

	public static int fireEye = -1;

	public static int primeLaser = -1;

	public static int primeCannon = -1;

	public static int primeVice = -1;

	public static int primeSaw = -1;

	public static int aquaticScourge = -1;

	public static int brimstoneElemental = -1;

	public static int cataclysm = -1;

	public static int catastrophe = -1;

	public static int calamitas = -1;

	public static int LeviAndAna = -1;

	public static int leviathan = -1;

	public static int siren = -1;

	public static int astrumAureus = -1;

	public static int scavenger = -1;

	public static int energyFlame = -1;

	public static int doughnutBoss = -1;

	public static int doughnutBossDefender = -1;

	public static int doughnutBossHealer = -1;

	public static int holyBossAttacker = -1;

	public static int holyBossDefender = -1;

	public static int holyBossHealer = -1;

	public static int holyBoss = -1;

	public static int voidBoss = -1;

	public static int signus = -1;

	public static int ghostBossClone = -1;

	public static int ghostBoss = -1;

	public static int DoGHead = -1;

	public static int DoGP2 = -1;

	public static int yharon = -1;

	public static int yharonP2 = -1;

	public static int SCalCataclysm = -1;

	public static int SCalCatastrophe = -1;

	public static int SCal = -1;

	public static int SCalWorm = -1;

	public static int SCalGrief = -1;

	public static int SCalLament = -1;

	public static int SCalEpiphany = -1;

	public static int SCalAcceptance = -1;

	public static int draedon = -1;

	public static int draedonAmbience = -1;

	public static int draedonExoMechWorm = -1;

	public static int draedonExoMechTwinRed = -1;

	public static int draedonExoMechTwinGreen = -1;

	public static int draedonExoMechPrime = -1;

	public static int draedonExoMechPrimePlasmaCannon = -1;

	public static int adultEidolonWyrmHead = -1;

	public FireParticleSet VulnerabilityHexFireDrawer;

	public FireParticleSet ManaBurnFireDrawer;

	public bool CurrentlyEnraged;

	public bool CurrentlyIncreasingDefenseOrDR;

	public bool DoesNotDisappearInBossRush;

	public bool gladiatorOnKill = true;

	public int arcZapCooldown;

	public float bestiaryWormTimer;

	private const float VanillaScalingFactor_2Players = 1.35f;

	private const float VanillaScalingFactor_3Players = 1.9166666f;

	public static bool DisableMultWhipTag = false;

	public static List<(string, Predicate<NPC>)> moddedDebuffTextureList = new List<(string, Predicate<NPC>)>
	{
		("CalamityMod/Buffs/DamageOverTime/AstralInfectionDebuff", (NPC NPC) => NPC.Calamity().astralInfection),
		("CalamityMod/Buffs/DamageOverTime/AuricRebuke", (NPC NPC) => NPC.Calamity().auricRebuke),
		("CalamityMod/Buffs/DamageOverTime/BanishingFire", (NPC NPC) => NPC.Calamity().banishingFire),
		("CalamityMod/Buffs/DamageOverTime/BrainRot", (NPC NPC) => NPC.Calamity().brainRot),
		("CalamityMod/Buffs/DamageOverTime/BrimstoneFlames", (NPC NPC) => NPC.Calamity().brimstoneFlames),
		("CalamityMod/Buffs/DamageOverTime/DemonicFlames", (NPC NPC) => NPC.Calamity().demonicFlames),
		("CalamityMod/Buffs/DamageOverTime/BurningBlood", (NPC NPC) => NPC.Calamity().burningBlood),
		("CalamityMod/Buffs/DamageOverTime/CrushDepth", (NPC NPC) => NPC.Calamity().crushDepth),
		("CalamityMod/Buffs/DamageOverTime/Dragonfire", (NPC NPC) => NPC.Calamity().dragonFire),
		("CalamityMod/Buffs/DamageOverTime/ElementalMix", (NPC NPC) => NPC.Calamity().elementalMix),
		("CalamityMod/Buffs/DamageOverTime/GodSlayerInferno", (NPC NPC) => NPC.Calamity().godSlayerInferno),
		("CalamityMod/Buffs/DamageOverTime/HadopelagicPressure", (NPC NPC) => NPC.Calamity().hadopelagicPressure),
		("CalamityMod/Buffs/DamageOverTime/HolyFlames", (NPC NPC) => NPC.Calamity().holyFlames),
		("CalamityMod/Buffs/DamageOverTime/Laceration", (NPC NPC) => NPC.Calamity().laceration),
		("CalamityMod/Buffs/DamageOverTime/HeavyBleeding", (NPC NPC) => NPC.Calamity().heavyBleeding),
		("CalamityMod/Buffs/DamageOverTime/ManaBurn", (NPC NPC) => NPC.Calamity().manaBurn > 0f),
		("CalamityMod/Buffs/DamageOverTime/MiracleBlight", (NPC NPC) => NPC.Calamity().miracleBlight),
		("CalamityMod/Buffs/DamageOverTime/Nightwither", (NPC NPC) => NPC.Calamity().nightwither),
		("CalamityMod/Buffs/DamageOverTime/Plague", (NPC NPC) => NPC.Calamity().plague),
		("CalamityMod/Buffs/DamageOverTime/RiptideDebuff", (NPC NPC) => NPC.Calamity().riptide),
		("CalamityMod/Buffs/DamageOverTime/SagePoison", (NPC NPC) => NPC.Calamity().sagePoison),
		("CalamityMod/Buffs/DamageOverTime/SearingLava", (NPC NPC) => NPC.HasBuff<SearingLava>()),
		("CalamityMod/Buffs/DamageOverTime/ShellfishClaps", (NPC NPC) => NPC.Calamity().shellfishStaffDebuff),
		("CalamityMod/Buffs/DamageOverTime/Shred", (NPC NPC) => NPC.Calamity().somaShredStacks > 0),
		("CalamityMod/Buffs/DamageOverTime/SnapClamDebuff", (NPC NPC) => NPC.Calamity().snapClamDebuff),
		("CalamityMod/Buffs/DamageOverTime/StaticDischarge", (NPC NPC) => NPC.Calamity().staticDischarge),
		("CalamityMod/Buffs/DamageOverTime/SulphuricPoisoning", (NPC NPC) => NPC.Calamity().sulphurPoison),
		("CalamityMod/Buffs/DamageOverTime/TrueVulnerabilityHex", (NPC NPC) => NPC.Calamity().trueVulnerabilityHex),
		("CalamityMod/Buffs/DamageOverTime/Vaporfied", (NPC NPC) => NPC.Calamity().vaporfied),
		("CalamityMod/Buffs/DamageOverTime/VermillionFlux", (NPC NPC) => NPC.Calamity().vermillionFlux),
		("CalamityMod/Buffs/DamageOverTime/Voidfrost", (NPC NPC) => NPC.Calamity().voidfrost),
		("CalamityMod/Buffs/DamageOverTime/VulnerabilityHex", (NPC NPC) => NPC.Calamity().vulnerabilityHex),
		("CalamityMod/Buffs/StatDebuffs/AbsorberAffliction", (NPC NPC) => NPC.Calamity().absorberAffliction),
		("CalamityMod/Buffs/StatDebuffs/ArmorCrunch", (NPC NPC) => NPC.Calamity().armorCrunch),
		("CalamityMod/Buffs/StatDebuffs/Crumbling", (NPC NPC) => NPC.Calamity().crumble),
		("CalamityMod/Buffs/StatDebuffs/Eutrophication", (NPC NPC) => NPC.Calamity().eutrophication),
		("CalamityMod/Buffs/StatDebuffs/GalvanicCorrosion", (NPC NPC) => NPC.Calamity().galvanicCorrosion),
		("CalamityMod/Buffs/StatDebuffs/GlacialState", (NPC NPC) => NPC.Calamity().glacialState),
		("CalamityMod/Buffs/StatDebuffs/Irradiated", (NPC NPC) => NPC.Calamity().irradiated),
		("CalamityMod/Buffs/StatDebuffs/MarkedforDeath", (NPC NPC) => NPC.Calamity().markedForDeath),
		("CalamityMod/Buffs/StatDebuffs/PearlAura", (NPC NPC) => NPC.Calamity().pearlAura),
		("CalamityMod/Buffs/StatDebuffs/ProfanedWeakness", (NPC NPC) => NPC.Calamity().relicOfResilienceWeakness),
		("CalamityMod/Buffs/StatDebuffs/TemporalSadness", (NPC NPC) => NPC.Calamity().temporalSadness),
		("CalamityMod/Buffs/StatDebuffs/TimeDistortion", (NPC NPC) => NPC.Calamity().timeDistortion),
		("CalamityMod/Buffs/StatDebuffs/WhisperingDeath", (NPC NPC) => NPC.Calamity().whisperingDeath),
		("CalamityMod/Buffs/StatDebuffs/WitherDebuff", (NPC NPC) => NPC.Calamity().wither)
	};

	public static readonly SoundStyle PlagueSound = new SoundStyle("CalamityMod/Sounds/Custom/PlagueUnleash");

	public static SortedDictionary<int, float> DRValues { get; set; }

	public float DR { get; set; }

	public int KillTime { get; set; }

	public override bool InstancePerEntity => true;

	public override GlobalNPC Clone(NPC npc, NPC npcClone)
	{
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC myClone = (CalamityGlobalNPC)base.Clone(npc, npcClone);
		myClone.DR = DR;
		myClone.unbreakableDR = unbreakableDR;
		myClone.KillTime = KillTime;
		myClone.VulnerableToHeat = VulnerableToHeat;
		myClone.VulnerableToCold = VulnerableToCold;
		myClone.VulnerableToSickness = VulnerableToSickness;
		myClone.VulnerableToElectricity = VulnerableToElectricity;
		myClone.VulnerableToWater = VulnerableToWater;
		myClone.IncreasedColdEffects_EskimoSet = IncreasedColdEffects_EskimoSet;
		myClone.IncreasedColdEffects_CryoStone = IncreasedColdEffects_CryoStone;
		myClone.IncreasedElectricityEffects_Unused = IncreasedElectricityEffects_Unused;
		myClone.IncreasedHeatEffects_Fireball = IncreasedHeatEffects_Fireball;
		myClone.IncreasedHeatEffects_CinnamonRoll = IncreasedHeatEffects_CinnamonRoll;
		myClone.IncreasedHeatEffects_FireBoots = IncreasedHeatEffects_FireBoots;
		myClone.IncreasedSicknessEffects_ToxicHeart = IncreasedSicknessEffects_ToxicHeart;
		myClone.IncreasedWaterEffects_Amulet1 = IncreasedWaterEffects_Amulet1;
		myClone.IncreasedWaterEffects_Amulet2 = IncreasedWaterEffects_Amulet2;
		myClone.IncreasedSicknessAndWaterEffects_CorrosiveSpine = IncreasedSicknessAndWaterEffects_CorrosiveSpine;
		myClone.IncreasedSicknessAndWaterEffects_EvergreenGin = IncreasedSicknessAndWaterEffects_EvergreenGin;
		myClone.IncreasedDebuffEffects_Amalgam = IncreasedDebuffEffects_Amalgam;
		myClone.velocityPriorToPhaseSwap = velocityPriorToPhaseSwap;
		myClone.canBreakPlayerDefense = canBreakPlayerDefense;
		myClone.miscDefenseLoss = miscDefenseLoss;
		myClone.dashImmunityTime = new int[256];
		for (int i = 0; i < 256; i++)
		{
			myClone.dashImmunityTime[i] = dashImmunityTime[i];
		}
		myClone.ProvidesProximityRage = ProvidesProximityRage;
		myClone.newAI = new float[4];
		for (int j = 0; j < 4; j++)
		{
			myClone.newAI[j] = newAI[j];
		}
		myClone.killTimeTimer = killTimeTimer;
		myClone.SplittingWorm = SplittingWorm;
		myClone.CanHaveBossHealthBar = CanHaveBossHealthBar;
		myClone.ShouldCloseHPBar = ShouldCloseHPBar;
		myClone.debuffResistanceTimer = debuffResistanceTimer;
		myClone.vaporfied = vaporfied;
		myClone.timeDistortion = timeDistortion;
		myClone.glacialState = glacialState;
		myClone.galvanicCorrosion = galvanicCorrosion;
		myClone.temporalSadness = temporalSadness;
		myClone.eutrophication = eutrophication;
		myClone.webbed = webbed;
		myClone.electrified = electrified;
		myClone.pearlAura = pearlAura;
		myClone.burningBlood = burningBlood;
		myClone.brainRot = brainRot;
		myClone.heavyBleeding = heavyBleeding;
		myClone.laceration = laceration;
		myClone.elementalMix = elementalMix;
		myClone.markedForDeath = markedForDeath;
		myClone.absorberAffliction = absorberAffliction;
		myClone.irradiated = irradiated;
		myClone.irradiatedContactBoost = irradiatedContactBoost;
		myClone.brimstoneFlames = brimstoneFlames;
		myClone.demonicFlames = demonicFlames;
		myClone.demonicFlamesBonusDamage = demonicFlamesBonusDamage;
		myClone.holyFlames = holyFlames;
		myClone.plague = plague;
		myClone.armorCrunch = armorCrunch;
		myClone.crumble = crumble;
		myClone.antlionCloudDebuffTimer = antlionCloudDebuffTimer;
		myClone.scionsCurioEffected = scionsCurioEffected;
		myClone.warbannerBurnTime = warbannerBurnTime;
		myClone.warbannerBurnTimer = warbannerBurnTimer;
		myClone.warbannerBurnStacks = warbannerBurnStacks;
		myClone.warbannerBurnDamage = warbannerBurnDamage;
		myClone.warbannerBurnDirection = warbannerBurnDirection;
		myClone.warbannerBurnIntensity = warbannerBurnIntensity;
		myClone.warbannerBurnMarked = warbannerBurnMarked;
		myClone.warbannerBurnHideEffects = warbannerBurnHideEffects;
		myClone.veriumDoomTimer = veriumDoomTimer;
		myClone.veriumDoomStacks = veriumDoomStacks;
		myClone.veriumDoomMarked = veriumDoomMarked;
		myClone.laserBurnDamage = laserBurnDamage;
		myClone.laserBurnMarked = laserBurnMarked;
		myClone.laserBurnStacks = laserBurnStacks;
		myClone.laserBurnTimer = laserBurnTimer;
		myClone.laserBurnType = laserBurnType;
		myClone.hyperiusDamage = hyperiusDamage;
		myClone.hyperiusMarked = hyperiusMarked;
		myClone.hyperiusOverflowTimer = hyperiusOverflowTimer;
		myClone.hyperiusFxTimer = hyperiusFxTimer;
		myClone.cursorFocus = cursorFocus;
		myClone.demonSwordImpales = demonSwordImpales;
		myClone.impalePacketTimer = impalePacketTimer;
		myClone.pacified = pacified;
		myClone.somaShredStacks = somaShredStacks;
		myClone.somaShredApplicator = somaShredApplicator;
		myClone.somaShredFalloff = somaShredFalloff;
		myClone.crushDepth = crushDepth;
		myClone.riptide = riptide;
		myClone.hadopelagicPressure = hadopelagicPressure;
		myClone.godSlayerInferno = godSlayerInferno;
		myClone.miracleBlight = miracleBlight;
		myClone.dragonFire = dragonFire;
		myClone.vermillionFlux = vermillionFlux;
		myClone.auricRebuke = auricRebuke;
		myClone.staticDischarge = staticDischarge;
		myClone.astralInfection = astralInfection;
		myClone.whisperingDeath = whisperingDeath;
		myClone.nightwither = nightwither;
		myClone.shocked = shocked;
		myClone.voidfrost = voidfrost;
		myClone.shellfishStaffDebuff = shellfishStaffDebuff;
		myClone.snapClamDebuff = snapClamDebuff;
		myClone.sulphurPoison = sulphurPoison;
		myClone.ladHearts = ladHearts;
		myClone.relicOfResilienceWeakness = relicOfResilienceWeakness;
		myClone.sagePoison = sagePoison;
		myClone.sagePoisonDamage = sagePoisonDamage;
		myClone.vulnerabilityHex = vulnerabilityHex;
		myClone.trueVulnerabilityHex = trueVulnerabilityHex;
		myClone.banishingFire = banishingFire;
		myClone.wither = wither;
		myClone.ashesOnDeath = ashesOnDeath;
		myClone.VulnerabilityHexFireDrawer = null;
		myClone.ManaBurnFireDrawer = null;
		myClone.CurrentlyEnraged = CurrentlyEnraged;
		myClone.CurrentlyIncreasingDefenseOrDR = CurrentlyIncreasingDefenseOrDR;
		myClone.DoesNotDisappearInBossRush = DoesNotDisappearInBossRush;
		return myClone;
	}

	public override void ResetEffects(NPC npc)
	{
		for (int i = 0; i < bobbitWormBottom.Length; i++)
		{
			ResetSavedIndex(ref bobbitWormBottom[i], ModContent.NPCType<BobbitWormSegment>());
		}
		ResetSavedIndex(ref hiveMind, ModContent.NPCType<global::CalamityMod.NPCs.HiveMind.HiveMind>());
		ResetSavedIndex(ref perfHive, ModContent.NPCType<PerforatorHive>());
		ResetSavedIndex(ref slimeGodPurple, ModContent.NPCType<EbonianPaladin>(), ModContent.NPCType<SplitEbonianPaladin>());
		ResetSavedIndex(ref slimeGodRed, ModContent.NPCType<CrimulanPaladin>(), ModContent.NPCType<SplitCrimulanPaladin>());
		ResetSavedIndex(ref slimeGod, ModContent.NPCType<SlimeGodCore>());
		ResetSavedIndex(ref laserEye, 125);
		ResetSavedIndex(ref fireEye, 126);
		ResetSavedIndex(ref primeLaser, 131);
		ResetSavedIndex(ref primeCannon, 128);
		ResetSavedIndex(ref primeVice, 130);
		ResetSavedIndex(ref primeSaw, 129);
		ResetSavedIndex(ref aquaticScourge, ModContent.NPCType<AquaticScourgeHead>());
		ResetSavedIndex(ref brimstoneElemental, ModContent.NPCType<global::CalamityMod.NPCs.BrimstoneElemental.BrimstoneElemental>());
		ResetSavedIndex(ref cataclysm, ModContent.NPCType<Cataclysm>());
		ResetSavedIndex(ref catastrophe, ModContent.NPCType<Catastrophe>());
		ResetSavedIndex(ref calamitas, ModContent.NPCType<CalamitasClone>());
		ResetSavedIndex(ref LeviAndAna, ModContent.NPCType<global::CalamityMod.NPCs.Leviathan.Leviathan>(), ModContent.NPCType<Anahita>());
		ResetSavedIndex(ref leviathan, ModContent.NPCType<global::CalamityMod.NPCs.Leviathan.Leviathan>());
		ResetSavedIndex(ref siren, ModContent.NPCType<Anahita>());
		ResetSavedIndex(ref astrumAureus, ModContent.NPCType<global::CalamityMod.NPCs.AstrumAureus.AstrumAureus>());
		ResetSavedIndex(ref scavenger, ModContent.NPCType<RavagerBody>());
		ResetSavedIndex(ref energyFlame, ModContent.NPCType<ProfanedEnergyBody>());
		ResetSavedIndex(ref doughnutBoss, ModContent.NPCType<ProfanedGuardianCommander>());
		ResetSavedIndex(ref doughnutBossDefender, ModContent.NPCType<ProfanedGuardianDefender>());
		ResetSavedIndex(ref doughnutBossHealer, ModContent.NPCType<ProfanedGuardianHealer>());
		ResetSavedIndex(ref holyBossAttacker, ModContent.NPCType<ProvSpawnOffense>());
		ResetSavedIndex(ref holyBossDefender, ModContent.NPCType<ProvSpawnDefense>());
		ResetSavedIndex(ref holyBossHealer, ModContent.NPCType<ProvSpawnHealer>());
		ResetSavedIndex(ref holyBoss, ModContent.NPCType<global::CalamityMod.NPCs.Providence.Providence>());
		ResetSavedIndex(ref voidBoss, ModContent.NPCType<global::CalamityMod.NPCs.CeaselessVoid.CeaselessVoid>());
		ResetSavedIndex(ref signus, ModContent.NPCType<global::CalamityMod.NPCs.Signus.Signus>());
		ResetSavedIndex(ref ghostBossClone, ModContent.NPCType<PolterPhantom>());
		ResetSavedIndex(ref ghostBoss, ModContent.NPCType<global::CalamityMod.NPCs.Polterghast.Polterghast>());
		ResetSavedIndex(ref DoGHead, ModContent.NPCType<DevourerofGodsHead>());
		ResetSavedIndex(ref DoGP2, ModContent.NPCType<DevourerofGodsHead>());
		ResetSavedIndex(ref yharon, ModContent.NPCType<global::CalamityMod.NPCs.Yharon.Yharon>());
		ResetSavedIndex(ref yharonP2, ModContent.NPCType<global::CalamityMod.NPCs.Yharon.Yharon>());
		ResetSavedIndex(ref SCalCataclysm, ModContent.NPCType<SupremeCataclysm>());
		ResetSavedIndex(ref SCalCatastrophe, ModContent.NPCType<SupremeCatastrophe>());
		ResetSavedIndex(ref SCal, ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>());
		ResetSavedIndex(ref SCalGrief, ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>());
		ResetSavedIndex(ref SCalLament, ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>());
		ResetSavedIndex(ref SCalEpiphany, ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>());
		ResetSavedIndex(ref SCalAcceptance, ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>());
		ResetSavedIndex(ref SCalWorm, ModContent.NPCType<SepulcherHead>());
		ResetSavedIndex(ref draedon, ModContent.NPCType<Draedon>());
		ResetSavedIndex(ref draedonAmbience, ModContent.NPCType<Draedon>());
		ResetSavedIndex(ref draedonExoMechWorm, ModContent.NPCType<ThanatosHead>());
		ResetSavedIndex(ref draedonExoMechTwinRed, ModContent.NPCType<Artemis>());
		ResetSavedIndex(ref draedonExoMechTwinGreen, ModContent.NPCType<Apollo>());
		ResetSavedIndex(ref draedonExoMechPrime, ModContent.NPCType<AresBody>());
		ResetSavedIndex(ref draedonExoMechPrimePlasmaCannon, ModContent.NPCType<AresPlasmaFlamethrower>());
		ResetSavedIndex(ref adultEidolonWyrmHead, ModContent.NPCType<PrimordialWyrmHead>());
		CurrentlyEnraged = false;
		CurrentlyIncreasingDefenseOrDR = false;
		CanHaveBossHealthBar = false;
		ShouldCloseHPBar = false;
		if (arcZapCooldown > 0)
		{
			arcZapCooldown--;
		}
		if (debuffResistanceTimer > 0)
		{
			debuffResistanceTimer--;
		}
		timeDistortion = false;
		galvanicCorrosion = false;
		glacialState = false;
		temporalSadness = false;
		eutrophication = false;
		webbed = false;
		vaporfied = false;
		electrified = false;
		pearlAura = false;
		burningBlood = false;
		brainRot = false;
		heavyBleeding = false;
		laceration = false;
		elementalMix = false;
		if (!trueVulnerabilityHex && !vulnerabilityHex)
		{
			cursorFocus = 0;
		}
		trueVulnerabilityHex = false;
		vulnerabilityHex = false;
		markedForDeath = false;
		absorberAffliction = false;
		irradiated = false;
		if (scionsCurioEffected)
		{
			irradiatedContactBoost = 2.0;
		}
		brimstoneFlames = false;
		if (!demonicFlames)
		{
			demonicFlamesBonusDamage = 0;
		}
		demonicFlames = false;
		holyFlames = false;
		plague = false;
		armorCrunch = false;
		crumble = false;
		crushDepth = false;
		hadopelagicPressure = false;
		riptide = false;
		godSlayerInferno = false;
		dragonFire = false;
		vermillionFlux = false;
		auricRebuke = false;
		staticDischarge = false;
		miracleBlight = false;
		astralInfection = false;
		whisperingDeath = false;
		nightwither = false;
		if (shocked > 0)
		{
			shocked--;
		}
		voidfrost = false;
		shellfishStaffDebuff = false;
		snapClamDebuff = false;
		sulphurPoison = false;
		sagePoison = false;
		if (ladHearts > 0)
		{
			ladHearts--;
		}
		banishingFire = false;
		wither = false;
		if (ashesOnDeath > 0)
		{
			ashesOnDeath--;
		}
		if (antlionCloudDebuffTimer > 0)
		{
			antlionCloudDebuffTimer--;
		}
		if (cursorFocus > 0 && cursorFocus < 300)
		{
			cursorFocus--;
		}
		relicOfResilienceWeakness = false;
		static void ResetSavedIndex(ref int type, int type1, int type2 = -1)
		{
			if (type >= 0)
			{
				if (!Main.npc[type].active)
				{
					type = -1;
				}
				else if (type2 == -1)
				{
					if (Main.npc[type].type != type1)
					{
						type = -1;
					}
				}
				else if (Main.npc[type].type != type1 && Main.npc[type].type != type2)
				{
					type = -1;
				}
			}
		}
	}

	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		if (npc.defDamage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage && BiomeTileCounterSystem.SulphurTiles > 30 && !npc.buffImmune[20] && !npc.buffImmune[ModContent.BuffType<CrushDepth>()])
		{
			if (npc.wet)
			{
				npc.AddBuff(20, 2);
			}
			if (Main.raining)
			{
				npc.AddBuff(ModContent.BuffType<Irradiated>(), 2);
			}
		}
		if (npc.venom)
		{
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			int projectileCount = 0;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if ((p.type == ModContent.ProjectileType<LionfishProj>() || p.type == ModContent.ProjectileType<JawsProjectile>()) && p.ai[0] == 1f && p.ai[1] == (float)npc.whoAmI)
				{
					projectileCount++;
				}
			}
			if (projectileCount > 0)
			{
				npc.lifeRegen -= projectileCount * (int)(DebuffData.AcidVenom.EnemyLostRegen / 2f);
				if (damage < projectileCount * 6)
				{
					damage = projectileCount * 6;
				}
			}
		}
		bool wormBoss = CalamityNPCTypeSets.DesertScourge.Contains(npc.type) || CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) || CalamityNPCTypeSets.Perforators.Contains(npc.type) || CalamityNPCTypeSets.AquaticScourge.Contains(npc.type) || CalamityNPCTypeSets.AstrumDeus.Contains(npc.type) || CalamityNPCTypeSets.StormWeaver.Contains(npc.type);
		CalamityNPCTypeSets.SlimeGod.Contains(npc.type);
		ActiveHeatDebuffMultiplier = HeatDebuffMultiplier;
		ActiveColdDebuffMultiplier = ColdDebuffMultiplier;
		ActiveSicknessDebuffMultiplier = SicknessDebuffMultiplier;
		ActiveElectricDebuffMultiplier = ElectricDebuffMultiplier;
		ActiveWaterDebuffMultiplier = WaterDebuffMultiplier;
		if (irradiated)
		{
			float irradiatedBoost = (scionsCurioEffected ? 1.75f : 1f);
			ActiveSicknessDebuffMultiplier += irradiatedBoost;
		}
		if (npc.drippingSlime || npc.drippingSparkleSlime)
		{
			ActiveHeatDebuffMultiplier += 1f;
		}
		if (npc.wet || npc.honeyWet || npc.lavaWet || npc.dripping)
		{
			ActiveElectricDebuffMultiplier += 1f;
		}
		if (npc.wet || npc.honeyWet || npc.dripping)
		{
			ActiveColdDebuffMultiplier += 1f;
			ActiveHeatDebuffMultiplier -= 0.5f;
		}
		if (npc.HasBuff(46))
		{
			ActiveWaterDebuffMultiplier += 1f;
		}
		if (VulnerableToHeat.HasValue)
		{
			if (VulnerableToHeat.Value)
			{
				ActiveHeatDebuffMultiplier *= (wormBoss ? 1.5f : 2f);
			}
			else
			{
				ActiveHeatDebuffMultiplier *= 0.5f;
			}
		}
		if (VulnerableToCold.HasValue)
		{
			if (VulnerableToCold.Value)
			{
				ActiveColdDebuffMultiplier *= (wormBoss ? 1.5f : 2f);
			}
			else
			{
				ActiveColdDebuffMultiplier *= 0.5f;
			}
		}
		if (VulnerableToSickness.HasValue)
		{
			if (VulnerableToSickness.Value)
			{
				ActiveSicknessDebuffMultiplier *= (wormBoss ? 1.5f : 2f);
			}
			else
			{
				ActiveSicknessDebuffMultiplier *= 0.5f;
			}
		}
		if (VulnerableToElectricity.HasValue)
		{
			if (VulnerableToElectricity.Value)
			{
				ActiveElectricDebuffMultiplier *= (wormBoss ? 1.5f : 2f);
			}
			else
			{
				ActiveElectricDebuffMultiplier *= 0.5f;
			}
		}
		if (VulnerableToWater.HasValue)
		{
			if (VulnerableToWater.Value)
			{
				ActiveWaterDebuffMultiplier *= (wormBoss ? 1.5f : 2f);
			}
			else
			{
				ActiveWaterDebuffMultiplier *= 0.5f;
			}
		}
		for (int index = 0; index < npc.buffType.Length; index++)
		{
			int type = npc.buffType[index];
			DebuffData debuffData = BuffDatasets.DebuffDataset[type];
			if (debuffData != null && debuffData != DebuffData.Oiled)
			{
				debuffData.NPCLifeRegenMethod(npc, type, ref index, ref damage);
			}
		}
		bool hasVanillaOil = npc.onFrostBurn || npc.onFrostBurn2 || npc.onFire || npc.onFire2 || npc.onFire3 || npc.shadowFlame;
		if (npc.oiled)
		{
			DebuffData oil = DebuffData.Oiled;
			int index2 = npc.FindBuffIndex(204);
			if (hasVanillaOil)
			{
				npc.lifeRegen -= oil.EnemyVanillaRegenToCancelOut;
			}
			oil.NPCLifeRegenMethod(npc, 204, ref index2, ref damage);
		}
		if (somaShredStacks > 0)
		{
			Shred.TickDebuff(npc, this);
		}
		if ((wormBoss || npc.type == 267) && npc.lifeRegen < 0)
		{
			npc.lifeRegen /= 4;
			if (npc.lifeRegen > -1)
			{
				npc.lifeRegen = -1;
			}
			if (((npc.ai[2] % 2f == 0f && npc.type == 14) || npc.type == 13) && (CalamityWorld.death || BossRushEvent.BossRushActive))
			{
				npc.lifeRegen = 0;
			}
		}
		if (manaBurn > 0f)
		{
			if (manaBurnPeak >= 0.1f)
			{
				manaBurnPeak *= 0.999f;
			}
			manaBurnPeak = Math.Max(manaBurnPeak, manaBurn);
			int burnPerSecond = (int)MathF.Ceiling(manaBurn * 0.5f);
			manaBurn -= (float)burnPerSecond / 60f;
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			npc.lifeRegen -= burnPerSecond * 2;
			damage += (int)((float)burnPerSecond * 0.5f);
		}
		else
		{
			manaBurnPeak = 0f;
			playerManaBurnIntensity = 0f;
		}
		if (glaiveShredTimer > 0 || blazingStarShredTimer > 0)
		{
			int dmg = 0;
			if (glaiveShredTimer > 0)
			{
				dmg += 120;
				glaiveShredTimer--;
			}
			if (blazingStarShredTimer > 0)
			{
				dmg += 480;
				blazingStarShredTimer--;
			}
			npc.lifeRegenCount -= dmg;
			if (damage < dmg / 12)
			{
				damage = dmg / 12;
			}
			if (-120 * damage >= npc.lifeRegenCount)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(npc.Center, Utils.RotatedByRandom(new Vector2(1f, 0f), 7.0), "CalamityMod/Particles/TrientCircularSmear", affectedByGravity: false, 15, 0.4f + 0.1f * (float)npc.width / 16f, Color.White, new Vector2(0.5f, 1f)));
			}
		}
	}

	public void ApplyDPSDebuff(int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
	{
		if (lifeRegen > 0)
		{
			lifeRegen = 0;
		}
		lifeRegen -= lifeRegenValue;
		if (damage < damageValue)
		{
			damage = damageValue;
		}
	}

	public override void Load()
	{
		DRValues = new SortedDictionary<int, float>
		{
			{ 439, 0.15f },
			{ 370, 0.15f },
			{ 245, 0.15f },
			{ 247, 0.15f },
			{ 248, 0.15f },
			{ 246, 0.15f },
			{ 398, 0.15f },
			{ 397, 0.15f },
			{ 396, 0.15f },
			{ 262, 0.15f },
			{ 636, 0.15f },
			{ 128, 0.2f },
			{ 131, 0.2f },
			{ 129, 0.2f },
			{ 130, 0.2f },
			{ 125, 0.2f },
			{ 127, 0.2f },
			{ 126, 0.2f },
			{ 134, 0.1f },
			{ 135, 0.2f },
			{ 136, 0.35f },
			{ 113, 0.15f }
		};
		BossKillTimes = new SortedDictionary<int, int>();
	}

	public override void Unload()
	{
		DRValues?.Clear();
		DRValues = null;
		BossKillTimes?.Clear();
		BossKillTimes = null;
	}

	public override void SetStaticDefaults()
	{
		Extensions.AddRange<int, int>((IDictionary<int, int>)BossKillTimes, (IDictionary<int, int>)new Dictionary<int, int>
		{
			{ 50, 5400 },
			{ 4, 5400 },
			{ 13, 7200 },
			{ 14, 7200 },
			{ 15, 7200 },
			{ 266, 7200 },
			{ 267, 1800 },
			{ 668, 5400 },
			{ 222, 7200 },
			{ 35, 7200 },
			{ 113, 7200 },
			{ 114, 7200 },
			{ 657, 7200 },
			{ 126, 10800 },
			{ 125, 10800 },
			{ 134, 10800 },
			{ 135, 10800 },
			{ 136, 10800 },
			{ 127, 10800 },
			{ 262, 10800 },
			{ 245, 9000 },
			{ 246, 3600 },
			{ 370, 9000 },
			{ 636, 10800 },
			{ 439, 9000 },
			{ 398, 14400 },
			{ 397, 7200 },
			{ 396, 7200 },
			{
				ModContent.NPCType<DesertScourgeHead>(),
				5400
			},
			{
				ModContent.NPCType<DesertScourgeBody>(),
				5400
			},
			{
				ModContent.NPCType<DesertScourgeTail>(),
				5400
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Crabulon.Crabulon>(),
				5400
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.HiveMind.HiveMind>(),
				7200
			},
			{
				ModContent.NPCType<PerforatorHive>(),
				7200
			},
			{
				ModContent.NPCType<SlimeGodCore>(),
				9000
			},
			{
				ModContent.NPCType<EbonianPaladin>(),
				4500
			},
			{
				ModContent.NPCType<CrimulanPaladin>(),
				4500
			},
			{
				ModContent.NPCType<SplitEbonianPaladin>(),
				4500
			},
			{
				ModContent.NPCType<SplitCrimulanPaladin>(),
				4500
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Cryogen.Cryogen>(),
				10800
			},
			{
				ModContent.NPCType<AquaticScourgeHead>(),
				9000
			},
			{
				ModContent.NPCType<AquaticScourgeBody>(),
				9000
			},
			{
				ModContent.NPCType<AquaticScourgeBodyAlt>(),
				9000
			},
			{
				ModContent.NPCType<AquaticScourgeTail>(),
				9000
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.BrimstoneElemental.BrimstoneElemental>(),
				10800
			},
			{
				ModContent.NPCType<CalamitasClone>(),
				10800
			},
			{
				ModContent.NPCType<Anahita>(),
				10800
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Leviathan.Leviathan>(),
				10800
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.AstrumAureus.AstrumAureus>(),
				10800
			},
			{
				ModContent.NPCType<AstrumDeusHead>(),
				7200
			},
			{
				ModContent.NPCType<AstrumDeusBody>(),
				7200
			},
			{
				ModContent.NPCType<AstrumDeusTail>(),
				7200
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.PlaguebringerGoliath.PlaguebringerGoliath>(),
				10800
			},
			{
				ModContent.NPCType<RavagerBody>(),
				10800
			},
			{
				ModContent.NPCType<ProfanedGuardianCommander>(),
				7200
			},
			{
				ModContent.NPCType<Dragonfolly>(),
				7200
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Providence.Providence>(),
				14400
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.CeaselessVoid.CeaselessVoid>(),
				10800
			},
			{
				ModContent.NPCType<DarkEnergy>(),
				1200
			},
			{
				ModContent.NPCType<StormWeaverHead>(),
				8100
			},
			{
				ModContent.NPCType<StormWeaverBody>(),
				8100
			},
			{
				ModContent.NPCType<StormWeaverTail>(),
				8100
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Signus.Signus>(),
				7200
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Polterghast.Polterghast>(),
				10800
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.OldDuke.OldDuke>(),
				10800
			},
			{
				ModContent.NPCType<DevourerofGodsHead>(),
				14400
			},
			{
				ModContent.NPCType<DevourerofGodsBody>(),
				14400
			},
			{
				ModContent.NPCType<DevourerofGodsTail>(),
				14400
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.Yharon.Yharon>(),
				14400
			},
			{
				ModContent.NPCType<Apollo>(),
				21600
			},
			{
				ModContent.NPCType<Artemis>(),
				21600
			},
			{
				ModContent.NPCType<AresBody>(),
				21600
			},
			{
				ModContent.NPCType<AresGaussNuke>(),
				21600
			},
			{
				ModContent.NPCType<AresLaserCannon>(),
				21600
			},
			{
				ModContent.NPCType<AresPlasmaFlamethrower>(),
				21600
			},
			{
				ModContent.NPCType<AresTeslaCannon>(),
				21600
			},
			{
				ModContent.NPCType<ThanatosHead>(),
				21600
			},
			{
				ModContent.NPCType<ThanatosBody1>(),
				21600
			},
			{
				ModContent.NPCType<ThanatosBody2>(),
				21600
			},
			{
				ModContent.NPCType<ThanatosTail>(),
				21600
			},
			{
				ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>(),
				18000
			},
			{
				ModContent.NPCType<PrimordialWyrmHead>(),
				18000
			}
		});
		NPCID.Sets.TrailingMode[262] = 1;
		NPCID.Sets.MPAllowedEnemies[398] = true;
	}

	public override void SetDefaults(NPC npc)
	{
		for (int i = 0; i < 256; i++)
		{
			dashImmunityTime[i] = 0;
		}
		for (int m = 0; m < 4; m++)
		{
			newAI[m] = 0f;
		}
		if (DRValues.ContainsKey(npc.type))
		{
			DRValues.TryGetValue(npc.type, out var newDR);
			DR = newDR;
		}
		if (BossKillTimes.TryGetValue(npc.type, out var revKillTime) && !CalamityNPCTypeSets.AquaticScourge.Contains(npc.type))
		{
			KillTime = revKillTime;
		}
		if (npc.type == 114)
		{
			npc.netAlways = true;
		}
		sagePoisonDamage = 0;
		if (npc.type == 245 && (CalamityWorld.revenge || BossRushEvent.BossRushActive))
		{
			npc.noGravity = true;
		}
		DeclareBossHealthUIVariables(npc);
		if (BossRushEvent.BossRushActive)
		{
			BossRushStatChanges(npc, base.Mod);
		}
		if (CalamityWorld.revenge)
		{
			RevDeathStatChanges(npc, base.Mod);
		}
		OtherStatChanges(npc);
		if (npc.type == 657)
		{
			npc.DeathSound = (Main.zenithWorld ? new SoundStyle("CalamityMod/Sounds/Item/GFBScreams/Scream", 8) : SoundID.NPCDeath1);
		}
		npc.SetDebuffImmunities();
		VulnerabilitiesAndResistances(npc);
		if (npc.type == 266 && CalamityWorld.revenge)
		{
			npc.BossBar = ModContent.GetInstance<RevBrainOfCthulhuBossBar>();
		}
		if (Main.BigBossProgressBar.TryGetSpecialVanillaBossBar(npc.type, out var bar) && bar is MoonLordProgressBar && CalamityWorld.revenge)
		{
			npc.BossBar = ModContent.GetInstance<RevMoonLordBossBar>();
		}
	}

	public override bool? CanFallThroughPlatforms(NPC npc)
	{
		if (npc.type == 249 && (CalamityWorld.revenge || BossRushEvent.BossRushActive))
		{
			return true;
		}
		return base.CanFallThroughPlatforms(npc);
	}

	public void DeclareBossHealthUIVariables(NPC npc)
	{
		if (npc.type == 13 || npc.type == 14 || npc.type == 15)
		{
			SplittingWorm = true;
		}
	}

	private void BossRushStatChanges(NPC npc, Mod mod)
	{
		if (CalamityNPCSets.BossRushHealth.TryGetValue(npc.type, out var newHP))
		{
			npc.lifeMax = newHP;
		}
	}

	private void RevDeathStatChanges(NPC npc, Mod mod)
	{
		if (!BossRushEvent.BossRushActive)
		{
			switch (npc.type)
			{
			case 50:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.5);
				break;
			case 4:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.5);
				break;
			case 5:
				npc.lifeMax *= 4;
				break;
			case 266:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.75);
				break;
			case 222:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.8);
				break;
			case 210:
			case 211:
				if (CalamityPlayer.areThereAnyDamnBosses)
				{
					npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.5);
				}
				break;
			case 668:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.4);
				break;
			case 36:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * (CalamityWorld.death ? 0.5 : 0.75));
				break;
			case 113:
			case 114:
				npc.lifeMax *= 2;
				break;
			case 657:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.8);
				break;
			case 125:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.4);
				break;
			case 126:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.3);
				break;
			case 139:
				if (CalamityWorld.death)
				{
					npc.lifeMax *= 2;
				}
				break;
			case 127:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.2);
				break;
			case 128:
			case 129:
			case 130:
			case 131:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.65);
				break;
			case 262:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 2.85);
				break;
			case 264:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.5);
				break;
			case 245:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 3.2);
				break;
			case 246:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.28);
				break;
			case 247:
			case 248:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.75);
				break;
			case 370:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 2.3);
				break;
			case 372:
			case 373:
				npc.lifeMax *= 5;
				break;
			case 636:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.7);
				break;
			case 439:
				npc.lifeMax *= 3;
				break;
			case 521:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.8);
				break;
			case 398:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 2.2);
				break;
			case 396:
			case 397:
			case 401:
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.2);
				break;
			}
			if (npc.type >= 454 && npc.type <= 459)
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 2.4);
			}
			else if (CalamityNPCTypeSets.Destroyer.Contains(npc.type))
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.25);
			}
			else if (CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type))
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 1.4);
			}
		}
		switch (npc.type)
		{
		case 50:
			npc.scale = ((!CalamityWorld.death) ? (Main.getGoodWorld ? 3f : 1.5f) : (Main.getGoodWorld ? 6f : 2.5f));
			break;
		case 222:
			npc.defense = 14;
			npc.defDefense = npc.defense;
			break;
		case 210:
		case 211:
			if (CalamityPlayer.areThereAnyDamnBosses)
			{
				npc.scale *= 1.25f;
			}
			break;
		case 139:
			npc.scale *= (Main.zenithWorld ? 2f : 1.2f);
			break;
		case 128:
		case 129:
		case 130:
		case 131:
			npc.scale *= 1.15f;
			break;
		case 247:
		case 248:
			npc.scale *= 1.15f;
			break;
		case 477:
			npc.scale *= 1.25f;
			break;
		case 82:
		case 85:
		case 253:
		case 316:
		case 341:
		case 541:
		case 629:
			if (Main.getGoodWorld)
			{
				npc.knockBackResist = 0f;
			}
			break;
		}
		if (CalamityNPCTypeSets.Destroyer.Contains(npc.type))
		{
			npc.scale *= (Main.zenithWorld ? 2f : 1.2f);
		}
		else if (CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) && CalamityWorld.death)
		{
			npc.scale *= 1.1f;
		}
	}

	private void VulnerabilitiesAndResistances(NPC npc)
	{
		switch (npc.type)
		{
		case 69:
		case 78:
		case 79:
		case 80:
		case 508:
		case 509:
		case 510:
		case 511:
		case 512:
		case 513:
		case 514:
		case 515:
		case 524:
		case 525:
		case 526:
		case 527:
		case 528:
		case 529:
		case 532:
		case 542:
		case 543:
		case 544:
		case 545:
		case 546:
		case 580:
		case 581:
		case 582:
		case 630:
			VulnerableToCold = true;
			VulnerableToSickness = true;
			VulnerableToWater = true;
			break;
		case 530:
		case 531:
		case 541:
			VulnerableToCold = true;
			VulnerableToSickness = false;
			VulnerableToWater = true;
			break;
		case 537:
			VulnerableToCold = true;
			VulnerableToSickness = false;
			VulnerableToWater = true;
			VulnerableToHeat = true;
			break;
		case -33:
		case -32:
		case 187:
		case 433:
			VulnerableToCold = true;
			VulnerableToHeat = true;
			break;
		case 59:
			VulnerableToCold = true;
			VulnerableToSickness = false;
			VulnerableToHeat = false;
			VulnerableToWater = true;
			break;
		case -25:
		case -24:
		case -10:
		case -9:
		case -8:
		case -7:
		case -6:
		case -5:
		case -4:
		case -3:
		case -2:
		case -1:
		case 1:
		case 16:
		case 50:
		case 71:
		case 81:
		case 121:
		case 122:
		case 138:
		case 141:
		case 183:
		case 204:
		case 225:
		case 244:
		case 302:
		case 333:
		case 334:
		case 335:
		case 336:
		case 535:
		case 657:
		case 658:
		case 659:
		case 660:
		case 667:
		case 676:
			VulnerableToSickness = false;
			VulnerableToHeat = true;
			break;
		case 277:
		case 278:
		case 279:
		case 280:
		case 285:
		case 286:
			VulnerableToHeat = false;
			VulnerableToCold = true;
			VulnerableToSickness = false;
			VulnerableToWater = true;
			break;
		case 635:
			VulnerableToHeat = true;
			VulnerableToSickness = false;
			VulnerableToWater = true;
			break;
		case -53:
		case -52:
		case -51:
		case -50:
		case -49:
		case -48:
		case -47:
		case -46:
		case -15:
		case -14:
		case -13:
		case 21:
		case 31:
		case 32:
		case 34:
		case 35:
		case 36:
		case 39:
		case 40:
		case 41:
		case 44:
		case 45:
		case 68:
		case 77:
		case 110:
		case 167:
		case 172:
		case 197:
		case 201:
		case 202:
		case 203:
		case 245:
		case 246:
		case 247:
		case 248:
		case 249:
		case 269:
		case 270:
		case 271:
		case 272:
		case 273:
		case 274:
		case 275:
		case 276:
		case 281:
		case 282:
		case 283:
		case 284:
		case 287:
		case 289:
		case 291:
		case 292:
		case 293:
		case 294:
		case 295:
		case 296:
		case 322:
		case 323:
		case 324:
		case 449:
		case 450:
		case 451:
		case 452:
		case 481:
		case 482:
		case 483:
		case 566:
		case 567:
		case 631:
			VulnerableToSickness = false;
			VulnerableToWater = true;
			break;
		case 85:
		case 140:
		case 290:
		case 341:
		case 473:
		case 474:
		case 475:
		case 476:
		case 492:
			VulnerableToSickness = false;
			break;
		case 127:
		case 128:
		case 129:
		case 130:
		case 131:
		case 134:
		case 135:
		case 136:
		case 139:
		case 346:
		case 347:
		case 378:
		case 387:
		case 388:
		case 392:
		case 393:
		case 394:
		case 395:
		case 399:
		case 467:
		case 520:
			VulnerableToElectricity = true;
			VulnerableToSickness = false;
			break;
		case 75:
		case 82:
		case 83:
		case 84:
		case 120:
		case 179:
		case 253:
		case 288:
		case 316:
		case 330:
		case 454:
		case 455:
		case 456:
		case 457:
		case 458:
		case 459:
		case 472:
		case 521:
		case 533:
		case 662:
			VulnerableToSickness = false;
			break;
		case -55:
		case -54:
		case -45:
		case -44:
		case -43:
		case -37:
		case -36:
		case -35:
		case -34:
		case -31:
		case -30:
		case -29:
		case -28:
		case -27:
		case -26:
		case -23:
		case -22:
		case -12:
		case -11:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 26:
		case 27:
		case 28:
		case 29:
		case 43:
		case 47:
		case 48:
		case 49:
		case 51:
		case 52:
		case 53:
		case 56:
		case 61:
		case 73:
		case 86:
		case 87:
		case 88:
		case 89:
		case 90:
		case 91:
		case 92:
		case 93:
		case 94:
		case 95:
		case 96:
		case 97:
		case 98:
		case 99:
		case 100:
		case 101:
		case 104:
		case 109:
		case 111:
		case 125:
		case 126:
		case 132:
		case 133:
		case 137:
		case 152:
		case 153:
		case 158:
		case 159:
		case 162:
		case 164:
		case 165:
		case 166:
		case 173:
		case 174:
		case 175:
		case 177:
		case 181:
		case 182:
		case 186:
		case 188:
		case 189:
		case 196:
		case 198:
		case 199:
		case 200:
		case 205:
		case 212:
		case 213:
		case 214:
		case 215:
		case 216:
		case 217:
		case 218:
		case 219:
		case 223:
		case 226:
		case 239:
		case 240:
		case 251:
		case 252:
		case 254:
		case 255:
		case 257:
		case 258:
		case 259:
		case 260:
		case 266:
		case 267:
		case 268:
		case 301:
		case 304:
		case 305:
		case 306:
		case 307:
		case 308:
		case 309:
		case 310:
		case 311:
		case 312:
		case 313:
		case 314:
		case 315:
		case 317:
		case 318:
		case 319:
		case 320:
		case 321:
		case 325:
		case 326:
		case 327:
		case 329:
		case 331:
		case 332:
		case 344:
		case 379:
		case 380:
		case 381:
		case 382:
		case 383:
		case 385:
		case 386:
		case 389:
		case 390:
		case 391:
		case 396:
		case 397:
		case 398:
		case 402:
		case 405:
		case 406:
		case 407:
		case 409:
		case 410:
		case 411:
		case 414:
		case 415:
		case 416:
		case 417:
		case 418:
		case 419:
		case 420:
		case 421:
		case 422:
		case 423:
		case 424:
		case 425:
		case 426:
		case 427:
		case 428:
		case 429:
		case 430:
		case 432:
		case 434:
		case 435:
		case 436:
		case 438:
		case 439:
		case 460:
		case 462:
		case 463:
		case 464:
		case 466:
		case 468:
		case 469:
		case 471:
		case 477:
		case 478:
		case 479:
		case 480:
		case 489:
		case 490:
		case 493:
		case 496:
		case 497:
		case 498:
		case 499:
		case 500:
		case 501:
		case 502:
		case 503:
		case 504:
		case 505:
		case 506:
		case 507:
		case 517:
		case 518:
		case 536:
		case 552:
		case 553:
		case 554:
		case 555:
		case 556:
		case 557:
		case 558:
		case 559:
		case 560:
		case 561:
		case 562:
		case 563:
		case 564:
		case 565:
		case 568:
		case 569:
		case 570:
		case 571:
		case 572:
		case 573:
		case 574:
		case 575:
		case 576:
		case 577:
		case 586:
		case 587:
		case 590:
		case 591:
		case 618:
		case 619:
		case 620:
		case 621:
		case 622:
		case 623:
		case 624:
		case 628:
		case 632:
		case 634:
		case 636:
			VulnerableToCold = true;
			VulnerableToHeat = true;
			VulnerableToSickness = true;
			break;
		case 62:
		case 66:
		case 113:
		case 114:
		case 115:
		case 116:
		case 117:
		case 118:
		case 119:
		case 156:
		case 534:
			VulnerableToCold = true;
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			break;
		case 24:
		case 60:
		case 151:
			VulnerableToCold = true;
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			VulnerableToWater = true;
			break;
		case 23:
			VulnerableToCold = true;
			VulnerableToHeat = false;
			VulnerableToSickness = false;
			VulnerableToWater = true;
			break;
		case 578:
			VulnerableToElectricity = false;
			VulnerableToCold = true;
			VulnerableToHeat = true;
			VulnerableToSickness = true;
			break;
		case 551:
			VulnerableToCold = true;
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			break;
		case 250:
			VulnerableToCold = true;
			VulnerableToElectricity = false;
			VulnerableToWater = false;
			VulnerableToHeat = false;
			VulnerableToSickness = false;
			break;
		case 150:
		case 154:
		case 155:
		case 161:
		case 168:
		case 170:
		case 171:
		case 180:
		case 185:
		case 206:
		case 338:
		case 339:
		case 340:
		case 343:
		case 348:
		case 349:
		case 350:
		case 351:
		case 431:
		case 470:
		case 668:
			VulnerableToHeat = true;
			VulnerableToCold = false;
			VulnerableToSickness = true;
			break;
		case 143:
		case 144:
		case 145:
		case 147:
		case 169:
		case 184:
		case 243:
		case 342:
		case 345:
		case 352:
		case 629:
			VulnerableToCold = false;
			VulnerableToHeat = true;
			VulnerableToSickness = false;
			break;
		case 57:
		case 58:
		case 63:
		case 64:
		case 65:
		case 67:
		case 102:
		case 103:
		case 157:
		case 220:
		case 221:
		case 224:
		case 241:
		case 242:
		case 256:
		case 370:
		case 372:
		case 373:
		case 461:
		case 465:
		case 494:
		case 495:
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			VulnerableToElectricity = true;
			VulnerableToWater = false;
			break;
		case -65:
		case -64:
		case -63:
		case -62:
		case -61:
		case -60:
		case -59:
		case -58:
		case -57:
		case -56:
		case -21:
		case -20:
		case -19:
		case -18:
		case 42:
		case 163:
		case 176:
		case 210:
		case 211:
		case 222:
		case 231:
		case 232:
		case 233:
		case 234:
		case 235:
		case 236:
		case 237:
		case 238:
		case 262:
		case 264:
			VulnerableToCold = true;
			VulnerableToHeat = true;
			VulnerableToSickness = false;
			break;
		case 17:
		case 18:
		case 19:
		case 20:
		case 22:
		case 37:
		case 38:
		case 54:
		case 105:
		case 106:
		case 107:
		case 108:
		case 123:
		case 124:
		case 160:
		case 178:
		case 207:
		case 208:
		case 227:
		case 228:
		case 229:
		case 353:
		case 354:
		case 368:
		case 369:
		case 376:
		case 550:
		case 579:
		case 588:
		case 589:
		case 633:
		case 637:
		case 638:
		case 656:
		case 663:
			VulnerableToCold = true;
			VulnerableToHeat = true;
			VulnerableToSickness = true;
			break;
		case 142:
			VulnerableToCold = false;
			VulnerableToHeat = true;
			VulnerableToSickness = true;
			break;
		case 441:
			VulnerableToCold = true;
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			break;
		case 209:
		case 685:
			VulnerableToSickness = false;
			break;
		case 670:
		case 678:
		case 679:
		case 680:
		case 681:
		case 682:
		case 683:
		case 684:
		case 686:
			VulnerableToSickness = false;
			VulnerableToHeat = true;
			break;
		case 46:
		case 74:
		case 297:
		case 298:
		case 299:
		case 300:
		case 303:
		case 337:
		case 355:
		case 356:
		case 357:
		case 358:
		case 359:
		case 360:
		case 361:
		case 362:
		case 363:
		case 364:
		case 365:
		case 366:
		case 367:
		case 374:
		case 375:
		case 377:
		case 442:
		case 443:
		case 444:
		case 445:
		case 446:
		case 447:
		case 448:
		case 484:
		case 485:
		case 486:
		case 487:
		case 538:
		case 539:
		case 540:
		case 595:
		case 596:
		case 597:
		case 598:
		case 599:
		case 600:
		case 601:
		case 602:
		case 603:
		case 604:
		case 605:
		case 606:
		case 608:
		case 609:
		case 610:
		case 611:
		case 612:
		case 613:
		case 614:
		case 661:
		case 669:
		case 671:
		case 672:
		case 673:
		case 674:
		case 675:
		case 677:
		case 687:
			VulnerableToCold = true;
			VulnerableToHeat = true;
			VulnerableToSickness = true;
			break;
		case 55:
		case 230:
		case 592:
		case 593:
		case 607:
		case 615:
		case 616:
		case 617:
		case 625:
		case 626:
		case 627:
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			VulnerableToElectricity = true;
			VulnerableToWater = false;
			break;
		case 148:
		case 149:
			VulnerableToCold = false;
			VulnerableToHeat = true;
			VulnerableToSickness = true;
			break;
		case 583:
		case 584:
		case 585:
			VulnerableToSickness = false;
			break;
		case 639:
		case 640:
		case 641:
		case 642:
		case 643:
		case 644:
		case 645:
		case 646:
		case 647:
		case 648:
		case 649:
		case 651:
		case 652:
			VulnerableToCold = true;
			VulnerableToSickness = true;
			VulnerableToWater = true;
			break;
		case 653:
		case 654:
		case 655:
			VulnerableToCold = true;
			VulnerableToHeat = false;
			VulnerableToSickness = true;
			VulnerableToWater = true;
			break;
		case -42:
		case -41:
		case -40:
		case -39:
		case -38:
		case -17:
		case -16:
		case 0:
		case 25:
		case 30:
		case 33:
		case 70:
		case 72:
		case 76:
		case 112:
		case 146:
		case 190:
		case 191:
		case 192:
		case 193:
		case 194:
		case 195:
		case 261:
		case 263:
		case 265:
		case 328:
		case 371:
		case 384:
		case 400:
		case 401:
		case 403:
		case 404:
		case 408:
		case 412:
		case 413:
		case 437:
		case 440:
		case 453:
		case 488:
		case 491:
		case 516:
		case 519:
		case 522:
		case 523:
		case 547:
		case 548:
		case 549:
		case 594:
		case 650:
		case 664:
		case 665:
		case 666:
			break;
		}
	}

	private void OtherStatChanges(NPC npc)
	{
		EditGlobalCoinDrops(npc);
		if ((npc.boss && npc.type != 395) || CalamityNPCSets.ScalesHealthLikeBoss[npc.type])
		{
			double HPBoost = (double)CalamityServerConfig.Instance.BossHealthBoost * 0.01;
			npc.lifeMax += (int)Math.Round((double)npc.lifeMax * HPBoost);
		}
		switch (npc.type)
		{
		case 4:
		case 13:
		case 35:
		case 50:
		case 68:
		case 113:
		case 125:
		case 126:
		case 127:
		case 129:
		case 130:
		case 134:
		case 135:
		case 136:
		case 222:
		case 245:
		case 247:
		case 248:
		case 262:
		case 264:
		case 266:
		case 290:
		case 328:
		case 346:
		case 370:
		case 454:
		case 473:
		case 474:
		case 475:
		case 477:
		case 551:
		case 576:
		case 577:
		case 618:
		case 636:
		case 657:
		case 668:
			canBreakPlayerDefense = true;
			break;
		case 316:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.5);
			break;
		case 619:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.25);
			break;
		case 582:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.5);
			break;
		case 508:
		case 580:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.75);
			npc.damage = (int)Math.Round((double)npc.damage * 0.75);
			npc.defDamage = npc.damage;
			npc.defense /= 2;
			npc.defDefense = npc.defense;
			break;
		case 69:
		case 509:
		case 581:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.75);
			npc.damage = (int)Math.Round((double)npc.damage * 0.75);
			npc.defDamage = npc.damage;
			npc.defense /= 2;
			npc.defDefense = npc.defense;
			break;
		case 513:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.5);
			npc.damage = (int)Math.Round((double)npc.damage * 0.75);
			npc.defDamage = npc.damage;
			break;
		case 514:
		case 515:
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.5);
			npc.damage = (int)Math.Round((double)npc.damage * 0.75);
			npc.defDamage = npc.damage;
			npc.defense /= 2;
			npc.defDefense = npc.defense;
			break;
		case 372:
		case 373:
			npc.width = (npc.height = 36);
			npc.chaseable = false;
			break;
		case 395:
			npc.width *= 2;
			npc.height *= 2;
			break;
		case 103:
			if (!Main.hardMode)
			{
				npc.damage = 40;
				npc.defDamage = npc.damage;
				npc.defense = 4;
				npc.defDefense = npc.defense;
			}
			break;
		case 265:
			npc.dontTakeDamage = true;
			break;
		case 371:
			if (CalamityWorld.death)
			{
				npc.lifeMax = 300;
			}
			break;
		}
		if (CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && !BossRushEvent.BossRushActive)
		{
			if (!NPC.downedMechBossAny)
			{
				if (CalamityNPCTypeSets.Destroyer.Contains(npc.type) || npc.type == 139 || CalamityNPCTypeSets.SkeletronPrime.Contains(npc.type) || npc.type == 126 || npc.type == 125)
				{
					double multiplier = (Main.expertMode ? 0.9 : 0.8);
					npc.lifeMax = (int)Math.Round((double)npc.lifeMax * multiplier);
					npc.damage = (int)Math.Round((double)npc.damage * multiplier);
					npc.defDamage = npc.damage;
				}
			}
			else if (((!NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (!NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (!NPC.downedMechBoss3 && !NPC.downedMechBoss1)) && (CalamityNPCTypeSets.Destroyer.Contains(npc.type) || npc.type == 139 || CalamityNPCTypeSets.SkeletronPrime.Contains(npc.type) || npc.type == 126 || npc.type == 125))
			{
				double multiplier2 = (Main.expertMode ? 0.95 : 0.9);
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * multiplier2);
				npc.damage = (int)Math.Round((double)npc.damage * multiplier2);
				npc.defDamage = npc.damage;
			}
		}
		if (!Main.hardMode)
		{
			if (npc.type == 254 || npc.type == 255 || npc.type == 257 || npc.type == 259 || npc.type == 258)
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 0.5);
				npc.damage = (int)Math.Round((double)npc.damage * 0.5);
				npc.defDamage = npc.damage;
			}
			if (npc.type == 261)
			{
				npc.damage = (int)Math.Round((double)npc.damage * 0.5);
				npc.defDamage = npc.damage;
			}
		}
		if (Main.hardMode && CalamityNPCSets.NerfDamageInHardmode[npc.type])
		{
			npc.damage = (int)Math.Round((double)npc.damage * 0.75);
			npc.defDamage = npc.damage;
		}
		if (DownedBossSystem.downedDoG)
		{
			if (CalamityNPCSets.IsBuffedPumpkinMoonEnemy[npc.type])
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 3.5);
				npc.damage += 30;
				npc.life = npc.lifeMax;
				npc.defDamage = npc.damage;
			}
			else if (CalamityNPCSets.IsBuffedFrostMoonEnemy[npc.type])
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 2.5);
				npc.damage += 30;
				npc.life = npc.lifeMax;
				npc.defDamage = npc.damage;
			}
			else if (CalamityNPCSets.IsBuffedSolarEclipseEnemy[npc.type])
			{
				npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 5.0);
				npc.damage += 30;
				npc.life = npc.lifeMax;
				npc.defDamage = npc.damage;
			}
		}
		if (NPC.downedMoonlord && CalamityNPCSets.IsBuffedDungeonEnemy[npc.type])
		{
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * 2.5);
			npc.damage += 30;
			npc.life = npc.lifeMax;
			npc.defDamage = npc.damage;
		}
	}

	private void EditGlobalCoinDrops(NPC npc)
	{
		npc.value = (int)((double)npc.value * 1.5);
		if (Main.expertMode)
		{
			npc.value = (int)((double)npc.value / 2.5);
			npc.value = (int)((double)npc.value * 1.5);
		}
	}

	public static void DrawGlowmask(NPC npc, SpriteBatch spriteBatch, Texture2D texture = null, bool invertedDirection = false, Vector2 offset = default(Vector2))
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Npc[npc.type].Value;
		}
		SpriteEffects effects = (SpriteEffects)((npc.spriteDirection != 1) ? ((!invertedDirection) ? 1 : 0) : (invertedDirection ? 1 : 0));
		Vector2 screenOffset = (npc.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition);
		spriteBatch.Draw(texture, npc.Center - screenOffset + offset, (Rectangle?)npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() * 0.5f, npc.scale, effects, 0f);
	}

	public static void DrawAfterimage(NPC npc, SpriteBatch spriteBatch, Color startingColor, Color endingColor, Texture2D texture = null, Func<NPC, int, float> rotationCalculation = null, bool directioning = false, bool invertedDirection = false)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		if (NPCID.Sets.TrailingMode[npc.type] != 1)
		{
			return;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if ((npc.spriteDirection == -1) & directioning)
		{
			spriteEffects = (SpriteEffects)1;
		}
		if (invertedDirection)
		{
			spriteEffects = (SpriteEffects)(spriteEffects ^ 1);
		}
		if (rotationCalculation == null)
		{
			rotationCalculation = (NPC nPC, int afterimageIndex) => nPC.rotation;
		}
		((Color)(ref endingColor)).A = 0;
		Color drawColor = npc.GetAlpha(startingColor);
		Texture2D npcTexture = texture ?? TextureAssets.Npc[npc.type].Value;
		Vector2 screenOffset = (npc.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition);
		for (int afterimageCounter = 1; afterimageCounter < NPCID.Sets.TrailCacheLength[npc.type]; afterimageCounter++)
		{
			if (!CalamityClientConfig.Instance.Afterimages)
			{
				break;
			}
			Color colorToDraw = Color.Lerp(drawColor, endingColor, (float)afterimageCounter / (float)NPCID.Sets.TrailCacheLength[npc.type]);
			colorToDraw *= (float)afterimageCounter / (float)NPCID.Sets.TrailCacheLength[npc.type];
			Vector2 imagePosition = npc.oldPos[afterimageCounter] + npc.Size / 2f - screenOffset + Vector2.UnitY * npc.gfxOffY;
			spriteBatch.Draw(npcTexture, imagePosition, (Rectangle?)npc.frame, colorToDraw, rotationCalculation(npc, afterimageCounter), npc.frame.Size() * 0.5f, npc.scale, spriteEffects, 0f);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
	{
		if (Main.netMode == 0 || numPlayers <= 1)
		{
			return;
		}
		int num;
		int num2;
		if (!npc.boss)
		{
			num = (NPCID.Sets.ShouldBeCountedAsBoss[npc.type] ? 1 : 0);
			if (num == 0)
			{
				num2 = (CalamityNPCSets.ScalesHealthLikeBoss[npc.type] ? 1 : 0);
				goto IL_0035;
			}
		}
		else
		{
			num = 1;
		}
		num2 = 1;
		goto IL_0035;
		IL_0035:
		bool scalesLikeBoss = (byte)num2 != 0;
		bool isCalamityNPC = npc.ModNPC != null && npc.ModNPC.Mod == CalamityMod.Instance;
		if (((uint)num | (scalesLikeBoss ? 1u : 0u)) != 0)
		{
			double adjustmentFactor = 1.0;
			switch (numPlayers)
			{
			case 2:
				adjustmentFactor = BalancingConstants.ExpertHealthScalingOverride_2Players / 1.35f;
				break;
			case 3:
				adjustmentFactor = BalancingConstants.ExpertHealthScalingOverride_3Players / 1.9166666f;
				break;
			}
			npc.life = (int)Math.Round((double)npc.life * adjustmentFactor);
		}
		else if (isCalamityNPC)
		{
			npc.lifeMax = (int)Math.Round((double)npc.lifeMax * numPlayers switch
			{
				1 => 1.0, 
				2 => 0.9, 
				3 => 0.82, 
				4 => 0.76, 
				5 => 0.71, 
				6 => 0.67, 
				_ => 0.64, 
			});
		}
	}

	public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
	{
		if (pacified)
		{
			return false;
		}
		if (target.Calamity().prismaticHelmet && !CalamityPlayer.areThereAnyDamnBosses && npc.lifeMax < 500)
		{
			return false;
		}
		return true;
	}

	public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
	{
		if (npc.ichor)
		{
			modifiers.Defense.Flat += 5f;
		}
		int defenseReduction = ((markedForDeath && DR <= 0f) ? MarkedforDeath.DefenseReduction : 0) + (wither ? RemsRevenge.WitherDefenseReduction : 0) + miscDefenseLoss;
		modifiers.ArmorPenetration += (float)defenseReduction;
		ApplyDR(npc, ref modifiers);
		if (CalamityWorld.revenge)
		{
			if (CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) && newAI[1] < 600f)
			{
				modifiers.FinalDamage *= 1f - (float)Math.Sqrt(MathHelper.Lerp(0f, 0.99f, MathHelper.Clamp(1f - newAI[1] / 600f, 0f, 1f)));
			}
			if (CalamityNPCTypeSets.Destroyer.Contains(npc.type) && newAI[1] < 600f)
			{
				modifiers.FinalDamage *= 1f - (float)Math.Sqrt(MathHelper.Lerp(0f, 0.99f, MathHelper.Clamp(1f - newAI[1] / 600f, 0f, 1f)));
			}
		}
		if (CalamityNPCTypeSets.AstrumDeus.Contains(npc.type))
		{
			float drTime = ((newAI[0] != 0f) ? 300f : 600f);
			if (newAI[1] < drTime)
			{
				modifiers.FinalDamage *= 1f - (float)Math.Sqrt(MathHelper.Lerp(0f, 0.99f, MathHelper.Clamp(1f - newAI[1] / drTime, 0f, 1f)));
			}
		}
	}

	private void ApplyDR(NPC npc, ref NPC.HitModifiers modifiers)
	{
		if (DR <= 0f && KillTime == 0)
		{
			return;
		}
		float finalMultiplier = 1f;
		float effectiveDR = (unbreakableDR ? DR : ApplyDRReduction(npc, DR));
		if (effectiveDR <= 0f)
		{
			effectiveDR = 0f;
		}
		bool enragedProvi = npc.type == ModContent.NPCType<global::CalamityMod.NPCs.Providence.Providence>() && !ProvUtils.StandardAI();
		if ((KillTime > 0 && killTimeTimer < KillTime && !BossRushEvent.BossRushActive) & enragedProvi)
		{
			float DRScalar = 10f;
			float extraDRLimit = (1f - DR) * DRScalar;
			float num = (float)npc.life / (float)npc.lifeMax;
			float killTimeRatio = (float)killTimeTimer / (float)KillTime;
			float extraDRScalar = num + killTimeRatio;
			if (extraDRScalar < 1f)
			{
				effectiveDR += extraDRLimit - extraDRLimit / (1f + (1f - extraDRScalar));
			}
		}
		finalMultiplier -= effectiveDR;
		modifiers.FinalDamage *= finalMultiplier;
	}

	private float ApplyDRReduction(NPC npc, float DR)
	{
		float calcDR = DR;
		if (markedForDeath)
		{
			calcDR *= 0.5f;
		}
		if (absorberAffliction)
		{
			calcDR *= 0.8f;
		}
		if (npc.Calamity().armorCrunch)
		{
			calcDR *= ArmorCrunch.MultiplicativeDamageReductionEnemy;
		}
		if (npc.Calamity().crumble)
		{
			calcDR *= Crumbling.MultiplicativeDamageReductionEnemy;
		}
		if (relicOfResilienceWeakness)
		{
			calcDR *= 0.5f;
		}
		return calcDR;
	}

	public bool IsArmored()
	{
		if (unbreakableDR)
		{
			return DR > 0.9f;
		}
		return false;
	}

	public override bool PreAI(NPC npc)
	{
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		if ((npc.type == 126 || npc.type == 125) && npc.ai[0] >= 2f)
		{
			VulnerableToCold = null;
			VulnerableToHeat = null;
			VulnerableToSickness = false;
			VulnerableToElectricity = true;
		}
		VulnerabilityHexFireDrawer?.Update();
		if (ManaBurnFireDrawer != null)
		{
			ManaBurnFireDrawer.LocalTimer = 0;
			float power = (float)npc.height / 100f;
			if (power > 2.75f)
			{
				power = 2.75f;
			}
			ManaBurnFireDrawer.RelativePower = power * MathHelper.Lerp(0.5f, 1.5f, MathHelper.Clamp(manaBurn / manaBurnPeak, 0f, 1f)) * playerManaBurnIntensity;
			ManaBurnFireDrawer.Update();
		}
		for (int i = 0; i < 256; i++)
		{
			if (dashImmunityTime[i] > 0)
			{
				dashImmunityTime[i]--;
			}
		}
		if (KillTime > 0 || npc.type == ModContent.NPCType<Draedon>())
		{
			if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, npc.Center) < 6400f)
			{
				Main.LocalPlayer.AddBuff(ModContent.BuffType<BossEffects>(), 2);
			}
			if (npc.type != ModContent.NPCType<Draedon>() && killTimeTimer < KillTime)
			{
				killTimeTimer++;
			}
		}
		if (npc.type == 488 || npc.type == ModContent.NPCType<SuperDummyNPC>())
		{
			npc.dontTakeDamage = CalamityPlayer.areThereAnyDamnBosses || (draedon != -1 && Main.npc[draedon].active);
		}
		if (CalamityNPCSets.DealsZeroContactDamage[npc.type] && (npc.type != 172 || !Main.zenithWorld))
		{
			npc.damage = 0;
		}
		if (BossRushEvent.BossRushActive && !npc.friendly && !npc.townNPC && !DoesNotDisappearInBossRush)
		{
			BossRushForceDespawnOtherNPCs(npc, base.Mod);
		}
		if (npc.type >= 583 && npc.type <= 585 && (npc.ai[2] < 2f || npc.ai[2] == 7f))
		{
			npc.TargetClosest();
			if (Main.player[npc.target].Calamity().fairyBoots)
			{
				NPCAimedTarget targetData = npc.GetTargetData();
				if (targetData.Type == NPCTargetType.Player && Main.player[npc.target].dead)
				{
					return true;
				}
				npc.ai[2] = 7f;
				npc.lavaImmune = true;
				npc.dontTakeDamage = true;
				npc.noTileCollide = true;
				npc.rarity = 0;
				if (Vector2.Distance(npc.Center, targetData.Center) > 1000f)
				{
					npc.Center = targetData.Center;
				}
				else if (Vector2.Distance(npc.Center, targetData.Center) > 80f)
				{
					Vector2 closestTargetPoint = Utils.CenteredRectangle(targetData.Center, new Vector2((float)(targetData.Width + 60), (float)(targetData.Height / 2))).ClosestPointInRect(npc.Center);
					Vector2 targetPointDir = npc.DirectionTo(closestTargetPoint) * (((Vector2)(ref targetData.Velocity)).Length() * 0.5f + 2f);
					float targetPointDist = npc.Distance(closestTargetPoint);
					if (targetPointDist > 225f)
					{
						targetPointDir *= 2f;
					}
					else if (targetPointDist > 120f)
					{
						targetPointDir *= 1.5f;
					}
					npc.velocity = Vector2.Lerp(npc.velocity, targetPointDir, 0.07f);
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC k = enumerator.Current;
					if (k != npc && k.aiStyle == 112 && Math.Abs(npc.position.X - k.position.X) + Math.Abs(npc.position.Y - k.position.Y) < (float)npc.width * 1.5f)
					{
						if (npc.position.Y < k.position.Y)
						{
							npc.velocity.Y -= 0.05f;
						}
						else
						{
							npc.velocity.Y += 0.05f;
						}
					}
				}
				npc.direction = ((npc.velocity.X >= 0f) ? 1 : (-1));
				npc.spriteDirection = -npc.direction;
				Color dustLerpColor1 = Color.HotPink;
				Color dustLerpColor2 = Color.LightPink;
				if (npc.type == 584)
				{
					dustLerpColor1 = Color.LimeGreen;
					dustLerpColor2 = Color.LightSeaGreen;
				}
				if (npc.type == 585)
				{
					dustLerpColor1 = Color.RoyalBlue;
					dustLerpColor2 = Color.LightBlue;
				}
				if ((int)Main.timeForVisualEffects % 2 == 0)
				{
					npc.position += npc.netOffset;
					Dust dust = Dust.NewDustDirect(npc.Center - new Vector2(2f), 8, 8, 278, 0f, 0f, 200, Color.Lerp(dustLerpColor1, dustLerpColor2, Main.rand.NextFloat()), 0.65f);
					dust.velocity *= 0f;
					dust.velocity += npc.velocity * 0.3f;
					dust.noGravity = true;
					dust.noLight = true;
					npc.position -= npc.netOffset;
				}
				Lighting.AddLight(npc.Center, ((Color)(ref dustLerpColor1)).ToVector3() * 0.7f);
				if (!Main.dedServ)
				{
					Player localPlayer = Main.LocalPlayer;
					if (!localPlayer.dead)
					{
						Rectangle hitboxForBestiaryNearbyCheck = localPlayer.HitboxForBestiaryNearbyCheck;
						if (((Rectangle)(ref hitboxForBestiaryNearbyCheck)).Intersects(npc.Hitbox))
						{
							AchievementsHelper.HandleSpecialEvent(localPlayer, 22);
						}
					}
				}
				return false;
			}
		}
		return true;
	}

	private void BossRushForceDespawnOtherNPCs(NPC npc, Mod mod)
	{
		if (BossRushEvent.BossRushStage < BossRushEvent.Bosses.Count && !BossRushEvent.Bosses[BossRushEvent.BossRushStage].HostileNPCsToNotDelete.Contains(npc.type))
		{
			npc.active = false;
			npc.netUpdate = true;
		}
	}

	public override void PostAI(NPC npc)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0845: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1007: Unknown result type (might be due to invalid IL or missing references)
		//IL_100c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1059: Unknown result type (might be due to invalid IL or missing references)
		//IL_1063: Unknown result type (might be due to invalid IL or missing references)
		//IL_1072: Unknown result type (might be due to invalid IL or missing references)
		//IL_1077: Unknown result type (might be due to invalid IL or missing references)
		//IL_107c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1241: Unknown result type (might be due to invalid IL or missing references)
		//IL_1252: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1302: Unknown result type (might be due to invalid IL or missing references)
		//IL_1315: Unknown result type (might be due to invalid IL or missing references)
		//IL_133e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1343: Unknown result type (might be due to invalid IL or missing references)
		//IL_1345: Unknown result type (might be due to invalid IL or missing references)
		//IL_1394: Unknown result type (might be due to invalid IL or missing references)
		//IL_1399: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1402: Unknown result type (might be due to invalid IL or missing references)
		//IL_1407: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1465: Unknown result type (might be due to invalid IL or missing references)
		Color newColor;
		if (npc.type == 10 || npc.type == 95 || npc.type == 7 || npc.type == 98 || npc.type == 513 || npc.type == 39 || npc.type == 510 || npc.type == 13 || npc.type == 134)
		{
			if (Framing.GetTileSafely(npc.Center.ToTileCoordinates()).HasUnactuatedTile && npc.Distance(Main.player[npc.target].Center) < 800f && Main.rand.NextBool())
			{
				Vector2 position = npc.position;
				int width = npc.width;
				int height = npc.height;
				newColor = default(Color);
				Dust dust = Dust.NewDustDirect(position, width, height, 204, 0f, 0f, 150, newColor, 0.3f);
				dust.fadeIn = 0.75f;
				dust.velocity *= 0.1f;
				dust.noLight = true;
			}
		}
		else if (npc.type == 43 || npc.type == 56 || npc.type == 175)
		{
			if (Framing.GetTileSafely(npc.Center.ToTileCoordinates()).HasUnactuatedTile && npc.Distance(Main.player[npc.target].Center) < 800f && Main.rand.NextBool(10))
			{
				Vector2 position2 = npc.position;
				int width2 = npc.width;
				int height2 = npc.height;
				newColor = default(Color);
				Dust.NewDustDirect(position2, width2, height2, 44, 0f, 0f, 250, newColor, 0.4f).fadeIn = 0.7f;
			}
		}
		else if (npc.type == 101)
		{
			if (Framing.GetTileSafely(npc.Center.ToTileCoordinates()).HasUnactuatedTile && npc.Distance(Main.player[npc.target].Center) < 800f)
			{
				if (Main.rand.NextBool(5))
				{
					Vector2 position3 = npc.position;
					int width3 = npc.width;
					int height3 = npc.height;
					newColor = default(Color);
					Dust.NewDustDirect(position3, width3, height3, 75, 0f, 0f, 100, newColor, 1.5f).noGravity = true;
				}
			}
			else if (npc.localAI[0] > (CalamityWorld.revenge ? 90f : 120f) - 30f)
			{
				Vector2 position4 = npc.Center + npc.SafeDirectionTo(Main.player[npc.target].Center, -Vector2.UnitY) * 20f + Main.rand.NextVector2CircularEdge(5f, 5f);
				newColor = default(Color);
				Dust dust2 = Dust.NewDustDirect(position4, 1, 1, 75, 0f, 0f, 100, newColor, 3f);
				dust2.noGravity = true;
				dust2.velocity *= 0f;
			}
			if (Collision.SolidCollision(npc.position, npc.width, npc.height) || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
			{
				npc.localAI[0] = 0f;
			}
		}
		else if (npc.type == 268)
		{
			if (npc.ai[3] > (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)) - 30f)
			{
				Vector2 position5 = new Vector2(npc.Center.X - 4f, npc.position.Y + (float)npc.height * 0.7f) + Main.rand.NextVector2CircularEdge(2f, 2f);
				newColor = default(Color);
				Dust dust3 = Dust.NewDustDirect(position5, 1, 1, 170, 0f, 0f, 100, newColor, 1.5f);
				dust3.noGravity = true;
				dust3.velocity *= 0f;
			}
			if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].head))
			{
				npc.ai[3] = 0f;
			}
		}
		if (warbannerBurnTimer > 0)
		{
			warbannerBurnTimer--;
		}
		if (warbannerBurnTimer == 0 && warbannerBurnMarked)
		{
			warbannerBurnTime = 0;
			warbannerBurnDamage = 0;
			warbannerBurnMarked = false;
			warbannerBurnStacks = 0;
		}
		if (warbannerBurnTimer <= 60)
		{
			warbannerBurnStacks = (int)((float)warbannerBurnStacks * 0.9f);
		}
		if (warbannerBurnMarked)
		{
			int maxStacks = 300;
			int fastestBurnRate = 2;
			int slowestBurnRate = 15;
			float burnPower = Utils.Remap(warbannerBurnStacks, 0f, maxStacks, slowestBurnRate, fastestBurnRate);
			float sizeBonus = 1f + Utils.GetLerpValue(0f, 170f, Math.Max((float)npc.Hitbox.Width / 2f, (float)npc.Hitbox.Height / 2f));
			if (!warbannerBurnHideEffects)
			{
				Vector2 center = npc.Center;
				newColor = Color.Gold;
				Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.3f * warbannerBurnIntensity);
			}
			if (warbannerBurnStacks == maxStacks && !warbannerBurnHideEffects)
			{
				for (int i = 0; i < 15; i++)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(npc.Center, Utils.RotatedByRandom(new Vector2(13f, 13f), 100.0) * Main.rand.NextFloat(0.4f, 1f), affectedByGravity: true, 45, 0.85f, Main.rand.NextBool() ? Color.Goldenrod : Color.Orange));
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceBurn");
				style.Volume = 0.7f;
				style.Pitch = 0.7f;
				SoundEngine.PlaySound(in style, npc.Center);
				warbannerBurnStacks++;
			}
			if (warbannerBurnIntensity > 2.5f && npc.CanBeMoved(ignoreKBImmune: true))
			{
				npc.velocity *= 1f - 0.25f * Utils.GetLerpValue(2.5f, 3f, warbannerBurnIntensity);
				if (((Vector2)(ref npc.velocity)).Length() > 5f && warbannerBurnIntensity > 2.85f)
				{
					npc.velocity = -npc.velocity * 0.7f;
				}
			}
			if (warbannerBurnTime == 0)
			{
				if (!warbannerBurnHideEffects)
				{
					int particleLevel = (int)(MathHelper.Clamp(((float)slowestBurnRate - burnPower) * 0.15f, 1f, 2f) * warbannerBurnIntensity);
					for (int d = 0; d < particleLevel; d++)
					{
						Color color = (Main.rand.NextBool() ? Color.Goldenrod : Color.Lerp(Color.OrangeRed, Color.Orange, Main.rand.NextFloat(0f, 1f)));
						Vector2 sparkPos = npc.Center - warbannerBurnDirection * 220f * Utils.GetLerpValue(0f, 200f, Math.Max((float)npc.Hitbox.Width / 2f, (float)npc.Hitbox.Height / 2f));
						float velAdjust = Main.rand.NextFloat(2f, 7f) * warbannerBurnIntensity * sizeBonus;
						Vector2 endVel = warbannerBurnDirection * velAdjust;
						Vector2 startVel = (warbannerBurnDirection * velAdjust).RotatedByRandom(0.6f * warbannerBurnIntensity);
						GeneralParticleHandler.SpawnParticle(new VelChangingSpark(sparkPos, startVel, endVel, "CalamityMod/Particles/SmallBloom", Main.rand.Next(18, 23), Main.rand.NextFloat(0.1f, 0.25f) * sizeBonus, color * 0.75f, new Vector2(0.7f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.45f));
						if (Main.rand.NextBool())
						{
							int type = ModContent.DustType<LightDust>();
							Vector2? velocity = startVel;
							float scale = Main.rand.NextFloat(0.5f, 0.9f) * Math.Min(sizeBonus, 1.3f);
							newColor = default(Color);
							Dust dust4 = Dust.NewDustPerfect(sparkPos, type, velocity, 0, newColor, scale);
							dust4.noGravity = true;
							dust4.color = color;
							dust4.noLightEmittence = true;
						}
					}
				}
				Projectile.NewProjectileDirect(Main.LocalPlayer.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<WarbannerDamage>(), (int)((float)warbannerBurnDamage * warbannerBurnIntensity), 0f, Main.myPlayer, npc.whoAmI).ArmorPenetration = 50;
				warbannerBurnTime = (int)(burnPower + (3f - warbannerBurnIntensity) * 4f);
			}
			warbannerBurnTime--;
		}
		if (veriumDoomTimer > 0)
		{
			veriumDoomTimer--;
		}
		if (veriumDoomTimer == 0 && veriumDoomMarked)
		{
			for (int j = 0; j < 14 + veriumDoomStacks; j++)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(npc.Center, new Vector2(Main.rand.NextFloat(-9f, 9f), Main.rand.NextFloat(-9f, 9f)), affectedByGravity: false, 45, 0.9f, Main.rand.NextBool() ? Color.Cyan : Color.SkyBlue));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenHit", 3);
			style.Volume = 0.6f;
			SoundEngine.PlaySound(in style, npc.Center);
			Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), 100 + 15 * veriumDoomStacks, 0f, Main.myPlayer, npc.whoAmI);
			veriumDoomMarked = false;
			veriumDoomStacks = 0;
		}
		if (shocked > 0)
		{
			Player player = Main.LocalPlayer;
			int frequency = 15;
			if (player.miscCounter % frequency == 0)
			{
				int sDamage = 10;
				Vector2 velocity2 = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * 5f;
				Projectile projectile = Projectile.NewProjectileDirect(npc.GetSource_FromThis(), npc.Center, velocity2, ModContent.ProjectileType<GenericElectricSpark>(), sDamage, 0f, player.whoAmI, 0f, 1f);
				projectile.timeLeft = 120;
				projectile.penetrate = 3;
			}
		}
		if (hyperiusMarked)
		{
			if (hyperiusFxTimer < 20)
			{
				hyperiusFxTimer++;
			}
			else if (hyperiusFxTimer > 20)
			{
				hyperiusFxTimer = (int)Utils.Lerp(hyperiusFxTimer, 20.0, 0.20000000298023224);
			}
			float num = (float)hyperiusDamage / (float)npc.lifeMax;
			int overflowSpeed = (int)Utils.Remap(num, 0.07f, 0.35f, 1f, 34f);
			if (num > 0.07f)
			{
				if (npc.defense < 1000 && !unbreakableDR && DR <= 0.9f && !npc.dontTakeDamage && !npc.immortal)
				{
					hyperiusOverflowTimer -= overflowSpeed;
				}
				if (hyperiusOverflowTimer <= 0)
				{
					hyperiusOverflowTimer = hyperiusOverflowTime;
					float damagePercent = 0.07f;
					int damage = Math.Max((int)((float)hyperiusDamage * damagePercent), 1);
					hyperiusDamage -= damage;
					Projectile.NewProjectileDirect(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<HyperiusBleed>(), damage, 0f, -1, npc.whoAmI).DamageType = DamageClass.Ranged;
					if (hyperiusFxTimer >= 20)
					{
						hyperiusFxTimer = 35;
					}
					if (hyperiusDamage <= 0)
					{
						hyperiusDamage = 0;
						hyperiusMarked = false;
					}
				}
			}
		}
		else if (hyperiusFxTimer > 0)
		{
			hyperiusFxTimer--;
		}
		if (laserBurnTimer > 0)
		{
			laserBurnTimer--;
		}
		if ((laserBurnTimer <= 0 || (float)laserBurnDamage >= (float)npc.life * 1.5f) && laserBurnMarked && laserBurnType > 0)
		{
			if (laserBurnType == 1)
			{
				Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), laserBurnDamage, 0f, Main.myPlayer, npc.whoAmI);
			}
			if (laserBurnType == 2)
			{
				Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), 70 + 20 * laserBurnStacks, 0f, Main.myPlayer, npc.whoAmI);
			}
			for (int k = 0; k < (int)(7f + (float)laserBurnStacks * 0.4f); k++)
			{
				Vector2 partVel = (new Vector2(10f) * ((float)laserBurnStacks * 0.025f)).RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(0.2f, 1f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(npc.Center, partVel, "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 12, Main.rand.NextFloat(0.02f, 0.03f), ArsenalEffects.ArsenalLaserColor * 0.8f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1f));
				Vector2 dustVel = (Vector2.UnitX * 5f * ((float)laserBurnStacks * 0.05f)).RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(0.85f, 1f);
				Dust.NewDustPerfect(npc.Center, ArsenalEffects.ArsenalLaserDust, dustVel, 0, Color.Red, Main.rand.NextFloat(0.65f, 0.8f)).noGravity = true;
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LaserBurn");
			style.Volume = 0.6f;
			style.Pitch = Main.rand.NextFloat(-0.15f, 0.15f);
			SoundEngine.PlaySound(in style, npc.Center);
			laserBurnMarked = false;
			laserBurnStacks = 0;
			laserBurnTimer = 0;
			laserBurnDamage = 0;
		}
		if (npc.type == 222 && !CalamityWorld.revenge && !BossRushEvent.BossRushActive)
		{
			return;
		}
		if (demonSwordImpales > 0 && npc.CanBeMoved(ignoreKBImmune: true))
		{
			npc.velocity *= Utils.Remap(demonSwordImpales, 1f, 5f, 0.95f, 0.3f);
			if (impalePacketTimer > 30)
			{
				npc.SyncMotionToServer();
				impalePacketTimer = 0;
			}
		}
		impalePacketTimer++;
		if (debuffResistanceTimer <= 0 || debuffResistanceTimer > 1800)
		{
			if (vulnerabilityHex)
			{
				npc.velocity = Vector2.Clamp(npc.velocity, new Vector2(-5f), new Vector2(5f, 10f));
			}
			float velocitySlownessFactor = 1f;
			if (temporalSadness)
			{
				velocitySlownessFactor += 0.2f;
			}
			if (timeDistortion)
			{
				velocitySlownessFactor += 0.15f;
			}
			if (webbed)
			{
				velocitySlownessFactor += 0.15f;
			}
			if (glacialState)
			{
				float baseSlownessFactor = 0.1f;
				if (VulnerableToCold.HasValue)
				{
					baseSlownessFactor = ((!VulnerableToCold.Value) ? 0.025f : 0.4f);
				}
				velocitySlownessFactor += baseSlownessFactor;
			}
			if (pearlAura)
			{
				velocitySlownessFactor += 0.1f;
			}
			if (eutrophication)
			{
				float baseSlownessFactor2 = 0.05f;
				if (VulnerableToWater.HasValue)
				{
					baseSlownessFactor2 = ((!VulnerableToWater.Value) ? 0.0125f : 0.2f);
				}
				velocitySlownessFactor += baseSlownessFactor2;
			}
			if (galvanicCorrosion)
			{
				float baseSlownessFactor3 = 0.05f;
				if (VulnerableToElectricity.HasValue)
				{
					baseSlownessFactor3 = ((!VulnerableToElectricity.Value) ? 0.0125f : 0.2f);
				}
				velocitySlownessFactor += baseSlownessFactor3;
			}
			if (vaporfied)
			{
				velocitySlownessFactor += 0.05f;
			}
			velocitySlownessFactor = 1f / velocitySlownessFactor;
			npc.velocity *= velocitySlownessFactor;
		}
		if (((!NPCID.Sets.ActsLikeTownNPC[npc.type] && !npc.townNPC) || npc.dontTakeDamage) && npc.type != ModContent.NPCType<SuperDummyNPC>())
		{
			return;
		}
		int auricOreID = ModContent.TileType<AuricOre>();
		int auricRepulserID = ModContent.TileType<AuricRepulserPanelTile>();
		List<Point> EdgeTiles = new List<Point>();
		int extraDist = (int)(8f * ((Vector2)(ref npc.velocity)).Length() / 6f) + 1;
		int left = (int)npc.position.X - extraDist;
		int up = (int)npc.position.Y - extraDist;
		int right = (int)npc.Right.X + extraDist;
		int down = (int)npc.Bottom.Y + extraDist;
		if (left % 16 == 0)
		{
			left--;
		}
		if (up % 16 == 0)
		{
			up--;
		}
		if (right % 16 == 0)
		{
			right++;
		}
		if (down % 16 == 0)
		{
			down++;
		}
		int width4 = right / 16 - left / 16;
		int height4 = down / 16 - up / 16;
		left /= 16;
		up /= 16;
		for (int l = left; l <= left + width4; l++)
		{
			EdgeTiles.Add(new Point(l, up));
			EdgeTiles.Add(new Point(l, up + height4));
		}
		for (int m = up; m < up + height4; m++)
		{
			EdgeTiles.Add(new Point(left, m));
			EdgeTiles.Add(new Point(left + width4, m));
		}
		foreach (Point touchedTile in EdgeTiles)
		{
			Tile tile = Framing.GetTileSafely(touchedTile);
			if (tile.HasTile && tile.HasUnactuatedTile && (tile.TileType == auricOreID || tile.TileType == auricRepulserID))
			{
				if (tile.TileType == auricOreID)
				{
					AuricOre.Animate = true;
				}
				Vector2 yeetVec = Vector2.Normalize(npc.Center - touchedTile.ToWorldCoordinates());
				npc.velocity += yeetVec * 20f;
				float clampedSpeed = MathHelper.Clamp(((Vector2)(ref npc.velocity)).Length(), -40f, 40f);
				npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero) * clampedSpeed;
				if (tile.TileType == auricOreID)
				{
					npc.SimpleStrikeNPC((int)((float)npc.lifeMax * 0.2f), 0);
					npc.AddBuff(ModContent.BuffType<AuricRebuke>(), 120);
				}
				SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/TeslaShoot1"), npc.Center);
				break;
			}
		}
	}

	public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage <= 0)
		{
			return;
		}
		if (target.Calamity().sulphurSet)
		{
			npc.AddBuff(20, 60);
		}
		if (target.Transformation().Type == ModContent.ItemType<Popo>() && (npc.type == 62 || npc.type == 66 || npc.type == 156))
		{
			target.AddBuff(ModContent.BuffType<PopoNoselessBuff>(), 36000);
		}
		switch (npc.type)
		{
		case 7:
		case 181:
			target.AddBuff(33, 180);
			break;
		case 197:
			if (Main.rand.NextBool(6))
			{
				target.AddBuff(36, 1800);
			}
			break;
		case 137:
			if (Main.rand.NextBool(14))
			{
				target.AddBuff(31, 180);
			}
			break;
		case 58:
			target.AddBuff(30, 180);
			break;
		case 157:
		case 241:
			target.AddBuff(30, 300);
			break;
		case 472:
			target.AddBuff(ModContent.BuffType<Shadowflame>(), 120);
			break;
		case 30:
			if (Main.hardMode || CalamityPlayer.areThereAnyDamnBosses)
			{
				target.AddBuff(ModContent.BuffType<Shadowflame>(), 120);
			}
			break;
		case 245:
			if (CalamityWorld.revenge)
			{
				target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 480);
			}
			break;
		case 246:
		case 247:
		case 248:
		case 249:
			if (CalamityWorld.revenge)
			{
				target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 240);
			}
			break;
		case 618:
			target.AddBuff(ModContent.BuffType<BurningBlood>(), 300);
			break;
		case 620:
		case 621:
			target.AddBuff(ModContent.BuffType<BurningBlood>(), 180);
			break;
		case 60:
			if (Main.expertMode)
			{
				target.AddBuff(24, 120);
			}
			break;
		case 151:
			target.AddBuff(24, 300);
			break;
		case 172:
			if (Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<MiracleBlight>(), 600);
			}
			break;
		}
		if ((npc.type == 266 || npc.type == 267) && Main.zenithWorld)
		{
			int buffType = Main.rand.Next(BuffLoader.BuffCount);
			target.AddBuff(buffType, Main.rand.Next(300, 601));
		}
	}

	public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
	{
		if (target.ModNPC is SunkenSeaNPC ssnpc)
		{
			ssnpc.OnHitByNPC(npc);
		}
	}

	public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
	{
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		int num = 15;
		List<int> list = new List<int>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<int> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = 65;
		num2++;
		span[num2] = 370;
		num2++;
		span[num2] = 372;
		num2++;
		span[num2] = 373;
		num2++;
		span[num2] = 542;
		num2++;
		span[num2] = 543;
		num2++;
		span[num2] = 544;
		num2++;
		span[num2] = 545;
		num2++;
		span[num2] = 620;
		num2++;
		span[num2] = ModContent.NPCType<FusionFeeder>();
		num2++;
		span[num2] = ModContent.NPCType<global::CalamityMod.NPCs.GreatSandShark.GreatSandShark>();
		num2++;
		span[num2] = ModContent.NPCType<Mauler>();
		num2++;
		span[num2] = ModContent.NPCType<global::CalamityMod.NPCs.OldDuke.OldDuke>();
		num2++;
		span[num2] = ModContent.NPCType<SulphurousSharkron>();
		num2++;
		span[num2] = ModContent.NPCType<ReaperShark>();
		if (list.Contains(npc.type) && target.name == "Rebecca" && Main.zenithWorld)
		{
			SoundEngine.PlaySound(in AresGaussNuke.NukeExplosionSound, target.Center);
			Main.LocalPlayer.SetScreenshake(12f);
			target.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Rebecca").ToNetworkText(target.name)), 1000.0, 0);
			modifiers.SourceDamage *= (float)target.statLifeMax2 * Main.rand.NextFloat(3f, 6f);
			if (Main.netMode != 1)
			{
				Projectile projectile = Projectile.NewProjectileDirect(npc.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ScorpioLargeRocket>(), 9999, 0f, Main.myPlayer, 4458f, 0.01f);
				projectile.friendly = false;
				projectile.hostile = true;
				projectile.timeLeft = 5;
			}
		}
	}

	public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (player.Calamity().camper && !player.StandingStill())
		{
			modifiers.SourceDamage *= 0.5f;
		}
		if (IsArmored())
		{
			modifiers.HideCombatText();
		}
		if (item.CountsAsClass<MeleeDamageClass>() && item.type != ModContent.ItemType<InfernaCutter>())
		{
			float damageMult = 1f;
			if (npc.type == ModContent.NPCType<global::CalamityMod.NPCs.Crabulon.Crabulon>())
			{
				damageMult = 0.8f;
			}
			else if (CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) || npc.type == 267 || npc.type == ModContent.NPCType<global::CalamityMod.NPCs.AstrumAureus.AstrumAureus>())
			{
				damageMult = 0.75f;
			}
			else if (CalamityNPCTypeSets.Perforators.Contains(npc.type) || CalamityNPCTypeSets.AquaticScourge.Contains(npc.type) || CalamityNPCTypeSets.Destroyer.Contains(npc.type) || CalamityNPCTypeSets.Ravager.Contains(npc.type) || CalamityNPCTypeSets.AstrumDeus.Contains(npc.type) || CalamityNPCTypeSets.StormWeaver.Contains(npc.type) || npc.type == ModContent.NPCType<ProfanedRocks>() || npc.type == ModContent.NPCType<DarkEnergy>())
			{
				damageMult = 0.5f;
			}
			else if (CalamityNPCTypeSets.Thanatos.Contains(npc.type))
			{
				damageMult = 0.35f;
			}
			modifiers.SourceDamage *= damageMult;
		}
	}

	public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (IsArmored())
		{
			modifiers.HideCombatText();
		}
		if (projectile.type == 12 && projectile.damage >= 1000 && (npc.type == ModContent.NPCType<PerforatorCyst>() || npc.type == ModContent.NPCType<HiveTumor>() || npc.type == ModContent.NPCType<LeviathanStart>()))
		{
			modifiers.SourceDamage *= 0f;
		}
		CalamityGlobalProjectile cgp = projectile.Calamity();
		if (cgp.supercritHits != 0)
		{
			cgp.supercritHits--;
			float critOver100 = (projectile.ContinuouslyUpdateDamageStats ? player.GetCritChance(projectile.DamageType) : ((float)projectile.CritChance)) - 100f;
			if (critOver100 > 0f)
			{
				int supercritLayers = (int)(critOver100 / 100f);
				float lastLayerCritChance = critOver100 % 100f;
				if (Main.rand.NextFloat(100f) <= lastLayerCritChance)
				{
					supercritLayers++;
				}
				modifiers.CritDamage += (float)supercritLayers;
			}
		}
		modifiers.CritDamage += cgp.bonusCritDamage;
		if (modPlayer.spiritOrigin && projectile.CountsAsClass<RangedDamageClass>())
		{
			int bullseyeType = ModContent.ProjectileType<SpiritOriginBullseye>();
			Projectile bullseye = null;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == bullseyeType && p.owner == player.whoAmI && npc.whoAmI == (int)p.ai[0])
				{
					bullseye = p;
					break;
				}
			}
			bool acceptableVelocity = projectile.velocity != Vector2.Zero;
			bool acceptableHitbox = projectile.width <= 36 || projectile.height <= 36;
			if ((bullseye != null) & acceptableVelocity & acceptableHitbox)
			{
				float bullseyeRadius = (npc.IsABoss() ? 18f : 8f);
				Vector2 normVelocity = projectile.velocity.SafeNormalize(Vector2.UnitY);
				Vector2 backedUpPosition = projectile.Center - 160f * normVelocity;
				Vector2 directionToBullseyeCenter = (bullseye.Center - backedUpPosition).SafeNormalize(Vector2.UnitY);
				Vector2 perp = directionToBullseyeCenter.RotatedBy(1.5707963705062866);
				Vector2 comparisonPointOne = bullseye.Center + perp * 2f * bullseyeRadius;
				Vector2 val = bullseye.Center - perp * 2f * bullseyeRadius;
				Vector2 dirToPointOne = (comparisonPointOne - backedUpPosition).SafeNormalize(-Vector2.UnitX);
				Vector2 dirToPointTwo = (val - backedUpPosition).SafeNormalize(Vector2.UnitX);
				float dotCenter = Vector2.Dot(normVelocity, directionToBullseyeCenter);
				float dotOne = Vector2.Dot(normVelocity, dirToPointOne);
				float dotTwo = Vector2.Dot(normVelocity, dirToPointTwo);
				if (dotCenter > dotOne && dotCenter > dotTwo)
				{
					modPlayer.spiritOriginCritBoost += player.HeldItem.useTime;
					if (bullseye.ai[2] == 0f)
					{
						bullseye.timeLeft = 90;
						bullseye.ai[2] = 1f;
					}
					if (Main.rand.NextBool(5))
					{
						int randomStarAmount = Main.rand.Next(3, 6);
						float randomCircleRotation = Main.rand.NextFloat((float)Math.PI * 2f);
						for (int i = 0; i < randomStarAmount; i++)
						{
							GeneralParticleHandler.SpawnParticle(new FancyStars(bullseye.Center, Main.rand.NextFloat((float)Math.PI * 2f) * (float)Main.rand.NextBool().ToDirectionInt(), Main.rand.NextFloat(0.42f, 0.63f), ((float)Math.PI * 2f / (float)randomStarAmount * (float)i).ToRotationVector2().RotatedBy(randomCircleRotation).RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(7f, 12f), Main.rand.NextFloat(0.1f, 0.5f), 55, new Color(Main.rand.Next(256), Main.rand.Next(256), Main.rand.Next(256)) * 1.2f));
						}
					}
					bullseye.netUpdate = true;
				}
			}
		}
		if (!projectile.npcProj && !projectile.trap)
		{
			if (projectile.CountsAsClass<RangedDamageClass>() && modPlayer.plagueReaper && plague)
			{
				modifiers.SourceDamage *= PlagueReaperMask.SetBonusPlaguedRangedDamageMult;
			}
			if (trueVulnerabilityHex)
			{
				modifiers.SourceDamage *= ((projectile.type == ModContent.ProjectileType<DirectStrike>() && projectile.ai[1] == 255f) ? 2.5f : 1.15f);
			}
		}
		BalancingChangesManager.ApplyFromProjectile(npc, ref modifiers, projectile);
		if (CalamityProjectileSets.ResistedExplosiveProjectile[projectile.type])
		{
			bool hasResist = CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) && !Main.expertMode;
			if (npc.type == 267 || CalamityNPCTypeSets.DesertScourge.Contains(npc.type) || CalamityNPCTypeSets.Perforators.Contains(npc.type))
			{
				hasResist = true;
			}
			if (hasResist)
			{
				modifiers.SourceDamage *= 0.33f;
			}
		}
		if (modPlayer.camper && !player.StandingStill())
		{
			modifiers.SourceDamage *= 0.5f;
		}
		if ((projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type]) && (player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfDeliveranceSpear>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfConvergenceCrystal>()] > 0 || (player.Calamity().rOfResilienceCooldown == 0 && player.HeldItem.type == ModContent.ItemType<RelicOfResilience>())))
		{
			modifiers.SourceDamage *= 0.1f;
		}
		if (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type])
		{
			EditSummonTagDamage(projectile, npc, ref modifiers);
		}
	}

	public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damagedone)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type])
		{
			SummonTagOnHitEffects(npc, projectile, hit, damagedone);
		}
		if (IsArmored())
		{
			CombatText.NewText(npc.Hitbox, Color.Gray, damagedone, hit.Crit);
		}
		if (projectile.type == ModContent.ProjectileType<HyperiusDamage>() || projectile.type == ModContent.ProjectileType<HyperiusBleed>())
		{
			float rate = Main.GlobalTimeWrappedHourly * 3f;
			int num = 5;
			List<Color> list = new List<Color>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<Color> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = Color.Yellow;
			num2++;
			span[num2] = Color.Magenta;
			num2++;
			span[num2] = Color.Red;
			num2++;
			span[num2] = Color.Cyan;
			num2++;
			span[num2] = Color.Lime;
			List<Color> eColors = list;
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			Color usedColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
			CombatText.NewText(npc.Hitbox, usedColor, damagedone, hit.Crit, dot: true);
		}
	}

	private void EditSummonTagDamage(Projectile proj, NPC npc, ref NPC.HitModifiers modifiers)
	{
		if (proj.npcProj || proj.trap || proj.owner == -1)
		{
			return;
		}
		Player player = Main.player[proj.owner];
		CalamityPlayer modPlayer = player.Calamity();
		float critChance = modPlayer.bonusCritTag;
		float TagDamageMult = ProjectileID.Sets.SummonTagDamageMultiplier[proj.type];
		TagDamageMult += modPlayer.bonusMultTag;
		modifiers.FlatBonusDamage += (float)modPlayer.bonusFlatTag;
		for (int i = 0; i < NPC.maxBuffs; i++)
		{
			if (npc.buffTime[i] >= 1)
			{
				int type = npc.buffType[i];
				if (CalamityBuffSets.SummonTagDebuff.TryGetValue(type, out var tag))
				{
					tag.TagModifyHitEffects(proj, npc, ref modifiers, ref TagDamageMult, ref critChance);
				}
			}
		}
		if (proj.type == 688 || proj.type == 689 || proj.type == 690)
		{
			if (player.setMonkT3)
			{
				critChance += 0.25f;
			}
			else if (player.setMonkT2)
			{
				critChance += 0.166f;
			}
		}
		if (modPlayer.forceSummonTagCrit && (!modPlayer.forceSummonTagMultiplicative || !Main.rand.NextBool()))
		{
			critChance += modifiers.ScalingBonusDamage.Value;
			modifiers.ScalingBonusDamage += 0f - modifiers.ScalingBonusDamage.Value;
		}
		else if (modPlayer.forceSummonTagMultiplicative)
		{
			modifiers.ScalingBonusDamage += critChance;
			critChance = 0f;
		}
		if (Main.rand.NextFloat() < critChance)
		{
			modifiers.SetCrit();
		}
		else
		{
			modifiers.DisableCrit();
		}
	}

	private void SummonTagOnHitEffects(NPC npc, Projectile projectile, NPC.HitInfo hit, int damagedone)
	{
		if (projectile.npcProj || projectile.trap || projectile.owner == -1)
		{
			return;
		}
		_ = Main.player[projectile.owner];
		for (int i = 0; i < NPC.maxBuffs; i++)
		{
			if (npc.buffTime[i] >= 1)
			{
				int type = npc.buffType[i];
				if (CalamityBuffSets.SummonTagDebuff.TryGetValue(type, out var tag))
				{
					tag.TagOnHit(npc, projectile, hit, damagedone);
				}
			}
		}
	}

	public override bool CheckDead(NPC npc)
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		if (npc.lifeMax > 1000 && npc.type != 288 && npc.type != ModContent.NPCType<PhantomSpirit>() && npc.type != ModContent.NPCType<PhantomSpiritS>() && npc.type != ModContent.NPCType<PhantomSpiritM>() && npc.type != ModContent.NPCType<PhantomSpiritL>() && npc.value > 0f && !npc.boss && npc.HasPlayerTarget && NPC.downedMoonlord && Main.player[npc.target].ZoneDungeon)
		{
			int baseValue = (Main.expertMode ? 4 : 6);
			if (Main.player[npc.target].RollLuck(baseValue) == 0 && Main.wallDungeon[Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].WallType])
			{
				int randomType = Utils.SelectRandom<int>(Main.rand, ModContent.NPCType<PhantomSpirit>(), ModContent.NPCType<PhantomSpiritS>(), ModContent.NPCType<PhantomSpiritM>(), ModContent.NPCType<PhantomSpiritL>());
				if (Main.netMode != 1)
				{
					NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, randomType);
				}
			}
		}
		return true;
	}

	public override void HitEffect(NPC npc, NPC.HitInfo hit)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		if (npc.life <= 0 && npc.Organic() && ashesOnDeath > 0)
		{
			DeathAshParticle.CreateAshesFromNPC(npc, Vector2.Zero);
		}
		if (npc.type == 439 && Main.netMode != 1)
		{
			newAI[1] = 35f;
			npc.netUpdate = true;
		}
		if (CalamityWorld.revenge)
		{
			switch (npc.type)
			{
			case 264:
				if (npc.life <= 0 && Main.netMode != 1)
				{
					NPC.NewNPC(npc.GetSource_FromAI(), (int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), ModContent.NPCType<PlanterasFreeTentacle>());
				}
				break;
			case 16:
			{
				if (npc.life > 0 || Main.netMode == 1)
				{
					break;
				}
				int slimeAmt = Main.rand.Next(2) + 2;
				for (int s = 0; s < slimeAmt; s++)
				{
					int slime = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)(npc.position.Y + (float)npc.height), 1);
					NPC obj = Main.npc[slime];
					obj.SetDefaults(-5);
					obj.velocity.X = npc.velocity.X * 2f;
					obj.velocity.Y = npc.velocity.Y;
					obj.velocity.X += (float)Main.rand.Next(-20, 20) * 0.1f + (float)(s * npc.direction) * 0.3f;
					obj.velocity.Y -= (float)Main.rand.Next(10) * 0.1f + (float)s;
					obj.ai[0] = -1000 * Main.rand.Next(3);
					if (Main.dedServ && slime < Main.maxNPCs)
					{
						NetMessage.SendData(23, -1, -1, null, slime);
					}
				}
				break;
			}
			case 83:
			case 84:
			case 101:
			case 122:
			case 153:
			case 154:
			case 163:
			case 179:
			case 238:
			case 290:
				if (Main.getGoodWorld)
				{
					npc.justHit = false;
				}
				break;
			}
			if (npc.type == ModContent.NPCType<Plagueshell>() && Main.getGoodWorld)
			{
				npc.justHit = false;
			}
		}
		if (plague && npc.life <= 0 && npc.realLife == -1 && Main.netMode != 1)
		{
			for (int i = 0; i < 10; i++)
			{
				int DustID = 220;
				Dust dust = Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID);
				dust.scale = Main.rand.NextFloat(0.6f, 0.75f);
				dust.velocity = Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.5f, 0.8f);
				dust.noGravity = true;
			}
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				NPC target = Main.npc[j];
				if (target != null && target.IsAnEnemy() && !target.buffImmune[ModContent.BuffType<Plague>()] && Vector2.Distance(target.Center, npc.Center) < 400f)
				{
					if (!target.HasBuff<Plague>() && target.life > 0)
					{
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(target.Center, Vector2.Zero, Main.rand.NextBool(3) ? Color.LimeGreen : Color.Green, Vector2.One, 0f, Main.rand.NextFloat(0.07f, 0.18f) * 3f, 0f, 15));
					}
					target.AddBuff(ModContent.BuffType<Plague>(), 300);
				}
			}
		}
		if (!scionsCurioEffected || npc.life > 0 || npc.realLife != -1)
		{
			return;
		}
		for (int g = 0; g < 17; g++)
		{
			Vector2 dustVel = Utils.RotatedByRandom(new Vector2(9f), 3.1415927410125732) * Main.rand.NextFloat(0.4f, 0.9f) + Vector2.UnitY * -10f;
			Dust dust2 = Dust.NewDustPerfect(npc.Center, ModContent.DustType<SquashDust>(), dustVel, 0, Main.rand.NextBool() ? Color.Green : Color.Chartreuse, Main.rand.NextFloat(1.1f, 1.35f));
			dust2.noGravity = false;
			dust2.fadeIn = Main.rand.NextFloat(0.2f, 2f);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(npc.Center, Vector2.Zero, Color.Chartreuse * 0.9f, "CalamityMod/Particles/ShineExplosion1", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.05f, 0.15f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		SoundStyle style = SoundID.DD2_ExplosiveTrapExplode with
		{
			Volume = 0.5f,
			Pitch = Main.rand.NextFloat(0.5f, 0.7f),
			MaxInstances = 6
		};
		SoundEngine.PlaySound(in style, npc.Center);
		int explosionDamage = 12;
		float highestDamage = 0f;
		Player Owner = null;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			float playerRangedDamage = p.GetTotalDamage(DamageClass.Ranged).ApplyTo(explosionDamage);
			if (playerRangedDamage > highestDamage && p.Calamity().scionsCurio)
			{
				highestDamage = playerRangedDamage;
				Owner = p;
			}
		}
		float blastSize = 115f;
		float minMultiplier = 0.5f;
		int hitsToMinMult = 5;
		int debuff = ModContent.BuffType<Irradiated>();
		int debuffTime = 300;
		Projectile projectile = Projectile.NewProjectileDirect((Owner != null) ? Owner.GetSource_FromThis() : npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)highestDamage, 7f, Owner?.whoAmI ?? (-1), blastSize, minMultiplier, hitsToMinMult);
		projectile.localAI[0] = debuff;
		projectile.localAI[1] = debuffTime;
		projectile.timeLeft = 15;
		projectile.DamageType = DamageClass.Ranged;
	}

	public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
	{
		if (player.Calamity().ZoneSulphur)
		{
			spawnRate = (int)((double)spawnRate * 1.1);
			maxSpawns = (int)((float)maxSpawns * 0.8f);
			if (Main.raining)
			{
				spawnRate = (int)((double)spawnRate * 0.7);
				maxSpawns = (int)((float)maxSpawns * 1.2f);
				if (!player.Calamity().ZoneAbyss && AcidRainEvent.AcidRainEventIsOngoing)
				{
					if (AcidRainEvent.AnyRainMinibosses)
					{
						maxSpawns = 5;
						spawnRate *= 2;
					}
					else
					{
						spawnRate = (Main.hardMode ? 36 : 33);
						maxSpawns = (Main.hardMode ? 15 : 12);
					}
				}
			}
		}
		else if (player.Calamity().ZoneAbyss)
		{
			spawnRate = (int)((double)spawnRate * 0.7);
			maxSpawns = (int)((float)maxSpawns * 1.1f);
		}
		else if (player.Calamity().ZoneCalamity)
		{
			spawnRate = (int)((double)spawnRate * 0.9);
			maxSpawns = (int)((float)maxSpawns * 1.1f);
		}
		else if (player.Calamity().ZoneAstral)
		{
			spawnRate = (int)((double)spawnRate * 0.6);
			maxSpawns = (int)((float)maxSpawns * 1.2f);
		}
		else if (player.Calamity().ZoneSunkenSea)
		{
			spawnRate = (int)((double)spawnRate * 0.9);
			maxSpawns = (int)((float)maxSpawns * 1.1f);
		}
		if (DownedBossSystem.downedDoG && (Main.pumpkinMoon || Main.snowMoon || Main.eclipse))
		{
			spawnRate = (int)((double)spawnRate * 0.75);
			maxSpawns = (int)((float)maxSpawns * 3f);
		}
		if (player.Calamity().clamity)
		{
			spawnRate = (int)((double)spawnRate * 0.02);
			maxSpawns = (int)((float)maxSpawns * 1.5f);
		}
		if (CalamityWorld.death && Main.bloodMoon && (double)player.position.Y < Main.worldSurface * 16.0)
		{
			spawnRate = (int)((double)spawnRate * 0.25);
			maxSpawns = (int)((float)maxSpawns * 5f);
		}
		if (CalamityWorld.death && player.ZoneGraveyard)
		{
			spawnRate = (int)((double)spawnRate * 0.6667);
			maxSpawns = (int)((float)maxSpawns * 1.5f);
		}
		if (NPC.LunarApocalypseIsUp && ((player.ZoneTowerNebula && NPC.ShieldStrengthTowerNebula == 0) || (player.ZoneTowerStardust && NPC.ShieldStrengthTowerStardust == 0) || (player.ZoneTowerVortex && NPC.ShieldStrengthTowerVortex == 0) || (player.ZoneTowerSolar && NPC.ShieldStrengthTowerSolar == 0)))
		{
			spawnRate = (int)((double)spawnRate * 0.85);
			maxSpawns = (int)((float)maxSpawns * 1.25f);
		}
		if (CalamityWorld.revenge)
		{
			spawnRate = (int)((double)spawnRate * 0.85);
		}
		if (player.Calamity().chaosCandle)
		{
			spawnRate = (int)((double)spawnRate * 0.5);
			maxSpawns = (int)((float)maxSpawns * 2f);
		}
		if (player.Calamity().zerg)
		{
			spawnRate = (int)((double)spawnRate * 0.25);
			maxSpawns = (int)((float)maxSpawns * 4f);
		}
		if (player.Calamity().bloodyMary)
		{
			spawnRate = (int)((double)spawnRate * 0.142);
			maxSpawns = (int)((float)maxSpawns * 5f);
		}
		if (player.Calamity().tranquilityCandle)
		{
			spawnRate = (int)((double)spawnRate * 1.6666);
			maxSpawns = (int)((float)maxSpawns * 0.6f);
		}
		if (player.Calamity().zen || (CalamityServerConfig.Instance.ForceTownSafety && player.townNPCs > 1f && Main.expertMode))
		{
			spawnRate = (int)((double)spawnRate * 2.5);
			maxSpawns = (int)((float)maxSpawns * 0.4f);
		}
		if (player.Calamity().isNearbyBoss && CalamityServerConfig.Instance.BossZen)
		{
			spawnRate *= 5;
			maxSpawns = (int)((float)maxSpawns * 0.001f);
		}
	}

	public override void EditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY)
	{
		if (player.Calamity().ZoneAbyss)
		{
			spawnRangeX = 60;
			safeRangeX = 38;
		}
	}

	public static void AttemptToSpawnLabCritters(Player player)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		int spawnRate = 400;
		int maxSpawnCount = NPC.maxSpawns;
		NPCLoader.EditSpawnRate(player, ref spawnRate, ref maxSpawnCount);
		if (player.nearbyActiveNPCs >= (float)maxSpawnCount)
		{
			return;
		}
		float playerCenterX = player.Center.X / 16f;
		float playerCenterY = player.Center.Y / 16f;
		Vector2 sunkenSeaLabCenter = CalamityWorld.SunkenSeaLabCenter / 16f;
		Vector2 planetoidLabCenter = CalamityWorld.PlanetoidLabCenter / 16f;
		Vector2 jungleLabCenter = CalamityWorld.JungleLabCenter / 16f;
		Vector2 hellLabCenter = CalamityWorld.HellLabCenter / 16f;
		Vector2 iceLabCenter = CalamityWorld.IceLabCenter / 16f;
		Vector2 checkPosition = default(Vector2);
		for (int i = 0; i < 8; i++)
		{
			int checkPositionX = (int)(playerCenterX + (float)(Main.rand.Next(30, 54) * Main.rand.NextBool().ToDirectionInt()));
			int checkPositionY = (int)(playerCenterY + (float)(Main.rand.Next(24, 45) * Main.rand.NextBool().ToDirectionInt()));
			((Vector2)(ref checkPosition))._002Ector((float)checkPositionX, (float)checkPositionY);
			Tile aboveSpawnTile = CalamityUtils.ParanoidTileRetrieval(checkPositionX, checkPositionY - 1);
			bool nearLab = checkPosition.ManhattanDistance(sunkenSeaLabCenter) < 180f;
			nearLab |= checkPosition.ManhattanDistance(planetoidLabCenter) < 180f;
			nearLab |= checkPosition.ManhattanDistance(jungleLabCenter) < 180f;
			nearLab |= checkPosition.ManhattanDistance(hellLabCenter) < 180f;
			nearLab |= checkPosition.ManhattanDistance(iceLabCenter) < 180f;
			bool nearPlagueLab = checkPosition.ManhattanDistance(jungleLabCenter) < 180f;
			if (!((aboveSpawnTile.WallType == ModContent.WallType<HazardChevronWall>() || aboveSpawnTile.WallType == ModContent.WallType<LaboratoryPanelWall>() || aboveSpawnTile.WallType == ModContent.WallType<LaboratoryPlateBeam>()) | (aboveSpawnTile.WallType == ModContent.WallType<LaboratoryPlatePillar>() || aboveSpawnTile.WallType == ModContent.WallType<LaboratoryPlatingWall>() || aboveSpawnTile.WallType == ModContent.WallType<RustedPlateBeam>())) || !nearLab || Collision.SolidCollision((checkPosition - new Vector2(2f, 2f)).ToWorldCoordinates(), 4, 4) || player.nearbyActiveNPCs >= (float)maxSpawnCount || !Main.rand.NextBool(spawnRate))
			{
				continue;
			}
			WeightedRandom<int> pool = new WeightedRandom<int>();
			pool.Add(0, 0.0);
			pool.Add(ModContent.NPCType<RepairUnitCritter>(), 0.02500000037252903);
			pool.Add(ModContent.NPCType<Androomba>(), 0.009999999776482582);
			if (nearPlagueLab)
			{
				pool.Add(ModContent.NPCType<NanodroidPlagueGreen>(), 0.02500000037252903);
				pool.Add(ModContent.NPCType<NanodroidPlagueRed>(), 0.02500000037252903);
				pool.Add(ModContent.NPCType<NanodroidDysfunctional>(), 0.019999999552965164);
			}
			else
			{
				pool.Add(ModContent.NPCType<Nanodroid>(), 0.05000000074505806);
				pool.Add(ModContent.NPCType<NanodroidDysfunctional>(), 0.05000000074505806);
			}
			int typeToSpawn = pool.Get();
			if (typeToSpawn != 0)
			{
				int spawnedNPC = NPCLoader.SpawnNPC(typeToSpawn, checkPositionX, checkPositionY - 1);
				if (Main.dedServ && spawnedNPC < Main.maxNPCs)
				{
					Main.npc[spawnedNPC].position.Y -= 8f;
					NetMessage.SendData(23, -1, -1, null, spawnedNPC);
					break;
				}
			}
		}
	}

	public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
	{
		bool calamityBiomeZone = spawnInfo.Player.Calamity().ZoneAbyss || spawnInfo.Player.Calamity().ZoneCalamity || spawnInfo.Player.Calamity().ZoneSulphur || spawnInfo.Player.Calamity().ZoneSunkenSea || (spawnInfo.Player.Calamity().ZoneAstral && !spawnInfo.Player.PillarZone());
		if (!spawnInfo.Water && spawnInfo.Player.ZoneRockLayerHeight)
		{
			if (NPC.downedGoblins && !NPC.savedGoblin && !NPC.AnyNPCs(105))
			{
				pool[105] = SpawnCondition.BoundCaveNPC.Chance * 2f;
			}
			if (Main.hardMode && !NPC.savedWizard && !NPC.AnyNPCs(106))
			{
				pool[106] = SpawnCondition.BoundCaveNPC.Chance * 2f;
			}
		}
		if (Main.hardMode && spawnInfo.Player.ZoneRockLayerHeight && !calamityBiomeZone && (spawnInfo.SpawnTileType == 117 || spawnInfo.SpawnTileType == 116 || spawnInfo.SpawnTileType == 164 || spawnInfo.SpawnTileType == 109 || spawnInfo.SpawnTileType == 402 || spawnInfo.SpawnTileType == 403))
		{
			pool[120] = SpawnCondition.Cavern.Chance * 0.125f;
		}
		if (spawnInfo.Player.ZoneRockLayerHeight && spawnInfo.Water && !calamityBiomeZone)
		{
			if (!Main.hardMode)
			{
				pool[103] = SpawnCondition.CaveJellyfish.Chance * 0.5f;
			}
			else
			{
				pool[63] = SpawnCondition.CaveJellyfish.Chance;
			}
		}
		if (spawnInfo.Player.ZoneGlowshroom && Main.hardMode && (spawnInfo.Player.ZoneOverworldHeight || spawnInfo.Player.ZoneSkyHeight) && NPC.CountNPCS(374) < 2)
		{
			pool[374] = SpawnCondition.OverworldMushroom.Chance * 0.5f;
		}
		if (!Main.dayTime && Main.time < 16200.0 && Main.hardMode && (spawnInfo.Player.ZoneOverworldHeight || spawnInfo.Player.ZoneSkyHeight) && !NPC.AnyNPCs(661))
		{
			pool[661] = SpawnCondition.OverworldHallow.Chance * 0.1f;
		}
		if (spawnInfo.Player.Calamity().fairyBoots)
		{
			int maxFairies = 5;
			if (NPC.CountNPCS(585) + NPC.CountNPCS(584) + NPC.CountNPCS(583) < maxFairies)
			{
				if (!NPC.AnyNPCs(585))
				{
					pool[585] = SpawnCondition.Overworld.Chance * 5f;
				}
				if (!NPC.AnyNPCs(584))
				{
					pool[584] = SpawnCondition.Overworld.Chance * 5f;
				}
				if (!NPC.AnyNPCs(583))
				{
					pool[583] = SpawnCondition.Overworld.Chance * 5f;
				}
			}
		}
		if (spawnInfo.Player.ZoneGraveyard)
		{
			pool[632] = SpawnCondition.OverworldNightMonster.Chance * 0.2f;
			pool[53] = SpawnCondition.OverworldNightMonster.Chance * 0.035f;
			pool[536] = SpawnCondition.OverworldNightMonster.Chance * 0.035f;
		}
		if (calamityBiomeZone)
		{
			pool[0] = 0f;
		}
		if (!AnyEvents(spawnInfo.Player) && spawnInfo.Player.InAstral())
		{
			pool[484] = SpawnCondition.TownCritter.Chance;
		}
		if (spawnInfo.Player.Calamity().ZoneSulphur && !spawnInfo.Player.Calamity().ZoneAbyss && AcidRainEvent.AcidRainEventIsOngoing)
		{
			pool.Clear();
			if (!DownedBossSystem.downedPolterghast || AcidRainEvent.AccumulatedKillPoints != 1)
			{
				Dictionary<int, AcidRainSpawnData> PossibleEnemies = AcidRainEvent.PossibleEnemiesPreHM;
				Dictionary<int, AcidRainSpawnData> PossibleMinibosses = new Dictionary<int, AcidRainSpawnData>();
				if (DownedBossSystem.downedAquaticScourge)
				{
					PossibleEnemies = AcidRainEvent.PossibleEnemiesAS;
					PossibleMinibosses = AcidRainEvent.PossibleMinibossesAS;
					if (!PossibleEnemies.ContainsKey(ModContent.NPCType<IrradiatedSlime>()))
					{
						PossibleEnemies.Add(ModContent.NPCType<IrradiatedSlime>(), new AcidRainSpawnData(1, 0f, AcidRainSpawnRequirement.Anywhere));
					}
				}
				if (DownedBossSystem.downedPolterghast)
				{
					PossibleEnemies = AcidRainEvent.PossibleEnemiesPolter;
					PossibleMinibosses = AcidRainEvent.PossibleMinibossesPolter;
				}
				foreach (int enemy in PossibleEnemies.Select((KeyValuePair<int, AcidRainSpawnData> enemyType) => enemyType.Key))
				{
					bool canSpawn = true;
					switch (PossibleEnemies[enemy].SpawnRequirement)
					{
					case AcidRainSpawnRequirement.Land:
						canSpawn = !spawnInfo.Water;
						break;
					case AcidRainSpawnRequirement.Water:
						canSpawn = spawnInfo.Water;
						break;
					}
					if (canSpawn && !pool.ContainsKey(enemy))
					{
						pool.Add(enemy, PossibleEnemies[enemy].SpawnRate);
					}
				}
				if (PossibleMinibosses.Count > 0)
				{
					foreach (int miniboss in PossibleMinibosses.Select((KeyValuePair<int, AcidRainSpawnData> keyValuePair) => keyValuePair.Key).ToList())
					{
						bool canSpawn2 = true;
						switch (PossibleMinibosses[miniboss].SpawnRequirement)
						{
						case AcidRainSpawnRequirement.Land:
							canSpawn2 = !spawnInfo.Water;
							break;
						case AcidRainSpawnRequirement.Water:
							canSpawn2 = spawnInfo.Water;
							break;
						}
						if (canSpawn2)
						{
							pool.Add(miniboss, PossibleMinibosses[miniboss].SpawnRate);
						}
					}
				}
				if (NPC.CountNPCS(ModContent.NPCType<NuclearToad>()) >= AcidRainEvent.MaxNuclearToadCount)
				{
					pool.Remove(ModContent.NPCType<NuclearToad>());
				}
			}
		}
		if (!spawnInfo.PlayerSafe && spawnInfo.Player.Calamity().disableVoodooSpawns)
		{
			pool.Remove(66);
		}
	}

	public override void OnSpawn(NPC npc, IEntitySource source)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (npc.type == 668)
		{
			DeerclopsAI.hasTargetBeenInRange = false;
		}
		if (npc.boss)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.type == 72 || n.type == 70)
				{
					n.active = false;
					n.netUpdate = true;
				}
			}
		}
		if (npc.type != 66 || !(source is EntitySource_SpawnNPC))
		{
			return;
		}
		bool voodooDemonDollActive = false;
		Vector2 v = npc.Center;
		for (int i = 0; i < 255; i++)
		{
			Player p = Main.player[i];
			if (p != null && p.active && p.DistanceSQ(v) < 4000000f && p.Calamity().disableVoodooSpawns)
			{
				voodooDemonDollActive = true;
				break;
			}
		}
		if (voodooDemonDollActive)
		{
			npc.Transform(62);
			npc.netUpdate = true;
		}
	}

	public override void FindFrame(NPC npc, int frameHeight)
	{
		if (npc.IsABestiaryIconDummy)
		{
			bestiaryWormTimer += 0.02f;
			if (bestiaryWormTimer > 4320f)
			{
				bestiaryWormTimer = 0f;
			}
		}
	}

	public override void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		if (!npc.canDisplayBuffs)
		{
			return;
		}
		if (absorberAffliction)
		{
			AbsorberAffliction.DrawEffects(npc, ref drawColor);
		}
		if (ashesOnDeath > 0)
		{
			if (Main.rand.NextBool(4))
			{
				Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Main.rand.NextVector2Circular(2.75f, 6.5f), ModContent.ProjectileType<RancorFog>(), 0, 0f, Main.myPlayer, 0f, 0.475f);
			}
			if (Main.rand.NextBool(6))
			{
				RancorLavaMetaball.SpawnParticle(new Vector2(npc.position.X + Main.rand.NextFloat(-10f, (float)npc.width + 10f), npc.position.Y + Main.rand.NextFloat(-10f, (float)npc.height + 10f)), Main.rand.NextFloat(30f, 37f));
			}
		}
		if (astralInfection)
		{
			AstralInfectionDebuff.DrawEffects(npc, ref drawColor);
		}
		if (brimstoneFlames || npc.HasBuff<Enraged>())
		{
			BrimstoneFlames.DrawEffects(npc, ref drawColor);
		}
		if (demonicFlames)
		{
			DemonicFlames.DrawEffects(npc, ref drawColor);
		}
		if (burningBlood)
		{
			BurningBlood.DrawEffects(npc, ref drawColor);
		}
		if (brainRot)
		{
			BrainRot.DrawEffects(npc, ref drawColor);
		}
		if (crushDepth)
		{
			CrushDepth.DrawEffects(npc, ref drawColor);
		}
		if (hadopelagicPressure)
		{
			HadopelagicPressure.DrawEffects(npc, ref drawColor);
		}
		if (dragonFire)
		{
			Dragonfire.DrawEffects(npc, ref drawColor);
		}
		if (vermillionFlux)
		{
			VermillionFlux.DrawEffects(npc, ref drawColor);
		}
		if (auricRebuke)
		{
			AuricRebuke.DrawEffects(npc, ref drawColor);
		}
		if (staticDischarge)
		{
			StaticDischarge.DrawEffects(npc, ref drawColor);
		}
		if (elementalMix)
		{
			ElementalMix.DrawEffects(npc, ref drawColor);
		}
		if (eutrophication || temporalSadness)
		{
			Eutrophication.DrawEffects(npc, ref drawColor);
		}
		if (godSlayerInferno)
		{
			GodSlayerInferno.DrawEffects(npc, ref drawColor);
		}
		if (holyFlames || banishingFire)
		{
			HolyFlames.DrawEffects(npc, ref drawColor);
		}
		if (heavyBleeding)
		{
			HeavyBleeding.DrawEffects(npc, ref drawColor);
		}
		Color val2;
		if (hyperiusFxTimer > 0)
		{
			float rate = Main.GlobalTimeWrappedHourly * 5f;
			int num = 5;
			List<Color> list = new List<Color>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<Color> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = Color.Yellow;
			num2++;
			span[num2] = Color.Magenta;
			num2++;
			span[num2] = Color.Red;
			num2++;
			span[num2] = Color.Cyan;
			num2++;
			span[num2] = Color.Lime;
			List<Color> eColors = list;
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			Color usedColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
			Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			Texture2D sparkle = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineSoftEdge", (AssetRequestMode)2).Value;
			Vector2 drawPosition = npc.Center - Main.screenPosition;
			float power = (float)Math.Pow(Utils.GetLerpValue(0f, 20f, hyperiusFxTimer, clamped: true), 3.0) * MathHelper.Lerp((float)(Math.Max(npc.height, npc.width) / 100), 1.4f, 0.5f);
			for (int i = 0; i < 4; i++)
			{
				float iMult = 1f + 0.25f * (float)i;
				val2 = Color.Lerp(usedColor, Color.White, (float)i * 0.1f);
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(tex2, drawPosition, null, val2 * 0.6f, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1f, 0.8f) * 0.35f * Main.rand.NextFloat(0.9f, 1.1f) * iMult * power * Utils.GetLerpValue(0f, 20f, hyperiusFxTimer), (SpriteEffects)0);
				for (int b = -1; b <= 1; b += 2)
				{
					float uncappedSine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 8f / (float)Math.PI);
					float sine = MathHelper.Lerp(Math.Abs(uncappedSine), 0.75f, 0.75f);
					Vector2 scale = new Vector2(0.25f / iMult + 0.7f * (1f - sine), 1.1f * sine * iMult) * power * 0.05f;
					float rotation = (float)Math.PI / 4f * (float)b * uncappedSine;
					val2 = Color.Lerp(usedColor, Color.White, (float)i * 0.1f);
					((Color)(ref val2)).A = 0;
					Main.EntitySpriteDraw(sparkle, drawPosition, null, val2, rotation, sparkle.Size() * 0.5f, scale, (SpriteEffects)0);
				}
			}
		}
		if (laceration)
		{
			Laceration.DrawEffects(npc, ref drawColor);
		}
		if (laserBurnTimer > 0)
		{
			int particleChance = Math.Max(3, 10 - laserBurnStacks / 3);
			if (laserBurnTimer % particleChance == 0)
			{
				Vector2 position = new Vector2(npc.position.X + (float)Main.rand.Next(0, npc.width), npc.position.Y + (float)Main.rand.Next(0, npc.height));
				int arsenalLaserDust = ArsenalEffects.ArsenalLaserDust;
				val2 = default(Color);
				Dust dust = Dust.NewDustPerfect(position, arsenalLaserDust, null, 0, val2);
				dust.velocity = (Vector2.UnitX * 3f * ((float)laserBurnStacks * 0.03f)).RotatedByRandom(100.0) * Main.rand.NextFloat(0.85f, 1f) + npc.velocity * 0.5f;
				dust.scale = Main.rand.NextFloat(0.55f, 0.7f) + (float)laserBurnStacks * 0.01f;
				dust.noGravity = true;
				dust.color = Color.Red;
				dust.fadeIn = (float)laserBurnStacks * 0.3f;
			}
			if (laserBurnType == 0)
			{
				laserBurnMarked = false;
				laserBurnTimer = 0;
			}
		}
		if (miracleBlight)
		{
			MiracleBlight.DrawEffects(npc, ref drawColor);
		}
		if (nightwither)
		{
			Nightwither.DrawEffects(npc, ref drawColor);
		}
		if (pearlAura)
		{
			PearlAura.DrawEffects(npc, ref drawColor);
		}
		if (plague)
		{
			Plague.DrawEffects(npc, ref drawColor);
		}
		if (relicOfResilienceWeakness)
		{
			ProfanedWeakness.DrawEffects(npc, ref drawColor);
		}
		if (riptide)
		{
			RiptideDebuff.DrawEffects(npc, ref drawColor);
		}
		if (somaShredStacks > 0 && !Main.dedServ)
		{
			Shred.DrawEffects(npc, this, ref drawColor);
		}
		if (sulphurPoison)
		{
			SulphuricPoisoning.DrawEffects(npc, ref drawColor);
		}
		if (trueVulnerabilityHex)
		{
			TrueVulnerabilityHex.DrawEffects(npc, ref drawColor);
		}
		if (vaporfied)
		{
			Vaporfied.DrawEffects(npc, ref drawColor);
		}
		if (veriumDoomTimer > 0)
		{
			int sparkleChance = Math.Max(2, 8 - veriumDoomStacks / 2);
			if (veriumDoomTimer % sparkleChance == 0)
			{
				float veriumRatio = (float)veriumDoomTimer / 90f;
				GeneralParticleHandler.SpawnParticle(new CustomPulse(new Vector2(npc.position.X + (float)Main.rand.Next(0, npc.width), npc.position.Y + (float)Main.rand.Next(0, npc.height)), Vector2.Zero, Color.Lerp(new Color(103, 230, 240), new Color(255, 110, 220), 1f - veriumRatio), "CalamityMod/Particles/Sparkle", Vector2.One, Main.rand.NextFloat(-0.75f, 0.75f), 0.9f, 1.1f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		if (voidfrost)
		{
			Voidfrost.DrawEffects(npc, ref drawColor);
		}
		if (electrified && Main.rand.NextBool())
		{
			Vector2 position2 = npc.position;
			int width = npc.width;
			int height = npc.height;
			float speedX = Main.rand.NextFloat(-2f, 2f);
			float speedY = Main.rand.NextFloat(-2f, 2f);
			val2 = default(Color);
			Dust.NewDustDirect(position2, width, height, 226, speedX, speedY, 0, val2, 0.35f);
		}
		if (webbed && Main.rand.Next(5) < 4)
		{
			Vector2 position3 = npc.position - new Vector2(2f, 2f);
			int width2 = npc.width + 4;
			int height2 = npc.height + 4;
			float speedX2 = npc.velocity.X * 0.4f;
			float speedY2 = npc.velocity.Y * 0.4f;
			val2 = default(Color);
			int dust2 = Dust.NewDust(position3, width2, height2, 30, speedX2, speedY2, 100, val2, 1.5f);
			Main.dust[dust2].noGravity = true;
			Dust obj = Main.dust[dust2];
			obj.velocity *= 1.1f;
			Main.dust[dust2].velocity.Y += 0.25f;
			if (Main.rand.NextBool())
			{
				Main.dust[dust2].noGravity = false;
				Main.dust[dust2].scale *= 0.5f;
			}
		}
		if (ladHearts > 0 && !npc.loveStruck && !Main.dedServ && Main.rand.NextBool(5))
		{
			Vector2 velocity = CalamityUtils.RandomVelocity(10f, 1f, 1f, 0.66f);
			int heart = Gore.NewGore(npc.GetSource_FromThis(), npc.position + new Vector2((float)Main.rand.Next(npc.width + 1), (float)Main.rand.Next(npc.height + 1)), velocity * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
			Main.gore[heart].sticky = false;
			Gore obj2 = Main.gore[heart];
			obj2.velocity *= 0.4f;
			Main.gore[heart].velocity.Y -= 0.6f;
		}
		drawColor = npc.GetNPCColorTintedByBuffs(drawColor);
		if (glacialState)
		{
			drawColor = Color.Cyan;
		}
		else if (auricRebuke)
		{
			int scaleFactor = (int)Utils.Remap(npc.width, 30f, 400f, 5f, 15f);
			drawColor = (Main.rand.NextBool(scaleFactor) ? Color.Lerp(Color.DarkBlue, Color.White, Utils.Remap(npc.width, 30f, 400f, 0.4f, 0.7f)) : Color.White);
		}
		else if (vermillionFlux)
		{
			int scaleFactor2 = (int)Utils.Remap(npc.width, 30f, 400f, 5f, 15f);
			drawColor = (Main.rand.NextBool(scaleFactor2) ? Color.Lerp(Color.DarkRed, Color.White, Utils.Remap(npc.width, 30f, 400f, 0f, 0.7f)) : Color.White);
		}
		else if (electrified)
		{
			int scaleFactor3 = (int)Utils.Remap(npc.width, 30f, 400f, 5f, 15f);
			drawColor = (Main.rand.NextBool(scaleFactor3) ? Color.Lerp(Color.SlateGray, Color.White, Utils.Remap(npc.width, 30f, 400f, 0f, 0.7f)) : Color.White);
		}
		else if (absorberAffliction)
		{
			drawColor = Color.DarkSeaGreen;
		}
		else if (markedForDeath || vaporfied)
		{
			drawColor = Color.Fuchsia;
		}
		else if (pearlAura)
		{
			drawColor = new Color(185, 185, 255);
		}
		else if (timeDistortion || galvanicCorrosion)
		{
			drawColor = Color.Aquamarine;
		}
	}

	public override Color? GetAlpha(NPC npc, Color drawColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		if (npc.IsABestiaryIconDummy)
		{
			return null;
		}
		if (Main.LocalPlayer.Calamity().trippy || (npc.type == 50 && Main.zenithWorld))
		{
			return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, Main.DiscoR);
		}
		if (npc.type == 222 && Main.zenithWorld)
		{
			if ((float)npc.life / (float)npc.lifeMax < 0.5f)
			{
				return new Color(0, 255, 0, 255 - npc.alpha);
			}
			return new Color(255, 0, 0, 255 - npc.alpha);
		}
		if (npc.HasBuff<Enraged>())
		{
			return new Color(200, 50, 50, 255 - npc.alpha);
		}
		if (npc.type == 112 || npc.type == 666)
		{
			return new Color(150, 200, 0, npc.alpha);
		}
		if (npc.type == 523 || npc.type == 658 || npc.type == 659 || npc.type == 660)
		{
			return new Color(255, 255, 255, npc.alpha);
		}
		return null;
	}

	public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0996: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		bool shouldDrawBool = true;
		if (npc.IsABestiaryIconDummy)
		{
			switch (npc.netID)
			{
			case 7:
			case 10:
			case 13:
			case 39:
			case 87:
			case 95:
			case 98:
			case 117:
			case 134:
			case 402:
			case 412:
			case 454:
			case 510:
			case 513:
			case 621:
				return DrawVanillaBestiaryWorms(spriteBatch, npc, drawColor);
			}
		}
		if (npc.type != 266 && (npc.type != 370 || npc.ai[0] <= 9f) && npc.active && CalamityClientConfig.Instance.DebuffDisplay && (npc.boss || BossHealthBarManager.MinibossHPBarList.Contains(npc.type) || BossHealthBarManager.OneToMany.ContainsKey(npc.type) || CalamityNPCSets.ForceDrawDebuffDisplay[npc.type]))
		{
			List<Texture2D> currentDebuffs = new List<Texture2D>();
			for (int b = 0; b < moddedDebuffTextureList.Count(); b++)
			{
				if (moddedDebuffTextureList[b].Item2(npc))
				{
					currentDebuffs.Add(ModContent.Request<Texture2D>(moddedDebuffTextureList[b].Item1, (AssetRequestMode)2).Value);
				}
			}
			if (electrified)
			{
				currentDebuffs.Add(TextureAssets.Buff[144].Value);
			}
			if (npc.onFire)
			{
				currentDebuffs.Add(TextureAssets.Buff[24].Value);
			}
			if (npc.poisoned)
			{
				currentDebuffs.Add(TextureAssets.Buff[20].Value);
			}
			if (npc.onFire2)
			{
				currentDebuffs.Add(TextureAssets.Buff[39].Value);
			}
			if (npc.onFrostBurn)
			{
				currentDebuffs.Add(TextureAssets.Buff[44].Value);
			}
			if (npc.venom)
			{
				currentDebuffs.Add(TextureAssets.Buff[70].Value);
			}
			if (npc.shadowFlame)
			{
				currentDebuffs.Add(TextureAssets.Buff[153].Value);
			}
			if (npc.oiled)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/Oiled", (AssetRequestMode)2).Value);
			}
			if (npc.javelined)
			{
				currentDebuffs.Add(TextureAssets.Buff[169].Value);
			}
			if (npc.daybreak)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/Buffs/DamageOverTime/Daybroken", (AssetRequestMode)2).Value);
			}
			if (npc.celled)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/Celled", (AssetRequestMode)2).Value);
			}
			if (npc.dryadBane)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/DryadsBane", (AssetRequestMode)2).Value);
			}
			if (npc.dryadWard)
			{
				currentDebuffs.Add(TextureAssets.Buff[165].Value);
			}
			if (npc.soulDrain && npc.realLife == -1)
			{
				currentDebuffs.Add(TextureAssets.Buff[151].Value);
			}
			if (npc.onFire3)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/Hellfire", (AssetRequestMode)2).Value);
			}
			if (npc.onFrostBurn2)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/Frostbite", (AssetRequestMode)2).Value);
			}
			if (npc.tentacleSpiked)
			{
				currentDebuffs.Add(TextureAssets.Buff[337].Value);
			}
			if (npc.confused)
			{
				currentDebuffs.Add(TextureAssets.Buff[31].Value);
			}
			if (npc.ichor)
			{
				currentDebuffs.Add(TextureAssets.Buff[69].Value);
			}
			if (webbed)
			{
				currentDebuffs.Add(TextureAssets.Buff[149].Value);
			}
			if (npc.midas)
			{
				currentDebuffs.Add(TextureAssets.Buff[72].Value);
			}
			if (npc.loveStruck)
			{
				currentDebuffs.Add(TextureAssets.Buff[119].Value);
			}
			if (npc.stinky)
			{
				currentDebuffs.Add(TextureAssets.Buff[120].Value);
			}
			if (npc.betsysCurse)
			{
				currentDebuffs.Add(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/BetsysCurse", (AssetRequestMode)2).Value);
			}
			if (npc.dripping)
			{
				currentDebuffs.Add(TextureAssets.Buff[103].Value);
			}
			if (npc.drippingSlime)
			{
				currentDebuffs.Add(TextureAssets.Buff[137].Value);
			}
			if (npc.drippingSparkleSlime)
			{
				currentDebuffs.Add(TextureAssets.Buff[320].Value);
			}
			int totalLength = currentDebuffs.Count * 14;
			int buffDisplayRowLimit = 5;
			float drawPosX = (((float)totalLength >= 80f) ? 40f : ((float)(totalLength / 2)));
			float drawPosY = (float)npc.height * npc.scale / 2f + npc.gfxOffY + 32f;
			for (int i = 0; i < currentDebuffs.Count; i++)
			{
				if (i != 0)
				{
					drawPosX = ((i % buffDisplayRowLimit != 0) ? (drawPosX - 14f) : 40f);
				}
				float additionalYOffset = 14f * (float)Math.Floor((double)i * 0.2);
				Texture2D tex = currentDebuffs[i];
				spriteBatch.Draw(tex, npc.Center - screenPos - new Vector2(drawPosX, drawPosY + additionalYOffset), (Rectangle?)null, Color.White, 0f, default(Vector2), 0.5f, (SpriteEffects)0, 0f);
				if (currentDebuffs[i] == TextureAssets.Buff[ModContent.BuffType<Shred>()].Value)
				{
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, somaShredStacks.ToString(), npc.Center - screenPos - new Vector2(drawPosX, drawPosY + additionalYOffset) + Vector2.One * 4f, Color.Gold, 0f, Vector2.Zero, Vector2.One * Main.UIScale * 0.8f);
				}
			}
			int yOffset = 0;
			for (int i2 = NPC.maxBuffs - 1; i2 >= 0; i2--)
			{
				if (npc.buffTime[i2] > 0 && CalamityBuffSets.SummonTagDebuff.TryGetValue(npc.buffType[i2], out var tag))
				{
					Texture2D tex2 = TextureAssets.Item[tag.TagItem].Value;
					Rectangle frame = ((Main.itemAnimations[tag.TagItem] == null) ? tex2.Frame() : Main.itemAnimations[tag.TagItem].GetFrame(tex2));
					if (tag.TagTexture != null)
					{
						tex2 = tag.TagTexture.Value;
						frame = tex2.Frame();
					}
					Vector2 drawPos = npc.Center - screenPos + Vector2.UnitY * (drawPosY + (float)frame.Height * 0.5f + (float)yOffset);
					spriteBatch.Draw(tex2, drawPos, (Rectangle?)frame, Color.White, 0f, frame.Size() * 0.5f, 0.75f, (SpriteEffects)0, 0f);
					yOffset += frame.Height + 4;
				}
			}
		}
		if (!Main.LocalPlayer.Calamity().trippy)
		{
			if (npc.Calamity().vulnerabilityHex || npc.Calamity().trueVulnerabilityHex)
			{
				float compactness = (float)npc.width * 0.6f;
				if (compactness < 10f)
				{
					compactness = 10f;
				}
				float power = (float)npc.height / 100f;
				if (power > 2.75f)
				{
					power = 2.75f;
				}
				if (VulnerabilityHexFireDrawer == null || VulnerabilityHexFireDrawer.LocalTimer >= VulnerabilityHexFireDrawer.SetLifetime)
				{
					VulnerabilityHexFireDrawer = new FireParticleSet(npc.Calamity().trueVulnerabilityHex ? npc.buffTime[npc.FindBuffIndex(ModContent.BuffType<TrueVulnerabilityHex>())] : npc.buffTime[npc.FindBuffIndex(ModContent.BuffType<VulnerabilityHex>())], 1, Color.Red * 1.25f, Color.Red, compactness, power);
				}
				else
				{
					VulnerabilityHexFireDrawer.DrawSet(npc.Bottom - Vector2.UnitY * (12f - npc.gfxOffY));
				}
			}
			else
			{
				VulnerabilityHexFireDrawer = null;
			}
			if (npc.Calamity().manaBurn > 0f)
			{
				float compactness2 = (float)npc.width * 0.6f;
				if (compactness2 < 10f)
				{
					compactness2 = 10f;
				}
				float power2 = (float)npc.height / 100f;
				if (power2 > 2.75f)
				{
					power2 = 2.75f;
				}
				Color color = Color.Blue;
				if (ManaBurnFireDrawer == null || ManaBurnFireDrawer.LocalTimer >= ManaBurnFireDrawer.SetLifetime)
				{
					ManaBurnFireDrawer = new FireParticleSet(60, 1, color * 1.25f, color, compactness2, power2);
				}
				else
				{
					ManaBurnFireDrawer.DrawSet(npc.Bottom - Vector2.UnitY * (12f - npc.gfxOffY));
				}
			}
			else
			{
				ManaBurnFireDrawer = null;
			}
		}
		if (Main.zenithWorld && NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.CeaselessVoid.CeaselessVoid>()))
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
			GameShaders.Armor.GetShaderFromItemId(3556).Apply();
		}
		return shouldDrawBool;
	}

	public static Color buffColor(Color newColor, float R, float G, float B, float A)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref newColor)).R = (byte)((float)(int)((Color)(ref newColor)).R * R);
		((Color)(ref newColor)).G = (byte)((float)(int)((Color)(ref newColor)).G * G);
		((Color)(ref newColor)).B = (byte)((float)(int)((Color)(ref newColor)).B * B);
		((Color)(ref newColor)).A = (byte)((float)(int)((Color)(ref newColor)).A * A);
		return newColor;
	}

	public static bool DrawVanillaBestiaryWorms(SpriteBatch spriteBatch, NPC npc, Color drawColor)
	{
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		npc.Opacity = 1f;
		int segments = 6;
		int spacing = 20;
		int bashLength = 0;
		float bashSpeed = 0f;
		int speed = 3;
		float rotation = 0.6f;
		Texture2D wyvernArm = TextureAssets.Npc[88].Value;
		Texture2D wyvernBody = TextureAssets.Npc[89].Value;
		return npc.netID switch
		{
			95 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, segments, 24, 0.4f, Vector2.Zero, speed, 10f, 10f, 0.2f), 
			10 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, 8, 14, 0.6f, new Vector2(20f, 0f), 4, 10f, 6f, 0.18f), 
			13 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, segments, 34, 0.2f, new Vector2(30f, 0f), speed, 10f, 16f, 0.24f), 
			87 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, (Texture2D[])(object)new Texture2D[4] { wyvernArm, wyvernBody, wyvernBody, wyvernBody }, 4, 28, 0.1f, new Vector2(36f, 0f), speed, 6f, 50f, 0.3f, flip: true), 
			402 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, 8, 14, rotation, new Vector2(0f, 10f), 4, 10f, 6f, 0.18f), 
			412 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, segments, spacing, rotation, Vector2.Zero, 6, 10f, 16f, 0.22f), 
			454 => DrawSpecialBestiaryWorm(spriteBatch, npc, drawColor), 
			134 => DrawSpecialBestiaryWorm(spriteBatch, npc, drawColor), 
			117 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, 8, 14, 0.6f, new Vector2(20f, 0f), 4, 10f, 6f, 0.18f), 
			7 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, segments, spacing, rotation, Vector2.Zero, speed, 20f, 10f, 0.2f), 
			513 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, 9, 14, rotation, Vector2.Zero, speed, 20f, 6f, 0.14f), 
			510 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, segments, 28, 0.4f, Vector2.Zero, speed, 10f, bashLength, bashSpeed), 
			621 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, 6, 22, 0.1f, Vector2.Zero, speed, 6f, 20f, 0.2f, flip: true), 
			39 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, 9, 16, rotation, Vector2.Zero, speed, 10f, 30f, 0.4f), 
			98 => CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, TextureAssets.Npc[npc.type].Value, TextureAssets.Npc[npc.type + 1].Value, segments, spacing, rotation, Vector2.Zero, speed, 20f, 10f, 0.2f), 
			_ => true, 
		};
	}

	public static bool DrawSpecialBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		bool dragon = npc.type == 454;
		Texture2D headTexture = TextureAssets.Npc[npc.type].Value;
		float wormTimer = npc.Calamity().bestiaryWormTimer;
		int frameAmt = ((!dragon) ? 1 : 3);
		npc.frame = TextureAssets.Npc[npc.type].Frame(1, frameAmt);
		Vector2 val = new Vector2((float)((!dragon) ? 20 : 0), (float)((!dragon) ? 20 : 0));
		float offset = -0.2f;
		float startX = val.X;
		float startY = val.Y;
		int segmentSpacing = (dragon ? 32 : 38);
		int animationSpeed = 3;
		int range = 10;
		int headOffset = (dragon ? 40 : 20);
		float headSpeedOffset = (dragon ? 0.2f : 0.16f);
		float rotationStrength = 0.2f;
		for (int i = 4; i > 0; i--)
		{
			float bodyOffset = ((i == 1) ? ((float)(i * segmentSpacing) * 0.4f) : ((float)(i * segmentSpacing) - (float)segmentSpacing * 0.5f));
			Texture2D toUse = ((i == 2) ? TextureAssets.Npc[455].Value : TextureAssets.Npc[456].Value);
			if (!dragon)
			{
				toUse = TextureAssets.Npc[135].Value;
			}
			int bodyFrameAmt = (dragon ? 1 : 2);
			spriteBatch.Draw(toUse, npc.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * (float)i) * (float)animationSpeed) * (float)range + startY), (Rectangle?)toUse.Frame(1, bodyFrameAmt), npc.GetAlpha(drawColor), npc.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer + offset * (float)i) * (float)animationSpeed) * ((float)Math.PI / 4f) * rotationStrength, new Vector2((float)toUse.Width * 0.5f, (float)toUse.Height * 0.5f / (float)bodyFrameAmt), npc.scale, (SpriteEffects)1, 0f);
		}
		spriteBatch.Draw(headTexture, npc.position + new Vector2(startX + (float)headOffset, MathF.Sin((wormTimer - headSpeedOffset) * (float)animationSpeed) * (float)range + startY), (Rectangle?)npc.frame, npc.GetAlpha(drawColor), npc.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer - headSpeedOffset) * (float)animationSpeed) * ((float)Math.PI / 4f) * rotationStrength, new Vector2((float)headTexture.Width * 0.5f, (float)headTexture.Height / (float)frameAmt), npc.scale, (SpriteEffects)1, 0f);
		return false;
	}

	public static bool AnyEvents(Player player, bool checkBloodMoon = false)
	{
		if (Main.invasionType > 0 && Main.invasionProgressNearInvasion)
		{
			return true;
		}
		if (player.PillarZone())
		{
			return true;
		}
		if (DD2Event.Ongoing && player.ZoneOldOneArmy)
		{
			return true;
		}
		if ((player.ZoneOverworldHeight || player.ZoneSkyHeight) && (Main.eclipse || Main.pumpkinMoon || Main.snowMoon))
		{
			return true;
		}
		if (AcidRainEvent.AcidRainEventIsOngoing && player.InSulphur())
		{
			return true;
		}
		if (((player.ZoneOverworldHeight || player.ZoneSkyHeight) && Main.bloodMoon) & checkBloodMoon)
		{
			return true;
		}
		return false;
	}

	public static bool GetDownedBossVariable(int type)
	{
		switch (type)
		{
		case 50:
			return NPC.downedSlimeKing;
		case 4:
			return NPC.downedBoss1;
		case 13:
		case 14:
		case 15:
		case 266:
		case 267:
			return NPC.downedBoss2;
		case 222:
			return NPC.downedQueenBee;
		case 35:
			return NPC.downedBoss3;
		case 668:
			return NPC.downedDeerclops;
		case 113:
		case 114:
			return Main.hardMode;
		case 657:
			return NPC.downedQueenSlime;
		case 134:
		case 135:
		case 136:
			return NPC.downedMechBoss1;
		case 125:
		case 126:
			return NPC.downedMechBoss2;
		case 127:
			return NPC.downedMechBoss3;
		case 262:
			return NPC.downedPlantBoss;
		case 636:
			return NPC.downedEmpressOfLight;
		case 245:
		case 246:
			return NPC.downedGolemBoss;
		case 370:
			return NPC.downedFishron;
		case 439:
			return NPC.downedAncientCultist;
		case 396:
		case 397:
		case 398:
			return NPC.downedMoonlord;
		default:
			if (type == ModContent.NPCType<DesertScourgeHead>() || type == ModContent.NPCType<DesertScourgeBody>() || type == ModContent.NPCType<DesertScourgeTail>())
			{
				return DownedBossSystem.downedDesertScourge;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Crabulon.Crabulon>())
			{
				return DownedBossSystem.downedCrabulon;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.HiveMind.HiveMind>())
			{
				return DownedBossSystem.downedHiveMind;
			}
			if (type == ModContent.NPCType<PerforatorHive>())
			{
				return DownedBossSystem.downedPerforator;
			}
			if (type == ModContent.NPCType<SlimeGodCore>())
			{
				return DownedBossSystem.downedSlimeGod;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Cryogen.Cryogen>())
			{
				return DownedBossSystem.downedCryogen;
			}
			if (type == ModContent.NPCType<AquaticScourgeHead>() || type == ModContent.NPCType<AquaticScourgeBody>() || type == ModContent.NPCType<AquaticScourgeBodyAlt>() || type == ModContent.NPCType<AquaticScourgeTail>())
			{
				return DownedBossSystem.downedAquaticScourge;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.BrimstoneElemental.BrimstoneElemental>())
			{
				return DownedBossSystem.downedBrimstoneElemental;
			}
			if (type == ModContent.NPCType<CalamitasClone>())
			{
				return DownedBossSystem.downedCalamitasClone;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Leviathan.Leviathan>() || type == ModContent.NPCType<Anahita>())
			{
				return DownedBossSystem.downedLeviathan;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.AstrumAureus.AstrumAureus>())
			{
				return DownedBossSystem.downedAstrumAureus;
			}
			if (type == ModContent.NPCType<AstrumDeusHead>() || type == ModContent.NPCType<AstrumDeusBody>() || type == ModContent.NPCType<AstrumDeusTail>())
			{
				return DownedBossSystem.downedAstrumDeus;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.PlaguebringerGoliath.PlaguebringerGoliath>())
			{
				return DownedBossSystem.downedPlaguebringer;
			}
			if (type == ModContent.NPCType<RavagerBody>())
			{
				return DownedBossSystem.downedRavager;
			}
			if (type == ModContent.NPCType<ProfanedGuardianCommander>())
			{
				return DownedBossSystem.downedGuardians;
			}
			if (type == ModContent.NPCType<Dragonfolly>())
			{
				return DownedBossSystem.downedDragonfolly;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Providence.Providence>())
			{
				return DownedBossSystem.downedProvidence;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.CeaselessVoid.CeaselessVoid>() || type == ModContent.NPCType<DarkEnergy>())
			{
				return DownedBossSystem.downedCeaselessVoid;
			}
			if (type == ModContent.NPCType<StormWeaverHead>() || type == ModContent.NPCType<StormWeaverBody>() || type == ModContent.NPCType<StormWeaverTail>())
			{
				return DownedBossSystem.downedStormWeaver;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Signus.Signus>())
			{
				return DownedBossSystem.downedSignus;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Polterghast.Polterghast>())
			{
				return DownedBossSystem.downedPolterghast;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.OldDuke.OldDuke>())
			{
				return DownedBossSystem.downedBoomerDuke;
			}
			if (type == ModContent.NPCType<DevourerofGodsHead>() || type == ModContent.NPCType<DevourerofGodsBody>() || type == ModContent.NPCType<DevourerofGodsTail>())
			{
				return DownedBossSystem.downedDoG;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.Yharon.Yharon>())
			{
				return DownedBossSystem.downedYharon;
			}
			if (type == ModContent.NPCType<Artemis>() || type == ModContent.NPCType<Apollo>() || type == ModContent.NPCType<AresBody>() || type == ModContent.NPCType<AresGaussNuke>() || type == ModContent.NPCType<AresLaserCannon>() || type == ModContent.NPCType<AresPlasmaFlamethrower>() || type == ModContent.NPCType<AresTeslaCannon>() || type == ModContent.NPCType<ThanatosHead>() || type == ModContent.NPCType<ThanatosBody1>() || type == ModContent.NPCType<ThanatosBody2>() || type == ModContent.NPCType<ThanatosTail>())
			{
				return DownedBossSystem.downedExoMechs;
			}
			if (type == ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>())
			{
				return DownedBossSystem.downedCalamitas;
			}
			if (type == ModContent.NPCType<PrimordialWyrmHead>())
			{
				return DownedBossSystem.downedPrimordialWyrm;
			}
			return true;
		}
	}

	public static void SetNewBossJustDowned(NPC npc)
	{
		if (GetDownedBossVariable(npc.type))
		{
			return;
		}
		CalamityNPCSets.BossSpeedrunTimerID.TryGetValue(npc.type, out var newBossTypeJustDowned);
		for (int i = 0; i < 255; i++)
		{
			Player player = Main.player[i];
			if (player.active)
			{
				CalamityPlayer calamityPlayer = player.Calamity();
				calamityPlayer.lastSplitType = newBossTypeJustDowned;
				calamityPlayer.lastSplit = calamityPlayer.previousSessionTotal.Add(SpeedrunTimerSystem.Elapsed);
			}
		}
	}

	public static bool AnyLivingPlayers()
	{
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (!player.dead && !player.ghost)
			{
				return true;
			}
		}
		return false;
	}

	public static int GetActivePlayerCount()
	{
		if (Main.netMode == 0)
		{
			return 1;
		}
		return Main.CurrentFrameFlags.ActivePlayersCount;
	}

	public static bool ShouldAffectNPC(NPC target)
	{
		if (CalamityNPCTypeSets.EaterOfWorlds.Contains(target.type) || CalamityNPCTypeSets.Destroyer.Contains(target.type))
		{
			return false;
		}
		if (target.damage > 0 && !target.boss && !target.friendly && !target.dontTakeDamage && target.type != 267 && target.type != ModContent.NPCType<RavagerClawLeft>() && target.type != 325 && target.type != 344 && target.type != 346 && target.type != ModContent.NPCType<RavagerClawRight>() && target.type != ModContent.NPCType<ReaperShark>() && target.type != ModContent.NPCType<Mauler>() && target.type != ModContent.NPCType<EidolonWyrmHead>() && target.type != 247 && target.type != 248 && target.type != ModContent.NPCType<PrimordialWyrmHead>() && target.type != ModContent.NPCType<ColossalSquid>() && target.type != 551 && !CalamityNPCSets.ResistSlowingDebuffsAndOtherSpecialEffects[target.type] && !AcidRainEvent.AllMinibosses.Contains(target.type))
		{
			return true;
		}
		return false;
	}

	public static void OldDukeSpawn(int plr, int type, int baitType)
	{
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[plr];
		if (!player.active || player.dead)
		{
			return;
		}
		for (int m = 0; m < Main.maxProjectiles; m++)
		{
			Projectile projectile = Main.projectile[m];
			if (!projectile.active || !projectile.bobber || projectile.owner != plr)
			{
				continue;
			}
			if (plr != Main.myPlayer || projectile.ai[0] != 0f)
			{
				break;
			}
			for (int item = 0; item < 58; item++)
			{
				if (player.inventory[item].type == baitType)
				{
					player.inventory[item].stack--;
					if (player.inventory[item].stack <= 0)
					{
						player.inventory[item].SetDefaults(0, false, null);
					}
					break;
				}
			}
			projectile.ai[0] = 2f;
			projectile.netUpdate = true;
			if (Main.myPlayer != projectile.owner || !player.active || player.dead)
			{
				break;
			}
			Projectile proj = null;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				proj = p;
				if (p.bobber && p.owner == player.whoAmI)
				{
					break;
				}
			}
			if (proj != null)
			{
				int spawnPosX = (int)proj.Center.X;
				int spawnPosY = (int)proj.Center.Y + 100;
				if (Main.netMode == 0)
				{
					CalamityUtils.BossAwakenMessage(NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), spawnPosX, spawnPosY, ModContent.NPCType<global::CalamityMod.NPCs.OldDuke.OldDuke>()));
				}
				else if (Main.netMode == 1)
				{
					SpawnBossOnPositionPacket.Send(spawnPosX, spawnPosY, ModContent.NPCType<global::CalamityMod.NPCs.OldDuke.OldDuke>(), player);
				}
			}
			break;
		}
	}

	public static void DoHitDust(NPC npc, int hitDirection, int dustType = 5, float xSpeedMult = 1f, int numHitDust = 5, int numDeathDust = 20)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(npc.position, npc.width, npc.height, dustType, (float)hitDirection * xSpeedMult, -1f);
		}
		if (npc.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, dustType, (float)hitDirection * xSpeedMult, -1f);
			}
		}
	}

	public static void DoFlyingAI(NPC npc, float maxSpeed, float acceleration, float circleTime, float minDistanceTarget = 150f, bool shouldAttackTarget = true)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		if (npc.target < 0 || npc.target >= 255 || Main.player[npc.target].dead)
		{
			npc.TargetClosest();
		}
		Player obj = Main.player[npc.target];
		Vector2 toTarget = obj.Center - npc.Center;
		float distanceToTarget = ((Vector2)(ref toTarget)).Length();
		Vector2 maxVelocity = toTarget;
		if (distanceToTarget < 3f)
		{
			maxVelocity = npc.velocity;
		}
		else
		{
			float magnitude = maxSpeed / distanceToTarget;
			maxVelocity *= magnitude;
		}
		npc.ai[0]++;
		if (npc.ai[0] > circleTime * 0.5f)
		{
			npc.velocity.Y += acceleration;
		}
		else
		{
			npc.velocity.Y -= acceleration;
		}
		if (npc.ai[0] < circleTime * 0.25f || npc.ai[0] > circleTime * 0.75f)
		{
			npc.velocity.X += acceleration;
		}
		else
		{
			npc.velocity.X -= acceleration;
		}
		if (npc.ai[0] > circleTime)
		{
			npc.ai[0] = 0f;
		}
		if (shouldAttackTarget && distanceToTarget < minDistanceTarget)
		{
			npc.velocity += maxVelocity * 0.007f;
		}
		if (obj.dead)
		{
			maxVelocity.X = (float)npc.direction * maxSpeed / 2f;
			maxVelocity.Y = (0f - maxSpeed) / 2f;
		}
		if (npc.velocity.X < maxVelocity.X)
		{
			npc.velocity.X += acceleration;
		}
		if (npc.velocity.X > maxVelocity.X)
		{
			npc.velocity.X -= acceleration;
		}
		if (npc.velocity.Y < maxVelocity.Y)
		{
			npc.velocity.Y += acceleration;
		}
		if (npc.velocity.Y > maxVelocity.Y)
		{
			npc.velocity.Y -= acceleration;
		}
		if (!obj.dead)
		{
			npc.rotation = toTarget.ToRotation();
		}
		else
		{
			npc.rotation = npc.velocity.ToRotation();
		}
		npc.rotation += (float)Math.PI;
		float collisionDamp = 0.7f;
		if (npc.collideX)
		{
			npc.netUpdate = true;
			npc.velocity.X = npc.oldVelocity.X * (0f - collisionDamp);
			if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
			{
				npc.velocity.X = 2f;
			}
			if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
			{
				npc.velocity.X = -2f;
			}
		}
		if (npc.collideY)
		{
			npc.netUpdate = true;
			npc.velocity.Y = npc.oldVelocity.Y * (0f - collisionDamp);
			if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f)
			{
				npc.velocity.Y = 1.5f;
			}
			if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f)
			{
				npc.velocity.Y = -1.5f;
			}
		}
		if (npc.wet)
		{
			if (npc.velocity.Y > 0f)
			{
				npc.velocity.Y *= 0.95f;
			}
			npc.velocity.Y -= 0.3f;
			if (npc.velocity.Y < -2f)
			{
				npc.velocity.Y = -2f;
			}
		}
		if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
		{
			npc.netUpdate = true;
		}
	}

	public static void DoSpiderWallAI(NPC npc, int transformType, float chaseMaxSpeed = 2f, float chaseAcceleration = 0.08f)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
		{
			npc.TargetClosest();
		}
		Vector2 between = Main.player[npc.target].Center - npc.Center;
		float distance = ((Vector2)(ref between)).Length();
		if (distance == 0f)
		{
			between.X = npc.velocity.X;
			between.Y = npc.velocity.Y;
		}
		else
		{
			distance = chaseMaxSpeed / distance;
			between.X *= distance;
			between.Y *= distance;
		}
		if (Main.player[npc.target].dead)
		{
			between.X = (float)npc.direction * chaseMaxSpeed / 2f;
			between.Y = (0f - chaseMaxSpeed) / 2f;
		}
		npc.spriteDirection = -1;
		if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
		{
			npc.ai[0]++;
			if (npc.ai[0] > 0f)
			{
				npc.velocity.Y += 0.023f;
			}
			else
			{
				npc.velocity.Y -= 0.023f;
			}
			if (npc.ai[0] < -100f || npc.ai[0] > 100f)
			{
				npc.velocity.X += 0.023f;
			}
			else
			{
				npc.velocity.X -= 0.023f;
			}
			if (npc.ai[0] > 200f)
			{
				npc.ai[0] = -200f;
			}
			npc.velocity.X += between.X * 0.007f;
			npc.velocity.Y += between.Y * 0.007f;
			npc.rotation = npc.velocity.ToRotation();
			if (npc.velocity.X > 1.5f)
			{
				npc.velocity.X *= 0.9f;
			}
			if (npc.velocity.X < -1.5f)
			{
				npc.velocity.X *= 0.9f;
			}
			if (npc.velocity.Y > 1.5f)
			{
				npc.velocity.Y *= 0.9f;
			}
			if (npc.velocity.Y < -1.5f)
			{
				npc.velocity.Y *= 0.9f;
			}
			npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -3f, 3f);
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -3f, 3f);
		}
		else
		{
			if (npc.velocity.X < between.X)
			{
				npc.velocity.X += chaseAcceleration;
				if (npc.velocity.X < 0f && between.X > 0f)
				{
					npc.velocity.X += chaseAcceleration;
				}
			}
			else if (npc.velocity.X > between.X)
			{
				npc.velocity.X -= chaseAcceleration;
				if (npc.velocity.X > 0f && between.X < 0f)
				{
					npc.velocity.X -= chaseAcceleration;
				}
			}
			if (npc.velocity.Y < between.Y)
			{
				npc.velocity.Y += chaseAcceleration;
				if (npc.velocity.Y < 0f && between.Y > 0f)
				{
					npc.velocity.Y += chaseAcceleration;
				}
			}
			else if (npc.velocity.Y > between.Y)
			{
				npc.velocity.Y -= chaseAcceleration;
				if (npc.velocity.Y > 0f && between.Y < 0f)
				{
					npc.velocity.Y -= chaseAcceleration;
				}
			}
			npc.rotation = between.ToRotation();
		}
		float collisionDamp = 0.5f;
		if (npc.collideX)
		{
			npc.netUpdate = true;
			npc.velocity.X = npc.oldVelocity.X * (0f - collisionDamp);
			if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
			{
				npc.velocity.X = 2f;
			}
			if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
			{
				npc.velocity.X = -2f;
			}
		}
		if (npc.collideY)
		{
			npc.netUpdate = true;
			npc.velocity.Y = npc.oldVelocity.Y * (0f - collisionDamp);
			if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f)
			{
				npc.velocity.Y = 2f;
			}
			if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f)
			{
				npc.velocity.Y = -2f;
			}
		}
		if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
		{
			npc.netUpdate = true;
		}
		if (Main.netMode == 1)
		{
			return;
		}
		int x = (int)npc.Center.X / 16;
		int y = (int)npc.Center.Y / 16;
		bool flag = false;
		for (int i = x - 1; i <= x + 1; i++)
		{
			for (int j = y - 1; j <= y + 1; j++)
			{
				if (Main.tile[i, j].WallType > 0)
				{
					flag = true;
				}
			}
		}
		if (!flag)
		{
			npc.Transform(transformType);
		}
	}

	public static void DoVultureAI(NPC npc, float acceleration = 0.1f, float maxSpeed = 3f, int sitWidth = 30, int flyWidth = 50, int rangeX = 100, int rangeY = 100)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		npc.localAI[0]++;
		npc.noGravity = true;
		npc.TargetClosest();
		if (npc.ai[0] == 0f)
		{
			npc.width = sitWidth;
			npc.noGravity = false;
			if (Main.netMode != 1)
			{
				if (npc.velocity.X != 0f || npc.velocity.Y < 0f || (double)npc.velocity.Y > 0.3)
				{
					npc.ai[0] = 1f;
					npc.netUpdate = true;
				}
				else
				{
					Rectangle playerRect = Main.player[npc.target].getRect();
					Rectangle rangeRect = default(Rectangle);
					((Rectangle)(ref rangeRect))._002Ector((int)npc.Center.X - rangeX, (int)npc.Center.Y - rangeY, rangeX * 2, rangeY * 2);
					if (npc.localAI[0] > 20f && (((Rectangle)(ref rangeRect)).Intersects(playerRect) || npc.life < npc.lifeMax))
					{
						npc.ai[0] = 1f;
						npc.velocity.Y -= 6f;
						npc.netUpdate = true;
					}
				}
			}
		}
		else if (!Main.player[npc.target].dead)
		{
			npc.width = flyWidth;
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.5f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
				{
					npc.velocity.Y = 1f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
				{
					npc.velocity.Y = -1f;
				}
			}
			if (npc.direction == -1 && npc.velocity.X > 0f - maxSpeed)
			{
				npc.velocity.X -= acceleration;
				if (npc.velocity.X > maxSpeed)
				{
					npc.velocity.X -= acceleration;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.velocity.X -= acceleration * 0.5f;
				}
				if (npc.velocity.X < 0f - maxSpeed)
				{
					npc.velocity.X = 0f - maxSpeed;
				}
			}
			else if (npc.direction == 1 && npc.velocity.X < maxSpeed)
			{
				npc.velocity.X += acceleration;
				if (npc.velocity.X < 0f - maxSpeed)
				{
					npc.velocity.X += acceleration;
				}
				else if (npc.velocity.X < 0f)
				{
					npc.velocity.X += acceleration * 0.5f;
				}
				if (npc.velocity.X > maxSpeed)
				{
					npc.velocity.X = maxSpeed;
				}
			}
			float num = Math.Abs(npc.Center.X - Main.player[npc.target].Center.X);
			float yLimiter = Main.player[npc.target].position.Y - (float)npc.height / 2f;
			if (num > 50f)
			{
				yLimiter -= 100f;
			}
			if (npc.position.Y < yLimiter)
			{
				npc.velocity.Y += acceleration * 0.5f;
				if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y += acceleration * 0.1f;
				}
			}
			else
			{
				npc.velocity.Y -= acceleration * 0.5f;
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y -= acceleration * 0.1f;
				}
			}
			if (npc.velocity.Y < 0f - maxSpeed)
			{
				npc.velocity.Y = 0f - maxSpeed;
			}
			if (npc.velocity.Y > maxSpeed)
			{
				npc.velocity.Y = maxSpeed;
			}
		}
		if (npc.wet)
		{
			if (npc.velocity.Y > 0f)
			{
				npc.velocity.Y *= 0.95f;
			}
			npc.velocity.Y -= 0.5f;
			if (npc.velocity.Y < -4f)
			{
				npc.velocity.Y = -4f;
			}
		}
	}

	public static Dust SpawnDustOnNPC(NPC npc, int frameWidth, int frameHeight, int dustType, Rectangle rect, Vector2 velocity = default(Vector2), float chance = 0.5f, bool useSpriteDirection = false)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		Vector2 half = default(Vector2);
		((Vector2)(ref half))._002Ector((float)frameWidth / 2f, (float)frameHeight / 2f);
		if ((!useSpriteDirection && npc.direction == 1) || (useSpriteDirection && npc.spriteDirection == 1))
		{
			rect.X = frameWidth - ((Rectangle)(ref rect)).Right;
		}
		if (Main.rand.NextFloat(1f) < chance)
		{
			Vector2 offset = npc.Center - half + new Vector2(Main.rand.NextFloat(((Rectangle)(ref rect)).Left, ((Rectangle)(ref rect)).Right), Main.rand.NextFloat(((Rectangle)(ref rect)).Top, ((Rectangle)(ref rect)).Bottom)) - npc.Center;
			offset = offset.RotatedBy(npc.rotation);
			return Dust.NewDustPerfect(npc.Center + offset, dustType, velocity);
		}
		return null;
	}

	public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		switch (npc.netID)
		{
		case 2:
		case 4:
		case 20:
		case 22:
		case 23:
		case 27:
		case 29:
		case 31:
		case 35:
		case 39:
		case 44:
		case 48:
		case 62:
		case 66:
		case 68:
		case 82:
		case 87:
		case 95:
		case 98:
		case 113:
		case 120:
		case 124:
		case 125:
		case 126:
		case 127:
		case 134:
		case 156:
		case 167:
		case 169:
		case 190:
		case 191:
		case 192:
		case 193:
		case 194:
		case 206:
		case 216:
		case 245:
		case 262:
		case 266:
		case 269:
		case 270:
		case 271:
		case 272:
		case 273:
		case 274:
		case 275:
		case 276:
		case 277:
		case 278:
		case 279:
		case 280:
		case 281:
		case 282:
		case 283:
		case 284:
		case 285:
		case 286:
		case 287:
		case 290:
		case 291:
		case 292:
		case 293:
		case 294:
		case 295:
		case 296:
		case 370:
		case 395:
		case 398:
		case 399:
		case 422:
		case 438:
		case 439:
		case 471:
		case 481:
		case 482:
		case 483:
		case 493:
		case 507:
		case 510:
		case 517:
		case 532:
		case 533:
		case 541:
		case 542:
		case 543:
		case 544:
		case 545:
		case 618:
		case 636:
		case 657:
		case 661:
		case 664:
		{
			FlavorTextBestiaryInfoElement f = new FlavorTextBestiaryInfoElement("Hi CS0120");
			bestiaryEntry.Info.RemoveAll((IBestiaryInfoElement i) => i.GetType() == f.GetType());
			bestiaryEntry.Info.Add(new FlavorTextBestiaryInfoElement(CalamityUtils.GetTextValue("Bestiary.Vanilla." + Lang.GetNPCName(npc.netID).Key)));
			break;
		}
		}
		string[] elements = new string[5]
		{
			NPCDebuffResistText(npc.Calamity().VulnerableToCold, CalamityUtils.GetTextValue("UI.DebuffSystem.Cold")),
			NPCDebuffResistText(npc.Calamity().VulnerableToElectricity, CalamityUtils.GetTextValue("UI.DebuffSystem.Electricity")),
			NPCDebuffResistText(npc.Calamity().VulnerableToHeat, CalamityUtils.GetTextValue("UI.DebuffSystem.Heat")),
			NPCDebuffResistText(npc.Calamity().VulnerableToSickness, CalamityUtils.GetTextValue("UI.DebuffSystem.Sickness")),
			NPCDebuffResistText(npc.Calamity().VulnerableToWater, CalamityUtils.GetTextValue("UI.DebuffSystem.Water"))
		};
		bool force = npc.type == ModContent.NPCType<Burrower>();
		bestiaryEntry.Info.Insert(0, new BestiaryDebuffInfo(elements, force));
		if (npc.type == 484)
		{
			bestiaryEntry.AddTags(ModContent.GetInstance<AstralInfectionBiome>().ModBiomeBestiaryInfoElement);
		}
		if (npc.type == 374)
		{
			bestiaryEntry.AddTags(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom);
		}
		switch (npc.netID)
		{
		case 7:
		case 10:
		case 13:
		case 39:
		case 87:
		case 95:
		case 98:
		case 117:
		case 134:
		case 402:
		case 412:
		case 454:
		case 510:
		case 513:
		case 621:
		{
			Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> nPCBestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
			int type = npc.type;
			NPCID.Sets.NPCBestiaryDrawModifiers value = NPCID.Sets.NPCBestiaryDrawOffset[npc.type];
			value.CustomTexturePath = null;
			nPCBestiaryDrawOffset[type] = value;
			break;
		}
		}
	}

	public static string NPCDebuffResistText(bool? effectiveness, string name)
	{
		string result = CalamityUtils.GetTextValue("UI.DebuffSystem.Neutral");
		if (effectiveness == true)
		{
			result = CalamityUtils.GetTextValue("UI.DebuffSystem.Weak");
		}
		else if (effectiveness == false)
		{
			result = CalamityUtils.GetTextValue("UI.DebuffSystem.Resistant");
		}
		return result + " " + CalamityUtils.GetTextValue("UI.DebuffSystem.To") + " " + name;
	}

	public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
	{
		LeadingConditionRule rev = npcLoot.DefineConditionalDropSet(DropHelper.RevNoMaster);
		LeadingConditionRule GFB = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		LeadingConditionRule pMoon = new LeadingConditionRule(new Conditions.PumpkinMoonDropGatingChance());
		LeadingConditionRule fMoon = new LeadingConditionRule(new Conditions.FrostMoonDropGatingChance());
		LeadingConditionRule postEoC = npcLoot.DefineConditionalDropSet(DropHelper.PostEoC());
		LeadingConditionRule hardmode = npcLoot.DefineConditionalDropSet(DropHelper.Hardmode());
		LeadingConditionRule postCal = npcLoot.DefineConditionalDropSet(DropHelper.PostCal());
		LeadingConditionRule postLevi = npcLoot.DefineConditionalDropSet(DropHelper.PostLevi());
		LeadingConditionRule postDoG = npcLoot.DefineConditionalDropSet(DropHelper.PostDoG());
		switch (npc.type)
		{
		case 55:
		case 230:
			npcLoot.Add(ModContent.ItemType<PineapplePet>(), 500);
			break;
		case 104:
			npcLoot.ChangeDropRate(485, 1, 20);
			break;
		case 48:
			postEoC.Add(ModContent.ItemType<SkyGlaze>(), 30);
			hardmode.AddIf(() => !npc.SpawnedFromStatue, ModContent.ItemType<EssenceofSunlight>(), 2);
			break;
		case 250:
			npcLoot.Add(ModContent.ItemType<EssenceofSunlight>(), 2);
			break;
		case 87:
			npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<EssenceofSunlight>(), 1, 8, 10, 10, 12));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AeroStone>(), 4, 3));
			break;
		case 496:
		case 497:
			hardmode.Add(ModContent.ItemType<GiantShell>());
			hardmode.OnFailedConditions(ItemDropRule.NormalvsExpert(ModContent.ItemType<GiantShell>(), 7, 4));
			break;
		case 494:
		case 495:
			hardmode.Add(ModContent.ItemType<CrawCarapace>());
			hardmode.OnFailedConditions(ItemDropRule.NormalvsExpert(ModContent.ItemType<CrawCarapace>(), 7, 4));
			break;
		case 480:
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is CommonDrop commonDrop && commonDrop.itemId == 3781);
			npcLoot.Add(ItemDropRule.NormalvsExpert(3781, 14, 7));
			break;
		case 45:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 3, 2));
			break;
		case 110:
			npcLoot.ChangeDropRate(1321, 1, 20);
			npcLoot.ChangeDropRate(682, 1, 50);
			break;
		case 77:
			npcLoot.ChangeDropRate(723, 1, 50);
			break;
		case 85:
			try
			{
				npcLoot.RemoveWhere((IItemDropRule rule) => rule is OneFromOptionsDropRule oneFromOptionsDropRule && oneFromOptionsDropRule.dropIds[0] == 437);
				int[] normalMimicItems = new int[6] { 517, 554, 535, 532, 536, 437 };
				int[] remixPreHardMimicItems = new int[6] { 49, 50, 53, 54, 975, 5011 };
				int[] remixHardmodeMimicItems = new int[6] { 3069, 554, 535, 532, 536, 437 };
				LeadingConditionRule notRemix = npcLoot.DefineConditionalDropSet(DropHelper.If(() => !npc.SpawnedFromStatue && !Main.remixWorld));
				LeadingConditionRule remixPreHM = npcLoot.DefineConditionalDropSet(DropHelper.If(() => !npc.SpawnedFromStatue && Main.remixWorld && !Main.hardMode));
				LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.If(() => !npc.SpawnedFromStatue && Main.remixWorld && Main.hardMode));
				notRemix.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, normalMimicItems));
				remixPreHM.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, remixPreHardMimicItems));
				mainRule.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, remixHardmodeMimicItems));
			}
			catch (ArgumentNullException)
			{
			}
			break;
		case 69:
			npcLoot.Add(ModContent.ItemType<AntlionSkewer>(), 20);
			break;
		case 513:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BurntSienna>(), 25, 15));
			break;
		case 533:
			npcLoot.Add(ItemDropRule.NormalvsExpert(891, 100, 50));
			break;
		case 527:
			npcLoot.Add(ItemDropRule.NormalvsExpert(893, 100, 50));
			npcLoot.ChangeDropRate(528, 1, 10);
			break;
		case 545:
			npcLoot.ChangeDropRate(528, 1, 10);
			break;
		case 541:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ElementalinaBottle>(), 5, 3));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RareElementalinaBottle>(), 10, 6));
			npcLoot.Add(4714, 10);
			break;
		case 525:
		case 526:
		case 543:
		case 544:
			npcLoot.ChangeDropRate(527, 1, 10);
			break;
		case 150:
			npcLoot.Add(ModContent.ItemType<FrostyBatBottle>(), 14);
			break;
		case 197:
			npcLoot.Add(ItemDropRule.NormalvsExpert(886, 100, 50));
			break;
		case 155:
		case 169:
		case 206:
			npcLoot.Add(ModContent.ItemType<EssenceofEleum>());
			break;
		case 154:
			npcLoot.Add(ModContent.ItemType<EssenceofEleum>());
			foreach (IItemDropRule item in npcLoot.Get(includeGlobalDrops: false))
			{
				if (item is CommonDrop { itemId: 1253 } drop)
				{
					drop.chanceDenominator = 20;
					return;
				}
			}
			break;
		case 629:
			try
			{
				npcLoot.RemoveWhere((IItemDropRule rule) => rule is OneFromOptionsDropRule oneFromOptionsDropRule && oneFromOptionsDropRule.dropIds[0] == 676);
				npcLoot.RemoveWhere((IItemDropRule rule) => rule is CommonDrop commonDrop && commonDrop.itemId == 1312);
				int[] normalIceMimicItems = new int[3] { 676, 725, 1264 };
				int[] remixPreHardIceMimicItems = new int[6] { 670, 724, 725, 950, 987, 1579 };
				int[] remixHardmodeIceMimicItems = new int[3] { 676, 1319, 1264 };
				LeadingConditionRule notRemix3 = npcLoot.DefineConditionalDropSet(DropHelper.If(() => !npc.SpawnedFromStatue && !Main.remixWorld));
				LeadingConditionRule remixPreHM2 = npcLoot.DefineConditionalDropSet(DropHelper.If(() => !npc.SpawnedFromStatue && Main.remixWorld && !Main.hardMode));
				LeadingConditionRule mainRule3 = npcLoot.DefineConditionalDropSet(DropHelper.If(() => !npc.SpawnedFromStatue && Main.remixWorld && Main.hardMode));
				notRemix3.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, normalIceMimicItems));
				remixPreHM2.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, remixPreHardIceMimicItems));
				mainRule3.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, remixHardmodeIceMimicItems));
				npcLoot.DefineConditionalDropSet(new Conditions.NotFromStatue()).Add(1312, 20);
			}
			catch (ArgumentNullException)
			{
			}
			break;
		case 243:
			npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<EssenceofEleum>(), 1, 8, 10, 10, 12));
			npcLoot.Add(1537, 10);
			break;
		case 64:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<LifeJelly>(), 10, 7));
			npcLoot.ChangeDropRate(1303, 1, 30);
			break;
		case 63:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CleansingJelly>(), 10, 7));
			npcLoot.ChangeDropRate(1303, 1, 30);
			break;
		case 103:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<VitalJelly>(), 8, 5));
			npcLoot.ChangeDropRate(1303, 1, 30);
			break;
		case 58:
		case 157:
		case 241:
			npcLoot.Add(ItemDropRule.NormalvsExpert(885, 100, 50));
			break;
		case 65:
			npcLoot.Add(ItemDropRule.NormalvsExpert(3212, 25, 15));
			npcLoot.Add(ModContent.ItemType<JoyfulHeart>(), 20);
			npcLoot.Add(ModContent.ItemType<SharkyPlush>(), 100);
			break;
		case 242:
		case 256:
			npcLoot.Add(1303, 30);
			break;
		case 7:
		case 181:
			npcLoot.Add(ItemDropRule.NormalvsExpert(892, 100, 50));
			break;
		case 120:
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is CommonDrop commonDrop && commonDrop.itemId == 1326);
			npcLoot.Add(ItemDropRule.NormalvsExpert(1326, 100, 50));
			break;
		case 137:
			npcLoot.Add(ItemDropRule.NormalvsExpert(893, 100, 50));
			break;
		case 122:
			npcLoot.Add(3111, 1, 5, 10);
			break;
		case 101:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CursedDagger>(), 25, 15));
			break;
		case 268:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<IchorSpear>(), 25, 15));
			break;
		case 473:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 7, 4));
			npcLoot.Add(1534, 10);
			break;
		case 474:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 7, 4));
			npcLoot.Add(1535, 10);
			break;
		case 475:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 7, 4));
			npcLoot.Add(1536, 10);
			break;
		case 98:
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is CommonDrop commonDrop && commonDrop.itemId == 522);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => !CalamityWorld.death, () => !CalamityWorld.death)).Add(522, 1, 2, 5);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.death, () => CalamityWorld.death)).Add(522, 1, 6, 15);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.death, () => CalamityWorld.death, CalamityUtils.GetTextValue("Condition.Drops.IsDeath"))).Add(521, 1, 4, 8);
			break;
		case 99:
		case 100:
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is CommonDrop commonDrop && commonDrop.itemId == 522);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => !CalamityWorld.death, () => !CalamityWorld.death)).Add(522, 1, 2, 5);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.death, () => CalamityWorld.death)).Add(522, 1, 6, 15);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.death, () => CalamityWorld.death, CalamityUtils.GetTextValue("Condition.Drops.IsDeath"))).Add(521, 1, 4, 8);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.death, ui: false)).Add(996, 200);
			npcLoot.DefineConditionalDropSet(DropHelper.If(() => CalamityWorld.death && Main.WindyEnoughForKiteDrops, ui: false)).Add(4611, 25);
			break;
		case 176:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Needler>(), 25, 15));
			break;
		case 205:
			npcLoot.ChangeDropRate(1611, 1, 1);
			break;
		case 476:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 7, 4));
			break;
		case 198:
		case 199:
		case 226:
			npcLoot.Add(1533, 150);
			break;
		case 32:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ShinobiBlade>(), 15, 10));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<StaffOfNecrosteocytes>(), 15, 10));
			break;
		case 269:
		case 270:
		case 271:
		case 272:
		case 273:
		case 274:
		case 275:
		case 276:
		case 277:
		case 278:
		case 279:
		case 280:
			npcLoot.ChangeDropRate(1183, 1, 200);
			break;
		case 289:
			postLevi.Add(ModContent.ItemType<Keelhaul>(), 15);
			break;
		case 287:
			npcLoot.ChangeDropRate(963, 1, 4);
			npcLoot.ChangeDropRate(977, 1, 4);
			break;
		case 283:
		case 284:
			npcLoot.Add(ItemDropRule.NormalvsExpert(889, 100, 50));
			break;
		case 290:
			npcLoot.ChangeDropRate(1513, 3, 20);
			npcLoot.ChangeDropRate(938, 1, 5);
			break;
		case 60:
			npcLoot.Add(ModContent.ItemType<ToastyBatBottle>(), 30);
			npcLoot.ChangeDropRate(1322, 1, 75);
			break;
		case 151:
			npcLoot.ChangeDropRate(1322, 1, 30);
			break;
		case 24:
			npcLoot.AddIf((DropAttemptInfo info) => !NPC.downedBoss2, ModContent.ItemType<AshenStalactite>(), 25);
			npcLoot.AddIf((DropAttemptInfo info) => NPC.downedBoss2, ModContent.ItemType<AshenStalactite>(), 7);
			break;
		case 62:
		case 66:
			npcLoot.AddIf((DropAttemptInfo info) => !NPC.downedBoss2, ModContent.ItemType<BladecrestOathsword>(), 50);
			npcLoot.AddIf((DropAttemptInfo info) => NPC.downedBoss2, ModContent.ItemType<BladecrestOathsword>(), 15);
			npcLoot.ChangeDropRate(272, 1, 20);
			break;
		case 39:
			npcLoot.AddIf((DropAttemptInfo info) => !NPC.downedBoss2, ModContent.ItemType<OldLordClaymore>(), 25);
			npcLoot.AddIf((DropAttemptInfo info) => NPC.downedBoss2, ModContent.ItemType<OldLordClaymore>(), 4);
			break;
		case 156:
			npcLoot.ChangeDropRate(1518, 1, 10);
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Abaddon>(), 12, 7));
			npcLoot.Add(ModContent.ItemType<EssenceofHavoc>(), 2);
			break;
		case 632:
			postEoC.Add(ModContent.ItemType<BloodOrb>(), 5);
			break;
		case 53:
		case 536:
			postEoC.Add(ModContent.ItemType<BloodOrb>(), 1, 3, 6);
			break;
		case 316:
			npcLoot.Add(ModContent.ItemType<GhostBracelet>(), 20);
			break;
		case 489:
		case 490:
			postEoC.Add(ModContent.ItemType<BloodOrb>(), 4);
			break;
		case 109:
			npcLoot.Add(ModContent.ItemType<BloodOrb>(), 1, 6, 12);
			break;
		case 586:
		case 587:
			npcLoot.Add(ModContent.ItemType<BouncingEyeball>(), 8);
			postEoC.Add(ModContent.ItemType<BloodOrb>(), 1, 10, 12);
			break;
		case 620:
		case 621:
			npcLoot.Add(ModContent.ItemType<BloodOrb>(), 1, 40, 48);
			break;
		case 618:
			npcLoot.Add(ModContent.ItemType<BloodOrb>(), 1, 100, 120);
			npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedDreadnautilus, ModContent.ItemType<LoreBloodMoon>(), ui: true, DropHelper.FirstKillText);
			break;
		case 29:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 25, 15));
			break;
		case 471:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BurningStrife>(), 5, 3));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TheFirstShadowflame>(), 5, 3));
			break;
		case 564:
			rev.Add(4796, 4);
			break;
		case 565:
			rev.Add(4946);
			rev.Add(4796, 4);
			break;
		case 577:
			rev.Add(4947);
			rev.Add(4816, 4);
			break;
		case 143:
		case 144:
		case 145:
			npcLoot.Add(ModContent.ItemType<EssenceofEleum>(), 5);
			break;
		case 214:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MidasPrime>(), 25, 15));
			break;
		case 491:
			rev.Add(4940);
			rev.Add(4792, 4);
			break;
		case 162:
		case 166:
		case 461:
		case 462:
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 10);
			break;
		case 460:
		case 468:
		case 469:
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 2);
			break;
		case 253:
		case 466:
			postCal.Add(ModContent.ItemType<SolarVeil>(), 2, 2, 4);
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 2);
			break;
		case 158:
		case 159:
			npcLoot.ChangeDropRate(900, 3, 20);
			npcLoot.Add(ItemDropRule.NormalvsExpert(1800, 40, 20));
			postCal.Add(ModContent.ItemType<SolarVeil>(), 2, 2, 4);
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 2);
			break;
		case 251:
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 1, 1, 2);
			break;
		case 463:
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 1, 3, 5);
			break;
		case 477:
		{
			LeadingConditionRule postMechs = new LeadingConditionRule(new Conditions.DownedAllMechBosses());
			postMechs.OnFailedConditions(ItemDropRule.ExpertGetsRerolls(1570, 4, 1), hideLootReport: true);
			postMechs.OnFailedConditions(ItemDropRule.ExpertGetsRerolls(2770, 20, 1), hideLootReport: true);
			postMechs.OnFailedConditions(ItemDropRule.ExpertGetsRerolls(3292, 3, 1), hideLootReport: true);
			npcLoot.Add(postMechs);
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 1, 20, 30);
			break;
		}
		case 467:
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DefectiveSphere>(), 25, 15));
			postDoG.Add(ModContent.ItemType<DarksunFragment>(), 2);
			break;
		case 326:
			postDoG.Add(ModContent.ItemType<NightmareFuel>(), 2);
			break;
		case 329:
		case 330:
			postDoG.Add(ModContent.ItemType<NightmareFuel>(), 2, 1, 2);
			break;
		case 315:
			postDoG.Add(ModContent.ItemType<NightmareFuel>(), 1, 3, 5);
			break;
		case 325:
			postDoG.Add(ModContent.ItemType<NightmareFuel>(), 1, 5, 10);
			pMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4941));
			pMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4793, 4));
			npcLoot.Add(pMoon);
			break;
		case 327:
			postDoG.Add(ModContent.ItemType<NightmareFuel>(), 1, 10, 20);
			pMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4942));
			pMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4812, 4));
			npcLoot.Add(pMoon);
			break;
		case 347:
		case 348:
		case 349:
		case 352:
			postDoG.Add(ModContent.ItemType<EndothermicEnergy>(), 2);
			break;
		case 341:
		case 343:
		case 351:
			postDoG.Add(ModContent.ItemType<EndothermicEnergy>(), 2, 1, 2);
			break;
		case 344:
			postDoG.Add(ModContent.ItemType<EndothermicEnergy>(), 1, 3, 5);
			fMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4944));
			fMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4813, 4));
			npcLoot.Add(fMoon);
			break;
		case 346:
			postDoG.Add(ModContent.ItemType<EndothermicEnergy>(), 1, 5, 10);
			fMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4945));
			fMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4794, 4));
			npcLoot.Add(fMoon);
			break;
		case 345:
			postDoG.Add(ModContent.ItemType<EndothermicEnergy>(), 1, 10, 20);
			fMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4943));
			fMoon.OnSuccess(ItemDropRule.ByCondition(DropHelper.RevNoMaster, 4814, 4));
			npcLoot.Add(fMoon);
			break;
		case 381:
		case 382:
		case 385:
		case 389:
		case 390:
			npcLoot.Add(ModContent.ItemType<DoomsdayDevice>(), 100);
			npcLoot.Add(ModContent.ItemType<Wingman>(), 100);
			npcLoot.Add(ModContent.ItemType<NullificationPistol>(), 100);
			break;
		case 386:
			npcLoot.Add(ModContent.ItemType<Wingman>(), 10);
			break;
		case 383:
			npcLoot.Add(ModContent.ItemType<DoomsdayDevice>(), 12);
			break;
		case 520:
			npcLoot.Add(ModContent.ItemType<NullificationPistol>(), 8);
			break;
		case 395:
			try
			{
				npcLoot.RemoveWhere((IItemDropRule rule) => rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsNotScaledWithLuckDropRule && oneFromOptionsNotScaledWithLuckDropRule.dropIds[0] == 2797);
				int[] saucerItems = new int[5] { 2797, 2749, 2795, 2796, 2880 };
				npcLoot.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, saucerItems));
				npcLoot.Add(2769, 4);
			}
			catch (ArgumentNullException)
			{
			}
			rev.Add(4939);
			rev.Add(4815, 4);
			break;
		case 412:
		case 415:
		case 416:
		case 417:
		case 418:
		case 419:
		case 518:
			npcLoot.Add(ItemDropRule.NormalvsExpert(3458, 5, 4));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MeldBlob>(), 5, 4));
			break;
		case 425:
		case 426:
		case 427:
		case 428:
		case 429:
			npcLoot.Add(ItemDropRule.NormalvsExpert(3456, 5, 4));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MeldBlob>(), 5, 4));
			break;
		case 420:
		case 421:
		case 423:
		case 424:
			npcLoot.Add(ItemDropRule.NormalvsExpert(3457, 5, 4));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MeldBlob>(), 5, 4));
			break;
		case 402:
		case 405:
		case 407:
		case 409:
		case 411:
			npcLoot.Add(ItemDropRule.NormalvsExpert(3459, 5, 4));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MeldBlob>(), 5, 4));
			break;
		case 54:
			hardmode.Add(ModContent.ItemType<ClothiersWrath>());
			break;
		case 17:
		{
			LeadingConditionRule morshuLCR = new LeadingConditionRule(DropHelper.If((DropAttemptInfo info) => info.npc.GivenName == "Morshu", ui: false));
			morshuLCR.Add(23, 1, 20, 30, hideLootReport: true);
			morshuLCR.Add(965, 1, 30, 50, hideLootReport: true);
			morshuLCR.Add(166, 1, 10, 15, hideLootReport: true);
			npcLoot.Add(morshuLCR);
			break;
		}
		case 369:
			hardmode.Add(2294, 12);
			break;
		case 50:
		{
			DropOneByOne.Parameters kingSlimeGelSpray = new DropOneByOne.Parameters
			{
				ChanceNumerator = 1,
				ChanceDenominator = 1,
				MinimumStackPerChunkBase = 4,
				MaximumStackPerChunkBase = 4,
				MinimumItemDropsCount = 18,
				MaximumItemDropsCount = 25
			};
			npcLoot.Add(new DropOneByOne(23, kingSlimeGelSpray));
			npcLoot.AddNormalOnly(ItemDropRule.ByCondition(DropHelper.Remix, 671, 4));
			npcLoot.AddNormalOnly(ItemDropRule.ByCondition(DropHelper.NotRemix, 2273, 4));
			npcLoot.AddNormalOnly(ModContent.ItemType<CrownJewel>(), DropHelper.NormalWeaponDropRateFraction);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4929);
			rev.Add(4797, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<AureusCell>(), 1, 45, 55));
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedSlimeKing, ModContent.ItemType<LoreKingSlime>(), ui: true, DropHelper.FirstKillText);
			break;
		}
		case 4:
			npcLoot.AddNormalOnly(ModContent.ItemType<TeardropCleaver>(), DropHelper.NormalWeaponDropRateFraction);
			npcLoot.AddNormalOnly(ModContent.ItemType<DeathstareRod>(), DropHelper.NormalWeaponDropRateFraction);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4924);
			rev.Add(4798, 4);
			GFB.Add(DropHelper.PerPlayer(2535), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedBoss1, ModContent.ItemType<LoreEyeofCthulhu>(), ui: true, DropHelper.FirstKillText);
			break;
		case 13:
		case 14:
		case 15:
		{
			LeadingConditionRule EoWKill = new LeadingConditionRule(DropHelper.If((DropAttemptInfo info) => info.npc.boss));
			EoWKill.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			npcLoot.AddNormalOnly(EoWKill);
			rev.AddIf((DropAttemptInfo info) => info.npc.boss, 4925);
			rev.AddIf((DropAttemptInfo info) => info.npc.boss, 4799, 4);
			LeadingConditionRule EoWKillGFB = new LeadingConditionRule(DropHelper.If((DropAttemptInfo info) => info.npc.boss));
			EoWKillGFB.Add(DropHelper.PerPlayer(561), hideLootReport: true);
			npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(EoWKillGFB);
			LeadingConditionRule eowCorruptionLore = new LeadingConditionRule(DropHelper.If((DropAttemptInfo info) => info.npc.boss && (!WorldGen.crimson || WorldGen.drunkWorldGen) && !NPC.downedBoss2, ui: true, DropHelper.FirstKillText));
			eowCorruptionLore.Add(ModContent.ItemType<LoreCorruption>(), 1, 1, 1, WorldGen.crimson && !WorldGen.drunkWorldGen);
			eowCorruptionLore.Add(ModContent.ItemType<LoreEaterofWorlds>(), 1, 1, 1, WorldGen.crimson && !WorldGen.drunkWorldGen);
			npcLoot.Add(eowCorruptionLore);
			LeadingConditionRule eowCrimsonLore = new LeadingConditionRule(DropHelper.If((DropAttemptInfo info) => info.npc.boss && (WorldGen.crimson || WorldGen.drunkWorldGen) && !NPC.downedBoss2, ui: true, DropHelper.FirstKillText));
			eowCrimsonLore.Add(ModContent.ItemType<LoreCrimson>(), 1, 1, 1, !WorldGen.crimson && !WorldGen.drunkWorldGen);
			eowCrimsonLore.Add(ModContent.ItemType<LoreBrainofCthulhu>(), 1, 1, 1, !WorldGen.crimson && !WorldGen.drunkWorldGen);
			npcLoot.Add(eowCrimsonLore);
			break;
		}
		case 266:
		{
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4926);
			rev.Add(4800, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<OccultSkullCrown>()), hideLootReport: true);
			LeadingConditionRule bocCorruptionLore = new LeadingConditionRule(DropHelper.If(() => (!WorldGen.crimson || WorldGen.drunkWorldGen) && !NPC.downedBoss2, ui: true, DropHelper.FirstKillText));
			bocCorruptionLore.Add(ModContent.ItemType<LoreCorruption>(), 1, 1, 1, WorldGen.crimson && !WorldGen.drunkWorldGen);
			bocCorruptionLore.Add(ModContent.ItemType<LoreEaterofWorlds>(), 1, 1, 1, WorldGen.crimson && !WorldGen.drunkWorldGen);
			npcLoot.Add(bocCorruptionLore);
			LeadingConditionRule bocCrimsonLore = new LeadingConditionRule(DropHelper.If(() => (WorldGen.crimson || WorldGen.drunkWorldGen) && !NPC.downedBoss2, ui: true, DropHelper.FirstKillText));
			bocCrimsonLore.Add(ModContent.ItemType<LoreCrimson>(), 1, 1, 1, !WorldGen.crimson && !WorldGen.drunkWorldGen);
			bocCrimsonLore.Add(ModContent.ItemType<LoreBrainofCthulhu>(), 1, 1, 1, !WorldGen.crimson && !WorldGen.drunkWorldGen);
			npcLoot.Add(bocCrimsonLore);
			break;
		}
		case 668:
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert10)
				{
					LCR_NotExpert10.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded tryIfSucceeded && tryIfSucceeded.RuleToChain is OneFromRulesRule);
					int[] deerWeapons = new int[4] { 5095, 5117, 5118, 5119 };
					LCR_NotExpert10.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, deerWeapons));
				}
			}
			catch (ArgumentNullException)
			{
			}
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(5110);
			rev.Add(5090, 4);
			break;
		case 222:
		{
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is DropBasedOnExpertMode dropBasedOnExpertMode && dropBasedOnExpertMode.ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsNotScaledWithLuckDropRule && oneFromOptionsNotScaledWithLuckDropRule.dropIds[0] == 1121);
			object loot = npcLoot;
			Fraction normalWeaponDropRateFraction = DropHelper.NormalWeaponDropRateFraction;
			int[] obj5 = new int[4] { 1123, 2888, 1121, 0 };
			obj5[3] = ModContent.ItemType<HardenedHoneycomb>();
			((ILoot)loot).AddNormalOnly(DropHelper.CalamityStyle(normalWeaponDropRateFraction, obj5));
			npcLoot.AddNormalOnly(ModContent.ItemType<TheBee>(), DropHelper.NormalWeaponDropRateFraction);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			npcLoot.Add(209, 1, 8, 12);
			rev.Add(4928);
			rev.Add(4802, 4);
			GFB.Add(DropHelper.PerPlayer(4821), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<AlchemicalDecanter>()), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedQueenBee, ModContent.ItemType<LoreQueenBee>(), ui: true, DropHelper.FirstKillText);
			break;
		}
		case 35:
		{
			DropOneByOne.Parameters skeletronBoneSpray = new DropOneByOne.Parameters
			{
				ChanceNumerator = 1,
				ChanceDenominator = 1,
				MinimumStackPerChunkBase = 5,
				MaximumStackPerChunkBase = 5,
				MinimumItemDropsCount = 14,
				MaximumItemDropsCount = 20
			};
			npcLoot.Add(new DropOneByOne(154, skeletronBoneSpray));
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4927);
			rev.Add(4801, 4);
			GFB.Add(DropHelper.PerPlayer(506), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedBoss3, ModContent.ItemType<LoreSkeletron>(), ui: true, DropHelper.FirstKillText);
			break;
		}
		case 113:
		{
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).FindLast((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert8)
				{
					LCR_NotExpert8.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded { RuleToChain: OneFromOptionsNotScaledWithLuckDropRule ruleToChain } && ruleToChain.dropIds[0] == 426);
					int[] obj3 = new int[8] { 426, 0, 434, 0, 514, 0, 4912, 0 };
					obj3[1] = ModContent.ItemType<Carnage>();
					obj3[3] = ModContent.ItemType<Meowthrower>();
					obj3[5] = ModContent.ItemType<BlackHawkRemote>();
					obj3[7] = ModContent.ItemType<BlastBarrel>();
					int[] wofWeapons = obj3;
					LCR_NotExpert8.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, wofWeapons));
				}
			}
			catch (ArgumentNullException)
			{
			}
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert9)
				{
					LCR_NotExpert9.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded { RuleToChain: OneFromOptionsNotScaledWithLuckDropRule ruleToChain } && ruleToChain.dropIds[0] == 490);
					int[] obj4 = new int[5] { 490, 491, 489, 2998, 0 };
					obj4[4] = ModContent.ItemType<RogueEmblem>();
					int[] wofEmblems = obj4;
					LCR_NotExpert9.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, wofEmblems));
				}
			}
			catch (ArgumentNullException)
			{
			}
			LeadingConditionRule firstWoFKill = new LeadingConditionRule(DropHelper.If(() => !Main.hardMode));
			firstWoFKill.Add(DropHelper.PerPlayer(ModContent.ItemType<HermitsBoxofOneHundredMedicines>()), hideLootReport: true);
			npcLoot.Add(firstWoFKill);
			LeadingConditionRule subsequentWoFKills = new LeadingConditionRule(DropHelper.If(() => Main.hardMode));
			subsequentWoFKills.Add(DropHelper.PerPlayer(ModContent.ItemType<HermitsBoxofOneHundredMedicines>(), 10));
			npcLoot.Add(subsequentWoFKills);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4930);
			rev.Add(4795, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<EyeofMagnus>()), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(770), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !Main.hardMode, ModContent.ItemType<LoreUnderworld>(), ui: true, DropHelper.FirstKillText);
			npcLoot.AddConditionalPerPlayer(() => !Main.hardMode, ModContent.ItemType<LoreWallofFlesh>(), ui: true, DropHelper.FirstKillText);
			break;
		}
		case 657:
			npcLoot.AddNormalOnly(520, 1, 15, 20);
			npcLoot.AddNormalOnly(3111, 1, 15, 20);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4950);
			rev.Add(4960, 4);
			GFB.Add(DropHelper.PerPlayer(5364), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedQueenSlime, ModContent.ItemType<LoreQueenSlime>(), ui: true, DropHelper.FirstKillText);
			break;
		case 134:
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is ItemDropWithConditionRule itemDropWithConditionRule && itemDropWithConditionRule.itemId == 1225);
			npcLoot.AddNormalOnly(ItemDropRule.ByCondition(DropHelper.HallowedBarsCondition, 1225, 1, 15, 30));
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4932);
			rev.Add(4803, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<BloodwormItem>()), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedMechBoss1, ModContent.ItemType<LoreDestroyer>(), ui: true, DropHelper.FirstKillText);
			npcLoot.AddConditionalPerPlayer(ShouldDropMechLore, ModContent.ItemType<LoreMechs>(), ui: true, DropHelper.MechBossText);
			break;
		case 125:
		case 126:
		{
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.MissingTwin) is LeadingConditionRule LCR_LTS && LCR_LTS.ChainedRules.Find((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded { RuleToChain: LeadingConditionRule ruleToChain } && ruleToChain.condition is Conditions.NotExpert).RuleToChain is LeadingConditionRule LCR_NotExpert7)
				{
					LCR_NotExpert7.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded { RuleToChain: CommonDrop ruleToChain } && ruleToChain.itemId == 1225);
				}
				npcLoot.AddNormalOnly(ItemDropRule.ByCondition(DropHelper.HallowedBarsCondition, 1225, 1, 15, 30));
			}
			catch (ArgumentNullException)
			{
			}
			npcLoot.AddIf((DropAttemptInfo info) => !Main.expertMode && IsLastTwinStanding(info), ModContent.ItemType<ThankYouPainting>(), 100);
			rev.AddIf((DropAttemptInfo info) => IsLastTwinStanding(info), 4931);
			rev.AddIf((DropAttemptInfo info) => IsLastTwinStanding(info), 4804, 4);
			LeadingConditionRule TwinsKillGFB = new LeadingConditionRule(DropHelper.If((DropAttemptInfo info) => IsLastTwinStanding(info)));
			TwinsKillGFB.Add(DropHelper.PerPlayer(3292), hideLootReport: true);
			npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(TwinsKillGFB);
			npcLoot.AddConditionalPerPlayer((DropAttemptInfo info) => !NPC.downedMechBoss2 && IsLastTwinStanding(info), ModContent.ItemType<LoreTwins>(), ui: true, DropHelper.FirstKillText);
			npcLoot.AddConditionalPerPlayer(ShouldDropMechLore, ModContent.ItemType<LoreMechs>(), ui: true, DropHelper.MechBossText);
			break;
		}
		case 127:
			npcLoot.RemoveWhere((IItemDropRule rule) => rule is ItemDropWithConditionRule itemDropWithConditionRule && itemDropWithConditionRule.itemId == 1225);
			npcLoot.AddNormalOnly(ItemDropRule.ByCondition(DropHelper.HallowedBarsCondition, 1225, 1, 15, 30));
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4933);
			rev.Add(4805, 4);
			GFB.Add(DropHelper.PerPlayer(786), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedMechBoss3, ModContent.ItemType<LoreSkeletronPrime>(), ui: true, DropHelper.FirstKillText);
			npcLoot.AddConditionalPerPlayer(ShouldDropMechLore, ModContent.ItemType<LoreMechs>(), ui: true, DropHelper.MechBossText);
			break;
		case 262:
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert6)
				{
					if (LCR_NotExpert6.ChainedRules.Find((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded { RuleToChain: LeadingConditionRule ruleToChain } && ruleToChain.condition is Conditions.FirstTimeKillingPlantera).RuleToChain is LeadingConditionRule LCR_FirstPlantera)
					{
						LCR_FirstPlantera.ChainedRules.Clear();
						int[] planteraWeapons = new int[8] { 1259, 3018, 758, 1255, 1178, 788, 1155, 1157 };
						LCR_NotExpert6.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, planteraWeapons));
					}
					LCR_NotExpert6.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded { RuleToChain: CommonDrop ruleToChain } && ruleToChain.itemId == 1157);
				}
			}
			catch (ArgumentNullException)
			{
			}
			npcLoot.AddNormalOnly(ModContent.ItemType<BloomStone>(), 10);
			npcLoot.AddNormalOnly(ModContent.ItemType<BlossomFlux>(), 10);
			npcLoot.AddNormalOnly(DropHelper.PerPlayer(ModContent.ItemType<LivingShard>(), 1, 30, 40));
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4934);
			rev.Add(4806, 4);
			GFB.Add(DropHelper.PerPlayer(1291, 1, 1, 9999), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedPlantBoss, ModContent.ItemType<LorePlantera>(), ui: true, DropHelper.FirstKillText);
			break;
		case 245:
		{
			try
			{
				List<IItemDropRule> golemRootRules = npcLoot.Get(includeGlobalDrops: false);
				if (golemRootRules.Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert5)
				{
					LCR_NotExpert5.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded tryIfSucceeded && tryIfSucceeded.RuleToChain is OneFromRulesRule);
					int[] golemItems = new int[7] { 1297, 1122, 1258, 1295, 1296, 1248, 899 };
					LCR_NotExpert5.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, golemItems));
					golemRootRules.RemoveAll((IItemDropRule rule) => rule is ItemDropWithConditionRule itemDropWithConditionRule && itemDropWithConditionRule.condition is Conditions.NotExpert && itemDropWithConditionRule.itemId == 1294);
				}
			}
			catch (ArgumentNullException)
			{
			}
			LeadingConditionRule mainRule2 = npcLoot.DefineNormalOnlyDropSet();
			mainRule2.Add(ModContent.ItemType<EssenceofSunlight>(), 1, 8, 10);
			mainRule2.Add(ModContent.ItemType<AegisBlade>(), 10);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			LeadingConditionRule firstGolemKill = new LeadingConditionRule(DropHelper.If(() => !NPC.downedGolemBoss));
			firstGolemKill.Add(DropHelper.PerPlayer(1294));
			npcLoot.Add(firstGolemKill);
			rev.Add(4935);
			rev.Add(4807, 4);
			GFB.Add(DropHelper.PerPlayer(299, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(298, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(304, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2329, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(296, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<PotionofOmniscience>(), 1, 1, 9999), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedGolemBoss, ModContent.ItemType<LoreGolem>(), ui: true, DropHelper.FirstKillText);
			break;
		}
		case 551:
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert4)
				{
					LCR_NotExpert4.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded tryIfSucceeded && tryIfSucceeded.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule);
					int[] betsyWeapons = new int[4] { 3827, 3858, 3859, 3870 };
					LCR_NotExpert4.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, betsyWeapons));
				}
			}
			catch (ArgumentNullException)
			{
			}
			rev.Add(4948);
			rev.Add(4817, 4);
			break;
		case 370:
			try
			{
				npcLoot.RemoveWhere((IItemDropRule rule) => rule is ItemDropWithConditionRule itemDropWithConditionRule && itemDropWithConditionRule.itemId == 2609);
				IItemDropRule notRemix2 = npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotRemixSeed);
				if (notRemix2 is LeadingConditionRule && notRemix2.ChainedRules.Find((IItemDropRuleChainAttempt chain) => chain.RuleToChain is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert).RuleToChain is LeadingConditionRule LCR_NotExpert3)
				{
					LCR_NotExpert3.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded tryIfSucceeded && tryIfSucceeded.RuleToChain is OneFromOptionsDropRule);
					int[] obj2 = new int[7] { 2611, 2624, 2623, 2622, 2621, 0, 2609 };
					obj2[5] = ModContent.ItemType<DukesDecapitator>();
					int[] dukeItems = obj2;
					LCR_NotExpert3.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, dukeItems));
				}
			}
			catch (ArgumentNullException)
			{
			}
			npcLoot.AddNormalOnly(ModContent.ItemType<BrinyBaron>(), 10);
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4936);
			rev.Add(4808, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<OldDie>()), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(669), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(250), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2500), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2498), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2499), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(3120), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2293), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(3036), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2360), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(5139), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2354, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2338, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(4067), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2460), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(2484), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(444, 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<FishboneBoomerang>()), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<FishofEleum>(), 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<FishofFlight>(), 1, 1, 9999), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedFishron, ModContent.ItemType<LoreDukeFishron>(), ui: true, DropHelper.FirstKillText);
			break;
		case 636:
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert2)
				{
					LCR_NotExpert2.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded tryIfSucceeded && tryIfSucceeded.RuleToChain is OneFromOptionsDropRule);
					int[] empressItems = new int[6] { 4923, 4953, 4952, 4715, 4914, 4823 };
					LCR_NotExpert2.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, empressItems));
					LCR_NotExpert2.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chain) => chain.RuleToChain is CommonDrop commonDrop && (commonDrop.itemId == 4823 || commonDrop.itemId == 4715));
				}
			}
			catch (ArgumentNullException)
			{
			}
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4949);
			rev.Add(4811, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<PurpleHaze>(), 1, 1, 9999), hideLootReport: true);
			GFB.Add(DropHelper.PerPlayer(5134), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedEmpressOfLight, ModContent.ItemType<LoreEmpressofLight>(), ui: true, DropHelper.FirstKillText);
			break;
		case 439:
			rev.Add(4937);
			rev.Add(4809, 4);
			npcLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
			GFB.Add(DropHelper.PerPlayer(3461, 1, 1, 9999), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedAncientCultist, ModContent.ItemType<LorePrelude>(), ui: true, DropHelper.FirstKillText);
			break;
		case 398:
			try
			{
				if (npcLoot.Get(includeGlobalDrops: false).Find((IItemDropRule rule) => rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) is LeadingConditionRule LCR_NotExpert)
				{
					LCR_NotExpert.ChainedRules.RemoveAll((IItemDropRuleChainAttempt chainAttempt) => chainAttempt is Chains.TryIfSucceeded tryIfSucceeded && tryIfSucceeded.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule);
					int[] obj = new int[10] { 3063, 3065, 3389, 3930, 1553, 3541, 3570, 3569, 3571, 0 };
					obj[9] = ModContent.ItemType<UtensilPoker>();
					int[] moonLordWeapons = obj;
					LCR_NotExpert.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, moonLordWeapons));
				}
			}
			catch (ArgumentNullException)
			{
			}
			npcLoot.AddNormalOnly(DropHelper.PerPlayer(ModContent.ItemType<CelestialOnion>()));
			npcLoot.AddNormalOnly(ModContent.ItemType<ThankYouPainting>(), 100);
			rev.Add(4938);
			rev.Add(4810, 4);
			GFB.Add(DropHelper.PerPlayer(ModContent.ItemType<CalamarisLament>()), hideLootReport: true);
			npcLoot.AddConditionalPerPlayer(() => !NPC.downedMoonlord, ModContent.ItemType<LoreRequiem>(), ui: true, DropHelper.FirstKillText);
			break;
		}
		if (CalamityNPCTypeSets.Skeleton[npc.type])
		{
			npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AncientBoneDust>(), 5, 3));
		}
		if (CalamityNPCSets.IsBuffedDungeonEnemy[npc.type])
		{
			npcLoot.Add(1508, 5);
		}
		static bool IsLastTwinStanding(DropAttemptInfo info)
		{
			NPC npc2 = info.npc;
			if (npc2 == null)
			{
				return false;
			}
			if (npc2.type == 125)
			{
				return !NPC.AnyNPCs(126);
			}
			if (npc2.type == 126)
			{
				return !NPC.AnyNPCs(125);
			}
			return false;
		}
		static bool ShouldDropMechLore(DropAttemptInfo info)
		{
			NPC npc2 = info.npc;
			if (npc2 == null)
			{
				return false;
			}
			bool lastTwinStanding = IsLastTwinStanding(info);
			if (!NPC.downedMechBossAny)
			{
				if (!lastTwinStanding && npc2.type != 134)
				{
					return npc2.type == 127;
				}
				return true;
			}
			return false;
		}
	}

	public override void ModifyGlobalLoot(GlobalLoot globalLoot)
	{
		LeadingConditionRule goldNormalEnemiesDrop = new LeadingConditionRule(DropHelper.GoldSetBonusGoldCondition);
		goldNormalEnemiesDrop.Add(73, 25, 1, 1, hideLootReport: true);
		globalLoot.Add(goldNormalEnemiesDrop);
		LeadingConditionRule goldBossDrop = new LeadingConditionRule(DropHelper.GoldSetBonusBossCondition);
		goldBossDrop.Add(73, 1, 3, 3, hideLootReport: true);
		globalLoot.Add(goldBossDrop);
		LeadingConditionRule tarragonDrop = new LeadingConditionRule(DropHelper.TarragonSetBonusHeartCondition);
		tarragonDrop.Add(58, 5, 1, 1, hideLootReport: true);
		globalLoot.Add(tarragonDrop);
	}

	public override bool PreKill(NPC npc)
	{
		if (CalamityWorld.revenge && (CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) || npc.type == 267))
		{
			DropHelper.BlockDrops(56, 86, 880, 1329);
		}
		if (BossRushEvent.BossRushActive)
		{
			DropHelper.BlockEverything(ModContent.ItemType<Rock>());
		}
		return true;
	}

	public override void OnKill(NPC npc)
	{
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			BossRushEvent.OnBossKill(npc, base.Mod);
		}
		if (AcidRainEvent.AcidRainEventIsOngoing)
		{
			AcidRainEvent.OnEnemyKill(npc);
		}
		bool lastTwinStanding = false;
		if (npc.type == 125)
		{
			lastTwinStanding = !NPC.AnyNPCs(126);
		}
		else if (npc.type == 126)
		{
			lastTwinStanding = !NPC.AnyNPCs(125);
		}
		if ((npc.boss && (npc.type == 13 || npc.type == 14 || npc.type == 15)) || npc.type == 266)
		{
			CalamityGlobalTownNPC.SetNewShopVariable(new int[3] { 17, 19, 20 }, NPC.downedBoss2);
			SetNewBossJustDowned(npc);
		}
		switch (npc.type)
		{
		case 50:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { 20 }, NPC.downedSlimeKing);
			SetNewBossJustDowned(npc);
			break;
		case 4:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[2] { 17, 20 }, NPC.downedBoss1);
			SetNewBossJustDowned(npc);
			break;
		case 668:
			SetNewBossJustDowned(npc);
			break;
		case 222:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[2] { 19, 20 }, NPC.downedQueenBee);
			SetNewBossJustDowned(npc);
			break;
		case 35:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[2] { 17, 20 }, NPC.downedBoss3);
			SetNewBossJustDowned(npc);
			if (!NPC.downedBoss3 && !BossRushEvent.BossRushActive)
			{
				if (Main.netMode != 1)
				{
					global::CalamityMod.World.Abyss.UnlockAllAbyssChests();
				}
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.SkeletronAbyssChestNotification", (Color?)new Color(76, 181, 76));
			}
			break;
		case 113:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[11]
			{
				17, 19, 20, 227, 228, 353, 207, 38, 208, 54,
				453
			}, Main.hardMode);
			SetNewBossJustDowned(npc);
			if (!Main.hardMode && !BossRushEvent.BossRushActive)
			{
				if (CalamityServerConfig.Instance.EarlyHardmodeProgressionRework)
				{
					WorldGen.altarCount++;
				}
				Color messageColor10 = Color.Aquamarine;
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.UglyBossText", messageColor10);
			}
			break;
		case 618:
			DownedBossSystem.downedDreadnautilus = true;
			CalamityNetcode.SyncWorld();
			break;
		case 657:
			SetNewBossJustDowned(npc);
			break;
		case 134:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[4] { 38, 550, 353, 160 }, NPC.downedMechBossAny);
			CalamityGlobalTownNPC.SetNewShopVariable(new int[3]
			{
				353,
				ModContent.NPCType<Archmage>(),
				ModContent.NPCType<Bandit>()
			}, NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3);
			SetNewBossJustDowned(npc);
			if (!NPC.downedMechBoss1 && CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && !BossRushEvent.BossRushActive)
			{
				SpawnMechBossHardmodeOres();
			}
			break;
		case 125:
		case 126:
			if (lastTwinStanding)
			{
				CalamityGlobalTownNPC.SetNewShopVariable(new int[4] { 38, 550, 353, 160 }, NPC.downedMechBossAny);
				CalamityGlobalTownNPC.SetNewShopVariable(new int[3]
				{
					353,
					ModContent.NPCType<Archmage>(),
					ModContent.NPCType<Bandit>()
				}, !NPC.downedMechBoss1 || NPC.downedMechBoss2 || !NPC.downedMechBoss3);
				SetNewBossJustDowned(npc);
				if (!NPC.downedMechBoss2 && CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && !BossRushEvent.BossRushActive)
				{
					SpawnMechBossHardmodeOres();
				}
			}
			break;
		case 127:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[4] { 38, 550, 353, 160 }, NPC.downedMechBossAny);
			CalamityGlobalTownNPC.SetNewShopVariable(new int[3]
			{
				353,
				ModContent.NPCType<Archmage>(),
				ModContent.NPCType<Bandit>()
			}, !NPC.downedMechBoss1 || !NPC.downedMechBoss2 || NPC.downedMechBoss3);
			SetNewBossJustDowned(npc);
			if (!NPC.downedMechBoss3 && CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && !BossRushEvent.BossRushActive)
			{
				SpawnMechBossHardmodeOres();
			}
			break;
		case 262:
		{
			int[] obj = new int[4] { 228, 160, 633, 0 };
			obj[3] = ModContent.NPCType<Bandit>();
			CalamityGlobalTownNPC.SetNewShopVariable(obj, NPC.downedPlantBoss);
			SetNewBossJustDowned(npc);
			if (!NPC.downedPlantBoss && !BossRushEvent.BossRushActive)
			{
				string key8 = "Mods.CalamityMod.Status.Progression.PlantOreText";
				Color messageColor8 = Color.GreenYellow;
				string key9 = "Mods.CalamityMod.Status.Progression.SandSharkText3";
				Color messageColor9 = Color.Goldenrod;
				CalamityUtils.SpawnOre(ModContent.TileType<PerennialOre>(), 0.00012, 0.65f, 0.85f, 5, 10, 0, 1);
				CalamityUtils.BroadcastLocalizedText(key8, messageColor8);
				CalamityUtils.BroadcastLocalizedText(key9, messageColor9);
			}
			break;
		}
		case 636:
			SetNewBossJustDowned(npc);
			break;
		case 344:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<Archmage>() }, NPC.downedChristmasTree || !NPC.downedChristmasSantank || !NPC.downedChristmasIceQueen);
			break;
		case 346:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<Archmage>() }, !NPC.downedChristmasTree || NPC.downedChristmasSantank || !NPC.downedChristmasIceQueen);
			break;
		case 345:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<Archmage>() }, !NPC.downedChristmasTree || !NPC.downedChristmasSantank || NPC.downedChristmasIceQueen);
			break;
		case 245:
		{
			int[] obj2 = new int[7] { 19, 209, 178, 108, 228, 550, 0 };
			obj2[6] = ModContent.NPCType<Bandit>();
			CalamityGlobalTownNPC.SetNewShopVariable(obj2, NPC.downedGolemBoss);
			SetNewBossJustDowned(npc);
			if (!NPC.downedGolemBoss && !BossRushEvent.BossRushActive)
			{
				if (!Main.LocalPlayer.dead && Main.LocalPlayer.active)
				{
					SoundEngine.PlaySound(in PlagueSound, Main.LocalPlayer.Center);
				}
				Color messageColor11 = Color.Lime;
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.BabyBossText", messageColor11);
			}
			break;
		}
		case 551:
			DownedBossSystem.downedBetsy = true;
			CalamityNetcode.SyncWorld();
			break;
		case 370:
			SetNewBossJustDowned(npc);
			break;
		case 439:
			SetNewBossJustDowned(npc);
			break;
		case 517:
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { 633 }, NPC.downedTowerSolar);
			break;
		case 398:
		{
			CalamityGlobalTownNPC.SetNewShopVariable(new int[2]
			{
				663,
				ModContent.NPCType<Bandit>()
			}, NPC.downedMoonlord);
			SetNewBossJustDowned(npc);
			string key5 = "Mods.CalamityMod.Status.Progression.MoonBossText";
			Color messageColor5 = Color.Orange;
			string key6 = "Mods.CalamityMod.Status.Progression.ProfanedBossText2";
			Color messageColor6 = Color.Cyan;
			string key7 = "Mods.CalamityMod.Status.Progression.FutureOreText";
			Color messageColor7 = Color.LightGray;
			if (BossRushEvent.BossRushActive)
			{
				break;
			}
			if (!CalamityWorld.HasGeneratedLuminitePlanetoids)
			{
				ThreadPool.QueueUserWorkItem(delegate
				{
					LuminitePlanet.GenerateLuminitePlanetoids();
				});
				CalamityWorld.HasGeneratedLuminitePlanetoids = true;
				if (NPC.downedMoonlord)
				{
					CalamityNetcode.SyncWorld();
				}
			}
			if (!NPC.downedMoonlord)
			{
				CalamityUtils.BroadcastLocalizedText(key5, messageColor5);
				CalamityUtils.BroadcastLocalizedText(key6, messageColor6);
				CalamityUtils.BroadcastLocalizedText(key7, messageColor7);
			}
			break;
		}
		}
	}

	private void SpawnMechBossHardmodeOres()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		if (!NPC.downedMechBossAny)
		{
			Color messageColor = default(Color);
			((Color)(ref messageColor))._002Ector(50, 255, 130);
			CalamityUtils.SpawnOre(108, 0.00012, 0.55f, 0.8f, 3, 8);
			CalamityUtils.SpawnOre(222, 0.00012, 0.55f, 0.8f, 3, 8);
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.HardmodeOreTier2Text", messageColor);
			return;
		}
		if ((!NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (!NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (!NPC.downedMechBoss3 && !NPC.downedMechBoss1))
		{
			Color messageColor2 = default(Color);
			((Color)(ref messageColor2))._002Ector(50, 255, 130);
			CalamityUtils.SpawnOre(111, 0.00012, 0.65f, 0.9f, 3, 8);
			CalamityUtils.SpawnOre(223, 0.00012, 0.65f, 0.9f, 3, 8);
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.HardmodeOreTier3Text", messageColor2);
			return;
		}
		string key = "Mods.CalamityMod.Status.Progression.HardmodeOreTier4Text";
		Color messageColor3 = default(Color);
		((Color)(ref messageColor3))._002Ector(50, 255, 130);
		CalamityUtils.SpawnOre(ModContent.TileType<HallowedOre>(), 0.00017, 0.55f, 0.9f, 8, 14, 117, 402, 403, 164);
		CalamityUtils.BroadcastLocalizedText(key, messageColor3);
	}
}
