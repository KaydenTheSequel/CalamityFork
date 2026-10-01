using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CalamityMod.Balancing;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Buffs.Cooldowns;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Placeables;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.Summon;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.CalPlayer.DrawLayers;
using CalamityMod.Cooldowns;
using CalamityMod.CustomRecipes;
using CalamityMod.DataStructures;
using CalamityMod.Dusts;
using CalamityMod.EntitySources;
using CalamityMod.Enums;
using CalamityMod.Events;
using CalamityMod.ExtraTextures;
using CalamityMod.FluidSimulation;
using CalamityMod.Graphics;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Armor.Aerospec;
using CalamityMod.Items.Armor.Astral;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.DesertProwler;
using CalamityMod.Items.Armor.Empyrean;
using CalamityMod.Items.Armor.Fearmonger;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Hydrothermic;
using CalamityMod.Items.Armor.LunicCorps;
using CalamityMod.Items.Armor.OmegaBlue;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Items.Armor.PlagueReaper;
using CalamityMod.Items.Armor.Prismatic;
using CalamityMod.Items.Armor.Reaver;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Armor.SnowRuffian;
using CalamityMod.Items.Armor.Sulphurous;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Armor.TitanHeart;
using CalamityMod.Items.Armor.Umbraphile;
using CalamityMod.Items.Armor.Victide;
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.Fishing;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.Fishing.SulphurCatches;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.Mounts.Minecarts;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Placeables.FurnitureDriftwood;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Tools.ClimateChange;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using CalamityMod.Items.VanillaArmorChanges;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Other;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;
using CalamityMod.Packets;
using CalamityMod.Particles;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Healing;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using CalamityMod.Systems.Collections;
using CalamityMod.Systems.Graphic.PixelationSystem;
using CalamityMod.Systems.Mechanic;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.Tiles.FurnitureAuric;
using CalamityMod.Tiles.Ores;
using CalamityMod.UI;
using CalamityMod.Utilities;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.NetModules;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Net;
using Terraria.Utilities;

namespace CalamityMod.CalPlayer;

public class CalamityPlayer : ModPlayer
{
	public enum FishingMinigames
	{
		None,
		ScrapBobber,
		NavystoneBobber,
		SkylineBobber,
		PerennialBobber,
		ScoriaBobber,
		DevourerofCods
	}

	internal ulong universalFrameTimer;

	public static bool areThereAnyDamnBosses;

	public static bool areThereAnyDamnEvents;

	public float calamityBonusLuck;

	public bool drawBossHPBar;

	public float stealthUIAlpha;

	public float SulphWaterUIOpacity;

	public bool shouldDrawSmallText;

	public int projTypeJustHitBy;

	public int sCalDeathCount;

	public int sCalKillCount;

	public int actualMaxLife;

	public static int chaosStateDuration;

	public static int chaosStateDuration_NR;

	public double contactDamageReduction;

	public double projectileDamageReduction;

	public int hellbornShots;

	public int garandShots;

	public bool blockAllDashes;

	public bool resetHeightandWidth;

	public bool noLifeRegen;

	public float ammoCost;

	public float healingPotionMultiplier;

	public bool heldGaelsLastFrame;

	internal bool hadNanomachinesLastFrame;

	public bool combHair;

	public bool disableVoodooSpawns;

	public bool disablePerfCystSpawns;

	public bool disableHiveCystSpawns;

	public bool disableNaturalScourgeSpawns;

	public bool disableAnahitaSpawns;

	public int whitewaterHeal;

	public bool blazingCursorDamage;

	public bool blazingCursorVisuals;

	public float blazingMouseAuraFade;

	public float GeneralScreenShakePower;

	public bool GivenBrimstoneLocus;

	public DoGCartSegment[] DoGCartSegments;

	public float SmoothenedMinecartRotation;

	public bool LungingDown;

	public Vector2? BossRushReturnPosition;

	public float moveSpeedBonus;

	public int momentumCapacitorTime;

	public float momentumCapacitorBoost;

	public FishingMinigames SelectedFishingMinigame;

	public int StratusStarburst;

	public int AvaliableStarburst;

	public static int MaxStratusStarburst;

	public float HalleyAccuracyCounter;

	public float StarburstSpawnFrameCounter;

	public int StratusStarburstResetTimer;

	public int Starshield;

	public List<StarburstEntity> StarburstEntities;

	public CombatText subtitletext;

	public Color[] subtitleColors;

	public int DoGHeadHitCounter;

	public StatModifier TypelessDebuffMultiplier;

	public StatModifier HeatDebuffMultiplier;

	public StatModifier ColdDebuffMultiplier;

	public StatModifier SicknessDebuffMultiplier;

	public StatModifier WaterDebuffMultiplier;

	public StatModifier ElectricDebuffMultiplier;

	public int TimeHoldingMelee;

	public int TimeHoldingRanged;

	public int TimeHoldingMagic;

	public int TimeHoldingSummon;

	public int TimeHoldingRogue;

	public int TimeHoldingClassless;

	internal TimeSpan previousSessionTotal;

	internal int lastSplitType;

	internal TimeSpan lastSplit;

	public int CurrentlyViewedFactoryID;

	public int CurrentlyViewedChargerID;

	public int CurrentlyViewedHologramID;

	public int CurrentlyViewedCanvasID;

	public int CurrentlyViewedCanvasType;

	public string CurrentlyViewedHologramText;

	[Obsolete("No longer does anything in the new abyss light system")]
	public int externalAbyssLight;

	public float externalBreathTickBoost;

	public float externalFlightTimeMultBoost;

	public bool? externalRageEnabled;

	public bool? externalAdrenalineEnabled;

	public bool externalColdImmunity;

	public bool externalHeatImmunity;

	public bool externalAuricRejectionImmunity;

	public bool externalDefenseDamageImmunity;

	public bool disableAllDodges;

	public bool newMerchantInventory;

	public bool newPainterInventory;

	public bool newGolferInventory;

	public bool newZoologistInventory;

	public bool newDyeTraderInventory;

	public bool newPartyGirlInventory;

	public bool newStylistInventory;

	public bool newDemolitionistInventory;

	public bool newDryadInventory;

	public bool newTavernkeepInventory;

	public bool newArmsDealerInventory;

	public bool newGoblinTinkererInventory;

	public bool newWitchDoctorInventory;

	public bool newClothierInventory;

	public bool newMechanicInventory;

	public bool newPirateInventory;

	public bool newTruffleInventory;

	public bool newWizardInventory;

	public bool newSteampunkerInventory;

	public bool newCyborgInventory;

	public bool newPrincessInventory;

	public bool newSkeletonMerchantInventory;

	public bool newPermafrostInventory;

	public bool newAmidiasInventory;

	public bool newBanditInventory;

	public bool newCalamitasInventory;

	public int gaelSwipes;

	public int arsenalCooldown;

	public int killModeCooldown;

	public bool demonSwordKillMode;

	public int dragoonDrizzlefishGelBoost;

	public int deadSunCounter;

	public int DragonsBreathAudioCooldown;

	public int DragonsBreathAudioCooldown2;

	public int darklightEnergy;

	public int darklightEnergyTimer;

	public bool darklightEnergyPaused;

	public bool darklightEnergyMaxFXPlayed;

	private int lucreciaParticleTimer;

	public int elementalMastery;

	public int elementalMasteryTimer;

	public bool elementalMasteryPaused;

	public bool elementalMasterySFXPlayed;

	public float unstableCastersGauntletVis;

	public int unstableCastersGauntletVisTimer;

	public int PhotoAudioCooldown;

	public int PhotoTimer;

	public int arpeggioCooldown;

	public int burningSeaBurnOut;

	public int evilSmasherBoost;

	public int plagueTaintedSMGDroneCooldown;

	public int flareGunOverheat;

	public bool brittleStarBuffMode;

	public bool sBlasterDashActivated;

	public int saharaSlicersBolts;

	public int oceanCrestTimer;

	public int Holyhammer;

	public int PHAThammer;

	public int StellarHammer;

	public int GalaxyHammer;

	public bool despoilerNerf;

	public int amputatorBuff;

	public int furyFuel;

	public const int FuryFuelMax = 1800;

	public float furyRefuelTimer;

	public bool buffedAuger;

	public int rOfResilienceCooldown;

	public int rOfResilienceEffect;

	public int rOfResilienceOrbitOffset;

	public int NorfleetCounter;

	public int hideOfDeusMeleeBoostTimer;

	public int alcoholPoisonLevel;

	public int dashTimeMod;

	public int hInfernoBoost;

	public int packetTimer;

	public int navyRodAuraTimer;

	public int hydrothermicInfernoTimer;

	public int tarraLifeAuraTimer;

	public int bloodflareHeartTimer;

	public int dragonRageHits;

	public int dragonRageCooldown;

	public float aquaticBoost;

	public int galileoCooldown;

	public int planarSpeedBoost;

	public int profanedSoulWeaponUsage;

	public int profanedSoulWeaponType;

	public int danceOfLightCharge;

	public int dogTextCooldown;

	public float auralisStealthCounter;

	public int auralisAuroraCounter;

	public int auralisAuroraCooldown;

	public int auralisAurora;

	public int necroReviveCounter;

	public int hideOfDeusTimer;

	public int murasamaHitCooldown;

	public int giantShellPostHit;

	public int tortShellPostHit;

	public int MiniSwarmerCooldown;

	public float SulphWaterPoisoningLevel;

	public float holyInfernoFadeIntensity;

	public NPC unstableSelectedTarget;

	public int zapActivity;

	public bool ragePulse;

	public int ragePulseVisualTimer;

	public int ragePulseTimer;

	private const int DashDisableCooldown = 12;

	public Dictionary<string, CooldownInstance> cooldowns;

	public bool canFireAtaxiaRangedProjectile;

	public bool canFireAtaxiaRogueProjectile;

	public bool canFireGodSlayerRangedProjectile;

	public bool canFireBloodflareMageProjectile;

	public bool canFireBloodflareRangedProjectile;

	public int consecutiveCaughtFish;

	public float WeakTimeFreezeUseTimer;

	public bool WeakTimeFreezeInUse;

	public int soundCooldown;

	public int hurtSoundTimer;

	public bool playRogueStealthSound;

	public int fullRageSoundCountdownTimer;

	private const int FullRageSoundDelay = 300;

	public bool playFullAdrenalineSound;

	public static readonly SoundStyle RageFilledSound;

	public static readonly SoundStyle RageActivationSound;

	public static readonly SoundStyle RageEndSound;

	public static readonly SoundStyle AdrenalineFilledSound;

	public static readonly SoundStyle AdrenalineActivationSound;

	public static readonly SoundStyle AdrenalineHurtSound;

	public static readonly SoundStyle AdrenalineHurtGFB;

	public static readonly SoundStyle NanomachinesActivationSound;

	public static readonly SoundStyle RogueStealthSound;

	public static readonly SoundStyle DefenseDamageSound;

	public static readonly SoundStyle IjiDeathSound;

	public static readonly SoundStyle DrownSound;

	public static readonly SoundStyle LeonDeathNoiseRE4_ForGFB;

	public static readonly SoundStyle BaroclawHit;

	public static readonly SoundStyle AbsorberHit;

	public float rogueStealth;

	public float rogueStealthMax;

	public int temporaryStealthTimer;

	public float temporaryStealthMax;

	public float stealthGenStandstill;

	public float stealthGenMoving;

	public int flatStealthLossReduction;

	public const float StealthAccelerationCap = 1.5f;

	public float stealthAcceleration;

	public bool stealthStrikeThisFrame;

	public bool stealthStrikeHalfCost;

	public bool stealthStrike75Cost;

	public bool stealthStrike90Cost;

	public bool wearingRogueArmor;

	public float accStealthGenBoost;

	public float stealthDamage;

	public double bonusStealthDamage;

	public float rogueVelocity;

	public int focusFlurryAttackCount;

	public bool onyxExcavator;

	public bool rimehound;

	public bool crysthamyr;

	public bool ExoChair;

	public AndromedaPlayerState andromedaState;

	public int andromedaCripple;

	public bool burrowerPet;

	public bool thirdSage;

	public bool perfmini;

	public bool akato;

	public bool yharonPet;

	public bool leviPet;

	public bool plaguebringerBab;

	public bool rotomPet;

	public bool ladShark;

	public int ladHearts;

	public bool sparks;

	public bool sirenPet;

	public bool spiritOriginPet;

	public bool fox;

	public bool chibii;

	public bool brimling;

	public bool bearPet;

	public bool kendra;

	public bool trashMan;

	public bool astrophage;

	public bool flakPet;

	public bool babyGhostBell;

	public bool radiator;

	public bool scalPet;

	public bool hiveMindPet;

	public bool bendyPet;

	public bool littleLightPet;

	public bool pineapplePet;

	public bool eidolonSnailPet;

	public bool lordePet;

	public bool frostyBat;

	public bool toastyBat;

	public bool rageModeActive;

	public float rage;

	public float rageMax;

	public int RageDuration;

	public int rageGainCooldown;

	public int rageCombatFrames;

	public float RageDamageBoost;

	public bool adrenalineModeActive;

	public float adrenaline;

	public float adrenalineMax;

	public int adrenalinePauseTimer;

	public int AdrenalineDuration;

	public int AdrenalineChargeTime;

	public int AdrenalineFadeTime;

	public double defenseDamageRatio;

	internal int totalDefenseDamage;

	internal const int DefenseDamageBaseRecoveryTime = 60;

	internal const int DefenseDamageMaxRecoveryTime = 900;

	internal int defenseDamageRecoveryFrames;

	internal int totalDefenseDamageRecoveryFrames;

	internal const int DefenseDamageRecoveryDelay = 10;

	internal int defenseDamageDelayFrames;

	public bool nextHitDealsDefenseDamage;

	public bool freeDodgeFromShieldAbsorption;

	public bool drawnAnyShieldThisFrame;

	public int RoverDriveShieldDurability;

	public int LunicCorpsShieldDurability;

	public int SpongeShieldDurability;

	public bool roverDrive;

	public bool roverDriveShieldVisible;

	internal float roverDriveShieldPartialRechargeProgress;

	internal bool playedRoverDriveShieldSound;

	internal float lunicCorpsShieldPartialRechargeProgress;

	internal bool playedLunicCorpsShieldSound;

	public int pSoulShieldDurability;

	public bool pSoulShieldVisible;

	internal bool playedProfanedSoulShieldSound;

	internal float pSoulShieldPartialRechargeProgress;

	public bool sponge;

	public bool spongeShieldVisible;

	internal float spongeShieldPartialRechargeProgress;

	internal bool playedSpongeShieldSound;

	public float abyssBreathLossRateStat;

	public int abyssLifeLostAtZeroBreathStat;

	public int abyssDefenseLossStat;

	public float abyssDarkness;

	public float abyssPlayerGlowMultiplier;

	public float abyssFlashlightWidthMultiplier;

	public float darknessIntensity;

	public Vector2? lastDeerclopsPosition;

	public bool spawnedPunchCard;

	public bool extraAccessoryML;

	public bool cShard;

	public bool eCore;

	public bool pHeart;

	public bool sTangerine;

	public bool mFruit;

	public bool tCloudberry;

	public bool sStrawberry;

	public bool revJamDrop;

	public bool rageBoostOne;

	public bool rageBoostTwo;

	public bool rageBoostThree;

	public bool adrenalineBoostOne;

	public bool adrenalineBoostTwo;

	public bool adrenalineBoostThree;

	public bool healToFull;

	public bool shieldOfTheHighRulerDashVelocityBoosted;

	public bool luxorsGift;

	public bool luxorHit;

	public bool luxorsGiftVanity;

	public bool fungalSymbiote;

	public bool trinketOfChi;

	public bool gladiatorSword;

	public int gladiatorTimer;

	public bool unstableGraniteCore;

	public bool regenerator;

	public float regeneratorDamage;

	public bool theBee;

	public bool arcFlashRing;

	public bool arcFlashRingVisual;

	public int generalBandCooldown;

	public bool bGlassBand;

	public bool bGlassBandVisual;

	public bool batholithBangle;

	public bool batholithBangleVisual;

	public bool protolithBangle;

	public bool protolithBangleVisual;

	public bool shouldTriggerBeeCooldown;

	public int theBeeCooldown;

	public bool aFossil;

	public bool aPowder;

	public bool fallingBlockProtection;

	public bool trapProtection;

	public bool alluringBait;

	public bool enchantedPearl;

	public bool fishingStation;

	public bool rBrain;

	public bool bloodyWormTooth;

	public bool ivDrip;

	public bool afflicted;

	public bool chiRegen;

	public bool affliction;

	public bool stressPills;

	public bool laudanum;

	public bool heartOfDarkness;

	public bool profanedSoulRelicBuff;

	public bool draedonsHeart;

	public bool vexation;

	public bool dodgeScarf;

	public bool evasionScarf;

	public bool badgeOfBravery;

	public bool WarbanneroftheRighteous;

	public bool warbannerGlow;

	public float warbannerDamageMult;

	public bool tesla;

	public bool teslaVisuals;

	public bool cryogenSoul;

	public bool ascendantInsignia;

	public int ascendantInsigniaBuffTime;

	public int ascendantInsigniaCooldown;

	public bool magmaStoneVisuals;

	public bool eGauntlet;

	public bool eGauntletVisuals;

	public int gloveLevel;

	public bool alreadyHasFrogLeg;

	public bool eTalisman;

	public bool lastDashWasTabi;

	public bool statisNinjaBelt;

	public bool voidSashVisuals;

	public bool statisVoidSash;

	public bool nucleogenesis;

	public bool nuclearFuelRod;

	public bool nebulousCore;

	public bool deepDiver;

	public bool aquaticHeartWaterBuff;

	public bool aquaticHeartIce;

	public bool ilSpark;

	public bool transformer;

	public bool transformerVisual;

	public int transformerCooldown;

	public int transformerDelay;

	public int transformerStoredKills;

	public int hookPullVisuals;

	public bool bloomStone;

	public bool bloomStoneHookVisuals;

	public int bloomStoneHealPool;

	public int bloomStoneTotalHeal;

	public float bloomStoneHealTimer;

	public float bloomStoneHealRate;

	public int bloomStoneBuffedHealRateTimer;

	public bool hideOfDeus;

	public bool dAmulet;

	public bool rampartOfDeities;

	public bool gShell;

	public bool lAmbergris;

	public bool lAmbergrisVisual;

	public bool tortShell;

	public bool absorber;

	public bool hadLifeRegenHinderingDebuff;

	public bool alwaysHoneyRegen;

	public float alwaysHoneyRegenAmount;

	public bool honeyTurboRegen;

	public bool honeyDewHalveDebuffs;

	public bool livingDewHalveDebuffs;

	public int jewelBonusDefense;

	public float pulseCounter;

	public float pulseRate;

	public bool aAmpoule;

	public bool rOoze;

	public float radiantOozeRegen;

	public float purityRegen;

	public bool fBarrier;

	public bool aBrain;

	public bool amalgam;

	public bool raiderTalisman;

	public int raiderCritLifespan;

	public int raiderSoundCooldown;

	public bool gSabaton;

	public int gSabatonHotkeyFallWindup;

	public int gSabatonFall;

	public bool gSabatonFalling;

	public int gSabatonTempJumpSpeed;

	public bool rOfDelivarenceRam;

	public bool sGlyph;

	public bool sRegen;

	public bool tracersDust;

	public bool moonWalkers;

	public bool voidStriders;

	public bool seraphTracers;

	public bool frostFlare;

	public bool evolution;

	public bool procDodgeEffects;

	public bool nanotech;

	public bool deadshotBrooch;

	public bool shadowMinions;

	public bool holyMinions;

	public bool alchFlask;

	public bool toxicHeart;

	public bool toxicHeartVisuals;

	public bool abaddon;

	public bool aeroStone;

	public bool lifejelly;

	public bool cleansingjelly;

	public bool GrandGelatin;

	public int CleansingEffect;

	public bool spawnedJellyAura;

	public bool community;

	public bool shatteredCommunity;

	public bool fleshTotem;

	public bool bloodPact;

	public bool bloodflareCore;

	public int bloodflareCoreRemainingHealOverTime;

	public bool chaliceOfTheBloodGod;

	public double chaliceBleedoutBuffer;

	public double chaliceDamagePointPartialProgress;

	public int chaliceBleedoutToApplyOnHurt;

	public int chaliceHitOriginalDamage;

	public bool chaliceHeartStyle;

	public bool elementalHeart;

	public bool crownJewel;

	public bool infectedJewel;

	public bool purity;

	public bool eleResist;

	public int PurityHealSlowdownFrames;

	public bool harpyRing;

	public bool angelTreads;

	public bool fleshKnuckles;

	public bool ironBoots;

	public bool depthCharm;

	public bool anechoicPlating;

	public bool jellyfishNecklace;

	public bool fairyBoots;

	public bool flameWakerBoots;

	public bool hellfireTreads;

	public int bootLevel;

	public bool sSpiritAmulet;

	public int sSpiritAmuletTimer;

	public bool sSpiritAmuletVisual;

	public bool dOfTheDeep;

	public int dOfTheDeepTimer;

	public bool dOfTheDeepVisual;

	public int dOfTheDeepDefenseBuffMax;

	public int dOfTheDeepDefenseBuffTimer;

	public bool oceanCrest;

	public bool aquaticEmblem;

	public bool spiritOrigin;

	public bool spiritOriginVanity;

	public int spiritOriginCritBoost;

	public float critDamage;

	public bool darkSunRing;

	public bool crawCarapace;

	public bool baroclaw;

	public bool IsFirstDashFrame;

	public int fallingBootVelCheckTimer;

	public bool voidOfCalamity;

	public bool voidOfExtinction;

	public bool eArtifact;

	public bool dArtifact;

	public bool auricSArtifact;

	public bool pSoulArtifact;

	public bool giantPearl;

	public bool normalityRelocator;

	public bool flameLickedShell;

	public int flameLickedShellParry;

	public bool flameLickedShellEmpoweredParry;

	public bool sPauldron;

	public bool sPauldronVisual;

	public bool XykVisualsBlue;

	public bool XykVisualsOrange;

	public Color XykFXColor;

	public int XykWingTimer;

	public Color lightRGB;

	public bool manaOverloader;

	public bool royalGel;

	public bool handWarmer;

	public bool ursaSergeant;

	public bool ursaSergeantVisual;

	public bool scuttlersJewel;

	public int scuttlerCooldown;

	public bool thiefsDime;

	public bool dynamoStemCells;

	public bool etherealExtorter;

	public bool blazingCore;

	public int blazingCoreParry;

	public int blazingCoreSuccessfulParry;

	public bool blazingCoreEmpoweredParry;

	public bool voltaicJelly;

	public bool jellyChargedBattery;

	public float summonProjCooldown;

	public bool sandElemental;

	public bool sandElementalVanity;

	public bool rareSandElemental;

	public bool rareSandElementalVanity;

	public bool cloudElemental;

	public bool cloudElementalVanity;

	public bool brimElemental;

	public bool brimElementalVanity;

	public bool waterElemental;

	public bool waterElementalVanity;

	public bool fungalClump;

	public bool fungalClumpVanity;

	public bool howlsHeart;

	public bool howlsHeartVanity;

	public bool darkGodSheath;

	public bool inkBomb;

	public bool abyssalMirror;

	public bool eclipseMirror;

	public bool featherCrown;

	public bool moonCrown;

	public int rogueCrownCooldown;

	public bool dragonScales;

	public bool gloveOfPrecision;

	public bool gloveOfRecklessness;

	public bool vampiricTalisman;

	public bool electricianGlove;

	public bool bloodyGlove;

	public bool filthyGlove;

	public bool sandCloak;

	public bool getSandCloakAccelBoost;

	public bool spectralVeil;

	public int spectralVeilImmunity;

	public bool hasJetpack;

	public bool plaguedFuelPack;

	public bool blunderBooster;

	public bool blunderBoosterVisibility;

	public int jetPackDash;

	public int jetPackDirection;

	public bool veneratedLocket;

	public bool camper;

	public bool corrosiveSpine;

	public bool scionsCurio;

	public bool scionsCurioGotHit;

	public bool scionsCurioVisuals;

	public float scionsCurioDebuffDamage;

	public bool miniOldDuke;

	public bool miniOldDukeVanity;

	public bool starbusterCore;

	public bool starTaintedGenerator;

	public bool hallowedRune;

	public int hallowedRuneCooldown;

	public bool phantomicArtifact;

	public int phantomicBulwarkCooldown;

	public int phantomicHeartRegen;

	public int wingProjectileCooldown;

	public bool noStupidNaturalARSpawns;

	public int voidFrameCounter;

	public int voidFrame;

	public bool rottenDogTooth;

	public bool angelicAlliance;

	public int angelicActivate;

	public bool ChaosStone;

	public bool CryoStone;

	public bool CryoStoneVanity;

	public bool voidField;

	public bool copyrightInfringementShield;

	public int ConsumableDodgeCooldown;

	public List<Func<Player, Player.HurtInfo, string?>> DodgeEffects;

	private bool storedShadowDodge;

	public int ArmorSetBonusKeyHeldTimer;

	public bool silverMedkit;

	public int silverMedkitTimer;

	public bool tungstenArmorHookBoost;

	public bool goldArmorGoldDrops;

	public bool miningSet;

	public int miningSetCooldown;

	public bool desertProwler;

	public bool snowRuffianSet;

	public bool forbiddenCirclet;

	public int forbiddenCooldown;

	public int tornadoCooldown;

	public bool eskimoSet;

	public bool rainSet;

	public bool meteorSet;

	public bool necroSet;

	public bool frostSet;

	public bool victideSet;

	public bool victideSummoner;

	public bool sulphurSet;

	public bool sulphurJump;

	public int sulphurBubbleCooldown;

	public bool aeroSet;

	public bool statigelSet;

	public bool tarraSet;

	public bool tarraMelee;

	public bool tarragonCloak;

	public int tarraDefenseTime;

	public bool tarraMage;

	public int tarraCrits;

	public bool tarraRanged;

	public int tarraRangedCooldown;

	public bool tarraThrowing;

	public bool tarragonImmunity;

	public int tarraThrowingCrits;

	public bool tarraSummon;

	public bool bloodflareSet;

	public bool bloodflareMelee;

	public bool bloodflareFrenzy;

	public int bloodflareMeleeHits;

	public bool bloodflareRanged;

	public bool bloodflareThrowing;

	public bool bloodflareMage;

	public int bloodflareMageCooldown;

	public bool bloodflareSummon;

	public int bloodflareSummonTimer;

	public bool godSlayer;

	public bool godSlayerDamage;

	public bool godSlayerRanged;

	public bool godSlayerThrowing;

	public bool godSlayerDashHotKeyPressed;

	public bool SpeedBlasterDashStarted;

	public bool ataxiaBolt;

	public bool ataxiaVolley;

	public bool ataxiaBlaze;

	public bool hydrothermalSmoke;

	public bool daedalusAbsorb;

	public bool daedalusShard;

	public bool brimflameSet;

	public bool brimflameFrenzy;

	public bool lunicCorpsSet;

	public bool lunicCorpsLegs;

	public bool shadeRegen;

	public bool shadowSpeed;

	public bool dsSetBonus;

	public bool auricSetMelee;

	public bool daedalusReflect;

	public bool daedalusSplit;

	public bool titanHeartSet;

	public bool titanHeartMask;

	public bool titanHeartMantle;

	public int titanCooldown;

	public bool umbraphileSet;

	public bool reaverSpeed;

	public bool reaverDefense;

	public bool reaverExplore;

	public bool fathomSwarmer;

	public bool fathomSwarmerVisage;

	public bool fathomSwarmerBreastplate;

	public bool fathomSwarmerTail;

	public int tailFrameUp;

	public int tailFrame;

	public bool astralStarRain;

	public int astralStarRainCooldown;

	public int AbaddonCooldown;

	public int VoidCooldown;

	public int ursaSergeantCooldown;

	public int AlchFlaskCooldown;

	public bool plagueReaper;

	public bool plaguebringerPatronSet;

	public bool plaguebringerCarapace;

	public float ataxiaDmg;

	public bool ataxiaMage;

	public bool ataxiaGeyser;

	public float xerocDmg;

	public bool xerocSet;

	public bool prismaticSet;

	public bool prismaticHelmet;

	public bool prismaticRegalia;

	public bool prismaticGreaves;

	public int prismaticLasers;

	public bool silvaSet;

	public bool silvaMage;

	public int silvaMageCooldown;

	public bool silvaSummon;

	public bool hasSilvaEffect;

	public int silvaCountdown;

	public bool auricSet;

	public bool omegaBlueChestplate;

	public bool omegaBlueSet;

	public bool omegaBlueAbyssalMadness;

	public bool valkyrie;

	public bool slimeGod;

	public bool molluskHelmet;

	public bool molluskChest;

	public bool molluskLegs;

	public bool fearmongerSet;

	public int fearmongerRegenFrames;

	public bool daedalusCrystal;

	public bool chaosSpirit;

	public bool redDevil;

	public bool GemTechSet;

	public bool CobaltSet;

	public bool MythrilSet;

	public int MythrilFlareSpawnCountdown;

	public bool AdamantiteSet;

	public int AdamantiteSetDecayDelay;

	public int ChlorophyteHealDelay;

	public bool WearingPostMLSummonerSet;

	private float adamantiteSetDefenseBoostInterpolant;

	private GemTechArmorState gemTechState;

	public bool alcoholPoisoning;

	public bool shadowflame;

	public bool daybroken;

	public bool whisperingDeath;

	public bool dragonFire;

	public bool vermillionFlux;

	public bool auricRebuke;

	public bool staticDischarge;

	public bool miracleBlight;

	public bool armorCrunch;

	public bool crumble;

	public bool irradiated;

	public bool brimstoneFlames;

	public bool weakBrimstoneFlames;

	public bool demonicFlames;

	public bool godSlayerInferno;

	public bool astralInfection;

	public bool plague;

	public bool holyFlames;

	public bool holyInferno;

	public bool burningBlood;

	public bool brainRot;

	public bool heavybleeding;

	public bool laceration;

	public bool elementalMix;

	public bool icarusFolly;

	public bool weakPetrification;

	public bool vHex;

	public bool trueVHex;

	public bool DoGExtremeGravity;

	public bool warped;

	public bool crushDepth;

	public bool riptide;

	public bool hadopelagicPressure;

	public bool fishAlert;

	public bool clamity;

	public bool NOU;

	public bool absorberAffliction;

	public bool sulphurPoison;

	public bool nightwither;

	public bool voidfrost;

	public bool eutrophication;

	public bool frozenLungs;

	public bool searingLava;

	public bool vaporfied;

	public bool banishingFire;

	public bool wither;

	public bool ManaBurn;

	public int ImmobilityDebuffImmunityTimer;

	public const int ImmobilityDebuffImmunityTimerMax = 300;

	public const int SulphSeaWaterSafetyTime = 720;

	public const int SulphSeaWaterRecoveryTime = 150;

	public bool sandsWindBuff;

	public bool aeolianEarthBuff;

	public int chiBuffTimer;

	public bool corrEffigy;

	public bool crimEffigy;

	public bool decayEffigy;

	public bool rRage;

	public bool tRegen;

	public bool xWrath;

	public bool graxDefense;

	public bool encased;

	public bool omniscience;

	public bool zerg;

	public bool zen;

	public bool isNearbyBoss;

	public bool flaskBrimstone;

	public bool purpleHaze;

	public int purpleHazeStealthTimer;

	public bool mushy;

	public bool PinkJellyRegen;

	public bool GreenJellyRegen;

	public bool AbsorberRegen;

	public bool cFreeze;

	public bool shine;

	public bool anechoicCoating;

	public bool enraged;

	public bool permafrostsConcoction;

	public bool flaskCrumbling;

	public bool ceaselessHunger;

	public bool calcium;

	public bool soaring;

	public bool bounding;

	public bool shadow;

	public bool photosynthesis;

	public bool astralInjection;

	public bool gravityNormalizer;

	public bool flaskHoly;

	public bool galvanicCorrosion;

	public bool sulphurskin;

	public bool baguette;

	public bool vodka;

	public bool redWine;

	public float redWineStoredY;

	public bool grapeBeer;

	public bool moonshine;

	public bool rum;

	public bool whiskey;

	public bool fireball;

	public bool everclear;

	public bool bloodyMary;

	public bool tequila;

	public bool caribbeanRum;

	public bool cinnamonRoll;

	public bool tequilaSunrise;

	public bool margarita;

	public bool oldFashioned;

	public bool starBeamRye;

	public bool screwdriver;

	public bool moscowMule;

	public bool whiteWine;

	public float whiteWineTimer;

	public bool evergreenGin;

	public bool tranquilityCandle;

	public bool chaosCandle;

	public bool blueCandle;

	public bool pinkCandle;

	public double pinkCandleHealFraction;

	public bool yellowCandle;

	public bool trippy;

	public bool amidiasBlessing;

	public bool bloodfinBoost;

	public int bloodfinTimer;

	public bool hallowedRegen;

	public bool hallowedPower;

	public bool avertorBonus;

	public bool divineBless;

	public bool infiniteFlight;

	public int hasteCounter;

	public int hasteLevel;

	public bool wDroid;

	public bool resButterfly;

	public bool hasVoidEaterMarionette;

	public bool IceClasperBool;

	public bool magicHat;

	public bool herring;

	public bool blackhawk;

	public bool cosmicViper;

	public bool CalamarisLament;

	public bool cEyes;

	public bool cSlime;

	public bool cSlime2;

	public bool aSlime;

	public bool brittleStar;

	public bool aquaticStar;

	public bool sunSpirit;

	public bool dCreeper;

	public bool eAxe;

	public bool endoCooper;

	public bool vengefulSunMinion;

	public bool sirius;

	public bool aChicken;

	public bool cLamp;

	public bool pGuy;

	public bool sandnado;

	public bool PlantationSummon;

	public bool astralProbe;

	public bool pSoulGuardians;

	public int healCounter;

	public bool cEnergy;

	public bool shellfish;

	public bool hCrab;

	public bool allElementals;

	public bool allElementalsVanity;

	public bool sCrystal;

	public bool sandEleBuff;

	public bool rareSandEleBuff;

	public bool cloudEleBuff;

	public bool brimEleBuff;

	public bool waterEleBuff;

	public bool fClump;

	public bool rDevil;

	public bool aValkyrie;

	public bool apexShark;

	public bool gastricBelcher;

	public bool hauntedDishes;

	public bool stormjaw;

	public bool sGod;

	public bool victideSnail;

	public bool cSpirit;

	public bool dCrystal;

	public bool endoHydra;

	public bool powerfulRaven;

	public bool dragonFamily;

	public bool providenceStabber;

	public bool seashineSwordBuff;

	public bool saros;

	public int sarosEclipseBeamUsage;

	public bool plaguebringerMK2;

	public bool igneousExaltation;

	public bool GlacialEmbrace;

	public bool voidAura;

	public bool voidAuraDamage;

	public bool voidConcentrationAura;

	public bool MutatedTruffleBool;

	public bool virili;

	public bool frostBlossom;

	public bool cinderBlossom;

	public bool belladonaSpirit;

	public bool puffWarrior;

	public bool vileFeeder;

	public bool scabRipper;

	public bool midnightUFO;

	public bool plagueEngine;

	public bool brimseeker;

	public bool necrosteocytesDudes;

	public bool gammaHead;

	public bool tundraFlameBlossom;

	public bool snakeEyes;

	public bool poleWarper;

	public bool aqueousHunterDrone;

	public bool causticDragon;

	public bool plaguebringerPatronSummon;

	public bool howlTrio;

	public bool mountedScanner;

	public bool sepulcher;

	public bool daedalusGolem;

	public bool deathstareEyeball;

	public bool witherBlossom;

	public bool flowersOfMortality;

	public bool viridVanguard;

	public bool ViridVanguardActiveAttackerThisFrame;

	public float ViridVanguardRotation;

	public float ViridVanguardActiveCooldown;

	public float ViridVanguardRotationToAdd;

	public bool InvertExaltationLineRotationDirections;

	public bool sageSpirit;

	public bool fleshBall;

	public bool eyeOfNight;

	public bool soulSeeker;

	public bool perditionBeacon;

	public bool MoonFist;

	public bool AresCannons;

	public bool celestialDragons;

	public bool KalandraMirror;

	public bool StellarTorus;

	public bool LiliesOfFinalityBool;

	public bool EnchantedKnifeStaffBool;

	public bool AmphibiansGuitarBool;

	public bool forceSummonTagCrit;

	public bool forceSummonTagMultiplicative;

	public int bonusFlatTag;

	public float bonusCritTag;

	public float bonusMultTag;

	public bool abyssDeath;

	public int abyssBreathCD;

	public float caveDarkness;

	public bool abyssalDivingSuit;

	public bool abyssalDivingSuitPrevious;

	public bool profanedCrystal;

	public int profanedCrystalStatePrevious;

	public bool profanedCrystalPrevious;

	public int profanedCrystalAnim;

	public bool profanedCrystalBuffs;

	public int pscState;

	public Color pscLerpColor;

	public bool aquaticHeartPrevious;

	public bool aquaticHeart;

	public bool snowmanNoseless;

	public bool meldTransformationPrevious;

	public bool meldTransformation;

	public bool meldTransformationForce;

	public bool meldTransformationPower;

	public bool omegaBlueTransformationPrevious;

	public bool omegaBlueTransformation;

	public bool omegaBlueTransformationForce;

	public bool omegaBlueTransformationPower;

	public bool cursedSummonsEnchant;

	public bool flamingItemEnchant;

	public bool lifeManaEnchant;

	public bool farProximityRewardEnchant;

	public bool closeProximityRewardEnchant;

	public bool dischargingItemEnchant;

	public bool explosiveMinionsEnchant;

	public bool bladeArmEnchant;

	public bool manaMonsterEnchant;

	public bool witheringWeaponEnchant;

	public bool witheredDebuff;

	public int witheredWeaponHoldTime;

	public int witheringDamageDone;

	public bool persecutedEnchant;

	public int persecutedEnchantSummonTimer;

	public bool lecherousOrbEnchant;

	public bool awaitingLecherousOrbSpawn;

	public FireParticleSet ProvidenceBurnEffectDrawer;

	public FluidField CalamityFireDrawer;

	public FluidField ProfanedMoonlightAuroraDrawer;

	public ArmorShaderData CalamityFireDyeShader;

	public Vector2 FireDrawerPosition;

	public int monolithAccursedShader;

	public int monolithBossRushShader;

	public int monolithExoShader;

	public int monolithLeviathanShader;

	public int monolithPlagueShader;

	public int monolithCryogenShader;

	public int monolithAstralShader;

	public int monolithDevourerBShader;

	public int monolithDevourerPShader;

	public int monolithYharonShader;

	public int BrimstoneLavaFountainCounter;

	public FireParticleSet ManaBurnFireDrawer;

	public bool AbleToSelectExoMech;

	public bool HasTalkedAtCodebreaker;

	public bool HasCraftedDraedonsForge;

	public List<ulong> SeenDraedonDialogs;

	public bool mouseRight;

	private bool oldMouseRight;

	public float oldGravDir;

	public float tempGravDir;

	public bool justChangedGravity;

	public Vector2 mouseWorldDeltaFromPlayer;

	public float mouseRotationFromPlayer;

	private Vector2 oldMouseWorldDeltaFromPlayer;

	private int mouseWorldPacketTimer;

	private const int MouseWorldPacketInterval = 2;

	public bool rightClickListener;

	public bool mouseWorldListener;

	public bool mouseRotationListener;

	public bool syncMousePosition;

	public bool syncMouseRotation;

	public bool syncMouseRightClick;

	private static int startMessageDisplayDelay;

	public bool ShouldHideControls;

	public bool pressedRight;

	public bool pressedLeft;

	public bool pressedUp;

	public bool pressedDown;

	public int VerticalOmnidashTimer;

	private string dashID;

	public string DeferredDashID;

	public string LastUsedDashID;

	public static readonly List<Color> MoonlightDyeDayColors;

	public static readonly List<Color> MoonlightDyeNightColors;

	public CalamityPlayerDrawingParameters drawingParameters;

	private CalamityPlayerDrawingParameters drawingParameters_LastNetSyncValue;

	private int drawingParameters_NetSyncCountdown;

	internal const int GlobalSyncPacketTimer = 15;

	public bool countsAsAnyWet
	{
		get
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if (base.Player.armor[0].type != 250 && (!Main.IsItRaining || !((double)base.Player.Center.Y < Main.worldSurface * 16.0)) && !base.Player.dripping && base.Player.wetCount <= 0 && !base.Player.wet && !base.Player.honeyWet)
			{
				return base.Player.lavaWet;
			}
			return true;
		}
	}

	public bool exaltedKillMode
	{
		get
		{
			if (demonSwordKillMode)
			{
				return base.Player.HeldItem.type == ModContent.ItemType<ExaltedOathblade>();
			}
			return false;
		}
	}

	public bool devilsDevastationKillMode
	{
		get
		{
			if (demonSwordKillMode)
			{
				return base.Player.HeldItem.type == ModContent.ItemType<DevilsDevastation>();
			}
			return false;
		}
	}

	public bool RageEnabled
	{
		get
		{
			if (externalRageEnabled.HasValue)
			{
				return externalRageEnabled.Value;
			}
			if (!CalamityWorld.revenge)
			{
				return shatteredCommunity;
			}
			return true;
		}
	}

	public bool AdrenalineEnabled
	{
		get
		{
			if (externalAdrenalineEnabled.HasValue)
			{
				return externalAdrenalineEnabled.Value;
			}
			if (!CalamityWorld.revenge)
			{
				return draedonsHeart;
			}
			return true;
		}
	}

	public int CurrentDefenseDamage => (int)((float)totalDefenseDamage * ((float)defenseDamageRecoveryFrames / (float)totalDefenseDamageRecoveryFrames));

	public bool HasAnyEnergyShield
	{
		get
		{
			if (!roverDrive && !lunicCorpsSet && (!pSoulArtifact || profanedCrystal) && !profanedCrystalBuffs && !sponge)
			{
				return Starshield > 0;
			}
			return true;
		}
	}

	public int TotalEnergyShielding => RoverDriveShieldDurability + LunicCorpsShieldDurability + pSoulShieldDurability + SpongeShieldDurability;

	public int TotalMaxShieldDurability => (roverDrive ? RoverDrive.ShieldDurabilityMax : 0) + (lunicCorpsSet ? LunicCorpsHelmet.ShieldDurabilityMax : 0) + (profanedCrystalBuffs ? ProfanedSoulCrystal.ShieldDurabilityMax : ((pSoulArtifact && !profanedCrystal) ? ProfanedSoulArtifact.ShieldDurabilityMax : 0)) + (sponge ? TheSponge.ShieldDurabilityMax : 0);

	public int AdamantiteSetDefenseBoost
	{
		get
		{
			return (int)(MathHelper.Clamp(adamantiteSetDefenseBoostInterpolant, 0f, 1f) * 10f);
		}
		set
		{
			adamantiteSetDefenseBoostInterpolant = MathHelper.Clamp((float)value / 10f, 0f, 1f);
		}
	}

	public GemTechArmorState GemTechState
	{
		get
		{
			if (gemTechState == null || gemTechState.HasInvalidOwner)
			{
				gemTechState = new GemTechArmorState(base.Player.whoAmI);
			}
			return gemTechState;
		}
		set
		{
			gemTechState = value;
		}
	}

	public bool ZoneSunkenSea
	{
		get
		{
			if (!ZoneTimelessShores && !ZoneRadiantReefs && !ZonePolypForest && !ZoneGleamingBurrows && !ZoneClamDen)
			{
				return ZoneBasaltGully;
			}
			return true;
		}
	}

	public bool ZoneTimelessShores => base.Player.InModBiome<TimelessShoresBiome>();

	public bool ZonePolypForest => base.Player.InModBiome<PolypForestBiome>();

	public bool ZoneRadiantReefs => base.Player.InModBiome<RadiantReefsBiome>();

	public bool ZoneGleamingBurrows => base.Player.InModBiome<GleamingBurrowsBiome>();

	public bool ZoneClamDen => base.Player.InModBiome<ClamDenBiome>();

	public bool ZoneBasaltGully => base.Player.InModBiome<BasaltGullyBiome>();

	public bool ZoneSulphur => base.Player.InModBiome<SulphurousSeaBiome>();

	public bool ZoneAbyss
	{
		get
		{
			if (!ZoneAbyssLayer1 && !ZoneAbyssLayer2 && !ZoneAbyssLayer3)
			{
				return ZoneAbyssLayer4;
			}
			return true;
		}
	}

	public bool ZoneAbyssLayer1 => base.Player.InModBiome<AbyssLayer1Biome>();

	public bool ZoneAbyssLayer2 => base.Player.InModBiome<AbyssLayer2Biome>();

	public bool ZoneAbyssLayer3 => base.Player.InModBiome<AbyssLayer3Biome>();

	public bool ZoneAbyssLayer4 => base.Player.InModBiome<AbyssLayer4Biome>();

	public bool ZoneCalamity => base.Player.InModBiome<BrimstoneCragsBiome>();

	public bool ZoneAstral
	{
		get
		{
			if (base.Player.InModBiome<AstralInfectionBiome>())
			{
				return !ZoneAbyss;
			}
			return false;
		}
	}

	public bool InAnyCalamityBiome
	{
		get
		{
			if (!ZoneAbyss && !ZoneCalamity && !ZoneSulphur && !ZoneSunkenSea)
			{
				return ZoneAstral;
			}
			return true;
		}
	}

	public Vector2 mouseWorld
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			return base.Player.MountedCenter + mouseWorldDeltaFromPlayer;
		}
	}

	public Vector2 mouseNormalFromPlayer
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return mouseRotationFromPlayer.ToRotationVector2();
		}
	}

	public string DashID
	{
		get
		{
			if (!string.IsNullOrEmpty(dashID) || base.Player.dashType != 0 || !CalamityServerConfig.Instance.DefaultDashEnabled)
			{
				return dashID;
			}
			return DefaultDash.ID;
		}
		set
		{
			dashID = value;
		}
	}

	public PlayerDashEffect UsedDash
	{
		get
		{
			PlayerDashManager.FindByID(DashID, out var dashEffect);
			return dashEffect;
		}
	}

	public bool HasCustomDash => !string.IsNullOrEmpty(DashID);

	internal Vector2 RandomDebuffVisualSpot
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			return base.Player.Center + new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-20f, 20f));
		}
	}

	public override void Initialize()
	{
		spawnedPunchCard = false;
		extraAccessoryML = false;
		eCore = false;
		mFruit = false;
		sTangerine = false;
		tCloudberry = false;
		sStrawberry = false;
		pHeart = false;
		cShard = false;
		revJamDrop = false;
		rageBoostOne = false;
		rageBoostTwo = false;
		rageBoostThree = false;
		adrenalineBoostOne = false;
		adrenalineBoostTwo = false;
		adrenalineBoostThree = false;
		drawBossHPBar = true;
		shouldDrawSmallText = true;
		newMerchantInventory = false;
		newPainterInventory = false;
		newGolferInventory = false;
		newZoologistInventory = false;
		newDyeTraderInventory = false;
		newPartyGirlInventory = false;
		newStylistInventory = false;
		newDemolitionistInventory = false;
		newDryadInventory = false;
		newTavernkeepInventory = false;
		newArmsDealerInventory = false;
		newGoblinTinkererInventory = false;
		newWitchDoctorInventory = false;
		newClothierInventory = false;
		newMechanicInventory = false;
		newPirateInventory = false;
		newTruffleInventory = false;
		newWizardInventory = false;
		newSteampunkerInventory = false;
		newCyborgInventory = false;
		newPrincessInventory = false;
		newSkeletonMerchantInventory = false;
		newPermafrostInventory = false;
		newAmidiasInventory = false;
		newBanditInventory = false;
		newCalamitasInventory = false;
		cooldowns = new Dictionary<string, CooldownInstance>(16);
	}

	public override void SaveData(TagCompound tag)
	{
		List<string> boost = new List<string>();
		boost.AddWithCondition("spawnedPunchCard", spawnedPunchCard);
		boost.AddWithCondition("extraAccessoryML", extraAccessoryML);
		boost.AddWithCondition("etherealCore", eCore);
		boost.AddWithCondition("miracleFruit", mFruit);
		boost.AddWithCondition("bloodOrange", sTangerine);
		boost.AddWithCondition("elderBerry", tCloudberry);
		boost.AddWithCondition("dragonFruit", sStrawberry);
		boost.AddWithCondition("phantomHeart", pHeart);
		boost.AddWithCondition("cometShard", cShard);
		boost.AddWithCondition("revJam", revJamDrop);
		boost.AddWithCondition("rageOne", rageBoostOne);
		boost.AddWithCondition("rageTwo", rageBoostTwo);
		boost.AddWithCondition("rageThree", rageBoostThree);
		boost.AddWithCondition("adrenalineOne", adrenalineBoostOne);
		boost.AddWithCondition("adrenalineTwo", adrenalineBoostTwo);
		boost.AddWithCondition("adrenalineThree", adrenalineBoostThree);
		boost.AddWithCondition("bossHPBar", drawBossHPBar);
		boost.AddWithCondition("drawSmallText", shouldDrawSmallText);
		boost.AddWithCondition("newMerchantInventory", newMerchantInventory);
		boost.AddWithCondition("newPainterInventory", newPainterInventory);
		boost.AddWithCondition("newGolferInventory", newGolferInventory);
		boost.AddWithCondition("newZoologistInventory", newZoologistInventory);
		boost.AddWithCondition("newDyeTraderInventory", newDyeTraderInventory);
		boost.AddWithCondition("newPartyGirlInventory", newPartyGirlInventory);
		boost.AddWithCondition("newStylistInventory", newStylistInventory);
		boost.AddWithCondition("newDemolitionistInventory", newDemolitionistInventory);
		boost.AddWithCondition("newDryadInventory", newDryadInventory);
		boost.AddWithCondition("newTavernkeepInventory", newTavernkeepInventory);
		boost.AddWithCondition("newArmsDealerInventory", newArmsDealerInventory);
		boost.AddWithCondition("newGoblinTinkererInventory", newGoblinTinkererInventory);
		boost.AddWithCondition("newWitchDoctorInventory", newWitchDoctorInventory);
		boost.AddWithCondition("newClothierInventory", newClothierInventory);
		boost.AddWithCondition("newMechanicInventory", newMechanicInventory);
		boost.AddWithCondition("newPirateInventory", newPirateInventory);
		boost.AddWithCondition("newTruffleInventory", newTruffleInventory);
		boost.AddWithCondition("newWizardInventory", newWizardInventory);
		boost.AddWithCondition("newSteampunkerInventory", newSteampunkerInventory);
		boost.AddWithCondition("newCyborgInventory", newCyborgInventory);
		boost.AddWithCondition("newPrincessInventory", newPrincessInventory);
		boost.AddWithCondition("newSkeletonMerchantInventory", newSkeletonMerchantInventory);
		boost.AddWithCondition("newPermafrostInventory", newPermafrostInventory);
		boost.AddWithCondition("newAmidiasInventory", newAmidiasInventory);
		boost.AddWithCondition("newBanditInventory", newBanditInventory);
		boost.AddWithCondition("newCalamitasInventory", newCalamitasInventory);
		boost.AddWithCondition("GivenBrimstoneLocus", GivenBrimstoneLocus);
		boost.AddWithCondition("HasTalkedAtCodebreaker", HasTalkedAtCodebreaker);
		boost.AddWithCondition("HasCraftedDraedonsForge", HasCraftedDraedonsForge);
		long totalTicks = previousSessionTotal.Add(SpeedrunTimerSystem.Elapsed).Ticks;
		TagCompound cooldownsTag = new TagCompound();
		Dictionary<string, CooldownInstance>.Enumerator cdIterator = cooldowns.GetEnumerator();
		while (cdIterator.MoveNext())
		{
			KeyValuePair<string, CooldownInstance> kv = cdIterator.Current;
			string id = kv.Key;
			CooldownInstance instance = kv.Value;
			if (instance.handler.SavedWithPlayer)
			{
				TagCompound singleCDTag = instance.Save();
				cooldownsTag.Add(id, singleCDTag);
			}
		}
		tag["boost"] = boost;
		tag["rage"] = rage;
		tag["adrenaline"] = adrenaline;
		tag["aquaticBoostPower"] = aquaticBoost;
		tag["sCalDeathCount"] = sCalDeathCount;
		tag["sCalKillCount"] = sCalKillCount;
		tag["moveSpeedBonus"] = moveSpeedBonus;
		tag["defenseDamage"] = totalDefenseDamage;
		tag["defenseDamageRecoveryFrames"] = defenseDamageRecoveryFrames;
		tag["totalSpeedrunTicks"] = totalTicks;
		tag["lastSplitType"] = lastSplitType;
		tag["lastSplitTicks"] = lastSplit.Ticks;
		tag["cooldowns"] = cooldownsTag;
		tag["SeenDraedonDialogs"] = SeenDraedonDialogs;
	}

	public override void LoadData(TagCompound tag)
	{
		IList<string> boost = tag.GetList<string>("boost");
		spawnedPunchCard = boost.Contains("spawnedPunchCard");
		extraAccessoryML = boost.Contains("extraAccessoryML");
		eCore = boost.Contains("etherealCore");
		mFruit = boost.Contains("miracleFruit");
		sTangerine = boost.Contains("bloodOrange");
		tCloudberry = boost.Contains("elderBerry");
		sStrawberry = boost.Contains("dragonFruit");
		pHeart = boost.Contains("phantomHeart");
		cShard = boost.Contains("cometShard");
		revJamDrop = boost.Contains("revJam");
		rageBoostOne = boost.Contains("rageOne");
		rageBoostTwo = boost.Contains("rageTwo");
		rageBoostThree = boost.Contains("rageThree");
		adrenalineBoostOne = boost.Contains("adrenalineOne");
		adrenalineBoostTwo = boost.Contains("adrenalineTwo");
		adrenalineBoostThree = boost.Contains("adrenalineThree");
		drawBossHPBar = boost.Contains("bossHPBar");
		shouldDrawSmallText = boost.Contains("drawSmallText");
		newMerchantInventory = boost.Contains("newMerchantInventory");
		newPainterInventory = boost.Contains("newPainterInventory");
		newGolferInventory = boost.Contains("newGolferInventory");
		newZoologistInventory = boost.Contains("newZoologistInventory");
		newDyeTraderInventory = boost.Contains("newDyeTraderInventory");
		newPartyGirlInventory = boost.Contains("newPartyGirlInventory");
		newStylistInventory = boost.Contains("newStylistInventory");
		newDemolitionistInventory = boost.Contains("newDemolitionistInventory");
		newDryadInventory = boost.Contains("newDryadInventory");
		newTavernkeepInventory = boost.Contains("newTavernkeepInventory");
		newArmsDealerInventory = boost.Contains("newArmsDealerInventory");
		newGoblinTinkererInventory = boost.Contains("newGoblinTinkererInventory");
		newWitchDoctorInventory = boost.Contains("newWitchDoctorInventory");
		newClothierInventory = boost.Contains("newClothierInventory");
		newMechanicInventory = boost.Contains("newMechanicInventory");
		newPirateInventory = boost.Contains("newPirateInventory");
		newTruffleInventory = boost.Contains("newTruffleInventory");
		newWizardInventory = boost.Contains("newWizardInventory");
		newSteampunkerInventory = boost.Contains("newSteampunkerInventory");
		newCyborgInventory = boost.Contains("newCyborgInventory");
		newPrincessInventory = boost.Contains("newPrincessInventory");
		newSkeletonMerchantInventory = boost.Contains("newSkeletonMerchantInventory");
		newPermafrostInventory = boost.Contains("newPermafrostInventory");
		newAmidiasInventory = boost.Contains("newAmidiasInventory");
		newBanditInventory = boost.Contains("newBanditInventory");
		newCalamitasInventory = boost.Contains("newCalamitasInventory");
		GivenBrimstoneLocus = boost.Contains("GivenBrimstoneLocus");
		HasTalkedAtCodebreaker = boost.Contains("HasTalkedAtCodebreaker");
		HasCraftedDraedonsForge = boost.Contains("HasCraftedDraedonsForge");
		rage = (tag.ContainsKey("rage") ? tag.GetFloat("rage") : 0f);
		if (tag.ContainsKey("adrenaline"))
		{
			object adrenObj = tag["adrenaline"];
			if (adrenObj is float adrenFloat)
			{
				adrenaline = adrenFloat;
			}
			else if (adrenObj is int adrenInt)
			{
				adrenaline = adrenInt;
			}
			else
			{
				adrenaline = 0f;
			}
		}
		if (tag.ContainsKey("aquaticBoostPower"))
		{
			aquaticBoost = tag.GetFloat("aquaticBoostPower");
		}
		sCalDeathCount = tag.GetInt("sCalDeathCount");
		sCalKillCount = tag.GetInt("sCalKillCount");
		if (tag.ContainsKey("moveSpeedBonus"))
		{
			moveSpeedBonus = tag.GetFloat("moveSpeedBonus");
		}
		totalDefenseDamage = tag.GetInt("defenseDamage");
		defenseDamageRecoveryFrames = tag.GetInt("defenseDamageRecoveryFrames");
		if (defenseDamageRecoveryFrames < 0)
		{
			defenseDamageRecoveryFrames = 0;
		}
		totalDefenseDamageRecoveryFrames = tag.GetInt("totalDefenseDamageRecoveryFrames");
		if (totalDefenseDamageRecoveryFrames <= 0)
		{
			totalDefenseDamageRecoveryFrames = 60;
		}
		long ticks = tag.GetLong("totalSpeedrunTicks");
		previousSessionTotal = new TimeSpan(ticks);
		lastSplitType = tag.GetInt("lastSplitType");
		ticks = tag.GetLong("lastSplitTicks");
		lastSplit = new TimeSpan(ticks);
		SeenDraedonDialogs = tag.GetList<ulong>("SeenDraedonDialogs").ToList();
		cooldowns.Clear();
		if (!tag.ContainsKey("cooldowns"))
		{
			return;
		}
		TagCompound cooldownsTag = tag.GetCompound("cooldowns");
		IEnumerator<KeyValuePair<string, object>> tagIterator = cooldownsTag.GetEnumerator();
		while (tagIterator.MoveNext())
		{
			string id = tagIterator.Current.Key;
			TagCompound singleCDTag = cooldownsTag.GetCompound(id);
			CooldownInstance instance = new CooldownInstance(base.Player, id, singleCDTag);
			if (instance.handler != null)
			{
				cooldowns.Add(id, instance);
			}
		}
	}

	public override void ResetEffects()
	{
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ab: Unknown result type (might be due to invalid IL or missing references)
		if (!areThereAnyDamnBosses)
		{
			DoGHeadHitCounter = 0;
		}
		ViridVanguardActiveAttackerThisFrame = Main.projectile.Any((Projectile x) => x.active && x.type == ModContent.ProjectileType<ViridVanguardBlade>() && x.owner == base.Player.whoAmI && x.ModProjectile<ViridVanguardBlade>().CurrentState == ViridVanguardBlade.ViridVanguardAIState.PhotonRipperZenithSlashes);
		ViridVanguardRotation = MathHelper.WrapAngle(ViridVanguardRotation + ViridVanguardRotationToAdd);
		ViridVanguardRotationToAdd = ViridVanguard.IdleCirclingSpeed;
		if (base.Player.HeldItem.type == ModContent.ItemType<ViridVanguard>())
		{
			if (ViridVanguardActiveCooldown > 0f)
			{
				ViridVanguardActiveCooldown--;
			}
		}
		else if (ViridVanguardActiveCooldown >= 0f && ViridVanguardActiveCooldown < (float)ViridVanguard.ActiveAttackCooldown)
		{
			ViridVanguardActiveCooldown++;
		}
		if (fleshKnuckles)
		{
			base.Player.statLifeMax2 += 25;
		}
		int percentMaxLifeIncrease = 0;
		if (bloodPact)
		{
			percentMaxLifeIncrease += 25;
		}
		if (chaliceOfTheBloodGod)
		{
			percentMaxLifeIncrease += 25;
		}
		if (affliction || afflicted)
		{
			percentMaxLifeIncrease += Affliction.MaxLifeBoostPercent;
		}
		if (community)
		{
			percentMaxLifeIncrease += (int)(TheCommunity.CalculatePower() * 50f);
		}
		if (shatteredCommunity)
		{
			percentMaxLifeIncrease += 10;
		}
		base.Player.statLifeMax2 += base.Player.statLifeMax / 5 / 20 * percentMaxLifeIncrease;
		if (crimEffigy)
		{
			base.Player.statLifeMax2 = (int)((float)base.Player.statLifeMax2 * (1f - CrimsonEffigy.MaxHealthLossPercent));
		}
		ResetRogueStealth();
		calamityBonusLuck = 0f;
		combHair = false;
		AdrenalineDuration = CalamityUtils.SecondsToFrames(5);
		defenseDamageRatio = 0.3333;
		contactDamageReduction = 0.0;
		projectileDamageReduction = 0.0;
		rogueVelocity = 1f;
		accStealthGenBoost = 0f;
		DashID = string.Empty;
		externalBreathTickBoost = 0f;
		externalFlightTimeMultBoost = 0f;
		externalRageEnabled = (externalAdrenalineEnabled = null);
		externalColdImmunity = (externalHeatImmunity = false);
		externalDefenseDamageImmunity = false;
		externalAuricRejectionImmunity = false;
		alcoholPoisonLevel = 0;
		noLifeRegen = false;
		if (HalleyAccuracyCounter > HalleysInferno.MaxAccuracy)
		{
			HalleyAccuracyCounter = HalleysInferno.MaxAccuracy;
		}
		if (HalleyAccuracyCounter < 0f)
		{
			HalleyAccuracyCounter = 0f;
		}
		while (StarburstSpawnFrameCounter >= 1f)
		{
			StratusStarburst++;
			StarburstSpawnFrameCounter--;
		}
		if (Starshield > 0)
		{
			Starshield--;
			StratusStarburstResetTimer = (int)MathHelper.Max(300f, (float)StratusStarburstResetTimer);
		}
		if (StratusStarburstResetTimer > 0)
		{
			StratusStarburstResetTimer--;
		}
		else if (StratusStarburst > 0)
		{
			StratusStarburst--;
		}
		if (StratusStarburst > MaxStratusStarburst)
		{
			StratusStarburst = MaxStratusStarburst;
		}
		int starpower = 0;
		int avaliableStarpower = 0;
		int oneCount = 0;
		Vector2 starSpawnPos = base.Player.Center;
		for (int i = 0; i < StarburstEntities.Count(); i++)
		{
			starpower += StarburstEntities[i].value;
			if (StarburstEntities[i].AICooldown <= 0)
			{
				avaliableStarpower += StarburstEntities[i].value;
			}
			if (starpower > StratusStarburst)
			{
				StarburstEntity deadStar = StarburstEntities[i];
				starpower -= deadStar.value;
				StarburstEntities.RemoveAt(i);
				starSpawnPos = deadStar.Center;
				i--;
			}
			else if (StarburstEntities[i].value == 1)
			{
				oneCount++;
			}
		}
		while (starpower < StratusStarburst)
		{
			StarburstEntities.Add(new StarburstEntity(starSpawnPos));
			oneCount++;
			starpower++;
			avaliableStarpower++;
		}
		while (oneCount >= 10)
		{
			StarburstEntity bigStar = StarburstEntities.First((StarburstEntity x) => x.value == 1);
			bigStar.value = 10;
			for (int i2 = 0; i2 < 9; i2++)
			{
				StarburstEntity star = StarburstEntities.Last((StarburstEntity x) => x.value == 1);
				bigStar.MergeChildren.Add(star);
				star.MergeTarget = bigStar;
				StarburstEntities.Remove(star);
			}
			oneCount -= 10;
		}
		AvaliableStarburst = avaliableStarpower;
		if (StratusStarburstResetTimer > 0)
		{
			if (cooldowns.TryGetValue(Starburst.ID, out var cooldown))
			{
				cooldown.timeLeft = MaxStratusStarburst - StratusStarburst;
			}
			else
			{
				base.Player.AddCooldown(Starburst.ID, MaxStratusStarburst);
			}
		}
		if (base.Player.whoAmI == Main.myPlayer)
		{
			if (!roverDrive)
			{
				RoverDriveShieldDurability = 0;
			}
			if (!lunicCorpsSet)
			{
				LunicCorpsShieldDurability = 0;
			}
			if (!sponge)
			{
				SpongeShieldDurability = 0;
			}
			if (!pSoulArtifact)
			{
				pSoulShieldDurability = 0;
			}
		}
		pSoulShieldVisible = false;
		roverDrive = false;
		roverDriveShieldVisible = false;
		sponge = false;
		spongeShieldVisible = false;
		thirdSage = false;
		perfmini = false;
		akato = false;
		yharonPet = false;
		burrowerPet = false;
		leviPet = false;
		plaguebringerBab = false;
		rotomPet = false;
		ladShark = false;
		sparks = false;
		sirenPet = false;
		spiritOriginPet = false;
		fox = false;
		chibii = false;
		brimling = false;
		bearPet = false;
		kendra = false;
		trashMan = false;
		astrophage = false;
		flakPet = false;
		babyGhostBell = false;
		radiator = false;
		scalPet = false;
		hiveMindPet = false;
		bendyPet = false;
		littleLightPet = false;
		pineapplePet = false;
		eidolonSnailPet = false;
		lordePet = false;
		frostyBat = false;
		toastyBat = false;
		onyxExcavator = false;
		rimehound = false;
		crysthamyr = false;
		ExoChair = false;
		miniOldDuke = false;
		miniOldDukeVanity = false;
		aquaticHeartWaterBuff = false;
		aquaticHeartIce = false;
		draedonsHeart = false;
		afflicted = false;
		chiRegen = false;
		affliction = false;
		dodgeScarf = false;
		evasionScarf = false;
		nebulousCore = false;
		godSlayer = false;
		godSlayerDamage = false;
		godSlayerRanged = false;
		godSlayerThrowing = false;
		silvaSet = false;
		silvaMage = false;
		silvaSummon = false;
		auricSet = false;
		auricSetMelee = false;
		GemTechSet = false;
		CobaltSet = false;
		MythrilSet = false;
		AdamantiteSet = false;
		WearingPostMLSummonerSet = false;
		omegaBlueChestplate = false;
		omegaBlueSet = false;
		omegaBlueAbyssalMadness = false;
		molluskHelmet = false;
		molluskChest = false;
		molluskLegs = false;
		fearmongerSet = false;
		ataxiaBolt = false;
		ataxiaGeyser = false;
		ataxiaVolley = false;
		ataxiaBlaze = false;
		ataxiaMage = false;
		shadeRegen = false;
		shadowSpeed = false;
		dsSetBonus = false;
		wearingRogueArmor = false;
		blockAllDashes = false;
		blazingCursorDamage = false;
		blazingCursorVisuals = false;
		luxorsGift = false;
		luxorsGiftVanity = false;
		fungalSymbiote = false;
		trinketOfChi = false;
		gladiatorSword = false;
		unstableGraniteCore = false;
		regenerator = false;
		deepDiver = false;
		theBee = false;
		arcFlashRing = false;
		arcFlashRingVisual = false;
		bGlassBand = false;
		bGlassBandVisual = false;
		batholithBangle = false;
		batholithBangleVisual = false;
		protolithBangle = false;
		protolithBangleVisual = false;
		aFossil = false;
		aPowder = false;
		fallingBlockProtection = false;
		trapProtection = false;
		alluringBait = false;
		enchantedPearl = false;
		fishingStation = false;
		rBrain = false;
		bloodyWormTooth = false;
		ivDrip = false;
		vexation = false;
		badgeOfBravery = false;
		if (!WarbanneroftheRighteous)
		{
			cooldowns.Remove(WarbanneroftheRighteousBuff.ID);
		}
		WarbanneroftheRighteous = false;
		warbannerGlow = false;
		ilSpark = false;
		transformer = false;
		transformerVisual = false;
		bloomStone = false;
		bloomStoneHookVisuals = false;
		hideOfDeus = false;
		dAmulet = false;
		rampartOfDeities = false;
		gShell = false;
		lAmbergris = false;
		tortShell = false;
		absorber = false;
		honeyDewHalveDebuffs = false;
		livingDewHalveDebuffs = false;
		aAmpoule = false;
		rOoze = false;
		radiantOozeRegen = 0f;
		purityRegen = 0f;
		fBarrier = false;
		aBrain = false;
		amalgam = false;
		frostFlare = false;
		evolution = false;
		nanotech = false;
		deadshotBrooch = false;
		tesla = false;
		teslaVisuals = true;
		cryogenSoul = false;
		ascendantInsignia = false;
		magmaStoneVisuals = true;
		eGauntlet = false;
		eGauntletVisuals = true;
		gloveLevel = 0;
		if (base.Player.dashDelay != -1)
		{
			statisNinjaBelt = false;
		}
		if (base.Player.dashDelay != -1)
		{
			statisVoidSash = false;
		}
		alreadyHasFrogLeg = false;
		eTalisman = false;
		nucleogenesis = false;
		nuclearFuelRod = false;
		heartOfDarkness = false;
		profanedSoulRelicBuff = false;
		shadowMinions = false;
		holyMinions = false;
		alchFlask = false;
		toxicHeart = false;
		toxicHeartVisuals = false;
		abaddon = false;
		aeroStone = false;
		lifejelly = false;
		GrandGelatin = false;
		cleansingjelly = false;
		spawnedJellyAura = false;
		community = false;
		shatteredCommunity = false;
		stressPills = false;
		laudanum = false;
		fleshTotem = false;
		bloodPact = false;
		bloodflareCore = false;
		chaliceOfTheBloodGod = false;
		chaliceHeartStyle = false;
		chaliceBleedoutToApplyOnHurt = 0;
		elementalHeart = false;
		crownJewel = false;
		infectedJewel = false;
		purity = false;
		harpyRing = false;
		angelTreads = false;
		fleshKnuckles = false;
		darkSunRing = false;
		crawCarapace = false;
		baroclaw = false;
		voidOfCalamity = false;
		voidOfExtinction = false;
		eArtifact = false;
		dArtifact = false;
		auricSArtifact = false;
		pSoulArtifact = false;
		giantPearl = false;
		normalityRelocator = false;
		flameLickedShell = false;
		sPauldron = false;
		XykVisualsBlue = false;
		XykVisualsOrange = false;
		manaOverloader = false;
		royalGel = false;
		handWarmer = false;
		raiderTalisman = false;
		gSabaton = false;
		sGlyph = false;
		sRegen = false;
		hallowedRune = false;
		phantomicArtifact = false;
		hallowedRegen = false;
		hallowedPower = false;
		tracersDust = false;
		moonWalkers = false;
		voidStriders = false;
		seraphTracers = false;
		ursaSergeant = false;
		ursaSergeantVisual = false;
		scuttlersJewel = false;
		thiefsDime = false;
		dynamoStemCells = false;
		etherealExtorter = false;
		blazingCore = false;
		voltaicJelly = false;
		jellyChargedBattery = false;
		starbusterCore = false;
		starTaintedGenerator = false;
		camper = false;
		corrosiveSpine = false;
		scionsCurio = false;
		rottenDogTooth = false;
		angelicAlliance = false;
		ChaosStone = false;
		CryoStone = false;
		CryoStoneVanity = false;
		voidField = false;
		copyrightInfringementShield = false;
		ConsumableDodgeCooldown = BalancingConstants.DodgeCooldownMax;
		DodgeEffects = new List<Func<Player, Player.HurtInfo, string>>();
		daedalusReflect = false;
		daedalusSplit = false;
		daedalusAbsorb = false;
		daedalusShard = false;
		brimflameSet = false;
		brimflameFrenzy = false;
		lunicCorpsSet = false;
		lunicCorpsLegs = false;
		ammoCost = 1f;
		healingPotionMultiplier = 1f;
		avertorBonus = false;
		reaverSpeed = false;
		reaverDefense = false;
		reaverExplore = false;
		ironBoots = false;
		depthCharm = false;
		anechoicPlating = false;
		jellyfishNecklace = false;
		fairyBoots = false;
		flameWakerBoots = false;
		hellfireTreads = false;
		bootLevel = 0;
		sSpiritAmulet = false;
		dOfTheDeep = false;
		oceanCrest = false;
		aquaticEmblem = false;
		if (!spiritOrigin)
		{
			spiritOriginCritBoost = 0;
		}
		spiritOrigin = false;
		spiritOriginVanity = false;
		critDamage = 0f;
		astralStarRain = false;
		desertProwler = false;
		snowRuffianSet = false;
		forbiddenCirclet = false;
		silverMedkit = false;
		tungstenArmorHookBoost = false;
		goldArmorGoldDrops = false;
		miningSet = false;
		eskimoSet = false;
		rainSet = false;
		meteorSet = false;
		necroSet = false;
		frostSet = false;
		victideSet = false;
		victideSummoner = false;
		sulphurSet = false;
		aeroSet = false;
		statigelSet = false;
		titanHeartSet = false;
		titanHeartMask = false;
		titanHeartMantle = false;
		umbraphileSet = false;
		plagueReaper = false;
		plaguebringerPatronSet = false;
		plaguebringerCarapace = false;
		fathomSwarmer = false;
		fathomSwarmerVisage = false;
		fathomSwarmerBreastplate = false;
		fathomSwarmerTail = false;
		prismaticSet = false;
		prismaticHelmet = false;
		prismaticRegalia = false;
		prismaticGreaves = false;
		tarraSet = false;
		tarraMelee = false;
		tarragonCloak = false;
		tarraMage = false;
		tarraRanged = false;
		tarraThrowing = false;
		tarragonImmunity = false;
		tarraSummon = false;
		bloodflareSet = false;
		bloodflareMelee = false;
		bloodflareFrenzy = false;
		bloodflareRanged = false;
		bloodflareThrowing = false;
		bloodflareMage = false;
		bloodflareSummon = false;
		xerocSet = false;
		weakPetrification = false;
		inkBomb = false;
		darkGodSheath = false;
		abyssalMirror = false;
		eclipseMirror = false;
		featherCrown = false;
		moonCrown = false;
		dragonScales = false;
		gloveOfPrecision = false;
		gloveOfRecklessness = false;
		vampiricTalisman = false;
		electricianGlove = false;
		bloodyGlove = false;
		filthyGlove = false;
		sandCloak = false;
		spectralVeil = false;
		hasJetpack = false;
		plaguedFuelPack = false;
		blunderBooster = false;
		blunderBoosterVisibility = true;
		veneratedLocket = false;
		alcoholPoisoning = false;
		shadowflame = false;
		daybroken = false;
		whisperingDeath = false;
		dragonFire = false;
		vermillionFlux = false;
		auricRebuke = false;
		staticDischarge = false;
		miracleBlight = false;
		armorCrunch = false;
		crumble = false;
		irradiated = false;
		brimstoneFlames = false;
		witheredDebuff = false;
		absorberAffliction = false;
		weakBrimstoneFlames = false;
		demonicFlames = false;
		godSlayerInferno = false;
		astralInfection = false;
		plague = false;
		holyFlames = false;
		holyInferno = false;
		burningBlood = false;
		brainRot = false;
		heavybleeding = false;
		laceration = false;
		elementalMix = false;
		icarusFolly = false;
		vHex = false;
		trueVHex = false;
		DoGExtremeGravity = false;
		warped = false;
		crushDepth = false;
		riptide = false;
		hadopelagicPressure = false;
		fishAlert = false;
		clamity = false;
		NOU = false;
		enraged = false;
		snowmanNoseless = false;
		sulphurPoison = false;
		nightwither = false;
		voidfrost = false;
		eutrophication = false;
		frozenLungs = false;
		searingLava = false;
		vaporfied = false;
		banishingFire = false;
		wither = false;
		ManaBurn = false;
		TypelessDebuffMultiplier = new StatModifier();
		HeatDebuffMultiplier = new StatModifier();
		ColdDebuffMultiplier = new StatModifier();
		SicknessDebuffMultiplier = new StatModifier();
		WaterDebuffMultiplier = new StatModifier();
		ElectricDebuffMultiplier = new StatModifier();
		sandsWindBuff = false;
		aeolianEarthBuff = false;
		corrEffigy = false;
		crimEffigy = false;
		decayEffigy = false;
		rRage = false;
		xWrath = false;
		graxDefense = false;
		encased = false;
		omniscience = false;
		zerg = false;
		zen = false;
		isNearbyBoss = false;
		permafrostsConcoction = false;
		flaskCrumbling = false;
		ceaselessHunger = false;
		calcium = false;
		soaring = false;
		bounding = false;
		shadow = false;
		photosynthesis = false;
		astralInjection = false;
		gravityNormalizer = false;
		flaskHoly = false;
		galvanicCorrosion = false;
		sulphurskin = false;
		baguette = false;
		trippy = false;
		amidiasBlessing = false;
		flaskBrimstone = false;
		purpleHaze = false;
		if (purpleHazeStealthTimer > 0)
		{
			purpleHazeStealthTimer--;
		}
		shine = false;
		anechoicCoating = false;
		mushy = false;
		PinkJellyRegen = false;
		GreenJellyRegen = false;
		AbsorberRegen = false;
		cFreeze = false;
		tRegen = false;
		bloodfinBoost = false;
		divineBless = false;
		vodka = false;
		redWine = false;
		grapeBeer = false;
		moonshine = false;
		rum = false;
		whiskey = false;
		fireball = false;
		everclear = false;
		bloodyMary = false;
		tequila = false;
		caribbeanRum = false;
		cinnamonRoll = false;
		tequilaSunrise = false;
		margarita = false;
		oldFashioned = false;
		starBeamRye = false;
		screwdriver = false;
		moscowMule = false;
		whiteWine = false;
		evergreenGin = false;
		tranquilityCandle = false;
		chaosCandle = false;
		blueCandle = false;
		pinkCandle = false;
		yellowCandle = false;
		if (!Main.gamePad)
		{
			LockOnHelper.ForceUsability = false;
		}
		SelectedFishingMinigame = FishingMinigames.None;
		wDroid = false;
		resButterfly = false;
		hasVoidEaterMarionette = false;
		IceClasperBool = false;
		magicHat = false;
		herring = false;
		blackhawk = false;
		cosmicViper = false;
		CalamarisLament = false;
		cEyes = false;
		cSlime = false;
		cSlime2 = false;
		aSlime = false;
		brittleStar = false;
		aquaticStar = false;
		sunSpirit = false;
		dCreeper = false;
		eAxe = false;
		endoCooper = false;
		apexShark = false;
		gastricBelcher = false;
		hauntedDishes = false;
		stormjaw = false;
		vengefulSunMinion = false;
		sirius = false;
		aChicken = false;
		cLamp = false;
		pGuy = false;
		cEnergy = false;
		pSoulGuardians = false;
		sandEleBuff = false;
		rareSandEleBuff = false;
		cloudEleBuff = false;
		brimEleBuff = false;
		waterEleBuff = false;
		fClump = false;
		rDevil = false;
		aValkyrie = false;
		sCrystal = false;
		sGod = false;
		sandnado = false;
		PlantationSummon = false;
		astralProbe = false;
		victideSnail = false;
		cSpirit = false;
		dCrystal = false;
		MutatedTruffleBool = false;
		sandElemental = false;
		sandElementalVanity = false;
		rareSandElemental = false;
		rareSandElementalVanity = false;
		cloudElemental = false;
		cloudElementalVanity = false;
		brimElemental = false;
		brimElementalVanity = false;
		waterElemental = false;
		waterElementalVanity = false;
		allElementals = false;
		allElementalsVanity = false;
		fungalClump = false;
		fungalClumpVanity = false;
		howlsHeart = false;
		howlsHeartVanity = false;
		redDevil = false;
		valkyrie = false;
		slimeGod = false;
		chaosSpirit = false;
		daedalusCrystal = false;
		shellfish = false;
		hCrab = false;
		endoHydra = false;
		powerfulRaven = false;
		dragonFamily = false;
		providenceStabber = false;
		seashineSwordBuff = false;
		plaguebringerMK2 = false;
		igneousExaltation = false;
		GlacialEmbrace = false;
		voidAura = false;
		voidAuraDamage = false;
		voidConcentrationAura = false;
		saros = false;
		if (sarosEclipseBeamUsage > 300)
		{
			sarosEclipseBeamUsage = 300;
		}
		if (sarosEclipseBeamUsage > 0 && base.Player.ownedProjectileCounts[ModContent.ProjectileType<SarosEclipseBeam>()] <= 0)
		{
			sarosEclipseBeamUsage--;
		}
		virili = false;
		frostBlossom = false;
		cinderBlossom = false;
		belladonaSpirit = false;
		puffWarrior = false;
		vileFeeder = false;
		scabRipper = false;
		midnightUFO = false;
		plagueEngine = false;
		brimseeker = false;
		necrosteocytesDudes = false;
		gammaHead = false;
		tundraFlameBlossom = false;
		snakeEyes = false;
		poleWarper = false;
		aqueousHunterDrone = false;
		causticDragon = false;
		plaguebringerPatronSummon = false;
		howlTrio = false;
		mountedScanner = false;
		sepulcher = false;
		daedalusGolem = false;
		deathstareEyeball = false;
		witherBlossom = false;
		flowersOfMortality = false;
		viridVanguard = false;
		sageSpirit = false;
		fleshBall = false;
		eyeOfNight = false;
		soulSeeker = false;
		perditionBeacon = false;
		MoonFist = false;
		AresCannons = false;
		celestialDragons = false;
		KalandraMirror = false;
		StellarTorus = false;
		LiliesOfFinalityBool = false;
		EnchantedKnifeStaffBool = false;
		AmphibiansGuitarBool = false;
		forceSummonTagMultiplicative = Main.zenithWorld;
		forceSummonTagCrit = Main.zenithWorld;
		bonusFlatTag = 0;
		bonusCritTag = 0f;
		bonusMultTag = 0f;
		abyssalDivingSuitPrevious = abyssalDivingSuit;
		abyssalDivingSuit = false;
		aquaticHeartPrevious = aquaticHeart;
		aquaticHeart = false;
		profanedCrystalStatePrevious = pscState;
		profanedCrystalPrevious = profanedCrystal;
		profanedCrystal = (profanedCrystalBuffs = false);
		pscState = 0;
		pscLerpColor = Color.White;
		meldTransformationPrevious = meldTransformation;
		meldTransformation = (meldTransformationForce = (meldTransformationPower = false));
		omegaBlueTransformationPrevious = omegaBlueTransformation;
		omegaBlueTransformation = (omegaBlueTransformationForce = (omegaBlueTransformationPower = false));
		rageModeActive = false;
		adrenalineModeActive = false;
		RageDuration = BalancingConstants.DefaultRageDuration;
		RageDamageBoost = BalancingConstants.DefaultRageDamageBoost;
		cursedSummonsEnchant = false;
		flamingItemEnchant = false;
		lifeManaEnchant = false;
		farProximityRewardEnchant = false;
		closeProximityRewardEnchant = false;
		dischargingItemEnchant = false;
		explosiveMinionsEnchant = false;
		bladeArmEnchant = false;
		manaMonsterEnchant = false;
		witheringWeaponEnchant = false;
		persecutedEnchant = false;
		lecherousOrbEnchant = false;
		flatStealthLossReduction = 0;
		AbleToSelectExoMech = false;
		infiniteFlight = false;
		noStupidNaturalARSpawns = false;
		disableAnahitaSpawns = false;
		disableHiveCystSpawns = false;
		disableNaturalScourgeSpawns = false;
		disablePerfCystSpawns = false;
		disableVoodooSpawns = false;
		abyssDarkness = 0f;
		abyssPlayerGlowMultiplier = 1f;
		abyssFlashlightWidthMultiplier = 1f;
		darknessIntensity = MathHelper.Max(darknessIntensity - 0.05f, 0f);
		EnchantHeldItemEffects(base.Player, base.Player.Calamity(), base.Player.HeldItem);
	}

	public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
	{
		health = StatModifier.Default;
		health.Base = sTangerine.ToInt() * 25 + mFruit.ToInt() * 25 + tCloudberry.ToInt() * 25 + sStrawberry.ToInt() * 25;
		mana = StatModifier.Default;
		mana.Base = cShard.ToInt() * 50 + eCore.ToInt() * 50 + pHeart.ToInt() * 50;
	}

	public override void ModifyScreenPosition()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		bool allowScreenshake = CalamityClientConfig.Instance.ScreenshakePower > 0f && !CalamityClientConfig.Instance.Photosensitivity;
		if ((GeneralScreenShakePower > 0f) & allowScreenshake)
		{
			Main.screenPosition += Main.rand.NextVector2Circular(GeneralScreenShakePower * CalamityClientConfig.Instance.ScreenshakePower, GeneralScreenShakePower * CalamityClientConfig.Instance.ScreenshakePower);
		}
		GeneralScreenShakePower = MathHelper.Clamp(GeneralScreenShakePower - 0.185f, 0f, 20f * CalamityClientConfig.Instance.ScreenshakePower);
	}

	public override void UpdateDead()
	{
		//IL_0c10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		if (cooldowns.Count > 0)
		{
			IList<string> removedCooldowns = new List<string>(16);
			Dictionary<string, CooldownInstance>.Enumerator cdIterator = cooldowns.GetEnumerator();
			while (cdIterator.MoveNext())
			{
				KeyValuePair<string, CooldownInstance> kv = cdIterator.Current;
				string id = kv.Key;
				if (!kv.Value.handler.PersistsThroughDeath)
				{
					removedCooldowns.Add(id);
				}
			}
			cdIterator.Dispose();
			if (removedCooldowns.Count > 0)
			{
				foreach (string cdID in removedCooldowns)
				{
					cooldowns.Remove(cdID);
				}
				SyncCooldownDictionary(Main.dedServ);
			}
		}
		calamityBonusLuck = 0f;
		totalDefenseDamage = 0;
		defenseDamageRecoveryFrames = 0;
		totalDefenseDamageRecoveryFrames = 60;
		defenseDamageDelayFrames = 0;
		nextHitDealsDefenseDamage = false;
		bloodflareCoreRemainingHealOverTime = 0;
		if (base.Player.HeldItem.IsAir || base.Player.HeldItem.fishingPole == 0)
		{
			consecutiveCaughtFish = 0;
		}
		heldGaelsLastFrame = false;
		gaelSwipes = 0;
		whitewaterHeal = 0;
		luxorHit = false;
		arsenalCooldown = 0;
		andromedaState = AndromedaPlayerState.Inactive;
		planarSpeedBoost = 0;
		galileoCooldown = 0;
		soundCooldown = 0;
		dogTextCooldown = 0;
		auralisStealthCounter = 0f;
		auralisAuroraCounter = 0;
		auralisAuroraCooldown = 0;
		auralisAurora = 0;
		necroReviveCounter = -1;
		hideOfDeusTimer = 0;
		bloomStoneHealPool = 0;
		bloomStoneTotalHeal = 0;
		bloomStoneHealTimer = 0f;
		bloomStoneHealRate = 0f;
		murasamaHitCooldown = 0;
		SulphWaterPoisoningLevel = 0f;
		holyInfernoFadeIntensity = 0f;
		spiritOriginCritBoost = 0;
		critDamage = 0f;
		rage = 0f;
		adrenaline = 0f;
		raiderCritLifespan = 0;
		raiderSoundCooldown = 0;
		gSabatonHotkeyFallWindup = -1;
		gSabatonFall = 0;
		gSabatonFalling = false;
		gSabatonTempJumpSpeed = 0;
		rOfDelivarenceRam = false;
		astralStarRainCooldown = 0;
		AbaddonCooldown = 0;
		VoidCooldown = 0;
		AlchFlaskCooldown = 0;
		ascendantInsigniaCooldown = 0;
		transformerCooldown = 0;
		transformerDelay = 0;
		transformerStoredKills = 0;
		silvaMageCooldown = 0;
		bloodflareMageCooldown = 0;
		tarraRangedCooldown = 0;
		hideOfDeusMeleeBoostTimer = 0;
		rOfResilienceCooldown = 0;
		rOfResilienceEffect = 0;
		demonSwordKillMode = false;
		externalBreathTickBoost = 0f;
		externalFlightTimeMultBoost = 0f;
		externalColdImmunity = (externalHeatImmunity = false);
		externalDefenseDamageImmunity = false;
		dragonRageHits = 0;
		dragonRageCooldown = 0;
		spectralVeilImmunity = 0;
		jetPackDash = 0;
		jetPackDirection = 0;
		andromedaCripple = 0;
		theBeeCooldown = 0;
		scuttlerCooldown = 0;
		rogueCrownCooldown = 0;
		wingProjectileCooldown = 0;
		hallowedRuneCooldown = 0;
		sulphurBubbleCooldown = 0;
		ladHearts = 0;
		prismaticLasers = 0;
		angelicActivate = -1;
		resetHeightandWidth = false;
		noLifeRegen = false;
		alcoholPoisoning = false;
		shadowflame = false;
		daybroken = false;
		whisperingDeath = false;
		dragonFire = false;
		vermillionFlux = false;
		auricRebuke = false;
		staticDischarge = false;
		miracleBlight = false;
		armorCrunch = false;
		crumble = false;
		irradiated = false;
		brimstoneFlames = false;
		witheredDebuff = false;
		absorberAffliction = false;
		weakBrimstoneFlames = false;
		demonicFlames = false;
		godSlayerInferno = false;
		astralInfection = false;
		plague = false;
		holyFlames = false;
		holyInferno = false;
		burningBlood = false;
		brainRot = false;
		heavybleeding = false;
		laceration = false;
		elementalMix = false;
		icarusFolly = false;
		vHex = false;
		trueVHex = false;
		DoGExtremeGravity = false;
		warped = false;
		crushDepth = false;
		riptide = false;
		hadopelagicPressure = false;
		fishAlert = false;
		clamity = false;
		NOU = false;
		snowmanNoseless = false;
		sulphurPoison = false;
		nightwither = false;
		voidfrost = false;
		eutrophication = false;
		frozenLungs = false;
		searingLava = false;
		vaporfied = false;
		banishingFire = false;
		wither = false;
		PurityHealSlowdownFrames = 0;
		ImmobilityDebuffImmunityTimer = 0;
		TypelessDebuffMultiplier = new StatModifier();
		HeatDebuffMultiplier = new StatModifier();
		ColdDebuffMultiplier = new StatModifier();
		SicknessDebuffMultiplier = new StatModifier();
		WaterDebuffMultiplier = new StatModifier();
		ElectricDebuffMultiplier = new StatModifier();
		rogueStealth = 0f;
		rogueStealthMax = 0f;
		stealthAcceleration = 1f;
		stealthDamage = 0f;
		bonusStealthDamage = 0.0;
		rogueVelocity = 1f;
		if (stealthUIAlpha > 0f)
		{
			stealthUIAlpha -= 0.035f;
			stealthUIAlpha = MathHelper.Clamp(stealthUIAlpha, 0f, 1f);
		}
		if (SulphWaterUIOpacity > 0f)
		{
			SulphWaterUIOpacity = MathHelper.Clamp(SulphWaterUIOpacity - 0.035f, 0f, 1f);
		}
		sRegen = false;
		hallowedRegen = false;
		hallowedPower = false;
		onyxExcavator = false;
		rimehound = false;
		crysthamyr = false;
		ExoChair = false;
		aquaticHeartWaterBuff = false;
		aquaticHeartIce = false;
		sandsWindBuff = false;
		aeolianEarthBuff = false;
		chiBuffTimer = 0;
		corrEffigy = false;
		crimEffigy = false;
		rRage = false;
		xWrath = false;
		graxDefense = false;
		encased = false;
		omniscience = false;
		zerg = false;
		zen = false;
		isNearbyBoss = false;
		permafrostsConcoction = false;
		flaskCrumbling = false;
		ceaselessHunger = false;
		calcium = false;
		soaring = false;
		bounding = false;
		shadow = false;
		adrenalinePauseTimer = 0;
		photosynthesis = false;
		astralInjection = false;
		gravityNormalizer = false;
		flaskHoly = false;
		galvanicCorrosion = false;
		sulphurskin = false;
		baguette = false;
		flaskBrimstone = false;
		purpleHaze = false;
		shine = false;
		anechoicCoating = false;
		mushy = false;
		PinkJellyRegen = false;
		GreenJellyRegen = false;
		AbsorberRegen = false;
		enraged = false;
		cFreeze = false;
		tRegen = false;
		rageModeActive = false;
		adrenalineModeActive = false;
		vodka = false;
		redWine = false;
		grapeBeer = false;
		moonshine = false;
		rum = false;
		whiskey = false;
		fireball = false;
		everclear = false;
		bloodyMary = false;
		tequila = false;
		caribbeanRum = false;
		cinnamonRoll = false;
		tequilaSunrise = false;
		margarita = false;
		oldFashioned = false;
		starBeamRye = false;
		screwdriver = false;
		moscowMule = false;
		whiteWine = false;
		evergreenGin = false;
		tranquilityCandle = false;
		chaosCandle = false;
		blueCandle = false;
		pinkCandle = false;
		pinkCandleHealFraction = 0.0;
		yellowCandle = false;
		trippy = false;
		amidiasBlessing = false;
		bloodfinBoost = false;
		bloodfinTimer = 0;
		healCounter = 300;
		danceOfLightCharge = 0;
		ammoCost = 1f;
		healingPotionMultiplier = 1f;
		avertorBonus = false;
		divineBless = false;
		hasteLevel = 0;
		hasteCounter = 0;
		silverMedkit = false;
		silverMedkitTimer = 0;
		tungstenArmorHookBoost = false;
		goldArmorGoldDrops = false;
		miningSet = false;
		miningSetCooldown = 0;
		shadowSpeed = false;
		godSlayer = false;
		godSlayerDamage = false;
		godSlayerRanged = false;
		godSlayerThrowing = false;
		godSlayerDashHotKeyPressed = false;
		SpeedBlasterDashStarted = false;
		auricSetMelee = false;
		silvaSet = false;
		silvaMage = false;
		silvaSummon = false;
		hasSilvaEffect = false;
		silvaCountdown = SilvaArmor.ReviveDuration;
		auricSet = false;
		GemTechSet = false;
		CobaltSet = false;
		MythrilSet = false;
		MythrilFlareSpawnCountdown = 0;
		AdamantiteSet = false;
		WearingPostMLSummonerSet = false;
		AdamantiteSetDecayDelay = 0;
		ChlorophyteHealDelay = 0;
		omegaBlueChestplate = false;
		omegaBlueSet = false;
		molluskHelmet = false;
		molluskChest = false;
		molluskLegs = false;
		fearmongerSet = false;
		daedalusReflect = false;
		daedalusSplit = false;
		daedalusAbsorb = false;
		daedalusShard = false;
		brimflameSet = false;
		brimflameFrenzy = false;
		lunicCorpsSet = false;
		lunicCorpsLegs = false;
		reaverSpeed = false;
		reaverDefense = false;
		reaverExplore = false;
		shadeRegen = false;
		dsSetBonus = false;
		titanHeartSet = false;
		titanHeartMask = false;
		titanHeartMantle = false;
		titanCooldown = 0;
		umbraphileSet = false;
		fathomSwarmer = false;
		fathomSwarmerVisage = false;
		fathomSwarmerBreastplate = false;
		fathomSwarmerTail = false;
		prismaticSet = false;
		prismaticHelmet = false;
		prismaticRegalia = false;
		prismaticGreaves = false;
		astralStarRain = false;
		plagueReaper = false;
		plaguebringerPatronSet = false;
		plaguebringerCarapace = false;
		ataxiaMage = false;
		ataxiaBolt = false;
		ataxiaGeyser = false;
		ataxiaVolley = false;
		ataxiaBlaze = false;
		hydrothermalSmoke = false;
		desertProwler = false;
		snowRuffianSet = false;
		forbiddenCirclet = false;
		forbiddenCooldown = 0;
		tornadoCooldown = 0;
		eskimoSet = false;
		rainSet = false;
		meteorSet = false;
		necroSet = false;
		frostSet = false;
		victideSet = false;
		aeroSet = false;
		sulphurSet = false;
		statigelSet = false;
		tarraSet = false;
		tarraMelee = false;
		tarragonCloak = false;
		tarraDefenseTime = 600;
		tarraMage = false;
		tarraRanged = false;
		tarraThrowing = false;
		tarragonImmunity = false;
		tarraThrowingCrits = 0;
		tarraSummon = false;
		bloodflareSet = false;
		bloodflareMelee = false;
		bloodflareFrenzy = false;
		bloodflareMeleeHits = 0;
		bloodflareRanged = false;
		bloodflareThrowing = false;
		bloodflareMage = false;
		bloodflareSummon = false;
		bloodflareSummonTimer = 0;
		fearmongerSet = false;
		fearmongerRegenFrames = 0;
		xerocSet = false;
		tracersDust = false;
		GemTechState.OnDeathEffects();
		blazingCoreParry = 0;
		blazingCoreEmpoweredParry = false;
		blazingCoreSuccessfulParry = 0;
		flameLickedShellParry = 0;
		flameLickedShellEmpoweredParry = false;
		profanedCrystalAnim = -1;
		RoverDriveShieldDurability = 0;
		LunicCorpsShieldDurability = 0;
		SpongeShieldDurability = 0;
		pSoulShieldDurability = 0;
		CurrentlyViewedFactoryID = -1;
		CurrentlyViewedChargerID = -1;
		CurrentlyViewedHologramID = -1;
		CurrentlyViewedCanvasID = -1;
		CurrentlyViewedHologramText = string.Empty;
		evilSmasherBoost = 0;
		burningSeaBurnOut = 0;
		flareGunOverheat = 0;
		hellbornShots = 0;
		darklightEnergy = 0;
		elementalMastery = 0;
		garandShots = 0;
		persecutedEnchantSummonTimer = 0;
		momentumCapacitorTime = 0;
		momentumCapacitorBoost = 0f;
		LungingDown = false;
		chaliceBleedoutBuffer = 0.0;
		chaliceDamagePointPartialProgress = 0.0;
		chaliceHitOriginalDamage = 0;
		if (BossRushEvent.BossRushActive)
		{
			IEntitySource source = Terraria.Entity.GetSource_None();
			if (base.Player.whoAmI == 0 && !CalamityGlobalNPC.AnyLivingPlayers() && CalamityUtils.CountProjectiles(ModContent.ProjectileType<BossRushFailureEffectThing>()) == 0)
			{
				Projectile.NewProjectile(source, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<BossRushFailureEffectThing>(), 0, 0f);
			}
		}
		int respawnTimerSet = (areThereAnyDamnBosses ? (CalamityServerConfig.Instance.PlayerRespawnTime_BossAlive * 60) : 180);
		if (base.Player.respawnTimer > respawnTimerSet)
		{
			base.Player.respawnTimer = respawnTimerSet;
		}
	}

	public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
	{
		if (!mediumCoreDeath)
		{
			yield return createItem(ModContent.ItemType<StarterBag>());
		}
		static Item createItem(int type)
		{
			Item item = new Item();
			item.SetDefaults(type);
			return item;
		}
	}

	public Item FindAccessory(int itemID)
	{
		for (int i = 0; i < 10; i++)
		{
			if (base.Player.armor[i].type == itemID)
			{
				return base.Player.armor[i];
			}
		}
		return ContentSamples.ItemsByType[itemID];
	}

	public Item FindAccessory<T>() where T : ModItem
	{
		return FindAccessory(ModContent.ItemType<T>());
	}

	public override void ProcessTriggers(TriggersSet triggersSet)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0856: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_104a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1089: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_1384: Unknown result type (might be due to invalid IL or missing references)
		//IL_138f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_1736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_096d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_178c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1793: Unknown result type (might be due to invalid IL or missing references)
		//IL_1798: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1902: Unknown result type (might be due to invalid IL or missing references)
		//IL_1907: Unknown result type (might be due to invalid IL or missing references)
		//IL_1888: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1414: Unknown result type (might be due to invalid IL or missing references)
		//IL_141a: Unknown result type (might be due to invalid IL or missing references)
		//IL_142e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1438: Unknown result type (might be due to invalid IL or missing references)
		//IL_143d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1448: Unknown result type (might be due to invalid IL or missing references)
		//IL_1464: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_1482: Unknown result type (might be due to invalid IL or missing references)
		//IL_1487: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_149f: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_098e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1978: Unknown result type (might be due to invalid IL or missing references)
		//IL_197d: Unknown result type (might be due to invalid IL or missing references)
		//IL_197f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1984: Unknown result type (might be due to invalid IL or missing references)
		//IL_1988: Unknown result type (might be due to invalid IL or missing references)
		//IL_198d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1994: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1601: Unknown result type (might be due to invalid IL or missing references)
		//IL_1606: Unknown result type (might be due to invalid IL or missing references)
		//IL_1610: Unknown result type (might be due to invalid IL or missing references)
		//IL_1615: Unknown result type (might be due to invalid IL or missing references)
		//IL_161c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1621: Unknown result type (might be due to invalid IL or missing references)
		//IL_1626: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.dead)
		{
			return;
		}
		if (ascendantInsignia && Main.myPlayer == base.Player.whoAmI && CalamityKeybinds.AscendantInsigniaHotKey.JustPressed && ascendantInsigniaCooldown <= 0)
		{
			Projectile.NewProjectile(base.Player.GetSource_Accessory(FindAccessory<AscendantInsignia>()), base.Player.Center - Vector2.UnitY * 45f, Vector2.Zero, ModContent.ProjectileType<AscendantAura>(), 0, 0f);
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/AscendantActivate"));
			ascendantInsigniaCooldown = AscendantInsignia.AbilityCooldown;
			ascendantInsigniaBuffTime = AscendantInsignia.AbilityDuration;
		}
		int numOfBlobs = base.Player.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()];
		if (transformer && numOfBlobs > 0 && Main.myPlayer == base.Player.whoAmI && CalamityKeybinds.TransformerHotKey.JustPressed && transformerCooldown <= 0)
		{
			CalamityUtils.AddCooldown(duration: transformerCooldown = 300, p: base.Player, id: TransformerCooldown.ID);
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile = Main.projectile[x];
				if (projectile.active && projectile.type == ModContent.ProjectileType<TransformerBlob>())
				{
					projectile.localAI[0] = 5f;
				}
			}
			if (transformerVisual)
			{
				SoundStyle activate = new SoundStyle("CalamityMod/Sounds/Item/NullShot");
				for (int i = 0; i < 3; i++)
				{
					SoundEngine.PlaySound(activate with
					{
						Volume = 0.3f,
						Pitch = 0.2f + (float)i * 0.3f,
						MaxInstances = -1
					}, base.Player.Center);
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, Vector2.Zero, Color.DodgerBlue, "CalamityMod/Particles/BloomRingThinLarge", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.2f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		if (CalamityKeybinds.GravistarSabatonHotkey.JustPressed && gSabatonHotkeyFallWindup < 0)
		{
			gSabatonHotkeyFallWindup = 0;
		}
		if (gSabaton && gSabatonHotkeyFallWindup >= 0 && Main.myPlayer == base.Player.whoAmI && base.Player.velocity.Y != 0f && !base.Player.pulley && !base.Player.mount.Active && base.Player.grappling[0] == -1 && !base.Player.tongued)
		{
			gSabatonHotkeyFallWindup++;
			if (gSabatonHotkeyFallWindup < 20 && (float)gSabatonHotkeyFallWindup % 2f == 0f)
			{
				SpawnGravistarParticle();
			}
		}
		else if (Main.myPlayer == base.Player.whoAmI)
		{
			gSabatonHotkeyFallWindup = -1;
		}
		if (CalamityKeybinds.NormalityRelocatorHotKey.JustPressed && normalityRelocator && Main.myPlayer == base.Player.whoAmI && !base.Player.CCed && !base.Player.chaosState)
		{
			Vector2 teleportLocation = default(Vector2);
			teleportLocation.X = (float)Main.mouseX + Main.screenPosition.X;
			if (base.Player.gravDir == 1f)
			{
				teleportLocation.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)base.Player.height;
			}
			else
			{
				teleportLocation.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
			}
			teleportLocation.X -= base.Player.width / 2;
			if (teleportLocation.X > 50f && teleportLocation.X < (float)(Main.maxTilesX * 16 - 50) && teleportLocation.Y > 50f && teleportLocation.Y < (float)(Main.maxTilesY * 16 - 50) && !Collision.SolidCollision(teleportLocation, base.Player.width, base.Player.height))
			{
				base.Player.Teleport(teleportLocation, 4);
				NetMessage.SendData(65, -1, -1, null, 0, base.Player.whoAmI, teleportLocation.X, teleportLocation.Y, 1);
				SoundEngine.PlaySound(in NormalityRelocator.TeleportSound, base.Player.Center);
				int duration = (areThereAnyDamnBosses ? chaosStateDuration_NR : 360);
				base.Player.AddBuff(88, duration);
				base.Player.AddCooldown(ChaosState.ID, duration, true, "normalityrelocator");
			}
		}
		if (CalamityKeybinds.AngelicAllianceHotKey.JustPressed && angelicAlliance && Main.myPlayer == base.Player.whoAmI && !divineBless && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.DivineBless.ID))
		{
			base.Player.AddBuff(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.DivineBless>(), AngelicAlliance.DivineBlessDuration, quiet: false);
			SoundEngine.PlaySound(in AngelicAlliance.ActivationSound, base.Player.Center);
			float angelAmt = 0f;
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (!(proj.minionSlots <= 0f) && proj.CountsAsClass<SummonDamageClass>() && proj.active && proj.owner == base.Player.whoAmI)
				{
					angelAmt++;
				}
			}
			IEntitySource source = base.Player.GetSource_Accessory(FindAccessory<AngelicAlliance>());
			for (int j = 0; (float)j < angelAmt; j++)
			{
				Projectile proj2 = Main.projectile[j];
				float start = 360f / angelAmt;
				Projectile.NewProjectile(source, new Vector2((float)(int)((double)base.Player.Center.X + Math.Sin((float)j * start) * 300.0), (float)(int)((double)base.Player.Center.Y + Math.Cos((float)j * start) * 300.0)), Vector2.Zero, ModContent.ProjectileType<AngelicAllianceArchangel>(), proj2.damage / 10, proj2.knockBack / 10f, base.Player.whoAmI, Main.rand.Next(180), (float)j * start);
				base.Player.HealPlayer(AngelicAlliance.HealPerAngelSpawned);
			}
		}
		if (CalamityKeybinds.SpectralVeilHotKey.JustPressed && spectralVeil && Main.myPlayer == base.Player.whoAmI && rogueStealth >= rogueStealthMax * 0.25f && wearingRogueArmor && rogueStealthMax > 0f && !base.Player.chaosState)
		{
			Vector2 teleportLocation2 = default(Vector2);
			teleportLocation2.X = (float)Main.mouseX + Main.screenPosition.X;
			if (base.Player.gravDir == 1f)
			{
				teleportLocation2.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)base.Player.height;
			}
			else
			{
				teleportLocation2.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
			}
			teleportLocation2.X -= (float)base.Player.width * 0.5f;
			Vector2 teleportOffset = teleportLocation2 - base.Player.position;
			if (((Vector2)(ref teleportOffset)).Length() > 845f)
			{
				teleportOffset = teleportOffset.SafeNormalize(Vector2.Zero) * 845f;
				teleportLocation2 = base.Player.position + teleportOffset;
			}
			if (teleportLocation2.X > 50f && teleportLocation2.X < (float)(Main.maxTilesX * 16 - 50) && teleportLocation2.Y > 50f && teleportLocation2.Y < (float)(Main.maxTilesY * 16 - 50) && !Collision.SolidCollision(teleportLocation2, base.Player.width, base.Player.height))
			{
				rogueStealth -= rogueStealthMax * 0.25f;
				base.Player.Teleport(teleportLocation2, 1);
				NetMessage.SendData(65, -1, -1, null, 0, base.Player.whoAmI, teleportLocation2.X, teleportLocation2.Y, 1);
				int duration2 = (areThereAnyDamnBosses ? chaosStateDuration : 360);
				base.Player.AddBuff(88, duration2);
				base.Player.AddCooldown(ChaosState.ID, duration2, true, "spectralveil");
				int numDust = 40;
				Vector2 step = teleportOffset / (float)numDust;
				for (int k = 0; k < numDust; k++)
				{
					Dust dust = Dust.NewDustDirect(base.Player.Center - step * (float)k, 1, 1, 21, step.X, step.Y);
					dust.noGravity = true;
					dust.noLight = true;
				}
				spectralVeilImmunity = SpectralVeil.VeilIFrames;
			}
		}
		if (CalamityKeybinds.BoosterDashHotKey.JustPressed && hasJetpack && Main.myPlayer == base.Player.whoAmI && rogueStealth >= rogueStealthMax * 0.25f && wearingRogueArmor && rogueStealthMax > 0f && !base.Player.HasCooldown(RogueBooster.ID) && !base.Player.mount.Active)
		{
			jetPackDash = (blunderBooster ? 15 : 10);
			jetPackDirection = base.Player.direction;
			base.Player.AddCooldown(RogueBooster.ID, 60, true, blunderBooster ? "birb" : "default");
			rogueStealth -= rogueStealthMax * 0.25f;
			SoundEngine.PlaySound(in SoundID.Item66, base.Player.Center);
			SoundEngine.PlaySound(in SoundID.Item34, base.Player.Center);
		}
		if (CalamityKeybinds.AmmoCycleHotkey.JustPressed && deadshotBrooch)
		{
			SoundEngine.PlaySound(in SoundID.Item149, base.Player.Center);
			int ammoType = base.Player.HeldItem.useAmmo;
			int lastSlot = 57;
			while (lastSlot >= 54 && (base.Player.inventory[lastSlot].IsAir || base.Player.inventory[lastSlot].ammo != ammoType))
			{
				lastSlot--;
			}
			int firstSlot;
			for (firstSlot = 54; firstSlot <= 57 && (base.Player.inventory[firstSlot].IsAir || base.Player.inventory[firstSlot].ammo != ammoType); firstSlot++)
			{
			}
			if (firstSlot != lastSlot)
			{
				int tempType = base.Player.inventory[lastSlot].type;
				int tempStack = base.Player.inventory[lastSlot].stack;
				List<bool> favorited = new List<bool>();
				for (int z = 54; z <= 57; z++)
				{
					favorited.Add(!base.Player.inventory[z].IsAir && base.Player.inventory[z].favorited);
				}
				for (int i2 = lastSlot; i2 >= 55; i2--)
				{
					if (!base.Player.inventory[i2].IsAir && base.Player.inventory[i2].ammo == ammoType)
					{
						if (i2 == firstSlot)
						{
							base.Player.inventory[i2].SetDefaults(tempType);
							base.Player.inventory[i2].stack = tempStack;
							base.Player.inventory[i2].favorited = favorited[lastSlot - 54];
						}
						else
						{
							int nextSlot = i2 - 1;
							while (nextSlot >= 54 && (base.Player.inventory[nextSlot].IsAir || base.Player.inventory[nextSlot].ammo != ammoType))
							{
								nextSlot--;
							}
							base.Player.inventory[i2].SetDefaults(base.Player.inventory[nextSlot].type);
							base.Player.inventory[i2].stack = base.Player.inventory[nextSlot].stack;
							base.Player.inventory[i2].favorited = favorited[nextSlot - 54];
						}
					}
				}
				if (firstSlot == 54)
				{
					base.Player.inventory[54].SetDefaults(tempType);
					base.Player.inventory[54].stack = tempStack;
					base.Player.inventory[54].favorited = favorited[lastSlot - 54];
				}
				int visualType = base.Player.inventory[firstSlot].type;
				Texture2D ammoTex = TextureAssets.Item[visualType].Value;
				int frameAmt = ((Main.itemAnimations[visualType] == null) ? 1 : (ammoTex.Height / Main.itemAnimations[visualType].GetFrame(ammoTex).Height));
				GeneralParticleHandler.SpawnParticle(new CustomSprite(base.Player.Center - Vector2.UnitY * 20f, -Vector2.UnitY * 7f, 30, ammoTex, 1f, Color.White, 0f, AddativeBlend: false, needed: false, frameAmt));
			}
		}
		if (CalamityKeybinds.ArmorSetBonusHotKey.JustPressed)
		{
			PlayerLoader.ArmorSetBonusActivated(base.Player);
			if (base.Player.setVortex && !base.Player.mount.Active)
			{
				base.Player.vortexStealthActive = !base.Player.vortexStealthActive;
			}
			if (base.Player.setForbidden)
			{
				base.Player.MinionRestTargetAim();
				if (!base.Player.setForbiddenCooldownLocked)
				{
					base.Player.CommandForbiddenStorm();
				}
			}
		}
		if (CalamityKeybinds.ArmorSetBonusHotKey.Current)
		{
			ArmorSetBonusKeyHeldTimer++;
			PlayerLoader.ArmorSetBonusHeld(base.Player, ArmorSetBonusKeyHeldTimer);
		}
		else
		{
			ArmorSetBonusKeyHeldTimer = 0;
		}
		if (CalamityKeybinds.AccessoryParryHotKey.JustPressed)
		{
			if (blazingCore && blazingCoreParry == 0 && blazingCoreSuccessfulParry == 0)
			{
				if (!base.Player.HasCooldown(ParryCooldown.ID) || base.Player.ownedProjectileCounts[ModContent.ProjectileType<BlazingStarHeal>()] == 0)
				{
					base.Player.SetScreenshake(3.5f);
					blazingCoreParry = 30;
					SoundEngine.PlaySound(in BlazingCore.ParryActivateSound, base.Player.Center);
					IEntitySource source_FromThis = base.Player.GetSource_FromThis();
					int blazingSun = Projectile.NewProjectile(source_FromThis, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<BlazingSun>(), 0, 0f, base.Player.whoAmI);
					Main.projectile[blazingSun].Center = base.Player.Center;
					int blazingSun2 = Projectile.NewProjectile(source_FromThis, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<BlazingSun2>(), 0, 0f, base.Player.whoAmI);
					Main.projectile[blazingSun2].Center = base.Player.Center;
				}
			}
			else if (flameLickedShell && flameLickedShellParry == 0 && (!base.Player.HasCooldown(ParryCooldown.ID) || base.Player.ownedProjectileCounts[ModContent.ProjectileType<FlameLickedBarrage>()] == 0))
			{
				base.Player.SetScreenshake(2.5f);
				SoundEngine.PlaySound(in ProfanedGuardianDefender.RockShieldSpawnSound, base.Player.Center);
				flameLickedShellParry = 30;
			}
		}
		if (CalamityKeybinds.GodSlayerDashHotKey.JustPressed && godSlayer && !base.Player.pulley && base.Player.grappling[0] == -1 && !base.Player.tongued && !base.Player.mount.Active && !base.Player.HasCooldown(GodSlayerDash.ID) && base.Player.dashDelay == 0)
		{
			godSlayerDashHotKeyPressed = true;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<RelicOfDeliveranceSpear>() && base.Player.whoAmI == p.owner)
				{
					(p.ModProjectile as RelicOfDeliveranceSpear).KillProj();
					return;
				}
			}
		}
		if (sBlasterDashActivated)
		{
			if ((base.Player.controlUp || base.Player.controlDown || base.Player.controlLeft || base.Player.controlRight) && !base.Player.pulley && base.Player.grappling[0] == -1 && !base.Player.tongued && !base.Player.mount.Active && (base.Player.HasCooldown(SpeedBlasterBoost.ID) || base.Player.HasCooldown(SuperradiantSawBoost.ID)) && base.Player.dashDelay == 0)
			{
				SpeedBlasterDashStarted = true;
			}
			sBlasterDashActivated = false;
		}
		if (base.Player.Calamity().SpeedBlasterDashStarted || (base.Player.dashDelay != 0 && (base.Player.Calamity().LastUsedDashID == SuperradiantSawDash.ID || base.Player.Calamity().LastUsedDashID == SpeedBlasterDash.ID)))
		{
			base.Player.Calamity().DeferredDashID = ((base.Player.HeldItem.type == ModContent.ItemType<SuperradiantSlaughterer>()) ? SuperradiantSawDash.ID : SpeedBlasterDash.ID);
			base.Player.dash = 0;
		}
		if (CalamityKeybinds.RageHotKey.JustPressed)
		{
			if (!base.Player.HasCooldown(GaelsRage.ID) && base.Player.HeldItem.type == ModContent.ItemType<GaelsGreatsword>() && rage > 0f)
			{
				SoundEngine.PlaySound(in SilvaArmor.DispelSound, base.Player.Center);
				for (int l = 0; l < 3; l++)
				{
					Dust.NewDust(base.Player.position, 120, 120, 218, 0f, 0f, 100, default(Color), 1.5f);
				}
				for (int m = 0; m < 30; m++)
				{
					float angle = (float)Math.PI * 2f * (float)m / 30f;
					Dust dust2 = Dust.NewDustDirect(base.Player.position, 120, 120, 218, 0f, 0f, 0, default(Color), 2f);
					dust2.noGravity = true;
					dust2.velocity *= 4f;
					Dust dust3 = Dust.NewDustDirect(base.Player.position, 120, 120, 218, 0f, 0f, 100);
					dust3.velocity *= 2.25f;
					dust3.noGravity = true;
					Dust.NewDust(base.Player.Center + angle.ToRotationVector2() * 160f, 0, 0, 218, 0f, 0f, 100);
				}
				IEntitySource source2 = base.Player.GetSource_ItemUse(base.Player.HeldItem, GaelsGreatsword.SkullsplosionEntitySourceContext);
				float baseDamage = rage / rageMax * GaelsGreatsword.SkullsplosionDamageMultiplier * (float)GaelsGreatsword.BaseDamage;
				int damage = (int)base.Player.GetTotalDamage<MeleeDamageClass>().ApplyTo(baseDamage);
				float skullCount = 14f + (rageBoostOne ? 4f : 0f) + (rageBoostTwo ? 4f : 0f) + (rageBoostThree ? 4f : 0f);
				float skullSpeed = 12f;
				for (float i3 = 0f; i3 < skullCount; i3++)
				{
					Vector2 initialVelocity = ((float)Math.PI * 2f * i3 / skullCount).ToRotationVector2().RotatedByRandom(MathHelper.ToRadians(12f)) * skullSpeed * new Vector2(0.82f, 1.5f) * Main.rand.NextFloat(0.8f, 1.2f) * ((i3 < skullCount / 2f) ? 0.25f : 1f);
					int projectileIndex = Projectile.NewProjectile(source2, base.Player.Center + initialVelocity * 3f, initialVelocity, ModContent.ProjectileType<GaelSkull2>(), damage, 2f, base.Player.whoAmI);
					Main.projectile[projectileIndex].tileCollide = false;
					Main.projectile[projectileIndex].localAI[1] = (Main.projectile[projectileIndex].velocity.Y < 0f).ToInt();
					if (projectileIndex.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[projectileIndex].DamageType = DamageClass.Generic;
					}
				}
				rage = 0f;
				base.Player.AddCooldown(GaelsRage.ID, 1800);
			}
			if (rage >= rageMax && !rageModeActive)
			{
				base.Player.AddBuff(ModContent.BuffType<RageMode>(), 2);
				if (base.Player.whoAmI == Main.myPlayer)
				{
					SoundEngine.PlaySound(in RageActivationSound);
				}
				int rageDustID = 235;
				int dustCount = 132;
				float minSpeed = 4f;
				float maxSpeed = 11f;
				for (int n = 0; n < dustCount; n++)
				{
					float speed = (float)Math.Sqrt(Main.rand.NextFloat(minSpeed * minSpeed, maxSpeed * maxSpeed));
					Vector2 dustVel = Main.rand.NextVector2Unit() * speed;
					Dust dust4 = Dust.NewDustPerfect(base.Player.Center, rageDustID, dustVel);
					dust4.noGravity = !Main.rand.NextBool(4);
					dust4.noLight = false;
					dust4.scale = Main.rand.NextFloat(0.9f, 2.1f);
				}
			}
		}
		if (!CalamityKeybinds.AdrenalineHotKey.JustPressed || !AdrenalineEnabled || adrenaline != adrenalineMax || adrenalineModeActive)
		{
			return;
		}
		base.Player.AddBuff(ModContent.BuffType<AdrenalineMode>(), AdrenalineDuration);
		SoundStyle ActivationSound = (draedonsHeart ? NanomachinesActivationSound : AdrenalineActivationSound);
		if (base.Player.whoAmI == Main.myPlayer)
		{
			SoundEngine.PlaySound(in ActivationSound);
		}
		int dustPerSegment = 96;
		Vector2 segmentOneStart = default(Vector2);
		((Vector2)(ref segmentOneStart))._002Ector(0f, -120f);
		Vector2 val = new Vector2(-48f, 24f);
		Vector2 segmentOneIncrement = (val - segmentOneStart) / (float)dustPerSegment;
		Vector2 segmentTwoStart = val;
		Vector2 val2 = new Vector2(48f, -24f);
		Vector2 segmentTwoIncrement = (val2 - segmentTwoStart) / (float)dustPerSegment;
		Vector2 segmentThreeStart = val2;
		Vector2 segmentThreeIncrement = (new Vector2(0f, 120f) - segmentThreeStart) / (float)dustPerSegment;
		float maxDustVelSpread = 1.2f;
		for (int num = 0; num < dustPerSegment; num++)
		{
			bool num2 = Main.rand.NextBool(4);
			int dustID = ((!num2) ? ModContent.DustType<AdrenDust>() : (Main.rand.NextBool() ? 132 : 131));
			float interpolant = (float)num + 0.5f;
			float spreadSpeed = Main.rand.NextFloat(0.5f, maxDustVelSpread);
			if (num2)
			{
				spreadSpeed *= 4f;
			}
			Dust d = Dust.NewDustPerfect(base.Player.Center + segmentOneStart + segmentOneIncrement * interpolant, dustID, Vector2.Zero);
			if (num2)
			{
				d.noGravity = false;
			}
			d.scale = Main.rand.NextFloat(1.2f, 1.8f);
			d.velocity = Main.rand.NextVector2Unit() * spreadSpeed;
			Vector2 segmentTwoPos = base.Player.Center + segmentTwoStart + segmentTwoIncrement * interpolant;
			d = DustExtensions.BetterCloneDust(d);
			d.position = segmentTwoPos;
			d.scale = Main.rand.NextFloat(1.2f, 1.8f);
			d.velocity = Main.rand.NextVector2Unit() * spreadSpeed;
			Vector2 segmentThreePos = base.Player.Center + segmentThreeStart + segmentThreeIncrement * interpolant;
			d = DustExtensions.BetterCloneDust(d);
			d.position = segmentThreePos;
			d.scale = Main.rand.NextFloat(1.2f, 1.8f);
			d.velocity = Main.rand.NextVector2Unit() * spreadSpeed;
		}
	}

	public override void ArmorSetBonusActivated()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb5: Unknown result type (might be due to invalid IL or missing references)
		if (brimflameSet && !base.Player.HasCooldown(BrimflameFrenzy.ID) && base.Player.whoAmI == Main.myPlayer)
		{
			if (brimflameFrenzy)
			{
				base.Player.ClearBuff(ModContent.BuffType<BrimflameFrenzyBuff>());
				base.Player.AddCooldown(BrimflameFrenzy.ID, BrimflameCowl.FrenzyCooldown);
			}
			else
			{
				base.Player.AddBuff(ModContent.BuffType<BrimflameFrenzyBuff>(), BrimflameCowl.FrenzyDuration);
				SoundEngine.PlaySound(in BrimflameCowl.ActivationSound, base.Player.Center);
				for (int i = 0; i < 36; i++)
				{
					Dust dust = Dust.NewDustDirect(new Vector2(base.Player.position.X, base.Player.position.Y + 16f), base.Player.width, base.Player.height - 16, 235);
					dust.velocity *= 3f;
					dust.scale *= 1.15f;
				}
				int dustAmt = 36;
				for (int j = 0; j < dustAmt; j++)
				{
					Vector2 val = (Vector2.Normalize(base.Player.velocity) * new Vector2((float)base.Player.width / 2f, (float)base.Player.height) * 0.75f).RotatedBy((float)(j - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Player.Center;
					Vector2 dustVelocity = val - base.Player.Center;
					Dust dust2 = Dust.NewDustDirect(val + dustVelocity, 0, 0, 235, dustVelocity.X * 1.5f, dustVelocity.Y * 1.5f, 100, default(Color), 1.4f);
					dust2.noGravity = true;
					dust2.noLight = true;
					dust2.velocity = dustVelocity;
				}
			}
		}
		if (tarraMelee && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.TarragonCloak.ID) && !tarragonCloak && base.Player.whoAmI == Main.myPlayer)
		{
			base.Player.AddBuff(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.TarragonCloak>(), TarragonHeadMelee.CloakDuration, quiet: false);
		}
		if (bloodflareRanged && !base.Player.HasCooldown(BloodflareRangedSet.ID))
		{
			if (base.Player.whoAmI == Main.myPlayer)
			{
				base.Player.AddCooldown(BloodflareRangedSet.ID, BloodflareHeadRanged.SoulCooldown);
			}
			SoundEngine.PlaySound(in BloodflareHeadRanged.ActivationSound, base.Player.Center);
			for (int d = 0; d < 64; d++)
			{
				Dust dust3 = Dust.NewDustDirect(new Vector2(base.Player.position.X, base.Player.position.Y + 16f), base.Player.width, base.Player.height - 16, 60);
				dust3.velocity *= 3f;
				dust3.scale *= 1.15f;
			}
			int dustAmt2 = 36;
			for (int k = 0; k < dustAmt2; k++)
			{
				Vector2 val2 = (Vector2.Normalize(base.Player.velocity) * new Vector2((float)base.Player.width / 2f, (float)base.Player.height) * 0.75f).RotatedBy((float)(k - (dustAmt2 / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt2) + base.Player.Center;
				Vector2 dustVel = val2 - base.Player.Center;
				Dust dust4 = Dust.NewDustDirect(val2 + dustVel, 0, 0, 60, dustVel.X * 1.5f, dustVel.Y * 1.5f, 100, default(Color), 1.4f);
				dust4.noGravity = true;
				dust4.noLight = true;
				dust4.velocity = dustVel;
			}
			if (base.Player.whoAmI == Main.myPlayer)
			{
				IEntitySource source = base.Player.GetSource_Misc("1");
				int damage = (int)base.Player.GetTotalDamage<RangedDamageClass>().ApplyTo(BloodflareHeadRanged.SoulDamage);
				for (int l = 0; l < BloodflareHeadRanged.SoulAmount; l++)
				{
					float ai1 = Main.rand.NextFloat() + 0.5f;
					Vector2 circleVel = ((float)Math.PI * 2f * (float)l / 16f).ToRotationVector2() * Main.rand.NextFloat(5f, 8f);
					int soul = Projectile.NewProjectile(source, base.Player.Center, circleVel, ModContent.ProjectileType<BloodflareSoul>(), damage, 0f, base.Player.whoAmI, 0f, ai1);
					if (soul.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[soul].DamageType = DamageClass.Generic;
					}
				}
			}
		}
		if (omegaBlueSet && !base.Player.HasCooldown(OmegaBlue.ID))
		{
			if (base.Player.whoAmI == Main.myPlayer)
			{
				base.Player.AddBuff(ModContent.BuffType<AbyssalMadness>(), OmegaBlueHelmet.MadnessDuration, quiet: false);
			}
			base.Player.AddCooldown(OmegaBlue.ID, OmegaBlueHelmet.MadnessDuration + OmegaBlueHelmet.MadnessCooldown);
			SoundEngine.PlaySound(in OmegaBlueHelmet.ActivationSound, base.Player.Center);
			for (int m = 0; m < 66; m++)
			{
				Dust dust5 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 20, 0f, 0f, 100, Color.Transparent, 2.6f);
				dust5.noGravity = true;
				dust5.noLight = true;
				dust5.fadeIn = 1f;
				dust5.velocity *= 6.6f;
			}
		}
		if (dsSetBonus)
		{
			SoundEngine.PlaySound(in DemonshadeHelm.ActivationSound, base.Player.Center);
			for (int n = 0; n < 36; n++)
			{
				Dust dust6 = Dust.NewDustDirect(new Vector2(base.Player.position.X, base.Player.position.Y + 16f), base.Player.width, base.Player.height - 16, 235);
				dust6.velocity *= 3f;
				dust6.scale *= 1.15f;
			}
			int dustAmt3 = 36;
			for (int num = 0; num < dustAmt3; num++)
			{
				Vector2 val3 = (Vector2.Normalize(base.Player.velocity) * new Vector2((float)base.Player.width / 2f, (float)base.Player.height) * 0.75f).RotatedBy((float)(num - (dustAmt3 / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt3) + base.Player.Center;
				Vector2 dustVelocity2 = val3 - base.Player.Center;
				Dust dust7 = Dust.NewDustDirect(val3 + dustVelocity2, 0, 0, 235, dustVelocity2.X * 1.5f, dustVelocity2.Y * 1.5f, 100, default(Color), 1.4f);
				dust7.noGravity = true;
				dust7.noLight = true;
				dust7.velocity = dustVelocity2;
			}
			if (base.Player.whoAmI == Main.myPlayer)
			{
				base.Player.AddBuff(ModContent.BuffType<Enraged>(), DemonshadeHelm.EnrageDuration, quiet: false);
			}
			if (Main.netMode != 1)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (!npc.friendly && !npc.dontTakeDamage && Vector2.Distance(base.Player.Center, npc.Center) <= 3000f)
					{
						npc.AddBuff(ModContent.BuffType<Enraged>(), DemonshadeHelm.EnrageDuration);
					}
				}
			}
		}
		if (plagueReaper && !base.Player.HasCooldown(PlagueBlackout.ID))
		{
			SoundEngine.PlaySound(in PlagueReaperMask.ActivationSound, base.Player.Center);
			base.Player.AddCooldown(PlagueBlackout.ID, PlagueReaperMask.BlackoutDuration + PlagueReaperMask.BlackoutCooldown);
		}
		if (forbiddenCirclet && base.Player.ownedProjectileCounts[ModContent.ProjectileType<CircletTornado>()] < 2)
		{
			forbiddenCooldown = ForbiddenCirclet.StormCooldown;
			int stormMana = (int)((float)ForbiddenCirclet.StormManaCost * base.Player.manaCost);
			if (base.Player.statMana < stormMana && base.Player.manaFlower)
			{
				base.Player.QuickMana();
			}
			if (base.Player.statMana >= stormMana && !base.Player.silence)
			{
				IEntitySource source2 = base.Player.GetSource_ItemUse(FindAccessory<ForbiddenCirclet>());
				base.Player.manaRegenDelay = (int)base.Player.maxRegenDelay;
				base.Player.statMana -= stormMana;
				int damage2 = (int)base.Player.GetTotalDamage<SummonDamageClass>().CombineWith(base.Player.GetDamage<RogueDamageClass>()).ApplyTo(ForbiddenCirclet.StormDamage);
				float kBack = base.Player.GetTotalKnockback<SummonDamageClass>().ApplyTo(ForbiddenCirclet.StormKB);
				if (base.Player.whoAmI == Main.myPlayer)
				{
					if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<CircletTornado>()] > 0)
					{
						ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							Projectile proj = enumerator2.Current;
							if (proj.owner == base.Player.whoAmI && proj.type == ModContent.ProjectileType<CircletTornado>())
							{
								proj.ai[0] = CircletTornado.Lifetime - CircletTornado.Fadetime + proj.ai[0] % 60f;
								proj.netUpdate = true;
							}
						}
					}
					Vector2 tornadoPos = base.Player.ClampedMouseWorld();
					Projectile.NewProjectile(source2, tornadoPos, Vector2.Zero, ModContent.ProjectileType<CircletTornado>(), damage2, kBack, base.Player.whoAmI);
					Vector2 diff = tornadoPos - base.Player.Center;
					float distance = ((Vector2)(ref diff)).Length();
					if (distance > 0f)
					{
						for (float i2 = 0f; i2 < distance; i2 += 15f)
						{
							Vector2 dustPos = base.Player.Center + diff * i2 / distance;
							Dust dust8 = Dust.NewDustDirect(dustPos, 0, 0, 269);
							dust8.position = dustPos;
							dust8.fadeIn = 0.5f;
							dust8.scale = 0.7f;
							dust8.velocity *= 0.4f;
							dust8.noLight = true;
						}
					}
					for (int num2 = 0; num2 < 30; num2++)
					{
						Dust dust9 = Dust.NewDustDirect(tornadoPos, 0, 0, 269);
						dust9.position = tornadoPos;
						dust9.fadeIn = 1f;
						dust9.scale = 0.3f;
						dust9.noLight = true;
					}
				}
			}
		}
		if (prismaticSet && !base.Player.HasCooldown(PrismaticLaser.ID) && prismaticLasers <= 0)
		{
			prismaticLasers = PrismaticHelmet.LaserDuration + PrismaticHelmet.LaserCooldown;
		}
		if (WulfrumHat.HasArmorSet(base.Player))
		{
			if (cooldowns.TryGetValue(WulfrumBastion.ID, out var cd))
			{
				if (cd.timeLeft > WulfrumHat.BastionCooldown && cd.timeLeft < WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - 180)
				{
					cd.timeLeft = WulfrumHat.BastionCooldown + 1;
					SyncCooldownDictionary(server: false);
				}
			}
			else if (base.Player.HasItem(ModContent.ItemType<WulfrumMetalScrap>()))
			{
				base.Player.ConsumeItem(ModContent.ItemType<WulfrumMetalScrap>());
				base.Player.AddCooldown(WulfrumBastion.ID, WulfrumHat.BastionCooldown + WulfrumHat.BastionTime);
				WulfrumHat.DummyCannon.SetDefaults(ModContent.ItemType<WulfrumFusionCannon>());
			}
		}
		if (DesertProwlerHat.HasArmorSet(base.Player) && !base.Player.HasCooldown(SandsmokeBomb.ID))
		{
			base.Player.AddCooldown(SandsmokeBomb.ID, DesertProwlerHat.SmokeCooldown + DesertProwlerHat.SmokeDuration);
		}
	}

	public static Vector2? GetUnderworldPosition(Player player)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		bool canSpawn = false;
		int halfWorldXTiles = Main.maxTilesX / 2;
		int largerCheckRadius = 100;
		int smallerCheckRadius = 50;
		int teleportStartY = Main.UnderworldLayer + 20;
		int teleportRangeY = 80;
		Player.RandomTeleportationAttemptSettings settings = new Player.RandomTeleportationAttemptSettings
		{
			mostlySolidFloor = true,
			avoidAnyLiquid = true,
			avoidLava = true,
			avoidHurtTiles = true,
			avoidWalls = true,
			attemptsBeforeGivingUp = 1000,
			maximumFallDistanceFromOrignalPoint = 30
		};
		Vector2 vector = player.CheckForGoodTeleportationSpot(ref canSpawn, halfWorldXTiles - smallerCheckRadius, largerCheckRadius, teleportStartY, teleportRangeY, settings);
		if (!canSpawn)
		{
			vector = player.CheckForGoodTeleportationSpot(ref canSpawn, halfWorldXTiles - largerCheckRadius, smallerCheckRadius, teleportStartY, teleportRangeY, settings);
		}
		if (!canSpawn)
		{
			vector = player.CheckForGoodTeleportationSpot(ref canSpawn, halfWorldXTiles + smallerCheckRadius, smallerCheckRadius, teleportStartY, teleportRangeY, settings);
		}
		if (canSpawn)
		{
			return vector;
		}
		return null;
	}

	public static void ModTeleport(Player player, Vector2 pos, bool playSound = true, int style = 3)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		bool postImmune = player.immune;
		int postImmuneTime = player.immuneTime;
		player.StopVanityActions(multiplayerBroadcast: false);
		player.RemoveAllGrapplingHooks();
		player.Teleport(pos, style);
		if (Main.dedServ)
		{
			RemoteClient.CheckSection(player.whoAmI, player.Center);
		}
		NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, pos.X, pos.Y, style);
		player.velocity = Vector2.Zero;
		player.immune = postImmune;
		player.immuneTime = postImmuneTime;
		for (int index = 0; index < 100; index++)
		{
			Dust.NewDust(player.position, player.width, player.height, 164, player.velocity.X * 0.1f, player.velocity.Y * 0.1f, 150, Color.Cyan, 1.2f);
		}
		Rectangle rect = player.getRect();
		int dustAmt = rect.Width * rect.Height / 5;
		for (int k = 0; k < dustAmt; k++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2((float)rect.X, (float)rect.Y), rect.Width, rect.Height, 164);
			dust.scale = Main.rand.NextFloat(0.2f, 0.7f);
			if (k < 10)
			{
				dust.scale += 0.25f;
			}
			if (k < 5)
			{
				dust.scale += 0.25f;
			}
		}
		for (int i = 0; i < 50; i++)
		{
			Dust dust2 = Dust.NewDustDirect(new Vector2((float)rect.X, (float)rect.Y), rect.Width, rect.Height, 180);
			dust2.noGravity = true;
			for (int j = 0; j < 5; j++)
			{
				if (Main.rand.NextBool(3))
				{
					dust2.velocity *= 0.75f;
				}
			}
			if (Main.rand.NextBool(3))
			{
				dust2.velocity *= 2f;
				dust2.scale *= 1.2f;
			}
			if (Main.rand.NextBool(3))
			{
				dust2.velocity *= 2f;
				dust2.scale *= 1.2f;
			}
			if (Main.rand.NextBool())
			{
				dust2.fadeIn = Main.rand.NextFloat(0.75f, 1f);
				dust2.scale = Main.rand.NextFloat(0.25f, 0.75f);
			}
			dust2.scale *= 0.8f;
		}
		if (playSound)
		{
			SoundEngine.PlaySound(in SoundID.Item6, player.Center);
		}
	}

	public override void UpdateEquips()
	{
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		CalamityClientConfig.Instance.BossHealthBarExtraInfo = shouldDrawSmallText;
		VanillaArmorChangeManager.ApplyPotentialEffectsTo(base.Player);
		if (base.Player.ghostDmg > 0f)
		{
			base.Player.ghostDmg += 2.6666665f;
		}
		if (CalamityServerConfig.Instance.FasterTilePlacement)
		{
			base.Player.tileSpeed += 0.5f;
			base.Player.wallSpeed += 0.5f;
		}
		float accRunSpeedMin = base.Player.accRunSpeed * 0.5f;
		base.Player.accRunSpeed += base.Player.accRunSpeed * moveSpeedBonus * 0.16f;
		if (base.Player.accRunSpeed < accRunSpeedMin)
		{
			base.Player.accRunSpeed = accRunSpeedMin;
		}
		if (base.Player.blackBelt)
		{
			DodgeEffects.Add(delegate(Player Player, Player.HurtInfo hit)
			{
				Player.NinjaDodge();
				return (string?)null;
			});
		}
		if (base.Player.brainOfConfusionItem != null && !base.Player.brainOfConfusionItem.IsAir)
		{
			DodgeEffects.Add(delegate(Player Player, Player.HurtInfo hit)
			{
				Player.BrainOfConfusionDodge();
				return (string?)null;
			});
		}
		if (base.Player.Transformation().Type == ModContent.ItemType<Popo>() && base.Player.whoAmI == Main.myPlayer && !snowmanNoseless)
		{
			base.Player.AddBuff(ModContent.BuffType<PopoBuff>(), 60);
		}
		if (abyssalDivingSuit)
		{
			base.Player.AddBuff(ModContent.BuffType<AbyssalDivingSuitBuff>(), 60);
		}
		if (aquaticHeart)
		{
			base.Player.AddBuff(ModContent.BuffType<AquaticHeartBuff>(), 60);
		}
		if (aquaticHeart && NPC.downedBoss3 && base.Player.whoAmI == Main.myPlayer && !base.Player.HasCooldown(AquaticHeartIceShield.ID))
		{
			base.Player.AddBuff(ModContent.BuffType<IceShieldBuff>(), 2);
		}
		if (profanedCrystal)
		{
			base.Player.AddBuff(ModContent.BuffType<ProfanedCrystalBuff>(), 60);
		}
		if (gSabaton)
		{
			if (base.Player.whoAmI == Main.myPlayer)
			{
				if (gSabatonHotkeyFallWindup < 20 && gSabatonHotkeyFallWindup != 0 && !gSabatonFalling)
				{
					base.Player.velocity.Y *= (60f - (float)gSabatonHotkeyFallWindup * 0.75f) / 60f;
				}
				if (gSabatonHotkeyFallWindup == 5 && !gSabatonFalling)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/GravistarCharge");
					style.Volume = 0.3f;
					SoundEngine.PlaySound(in style);
				}
				if (gSabatonHotkeyFallWindup == 20)
				{
					gSabatonFalling = true;
					base.Player.velocity.Y = 0.01f;
				}
				if ((base.Player.gravDir == 1f && base.Player.velocity.Y < 0f) || (base.Player.gravDir == -1f && base.Player.velocity.Y > 1f) || base.Player.pulley || base.Player.mount.Active || base.Player.grappling[0] != -1 || base.Player.tongued)
				{
					gSabatonFall = 0;
					gSabatonFalling = false;
					gSabatonHotkeyFallWindup = -1;
				}
				if (gSabatonFalling)
				{
					SpawnGravistarParticle();
					if (gSabatonFall < 120)
					{
						gSabatonFall++;
					}
					base.Player.maxFallSpeed = 40f;
					base.Player.gravity = 1.3f;
					base.Player.controlJump = false;
					if (0f == base.Player.velocity.Y)
					{
						Projectile.NewProjectile(base.Player.GetSource_Accessory(FindAccessory<InterstellarStompers>()), Damage: base.Player.CalcIntDamage<MeleeDamageClass>(InterstellarStompers.SlamDamage), position: base.Player.Center, velocity: Vector2.Zero, Type: ModContent.ProjectileType<StomperSlam>(), KnockBack: 4f, Owner: base.Player.whoAmI, ai0: gSabatonFall);
						gSabatonFall = 0;
						gSabatonFalling = false;
						gSabatonHotkeyFallWindup = -1;
						gSabatonTempJumpSpeed = 40;
					}
				}
			}
		}
		else
		{
			gSabatonFall = 0;
			gSabatonFalling = false;
			gSabatonHotkeyFallWindup = -1;
		}
		if (!evolution || !base.Player.HasCooldown(GlobalDodge.ID))
		{
			projTypeJustHitBy = -1;
		}
	}

	public override void PreUpdate()
	{
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		universalFrameTimer++;
		if (infiniteFlight)
		{
			base.Player.wingTime = base.Player.wingTimeMax;
		}
		CalamityFireDyeShader = null;
		if (HasCustomDash && UsedDash.IsOmnidirectional)
		{
			base.Player.maxFallSpeed = 50f;
		}
		tailFrameUp++;
		if (tailFrameUp == 8)
		{
			tailFrame++;
			if (tailFrame >= 4)
			{
				tailFrame = 0;
			}
			tailFrameUp = 0;
		}
		int frames = 4;
		if (voidFrameCounter >= 6)
		{
			voidFrameCounter = 0;
			voidFrame = ((voidFrame != frames - 1) ? (voidFrame + 1) : 0);
		}
		voidFrameCounter++;
		for (int i = 0; i < base.Player.dye.Length; i++)
		{
			if (base.Player.dye[i].type == ModContent.ItemType<ProfanedMoonlightDye>())
			{
				GameShaders.Armor.GetSecondaryShader(base.Player.dye[i].dye, base.Player)?.UseColor(GetCurrentMoonlightDyeColor());
			}
		}
		if (Main.myPlayer == base.Player.whoAmI)
		{
			mouseRight = PlayerInput.Triggers.Current.MouseRight;
			Vector2 worldPos = (LockOnHelper.Enabled ? LockOnHelper.PredictedPosition : Main.MouseWorld);
			mouseWorldDeltaFromPlayer = worldPos - base.Player.MountedCenter;
			mouseRotationFromPlayer = mouseWorldDeltaFromPlayer.ToRotation();
			if (rightClickListener && mouseRight != oldMouseRight)
			{
				oldMouseRight = mouseRight;
				syncMouseRightClick = true;
				rightClickListener = false;
			}
			if (mouseWorldListener && Vector2.Distance(mouseWorldDeltaFromPlayer, oldMouseWorldDeltaFromPlayer) > 5f)
			{
				oldMouseWorldDeltaFromPlayer = mouseWorldDeltaFromPlayer;
				syncMousePosition = true;
				mouseWorldListener = false;
			}
			if (mouseRotationListener && Math.Abs(mouseWorldDeltaFromPlayer.ToRotation() - oldMouseWorldDeltaFromPlayer.ToRotation()) > 0.15f)
			{
				oldMouseWorldDeltaFromPlayer = mouseWorldDeltaFromPlayer;
				syncMouseRotation = true;
				mouseRotationListener = false;
			}
		}
	}

	public override void PreUpdateBuffs()
	{
		if (base.Player.ZoneDesert && (ZoneAstral || areThereAnyDamnBosses) && base.Player.HasBuff(194))
		{
			base.Player.ClearBuff(194);
		}
		if (base.Player.statMana < 0 && base.Player.Calamity().ChaosStone)
		{
			base.Player.AddBuff(ModContent.BuffType<ManaBurn>(), 10);
		}
		else if (base.Player.HasBuff(ModContent.BuffType<ManaBurn>()))
		{
			base.Player.ClearBuff(ModContent.BuffType<ManaBurn>());
		}
	}

	public override void OnExtraJumpStarted(ExtraJump jump, ref bool playSound)
	{
		if (rainSet && base.Player.whoAmI == Main.myPlayer)
		{
			RainArmorSetChange.SpawnRainArmorJump(base.Player);
		}
	}

	public override void PreUpdateMovement()
	{
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		if (rainSet && base.Player.justJumped && base.Player.whoAmI == Main.myPlayer)
		{
			RainArmorSetChange.SpawnRainArmorJump(base.Player);
		}
		if (redWine)
		{
			if (base.Player.velocity.Y > 0.2f || base.Player.velocity.Y < -0.2f)
			{
				redWineStoredY = base.Player.velocity.Y;
				base.Player.velocity.Y *= 1f + RedWine.VerticalSpeedBoost;
			}
			else
			{
				redWineStoredY = 0f;
			}
		}
		if (base.Player.whoAmI == Main.myPlayer && ExoChair)
		{
			float speed = 12f;
			if (base.Player.controlLeft)
			{
				base.Player.velocity.X = 0f - speed;
				base.Player.ChangeDir(-1);
			}
			else if (base.Player.controlRight)
			{
				base.Player.velocity.X = speed;
				base.Player.ChangeDir(1);
			}
			else
			{
				base.Player.velocity.X = 0f;
			}
			if (base.Player.controlUp || base.Player.controlJump)
			{
				base.Player.velocity.Y = 0f - speed;
			}
			else if (base.Player.controlDown)
			{
				base.Player.velocity.Y = speed;
				if (Collision.TileCollision(base.Player.position, base.Player.velocity, base.Player.width, base.Player.height, fallThrough: true, fall2: false, (int)base.Player.gravDir).Y == 0f)
				{
					base.Player.velocity.Y = 0.5f;
				}
			}
			else
			{
				base.Player.velocity.Y = 0f;
			}
			if (CalamityKeybinds.ExoChairSlowdownHotkey.Current)
			{
				Player player = base.Player;
				player.velocity *= 0.5f;
			}
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.type != ModContent.NPCType<RockPillar>() || !(npc.Opacity > 0.9f) || npc.damage >= 1)
			{
				continue;
			}
			bool LandedOnPlayer = false;
			float origX = base.Player.velocity.X;
			float origY = base.Player.velocity.Y;
			NPC nPC = npc;
			nPC.position += npc.velocity;
			bool intersectPreVel = false;
			Player player2 = base.Player;
			player2.position += base.Player.velocity;
			Rectangle hitbox = base.Player.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(npc.Hitbox))
			{
				intersectPreVel = true;
			}
			Player player3 = base.Player;
			player3.position -= base.Player.velocity;
			if (intersectPreVel)
			{
				Player player4 = base.Player;
				player4.position += npc.velocity;
				if (Collision.SolidCollision(base.Player.position, base.Player.Hitbox.Width, base.Player.Hitbox.Height) || Main.npc.Any(delegate(NPC x)
				{
					//IL_0036: Unknown result type (might be due to invalid IL or missing references)
					//IL_003b: Unknown result type (might be due to invalid IL or missing references)
					//IL_0049: Unknown result type (might be due to invalid IL or missing references)
					if (x.active && x.type == ModContent.NPCType<RockPillar>() && x.Opacity > 0.9f && x.whoAmI != npc.whoAmI)
					{
						Rectangle hitbox2 = x.Hitbox;
						return ((Rectangle)(ref hitbox2)).Intersects(base.Player.Hitbox);
					}
					return false;
				}))
				{
					npc.damage = npc.defDamage;
					npc.ai[0] = 1f;
					LandedOnPlayer = true;
					Player player5 = base.Player;
					player5.position -= npc.velocity;
				}
			}
			NPC nPC2 = npc;
			nPC2.position -= npc.velocity;
			if (LandedOnPlayer)
			{
				continue;
			}
			bool intersectX = false;
			base.Player.position.X += base.Player.velocity.X;
			hitbox = base.Player.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(npc.Hitbox))
			{
				intersectX = true;
			}
			base.Player.position.X -= base.Player.velocity.X;
			bool intersectY = false;
			base.Player.position.Y += base.Player.velocity.Y;
			hitbox = base.Player.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(npc.Hitbox))
			{
				intersectY = true;
			}
			base.Player.position.Y -= base.Player.velocity.Y;
			bool intersect = false;
			if (!intersectX && !intersectY)
			{
				Player player6 = base.Player;
				player6.position += base.Player.velocity;
				hitbox = base.Player.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(npc.Hitbox))
				{
					intersect = true;
				}
				Player player7 = base.Player;
				player7.position -= base.Player.velocity;
			}
			if (intersectX | intersect)
			{
				if (base.Player.Center.X < npc.Center.X)
				{
					float num = base.Player.Hitbox.X + base.Player.Hitbox.Width;
					float pillarEdge = npc.Hitbox.X;
					float goalEdge = num + base.Player.velocity.X;
					if (goalEdge > pillarEdge)
					{
						base.Player.velocity.X += pillarEdge - goalEdge;
						if (base.Player.velocity.X < 0f)
						{
							if (!Collision.SolidCollision(base.Player.position + new Vector2(base.Player.velocity.X, 0f), base.Player.Hitbox.Width, base.Player.Hitbox.Height))
							{
								base.Player.position.X += base.Player.velocity.X;
								base.Player.velocity.X = 0f;
							}
							else
							{
								base.Player.velocity.X = origX;
							}
						}
					}
				}
				else
				{
					float num2 = base.Player.Hitbox.X;
					float pillarEdge2 = npc.Hitbox.X + npc.Hitbox.Width;
					float goalEdge2 = num2 + base.Player.velocity.X;
					if (goalEdge2 < pillarEdge2)
					{
						base.Player.velocity.X += pillarEdge2 - goalEdge2;
						if (base.Player.velocity.X > 0f)
						{
							if (!Collision.SolidCollision(base.Player.position + new Vector2(base.Player.velocity.X, 0f), base.Player.Hitbox.Width, base.Player.Hitbox.Height))
							{
								base.Player.position.X += base.Player.velocity.X;
								base.Player.velocity.X = 0f;
							}
							else
							{
								base.Player.velocity.X = origX;
							}
						}
					}
				}
			}
			if (intersectY | intersect)
			{
				if (base.Player.Center.Y < npc.Center.Y)
				{
					float num3 = base.Player.Hitbox.Y + base.Player.Hitbox.Height;
					float pillarEdge3 = npc.Hitbox.Y;
					float goalEdge3 = num3 + base.Player.velocity.Y;
					if (goalEdge3 > pillarEdge3)
					{
						base.Player.velocity.Y += pillarEdge3 - goalEdge3;
						if (base.Player.velocity.Y < 0f)
						{
							if (!Collision.SolidCollision(base.Player.position + new Vector2(0f, base.Player.velocity.Y), base.Player.Hitbox.Width, base.Player.Hitbox.Height))
							{
								base.Player.position.Y += base.Player.velocity.Y;
								base.Player.velocity.Y = 0f;
							}
							else
							{
								base.Player.velocity.Y = origY;
							}
						}
					}
				}
				else
				{
					float num4 = base.Player.Hitbox.Y;
					float pillarEdge4 = npc.Hitbox.Y + npc.Hitbox.Height;
					float goalEdge4 = num4 + base.Player.velocity.Y;
					if (goalEdge4 < pillarEdge4)
					{
						base.Player.velocity.Y += pillarEdge4 - goalEdge4;
						if (base.Player.velocity.Y > 0f)
						{
							if (!Collision.SolidCollision(base.Player.position + new Vector2(0f, base.Player.velocity.Y), base.Player.Hitbox.Width, base.Player.Hitbox.Height))
							{
								base.Player.position.Y += base.Player.velocity.Y;
								base.Player.velocity.Y = 0f;
							}
							else
							{
								base.Player.velocity.Y = origY;
							}
						}
					}
				}
			}
			if (intersectY && base.Player.velocity.Y < 0.25f && base.Player.velocity.Y > -0.25f)
			{
				base.Player.velocity.Y = 0f;
			}
		}
	}

	public override void PostUpdateBuffs()
	{
		if (base.Player.whoAmI == Main.myPlayer && CalamityClientConfig.Instance.VanillaCooldownDisplay)
		{
			if (cooldowns.TryGetValue(PotionSickness.ID, out var cd))
			{
				if (base.Player.potionDelay != cd.timeLeft && cd.timeLeft > 0)
				{
					cd.timeLeft = base.Player.potionDelay;
				}
				if (cd.timeLeft > cd.duration)
				{
					cd.duration = cd.timeLeft;
				}
			}
			else if (base.Player.whoAmI == Main.myPlayer && base.Player.potionDelay > 0)
			{
				base.Player.AddCooldown(PotionSickness.ID, base.Player.potionDelay, overwrite: false);
			}
			if (base.Player.chaosState && !base.Player.HasCooldown(ChaosState.ID))
			{
				for (int l = 0; l < Player.MaxBuffs; l++)
				{
					if (base.Player.buffType[l] == 88)
					{
						base.Player.AddCooldown(ChaosState.ID, base.Player.buffTime[l], overwrite: false);
						break;
					}
				}
			}
		}
		if (base.Player.whoAmI == Main.myPlayer)
		{
			if (Main.expertMode)
			{
				_ = BalancingConstants.LifeStealRecoveryRate_Expert;
			}
			else
				_ = BalancingConstants.LifeStealRecoveryRate_Classic;
			float lifeStealRecoveryRateReduction = (Main.expertMode ? BalancingConstants.LifeStealRecoveryRateReduction_Expert : BalancingConstants.LifeStealRecoveryRateReduction_Classic);
			float lifeStealCap = (Main.expertMode ? BalancingConstants.LifeStealCap_Expert : BalancingConstants.LifeStealCap_Classic);
			if (base.Player.lifeSteal < lifeStealCap)
			{
				if (base.Player.Calamity().cooldowns.TryGetValue(LifeSteal.ID, out var cooldown))
				{
					cooldown.timeLeft = (int)Math.Max(0f, lifeStealCap - Math.Abs(base.Player.lifeSteal));
				}
				else
				{
					base.Player.AddCooldown(LifeSteal.ID, (int)lifeStealCap).timeLeft = (int)Math.Max(0f, lifeStealCap - Math.Abs(base.Player.lifeSteal));
				}
			}
		}
		if (moonshine)
		{
			base.Player.statLifeMax2 = (int)((float)base.Player.statLifeMax2 * (1f + Moonshine.MaxLifePercentBoost));
		}
		ForceVariousEffects();
	}

	public override void PostUpdateEquips()
	{
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		Item item = base.Player.HeldItem;
		if (!item.IsAir && item.damage > 0)
		{
			if (item.DamageType.CountsAsClass(DamageClass.Melee))
			{
				TimeHoldingMelee++;
				if ((float)TimeHoldingMelee > Whiskey.TimeToDischarge)
				{
					TimeHoldingMelee = (int)Whiskey.TimeToDischarge;
				}
				if (whiskey)
				{
					base.Player.GetDamage(DamageClass.Melee) += MathHelper.Lerp(Whiskey.MaxDamageBoost, Whiskey.MinDamageBoost, (float)TimeHoldingMelee / Whiskey.TimeToDischarge);
				}
			}
			else
			{
				TimeHoldingMelee -= 2;
				if (TimeHoldingMelee < 0)
				{
					TimeHoldingMelee = 0;
				}
			}
			if (item.DamageType.CountsAsClass(DamageClass.Ranged))
			{
				TimeHoldingRanged++;
				if ((float)TimeHoldingRanged > Whiskey.TimeToDischarge)
				{
					TimeHoldingRanged = (int)Whiskey.TimeToDischarge;
				}
				if (whiskey)
				{
					base.Player.GetDamage(DamageClass.Ranged) += MathHelper.Lerp(Whiskey.MaxDamageBoost, Whiskey.MinDamageBoost, (float)TimeHoldingRanged / Whiskey.TimeToDischarge);
				}
			}
			else
			{
				TimeHoldingRanged -= 2;
				if (TimeHoldingRanged < 0)
				{
					TimeHoldingRanged = 0;
				}
			}
			if (item.DamageType.CountsAsClass(DamageClass.Magic))
			{
				TimeHoldingMagic++;
				if ((float)TimeHoldingMagic > Whiskey.TimeToDischarge)
				{
					TimeHoldingMagic = (int)Whiskey.TimeToDischarge;
				}
				if (whiskey)
				{
					base.Player.GetDamage(DamageClass.Magic) += MathHelper.Lerp(Whiskey.MaxDamageBoost, Whiskey.MinDamageBoost, (float)TimeHoldingMagic / Whiskey.TimeToDischarge);
				}
			}
			else
			{
				TimeHoldingMagic -= 2;
				if (TimeHoldingMagic < 0)
				{
					TimeHoldingMagic = 0;
				}
			}
			if (item.DamageType.CountsAsClass(DamageClass.Summon))
			{
				TimeHoldingSummon++;
				if ((float)TimeHoldingSummon > Whiskey.TimeToDischarge)
				{
					TimeHoldingSummon = (int)Whiskey.TimeToDischarge;
				}
				if (whiskey)
				{
					base.Player.GetDamage(DamageClass.Summon) += MathHelper.Lerp(Whiskey.MaxDamageBoost, Whiskey.MinDamageBoost, (float)TimeHoldingSummon / Whiskey.TimeToDischarge);
				}
			}
			else
			{
				TimeHoldingSummon -= 2;
				if (TimeHoldingSummon < 0)
				{
					TimeHoldingSummon = 0;
				}
			}
			if (item.DamageType.CountsAsClass(RogueDamageClass.Instance))
			{
				TimeHoldingRogue++;
				if ((float)TimeHoldingRogue > Whiskey.TimeToDischarge)
				{
					TimeHoldingRogue = (int)Whiskey.TimeToDischarge;
				}
				if (whiskey)
				{
					base.Player.GetDamage(RogueDamageClass.Instance) += MathHelper.Lerp(Whiskey.MaxDamageBoost, Whiskey.MinDamageBoost, (float)TimeHoldingRogue / Whiskey.TimeToDischarge);
				}
			}
			else
			{
				TimeHoldingRogue -= 2;
				if (TimeHoldingRogue < 0)
				{
					TimeHoldingRogue = 0;
				}
			}
		}
		else
		{
			if (TimeHoldingMelee-- < 0)
			{
				TimeHoldingMelee = 0;
			}
			if (TimeHoldingRanged-- < 0)
			{
				TimeHoldingRanged = 0;
			}
			if (TimeHoldingMagic-- < 0)
			{
				TimeHoldingMagic = 0;
			}
			if (TimeHoldingSummon-- < 0)
			{
				TimeHoldingSummon = 0;
			}
			if (TimeHoldingRogue-- < 0)
			{
				TimeHoldingRogue = 0;
			}
		}
		if (base.Player.chiselSpeed)
		{
			base.Player.pickSpeed += 0.1f;
		}
		if (oceanCrest)
		{
			bool surface = (double)base.Player.Center.Y < Main.worldSurface * 16.0;
			bool GetEffects = (Main.raining & surface) || base.Player.dripping || (base.Player.wet && !base.Player.lavaWet && !base.Player.honeyWet);
			if (GetEffects)
			{
				if (oceanCrestTimer < 300)
				{
					oceanCrestTimer += 5;
				}
				if (base.Player.StandingStill(0.1f) && !ZoneAbyss && base.Player.breath < 201 && base.Player.miscCounter % 2 == 0)
				{
					base.Player.breath++;
				}
			}
			else if (oceanCrestTimer > 0)
			{
				oceanCrestTimer--;
			}
			if ((oceanCrestTimer > 0) | GetEffects)
			{
				base.Player.pickSpeed -= 0.15f;
			}
			Vector3 Light = default(Vector3);
			((Vector3)(ref Light))._002Ector(0.09f, 0.18f, 0.2f);
			Lighting.AddLight(base.Player.Center, Light * (0.55f + (float)oceanCrestTimer * 0.0035f));
		}
		if (rampartOfDeities)
		{
			if ((double)base.Player.statLife <= (double)base.Player.statLifeMax2 * 0.5)
			{
				base.Player.AddBuff(62, 5);
			}
			if ((float)base.Player.statLife > (float)base.Player.statLifeMax2 * 0.25f)
			{
				base.Player.hasPaladinShield = true;
				if (base.Player.whoAmI != Main.myPlayer && base.Player.miscCounter % 10 == 0)
				{
					Player localPlayer = Main.LocalPlayer;
					if (localPlayer.team == base.Player.team && base.Player.team != 0)
					{
						float num = base.Player.position.X - localPlayer.position.X;
						float teamPlayerYDist = base.Player.position.Y - localPlayer.position.Y;
						if ((float)Math.Sqrt(num * num + teamPlayerYDist * teamPlayerYDist) < 800f)
						{
							localPlayer.AddBuff(43, 20);
						}
					}
				}
			}
		}
		if (scionsCurio)
		{
			scionsCurioDebuffDamage = base.Player.GetTotalDamage(DamageClass.Ranged).ApplyTo(Irradiated.debuffData.EnemyLostRegen * (1f + base.Player.GetTotalCritChance(DamageClass.Ranged) * 0.01f));
		}
		else
		{
			scionsCurioDebuffDamage = 0f;
		}
		if (base.Player.kbGlove)
		{
			base.Player.GetDamage<TrueMeleeDamageClass>() += 0.1f;
		}
		ForceVariousEffects();
		BaseIdleHoldoutProjectile.CheckForEveryHoldout(base.Player);
		if (gSabatonTempJumpSpeed > 0)
		{
			gSabatonTempJumpSpeed--;
			if (gSabaton && base.Player.whoAmI == Main.myPlayer)
			{
				base.Player.jumpSpeedBoost += 2f;
			}
		}
	}

	public override bool CanSellItem(NPC vendor, Item[] shopInventory, Item item)
	{
		if (item.type == ModContent.ItemType<ProfanedSoulCrystal>())
		{
			if (DownedBossSystem.downedCalamitas)
			{
				return DownedBossSystem.downedExoMechs;
			}
			return false;
		}
		return base.CanSellItem(vendor, shopInventory, item);
	}

	public override void PostUpdate()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		if (ZoneAbyss && Main.netMode != 2)
		{
			EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource(base.Player.Center, null, 4f * abyssPlayerGlowMultiplier));
			Vector2 mouseworld = Main.MouseWorld + base.Player.position - base.Player.oldPosition;
			EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource((Vector2?)(base.Player.Center + base.Player.DirectionTo(mouseworld) * 750f), rotation: base.Player.DirectionTo(mouseworld).ToRotation() - (float)Math.PI / 2f, vectorScale: (Vector2?)new Vector2(0.85f * abyssFlashlightWidthMultiplier, 0.75f), texture: ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineFade", (AssetRequestMode)2).Value, scale: 1f, opacity: 1f));
		}
		if (lastDeerclopsPosition.HasValue)
		{
			if (Main.npc.Any((NPC x) => x.active && x.type == 668) && base.Player.DistanceSQ(lastDeerclopsPosition.Value) < 10240000f)
			{
				darknessIntensity = MathHelper.Min(Main.LocalPlayer.Calamity().darknessIntensity + 0.06f, 1f);
			}
			if (DeerclopsAI.ArenaTex == null)
			{
				DeerclopsAI.ArenaTex = ModContent.Request<Texture2D>(DeerclopsAI.ArenaTexPath, (AssetRequestMode)2);
			}
			EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource(lastDeerclopsPosition.Value, scale: DeerclopsAI.borderScale, texture: DeerclopsAI.ArenaTex.Value));
			List<EnhancedDarknessSystem.LightSource> lights = EnhancedDarknessSystem.lights;
			float opacity = MathHelper.Clamp(Main.LocalPlayer.DistanceSQ(lastDeerclopsPosition.Value) / 409600f, 0f, 1f);
			lights.Add(new EnhancedDarknessSystem.LightSource(null, null, 0.75f, 0f, null, opacity));
			if (darknessIntensity == 0f)
			{
				lastDeerclopsPosition = null;
			}
		}
		if (redWine && (redWineStoredY > 0.2f || redWineStoredY < -0.2f) && (base.Player.velocity.Y > 0.2f || base.Player.velocity.Y < -0.2f))
		{
			base.Player.velocity.Y = redWineStoredY;
		}
		if (subtitletext != null)
		{
			if (!subtitletext.active)
			{
				subtitletext = null;
			}
			else
			{
				subtitletext.position = base.Player.Center + new Vector2((0f - FontAssets.CombatText[subtitletext.crit ? 1u : 0u].Value.MeasureString(subtitletext.text).X) * 0.5f, 64f);
				subtitletext.color = Color.Lerp(subtitleColors[1], subtitleColors[0], (float)subtitletext.lifeTime / 120f);
			}
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfConvergenceCrystal>()] > 0 && base.Player.HeldItem.type == ModContent.ItemType<RelicOfConvergence>())
		{
			base.Player.statDefense *= RelicOfConvergence.DefenseMultiplier;
		}
		if (Main.netMode == 2 || base.Player != Main.LocalPlayer)
		{
			return;
		}
		CreativePowers.FreezeTime power = CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>();
		double dayrate = CreativePowerManager.Instance.GetPower<CreativePowers.ModifyTimeRate>().TargetTimeRate;
		if (Main.CurrentFrameFlags.SleepingPlayersCount == Main.CurrentFrameFlags.ActivePlayersCount && Main.CurrentFrameFlags.SleepingPlayersCount > 0)
		{
			dayrate *= 5.0;
		}
		if (Main.IsFastForwardingTime())
		{
			dayrate = 60.0;
		}
		double tileUpdate = 1.0;
		double eventUpdate = 1.0;
		SystemLoader.ModifyTimeRate(ref dayrate, ref tileUpdate, ref eventUpdate);
		if (WeakTimeFreezeInUse)
		{
			if (!power.Enabled)
			{
				WeakTimeFreezeInUse = false;
				return;
			}
			if (WeakTimeFreezeUseTimer >= (float)Bakidon.FreezeTime / Bakidon.RechargeMultiplier || Main.bloodMoon || Main.eclipse || Main.pumpkinMoon || Main.snowMoon)
			{
				NetPacket packet = NetCreativePowersModule.PreparePacket(power.PowerId, 1);
				packet.Writer.Write(value: false);
				NetManager.Instance.SendToServerOrLoopback(packet);
				WeakTimeFreezeInUse = false;
			}
			WeakTimeFreezeUseTimer += 1f * (float)dayrate;
		}
		else if (WeakTimeFreezeUseTimer > 0f)
		{
			WeakTimeFreezeUseTimer -= 1f * (float)dayrate;
		}
		else
		{
			WeakTimeFreezeUseTimer = 0f;
		}
	}

	public override void PostUpdateRunSpeeds()
	{
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Player.mount.Active)
		{
			float runAccMult = 1f + (lunicCorpsLegs ? LunicCorpsBoots.MoveSpeedAccelerationBoost : 0f) + (shadowSpeed ? DemonshadeGreaves.AccelerationBoost : 0f) + (stressPills ? 0.05f : 0f) + ((abyssalDivingSuit && base.Player.IsUnderwater()) ? 0.05f : 0f) + (aquaticHeartWaterBuff ? AquaticHeart.WaterSpeedBoost : 0f) + ((laudanum && base.Player.HasBuff(164)) ? 0.15f : 0f) + ((frostFlare && base.Player.statLife <= (int)((double)base.Player.statLifeMax2 * 0.5)) ? 0.15f : 0f) + (dragonScales ? 0.1f : 0f) + (CobaltSet ? 0.099999994f : 0f) + (silvaSet ? SilvaArmor.AccelerationBoost : 0f) + (getSandCloakAccelBoost ? 0.75f : 0f) + (ascendantInsignia ? (AscendantInsignia.AccelerationBoost - 0.25f) : 0f) + (statisNinjaBelt ? 0.6f : 0f) + (statisVoidSash ? 0.85f : 0f) + (blueCandle ? WeightlessCandle.AccelerationBoost : 0f) + ((planarSpeedBoost > 0) ? (0.01f * (float)planarSpeedBoost) : 0f) + (float)hasteLevel * 0.05f;
			float runSpeedMult = 1f + (lunicCorpsLegs ? LunicCorpsBoots.MoveSpeedAccelerationBoost : 0f) + (shadowSpeed ? DemonshadeGreaves.AccelerationBoost : 0f) + (stressPills ? 0.05f : 0f) + ((abyssalDivingSuit && base.Player.IsUnderwater()) ? 0.05f : 0f) + (aquaticHeartWaterBuff ? AquaticHeart.WaterSpeedBoost : 0f) + ((frostFlare && base.Player.statLife <= (int)((double)base.Player.statLifeMax2 * 0.5)) ? 0.15f : 0f) + (dragonScales ? 0.1f : 0f) + (CobaltSet ? 0.099999994f : 0f) + (silvaSet ? SilvaArmor.AccelerationBoost : 0f) + ((planarSpeedBoost > 0) ? (0.01f * (float)planarSpeedBoost) : 0f) + (float)hasteLevel * 0.05f;
			if ((base.Player.slippy || base.Player.slippy2) && base.Player.iceSkate)
			{
				runAccMult *= 0.6f;
			}
			if (momentumCapacitorTime > 0)
			{
				runAccMult += momentumCapacitorBoost * 0.25f;
				runSpeedMult += momentumCapacitorBoost;
				if (momentumCapacitorTime < 13)
				{
					momentumCapacitorBoost *= Main.rand.NextFloat(0.955f, 0.99f);
				}
			}
			else
			{
				momentumCapacitorBoost = 0f;
			}
			base.Player.runAcceleration *= runAccMult;
			base.Player.maxRunSpeed *= runSpeedMult;
		}
		if (!string.IsNullOrEmpty(DeferredDashID))
		{
			DashID = DeferredDashID;
			DeferredDashID = string.Empty;
		}
		if (base.Player.pulley && HasCustomDash)
		{
			ModDashMovement();
		}
		else if (base.Player.grappling[0] == -1 && !base.Player.tongued)
		{
			ModHorizontalMovement();
			if (HasCustomDash)
			{
				ModDashMovement();
			}
		}
		base.Player.oldVelocity = base.Player.velocity;
	}

	public override void OnRespawn()
	{
		healToFull = true;
		base.Player.fullRotation = 0f;
	}

	public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
	{
		healValue = (int)((float)healValue * healingPotionMultiplier);
		if (bloomStone)
		{
			healValue = 0;
		}
	}

	public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
	{
		if (item.CountsAsClass<RogueDamageClass>() && item.TryGetGlobalItem<RogueGlobalItem>(out var rogueItem) && rogueItem.StealthStrikePrefixBonus != 0f && StealthStrikeAvailable())
		{
			damage *= rogueItem.StealthStrikePrefixBonus;
		}
	}

	public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
	{
		bool rogue = item.CountsAsClass<RogueDamageClass>();
		if (moscowMule)
		{
			knockback += MoscowMule.KnockbackBoost;
		}
		if (titanHeartMantle & rogue)
		{
			knockback += TitanHeartMantle.RogueKnockbackBoost;
		}
		if ((titanHeartSet && StealthStrikeAvailable()) & rogue)
		{
			knockback *= TitanHeartMask.StealthStrikeKnockbackMult;
		}
	}

	public override void ModifyLuck(ref float luck)
	{
		luck += calamityBonusLuck;
	}

	public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
	{
		if (CalamityItemSets.MagicGun[item.type] && meteorSet)
		{
			mult *= 0.33f;
		}
	}

	public override void MeleeEffects(Item item, Rectangle hitbox)
	{
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		if (!item.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic && base.Player.meleeEnchant > 0 && base.Player.meleeEnchant == 7)
		{
			if (Main.rand.NextBool(20))
			{
				int confettiDust = Main.rand.Next(139, 143);
				Dust dust = Dust.NewDustDirect(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, confettiDust, base.Player.velocity.X, base.Player.velocity.Y, 0, default(Color), 1.2f);
				dust.velocity.X *= (float)(1.0 + (double)Main.rand.Next(-50, 51) * 0.01);
				dust.velocity.Y *= (float)(1.0 + (double)Main.rand.Next(-50, 51) * 0.01);
				dust.velocity.X += (float)Main.rand.Next(-50, 51) * 0.05f;
				dust.velocity.Y += (float)Main.rand.Next(-50, 51) * 0.05f;
				dust.scale *= (float)(1.0 + (double)Main.rand.Next(-30, 31) * 0.01);
			}
			if (Main.rand.NextBool(40) && !Main.dedServ)
			{
				int confettiGore = Main.rand.Next(276, 283);
				int confetti = Gore.NewGore(base.Player.GetSource_ItemUse(item), new Vector2((float)hitbox.X, (float)hitbox.Y), base.Player.velocity, confettiGore);
				Main.gore[confetti].velocity.X *= (float)(1.0 + (double)Main.rand.Next(-50, 51) * 0.01);
				Main.gore[confetti].velocity.Y *= (float)(1.0 + (double)Main.rand.Next(-50, 51) * 0.01);
				Main.gore[confetti].scale *= (float)(1.0 + (double)Main.rand.Next(-20, 21) * 0.01);
				Main.gore[confetti].velocity.X += (float)Main.rand.Next(-50, 51) * 0.05f;
				Main.gore[confetti].velocity.Y += (float)Main.rand.Next(-50, 51) * 0.05f;
			}
		}
		if (!item.CountsAsClass<MeleeDamageClass>())
		{
			return;
		}
		IEntitySource source = base.Player.GetSource_ItemUse(item);
		if (fungalSymbiote && base.Player.HasBuff(ModContent.BuffType<Mushy>()) && base.Player.whoAmI == Main.myPlayer && (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.1) || base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.3) || base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.5) || base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.7) || base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.9)))
		{
			float yVel = 0f;
			float xVel = 0f;
			float yOffset = 0f;
			float xOffset = 0f;
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.9))
			{
				yVel = -7f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.7))
			{
				yVel = -6f;
				xVel = 2f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.5))
			{
				yVel = -4f;
				xVel = 4f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.3))
			{
				yVel = -2f;
				xVel = 6f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.1))
			{
				xVel = 7f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.7))
			{
				xOffset = 26f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.3))
			{
				xOffset -= 4f;
				yOffset -= 20f;
			}
			if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.1))
			{
				yOffset += 6f;
			}
			if (base.Player.direction == -1)
			{
				if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.9))
				{
					xOffset -= 8f;
				}
				if (base.Player.itemAnimation == (int)((double)base.Player.itemAnimationMax * 0.7))
				{
					xOffset -= 6f;
				}
			}
			yVel *= 1.5f;
			xVel *= 1.5f;
			xOffset *= (float)base.Player.direction;
			yOffset *= base.Player.gravDir;
			Projectile.NewProjectile(source, (float)(hitbox.X + hitbox.Width / 2) + xOffset, (float)(hitbox.Y + hitbox.Height / 2) + yOffset, (float)base.Player.direction * xVel, yVel * base.Player.gravDir, 131, 0, 0f, base.Player.whoAmI);
		}
		if (flaskHoly && Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 244, base.Player.velocity.X * 0.2f + (float)base.Player.direction * 3f, base.Player.velocity.Y * 0.2f, 100);
		}
		if (flaskBrimstone && Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, Main.rand.NextBool(3) ? 114 : ModContent.DustType<BrimstoneFlame>(), base.Player.velocity.X * 0.2f + (float)base.Player.direction * 3f, base.Player.velocity.Y * 0.2f, 100, default(Color), Main.rand.NextFloat(0.3f, 1f));
		}
		if (flaskCrumbling && Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, (!Main.rand.NextBool()) ? 1 : 121, base.Player.velocity.X * 0.2f + (float)base.Player.direction * 3f, base.Player.velocity.Y * 0.2f, 100, default(Color), Main.rand.NextFloat(0.2f, 0.7f));
		}
		if (eGauntlet && eGauntletVisuals && Main.rand.NextBool(3))
		{
			Dust.NewDustDirect(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, base.Player.velocity.X * 0.2f + (float)base.Player.direction * 3f, base.Player.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.25f).noGravity = true;
		}
		if (dsSetBonus && Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 27, base.Player.velocity.X * 0.2f + (float)base.Player.direction * 3f, base.Player.velocity.Y * 0.2f, 100, default(Color), 0.7f);
		}
	}

	public override bool CanUseItem(Item item)
	{
		if (item.healMana > 0 && brimflameFrenzy)
		{
			return false;
		}
		return base.CanUseItem(item);
	}

	public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		if (bladeArmEnchant)
		{
			return false;
		}
		if (purpleHaze && item.CountsAsClass<RogueDamageClass>() && StealthStrikeAvailable())
		{
			purpleHazeStealthTimer = 300;
		}
		if (veneratedLocket)
		{
			IEntitySource LocketSource = base.Player.GetSource_Accessory(FindAccessory<VeneratedLocket>());
			if (item.CountsAsClass<RogueDamageClass>())
			{
				if (!CalamityItemSets.DisablesVeneratedLocketEffect[item.type])
				{
					float veneratedCloneSpeed = item.shootSpeed;
					Vector2 realPlayerPos = base.Player.RotatedRelativePoint(base.Player.MountedCenter, reverseRotation: true);
					float veneratedCloneXPos = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
					float veneratedCloneYPos = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
					if (base.Player.gravDir == -1f)
					{
						veneratedCloneYPos = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - realPlayerPos.Y;
					}
					float veneratedCloneDistance = (float)Math.Sqrt(veneratedCloneXPos * veneratedCloneXPos + veneratedCloneYPos * veneratedCloneYPos);
					if ((float.IsNaN(veneratedCloneXPos) && float.IsNaN(veneratedCloneYPos)) || (veneratedCloneXPos == 0f && veneratedCloneYPos == 0f))
					{
						veneratedCloneXPos = base.Player.direction;
						veneratedCloneYPos = 0f;
						veneratedCloneDistance = veneratedCloneSpeed;
					}
					else
					{
						veneratedCloneDistance = veneratedCloneSpeed / veneratedCloneDistance;
					}
					((Vector2)(ref realPlayerPos))._002Ector(base.Player.position.X + (float)base.Player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)base.Player.direction) + ((float)Main.mouseX + Main.screenPosition.X - base.Player.position.X), base.Player.MountedCenter.Y - 600f);
					realPlayerPos.X = (realPlayerPos.X + base.Player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
					realPlayerPos.Y -= 100f;
					veneratedCloneXPos = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
					veneratedCloneYPos = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
					if (veneratedCloneYPos < 0f)
					{
						veneratedCloneYPos *= -1f;
					}
					if (veneratedCloneYPos < 20f)
					{
						veneratedCloneYPos = 20f;
					}
					veneratedCloneDistance = (float)Math.Sqrt(veneratedCloneXPos * veneratedCloneXPos + veneratedCloneYPos * veneratedCloneYPos);
					veneratedCloneDistance = veneratedCloneSpeed / veneratedCloneDistance;
					veneratedCloneXPos *= veneratedCloneDistance;
					veneratedCloneYPos *= veneratedCloneDistance;
					float speedX4 = veneratedCloneXPos + (float)Main.rand.Next(-30, 31) * 0.02f;
					float speedY5 = veneratedCloneYPos + (float)Main.rand.Next(-30, 31) * 0.02f;
					int locketDamage = (int)((float)damage * 0.07f);
					int p = Projectile.NewProjectile(LocketSource, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, type, locketDamage, knockBack * 0.5f, base.Player.whoAmI);
					if (p.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[p].DamageType = DamageClass.Generic;
						Main.projectile[p].Calamity().LocketClone = true;
					}
					if (item.type == ModContent.ItemType<TheFinalDawn>())
					{
						Main.projectile[p].ai[1] = 1f;
					}
					if (item.type == ModContent.ItemType<TheAtomSplitter>())
					{
						Main.projectile[p].ai[0] = -1f;
					}
				}
				if (StealthStrikeAvailable())
				{
					int knifeCount = 12;
					int knifeDamage = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(55f);
					float angleStep = (float)Math.PI * 2f / (float)knifeCount;
					float speed = 14f;
					Vector2 velocity2 = default(Vector2);
					for (int i = 0; i < knifeCount; i++)
					{
						((Vector2)(ref velocity2))._002Ector(0f, speed);
						velocity2 = velocity2.RotatedBy(angleStep * (float)i);
						int knifeCol = Main.rand.Next(0, 2);
						int knife = Projectile.NewProjectile(LocketSource, base.Player.Center, velocity2, ModContent.ProjectileType<VeneratedKnife>(), knifeDamage, 0f, base.Player.whoAmI, knifeCol);
						if (knife.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[knife].DamageType = DamageClass.Generic;
						}
					}
				}
			}
		}
		return true;
	}

	public override void FrameEffects()
	{
		if (base.Player.isDisplayDollOrInanimate)
		{
			if (base.Player.armor[1].type == ModContent.ItemType<AuricTeslaBodyArmor>())
			{
				base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "AuricTeslaBodyArmor", EquipType.Body);
			}
			else if (base.Player.armor[1].type == ModContent.ItemType<DaedalusBreastplate>())
			{
				base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "DaedalusBreastplate", EquipType.Body);
			}
			else if (base.Player.armor[1].type == ModContent.ItemType<EmpyreanCloak>())
			{
				base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "EmpyreanCloak", EquipType.Body);
			}
			else if (base.Player.armor[1].type == ModContent.ItemType<SnowRuffianChestplate>())
			{
				base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "SnowRuffianChestplate", EquipType.Body);
			}
			else if (base.Player.armor[1].type == ModContent.ItemType<VictideBreastplate>())
			{
				base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "VictideBreastplate", EquipType.Body);
			}
			if (base.Player.armor[2].type == ModContent.ItemType<VictideGreaves>())
			{
				base.Player.legs = EquipLoader.GetEquipSlot(base.Mod, "VictideGreaves", EquipType.Legs);
			}
			if (base.Player.armor[0].type == ModContent.ItemType<SnowRuffianMask>() && base.Player.armor[1].type == ModContent.ItemType<SnowRuffianChestplate>() && base.Player.armor[2].type == ModContent.ItemType<SnowRuffianGreaves>())
			{
				snowRuffianSet = true;
			}
		}
		CooldownInstance cd;
		if (base.Player.Calamity().andromedaState == AndromedaPlayerState.LargeRobot || base.Player.Calamity().andromedaState == AndromedaPlayerState.SpecialAttack)
		{
			base.Player.head = EquipLoader.GetEquipSlot(base.Mod, "HeadlessEquipTexture", EquipType.Head);
		}
		else if (AresExoskeleton.ArmExists(base.Player))
		{
			base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "AresExoskeleton", EquipType.Body);
		}
		else if (meldTransformationPower || meldTransformationForce)
		{
			base.Player.legs = EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Legs);
			base.Player.body = EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Body);
			base.Player.neck = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Neck);
			base.Player.head = EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Head);
			base.Player.face = -1;
		}
		else if ((omegaBlueTransformationPower || omegaBlueTransformationForce) && cooldowns.TryGetValue(OmegaBlue.ID, out cd) && cd.timeLeft > 1500)
		{
			base.Player.head = EquipLoader.GetEquipSlot(base.Mod, "OmegaBlueTransformation", EquipType.Head);
		}
		if (snowRuffianSet)
		{
			base.Player.wings = EquipLoader.GetEquipSlot(base.Mod, "SnowRuffianMask", EquipType.Wings);
			bool falling = ((base.Player.gravDir == -1f) ? (base.Player.velocity.Y < 0.05f) : (base.Player.velocity.Y > 0.05f));
			if (base.Player.controlJump & falling)
			{
				if (!base.Player.mount.Active)
				{
					base.Player.velocity.Y *= SnowRuffianMask.GlideFallSpeedMult;
					base.Player.wingFrame = 3;
				}
				base.Player.noFallDmg = true;
				base.Player.fallStart = (int)(base.Player.position.Y / 16f);
			}
		}
		if (base.Player.body == EquipLoader.GetEquipSlot(base.Mod, "AuricTeslaBodyArmor", EquipType.Body))
		{
			base.Player.back = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "AuricTeslaBodyArmor", EquipType.Back);
		}
		if (base.Player.body == EquipLoader.GetEquipSlot(base.Mod, "SnowRuffianChestplate", EquipType.Body))
		{
			base.Player.back = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "SnowRuffianChestplate", EquipType.Back);
			base.Player.neck = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "SnowRuffianChestplate", EquipType.Neck);
		}
		if (base.Player.body == EquipLoader.GetEquipSlot(base.Mod, "EmpyreanCloak", EquipType.Body) && !meldTransformationPower && !meldTransformationForce)
		{
			base.Player.back = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "EmpyreanCloak", EquipType.Back);
			base.Player.neck = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "EmpyreanCloak", EquipType.Neck);
		}
		if (base.Player.body == EquipLoader.GetEquipSlot(base.Mod, "DaedalusBreastplate", EquipType.Body))
		{
			base.Player.waist = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "DaedalusBreastplate", EquipType.Waist);
		}
		bool victideBreastplateVisible = base.Player.body == EquipLoader.GetEquipSlot(base.Mod, "VictideBreastplate", EquipType.Body);
		if (victideBreastplateVisible || base.Player.legs == EquipLoader.GetEquipSlot(base.Mod, "VictideGreaves", EquipType.Legs))
		{
			base.Player.waist = (sbyte)EquipLoader.GetEquipSlot(base.Mod, "VictideFaulds", EquipType.Waist);
			if (victideBreastplateVisible)
			{
				base.Player.front = -1;
				base.Player.handoff = -1;
				base.Player.handon = -1;
			}
		}
		if (NOU)
		{
			NOULOL();
		}
	}

	private void ForceVariousEffects()
	{
		if (blockAllDashes)
		{
			DisableDashes();
		}
		if (weakPetrification)
		{
			WeakPetrification();
		}
		if (godSlayerDashHotKeyPressed || SpeedBlasterDashStarted)
		{
			base.Player.dashType = 0;
			base.Player.eocHit = -1;
			if (base.Player.eocDash != 0)
			{
				base.Player.eocDash = 0;
			}
		}
		if (((silvaCountdown > 0 && hasSilvaEffect && silvaSet) || (DashID == GodslayerArmorDash.ID && base.Player.dashDelay < 0)) && base.Player.lifeRegen < 0)
		{
			base.Player.lifeRegen = 0;
		}
		if (meteorSet)
		{
			base.Player.spaceGun = false;
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<GiantIbanRobotOfDoom>()] > 0)
		{
			base.Player.yoraiz0rEye = 0;
		}
		int totalMoonlightDyes = base.Player.dye.Count((Item dyeItem) => dyeItem.type == ModContent.ItemType<ProfanedMoonlightDye>());
		if (totalMoonlightDyes > 0)
		{
			int size = 340;
			FluidFieldManager.AdjustSizeRelativeToGraphicsQuality(ref size);
			float scale = MathHelper.Max((float)Main.screenWidth, (float)Main.screenHeight) / (float)size * 0.4f;
			if (ProfanedMoonlightAuroraDrawer == null || ProfanedMoonlightAuroraDrawer.Size != size)
			{
				ProfanedMoonlightAuroraDrawer = FluidFieldManager.CreateField(size, scale, 0.1f, 50f, 0.992f);
			}
			int sourceArea = (int)Math.Ceiling(6f / ProfanedMoonlightAuroraDrawer.Scale) + 1;
			ProfanedMoonlightAuroraDrawer.ShouldUpdate = base.Player.miscCounter % 2 == 0;
			ProfanedMoonlightAuroraDrawer.UpdateAction = delegate
			{
				//IL_0044: Unknown result type (might be due to invalid IL or missing references)
				//IL_005e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0068: Unknown result type (might be due to invalid IL or missing references)
				//IL_006d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0074: Unknown result type (might be due to invalid IL or missing references)
				//IL_0079: Unknown result type (might be due to invalid IL or missing references)
				//IL_007e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0083: Unknown result type (might be due to invalid IL or missing references)
				//IL_0085: Unknown result type (might be due to invalid IL or missing references)
				//IL_0087: Unknown result type (might be due to invalid IL or missing references)
				//IL_0091: Unknown result type (might be due to invalid IL or missing references)
				//IL_0096: Unknown result type (might be due to invalid IL or missing references)
				//IL_009b: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
				//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
				//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
				//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
				//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
				//IL_0110: Unknown result type (might be due to invalid IL or missing references)
				//IL_0112: Unknown result type (might be due to invalid IL or missing references)
				//IL_0114: Unknown result type (might be due to invalid IL or missing references)
				//IL_0119: Unknown result type (might be due to invalid IL or missing references)
				//IL_011b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0120: Unknown result type (might be due to invalid IL or missing references)
				//IL_0125: Unknown result type (might be due to invalid IL or missing references)
				//IL_012f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0134: Unknown result type (might be due to invalid IL or missing references)
				//IL_0136: Unknown result type (might be due to invalid IL or missing references)
				//IL_0138: Unknown result type (might be due to invalid IL or missing references)
				//IL_0156: Unknown result type (might be due to invalid IL or missing references)
				//IL_015d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0179: Unknown result type (might be due to invalid IL or missing references)
				//IL_0180: Unknown result type (might be due to invalid IL or missing references)
				//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
				//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
				int num = 5;
				float num2 = (float)totalMoonlightDyes / 3f;
				float num3 = Main.GlobalTimeWrappedHourly * 0.56f;
				float num4 = Main.GlobalTimeWrappedHourly * 0.32f;
				float num5 = Main.GlobalTimeWrappedHourly * 0.91f;
				Vector2 val = default(Vector2);
				((Vector2)(ref val))._002Ector(0.15f, 1f);
				Vector2 val2 = Vector2.UnitX * base.Player.velocity.X / 9f;
				Vector2 val3 = Main.LocalPlayer.Center - Main.screenPosition;
				Vector2 val4 = val3 - Vector2.UnitY * 15f;
				int num6 = size / 2;
				float density = MathHelper.Clamp(num2, 0f, 1f);
				for (int i = 0; i < num; i++)
				{
					float num7 = (float)Math.PI * 2f * (float)i / (float)num + num3;
					Color color = GetCurrentMoonlightDyeColor(num7) * 0.8f;
					((Color)(ref color)).A = 0;
					Vector2 val5 = (num7 / 3f + num4).ToRotationVector2();
					val5.Y = 0f - Math.Abs(val5.Y);
					val5 = (val5 * val - val2).SafeNormalize(Vector2.UnitY) * 0.07f;
					Vector2 val6 = val4;
					val6.X += (float)Math.Cos(num7 + num5) * 75f;
					int num8 = (int)((val6.X - val3.X) / ProfanedMoonlightAuroraDrawer.Scale);
					int num9 = (int)((val6.Y - val3.Y) / ProfanedMoonlightAuroraDrawer.Scale);
					for (int j = -sourceArea; j <= sourceArea; j++)
					{
						for (int k = -sourceArea; k <= sourceArea; k++)
						{
							ProfanedMoonlightAuroraDrawer.CreateSource(num8 + num6 + j, num9 + num6 + k, density, color, val5);
						}
					}
				}
			};
		}
		if (NOU)
		{
			NOULOL();
		}
	}

	private void DisableDashes()
	{
		base.Player.dashType = 0;
		DashID = string.Empty;
		if (base.Player.dashDelay >= 0 && base.Player.dashDelay < 12)
		{
			base.Player.dashDelay = 12;
		}
		base.Player.eocHit = -1;
		if (base.Player.eocDash != 0)
		{
			base.Player.eocDash = 0;
		}
	}

	private void WeakPetrification()
	{
		weakPetrification = true;
		base.Player.blockExtraJumps = true;
		base.Player.rocketBoots = 0;
		base.Player.wingTimeMax = (int)((double)base.Player.wingTimeMax * 0.5);
	}

	private void NOULOL()
	{
		base.Player.ResetEffects();
		for (int j = 0; j < 1000; j++)
		{
			if (Main.projectile[j].active && Main.projectile[j].owner == base.Player.whoAmI)
			{
				base.Player.ownedProjectileCounts[Main.projectile[j].type]++;
			}
		}
		base.Player.head = -1;
		base.Player.body = -1;
		base.Player.legs = -1;
		base.Player.handon = -1;
		base.Player.handoff = -1;
		base.Player.back = -1;
		base.Player.front = -1;
		base.Player.shoe = -1;
		base.Player.waist = -1;
		base.Player.shield = -1;
		base.Player.neck = -1;
		base.Player.face = -1;
		base.Player.balloon = -1;
		NOU = true;
	}

	public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.whoAmI == Main.myPlayer && base.Player.HeldItem.type == ModContent.ItemType<TheAnomalysNanogun>() && Main.rand.NextBool(20))
		{
			SoundEngine.PlaySound(in IjiDeathSound, base.Player.Center);
		}
	}

	public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			IEntitySource source = nurse.GetSource_FromThis("Calamity_GetFixedBoiNurseExtinctionMeteor");
			if (base.Player.whoAmI == Main.myPlayer)
			{
				int proj = Projectile.NewProjectile(source, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<LeviathanBomb>(), 9999, 10f, base.Player.whoAmI);
				if (Main.projectile[proj].whoAmI.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].timeLeft = 10;
					Main.projectile[proj].scale = 6f;
					Main.projectile[proj].friendly = true;
					Main.projectile[proj].netUpdate = true;
				}
			}
			return false;
		}
		return true;
	}

	public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
	{
		if (price <= 0)
		{
			return;
		}
		if (CalamityWorld.death)
		{
			price *= 2;
		}
		if (areThereAnyDamnBosses)
		{
			price *= 5;
		}
		if (DownedBossSystem.downedExoMechs || DownedBossSystem.downedCalamitas)
		{
			price *= 600;
		}
		else if (DownedBossSystem.downedYharon)
		{
			price *= 500;
		}
		else if (DownedBossSystem.downedDoG)
		{
			price *= 400;
		}
		else if (DownedBossSystem.downedProvidence)
		{
			price *= 300;
		}
		else
		{
			if (!NPC.downedMoonlord)
			{
				return;
			}
			price *= 250;
		}
		int vanillaPriceMult = 1;
		if (NPC.downedGolemBoss)
		{
			vanillaPriceMult = 200;
		}
		else if (NPC.downedPlantBoss)
		{
			vanillaPriceMult = 150;
		}
		else if (NPC.downedMechBossAny)
		{
			vanillaPriceMult = 100;
		}
		else if (Main.hardMode)
		{
			vanillaPriceMult = 60;
		}
		else if (NPC.downedBoss3 || NPC.downedQueenBee)
		{
			vanillaPriceMult = 25;
		}
		else if (NPC.downedBoss2)
		{
			vanillaPriceMult = 10;
		}
		else if (NPC.downedBoss1)
		{
			vanillaPriceMult = 3;
		}
		price /= vanillaPriceMult;
	}

	public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
	{
		if (!removeDebuffs || alcoholPoisonLevel <= 3)
		{
			return;
		}
		List<int[]> Alcohol = new List<int[]>();
		for (int i = 0; i < Player.MaxBuffs; i++)
		{
			int buff = base.Player.buffType[i];
			if (CalamityBuffSets.AlcoholStrength.TryGetValue(buff, out var level))
			{
				Alcohol.Insert(0, new int[2] { i, level });
			}
		}
		int poison = alcoholPoisonLevel;
		do
		{
			int[] relation = Alcohol[0];
			base.Player.DelBuff(relation[0]);
			poison -= relation[1];
			Alcohol.RemoveAt(0);
		}
		while (poison > 3);
	}

	private void ResetRogueStealth()
	{
		stealthDamage = 0f;
		bonusStealthDamage = 0.0;
		rogueStealthMax = 0f;
		stealthGenStandstill = 1f;
		stealthGenMoving = 1f;
		stealthStrikeThisFrame = false;
		stealthStrikeHalfCost = false;
		stealthStrike75Cost = false;
		stealthStrike90Cost = false;
		if (!darkGodSheath && !eclipseMirror)
		{
			stealthAcceleration = 1f;
		}
		if (temporaryStealthTimer > 0)
		{
			temporaryStealthTimer--;
		}
		else
		{
			temporaryStealthMax = 0.1f;
		}
	}

	public void UpdateRogueStealth()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (temporaryStealthTimer > 0)
		{
			if (rogueStealthMax < temporaryStealthMax)
			{
				rogueStealthMax = temporaryStealthMax;
			}
			wearingRogueArmor = true;
		}
		if (!wearingRogueArmor)
		{
			rogueStealth = 0f;
			playRogueStealthSound = false;
			return;
		}
		if (playRogueStealthSound && rogueStealth >= rogueStealthMax && base.Player.whoAmI == Main.myPlayer)
		{
			playRogueStealthSound = false;
			SoundEngine.PlaySound(in RogueStealthSound, base.Player.Center);
		}
		else if (rogueStealth < rogueStealthMax)
		{
			playRogueStealthSound = true;
		}
		float currentStealthGen = UpdateStealthGenStats();
		rogueStealth += rogueStealthMax * (currentStealthGen / 120f);
		if (rogueStealth > rogueStealthMax)
		{
			rogueStealth = rogueStealthMax;
		}
		ProvideStealthStatBonuses();
		Item it = base.Player.HeldItem;
		bool num = it.damage > 0;
		bool hasHitboxes = !it.noMelee || it.shoot > 0;
		bool num2 = it.pick > 0;
		bool isAxe = it.axe > 0;
		bool isHammer = it.hammer > 0;
		bool isPlaced = it.createTile != -1;
		bool isChannelable = it.channel;
		bool hasNonWeaponFunction = num2 | isAxe | isHammer | isPlaced | isChannelable;
		bool playerUsingWeapon = (num & hasHitboxes) && !hasNonWeaponFunction;
		if (it.IsAir || (!it.CountsAsClass<RogueDamageClass>() && GemTechSet && GemTechState.IsRedGemActive) || (it.CountsAsClass<SummonDamageClass>() && forbiddenCirclet))
		{
			playerUsingWeapon = false;
		}
		if (it.type == ModContent.ItemType<MoltenAmputator>())
		{
			playerUsingWeapon = false;
		}
		if (it.type == ModContent.ItemType<DoomsdayDevice>())
		{
			playerUsingWeapon = false;
		}
		bool animationCheck = ((it.useAnimation == it.useTime) ? (base.Player.itemAnimation == base.Player.itemAnimationMax - 1) : (base.Player.itemTime == (int)((float)it.useTime / base.Player.GetAttackSpeed<RogueDamageClass>())));
		if (!stealthStrikeThisFrame & animationCheck & playerUsingWeapon)
		{
			if (StealthStrikeAvailable())
			{
				ConsumeStealthByAttacking();
			}
			else
			{
				rogueStealth = 0f;
			}
		}
	}

	private void ProvideStealthStatBonuses()
	{
		if (wearingRogueArmor && !(rogueStealthMax <= 0f))
		{
			Item it = ((!Main.HoverItem.IsAir) ? Main.HoverItem : base.Player.HeldItem);
			double averagedStealthGen = 0.8 * (double)stealthGenMoving + 0.2 * (double)stealthGenStandstill;
			double x = (double)BalancingConstants.BaseStealthGenTime / averagedStealthGen;
			int realUseTime = Math.Max(it.useTime, it.useAnimation);
			double useTimeFactor = 0.75 + 0.75 * Math.Log((double)realUseTime + 2.0, 4.0);
			double stealthGenFactor = Math.Max(Math.Pow(x, 2.0 / 3.0), 1.5);
			double stealthAddedDamage = (double)rogueStealth * BalancingConstants.UniversalStealthStrikeDamageFactor * useTimeFactor * stealthGenFactor;
			stealthDamage += (float)stealthAddedDamage;
			base.Player.aggro -= (int)(rogueStealth * 300f);
		}
	}

	private float UpdateStealthGenStats()
	{
		int finalDawnProjCount = base.Player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnProjectile>()] + base.Player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnFireSlash>()] + base.Player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnHorizontalSlash>()] + base.Player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnThrow>()] + base.Player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnThrow2>()];
		if (base.Player.itemAnimation > 0 || finalDawnProjCount > 0)
		{
			return 0f;
		}
		if (shadow)
		{
			stealthGenStandstill += ShadowPotion.StealthRegenBoost;
			stealthGenMoving += ShadowPotion.StealthRegenBoost;
		}
		if (eArtifact)
		{
			stealthGenStandstill += 0.15f;
			stealthGenMoving += 0.15f;
		}
		stealthGenStandstill += accStealthGenBoost;
		stealthGenMoving += accStealthGenBoost;
		if (darkGodSheath && eclipseMirror)
		{
			stealthAcceleration += 0.075f;
		}
		else if (eclipseMirror)
		{
			stealthAcceleration += 0.005f;
		}
		else if (darkGodSheath)
		{
			stealthAcceleration += 0.005f;
		}
		stealthAcceleration = MathHelper.Clamp(stealthAcceleration, 1f, 1.5f);
		if (!base.Player.StandingStill(0.1f) || base.Player.mount.Active)
		{
			return stealthGenMoving * BalancingConstants.MovingStealthGenRatio * stealthAcceleration;
		}
		return stealthGenStandstill;
	}

	public bool StealthStrikeAvailable()
	{
		if (rogueStealthMax <= 0f)
		{
			return false;
		}
		float consumptionMult = 1f;
		if (stealthStrikeHalfCost)
		{
			consumptionMult = 0.5f;
		}
		else if (stealthStrike75Cost)
		{
			consumptionMult = 0.75f;
		}
		else if (stealthStrike90Cost)
		{
			consumptionMult = 0.9f;
		}
		return rogueStealth >= rogueStealthMax * consumptionMult;
	}

	public void ConsumeStealthByAttacking()
	{
		stealthStrikeThisFrame = true;
		stealthAcceleration = 1f;
		float lossReductionRatio = (float)flatStealthLossReduction / (rogueStealthMax * 100f);
		float remainingStealth = rogueStealthMax * lossReductionRatio;
		float stealthToLose = rogueStealthMax - remainingStealth;
		if (stealthToLose < 0.01f)
		{
			stealthToLose = 0.01f;
		}
		if (stealthStrikeHalfCost)
		{
			rogueStealth -= 0.5f * stealthToLose;
			if (rogueStealth <= 0f)
			{
				rogueStealth = 0f;
			}
		}
		else if (stealthStrike75Cost)
		{
			rogueStealth -= 0.75f * stealthToLose;
			if (rogueStealth <= 0f)
			{
				rogueStealth = 0f;
			}
		}
		else if (stealthStrike90Cost)
		{
			rogueStealth -= 0.9f * stealthToLose;
			if (rogueStealth <= 0f)
			{
				rogueStealth = 0f;
			}
		}
		else
		{
			rogueStealth = remainingStealth;
		}
	}

	internal void rollBabSpears(int randAmt, bool chaseable)
	{
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = base.Player.GetSource_ItemUse(base.Player.HeldItem);
		if (!((base.Player.whoAmI == Main.myPlayer && !endoCooper && randAmt > 0 && Main.rand.NextBool(randAmt)) & chaseable))
		{
			return;
		}
		int spearsFired = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (spearsFired == 2)
			{
				break;
			}
			if (p.owner == base.Player.whoAmI && p.friendly && p.type == ModContent.ProjectileType<MiniGuardianAttack>())
			{
				int numSpears = (profanedCrystalBuffs ? 12 : 6);
				int dam = (int)((float)p.originalDamage * (profanedCrystalBuffs ? 1f : 0.25f));
				for (int x = 0; x < numSpears; x++)
				{
					float angle = (float)Math.PI * 2f / (float)numSpears * (float)x;
					int proj = Projectile.NewProjectile(source, p.Center, angle.ToRotationVector2().RotatedBy(Math.Atan(-45.0)) * 8f, ModContent.ProjectileType<MiniGuardianSpear>(), dam, 0f, base.Player.whoAmI, pscState);
					Main.projectile[proj].originalDamage = dam;
				}
				spearsFired++;
			}
		}
	}

	public override void OnEnterWorld()
	{
		if (CalamityClientConfig.Instance.StutterFix)
		{
			WorldGen.SectionTileFrameWithCheck(0, 0, Main.maxTilesX, Main.maxTilesY);
		}
		if (Main.netMode == 1)
		{
			EnterWorldSync();
		}
		if (CalamityClientConfig.Instance.SpeedrunTimer)
		{
			SpeedrunTimerSystem.Restart();
		}
		bool wikiStatusMessage = CalamityClientConfig.Instance.WikiStatusMessage;
		bool showVCMMMessage = CalamityClientConfig.Instance.VCMMStatusMessage && !ExternalMods.VCMMAvailable;
		if (wikiStatusMessage | showVCMMMessage)
		{
			startMessageDisplayDelay = Main.rand.Next(CalamityUtils.SecondsToFrames(12), CalamityUtils.SecondsToFrames(20) + 1);
		}
	}

	public float GetAbyssAggro(float range)
	{
		range *= (fishAlert ? 3f : 1f);
		range *= (eidolonSnailPet ? 0.85f : 1f);
		range *= (anechoicCoating ? 0.5f : 1f);
		range *= (anechoicPlating ? 0.5f : 1f);
		range *= (abyssalMirror ? 0.65f : 1f);
		range *= (eclipseMirror ? 0.3f : 1f);
		range *= (reaverExplore ? 0.9f : 1f);
		return range;
	}

	public void SpawnGravistarParticle()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		float height = base.Player.height;
		if (base.Player.gravDir == -1f)
		{
			height = 0f;
		}
		Vector2 relativePosition = base.Player.position + new Vector2((float)(base.Player.width / 14), height);
		Vector2 position2 = base.Player.position + new Vector2((float)(base.Player.width * 13 / 14), height);
		SquareParticle square1 = new SquareParticle(relativePosition, base.Player.velocity * (0.15f + Main.rand.NextFloat(0.1f)), affectedByGravity: false, 15, 1.7f + Main.rand.NextFloat(0.6f), Color.Cyan * 1.5f);
		SquareParticle particle = new SquareParticle(position2, base.Player.velocity * (0.15f + Main.rand.NextFloat(0.1f)), affectedByGravity: false, 15, 1.7f + Main.rand.NextFloat(0.6f), Color.Cyan * 1.5f);
		GeneralParticleHandler.SpawnParticle(square1);
		GeneralParticleHandler.SpawnParticle(particle);
	}

	public override void OnConsumeMana(Item item, int manaConsumed)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = base.Player.Calamity();
		if (!Main.rand.NextBool() || !modPlayer.lifeManaEnchant)
		{
			return;
		}
		if (Main.myPlayer == base.Player.whoAmI)
		{
			base.Player.HealEffect(-5);
			base.Player.statLife -= 5;
			if (base.Player.statLife <= 0)
			{
				base.Player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ManaConversionEnchant").ToNetworkText(base.Player.name)), 1000.0, -1);
			}
		}
		for (int i = 0; i < 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Player.Top + Main.rand.NextVector2Circular((float)base.Player.width * 0.5f, 6f), 267);
			dust.color = Color.Red;
			dust.velocity = -Vector2.UnitY.RotatedByRandom(0.47999998927116394) * Main.rand.NextFloat(3f, 4.4f);
			dust.scale = Main.rand.NextFloat(1.5f, 1.72f);
			dust.fadeIn = 0.7f;
			dust.noGravity = true;
		}
	}

	public override void SetControls()
	{
		pressedRight = base.Player.controlRight;
		pressedLeft = base.Player.controlLeft;
		pressedUp = base.Player.controlUp;
		pressedDown = base.Player.controlDown;
		if (ShouldHideControls)
		{
			base.Player.controlLeft = false;
			base.Player.controlUp = false;
			base.Player.controlDown = false;
			base.Player.controlRight = false;
		}
		ShouldHideControls = false;
	}

	public bool HandleDashDodges()
	{
		bool playerDashing = base.Player.pulley || (base.Player.grappling[0] == -1 && !base.Player.tongued);
		if (playerDashing && DashID == GodslayerArmorDash.ID && base.Player.dashDelay < 0)
		{
			GodSlayerDodge();
			return true;
		}
		if (playerDashing && DashID == CounterScarfDash.ID && base.Player.dashDelay < 0 && dodgeScarf && !base.Player.HasCooldown(ScarfCooldown.ID))
		{
			CounterScarfDodge();
			return true;
		}
		return false;
	}

	public void ModDashMovement()
	{
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (HasCustomDash && base.Player.dashDelay < 0 && UsedDash.dashTime < UsedDash.dashStartup)
		{
			UsedDash.DashStartupEffects(base.Player);
			UsedDash.dashTime++;
			return;
		}
		if (HasCustomDash && UsedDash.dashStartup > 0 && base.Player.dashDelay < 0 && UsedDash.dashTime == UsedDash.dashStartup && DoADash(UsedDash.CalculateDashSpeed(base.Player), forceDash: true))
		{
			UsedDash.OnDashEffects(base.Player);
		}
		ProjectileSource_PlayerDashHit source = new ProjectileSource_PlayerDashHit(base.Player);
		if (HasCustomDash && base.Player.dashDelay < 0)
		{
			Rectangle hitArea = default(Rectangle);
			((Rectangle)(ref hitArea))._002Ector((int)((double)base.Player.position.X + (double)base.Player.velocity.X * 0.5 - 4.0), (int)((double)base.Player.position.Y + (double)base.Player.velocity.Y * 0.5 - 4.0), base.Player.width + 8, base.Player.height + 8);
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if ((base.Player.dontHurtCritters && NPCID.Sets.CountsAsCritter[n.type]) || n.dontTakeDamage || n.friendly || n.Calamity().dashImmunityTime[base.Player.whoAmI] > 0 || !((Rectangle)(ref hitArea)).Intersects(n.getRect()) || (!n.noTileCollide && !base.Player.CanHit(n)))
				{
					continue;
				}
				DashHitContext hitContext = default(DashHitContext);
				UsedDash.OnHitEffects(base.Player, n, source, ref hitContext);
				if (hitContext.damageClass != null && hitContext.BaseDamage > 0)
				{
					int dashDamage = (int)base.Player.GetTotalDamage(hitContext.damageClass).ApplyTo(hitContext.BaseDamage);
					Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), n.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), dashDamage, 0f, base.Player.whoAmI, n.whoAmI).DamageType = hitContext.damageClass;
					if (n.Calamity().dashImmunityTime[base.Player.whoAmI] < 12)
					{
						n.Calamity().dashImmunityTime[base.Player.whoAmI] = 12;
					}
					base.Player.GiveImmuneTimeForCollisionAttack(hitContext.PlayerImmunityFrames);
				}
			}
			UsedDash.dashTime++;
		}
		if (base.Player.dashDelay > 0)
		{
			VerticalOmnidashTimer = 0;
			LastUsedDashID = string.Empty;
		}
		else if (base.Player.dashDelay < 0)
		{
			int dashDelayToApply = 30;
			if (UsedDash.CollisionType == DashCollisionType.ShieldSlam)
			{
				dashDelayToApply = 30;
			}
			else if (UsedDash.CollisionType == DashCollisionType.ShieldBonk)
			{
				dashDelayToApply = 30;
			}
			if (DashID == DeepDiverDash.ID || (evasionScarf && DashID == CounterScarfDash.ID))
			{
				dashDelayToApply = (int)((float)dashDelayToApply * 0.75f);
			}
			if (DashID == StatisNinjaBeltDash.ID || DashID == StatisVoidSashDash.ID || base.Player.dashType == 1)
			{
				dashDelayToApply = 30;
			}
			float dashSpeed = 12f;
			float dashSpeedDecelerationFactor = 0.985f;
			float runSpeed = Math.Max(base.Player.accRunSpeed, base.Player.maxRunSpeed);
			float runSpeedDecelerationFactor = 0.94f;
			LastUsedDashID = DashID;
			UsedDash.MidDashEffects(base.Player, ref dashSpeed, ref dashSpeedDecelerationFactor, ref runSpeedDecelerationFactor);
			int VerticalOmnidashCap = ((DashID == GodslayerArmorDash.ID) ? 75 : 25);
			if (UsedDash.IsOmnidirectional && VerticalOmnidashTimer < VerticalOmnidashCap)
			{
				VerticalOmnidashTimer++;
				if (VerticalOmnidashTimer >= VerticalOmnidashCap)
				{
					base.Player.dashDelay = dashDelayToApply;
					Player player = base.Player;
					player.velocity *= 0.2f;
				}
			}
			if (!HasCustomDash)
			{
				return;
			}
			base.Player.vortexStealthActive = false;
			if (base.Player.velocity.X != 0f)
			{
				base.Player.ChangeDir(Math.Sign(base.Player.velocity.X));
			}
			if (UsedDash.IsOmnidirectional)
			{
				if (DashID == GodslayerArmorDash.ID)
				{
					return;
				}
				if (((Vector2)(ref base.Player.velocity)).Length() > dashSpeed)
				{
					Player player2 = base.Player;
					player2.velocity *= dashSpeedDecelerationFactor;
					return;
				}
				if (((Vector2)(ref base.Player.velocity)).Length() > runSpeed)
				{
					Player player3 = base.Player;
					player3.velocity *= runSpeedDecelerationFactor;
					return;
				}
			}
			else
			{
				if (base.Player.velocity.X > dashSpeed || base.Player.velocity.X < 0f - dashSpeed)
				{
					base.Player.velocity.X *= dashSpeedDecelerationFactor;
					return;
				}
				if (base.Player.velocity.X > runSpeed || base.Player.velocity.X < 0f - runSpeed)
				{
					base.Player.velocity.X *= runSpeedDecelerationFactor;
					return;
				}
			}
			if (!HasCustomDash || UsedDash.dashStartup <= 0 || base.Player.dashDelay >= 0 || UsedDash.dashTime > UsedDash.dashStartup + 10)
			{
				base.Player.dashDelay = dashDelayToApply;
			}
			if (UsedDash.IsOmnidirectional)
			{
				if (((Vector2)(ref base.Player.velocity)).Length() < 0f)
				{
					((Vector2)(ref base.Player.velocity)).Normalize();
					Player player4 = base.Player;
					player4.velocity *= 0f - runSpeed;
				}
				else if (((Vector2)(ref base.Player.velocity)).Length() > 0f)
				{
					((Vector2)(ref base.Player.velocity)).Normalize();
					Player player5 = base.Player;
					player5.velocity *= runSpeed;
				}
			}
			else if (base.Player.velocity.X < 0f)
			{
				base.Player.velocity.X = 0f - runSpeed;
			}
			else if (base.Player.velocity.X > 0f)
			{
				base.Player.velocity.X = runSpeed;
			}
		}
		else
		{
			if (!HasCustomDash || base.Player.mount.Active)
			{
				return;
			}
			UsedDash.dashTime = 0;
			if (DoADash(UsedDash.CalculateDashSpeed(base.Player)))
			{
				if (UsedDash.dashStartup <= 0)
				{
					UsedDash.OnDashEffects(base.Player);
				}
				else
				{
					UsedDash.OnDashStartupEffects(base.Player);
				}
			}
		}
	}

	public bool HandleHorizontalDash(out DashDirection direction, bool forceDash = false)
	{
		direction = DashDirection.Directionless;
		bool dashWasExecuted = false;
		bool manualHotkeyBound = (CalamityKeybinds.DashHotkey.GetAssignedKeysOrEmpty()?.Count ?? 0) > 0;
		bool pressedManualHotkey = manualHotkeyBound && (CalamityKeybinds.DashHotkey.JustPressed | forceDash);
		int dashDirectionToUse = 0;
		if (pressedManualHotkey)
		{
			dashDirectionToUse = ((base.Player.controlRight && !base.Player.controlLeft) ? 1 : ((base.Player.controlLeft && !base.Player.controlRight) ? (-1) : ((!(MathF.Abs(base.Player.velocity.X) <= 0.01f)) ? ((base.Player.velocity.X > 0f) ? 1 : (-1)) : base.Player.direction)));
		}
		else if (!manualHotkeyBound)
		{
			bool vanillaLeftDashInput = !manualHotkeyBound && base.Player.controlLeft && base.Player.releaseLeft;
			dashDirectionToUse = ((!manualHotkeyBound && base.Player.controlRight && base.Player.releaseRight) ? 1 : (vanillaLeftDashInput ? (-1) : 0));
		}
		switch (dashDirectionToUse)
		{
		case 1:
			if ((dashTimeMod > 0) | pressedManualHotkey)
			{
				direction = DashDirection.Right;
				dashWasExecuted = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = 15;
			}
			break;
		case -1:
			if ((dashTimeMod < 0) | pressedManualHotkey)
			{
				direction = DashDirection.Left;
				dashWasExecuted = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = -15;
			}
			break;
		}
		return dashWasExecuted;
	}

	public bool HandleOmnidirectionalDash(out DashDirection direction)
	{
		direction = DashDirection.Directionless;
		bool justDashed = false;
		if (base.Player.controlUp && base.Player.controlLeft)
		{
			if (dashTimeMod < 0)
			{
				direction = DashDirection.UpLeft;
				justDashed = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = -15;
			}
		}
		else if (base.Player.controlUp && base.Player.controlRight)
		{
			if (dashTimeMod > 0)
			{
				direction = DashDirection.UpRight;
				justDashed = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = 15;
			}
		}
		else if (base.Player.controlDown && base.Player.controlLeft)
		{
			if (dashTimeMod < 0)
			{
				direction = DashDirection.DownLeft;
				justDashed = true;
				dashTimeMod = 0;
				base.Player.maxFallSpeed = 50f;
			}
			else
			{
				dashTimeMod = -15;
			}
		}
		else if (base.Player.controlDown && base.Player.controlRight)
		{
			if (dashTimeMod > 0)
			{
				direction = DashDirection.DownRight;
				justDashed = true;
				dashTimeMod = 0;
				base.Player.maxFallSpeed = 50f;
			}
			else
			{
				dashTimeMod = 15;
			}
		}
		else if (base.Player.controlUp)
		{
			if (dashTimeMod < 0)
			{
				direction = DashDirection.Up;
				justDashed = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = -15;
			}
		}
		else if (base.Player.controlDown)
		{
			if (dashTimeMod > 0)
			{
				direction = DashDirection.Down;
				justDashed = true;
				dashTimeMod = 0;
				base.Player.maxFallSpeed = 50f;
			}
			else
			{
				dashTimeMod = 15;
			}
		}
		else if (base.Player.controlLeft)
		{
			if (dashTimeMod < 0)
			{
				direction = DashDirection.Left;
				justDashed = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = -15;
			}
		}
		else if (base.Player.controlRight)
		{
			if (dashTimeMod > 0)
			{
				direction = DashDirection.Right;
				justDashed = true;
				dashTimeMod = 0;
			}
			else
			{
				dashTimeMod = 15;
			}
		}
		return justDashed;
	}

	public bool HandleGodSlayerDash(out DashDirection direction)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		bool justDashed = false;
		direction = DashDirection.Directionless;
		Vector2 dashVel = Main.MouseWorld - base.Player.Center;
		dashVel = dashVel.SafeNormalize(Vector2.UnitX) * UsedDash.CalculateDashSpeed(base.Player);
		base.Player.velocity = dashVel;
		if (dashTimeMod > 0)
		{
			justDashed = true;
			dashTimeMod = 0;
		}
		else
		{
			dashTimeMod = 15;
		}
		return justDashed;
	}

	public bool DoADash(float dashSpeed, bool forceDash = false)
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		bool justDashed = forceDash;
		bool omnidirectionalDash = UsedDash?.IsOmnidirectional ?? false;
		if (dashTimeMod != 0)
		{
			dashTimeMod -= (dashTimeMod > 0).ToDirectionInt();
		}
		justDashed = ((DashID == GodslayerArmorDash.ID) ? HandleGodSlayerDash(out var direction) : ((!omnidirectionalDash) ? HandleHorizontalDash(out direction, forceDash) : HandleOmnidirectionalDash(out direction)));
		if (justDashed && !forceDash && UsedDash.dashStartup > 0)
		{
			base.Player.timeSinceLastDashStarted = 0;
			base.Player.dashDelay = -1;
			return justDashed;
		}
		if (justDashed)
		{
			int totalDirections = 8;
			Vector2[] possibleVelocities = (Vector2[])(object)new Vector2[totalDirections];
			for (int i = 0; i < totalDirections; i++)
			{
				possibleVelocities[i] = -Vector2.UnitY.RotatedBy((float)Math.PI * 2f * (float)i / (float)totalDirections) * dashSpeed;
			}
			switch (direction)
			{
			case DashDirection.UpLeft:
				base.Player.velocity = possibleVelocities[7];
				break;
			case DashDirection.DownLeft:
				base.Player.velocity = possibleVelocities[5];
				break;
			case DashDirection.Up:
				base.Player.velocity = possibleVelocities[0];
				break;
			case DashDirection.Left:
				base.Player.velocity = (Vector2)(omnidirectionalDash ? possibleVelocities[6] : new Vector2(possibleVelocities[6].X, base.Player.velocity.Y));
				break;
			case DashDirection.Right:
				base.Player.velocity = (Vector2)(omnidirectionalDash ? possibleVelocities[2] : new Vector2(possibleVelocities[2].X, base.Player.velocity.Y));
				break;
			case DashDirection.Down:
				base.Player.velocity = possibleVelocities[4];
				break;
			case DashDirection.DownRight:
				base.Player.velocity = possibleVelocities[3];
				break;
			case DashDirection.UpRight:
				base.Player.velocity = possibleVelocities[1];
				break;
			}
			Point upwardTilePoint = (base.Player.Center + new Vector2(MathHelper.Clamp((float)direction, -1f, 1f) * (float)base.Player.width / 2f + 2f, base.Player.gravDir * (float)(-base.Player.height) / 2f + base.Player.gravDir * 2f)).ToTileCoordinates();
			Point aheadTilePoint = (base.Player.Center + new Vector2(MathHelper.Clamp((float)direction, -1f, 1f) * (float)base.Player.width / 2f + 2f, 0f)).ToTileCoordinates();
			if (WorldGen.SolidOrSlopedTile(upwardTilePoint.X, upwardTilePoint.Y) || WorldGen.SolidOrSlopedTile(aheadTilePoint.X, aheadTilePoint.Y))
			{
				base.Player.velocity.X /= 2f;
			}
			base.Player.timeSinceLastDashStarted = 0;
			base.Player.dashDelay = -1;
		}
		return justDashed;
	}

	public void ModHorizontalMovement()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.mount.Active && base.Player.mount.Type == ModContent.MountType<ExoTank>() && Math.Abs(base.Player.velocity.X) > base.Player.mount.RunSpeed)
		{
			Rectangle damageHitbox = base.Player.getRect();
			if (base.Player.direction == 1)
			{
				((Rectangle)(ref damageHitbox)).Offset(base.Player.width - 1, 0);
			}
			damageHitbox.Width = 2;
			((Rectangle)(ref damageHitbox)).Inflate(6, 12);
			float damage = base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(ExoTank.DashDamage);
			float knockback = 10f;
			int NPCImmuneTime = 30;
			int playerImmuneTime = 6;
			DoMountDashDamage(damageHitbox, damage, knockback, NPCImmuneTime, playerImmuneTime);
		}
		if (base.Player.mount.Active && base.Player.mount.Type == ModContent.MountType<RimehoundMount>() && Math.Abs(base.Player.velocity.X) > base.Player.mount.RunSpeed / 2f)
		{
			Rectangle damageHitbox2 = base.Player.getRect();
			if (base.Player.direction == 1)
			{
				((Rectangle)(ref damageHitbox2)).Offset(base.Player.width - 1, 0);
			}
			damageHitbox2.Width = 2;
			((Rectangle)(ref damageHitbox2)).Inflate(6, 12);
			float damage2 = base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(50f);
			float knockback2 = 8f;
			int NPCImmuneTime2 = 30;
			int playerImmuneTime2 = 6;
			DoMountDashDamage(damageHitbox2, damage2, knockback2, NPCImmuneTime2, playerImmuneTime2);
		}
		if (base.Player.mount.Active && base.Player.mount.Type == ModContent.MountType<OnyxExcavator>() && Math.Abs(base.Player.velocity.X) > base.Player.mount.RunSpeed / 2f)
		{
			Rectangle damageHitbox3 = base.Player.getRect();
			if (base.Player.direction == 1)
			{
				((Rectangle)(ref damageHitbox3)).Offset(base.Player.width - 1, 0);
			}
			damageHitbox3.Width = 2;
			((Rectangle)(ref damageHitbox3)).Inflate(6, 12);
			float damage3 = base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(25f);
			float knockback3 = 5f;
			int NPCImmuneTime3 = 30;
			int playerImmuneTime3 = 6;
			DoMountDashDamage(damageHitbox3, damage3, knockback3, NPCImmuneTime3, playerImmuneTime3);
		}
	}

	public int DoMountDashDamage(Rectangle myRect, float Damage, float Knockback, int NPCImmuneTime, int PlayerImmuneTime)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		int totalHurtNPCs = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		Vector2 hitVelocity = default(Vector2);
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if ((base.Player.dontHurtCritters && NPCID.Sets.CountsAsCritter[n.type]) || n.dontTakeDamage || n.friendly || n.Calamity().dashImmunityTime[base.Player.whoAmI] > 0)
			{
				continue;
			}
			Rectangle npcHitbox = n.getRect();
			if (((Rectangle)(ref myRect)).Intersects(npcHitbox) && (n.noTileCollide || Collision.CanHit(base.Player.position, base.Player.width, base.Player.height, n.position, n.width, n.height)))
			{
				int hitDirection = Math.Sign(base.Player.velocity.X);
				if (hitDirection == 0)
				{
					hitDirection = base.Player.direction;
				}
				((Vector2)(ref hitVelocity))._002Ector((float)(2 * hitDirection), 0f);
				if (base.Player.whoAmI == Main.myPlayer)
				{
					Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), n.Center, hitVelocity, ModContent.ProjectileType<DirectStrike>(), (int)Damage, Knockback, base.Player.whoAmI, n.whoAmI).DamageType = DamageClass.Summon;
				}
				n.Calamity().dashImmunityTime[base.Player.whoAmI] = NPCImmuneTime;
				base.Player.GiveUniversalIFrames(PlayerImmuneTime);
				totalHurtNPCs++;
				break;
			}
		}
		return totalHurtNPCs;
	}

	public override void HideDrawLayers(PlayerDrawSet drawInfo)
	{
		if (base.Player == null)
		{
			return;
		}
		if (LegOverrideList.Includes(base.Player.legs))
		{
			PlayerDrawLayers.Shoes.Hide();
		}
		if (drawInfo.drawPlayer.Calamity().andromedaState != AndromedaPlayerState.Inactive)
		{
			foreach (PlayerDrawLayer layer in PlayerDrawLayerLoader.Layers)
			{
				if (layer != PlayerDrawLayers.BackAcc)
				{
					layer.Hide();
				}
			}
		}
		if (base.Player.HeldItem.ModItem is IHideFrontArm amputator && amputator.ShouldHideArm(base.Player))
		{
			PlayerDrawLayers.ArmOverItem.Hide();
		}
	}

	public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1033: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1073: Unknown result type (might be due to invalid IL or missing references)
		//IL_1078: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1096: Unknown result type (might be due to invalid IL or missing references)
		//IL_109b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_123f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Unknown result type (might be due to invalid IL or missing references)
		//IL_125e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1268: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e42: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1416: Unknown result type (might be due to invalid IL or missing references)
		//IL_141b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_142a: Unknown result type (might be due to invalid IL or missing references)
		//IL_142f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0845: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_1330: Unknown result type (might be due to invalid IL or missing references)
		//IL_133a: Unknown result type (might be due to invalid IL or missing references)
		//IL_133f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1390: Unknown result type (might be due to invalid IL or missing references)
		//IL_1396: Unknown result type (might be due to invalid IL or missing references)
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_147c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1481: Unknown result type (might be due to invalid IL or missing references)
		//IL_1486: Unknown result type (might be due to invalid IL or missing references)
		//IL_148b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1501: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_150b: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_1910: Unknown result type (might be due to invalid IL or missing references)
		//IL_1942: Unknown result type (might be due to invalid IL or missing references)
		//IL_194d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1952: Unknown result type (might be due to invalid IL or missing references)
		//IL_1957: Unknown result type (might be due to invalid IL or missing references)
		//IL_195c: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1def: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e62: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2014: Unknown result type (might be due to invalid IL or missing references)
		//IL_201e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2023: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.Calamity().andromedaState != AndromedaPlayerState.Inactive)
		{
			AndromedaMechLayer.DrawTheStupidFuckingRobot(ref drawInfo);
		}
		CalamityPlayer calamityPlayer = base.Player.Calamity();
		if (Starshield > 0 && drawInfo.shadow == 0f)
		{
			Color.Lerp(Color.DeepSkyBlue, Color.LightSkyBlue, (float)StratusStarburst / (float)MaxStratusStarburst);
			float opacity = MathHelper.Min(MathHelper.Min((float)Starshield / 30f, 1f), (float)(CalamityUtils.MinutesToFrames(10) - Starshield) / 30f);
			float size = 80f + 32f * ((float)StratusStarburst / (float)MaxStratusStarburst);
			Vector2 drawPosition = base.Player.Center + new Vector2(0f, base.Player.gfxOffY) - Main.screenPosition;
			PixelationManager.AddPixelatedDrawer(delegate(Matrix matrix)
			{
				//IL_000d: Unknown result type (might be due to invalid IL or missing references)
				//IL_008c: Unknown result type (might be due to invalid IL or missing references)
				//IL_009a: Unknown result type (might be due to invalid IL or missing references)
				//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
				//IL_017c: Unknown result type (might be due to invalid IL or missing references)
				//IL_018a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0195: Unknown result type (might be due to invalid IL or missing references)
				//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
				//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
				//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
				Texture2D transparentBloomTex = StratusBlackHole.GetTransparentBloomTex();
				Main.spriteBatch.EnterShaderRegion(null, null, matrix);
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.5f);
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.2f);
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoiseHighContrast", (AssetRequestMode)2));
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
				Main.EntitySpriteDraw(transparentBloomTex, drawPosition, null, Color.DarkSlateBlue * opacity, 0f, transparentBloomTex.Size() / 2f, size * 1.5f * opacity / (float)transparentBloomTex.Width, (SpriteEffects)0);
				Main.spriteBatch.EnterShaderRegion(null, null, matrix);
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.25f);
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.1f);
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons", (AssetRequestMode)2));
				GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
				transparentBloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/HighResFoggyCircleHardEdge", (AssetRequestMode)2).Value;
				Main.EntitySpriteDraw(transparentBloomTex, drawPosition, null, Color.SkyBlue * opacity, 0f, transparentBloomTex.Size() / 2f, size * opacity / (float)transparentBloomTex.Width, (SpriteEffects)0);
				Main.spriteBatch.ExitShaderRegion(matrix);
			}, GeneralDrawLayer.AfterProjectiles);
		}
		DevourerofGodsHead DoG = null;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC item = enumerator.Current;
			if (item.type == ModContent.NPCType<DevourerofGodsHead>())
			{
				DoG = item.ModNPC<DevourerofGodsHead>();
				break;
			}
		}
		if (DoG != null && Main.mapStyle != 2 && !Main.hideUI)
		{
			int drawCount = ((!calamityPlayer.trippy) ? 1 : 4);
			for (int i = 0; i < drawCount; i++)
			{
				Vector2 rift = DoG.GetRiftLocation();
				bool drawingRift = rift != Vector2.Zero && !Main.zenithWorld;
				Vector2 val = (drawingRift ? rift : DoG.NPC.Center);
				float diffX = val.X - base.Player.Center.X;
				float diffY = val.Y - base.Player.Center.Y;
				if (calamityPlayer.trippy)
				{
					switch (i)
					{
					case 0:
						diffX = 0f - Math.Abs(diffX);
						diffY = 0f - Math.Abs(diffY);
						break;
					case 1:
						diffX = Math.Abs(diffX);
						diffY = 0f - Math.Abs(diffY);
						break;
					case 2:
						diffX = Math.Abs(diffX);
						diffY = Math.Abs(diffY);
						break;
					case 3:
						diffX = 0f - Math.Abs(diffX);
						diffY = Math.Abs(diffY);
						break;
					}
				}
				Vector2 virtualTargetPos = base.Player.Center + new Vector2(diffX, diffY);
				float dist = base.Player.Distance(virtualTargetPos);
				Vector2 directionToTarget = base.Player.DirectionTo(virtualTargetPos);
				if (drawingRift)
				{
					Texture2D tex = ModContent.Request<Texture2D>("Terraria/Images/Extra_173", (AssetRequestMode)2).Value;
					float opacity2 = 0.9f * Math.Clamp(MathHelper.Lerp(0f, 1f, (dist - 300f) / 600f), 0f, 1f);
					Color drawColor = (calamityPlayer.trippy ? Main.DiscoColor : (Color.White * 0.9f));
					Main.spriteBatch.Draw(tex, base.Player.Center + directionToTarget * 196f * Math.Min(dist / 2400f, 2f) - Main.screenPosition, (Rectangle?)null, drawColor * opacity2, 0f, tex.Size() / 2f, 0.9f, (SpriteEffects)1, 0f);
					continue;
				}
				float dis = base.Player.Distance(virtualTargetPos);
				if ((!(DoG.NPC.ai[3] < 3f) && DoG.Phase2Started) || !(DoG.NPC.Opacity > 0.5f) || DoG.Dying || drawInfo.drawPlayer.isDisplayDollOrInanimate)
				{
					continue;
				}
				int headIconIndex = -1;
				DoG.BossHeadSlot(ref headIconIndex);
				if (headIconIndex <= -1)
				{
					continue;
				}
				Texture2D tex2 = TextureAssets.NpcHeadBoss[headIconIndex].Value;
				float baseRotation = DoG.NPC.rotation;
				if (calamityPlayer.trippy)
				{
					Vector2 rotVec = baseRotation.ToRotationVector2();
					switch (i)
					{
					case 0:
						rotVec.X *= -1f;
						rotVec.Y *= -1f;
						break;
					case 1:
						rotVec.Y *= -1f;
						break;
					case 3:
						rotVec.X *= -1f;
						break;
					}
					baseRotation = rotVec.ToRotation();
				}
				float opacity3 = 0.9f * Math.Clamp(MathHelper.Lerp(0f, 1f, (dis - 600f) / 300f), 0f, 1f);
				Color drawColor2 = (Color)(calamityPlayer.trippy ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 229) : (Color.White * 0.9f));
				Main.spriteBatch.Draw(tex2, base.Player.Center + directionToTarget * 196f * Math.Min(dis / 2400f, 2f) - Main.screenPosition, (Rectangle?)null, drawColor2 * opacity3, baseRotation, tex2.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			}
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<ThreadOfEradication>() && !base.Player.ItemTimeIsZero && drawInfo.shadow == 0f)
		{
			Color color = Color.Fuchsia;
			float scale = (1f - (float)(base.Player.itemTime - 7) / 50f) * 0.2f;
			if (base.Player.itemTime < 7)
			{
				scale = (float)base.Player.itemTime / 7f * 0.2f;
			}
			if (base.Player.itemTime > 60)
			{
				scale = (1f - (float)(base.Player.itemTime - 70) / 110f) * 0.5f;
				if (base.Player.itemTime < 70)
				{
					scale = (float)(base.Player.itemTime - 60) / 10f * 0.5f;
				}
				color = Color.Cyan;
			}
			if (CalamityClientConfig.Instance.Photosensitivity)
			{
				color *= 0.2f;
			}
			Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			Texture2D circleTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
			using (Main.spriteBatch.Scope())
			{
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
				Main.spriteBatch.Draw(bloomTex, base.Player.Center + (Vector2.UnitX * (float)base.Player.direction).RotatedBy(base.Player.itemRotation) * (48f + scale * 96f) - Main.screenPosition, (Rectangle?)null, color, 0f, bloomTex.Size() * 0.5f, scale * 2f, (SpriteEffects)0, 0f);
				Main.spriteBatch.Draw(bloomTex, base.Player.Center + (Vector2.UnitX * (float)base.Player.direction).RotatedBy(base.Player.itemRotation) * (48f + scale * 96f) - Main.screenPosition, (Rectangle?)null, color, 0f, bloomTex.Size() * 0.5f, scale * 2f, (SpriteEffects)0, 0f);
				Main.spriteBatch.End();
			}
			for (int i2 = 0; i2 < 5; i2++)
			{
				Main.spriteBatch.Draw(circleTex, base.Player.Center + (Vector2.UnitX * (float)base.Player.direction).RotatedBy(base.Player.itemRotation) * (48f + scale * 96f) - Main.screenPosition, (Rectangle?)null, Color.Black * ((float)(i2 + 1) / 5f) * (CalamityClientConfig.Instance.Photosensitivity ? 0.2f : 1f), 0f, circleTex.Size() * 0.5f, scale * 2.2f * (0.5f + 0.5f * (1f - (float)i2 / 5f)), (SpriteEffects)0, 0f);
			}
		}
		if (calamityPlayer.trippy)
		{
			if (Main.myPlayer == base.Player.whoAmI)
			{
				Rectangle screenArea = default(Rectangle);
				((Rectangle)(ref screenArea))._002Ector((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 50, Main.screenWidth + 1000, Main.screenHeight + 100);
				int dustDrawn = 0;
				float maxShroomDust = Main.maxDustToDraw / 2;
				Color shroomColor = default(Color);
				((Color)(ref shroomColor))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB, Main.DiscoR);
				for (int i3 = 0; i3 < Main.maxDustToDraw; i3++)
				{
					Dust dust = Main.dust[i3];
					if (!dust.active)
					{
						continue;
					}
					Rectangle val2 = new Rectangle((int)dust.position.X, (int)dust.position.Y, 4, 4);
					if (!((Rectangle)(ref val2)).Intersects(screenArea))
					{
						continue;
					}
					dust.color = shroomColor;
					for (int j = 0; j < 4; j++)
					{
						Vector2 dustDrawPosition = dust.position;
						Vector2 val3 = dustDrawPosition + new Vector2(4f);
						float distanceX = Math.Abs(val3.X - base.Player.Center.X);
						float distanceY = Math.Abs(val3.Y - base.Player.Center.Y);
						if (j == 0 || j == 2)
						{
							dustDrawPosition.X = base.Player.Center.X + distanceX;
						}
						else
						{
							dustDrawPosition.X = base.Player.Center.X - distanceX;
						}
						dustDrawPosition.X -= 4f;
						if (j == 0 || j == 1)
						{
							dustDrawPosition.Y = base.Player.Center.Y + distanceY;
						}
						else
						{
							dustDrawPosition.Y = base.Player.Center.Y - distanceY;
						}
						dustDrawPosition.Y -= 4f;
						Main.spriteBatch.Draw(TextureAssets.Dust.Value, dustDrawPosition - Main.screenPosition, (Rectangle?)dust.frame, dust.color, dust.rotation, new Vector2(4f), dust.scale, (SpriteEffects)0, 0f);
						dustDrawn++;
					}
					if ((float)dustDrawn > maxShroomDust)
					{
						break;
					}
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				Color rainbow = default(Color);
				while (enumerator2.MoveNext())
				{
					NPC n = enumerator2.Current;
					((Color)(ref rainbow))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB, Main.DiscoR);
					Color alphaColor = n.GetAlpha(rainbow);
					float RGBMult = 0.99f;
					((Color)(ref alphaColor)).R = (byte)((float)(int)((Color)(ref alphaColor)).R * RGBMult);
					((Color)(ref alphaColor)).G = (byte)((float)(int)((Color)(ref alphaColor)).G * RGBMult);
					((Color)(ref alphaColor)).B = (byte)((float)(int)((Color)(ref alphaColor)).B * RGBMult);
					((Color)(ref alphaColor)).A = (byte)((float)(int)((Color)(ref alphaColor)).A * RGBMult);
					for (int i4 = 0; i4 < 4; i4++)
					{
						Vector2 position = n.position;
						float distanceFromTargetX = Math.Abs(n.Center.X - Main.LocalPlayer.Center.X);
						float distanceFromTargetY = Math.Abs(n.Center.Y - Main.LocalPlayer.Center.Y);
						switch (i4)
						{
						case 0:
							position.X = Main.LocalPlayer.Center.X - distanceFromTargetX;
							position.Y = Main.LocalPlayer.Center.Y - distanceFromTargetY;
							break;
						case 1:
							position.X = Main.LocalPlayer.Center.X + distanceFromTargetX;
							position.Y = Main.LocalPlayer.Center.Y - distanceFromTargetY;
							break;
						case 2:
							position.X = Main.LocalPlayer.Center.X + distanceFromTargetX;
							position.Y = Main.LocalPlayer.Center.Y + distanceFromTargetY;
							break;
						case 3:
							position.X = Main.LocalPlayer.Center.X - distanceFromTargetX;
							position.Y = Main.LocalPlayer.Center.Y + distanceFromTargetY;
							break;
						}
						Vector2 posDiff = n.Center - position;
						Main.instance.DrawNPCDirect(Main.spriteBatch, n, n.behindTiles, Main.screenPosition + posDiff);
					}
				}
				ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
				Color rainbow2 = default(Color);
				while (enumerator3.MoveNext())
				{
					Projectile p = enumerator3.Current;
					_ = TextureAssets.Projectile[p.type].Value;
					((Color)(ref rainbow2))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB, Main.DiscoR);
					Color alphaColor2 = p.GetAlpha(rainbow2);
					float RGBMult2 = 0.99f;
					((Color)(ref alphaColor2)).R = (byte)((float)(int)((Color)(ref alphaColor2)).R * RGBMult2);
					((Color)(ref alphaColor2)).G = (byte)((float)(int)((Color)(ref alphaColor2)).G * RGBMult2);
					((Color)(ref alphaColor2)).B = (byte)((float)(int)((Color)(ref alphaColor2)).B * RGBMult2);
					((Color)(ref alphaColor2)).A = (byte)((float)(int)((Color)(ref alphaColor2)).A * RGBMult2);
					Vector2 storedProjPos = p.Center;
					for (int i5 = 0; i5 < 4; i5++)
					{
						Vector2 position2 = p.position;
						float distanceFromTargetX2 = Math.Abs(p.Center.X - Main.LocalPlayer.Center.X);
						float distanceFromTargetY2 = Math.Abs(p.Center.Y - Main.LocalPlayer.Center.Y);
						switch (i5)
						{
						case 0:
							position2.X = Main.LocalPlayer.Center.X - distanceFromTargetX2;
							position2.Y = Main.LocalPlayer.Center.Y - distanceFromTargetY2;
							break;
						case 1:
							position2.X = Main.LocalPlayer.Center.X + distanceFromTargetX2;
							position2.Y = Main.LocalPlayer.Center.Y - distanceFromTargetY2;
							break;
						case 2:
							position2.X = Main.LocalPlayer.Center.X + distanceFromTargetX2;
							position2.Y = Main.LocalPlayer.Center.Y + distanceFromTargetY2;
							break;
						case 3:
							position2.X = Main.LocalPlayer.Center.X - distanceFromTargetX2;
							position2.Y = Main.LocalPlayer.Center.Y + distanceFromTargetY2;
							break;
						}
						p.Center = position2;
						Main.instance.DrawProjDirect(p);
					}
					p.Center = storedProjPos;
				}
			}
		}
		else if (base.Player.statMana < 0 && base.Player.Calamity().ChaosStone)
		{
			float compactness = (float)base.Player.width * 0.6f;
			if (compactness < 10f)
			{
				compactness = 10f;
			}
			float power = (float)base.Player.height / 100f;
			if (power > 2.75f)
			{
				power = 2.75f;
			}
			Color color2 = Color.Blue;
			if (ManaBurnFireDrawer == null || ManaBurnFireDrawer.LocalTimer >= ManaBurnFireDrawer.SetLifetime)
			{
				ManaBurnFireDrawer = new FireParticleSet(60 - base.Player.statMana / 4, 1, color2 * 1.25f, color2, compactness, power);
			}
			else
			{
				ManaBurnFireDrawer.DrawSet(base.Player.Bottom - Vector2.UnitY * (12f - base.Player.gfxOffY));
			}
		}
		else
		{
			ManaBurnFireDrawer = null;
		}
		if (calamityPlayer.rogueStealth > 0f && calamityPlayer.rogueStealthMax > 0f && base.Player.townNPCs < 3f && CalamityClientConfig.Instance.StealthInvisibility)
		{
			float colorValue = calamityPlayer.rogueStealth / calamityPlayer.rogueStealthMax * 0.9f;
			r *= 1f - colorValue * 0.89f;
			g *= 1f - colorValue;
			b *= 1f - colorValue * 0.89f;
			a *= 1f - colorValue;
			base.Player.armorEffectDrawOutlines = false;
			base.Player.armorEffectDrawShadow = false;
			base.Player.armorEffectDrawShadowSubtle = false;
		}
		Color newColor;
		if (calamityPlayer.tracersDust && drawInfo.shadow == 0f && !base.Player.StandingStill() && !base.Player.mount.Active && Main.rand.NextBool())
		{
			Vector2 position3 = drawInfo.Position - new Vector2(2f);
			int width = base.Player.width + 4;
			int height = base.Player.height + 4;
			float speedX = base.Player.velocity.X * 0.4f;
			float speedY = base.Player.velocity.Y * 0.4f;
			newColor = default(Color);
			Dust dust2 = Dust.NewDustDirect(position3, width, height, 229, speedX, speedY, 100, newColor);
			dust2.noGravity = true;
			dust2.velocity *= 0.5f;
			drawInfo.DustCache.Add(dust2.dustIndex);
		}
		if (calamityPlayer.dsSetBonus && drawInfo.shadow == 0f)
		{
			if (base.Player != null && !base.Player.dead)
			{
				Lighting.AddLight((int)base.Player.Center.X / 16, (int)base.Player.Center.Y / 16, 0.42553192f, 0.004255319f, 1.0638298f);
				if (!base.Player.StandingStill() && !base.Player.mount.Active && Main.rand.NextBool())
				{
					Vector2 position4 = drawInfo.Position - new Vector2(2f);
					int width2 = base.Player.width + 4;
					int height2 = base.Player.height + 4;
					float speedX2 = base.Player.velocity.X * 0.4f;
					float speedY2 = base.Player.velocity.Y * 0.4f;
					newColor = default(Color);
					Dust dust3 = Dust.NewDustDirect(position4, width2, height2, 27, speedX2, speedY2, 100, newColor, 1.5f);
					dust3.noGravity = true;
					dust3.velocity *= 0.5f;
					drawInfo.DustCache.Add(dust3.dustIndex);
				}
			}
		}
		else if (calamityPlayer.auricSet && drawInfo.shadow == 0f && base.Player != null && !base.Player.dead)
		{
			Vector2 center = base.Player.Center;
			newColor = Color.Lerp(Color.Cyan, Color.White, 0.7f);
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
			if (!base.Player.StandingStill() && !base.Player.mount.Active && Main.rand.NextBool())
			{
				Vector2 velocity = -base.Player.velocity.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(1f, 3f);
				GeneralParticleHandler.SpawnParticle(new NanoParticle(drawInfo.Position + new Vector2((float)Main.rand.Next(base.Player.width + 1), (float)Main.rand.Next(base.Player.height + 1)), velocity, (Main.rand.NextBool(3) ? Color.RoyalBlue : Color.Cyan) * 0.9f, Main.rand.NextFloat(0.2f, 0.7f), 9, bigSize: false, emitsLight: true));
			}
		}
		if (calamityPlayer.rageModeActive && drawInfo.shadow == 0f)
		{
			RageMode.DrawEffects(drawInfo);
		}
		if (calamityPlayer.adrenalineModeActive && drawInfo.shadow == 0f)
		{
			AdrenalineMode.DrawEffects(drawInfo);
		}
		if (calamityPlayer.astralInfection && drawInfo.shadow == 0f)
		{
			AstralInfectionDebuff.DrawEffects(drawInfo);
		}
		if (calamityPlayer.auricRebuke && drawInfo.shadow == 0f)
		{
			AuricRebuke.DrawEffects(drawInfo);
		}
		if (calamityPlayer.burningBlood && drawInfo.shadow == 0f)
		{
			BurningBlood.DrawEffects(drawInfo);
		}
		if (calamityPlayer.brimstoneFlames && drawInfo.shadow == 0f)
		{
			bool resistsBrimstoneFlames = abaddon;
			BrimstoneFlames.DrawEffects(drawInfo, resistsBrimstoneFlames);
		}
		if (calamityPlayer.brainRot && drawInfo.shadow == 0f)
		{
			BrainRot.DrawEffects(drawInfo);
		}
		if (calamityPlayer.crushDepth && drawInfo.shadow == 0f)
		{
			CrushDepth.DrawEffects(drawInfo);
		}
		if (calamityPlayer.daybroken && drawInfo.shadow == 0f)
		{
			Daybroken.DrawEffects(drawInfo);
		}
		if (calamityPlayer.demonicFlames && drawInfo.shadow == 0f)
		{
			DemonicFlames.DrawEffects(drawInfo);
		}
		if (calamityPlayer.dragonFire && drawInfo.shadow == 0f)
		{
			Dragonfire.DrawEffects(drawInfo);
		}
		if (calamityPlayer.elementalMix && drawInfo.shadow == 0f)
		{
			ElementalMix.DrawEffects(drawInfo);
		}
		if (calamityPlayer.eutrophication && drawInfo.shadow == 0f)
		{
			Eutrophication.DrawEffects(drawInfo);
		}
		if (calamityPlayer.godSlayerInferno && drawInfo.shadow == 0f)
		{
			GodSlayerInferno.DrawEffects(drawInfo);
		}
		if (calamityPlayer.heavybleeding && drawInfo.shadow == 0f)
		{
			HeavyBleeding.DrawEffects(drawInfo);
		}
		if (drawInfo.shadow == 0f && (calamityPlayer.holyFlames || calamityPlayer.holyInferno || calamityPlayer.banishingFire))
		{
			HolyFlames.DrawEffects(drawInfo);
		}
		if (calamityPlayer.hadopelagicPressure && drawInfo.shadow == 0f)
		{
			HadopelagicPressure.DrawEffects(drawInfo);
		}
		else if (calamityPlayer.icarusFolly && drawInfo.shadow == 0f)
		{
			IcarusFolly.DrawEffects(drawInfo);
		}
		if (calamityPlayer.laceration && drawInfo.shadow == 0f)
		{
			Laceration.DrawEffects(drawInfo);
		}
		if (calamityPlayer.miracleBlight && drawInfo.shadow == 0f)
		{
			MiracleBlight.DrawEffects(drawInfo);
		}
		if (calamityPlayer.mushy && drawInfo.shadow == 0f)
		{
			Mushy.DrawEffects(drawInfo);
		}
		if (calamityPlayer.nightwither && drawInfo.shadow == 0f)
		{
			Nightwither.DrawEffects(drawInfo);
		}
		if (calamityPlayer.plague && drawInfo.shadow == 0f)
		{
			Plague.DrawEffects(drawInfo);
		}
		if (calamityPlayer.riptide && drawInfo.shadow == 0f)
		{
			RiptideDebuff.DrawEffects(drawInfo);
		}
		if (calamityPlayer.shadowflame && drawInfo.shadow == 0f)
		{
			Shadowflame.DrawEffects(drawInfo);
		}
		if (calamityPlayer.staticDischarge && drawInfo.shadow == 0f)
		{
			StaticDischarge.DrawEffects(drawInfo);
		}
		if (calamityPlayer.sulphurPoison && drawInfo.shadow == 0f)
		{
			SulphuricPoisoning.DrawEffects(drawInfo);
		}
		if (calamityPlayer.tRegen && drawInfo.shadow == 0f)
		{
			TarraLifeRegen.DrawEffects(drawInfo);
		}
		if (calamityPlayer.trueVHex && drawInfo.shadow == 0f)
		{
			TrueVulnerabilityHex.DrawEffects(drawInfo);
		}
		if (calamityPlayer.vaporfied && drawInfo.shadow == 0f)
		{
			Vaporfied.DrawEffects(drawInfo);
		}
		if (calamityPlayer.vermillionFlux && drawInfo.shadow == 0f)
		{
			VermillionFlux.DrawEffects(drawInfo);
		}
		if (calamityPlayer.voidfrost && drawInfo.shadow == 0f)
		{
			Voidfrost.DrawEffects(drawInfo);
		}
		if (calamityPlayer.vHex && drawInfo.shadow == 0f)
		{
			VulnerabilityHex.DrawEffects(drawInfo);
		}
		if (calamityPlayer.PinkJellyRegen && drawInfo.shadow == 0f && Main.rand.NextBool(24))
		{
			GeneralParticleHandler.SpawnParticle(new HealingPlus(base.Player.Center, Main.rand.NextFloat(0.5f, 1.2f), new Vector2(0f, Main.rand.NextFloat(-2f, -3.5f)) + base.Player.velocity, Color.HotPink, Color.LightPink, Main.rand.Next(10, 15)));
		}
		if (calamityPlayer.GreenJellyRegen && drawInfo.shadow == 0f && Main.rand.NextBool(16))
		{
			GeneralParticleHandler.SpawnParticle(new HealingPlus(base.Player.Center, Main.rand.NextFloat(0.6f, 1.3f), new Vector2(0f, Main.rand.NextFloat(-2f, -3.5f)) + base.Player.velocity, Color.Lime, Color.LimeGreen, Main.rand.Next(10, 15)));
		}
		if (calamityPlayer.AbsorberRegen && drawInfo.shadow == 0f && Main.rand.NextBool(11))
		{
			GeneralParticleHandler.SpawnParticle(new HealingPlus(base.Player.Center, Main.rand.NextFloat(0.7f, 1.4f), new Vector2(0f, Main.rand.NextFloat(-2f, -3.5f)) + base.Player.velocity, Color.DarkSeaGreen, Color.DarkSeaGreen, Main.rand.Next(10, 15)));
		}
		if (calamityPlayer.bloomStoneBuffedHealRateTimer > 0 && drawInfo.shadow == 0f)
		{
			if (Main.rand.NextBool(10))
			{
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Player.Center, Main.rand.NextVector2Circular(1f, 1f), Color.Yellow, Color.Gold, 0.85f, 100f));
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust4 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, ModContent.DustType<LightDust>(), 0f, 0f, 0, Color.Gold, 0.4f);
				dust4.noLightEmittence = true;
				dust4.noGravity = true;
			}
		}
		if (calamityPlayer.bloodfinBoost && drawInfo.shadow == 0f)
		{
			if (Main.rand.NextBool(3))
			{
				Vector2 position5 = drawInfo.Position - new Vector2(2f);
				int width3 = base.Player.width + 4;
				int height3 = base.Player.height + 4;
				int type = (Main.rand.NextBool(8) ? 296 : 5);
				float speedX3 = base.Player.velocity.X * 0.4f;
				float speedY3 = base.Player.velocity.Y * 0.4f;
				newColor = default(Color);
				Dust dust5 = Dust.NewDustDirect(position5, width3, height3, type, speedX3, speedY3, 100, newColor, 1.25f);
				dust5.noGravity = true;
				dust5.velocity *= 1.3f;
				dust5.velocity.Y -= 0.5f;
				drawInfo.DustCache.Add(dust5.dustIndex);
			}
			if (Main.rand.NextBool(16))
			{
				GeneralParticleHandler.SpawnParticle(new HealingPlus(base.Player.Center - new Vector2(4f, 0f), Main.rand.NextFloat(0.4f, 0.8f), new Vector2(0f, Main.rand.NextFloat(-2f, -3.5f)) + base.Player.velocity, Color.Red, Color.DarkRed, Main.rand.Next(10, 15)));
			}
		}
		if (calamityPlayer.planarSpeedBoost > 0 && drawInfo.shadow == 0f)
		{
			int spawnChance = 13 - calamityPlayer.planarSpeedBoost / 2;
			if (Main.rand.NextBool(spawnChance))
			{
				Vector2 sparkVelocity = -(Vector2.UnitY * Main.rand.NextFloat(2.5f, 5f)).RotatedByRandom(0.3141592741012573);
				GeneralParticleHandler.SpawnParticle(new AltLineParticle(new Vector2(base.Player.position.X + Main.rand.NextFloat(-8f, 40f), base.Player.position.Y + Main.rand.NextFloat(-8f, 56f)), sparkVelocity, affectedByGravity: false, 20, Main.rand.NextFloat(0.375f, 0.5f), new Color(130, 255, 255)));
				sparkVelocity = -(Vector2.UnitY * Main.rand.NextFloat(3f, 5.5f)).RotatedByRandom(0.5235987901687622);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(new Vector2(base.Player.position.X + Main.rand.NextFloat(-8f, 40f), base.Player.position.Y + Main.rand.NextFloat(-8f, 56f)), sparkVelocity, new Color(180, 255, 255), "CalamityMod/Particles/ElectricSpark", Vector2.One, 0f, 0.5f, 0.65f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		if (calamityPlayer.ladHearts > 0 && !base.Player.loveStruck && !Main.dedServ && drawInfo.shadow == 0f && Main.rand.NextBool(5))
		{
			Vector2 velocity2 = Main.rand.NextVector2Unit();
			velocity2.X *= 0.66f;
			velocity2 *= Main.rand.NextFloat(1f, 2f);
			int heart = Gore.NewGore(base.Player.GetSource_FromThis(), drawInfo.Position + new Vector2((float)Main.rand.Next(base.Player.width + 1), (float)Main.rand.Next(base.Player.height + 1)), velocity2, 331, Main.rand.NextFloat(0.4f, 1.2f));
			Main.gore[heart].sticky = false;
			Gore obj = Main.gore[heart];
			obj.velocity *= 0.4f;
			Main.gore[heart].velocity.Y -= 0.6f;
			drawInfo.GoreCache.Add(heart);
		}
	}

	public static void DetermineMoonlightDyeColors(out Color drawColor, Color dayColor, Color nightColor)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		int totalTime = (Main.dayTime ? 54000 : 32400);
		float transitionTime = 5400f;
		float interval = Utils.GetLerpValue(0f, transitionTime, (float)Main.time, clamped: true) + Utils.GetLerpValue((float)totalTime - transitionTime, totalTime, (float)Main.time, clamped: true);
		if (Main.dayTime)
		{
			if (Main.time >= (double)((float)totalTime - transitionTime))
			{
				drawColor = Color.Lerp(dayColor, nightColor, Utils.GetLerpValue((float)totalTime - transitionTime, totalTime, (float)Main.time, clamped: true));
			}
			else if (Main.time <= (double)transitionTime)
			{
				drawColor = Color.Lerp(nightColor, dayColor, interval);
			}
			else
			{
				drawColor = dayColor;
			}
		}
		else
		{
			drawColor = nightColor;
		}
	}

	public static Color GetCurrentMoonlightDyeColor(float angleOffset = 0f)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		float increment = MathHelper.Clamp((float)Math.Cos(Main.GlobalTimeWrappedHourly * 0.6f + angleOffset) * 0.5f + 0.5f, 0f, 0.995f);
		Color dayColorToUse = CalamityUtils.MulticolorLerp(increment, MoonlightDyeDayColors.ToArray());
		Color nightColorToUse = CalamityUtils.MulticolorLerp(increment, MoonlightDyeNightColors.ToArray());
		DetermineMoonlightDyeColors(out var drawColor, dayColorToUse, nightColorToUse);
		return drawColor;
	}

	public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
	{
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		if (drawInfo.shadow != 0f)
		{
			return;
		}
		Player drawPlayer = drawInfo.drawPlayer;
		Item item = drawPlayer.HeldItem;
		if (drawPlayer.frozen || (!item.IsAir && item.type <= 0) || drawPlayer.dead || (drawPlayer.wet && item.noWet) || (drawPlayer.wings != 0 && drawPlayer.velocity.Y != 0f))
		{
			return;
		}
		List<int> tankItems = new List<int>
		{
			ModContent.ItemType<FlurrystormCannon>(),
			ModContent.ItemType<BlightSpewer>(),
			ModContent.ItemType<HavocsBreath>(),
			ModContent.ItemType<SparkSpreader>(),
			ModContent.ItemType<HalleysInferno>(),
			ModContent.ItemType<CleansingBlaze>(),
			ModContent.ItemType<ChromaticEruption>(),
			ModContent.ItemType<DeadSunsWind>(),
			ModContent.ItemType<Meowthrower>(),
			ModContent.ItemType<OverloadedBlaster>(),
			ModContent.ItemType<WildfireBloom>(),
			ModContent.ItemType<Photoviscerator>(),
			ModContent.ItemType<Shadethrower>(),
			ModContent.ItemType<BloodBoiler>(),
			ModContent.ItemType<PristineFury>(),
			ModContent.ItemType<AuroraBlazer>(),
			ModContent.ItemType<PurgeGuzzler>()
		};
		List<Texture2D> tankTextures = new List<Texture2D>
		{
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_FlurrystormCannon", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_BlightSpewer", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_HavocsBreath", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_SparkSpreader", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_HalleysInferno", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_CleansingBlaze", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_ElementalEruption", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_DeadSunsWind", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_Meowthrower", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_OverloadedBlaster", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_WildfireBloom", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_Photoviscerator", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_Shadethrower", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_BloodBoiler", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_PristineFury", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_AuroraBlazer", (AssetRequestMode)2).Value,
			ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_PurgeGuzzler", (AssetRequestMode)2).Value
		};
		if (!tankItems.Contains(item.type) && !drawPlayer.Calamity().plaguebringerCarapace)
		{
			return;
		}
		Texture2D thingToDraw = null;
		if (tankItems.Contains(item.type))
		{
			for (int i = 0; i < tankItems.Count; i++)
			{
				if (item.type == tankItems[i])
				{
					thingToDraw = tankTextures[i];
					break;
				}
			}
		}
		else if (drawPlayer.Calamity().plaguebringerCarapace)
		{
			thingToDraw = ModContent.Request<Texture2D>("CalamityMod/Items/Armor/Plaguebringer/PlaguebringerCarapace_Back", (AssetRequestMode)2).Value;
		}
		if (thingToDraw != null)
		{
			SpriteEffects spriteEffects = (SpriteEffects)(base.Player.direction == -1);
			int xOffset = 9;
			if (thingToDraw == ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/Backpack_Photoviscerator", (AssetRequestMode)2).Value)
			{
				xOffset = 16;
			}
			DrawData howDoIDrawThings = new DrawData(thingToDraw, new Vector2((float)(int)(drawPlayer.position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) - (float)(xOffset * drawPlayer.direction)) - 4f * (float)drawPlayer.direction, (float)(int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2) + 2f * drawPlayer.gravDir - 8f * drawPlayer.gravDir + drawPlayer.gfxOffY)), (Rectangle?)new Rectangle(0, 0, thingToDraw.Width, thingToDraw.Height), drawInfo.colorArmorBody, drawPlayer.bodyRotation, new Vector2((float)(thingToDraw.Width / 2), (float)(thingToDraw.Height / 2)), 1f, spriteEffects, 0f);
			howDoIDrawThings.shader = 0;
			drawInfo.DrawDataCache.Add(howDoIDrawThings);
		}
	}

	private void UpdateDrawingParameters()
	{
		if (base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		UpdateDrawParameter_RoverDriveShield();
		UpdateDrawParameter_LunicCorps();
		UpdateDrawParameter_ProfanedShield();
		UpdateDrawParameter_TheSponge();
		if (Main.netMode == 1)
		{
			if (drawingParameters_NetSyncCountdown > 0)
			{
				drawingParameters_NetSyncCountdown--;
			}
			else if (drawingParameters != drawingParameters_LastNetSyncValue)
			{
				drawingParameters_NetSyncCountdown = 8;
				drawingParameters_LastNetSyncValue = drawingParameters;
				SyncPlayerDrawParameterPacket.Send(this);
			}
		}
	}

	private void UpdateDrawParameter_RoverDriveShield()
	{
		bool isVanityOnly = roverDriveShieldVisible && !roverDrive;
		bool shieldExists = isVanityOnly || RoverDriveShieldDurability > 0;
		if (roverDriveShieldVisible & shieldExists)
		{
			float visualShieldStrength = 1f;
			if (!isVanityOnly)
			{
				visualShieldStrength = MathF.Pow((float)RoverDriveShieldDurability / (float)RoverDrive.ShieldDurabilityMax, 0.5f);
			}
			drawingParameters.RoverShieldCharge = visualShieldStrength;
		}
		else
		{
			drawingParameters.RoverShieldCharge = -1f;
		}
	}

	private void UpdateDrawParameter_LunicCorps()
	{
		if (LunicCorpsShieldDurability > 0)
		{
			float visualShieldStrength = MathF.Pow((float)LunicCorpsShieldDurability / (float)LunicCorpsHelmet.ShieldDurabilityMax, 0.5f);
			drawingParameters.LunicShieldCharge = visualShieldStrength;
		}
		else
		{
			drawingParameters.LunicShieldCharge = -1f;
		}
	}

	private void UpdateDrawParameter_ProfanedShield()
	{
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		bool isVanityOnly = pSoulShieldVisible && !pSoulArtifact;
		bool shouldNotDraw = andromedaState >= AndromedaPlayerState.LargeRobot;
		bool shieldExists = isVanityOnly || pSoulShieldDurability > 0;
		if ((pSoulShieldVisible && !shouldNotDraw) & shieldExists)
		{
			ProfanedSoulCrystal.DetermineTransformationEligibility(base.Player);
			int psState = (int)ProfanedSoulCrystal.GetPscStateFor(base.Player, profanedCrystalAnim >= 0);
			bool psc = profanedCrystalBuffs || (profanedCrystalAnim >= 0 && psState >= 1);
			float visualShieldStrength = 1f;
			if (!isVanityOnly)
			{
				float max = (psc ? ProfanedSoulCrystal.ShieldDurabilityMax : ProfanedSoulArtifact.ShieldDurabilityMax);
				visualShieldStrength = MathF.Pow((float)pSoulShieldDurability / max, 0.5f);
			}
			Color shieldColor = ProfanedSoulCrystal.GetColorForPsc(psState, Main.dayTime);
			if (psState >= 1)
			{
				shieldColor = (ProfanedSoulCrystal.contributorNames.Any((string name) => name.Equals(base.Player.name)) ? CalamityUtils.ColorSwap(new Color(255, 166, 0), new Color(25, 250, 25) * 0.8f, 6f) : ProfanedSoulCrystal.GetLerpedColorForPsc(this));
			}
			drawingParameters.ProfanedShieldColor = shieldColor;
			drawingParameters.ProfanedShieldCharge = visualShieldStrength;
		}
		else
		{
			drawingParameters.ProfanedShieldCharge = -1f;
			drawingParameters.ProfanedShieldColor = Color.White;
		}
	}

	private void UpdateDrawParameter_TheSponge()
	{
		bool isVanityOnly = spongeShieldVisible && !sponge;
		bool shieldExists = isVanityOnly || SpongeShieldDurability > 0;
		if (spongeShieldVisible & shieldExists)
		{
			float visualShieldStrength = 1f;
			if (!isVanityOnly)
			{
				visualShieldStrength = MathF.Pow((float)SpongeShieldDurability / (float)TheSponge.ShieldDurabilityMax, 0.5f);
			}
			drawingParameters.SpongeShieldCharge = visualShieldStrength;
		}
		else
		{
			drawingParameters.SpongeShieldCharge = -1f;
		}
	}

	public override void ModifyFishingAttempt(ref FishingAttempt attempt)
	{
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		if (!enchantedPearl)
		{
			return;
		}
		if (!attempt.crate)
		{
			attempt.crate = Main.rand.NextBool(6);
		}
		if (!attempt.crate)
		{
			return;
		}
		int uncommonRate = Math.Clamp(240 / attempt.fishingLevel, 3, 240);
		attempt.uncommon = Main.rand.NextBool(uncommonRate);
		int rareRate = Math.Clamp(840 / attempt.fishingLevel, 4, 840);
		bool rareRoll = (attempt.rare = Main.rand.NextBool(rareRate));
		int veryRareRate = Math.Clamp(1800 / attempt.fishingLevel, 5, 1800);
		bool veryRareRoll = (attempt.veryrare = Main.rand.NextBool(veryRareRate));
		Vector2 basePos = default(Vector2);
		((Vector2)(ref basePos))._002Ector((float)attempt.X * 16f, (float)attempt.Y * 16f);
		if (rareRoll | veryRareRoll)
		{
			for (int i = 0; i < 8; i++)
			{
				Vector2 position = basePos + Vector2.UnitX * Main.rand.NextFloat(-16f, 16f);
				Vector2 velocity = (Vector2.UnitY * Main.rand.NextFloat(-12f, -9f)).RotatedByRandom(MathHelper.ToRadians(18f));
				float scale = Main.rand.NextFloat(0.5f, 1.5f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(position, velocity, Color.White, Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f), scale, 36, 0.2f, scale * 2f));
			}
		}
		for (int j = 0; j < 6; j++)
		{
			Vector2 relativePosition = basePos + Vector2.UnitX * Main.rand.NextFloat(-16f, 16f);
			Vector2 velocity2 = (Vector2.UnitY * Main.rand.NextFloat(-3f, -0.5f)).RotatedByRandom(MathHelper.ToRadians(12f));
			GeneralParticleHandler.SpawnParticle(new PearlParticle(relativePosition, velocity2, affectedByGravity: false, 24, Main.rand.NextFloat(0.5f, 1f), Main.hslToRgb(Main.rand.NextFloat(), 1f, (rareRoll | veryRareRoll) ? 0.75f : 1f)));
		}
	}

	public override bool? CanConsumeBait(Item bait)
	{
		if (bait.type == ModContent.ItemType<BloodwormItem>())
		{
			return true;
		}
		return null;
	}

	public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		if (npcSpawn > 0)
		{
			return;
		}
		int bait = attempt.playerFishingConditions.BaitItemType;
		int questFish = attempt.questFish;
		int poolSize = attempt.waterTilesCount;
		bool inLava = attempt.inLava;
		bool honey = attempt.inHoney;
		bool sky = attempt.heightLevel == 0;
		bool surface = attempt.heightLevel == 1;
		bool underground = attempt.heightLevel == 2;
		bool cavern = attempt.heightLevel == 3;
		bool grabBagFish = attempt.uncommon && Main.rand.Next(100) < (enchantedPearl ? 30 : 15);
		Point point = base.Player.Center.ToTileCoordinates();
		bool canSulphurFish = false;
		if (Abyss.AtLeftSideOfWorld)
		{
			if (point.X < 380)
			{
				canSulphurFish = true;
			}
		}
		else if (point.X > Main.maxTilesX - 380)
		{
			canSulphurFish = true;
		}
		if (ZoneAbyss || ZoneSulphur)
		{
			canSulphurFish = true;
		}
		if (inLava)
		{
			if (attempt.CanFishInLava && ZoneCalamity)
			{
				if (attempt.crate && attempt.rare)
				{
					itemDrop = (Main.hardMode ? ModContent.ItemType<BrimstoneCrate>() : ModContent.ItemType<SlagCrate>());
				}
				else if (attempt.legendary)
				{
					itemDrop = ModContent.ItemType<DragoonDrizzlefish>();
				}
				else if (attempt.veryrare)
				{
					itemDrop = ModContent.ItemType<CharredLasher>();
				}
				else if (DownedBossSystem.downedProvidence && ((attempt.rare && Main.rand.NextBool(2)) || (attempt.uncommon && Main.rand.NextBool(4))))
				{
					itemDrop = ModContent.ItemType<Bloodfin>();
				}
				else if (questFish == ModContent.ItemType<Brimlish>() && attempt.uncommon)
				{
					itemDrop = ModContent.ItemType<Brimlish>();
				}
				else if (questFish == ModContent.ItemType<Slurpfish>() && attempt.uncommon)
				{
					itemDrop = ModContent.ItemType<Slurpfish>();
				}
				else if (questFish == ModContent.ItemType<Havocfish>() && attempt.uncommon)
				{
					itemDrop = ModContent.ItemType<Havocfish>();
				}
				else if (attempt.rare)
				{
					List<int> uncommonCatches = new List<int>
					{
						ModContent.ItemType<CoastalDemonfish>(),
						ModContent.ItemType<Shadowfish>()
					};
					itemDrop = uncommonCatches[Main.rand.Next(uncommonCatches.Count)];
				}
				else
				{
					itemDrop = ModContent.ItemType<CragBullhead>();
				}
			}
		}
		else
		{
			if (honey)
			{
				return;
			}
			if (canSulphurFish && bait == ModContent.ItemType<BloodwormItem>() && !BossRushEvent.BossRushActive)
			{
				if (!Main.projectile.Any((Projectile x) => x.active && x.aiStyle == 61 && x.ai[1] != 0f && x.localAI[1] == (float)(ModContent.NPCType<OldDuke>() * -1)))
				{
					npcSpawn = ModContent.NPCType<OldDuke>();
				}
				itemDrop = -1;
				sonar.Text = "";
				return;
			}
			if (attempt.playerFishingConditions.PoleItemType == ModContent.ItemType<WulfrumRod>())
			{
				if (Main.rand.NextBool(5))
				{
					itemDrop = ModContent.ItemType<WulfrumMetalScrap>();
					return;
				}
				if (Main.rand.NextBool(15))
				{
					itemDrop = ModContent.ItemType<EnergyCore>();
					return;
				}
				if (Main.rand.NextBool(50))
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						itemDrop = ModContent.ItemType<RoverDrive>();
						break;
					case 1:
						itemDrop = ModContent.ItemType<WulfrumBattery>();
						break;
					case 2:
						itemDrop = ModContent.ItemType<AbandonedWulfrumHelmet>();
						break;
					}
					return;
				}
			}
			if (itemDrop == 2337 || itemDrop == 2338 || itemDrop == 2339 || itemDrop == 5275)
			{
				return;
			}
			if (attempt.crate)
			{
				if (attempt.rare)
				{
					if (ZoneAstral)
					{
						itemDrop = (Main.hardMode ? ModContent.ItemType<AstralCrate>() : ModContent.ItemType<MonolithCrate>());
					}
					if (ZoneSunkenSea)
					{
						itemDrop = (Main.hardMode ? ModContent.ItemType<PrismCrate>() : ModContent.ItemType<EutrophicCrate>());
					}
					if (canSulphurFish)
					{
						itemDrop = (Main.hardMode ? ModContent.ItemType<HydrothermalCrate>() : ModContent.ItemType<SulphurousCrate>());
					}
				}
			}
			else
			{
				if (new List<int> { 4382, 5240, 2423, 3225, 2420 }.Contains(itemDrop))
				{
					return;
				}
				if (DownedBossSystem.downedLeviathan && attempt.legendary && poolSize > 1000 && !Main.rand.NextBool(3))
				{
					itemDrop = ModContent.ItemType<Floodtide>();
					return;
				}
				if (NPC.downedBoss3 && (ZoneAbyssLayer2 || ZoneAbyssLayer3 || ZoneAbyssLayer4) && attempt.rare)
				{
					switch (Main.rand.Next(10))
					{
					case 0:
						itemDrop = ModContent.ItemType<Lionfish>();
						return;
					case 1:
						itemDrop = ModContent.ItemType<HerringStaff>();
						return;
					case 2:
						itemDrop = ModContent.ItemType<BallOFugu>();
						return;
					case 3:
						itemDrop = ModContent.ItemType<BlackAnurian>();
						return;
					case 4:
						itemDrop = ModContent.ItemType<Archerfish>();
						return;
					case 5:
						itemDrop = ModContent.ItemType<AnechoicPlating>();
						return;
					case 6:
						itemDrop = ModContent.ItemType<IronBoots>();
						return;
					case 7:
						itemDrop = ModContent.ItemType<DepthCharm>();
						return;
					case 8:
						itemDrop = ModContent.ItemType<StrangeOrb>();
						return;
					case 9:
						itemDrop = ModContent.ItemType<TorrentialTear>();
						return;
					}
				}
				if (sky && questFish == ModContent.ItemType<SunbeamFish>() && attempt.uncommon)
				{
					itemDrop = ModContent.ItemType<SunbeamFish>();
				}
				if (base.Player.ZoneSnow && questFish == ModContent.ItemType<FishofEleum>() && attempt.uncommon)
				{
					itemDrop = ModContent.ItemType<FishofEleum>();
				}
				if (grabBagFish)
				{
					if (surface && Main.bloodMoon)
					{
						itemDrop = ModContent.ItemType<Gorecodile>();
					}
					else if (surface && Main.dayTime)
					{
						itemDrop = ModContent.ItemType<StuffedFish>();
					}
					else if (cavern)
					{
						itemDrop = ModContent.ItemType<GlimmeringGemfish>();
					}
					if (Main.hardMode & sky)
					{
						itemDrop = ModContent.ItemType<FishofFlight>();
					}
				}
				if (surface && !Main.dayTime)
				{
					int chance = ((base.Player.ConsumedManaCrystals >= 9) ? 20 : 5);
					if (attempt.uncommon && Main.rand.NextBool(chance))
					{
						itemDrop = ModContent.ItemType<EnchantedStarfish>();
					}
					if (attempt.uncommon && Main.rand.NextBool(10))
					{
						itemDrop = ModContent.ItemType<Shadowfish>();
					}
				}
				if (underground)
				{
					int chance2 = (Main.hardMode ? 10 : 2);
					if (attempt.veryrare && Main.rand.NextBool(chance2))
					{
						itemDrop = ModContent.ItemType<Spadefish>();
					}
				}
				if (ZoneAstral)
				{
					if (attempt.legendary)
					{
						int legendaryCatch = Utils.SelectRandom<int>(Main.rand, ModContent.ItemType<PolarisParrotfish>(), ModContent.ItemType<GacruxianMollusk>(), ModContent.ItemType<UrsaSergeant>());
						itemDrop = legendaryCatch;
					}
					else if (attempt.veryrare)
					{
						itemDrop = ModContent.ItemType<ArcturusAstroidean>();
					}
					else if (attempt.uncommon || attempt.rare)
					{
						int uncommonCatch = Utils.SelectRandom<int>(Main.rand, ModContent.ItemType<ProcyonidPrawn>(), ModContent.ItemType<AldebaranAlewife>());
						itemDrop = uncommonCatch;
					}
					else
					{
						itemDrop = ModContent.ItemType<TwinklingPollox>();
					}
				}
				else if (ZoneSunkenSea)
				{
					if (!base.Player.ZoneDesert || !Main.rand.NextBool())
					{
						if (attempt.legendary)
						{
							int num = 1;
							List<int> list = new List<int>(num);
							CollectionsMarshal.SetCount(list, num);
							Span<int> span = CollectionsMarshal.AsSpan(list);
							int index = 0;
							span[index] = ModContent.ItemType<RustedJingleBell>();
							List<int> legendaryCatches = list;
							legendaryCatches.AddWithCondition(ModContent.ItemType<SerpentsBite>(), Main.hardMode);
							itemDrop = legendaryCatches[Main.rand.Next(legendaryCatches.Count)];
						}
						else if (attempt.veryrare)
						{
							int index = 1;
							List<int> list2 = new List<int>(index);
							CollectionsMarshal.SetCount(list2, index);
							Span<int> span2 = CollectionsMarshal.AsSpan(list2);
							int num = 0;
							span2[num] = ModContent.ItemType<GreenwaveLoach>();
							List<int> veryRareCatches = list2;
							veryRareCatches.AddWithCondition(ModContent.ItemType<SparklingEmpress>(), DownedBossSystem.downedDesertScourge);
							veryRareCatches.AddWithCondition(ModContent.ItemType<SeaSpiritAmulet>(), DownedBossSystem.downedDesertScourge);
							itemDrop = veryRareCatches[Main.rand.Next(veryRareCatches.Count)];
						}
						else if (questFish == ModContent.ItemType<EutrophicSandfish>() && attempt.uncommon)
						{
							itemDrop = ModContent.ItemType<EutrophicSandfish>();
						}
						else if (questFish == ModContent.ItemType<SurfClam>() && attempt.uncommon)
						{
							itemDrop = ModContent.ItemType<SurfClam>();
						}
						else if (questFish == ModContent.ItemType<Serpentuna>() && attempt.uncommon)
						{
							itemDrop = ModContent.ItemType<Serpentuna>();
						}
						else if (attempt.uncommon || attempt.rare)
						{
							itemDrop = ModContent.ItemType<SunkenSailfish>();
						}
						else if (Main.rand.NextBool())
						{
							itemDrop = ModContent.ItemType<Driftwood>();
						}
						else
						{
							itemDrop = ModContent.ItemType<PrismaticGuppy>();
						}
					}
				}
				else if (canSulphurFish)
				{
					if (attempt.legendary)
					{
						itemDrop = ModContent.ItemType<AlluringBait>();
					}
					else if (attempt.common && Main.rand.NextBool())
					{
						itemDrop = ModContent.ItemType<PlantyMush>();
					}
				}
			}
		}
	}

	public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if ((ZoneAstral || ZoneAbyss || ZoneSulphur) && bait.type == ModContent.ItemType<ArcturusAstroidean>())
		{
			fishingLevel *= ArcturusAstroidean.FishingPowerBiomeMult;
		}
		if (base.Player.ZoneSnow && fishingRod.type == ModContent.ItemType<VerstaltiteFishingRod>())
		{
			fishingLevel *= VerstaltiteFishingRod.FishingPowerBiomeMult;
		}
		if (base.Player.ZoneSkyHeight && fishingRod.type == ModContent.ItemType<HeronRod>())
		{
			fishingLevel *= HeronRod.FishingPowerBiomeMult;
		}
		if (bait.type != ModContent.ItemType<BloodwormItem>())
		{
			return;
		}
		Point point = base.Player.Center.ToTileCoordinates();
		bool canSulphurFish = false;
		if (Abyss.AtLeftSideOfWorld)
		{
			if (point.X < 380)
			{
				canSulphurFish = true;
			}
		}
		else if (point.X > Main.maxTilesX - 380)
		{
			canSulphurFish = true;
		}
		if (ZoneAbyss || ZoneSulphur)
		{
			canSulphurFish = true;
		}
		Item item = base.Player.HeldItem;
		if (!canSulphurFish || item.fishingPole <= 0 || item.holdStyle != 1)
		{
			fishingLevel = -1f;
		}
		base.Player.displayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
	}

	public override void ModifyCaughtFish(Item fish)
	{
		if (fish.type == ModContent.ItemType<Driftwood>())
		{
			fish.stack = ((!Main.rand.NextBool(14)) ? 1 : 20) * Main.rand.Next(8, 21);
		}
		if (alluringBait && new List<int>
		{
			2312,
			2315,
			2303,
			2321,
			2309,
			2317,
			2311,
			2313,
			2306,
			2318,
			2305,
			2319,
			2307,
			2310,
			2304,
			ModContent.ItemType<CoastalDemonfish>(),
			ModContent.ItemType<Shadowfish>(),
			ModContent.ItemType<AldebaranAlewife>(),
			ModContent.ItemType<SunkenSailfish>()
		}.Contains(fish.type))
		{
			fish.stack += Main.rand.Next(1, 4);
		}
		if (fish.type == ModContent.ItemType<WulfrumMetalScrap>())
		{
			fish.stack = Main.rand.Next(1, 6);
		}
	}

	private void SpectralVeilDodge()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		int spectralVeilIFrames = spectralVeilImmunity + (base.Player.longInvince ? 40 : 0);
		base.Player.GiveUniversalIFrames(spectralVeilIFrames, blink: true);
		rogueStealth = rogueStealthMax;
		spectralVeilImmunity = 0;
		Vector2 sVeilDustDir = default(Vector2);
		((Vector2)(ref sVeilDustDir))._002Ector(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
		((Vector2)(ref sVeilDustDir)).Normalize();
		sVeilDustDir *= 0.5f;
		for (int j = 0; j < 20; j++)
		{
			Dust sVeilDust1 = Dust.NewDustDirect(base.Player.Center, 1, 1, 21, sVeilDustDir.X * (float)j, sVeilDustDir.Y * (float)j);
			Dust dust = Dust.NewDustDirect(base.Player.Center, 1, 1, 21, (0f - sVeilDustDir.X) * (float)j, (0f - sVeilDustDir.Y) * (float)j);
			sVeilDust1.noGravity = false;
			sVeilDust1.noLight = false;
			dust.noGravity = false;
			dust.noLight = false;
		}
		SoundEngine.PlaySound(in SilvaArmor.DispelSound, base.Player.Center);
		NetMessage.SendData(62, -1, -1, null, base.Player.whoAmI, 1f);
	}

	private void GodSlayerDodge()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		int godSlayerDodgeIFrames = base.Player.ComputeDodgeIFrames();
		base.Player.GiveUniversalIFrames(godSlayerDodgeIFrames, blink: true);
		SoundEngine.PlaySound(in SoundID.Item67, base.Player.Center);
		for (int j = 0; j < 30; j++)
		{
			Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 173, 0f, 0f, 100, default(Color), 2f);
			dust.position.X += Main.rand.Next(-20, 21);
			dust.position.Y += Main.rand.Next(-20, 21);
			dust.velocity *= 0.4f;
			dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
			dust.shader = GameShaders.Armor.GetSecondaryShader(base.Player.ArmorSetDye(), base.Player);
			if (Main.rand.NextBool())
			{
				dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				dust.noGravity = true;
			}
		}
		NetMessage.SendData(62, -1, -1, null, base.Player.whoAmI, 1f);
	}

	private void CounterScarfDodge()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		int duration = CalamityUtils.SecondsToFrames(30);
		base.Player.AddCooldown(ScarfCooldown.ID, duration, true, evasionScarf ? "evasionscarf" : "counterscarf");
		int counterScarfIFrames = base.Player.ComputeDodgeIFrames();
		base.Player.GiveUniversalIFrames(counterScarfIFrames, blink: true);
		for (int j = 0; j < 100; j++)
		{
			Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 235, 0f, 0f, 100, default(Color), 2f);
			dust.position.X += Main.rand.Next(-20, 21);
			dust.position.Y += Main.rand.Next(-20, 21);
			dust.velocity *= 0.4f;
			dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
			dust.shader = GameShaders.Armor.GetSecondaryShader(base.Player.cNeck, base.Player);
			if (Main.rand.NextBool())
			{
				dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				dust.noGravity = true;
			}
		}
		NetMessage.SendData(62, -1, -1, null, base.Player.whoAmI, 1f);
	}

	public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		PopupGUIManager.SuspendAll();
		if (andromedaState == AndromedaPlayerState.LargeRobot && !Main.dedServ)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Player.Center + Main.rand.NextVector2Circular(60f, 90f), 133);
				dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.35f);
			}
			for (int j = 0; j < 3; j++)
			{
				Utils.PoofOfSmoke(base.Player.Center + Main.rand.NextVector2Circular(20f, 30f));
			}
		}
		if (XykVisualsBlue || XykVisualsOrange)
		{
			Projectile.NewProjectile(base.Player.GetSource_FromThis(), base.Player.Center, Vector2.Zero, ModContent.ProjectileType<XykDeathAnim>(), 0, 0f, base.Player.whoAmI);
		}
		if (holyInferno)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.type == ModContent.NPCType<Providence>())
				{
					n.active = false;
				}
			}
		}
		if (nebulousCore && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.NebulousCore.ID))
		{
			SoundEngine.PlaySound(in SoundID.Item67, base.Player.Center);
			for (int k = 0; k < 50; k++)
			{
				Dust dust2 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 173, 0f, 0f, 100, default(Color), 2f);
				dust2.position.X += Main.rand.Next(-20, 21);
				dust2.position.Y += Main.rand.Next(-20, 21);
				dust2.velocity *= 0.9f;
				dust2.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				dust2.shader = GameShaders.Armor.GetSecondaryShader(base.Player.cBody, base.Player);
				if (Main.rand.NextBool())
				{
					dust2.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				}
			}
			if (chaliceOfTheBloodGod)
			{
				chaliceBleedoutBuffer = 0.0;
				chaliceDamagePointPartialProgress = 0.0;
			}
			base.Player.HealPlayer(100);
			base.Player.AddCooldown(global::CalamityMod.Cooldowns.NebulousCore.ID, CalamityUtils.SecondsToFrames(90));
			return false;
		}
		if (DashID == GodslayerArmorDash.ID && base.Player.dashDelay < 0)
		{
			if (base.Player.statLife < 1)
			{
				base.Player.statLife = 1;
			}
			return false;
		}
		if (silvaSet && silvaCountdown > 0)
		{
			if (silvaCountdown == SilvaArmor.ReviveDuration && !hasSilvaEffect)
			{
				SoundEngine.PlaySound(in SilvaArmor.ActivationSound, base.Player.Center);
				base.Player.AddBuff(ModContent.BuffType<SilvaRevival>(), SilvaArmor.ReviveDuration);
			}
			hasSilvaEffect = true;
			if (base.Player.statLife < 1)
			{
				base.Player.statLife = 1;
			}
			if (chaliceOfTheBloodGod)
			{
				chaliceBleedoutBuffer = 0.0;
				chaliceDamagePointPartialProgress = 0.0;
			}
			return false;
		}
		if (necroSet && necroReviveCounter == -1)
		{
			SoundEngine.PlaySound(in SoundID.DD2_SkeletonDeath, base.Player.Center);
			necroReviveCounter = 0;
			base.Player.statLife = base.Player.statLifeMax2;
			if (base.Player.statLife < 1)
			{
				base.Player.statLife = 1;
			}
			return false;
		}
		if (permafrostsConcoction && !base.Player.HasCooldown(PermafrostConcoction.ID))
		{
			base.Player.AddCooldown(PermafrostConcoction.ID, CalamityUtils.SecondsToFrames(180));
			base.Player.AddBuff(ModContent.BuffType<Encased>(), CalamityUtils.SecondsToFrames(3f));
			base.Player.statLife = base.Player.statLifeMax2 * 3 / 10;
			if (base.Player.statMana < 0)
			{
				base.Player.statMana = 0;
			}
			SoundEngine.PlaySound(in SoundID.Item92, base.Player.Center);
			for (int l = 0; l < 60; l++)
			{
				Dust dust3 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 88, 0f, 0f, 0, default(Color), 2.5f);
				dust3.noGravity = true;
				dust3.velocity *= 5f;
			}
			return false;
		}
		if (damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
		{
			if (alcoholPoisoning)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.AlcoholBig" + Main.rand.Next(1, 3)).ToNetworkText(base.Player.name));
			}
			if (vHex)
			{
				string vHexKeyToUse = "Status.Death.VulnerabilityHex" + Main.rand.Next(1, 4);
				if (Main.rand.NextBool() && CalamityGlobalNPC.SCal != -1)
				{
					if (CalamityGlobalNPC.SCalGrief != -1)
					{
						vHexKeyToUse = "Status.Death.VulnerabilityHexGrief";
					}
					else if (CalamityGlobalNPC.SCalLament != -1)
					{
						vHexKeyToUse = "Status.Death.VulnerabilityHexLament";
					}
					else if (CalamityGlobalNPC.SCalEpiphany != -1)
					{
						vHexKeyToUse = "Status.Death.VulnerabilityHexEpiphany";
					}
					else if (CalamityGlobalNPC.SCalAcceptance != -1)
					{
						vHexKeyToUse = "Status.Death.VulnerabilityHexAcceptance";
					}
				}
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText(vHexKeyToUse).ToNetworkText(base.Player.name));
			}
			if (ZoneCalamity && base.Player.lavaWet)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.SearingLava" + Main.rand.Next(1, 3)).ToNetworkText(base.Player.name));
			}
			if (godSlayerInferno)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.GodSlayerInferno" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (sulphurPoison)
			{
				if (!Main.rand.NextBool(4))
				{
					damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.SulphuricPoisoning" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
				}
				else
				{
					damageSource = PlayerDeathReason.ByOther(9);
				}
			}
			if (dragonFire)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Dragonfire" + Main.rand.Next(1, 5)).ToNetworkText(base.Player.name));
			}
			if (vermillionFlux)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.VermillionFlux" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (auricRebuke)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.AuricRebuke" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (staticDischarge)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.StaticDischarge" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (miracleBlight)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.MiracleBlight" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (holyInferno)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.HolyInferno").ToNetworkText(base.Player.name));
			}
			if (holyFlames || banishingFire)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.HolyFlames" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (shadowflame)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Shadowflame").ToNetworkText(base.Player.name));
			}
			if (daybroken)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Daybroken").ToNetworkText(base.Player.name));
			}
			if (burningBlood)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.BurningBlood" + Main.rand.Next(1, 3)).ToNetworkText(base.Player.name));
			}
			if (brainRot)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.BrainRot" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (heavybleeding)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.HeavyBleeding" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (laceration)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Laceration" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (elementalMix)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ElementalMix" + Main.rand.Next(1, 3)).ToNetworkText(base.Player.name));
			}
			if (crushDepth)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.CrushDepth" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (riptide)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Riptide" + Main.rand.Next(1, 3)).ToNetworkText(base.Player.name));
			}
			if (hadopelagicPressure)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.HadopelagicPressure" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (brimstoneFlames || weakBrimstoneFlames || demonicFlames)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.BrimstoneFlames" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (plague)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Plague" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (astralInfection)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.AstralInfection" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			if (nightwither)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Nightwither").ToNetworkText(base.Player.name));
			}
			if (vaporfied)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Vaporfied").ToNetworkText(base.Player.name));
			}
			if (manaOverloader || ManaBurn)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ManaBurn").ToNetworkText(base.Player.name));
			}
			if (witheredDebuff)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Withered").ToNetworkText(base.Player.name));
			}
		}
		if (profanedCrystalBuffs && base.Player.Transformation().Type == ModContent.ItemType<ProfanedSoulCrystal>())
		{
			damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ProfanedSoulCrystal").ToNetworkText(base.Player.name));
		}
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in LeonDeathNoiseRE4_ForGFB, base.Player.Center);
		}
		if (NorfleetCounter > 3 && NorfleetCounter < 1000)
		{
			damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Norfleet").ToNetworkText(base.Player.name));
		}
		NorfleetCounter = 0;
		if (damageSource.TryGetCausingEntity(out var Entity) && Entity is NPC && (Entity as NPC).type == ModContent.NPCType<DevourerofGodsHead>())
		{
			NPC npc = Main.npc[damageSource.SourceNPCIndex];
			if (npc.ai[3] < 2f)
			{
				damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.DivinityDevourer" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
			}
			else if (npc.ai[3] >= 3f)
			{
				if (npc.life > npc.lifeMax / 4)
				{
					damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.DimensionalDrive").ToNetworkText(base.Player.name));
				}
				else
				{
					damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.UltracosmicMaelstrom" + Main.rand.Next(1, 3)).ToNetworkText(base.Player.name));
				}
			}
		}
		if (NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()) && sCalDeathCount < 51)
		{
			sCalDeathCount++;
		}
		return true;
	}

	public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		modifiers.CritDamage += critDamage;
		float totalDamageMult = 1f;
		CalamityUtils.ApplyRippersToDamage(this, item.IsTrueMelee(), ref totalDamageMult);
		if (enraged)
		{
			totalDamageMult += DemonshadeHelm.MultDamageBoost;
		}
		if (witheredDebuff && witheringWeaponEnchant)
		{
			totalDamageMult += 0.6f;
		}
		modifiers.SourceDamage *= totalDamageMult;
		CalamityGlobalNPC cgn = target.Calamity();
		if (yellowCandle && cgn.DR < 0.99f && target.takenDamageMultiplier > 0.05f)
		{
			modifiers.ModifyHitInfo += YellowCandleBuff.ModifyHitInfo_Spite;
		}
		if (base.Player.Calamity().scionsCurio && item.CountsAsClass<RangedDamageClass>())
		{
			target.Calamity().scionsCurioEffected = true;
		}
		if (!frostSet)
		{
			return;
		}
		float DistanceInterpolant = Utils.GetLerpValue(160f, 800f, target.Distance(Main.LocalPlayer.Center), clamped: true);
		if (item.CountsAsClass<MeleeDamageClass>())
		{
			float meleeBoost = MathHelper.Lerp(0f, 0.2f, 1f - DistanceInterpolant);
			modifiers.SourceDamage += meleeBoost;
			if (meleeBoost >= 0.1f)
			{
				float intensity = meleeBoost / 0.2f;
				SoundStyle style = Cryogen.HitSound with
				{
					Volume = intensity - 0.2f
				};
				SoundEngine.PlaySound(in style, base.Player.Center);
			}
			if (meleeBoost > 0f)
			{
				int count = (int)(30f * meleeBoost);
				for (int i = 0; i < count; i++)
				{
					Vector2 velocity = Main.rand.NextVector2Unit() * (5f + 100f * meleeBoost);
					float scale = Main.rand.NextFloat(0.5f, 1f) + 0.5f * meleeBoost / 0.2f;
					GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, velocity, Color.White, Color.DodgerBlue, scale, 15, 0.1f, scale * 2f));
				}
			}
		}
		else
		{
			if (!item.CountsAsClass<RangedDamageClass>())
			{
				return;
			}
			float rangedBoost = MathHelper.Lerp(0f, 0.2f, DistanceInterpolant);
			modifiers.SourceDamage += rangedBoost;
			if (rangedBoost >= 0.1f)
			{
				float intensity2 = rangedBoost / 0.2f;
				SoundStyle style = Cryogen.HitSound with
				{
					Volume = intensity2 - 0.2f
				};
				SoundEngine.PlaySound(in style, base.Player.Center);
			}
			if (rangedBoost > 0f)
			{
				int count2 = (int)(30f * rangedBoost);
				for (int j = 0; j < count2; j++)
				{
					Vector2 velocity2 = Main.rand.NextVector2Unit() * (5f + 100f * rangedBoost);
					float scale2 = Main.rand.NextFloat(0.5f, 1f) + 0.5f * rangedBoost / 0.2f;
					GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, velocity2, Color.White, Color.DodgerBlue, scale2, 15, 0.1f, scale2 * 2f));
				}
			}
		}
	}

	public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		if (proj.npcProj || proj.trap)
		{
			return;
		}
		modifiers.CritDamage += critDamage;
		float totalDamageMult = 1f;
		CalamityUtils.ApplyRippersToDamage(this, proj.IsTrueMelee(), ref totalDamageMult);
		if (enraged)
		{
			totalDamageMult += DemonshadeHelm.MultDamageBoost;
		}
		if (witheredDebuff && witheringWeaponEnchant)
		{
			totalDamageMult += 0.6f;
		}
		modifiers.SourceDamage *= totalDamageMult;
		CalamityGlobalNPC cgn = target.Calamity();
		if (yellowCandle && cgn.DR < 0.99f && target.takenDamageMultiplier > 0.05f)
		{
			modifiers.ModifyHitInfo += YellowCandleBuff.ModifyHitInfo_Spite;
		}
		if (proj.Calamity().stealthStrike && proj.CountsAsClass<RogueDamageClass>())
		{
			modifiers.SourceDamage *= (float)bonusStealthDamage + 1f;
		}
		if (base.Player.Calamity().scionsCurio && proj.CountsAsClass<RangedDamageClass>())
		{
			target.Calamity().scionsCurioEffected = true;
		}
		if (frostSet)
		{
			float DistanceInterpolant = Utils.GetLerpValue(160f, 800f, target.Distance(Main.LocalPlayer.Center), clamped: true);
			if (proj.CountsAsClass<MeleeDamageClass>())
			{
				float meleeBoost = MathHelper.Lerp(0f, 0.2f, 1f - DistanceInterpolant);
				modifiers.SourceDamage += meleeBoost;
				if (meleeBoost >= 0.1f)
				{
					float intensity = meleeBoost / 0.2f;
					SoundStyle style = Cryogen.HitSound with
					{
						Volume = intensity - 0.2f
					};
					SoundEngine.PlaySound(in style, base.Player.Center);
				}
				if (meleeBoost > 0f)
				{
					int count = (int)(30f * meleeBoost);
					for (int i = 0; i < count; i++)
					{
						Vector2 velocity = Main.rand.NextVector2Unit() * (5f + 100f * meleeBoost);
						float scale = Main.rand.NextFloat(0.5f, 1f) + 0.5f * meleeBoost / 0.2f;
						GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, velocity, Color.White, Color.DodgerBlue, scale, 15, 0.1f, scale * 2f));
					}
				}
			}
			else if (proj.CountsAsClass<RangedDamageClass>())
			{
				float rangedBoost = MathHelper.Lerp(0f, 0.2f, DistanceInterpolant);
				modifiers.SourceDamage += rangedBoost;
				if (rangedBoost >= 0.1f)
				{
					float intensity2 = rangedBoost / 0.2f;
					SoundStyle style = Cryogen.HitSound with
					{
						Volume = intensity2 - 0.2f
					};
					SoundEngine.PlaySound(in style, base.Player.Center);
				}
				if (rangedBoost > 0f)
				{
					int count2 = (int)(30f * rangedBoost);
					for (int j = 0; j < count2; j++)
					{
						Vector2 velocity2 = Main.rand.NextVector2Unit() * (5f + 100f * rangedBoost);
						float scale2 = Main.rand.NextFloat(0.5f, 1f) + 0.5f * rangedBoost / 0.2f;
						GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, velocity2, Color.White, Color.DodgerBlue, scale2, 15, 0.1f, scale2 * 2f));
					}
				}
			}
		}
		if (proj.CountsAsClass<SummonDamageClass>())
		{
			Item heldItem = base.Player.HeldItem;
			if (CalamityUtils.ShouldTriggerSummonPenalty(base.Player, heldItem) && !CalamityProjectileSets.MinionWhichIgnoresSummonerNerf[proj.type])
			{
				modifiers.FinalDamage *= BalancingConstants.SummonerCrossClassNerf;
			}
		}
	}

	public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
	{
		if (npc.Calamity().antlionCloudDebuffTimer > 0)
		{
			modifiers.SourceDamage *= AntlionSkewer.CloudDamageDebuffMult;
		}
		if (npc.poisoned)
		{
			float damageReductionFromPoison = (float)((npc.Calamity().irradiated ? npc.Calamity().irradiatedContactBoost : 1.0) * 0.05000000074505806);
			if (npc.Calamity().VulnerableToSickness.HasValue)
			{
				damageReductionFromPoison = ((!npc.Calamity().VulnerableToSickness.Value) ? (damageReductionFromPoison / 2f) : (damageReductionFromPoison * 2f));
			}
			damageReductionFromPoison = 1f - damageReductionFromPoison;
			modifiers.SourceDamage *= damageReductionFromPoison;
		}
		if (npc.venom)
		{
			float damageReductionFromVenom = (float)((npc.Calamity().irradiated ? npc.Calamity().irradiatedContactBoost : 1.0) * 0.05000000074505806);
			if (npc.Calamity().VulnerableToSickness.HasValue)
			{
				damageReductionFromVenom = ((!npc.Calamity().VulnerableToSickness.Value) ? (damageReductionFromVenom / 2f) : (damageReductionFromVenom * 2f));
			}
			damageReductionFromVenom = 1f - damageReductionFromVenom;
			modifiers.SourceDamage *= damageReductionFromVenom;
		}
		if (npc.Calamity().astralInfection)
		{
			float damageReductionFromAstralInfection = (float)((npc.Calamity().irradiated ? npc.Calamity().irradiatedContactBoost : 1.0) * 0.05000000074505806);
			if (npc.Calamity().VulnerableToSickness.HasValue)
			{
				damageReductionFromAstralInfection = ((!npc.Calamity().VulnerableToSickness.Value) ? (damageReductionFromAstralInfection / 2f) : (damageReductionFromAstralInfection * 2f));
			}
			damageReductionFromAstralInfection = 1f - damageReductionFromAstralInfection;
			modifiers.SourceDamage *= damageReductionFromAstralInfection;
		}
		if (npc.Calamity().plague)
		{
			float damageReductionFromPlague = (float)((npc.Calamity().irradiated ? npc.Calamity().irradiatedContactBoost : 1.0) * 0.05000000074505806);
			if (npc.Calamity().VulnerableToSickness.HasValue)
			{
				damageReductionFromPlague = ((!npc.Calamity().VulnerableToSickness.Value) ? (damageReductionFromPlague / 2f) : (damageReductionFromPlague * 2f));
			}
			damageReductionFromPlague = 1f - damageReductionFromPlague;
			modifiers.SourceDamage *= damageReductionFromPlague;
		}
		if (npc.Calamity().whisperingDeath)
		{
			float damageReductionFromWhisperingDeath = (float)((npc.Calamity().irradiated ? npc.Calamity().irradiatedContactBoost : 1.0) * 0.10000000149011612);
			if (npc.Calamity().VulnerableToSickness.HasValue)
			{
				damageReductionFromWhisperingDeath = ((!npc.Calamity().VulnerableToSickness.Value) ? (damageReductionFromWhisperingDeath / 2f) : (damageReductionFromWhisperingDeath * 2f));
			}
			damageReductionFromWhisperingDeath = 1f - damageReductionFromWhisperingDeath;
			modifiers.SourceDamage *= damageReductionFromWhisperingDeath;
		}
		if (trueVHex)
		{
			modifiers.SourceDamage *= 1.15f;
		}
		if (fleshTotem && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.FleshTotem.ID) && TotalEnergyShielding <= 0)
		{
			modifiers.FinalDamage *= 0.5f;
		}
		if (tarragonCloak && tarraMelee && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.TarragonCloak.ID))
		{
			modifiers.FinalDamage *= 1f - TarragonHeadMelee.CloakContactDamageReduction;
		}
		if (bloodflareMelee && bloodflareFrenzy && !base.Player.HasCooldown(BloodflareFrenzy.ID))
		{
			modifiers.FinalDamage *= 1f - BloodflareHeadMelee.FrenzyContactDamageReduction;
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<EnergyShell>()] > 0 && base.Player.HeldItem.type == ModContent.ItemType<LionHeart>())
		{
			modifiers.FinalDamage *= 0.5f;
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfConvergenceCrystal>()] > 0 && base.Player.HeldItem.type == ModContent.ItemType<RelicOfConvergence>())
		{
			modifiers.FinalDamage *= RelicOfConvergence.IncomingDamageMultiplier;
		}
		bool lifeAndShieldCondition = base.Player.statLife >= base.Player.statLifeMax2 && (!HasAnyEnergyShield || TotalEnergyShielding >= TotalMaxShieldDurability);
		if ((theBee && theBeeCooldown <= 0) & lifeAndShieldCondition)
		{
			modifiers.FinalDamage *= 0.5f;
			shouldTriggerBeeCooldown = true;
		}
		if (AdrenalineEnabled)
		{
			bool num = !draedonsHeart && adrenaline == adrenalineMax && !adrenalineModeActive;
			bool usingNanomachinesWithDH = draedonsHeart && adrenalineModeActive;
			if ((num | usingNanomachinesWithDH) && TotalEnergyShielding <= 0)
			{
				modifiers.IncomingDamageMultiplier *= 1f - this.GetAdrenalineDR();
			}
		}
		if (Main.hardMode && Main.expertMode && ((npc.type == 30 && !NPC.AnyNPCs(471)) || npc.type == 665 || npc.type == 25 || npc.type == 33))
		{
			modifiers.SourceDamage *= 0.6f;
		}
	}

	public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
	{
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityWorld.revenge)
		{
			if (proj.type == 464 && proj.ai[1] == 1f)
			{
				base.Player.AddBuff(47, 120);
				modifiers.Cancel();
				return;
			}
			if (proj.type == 464 && proj.ai[1] != 1f)
			{
				base.Player.AddBuff(46, 240);
				modifiers.Cancel();
				return;
			}
		}
		if (proj.active && proj.hostile && modifiers.Dodgeable && proj.damage > 0)
		{
			double dodgeDamageGateValuePercent = 0.05;
			int dodgeDamageGateValue = (int)Math.Round((double)base.Player.statLifeMax2 * dodgeDamageGateValuePercent);
			int actualProjDamage = proj.damage;
			if (!proj.reflected && !ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[proj.type])
			{
				float damageMult = Main.GameModeInfo.EnemyDamageMultiplier;
				if (Main.GameModeInfo.IsJourneyMode)
				{
					CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
					if (power.GetIsUnlocked())
					{
						damageMult = power.StrengthMultiplierToGiveNPCs;
					}
				}
				actualProjDamage = (int)Math.Floor(2f * damageMult * (float)actualProjDamage);
			}
			if (!disableAllDodges && !base.Player.HasCooldown(GlobalDodge.ID) && actualProjDamage >= dodgeDamageGateValue)
			{
				double maxCooldownDurationDamagePercent = 0.5;
				int maxCooldownDurationDamageValue = (int)Math.Round((double)base.Player.statLifeMax2 * (maxCooldownDurationDamagePercent - dodgeDamageGateValuePercent));
				if (maxCooldownDurationDamageValue <= 0)
				{
					maxCooldownDurationDamageValue = 1;
				}
				float cooldownDurationScalar = MathHelper.Clamp((float)(actualProjDamage - dodgeDamageGateValue) / (float)maxCooldownDurationDamageValue, 0f, 1f);
				if (evolution)
				{
					if (base.Player.whoAmI == Main.myPlayer)
					{
						IEntitySource source = base.Player.GetSource_Accessory_OnHurt(FindAccessory<TheEvolution>(), modifiers.DamageSource);
						int mirrorDamage = (int)MathHelper.Min((float)actualProjDamage, 1000f) * 50;
						for (int i = 0; i < 5; i++)
						{
							Projectile.NewProjectile(source, base.Player.Center + Vector2.UnitX.RotatedBy((float)Math.PI * 2f * ((float)i / 5f)), Vector2.Zero, ModContent.ProjectileType<MirrorBlast>(), mirrorDamage, 5f, Main.myPlayer, 1f);
						}
					}
					projTypeJustHitBy = proj.type;
					procDodgeEffects = true;
					return;
				}
				if (daedalusReflect && !CalamityProjectileSets.ShouldNotBeReflected[proj.type] && !modifiers.PvP && !proj.friendly)
				{
					proj.hostile = false;
					proj.friendly = true;
					proj.damage = actualProjDamage;
					proj.velocity *= -1f;
					proj.penetrate = 1;
					int daedalusReflectIFrames = base.Player.ComputeReflectIFrames();
					base.Player.GiveUniversalIFrames(daedalusReflectIFrames, blink: true);
					modifiers.Cancel();
					int cooldownDuration = (int)MathHelper.Lerp((float)DaedalusHeadMelee.ReflectCooldownMin, (float)DaedalusHeadMelee.ReflectCooldownMax, cooldownDurationScalar);
					base.Player.AddCooldown(GlobalDodge.ID, cooldownDuration);
				}
			}
		}
		if (phantomicArtifact && base.Player.ownedProjectileCounts[ModContent.ProjectileType<global::CalamityMod.Projectiles.Summon.PhantomicShield>()] != 0)
		{
			Projectile projectile = (from projectile2 in Main.projectile.AsEnumerable()
				where projectile2.friendly && projectile2.owner == base.Player.whoAmI && projectile2.type == ModContent.ProjectileType<global::CalamityMod.Projectiles.Summon.PhantomicShield>()
				select projectile2).First();
			phantomicBulwarkCooldown = 1800;
			projectile.Kill();
			modifiers.FinalDamage *= 0.8f;
		}
		if (trueVHex)
		{
			modifiers.SourceDamage *= 1.15f;
		}
		if (auralisAuroraCounter >= 300)
		{
			modifiers.SourceDamage.Flat -= 100f;
			auralisAuroraCounter = 0;
			auralisAuroraCooldown = CalamityUtils.SecondsToFrames(30f);
		}
		if (proj.type == 949)
		{
			modifiers.SetMaxDamage(1);
		}
		if (proj.type == 108)
		{
			modifiers.SourceDamage *= 0.7f;
		}
		else if (proj.type == 727 || proj.type == 763)
		{
			modifiers.SourceDamage *= 0.6f;
		}
		if (Main.expertMode && !areThereAnyDamnBosses && (proj.type == 99 || proj.type == 1005 || proj.type == 1013 || proj.type == 1014))
		{
			modifiers.SourceDamage *= 0.75f;
		}
		bool isFallingBlock = proj.type == 31 || proj.type == 71 || proj.type == 40 || proj.type == 241 || proj.type == 56 || proj.type == 67 || proj.type == 812 || proj.type == 179 || proj.type == ModContent.ProjectileType<AstralSandBallFalling>();
		if (base.Player.Calamity().fallingBlockProtection & isFallingBlock)
		{
			modifiers.Cancel();
		}
		bool isIgnoredTrap = proj.type == 184 || proj.type == 980 || proj.type == 98;
		if (base.Player.Calamity().trapProtection & isIgnoredTrap)
		{
			modifiers.Cancel();
		}
		bool isReducedTrap = (proj.trap || proj.type == 763 || proj.type == 164) && !isIgnoredTrap;
		if (base.Player.Calamity().trapProtection & isReducedTrap)
		{
			modifiers.SourceDamage *= 1f - ArchaicPowder.TrapDamageReduction;
		}
		if (proj.type == 872)
		{
			Rectangle hitbox = proj.Hitbox;
			int trailLength = 80;
			int startOfDamageFalloff = 20;
			for (int k = 0; k < trailLength; k += 2)
			{
				Vector2 trailHitbox = proj.oldPos[k];
				if (trailHitbox == Vector2.Zero)
				{
					continue;
				}
				hitbox.X = (int)trailHitbox.X;
				hitbox.Y = (int)trailHitbox.Y;
				if (((Rectangle)(ref hitbox)).Intersects(base.Player.Hitbox))
				{
					if (k > startOfDamageFalloff)
					{
						modifiers.SourceDamage *= EmpressofLightAI.EverlastingRainbowTrailDamageMult;
					}
					break;
				}
			}
		}
		if (evolution && proj.type == projTypeJustHitBy)
		{
			modifiers.FinalDamage *= 0.75f;
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<EnergyShell>()] > 0 && base.Player.HeldItem.type == ModContent.ItemType<LionHeart>())
		{
			modifiers.FinalDamage *= 0.5f;
		}
		bool lifeAndShieldCondition = base.Player.statLife >= base.Player.statLifeMax2 && (!HasAnyEnergyShield || TotalEnergyShielding >= TotalMaxShieldDurability);
		if ((theBee && theBeeCooldown <= 0) & lifeAndShieldCondition)
		{
			modifiers.FinalDamage *= 0.5f;
			shouldTriggerBeeCooldown = true;
		}
		if (AdrenalineEnabled)
		{
			bool num = !draedonsHeart && adrenaline == adrenalineMax && !adrenalineModeActive;
			bool usingNanomachinesWithDH = draedonsHeart && adrenalineModeActive;
			if ((num | usingNanomachinesWithDH) && TotalEnergyShielding <= 0)
			{
				modifiers.IncomingDamageMultiplier *= 1f - this.GetAdrenalineDR();
			}
		}
		if (!copyrightInfringementShield)
		{
			return;
		}
		bool projectileRight = base.Player.Center.X - proj.Center.X < 0f;
		bool projectileLeft = base.Player.Center.X - proj.Center.X > 0f;
		if (base.Player.direction == 1)
		{
			if (projectileRight)
			{
				modifiers.FinalDamage *= 0.85f;
			}
		}
		else if (projectileLeft)
		{
			modifiers.FinalDamage *= 0.85f;
		}
	}

	public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		bool hasIFrames = false;
		for (int i = 0; i < base.Player.hurtCooldowns.Length; i++)
		{
			if (base.Player.hurtCooldowns[i] > 0)
			{
				hasIFrames = true;
			}
		}
		if (!hasIFrames && !base.Player.creativeGodMode)
		{
			nextHitDealsDefenseDamage |= npc.Calamity().canBreakPlayerDefense;
		}
		if (fleshTotem && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.FleshTotem.ID) && hurtInfo.Damage > 0)
		{
			base.Player.AddCooldown(global::CalamityMod.Cooldowns.FleshTotem.ID, CalamityUtils.SecondsToFrames(20));
		}
		if (NPC.AnyNPCs(ModContent.NPCType<THELORDE>()))
		{
			base.Player.AddBuff(ModContent.BuffType<NOU>(), 15);
		}
		if (crawCarapace)
		{
			npc.AddBuff(ModContent.BuffType<Crumbling>(), 900);
			Vector2 pushVel = base.Player.Center.DirectionTo(npc.Center) * 7f;
			if (!npc.dontTakeDamage)
			{
				int onHitDamage = (int)base.Player.GetBestClassDamage().ApplyTo(CrawCarapace.ThornsDamage);
				Projectile.NewProjectile(base.Player.GetSource_Accessory_OnHurt(FindAccessory<CrawCarapace>(), npc), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage, 0f, base.Player.whoAmI, npc.whoAmI, pushVel.X, pushVel.Y);
			}
			SoundEngine.PlaySound(SoundID.NPCHit33 with
			{
				Volume = 0.5f
			}, base.Player.Center);
			for (int j = 0; j < 10; j++)
			{
				float accuracy = Main.rand.NextFloat(-0.4f, 0.4f);
				float powerMult = 1f - Math.Abs(accuracy);
				Vector2 dustVel = pushVel.SafeNormalize(Vector2.UnitY).RotatedBy(accuracy * 2f) * Main.rand.NextFloat(4f, 7f) * powerMult;
				Dust dust = Dust.NewDustPerfect(base.Player.Center + dustVel, Main.rand.NextBool(4) ? 249 : 115, dustVel, 0, default(Color), Main.rand.NextFloat(0.75f, 1.2f));
				dust.noGravity = true;
				dust.noGravity = true;
				dust.fadeIn = 1f;
			}
		}
		if (baroclaw)
		{
			Vector2 pushVel2 = base.Player.Center.DirectionTo(npc.Center) * 15f;
			if (!npc.dontTakeDamage)
			{
				int onHitDamage2 = (int)base.Player.GetBestClassDamage().ApplyTo(Baroclaw.ThornsDamage);
				Projectile.NewProjectile(base.Player.GetSource_Accessory_OnHurt(FindAccessory<Baroclaw>(), npc), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage2, -1f, base.Player.whoAmI, npc.whoAmI, pushVel2.X, pushVel2.Y);
				npc.AddBuff(ModContent.BuffType<ArmorCrunch>(), 900);
				npc.AddBuff(ModContent.BuffType<CrushDepth>(), 900);
			}
			SoundEngine.PlaySound(in BaroclawHit, base.Player.Center);
			for (int k = 0; k < 17; k++)
			{
				float accuracy2 = Main.rand.NextFloat(-0.55f, 0.55f);
				float powerMult2 = 1f - Math.Abs(accuracy2);
				Vector2 fxVel = pushVel2.SafeNormalize(Vector2.UnitY).RotatedBy(accuracy2) * Main.rand.NextFloat(5f, 12f) * powerMult2;
				Vector2 dustVel2 = pushVel2.SafeNormalize(Vector2.UnitY).RotatedBy(accuracy2 * 2f) * Main.rand.NextFloat(10f, 20f) * powerMult2;
				Vector2 fxPos = base.Player.Center + fxVel;
				Color fxColor = Color.Lerp(Color.RoyalBlue, Color.DarkBlue, Main.rand.NextFloat(1f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(fxPos, fxVel, "CalamityMod/Particles/PointParticle", affectedByGravity: false, (int)((float)Main.rand.Next(22, 41) * powerMult2), Main.rand.NextFloat(1.95f, 2.2f) * powerMult2, fxColor, new Vector2(0.5f, 1.1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.3f) + (1f - powerMult2) * 0.3f));
				if (k % 3 == 0)
				{
					Dust dust2 = Dust.NewDustPerfect(fxPos, 278, dustVel2, 0, default(Color), Main.rand.NextFloat(0.75f, 1.1f));
					dust2.noGravity = true;
					dust2.color = Color.Gold;
					dust2.noGravity = false;
				}
			}
		}
		if (absorber)
		{
			Vector2 pushVel3 = base.Player.Center.DirectionTo(npc.Center) * 22f;
			if (!npc.dontTakeDamage)
			{
				int onHitDamage3 = (int)base.Player.GetBestClassDamage().ApplyTo(TheAbsorber.ThornsDamage);
				Projectile.NewProjectile(base.Player.GetSource_Accessory_OnHurt(FindAccessory<TheAbsorber>(), npc), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage3, -1f, base.Player.whoAmI, npc.whoAmI, pushVel3.X, pushVel3.Y);
				npc.AddBuff(ModContent.BuffType<AbsorberAffliction>(), 900);
			}
			SoundEngine.PlaySound(in AbsorberHit, base.Player.Center);
			for (int l = 0; l < 25; l++)
			{
				float accuracy3 = Main.rand.NextFloat(-0.7f, 0.7f);
				float powerMult3 = 1f - Math.Abs(accuracy3);
				Vector2 fxVel2 = pushVel3.SafeNormalize(Vector2.UnitY).RotatedBy(accuracy3) * Main.rand.NextFloat(10f, 18f) * powerMult3;
				Vector2 dustVel3 = pushVel3.SafeNormalize(Vector2.UnitY).RotatedBy(accuracy3 * 2f) * Main.rand.NextFloat(15f, 30f) * powerMult3;
				Vector2 val = base.Player.Center + fxVel2;
				Color fxColor2 = Color.Lerp(Color.DarkSeaGreen, Color.MediumSeaGreen, Main.rand.NextFloat(1f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(val, fxVel2, "CalamityMod/Particles/Sparkle", affectedByGravity: false, (int)((float)Main.rand.Next(32, 51) * powerMult3), Main.rand.NextFloat(2.25f, 2.5f) * powerMult3, fxColor2, new Vector2(0.5f, 1.1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.3f) + (1f - powerMult3) * 0.3f));
				Dust dust3 = Dust.NewDustPerfect(val, ModContent.DustType<LightDust>(), dustVel3, 0, default(Color), Main.rand.NextFloat(0.95f, 2.1f));
				dust3.noGravity = true;
				dust3.color = fxColor2;
			}
		}
		OnHitByCombat(hurtInfo);
	}

	public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
	{
		bool hasIFrames = false;
		for (int i = 0; i < base.Player.hurtCooldowns.Length; i++)
		{
			if (base.Player.hurtCooldowns[i] > 0)
			{
				hasIFrames = true;
			}
		}
		if (!hasIFrames && !base.Player.creativeGodMode)
		{
			nextHitDealsDefenseDamage |= proj.Calamity().DealsDefenseDamage;
		}
		if (!proj.friendly && hurtInfo.Damage > 0 && proj.Calamity().ParentNPCIndex != -1 && Main.npc[proj.Calamity().ParentNPCIndex].active && sulphurSet)
		{
			Main.npc[proj.Calamity().ParentNPCIndex].AddBuff(20, SulphurousHelmet.SetBonusPoisonDuration);
		}
		if (proj.hostile && hurtInfo.Damage > 0)
		{
			if (proj.type == 949)
			{
				int fireDebuffTypes = (CalamityWorld.death ? 9 : (CalamityWorld.revenge ? 7 : (Main.expertMode ? 5 : 3)));
				switch (Main.zenithWorld ? 9 : Main.rand.Next(fireDebuffTypes))
				{
				case 0:
					base.Player.AddBuff(24, 600);
					break;
				case 1:
					base.Player.AddBuff(44, 300);
					break;
				case 2:
					base.Player.AddBuff(39, 300);
					break;
				case 3:
					base.Player.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
					break;
				case 4:
					base.Player.AddBuff(ModContent.BuffType<Shadowflame>(), 150);
					break;
				case 5:
					base.Player.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 100);
					break;
				case 6:
					base.Player.AddBuff(ModContent.BuffType<HolyFlames>(), 200);
					break;
				case 7:
					base.Player.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 300);
					break;
				case 8:
					base.Player.AddBuff(ModContent.BuffType<Dragonfire>(), 150);
					break;
				case 9:
					base.Player.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
					break;
				}
			}
			else if (proj.type == 108)
			{
				base.Player.AddBuff(24, 600);
			}
			else if (proj.type == 99)
			{
				base.Player.AddBuff(36, 600);
			}
			else if (proj.type == 596)
			{
				base.Player.AddBuff(23, 180);
			}
			else if (proj.type == 814)
			{
				base.Player.AddBuff(ModContent.BuffType<BurningBlood>(), 240);
			}
			else if (proj.type == 811)
			{
				base.Player.AddBuff(ModContent.BuffType<BurningBlood>(), 180);
			}
			else if (proj.type == 129 && Main.zenithWorld)
			{
				base.Player.AddBuff(ModContent.BuffType<MiracleBlight>(), 600);
			}
			if (CalamityWorld.revenge)
			{
				if (proj.type == 96 || proj.type == 101)
				{
					base.Player.AddBuff(39, 60);
				}
				else if (proj.type == 277)
				{
					base.Player.AddBuff(70, 120);
				}
				else if (proj.type == 467)
				{
					base.Player.AddBuff(ModContent.BuffType<Daybroken>(), 180);
				}
				else if (proj.type == 466)
				{
					base.Player.AddBuff(144, 180);
				}
				else if (proj.type == 468)
				{
					base.Player.AddBuff(ModContent.BuffType<Shadowflame>(), 240);
				}
				else if (proj.type == 462 || proj.type == 452)
				{
					base.Player.AddBuff(ModContent.BuffType<Nightwither>(), 120);
				}
				else if (proj.type == 454)
				{
					base.Player.AddBuff(ModContent.BuffType<Nightwither>(), 180);
				}
				else if (proj.type == 455)
				{
					base.Player.AddBuff(ModContent.BuffType<Nightwither>(), 240);
				}
			}
		}
		if (NPC.AnyNPCs(ModContent.NPCType<THELORDE>()))
		{
			base.Player.AddBuff(ModContent.BuffType<NOU>(), 15);
		}
		OnHitByCombat(hurtInfo);
	}

	public void OnHitByCombat(Player.HurtInfo hurtInfo)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		if (theBee && shouldTriggerBeeCooldown)
		{
			shouldTriggerBeeCooldown = false;
			if (hurtInfo.Damage > 0)
			{
				theBeeCooldown = TheBee.CooldownLength;
			}
		}
		if (rOfResilienceCooldown == 0 && rOfResilienceEffect > 0)
		{
			CalamityUtils.AddCooldown(duration: rOfResilienceCooldown = (base.Player.Calamity().profanedSoulRelicBuff ? 300 : 600), p: base.Player, id: RelicOfResilienceCooldown.ID);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRockShieldActivate");
			style.Volume = 0.7f;
			style.Pitch = -0.1f;
			SoundEngine.PlaySound(in style, base.Player.Center);
		}
		if (alchFlask)
		{
			for (int i = 0; i < (base.Player.strongBees ? 12 : 9); i++)
			{
				int seekerDamage = (int)base.Player.GetBestClassDamage().ApplyTo(15f);
				Projectile projectile = Projectile.NewProjectileDirect(base.Player.GetSource_Accessory_OnHurt(FindAccessory<AlchemicalDecanter>(), hurtInfo.DamageSource), base.Player.Center, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.5f, 1.2f), ModContent.ProjectileType<BasicPlagueBee>(), seekerDamage, 0f, base.Player.whoAmI, -20f, 30f, 2f);
				projectile.ArmorPenetration = 35;
				projectile.penetrate = 6;
				projectile.extraUpdates = 2;
				projectile.timeLeft = 600;
			}
			base.Player.AddBuff(48, 900);
		}
		if (ursaSergeant)
		{
			ursaSergeantCooldown = MathHelper.Clamp(ursaSergeantCooldown - 180, 0, 300);
			base.Player.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 150);
			for (int j = 0; j < 9; j++)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Player.Center, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.5f, 1f), affectedByGravity: false, 20, Main.rand.NextFloat(0.5f, 1.1f), Main.rand.NextBool() ? Color.Coral : Color.DarkTurquoise));
				Dust dust = Dust.NewDustPerfect(base.Player.Center, 267, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.5f, 1f));
				dust.scale = Main.rand.NextFloat(0.75f, 1.2f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.Coral : Color.DarkTurquoise);
			}
		}
		if (!corrosiveSpine)
		{
			return;
		}
		int cloudCount = 3;
		for (int k = 0; k < cloudCount; k++)
		{
			float speed = 2f;
			int damage = 40;
			int cloud = Projectile.NewProjectile(base.Player.GetSource_Accessory_OnHurt(FindAccessory<CorrosiveSpine>(), hurtInfo.DamageSource), base.Player.Center, Vector2.One.RotatedByRandom(6.2831854820251465) * speed, ModContent.ProjectileType<ScourgeVenomCloud>(), damage, 0f, base.Player.whoAmI);
			if (cloud.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[cloud].DamageType = DamageClass.Generic;
			}
		}
	}

	public override bool FreeDodge(Player.HurtInfo info)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		if (info.Damage < 1)
		{
			return true;
		}
		if (silvaCountdown > 0 && hasSilvaEffect && silvaSet)
		{
			return true;
		}
		if (freeDodgeFromShieldAbsorption)
		{
			freeDodgeFromShieldAbsorption = false;
			LoseAdrenalineOnHurt(info, fullyAbsorbedByShield: true);
			return true;
		}
		Rectangle rect;
		if (gSabatonFalling)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if ((!base.Player.dontHurtCritters || !NPCID.Sets.CountsAsCritter[n.type]) && !n.dontTakeDamage && !n.friendly && n.Calamity().dashImmunityTime[base.Player.whoAmI] <= 0)
				{
					Rectangle npcHitbox = n.getRect();
					rect = base.Player.getRect();
					if (((Rectangle)(ref rect)).Intersects(npcHitbox) && (n.noTileCollide || Collision.CanHit(base.Player.position, base.Player.width, base.Player.height, n.position, n.width, n.height)))
					{
						int damage = base.Player.CalcIntDamage<MeleeDamageClass>(InterstellarStompers.PassthroughDamage);
						Projectile.NewProjectile(base.Player.GetSource_Accessory(FindAccessory<InterstellarStompers>()), n.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage, 0f, Main.myPlayer);
						n.Calamity().dashImmunityTime[base.Player.whoAmI] = 4;
						base.Player.GiveUniversalIFrames(InterstellarStompers.PassthroughIFrames);
						return true;
					}
				}
			}
		}
		if (rOfDelivarenceRam)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC n2 = enumerator2.Current;
				if ((!base.Player.dontHurtCritters || !NPCID.Sets.CountsAsCritter[n2.type]) && !n2.dontTakeDamage && !n2.friendly && n2.Calamity().dashImmunityTime[base.Player.whoAmI] <= 0)
				{
					Rectangle npcHitbox2 = n2.getRect();
					rect = base.Player.getRect();
					if (((Rectangle)(ref rect)).Intersects(npcHitbox2) && (n2.noTileCollide || Collision.CanHit(base.Player.position, base.Player.width, base.Player.height, n2.position, n2.width, n2.height)))
					{
						n2.Calamity().dashImmunityTime[base.Player.whoAmI] = 4;
						base.Player.GiveUniversalIFrames(InterstellarStompers.PassthroughIFrames);
						return true;
					}
				}
			}
		}
		storedShadowDodge = base.Player.shadowDodge;
		base.Player.shadowDodge = false;
		return base.FreeDodge(info);
	}

	public override bool ConsumableDodge(Player.HurtInfo info)
	{
		double dodgeDamageGateValuePercent = 0.05;
		int dodgeDamageGateValue = (int)Math.Round((double)base.Player.statLifeMax2 * dodgeDamageGateValuePercent);
		int actualDamageTaken = (chaliceOfTheBloodGod ? chaliceHitOriginalDamage : info.Damage);
		bool sufficientDamageForDodging = actualDamageTaken >= dodgeDamageGateValue;
		if (procDodgeEffects)
		{
			DodgeEffects.Add((Player _, Player.HurtInfo _) => (string?)null);
			GenericDodgeEffects();
			procDodgeEffects = false;
			return true;
		}
		if (spectralVeil && spectralVeilImmunity > 0)
		{
			SpectralVeilDodge();
			return true;
		}
		if (HandleDashDodges())
		{
			return true;
		}
		if (storedShadowDodge)
		{
			base.Player.ShadowDodge();
			storedShadowDodge = false;
			return true;
		}
		if ((!base.Player.HasCooldown(GlobalDodge.ID) & sufficientDamageForDodging) && DodgeEffects.Count > 0)
		{
			GenericDodgeEffects();
			return true;
		}
		if (base.Player.whoAmI != Main.myPlayer || disableAllDodges)
		{
			return false;
		}
		return base.ConsumableDodge(info);
		void GenericDodgeEffects()
		{
			double maxCooldownDurationDamagePercent = 0.5;
			int maxCooldownDurationDamageValue = (int)Math.Round((double)base.Player.statLifeMax2 * (maxCooldownDurationDamagePercent - dodgeDamageGateValuePercent));
			if (maxCooldownDurationDamageValue <= 0)
			{
				maxCooldownDurationDamageValue = 1;
			}
			float cooldownDurationScalar = MathHelper.Clamp((float)(actualDamageTaken - dodgeDamageGateValue) / (float)maxCooldownDurationDamageValue, 0f, 1f);
			float cooldownMultiplier = 1f;
			if (DodgeEffects.Count > 1)
			{
				cooldownMultiplier = MathF.Pow(BalancingConstants.DodgeCooldownMultPerStack, DodgeEffects.Count - 1);
			}
			string IconToUse = null;
			foreach (Func<Player, Player.HurtInfo, string> dodgeEffect in DodgeEffects)
			{
				string str = dodgeEffect(base.Player, info);
				if (str != null)
				{
					IconToUse = str;
				}
			}
			int cooldownDuration = (int)MathHelper.Lerp((float)ConsumableDodgeCooldown * cooldownMultiplier * BalancingConstants.DodgeCooldownDamageMult, (float)ConsumableDodgeCooldown * cooldownMultiplier, cooldownDurationScalar);
			if (IconToUse == null)
			{
				base.Player.AddCooldown(GlobalDodge.ID, cooldownDuration);
			}
			else
			{
				base.Player.AddCooldown(GlobalDodge.ID, cooldownDuration, true, IconToUse);
			}
		}
	}

	public override void ModifyHurt(ref Player.HurtModifiers modifiers)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		if (calcium)
		{
			modifiers.Knockback *= 1f - CalciumPotion.KnockbackResistance;
		}
		modifiers.ModifyHurtInfo += ModifyHurtInfo_Calamity;
		if (hurtSoundTimer == 0)
		{
			if (base.Player.Transformation().Type != -1 && base.Player.Transformation().currentTransformation.HurtSound(base.Player).HasValue)
			{
				(SoundStyle, int) hurtSound = base.Player.Transformation().currentTransformation.HurtSound(base.Player).Value;
				modifiers.DisableSound();
				SoundEngine.PlaySound(in hurtSound.Item1, base.Player.Center);
				hurtSoundTimer = hurtSound.Item2;
			}
			else if (roverDrive && RoverDriveShieldDurability > 0)
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(in RoverDrive.ShieldHurtSound, base.Player.Center);
				hurtSoundTimer = 20;
			}
			else if (lunicCorpsSet && LunicCorpsShieldDurability > 0)
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(in LunicCorpsHelmet.ShieldHurtSound, base.Player.Center);
				hurtSoundTimer = 20;
			}
			else if (sponge && SpongeShieldDurability > 0)
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(in TheSponge.ShieldHurtSound, base.Player.Center);
				hurtSoundTimer = 20;
			}
			else if (titanHeartSet)
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(in Atlas.HurtSound, base.Player.Center);
				hurtSoundTimer = 10;
			}
			else if (base.Player.GetModPlayer<WulfrumArmorPlayer>().wulfrumSet && (base.Player.name.ToLower() == "wagstaff" || base.Player.name.ToLower() == "john wulfrum"))
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(in SoundID.DSTMaleHurt, base.Player.Center);
				hurtSoundTimer = 10;
			}
		}
		double damageMult = 1.0;
		if (dArtifact)
		{
			damageMult += 0.15;
		}
		if (enraged)
		{
			damageMult += DemonshadeHelm.MultDamageTakenBoost;
		}
		modifiers.SourceDamage *= (float)damageMult;
		if (blazingCoreParry > 0)
		{
			if (blazingCoreParry >= 12)
			{
				if (!base.Player.HasCooldown(ParryCooldown.ID))
				{
					int blazingCoreParryIFrames = base.Player.ComputeParryIFrames();
					base.Player.GiveUniversalIFrames(blazingCoreParryIFrames, blink: true);
					blazingCoreEmpoweredParry = true;
					modifiers.Cancel();
					modifiers.DisableSound();
				}
				SoundEngine.PlaySound(in BlazingCore.ParrySuccessSound, base.Player.Center);
				float power = 2f;
				for (int i = 0; i < (int)(20f * power); i++)
				{
					if (Main.rand.NextBool())
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Player.Center, (new Vector2(19f, 19f) * power).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 47, Main.rand.NextFloat(1.15f, 1.3f) * power, Main.rand.NextBool(4) ? Color.Khaki : Color.Orange, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
						continue;
					}
					bool isSpark = Main.rand.NextBool(5);
					Dust dust = Dust.NewDustPerfect(base.Player.Center, isSpark ? 278 : ModContent.DustType<LightDust>(), (new Vector2(15f, 15f) * power).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f));
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(0.85f, 1.15f) * power * (isSpark ? 0.5f : 1f);
					dust.color = (Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod);
					if (isSpark)
					{
						dust.noGravity = false;
					}
					else
					{
						dust.noLightEmittence = true;
					}
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, Vector2.Zero, Color.Goldenrod, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1f, 0.8f), 0f, 0f, 0.14f * power, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, Vector2.Zero, Color.Khaki, "CalamityMod/Particles/BloomRing", new Vector2(1f, 0.5f), 0f, 0f, 2.1f * power, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				blazingCoreSuccessfulParry = 60;
				base.Player.AddCooldown(ParryCooldown.ID, 1800, false, "blazingcore");
			}
			if (blazingCoreParry > 1)
			{
				blazingCoreParry = 1;
			}
		}
		else if (flameLickedShellParry > 0 && flameLickedShellParry >= 12)
		{
			if (!base.Player.HasCooldown(ParryCooldown.ID))
			{
				int flameLickedShellParryIFrames = base.Player.ComputeParryIFrames();
				base.Player.GiveUniversalIFrames(flameLickedShellParryIFrames, blink: true);
				flameLickedShellEmpoweredParry = true;
				modifiers.FinalDamage *= 0.1f;
				modifiers.DisableSound();
			}
			SoundEngine.PlaySound(in ProfanedGuardianDefender.ShieldDeathSound, base.Player.Center);
			base.Player.AddCooldown(ParryCooldown.ID, 1200, false, "flamelickedshell");
			FlameLickedShell.handleParry(base.Player);
		}
		if (base.Player.Calamity().scionsCurio)
		{
			scionsCurioGotHit = true;
		}
	}

	private void ModifyHurtInfo_Calamity(ref Player.HurtInfo info)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		if (info.Cancelled)
		{
			return;
		}
		if (BossRushEvent.BossRushActive)
		{
			int bossRushDamageFloor = (Main.expertMode ? 160 : 100) + BossRushEvent.BossRushStage * 2;
			if (info.Damage < bossRushDamageFloor)
			{
				info.Damage += bossRushDamageFloor - info.Damage;
			}
		}
		bool shieldsFullyAbsorbedHit = false;
		if (HasAnyEnergyShield)
		{
			bool shieldsTookHit = false;
			bool anyShieldBroke = false;
			int totalDamageBlocked = 0;
			if (roverDrive && RoverDriveShieldDurability > 0 && !shieldsFullyAbsorbedHit)
			{
				bool num = RoverDriveShieldDurability >= info.Damage;
				int roverDriveDamageBlocked = Math.Min(RoverDriveShieldDurability, info.Damage);
				totalDamageBlocked += roverDriveDamageBlocked;
				RoverDriveShieldDurability -= info.Damage;
				shieldsTookHit = true;
				if (RoverDriveShieldDurability <= 0)
				{
					RoverDriveShieldDurability = 0;
					SoundEngine.PlaySound(in RoverDrive.BreakSound, base.Player.Center);
					base.Player.Calamity().GeneralScreenShakePower += (anyShieldBroke ? 0.5f : 2f);
					anyShieldBroke = true;
				}
				if (num)
				{
					shieldsFullyAbsorbedHit = true;
				}
				info.Damage -= roverDriveDamageBlocked;
			}
			if (lunicCorpsSet && LunicCorpsShieldDurability > 0 && !shieldsFullyAbsorbedHit)
			{
				bool num2 = LunicCorpsShieldDurability >= info.Damage;
				int masterChefDamageBlocked = Math.Min(LunicCorpsShieldDurability, info.Damage);
				totalDamageBlocked += masterChefDamageBlocked;
				LunicCorpsShieldDurability -= info.Damage;
				shieldsTookHit = true;
				if (LunicCorpsShieldDurability <= 0)
				{
					LunicCorpsShieldDurability = 0;
					SoundEngine.PlaySound(in LunicCorpsHelmet.BreakSound, base.Player.Center);
					base.Player.Calamity().GeneralScreenShakePower += (anyShieldBroke ? 0.5f : 2f);
					anyShieldBroke = true;
				}
				if (num2)
				{
					shieldsFullyAbsorbedHit = true;
				}
				info.Damage -= masterChefDamageBlocked;
			}
			if (pSoulArtifact && pSoulShieldDurability > 0 && !shieldsFullyAbsorbedHit)
			{
				bool num3 = pSoulShieldDurability >= info.Damage;
				int pSoulDamageBlocked = Math.Min(pSoulShieldDurability, info.Damage);
				totalDamageBlocked += pSoulDamageBlocked;
				pSoulShieldDurability -= info.Damage;
				shieldsTookHit = true;
				if (pSoulShieldDurability <= 0)
				{
					pSoulShieldDurability = 0;
					SoundEngine.PlaySound(in SoundID.DD2_BetsyFlameBreath, base.Player.Center);
					base.Player.Calamity().GeneralScreenShakePower += (anyShieldBroke ? 0.5f : 2f);
					anyShieldBroke = true;
				}
				if (num3)
				{
					shieldsFullyAbsorbedHit = true;
				}
				info.Damage -= pSoulDamageBlocked;
			}
			if (sponge && SpongeShieldDurability > 0 && !shieldsFullyAbsorbedHit)
			{
				bool num4 = SpongeShieldDurability >= info.Damage;
				int spongeDamageBlocked = Math.Min(SpongeShieldDurability, info.Damage);
				totalDamageBlocked += spongeDamageBlocked;
				SpongeShieldDurability -= info.Damage;
				shieldsTookHit = true;
				if (SpongeShieldDurability <= 0)
				{
					SpongeShieldDurability = 0;
					SoundEngine.PlaySound(in TheSponge.BreakSound, base.Player.Center);
					base.Player.Calamity().GeneralScreenShakePower += (anyShieldBroke ? 0.5f : 2f);
					anyShieldBroke = true;
				}
				if (num4)
				{
					shieldsFullyAbsorbedHit = true;
				}
				info.Damage -= spongeDamageBlocked;
			}
			if (Starshield > 0 && StratusStarburst > 0 && !shieldsFullyAbsorbedHit)
			{
				bool num5 = StratusStarburst >= info.Damage;
				int damageblocked = Math.Min(StratusStarburst, info.Damage);
				totalDamageBlocked += damageblocked;
				StratusStarburst -= info.Damage;
				shieldsTookHit = true;
				if (StratusStarburst <= 0)
				{
					StratusStarburst = 0;
					SoundEngine.PlaySound(in SoundID.DD2_CrystalCartImpact, base.Player.Center);
					base.Player.Calamity().GeneralScreenShakePower += (anyShieldBroke ? 0.5f : 2f);
					anyShieldBroke = true;
				}
				if (num5)
				{
					shieldsFullyAbsorbedHit = true;
				}
				info.Damage -= damageblocked;
			}
			if (shieldsTookHit)
			{
				string shieldDamageText = (-totalDamageBlocked).ToString();
				CombatText.NewText(new Rectangle((int)base.Player.position.X, (int)base.Player.position.Y - 16, base.Player.width, base.Player.height), Color.LightBlue, Language.GetTextValue(shieldDamageText));
				int shieldHitIFrames = base.Player.ComputeHitIFrames(info);
				base.Player.GiveIFrames(info.CooldownCounter, shieldHitIFrames, blink: true);
				if (pSoulArtifact)
				{
					for (int i = 0; i < Main.rand.Next(4, 8); i++)
					{
						Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 244);
						dust.velocity = Main.rand.NextVector2Circular(3.5f, 3.5f);
						dust.velocity.Y -= Main.rand.NextFloat(1f, 3f);
						dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
					}
				}
				else
				{
					int numParticles = Main.rand.Next(2, 6) + (anyShieldBroke ? 6 : 0);
					for (int j = 0; j < numParticles; j++)
					{
						float maxVelocity = (roverDrive ? 14f : 7f);
						Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(3f, maxVelocity);
						velocity.X += 5f * (float)info.HitDirection;
						float scale = Main.rand.NextFloat(2.5f, 3f);
						Color particleColor = (Main.rand.NextBool() ? new Color(99, 255, 229) : new Color(25, 132, 247));
						int lifetime = 25;
						GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(base.Player.Center, velocity, scale, particleColor, lifetime));
					}
				}
				if (roverDrive && cooldowns.TryGetValue(WulfrumRoverDriveDurability.ID, out var roverDriveDurabilityCD))
				{
					roverDriveDurabilityCD.timeLeft = RoverDriveShieldDurability;
				}
				if (lunicCorpsSet && cooldowns.TryGetValue(global::CalamityMod.Cooldowns.LunicCorpsShieldDurability.ID, out var masterChefDurabilityCD))
				{
					masterChefDurabilityCD.timeLeft = LunicCorpsShieldDurability;
				}
				if (pSoulArtifact && (!profanedCrystal || profanedCrystalBuffs) && cooldowns.TryGetValue(ProfanedSoulShield.ID, out var profanedSoulDurabilityCD))
				{
					profanedSoulDurabilityCD.timeLeft = pSoulShieldDurability;
				}
				if (sponge && cooldowns.TryGetValue(SpongeDurability.ID, out var spongeDurabilityCD))
				{
					spongeDurabilityCD.timeLeft = SpongeShieldDurability;
				}
			}
			if (roverDrive)
			{
				base.Player.AddCooldown(WulfrumRoverDriveRecharge.ID, RoverDrive.ShieldRechargeDelay);
			}
			if (lunicCorpsSet)
			{
				base.Player.AddCooldown(LunicCorpsShieldRecharge.ID, LunicCorpsHelmet.ShieldRechargeDelay);
			}
			if (pSoulArtifact && (!profanedCrystal || profanedCrystalBuffs))
			{
				base.Player.AddCooldown(ProfanedSoulShieldRecharge.ID, profanedCrystalBuffs ? 300 : 600);
			}
			if (sponge)
			{
				base.Player.AddCooldown(SpongeRecharge.ID, TheSponge.ShieldRechargeDelay);
			}
			if (shieldsFullyAbsorbedHit)
			{
				freeDodgeFromShieldAbsorption = true;
				nextHitDealsDefenseDamage = false;
			}
		}
		if (chaliceOfTheBloodGod && !shieldsFullyAbsorbedHit && info.Damage > ChaliceOfTheBloodGod.MinAllowedDamage)
		{
			chaliceBleedoutToApplyOnHurt = info.Damage - ChaliceOfTheBloodGod.MinAllowedDamage;
			chaliceHitOriginalDamage = info.Damage;
			info.Damage = ChaliceOfTheBloodGod.MinAllowedDamage;
		}
	}

	public override void OnHurt(Player.HurtInfo hurtInfo)
	{
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8a: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityWorld.armageddon && areThereAnyDamnBosses)
		{
			KillPlayer();
		}
		bool hasIFrames = base.Player.HasIFrames();
		bool num = (nextHitDealsDefenseDamage || bloodflareCore || moonshine) && !hasIFrames && !base.Player.creativeGodMode;
		bool externalFlagsAppropriate = !CalamityMod.ExternalFlag_DisableDefenseDamage && !externalDefenseDamageImmunity;
		Vector2 center;
		if (num & externalFlagsAppropriate)
		{
			double specialDefenseDmgMinimum = 0.0;
			double halfDefense = (double)(int)base.Player.statDefense / 2.0;
			if (bloodflareCore)
			{
				specialDefenseDmgMinimum += halfDefense;
			}
			if (moonshine)
			{
				specialDefenseDmgMinimum += halfDefense;
			}
			double standardDefenseDamage = (double)(hurtInfo.SourceDamage - hurtInfo.Damage) * defenseDamageRatio;
			if (specialDefenseDmgMinimum > 0.0 && standardDefenseDamage < specialDefenseDmgMinimum)
			{
				DealDefenseDamage((int)specialDefenseDmgMinimum, absolute: true);
				if (bloodflareCore && (double)bloodflareCoreRemainingHealOverTime < specialDefenseDmgMinimum)
				{
					bloodflareCoreRemainingHealOverTime = (int)specialDefenseDmgMinimum;
				}
				SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, base.Player.Center);
				Vector2 dustVel = default(Vector2);
				for (int i = 0; i < 36; i++)
				{
					float speed = Main.rand.NextFloat(1.8f, 8f);
					((Vector2)(ref dustVel))._002Ector(speed, speed);
					Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 90);
					dust.velocity = dustVel;
					dust.noGravity = true;
					dust.scale *= Main.rand.NextFloat(1.1f, 1.4f);
					Dust dust2 = DustExtensions.BetterCloneDust(dust);
					Vector2 spinningpoint = dustVel;
					center = default(Vector2);
					dust2.velocity = spinningpoint.RotatedBy(1.5707963705062866, center);
					Dust dust3 = DustExtensions.BetterCloneDust(dust);
					Vector2 spinningpoint2 = dustVel;
					center = default(Vector2);
					dust3.velocity = spinningpoint2.RotatedBy(3.1415927410125732, center);
					Dust dust4 = DustExtensions.BetterCloneDust(dust);
					Vector2 spinningpoint3 = dustVel;
					center = default(Vector2);
					dust4.velocity = spinningpoint3.RotatedBy(4.71238899230957, center);
				}
			}
			else if (chaliceOfTheBloodGod)
			{
				DealDefenseDamage(hurtInfo, chaliceBleedoutToApplyOnHurt);
			}
			else
			{
				DealDefenseDamage(hurtInfo);
			}
		}
		nextHitDealsDefenseDamage = false;
		if (chaliceOfTheBloodGod)
		{
			int bleedoutToApply = chaliceBleedoutToApplyOnHurt;
			chaliceBleedoutBuffer += bleedoutToApply;
			string text = $"({-bleedoutToApply})";
			CombatText.NewText(new Rectangle((int)base.Player.position.X + 4, (int)base.Player.position.Y - 3, base.Player.width - 4, base.Player.height - 4), ChaliceOfTheBloodGod.BleedoutBufferDamageTextColor, Language.GetTextValue(text), dramatic: false, dot: true);
		}
		if (shatteredCommunity && rageGainCooldown == 0)
		{
			float HPRatio = (float)hurtInfo.SourceDamage / (float)base.Player.statLifeMax2;
			float rageConversionRatio = 0.8f;
			if (rageModeActive)
			{
				rageConversionRatio *= 0.5f;
			}
			if (rage >= rageMax)
			{
				rageConversionRatio *= 3f / (3f + rage / rageMax);
			}
			rage += rageMax * HPRatio * rageConversionRatio;
			rageGainCooldown = ShatteredCommunity.RageGainCooldown;
		}
		if (RageEnabled)
		{
			rageCombatFrames = BalancingConstants.RageCombatDelayTime;
		}
		if (hideOfDeus)
		{
			hideOfDeusMeleeBoostTimer += 3 * hurtInfo.Damage;
			if (hideOfDeusMeleeBoostTimer > 600)
			{
				hideOfDeusMeleeBoostTimer = 600;
			}
		}
		if (base.Player.whoAmI == Main.myPlayer)
		{
			if (base.Player.Calamity().persecutedEnchant && NPC.CountNPCS(ModContent.NPCType<DemonPortal>()) < 2)
			{
				int tries = 0;
				Vector2 spawnPositionOffset = Vector2.One * 24f;
				Vector2 spawnPosition;
				do
				{
					spawnPosition = base.Player.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(270f, 420f);
					tries++;
				}
				while (Collision.SolidCollision(spawnPosition - spawnPositionOffset, 48, 24) && tries < 100);
				CalamityNetcode.NewNPC_ClientSide(spawnPosition, ModContent.NPCType<DemonPortal>(), base.Player);
			}
			if (daedalusAbsorb && Main.rand.NextBool(DaedalusHeadMagic.AbsorptionChanceDenominator))
			{
				int healAmt = (int)((float)hurtInfo.Damage * DaedalusHeadMagic.DamageAbsorptionPercent);
				base.Player.HealPlayer(healAmt);
			}
			if (absorber)
			{
				int healAmt2 = (int)((float)hurtInfo.Damage * TheAbsorber.DamageTakenHealedPercent);
				base.Player.HealPlayer(healAmt2);
			}
			if (witheringDamageDone > 0)
			{
				double healCompenstationRatio = Math.Log(witheringDamageDone) * Math.Pow(witheringDamageDone, 2.0 / 3.0) / 177000.0;
				if (healCompenstationRatio > 1.0)
				{
					healCompenstationRatio = 1.0;
				}
				_ = hurtInfo.Damage;
				base.Player.HealPlayer((int)(healCompenstationRatio * (double)hurtInfo.Damage));
				base.Player.AddBuff(ModContent.BuffType<Withered>(), 1080);
				witheringDamageDone = 0;
			}
			if (AdrenalineEnabled)
			{
				LoseAdrenalineOnHurt(hurtInfo);
			}
			if (evilSmasherBoost > 0)
			{
				evilSmasherBoost--;
			}
			if (trinketOfChi)
			{
				chiBuffTimer = 0;
			}
			if (amidiasBlessing && (chaliceOfTheBloodGod ? chaliceBleedoutToApplyOnHurt : hurtInfo.Damage) > 50)
			{
				base.Player.ClearBuff(ModContent.BuffType<AmidiasBlessing>());
				SoundEngine.PlaySound(in SoundID.Item96, base.Player.Center);
			}
			if (gShell)
			{
				if (giantShellPostHit == 0)
				{
					float numberOfDusts = 35f;
					float rotFactor = 360f / numberOfDusts;
					for (int j = 0; (float)j < numberOfDusts; j++)
					{
						float rot = MathHelper.ToRadians((float)j * rotFactor);
						Vector2 spinningpoint4 = new Vector2(Main.rand.NextFloat(0.5f, 2.5f), 0f);
						double radians = rot * Main.rand.NextFloat(1.1f, 9.1f);
						center = default(Vector2);
						Vector2 offset = Utils.RotatedBy(spinningpoint4, radians, center);
						Vector2 spinningpoint5 = new Vector2(Main.rand.NextFloat(0.5f, 2.5f), 0f);
						double radians2 = rot * Main.rand.NextFloat(1.1f, 9.1f);
						center = default(Vector2);
						Vector2 velOffset = Utils.RotatedBy(spinningpoint5, radians2, center);
						Dust dust5 = Dust.NewDustPerfect(base.Player.Center + offset, Main.rand.NextBool() ? 249 : 118, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
						dust5.noGravity = false;
						dust5.velocity = velOffset;
						dust5.scale = Main.rand.NextFloat(1.5f, 1.2f);
					}
				}
				giantShellPostHit = 180;
			}
			if (tortShell)
			{
				if (tortShellPostHit == 0)
				{
					float numberOfDusts2 = 43f;
					float rotFactor2 = 360f / numberOfDusts2;
					for (int k = 0; (float)k < numberOfDusts2; k++)
					{
						float rot2 = MathHelper.ToRadians((float)k * rotFactor2);
						Vector2 spinningpoint6 = new Vector2(Main.rand.NextFloat(0.5f, 3.1f), 0f);
						double radians3 = rot2 * Main.rand.NextFloat(1.1f, 9.1f);
						center = default(Vector2);
						Vector2 offset2 = Utils.RotatedBy(spinningpoint6, radians3, center);
						Vector2 spinningpoint7 = new Vector2(Main.rand.NextFloat(0.5f, 3.1f), 0f);
						double radians4 = rot2 * Main.rand.NextFloat(1.1f, 9.1f);
						center = default(Vector2);
						Vector2 velOffset2 = Utils.RotatedBy(spinningpoint7, radians4, center);
						Dust dust6 = Dust.NewDustPerfect(base.Player.Center + offset2, Main.rand.NextBool() ? 215 : 22, (Vector2?)new Vector2(velOffset2.X, velOffset2.Y), 0, default(Color), 1f);
						dust6.noGravity = false;
						dust6.velocity = velOffset2;
						dust6.scale = Main.rand.NextFloat(1.6f, 2.2f);
					}
				}
				tortShellPostHit = 180;
			}
			if (aquaticHeartIce)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath7, base.Player.Center);
				base.Player.AddCooldown(AquaticHeartIceShield.ID, AquaticHeart.IceShieldCooldown);
				for (int d = 0; d < 10; d++)
				{
					Dust ice = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 67, 0f, 0f, 100, default(Color), 2f);
					ice.velocity *= 3f;
					if (Main.rand.NextBool())
					{
						ice.scale = 0.5f;
						ice.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int l = 0; l < 15; l++)
				{
					Dust dust7 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 67, 0f, 0f, 100, default(Color), 3f);
					dust7.noGravity = true;
					dust7.velocity *= 5f;
					Dust dust8 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 67, 0f, 0f, 100, default(Color), 2f);
					dust8.velocity *= 2f;
				}
			}
			if (tarraMelee)
			{
				base.Player.AddBuff(ModContent.BuffType<TarraLifeRegen>(), TarragonHeadMelee.TarraLifeDuration);
			}
			else if (xerocSet)
			{
				base.Player.AddBuff(ModContent.BuffType<EmpyreanWrath>(), EmpyreanMask.WrathDuration);
			}
			else if (reaverDefense)
			{
				base.Player.AddBuff(ModContent.BuffType<ReaverRage>(), ReaverHeadTank.ReaverRageDuration);
			}
			if (fBarrier || (aquaticHeart && NPC.downedBoss3))
			{
				SoundEngine.PlaySound(in SoundID.Item27, base.Player.Center);
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (npc.friendly || npc.dontTakeDamage)
					{
						continue;
					}
					center = npc.Center - base.Player.Center;
					float num2 = ((Vector2)(ref center)).Length();
					float freezeDist = 300 + hurtInfo.Damage * 2;
					if (freezeDist > 500f)
					{
						freezeDist = 500f + (freezeDist - 500f) * 0.5f;
					}
					if (num2 < freezeDist)
					{
						float duration = Main.rand.Next(10 + hurtInfo.Damage / 2, 20 + hurtInfo.Damage);
						if (duration > 120f)
						{
							duration = 120f;
						}
						npc.AddBuff(ModContent.BuffType<GlacialState>(), (int)duration);
					}
				}
			}
			if (aBrain || amalgam)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC npc2 = enumerator2.Current;
					if (!npc2.friendly && !npc2.dontTakeDamage)
					{
						center = npc2.Center - base.Player.Center;
						float num3 = ((Vector2)(ref center)).Length();
						float range = Main.rand.Next(200 + hurtInfo.Damage / 2, 301 + hurtInfo.Damage * 2);
						if (range > 500f)
						{
							range = 500f + (range - 500f) * 0.75f;
						}
						if (range > 700f)
						{
							range = 700f + (range - 700f) * 0.5f;
						}
						if (range > 900f)
						{
							range = 900f + (range - 900f) * 0.25f;
						}
						if (num3 < range)
						{
							int duration2 = Main.rand.Next(300 + hurtInfo.Damage / 3, 480 + hurtInfo.Damage / 2);
							npc2.AddBuff(31, duration2);
						}
					}
				}
				Projectile.NewProjectile(base.Player.GetSource_Accessory_OnHurt(amalgam ? FindAccessory<TheAmalgam>() : FindAccessory<AmalgamatedBrain>(), hurtInfo.DamageSource), base.Player.Center.X + (float)Main.rand.Next(-40, 40), base.Player.Center.Y - (float)Main.rand.Next(20, 60), base.Player.velocity.X * 0.3f, base.Player.velocity.Y * 0.3f, 565, 0, 0f, base.Player.whoAmI);
			}
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<DrataliornusBow>()] != 0)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Projectile p = enumerator3.Current;
				if (p.type == ModContent.ProjectileType<DrataliornusBow>() && p.owner == base.Player.whoAmI)
				{
					p.Kill();
					break;
				}
			}
			if (base.Player.wingTime > (float)(base.Player.wingTimeMax / 2))
			{
				base.Player.wingTime = base.Player.wingTimeMax / 2;
			}
		}
		if (base.Player.wingsLogic != 8)
		{
			return;
		}
		if (!Main.dedServ && base.Player.wingTime > 0f)
		{
			IEntitySource source = base.Player.GetSource_Accessory_OnHurt(FindAccessory(786), hurtInfo.DamageSource);
			for (int m = 0; m < 6; m++)
			{
				Vector2 boneVelocity = Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(1.5f, 2.5f);
				Gore.NewGoreDirect(source, base.Player.Center, boneVelocity, 57, Main.rand.NextFloat(0.6f, 0.9f)).timeLeft = Main.rand.Next(6, 31);
			}
		}
		base.Player.wingTime /= 2f;
	}

	public override void PostHurt(Player.HurtInfo hurtInfo)
	{
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		if (silverMedkit && (double)hurtInfo.Damage >= 20.0)
		{
			silverMedkitTimer = 120;
		}
		if (dAmulet)
		{
			base.Player.AddBuff(48, 300, quiet: false);
		}
		base.Player.Calamity().GemTechState.PlayerOnHitEffects(hurtInfo.Damage);
		if (base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		int iFramesToAdd = base.Player.GetExtraHitIFrames(hurtInfo);
		if (hurtInfo.CooldownCounter != -1)
		{
			base.Player.hurtCooldowns[hurtInfo.CooldownCounter] += iFramesToAdd;
		}
		else
		{
			base.Player.immuneTime += iFramesToAdd;
		}
		if (hurtInfo.CooldownCounter != -1 && hurtInfo.CooldownCounter != 1)
		{
			return;
		}
		if (aeroSet && hurtInfo.Damage > AerospecBreastplate.SetBonusHurtDamageThreshold)
		{
			IEntitySource source = base.Player.GetSource_OnHurt(hurtInfo.DamageSource, AerospecBreastplate.FeatherEntitySourceContext);
			int featherDamage = (int)base.Player.GetBestClassDamage().ApplyTo(AerospecBreastplate.SetBonusFeatherDamage);
			for (int n = 0; n < 4; n++)
			{
				CalamityUtils.ProjectileRain(source, base.Player.Center, 400f, 100f, 500f, 800f, 20f, ModContent.ProjectileType<StickyFeatherAero>(), featherDamage, 1f, base.Player.whoAmI);
			}
		}
		if (hideOfDeus)
		{
			IEntitySource source_Accessory_OnHurt = base.Player.GetSource_Accessory_OnHurt(FindAccessory<HideofAstrumDeus>(), hurtInfo.DamageSource);
			SoundEngine.PlaySound(in SoundID.Item74, base.Player.Center);
			Projectile.NewProjectile(Damage: (int)base.Player.GetBestClassDamage().ApplyTo(HideofAstrumDeus.BlazeDamage), spawnSource: source_Accessory_OnHurt, X: base.Player.Center.X, Y: base.Player.Center.Y, SpeedX: 0f, SpeedY: 0f, Type: ModContent.ProjectileType<HideOfAstrumDeusExplosion>(), KnockBack: 5f, Owner: base.Player.whoAmI, ai0: 0f, ai1: 1f);
		}
		if (dAmulet)
		{
			IEntitySource source2 = base.Player.GetSource_Accessory_OnHurt(FindAccessory<DeificAmulet>(), hurtInfo.DamageSource);
			int projAmount = (rampartOfDeities ? 12 : 6);
			for (int i = 0; i < projAmount; i++)
			{
				int deificProjDamage = (int)(base.Player.GetBestClassDamage().ApplyTo(DeificAmulet.StarDamage) * (base.Player.strongBees ? 0.85f : 1f));
				Projectile onHitProj = Main.projectile[Projectile.NewProjectile(source2, base.Player.Center, Utils.RotatedBy(new Vector2(0f, -15f * ((rampartOfDeities && i % 2 == 0) ? 0.75f : 1.25f)), (double)((float)Math.PI * 2f / (float)projAmount * (float)i), default(Vector2)), ModContent.ProjectileType<AstralStar>(), deificProjDamage, 4f, base.Player.whoAmI)];
				if (onHitProj.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					onHitProj.DamageType = DamageClass.Generic;
					onHitProj.usesLocalNPCImmunity = true;
					onHitProj.localNPCHitCooldown = 30;
					onHitProj.tileCollide = false;
					onHitProj.extraUpdates = 1;
					onHitProj.Calamity().conditionalHomingRange = 600f;
					if (base.Player.strongBees)
					{
						onHitProj.penetrate++;
					}
				}
			}
		}
		if (ilSpark)
		{
			IEntitySource source3 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<HideofAstrumDeus>()));
			if (hurtInfo.Damage > 0)
			{
				SoundEngine.PlaySound(in SoundID.Item93, base.Player.Center);
				float spread = 0.783f;
				double startAngle = Math.Atan2(base.Player.velocity.X, base.Player.velocity.Y) - (double)(spread / 2f);
				double deltaAngle = spread / 8f;
				int sDamage = 6;
				if (transformer)
				{
					sDamage += 42;
				}
				sDamage = (int)base.Player.GetBestClassDamage().ApplyTo(sDamage);
				if (base.Player.whoAmI == Main.myPlayer)
				{
					for (int j = 0; j < 4; j++)
					{
						double offsetAngle = startAngle + deltaAngle * (double)(j + j * j) / 2.0 + (double)(32f * (float)j);
						int spark1 = Projectile.NewProjectile(source3, base.Player.Center.X, base.Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5.0), (float)(Math.Cos(offsetAngle) * 5.0), ModContent.ProjectileType<GenericElectricSpark>(), sDamage, 1.25f, base.Player.whoAmI, 0f, 1f);
						int spark2 = Projectile.NewProjectile(source3, base.Player.Center.X, base.Player.Center.Y, (float)((0.0 - Math.Sin(offsetAngle)) * 5.0), (float)((0.0 - Math.Cos(offsetAngle)) * 5.0), ModContent.ProjectileType<GenericElectricSpark>(), sDamage, 1.25f, base.Player.whoAmI, 0f, 1f);
						if (spark1.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[spark1].timeLeft = 120;
						}
						if (spark2.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[spark2].timeLeft = 120;
						}
					}
				}
			}
		}
		if (rBrain && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<ShadeNimbus>()) && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<ShadeNimbusSpawner>()))
		{
			IEntitySource source_Accessory_OnHurt2 = base.Player.GetSource_Accessory_OnHurt(amalgam ? FindAccessory<TheAmalgam>() : (aBrain ? FindAccessory<AmalgamatedBrain>() : FindAccessory<RottenBrain>()), hurtInfo.DamageSource);
			int effectStrength = (amalgam ? 3 : ((!aBrain) ? 1 : 2));
			int effectDamage = (amalgam ? TheAmalgam.NimbusDamage : (aBrain ? AmalgamatedBrain.NimbusDamage : RottenBrain.NimbusDamage));
			Projectile.NewProjectile(Damage: (int)base.Player.GetBestClassDamage().ApplyTo(effectDamage), velocity: -Vector2.UnitY.RotatedByRandom(0.07853981852531433) * 12.5f, spawnSource: source_Accessory_OnHurt2, position: base.Player.Center, Type: ModContent.ProjectileType<ShadeNimbusSpawner>(), KnockBack: 0f, Owner: base.Player.whoAmI, ai0: 0f, ai1: 0f, ai2: effectStrength);
		}
		if (inkBomb && !abyssalMirror && !eclipseMirror && base.Player.whoAmI == Main.myPlayer)
		{
			if (!base.Player.HasCooldown(global::CalamityMod.Cooldowns.InkBomb.ID))
			{
				base.Player.AddCooldown(global::CalamityMod.Cooldowns.InkBomb.ID, CalamityUtils.SecondsToFrames(20));
				rogueStealth += 0.5f;
				SoundStyle style = SoundID.NPCDeath28 with
				{
					Volume = 2f
				};
				SoundEngine.PlaySound(in style, base.Player.Center);
			}
			IEntitySource source4 = base.Player.GetSource_Accessory_OnHurt(FindAccessory<global::CalamityMod.Items.Accessories.InkBomb>(), hurtInfo.DamageSource);
			SoundEngine.PlaySound(in SoundID.Item1, base.Player.Center);
			for (int k = 0; k < 3; k++)
			{
				int ink = Projectile.NewProjectile(source4, base.Player.Center, Vector2.One.RotatedByRandom(6.2831854820251465) * 2f, ModContent.ProjectileType<InkBombProjectile>(), 0, 0f, base.Player.whoAmI);
				if (ink.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[ink].DamageType = DamageClass.Generic;
				}
			}
		}
		if (ataxiaBlaze)
		{
			IEntitySource fuckYouBitch = base.Player.GetSource_OnHurt(hurtInfo.DamageSource);
			if (hurtInfo.Damage > 0)
			{
				SoundEngine.PlaySound(in SoundID.Item74, base.Player.Center);
				int eDamage = (int)base.Player.GetBestClassDamage().ApplyTo(HydrothermicArmor.BlazeDamage);
				if (base.Player.whoAmI == Main.myPlayer)
				{
					Projectile.NewProjectile(fuckYouBitch, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<DeepseaBlaze>(), eDamage, 1f, base.Player.whoAmI);
				}
			}
		}
		else if (daedalusShard)
		{
			if (hurtInfo.Damage <= 0)
			{
				return;
			}
			SoundEngine.PlaySound(in SoundID.Item27, base.Player.Center);
			if (base.Player.whoAmI != Main.myPlayer)
			{
				return;
			}
			IEntitySource source5 = base.Player.GetSource_OnHurt(hurtInfo.DamageSource);
			float offset = Main.rand.NextFloat((float)Math.PI * 2f);
			int sDamage2 = (int)base.Player.GetTotalDamage<RangedDamageClass>().ApplyTo(DaedalusHeadRanged.ShardDamage);
			for (int l = 0; l < 10; l++)
			{
				Vector2 circleVel = ((float)Math.PI * 2f * (float)l / 10f + offset).ToRotationVector2() * Main.rand.NextFloat(5f, 8f);
				int shard = Projectile.NewProjectile(source5, base.Player.Center, circleVel, 90, sDamage2, 1f, base.Player.whoAmI);
				if (shard.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[shard].DamageType = DamageClass.Generic;
				}
			}
		}
		else if (godSlayerDamage)
		{
			IEntitySource source6 = base.Player.GetSource_OnHurt(hurtInfo.DamageSource);
			if (hurtInfo.Damage <= GodSlayerHeadMelee.SetBonusHurtDamageThreshold)
			{
				return;
			}
			SoundEngine.PlaySound(in SoundID.Item73, base.Player.Center);
			float spread2 = 0.783f;
			double startAngle2 = Math.Atan2(base.Player.velocity.X, base.Player.velocity.Y) - (double)(spread2 / 2f);
			double deltaAngle2 = spread2 / 8f;
			int shrapnelDamage = base.Player.CalcIntDamage<MeleeDamageClass>(GodSlayerHeadMelee.DartDamage);
			if (base.Player.whoAmI == Main.myPlayer)
			{
				for (int m = 0; m < 4; m++)
				{
					double offsetAngle2 = startAngle2 + deltaAngle2 * (double)(m + m * m) / 2.0 + (double)(32f * (float)m);
					Projectile.NewProjectile(source6, base.Player.Center.X, base.Player.Center.Y, (float)(Math.Sin(offsetAngle2) * 5.0), (float)(Math.Cos(offsetAngle2) * 5.0), ModContent.ProjectileType<GodKiller>(), shrapnelDamage, 5f, base.Player.whoAmI);
					Projectile.NewProjectile(source6, base.Player.Center.X, base.Player.Center.Y, (float)((0.0 - Math.Sin(offsetAngle2)) * 5.0), (float)((0.0 - Math.Cos(offsetAngle2)) * 5.0), ModContent.ProjectileType<GodKiller>(), shrapnelDamage, 5f, base.Player.whoAmI);
				}
			}
		}
		else
		{
			if (!dsSetBonus || base.Player.whoAmI != Main.myPlayer)
			{
				return;
			}
			IEntitySource source7 = base.Player.GetSource_OnHurt(hurtInfo.DamageSource, DemonshadeHelm.ShadowScytheEntitySourceContext);
			for (int num = 0; num < 2; num++)
			{
				int shadowbeamDamage = (int)base.Player.GetBestClassDamage().ApplyTo(DemonshadeHelm.BeamDamage);
				Projectile beam = CalamityUtils.ProjectileRain(source7, base.Player.Center, 400f, 100f, 500f, 800f, 22f, 294, shadowbeamDamage, 7f, base.Player.whoAmI);
				if (beam.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					beam.DamageType = DamageClass.Generic;
					beam.usesLocalNPCImmunity = true;
					beam.localNPCHitCooldown = 10;
				}
			}
			for (int num2 = 0; num2 < 5; num2++)
			{
				int scytheDamage = (int)base.Player.GetBestClassDamage().ApplyTo(DemonshadeHelm.ScytheDamage);
				Projectile scythe = CalamityUtils.ProjectileRain(source7, base.Player.Center, 400f, 100f, 500f, 800f, 22f, 45, scytheDamage, 7f, base.Player.whoAmI);
				if (scythe.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					scythe.DamageType = DamageClass.Generic;
					scythe.usesLocalNPCImmunity = true;
					scythe.localNPCHitCooldown = 10;
				}
			}
		}
	}

	public void KillPlayer()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = base.Player.GetSource_Death();
		base.Player.lastDeathPostion = base.Player.Center;
		base.Player.lastDeathTime = DateTime.Now;
		base.Player.showLastDeath = true;
		int coinsOwned = (int)Utils.CoinsCount(out var _, base.Player.inventory);
		if (Main.myPlayer == base.Player.whoAmI)
		{
			base.Player.lostCoins = coinsOwned;
			base.Player.lostCoinString = Main.ValueToCoins(base.Player.lostCoins);
		}
		if (Main.myPlayer == base.Player.whoAmI)
		{
			Main.mapFullscreen = false;
		}
		if (Main.myPlayer == base.Player.whoAmI)
		{
			base.Player.trashItem.SetDefaults(0, false, null);
			if (base.Player.difficulty == 0 || base.Player.difficulty == 3)
			{
				for (int i = 0; i < 59; i++)
				{
					if (base.Player.inventory[i].stack > 0 && ((base.Player.inventory[i].type >= 1522 && base.Player.inventory[i].type <= 1527) || base.Player.inventory[i].type == 3643))
					{
						int droppedLargeGem = Item.NewItem(source, (int)base.Player.position.X, (int)base.Player.position.Y, base.Player.width, base.Player.height, base.Player.inventory[i].type);
						Main.item[droppedLargeGem].netDefaults(base.Player.inventory[i].netID);
						Main.item[droppedLargeGem].Prefix(base.Player.inventory[i].prefix);
						Main.item[droppedLargeGem].stack = base.Player.inventory[i].stack;
						Main.item[droppedLargeGem].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
						Main.item[droppedLargeGem].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
						Main.item[droppedLargeGem].noGrabDelay = 100;
						Main.item[droppedLargeGem].favorited = false;
						Main.item[droppedLargeGem].newAndShiny = false;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, null, droppedLargeGem);
						}
						base.Player.inventory[i].SetDefaults(0, false, null);
					}
				}
			}
			else if (base.Player.difficulty == 1)
			{
				base.Player.DropItems();
			}
			else if (base.Player.difficulty == 2)
			{
				base.Player.DropItems();
				base.Player.KillMeForGood();
			}
		}
		SoundEngine.PlaySound(in SoundID.PlayerKilled, base.Player.Center);
		base.Player.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
		base.Player.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
		base.Player.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
		base.Player.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + 0f;
		base.Player.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + 0f;
		base.Player.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + 0f;
		if (base.Player.stoned)
		{
			base.Player.headPosition = Vector2.Zero;
			base.Player.bodyPosition = Vector2.Zero;
			base.Player.legPosition = Vector2.Zero;
		}
		for (int j = 0; j < 100; j++)
		{
			Dust.NewDust(base.Player.position, base.Player.width, base.Player.height, 235, 0f, -2f);
		}
		base.Player.mount.Dismount(base.Player);
		base.Player.dead = true;
		base.Player.respawnTimer = 600;
		if (Main.expertMode)
		{
			base.Player.respawnTimer = (int)((double)base.Player.respawnTimer * 1.5);
		}
		base.Player.immuneAlpha = 0;
		base.Player.palladiumRegen = false;
		base.Player.iceBarrier = false;
		base.Player.crystalLeaf = false;
		PlayerDeathReason damageSource = PlayerDeathReason.ByOther(base.Player.Male ? 14 : 15);
		if (abyssDeath)
		{
			SoundEngine.PlaySound(in DrownSound, base.Player.Center);
			damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.AbyssDrown" + Main.rand.Next(1, 4)).ToNetworkText(base.Player.name));
		}
		else if (CalamityWorld.armageddon && areThereAnyDamnBosses)
		{
			damageSource = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Armageddon").ToNetworkText(base.Player.name));
		}
		NetworkText deathText = damageSource.GetDeathText(base.Player.name);
		if (Main.netMode == 1 && base.Player.whoAmI == Main.myPlayer)
		{
			NetMessage.SendPlayerDeath(base.Player.whoAmI, damageSource, 1000, 0, pvp: false);
		}
		if (Main.dedServ)
		{
			ChatHelper.BroadcastChatMessage(deathText, new Color(225, 25, 25));
		}
		else if (Main.netMode == 0)
		{
			Main.NewText(deathText.ToString(), 225, 25, 25);
		}
		if (base.Player.whoAmI == Main.myPlayer && (base.Player.difficulty == 0 || base.Player.difficulty == 3))
		{
			base.Player.DropCoins();
		}
		base.Player.DropTombstone(coinsOwned, deathText, 0);
		if (base.Player.whoAmI == Main.myPlayer)
		{
			try
			{
				WorldGen.saveToonWhilePlaying();
			}
			catch
			{
			}
		}
	}

	public void DealDefenseDamage(Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0 && hurtInfo.SourceDamage > 0)
		{
			int netMitigation = hurtInfo.SourceDamage - hurtInfo.Damage;
			int incomingDamageToUse = ((netMitigation > 0) ? netMitigation : 0);
			DealDefenseDamage(incomingDamageToUse);
		}
	}

	public void DealDefenseDamage(Player.HurtInfo hurtInfo, int bleedoutApplied)
	{
		if (hurtInfo.Damage > 0 && hurtInfo.SourceDamage > 0)
		{
			int netMitigation = hurtInfo.SourceDamage - (hurtInfo.Damage + bleedoutApplied);
			int incomingDamageToUse = ((netMitigation > 0) ? netMitigation : 0);
			DealDefenseDamage(incomingDamageToUse);
		}
	}

	public void DealDefenseDamage(int incomingDamage, bool absolute = false)
	{
		double ratioToUse = (absolute ? 1.0 : defenseDamageRatio);
		int defenseDamageTaken = (int)Math.Round((double)incomingDamage * ratioToUse);
		if (areThereAnyDamnBosses && !absolute)
		{
			int defenseDamageFloor = CalamityUtils.GetDefenseDamageFloor();
			if (defenseDamageTaken < defenseDamageFloor)
			{
				defenseDamageTaken = defenseDamageFloor;
			}
		}
		ApplyDefenseDamageInternal(defenseDamageTaken);
	}

	private void ApplyDefenseDamageInternal(int defenseDamage, bool showVisuals = true)
	{
		if (defenseDamage <= 0 || CalamityMod.ExternalFlag_DisableDefenseDamage || externalDefenseDamageImmunity)
		{
			return;
		}
		int defenseDamageTaken = defenseDamage;
		if (AdamantiteSetDefenseBoost > 0)
		{
			int defenseDamageToAdamantite = Math.Min(AdamantiteSetDefenseBoost, defenseDamageTaken);
			AdamantiteSetDefenseBoost -= defenseDamageToAdamantite;
			defenseDamageTaken -= defenseDamageToAdamantite;
			if (defenseDamageTaken <= 0)
			{
				ShowDefenseDamageEffects(defenseDamageToAdamantite);
				return;
			}
		}
		int previousDefenseDamage = CurrentDefenseDamage;
		totalDefenseDamage = previousDefenseDamage + defenseDamageTaken;
		if (defenseDamageRecoveryFrames < 0)
		{
			defenseDamageRecoveryFrames = 0;
		}
		int baseTime = 60 * ((!moonshine) ? 1 : 3);
		totalDefenseDamageRecoveryFrames = defenseDamageRecoveryFrames + baseTime;
		if (totalDefenseDamageRecoveryFrames > 900)
		{
			totalDefenseDamageRecoveryFrames = 900;
		}
		defenseDamageRecoveryFrames = totalDefenseDamageRecoveryFrames;
		defenseDamageDelayFrames = 10;
		if (showVisuals)
		{
			ShowDefenseDamageEffects(defenseDamage);
		}
	}

	private void ShowDefenseDamageEffects(int defenseDamage)
	{
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		if (hurtSoundTimer == 0 && Main.myPlayer == base.Player.whoAmI)
		{
			double maxVolumeDefenseDamageScalar = (Main.masterMode ? 0.7 : (CalamityWorld.death ? 0.6 : (CalamityWorld.revenge ? 0.55 : (Main.expertMode ? 0.5 : 0.4))));
			float maxVolumeDefenseDamage = (float)Math.Round((double)(int)base.Player.statDefense * maxVolumeDefenseDamageScalar);
			float maxVolume = 1f;
			float lerpAmount = MathHelper.Clamp((float)defenseDamage / maxVolumeDefenseDamage, 0f, 1f);
			float defenseDamageSoundVolumeMultiplier = MathHelper.Lerp(0.5f, maxVolume, lerpAmount);
			SoundEngine.PlaySound(DefenseDamageSound with
			{
				Volume = DefenseDamageSound.Volume * defenseDamageSoundVolumeMultiplier
			}, base.Player.Center);
			hurtSoundTimer = 30;
		}
		string text = (-defenseDamage).ToString();
		Color messageColor = Color.LightGray;
		CombatText.NewText(new Rectangle((int)base.Player.position.X, (int)base.Player.position.Y - 16, base.Player.width, base.Player.height), messageColor, Language.GetTextValue(text));
	}

	private void LoseAdrenalineOnHurt(Player.HurtInfo hurtInfo, bool fullyAbsorbedByShield = false)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (hurtInfo.Damage <= 0 || adrenalineModeActive)
		{
			return;
		}
		if (draedonsHeart)
		{
			int pauseTime = (fullyAbsorbedByShield ? DraedonsHeart.NanomachinePauseAfterShieldDamage : DraedonsHeart.NanomachinePauseAfterDamage);
			adrenalinePauseTimer += pauseTime;
			return;
		}
		adrenalinePauseTimer += BalancingConstants.AdrenalinePauseAfterDamage;
		if (adrenaline >= adrenalineMax)
		{
			SoundEngine.PlaySound(Main.zenithWorld ? AdrenalineHurtGFB : AdrenalineHurtSound, base.Player.Center);
			adrenaline = 0f;
			return;
		}
		int damageToUse = hurtInfo.Damage;
		if (chaliceOfTheBloodGod && chaliceHitOriginalDamage > 0)
		{
			damageToUse = chaliceHitOriginalDamage;
			chaliceHitOriginalDamage = 0;
		}
		float damageMaxHPRatio = (float)damageToUse / (float)base.Player.statLifeMax2;
		float smallHitAdrenalineLossRatio = Utils.GetLerpValue(0f, BalancingConstants.AdrenalineFalloffTinyHitHealthRatio, damageMaxHPRatio, clamped: true);
		float adrenalineLossFraction = MathHelper.Lerp(BalancingConstants.MinimumAdrenalineLoss, 1f, smallHitAdrenalineLossRatio);
		float adrenalineToLose = adrenaline * adrenalineLossFraction;
		if (fullyAbsorbedByShield)
		{
			adrenalineToLose /= 2f;
		}
		adrenaline -= adrenalineToLose;
		if (adrenaline < 0f)
		{
			adrenaline = 0f;
		}
	}

	public override void UpdateBadLifeRegen()
	{
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		float deathNegativeRegenBonus = 0.25f;
		float calamityDebuffMultiplier = 1f + (CalamityWorld.death ? deathNegativeRegenBonus : 0f);
		float totalNegativeLifeRegen = 0f;
		if (CalamityWorld.death)
		{
			int totalVanillaDoT = 0;
			if (base.Player.poisoned && !purity)
			{
				totalVanillaDoT += 4;
			}
			if (base.Player.onFire && !purity)
			{
				totalVanillaDoT += 8;
			}
			if (base.Player.tongued)
			{
				totalVanillaDoT += 100;
			}
			if (base.Player.venom && !purity)
			{
				totalVanillaDoT += 12;
			}
			if (base.Player.onFrostBurn && !purity)
			{
				totalVanillaDoT += 12;
			}
			if (base.Player.onFire2 && !purity)
			{
				totalVanillaDoT += 12;
			}
			if (base.Player.burned)
			{
				totalVanillaDoT += 60;
			}
			if (base.Player.suffocating)
			{
				totalVanillaDoT += 40;
			}
			if (base.Player.electrified && !purity)
			{
				totalVanillaDoT += (eleResist ? 4 : 8);
				if (base.Player.controlLeft || base.Player.controlRight)
				{
					totalVanillaDoT += (eleResist ? 16 : 32);
				}
			}
			totalNegativeLifeRegen += (float)totalVanillaDoT * deathNegativeRegenBonus;
		}
		ApplyDoTDebuff(whisperingDeath, 0, laudanum);
		ApplyDoTDebuff(irradiated, 4, purity);
		int sulphurDoT = 6 - (sulphurSet ? 2 : 0) - (sulphurskin ? 2 : 0) - (corrosiveSpine ? 2 : 0);
		ApplyDoTDebuff(sulphurPoison, sulphurDoT, purity);
		ApplyDoTDebuff(riptide, 6, purity);
		ApplyDoTDebuff(weakBrimstoneFlames, 7);
		ApplyDoTDebuff(burningBlood, 8, purity);
		ApplyDoTDebuff(brainRot, 8, purity);
		ApplyDoTDebuff(vaporfied, 8, purity);
		int staticDoT = ((base.Player.controlLeft || base.Player.controlRight) ? 12 : 3) / ((!eleResist) ? 1 : 2);
		ApplyDoTDebuff(staticDischarge, staticDoT, purity);
		ApplyDoTDebuff(heavybleeding, 16, purity);
		ApplyDoTDebuff(crushDepth, 18, purity);
		ApplyDoTDebuff(astralInfection, 24, infectedJewel || hideOfDeus || purity);
		ApplyDoTDebuff(shadowflame, 30, purity);
		ApplyDoTDebuff(brimstoneFlames, (int)MathF.Round(30f * (abaddon ? (1f - Abaddon.BrimstoneFlamesReduction) : 1f)), purity);
		ApplyDoTDebuff(plague, (int)MathF.Round(30f * (abaddon ? (1f - AlchemicalDecanter.PlagueReduction) : 1f)), purity);
		ApplyDoTDebuff(vHex, 30);
		ApplyDoTDebuff(searingLava, 30);
		ApplyDoTDebuff(demonicFlames, 33, purity);
		ApplyDoTDebuff(laceration, 36, purity);
		ApplyDoTDebuff(daybroken, 40, purity);
		ApplyDoTDebuff(nightwither, 40, purity);
		ApplyDoTDebuff(holyFlames, 40, purity);
		ApplyDoTDebuff(voidfrost, 40, purity);
		ApplyDoTDebuff(hadopelagicPressure, 40, purity);
		ApplyDoTDebuff(godSlayerInferno, profanedCrystalBuffs ? 50 : 40);
		int fluxDoT = ((base.Player.controlLeft || base.Player.controlRight) ? 50 : 10) / ((!eleResist) ? 1 : 2);
		ApplyDoTDebuff(vermillionFlux, fluxDoT);
		ApplyDoTDebuff(elementalMix, 50, purity);
		ApplyDoTDebuff(trueVHex, 50);
		int dragonfireDoT = ((base.Player.name == "JFL" || base.Player.name == "MrJFL") ? 200 : 50) / ((!dynamoStemCells) ? 1 : 2);
		ApplyDoTDebuff(dragonFire, dragonfireDoT);
		ApplyDoTDebuff(miracleBlight, 60);
		ApplyDoTDebuff(banishingFire, 60);
		int rebukeDoT = ((base.Player.controlLeft || base.Player.controlRight) ? 80 : 16) / ((!eleResist) ? 1 : 2);
		ApplyDoTDebuff(auricRebuke, rebukeDoT);
		bool nearSafeZone = false;
		if (SulphuricWaterSafeZoneSystem.NearbySafeTiles.Count >= 1)
		{
			Point closestSafeZone = SulphuricWaterSafeZoneSystem.NearbySafeTiles.Keys.OrderBy(delegate(Point t)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				return t.ToVector2().DistanceSQ(base.Player.Center / 16f);
			}).First();
			if (Vector2.Distance(base.Player.Center.ToTileCoordinates().ToVector2(), closestSafeZone.ToVector2()) < SulphuricWaterSafeZoneSystem.NearbySafeTiles[closestSafeZone] * 17f)
			{
				nearSafeZone = true;
			}
		}
		float ASPoisonLevel = 0f;
		if (CalamityGlobalNPC.aquaticScourge >= 0 && Main.zenithWorld)
		{
			NPC AS = Main.npc[CalamityGlobalNPC.aquaticScourge];
			float scoogDistance = Vector2.Distance(base.Player.Center, AS.Center);
			if (AS.life < AS.lifeMax && scoogDistance < 4000f)
			{
				ASPoisonLevel = Utils.GetLerpValue(800f, 1600f, scoogDistance, clamped: true);
			}
		}
		bool ASPoisoning = ASPoisonLevel > 0f;
		if (ASPoisoning || ((ZoneSulphur || ZoneAbyssLayer1) && !base.Player.creativeGodMode && base.Player.IsUnderwater() && !decayEffigy && !abyssalDivingSuit && !base.Player.lavaWet && !base.Player.honeyWet && !nearSafeZone))
		{
			float increment = 0.0013888889f;
			if (ASPoisoning)
			{
				increment *= 3f + 6f * ASPoisonLevel;
			}
			if (sulphurskin && !ASPoisoning)
			{
				increment *= 0.5f;
			}
			if (sulphurSet && !ASPoisoning)
			{
				increment *= 0.5f;
			}
			if (corrosiveSpine && !ASPoisoning)
			{
				increment *= 0.5f;
			}
			if (ZoneAbyssLayer1 && !ASPoisoning)
			{
				increment *= 0.33f;
			}
			SulphWaterPoisoningLevel = MathHelper.Clamp(SulphWaterPoisoningLevel + increment, 0f, 1f);
			if (SulphWaterPoisoningLevel >= 1f)
			{
				SulphWaterPoisoningLevel = 0f;
				base.Player.Hurt(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.SulphurMeter").ToNetworkText(base.Player.name)), Math.Min(base.Player.statLifeMax2 / 4, 150), 0);
			}
		}
		else
		{
			SulphWaterPoisoningLevel = MathHelper.Clamp(SulphWaterPoisoningLevel - 1f / 150f, 0f, 1f);
		}
		for (int l = 0; l < Player.MaxBuffs; l++)
		{
			int buff = base.Player.buffType[l];
			if (CalamityBuffSets.AlcoholStrength.TryGetValue(buff, out var level))
			{
				alcoholPoisonLevel += level;
			}
		}
		if (base.Player.Calamity().ivDrip)
		{
			alcoholPoisonLevel++;
		}
		if (everclear)
		{
			totalNegativeLifeRegen += Everclear.RegenLoss;
		}
		if (alcoholPoisonLevel > 0)
		{
			base.Player.tipsy = true;
			if (!base.Player.HasBuff(25))
			{
				base.Player.fishingSkill += 5;
			}
		}
		if (alcoholPoisonLevel > 3)
		{
			base.Player.nebulaLevelLife = 0;
			if (base.Player.whoAmI == Main.myPlayer)
			{
				base.Player.AddBuff(ModContent.BuffType<AlcoholPoisoning>(), 61, quiet: false);
			}
			if (base.Player.lifeRegen > 0)
			{
				base.Player.lifeRegen = 0;
			}
			base.Player.lifeRegenTime = 0f;
			totalNegativeLifeRegen += 3 * alcoholPoisonLevel;
		}
		if (brimflameFrenzy)
		{
			base.Player.manaRegen = 0;
			base.Player.manaRegenBonus = 0;
			base.Player.manaRegenDelay = (int)base.Player.maxRegenDelay;
			if (base.Player.lifeRegen > 0)
			{
				base.Player.lifeRegen = 0;
			}
			totalNegativeLifeRegen += 42f;
		}
		if (witheredDebuff)
		{
			witheredWeaponHoldTime += witheringWeaponEnchant.ToDirectionInt();
			if (witheredWeaponHoldTime < 0)
			{
				witheredWeaponHoldTime = 0;
			}
			else
			{
				totalNegativeLifeRegen += (int)(5.0 * Math.Pow(1.5, (double)witheredWeaponHoldTime / 87.0));
				if (base.Player.lifeRegen > 0)
				{
					base.Player.lifeRegen = 0;
				}
			}
		}
		else
		{
			witheredWeaponHoldTime = 0;
		}
		if (base.Player.statMana < 0 && base.Player.Calamity().ChaosStone)
		{
			totalNegativeLifeRegen -= (float)base.Player.statMana / 100f * global::CalamityMod.Items.Accessories.ChaosStone.LostRegenPer100Mana;
		}
		if (reaverDefense)
		{
			totalNegativeLifeRegen -= (int)(totalNegativeLifeRegen * ReaverHeadTank.SetBonusDebuffDamageReduction);
		}
		if (tequilaSunrise)
		{
			totalNegativeLifeRegen = (int)(totalNegativeLifeRegen * TequilaSunrise.DoTMultiplier);
		}
		base.Player.lifeRegen -= (int)totalNegativeLifeRegen;
		bool hasLifeRegenHinderingDebuff = base.Player.lifeRegenTime == 0f;
		if (honeyDewHalveDebuffs)
		{
			for (int l2 = 0; l2 < Player.MaxBuffs; l2++)
			{
				int buffID = base.Player.buffType[l2];
				if (base.Player.buffTime[l2] > 2 && BuffDatasets.DebuffDataset[buffID] != null)
				{
					bool shouldHalveDuration = BuffDatasets.DebuffDataset[buffID].SicknessDebuffScaling > 0f;
					if (livingDewHalveDebuffs)
					{
						shouldHalveDuration |= BuffDatasets.DebuffDataset[buffID].HeatDebuffScaling > 0f;
					}
					if (purity)
					{
						shouldHalveDuration |= CalamityBuffSets.IsDebuff[buffID];
					}
					if (shouldHalveDuration)
					{
						base.Player.buffTime[l2]--;
					}
				}
			}
		}
		if (divineBless && base.Player.whoAmI == Main.myPlayer && base.Player.miscCounter % AngelicAlliance.DivineBlessFramesPerHeal == 0 && !noLifeRegen)
		{
			base.Player.HealPlayer(1, HealTextType.None);
		}
		if (bloodfinBoost)
		{
			if (base.Player.lifeRegen < 0)
			{
				if (base.Player.lifeRegenTime < (float)Bloodfin.DebuffedRegenTimeFloor)
				{
					base.Player.lifeRegenTime = Bloodfin.DebuffedRegenTimeFloor;
				}
				base.Player.lifeRegen += Bloodfin.DebuffedRegenBoost;
			}
			else
			{
				base.Player.lifeRegen += Bloodfin.RegenBoost;
				base.Player.lifeRegenTime += Bloodfin.RegenTimeBoost;
			}
			if (bloodfinTimer > 0)
			{
				bloodfinTimer--;
			}
			if (base.Player.whoAmI == Main.myPlayer && bloodfinTimer <= 0)
			{
				bloodfinTimer = Bloodfin.FramesForExtraRegen;
				if (base.Player.statLife < (int)((double)base.Player.statLifeMax2 * Bloodfin.ExtraRegenHealthThreshold) && !noLifeRegen)
				{
					base.Player.HealPlayer(1, HealTextType.None);
				}
			}
		}
		if (permafrostsConcoction && base.Player.buffType.Any((int num) => BuffDatasets.DebuffDataset[num] != null && BuffDatasets.DebuffDataset[num].HeatDebuffScaling > 0f))
		{
			if (base.Player.lifeRegenTime < 900f)
			{
				base.Player.lifeRegenTime = 900f;
			}
			base.Player.lifeRegen += 6;
		}
		if (rOoze || aAmpoule || purity)
		{
			float missingLifeRatio = (float)(base.Player.statLifeMax2 - base.Player.statLife) / (float)base.Player.statLifeMax2;
			int lifeRegenToGive = (int)Math.Round(MathHelper.Lerp((float)RadiantOoze.MinRegenBoost, (float)RadiantOoze.MaxRegenBoost, missingLifeRatio));
			base.Player.lifeRegen += lifeRegenToGive;
			radiantOozeRegen += (float)lifeRegenToGive / 2f;
			purityRegen += (float)lifeRegenToGive / 2f;
		}
		if (purity)
		{
			int intendedPurityDefense = 0;
			int currentDebuffs = base.Player.buffType.Count((int i) => CalamityBuffSets.IsDebuff[i]);
			if (currentDebuffs > 0)
			{
				int healFrameCadence = 12;
				int punishmentFrames = PurityHealSlowdownFrames - 300;
				if (healFrameCadence < 52)
				{
					healFrameCadence += ((punishmentFrames >= 0) ? (punishmentFrames / 15) : 0);
				}
				if (base.Player.miscCounter % healFrameCadence == healFrameCadence - 1)
				{
					base.Player.Heal(1);
				}
				intendedPurityDefense = 15 + (currentDebuffs - 1) * 5;
				if (jewelBonusDefense < intendedPurityDefense)
				{
					jewelBonusDefense = intendedPurityDefense;
				}
				if (PurityHealSlowdownFrames < 900)
				{
					PurityHealSlowdownFrames++;
				}
				purityRegen += 60f / (float)healFrameCadence;
			}
			if (base.Player.miscCounter % 60 == 0 && jewelBonusDefense > intendedPurityDefense)
			{
				jewelBonusDefense--;
			}
			if (currentDebuffs <= 0)
			{
				PurityHealSlowdownFrames--;
				if (PurityHealSlowdownFrames < 0)
				{
					PurityHealSlowdownFrames = 0;
				}
			}
			base.Player.statDefense += jewelBonusDefense;
		}
		else if (infectedJewel)
		{
			base.Player.lifeRegen += 2;
			int intendedJewelDefense = 0;
			int currentDebuffs2 = base.Player.buffType.Count((int i) => CalamityBuffSets.IsDebuff[i]);
			if (currentDebuffs2 > 0)
			{
				base.Player.lifeRegen += 4;
				intendedJewelDefense = 12 + (currentDebuffs2 - 1) * 4;
				if (jewelBonusDefense < intendedJewelDefense)
				{
					jewelBonusDefense = intendedJewelDefense;
				}
			}
			if (base.Player.miscCounter % 60 == 0 && jewelBonusDefense > intendedJewelDefense)
			{
				jewelBonusDefense--;
			}
			base.Player.statDefense += jewelBonusDefense;
		}
		else if (crownJewel)
		{
			base.Player.lifeRegen += 2;
			if (base.Player.buffType.Any((int i) => CalamityBuffSets.IsDebuff[i]))
			{
				base.Player.lifeRegen += 3;
			}
		}
		if (tequilaSunrise && hadLifeRegenHinderingDebuff && !hasLifeRegenHinderingDebuff)
		{
			base.Player.lifeRegenTime += 1800f;
		}
		if (((silvaCountdown > 0 && hasSilvaEffect && silvaSet) || (LastUsedDashID == GodslayerArmorDash.ID && base.Player.dashDelay < 0)) && base.Player.lifeRegen < 0)
		{
			base.Player.lifeRegen = 0;
		}
		if (noLifeRegen)
		{
			base.Player.nebulaLevelLife = 0;
			if (base.Player.lifeRegen > 0)
			{
				base.Player.lifeRegen = 0;
			}
			base.Player.lifeRegenTime = 0f;
			if (base.Player.lifeRegenCount > 0)
			{
				base.Player.lifeRegenCount = 0;
			}
		}
		if (holyInferno)
		{
			base.Player.nebulaLevelLife = 0;
			hInfernoBoost++;
			if (base.Player.lifeRegen > 0)
			{
				base.Player.lifeRegen = 0;
			}
			base.Player.lifeRegenTime = 0f;
			base.Player.lifeRegen -= (int)((float)hInfernoBoost * calamityDebuffMultiplier);
			if (base.Player.lifeRegen < -200)
			{
				base.Player.lifeRegen = -200;
			}
		}
		else
		{
			hInfernoBoost = 0;
		}
		if (ZoneAbyss && !base.Player.IsUnderwater() && base.Player.statLife > 100)
		{
			base.Player.nebulaLevelLife = 0;
			if (base.Player.lifeRegen > 0)
			{
				base.Player.lifeRegen = 0;
			}
			base.Player.lifeRegenTime = 0f;
			base.Player.lifeRegen -= (int)(160.0 * (double)calamityDebuffMultiplier);
		}
		ChaliceOfTheBloodGod.HandleBleedout(base.Player);
		hadLifeRegenHinderingDebuff = hasLifeRegenHinderingDebuff;
		void ApplyDoTDebuff(bool hasDebuff, int negativeLifeRegenToApply, bool immuneCondition = false)
		{
			if (!(!hasDebuff | immuneCondition))
			{
				if (base.Player.lifeRegen > 0)
				{
					base.Player.lifeRegen = 0;
				}
				base.Player.lifeRegenTime = 0f;
				totalNegativeLifeRegen += (float)negativeLifeRegenToApply * calamityDebuffMultiplier;
			}
		}
	}

	public override void UpdateLifeRegen()
	{
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		if (mushy)
		{
			base.Player.lifeRegen += Mushy.RegenBoost;
		}
		if (permafrostsConcoction)
		{
			if (base.Player.statLife < actualMaxLife / 2)
			{
				base.Player.lifeRegen++;
			}
			if (base.Player.statLife < actualMaxLife / 4)
			{
				base.Player.lifeRegen++;
			}
			if (base.Player.statLife < actualMaxLife / 10)
			{
				base.Player.lifeRegen += 2;
			}
		}
		if (tRegen)
		{
			base.Player.lifeRegen += TarragonHeadMelee.TarraLifeRegenBoost;
		}
		if (sRegen)
		{
			base.Player.lifeRegen += SpiritGlyph.RegenBoost;
		}
		if (PinkJellyRegen)
		{
			base.Player.lifeRegen += LifeJelly.AuraRegenBoost;
		}
		if (GreenJellyRegen)
		{
			base.Player.lifeRegen += global::CalamityMod.Items.Accessories.GrandGelatin.AuraRegenBoost;
		}
		if (AbsorberRegen)
		{
			base.Player.lifeRegen += TheAbsorber.AuraRegenBoost;
		}
		if (hallowedRegen)
		{
			base.Player.lifeRegen += HallowedRune.RegenBoost;
		}
		if (affliction || afflicted)
		{
			base.Player.lifeRegen += Affliction.RegenBoost;
		}
		if (trinketOfChi || chiRegen)
		{
			base.Player.lifeRegen += 2;
		}
		if (darkSunRing && (Main.eclipse || Main.dayTime))
		{
			base.Player.lifeRegen += (Main.eclipse ? 2 : 4);
		}
		if (silvaSet)
		{
			base.Player.lifeRegen += SilvaArmor.SetBonusRegenBoost;
		}
		if (phantomicHeartRegen > 0 && phantomicHeartRegen < 1000)
		{
			base.Player.lifeRegen += PhantomicArtifact.RegenBoost;
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 5, 0f, 0f, 200, new Color(99, 54, 84), 2f);
				dust.noGravity = true;
				dust.fadeIn = 1.3f;
				Vector2 velocity = (dust.velocity = CalamityUtils.RandomVelocity(100f, 50f, 100f, 0.04f));
				((Vector2)(ref velocity)).Normalize();
				velocity *= 34f;
				dust.position = base.Player.Center - velocity;
			}
		}
		if (community)
		{
			int regenBoost = 1 + (int)(TheCommunity.CalculatePower() * 10f);
			bool lesserEffect = false;
			for (int l = 0; l < Player.MaxBuffs; l++)
			{
				int hasBuff = base.Player.buffType[l];
				lesserEffect = CalamityBuffSets.AlcoholStrength.TryGetValue(hasBuff, out var _);
			}
			if (base.Player.lifeRegen < 0)
			{
				base.Player.lifeRegen += (lesserEffect ? 1 : regenBoost);
			}
		}
		if (handWarmer && eskimoSet)
		{
			base.Player.lifeRegen += 2;
		}
		if (avertorBonus)
		{
			base.Player.lifeRegen += 4;
		}
		if (fearmongerSet && fearmongerRegenFrames > 0)
		{
			if (base.Player.lifeRegenTime < (float)FearmongerGreathelm.MinionRegenTimeFloor)
			{
				base.Player.lifeRegenTime = FearmongerGreathelm.MinionRegenTimeFloor;
			}
			base.Player.lifeRegen += FearmongerGreathelm.MinionRegenBoost;
			base.Player.lifeRegenTime += FearmongerGreathelm.MinionRegenTimeBoost;
		}
		if (pinkCandle && !noLifeRegen)
		{
			pinkCandleHealFraction += (double)base.Player.statLifeMax2 * VigorousCandle.PercentHealthPerSecond / 60.0;
			if (pinkCandleHealFraction >= 1.0)
			{
				pinkCandleHealFraction = 0.0;
				base.Player.HealPlayer(1, HealTextType.None);
			}
		}
		else
		{
			pinkCandleHealFraction = 0.0;
		}
		if (manaOverloader)
		{
			float manaRatio = (float)base.Player.statMana / (float)base.Player.statManaMax2;
			base.Player.lifeRegen += (int)(MathF.Round(MathHelper.Lerp(4f, -4f, manaRatio)) * (base.Player.HasBuff(94) ? 0.5f : 1f));
		}
		if (!base.Player.shinyStone && base.Player.StandingStill() && base.Player.velocity.Y == 0f && base.Player.itemAnimation == 0 && (shadeRegen || cFreeze))
		{
			if (base.Player.lifeRegen < 0)
			{
				base.Player.lifeRegen /= 2;
			}
			if (base.Player.lifeRegen > 0 && base.Player.statLife < actualMaxLife)
			{
				int dustType = (shadeRegen ? 173 : 67);
				bool dustSpawnRolled = (((float)Main.rand.Next(30000) < base.Player.lifeRegenTime) ? Main.rand.NextBool() : Main.rand.NextBool(30));
				if ((dustType != -1) & dustSpawnRolled)
				{
					Dust dust2 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, dustType, 0f, 0f, 200);
					dust2.noGravity = true;
					dust2.fadeIn = 1.3f;
					Vector2 velocity2 = (dust2.velocity = CalamityUtils.RandomVelocity(100f, 50f, 100f, 0.04f));
					((Vector2)(ref velocity2)).Normalize();
					dust2.position = base.Player.Center - velocity2;
				}
			}
			float regenTimeNeededForTurboRegen = (shadeRegen ? 40f : 60f);
			if (base.Player.lifeRegenTime > regenTimeNeededForTurboRegen && base.Player.lifeRegenTime < 900f)
			{
				base.Player.lifeRegenTime = 900f;
			}
			base.Player.lifeRegen += 4;
			base.Player.lifeRegenTime += 4f;
		}
		if (regenerator)
		{
			if (base.Player.miscCounter % Regenerator.FramesPerHeal == 0 && base.Player.statLife < (int)((float)base.Player.statLifeMax2 * 0.5f))
			{
				base.Player.HealPlayer(1, HealTextType.None);
			}
			if (base.Player.lifeRegenTime < 3600f)
			{
				base.Player.lifeRegenTime += Regenerator.RegenTimeBoost;
			}
		}
		else
		{
			regeneratorDamage = 0f;
		}
		if (!toxicHeart)
		{
			return;
		}
		float minLifeRegen = -20f;
		float maxLifeRegen = 15f;
		int auraDamage = (int)base.Player.GetBestClassDamage().ApplyTo(200f);
		IEntitySource source = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<ToxicHeart>()));
		float lifeRegenRate = Utils.Remap(base.Player.lifeRegen, minLifeRegen, maxLifeRegen, 20f, 1f);
		if (pulseRate < lifeRegenRate)
		{
			pulseRate = lifeRegenRate;
		}
		else
		{
			pulseRate = MathHelper.Lerp(pulseRate, lifeRegenRate, 0.002f);
		}
		if (pulseCounter >= 420f)
		{
			Projectile.NewProjectile(source, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<PlaguePulse>(), auraDamage, 0f, base.Player.whoAmI);
			pulseCounter = 0f;
			if (toxicHeartVisuals)
			{
				float soundVolume = Utils.Remap(base.Player.lifeRegen, minLifeRegen, maxLifeRegen, 1f, 0.3f);
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/Heartbeat");
				style.Volume = soundVolume;
				style.PitchVariance = 0.2f;
				SoundEngine.PlaySound(in style, base.Player.Center);
			}
		}
		else
		{
			pulseCounter += MathHelper.Clamp(pulseRate, 1f, 20f);
		}
	}

	public override void NaturalLifeRegen(ref float regen)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (camper && base.Player.velocity.X != 0f && base.Player.grappling[0] <= 0)
		{
			regen *= 2.5f;
			if ((float)Main.rand.Next(30000) < base.Player.lifeRegenTime || Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 12, 0f, 0f, 200, Color.OrangeRed);
				dust.noGravity = true;
				dust.fadeIn = 1.3f;
				Vector2 velocity = (dust.velocity = CalamityUtils.RandomVelocity(100f, 50f, 100f, 0.04f));
				((Vector2)(ref velocity)).Normalize();
				velocity *= 34f;
				dust.position = base.Player.Center - velocity;
			}
		}
		if (regenerator)
		{
			int finalRegen = base.Player.lifeRegen + (int)Math.Round(regen * ((float)base.Player.statLifeMax2 / 400f * 0.85f + 0.15f));
			finalRegen = Math.Max(finalRegen, 0);
			if (base.Player.palladiumRegen)
			{
				finalRegen += 4;
			}
			regeneratorDamage = (float)finalRegen * Regenerator.RegenToDamageRatio;
			base.Player.GetDamage<GenericDamageClass>() += regeneratorDamage;
			if (base.Player.lifeRegen > 0)
			{
				base.Player.lifeRegen = 0;
			}
			if (regen > 0f)
			{
				regen = 0f;
			}
			if (base.Player.lifeRegenCount > 0)
			{
				base.Player.lifeRegenCount = 0;
			}
			if (base.Player.statLife >= (int)((float)base.Player.statLifeMax2 * Regenerator.HealthRatioCap))
			{
				base.Player.statLife = (int)((float)base.Player.statLifeMax2 * Regenerator.HealthRatioCap);
				base.Player.moonLeech = true;
				healingPotionMultiplier = 0f;
			}
		}
	}

	public override void PostUpdateMiscEffects()
	{
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_114b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1227: Unknown result type (might be due to invalid IL or missing references)
		//IL_1232: Unknown result type (might be due to invalid IL or missing references)
		//IL_1266: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Unknown result type (might be due to invalid IL or missing references)
		//IL_127b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1280: Unknown result type (might be due to invalid IL or missing references)
		//IL_1285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		//IL_1549: Unknown result type (might be due to invalid IL or missing references)
		//IL_154e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1553: Unknown result type (might be due to invalid IL or missing references)
		//IL_155d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1355: Unknown result type (might be due to invalid IL or missing references)
		//IL_1360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_141e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1429: Unknown result type (might be due to invalid IL or missing references)
		//IL_1433: Unknown result type (might be due to invalid IL or missing references)
		//IL_1439: Unknown result type (might be due to invalid IL or missing references)
		//IL_144d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1458: Unknown result type (might be due to invalid IL or missing references)
		//IL_1496: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1373: Unknown result type (might be due to invalid IL or missing references)
		//IL_1378: Unknown result type (might be due to invalid IL or missing references)
		//IL_137e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1388: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13db: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1906: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1704: Unknown result type (might be due to invalid IL or missing references)
		//IL_170b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1712: Unknown result type (might be due to invalid IL or missing references)
		//IL_1728: Unknown result type (might be due to invalid IL or missing references)
		//IL_1732: Unknown result type (might be due to invalid IL or missing references)
		//IL_1741: Unknown result type (might be due to invalid IL or missing references)
		//IL_159a: Unknown result type (might be due to invalid IL or missing references)
		//IL_159f: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15da: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1603: Unknown result type (might be due to invalid IL or missing references)
		//IL_1619: Unknown result type (might be due to invalid IL or missing references)
		//IL_1623: Unknown result type (might be due to invalid IL or missing references)
		//IL_1632: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1800: Unknown result type (might be due to invalid IL or missing references)
		//IL_180d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1813: Unknown result type (might be due to invalid IL or missing references)
		//IL_183e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1843: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.wingsLogic > 0)
		{
			base.Player.jumpSpeedBoost++;
		}
		if (fearmongerRegenFrames > 0)
		{
			fearmongerRegenFrames--;
		}
		HandleTileEffects();
		if (blazingCursorDamage || blazingCursorVisuals)
		{
			HandleBlazingMouseEffects();
		}
		RevengeanceModeMiscEffects();
		UpdateRippers();
		AbyssEffects();
		MiscEffects();
		StandingStillEffects();
		OtherBuffEffects();
		EnergyShields();
		DefenseEffects();
		Limits();
		HandlePotions();
		HandleTextChatMessages();
		CheckIfMouseItemIsSchematic();
		AndroombaRightClick();
		UpdateDrawingParameters();
		HandleLucreciaLineEffects();
		CalamityGlobalItem.UpdateAllParticleSets();
		BrokenBiomeBlade.UpdateAllParticleSets();
		TrueBiomeBlade.UpdateAllParticleSets();
		OmegaBiomeBlade.UpdateAllParticleSets();
		GemTechState.Update();
		if (base.Player.whoAmI == Main.myPlayer && Main.netMode == 1)
		{
			packetTimer++;
			if (packetTimer == 15)
			{
				packetTimer = 0;
				StandardSync();
			}
			if (syncMouseRightClick)
			{
				syncMouseRightClick = false;
				MouseRightClickSync();
			}
			mouseWorldPacketTimer = Math.Min(mouseWorldPacketTimer + 1, 2);
			if (mouseWorldPacketTimer >= 2)
			{
				if (syncMousePosition)
				{
					mouseWorldPacketTimer = 0;
					syncMousePosition = false;
					syncMouseRotation = false;
					MousePositionSync();
				}
				if (syncMouseRotation)
				{
					mouseWorldPacketTimer = 0;
					syncMouseRotation = false;
					MouseRotationSync();
				}
			}
		}
		if (base.Player.HeldItem.type != ModContent.ItemType<SaharaSlicers>())
		{
			saharaSlicersBolts = 0;
		}
		if (base.Player.whoAmI == Main.myPlayer && base.Player.HeldItem.type == ModContent.ItemType<Starfleet>() && base.Player.ownedProjectileCounts[ModContent.ProjectileType<StarfleetHoldout>()] == 0 && !base.Player.dead && !Main.mapFullscreen && !base.Player.mouseInterface)
		{
			int damage = (int)base.Player.GetTotalDamage<RangedDamageClass>().ApplyTo(base.Player.HeldItem.damage);
			Projectile.NewProjectile(base.Player.GetSource_FromThis(), base.Player.Center, base.Player.Center.DirectionTo(base.Player.Calamity().mouseWorld), ModContent.ProjectileType<StarfleetHoldout>(), damage, base.Player.HeldItem.knockBack, base.Player.whoAmI);
		}
		if (base.Player.whoAmI == Main.myPlayer && base.Player.HeldItem.type == ModContent.ItemType<Starmada>() && base.Player.ownedProjectileCounts[ModContent.ProjectileType<StarmadaHoldout>()] == 0 && !base.Player.dead && !Main.mapFullscreen && !base.Player.mouseInterface)
		{
			int damage2 = (int)base.Player.GetTotalDamage<RangedDamageClass>().ApplyTo(base.Player.HeldItem.damage);
			Projectile.NewProjectile(base.Player.GetSource_FromThis(), base.Player.Center, base.Player.Center.DirectionTo(base.Player.Calamity().mouseWorld), ModContent.ProjectileType<StarmadaHoldout>(), damage2, base.Player.HeldItem.knockBack, base.Player.whoAmI);
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<GaelsGreatsword>())
		{
			heldGaelsLastFrame = true;
		}
		else if (heldGaelsLastFrame)
		{
			heldGaelsLastFrame = false;
			rage = 0f;
		}
		if (furyFuel < 1800 && furyRefuelTimer >= 0f)
		{
			furyFuel += (int)furyRefuelTimer;
			furyRefuelTimer = MathHelper.Lerp(furyRefuelTimer, 25f, 0.01f);
			if (furyFuel > 1800)
			{
				furyFuel = 1800;
			}
		}
		else if (furyRefuelTimer < 0f)
		{
			furyRefuelTimer++;
		}
		if (!draedonsHeart && hadNanomachinesLastFrame)
		{
			hadNanomachinesLastFrame = false;
			adrenaline = 0f;
		}
		base.Player.GetDamage<RogueDamageClass>() += stealthDamage;
		if (XykVisualsBlue || XykVisualsOrange)
		{
			bool Orange = XykVisualsOrange;
			Color effectColor = (Orange ? Color.Gold : Color.DodgerBlue);
			float rate = Main.GlobalTimeWrappedHourly * 12f;
			List<Color> eColors = new List<Color>
			{
				(Color)(Orange ? new Color(248, 117, 52) : Color.DodgerBlue),
				Orange ? Color.Gold : Color.Cyan,
				Orange ? Color.Orange : Color.RoyalBlue
			};
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			effectColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
			bool rageOrAdren = base.Player.Calamity().rageModeActive || base.Player.Calamity().adrenalineModeActive;
			Color attemptColor = (Color)((base.Player.Calamity().rageModeActive && base.Player.Calamity().adrenalineModeActive) ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) : (base.Player.Calamity().adrenalineModeActive ? Color.MediumSpringGreen : (base.Player.Calamity().rageModeActive ? Color.Crimson : effectColor)));
			XykFXColor = Color.Lerp(XykFXColor, attemptColor, rageOrAdren ? 0.05f : 0.25f);
			int maxWingPieces = 7;
			int numOfActiveWings = 0;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<XykWings>() && p.owner == base.Player.whoAmI && p.ai[1] == 0f)
				{
					numOfActiveWings++;
				}
			}
			if (numOfActiveWings < maxWingPieces && !base.Player.dead && base.Player.wingsLogic > 0 && (((base.Player.wingTime != (float)base.Player.wingTimeMax || base.Player.velocity.Y != 0f) && base.Player.wingTime > 0f) || XykWingTimer >= 3))
			{
				if (XykWingTimer >= 3)
				{
					int wingCount = numOfActiveWings;
					Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), base.Player.Center, Vector2.Zero, ModContent.ProjectileType<XykWings>(), 0, 0f, base.Player.whoAmI, wingCount);
					if (numOfActiveWings + 1 == maxWingPieces)
					{
						XykWingTimer = 0;
					}
				}
				else
				{
					XykWingTimer++;
				}
			}
			else
			{
				XykWingTimer = 0;
			}
		}
		bool dashStart = base.Player.dashDelay == -1 && ((!HasCustomDash && IsFirstDashFrame) || (HasCustomDash && UsedDash.DashTimeAdjustedForStartup == 1));
		int dir = MathF.Sign(base.Player.velocity.X);
		if (dashStart)
		{
			lastDashWasTabi = false;
		}
		if (((gShell && giantShellPostHit == 0) || (tortShell && tortShellPostHit == 0)) & dashStart)
		{
			base.Player.velocity.X *= 0.9f;
		}
		if (lastDashWasTabi)
		{
			base.Player.dashType = 1;
		}
		if (base.Player.dashType == 1 && !base.Player.Calamity().statisNinjaBelt && !base.Player.Calamity().statisVoidSash)
		{
			if (dashStart)
			{
				base.Player.velocity.X *= 2f;
				lastDashWasTabi = true;
			}
			if (base.Player.dashDelay == -1)
			{
				base.Player.velocity.X *= 0.95f;
				if (base.Player.timeSinceLastDashStarted > 12)
				{
					base.Player.velocity.X *= 0.75f;
				}
			}
		}
		Color newColor;
		if ((devilsDevastationKillMode || exaltedKillMode) && !base.Player.mount.Active)
		{
			float fxScale = 1f;
			if (exaltedKillMode)
			{
				if (base.Player.wingTime > 0f && base.Player.miscCounter % 2 == 0)
				{
					base.Player.wingTime++;
				}
				if (base.Player.miscCounter % 4 == 0)
				{
					base.Player.HealPlayer(1, HealTextType.None);
				}
				if (base.Player.dashDelay > 0)
				{
					base.Player.dashDelay = 0;
				}
				if (base.Player.dashDelay == -1)
				{
					fxScale = 1.5f;
				}
			}
			else
			{
				if (base.Player.wingTime > 0f && base.Player.miscCounter % 3 == 0)
				{
					base.Player.wingTime += 2f;
				}
				if (base.Player.miscCounter % 5 == 0)
				{
					base.Player.HealPlayer(2, HealTextType.None);
				}
				if (base.Player.dashDelay > 0)
				{
					base.Player.dashDelay = 0;
				}
				if (base.Player.dashDelay == -1)
				{
					fxScale = 1.5f;
				}
			}
			if (((Vector2)(ref base.Player.velocity)).Length() > 2f)
			{
				if (Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Player.Center + Main.rand.NextVector2Circular(20f, 20f), -base.Player.velocity * Main.rand.NextFloat(0.3f, 0.8f) * fxScale, "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 22, Main.rand.NextFloat(0.2f, 0.3f) * fxScale, Color.Lerp(Color.MediumOrchid, Color.BlueViolet, Main.rand.NextFloat(0f, 0.7f)) * 0.8f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, fxScale - 1f));
				}
				else
				{
					float sine = (float)Math.Sin((float)base.Player.miscCounter * 0.575f / (float)Math.PI);
					for (int i = 0; i < 2; i++)
					{
						Vector2 offset = base.Player.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
						float scale = Main.rand.NextFloat(0.8f, 1.6f) * fxScale;
						Vector2 position = base.Player.Center + offset * (float)((i != 0) ? 1 : (-1)) * fxScale;
						int type = ModContent.DustType<LightDust>();
						Vector2? velocity = base.Player.velocity * Main.rand.NextFloat(0.2f, 0.5f);
						newColor = default(Color);
						Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
						dust.noGravity = true;
						dust.scale = scale;
						dust.color = (Main.rand.NextBool() ? Color.MediumOrchid : Color.BlueViolet);
					}
				}
			}
			Vector2 center = base.Player.Center;
			newColor = Color.MediumOrchid;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		}
		if (base.Player.vortexStealthActive && base.Player.wingsLogic == 30 && base.Player.wingTime > 0f && base.Player.miscCounter % 3 == 0)
		{
			base.Player.wingTime++;
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<Lucrecia>() && darklightEnergy > 0)
		{
			if (darklightEnergy == Lucrecia.MaxEnergy && !darklightEnergyPaused)
			{
				darklightEnergyPaused = true;
				darklightEnergyTimer = 0;
			}
			darklightEnergyTimer++;
			if (darklightEnergyPaused)
			{
				if (darklightEnergyTimer >= 1 && !darklightEnergyMaxFXPlayed)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DarklightEnergyCharged");
					style.Volume = 0.9f;
					SoundEngine.PlaySound(in style, base.Player.Center);
					for (int j = 0; j < 10; j++)
					{
						Vector2 spawnDirection = ((float)Math.PI * 2f * ((float)j / 10f)).ToRotationVector2();
						Vector2 velocity2 = spawnDirection * 14f;
						GeneralParticleHandler.SpawnParticle(new CritSpark(base.Player.Center + spawnDirection * 3f, velocity2, Color.Lerp(Color.CornflowerBlue, Color.MediumPurple, Main.rand.NextFloat(1f)), Color.White * 0.33f, 1.2f, 12, 0.3f, 1.2f));
					}
					darklightEnergyMaxFXPlayed = true;
				}
				if (darklightEnergyTimer >= 180)
				{
					darklightEnergyPaused = false;
					darklightEnergyTimer = 0;
					darklightEnergy--;
				}
			}
			else
			{
				darklightEnergyMaxFXPlayed = false;
				if (darklightEnergyTimer >= 20)
				{
					darklightEnergy--;
					darklightEnergyTimer = 0;
				}
			}
		}
		else
		{
			darklightEnergyTimer = 0;
			darklightEnergyPaused = false;
			darklightEnergy = 0;
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<Lightspeed>() && elementalMastery > 0)
		{
			if (elementalMastery == Lightspeed.MaxEnergy && !elementalMasteryPaused)
			{
				elementalMasteryPaused = true;
				elementalMasteryTimer = 0;
			}
			elementalMasteryTimer++;
			if (elementalMasteryPaused)
			{
				if (elementalMasteryTimer >= 1 && !elementalMasterySFXPlayed)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DarklightEnergyCharged");
					style.Volume = 0.9f;
					SoundEngine.PlaySound(in style, base.Player.Center);
					for (int k = 0; k < 10; k++)
					{
						Vector2 spawnDirection2 = ((float)Math.PI * 2f * ((float)k / 10f)).ToRotationVector2();
						Vector2 velocity3 = spawnDirection2 * 14f;
						GeneralParticleHandler.SpawnParticle(new CritSpark(base.Player.Center + spawnDirection2 * 3f, velocity3, Color.Lerp(Color.CornflowerBlue, Color.MediumPurple, Main.rand.NextFloat(1f)), Color.White * 0.33f, 1.2f, 12, 0.3f, 1.2f));
					}
					elementalMasterySFXPlayed = true;
				}
				if (elementalMasteryTimer >= 180)
				{
					elementalMasteryPaused = false;
					elementalMasteryTimer = 0;
					elementalMastery--;
				}
			}
			else
			{
				elementalMasterySFXPlayed = false;
				if (elementalMasteryTimer >= 30)
				{
					elementalMastery--;
					elementalMasteryTimer = 0;
				}
			}
		}
		else
		{
			elementalMasteryTimer = 0;
			elementalMasteryPaused = false;
			elementalMastery = 0;
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<UnstableCastersGauntlet>() && unstableCastersGauntletVis < 100f)
		{
			unstableCastersGauntletVisTimer++;
			if (unstableCastersGauntletVisTimer >= 4)
			{
				unstableCastersGauntletVis += 0.1f;
				unstableCastersGauntletVisTimer = 0;
			}
		}
		if (unstableCastersGauntletVis >= 100f)
		{
			unstableCastersGauntletVis = 100f;
		}
		if (lAmbergris)
		{
			if (dashStart)
			{
				base.Player.velocity.X *= 2.2f;
				int damage3 = (int)base.Player.GetBestClassDamage().ApplyTo(LeviathanAmbergris.ambergrisDashDamage);
				Projectile.NewProjectile(base.Player.GetSource_FromThis(), base.Player.Center, Vector2.Zero, ModContent.ProjectileType<LeviAmberDash>(), damage3, 0f, base.Player.whoAmI);
			}
			if (base.Player.miscCounter % 3 == 2 && base.Player.dashDelay > 0)
			{
				base.Player.dashDelay--;
			}
			if (base.Player.dashDelay == -1 && (!HasCustomDash || UsedDash.DashTimeAdjustedForStartup >= 1))
			{
				base.Player.velocity.X *= 0.9f;
			}
		}
		if (sPauldron)
		{
			if (dashStart)
			{
				SoundStyle style = SoundID.DD2_BetsyFireballImpact with
				{
					Volume = 0.4f,
					PitchVariance = 0.4f
				};
				SoundEngine.PlaySound(in style, base.Player.Center);
				int damage4 = (int)base.Player.GetBestClassDamage().ApplyTo(SlagsplitterPauldron.PauldronSlamDamage);
				Projectile.NewProjectile(base.Player.GetSource_FromThis(), base.Player.Center + base.Player.velocity * 1.5f, Vector2.Zero, ModContent.ProjectileType<PauldronDash>(), damage4, 0f, base.Player.whoAmI);
			}
			if (base.Player.dashDelay == -1)
			{
				base.Player.endurance += 0.1f;
				base.Player.noKnockback = true;
			}
		}
		if (XykVisualsBlue || XykVisualsOrange)
		{
			bool Orange2 = XykVisualsOrange;
			if (dashStart)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DashSound");
				style.Volume = 0.7f;
				style.Pitch = Main.rand.NextFloat(0f, 0.2f) + (Orange2 ? 0f : 0.2f);
				SoundEngine.PlaySound(in style, base.Player.Center);
				if (Orange2)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, Vector2.Zero, XykFXColor, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, (float)Math.PI / 4f, 0.45f, 0.25f, 47, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, Vector2.Zero, XykFXColor, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, (float)Math.PI / 4f, 0f, 0.8f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				else
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, base.Player.velocity * 0.3f, XykFXColor, "CalamityMod/Particles/BloomRing", new Vector2(0.4f, 1f), base.Player.velocity.ToRotation(), 0f, 1f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, base.Player.velocity * 0.5f, XykFXColor, "CalamityMod/Particles/BloomRing", new Vector2(0.5f, 1f), base.Player.velocity.ToRotation(), 0f, 0.75f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
			if (base.Player.dashDelay == -1)
			{
				float sparkscale1 = MathF.Min(base.Player.velocity.X * (float)dir * 0.08f, 1.2f);
				Vector2 SparkVelocity1 = -base.Player.velocity.SafeNormalize(Vector2.UnitX) * 5f;
				if (!Orange2)
				{
					float sine2 = (float)Math.Sin((float)base.Player.miscCounter * 0.875f / (float)Math.PI);
					for (int l = -1; l <= 1; l += 2)
					{
						Vector2 offset2 = base.Player.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine2 * 18f * (float)l;
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Player.Center + offset2 + SparkVelocity1, SparkVelocity1 * 2f * sparkscale1, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, 0.3f * sparkscale1, XykFXColor * 0.85f, new Vector2(0.9f, 1.4f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.2f, 1f, 0.85f));
					}
				}
				else
				{
					MathF.Min(base.Player.velocity.X * (float)dir * 0.07f, 1.1f);
					for (int m = -1; m <= 1; m += 2)
					{
						Vector2 offset3 = base.Player.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * 17f * (float)m;
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Player.Center + offset3 + SparkVelocity1, SparkVelocity1 * 2f * sparkscale1, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 20, 0.4f * sparkscale1, XykFXColor * 0.95f, new Vector2(0.3f, 1.2f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.2f, 1f, 0.95f));
					}
				}
				for (int n = 0; n < 2; n++)
				{
					bool altDust = Main.rand.NextBool(3);
					Vector2 position2 = base.Player.Center + Main.rand.NextVector2Circular(20f, 30f) + SparkVelocity1;
					int type2 = ((!altDust) ? ModContent.DustType<SquashDust>() : (Orange2 ? ModContent.DustType<SquareDust>() : ModContent.DustType<SquashDustHollow>()));
					Vector2? velocity4 = SparkVelocity1 * Main.rand.NextFloat(0.5f, 3f) * sparkscale1;
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity4, 0, newColor, Main.rand.NextFloat(1.5f, 1.9f) * sparkscale1);
					dust2.noGravity = true;
					dust2.color = XykFXColor;
					dust2.fadeIn = ((!altDust) ? 2 : 0);
					if (altDust)
					{
						dust2.scale *= 0.5f;
					}
				}
			}
		}
		if ((sandCloak & dashStart) && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<SandCloakVeil>()) && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.SandCloak.ID))
		{
			Projectile.NewProjectile(base.Player.GetSource_FromThis(), base.Player.Center, Vector2.Zero, ModContent.ProjectileType<SandCloakVeil>(), 13, 2.5f, base.Player.whoAmI);
			SoundEngine.PlaySound(in SoundID.Item45, base.Player.Center);
		}
		if (cinnamonRoll && (!Main.getGoodWorld || !Main.npc.Any((NPC x) => x.active && x.type == ModContent.NPCType<DevourerofGodsHead>())))
		{
			if (dashStart)
			{
				base.Player.velocity.X *= 3f;
			}
			else if (base.Player.dashDelay == -1)
			{
				base.Player.velocity.X *= 0.8f;
			}
		}
		if (base.Player.dashDelay == -1)
		{
			IsFirstDashFrame = false;
		}
		else
		{
			IsFirstDashFrame = true;
		}
		if (ExternalMods.overhaul == null && ExternalMods.remnants == null && CalamityServerConfig.Instance.FasterBaseSpeed)
		{
			base.Player.moveSpeed *= BalancingConstants.DefaultMoveSpeedBoost;
		}
		moveSpeedBonus = base.Player.moveSpeed - 1f;
	}

	private void RevengeanceModeMiscEffects()
	{
		if (!CalamityWorld.revenge || base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (base.Player.onHitDodge)
		{
			for (int l = 0; l < Player.MaxBuffs; l++)
			{
				int hasBuff = base.Player.buffType[l];
				if (base.Player.buffTime[l] > 600 && hasBuff == 59)
				{
					base.Player.buffTime[l] = 600;
				}
			}
		}
		int immuneTimeLimit = 150;
		if (base.Player.immuneTime > immuneTimeLimit)
		{
			base.Player.immuneTime = immuneTimeLimit;
		}
		for (int k = 0; k < base.Player.hurtCooldowns.Length; k++)
		{
			if (base.Player.hurtCooldowns[k] > immuneTimeLimit)
			{
				base.Player.hurtCooldowns[k] = immuneTimeLimit;
			}
		}
	}

	private void UpdateRippers()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		if (rageBoostOne)
		{
			RageDuration += BalancingConstants.RageDurationPerBooster;
		}
		if (rageBoostTwo)
		{
			RageDuration += BalancingConstants.RageDurationPerBooster;
		}
		if (rageBoostThree)
		{
			RageDuration += BalancingConstants.RageDurationPerBooster;
		}
		if (rageCombatFrames > 0)
		{
			rageCombatFrames--;
		}
		if (rageGainCooldown > 0)
		{
			rageGainCooldown--;
		}
		float rageDiff = 0f;
		float rageGen = 0f;
		if (shatteredCommunity)
		{
			float scRageGen = rageMax * 0.02f / 60f;
			if (rageGen < scRageGen)
			{
				rageGen = scRageGen;
			}
		}
		else if (heartOfDarkness)
		{
			float hodRageGen = rageMax * 0.02f / 60f;
			if (rageGen < hodRageGen)
			{
				rageGen = hodRageGen;
			}
		}
		rageDiff += rageGen;
		if (heldGaelsLastFrame)
		{
			rageDiff += rageMax * GaelsGreatsword.RagePerSecond / 60f;
		}
		float rate = Main.GlobalTimeWrappedHourly * 29f;
		List<Color> eColors = new List<Color>
		{
			Color.PaleVioletRed,
			Color.Coral,
			Color.Khaki,
			Color.PaleGreen,
			Color.Turquoise,
			Color.Violet
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		lightRGB = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		float minProxRageDistance;
		if (!rageModeActive)
		{
			float bossProxRageMultiplier = 3f;
			minProxRageDistance = 160f;
			float maxProxRageDistance = 800f;
			float enemyDistance = maxProxRageDistance + 1f;
			float bossDistance = maxProxRageDistance + 1f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc = enumerator.Current;
				if (npc.type == 0 || !npc.IsAnEnemy() || !npc.Calamity().ProvidesProximityRage)
				{
					continue;
				}
				float generousHitboxWidth = Math.Max((float)npc.Hitbox.Width / 2f, (float)npc.Hitbox.Height / 2f);
				float hitboxEdgeDist = npc.Distance(base.Player.Center) - generousHitboxWidth;
				if (enemyDistance > hitboxEdgeDist)
				{
					enemyDistance = hitboxEdgeDist;
					if (npc.IsABoss() && !CalamityNPCSets.BossSegmentThatDoesNotGenerateRageFaster[npc.type])
					{
						bossDistance = hitboxEdgeDist;
					}
				}
			}
			if (enemyDistance <= maxProxRageDistance)
			{
				rageCombatFrames = Math.Max(rageCombatFrames, 3);
				float val = ProxRageFromDistance(enemyDistance);
				float proxRageFromBoss = 0f;
				if (bossDistance <= maxProxRageDistance)
				{
					proxRageFromBoss = bossProxRageMultiplier * ProxRageFromDistance(bossDistance);
				}
				float finalProxRage = Math.Max(val, proxRageFromBoss);
				rageDiff += finalProxRage * rageMax / (float)CalamityUtils.SecondsToFrames(45f);
			}
		}
		bool rageFading = rageCombatFrames <= 0 && !heartOfDarkness && !shatteredCommunity;
		if (rageModeActive)
		{
			rageDiff -= rageMax / (float)RageDuration;
		}
		else if (!rageModeActive & rageFading)
		{
			rageDiff -= rageMax / (float)BalancingConstants.RageFadeTime;
		}
		if (RageEnabled || rageDiff < 0f)
		{
			rage += rageDiff;
			if (rage < 0f)
			{
				rage = 0f;
			}
			if (rage >= rageMax)
			{
				if (!rageModeActive)
				{
					rage = rageMax;
				}
				else if (shatteredCommunity && rage >= 2f * rageMax)
				{
					rage = 2f * rageMax;
				}
				if (base.Player.whoAmI == Main.myPlayer && fullRageSoundCountdownTimer <= 0)
				{
					SoundEngine.PlaySound(in RageFilledSound);
				}
				fullRageSoundCountdownTimer = 300;
			}
		}
		float adrenalineDiff = 0f;
		bool wofAndNotHell = Main.wofNPCIndex >= 0 && base.Player.position.Y < (float)(Main.UnderworldLayer * 16);
		if (adrenalineModeActive)
		{
			adrenalineDiff = (0f - adrenalineMax) / (float)AdrenalineDuration;
			if (draedonsHeart && base.Player.miscCounter % 2 == 1)
			{
				base.Player.HealPlayer(DraedonsHeart.NanomachinesHealPerFrame, HealTextType.None);
				int dustID = 107;
				Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, dustID, 0f, 0f, 200);
				dust.noGravity = true;
				dust.fadeIn = 1.3f;
				Vector2 velocity = (dust.velocity = CalamityUtils.RandomVelocity(100f, 50f, 100f, 0.04f));
				((Vector2)(ref velocity)).Normalize();
				velocity *= 34f;
				dust.position = base.Player.Center - velocity;
			}
		}
		else if (areThereAnyDamnBosses && !wofAndNotHell)
		{
			adrenalineDiff += adrenalineMax / (float)AdrenalineChargeTime;
		}
		else if (!BossRushEvent.BossRushActive)
		{
			adrenalineDiff = (0f - adrenalineMax) / (float)AdrenalineFadeTime;
		}
		if (adrenalineDiff > 0f && stressPills)
		{
			adrenalineDiff *= 1.2f;
		}
		if ((AdrenalineEnabled || adrenalineDiff < 0f) && adrenalinePauseTimer == 0)
		{
			adrenaline += adrenalineDiff;
			if (adrenaline < 0f)
			{
				adrenaline = 0f;
			}
			if (adrenaline >= adrenalineMax)
			{
				adrenaline = adrenalineMax;
				if (base.Player.whoAmI == Main.myPlayer && playFullAdrenalineSound)
				{
					playFullAdrenalineSound = false;
					SoundEngine.PlaySound(in AdrenalineFilledSound);
				}
			}
			else
			{
				playFullAdrenalineSound = true;
			}
		}
		if (adrenalinePauseTimer > 0)
		{
			adrenalinePauseTimer--;
		}
		float ProxRageFromDistance(float dist)
		{
			float d = Math.Max(dist - minProxRageDistance, 0f);
			return MathHelper.Clamp(1f / (0.034f * d + 2f) + (590.5f - d) / 1181f, 0f, 1f);
		}
	}

	private void HandleTileEffects()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		int astralOreID = ModContent.TileType<AstralOre>();
		int auricOreID = ModContent.TileType<AuricOre>();
		int auricRepulserID = ModContent.TileType<AuricRepulserPanelTile>();
		int scoriaOreID = ModContent.TileType<ScoriaOre>();
		int abyssKelpID = ModContent.TileType<AbyssKelp>();
		int auricRejectionDamage = 300;
		float auricRejectionKB = (base.Player.noKnockback ? 20f : 40f);
		List<Point> list = new List<Point>();
		Collision.GetEntityEdgeTiles(list, base.Player);
		foreach (Point touchedTile in list)
		{
			Tile tile = Framing.GetTileSafely(touchedTile);
			if (!tile.HasTile || !tile.HasUnactuatedTile)
			{
				continue;
			}
			if (tile.TileType == abyssKelpID)
			{
				if (((Vector2)(ref base.Player.velocity)).Length() == 0f)
				{
					break;
				}
				Dust dust = Dust.NewDustDirect(base.Player.Center, 16, 16, 304, 0.2f, 0f, 0, new Color(117, 55, 15), Main.rand.NextFloat(1f, 2f));
				dust.noGravity = true;
				dust.noLight = true;
				dust.fadeIn = 2.5f;
			}
			if (!seraphTracers)
			{
				if (tile.TileType == astralOreID)
				{
					base.Player.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 2);
				}
				if (tile.TileType == scoriaOreID && !base.Player.fireWalk)
				{
					base.Player.AddBuff(67, 2);
				}
			}
			bool rejectionImmunity = auricSet || seraphTracers || base.Player.creativeGodMode || externalAuricRejectionImmunity;
			bool num = tile.TileType == auricOreID && !rejectionImmunity;
			bool repulserRejection = tile.TileType == auricRepulserID;
			if (num | repulserRejection)
			{
				base.Player.RemoveAllGrapplingHooks();
				if (tile.TileType == auricOreID)
				{
					AuricOre.Animate = true;
				}
				Vector2 yeetVec = Vector2.Normalize(base.Player.Center - touchedTile.ToWorldCoordinates());
				Player player = base.Player;
				player.velocity += yeetVec * auricRejectionKB;
				if (tile.TileType == auricOreID)
				{
					base.Player.Hurt(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.AuricRejection").ToNetworkText(base.Player.name)), auricRejectionDamage, 0);
					base.Player.AddBuff(ModContent.BuffType<AuricRebuke>(), 120);
				}
				SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/TeslaShoot1"), base.Player.Center);
			}
		}
	}

	private void HandleBlazingMouseEffects()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		blazingMouseAuraFade = MathHelper.Clamp(blazingMouseAuraFade - 0.025f, 0.25f, 1f);
		if (!blazingCursorDamage)
		{
			return;
		}
		Vector2 val = base.Player.Calamity().mouseWorld;
		Vector2 mouseClampedPosition = base.Player.ClampedMouseWorld();
		if (val != mouseClampedPosition)
		{
			return;
		}
		Rectangle sigilHitbox = Utils.CenteredRectangle(mouseClampedPosition, new Vector2(35f, 62f));
		int sigilDamage = (int)base.Player.GetBestClassDamage().ApplyTo(320f);
		bool brightenedSigil = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			Rectangle hitbox = target.Hitbox;
			if (!((Rectangle)(ref hitbox)).Intersects(sigilHitbox) || target.immortal || target.dontTakeDamage || target.friendly || NPCID.Sets.CountsAsCritter[target.type])
			{
				continue;
			}
			if (target.Calamity().cursorFocus < 300)
			{
				target.Calamity().cursorFocus += 3;
				float cursorFocusRatio = (float)target.Calamity().cursorFocus / 300f;
				GeneralParticleHandler.SpawnParticle(new StrongBloom(mouseClampedPosition, Vector2.Zero, Color.Lerp(Color.Magenta, Color.Red, cursorFocusRatio), cursorFocusRatio * 0.7f, 2));
				if (target.Calamity().cursorFocus >= 300)
				{
					int vHexDuration = 0;
					if (target.HasBuff<VulnerabilityHex>())
					{
						vHexDuration = target.buffTime[target.FindBuffIndex(ModContent.BuffType<VulnerabilityHex>())];
					}
					target.AddBuff(ModContent.BuffType<TrueVulnerabilityHex>(), (vHexDuration <= 300) ? vHexDuration : 300);
					target.RequestBuffRemoval(ModContent.BuffType<VulnerabilityHex>());
					SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/WeaponEnchant"), target.Center);
					for (int i = 0; i < 18; i++)
					{
						Vector2 orbVel = Utils.RotatedByRandom(new Vector2(15f, 15f), 3.1415927410125732) * Main.rand.NextFloat(0.33f, 1f);
						GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(target.Center + orbVel, orbVel, affectedByGravity: false, 60, Main.rand.NextFloat(0.95f, 1.75f), Color.Lerp(Color.Red, Color.Magenta, 0.3f)));
					}
				}
			}
			if (base.Player.miscCounter % 5 != 1)
			{
				break;
			}
			if (!brightenedSigil)
			{
				blazingMouseAuraFade = MathHelper.Clamp(blazingMouseAuraFade + 0.2f, 0.25f, 1f);
				brightenedSigil = true;
			}
			Projectile.NewProjectileDirect(base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<Calamity>())), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), sigilDamage, 0f, base.Player.whoAmI, target.whoAmI, 255f).DamageType = base.Player.GetBestClass();
			int buffToInflict = (target.Calamity().trueVulnerabilityHex ? ModContent.BuffType<TrueVulnerabilityHex>() : ModContent.BuffType<VulnerabilityHex>());
			if (!target.HasBuff(buffToInflict))
			{
				target.AddBuff(buffToInflict, 52);
			}
			target.buffTime[target.FindBuffIndex(buffToInflict)] += 8;
			if (target.buffTime[target.FindBuffIndex(buffToInflict)] < 60)
			{
				target.buffTime[target.FindBuffIndex(buffToInflict)] = 60;
			}
			for (int j = 0; j < 12; j++)
			{
				Dust fire = Dust.NewDustDirect(target.position, target.width, target.height, 267);
				fire.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2f, 3.45f));
				fire.scale = 1f + ((Vector2)(ref fire.velocity)).Length() / 6f;
				fire.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0.85f));
				fire.noGravity = true;
				fire.noLightEmittence = true;
			}
		}
	}

	private void MiscEffects()
	{
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16db: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1830: Unknown result type (might be due to invalid IL or missing references)
		//IL_183b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_185d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1862: Unknown result type (might be due to invalid IL or missing references)
		//IL_186a: Unknown result type (might be due to invalid IL or missing references)
		//IL_186f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1873: Unknown result type (might be due to invalid IL or missing references)
		//IL_1883: Unknown result type (might be due to invalid IL or missing references)
		//IL_1891: Unknown result type (might be due to invalid IL or missing references)
		//IL_1896: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18db: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1906: Unknown result type (might be due to invalid IL or missing references)
		//IL_190b: Unknown result type (might be due to invalid IL or missing references)
		//IL_190d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1921: Unknown result type (might be due to invalid IL or missing references)
		//IL_192e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1930: Unknown result type (might be due to invalid IL or missing references)
		//IL_1936: Unknown result type (might be due to invalid IL or missing references)
		//IL_1948: Unknown result type (might be due to invalid IL or missing references)
		//IL_194e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1950: Unknown result type (might be due to invalid IL or missing references)
		//IL_1955: Unknown result type (might be due to invalid IL or missing references)
		//IL_1957: Unknown result type (might be due to invalid IL or missing references)
		//IL_1972: Unknown result type (might be due to invalid IL or missing references)
		//IL_197d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1982: Unknown result type (might be due to invalid IL or missing references)
		//IL_1987: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_206a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2075: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f34: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f39: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f74: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2650: Unknown result type (might be due to invalid IL or missing references)
		//IL_2655: Unknown result type (might be due to invalid IL or missing references)
		//IL_265f: Unknown result type (might be due to invalid IL or missing references)
		//IL_266d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2241: Unknown result type (might be due to invalid IL or missing references)
		//IL_2248: Unknown result type (might be due to invalid IL or missing references)
		//IL_2178: Unknown result type (might be due to invalid IL or missing references)
		//IL_217f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ffe: Unknown result type (might be due to invalid IL or missing references)
		//IL_3006: Unknown result type (might be due to invalid IL or missing references)
		//IL_300b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3010: Unknown result type (might be due to invalid IL or missing references)
		//IL_3015: Unknown result type (might be due to invalid IL or missing references)
		//IL_302e: Unknown result type (might be due to invalid IL or missing references)
		//IL_305e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3063: Unknown result type (might be due to invalid IL or missing references)
		//IL_307c: Unknown result type (might be due to invalid IL or missing references)
		//IL_308b: Unknown result type (might be due to invalid IL or missing references)
		//IL_30e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_30fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3101: Unknown result type (might be due to invalid IL or missing references)
		//IL_3118: Unknown result type (might be due to invalid IL or missing references)
		//IL_311e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3153: Unknown result type (might be due to invalid IL or missing references)
		//IL_3158: Unknown result type (might be due to invalid IL or missing references)
		//IL_3174: Unknown result type (might be due to invalid IL or missing references)
		//IL_3179: Unknown result type (might be due to invalid IL or missing references)
		//IL_3185: Unknown result type (might be due to invalid IL or missing references)
		//IL_318a: Unknown result type (might be due to invalid IL or missing references)
		//IL_318f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3194: Unknown result type (might be due to invalid IL or missing references)
		//IL_31ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_31b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_26f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_26fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_347d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3487: Unknown result type (might be due to invalid IL or missing references)
		//IL_348c: Unknown result type (might be due to invalid IL or missing references)
		//IL_324e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3259: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_28fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2907: Unknown result type (might be due to invalid IL or missing references)
		//IL_2917: Unknown result type (might be due to invalid IL or missing references)
		//IL_291c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2921: Unknown result type (might be due to invalid IL or missing references)
		//IL_2955: Unknown result type (might be due to invalid IL or missing references)
		//IL_2957: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21be: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_32f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3300: Unknown result type (might be due to invalid IL or missing references)
		//IL_3310: Unknown result type (might be due to invalid IL or missing references)
		//IL_331f: Unknown result type (might be due to invalid IL or missing references)
		//IL_270a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2714: Unknown result type (might be due to invalid IL or missing references)
		//IL_2722: Unknown result type (might be due to invalid IL or missing references)
		//IL_273b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2740: Unknown result type (might be due to invalid IL or missing references)
		//IL_2748: Unknown result type (might be due to invalid IL or missing references)
		//IL_2752: Unknown result type (might be due to invalid IL or missing references)
		//IL_275c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2762: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_349c: Unknown result type (might be due to invalid IL or missing references)
		//IL_34a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_34a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_34b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_34b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_34cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_34d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34df: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_351b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3520: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_29bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_29bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_29db: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ab5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2abd: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_27a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_27b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_35dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_35e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_22eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2306: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_234c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2351: Unknown result type (might be due to invalid IL or missing references)
		//IL_2356: Unknown result type (might be due to invalid IL or missing references)
		//IL_236a: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2406: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_3df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ea5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_3edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eec: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ef0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_4471: Unknown result type (might be due to invalid IL or missing references)
		//IL_449e: Unknown result type (might be due to invalid IL or missing references)
		//IL_44a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_44b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_44b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_44ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_44cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_44d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_44e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_44e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4501: Unknown result type (might be due to invalid IL or missing references)
		//IL_4506: Unknown result type (might be due to invalid IL or missing references)
		//IL_45dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_45e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_488f: Unknown result type (might be due to invalid IL or missing references)
		//IL_48c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4919: Unknown result type (might be due to invalid IL or missing references)
		//IL_4923: Unknown result type (might be due to invalid IL or missing references)
		//IL_4928: Unknown result type (might be due to invalid IL or missing references)
		//IL_4682: Unknown result type (might be due to invalid IL or missing references)
		//IL_468d: Unknown result type (might be due to invalid IL or missing references)
		//IL_46b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_46f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_4742: Unknown result type (might be due to invalid IL or missing references)
		//IL_474c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4751: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c07: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5089: Unknown result type (might be due to invalid IL or missing references)
		//IL_50a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_516e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5198: Unknown result type (might be due to invalid IL or missing references)
		//IL_519e: Unknown result type (might be due to invalid IL or missing references)
		//IL_51b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_51bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_51c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_55f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_55f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_55fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_53eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_53f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_5268: Unknown result type (might be due to invalid IL or missing references)
		//IL_526f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5637: Unknown result type (might be due to invalid IL or missing references)
		//IL_563c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5641: Unknown result type (might be due to invalid IL or missing references)
		//IL_564f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5668: Unknown result type (might be due to invalid IL or missing references)
		//IL_566d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5693: Unknown result type (might be due to invalid IL or missing references)
		//IL_5698: Unknown result type (might be due to invalid IL or missing references)
		//IL_54cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_54d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_57ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_57d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_57d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_57e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_57ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_5804: Unknown result type (might be due to invalid IL or missing references)
		//IL_582a: Unknown result type (might be due to invalid IL or missing references)
		//IL_582f: Unknown result type (might be due to invalid IL or missing references)
		//IL_570b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5744: Unknown result type (might be due to invalid IL or missing references)
		//IL_574a: Unknown result type (might be due to invalid IL or missing references)
		//IL_575f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5769: Unknown result type (might be due to invalid IL or missing references)
		//IL_576e: Unknown result type (might be due to invalid IL or missing references)
		//IL_589b: Unknown result type (might be due to invalid IL or missing references)
		//IL_58d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_58da: Unknown result type (might be due to invalid IL or missing references)
		//IL_58ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_58f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_58fe: Unknown result type (might be due to invalid IL or missing references)
		if (ManaBurnFireDrawer != null)
		{
			ManaBurnFireDrawer.LocalTimer = 0;
			ManaBurnFireDrawer.RelativePower = MathHelper.Lerp(0.25f, 0.5f, (float)(-base.Player.statMana) / (float)base.Player.statManaMax2);
			ManaBurnFireDrawer.Update();
		}
		if (!Main.dedServ && base.Player.whoAmI == Main.myPlayer)
		{
			Asset<Texture2D> carpetAuric = ExtraTextureRefs.FlyingCarpetAuric;
			Asset<Texture2D> carpetOriginal = ExtraTextureRefs.FlyingCarpetVanilla;
			TextureAssets.FlyingCarpet = (auricSet ? carpetAuric : carpetOriginal);
			for (int l = 0; l < Player.MaxBuffs; l++)
			{
				if (base.Player.buffType[l] == 257)
				{
					if (base.Player.buffTime[l] > CalamityUtils.SecondsToFrames(600f))
					{
						TextureAssets.Buff[257] = ExtraTextureRefs.LuckIconGreater;
					}
					else if (base.Player.buffTime[l] > CalamityUtils.SecondsToFrames(300f))
					{
						TextureAssets.Buff[257] = ExtraTextureRefs.LuckIconVanilla;
					}
					else
					{
						TextureAssets.Buff[257] = ExtraTextureRefs.LuckIconLesser;
					}
					break;
				}
			}
		}
		if (base.Player.mount.Active && base.Player.mount.Type == ModContent.MountType<DoGCartMount>())
		{
			SmoothenedMinecartRotation = MathHelper.Lerp(SmoothenedMinecartRotation, DelegateMethods.Minecart.rotation, 0.05f);
			int direction = (base.Player.velocity.SafeNormalize(Vector2.UnitX * (float)base.Player.direction).X > 0f).ToDirectionInt();
			if (base.Player.velocity.X == 0f)
			{
				direction = base.Player.direction;
			}
			float idealRotation = DoGCartMount.CalculateIdealWormRotation(base.Player);
			float minecartRotation = DelegateMethods.Minecart.rotation;
			if (Math.Abs(minecartRotation) < 0.5f)
			{
				minecartRotation = 0f;
			}
			Vector2 stickOffset = minecartRotation.ToRotationVector2() * ((Vector2)(ref base.Player.velocity)).Length() * (float)direction * 1.25f;
			for (int i = 0; i < DoGCartSegments.Length; i++)
			{
				if (DoGCartSegments[i] == null)
				{
					DoGCartSegments[i] = new DoGCartSegment
					{
						Center = base.Player.Center - idealRotation.ToRotationVector2() * (float)i * 20f
					};
				}
			}
			Vector2 startingStickPosition = base.Player.Center + stickOffset + new Vector2((float)direction * (float)Math.Cos(SmoothenedMinecartRotation * 2f) * -34f, 12f);
			DoGCartSegments[0].Update(base.Player, startingStickPosition, idealRotation);
			DoGCartSegments[0].Center = startingStickPosition;
			for (int j = 1; j < DoGCartSegments.Length; j++)
			{
				Vector2 waveOffset = DoGCartMount.CalculateSegmentWaveOffset(j, base.Player);
				DoGCartSegments[j].Update(base.Player, DoGCartSegments[j - 1].Center + waveOffset, DoGCartSegments[j - 1].Rotation);
			}
		}
		else
		{
			DoGCartSegments = new DoGCartSegment[DoGCartSegments.Length];
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<PhosphorescentGauntlet>())
		{
			PhosphorescentGauntletPunches.GenerateDustOnOwnerHand(base.Player);
		}
		if (temporaryStealthTimer > 0)
		{
			stealthUIAlpha = 1f;
		}
		else if (stealthUIAlpha > 0f && (rogueStealth <= 0f || rogueStealthMax <= 0f))
		{
			stealthUIAlpha -= 0.035f;
			stealthUIAlpha = MathHelper.Clamp(stealthUIAlpha, 0f, 1f);
		}
		else if (stealthUIAlpha < 1f)
		{
			stealthUIAlpha += 0.035f;
			stealthUIAlpha = MathHelper.Clamp(stealthUIAlpha, 0f, 1f);
		}
		if (andromedaState == AndromedaPlayerState.LargeRobot || base.Player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfDeliveranceSpear>()] > 0)
		{
			base.Player.controlHook = (base.Player.releaseHook = false);
		}
		if (andromedaCripple > 0)
		{
			base.Player.velocity = Vector2.Clamp(base.Player.velocity, new Vector2(-11f, -8f), new Vector2(11f, 8f));
			andromedaCripple--;
		}
		if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<GiantIbanRobotOfDoom>()] <= 0 && andromedaState != AndromedaPlayerState.Inactive)
		{
			andromedaState = AndromedaPlayerState.Inactive;
		}
		if (andromedaState == AndromedaPlayerState.LargeRobot)
		{
			base.Player.width = 80;
			base.Player.height = 212;
			base.Player.position.Y -= 170f;
			resetHeightandWidth = true;
		}
		else if (andromedaState == AndromedaPlayerState.SpecialAttack)
		{
			base.Player.width = 24;
			base.Player.height = 98;
			base.Player.position.Y -= 56f;
			resetHeightandWidth = true;
		}
		else if (!base.Player.mount.Active && resetHeightandWidth)
		{
			base.Player.width = 20;
			base.Player.height = 42;
			resetHeightandWidth = false;
		}
		if (spiritOrigin)
		{
			int bullseyeType = ModContent.ProjectileType<SpiritOriginBullseye>();
			List<int> alreadyTargetedNPCs = new List<int>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == bullseyeType && p.owner == base.Player.whoAmI)
				{
					alreadyTargetedNPCs.Add((int)p.ai[0]);
				}
			}
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC target = enumerator2.Current;
				if (target.friendly || target.lifeMax < 5 || alreadyTargetedNPCs.Contains(target.whoAmI) || target.realLife >= 0 || target.dontTakeDamage || target.immortal || target.townNPC || NPCID.Sets.ActsLikeTownNPC[target.type] || NPCID.Sets.CountsAsCritter[target.type])
				{
					continue;
				}
				IEntitySource source = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<DaawnlightSpiritOrigin>()));
				if (Main.myPlayer != base.Player.whoAmI || !target.WithinRange(base.Player.Center, 2000f))
				{
					continue;
				}
				Projectile.NewProjectile(source, target.Center, Vector2.Zero, bullseyeType, 0, 0f, base.Player.whoAmI, target.whoAmI);
				ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					Projectile proj = enumerator3.Current;
					if (proj.owner == base.Player.whoAmI && proj.type == ModContent.ProjectileType<DaawnlightSpiritOriginMinion>())
					{
						DaawnlightSpiritOriginMinion dsoPet = proj.ModProjectile<DaawnlightSpiritOriginMinion>();
						dsoPet.Projectile.spriteDirection = MathF.Sign(dsoPet.Projectile.Center.X - target.Center.X);
						if (dsoPet.CurrentAnimation != DaawnlightSpiritOriginMinion.AnimationState.Pointing)
						{
							dsoPet.CurrentAnimation = DaawnlightSpiritOriginMinion.AnimationState.Pointing;
						}
						break;
					}
				}
			}
		}
		if (brittleStar && brittleStarBuffMode)
		{
			base.Player.statDefense += 4 * base.Player.ownedProjectileCounts[ModContent.ProjectileType<BrittleStarMinion>()];
		}
		float lifeStealRecoveryRateReduction = (Main.expertMode ? BalancingConstants.LifeStealRecoveryRateReduction_Expert : BalancingConstants.LifeStealRecoveryRateReduction_Classic);
		float lifeStealCap = (Main.expertMode ? BalancingConstants.LifeStealCap_Expert : BalancingConstants.LifeStealCap_Classic);
		if (base.Player.lifeSteal < lifeStealCap)
		{
			base.Player.lifeSteal -= lifeStealRecoveryRateReduction;
		}
		if (Main.myPlayer == base.Player.whoAmI)
		{
			BossHealthBarManager.CanDrawExtraSmallText = shouldDrawSmallText;
		}
		if (margarita && Main.myPlayer == base.Player.whoAmI)
		{
			for (int k = 0; k < Player.MaxBuffs; k++)
			{
				int buffID = base.Player.buffType[k];
				if (base.Player.buffTime[k] > 2 && CalamityBuffSets.IsDebuff[buffID])
				{
					base.Player.buffTime[k]--;
				}
			}
		}
		float providenceBurnIntensity = 0f;
		int provID = ModContent.NPCType<Providence>();
		if (Main.npc.IndexInRange(CalamityGlobalNPC.holyBoss) && Main.npc[CalamityGlobalNPC.holyBoss].active && Main.npc[CalamityGlobalNPC.holyBoss].type == provID)
		{
			providenceBurnIntensity = (Main.npc[CalamityGlobalNPC.holyBoss].ModNPC as Providence).CalculateBurnIntensity();
		}
		ProvidenceBurnEffectDrawer.ParticleSpawnRate = int.MaxValue;
		if (holyInferno)
		{
			ProvidenceBurnEffectDrawer.ParticleSpawnRate = 1;
		}
		else if (providenceBurnIntensity > 0f)
		{
			int cinderCount = (int)MathHelper.Lerp(1f, 4f, Utils.GetLerpValue(0f, 0.45f, providenceBurnIntensity, clamped: true));
			for (int m = 0; m < cinderCount; m++)
			{
				if (Main.rand.NextBool(3))
				{
					Dust dust = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 244);
					dust.velocity = Main.rand.NextVector2Circular(3.5f, 3.5f);
					dust.velocity.Y -= Main.rand.NextFloat(1f, 3f);
					dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
					dust.noGravity = true;
				}
			}
		}
		ProvidenceBurnEffectDrawer.Update();
		if (holyInferno && holyInfernoFadeIntensity < 1f)
		{
			holyInfernoFadeIntensity = MathHelper.Clamp(holyInfernoFadeIntensity + 0.015f, 0f, 1f);
		}
		else if (!holyInferno && holyInfernoFadeIntensity > 0f)
		{
			holyInfernoFadeIntensity = MathHelper.Clamp(holyInfernoFadeIntensity - 0.01f, 0f, 1f);
		}
		bool canBreath = (aquaticHeart && NPC.downedBoss3) || base.Player.gills || base.Player.merman;
		if (base.Player.arcticDivingGear | canBreath)
		{
			base.Player.buffImmune[ModContent.BuffType<FrozenLungs>()] = true;
		}
		if (CalamityServerConfig.Instance.ChilledWaterRework)
		{
			if (Main.expertMode && base.Player.ZoneSnow && base.Player.wet && !base.Player.lavaWet && !base.Player.honeyWet)
			{
				base.Player.buffImmune[46] = true;
				if (base.Player.IsUnderwater() && Main.myPlayer == base.Player.whoAmI)
				{
					base.Player.AddBuff(ModContent.BuffType<FrozenLungs>(), 2, quiet: false);
				}
			}
			if (frozenLungs && base.Player.breath > 0 && base.Player.miscCounter % 2 == 0)
			{
				base.Player.breath--;
			}
		}
		if (!base.Player.lavaWet)
		{
			if (base.Player.lavaImmune && base.Player.lavaTime < base.Player.lavaMax)
			{
				base.Player.lavaTime++;
			}
		}
		else if (ZoneCalamity && !flameLickedShell)
		{
			base.Player.AddBuff(ModContent.BuffType<SearingLava>(), 2, quiet: false);
		}
		if (base.Player.whoAmI == Main.myPlayer && AcidRainEvent.AcidRainEventIsOngoing && ZoneSulphur && !areThereAnyDamnBosses && (double)base.Player.Center.Y < Main.worldSurface * 16.0 + 800.0)
		{
			int slimeRainRate = (int)((double)MathHelper.Clamp((float)Main.invasionSize * 0.4f, 13.5f, 50f) * 2.25);
			Vector2 spawnPoint = default(Vector2);
			((Vector2)(ref spawnPoint))._002Ector(base.Player.Center.X + (float)Main.rand.Next(-1000, 1001), base.Player.Center.Y - (float)Main.rand.Next(700, 801));
			if ((float)(base.Player.miscCounter % slimeRainRate) == 0f && DownedBossSystem.downedAquaticScourge && !DownedBossSystem.downedPolterghast && Main.rand.NextBool(12))
			{
				NPC.NewNPC(new EntitySource_SpawnNPC(), (int)spawnPoint.X, (int)spawnPoint.Y, ModContent.NPCType<IrradiatedSlime>());
			}
		}
		if (base.Player.whoAmI == Main.myPlayer)
		{
			if (hydrothermalSmoke && (Math.Abs(base.Player.velocity.X) > 0.1f || Math.Abs(base.Player.velocity.Y) > 0.1f))
			{
				Projectile.NewProjectile(base.Player.GetSource_FromThis(HydrothermicArmor.VanitySmokeEntitySourceContext), base.Player.Center, Vector2.Zero, ModContent.ProjectileType<HydrothermalSmoke>(), 0, 0f, base.Player.whoAmI);
			}
			if (!base.Player.armorEffectDrawOutlines)
			{
				hydrothermalSmoke = false;
			}
		}
		caveDarkness = 0f;
		if ((CalamityWorld.death || Main.getGoodWorld) && base.Player.whoAmI == Main.myPlayer)
		{
			switch (((base.Player.mount.Active && base.Player.mount.Cart) ? Collision.HurtTiles(base.Player.position, base.Player.width, base.Player.height - 16, base.Player) : Collision.HurtTiles(base.Player.position, base.Player.width, base.Player.height, base.Player)).type)
			{
			case 10:
				base.Player.AddBuff(33, 300, quiet: false);
				base.Player.AddBuff(30, 300, quiet: false);
				break;
			case 17:
				base.Player.AddBuff(20, 300, quiet: false);
				break;
			case 80:
				base.Player.AddBuff(70, 300, quiet: false);
				break;
			}
		}
		if (!base.Player.mount.Active)
		{
			if (!base.Player.wet)
			{
				if (aeroSet)
				{
					base.Player.maxFallSpeed = AerospecBreastplate.SetBonusFallSpeed;
				}
				if (base.Player.PortalPhysicsEnabled)
				{
					base.Player.maxFallSpeed = 20f;
				}
			}
			if (base.Player.controlDown && !base.Player.controlJump && (ironBoots || gSabaton) && !gSabatonFalling)
			{
				base.Player.maxFallSpeed *= 2f;
				if ((base.Player.gravDir == 1f) ? (base.Player.velocity.Y <= 0f) : (base.Player.velocity.Y >= 0f))
				{
					base.Player.velocity.Y *= 0.7f;
				}
			}
			if (LungingDown)
			{
				base.Player.maxFallSpeed = 80f;
				base.Player.noFallDmg = true;
			}
			if (CalamityClientConfig.Instance.FasterFallHotkey)
			{
				bool num = base.Player.controlDown && !base.Player.controlJump;
				base.Player.ControlsEnabled();
				bool notInLiquid = !base.Player.wet;
				bool notOnRope = !base.Player.pulley && base.Player.ropeCount == 0;
				bool notGrappling = base.Player.grappling[0] == -1;
				bool airborne = base.Player.velocity.Y != 0f;
				if (((num && base.Player.ControlsEnabled()) & notInLiquid & notOnRope & notGrappling & airborne) && !base.Player.Calamity().gSabatonFalling)
				{
					base.Player.velocity.Y += base.Player.gravity * base.Player.gravDir * (BalancingConstants.HoldingDownGravityMultiplier - 1f);
					if (base.Player.velocity.Y * base.Player.gravDir > base.Player.maxFallSpeed)
					{
						base.Player.velocity.Y = base.Player.maxFallSpeed * base.Player.gravDir;
					}
				}
			}
		}
		else if (base.Player.mount.Type == 3)
		{
			base.Player.velocity.X *= 0.91f;
		}
		else if (base.Player.mount.Type == 50)
		{
			base.Player.velocity.X *= 0.95f;
		}
		else if (base.Player.mount.Type == 43)
		{
			if (base.Player.velocity.X > 10f || base.Player.velocity.X < -10f)
			{
				base.Player.velocity.X *= 0.99f;
			}
			base.Player.maxFallSpeed *= 0.75f;
		}
		if (CalamityServerConfig.Instance.FasterRopeClimbSpeed && base.Player.pulley)
		{
			int xPos = (int)(base.Player.position.X + (float)(base.Player.width / 2)) / 16;
			int yPos = (int)(base.Player.position.Y - 16f) / 16;
			int yPos2 = (int)(base.Player.position.Y - 8f) / 16;
			bool ropeAbove = true;
			bool onRope = false;
			if (WorldGen.IsRope(xPos, yPos2 - 1) || WorldGen.IsRope(xPos, yPos2 + 1))
			{
				onRope = true;
			}
			if (!WorldGen.IsRope(xPos, yPos))
			{
				ropeAbove = false;
				if (base.Player.velocity.Y < 0f)
				{
					base.Player.velocity.Y = 0f;
				}
			}
			if (onRope)
			{
				if (base.Player.controlUp & ropeAbove)
				{
					if (base.Player.velocity.Y > 0f)
					{
						base.Player.velocity.Y *= 0.7f;
					}
					if (base.Player.velocity.Y > -3f)
					{
						base.Player.velocity.Y -= 0.2f;
					}
					else
					{
						base.Player.velocity.Y -= 0.18f;
					}
					if (base.Player.velocity.Y < -8f)
					{
						base.Player.velocity.Y = -8f;
					}
				}
				else if (base.Player.controlDown)
				{
					if (base.Player.velocity.Y < 0f)
					{
						base.Player.velocity.Y *= 0.7f;
					}
					if (base.Player.velocity.Y < 3f)
					{
						base.Player.velocity.Y += 0.4f;
					}
					else
					{
						base.Player.velocity.Y += 0.2f;
					}
					if (base.Player.velocity.Y > base.Player.maxFallSpeed)
					{
						base.Player.velocity.Y = base.Player.maxFallSpeed;
					}
				}
				else if (Math.Abs(base.Player.velocity.Y) > 0f)
				{
					base.Player.velocity.Y *= 0.7f;
					if (Math.Abs(base.Player.velocity.Y) < 0.1f)
					{
						base.Player.velocity.Y = 0f;
					}
				}
			}
		}
		if (omegaBlueSet)
		{
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<OmegaBlueTentacle>()] < 6 && Main.myPlayer == base.Player.whoAmI)
			{
				bool[] tentaclesPresent = new bool[6];
				ActiveEntityIterator<Projectile>.Enumerator enumerator4 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator4.MoveNext())
				{
					Projectile projectile = enumerator4.Current;
					if (projectile.type == ModContent.ProjectileType<OmegaBlueTentacle>() && projectile.owner == Main.myPlayer && projectile.ai[1] >= 0f && projectile.ai[1] < 6f)
					{
						tentaclesPresent[(int)projectile.ai[1]] = true;
					}
				}
				for (int n = 0; n < 6; n++)
				{
					if (!tentaclesPresent[n])
					{
						int damage = (int)base.Player.GetBestClassDamage().ApplyTo(OmegaBlueHelmet.TentacleDamage);
						Projectile.NewProjectile(base.Player.GetSource_FromThis(OmegaBlueHelmet.TentacleEntitySourceContext), velocity: new Vector2((float)Main.rand.Next(-13, 14), (float)Main.rand.Next(-13, 14)) * 0.25f, position: base.Player.Center, Type: ModContent.ProjectileType<OmegaBlueTentacle>(), Damage: damage, KnockBack: 8f, Owner: Main.myPlayer, ai0: Main.rand.Next(120), ai1: n);
					}
				}
			}
			if (omegaBlueAbyssalMadness)
			{
				base.Player.GetDamage<GenericDamageClass>() += OmegaBlueHelmet.MadnessDamageBoost;
				base.Player.GetCritChance<GenericDamageClass>() += OmegaBlueHelmet.MadnessCritBoost;
			}
		}
		if (profanedCrystalBuffs || (!profanedCrystal && pSoulArtifact) || (profanedCrystal && DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs))
		{
			base.Player.maxMinions++;
			if (healCounter > 0)
			{
				healCounter--;
			}
			if (healCounter <= 0)
			{
				healCounter = 300;
				if (base.Player.whoAmI == Main.myPlayer)
				{
					base.Player.HealPlayer(15);
					if (profanedCrystal)
					{
						int healerID = ModContent.ProjectileType<MiniGuardianHealer>();
						Projectile healer = Main.projectile.FirstOrDefault((Projectile projectile9) => projectile9.active && projectile9.owner == Main.myPlayer && projectile9.type == healerID, null);
						if (healer != null)
						{
							int maxHealDustIterations = (int)Vector2.Distance(healer.Center, base.Player.Center);
							int maxDust = 40;
							int dustDivisor = maxHealDustIterations / maxDust;
							if (dustDivisor < 2)
							{
								dustDivisor = 2;
							}
							Vector2 dustLineStart = healer.Center;
							Vector2 dustLineEnd = base.Player.Center;
							Vector2 currentDustPos = default(Vector2);
							Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
							Vector2 healerDustVel = default(Vector2);
							((Vector2)(ref healerDustVel))._002Ector(2.1f, 2f);
							Color dustColor = Main.hslToRgb(Main.rgbToHsl(new Color(255, 200, Main.DiscoB)).X, 1f, 0.5f);
							((Color)(ref dustColor)).A = byte.MaxValue;
							for (int i2 = 0; i2 < maxHealDustIterations; i2++)
							{
								if (i2 % dustDivisor == 0)
								{
									currentDustPos = Vector2.Lerp(dustLineStart, dustLineEnd, (float)i2 / (float)maxHealDustIterations);
									Dust dust2 = Dust.NewDustDirect(currentDustPos, 0, 0, 267, 0f, 0f, 0, dustColor);
									dust2.position = currentDustPos;
									dust2.velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * (float)i2 / (float)maxHealDustIterations) * healerDustVel * (0.8f + Main.rand.NextFloat() * 0.4f) + base.Player.velocity;
									dust2.noGravity = true;
									dust2.scale = 1f;
									dust2.fadeIn = Main.rand.NextFloat() * 2f;
									Dust dust3 = DustExtensions.BetterCloneDust(dust2);
									dust3.scale /= 2f;
									dust3.fadeIn /= 2f;
									dust3.color = new Color(255, 255, 255, 255);
								}
							}
						}
					}
				}
			}
		}
		if (bootLevel > 0)
		{
			HeatDebuffMultiplier += 0.25f * (float)bootLevel;
		}
		if (rOfResilienceEffect > 0)
		{
			rOfResilienceEffect--;
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<RelicOfResilience>())
		{
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<RelicGuard>()] < 1 && !base.Player.dead)
			{
				Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), base.Player.Center, Vector2.Zero, ModContent.ProjectileType<RelicGuard>(), 0, 0f, base.Player.whoAmI);
			}
			for (int index = 0; index < Main.player.Length; index++)
			{
				Player fella = Main.player[index];
				if (fella.Center.Distance(base.Player.Center) < 650f && fella.team == base.Player.team)
				{
					fella.Calamity().rOfResilienceEffect = ((fella != base.Player) ? 480 : 2);
				}
			}
		}
		if (rOfResilienceEffect > 0)
		{
			if (base.Player.Calamity().mouseRight && !base.Player.mouseInterface && rOfResilienceCooldown == 0)
			{
				CalamityUtils.AddCooldown(duration: rOfResilienceCooldown = (base.Player.Calamity().profanedSoulRelicBuff ? 300 : 600), p: base.Player, id: RelicOfResilienceCooldown.ID);
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRockShieldActivate");
				style.Volume = 0.7f;
				style.Pitch = -0.1f;
				SoundEngine.PlaySound(in style, base.Player.Center);
			}
			if (base.Player.Calamity().rOfResilienceCooldown > 300 || base.Player.Calamity().rOfResilienceCooldown == 0)
			{
				float fadeStats = ((base.Player.Calamity().rOfResilienceCooldown == 0) ? 1f : Utils.GetLerpValue(300f, 600f, base.Player.Calamity().rOfResilienceCooldown, clamped: true));
				int maxDefFloor = (int)(150f * fadeStats);
				float MaxDRFloor = 0.1f * fadeStats;
				if ((int)base.Player.statDefense < maxDefFloor)
				{
					base.Player.statDefense += maxDefFloor - (int)base.Player.statDefense;
				}
				if (base.Player.endurance < MaxDRFloor)
				{
					base.Player.endurance += MaxDRFloor - base.Player.endurance;
				}
			}
			int numOfShards = 0;
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile2 = Main.projectile[x];
				if (projectile2.active && projectile2.type == ModContent.ProjectileType<ArtifactOfResilienceShards>() && projectile2.ai[1] == 0f && projectile2.owner == base.Player.whoAmI)
				{
					numOfShards++;
				}
			}
			if (base.Player.miscCounter % (base.Player.Calamity().profanedSoulRelicBuff ? 4 : 8) == 0 && numOfShards < (base.Player.Calamity().profanedSoulRelicBuff ? 75 : 30) && base.Player.Calamity().rOfResilienceCooldown == 0)
			{
				if (numOfShards == 0)
				{
					rOfResilienceOrbitOffset = Main.rand.Next(0, 101);
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianDash");
					style.Volume = 0.5f;
					style.Pitch = -0.3f;
					SoundEngine.PlaySound(in style, base.Player.Center);
				}
				int shardDamage = (int)base.Player.GetBestClassDamage().ApplyTo(420f);
				Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), base.Player.Center, new Vector2(1f, 0f), ModContent.ProjectileType<ArtifactOfResilienceShards>(), shardDamage, 0f, base.Player.whoAmI, 0f, 0f, numOfShards + 1);
			}
		}
		if (rOfResilienceCooldown > 0)
		{
			rOfResilienceCooldown--;
		}
		if (transformer && base.Player.Calamity().transformerCooldown == 0 && transformerDelay == 0)
		{
			float zoneSize = 150f;
			int blobDamage = (int)base.Player.GetBestClassDamage().ApplyTo(TheTransformer.blobDamage);
			int numOfBlobs = base.Player.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()];
			if (numOfBlobs >= TheTransformer.blobCap && !Main.zenithWorld)
			{
				base.Player.Calamity().transformerStoredKills = 0;
			}
			if (base.Player.Calamity().transformerStoredKills > 0 && (numOfBlobs < TheTransformer.blobCap || Main.zenithWorld))
			{
				transformerDelay = 2;
				int layer = (int)(Utils.GetLerpValue(0f, 10f, numOfBlobs + 1) + 0.9f);
				Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), base.Player.Center, Utils.RotatedByRandom(new Vector2(0f, 16f), 3.1415927410125732), ModContent.ProjectileType<TransformerBlob>(), blobDamage, 0f, base.Player.whoAmI, layer, numOfBlobs + 1);
				if (base.Player.Calamity().transformerVisual)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Player.Center, Vector2.Zero, Color.LightSkyBlue, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.5f, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/NullImpact");
					style.Volume = 0.1f;
					style.Pitch = Main.rand.NextFloat(-0.3f, -0.4f) + (float)numOfBlobs * 0.015f;
					style.MaxInstances = -1;
					SoundEngine.PlaySound(in style, base.Player.Center);
				}
				ActiveEntityIterator<Projectile>.Enumerator enumerator5 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator5.MoveNext())
				{
					Projectile p2 = enumerator5.Current;
					float angleMax = MathHelper.ToRadians((float)(360 * layer));
					if (numOfBlobs == 0)
					{
						angleMax = 0f;
					}
					if (p2.type == ModContent.ProjectileType<TransformerBlob>() && p2.owner == base.Player.whoAmI)
					{
						int blobNum = base.Player.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()] + 1;
						float insanityValue = p2.ai[1] % 10f;
						if (p2.ai[0] == (float)layer)
						{
							p2.ai[2] = insanityValue / (float)blobNum * angleMax - angleMax / 2f;
						}
						p2.netUpdate = true;
					}
				}
				base.Player.Calamity().transformerStoredKills--;
			}
			else
			{
				for (int x2 = 0; x2 < Main.maxProjectiles; x2++)
				{
					Projectile projectile3 = Main.projectile[x2];
					if (Main.zenithWorld && Vector2.Distance(base.Player.Center, projectile3.Center) <= zoneSize && projectile3.active && projectile3.hostile)
					{
						projectile3.velocity = Vector2.Lerp(projectile3.velocity, base.Player.Center.DirectionTo(projectile3.Center) * 4f, Utils.GetLerpValue(300f, 230f, projectile3.timeLeft, clamped: true));
						projectile3.timeLeft = (int)MathHelper.Lerp((float)projectile3.timeLeft, 0f, 0.25f);
						if (projectile3.damage > 1)
						{
							projectile3.damage = (int)((float)projectile3.damage * 0.85f);
						}
						projectile3.friendly = true;
					}
					if (!(Vector2.Distance(base.Player.Center, projectile3.Center) <= zoneSize) || !projectile3.active || !projectile3.hostile || projectile3.Calamity().TransformerTimer != 0 || (numOfBlobs >= TheTransformer.blobCap && !Main.zenithWorld) || transformerDelay != 0)
					{
						continue;
					}
					transformerDelay = 2;
					projectile3.Calamity().TransformerTimer = (Main.zenithWorld ? 3 : 30);
					int layer2 = (int)(Utils.GetLerpValue(0f, 10f, numOfBlobs + 1) + 0.9f);
					Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), projectile3.Center, projectile3.velocity.SafeNormalize(Vector2.UnitX) * 16f, ModContent.ProjectileType<TransformerBlob>(), blobDamage, 0f, base.Player.whoAmI, layer2, numOfBlobs + 1);
					if (base.Player.Calamity().transformerVisual)
					{
						GeneralParticleHandler.SpawnParticle(new CustomPulse(projectile3.Center, Vector2.Zero, Color.LightSkyBlue, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.5f, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/NullImpact");
						style.Volume = 0.1f;
						style.Pitch = Main.rand.NextFloat(-0.3f, -0.4f) + (float)numOfBlobs * 0.015f;
						style.MaxInstances = -1;
						SoundEngine.PlaySound(in style, projectile3.Center);
					}
					float index2 = 1f;
					ActiveEntityIterator<Projectile>.Enumerator enumerator6 = Main.ActiveProjectiles.GetEnumerator();
					while (enumerator6.MoveNext())
					{
						Projectile p3 = enumerator6.Current;
						float angleMax2 = MathHelper.ToRadians((float)(360 * layer2));
						if (numOfBlobs == 0)
						{
							angleMax2 = 0f;
						}
						if (p3.type == ModContent.ProjectileType<TransformerBlob>() && p3.owner == base.Player.whoAmI)
						{
							int blobNum2 = base.Player.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()] + 1;
							float insanityValue2 = p3.ai[1] % 10f;
							if (p3.ai[0] == (float)layer2)
							{
								p3.ai[2] = insanityValue2 / (float)blobNum2 * angleMax2 - angleMax2 / 2f;
							}
							p3.netUpdate = true;
							index2++;
						}
					}
				}
			}
		}
		if (transformerDelay > 0)
		{
			transformerDelay--;
		}
		if (sSpiritAmulet)
		{
			int spawnTime = 75;
			int energyCap = 8;
			Projectile projectile4 = null;
			if (base.Player.dashDelay == -1)
			{
				int energyCount = 0;
				for (int x3 = 0; x3 < Main.maxProjectiles; x3++)
				{
					projectile4 = Main.projectile[x3];
					if (projectile4.active && projectile4.type == ModContent.ProjectileType<AmuletEnergy>() && projectile4.ai[2] == 0f && projectile4.owner == base.Player.whoAmI)
					{
						projectile4.ai[2] = 5f;
						energyCount++;
					}
				}
				if (energyCount > 0)
				{
					sSpiritAmuletTimer = -spawnTime * 3;
				}
			}
			int numOfEnergy = 0;
			for (int x4 = 0; x4 < Main.maxProjectiles; x4++)
			{
				projectile4 = Main.projectile[x4];
				if (projectile4.active && projectile4.type == ModContent.ProjectileType<AmuletEnergy>() && projectile4.ai[2] == 0f && projectile4.owner == base.Player.whoAmI)
				{
					numOfEnergy++;
				}
			}
			if (sSpiritAmuletTimer >= spawnTime)
			{
				if (numOfEnergy < energyCap)
				{
					int energyDamage = (int)base.Player.GetBestClassDamage().ApplyTo(8f);
					Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), base.Player.Center, (Vector2.One * 4f).RotatedByRandom(6.2831854820251465), ModContent.ProjectileType<AmuletEnergy>(), energyDamage, 0f, base.Player.whoAmI, 0f, numOfEnergy);
					if (numOfEnergy + 1 == energyCap && base.Player.Calamity().sSpiritAmuletVisual)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WaterSplash1");
						style.Volume = 0.25f;
						style.Pitch = 0.9f;
						style.MaxInstances = -1;
						SoundEngine.PlaySound(in style, base.Player.Center);
						for (int i3 = 0; i3 <= 14; i3++)
						{
							Vector2 vel = (Vector2.One * 5f).RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(0.4f, 1.2f);
							Dust dust4 = Dust.NewDustPerfect(base.Player.Center, ModContent.DustType<LightDust>(), vel);
							dust4.scale = Main.rand.NextFloat(0.8f, 1.4f);
							dust4.noGravity = false;
							dust4.alpha = 180;
							dust4.color = (Main.rand.NextBool() ? Color.Turquoise : Color.Aquamarine);
							dust4.noLight = true;
							dust4.noLightEmittence = true;
						}
					}
				}
				sSpiritAmuletTimer = 0;
			}
			else if (!base.Player.dead)
			{
				sSpiritAmuletTimer++;
			}
		}
		if (ironBoots)
		{
			if (base.Player.controlDown && !base.Player.controlJump)
			{
				if ((base.Player.gravDir == 1f) ? (base.Player.velocity.Y > 0f) : (base.Player.velocity.Y < 0f))
				{
					fallingBootVelCheckTimer++;
				}
				if (base.Player.velocity.Y == 0f && fallingBootVelCheckTimer > 10)
				{
					float power = Utils.Remap(fallingBootVelCheckTimer, 10f, 40f, 0.1f, 1f);
					float scaledPower = (float)Math.Pow(power, 3.0);
					int damage2 = (int)base.Player.GetBestClassDamage().ApplyTo(1500f * scaledPower);
					Vector2 playerFeet = base.Player.Center + Vector2.UnitY * 26f * base.Player.gravDir;
					float blastSize = 120f * power;
					float minMultiplier = 0.1f;
					int hitsToMinMult = 6;
					int debuff = ModContent.BuffType<ArmorCrunch>();
					int debuffTime = (int)(300f * power);
					Projectile projectile5 = Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), playerFeet, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), damage2, -25f * power, base.Player.whoAmI, blastSize, minMultiplier, hitsToMinMult);
					projectile5.localAI[0] = debuff;
					projectile5.localAI[1] = debuffTime;
					projectile5.timeLeft = 5;
					int particleNumber = (int)Math.Max(15f * power, 2f);
					for (int i4 = -particleNumber; i4 <= particleNumber; i4++)
					{
						GeneralParticleHandler.SpawnParticle(new AltSparkParticle(playerFeet, (Vector2.UnitX * (float)(5 + Math.Abs(i4)) * (float)Math.Sign(i4)).RotatedByRandom(0.25) * Main.rand.NextFloat(0.5f, 1f) * power, affectedByGravity: false, Main.rand.Next(17, 31), Main.rand.NextFloat(0.2f, 0.8f), Main.rand.NextBool() ? Color.Lerp(Color.Gold, Color.Silver, 0.3f) : Color.Silver));
					}
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit3");
					style.Volume = 0.6f * power;
					style.Pitch = Main.rand.NextFloat(0.6f, 0.8f);
					SoundEngine.PlaySound(in style, playerFeet);
					base.Player.SetScreenshake(4f * scaledPower);
					fallingBootVelCheckTimer = 0;
				}
			}
			if ((base.Player.gravDir == 1f) ? (base.Player.velocity.Y <= 0f) : (base.Player.velocity.Y >= 0f))
			{
				fallingBootVelCheckTimer = 0;
			}
		}
		if (dOfTheDeep)
		{
			if (dOfTheDeepDefenseBuffTimer > 0)
			{
				int maxDefense = 10;
				int givenDefense = (int)Utils.Remap(dOfTheDeepDefenseBuffTimer, 0f, (float)dOfTheDeepDefenseBuffMax * 0.5f, 0f, maxDefense);
				base.Player.statDefense += givenDefense;
				dOfTheDeepDefenseBuffTimer--;
			}
			int spawnTime2 = 150;
			int energyCap2 = 3;
			Projectile projectile6 = null;
			if (base.Player.dashDelay == -1 && dOfTheDeepTimer > 0)
			{
				int energyCount2 = 0;
				for (int x5 = 0; x5 < Main.maxProjectiles; x5++)
				{
					projectile6 = Main.projectile[x5];
					if (projectile6.active && projectile6.type == ModContent.ProjectileType<DiamondOfTheDeepProjectile>() && projectile6.ai[2] == 0f && projectile6.owner == base.Player.whoAmI)
					{
						projectile6.ai[2] = 5f;
						energyCount2++;
					}
				}
				if (energyCount2 > 0)
				{
					dOfTheDeepTimer = (int)((float)(-spawnTime2) * 0.5f);
				}
			}
			int numOfEnergy2 = 0;
			for (int x6 = 0; x6 < Main.maxProjectiles; x6++)
			{
				projectile6 = Main.projectile[x6];
				if (projectile6.active && projectile6.type == ModContent.ProjectileType<DiamondOfTheDeepProjectile>() && projectile6.ai[2] == 0f && projectile6.owner == base.Player.whoAmI)
				{
					numOfEnergy2++;
				}
			}
			if (dOfTheDeepTimer >= spawnTime2)
			{
				if (numOfEnergy2 < energyCap2)
				{
					int energyDamage2 = (int)base.Player.GetBestClassDamage().ApplyTo(400f);
					int energyType = numOfEnergy2 % 3;
					Projectile.NewProjectileDirect(base.Player.GetSource_FromThis(), base.Player.Center, (Vector2.One * 4f).RotatedByRandom(6.2831854820251465), ModContent.ProjectileType<DiamondOfTheDeepProjectile>(), energyDamage2, 0f, base.Player.whoAmI, 0f, numOfEnergy2).localAI[2] = energyType;
					if (numOfEnergy2 + 1 == energyCap2 && base.Player.Calamity().dOfTheDeepVisual)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WaterSplash1");
						style.Volume = 0.35f;
						style.Pitch = 0.8f;
						style.MaxInstances = -1;
						SoundEngine.PlaySound(in style, base.Player.Center);
						for (int i5 = 0; i5 <= 14; i5++)
						{
							Vector2 vel2 = (Vector2.One * 5f).RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(0.4f, 1.2f);
							Dust dust5 = Dust.NewDustPerfect(base.Player.Center, ModContent.DustType<LightDust>(), vel2);
							dust5.scale = Main.rand.NextFloat(0.8f, 1.4f);
							dust5.noGravity = false;
							dust5.alpha = 180;
							dust5.color = (Main.rand.NextBool() ? Color.Turquoise : Color.Aquamarine);
							dust5.noLight = true;
							dust5.noLightEmittence = true;
						}
					}
				}
				dOfTheDeepTimer = 0;
			}
			else if (!base.Player.dead)
			{
				dOfTheDeepTimer++;
			}
		}
		if (hookPullVisuals > 0)
		{
			if (bloomStone && base.Player.Calamity().bloomStoneHookVisuals)
			{
				Vector2 spawnPos = base.Player.Center + Main.rand.NextVector2Circular(20f, 20f);
				float fade = (float)Math.Pow(Utils.GetLerpValue(0f, 30f, hookPullVisuals, clamped: true), 2.0);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Player.Center, -base.Player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(5f, 8f), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, Main.rand.Next(7, 11), 0.65f, Color.Lerp(Color.HotPink, Color.Gold, Utils.GetLerpValue(60f, 30f, hookPullVisuals, clamped: true)) * 0.75f * fade, new Vector2(1f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.6f));
				if (Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(spawnPos, -base.Player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(5f, 8f), "CalamityMod/Particles/MiniFlower", affectedByGravity: false, Main.rand.Next(25, 29), Main.rand.NextFloat(2.3f, 2.6f) * fade, Color.Lerp(Color.HotPink, Color.Plum, Main.rand.NextFloat(0f, 0.65f)), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(0f, (float)Math.PI * 2f)));
				}
				if (Main.rand.NextBool())
				{
					Dust dust6 = Dust.NewDustPerfect(base.Player.Center + Main.rand.NextVector2Circular(20f, 20f), ModContent.DustType<SquashDust>());
					dust6.noLightEmittence = true;
					dust6.noGravity = true;
					dust6.scale = Main.rand.NextFloat(0.9f, 1.4f);
					dust6.color = Color.Lerp(Color.Gold, Color.HotPink, Utils.GetLerpValue(60f, 30f, hookPullVisuals, clamped: true));
					dust6.velocity = -base.Player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(2f, 5f);
					dust6.fadeIn = -0.5f * fade;
				}
			}
			hookPullVisuals--;
			if (((Vector2)(ref base.Player.velocity)).Length() < 6f)
			{
				hookPullVisuals = 0;
			}
		}
		if (unstableGraniteCore)
		{
			zapActivity++;
			if (zapActivity <= 300 && zapActivity % 30 == 0)
			{
				float maxDistance = 300f;
				int target2 = -1;
				ActiveEntityIterator<NPC>.Enumerator enumerator7 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator7.MoveNext())
				{
					NPC npc = enumerator7.Current;
					float targetDist = Vector2.Distance(npc.Center, base.Player.Center);
					if (targetDist < maxDistance && npc.Calamity().arcZapCooldown == 0 && npc.CanBeChasedBy())
					{
						maxDistance = targetDist;
						target2 = npc.whoAmI;
					}
				}
				if (target2 > 0)
				{
					unstableSelectedTarget = Main.npc[target2];
					unstableSelectedTarget.Calamity().arcZapCooldown = 25;
					int damage3 = (int)base.Player.GetBestClassDamage().ApplyTo(15f);
					Projectile.NewProjectile(base.Player.GetSource_FromThis(), new Vector2(base.Player.Center.X, base.Player.Center.Y - 20f), new Vector2(0f, -2f), ModContent.ProjectileType<ArcZap>(), damage3, 0f, base.Player.whoAmI, target2, 5f);
					target2 = -1;
				}
			}
			else if (zapActivity > 600)
			{
				zapActivity = 0;
			}
		}
		if (nucleogenesis)
		{
			base.Player.maxMinions += 4;
		}
		else
		{
			if (shadowMinions)
			{
				base.Player.maxMinions++;
			}
			if (holyMinions)
			{
				base.Player.maxMinions += 2;
			}
			if (starTaintedGenerator)
			{
				base.Player.maxMinions += 2;
			}
			else
			{
				if (starbusterCore)
				{
					base.Player.maxMinions++;
				}
				if (voltaicJelly)
				{
					base.Player.maxMinions++;
				}
				if (nuclearFuelRod)
				{
					base.Player.maxMinions++;
				}
			}
		}
		if (whitewaterHeal > 0)
		{
			if (whitewaterHeal % 15 == 0)
			{
				base.Player.HealPlayer(1);
			}
			whitewaterHeal--;
			Vector2 vel3 = ((float)Math.PI * 2f * (float)whitewaterHeal * (float)base.Player.direction / 25f).ToRotationVector2() * 3f;
			for (int i6 = -1; i6 <= 1; i6 += 2)
			{
				Dust dust7 = Dust.NewDustPerfect(base.Player.Center + vel3 * 4f * (float)i6, ModContent.DustType<LightDust>());
				dust7.velocity = vel3 * (float)i6;
				dust7.scale = Main.rand.NextFloat(0.9f, 1f);
				dust7.noGravity = true;
				dust7.alpha = 150;
				dust7.color = Color.SkyBlue;
				dust7.noLightEmittence = true;
			}
		}
		IList<string> expiredCooldowns = new List<string>(16);
		Dictionary<string, CooldownInstance>.Enumerator cdIterator = cooldowns.GetEnumerator();
		while (cdIterator.MoveNext())
		{
			KeyValuePair<string, CooldownInstance> kv = cdIterator.Current;
			string id = kv.Key;
			CooldownInstance instance = kv.Value;
			CooldownHandler handler = instance.handler;
			if (handler.CanTickDown)
			{
				instance.timeLeft--;
			}
			handler.Tick();
			if (instance.timeLeft < 0)
			{
				handler.OnCompleted();
				if (handler.EndSound.HasValue && handler.ShouldPlayEndSound)
				{
					SoundEngine.PlaySound(handler.EndSound.GetValueOrDefault(), base.Player.Center);
				}
				expiredCooldowns.Add(id);
			}
		}
		cdIterator.Dispose();
		foreach (string cdID in expiredCooldowns)
		{
			cooldowns.Remove(cdID);
		}
		if (expiredCooldowns.Count > 0)
		{
			SyncCooldownRemoval(Main.dedServ, expiredCooldowns);
		}
		if (base.Player.stoned || base.Player.frozen || base.Player.webbed)
		{
			ImmobilityDebuffImmunityTimer = 300;
		}
		else if (ImmobilityDebuffImmunityTimer > 0)
		{
			ImmobilityDebuffImmunityTimer--;
			base.Player.buffImmune[156] = true;
			base.Player.buffImmune[47] = true;
			base.Player.buffImmune[149] = true;
		}
		if (arsenalCooldown > 0)
		{
			arsenalCooldown--;
		}
		if (killModeCooldown > 0)
		{
			killModeCooldown--;
		}
		if (ascendantInsigniaCooldown > 0 && ascendantInsigniaBuffTime <= 0)
		{
			ascendantInsigniaCooldown--;
		}
		if (transformerCooldown > 0)
		{
			transformerCooldown--;
		}
		if (DragonsBreathAudioCooldown > 0)
		{
			DragonsBreathAudioCooldown--;
		}
		if (DragonsBreathAudioCooldown2 > 0)
		{
			DragonsBreathAudioCooldown2--;
		}
		if (PhotoAudioCooldown > 0)
		{
			PhotoAudioCooldown--;
		}
		if (arpeggioCooldown > 0)
		{
			arpeggioCooldown--;
		}
		if (fullRageSoundCountdownTimer > 0)
		{
			fullRageSoundCountdownTimer--;
		}
		if (plagueTaintedSMGDroneCooldown > 0)
		{
			plagueTaintedSMGDroneCooldown--;
		}
		if (flareGunOverheat > 0)
		{
			flareGunOverheat--;
		}
		if (momentumCapacitorTime > 0)
		{
			momentumCapacitorTime--;
		}
		if (phantomicHeartRegen > 0 && phantomicHeartRegen < 1000)
		{
			phantomicHeartRegen--;
		}
		if (phantomicBulwarkCooldown > 0)
		{
			phantomicBulwarkCooldown--;
		}
		if (galileoCooldown > 0)
		{
			galileoCooldown--;
		}
		if (gladiatorTimer > 0)
		{
			gladiatorTimer--;
		}
		if (dragonRageCooldown > 0)
		{
			dragonRageCooldown--;
		}
		if (soundCooldown > 0)
		{
			soundCooldown--;
		}
		if ((float)raiderCritLifespan > 0f)
		{
			raiderCritLifespan--;
		}
		if (raiderSoundCooldown > 0)
		{
			raiderSoundCooldown--;
		}
		if (astralStarRainCooldown > 0)
		{
			astralStarRainCooldown--;
		}
		if (AbaddonCooldown > 0)
		{
			AbaddonCooldown--;
		}
		if (VoidCooldown > 0)
		{
			VoidCooldown--;
		}
		if (ursaSergeantCooldown > 0)
		{
			ursaSergeantCooldown--;
		}
		if (generalBandCooldown > 0)
		{
			generalBandCooldown--;
		}
		if (AlchFlaskCooldown > 0)
		{
			AlchFlaskCooldown--;
		}
		if (tarraRangedCooldown > 0)
		{
			tarraRangedCooldown--;
		}
		if (bloodflareMageCooldown > 0)
		{
			bloodflareMageCooldown--;
		}
		if (silvaMageCooldown > 0)
		{
			silvaMageCooldown--;
		}
		if (scuttlerCooldown > 0)
		{
			scuttlerCooldown--;
		}
		if (rogueCrownCooldown > 0)
		{
			rogueCrownCooldown--;
		}
		if (spectralVeilImmunity > 0)
		{
			spectralVeilImmunity--;
		}
		if (jetPackDash > 0)
		{
			jetPackDash--;
		}
		if (theBeeCooldown > 0)
		{
			theBeeCooldown--;
		}
		if (bloomStoneTotalHeal > 0)
		{
			float healRateDiv = ((base.Player.statLife >= base.Player.statLifeMax) ? 5f : ((bloomStoneBuffedHealRateTimer > 0) ? Utils.Remap(bloomStoneBuffedHealRateTimer, 90f, 0f, 0.5f, 1f) : 1f));
			bloomStoneHealRate = 1f / healRateDiv;
			if (!bloomStone)
			{
				bloomStoneTotalHeal = 0;
			}
			float secondsOfHealing = 16f;
			if (bloomStoneHealTimer >= 60f)
			{
				int healAmount = Math.Max(Math.Min(bloomStoneHealPool, (int)((float)bloomStoneTotalHeal / secondsOfHealing)), 1);
				base.Player.HealPlayer(healAmount);
				bloomStoneHealPool -= healAmount;
				if (chaliceOfTheBloodGod && chaliceBleedoutBuffer > 0.0)
				{
					float amountOfBleedToClear = 0.5f * (float)healAmount;
					chaliceBleedoutBuffer -= amountOfBleedToClear;
					if (!Main.dedServ)
					{
						string text = $"(+{amountOfBleedToClear})";
						CombatText.NewText(new Rectangle((int)base.Player.position.X + 4, (int)base.Player.position.Y - 3, base.Player.width - 4, base.Player.height - 4), ChaliceOfTheBloodGod.BleedoutBufferDamageTextColor, Language.GetTextValue(text), dramatic: false, dot: true);
					}
				}
				if (bloomStoneHealPool == 0)
				{
					bloomStoneTotalHeal = 0;
				}
				bloomStoneHealTimer = 0f;
			}
			bloomStoneHealTimer += bloomStoneHealRate;
		}
		if (bloomStoneBuffedHealRateTimer > 0)
		{
			bloomStoneBuffedHealRateTimer--;
		}
		if (summonProjCooldown > 0f)
		{
			summonProjCooldown--;
		}
		if (ataxiaDmg > 0f)
		{
			ataxiaDmg -= 1.5f;
		}
		if (ataxiaDmg < 0f)
		{
			ataxiaDmg = 0f;
		}
		if (xerocDmg > 0f)
		{
			xerocDmg -= 2f;
		}
		if (xerocDmg < 0f)
		{
			xerocDmg = 0f;
		}
		if (hideOfDeusMeleeBoostTimer > 0)
		{
			hideOfDeusMeleeBoostTimer--;
		}
		if (hurtSoundTimer > 0)
		{
			hurtSoundTimer--;
		}
		if (wingProjectileCooldown > 0)
		{
			wingProjectileCooldown--;
		}
		if (hallowedRuneCooldown > 0)
		{
			hallowedRuneCooldown--;
		}
		if (sulphurBubbleCooldown > 0)
		{
			sulphurBubbleCooldown--;
		}
		if (forbiddenCooldown > 0)
		{
			forbiddenCooldown--;
		}
		if (tornadoCooldown > 0)
		{
			tornadoCooldown--;
		}
		if (ladHearts > 0)
		{
			ladHearts--;
		}
		if (prismaticLasers > 0)
		{
			prismaticLasers--;
		}
		if (dogTextCooldown > 0)
		{
			dogTextCooldown--;
		}
		if (titanCooldown > 0)
		{
			titanCooldown--;
		}
		if (hideOfDeusTimer > 0)
		{
			hideOfDeusTimer--;
		}
		if (murasamaHitCooldown > 0)
		{
			murasamaHitCooldown--;
		}
		if (burningSeaBurnOut > 0)
		{
			burningSeaBurnOut--;
			if (Main.rand.NextBool())
			{
				Vector2 position = base.Player.position + new Vector2(Main.rand.NextFloat(base.Player.width), 0f);
				Vector2 dustVelocity = -Vector2.UnitY * Main.rand.NextFloat(3f, 6f);
				Dust.NewDustPerfect(position, 235, dustVelocity).noGravity = true;
			}
		}
		if (persecutedEnchantSummonTimer < 1800)
		{
			persecutedEnchantSummonTimer++;
		}
		else
		{
			persecutedEnchantSummonTimer = 0;
			if (Main.myPlayer == base.Player.whoAmI && persecutedEnchant && NPC.CountNPCS(ModContent.NPCType<DemonPortal>()) < 2)
			{
				int tries = 0;
				Vector2 spawnPositionOffset = Vector2.One * 24f;
				Vector2 spawnPosition;
				do
				{
					spawnPosition = base.Player.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(270f, 420f);
					tries++;
				}
				while (Collision.SolidCollision(spawnPosition - spawnPositionOffset, 48, 24) && tries < 100);
				CalamityNetcode.NewNPC_ClientSide(spawnPosition, ModContent.NPCType<DemonPortal>(), base.Player);
			}
		}
		if (base.Player.miscCounter % HydrothermicHeadRanged.FlareCooldown == 0)
		{
			canFireAtaxiaRangedProjectile = true;
		}
		if (base.Player.miscCounter % HydrothermicHeadRogue.VolleyCooldown == 0)
		{
			canFireAtaxiaRogueProjectile = true;
		}
		if (base.Player.miscCounter % BloodflareHeadMagic.GhostBoltCooldown == 0)
		{
			canFireBloodflareMageProjectile = true;
		}
		if (base.Player.miscCounter % BloodflareHeadRanged.BloodBombCooldown == 0)
		{
			canFireBloodflareRangedProjectile = true;
		}
		if (base.Player.miscCounter % GodSlayerHeadRanged.ShrapnelRoundCooldown == 0)
		{
			canFireGodSlayerRangedProjectile = true;
		}
		if (auralisAuroraCounter > 300)
		{
			for (int i7 = -1; i7 <= 1; i7 += 2)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSquareParticle(base.Player.Center, Vector2.Zero, affectedByGravity: false, 2, 6.25f, new Color(92, 89, 251), GlowCenter: true, MathHelper.ToRadians((float)auralisAuroraCounter * 2f * (float)i7)));
			}
			auralisAuroraCounter++;
		}
		if (auralisAuroraCounter > 1500)
		{
			auralisAuroraCounter = 0;
			auralisAuroraCooldown = CalamityUtils.SecondsToFrames(30f);
		}
		if (auralisAuroraCooldown > 0)
		{
			auralisAuroraCooldown--;
		}
		if (blazingCore)
		{
			if (blazingCoreSuccessfulParry > 0)
			{
				BlazingCore.HandleStars(base.Player);
			}
			else if (blazingCoreParry > 0)
			{
				BlazingCore.HandleParryCountdown(base.Player);
			}
		}
		else if (blazingCoreParry > 0)
		{
			blazingCoreParry--;
		}
		else if (flameLickedShellParry > 0)
		{
			if (flameLickedShell)
			{
				FlameLickedShell.HandleParryCountdown(base.Player);
			}
			else
			{
				flameLickedShellParry--;
			}
		}
		if (!flameLickedShell && flameLickedShellParry > 0)
		{
			flameLickedShellParry--;
		}
		if (silverMedkitTimer > 0)
		{
			silverMedkitTimer--;
			if (silverMedkitTimer == 0)
			{
				base.Player.HealPlayer(10);
				SilverArmorSetChange.OnHealEffects(base.Player);
			}
		}
		if (MythrilFlareSpawnCountdown > 0)
		{
			MythrilFlareSpawnCountdown--;
		}
		if (AdamantiteSetDecayDelay > 0)
		{
			AdamantiteSetDecayDelay--;
		}
		else if (AdamantiteSet)
		{
			adamantiteSetDefenseBoostInterpolant -= 0.004761905f;
			adamantiteSetDefenseBoostInterpolant = MathHelper.Clamp(adamantiteSetDefenseBoostInterpolant, 0f, 1f);
		}
		else
		{
			adamantiteSetDefenseBoostInterpolant = 0f;
		}
		if (ChlorophyteHealDelay > 0)
		{
			ChlorophyteHealDelay--;
		}
		if (monolithAccursedShader > 0)
		{
			monolithAccursedShader--;
		}
		if (monolithBossRushShader > 0)
		{
			monolithBossRushShader--;
		}
		if (monolithExoShader > 0)
		{
			monolithExoShader--;
		}
		if (monolithLeviathanShader > 0)
		{
			monolithLeviathanShader--;
		}
		if (monolithCryogenShader > 0)
		{
			monolithCryogenShader--;
		}
		if (monolithDevourerBShader > 0)
		{
			monolithDevourerBShader--;
		}
		if (monolithDevourerPShader > 0)
		{
			monolithDevourerPShader--;
		}
		if (monolithYharonShader > 0)
		{
			monolithYharonShader--;
		}
		if (monolithPlagueShader > 0)
		{
			monolithPlagueShader--;
		}
		if (monolithAstralShader > 0)
		{
			monolithAstralShader--;
		}
		if (BrimstoneLavaFountainCounter > 0)
		{
			BrimstoneLavaFountainCounter--;
		}
		if (miningSetCooldown > 0)
		{
			miningSetCooldown--;
		}
		if (MiniSwarmerCooldown > 0)
		{
			MiniSwarmerCooldown--;
		}
		if (LastUsedDashID == GodslayerArmorDash.ID && base.Player.dashDelay < 0)
		{
			for (int d = 0; d < base.Player.buffImmune.Length; d++)
			{
				ref bool reference = ref base.Player.buffImmune[d];
				reference |= CalamityBuffSets.IsDebuff[d];
			}
		}
		if (copyrightInfringementShield)
		{
			if (base.Player.dashType == 2 && DashID == string.Empty)
			{
				if (base.Player.eocHit == -1 && base.Player.dashDelay == -1)
				{
					if (!shieldOfTheHighRulerDashVelocityBoosted)
					{
						shieldOfTheHighRulerDashVelocityBoosted = true;
						if (Math.Abs(base.Player.velocity.X) <= 18.9f)
						{
							base.Player.velocity.X *= 1.3034482f;
						}
					}
				}
				else
				{
					shieldOfTheHighRulerDashVelocityBoosted = false;
				}
				if (base.Player.eocHit != -1 && base.Player.dashDelay > 15)
				{
					base.Player.dashDelay = 15;
				}
			}
		}
		else
		{
			shieldOfTheHighRulerDashVelocityBoosted = false;
		}
		int auricDyeCount = base.Player.dye.Count((Item dyeItem) => dyeItem.type == ModContent.ItemType<AuricDye>());
		if (auricDyeCount > 0)
		{
			int sparkCreationChance = (int)MathHelper.Lerp(15f, 50f, Utils.GetLerpValue(4f, 1f, auricDyeCount, clamped: true));
			if (Main.rand.NextBool(sparkCreationChance))
			{
				Dust dust8 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 267);
				dust8.color = Color.Lerp(Color.Cyan, Color.SeaGreen, Main.rand.NextFloat(0.5f));
				dust8.velocity = -Vector2.UnitY.RotatedByRandom(2.0891592502593994) * Main.rand.NextFloat(2f, 5.4f);
				dust8.noGravity = true;
			}
		}
		if (necroReviveCounter >= 0)
		{
			necroReviveCounter++;
			float ratioUntilDead = (float)necroReviveCounter / 600f;
			int upperHealthLimit = (int)MathHelper.Lerp((float)base.Player.statLifeMax2, 1f, ratioUntilDead);
			if (base.Player.statLife > upperHealthLimit)
			{
				base.Player.statLife = upperHealthLimit;
			}
			if (necroReviveCounter >= 600)
			{
				base.Player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.NecroRevive").ToNetworkText(base.Player.name)), 1000.0, -1);
				necroReviveCounter = -1;
			}
			else if (necroReviveCounter % 60 == 59)
			{
				SoundEngine.PlaySound(in NecroArmorSetChange.TimerSound, base.Player.Center);
			}
		}
		if (silvaCountdown > 0 && hasSilvaEffect && silvaSet)
		{
			int[] buffType = base.Player.buffType;
			foreach (int debuff2 in buffType)
			{
				if (CalamityBuffSets.IsDebuff[debuff2])
				{
					base.Player.buffImmune[debuff2] = true;
				}
			}
			base.Player.thorns = 0f;
			silvaCountdown--;
			if (silvaCountdown <= 0)
			{
				SoundEngine.PlaySound(in SilvaArmor.DispelSound, base.Player.Center);
				base.Player.AddCooldown(SilvaRevive.ID, SilvaArmor.ReviveCooldown);
			}
			for (int j2 = 0; j2 < 2; j2++)
			{
				Dust green = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 2f);
				green.position.X += Main.rand.Next(-20, 21);
				green.position.Y += Main.rand.Next(-20, 21);
				green.velocity *= 0.9f;
				green.noGravity = true;
				green.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				green.shader = GameShaders.Armor.GetSecondaryShader(base.Player.ArmorSetDye(), base.Player);
				if (Main.rand.NextBool())
				{
					green.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				}
			}
		}
		if (!base.Player.HasCooldown(SilvaRevive.ID) && hasSilvaEffect && silvaCountdown <= 0 && !areThereAnyDamnBosses && !areThereAnyDamnEvents)
		{
			silvaCountdown = SilvaArmor.ReviveDuration;
			hasSilvaEffect = false;
		}
		if (tarragonCloak)
		{
			tarraDefenseTime--;
			if (tarraDefenseTime <= 0)
			{
				tarraDefenseTime = 600;
				if (base.Player.whoAmI == Main.myPlayer)
				{
					base.Player.AddCooldown(global::CalamityMod.Cooldowns.TarragonCloak.ID, TarragonHeadMelee.CloakCooldown);
				}
			}
			for (int j3 = 0; j3 < 2; j3++)
			{
				Dust dust9 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 2f);
				dust9.position.X += Main.rand.Next(-20, 21);
				dust9.position.Y += Main.rand.Next(-20, 21);
				dust9.velocity *= 0.9f;
				dust9.noGravity = true;
				dust9.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				dust9.shader = GameShaders.Armor.GetSecondaryShader(base.Player.ArmorSetDye(), base.Player);
				if (Main.rand.NextBool())
				{
					dust9.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				}
			}
		}
		if (tarraThrowing)
		{
			if (tarragonImmunity && !disableAllDodges)
			{
				base.Player.GiveUniversalIFrames(2, blink: true);
			}
			if (tarraThrowingCrits >= TarragonHeadRogue.CritsToActivateImmunity)
			{
				tarraThrowingCrits = 0;
				if (base.Player.whoAmI == Main.myPlayer && !disableAllDodges)
				{
					base.Player.AddBuff(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.TarragonImmunity>(), TarragonHeadRogue.ImmunityDuration, quiet: false);
				}
			}
			for (int l2 = 0; l2 < Player.MaxBuffs; l2++)
			{
				int buffID2 = base.Player.buffType[l2];
				if (base.Player.buffTime[l2] <= 2 && buffID2 == ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.TarragonImmunity>() && base.Player.whoAmI == Main.myPlayer)
				{
					base.Player.AddCooldown(global::CalamityMod.Cooldowns.TarragonImmunity.ID, TarragonHeadRogue.ImmunityCooldown);
				}
				if (CalamityBuffSets.IsDebuff[buffID2])
				{
					base.Player.GetDamage<RogueDamageClass>() += TarragonHeadRogue.RogueDamageBoostWhileDebuffed;
				}
			}
		}
		if (bloodflareSet && bloodflareHeartTimer > 0)
		{
			bloodflareHeartTimer--;
		}
		if (bloodflareMelee)
		{
			if (bloodflareMeleeHits >= 15)
			{
				bloodflareMeleeHits = 0;
				if (base.Player.whoAmI == Main.myPlayer)
				{
					base.Player.AddBuff(ModContent.BuffType<BloodflareBloodFrenzy>(), BloodflareHeadMelee.FrenzyDuration, quiet: false);
				}
			}
			if (bloodflareFrenzy)
			{
				for (int l3 = 0; l3 < Player.MaxBuffs; l3++)
				{
					int hasBuff = base.Player.buffType[l3];
					if (base.Player.buffTime[l3] <= 2 && hasBuff == ModContent.BuffType<BloodflareBloodFrenzy>() && base.Player.whoAmI == Main.myPlayer)
					{
						base.Player.AddCooldown(BloodflareFrenzy.ID, BloodflareHeadMelee.FrenzyCooldown);
					}
				}
				base.Player.GetCritChance<MeleeDamageClass>() += BloodflareHeadMelee.FrenzyMeleeCritBoost;
				base.Player.GetDamage<MeleeDamageClass>() += BloodflareHeadMelee.FrenzyMeleeDamageBoost;
				for (int j4 = 0; j4 < 2; j4++)
				{
					Dust blood = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 5, 0f, 0f, 100, default(Color), 2f);
					blood.position.X += Main.rand.Next(-20, 21);
					blood.position.Y += Main.rand.Next(-20, 21);
					blood.velocity *= 0.9f;
					blood.noGravity = true;
					blood.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					blood.shader = GameShaders.Armor.GetSecondaryShader(base.Player.ArmorSetDye(), base.Player);
					if (Main.rand.NextBool())
					{
						blood.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
			}
		}
		if (brimflameFrenzy)
		{
			base.Player.GetDamage<MagicDamageClass>() += BrimflameCowl.FrenzyMagicDamageBoost;
			for (int l4 = 0; l4 < Player.MaxBuffs; l4++)
			{
				if (base.Player.buffTime[l4] <= 2 && base.Player.buffType[l4] == ModContent.BuffType<BrimflameFrenzyBuff>() && base.Player.whoAmI == Main.myPlayer)
				{
					base.Player.AddCooldown(BrimflameFrenzy.ID, BrimflameCowl.FrenzyCooldown);
				}
			}
		}
		if (raiderTalisman && !vampiricTalisman && !StealthStrikeAvailable() && (float)raiderCritLifespan > 0f)
		{
			base.Player.GetCritChance<ThrowingDamageClass>() += 15f;
		}
		if (vampiricTalisman && !StealthStrikeAvailable() && (float)raiderCritLifespan > 0f)
		{
			base.Player.GetCritChance<ThrowingDamageClass>() += 15f;
		}
		if (avertorBonus)
		{
			base.Player.GetDamage<GenericDamageClass>() += 0.1f;
		}
		if (base.Player.ichor)
		{
			base.Player.statDefense += 5;
		}
		if (fairyBoots && base.Player.isNearFairy())
		{
			base.Player.lifeRegen += 4;
			base.Player.statDefense += 6;
			base.Player.moveSpeed += 0.1f;
		}
		if (absorber)
		{
			base.Player.moveSpeed += TheAbsorber.MoveSpeedBoost;
			base.Player.jumpSpeedBoost += TheAbsorber.JumpSpeedBoost;
		}
		if (affliction || afflicted)
		{
			base.Player.endurance += Affliction.DamageReductionBoost;
			base.Player.statDefense += Affliction.DefenseBoost;
			base.Player.GetDamage<GenericDamageClass>() += Affliction.DamageBoost;
		}
		float[] light = new float[3];
		if (cFreeze)
		{
			light[0] += 0.3f;
			light[1] += (float)Main.DiscoG / 400f;
			light[2] += 0.5f;
		}
		if (aquaticHeartIce)
		{
			base.Player.endurance += AquaticHeart.IceShieldDamageReductionBoost;
			light[0] += 0.35f;
			light[1]++;
			light[2] += 1.25f;
		}
		if (aquaticHeart)
		{
			light[0] += 0.1f;
			light[1]++;
			light[2] += 1.5f;
		}
		if (tarraSummon)
		{
			light[0] += 0f;
			light[1] += 3f;
			light[2] += 0f;
		}
		if (forbiddenCirclet)
		{
			light[0] += 0.8f;
			light[1] += 0.7f;
			light[2] += 0.2f;
		}
		Lighting.AddLight((int)(base.Player.Center.X / 16f), (int)(base.Player.Center.Y / 16f), light[0], light[1], light[2]);
		if (permafrostsConcoction)
		{
			base.Player.manaCost -= 0.15f;
			base.Player.statManaMax2 += 50;
		}
		if (encased)
		{
			base.Player.statDefense += PermafrostsConcoction.EncasedDefenseBoost;
			base.Player.endurance += PermafrostsConcoction.EncasedDamageReductionBoost;
			base.Player.frozen = true;
			base.Player.velocity.X = 0f;
			base.Player.velocity.Y = -0.4f;
			Dust dust10 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 88);
			dust10.noGravity = true;
			dust10.velocity *= 2f;
			base.Player.buffImmune[47] = true;
			base.Player.buffImmune[46] = true;
		}
		if (cFreeze)
		{
			int buffType2 = ModContent.BuffType<GlacialState>();
			float freezeDist = 200f;
			if (base.Player.whoAmI == Main.myPlayer && Main.rand.NextBool(5))
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator9 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator9.MoveNext())
				{
					NPC npc2 = enumerator9.Current;
					if (!npc2.friendly && npc2.damage > 0 && !npc2.dontTakeDamage && !npc2.buffImmune[buffType2] && Vector2.Distance(base.Player.Center, npc2.Center) <= freezeDist && npc2.FindBuffIndex(buffType2) == -1)
					{
						npc2.AddBuff(buffType2, 60);
					}
				}
			}
		}
		if (base.Player.vortexStealthActive && base.Player.HeldItem.type != 3106)
		{
			base.Player.GetDamage<RangedDamageClass>() -= (1f - base.Player.stealth) * 0.4f;
			base.Player.GetCritChance<RangedDamageClass>() -= (int)((1f - base.Player.stealth) * 5f);
		}
		if (hasteLevel > 0)
		{
			if (hasteLevel > 3)
			{
				hasteLevel = 3;
			}
			if (++hasteCounter == 300)
			{
				hasteLevel--;
				if (hasteLevel <= 0 && base.Player.FindBuffIndex(ModContent.BuffType<Haste>()) > -1)
				{
					base.Player.ClearBuff(ModContent.BuffType<Haste>());
				}
				hasteCounter = 0;
			}
		}
		if (ceaselessHunger)
		{
			ActiveEntityIterator<Item>.Enumerator enumerator10 = Main.ActiveItems.GetEnumerator();
			while (enumerator10.MoveNext())
			{
				Item item = enumerator10.Current;
				if (item.noGrabDelay != 0 || item.playerIndexTheItemIsReservedFor != base.Player.whoAmI)
				{
					continue;
				}
				item.beingGrabbed = true;
				if (base.Player.Center.X > item.Center.X)
				{
					if (item.velocity.X < 90f + base.Player.velocity.X)
					{
						item.velocity.X += 9f;
					}
					if (item.velocity.X < 0f)
					{
						item.velocity.X += 6.75f;
					}
				}
				else
				{
					if (item.velocity.X > -90f + base.Player.velocity.X)
					{
						item.velocity.X -= 9f;
					}
					if (item.velocity.X > 0f)
					{
						item.velocity.X -= 6.75f;
					}
				}
				if (base.Player.Center.Y > item.Center.Y)
				{
					if (item.velocity.Y < 90f)
					{
						item.velocity.Y += 9f;
					}
					if (item.velocity.Y < 0f)
					{
						item.velocity.Y += 6.75f;
					}
				}
				else
				{
					if (item.velocity.Y > -90f)
					{
						item.velocity.Y -= 9f;
					}
					if (item.velocity.Y > 0f)
					{
						item.velocity.Y -= 6.75f;
					}
				}
			}
		}
		if (jetPackDash > 0 && base.Player.whoAmI == Main.myPlayer)
		{
			int velocityMult = (int)((float)(blunderBooster ? 35 : 25) * Utils.GetLerpValue(-4f, 5f, jetPackDash, clamped: true));
			base.Player.velocity = new Vector2((float)jetPackDirection, -1f) * (float)velocityMult;
			if (blunderBooster)
			{
				int lightningCount = 4;
				IEntitySource source2 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<BlunderBooster>()));
				for (int i8 = 0; i8 < lightningCount; i8++)
				{
					Vector2 lightningVel = base.Player.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(7f, 10f);
					int damage4 = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(35f);
					int projectile7 = Projectile.NewProjectile(source2, base.Player.Center, lightningVel, ModContent.ProjectileType<BlunderBoosterLightning>(), damage4, 0f, base.Player.whoAmI, Main.rand.Next(2));
					if (projectile7.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[projectile7].DamageType = DamageClass.Generic;
					}
				}
				for (int i9 = 0; i9 < 3; i9++)
				{
					Dust dust11 = Dust.NewDustDirect(base.Player.Center, 1, 1, 60, base.Player.velocity.X * -0.1f, base.Player.velocity.Y * -0.1f, 100, default(Color), 3.5f);
					dust11.noGravity = true;
					dust11.velocity *= 1.2f;
					dust11.velocity.Y -= 0.15f;
				}
			}
			else if (plaguedFuelPack)
			{
				int numClouds = 3;
				IEntitySource source3 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<PlaguedFuelPack>()));
				for (int i10 = 0; i10 < numClouds; i10++)
				{
					Vector2 cloudVelocity = base.Player.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(5f, 7f);
					int damage5 = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(30f);
					int projectile8 = Projectile.NewProjectile(source3, base.Player.Center, cloudVelocity, ModContent.ProjectileType<PlaguedFuelPackCloud>(), damage5, 0f, base.Player.whoAmI);
					if (projectile8.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[projectile8].DamageType = DamageClass.Generic;
					}
				}
				for (int i11 = 0; i11 < 3; i11++)
				{
					Dust dust12 = Dust.NewDustDirect(base.Player.Center, 1, 1, 89, base.Player.velocity.X * -0.1f, base.Player.velocity.Y * -0.1f, 100, default(Color), 3.5f);
					dust12.noGravity = true;
					dust12.velocity *= 1.2f;
					dust12.velocity.Y -= 0.15f;
				}
			}
		}
		if (!ascendantInsignia && ascendantInsigniaBuffTime > 0)
		{
			ascendantInsigniaBuffTime = 0;
			ascendantInsigniaCooldown = AscendantInsignia.AbilityCooldown;
			base.Player.AddCooldown(AscendEffect.ID, AscendantInsignia.AbilityCooldown);
		}
		if (!brimflameSet && brimflameFrenzy)
		{
			brimflameFrenzy = false;
			base.Player.ClearBuff(ModContent.BuffType<BrimflameFrenzyBuff>());
			base.Player.AddCooldown(BrimflameFrenzy.ID, BrimflameCowl.FrenzyCooldown);
		}
		if (!bloodflareMelee && bloodflareFrenzy)
		{
			bloodflareFrenzy = false;
			base.Player.ClearBuff(ModContent.BuffType<BloodflareBloodFrenzy>());
			base.Player.AddCooldown(BloodflareFrenzy.ID, BloodflareHeadMelee.FrenzyCooldown);
		}
		if (!tarraMelee && tarragonCloak)
		{
			tarragonCloak = false;
			base.Player.ClearBuff(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.TarragonCloak>());
			base.Player.AddCooldown(global::CalamityMod.Cooldowns.TarragonCloak.ID, TarragonHeadMelee.CloakCooldown);
		}
		if (!tarraThrowing && tarragonImmunity)
		{
			tarragonImmunity = false;
			base.Player.ClearBuff(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.TarragonImmunity>());
			base.Player.AddCooldown(global::CalamityMod.Cooldowns.TarragonImmunity.ID, TarragonHeadRogue.ImmunityCooldown);
		}
		bool hasOmegaBlueCooldown = cooldowns.TryGetValue(OmegaBlue.ID, out var omegaBlueCD);
		if ((!omegaBlueSet & hasOmegaBlueCooldown) && omegaBlueCD.timeLeft > OmegaBlueHelmet.MadnessCooldown)
		{
			base.Player.ClearBuff(ModContent.BuffType<AbyssalMadness>());
			omegaBlueCD.timeLeft = OmegaBlueHelmet.MadnessCooldown;
		}
		if (cooldowns.TryGetValue(KillMode.ID, out var killModeCD) && killModeCD.timeLeft > KillMode.cooldownMax && base.Player.HeldItem.type != ModContent.ItemType<ForbiddenOathblade>() && base.Player.HeldItem.type != ModContent.ItemType<ExaltedOathblade>() && base.Player.HeldItem.type != ModContent.ItemType<DevilsDevastation>())
		{
			killModeCD.timeLeft = KillMode.cooldownMax - 1;
			base.Player.Calamity().killModeCooldown = KillMode.cooldownMax - 1;
			base.Player.Calamity().demonSwordKillMode = false;
		}
		bool hasPlagueBlackoutCD = cooldowns.TryGetValue(PlagueBlackout.ID, out var plagueBlackoutCD);
		if ((!plagueReaper & hasPlagueBlackoutCD) && plagueBlackoutCD.timeLeft > PlagueReaperMask.BlackoutCooldown)
		{
			plagueBlackoutCD.timeLeft = PlagueReaperMask.BlackoutCooldown;
		}
		if (!prismaticSet && prismaticLasers > PrismaticHelmet.LaserCooldown)
		{
			prismaticLasers = PrismaticHelmet.LaserCooldown;
			base.Player.AddCooldown(PrismaticLaser.ID, PrismaticHelmet.LaserCooldown);
		}
		if (!angelicAlliance && divineBless)
		{
			divineBless = false;
			base.Player.ClearBuff(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.DivineBless>());
			base.Player.AddCooldown(global::CalamityMod.Cooldowns.DivineBless.ID, AngelicAlliance.DivineBlessCooldown);
		}
		if (disableAllDodges && base.Player.shadowDodgeTimer < 2)
		{
			base.Player.shadowDodgeTimer = 2;
		}
	}

	private void AbyssEffects()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		base.Player.SetAbyssLightLevels();
		if (ZoneAbyss)
		{
			if (Main.myPlayer != base.Player.whoAmI)
			{
				return;
			}
			Point point = base.Player.Center.ToTileCoordinates();
			double abyssSurface = (Main.remixWorld ? ((double)SulphurousSea.YStart) : (Main.rockLayer - (double)Main.maxTilesY * 0.05));
			double abyssLevel1 = (Main.remixWorld ? ((double)SulphurousSea.YStart - (double)Main.maxTilesY * 0.05) : (Main.rockLayer + (double)Main.maxTilesY * 0.03));
			double totalAbyssDepth = (Main.remixWorld ? ((double)SulphurousSea.YStart) : ((double)Main.maxTilesY - 250.0 - abyssSurface));
			double totalAbyssDepthFromLayer1 = (Main.remixWorld ? ((double)SulphurousSea.YStart - (double)Main.maxTilesY * 0.05) : ((double)Main.maxTilesY - 250.0 - abyssLevel1));
			double playerAbyssDepth = (Main.remixWorld ? (totalAbyssDepth - (double)point.Y) : ((double)point.Y - abyssSurface));
			double num = (Main.remixWorld ? (abyssLevel1 - (double)point.Y) : ((double)point.Y - abyssLevel1));
			double depthRatio = playerAbyssDepth / totalAbyssDepth;
			_ = num / totalAbyssDepthFromLayer1;
			darknessIntensity = abyssDarkness + (float)depthRatio * 3f;
			if (!base.Player.headcovered)
			{
				float screenObstructionAmt = MathHelper.Clamp(caveDarkness, 0f, 0.95f);
				float targetValue = MathHelper.Clamp(screenObstructionAmt * 0.7f, 0.1f, 0.3f);
				ScreenObstruction.screenObstruction = MathHelper.Lerp(ScreenObstruction.screenObstruction, screenObstructionAmt, targetValue);
			}
			double breathLoss = ((!Main.remixWorld) ? (((double)point.Y > abyssLevel1) ? 1.0 : 0.0) : (((double)point.Y < abyssLevel1) ? 1.0 : 0.0));
			int defenseLoss = (int)(120.0 * depthRatio);
			if (anechoicPlating && fathomSwarmerBreastplate)
			{
				defenseLoss = (int)((float)defenseLoss * 0.2f);
			}
			else if (anechoicPlating)
			{
				defenseLoss /= 3;
			}
			else if (fathomSwarmerBreastplate)
			{
				defenseLoss = (int)((float)defenseLoss * 0.6f);
			}
			base.Player.statDefense -= defenseLoss;
			abyssDefenseLossStat = defenseLoss;
			double tick = 10.0 * (1.0 - depthRatio);
			if (tick < 1.0)
			{
				tick = 1.0;
			}
			double tickMult = 1.0 + (base.Player.gills ? 2.0 : 0.0) + (oceanCrest ? 2.0 : 0.0) + (base.Player.ignoreWater ? 3.0 : 0.0) + (base.Player.accDivingHelm ? 5.0 : 0.0) + (base.Player.arcticDivingGear ? 5.0 : 0.0) + (aquaticEmblem ? 5.0 : 0.0) + (base.Player.accMerman ? 8.0 : 0.0) + (victideSet ? 2.0 : 0.0) + ((aquaticHeart && NPC.downedBoss3) ? 8.0 : 0.0) + (abyssalDivingSuit ? 8.0 : 0.0) + (double)externalBreathTickBoost;
			if (tickMult > 50.0)
			{
				tickMult = 50.0;
			}
			tick *= tickMult;
			abyssBreathLossRateStat = (float)tick;
			float resistanceSlowdownFactor = 1f;
			if (hadopelagicPressure)
			{
				resistanceSlowdownFactor -= (abyssalDivingSuit ? 0.2f : 0.5f);
			}
			abyssBreathCD++;
			if (abyssBreathCD >= (int)(tick * (double)resistanceSlowdownFactor))
			{
				abyssBreathCD = 0;
				if (base.Player.breath > 0)
				{
					base.Player.breath -= (int)((crushDepth && !depthCharm) ? (breathLoss + 1.0) : breathLoss);
				}
			}
			if (base.Player.breath > 0 && (base.Player.gills || base.Player.merman || base.Player.accMerman))
			{
				base.Player.breath -= 3;
			}
			int lifeLossAtZeroBreath = (int)(12.0 * depthRatio);
			int lifeLossAtZeroBreathResist = (depthCharm ? 4 : 0) + (abyssalDivingSuit ? 5 : 0);
			lifeLossAtZeroBreath -= lifeLossAtZeroBreathResist;
			if (lifeLossAtZeroBreath < 0)
			{
				lifeLossAtZeroBreath = 0;
			}
			abyssLifeLostAtZeroBreathStat = lifeLossAtZeroBreath;
			if (base.Player.breath <= 0)
			{
				base.Player.statLife -= lifeLossAtZeroBreath;
				if (base.Player.statLife <= 0)
				{
					abyssDeath = true;
					KillPlayer();
				}
			}
		}
		else
		{
			abyssBreathCD = 0;
			abyssDeath = false;
			if (Main.zenithWorld && CalamityGlobalNPC.signus != -1 && Main.npc[CalamityGlobalNPC.signus].active && Vector2.Distance(Main.LocalPlayer.Center, Main.npc[CalamityGlobalNPC.signus].Center) <= 5200f)
			{
				float darkRatio = MathHelper.Clamp(caveDarkness, 0f, 1f);
				float num2 = 1f - (float)(Main.npc[CalamityGlobalNPC.signus].life / Main.npc[CalamityGlobalNPC.signus].lifeMax);
				float multiplier = 1f;
				darkRatio = MathHelper.Clamp(num2 * multiplier, 0f, 1f);
				ScreenObstruction.screenObstruction = MathHelper.Lerp(ScreenObstruction.screenObstruction, -0.8f * (0f - darkRatio), 0.3f);
			}
		}
	}

	public static void EnchantHeldItemEffects(Player player, CalamityPlayer modPlayer, Item heldItem)
	{
		if (heldItem.IsAir)
		{
			return;
		}
		Item[] inventory = player.inventory;
		foreach (Item item in inventory)
		{
			if (item.IsAir)
			{
				continue;
			}
			if (item.Calamity().AppliedEnchantment.HasValue && item.Calamity().AppliedEnchantment.Value.ID == 600)
			{
				if (item.Calamity().DischargeEnchantExhaustion <= 0f)
				{
					item.Calamity().DischargeEnchantExhaustion = 1600f;
				}
				else if (item.Calamity().DischargeEnchantExhaustion < 1600f)
				{
					item.Calamity().DischargeEnchantExhaustion++;
				}
			}
			else
			{
				item.Calamity().DischargeEnchantExhaustion = 0f;
			}
		}
		if (heldItem.Calamity().AppliedEnchantment.HasValue && heldItem.Calamity().AppliedEnchantment.Value.HoldEffect != null)
		{
			heldItem.Calamity().AppliedEnchantment.Value.HoldEffect(player);
			if (modPlayer.flamingItemEnchant)
			{
				player.AddBuff(ModContent.BuffType<WeakBrimstoneFlames>(), 10);
			}
		}
	}

	private void StandingStillEffects()
	{
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		UpdateRogueStealth();
		if (aquaticEmblem)
		{
			if (countsAsAnyWet && !base.Player.lavaWet && !base.Player.honeyWet)
			{
				if (aquaticBoost < (float)AquaticEmblem.TimeToReachMaxBoost)
				{
					aquaticBoost++;
					if (aquaticBoost > (float)AquaticEmblem.TimeToReachMaxBoost)
					{
						aquaticBoost = AquaticEmblem.TimeToReachMaxBoost;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(84, -1, -1, null, base.Player.whoAmI);
						}
					}
				}
			}
			else
			{
				aquaticBoost--;
				if (aquaticBoost <= 0f)
				{
					aquaticBoost = 0f;
				}
			}
			if (!base.Player.mount.Active)
			{
				base.Player.statDefense += (int)Utils.Remap(aquaticBoost, 0f, AquaticEmblem.TimeToReachMaxBoost, 0f, AquaticEmblem.MaxDefenseBoost);
				base.Player.moveSpeed -= Utils.Remap(aquaticBoost, 0f, AquaticEmblem.TimeToReachMaxBoost, 0f, AquaticEmblem.MaxMoveSpeedReduction);
			}
		}
		else
		{
			aquaticBoost = 0f;
		}
		if (base.Player.HeldItem.type == ModContent.ItemType<Auralis>() && base.Player.StandingStill(0.1f))
		{
			if (auralisStealthCounter < 300f)
			{
				auralisStealthCounter++;
			}
			bool usingScope = false;
			if (!Main.gameMenu && !Main.dedServ && ((base.Player.noThrow <= 0 && !base.Player.lastMouseInterface) || !(Main.CurrentPan == Vector2.Zero)))
			{
				if (PlayerInput.UsingGamepad)
				{
					if (((Vector2)(ref PlayerInput.GamepadThumbstickRight)).Length() != 0f || !Main.SmartCursorIsUsed)
					{
						usingScope = true;
					}
				}
				else if (Main.mouseRight)
				{
					usingScope = true;
				}
			}
			int chargeDuration = CalamityUtils.SecondsToFrames(5f);
			CalamityUtils.SecondsToFrames(20f);
			if (usingScope && auralisAuroraCounter < chargeDuration && auralisAuroraCooldown == 0)
			{
				auralisAuroraCounter++;
			}
			if (auralisAuroraCounter > 0 && auralisAuroraCounter < chargeDuration && !usingScope)
			{
				auralisAuroraCounter--;
			}
		}
		else
		{
			auralisStealthCounter = 0f;
			if (auralisAuroraCounter > 0 && auralisAuroraCounter < 300)
			{
				auralisAuroraCounter--;
			}
		}
		if (auralisAuroraCooldown <= 0)
		{
			return;
		}
		if (auralisAuroraCooldown == 1)
		{
			int dustAmt = 36;
			for (int d = 0; d < dustAmt; d++)
			{
				Vector2 val = (Vector2.Normalize(base.Player.velocity) * new Vector2((float)base.Player.width / 2f, (float)base.Player.height) * 1f).RotatedBy((float)(d - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Player.Center;
				Vector2 dustVel = val - base.Player.Center;
				Dust dust = Dust.NewDustDirect(val + dustVel, 0, 0, 229, dustVel.X, dustVel.Y, 100, default(Color), 1.2f);
				dust.noGravity = true;
				dust.noLight = false;
				dust.velocity = dustVel;
			}
			for (int i = 0; i < dustAmt; i++)
			{
				Vector2 val2 = (Vector2.Normalize(base.Player.velocity) * new Vector2((float)base.Player.width / 2f, (float)base.Player.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Player.Center;
				Vector2 dustVel2 = val2 - base.Player.Center;
				Dust dust2 = Dust.NewDustDirect(val2 + dustVel2, 0, 0, 107, dustVel2.X, dustVel2.Y, 100, default(Color), 1.2f);
				dust2.noGravity = true;
				dust2.noLight = false;
				dust2.velocity = dustVel2;
			}
		}
		auralisAuroraCounter = 0;
	}

	private void OtherBuffEffects()
	{
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1631: Unknown result type (might be due to invalid IL or missing references)
		//IL_1641: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_180e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1815: Unknown result type (might be due to invalid IL or missing references)
		//IL_1827: Unknown result type (might be due to invalid IL or missing references)
		//IL_182c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1926: Unknown result type (might be due to invalid IL or missing references)
		//IL_192d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1956: Unknown result type (might be due to invalid IL or missing references)
		//IL_195b: Unknown result type (might be due to invalid IL or missing references)
		//IL_202b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2030: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_214e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2153: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c19: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c88: Unknown result type (might be due to invalid IL or missing references)
		//IL_23de: Unknown result type (might be due to invalid IL or missing references)
		//IL_2403: Unknown result type (might be due to invalid IL or missing references)
		//IL_241e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2432: Unknown result type (might be due to invalid IL or missing references)
		//IL_2437: Unknown result type (might be due to invalid IL or missing references)
		//IL_243c: Unknown result type (might be due to invalid IL or missing references)
		//IL_243e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2443: Unknown result type (might be due to invalid IL or missing references)
		//IL_2448: Unknown result type (might be due to invalid IL or missing references)
		//IL_2452: Unknown result type (might be due to invalid IL or missing references)
		//IL_2457: Unknown result type (might be due to invalid IL or missing references)
		//IL_245b: Unknown result type (might be due to invalid IL or missing references)
		//IL_245d: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2519: Unknown result type (might be due to invalid IL or missing references)
		//IL_251e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2532: Unknown result type (might be due to invalid IL or missing references)
		//IL_2537: Unknown result type (might be due to invalid IL or missing references)
		//IL_254b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2550: Unknown result type (might be due to invalid IL or missing references)
		//IL_2564: Unknown result type (might be due to invalid IL or missing references)
		//IL_2569: Unknown result type (might be due to invalid IL or missing references)
		//IL_2579: Unknown result type (might be due to invalid IL or missing references)
		//IL_257e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2592: Unknown result type (might be due to invalid IL or missing references)
		//IL_2597: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_25de: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_260d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2612: Unknown result type (might be due to invalid IL or missing references)
		//IL_2627: Unknown result type (might be due to invalid IL or missing references)
		//IL_262c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2631: Unknown result type (might be due to invalid IL or missing references)
		//IL_2636: Unknown result type (might be due to invalid IL or missing references)
		//IL_263e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2643: Unknown result type (might be due to invalid IL or missing references)
		//IL_2666: Unknown result type (might be due to invalid IL or missing references)
		//IL_266b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2675: Unknown result type (might be due to invalid IL or missing references)
		//IL_2691: Unknown result type (might be due to invalid IL or missing references)
		//IL_2697: Unknown result type (might be due to invalid IL or missing references)
		//IL_2699: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_26cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_26dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_26eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2705: Unknown result type (might be due to invalid IL or missing references)
		//IL_2707: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d52: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd9: Unknown result type (might be due to invalid IL or missing references)
		if (gravityNormalizer)
		{
			base.Player.buffImmune[164] = true;
			if (base.Player.ReducedSpaceGravity())
			{
				base.Player.gravity = Player.defaultGravity;
				if (base.Player.wet)
				{
					if (base.Player.honeyWet)
					{
						base.Player.gravity = 0.1f;
					}
					else if (base.Player.merman)
					{
						base.Player.gravity = 0.3f;
					}
					else if (base.Player.trident && !base.Player.lavaWet)
					{
						base.Player.gravity = (base.Player.controlUp ? 0.1f : 0.25f);
					}
					else
					{
						base.Player.gravity = 0.2f;
					}
				}
			}
		}
		if (decayEffigy)
		{
			base.Player.buffImmune[ModContent.BuffType<SulphuricPoisoning>()] = true;
			if (!ZoneAbyss && base.Player.IsUnderwater())
			{
				base.Player.gills = true;
			}
		}
		if (CobaltSet)
		{
			CobaltArmorSetChange.ApplyMovementSpeedBonuses(base.Player);
		}
		if (AdamantiteSet)
		{
			base.Player.statDefense += AdamantiteSetDefenseBoost;
		}
		if (astralInjection)
		{
			if (base.Player.statMana < base.Player.statManaMax2)
			{
				base.Player.statMana += AstralInjection.ManaPerFrame;
			}
			if (base.Player.statMana > base.Player.statManaMax2)
			{
				base.Player.statMana = base.Player.statManaMax2;
			}
		}
		if (irradiated)
		{
			base.Player.statDefense -= 10;
		}
		if (rRage)
		{
			base.Player.GetDamage<GenericDamageClass>() += ReaverHeadTank.ReaverRageDamageBoost;
			base.Player.statDefense += ReaverHeadTank.ReaverRageDefenseBoost;
		}
		if (xWrath)
		{
			base.Player.GetDamage<ThrowingDamageClass>() += EmpyreanMask.WrathRogueDamageBoost;
			base.Player.GetCritChance<RogueDamageClass>() += EmpyreanMask.WrathRogueCritBoost;
		}
		if (graxDefense)
		{
			base.Player.statDefense += Grax.DefenseBoost;
			base.Player.endurance += Grax.DamageReductionBoost;
			base.Player.GetDamage<GenericDamageClass>() += Grax.DamageBoost;
		}
		if (trinketOfChi)
		{
			if (chiBuffTimer < 600)
			{
				chiBuffTimer++;
			}
			else
			{
				base.Player.AddBuff(ModContent.BuffType<ChiBuff>(), 6);
			}
		}
		else
		{
			chiBuffTimer = 0;
		}
		if (darkSunRing)
		{
			base.Player.maxMinions += 2;
			base.Player.GetDamage<GenericDamageClass>() += 0.12f;
			base.Player.GetKnockback<SummonDamageClass>() += 1.2f;
			if (Main.eclipse || !Main.dayTime)
			{
				base.Player.statDefense += (Main.eclipse ? 8 : 16);
			}
		}
		if (AbsorberRegen)
		{
			base.Player.GetDamage<GenericDamageClass>() += TheAbsorber.AuraDamageBoost;
			base.Player.endurance += TheAbsorber.AuraDamageReductionBoost;
		}
		if (crawCarapace)
		{
			base.Player.GetDamage<GenericDamageClass>() += 0.07f;
		}
		if (baroclaw)
		{
			base.Player.endurance += 0.05f;
			base.Player.GetDamage<GenericDamageClass>() += 0.1f;
		}
		if (aeroStone && !base.Player.slowFall && base.Player.wingTime < (float)base.Player.wingTimeMax && !base.Player.controlJump && base.Player.miscCounter % 4 == 0)
		{
			base.Player.wingTime++;
		}
		if (gShell)
		{
			if (giantShellPostHit == 1)
			{
				SoundEngine.PlaySound(in SoundID.Zombie58, base.Player.Center);
			}
			if (giantShellPostHit > 0)
			{
				base.Player.statDefense -= 3;
				giantShellPostHit--;
			}
			if (giantShellPostHit < 0)
			{
				giantShellPostHit = 0;
			}
		}
		if (tortShell)
		{
			if (tortShellPostHit == 1)
			{
				SoundStyle style = SoundID.NPCHit24 with
				{
					Volume = 0.5f
				};
				SoundEngine.PlaySound(in style, base.Player.Center);
			}
			if (tortShellPostHit > 0)
			{
				base.Player.statDefense -= 6;
				tortShellPostHit--;
			}
			else
			{
				base.Player.endurance += 0.05f;
			}
			if (tortShellPostHit < 0)
			{
				tortShellPostHit = 0;
			}
		}
		if (eGauntlet)
		{
			base.Player.GetDamage<MeleeDamageClass>() += 0.15f;
			base.Player.GetCritChance<MeleeDamageClass>() += 5f;
		}
		if (gloveLevel > 0)
		{
			float gloveAttackSpeed = ((gloveLevel == 5) ? 0.15f : ((gloveLevel == 4) ? 0.14f : ((gloveLevel == 3) ? 0.12f : ((gloveLevel <= 2) ? 0.1f : 0f))));
			base.Player.GetAttackSpeed<MeleeDamageClass>() += gloveAttackSpeed;
		}
		if (bloodflareCore && bloodflareCoreRemainingHealOverTime > 0 && base.Player.miscCounter % BloodflareCore.HealFrameCooldown == 0)
		{
			base.Player.HealPlayer(1, HealTextType.Local);
			for (int i = 0; i < 3; i++)
			{
				Vector2 offset = Main.rand.NextVector2Unit() * Main.rand.NextFloat(23f, 33f);
				Vector2 position = base.Player.Center + offset;
				Vector2 dustVel = offset * -0.08f;
				Dust dust = Dust.NewDustDirect(position, 0, 0, 90, 0.08f, 0.08f);
				dust.velocity = dustVel;
				dust.noGravity = true;
			}
			bloodflareCoreRemainingHealOverTime--;
		}
		if (base.Player.chilled)
		{
			base.Player.moveSpeed *= 1.1666666f;
		}
		if (purpleHazeStealthTimer > 0)
		{
			if (!StealthStrikeAvailable() || base.Player.HeldItem.DamageType != RogueDamageClass.Instance)
			{
				base.Player.GetDamage(DamageClass.Generic) += PurpleHaze.DamageBoost;
			}
			else
			{
				stealthDamage -= PurpleHaze.StealthDamageLoss;
			}
		}
		if (everclear)
		{
			base.Player.GetDamage<GenericDamageClass>() += Everclear.DamageBoost;
		}
		if (caribbeanRum)
		{
			base.Player.gravity *= CaribbeanRum.GravityMultiplier;
			base.Player.moveSpeed += CaribbeanRum.MoveSpeedBoost;
		}
		if (starBeamRye)
		{
			base.Player.manaRegenCount += StarBeamRye.ManaRegenBoost;
			base.Player.GetDamage<MagicDamageClass>() *= StarBeamRye.MagicDmgMult;
		}
		if (whiteWine)
		{
			base.Player.wingTimeMax = (int)((float)base.Player.wingTimeMax * (1f - WhiteWine.FlightTimeLoss));
			float bonus = 0f;
			float MaxDistance = 640f;
			NPC closestTarget = base.Player.Center.ClosestNPCAt(MaxDistance * 7f);
			if (closestTarget != null)
			{
				float generousHitboxWidth = Math.Max((float)closestTarget.Hitbox.Width / 2f, (float)closestTarget.Hitbox.Height / 2f) + 100f;
				bonus = Utils.Remap(base.Player.Center.Distance(closestTarget.Center), MaxDistance + generousHitboxWidth, generousHitboxWidth, 0f, 1f);
			}
			else
			{
				bonus = 0f;
			}
			whiteWineTimer += bonus * WhiteWine.FlightTimeRecoveryAmount;
			while (whiteWineTimer > 1f)
			{
				if (base.Player.wingTime < (float)base.Player.wingTimeMax)
				{
					base.Player.wingTime++;
				}
				whiteWineTimer--;
			}
		}
		if (redWine)
		{
			base.Player.wingTimeMax = (int)((float)base.Player.wingTimeMax * (1f - RedWine.FlightTimeLoss));
		}
		Vector2 center;
		if (giantPearl && Main.netMode != 1)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc = enumerator.Current;
				if (!npc.friendly && !npc.dontTakeDamage)
				{
					center = npc.Center - base.Player.Center;
					if (((Vector2)(ref center)).Length() < 120f)
					{
						npc.AddBuff(ModContent.BuffType<PearlAura>(), 20);
					}
				}
			}
		}
		if (CalamityItemSets.FishingPoleThatNeverBreaks[base.Player.HeldItem.type])
		{
			base.Player.accFishingLine = true;
		}
		if (planarSpeedBoost != 0 && base.Player.HeldItem.type != ModContent.ItemType<PridefulHuntersPlanarRipper>())
		{
			planarSpeedBoost = 0;
		}
		if (evilSmasherBoost > 0 && base.Player.HeldItem.type != ModContent.ItemType<EvilSmasher>())
		{
			evilSmasherBoost = 0;
		}
		double flightTimeMult = 1.0 + (harpyRing ? 0.2 : 0.0) + (reaverSpeed ? ((double)ReaverHeadMobility.SetBonusFlightBoost) : 0.0) + (angelTreads ? ((double)AngelTreads.FlightTimeBoost) : 0.0) + (blueCandle ? WeightlessCandle.WingTimeBoost : 0.0) + (soaring ? ((double)SoaringPotion.FlightBoost) : 0.0) + (prismaticGreaves ? ((double)PrismaticGreaves.FlightTimeBoost) : 0.0) + (plagueReaper ? ((double)PlagueReaperMask.SetBonusFlightTimeBoost) : 0.0) + (ascendantInsignia ? AscendantInsignia.FlightTimeBoost : (base.Player.empressBrooch ? 0.33 : 0.0)) + (double)externalFlightTimeMultBoost;
		if (community)
		{
			float baseBoost = TheCommunity.CalculatePower();
			base.Player.endurance += baseBoost * 0.25f;
			base.Player.statDefense += (int)(baseBoost * 50f);
			base.Player.GetDamage<GenericDamageClass>() += baseBoost * 0.5f;
			base.Player.GetCritChance<GenericDamageClass>() += baseBoost * 25f;
			base.Player.moveSpeed += baseBoost * 0.5f;
			flightTimeMult += (double)(baseBoost * 1f);
		}
		if (shatteredCommunity)
		{
			flightTimeMult += 0.20000000298023224;
		}
		if (reaverDefense)
		{
			flightTimeMult -= (double)ReaverHeadTank.SetBonusMobilityReduction;
		}
		if (base.Player.wingTimeMax > 0)
		{
			base.Player.wingTimeMax = (int)((double)base.Player.wingTimeMax * flightTimeMult);
		}
		if (vHex)
		{
			base.Player.statDefense -= 20;
		}
		if (icarusFolly)
		{
			if (base.Player.wingTimeMax < 0)
			{
				base.Player.wingTimeMax = 0;
			}
			if (base.Player.wingTimeMax > IcarusFolly.MaxFlightTimeCap)
			{
				base.Player.wingTimeMax = IcarusFolly.MaxFlightTimeCap;
			}
			base.Player.wingTimeMax = (int)((float)base.Player.wingTimeMax * (1f - IcarusFolly.FlightTimeLossPercent));
		}
		if (DoGExtremeGravity)
		{
			if (base.Player.wingTimeMax < 0)
			{
				base.Player.wingTimeMax = 0;
			}
			if (base.Player.wingTimeMax > global::CalamityMod.Buffs.StatDebuffs.DoGExtremeGravity.MaxFlightTimeCap)
			{
				base.Player.wingTimeMax = global::CalamityMod.Buffs.StatDebuffs.DoGExtremeGravity.MaxFlightTimeCap;
			}
			base.Player.wingTimeMax = (int)((float)base.Player.wingTimeMax * (1f - global::CalamityMod.Buffs.StatDebuffs.DoGExtremeGravity.FlightTimeLossPercent));
		}
		if (bounding)
		{
			base.Player.jumpSpeedBoost += BoundingPotion.JumpSpeedBoost;
			Player.jumpHeight += (int)(BoundingPotion.JumpHeightPercentBoost * 15f);
		}
		if (mushy)
		{
			base.Player.statDefense += Mushy.DefenseBoost;
			if (fungalSymbiote)
			{
				base.Player.GetDamage<GenericDamageClass>() += 0.1f;
			}
		}
		if (omniscience)
		{
			base.Player.detectCreature = true;
			base.Player.dangerSense = true;
			base.Player.findTreasure = true;
		}
		if (tarraSet)
		{
			base.Player.lifeMagnet = true;
		}
		if (whisperingDeath && !laudanum && !purity)
		{
			base.Player.GetDamage<GenericDamageClass>() -= 0.2f;
		}
		if (armorCrunch && !laudanum && !purity)
		{
			base.Player.statDefense -= ArmorCrunch.DefenseReduction;
			base.Player.endurance *= ArmorCrunch.MultiplicativeDamageReductionPlayer;
		}
		if (wither && !purity)
		{
			base.Player.statDefense -= RemsRevenge.WitherDefenseReduction;
		}
		if (eutrophication && !purity)
		{
			base.Player.velocity = Vector2.Zero;
		}
		if ((vaporfied && !purity) || galvanicCorrosion)
		{
			Player player = base.Player;
			player.velocity *= 0.98f;
		}
		if (molluskHelmet)
		{
			base.Player.velocity.X *= 0.996f;
		}
		if (molluskChest)
		{
			base.Player.velocity.X *= 0.996f;
		}
		if (molluskLegs)
		{
			base.Player.velocity.X *= 0.996f;
		}
		if (warped && !base.Player.slowFall && !base.Player.mount.Active)
		{
			float velocityYMultiplier = 1.01f;
			base.Player.velocity.Y *= velocityYMultiplier;
		}
		if (corrEffigy)
		{
			base.Player.moveSpeed += CorruptionEffigy.MoveSpeedBoost;
			base.Player.GetCritChance<GenericDamageClass>() += CorruptionEffigy.CritBoost;
		}
		if (crimEffigy)
		{
			base.Player.GetDamage<GenericDamageClass>() += CrimsonEffigy.DamageBoost;
			base.Player.statDefense += CrimsonEffigy.DefenseBoost;
		}
		actualMaxLife = base.Player.statLifeMax2;
		if (!base.Player.dead && healToFull)
		{
			healToFull = false;
			base.Player.statLife = actualMaxLife;
		}
		if (manaOverloader)
		{
			float manaRatio = (float)base.Player.statMana / (float)base.Player.statManaMax2;
			base.Player.GetDamage<MagicDamageClass>() += MathHelper.Lerp(0.05f, 0.15f, manaRatio);
		}
		if (bloodyWormTooth)
		{
			base.Player.GetDamage<MeleeDamageClass>() += 0.1f;
		}
		if (filthyGlove)
		{
			bonusStealthDamage += (nanotech ? 0.05f : 0.08f);
		}
		if (rottenDogTooth && !nanotech)
		{
			bonusStealthDamage += 0.07999999821186066;
		}
		if (sandsWindBuff)
		{
			base.Player.GetDamage<GenericDamageClass>() += PrimordialEarth.BuffDamageBoost;
			base.Player.statDefense += PrimordialEarth.BuffDefenseBoost;
			base.Player.manaRegenDelayBonus++;
			base.Player.manaRegenBonus += 50 + (int)(400f * (float)Math.Pow(1f - (float)base.Player.statMana / (float)base.Player.statManaMax2, 2.0));
		}
		if (aeolianEarthBuff)
		{
			base.Player.GetDamage<GenericDamageClass>() += PrimordialAncient.BuffDamageBoost;
			base.Player.endurance += PrimordialAncient.BuffDamageReductionBoost;
			base.Player.manaRegenDelayBonus++;
			base.Player.manaRegenBonus += 75 + (int)(600f * (float)Math.Pow(1f - (float)base.Player.statMana / (float)base.Player.statManaMax2, 2.0));
		}
		if (frostFlare)
		{
			base.Player.resistCold = true;
			base.Player.buffImmune[44] = true;
			base.Player.buffImmune[46] = true;
			base.Player.buffImmune[47] = true;
			if (base.Player.statLife > (int)((double)base.Player.statLifeMax2 * 0.5))
			{
				base.Player.GetDamage<GenericDamageClass>() += 0.1f;
			}
			if (base.Player.statLife <= (int)((double)base.Player.statLifeMax2 * 0.5))
			{
				base.Player.statDefense += 15;
			}
		}
		if (vexation)
		{
			base.Player.GetDamage<GenericDamageClass>() += 0.3f * (1f - (float)base.Player.statLife / (float)base.Player.statLifeMax2);
		}
		if (ataxiaBlaze && base.Player.statLife <= (int)((float)base.Player.statLifeMax2 * HydrothermicArmor.InfernoHealthThreshold))
		{
			base.Player.AddBuff(116, 2);
		}
		if (bloodflareThrowing && base.Player.statLife > (int)((float)base.Player.statLifeMax2 * BloodflareHeadRogue.DefenseBoostHealthThreshold))
		{
			base.Player.statDefense += BloodflareHeadRogue.DefenseBoostAboveHealthThreshold;
		}
		if (bloodflareSummon)
		{
			if (base.Player.statLife <= (int)((float)base.Player.statLifeMax2 * BloodflareHeadSummon.DefenseBoostHealthThreshold))
			{
				base.Player.statDefense += BloodflareHeadSummon.DefenseBoostBelowHealthThreshold;
			}
			if (bloodflareSummonTimer > 0)
			{
				bloodflareSummonTimer--;
			}
			if (base.Player.whoAmI == Main.myPlayer && bloodflareSummonTimer <= 0)
			{
				bloodflareSummonTimer = BloodflareHeadSummon.MineCooldown;
				IEntitySource source = base.Player.GetSource_FromThis(BloodflareHeadSummon.GhostMineEntitySourceContext);
				for (int I = 0; I < 3; I++)
				{
					float ai1 = I * 120;
					int damage = base.Player.CalcIntDamage<SummonDamageClass>(BloodflareHeadSummon.MineDamage);
					int projectile = Projectile.NewProjectile(source, base.Player.Center.X + (float)(Math.Sin(I * 120) * 550.0), base.Player.Center.Y + (float)(Math.Cos(I * 120) * 550.0), 0f, 0f, ModContent.ProjectileType<GhostlyMine>(), damage, 1f, base.Player.whoAmI, ai1);
					if (projectile.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[projectile].originalDamage = BloodflareHeadSummon.MineDamage;
						Main.projectile[projectile].DamageType = DamageClass.Generic;
					}
				}
			}
		}
		if (silvaSummon && base.Player.whoAmI == Main.myPlayer)
		{
			IEntitySource source2 = base.Player.GetSource_FromThis(SilvaHeadSummon.SilvaCrystalEntitySourceContext);
			if (base.Player.FindBuffIndex(ModContent.BuffType<SilvaCrystalBuff>()) == -1)
			{
				base.Player.AddBuff(ModContent.BuffType<SilvaCrystalBuff>(), 3600);
			}
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<SilvaCrystal>()] < 1)
			{
				int baseDmg = (auricSet ? AuricTeslaHeadSummon.CrystalDamage : SilvaHeadSummon.CrystalDamage);
				int damage2 = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDmg);
				int p = Projectile.NewProjectile(source2, base.Player.Center.X, base.Player.Center.Y, 0f, -1f, ModContent.ProjectileType<SilvaCrystal>(), damage2, 0f, Main.myPlayer, -20f);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = baseDmg;
				}
			}
		}
		if (ascendantInsignia && ascendantInsigniaBuffTime > 0)
		{
			infiniteFlight = true;
			if (ascendantInsigniaBuffTime == 1)
			{
				base.Player.AddCooldown(AscendEffect.ID, AscendantInsignia.AbilityCooldown);
			}
			ascendantInsigniaBuffTime--;
		}
		if (abyssalDivingSuit && !base.Player.IsUnderwater())
		{
			base.Player.moveSpeed -= 0.6f;
		}
		if (godSlayerThrowing && base.Player.statLife >= base.Player.statLifeMax2)
		{
			base.Player.GetDamage<ThrowingDamageClass>() += GodSlayerHeadRogue.RogueDamageBoostAtFullHealth;
			base.Player.GetCritChance<RogueDamageClass>() += GodSlayerHeadRogue.RogueCritBoostAtFullHealth;
			rogueVelocity += GodSlayerHeadRogue.RogueVelocityBoostAtFullHealth;
		}
		if (tarraSummon)
		{
			tarraLifeAuraTimer = (tarraLifeAuraTimer + 1) % 80;
			if (tarraLifeAuraTimer == 0 && base.Player.whoAmI == Main.myPlayer)
			{
				int damage3 = (int)base.Player.CalcDamage<SummonDamageClass>(TarragonHeadSummon.AuraDamage);
				IEntitySource source3 = base.Player.GetSource_FromThis(TarragonHeadSummon.LifeAuraEntitySourceContext);
				float range = 300f;
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC npc2 = enumerator2.Current;
					if (!npc2.friendly && !npc2.dontTakeDamage && Vector2.Distance(base.Player.Center, npc2.Center) <= range)
					{
						Projectile.NewProjectile(source3, npc2.Center, Vector2.Zero, ModContent.ProjectileType<TarragonAura>(), damage3, 0f, base.Player.whoAmI, npc2.whoAmI);
					}
				}
			}
		}
		if (ataxiaBlaze && base.Player.inferno)
		{
			hydrothermicInfernoTimer = (hydrothermicInfernoTimer + 1) % HydrothermicArmor.InfernoHitRate;
			if (base.Player.whoAmI == Main.myPlayer)
			{
				int damage4 = (int)base.Player.GetBestClassDamage().ApplyTo(HydrothermicArmor.InfernoDamage);
				IEntitySource source4 = base.Player.GetSource_FromThis(HydrothermicArmor.InfernoPotionEntitySourceContext);
				float range2 = HydrothermicArmor.InfernoRange;
				ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					NPC npc3 = enumerator3.Current;
					if (!npc3.friendly && npc3.damage > 0 && !npc3.dontTakeDamage && Vector2.Distance(base.Player.Center, npc3.Center) <= range2)
					{
						npc3.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
						if (hydrothermicInfernoTimer == 0)
						{
							Projectile.NewProjectile(source4, npc3.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage4, 0f, base.Player.whoAmI, npc3.whoAmI);
						}
					}
				}
			}
		}
		if (royalGel)
		{
			base.Player.npcTypeNoAggro[ModContent.NPCType<AeroSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<BloomSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<InfernalCongealment>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<CrimulanBlightSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<CryoSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<EbonianBlightSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<IrradiatedSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<PerennialSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<PestilentSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<AstralSlime>()] = true;
			base.Player.npcTypeNoAggro[ModContent.NPCType<GammaSlime>()] = true;
		}
		if (eArtifact)
		{
			base.Player.manaCost -= 0.25f;
			base.Player.maxMinions++;
		}
		if (auricSArtifact && base.Player.FindBuffIndex(ModContent.BuffType<FieryDraconidBuff>()) != -1)
		{
			base.Player.maxMinions += base.Player.ownedProjectileCounts[ModContent.ProjectileType<FieryDraconid>()];
		}
		if (pSoulArtifact && base.Player.whoAmI == Main.myPlayer)
		{
			IEntitySource source5 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<ProfanedSoulArtifact>()));
			if (base.Player.HasBuff(ModContent.BuffType<ProfanedSoulGuardians>()))
			{
				base.Player.buffTime[base.Player.FindBuffIndex(ModContent.BuffType<ProfanedSoulGuardians>())] = 3600;
			}
			else
			{
				base.Player.AddBuff(ModContent.BuffType<ProfanedSoulGuardians>(), 3600);
			}
			pSoulGuardians = true;
			int guardianAmt = 1;
			float babCheck = (profanedCrystal ? 1f : 0f);
			int babDamage = (profanedCrystal ? 346 : 52);
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<MiniGuardianHealer>()] < guardianAmt)
			{
				Projectile.NewProjectileDirect(source5, base.Player.Center, Vector2.UnitY * -6f, ModContent.ProjectileType<MiniGuardianHealer>(), 0, 0f, Main.myPlayer, babCheck).originalDamage = babDamage;
			}
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<MiniGuardianDefense>()] < guardianAmt)
			{
				Projectile.NewProjectileDirect(source5, base.Player.Center, Vector2.UnitY * -3f, ModContent.ProjectileType<MiniGuardianDefense>(), 1, 1f, Main.myPlayer, babCheck).originalDamage = babDamage;
			}
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<MiniGuardianAttack>()] < guardianAmt)
			{
				float spearCounter = (profanedCrystal ? 480f : 15f);
				Projectile.NewProjectileDirect(source5, base.Player.Center, Vector2.UnitY * -1f, ModContent.ProjectileType<MiniGuardianAttack>(), 1, 1f, Main.myPlayer, babCheck, spearCounter).originalDamage = babDamage;
			}
		}
		if (profanedCrystal)
		{
			ProfanedSoulCrystal.DetermineTransformationEligibility(base.Player);
			CalamityPlayer calPlayer = base.Player.Calamity();
			if (calPlayer.pscState != 0)
			{
				bool num = calPlayer.pscState == 3;
				bool night = num || calPlayer.pscState == 2;
				bool day = num || calPlayer.pscState == 1;
				base.Player.lavaImmune = true;
				base.Player.fireWalk = true;
				base.Player.buffImmune[ModContent.BuffType<HolyFlames>()] = true;
				base.Player.buffImmune[24] = true;
				base.Player.buffImmune[67] = true;
				base.Player.buffImmune[189] = true;
				if (base.Player.wingTimeMax > 0)
				{
					base.Player.wingTimeMax = (int)((double)base.Player.wingTimeMax * 1.1);
				}
				base.Player.GetDamage<SummonDamageClass>() += 0.15f;
				if (day)
				{
					base.Player.GetKnockback<SummonDamageClass>() += 0.15f;
					base.Player.moveSpeed += 0.1f;
					base.Player.ignoreWater = true;
					base.Player.GetAttackSpeed(DamageClass.SummonMeleeSpeed)++;
				}
				else if (night)
				{
					base.Player.endurance += 0.05f;
					base.Player.statDefense += 15;
					base.Player.lifeRegen += 5;
				}
				if (!calPlayer.ZoneAbyss)
				{
					Lighting.AddLight(base.Player.Center, night ? 1.2f : (day ? 1f : 0.2f), night ? 0.21f : (day ? 0.2f : 0.01f), 0f);
				}
			}
		}
		List<int> summonDeleteList = new List<int>
		{
			ModContent.ProjectileType<BrimstoneElementalMinion>(),
			ModContent.ProjectileType<WaterElementalMinion>(),
			ModContent.ProjectileType<SandElementalHealer>(),
			ModContent.ProjectileType<SandElementalMinion>(),
			ModContent.ProjectileType<CloudElementalMinion>(),
			ModContent.ProjectileType<FungalClumpMinion>(),
			ModContent.ProjectileType<HowlsHeartHowl>(),
			ModContent.ProjectileType<HowlsHeartCalcifer>(),
			ModContent.ProjectileType<HowlsHeartTurnipHead>(),
			ModContent.ProjectileType<MiniGuardianAttack>(),
			ModContent.ProjectileType<MiniGuardianDefense>(),
			ModContent.ProjectileType<MiniGuardianHealer>()
		};
		int projAmt = 1;
		for (int j = 0; j < summonDeleteList.Count; j++)
		{
			if (base.Player.ownedProjectileCounts[summonDeleteList[j]] <= projAmt)
			{
				continue;
			}
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (proj.active && proj.owner == base.Player.whoAmI && summonDeleteList.Contains(proj.type))
				{
					proj.Kill();
				}
			}
		}
		if (blunderBooster)
		{
			if (base.Player.whoAmI == Main.myPlayer)
			{
				IEntitySource source6 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<BlunderBooster>()));
				int damage5 = (int)base.Player.CalcDamage<RogueDamageClass>(30f);
				if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<BlunderBoosterAura>()] < 1)
				{
					Projectile.NewProjectile(source6, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<BlunderBoosterAura>(), damage5, 0f, base.Player.whoAmI);
				}
			}
		}
		else if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<BlunderBoosterAura>()] != 0 && base.Player.whoAmI == Main.myPlayer)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator4 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator4.MoveNext())
			{
				Projectile p2 = enumerator4.Current;
				if (p2.type == ModContent.ProjectileType<BlunderBoosterAura>() && p2.owner == base.Player.whoAmI)
				{
					p2.Kill();
					break;
				}
			}
		}
		if (tesla)
		{
			if (base.Player.whoAmI == Main.myPlayer)
			{
				if (true)
				{
					IEntitySource source7 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<TeslasAmulet>()));
					int damage6 = (int)base.Player.GetBestClassDamage().ApplyTo(12f);
					if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<TeslaAura>()] < 1)
					{
						Projectile.NewProjectile(source7, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<TeslaAura>(), damage6, 0f, base.Player.whoAmI);
					}
				}
				for (int l = 0; l < Player.MaxBuffs; l++)
				{
					if (base.Player.buffType[l] == ModContent.BuffType<StaticDischarge>() && base.Player.buffTime[l] > 2)
					{
						base.Player.buffTime[l]--;
						break;
					}
				}
			}
		}
		else if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<TeslaAura>()] > 0 && base.Player.whoAmI == Main.myPlayer)
		{
			int auraType = ModContent.ProjectileType<TeslaAura>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator5 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator5.MoveNext())
			{
				Projectile p3 = enumerator5.Current;
				if (p3.type == auraType && p3.owner == base.Player.whoAmI)
				{
					p3.Kill();
					break;
				}
			}
		}
		if (CryoStone || CryoStoneVanity)
		{
			IEntitySource source8 = base.Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<CryoStone>()));
			int damage7 = (int)base.Player.GetBestClassDamage().ApplyTo(70f);
			if (base.Player.whoAmI == Main.myPlayer && base.Player.ownedProjectileCounts[ModContent.ProjectileType<CryonicShield>()] == 0)
			{
				Projectile.NewProjectile(source8, base.Player.Center, Vector2.Zero, ModContent.ProjectileType<CryonicShield>(), damage7, 0f, base.Player.whoAmI);
			}
		}
		else if (base.Player.whoAmI == Main.myPlayer)
		{
			int shieldType = ModContent.ProjectileType<CryonicShield>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator6 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator6.MoveNext())
			{
				Projectile p4 = enumerator6.Current;
				if (p4.type == shieldType && p4.owner == base.Player.whoAmI)
				{
					p4.Kill();
					break;
				}
			}
		}
		if (prismaticLasers > PrismaticHelmet.LaserCooldown && base.Player.whoAmI == Main.myPlayer)
		{
			int dmg = (int)base.Player.GetTotalDamage<MagicDamageClass>().ApplyTo(PrismaticHelmet.LaserDamage);
			IEntitySource source9 = base.Player.GetSource_FromThis(PrismaticHelmet.LaserEntitySourceContext);
			int laserAmt = Main.rand.Next(2);
			Vector2 newPos = default(Vector2);
			for (int index = 0; index < laserAmt; index++)
			{
				((Vector2)(ref newPos))._002Ector(base.Player.ClampedMouseWorld().X + Main.rand.NextFloat(-240f, 240f), base.Player.MountedCenter.Y - 960f);
				Vector2 newVel = (base.Player.ClampedMouseWorld() + Main.rand.NextVector2CircularEdge(24f, 24f) - newPos).SafeNormalize(Vector2.Zero) * 18f;
				Projectile projectile2 = Projectile.NewProjectileDirect(source9, newPos, newVel, ModContent.ProjectileType<DeathhailBeam>(), dmg, 4f, base.Player.whoAmI);
				projectile2.localNPCHitCooldown = 5;
				projectile2.DamageType = DamageClass.Generic;
			}
			SoundEngine.PlaySound(in SoundID.Item12, base.Player.Center);
		}
		if (prismaticLasers == PrismaticHelmet.LaserCooldown)
		{
			base.Player.AddCooldown(PrismaticLaser.ID, PrismaticHelmet.LaserCooldown);
		}
		if (prismaticLasers == 1)
		{
			int dustAmt = 36;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Color color = Utils.SelectRandom(Main.rand, (Color[])(object)new Color[12]
				{
					new Color(255, 0, 0, 50),
					new Color(255, 128, 0, 50),
					new Color(255, 255, 0, 50),
					new Color(128, 255, 0, 50),
					new Color(0, 255, 0, 50),
					new Color(0, 255, 128, 50),
					new Color(0, 255, 255, 50),
					new Color(0, 128, 255, 50),
					new Color(0, 0, 255, 50),
					new Color(128, 0, 255, 50),
					new Color(255, 0, 255, 50),
					new Color(255, 0, 128, 50)
				});
				Vector2 spinningpoint = Vector2.Normalize(base.Player.velocity) * new Vector2((float)base.Player.width / 2f, (float)base.Player.height) * 0.75f;
				double radians = (float)(dustIndex - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt;
				center = default(Vector2);
				Vector2 val = spinningpoint.RotatedBy(radians, center) + base.Player.Center;
				Vector2 dustVel2 = val - base.Player.Center;
				Dust dust2 = Dust.NewDustDirect(val + dustVel2, 0, 0, 267, dustVel2.X * 1f, dustVel2.Y * 1f, 100, color);
				dust2.noGravity = true;
				dust2.noLight = true;
				dust2.velocity = dustVel2;
			}
		}
		if (angelicAlliance && Main.myPlayer == base.Player.whoAmI)
		{
			for (int k = 0; k < Player.MaxBuffs; k++)
			{
				if (base.Player.buffType[k] == ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.DivineBless>())
				{
					angelicActivate = base.Player.buffTime[k];
				}
			}
			if (base.Player.FindBuffIndex(ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.DivineBless>()) == -1)
			{
				angelicActivate = -1;
			}
			if (angelicActivate == 1)
			{
				base.Player.AddCooldown(global::CalamityMod.Cooldowns.DivineBless.ID, AngelicAlliance.DivineBlessCooldown);
			}
		}
		if (theBee && base.Player.statLife >= base.Player.statLifeMax2 && (!HasAnyEnergyShield || TotalEnergyShielding >= TotalMaxShieldDurability))
		{
			float beeBoost = base.Player.endurance / 2f;
			base.Player.GetDamage<GenericDamageClass>() += beeBoost;
		}
		if (badgeOfBravery)
		{
			base.Player.GetDamage<MeleeDamageClass>() += 0.1f;
			base.Player.GetCritChance<MeleeDamageClass>() += 10f;
		}
		if (Main.myPlayer == base.Player.whoAmI)
		{
			for (int m = 0; m < Player.MaxBuffs; m++)
			{
				int buffID = base.Player.buffType[m];
				if (!CalamityBuffSets.BuffedByAmalgam[buffID])
				{
					continue;
				}
				if (amalgam)
				{
					if (base.Player.miscCounter % 2 == 0 && base.Player.buffTime[m] > 2)
					{
						base.Player.buffTime[m]++;
					}
					if (!Main.persistentBuff[buffID])
					{
						Main.persistentBuff[buffID] = true;
					}
				}
				else if (Main.persistentBuff[buffID] && !CalamityBuffSets.IsPersistentBuff[buffID])
				{
					Main.persistentBuff[buffID] = false;
				}
			}
		}
		if (laudanum)
		{
			int[] obj = new int[14]
			{
				0, 0, 164, 69, 30, 46, 36, 33, 32, 31,
				23, 35, 80, 22
			};
			obj[0] = ModContent.BuffType<ArmorCrunch>();
			obj[1] = ModContent.BuffType<WhisperingDeath>();
			int[] buffsAffected = obj;
			for (int n = 0; n < buffsAffected.Length; n++)
			{
				base.Player.buffImmune[buffsAffected[n]] = false;
			}
			if (Main.myPlayer == base.Player.whoAmI)
			{
				for (int num2 = 0; num2 < Player.MaxBuffs; num2++)
				{
					int hasBuff = base.Player.buffType[num2];
					if (buffsAffected.Contains(hasBuff) && base.Player.miscCounter % 2 == 0)
					{
						base.Player.buffTime[num2]++;
					}
					if (hasBuff == ModContent.BuffType<ArmorCrunch>())
					{
						base.Player.statDefense += ArmorCrunch.DefenseReduction;
					}
					else if (hasBuff == ModContent.BuffType<WhisperingDeath>())
					{
						base.Player.lifeRegenCount += 5;
						base.Player.GetDamage<GenericDamageClass>() += 0.2f;
					}
					switch (hasBuff)
					{
					case 164:
						base.Player.vortexDebuff = false;
						base.Player.moveSpeed += 0.2f;
						base.Player.jumpSpeedBoost++;
						break;
					case 69:
						base.Player.statDefense += 20;
						break;
					case 30:
						base.Player.bleed = false;
						base.Player.lifeRegen += 4;
						base.Player.lifeRegenTime += 4f;
						break;
					case 46:
						base.Player.chilled = false;
						base.Player.moveSpeed *= 1.25f;
						break;
					case 36:
						base.Player.brokenArmor = false;
						base.Player.statDefense += (int)((double)(int)base.Player.statDefense * 0.25);
						break;
					case 33:
						base.Player.GetDamage<MeleeDamageClass>() += 0.051f;
						base.Player.GetDamage<GenericDamageClass>() += 0.1f;
						base.Player.statDefense += 10;
						base.Player.moveSpeed += 0.25f;
						break;
					case 32:
						base.Player.slow = false;
						base.Player.moveSpeed *= 1.5f;
						break;
					case 31:
						base.Player.confused = false;
						base.Player.statDefense += 15;
						break;
					case 23:
						base.Player.cursed = false;
						if (base.Player.mount.Type != 8)
						{
							base.Player.noItems = false;
						}
						base.Player.GetDamage<GenericDamageClass>() += 0.1f;
						break;
					case 35:
						base.Player.silence = false;
						base.Player.GetDamage<MagicDamageClass>() += 0.1f;
						break;
					case 80:
						base.Player.blackout = false;
						base.Player.GetCritChance<GenericDamageClass>() += 15f;
						break;
					case 22:
						base.Player.blind = false;
						base.Player.GetCritChance<GenericDamageClass>() += 10f;
						break;
					}
				}
			}
		}
		EnduranceReductions();
		if (spectralVeilImmunity > 0)
		{
			int numDust = 2;
			for (int num3 = 0; num3 < numDust; num3++)
			{
				Dust dust3 = Dust.NewDustDirect(base.Player.position, base.Player.width, base.Player.height, 21);
				dust3.position.X += Main.rand.Next(-5, 6);
				dust3.position.Y += Main.rand.Next(-5, 6);
				dust3.velocity *= 0.2f;
				dust3.noGravity = true;
				dust3.noLight = true;
			}
		}
		GemTechState.ProvideGemBoosts();
		float multiplicativeDamage = 1f;
		if (dArtifact)
		{
			multiplicativeDamage += 0.2f;
		}
		if (WarbanneroftheRighteous)
		{
			multiplicativeDamage += warbannerDamageMult;
		}
		if (multiplicativeDamage != 1f)
		{
			base.Player.GetDamage<GenericDamageClass>() *= multiplicativeDamage;
		}
	}

	public void HandleLucreciaLineEffects()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		int lucreciaItemID = ModContent.ItemType<Lucrecia>();
		int lightspeedItemID = ModContent.ItemType<Lightspeed>();
		Color color2;
		if (base.Player.HeldItem.type == lucreciaItemID && darklightEnergy > 0)
		{
			lucreciaParticleTimer--;
			if (lucreciaParticleTimer <= 0)
			{
				lucreciaParticleTimer = 20 - 15 * (darklightEnergy / 60);
				float radius = Main.rand.NextFloat(160f, 190f);
				float spawnAngle = Main.rand.NextFloat((float)Math.PI * 2f);
				Vector2 position = base.Player.Center + spawnAngle.ToRotationVector2() * radius;
				float opacity = (float)darklightEnergy / (float)Lucrecia.MaxEnergy;
				Color color = (Main.rand.NextBool() ? Color.MediumPurple : Color.CornflowerBlue);
				color *= opacity * 0.5f;
				if (darklightEnergy >= 100)
				{
					color *= 2.4f;
				}
				Vector2 dummyVelocity = Vector2.Zero;
				float rotationSpeed = 0.04f;
				color2 = color;
				((Color)(ref color2)).A = 0;
				GeneralParticleHandler.SpawnParticle(new RoundedStarParticle(position, dummyVelocity, color2, Main.rand.NextFloat(0.05f, 0.065f), Main.rand.Next(30, 60), rotationSpeed, 1f, useSpiralAI: true, base.Player.Center, base.Player.whoAmI));
			}
		}
		if (base.Player.HeldItem.type != lightspeedItemID || elementalMastery <= 0)
		{
			return;
		}
		lucreciaParticleTimer--;
		if (lucreciaParticleTimer <= 0)
		{
			lucreciaParticleTimer = 20 - 15 * (elementalMastery / 60);
			float radius2 = Main.rand.NextFloat(160f, 190f);
			float spawnAngle2 = Main.rand.NextFloat((float)Math.PI * 2f);
			Vector2 position2 = base.Player.Center + spawnAngle2.ToRotationVector2() * radius2;
			float opacity2 = (float)elementalMastery / (float)Lightspeed.MaxEnergy;
			Color color3 = (Main.rand.NextBool() ? Color.Aqua : Color.OrangeRed);
			color3 *= opacity2 * 0.5f;
			if (elementalMastery >= 100)
			{
				color3 *= 2.4f;
			}
			Vector2 dummyVelocity2 = Vector2.Zero;
			float rotationSpeed2 = 0.04f;
			color2 = color3;
			((Color)(ref color2)).A = 0;
			GeneralParticleHandler.SpawnParticle(new RoundedStarParticle(position2, dummyVelocity2, color2, Main.rand.NextFloat(0.05f, 0.065f), Main.rand.Next(30, 60), rotationSpeed2, 1f, useSpiralAI: true, base.Player.Center, base.Player.whoAmI));
		}
	}

	private void EnergyShields()
	{
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		bool shieldAddedLight = false;
		Color val;
		if (!sponge)
		{
			if (cooldowns.TryGetValue(SpongeDurability.ID, out var cdDurability))
			{
				cdDurability.timeLeft = 0;
			}
			SpongeShieldDurability = 0;
		}
		else
		{
			if (SpongeShieldDurability == 0 && !cooldowns.ContainsKey(SpongeRecharge.ID))
			{
				base.Player.AddCooldown(SpongeRecharge.ID, TheSponge.ShieldRechargeDelay);
			}
			if (SpongeShieldDurability > 0 && !cooldowns.ContainsKey(SpongeDurability.ID))
			{
				base.Player.AddCooldown(SpongeDurability.ID, TheSponge.ShieldDurabilityMax).timeLeft = SpongeShieldDurability;
			}
			if (SpongeShieldDurability > 0 && !cooldowns.ContainsKey(SpongeRecharge.ID))
			{
				if (!playedSpongeShieldSound)
				{
					SoundEngine.PlaySound(in TheSponge.ActivationSound, base.Player.Center);
				}
				playedSpongeShieldSound = true;
				spongeShieldPartialRechargeProgress += (float)TheSponge.ShieldDurabilityMax / (float)TheSponge.TotalShieldRechargeTime;
				int pointsActuallyRecharged = (int)MathF.Floor(spongeShieldPartialRechargeProgress);
				SpongeShieldDurability = Math.Min(SpongeShieldDurability + pointsActuallyRecharged, TheSponge.ShieldDurabilityMax);
				spongeShieldPartialRechargeProgress -= pointsActuallyRecharged;
				if (cooldowns.TryGetValue(SpongeDurability.ID, out var cdDurability2))
				{
					cdDurability2.timeLeft = SpongeShieldDurability;
				}
			}
			if (SpongeShieldDurability > 0 && !shieldAddedLight)
			{
				Vector2 center = base.Player.Center;
				val = Color.White;
				Lighting.AddLight(center, ((Color)(ref val)).ToVector3() * 0.75f);
				shieldAddedLight = true;
			}
		}
		if (!pSoulArtifact)
		{
			if (cooldowns.TryGetValue(ProfanedSoulShield.ID, out var cdDurability3))
			{
				cdDurability3.timeLeft = 0;
			}
			pSoulShieldDurability = 0;
		}
		else
		{
			ProfanedSoulCrystal.DetermineTransformationEligibility(base.Player);
			int maxDurability = (profanedCrystalBuffs ? ProfanedSoulCrystal.ShieldDurabilityMax : ProfanedSoulArtifact.ShieldDurabilityMax);
			int delay = (profanedCrystalBuffs ? ProfanedSoulCrystal.ShieldRechargeDelay : ProfanedSoulArtifact.ShieldRechargeDelay);
			int totalRecharge = (profanedCrystalBuffs ? ProfanedSoulCrystal.TotalShieldRechargeTime : ProfanedSoulArtifact.TotalShieldRechargeTime);
			if (pSoulShieldDurability == 0 && !cooldowns.ContainsKey(ProfanedSoulShieldRecharge.ID))
			{
				base.Player.AddCooldown(ProfanedSoulShieldRecharge.ID, delay);
			}
			if (pSoulShieldDurability > 0 && !cooldowns.ContainsKey(ProfanedSoulShield.ID))
			{
				base.Player.AddCooldown(ProfanedSoulShield.ID, maxDurability).timeLeft = pSoulShieldDurability;
			}
			if (pSoulShieldDurability > 0 && !cooldowns.ContainsKey(ProfanedSoulShieldRecharge.ID))
			{
				if (!playedProfanedSoulShieldSound)
				{
					SoundEngine.PlaySound(in Providence.BurnStartSound, base.Player.Center);
				}
				playedProfanedSoulShieldSound = true;
				pSoulShieldPartialRechargeProgress += (float)maxDurability / (float)totalRecharge;
				int pointsActuallyRecharged2 = (int)MathF.Floor(pSoulShieldPartialRechargeProgress);
				pSoulShieldDurability = Math.Min(pSoulShieldDurability + pointsActuallyRecharged2, maxDurability);
				pSoulShieldPartialRechargeProgress -= pointsActuallyRecharged2;
				if (cooldowns.TryGetValue(ProfanedSoulShield.ID, out var cdDurability4))
				{
					cdDurability4.timeLeft = pSoulShieldDurability;
				}
			}
			if (pSoulShieldDurability > 0 && !shieldAddedLight)
			{
				Vector2 center2 = base.Player.Center;
				val = Color.Orange;
				Lighting.AddLight(center2, ((Color)(ref val)).ToVector3() * 0.4f);
				shieldAddedLight = true;
			}
		}
		if (!lunicCorpsSet)
		{
			if (cooldowns.TryGetValue(global::CalamityMod.Cooldowns.LunicCorpsShieldDurability.ID, out var cdDurability5))
			{
				cdDurability5.timeLeft = 0;
			}
			LunicCorpsShieldDurability = 0;
		}
		else
		{
			if (LunicCorpsShieldDurability == 0 && !cooldowns.ContainsKey(LunicCorpsShieldRecharge.ID))
			{
				base.Player.AddCooldown(LunicCorpsShieldRecharge.ID, LunicCorpsHelmet.ShieldRechargeDelay);
			}
			if (LunicCorpsShieldDurability > 0 && !cooldowns.ContainsKey(global::CalamityMod.Cooldowns.LunicCorpsShieldDurability.ID))
			{
				base.Player.AddCooldown(global::CalamityMod.Cooldowns.LunicCorpsShieldDurability.ID, LunicCorpsHelmet.ShieldDurabilityMax).timeLeft = LunicCorpsShieldDurability;
			}
			if (LunicCorpsShieldDurability > 0 && !cooldowns.ContainsKey(LunicCorpsShieldRecharge.ID))
			{
				if (!playedLunicCorpsShieldSound)
				{
					SoundEngine.PlaySound(in LunicCorpsHelmet.ActivationSound, base.Player.Center);
				}
				playedLunicCorpsShieldSound = true;
				lunicCorpsShieldPartialRechargeProgress += (float)LunicCorpsHelmet.ShieldDurabilityMax / (float)LunicCorpsHelmet.TotalShieldRechargeTime;
				int pointsActuallyRecharged3 = (int)MathF.Floor(lunicCorpsShieldPartialRechargeProgress);
				LunicCorpsShieldDurability = Math.Min(LunicCorpsShieldDurability + pointsActuallyRecharged3, LunicCorpsHelmet.ShieldDurabilityMax);
				lunicCorpsShieldPartialRechargeProgress -= pointsActuallyRecharged3;
				if (cooldowns.TryGetValue(global::CalamityMod.Cooldowns.LunicCorpsShieldDurability.ID, out var cdDurability6))
				{
					cdDurability6.timeLeft = LunicCorpsShieldDurability;
				}
			}
			if (LunicCorpsShieldDurability > 0 && !shieldAddedLight)
			{
				Vector2 center3 = base.Player.Center;
				val = Color.DeepSkyBlue;
				Lighting.AddLight(center3, ((Color)(ref val)).ToVector3() * 0.2f);
				shieldAddedLight = true;
			}
		}
		if (!roverDrive)
		{
			if (cooldowns.TryGetValue(WulfrumRoverDriveDurability.ID, out var cdDurability7))
			{
				cdDurability7.timeLeft = 0;
			}
			RoverDriveShieldDurability = 0;
			return;
		}
		if (RoverDriveShieldDurability == 0 && !cooldowns.ContainsKey(WulfrumRoverDriveRecharge.ID))
		{
			base.Player.AddCooldown(WulfrumRoverDriveRecharge.ID, RoverDrive.ShieldRechargeDelay);
		}
		if (RoverDriveShieldDurability > 0 && !cooldowns.ContainsKey(WulfrumRoverDriveDurability.ID))
		{
			base.Player.AddCooldown(WulfrumRoverDriveDurability.ID, RoverDrive.ShieldDurabilityMax).timeLeft = RoverDriveShieldDurability;
		}
		if (RoverDriveShieldDurability > 0 && !cooldowns.ContainsKey(WulfrumRoverDriveRecharge.ID))
		{
			if (!playedRoverDriveShieldSound)
			{
				SoundEngine.PlaySound(in RoverDrive.ActivationSound, base.Player.Center);
			}
			playedRoverDriveShieldSound = true;
			roverDriveShieldPartialRechargeProgress += (float)RoverDrive.ShieldDurabilityMax / (float)RoverDrive.TotalShieldRechargeTime;
			int pointsActuallyRecharged4 = (int)MathF.Floor(roverDriveShieldPartialRechargeProgress);
			RoverDriveShieldDurability = Math.Min(RoverDriveShieldDurability + pointsActuallyRecharged4, RoverDrive.ShieldDurabilityMax);
			roverDriveShieldPartialRechargeProgress -= pointsActuallyRecharged4;
			if (cooldowns.TryGetValue(WulfrumRoverDriveDurability.ID, out var cdDurability8))
			{
				cdDurability8.timeLeft = RoverDriveShieldDurability;
			}
		}
		if (RoverDriveShieldDurability > 0 && !shieldAddedLight)
		{
			Vector2 center4 = base.Player.Center;
			val = Color.DeepSkyBlue;
			Lighting.AddLight(center4, ((Color)(ref val)).ToVector3() * 0.2f);
			shieldAddedLight = true;
		}
	}

	private void DefenseEffects()
	{
		if (totalDefenseDamage > 0)
		{
			if (!Main.getGoodWorld && totalDefenseDamage > (int)base.Player.statDefense)
			{
				totalDefenseDamage = base.Player.statDefense;
			}
			if (!base.Player.HasIFrames())
			{
				if (defenseDamageDelayFrames > 0)
				{
					defenseDamageDelayFrames--;
				}
				else if (defenseDamageDelayFrames <= 0)
				{
					defenseDamageRecoveryFrames--;
					if (defenseDamageRecoveryFrames <= 0)
					{
						totalDefenseDamage = 0;
						defenseDamageRecoveryFrames = 0;
						totalDefenseDamageRecoveryFrames = 60;
						defenseDamageDelayFrames = 0;
					}
				}
			}
			int currentDefenseDamage = CurrentDefenseDamage;
			if ((int)base.Player.statDefense > 0 && base.Player.endurance > 0f)
			{
				float drDamageRatio = (float)currentDefenseDamage / (float)(int)base.Player.statDefense;
				base.Player.endurance *= 1f - drDamageRatio;
			}
			base.Player.statDefense -= currentDefenseDamage;
		}
		if ((int)base.Player.statDefense < 0)
		{
			base.Player.statDefense *= 0f;
		}
		if (everclear && (int)base.Player.statDefense > 0)
		{
			base.Player.statDefense -= (int)(base.Player.statDefense * Everclear.DefenseLossPercent);
		}
		if (DesertProwlerHat.ShroudedInSmoke(base.Player, out var _))
		{
			base.Player.statDefense -= (int)(base.Player.statDefense * DesertProwlerHat.SmokeDefenseMult);
		}
	}

	private void Limits()
	{
		if (base.Player.endurance > 0f)
		{
			base.Player.endurance = 1f - 1f / (1f + base.Player.endurance);
		}
		if (areThereAnyDamnBosses && Main.netMode == 0 && base.Player.aggro < 0)
		{
			base.Player.aggro = 0;
		}
	}

	private void EnduranceReductions()
	{
		if (vHex)
		{
			base.Player.endurance -= 0.1f;
		}
		if (irradiated)
		{
			base.Player.endurance -= 0.1f;
		}
		if (corrEffigy)
		{
			base.Player.endurance -= CorruptionEffigy.DamageReductionLoss;
		}
	}

	private void HandleTextChatMessages()
	{
		if (base.Player.whoAmI != Main.myPlayer || Main.dedServ || startMessageDisplayDelay < 0)
		{
			return;
		}
		if (startMessageDisplayDelay == 0)
		{
			if (CalamityClientConfig.Instance.WikiStatusMessage)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Misc.WikiStatus1");
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Misc.WikiStatus2");
			}
			if (CalamityClientConfig.Instance.VCMMStatusMessage && !ExternalMods.VCMMAvailable)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Misc.VCMMStatus");
			}
		}
		startMessageDisplayDelay--;
	}

	public void CheckIfMouseItemIsSchematic()
	{
		if (Main.myPlayer != base.Player.whoAmI)
		{
			return;
		}
		bool shouldSync = false;
		if (Main.mouseItem != null && !Main.mouseItem.IsAir)
		{
			if (Main.mouseItem.type == ModContent.ItemType<EncryptedSchematicSunkenSea>() && !RecipeUnlockHandler.HasFoundSunkenSeaSchematic)
			{
				RecipeUnlockHandler.HasFoundSunkenSeaSchematic = true;
				shouldSync = true;
			}
			if (Main.mouseItem.type == ModContent.ItemType<EncryptedSchematicPlanetoid>() && !RecipeUnlockHandler.HasFoundPlanetoidSchematic)
			{
				RecipeUnlockHandler.HasFoundPlanetoidSchematic = true;
				shouldSync = true;
			}
			if (Main.mouseItem.type == ModContent.ItemType<EncryptedSchematicJungle>() && !RecipeUnlockHandler.HasFoundJungleSchematic)
			{
				RecipeUnlockHandler.HasFoundJungleSchematic = true;
				shouldSync = true;
			}
			if (Main.mouseItem.type == ModContent.ItemType<EncryptedSchematicHell>() && !RecipeUnlockHandler.HasFoundHellSchematic)
			{
				RecipeUnlockHandler.HasFoundHellSchematic = true;
				shouldSync = true;
			}
			if (Main.mouseItem.type == ModContent.ItemType<EncryptedSchematicIce>() && !RecipeUnlockHandler.HasFoundIceSchematic)
			{
				RecipeUnlockHandler.HasFoundIceSchematic = true;
				shouldSync = true;
			}
		}
		if (shouldSync)
		{
			CalamityNetcode.SyncWorld();
		}
	}

	public void AndroombaRightClick()
	{
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Player.whoAmI)
		{
			return;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.type != ModContent.NPCType<AndroombaFriendly>())
			{
				continue;
			}
			bool holdingsol = (base.Player.HeldItem.type >= 780 && base.Player.HeldItem.type <= 784) || (base.Player.HeldItem.type >= 5392 && base.Player.HeldItem.type <= 5394) || base.Player.HeldItem.type == ModContent.ItemType<AstralSolution>();
			int heldType = -1;
			for (int e = 0; e < AndroombaFriendly.customConversionTypes.Count; e++)
			{
				(int, string, Action<NPC>) entry = AndroombaFriendly.customConversionTypes[e];
				if (base.Player.HeldItem.type == entry.Item1)
				{
					holdingsol = true;
					heldType = e;
					break;
				}
			}
			Rectangle hitbox = npc.Hitbox;
			if ((((Rectangle)(ref hitbox)).Contains(Main.MouseWorld.ToPoint()) & holdingsol) && base.Player.Distance(npc.Center) < 450f)
			{
				base.Player.cursorItemIconEnabled = true;
				base.Player.cursorItemIconID = base.Player.HeldItem.type;
				base.Player.cursorItemIconText = "";
				npc.ShowNameOnHover = false;
				if (!Main.mouseRight || !Main.mouseRightRelease || !(base.Player.Distance(npc.Center) < 300f))
				{
					continue;
				}
				npc.netUpdate = true;
				int soltype = 0;
				soltype = ((base.Player.HeldItem.type == ModContent.ItemType<AstralSolution>()) ? 8 : (base.Player.HeldItem.type switch
				{
					780 => 0, 
					782 => 1, 
					781 => 2, 
					783 => 3, 
					784 => 4, 
					5392 => 5, 
					5393 => 6, 
					5394 => 7, 
					_ => heldType + 9, 
				}));
				if (npc.ai[3] == (float)soltype && npc.ai[0] != 0f)
				{
					continue;
				}
				base.Player.ConsumeItem(base.Player.HeldItem.type);
				SoundEngine.PlaySound(in SoundID.Item87);
				if (Main.netMode == 0)
				{
					AndroombaFriendly.SwapSolution(npc.whoAmI, soltype);
				}
				else
				{
					SyncAndroombaSolutionPacket.Send(npc.ModNPC<AndroombaFriendly>(), soltype);
				}
				if (npc.ai[0] == 0f)
				{
					if (Main.netMode == 0)
					{
						AndroombaFriendly.ChangeAI(npc.whoAmI, 1);
					}
					else
					{
						SyncAndroombaAIPacket.Send(npc.ModNPC<AndroombaFriendly>(), 1);
					}
				}
				if (Main.dedServ)
				{
					NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
				}
			}
			else
			{
				npc.ShowNameOnHover = true;
			}
		}
	}

	private void HandlePotions()
	{
		if (!PlayerInput.Triggers.JustPressed.QuickBuff)
		{
			return;
		}
		for (int i = 0; i < 58; i++)
		{
			Item item = base.Player.inventory[i];
			if (base.Player.potionDelay <= 0)
			{
				if (item != null && item.stack > 0 && item.type == ModContent.ItemType<HadalStew>())
				{
					CalamityUtils.ConsumeItemViaQuickBuff(base.Player, item, HadalStew.BuffType, HadalStew.BuffDuration, reducedPotionSickness: true);
				}
				continue;
			}
			break;
		}
	}

	private void EnterWorldSync()
	{
		StandardSync();
	}

	internal void StandardSync()
	{
		RageSyncPacket.Send(this);
		AdrenalineSyncPacket.Send(this);
		DefenseDamageSyncPacket.Send(this);
	}

	internal void MousePositionSync()
	{
		MousePositionSyncPacket.Send(this);
	}

	internal void MouseRotationSync()
	{
		MouseRotationSyncPacket.Send(this);
	}

	internal void MouseRightClickSync()
	{
		RightClickSyncPacket.Send(this);
	}

	public void SyncCooldownAddition(bool server, CooldownInstance cd)
	{
		if (Main.netMode != 0)
		{
			CooldownAdditionPacket.Send(this, cd);
		}
	}

	public void SyncCooldownRemoval(bool server, IList<string> cooldownIDs)
	{
		if (Main.netMode != 0)
		{
			CooldownRemovalPacket.Send(this, cooldownIDs.Select((string id) => CooldownRegistry.Get(id).netID).ToArray());
		}
	}

	public void SyncCooldownDictionary(bool server)
	{
		if (Main.netMode != 0)
		{
			SyncCooldownDictionaryPacket.Send(this);
		}
	}

	public override void OnHitAnything(float x, float y, Entity victim)
	{
		rageCombatFrames = BalancingConstants.RageCombatDelayTime;
		if (AdamantiteSet)
		{
			adamantiteSetDefenseBoostInterpolant += 1f / 120f;
			adamantiteSetDefenseBoostInterpolant = MathHelper.Clamp(adamantiteSetDefenseBoostInterpolant, 0f, 1f);
			AdamantiteSetDecayDelay = 60;
		}
	}

	public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (item.CountsAsClass<MeleeDamageClass>())
		{
			GemTechState.MeleeOnHitEffects(target);
		}
		MythrilArmorSetChange.OnHitEffects(target, damageDone, base.Player);
		if (witheringWeaponEnchant)
		{
			witheringDamageDone += (int)((double)damageDone * (hit.Crit ? 2.0 : 1.0));
		}
		if (flamingItemEnchant)
		{
			target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 120);
		}
		target.Calamity().IncreasedColdEffects_EskimoSet = eskimoSet;
		target.Calamity().IncreasedColdEffects_CryoStone = CryoStone;
		target.Calamity().IncreasedElectricityEffects_Unused = false;
		target.Calamity().IncreasedHeatEffects_Fireball = fireball;
		target.Calamity().IncreasedHeatEffects_CinnamonRoll = cinnamonRoll;
		target.Calamity().IncreasedHeatEffects_FireBoots = bootLevel;
		target.Calamity().IncreasedSicknessEffects_ToxicHeart = toxicHeart;
		target.Calamity().IncreasedWaterEffects_Amulet1 = sSpiritAmulet;
		target.Calamity().IncreasedWaterEffects_Amulet2 = dOfTheDeep;
		target.Calamity().IncreasedSicknessAndWaterEffects_EvergreenGin = evergreenGin;
		target.Calamity().IncreasedSicknessAndWaterEffects_CorrosiveSpine = corrosiveSpine;
		target.Calamity().IncreasedDebuffEffects_Amalgam = amalgam;
		switch (item.type)
		{
		case 2880:
			target.AddBuff(144, 300);
			break;
		case 190:
		case 1123:
			target.AddBuff(20, 240);
			break;
		case 121:
			target.AddBuff(323, 90);
			break;
		case 676:
		case 1306:
			target.AddBuff(324, 300);
			break;
		case 724:
			target.AddBuff(44, 120);
			break;
		}
		if (flameWakerBoots)
		{
			target.AddBuff(24, 120);
		}
		if (hellfireTreads)
		{
			if (Main.rand.NextBool(4))
			{
				target.AddBuff(323, 360);
			}
			else if (Main.rand.NextBool())
			{
				target.AddBuff(323, 240);
			}
			else
			{
				target.AddBuff(323, 120);
			}
		}
		bool targetIsDummy = target.type == 488 || target.type == ModContent.NPCType<SuperDummyNPC>();
		ItemLifesteal(target, item, damageDone);
		ItemOnHit(item, damageDone, target, hit.Crit, target.IsAnEnemy(allowStatues: false), targetIsDummy);
		NPCDebuffs(target, item.CountsAsClass<MeleeDamageClass>(), item.CountsAsClass<RangedDamageClass>(), item.CountsAsClass<MagicDamageClass>(), item.CountsAsClass<SummonDamageClass>(), item.CountsAsClass<ThrowingDamageClass>(), item.CountsAsClass<SummonMeleeSpeedDamageClass>(), hit.Crit);
		if (ursaSergeant && target.life <= 0 && target.realLife == -1)
		{
			ursaSergeantCooldown = MathHelper.Clamp(ursaSergeantCooldown - 180, 0, 300);
		}
		if (generalBandCooldown == 0)
		{
			int cooldown = 0;
			if (bGlassBand)
			{
				int damage = (int)base.Player.GetBestClassDamage().ApplyTo(BlackGlassBand.damage);
				Vector2 launchVel = base.Player.Center.DirectionTo(target.Center) * 6f;
				Projectile.NewProjectile(source, target.Center, Vector2.Zero, ModContent.ProjectileType<BlackGlassBandProjectile>(), damage, -1f, base.Player.whoAmI, target.whoAmI, launchVel.X, launchVel.Y);
				if (cooldown < BlackGlassBand.cooldown)
				{
					cooldown = BlackGlassBand.cooldown;
				}
			}
			if (protolithBangle && item.DamageType == DamageClass.Ranged)
			{
				int damage2 = (int)base.Player.GetBestClassDamage().ApplyTo(ProtolithBangle.damage);
				Projectile.NewProjectileDirect(source, target.Center, Vector2.Zero, ModContent.ProjectileType<ProtolithBangleProjectile>(), damage2, -1f, base.Player.whoAmI, target.whoAmI).DamageType = DamageClass.Ranged;
				if (cooldown < ProtolithBangle.cooldown)
				{
					cooldown = ProtolithBangle.cooldown;
				}
			}
			if (batholithBangle && item.DamageType == DamageClass.Magic)
			{
				int damage3 = (int)base.Player.GetBestClassDamage().ApplyTo(BatholithBangle.damage);
				Projectile.NewProjectileDirect(source, target.Center, Vector2.Zero, ModContent.ProjectileType<BatholithBangleProjectile>(), damage3, -1f, base.Player.whoAmI, target.whoAmI).DamageType = DamageClass.Magic;
				if (cooldown < BatholithBangle.cooldown)
				{
					cooldown = BatholithBangle.cooldown;
				}
			}
			if (cooldown > 0)
			{
				generalBandCooldown = cooldown;
				base.Player.AddCooldown(GenericBandCooldown.ID, cooldown);
			}
		}
		if (luxorsGift)
		{
			luxorHit = true;
		}
		if (transformer && base.Player.Calamity().transformerCooldown == 0 && target.life <= 0 && target.realLife == -1)
		{
			base.Player.Calamity().transformerStoredKills += ((!Main.zenithWorld) ? 2 : 10);
		}
		bool spawnChance = Main.rand.Next(100) < ArcFlashRing.LightningSpawnPercent;
		if (arcFlashRing & spawnChance)
		{
			int damage4 = (int)((float)hit.Damage * ArcFlashRing.LightningDamageMult * (hit.Crit ? (1f / (base.Player.Calamity().critDamage + 2f)) : 1f) / (base.Player.Calamity().adrenalineModeActive ? (base.Player.Calamity().GetAdrenalineDamage() + 1f) : 1f));
			Projectile.NewProjectileDirect(source, target.Center, Vector2.Zero, ModContent.ProjectileType<FlashBolt>(), damage4, 0f, base.Player.whoAmI, target.whoAmI).DamageType = hit.DamageType;
		}
		if (!targetIsDummy && rageModeActive && shatteredCommunity)
		{
			base.Player.GetModPlayer<ShatteredCommunityPlayer>().AccumulateRageDamage(damageDone);
		}
	}

	public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e15: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_130f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_1317: Unknown result type (might be due to invalid IL or missing references)
		//IL_131c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1323: Unknown result type (might be due to invalid IL or missing references)
		//IL_1328: Unknown result type (might be due to invalid IL or missing references)
		//IL_132d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1343: Unknown result type (might be due to invalid IL or missing references)
		//IL_135f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_141f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1424: Unknown result type (might be due to invalid IL or missing references)
		//IL_1427: Unknown result type (might be due to invalid IL or missing references)
		//IL_1435: Unknown result type (might be due to invalid IL or missing references)
		//IL_144e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1453: Unknown result type (might be due to invalid IL or missing references)
		//IL_1456: Unknown result type (might be due to invalid IL or missing references)
		//IL_145b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1462: Unknown result type (might be due to invalid IL or missing references)
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_146c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1482: Unknown result type (might be due to invalid IL or missing references)
		//IL_149e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1418: Unknown result type (might be due to invalid IL or missing references)
		//IL_1411: Unknown result type (might be due to invalid IL or missing references)
		//IL_1015: Unknown result type (might be due to invalid IL or missing references)
		//IL_1020: Unknown result type (might be due to invalid IL or missing references)
		//IL_1026: Unknown result type (might be due to invalid IL or missing references)
		//IL_1028: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_103e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1040: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_1047: Unknown result type (might be due to invalid IL or missing references)
		//IL_1049: Unknown result type (might be due to invalid IL or missing references)
		//IL_1050: Unknown result type (might be due to invalid IL or missing references)
		//IL_1055: Unknown result type (might be due to invalid IL or missing references)
		//IL_105a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1061: Unknown result type (might be due to invalid IL or missing references)
		//IL_106e: Unknown result type (might be due to invalid IL or missing references)
		//IL_107a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1101: Unknown result type (might be due to invalid IL or missing references)
		//IL_1103: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_110c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1111: Unknown result type (might be due to invalid IL or missing references)
		//IL_1116: Unknown result type (might be due to invalid IL or missing references)
		//IL_111d: Unknown result type (might be due to invalid IL or missing references)
		//IL_112a: Unknown result type (might be due to invalid IL or missing references)
		//IL_118d: Unknown result type (might be due to invalid IL or missing references)
		//IL_119e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1205: Unknown result type (might be due to invalid IL or missing references)
		//IL_1207: Unknown result type (might be due to invalid IL or missing references)
		//IL_123d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1242: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12be: Unknown result type (might be due to invalid IL or missing references)
		if (base.Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = base.Player.GetSource_OnHit(target);
		CalamityGlobalNPC cgn = target.Calamity();
		if (proj.CountsAsClass<MeleeDamageClass>())
		{
			GemTechState.MeleeOnHitEffects(target);
		}
		if (proj.CountsAsClass<RangedDamageClass>() && proj.type != ModContent.ProjectileType<GemTechGreenFlechette>())
		{
			GemTechState.RangedOnHitEffects(target, proj.damage);
		}
		if (proj.type != ModContent.ProjectileType<MythrilFlare>())
		{
			MythrilArmorSetChange.OnHitEffects(target, damageDone, base.Player);
		}
		if (moscowMule)
		{
			Vector2 center = base.Player.Center;
			if (proj.IsMinionOrSentryRelated)
			{
				center = proj.Center;
			}
			Vector2 launchVel = center.DirectionTo(target.Center);
			target.MoveNPC(launchVel, proj.knockBack, ignoreKBImmune: true);
		}
		if ((moscowMule || bloodyMary) && !PierceResistNPC.exemptProjectiles.Contains(proj.type) && (!PierceResistNPC.singleHitboxExemptProjectiles.ContainsKey(proj.type) || !PierceResistNPC.singleHitboxExemptProjectiles[proj.type]))
		{
			proj.damage = (int)((float)proj.damage * (moscowMule ? 0.8f : 1f) * (bloodyMary ? 0.75f : 1f));
		}
		if (witheringWeaponEnchant)
		{
			witheringDamageDone += (int)((double)damageDone * (hit.Crit ? 2.0 : 1.0));
		}
		cgn.TypelessDebuffMultiplier = TypelessDebuffMultiplier;
		cgn.HeatDebuffMultiplier = HeatDebuffMultiplier;
		cgn.ColdDebuffMultiplier = ColdDebuffMultiplier;
		cgn.SicknessDebuffMultiplier = SicknessDebuffMultiplier;
		cgn.WaterDebuffMultiplier = WaterDebuffMultiplier;
		cgn.ElectricDebuffMultiplier = ElectricDebuffMultiplier;
		switch (proj.type)
		{
		case 367:
			target.AddBuff(323, 180);
			break;
		case 433:
		case 443:
		case 451:
			target.AddBuff(144, 180);
			break;
		case 730:
		case 731:
		case 732:
			target.AddBuff(ModContent.BuffType<StaticDischarge>(), 90);
			break;
		case 262:
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
			break;
		case 190:
		case 509:
			target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
			break;
		case 645:
			target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
			break;
		case 545:
		case 948:
			target.AddBuff(24, 60);
			break;
		case 181:
		case 566:
		case 976:
			target.AddBuff(20, 120);
			break;
		case 189:
			target.AddBuff(70, 60);
			break;
		case 117:
			target.AddBuff(ModContent.BuffType<Crumbling>(), 300);
			break;
		case 342:
			target.AddBuff(44, 300);
			break;
		case 120:
		case 263:
		case 343:
			target.AddBuff(324, 180);
			break;
		case 337:
		case 344:
			target.AddBuff(324, 120);
			break;
		case 118:
		case 520:
			target.AddBuff(44, 60);
			break;
		}
		if (flameWakerBoots)
		{
			target.AddBuff(24, 120);
		}
		if ((proj.arrow && base.Player.hasMoltenQuiver) || hellfireTreads)
		{
			if (Main.rand.NextBool(4))
			{
				target.AddBuff(323, 360);
			}
			else if (Main.rand.NextBool())
			{
				target.AddBuff(323, 240);
			}
			else
			{
				target.AddBuff(323, 120);
			}
		}
		if (ursaSergeant && target.life <= 0 && target.realLife == -1)
		{
			ursaSergeantCooldown = MathHelper.Clamp(ursaSergeantCooldown - UrsaSergeant.CooldownReducedPerKill, 0, UrsaSergeant.MaxCooldown);
		}
		if (generalBandCooldown == 0)
		{
			int cooldown = 0;
			if (bGlassBand)
			{
				int damage = (int)base.Player.GetBestClassDamage().ApplyTo(BlackGlassBand.damage);
				Vector2 launchVel2 = base.Player.Center.DirectionTo(target.Center) * 6f;
				Projectile.NewProjectile(source, target.Center, Vector2.Zero, ModContent.ProjectileType<BlackGlassBandProjectile>(), damage, -1f, base.Player.whoAmI, target.whoAmI, launchVel2.X, launchVel2.Y);
				if (cooldown < BlackGlassBand.cooldown)
				{
					cooldown = BlackGlassBand.cooldown;
				}
			}
			if (protolithBangle && proj.DamageType == DamageClass.Ranged)
			{
				int damage2 = (int)base.Player.GetBestClassDamage().ApplyTo(ProtolithBangle.damage);
				Projectile.NewProjectileDirect(source, target.Center, Vector2.Zero, ModContent.ProjectileType<ProtolithBangleProjectile>(), damage2, -1f, base.Player.whoAmI, target.whoAmI).DamageType = DamageClass.Ranged;
				if (cooldown < ProtolithBangle.cooldown)
				{
					cooldown = ProtolithBangle.cooldown;
				}
			}
			if (batholithBangle && proj.DamageType == DamageClass.Magic)
			{
				int damage3 = (int)base.Player.GetBestClassDamage().ApplyTo(BatholithBangle.damage);
				Projectile.NewProjectileDirect(source, target.Center, Vector2.Zero, ModContent.ProjectileType<BatholithBangleProjectile>(), damage3, -1f, base.Player.whoAmI, target.whoAmI).DamageType = DamageClass.Magic;
				if (cooldown < BatholithBangle.cooldown)
				{
					cooldown = BatholithBangle.cooldown;
				}
			}
			if (cooldown > 0)
			{
				generalBandCooldown = cooldown;
				base.Player.AddCooldown(GenericBandCooldown.ID, cooldown);
			}
		}
		if (luxorsGift && proj.type != ModContent.ProjectileType<LuxorsGiftMelee>() && proj.type != ModContent.ProjectileType<LuxorsGiftRanged>() && proj.type != ModContent.ProjectileType<LuxorsGiftMagic>() && proj.type != ModContent.ProjectileType<LuxorsGiftSummon>() && proj.type != ModContent.ProjectileType<LuxorsGiftRogue>() && proj.type != ModContent.ProjectileType<LuxorsGiftClassless>())
		{
			luxorHit = true;
		}
		if (transformer && base.Player.Calamity().transformerCooldown == 0 && target.life <= 0 && target.realLife == -1)
		{
			base.Player.Calamity().transformerStoredKills += ((!Main.zenithWorld) ? 2 : 10);
		}
		CalamityGlobalProjectile globalProj = proj.Calamity();
		bool spawnChance = Main.rand.Next(0, 100) < ArcFlashRing.LightningSpawnPercent;
		if ((arcFlashRing & spawnChance) && proj.type != ModContent.ProjectileType<FlashBolt>())
		{
			proj.active = true;
			int damage4 = (int)((float)hit.Damage * ArcFlashRing.LightningDamageMult * (hit.Crit ? (1f / (base.Player.Calamity().critDamage + 2f)) : 1f) / (base.Player.Calamity().adrenalineModeActive ? (base.Player.Calamity().GetAdrenalineDamage() + 1f) : 1f));
			Projectile.NewProjectileDirect(source, target.Center, Vector2.Zero, ModContent.ProjectileType<FlashBolt>(), damage4, 0f, base.Player.whoAmI, target.whoAmI, (!globalProj.showArcFlash) ? 1 : 0).DamageType = hit.DamageType;
			globalProj.showArcFlash = false;
			globalProj.arcFlashCooldown = 30;
		}
		if (forbiddenCirclet && globalProj.stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<ForbiddenStealthSummonTagBuff>(), ForbiddenCirclet.TagDuration);
		}
		if (proj.npcProj || proj.trap || !proj.friendly)
		{
			return;
		}
		if (plaguebringerPatronSet && CalamityProjectileSets.IsFriendlyBeeProjectile[proj.type])
		{
			target.AddBuff(ModContent.BuffType<Plague>(), PlaguebringerVisor.BeePlagueDuration);
		}
		CalamityGlobalProjectile cgp = proj.Calamity();
		if (cgp.appliesSomaShred)
		{
			bool actuallyApplyShred = false;
			if (Main.rand.NextFloat() < 0.05f)
			{
				actuallyApplyShred = true;
			}
			if (hit.Crit && Main.rand.NextFloat() < 0.3f)
			{
				actuallyApplyShred = true;
			}
			if (actuallyApplyShred)
			{
				target.AddBuff(ModContent.BuffType<Shred>(), 320);
				cgn.somaShredApplicator = base.Player.whoAmI;
				cgp.appliesSomaShred = false;
			}
		}
		if (cgp.brimstoneBullets)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 90);
			if (Main.zenithWorld)
			{
				GungeonMusicSystem.GUN();
			}
		}
		if (cgp.fireBullet)
		{
			target.AddBuff(323, 60);
			if (proj.numHits == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					GeneralParticleHandler.SpawnParticle(new CritSpark(proj.Center, proj.velocity.RotatedByRandom(0.4) * Main.rand.NextFloat(0.8f, 1.5f), Main.rand.NextBool() ? Color.Orange : Color.OrangeRed, Color.Yellow, Main.rand.NextFloat(0.4f, 0.6f), 15, Main.rand.NextFloat(-2f, 2f), 1.5f));
				}
				SoundStyle style = SoundID.DD2_BetsyFireballImpact with
				{
					Volume = 0.3f,
					Pitch = 1f
				};
				SoundEngine.PlaySound(in style, proj.Center);
			}
		}
		if (cgp.iceBullet)
		{
			target.AddBuff(324, 60);
			if (proj.numHits == 0)
			{
				for (int j = 0; j < 3; j++)
				{
					GeneralParticleHandler.SpawnParticle(new CritSpark(proj.Center, proj.velocity.RotatedByRandom(0.4) * Main.rand.NextFloat(0.8f, 1.5f), Main.rand.NextBool() ? Color.DeepSkyBlue : Color.LightSkyBlue, Color.White, Main.rand.NextFloat(0.4f, 0.6f), 15, Main.rand.NextFloat(-2f, 2f), 1.5f));
				}
				SoundStyle style = SoundID.Item27 with
				{
					Volume = 0.3f,
					Pitch = 0.8f
				};
				SoundEngine.PlaySound(in style, proj.Center);
			}
		}
		if (cgp.shockBullet)
		{
			target.AddBuff(144, 180);
			if (proj.numHits == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(proj.Center, Vector2.Zero, Color.Turquoise, "CalamityMod/Particles/PlasmaExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-2f, 2f), 0.005f, Main.rand.NextFloat(0.048f, 0.055f), 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				int points = 6;
				float radians = (float)Math.PI * 2f / (float)points;
				Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
				float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
				for (int k = 0; k < points; k++)
				{
					Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.45f * rotRando);
					GeneralParticleHandler.SpawnParticle(new SparkParticle(proj.Center + velocity * 4.5f, velocity * 8f, affectedByGravity: false, 13, 0.85f, Color.Turquoise));
				}
				for (int l = 0; l <= 12; l++)
				{
					Dust dust = Dust.NewDustPerfect(proj.Center, 278, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.1f, 2.9f));
					dust.noGravity = false;
					dust.scale = Main.rand.NextFloat(0.3f, 0.9f);
					dust.color = Color.Turquoise;
				}
				int onHitDamage = base.Player.CalcIntDamage<RangedDamageClass>(0.2f * (float)proj.damage);
				Projectile.NewProjectileDirect(proj.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage, 0f, base.Player.whoAmI, target.whoAmI).DamageType = proj.DamageType;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ElectricHit");
				style.Volume = 0.2f;
				style.Pitch = 0.7f;
				style.PitchVariance = 0.2f;
				SoundEngine.PlaySound(in style, proj.Center);
			}
		}
		if ((cgp.pearlBullet1 || cgp.pearlBullet2 || cgp.pearlBullet3) && proj.numHits == 0)
		{
			Color color = (cgp.pearlBullet1 ? Color.LightBlue : (cgp.pearlBullet2 ? Color.LightPink : Color.Khaki));
			Vector2 spinningPoint2 = Vector2.Normalize(new Vector2(-1f, -1f));
			float radians2 = (float)Math.PI * 2f / 3f;
			Vector2 Position = target.Center + spinningPoint2.RotatedBy(radians2 * (float)((!cgp.pearlBullet1) ? (cgp.pearlBullet2 ? 1 : 2) : 0)).RotatedBy(-0.44999998807907104) * 55f;
			int bulletType = ((!cgp.pearlBullet1) ? (cgp.pearlBullet2 ? 1 : 2) : 0);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/HighResHollowCircleHardEdge", new Vector2(1f, 1f), Main.rand.NextFloat(-2f, 2f), 0.005f, 0.035f + 0.018f * (float)bulletType, 14 + bulletType, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(Position, Vector2.Zero, color, "CalamityMod/Particles/HighResFoggyCircleHardEdge", new Vector2(1f, 1f), Main.rand.NextFloat(-2f, 2f), 0.005f, 0.06f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			int points2 = 6;
			radians2 = (float)Math.PI * 2f / (float)points2;
			spinningPoint2 = Vector2.Normalize(new Vector2(-1f, -1f));
			float rotRando2 = Main.rand.NextFloat(0.1f, 2.5f);
			for (int m = 0; m < points2; m++)
			{
				Vector2 velocity2 = spinningPoint2.RotatedBy(radians2 * (float)m).RotatedBy(-0.45f * rotRando2);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(Position + velocity2 * 10f, velocity2 * 15f, affectedByGravity: false, 12, 0.03f, color, new Vector2(1.35f, 0.5f), quickShrink: true));
			}
			int pearls = MathHelper.Clamp(7 - (int)((float)proj.numHits * 0.5f), 2, 7);
			for (int n = 0; n < pearls; n++)
			{
				Vector2 velocity3 = Utils.RotatedByRandom(new Vector2(1f, 1f), 100.0) * Main.rand.NextFloat(0.7f, 1.2f);
				GeneralParticleHandler.SpawnParticle(new PearlParticle(Position + velocity3 * 11f, velocity3 * 10f, affectedByGravity: true, 50, 0.85f, color, 0.95f, Main.rand.NextFloat(2f, -2f), hitTiles: true));
			}
			int dusts = MathHelper.Clamp(10 - (int)((float)proj.numHits * 0.5f), 2, 10);
			for (int num = 0; num <= dusts; num++)
			{
				Dust dust2 = Dust.NewDustPerfect(Position, 278, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.1f, 2.9f));
				dust2.noGravity = false;
				dust2.scale = Main.rand.NextFloat(0.3f, 0.8f);
				dust2.color = color;
			}
			int onHitDamage2 = base.Player.CalcIntDamage<RangedDamageClass>(0.2f * (float)proj.damage);
			Projectile.NewProjectileDirect(proj.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage2, 0f, base.Player.whoAmI, target.whoAmI).DamageType = proj.DamageType;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HadalUrnClose");
			style.Volume = 0.4f;
			style.Pitch = 0.4f;
			style.PitchVariance = 0.2f;
			SoundEngine.PlaySound(in style, proj.Center);
		}
		if (cgp.lifeBullet && proj.numHits == 0)
		{
			int points3 = 10;
			for (int num2 = 0; num2 < points3; num2++)
			{
				Vector2 velocity4 = proj.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.3f, 0.8f);
				GeneralParticleHandler.SpawnParticle(new LineParticle(proj.Center + velocity4 * 1.5f, velocity4 * Main.rand.NextFloat(1f, 2f), affectedByGravity: false, 18, Main.rand.NextFloat(0.4f, 0.7f), Color.White * 0.85f));
			}
			Main.player[proj.owner].SpawnLifeStealProjectile(target, proj, ModContent.ProjectileType<AltTransfusionTrail>(), (int)Math.Round((double)hit.Damage * 0.035));
		}
		if ((cgp.betterLifeBullet1 || cgp.betterLifeBullet2) && proj.numHits == 0)
		{
			int points4 = 12;
			for (int num3 = 0; num3 < points4; num3++)
			{
				Color color2 = (Color)(Main.rand.Next(1, 4) switch
				{
					2 => Color.LightPink, 
					1 => Color.LightBlue, 
					_ => Color.Khaki, 
				});
				Vector2 velocity5 = proj.velocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 0.8f);
				GeneralParticleHandler.SpawnParticle(new LineParticle(proj.Center + velocity5 * 1.5f, velocity5 * Main.rand.NextFloat(1f, 3f), affectedByGravity: false, 18, Main.rand.NextFloat(0.4f, 0.7f), color2));
			}
			for (int num4 = 0; num4 <= 2; num4++)
			{
				Main.player[proj.owner].SpawnLifeStealProjectile(target, proj, ModContent.ProjectileType<AltTransfusionTrail>(), (int)Math.Round((double)hit.Damage * 0.01), 0.75f);
			}
		}
		bool targetIsDummy = target.type == 488 || target.type == ModContent.NPCType<SuperDummyNPC>();
		ProjLifesteal(target, proj, damageDone, hit.Crit);
		ProjOnHit(proj, target, hit.Crit, target.IsAnEnemy(allowStatues: false), targetIsDummy);
		NPCDebuffs(target, proj.CountsAsClass<MeleeDamageClass>(), proj.CountsAsClass<RangedDamageClass>(), proj.CountsAsClass<MagicDamageClass>(), proj.CountsAsClass<SummonDamageClass>(), proj.CountsAsClass<ThrowingDamageClass>(), proj.CountsAsClass<SummonMeleeSpeedDamageClass>(), hit.Crit, proj: true, proj.noEnchantments);
		if (!targetIsDummy && rageModeActive && shatteredCommunity)
		{
			base.Player.GetModPlayer<ShatteredCommunityPlayer>().AccumulateRageDamage(damageDone);
		}
	}

	public void ItemOnHit(Item item, int damage, NPC target, bool crit, bool npcCheck, bool targetIsDummy)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = target.Center;
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (!item.CountsAsClass<MeleeDamageClass>() && base.Player.meleeEnchant == 7)
		{
			Projectile.NewProjectile(source, position, base.Player.velocity, 289, 0, 0f, base.Player.whoAmI);
		}
		if (npcCheck)
		{
			if (item.CountsAsClass<MeleeDamageClass>() && hideOfDeus && hideOfDeusTimer == 0)
			{
				hideOfDeusTimer = 10;
				int bulwarkStarDamage = (int)base.Player.GetTotalDamage<MeleeDamageClass>().ApplyTo(HideofAstrumDeus.StarDamage);
				for (int n = 0; n < 3; n++)
				{
					CalamityUtils.ProjectileRain(source, base.Player.Center, 400f, 100f, 500f, 800f, 29f, ModContent.ProjectileType<AstralStar>(), bulwarkStarDamage, 5f, base.Player.whoAmI);
				}
			}
			if ((astralStarRain & crit) && astralStarRainCooldown <= 0)
			{
				astralStarRainCooldown = AstralHelm.StarRainCooldown;
				for (int i = 0; i < 3; i++)
				{
					UnifiedRandom rand = Main.rand;
					int[] obj = new int[4] { 0, 724, 726, 955 };
					obj[0] = ModContent.ProjectileType<AstralStar>();
					int projectileType = Utils.SelectRandom(rand, obj);
					int astralStarDamage = (int)base.Player.GetBestClassDamage().ApplyTo(AstralHelm.StarDamage);
					Projectile star = CalamityUtils.ProjectileRain(source, position, 400f, 100f, 500f, 800f, 12f, projectileType, astralStarDamage, 5f, base.Player.whoAmI);
					if (star.whoAmI.WithinBounds(Main.maxProjectiles))
					{
						star.DamageType = DamageClass.Generic;
					}
				}
			}
		}
		if (item.CountsAsClass<MeleeDamageClass>() && npcCheck)
		{
			if (ataxiaGeyser && base.Player.ownedProjectileCounts[ModContent.ProjectileType<ChaoticGeyser>()] < HydrothermicHeadMelee.GeyserCountLimit)
			{
				int geyserDamage = CalamityUtils.DamageSoftCap((double)damage * HydrothermicHeadMelee.GeyserDamageRatio, HydrothermicHeadMelee.GeyserDamageSoftcap);
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ChaoticGeyser>(), geyserDamage, 2f, base.Player.whoAmI);
			}
			if (bloodflareMelee && item.CountsAsClass<MeleeDamageClass>() && bloodflareMeleeHits < BloodflareHeadMelee.HitsToActivateFrenzy && !bloodflareFrenzy && !base.Player.HasCooldown(BloodflareFrenzy.ID))
			{
				bloodflareMeleeHits++;
			}
		}
	}

	public void ProjOnHit(Projectile proj, NPC target, bool crit, bool npcCheck, bool targetIsDummy)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalProjectile modProj = proj.Calamity();
		Vector2 position = target.Center;
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (proj.CountsAsClass<MeleeDamageClass>() || proj.CountsAsClass<RangedDamageClass>() || proj.CountsAsClass<MagicDamageClass>() || proj.CountsAsClass<SummonDamageClass>())
		{
			_ = 1;
		}
		else
			proj.CountsAsClass<ThrowingDamageClass>();
		if (!proj.CountsAsClass<MeleeDamageClass>() && !proj.CountsAsClass<SummonMeleeSpeedDamageClass>() && base.Player.meleeEnchant == 7)
		{
			Projectile.NewProjectile(source, position, proj.velocity, 289, 0, 0f, proj.owner);
		}
		if (alchFlask && AlchFlaskCooldown == 0 && proj.type != ModContent.ProjectileType<BasicPlagueBee>())
		{
			int seekerDamage = (int)base.Player.GetBestClassDamage().ApplyTo(10f);
			Vector2 seekerVelocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.5f, 1.2f);
			Projectile projectile = Projectile.NewProjectileDirect(source, position, seekerVelocity, ModContent.ProjectileType<BasicPlagueBee>(), seekerDamage, 0f, base.Player.whoAmI, -20f, 30f, 2f);
			projectile.ArmorPenetration = 20;
			projectile.penetrate = 2;
			projectile.extraUpdates = 1;
			AlchFlaskCooldown = (base.Player.strongBees ? 6 : 7);
		}
		bool lifeAndShieldCondition = base.Player.statLife >= base.Player.statLifeMax2 && (!HasAnyEnergyShield || TotalEnergyShielding >= TotalMaxShieldDurability);
		if (theBee & lifeAndShieldCondition)
		{
			SoundEngine.PlaySound(in SoundID.Item110, proj.Center);
		}
		if (npcCheck && (astralStarRain & crit) && astralStarRainCooldown <= 0)
		{
			astralStarRainCooldown = AstralHelm.StarRainCooldown;
			for (int n = 0; n < 3; n++)
			{
				UnifiedRandom rand = Main.rand;
				int[] obj = new int[4] { 0, 724, 726, 955 };
				obj[0] = ModContent.ProjectileType<AstralStar>();
				int projectileType = Utils.SelectRandom(rand, obj);
				int astralStarDamage = (int)base.Player.GetBestClassDamage().ApplyTo(AstralHelm.StarDamage);
				Projectile star = CalamityUtils.ProjectileRain(source, position, 400f, 100f, 500f, 800f, 25f, projectileType, astralStarDamage, 5f, base.Player.whoAmI);
				if (star.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					star.DamageType = DamageClass.Generic;
				}
			}
		}
		if ((abaddon & crit) && AbaddonCooldown <= 0 && !voidOfExtinction)
		{
			AbaddonCooldown = 15;
			int AbaddonExploDamage = (int)base.Player.GetBestClassDamage().ApplyTo(Abaddon.AbaddonExploDamage);
			Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<AbaddonCrit>(), AbaddonExploDamage, 0f, base.Player.whoAmI);
		}
		if ((voidOfExtinction & crit) && VoidCooldown <= 0)
		{
			VoidCooldown = 15;
			int VoidExploDamage = (int)base.Player.GetBestClassDamage().ApplyTo(VoidofExtinction.VoidExploDamage);
			Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<VoidofExtinctionCrit>(), VoidExploDamage, 0f, base.Player.whoAmI);
		}
		if (ursaSergeant && ursaSergeantCooldown <= 0)
		{
			ursaSergeantCooldown = UrsaSergeant.MaxCooldown;
			int ursaSlashdamage = (int)base.Player.GetBestClassDamage().ApplyTo(UrsaSergeant.BaseSwipeDamage);
			Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<UrsaSlash>(), ursaSlashdamage, 0f, base.Player.whoAmI);
		}
		if (proj.CountsAsClass<MeleeDamageClass>())
		{
			MeleeOnHit(proj, modProj, target, crit, npcCheck, targetIsDummy);
		}
		if (proj.CountsAsClass<RangedDamageClass>())
		{
			RangedOnHit(proj, modProj, target, crit, npcCheck);
		}
		if (proj.CountsAsClass<MagicDamageClass>())
		{
			MagicOnHit(proj, modProj, target, crit, npcCheck);
		}
		if (proj.CountsAsClass<SummonDamageClass>() && !proj.CountsAsClass<SummonMeleeSpeedDamageClass>())
		{
			SummonOnHit(proj, modProj, target, crit, npcCheck);
		}
		if (proj.CountsAsClass<ThrowingDamageClass>())
		{
			RogueOnHit(proj, modProj, target, crit, npcCheck);
		}
	}

	private void MeleeOnHit(Projectile proj, CalamityGlobalProjectile modProj, NPC target, bool crit, bool npcCheck, bool targetIsDummy)
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (proj.IsTrueMelee() && hideOfDeus && hideOfDeusTimer == 0)
		{
			hideOfDeusTimer = 10;
			int bulwarkStarDamage = (int)base.Player.GetTotalDamage<MeleeDamageClass>().ApplyTo(320f);
			for (int n = 0; n < 3; n++)
			{
				CalamityUtils.ProjectileRain(source, base.Player.Center, 400f, 100f, 500f, 800f, 29f, ModContent.ProjectileType<AstralStar>(), bulwarkStarDamage, 5f, base.Player.whoAmI);
			}
		}
		if (npcCheck)
		{
			if (ataxiaGeyser && base.Player.ownedProjectileCounts[ModContent.ProjectileType<ChaoticGeyser>()] < 3)
			{
				int geyserDamage = CalamityUtils.DamageSoftCap((double)proj.damage * 0.15, 36);
				Projectile.NewProjectile(source, proj.Center, Vector2.Zero, ModContent.ProjectileType<ChaoticGeyser>(), geyserDamage, 0f, base.Player.whoAmI);
			}
			if (bloodflareMelee && proj.IsTrueMelee() && bloodflareMeleeHits < BloodflareHeadMelee.HitsToActivateFrenzy && !bloodflareFrenzy && !base.Player.HasCooldown(BloodflareFrenzy.ID))
			{
				bloodflareMeleeHits++;
			}
		}
	}

	private void RangedOnHit(Projectile proj, CalamityGlobalProjectile modProj, NPC target, bool crit, bool npcCheck)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = target.Center;
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (npcCheck && tarraRanged && proj.CountsAsClass<RangedDamageClass>() && tarraRangedCooldown <= 0)
		{
			tarraRangedCooldown = TarragonHeadRanged.OnHitEffectCooldown;
			for (int l = 0; l < 2; l++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				int leafDamage = CalamityUtils.DamageSoftCap((int)((float)proj.damage * TarragonHeadRanged.LeafDamageRatio), TarragonHeadRanged.LeafDamageSoftcap);
				int leaf = Projectile.NewProjectile(source, position, velocity, 206, leafDamage, 0f, base.Player.whoAmI);
				if (leaf.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[leaf].DamageType = DamageClass.Generic;
					Main.projectile[leaf].netUpdate = true;
				}
			}
			if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<TarraEnergy>()] < 2)
			{
				for (int projCount = 0; projCount < 2; projCount++)
				{
					Vector2 velocity2 = CalamityUtils.RandomVelocity(100f, 70f, 100f);
					int energyDamage = CalamityUtils.DamageSoftCap((int)((float)proj.damage * TarragonHeadRanged.EnergyDamageRatio), TarragonHeadRanged.EnergyDamageSoftcap);
					Projectile.NewProjectile(source, proj.Center, velocity2, ModContent.ProjectileType<TarraEnergy>(), energyDamage, 0f, proj.owner);
				}
			}
		}
		if (dynamoStemCells && MiniSwarmerCooldown <= 0 && proj.CountsAsClass<RangedDamageClass>())
		{
			MiniSwarmerCooldown = 180;
			Vector2 directionToMouse = Main.MouseWorld - base.Player.Center;
			((Vector2)(ref directionToMouse)).Normalize();
			Vector2 velocity3 = directionToMouse * 19f;
			int MiniSwamerDamage = (int)base.Player.GetTotalDamage<RangedDamageClass>().ApplyTo(DynamoStemCells.MiniSwamerDamage);
			Projectile.NewProjectile(source, base.Player.Center, velocity3, ModContent.ProjectileType<MiniatureFolly>(), MiniSwamerDamage, 2f, proj.owner);
		}
	}

	private void MagicOnHit(Projectile proj, CalamityGlobalProjectile modProj, NPC target, bool crit, bool npcCheck)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = target.Center;
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (ataxiaMage && ataxiaDmg <= 0f)
		{
			int orbDamage = (int)((double)proj.damage * HydrothermicHeadMagic.OrbDamageRatio);
			Vector2 velocity = CalamityUtils.RandomVelocity(100f, 20f, 20f, 1f);
			Projectile.NewProjectile(source, proj.Center, velocity, ModContent.ProjectileType<HydrothermicSphere>(), orbDamage, 0f, proj.owner);
			int cooldown = (int)((float)orbDamage * HydrothermicHeadMagic.OrbDamageCooldownMult);
			ataxiaDmg += cooldown;
		}
		if (tarraMage & crit)
		{
			tarraCrits++;
		}
		if (npcCheck && ((bloodflareMage && bloodflareMageCooldown <= 0) & crit))
		{
			bloodflareMageCooldown = BloodflareHeadMagic.BloodsplosionCooldown;
			int bloodflareFireballDamage = CalamityUtils.DamageSoftCap((double)proj.damage * BloodflareHeadMagic.BloodsplosionDamageRatio, BloodflareHeadMagic.BloodsplosionDamageSoftcap);
			int fire = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BloodBombExplosion>(), bloodflareFireballDamage, 0f, base.Player.whoAmI, 0f, 0f, 1f);
			if (fire.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[fire].DamageType = DamageClass.Generic;
				Main.projectile[fire].netUpdate = true;
			}
		}
		if (silvaMage && silvaMageCooldown <= 0 && (proj.penetrate == 1 || proj.timeLeft <= 5))
		{
			silvaMageCooldown = SilvaHeadMagic.BurstCooldown;
			SoundEngine.PlaySound(in SoundID.Zombie103, proj.Center);
			int silvaBurstDamage = CalamityUtils.DamageSoftCap((double)SilvaHeadMagic.BurstDamage + (double)proj.damage * SilvaHeadMagic.BurstDamageRatio, SilvaHeadMagic.BurstDamageSoftcap);
			Projectile.NewProjectile(source, proj.Center, Vector2.Zero, ModContent.ProjectileType<SilvaBurst>(), silvaBurstDamage, 8f, base.Player.whoAmI);
		}
	}

	private void SummonOnHit(Projectile proj, CalamityGlobalProjectile modProj, NPC target, bool crit, bool npcCheck)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = target.Center;
		IEntitySource source = base.Player.GetSource_OnHit(target);
		if (phantomicArtifact)
		{
			int restoreBuff = ModContent.BuffType<PhantomicRegen>();
			int empowerBuff = ModContent.BuffType<PhantomicEmpowerment>();
			int shieldBuff = ModContent.BuffType<global::CalamityMod.Buffs.StatBuffs.PhantomicShield>();
			int buffType = Utils.SelectRandom<int>(Main.rand, restoreBuff, empowerBuff, shieldBuff);
			base.Player.AddBuff(buffType, 120);
			if (buffType == restoreBuff)
			{
				if (phantomicHeartRegen == 1000 && base.Player.ownedProjectileCounts[ModContent.ProjectileType<PhantomicHeart>()] == 0)
				{
					Vector2 spawnPos = proj.Center;
					spawnPos.Y += Main.rand.Next(-50, 50);
					spawnPos.X += Main.rand.Next(-50, 50);
					Projectile.NewProjectile(source, spawnPos, Vector2.Zero, ModContent.ProjectileType<PhantomicHeart>(), 0, 0f, base.Player.whoAmI);
				}
			}
			else if (buffType == empowerBuff)
			{
				if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<PhantomicDagger>()] < 3 && Main.rand.NextBool(4))
				{
					int damage = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(100f);
					Projectile.NewProjectile(source, base.Player.Center, Utils.RotatedByRandom(new Vector2(base.Player.velocity.X, base.Player.velocity.Y - 10f), 0.30000001192092896), ModContent.ProjectileType<PhantomicDagger>(), damage, 1f, base.Player.whoAmI);
				}
			}
			else if (base.Player.ownedProjectileCounts[ModContent.ProjectileType<global::CalamityMod.Projectiles.Summon.PhantomicShield>()] == 0 && phantomicBulwarkCooldown == 0)
			{
				Projectile.NewProjectile(source, base.Player.position, Vector2.Zero, ModContent.ProjectileType<global::CalamityMod.Projectiles.Summon.PhantomicShield>(), 0, 0f, base.Player.whoAmI);
			}
		}
		else if (hallowedRune)
		{
			int buffType2 = Utils.SelectRandom<int>(Main.rand, ModContent.BuffType<HallowedRunePower>(), ModContent.BuffType<HallowedRuneRegeneration>(), ModContent.BuffType<HallowedRuneDefense>());
			base.Player.AddBuff(buffType2, 120);
		}
		else if (sGlyph)
		{
			int buffType3 = Utils.SelectRandom<int>(Main.rand, ModContent.BuffType<SpiritPower>(), ModContent.BuffType<SpiritRegen>(), ModContent.BuffType<SpiritDefense>());
			base.Player.AddBuff(buffType3, 120);
		}
		if (fearmongerSet)
		{
			fearmongerRegenFrames += FearmongerGreathelm.RegenBoostDurationPerHit;
			if (fearmongerRegenFrames > FearmongerGreathelm.RegenBoostDurationLimit)
			{
				fearmongerRegenFrames = FearmongerGreathelm.RegenBoostDurationLimit;
			}
		}
		if (!new List<int>
		{
			ModContent.ProjectileType<EnergyOrb>(),
			ModContent.ProjectileType<IrradiatedAura>(),
			ModContent.ProjectileType<SummonAstralExplosion>(),
			ModContent.ProjectileType<ApparatusExplosion>(),
			ModContent.ProjectileType<HallowedStarSummon>()
		}.TrueForAll((int x) => proj.type != x))
		{
			return;
		}
		if (summonProjCooldown <= 0f)
		{
			if (nucleogenesis)
			{
				int apparatusDamage = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(60f);
				Projectile.NewProjectile(source, proj.Center, Vector2.Zero, ModContent.ProjectileType<ApparatusExplosion>(), apparatusDamage, 4f, proj.owner);
				summonProjCooldown = 100f;
			}
			else if (starbusterCore)
			{
				int starburstDamage = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(40f);
				Projectile.NewProjectile(source, proj.Center, Vector2.Zero, ModContent.ProjectileType<SummonAstralExplosion>(), starburstDamage, 3.5f, proj.owner);
				summonProjCooldown = 60f;
			}
			else if (nuclearFuelRod)
			{
				int nuclearDamage = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(20f);
				Projectile.NewProjectile(source, proj.Center, Vector2.Zero, ModContent.ProjectileType<IrradiatedAura>(), nuclearDamage, 0f, proj.owner);
				summonProjCooldown = 60f;
			}
			else if (jellyChargedBattery)
			{
				int batteryDamage = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(15f);
				CalamityUtils.SpawnOrb(proj, batteryDamage, ModContent.ProjectileType<EnergyOrb>(), 800f, 15f);
				summonProjCooldown = 60f;
			}
		}
		if (hallowedPower && hallowedRuneCooldown <= 0)
		{
			hallowedRuneCooldown = 180;
			for (int i = 0; i < 3; i++)
			{
				Vector2 spawnPosition = position - Utils.RotatedByRandom(new Vector2(0f, 920f), 0.30000001192092896);
				float speed = Main.rand.NextFloat(17f, 23f);
				int hallowedDamage = (int)base.Player.GetTotalDamage<SummonDamageClass>().ApplyTo(50f);
				Projectile.NewProjectile(source, spawnPosition, Vector2.Normalize(position - spawnPosition) * speed, ModContent.ProjectileType<HallowedStarSummon>(), hallowedDamage, 3f, proj.owner);
			}
		}
	}

	private void RogueOnHit(Projectile proj, CalamityGlobalProjectile modProj, NPC target, bool crit, bool npcCheck)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = target.Center;
		IEntitySource spawnSource = base.Player.GetSource_OnHit(target);
		int Type = ModContent.ProjectileType<DragonScalesInfernado>();
		if (modProj.stealthStrike && dragonScales && Main.projectile.Count((Projectile projectile) => projectile.type == Type && projectile.active) < 1)
		{
			int damage = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(DragonScales.TornadoBaseDamage);
			int projectileIndex = Projectile.NewProjectile(spawnSource, proj.Center.X, proj.Center.Y, 0f, 0f, ModContent.ProjectileType<DragonScalesInfernado>(), damage, 15f, Main.myPlayer, 10f, 9f);
			if (projectileIndex.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[projectileIndex].netUpdate = true;
			}
		}
		if (crit && tarraThrowing && tarraThrowingCrits < 50 && !tarragonImmunity && !base.Player.HasCooldown(global::CalamityMod.Cooldowns.TarragonImmunity.ID))
		{
			tarraThrowingCrits++;
		}
		if (xerocSet && xerocDmg <= 0f && base.Player.ownedProjectileCounts[ModContent.ProjectileType<EmpyreanEmber>()] < 3 && base.Player.ownedProjectileCounts[ModContent.ProjectileType<EmpyreanBlast>()] < 3)
		{
			switch (Main.rand.Next(5))
			{
			case 0:
			{
				int starDamage = (int)((float)proj.damage * 0.8f);
				CalamityUtils.SpawnOrb(proj, starDamage, ModContent.ProjectileType<EmpyreanStellarDetritus>(), 800f, Main.rand.Next(15, 30));
				xerocDmg += (int)((float)starDamage * 0.5f);
				break;
			}
			case 1:
			{
				int orbDamage = (int)((float)proj.damage * 0.6f);
				CalamityUtils.SpawnOrb(proj, orbDamage, ModContent.ProjectileType<EmpyreanMarble>(), 800f, 30f);
				xerocDmg += (int)((float)orbDamage * 0.5f);
				break;
			}
			case 2:
			{
				int fireDamage = (int)((float)proj.damage * 0.15f);
				Projectile.NewProjectile(spawnSource, proj.Center, Vector2.Zero, ModContent.ProjectileType<EmpyreanEmber>(), fireDamage, 0f, proj.owner);
				break;
			}
			case 3:
			{
				int blastDamage = (int)((float)proj.damage * 0.2f);
				Projectile.NewProjectile(spawnSource, proj.Center, Vector2.Zero, ModContent.ProjectileType<EmpyreanBlast>(), blastDamage, 0f, proj.owner);
				break;
			}
			case 4:
			{
				int bubbleDamage = (int)((float)proj.damage * 0.6f);
				CalamityUtils.SpawnOrb(proj, bubbleDamage, ModContent.ProjectileType<EmpyreanGlob>(), 800f, 15f);
				xerocDmg += (int)((double)bubbleDamage * 0.5);
				break;
			}
			}
		}
		if (modProj.stealthStrike && rogueCrownCooldown <= 0 && modProj.stealthStrikeHitCount < 3)
		{
			bool spawnedFeathers = false;
			if (nanotech)
			{
				Vector2 source = default(Vector2);
				for (int i = 0; i < 3; i++)
				{
					((Vector2)(ref source))._002Ector(position.X + (float)Main.rand.Next(-201, 201), Main.screenPosition.Y - 600f - (float)Main.rand.Next(50));
					Vector2 velocity = (position - source) / 40f;
					int damage2 = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(110f);
					Projectile.NewProjectile(spawnSource, source, velocity, ModContent.ProjectileType<NanoFlare>(), damage2, 3f, proj.owner);
				}
			}
			else if (moonCrown)
			{
				int lunarFlareDamage = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(MoonstoneCrown.BaseDamage);
				float lunarFlareKB = 3f;
				Vector2 source2 = default(Vector2);
				for (int i2 = 0; i2 < 3; i2++)
				{
					((Vector2)(ref source2))._002Ector(position.X + (float)Main.rand.Next(-201, 201), Main.screenPosition.Y - 600f - (float)Main.rand.Next(50));
					Vector2 velocity2 = (position - source2) / 10f;
					int flare = Projectile.NewProjectile(spawnSource, source2, velocity2, 645, lunarFlareDamage, lunarFlareKB, proj.owner);
					if (flare.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[flare].DamageType = DamageClass.Generic;
					}
				}
			}
			else if (featherCrown)
			{
				Vector2 source3 = default(Vector2);
				Vector2 velocity3 = default(Vector2);
				for (int i3 = 0; i3 < 3; i3++)
				{
					((Vector2)(ref source3))._002Ector(position.X + (float)Main.rand.Next(-201, 201), Main.screenPosition.Y - 600f - (float)Main.rand.Next(50));
					float speedX = (position.X - source3.X) / 30f;
					float speedY = (position.Y - source3.Y) * 8f;
					((Vector2)(ref velocity3))._002Ector(speedX, speedY);
					int featherDamage = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(25f);
					int feather = Projectile.NewProjectile(spawnSource, source3, velocity3, ModContent.ProjectileType<StickyFeather>(), featherDamage, 3f, proj.owner);
					if (feather.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[feather].DamageType = DamageClass.Generic;
						Main.projectile[feather].extraUpdates += 3;
					}
				}
				spawnedFeathers = true;
			}
			rogueCrownCooldown = (spawnedFeathers ? 15 : 60);
		}
		if (titanHeartSet && modProj.stealthStrike && titanCooldown <= 0 && modProj.stealthStrikeHitCount < 3)
		{
			int damage3 = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(TitanHeartMask.ExplosionDamage);
			Projectile.NewProjectile(spawnSource, proj.Center, Vector2.Zero, ModContent.ProjectileType<TitanHeartBoom>(), damage3, proj.knockBack, proj.owner, 1f);
			SoundEngine.PlaySound(in SoundID.Item14, proj.Center);
			for (int dustexplode = 0; dustexplode < 120; dustexplode++)
			{
				Vector2 dustd = Vector2.One.RotatedBy(MathHelper.ToRadians((float)(dustexplode * 3))) * 1.7f;
				Dust.NewDustPerfect(proj.Center, Main.rand.NextBool() ? ModContent.DustType<AstralBlue>() : ModContent.DustType<AstralOrange>(), dustd, 100).noGravity = true;
			}
			titanCooldown = 15;
		}
		if (raiderTalisman && modProj.stealthStrike)
		{
			raiderCritLifespan = CalamityUtils.SecondsToFrames(10);
			base.Player.AddCooldown(RaiderBoost.ID, raiderCritLifespan, true, vampiricTalisman ? "Bloodfeast" : "default");
			if (raiderSoundCooldown <= 0)
			{
				SoundEngine.PlaySound(in RaidersTalisman.StealthHitSound, base.Player.Center);
				raiderSoundCooldown = 60;
			}
		}
		if (npcCheck)
		{
			if (umbraphileSet && ((modProj.stealthStrike && modProj.stealthStrikeHitCount < 3) || Main.rand.NextBool(5)))
			{
				int umbraBlastDamage = CalamityUtils.DamageSoftCap((double)proj.damage * UmbraphileHood.ExplosionDamageRatio, UmbraphileHood.ExplosionDamageSoftcap);
				Projectile.NewProjectile(spawnSource, proj.Center, Vector2.Zero, ModContent.ProjectileType<UmbraphileBoom>(), umbraBlastDamage, 0f, base.Player.whoAmI);
			}
			if (electricianGlove && modProj.stealthStrike && modProj.stealthStrikeHitCount < 3)
			{
				for (int s = 0; s < 3; s++)
				{
					Vector2 velocity4 = CalamityUtils.RandomVelocity(50f, 30f, 60f);
					int damage4 = (int)base.Player.GetTotalDamage<RogueDamageClass>().ApplyTo(9f);
					int spark = Projectile.NewProjectile(spawnSource, position, velocity4, ModContent.ProjectileType<EGloveSpark>(), damage4, 0f, base.Player.whoAmI);
					if (spark.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[spark].DamageType = DamageClass.Generic;
						Main.projectile[spark].localNPCHitCooldown = -1;
					}
				}
			}
		}
		modProj.stealthStrikeHitCount++;
	}

	public void NPCDebuffs(NPC target, bool melee, bool ranged, bool magic, bool summon, bool rogue, bool whip, bool crit, bool proj = false, bool noFlask = false)
	{
		if (melee && !noFlask && eGauntlet)
		{
			CalamityUtils.Inflict246DebuffsNPC(target, ModContent.BuffType<ElementalMix>());
		}
		if ((melee | rogue | whip) && !noFlask)
		{
			if (flaskCrumbling)
			{
				CalamityUtils.Inflict246DebuffsNPC(target, ModContent.BuffType<Crumbling>());
			}
			if (flaskBrimstone)
			{
				CalamityUtils.Inflict246DebuffsNPC(target, ModContent.BuffType<BrimstoneFlames>(), 4f);
			}
			if (flaskHoly)
			{
				target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
			}
		}
		if (rogue && !noFlask)
		{
			switch (base.Player.meleeEnchant)
			{
			case 1:
				target.AddBuff(70, 60 * Main.rand.Next(5, 10));
				break;
			case 2:
				target.AddBuff(39, 60 * Main.rand.Next(3, 7));
				break;
			case 3:
				target.AddBuff(24, 60 * Main.rand.Next(3, 7));
				break;
			case 5:
				target.AddBuff(69, 60 * Main.rand.Next(10, 20));
				break;
			case 6:
				target.AddBuff(31, 60 * Main.rand.Next(1, 4));
				break;
			case 8:
				target.AddBuff(20, 60 * Main.rand.Next(5, 10));
				break;
			case 4:
				target.AddBuff(72, 120);
				break;
			}
			if (titanHeartMask)
			{
				target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), TitanHeartMask.OnHitDebuffDuration);
			}
		}
		if (summon && !whip)
		{
			if (profanedCrystal && DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs)
			{
				target.AddBuff(ModContent.BuffType<HolyFlames>(), 600);
			}
			else if (pSoulArtifact)
			{
				target.AddBuff(ModContent.BuffType<HolyFlames>(), 300);
			}
			if (divineBless)
			{
				target.AddBuff(ModContent.BuffType<BanishingFire>(), AngelicAlliance.BanishingFireDuration);
			}
			if (shadowMinions)
			{
				target.AddBuff(153, 180);
			}
			if (voltaicJelly && Main.rand.NextBool(starTaintedGenerator ? 1 : 5))
			{
				target.AddBuff(ModContent.BuffType<StaticDischarge>(), 60);
			}
			if (starTaintedGenerator)
			{
				target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
				target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
			}
		}
		if (amalgam)
		{
			target.AddBuff(189, 120);
			target.AddBuff(ModContent.BuffType<Nightwither>(), 120);
			target.AddBuff(ModContent.BuffType<Plague>(), 120);
			target.AddBuff(ModContent.BuffType<VermillionFlux>(), 120);
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
		}
		if (frostFlare)
		{
			CalamityUtils.Inflict246DebuffsNPC(target, 324);
		}
		if (omegaBlueChestplate)
		{
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
		}
		if (sulphurSet)
		{
			target.AddBuff(20, SulphurousHelmet.SetBonusPoisonDuration);
		}
		if (corrosiveSpine)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
		}
		if (alchFlask)
		{
			CalamityUtils.Inflict246DebuffsNPC(target, ModContent.BuffType<Plague>());
		}
		if (vexation)
		{
			target.AddBuff(70, 120);
		}
		if (snowRuffianSet & ranged & crit)
		{
			target.AddBuff(44, SnowRuffianMask.SetBonusFrostburnDuration);
		}
	}

	public void ProjLifesteal(NPC target, Projectile proj, int damage, bool crit)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalProjectile modProj = proj.Calamity();
		if (!target.IsAnEnemy(allowStatues: false))
		{
			return;
		}
		if (bloodflareSet && !base.Player.moonLeech && (double)target.life < (double)target.lifeMax * 0.5 && bloodflareHeartTimer <= 0)
		{
			bloodflareHeartTimer = 300;
			Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
		}
		if (gladiatorSword && target.life <= 0 && target.Calamity().gladiatorOnKill)
		{
			float healPower = 10f * Utils.GetLerpValue(300f, 0f, gladiatorTimer, clamped: true);
			target.Calamity().gladiatorOnKill = false;
			if (healPower >= 1f)
			{
				Projectile.NewProjectile(base.Player.GetSource_OnHit(target), target.Center, target.velocity * 0.5f, ModContent.ProjectileType<GladiatorHealOrb>(), 0, 0f, -1, (int)healPower);
				gladiatorTimer = 300;
			}
		}
		if (((vampiricTalisman && proj.CountsAsClass<RogueDamageClass>()) & crit) && proj.numHits < 1)
		{
			int heal = (int)Math.Round((double)damage * 0.008);
			if (heal > 2)
			{
				heal = 2;
			}
			base.Player.SpawnLifeStealProjectile(target, proj, 305, heal, (raiderCritLifespan > 0 && !proj.Calamity().stealthStrike) ? 1.3f : 1.6f);
		}
		if (bloodyGlove && proj.CountsAsClass<RogueDamageClass>() && modProj.stealthStrike && proj.numHits < 1)
		{
			base.Player.SpawnLifeStealProjectile(target, proj, 305, electricianGlove ? 10 : 5, 2f);
		}
		if ((bloodflareThrowing && proj.CountsAsClass<ThrowingDamageClass>()) & crit)
		{
			base.Player.SpawnLifeStealProjectile(target, proj, 305, 2, 1.5f);
		}
		if (proj.CountsAsClass<MagicDamageClass>() && base.Player.HeldItem.CountsAsClass<MagicDamageClass>())
		{
			if (manaOverloader)
			{
				double healMult = 0.1 - (double)proj.numHits * 0.025;
				base.Player.SpawnLifeStealProjectile(target, proj, ModContent.ProjectileType<ManaPolarizerHealOrb>(), (int)Math.Round((double)damage * healMult), 1.75f);
			}
			if (ataxiaMage)
			{
				double healMult2 = HydrothermicHeadMagic.OrbHealingRatio - (double)proj.numHits * HydrothermicHeadMagic.OrbHealingRatioLossPerPierce;
				base.Player.SpawnLifeStealProjectile(target, proj, ModContent.ProjectileType<HydrothermicHealOrb>(), (int)Math.Round((double)damage * healMult2), HydrothermicHeadMagic.OrbHealingCooldownMult);
			}
		}
	}

	public void ItemLifesteal(NPC target, Item item, int damage)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (!target.IsAnEnemy(allowStatues: false))
		{
			return;
		}
		if (bloodflareSet && (double)target.life < (double)target.lifeMax * 0.5 && bloodflareHeartTimer <= 0)
		{
			bloodflareHeartTimer = 300;
			Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
		}
		if (gladiatorSword && target.life <= 0 && target.Calamity().gladiatorOnKill)
		{
			float healPower = 10f * Utils.GetLerpValue(300f, 0f, gladiatorTimer, clamped: true);
			target.Calamity().gladiatorOnKill = false;
			if (healPower >= 1f)
			{
				Projectile.NewProjectile(base.Player.GetSource_OnHit(target), target.Center, target.velocity * 0.5f, ModContent.ProjectileType<GladiatorHealOrb>(), 0, 0f, -1, (int)healPower);
				gladiatorTimer = 300;
			}
		}
	}

	public static void HorsemansBladeOnHit(Player player, int targetIdx, int damage, float knockback, int extraUpdateAmt = 0, int type = 321)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		int logicCheckScreenHeight = Main.LogicCheckScreenHeight;
		int logicCheckScreenWidth = Main.LogicCheckScreenWidth;
		int x = Main.rand.Next(100, 300);
		int y = Main.rand.Next(100, 300);
		switch (Main.rand.Next(4))
		{
		case 0:
			x -= logicCheckScreenWidth / 2 + x;
			break;
		case 1:
			x += logicCheckScreenWidth / 2 - x;
			break;
		case 2:
			y -= logicCheckScreenHeight / 2 + y;
			break;
		case 3:
			y += logicCheckScreenHeight / 2 - y;
			break;
		}
		x += (int)player.position.X;
		y += (int)player.position.Y;
		float speed = 8f;
		Vector2 spawnPos = default(Vector2);
		((Vector2)(ref spawnPos))._002Ector((float)x, (float)y);
		Vector2 velocity = Main.npc[targetIdx].DirectionFrom(spawnPos);
		velocity *= speed;
		int projectile = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), spawnPos, velocity, type, damage, knockback, player.whoAmI, targetIdx);
		Main.projectile[projectile].extraUpdates += extraUpdateAmt;
	}

	public CalamityPlayer()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		drawBossHPBar = true;
		stealthUIAlpha = 1f;
		SulphWaterUIOpacity = 1f;
		shouldDrawSmallText = true;
		ammoCost = 1f;
		healingPotionMultiplier = 1f;
		DoGCartSegments = new DoGCartSegment[18];
		StarburstEntities = new List<StarburstEntity>();
		subtitleColors = (Color[])(object)new Color[2]
		{
			Color.White,
			Color.White
		};
		TypelessDebuffMultiplier = new StatModifier();
		HeatDebuffMultiplier = new StatModifier();
		ColdDebuffMultiplier = new StatModifier();
		SicknessDebuffMultiplier = new StatModifier();
		WaterDebuffMultiplier = new StatModifier();
		ElectricDebuffMultiplier = new StatModifier();
		lastSplitType = -1;
		CurrentlyViewedFactoryID = -1;
		CurrentlyViewedChargerID = -1;
		CurrentlyViewedHologramID = -1;
		CurrentlyViewedCanvasID = -1;
		CurrentlyViewedCanvasType = -1;
		dragoonDrizzlefishGelBoost = 1;
		deadSunCounter = 6;
		PhotoTimer = 90;
		furyFuel = 1800;
		bloodflareHeartTimer = 300;
		necroReviveCounter = -1;
		playFullAdrenalineSound = true;
		temporaryStealthMax = 0.1f;
		stealthGenStandstill = 1f;
		stealthGenMoving = 1f;
		stealthAcceleration = 1f;
		rogueVelocity = 1f;
		rageMax = 100f;
		RageDuration = BalancingConstants.DefaultRageDuration;
		RageDamageBoost = BalancingConstants.DefaultRageDamageBoost;
		adrenalineMax = 100f;
		AdrenalineDuration = CalamityUtils.SecondsToFrames(5);
		AdrenalineChargeTime = CalamityUtils.SecondsToFrames(30);
		AdrenalineFadeTime = CalamityUtils.SecondsToFrames(2);
		defenseDamageRatio = 0.3333;
		totalDefenseDamageRecoveryFrames = 60;
		abyssPlayerGlowMultiplier = 1f;
		abyssFlashlightWidthMultiplier = 1f;
		teslaVisuals = true;
		magmaStoneVisuals = true;
		eGauntletVisuals = true;
		voidSashVisuals = true;
		pulseRate = 1f;
		gSabatonHotkeyFallWindup = -1;
		procDodgeEffects = true;
		dOfTheDeepDefenseBuffMax = 420;
		IsFirstDashFrame = true;
		XykFXColor = Color.Black;
		lightRGB = Color.Black;
		blunderBoosterVisibility = true;
		angelicActivate = -1;
		DodgeEffects = new List<Func<Player, Player.HurtInfo, string>>();
		tarraDefenseTime = 600;
		silvaCountdown = SilvaArmor.ReviveDuration;
		bloodfinTimer = 30;
		healCounter = 300;
		pscLerpColor = Color.White;
		ProvidenceBurnEffectDrawer = new FireParticleSet(-1, int.MaxValue, Color.Yellow, Color.Red * 1.2f, 10f, 0.65f);
		SeenDraedonDialogs = new List<ulong>();
		oldGravDir = 1f;
		tempGravDir = 1f;
		base._002Ector();
	}

	static CalamityPlayer()
	{
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		areThereAnyDamnBosses = false;
		areThereAnyDamnEvents = false;
		chaosStateDuration = 900;
		chaosStateDuration_NR = 1200;
		MaxStratusStarburst = 100;
		RageFilledSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/FullRage");
		RageActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/RageActivate");
		RageEndSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/RageEnd");
		AdrenalineFilledSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/FullAdrenaline");
		AdrenalineActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/AdrenalineActivate");
		AdrenalineHurtSound = new SoundStyle("CalamityMod/Sounds/Custom/AdrenalineMajorLoss");
		AdrenalineHurtGFB = new SoundStyle("CalamityMod/Sounds/Custom/AdrenalineMajorLossGFB");
		NanomachinesActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/NanomachinesActivate");
		RogueStealthSound = new SoundStyle("CalamityMod/Sounds/Custom/RogueStealth");
		DefenseDamageSound = new SoundStyle("CalamityMod/Sounds/Custom/DefenseDamage");
		IjiDeathSound = new SoundStyle("CalamityMod/Sounds/Custom/IjiDies");
		DrownSound = new SoundStyle("CalamityMod/Sounds/Custom/AbyssDrown");
		LeonDeathNoiseRE4_ForGFB = new SoundStyle("CalamityMod/Sounds/Custom/GFB/LeonDeathNoiseRE4");
		BaroclawHit = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerSegmentBreak2")
		{
			Volume = 0.7f
		};
		AbsorberHit = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/SilvaActivation")
		{
			Volume = 0.7f
		};
		startMessageDisplayDelay = -1;
		MoonlightDyeDayColors = new List<Color>
		{
			new Color(255, 163, 56),
			new Color(235, 30, 19),
			new Color(242, 48, 187)
		};
		MoonlightDyeNightColors = new List<Color>
		{
			new Color(24, 134, 198),
			new Color(130, 40, 150),
			new Color(40, 64, 150)
		};
	}
}
