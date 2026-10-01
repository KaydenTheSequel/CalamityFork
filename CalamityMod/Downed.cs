using System;

namespace CalamityMod;

internal class Downed
{
	public static readonly Func<bool> DownedDesertScourge = () => DownedBossSystem.downedDesertScourge;

	public static readonly Func<bool> DownedGiantClam = () => DownedBossSystem.downedCLAM;

	public static readonly Func<bool> DownedCrabulon = () => DownedBossSystem.downedCrabulon;

	public static readonly Func<bool> DownedHiveMind = () => DownedBossSystem.downedHiveMind;

	public static readonly Func<bool> DownedPerforators = () => DownedBossSystem.downedPerforator;

	public static readonly Func<bool> DownedSlimeGod = () => DownedBossSystem.downedSlimeGod;

	public static readonly Func<bool> DownedCryogen = () => DownedBossSystem.downedCryogen;

	public static readonly Func<bool> DownedBrimstoneElemental = () => DownedBossSystem.downedBrimstoneElemental;

	public static readonly Func<bool> DownedAquaticScourge = () => DownedBossSystem.downedAquaticScourge;

	public static readonly Func<bool> DownedCragmawMire = () => DownedBossSystem.downedCragmawMire;

	public static readonly Func<bool> DownedCalClone = () => DownedBossSystem.downedCalamitasClone;

	public static readonly Func<bool> NotDownedCalClone = () => !DownedBossSystem.downedCalamitasClone;

	public static readonly Func<bool> DownedGSS = () => DownedBossSystem.downedGSS;

	public static readonly Func<bool> DownedLeviathan = () => DownedBossSystem.downedLeviathan;

	public static readonly Func<bool> DownedAureus = () => DownedBossSystem.downedAstrumAureus;

	public static readonly Func<bool> DownedPBG = () => DownedBossSystem.downedPlaguebringer;

	public static readonly Func<bool> DownedRavager = () => DownedBossSystem.downedRavager;

	public static readonly Func<bool> DownedDeus = () => DownedBossSystem.downedAstrumDeus;

	public static readonly Func<bool> DownedGuardians = () => DownedBossSystem.downedGuardians;

	public static readonly Func<bool> DownedDragonfolly = () => DownedBossSystem.downedDragonfolly;

	public static readonly Func<bool> DownedProvidence = () => DownedBossSystem.downedProvidence;

	public static readonly Func<bool> DownedCeaselessVoid = () => DownedBossSystem.downedCeaselessVoid;

	public static readonly Func<bool> DownedStormWeaver = () => DownedBossSystem.downedStormWeaver;

	public static readonly Func<bool> DownedSignus = () => DownedBossSystem.downedSignus;

	public static readonly Func<bool> DownedPolterghast = () => DownedBossSystem.downedPolterghast;

	public static readonly Func<bool> DownedMauler = () => DownedBossSystem.downedMauler;

	public static readonly Func<bool> DownedNuclearTerror = () => DownedBossSystem.downedNuclearTerror;

	public static readonly Func<bool> DownedOldDuke = () => DownedBossSystem.downedBoomerDuke;

	public static readonly Func<bool> DownedDoG = () => DownedBossSystem.downedDoG;

	public static readonly Func<bool> DownedYharon = () => DownedBossSystem.downedYharon;

	public static readonly Func<bool> DownedExoMechs = () => DownedBossSystem.downedExoMechs;

	public static readonly Func<bool> DownedCalamitas = () => DownedBossSystem.downedCalamitas;

	public static readonly Func<bool> DownedPrimordialWyrm = () => DownedBossSystem.downedPrimordialWyrm;

	public static readonly Func<bool> DownedAcidRainInitial = () => DownedBossSystem.downedEoCAcidRain;

	public static readonly Func<bool> DownedAcidRainHardmode = () => DownedBossSystem.downedAquaticScourgeAcidRain;

	public static readonly Func<bool> DownedBossRush = () => DownedBossSystem.downedBossRush;
}
