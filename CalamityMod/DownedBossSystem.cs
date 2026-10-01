using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod;

public class DownedBossSystem : ModSystem
{
	internal static bool _downedDesertScourge;

	internal static bool _downedCrabulon;

	internal static bool _downedHiveMind;

	internal static bool _downedPerforator;

	internal static bool _downedSlimeGod;

	internal static bool _downedCryogen;

	internal static bool _downedAquaticScourge;

	internal static bool _downedBrimstoneElemental;

	internal static bool _downedCalamitasClone;

	internal static bool _downedLeviathan;

	internal static bool _downedAstrumAureus;

	internal static bool _downedPlaguebringer;

	internal static bool _downedRavager;

	internal static bool _downedAstrumDeus;

	internal static bool _downedGuardians;

	internal static bool _downedDragonfolly;

	internal static bool _downedProvidence;

	internal static bool _downedCeaselessVoid;

	internal static bool _downedStormWeaver;

	internal static bool _downedSignus;

	internal static bool _downedSecondSentinels;

	internal static bool _downedPolterghast;

	internal static bool _downedBoomerDuke;

	internal static bool _downedDoG;

	internal static bool _downedYharon;

	internal static bool _downedAres;

	internal static bool _downedThanatos;

	internal static bool _downedArtemisAndApollo;

	internal static bool _downedExoMechs;

	internal static bool _downedCalamitas;

	internal static bool _downedPrimordialWyrm;

	internal static bool _downedGSS;

	internal static bool _downedCLAM;

	internal static bool _downedCLAMHardMode;

	internal static bool _downedCragmawMire;

	internal static bool _downedMauler;

	internal static bool _downedNuclearTerror;

	internal static bool _downedEoCAcidRain;

	internal static bool _downedAquaticScourgeAcidRain;

	internal static bool _startedBossRushAtLeastOnce;

	internal static bool _downedBossRush;

	internal static bool _downedBetsy;

	internal static bool _downedDreadnautilus;

	public static bool downedDesertScourge
	{
		get
		{
			return _downedDesertScourge;
		}
		set
		{
			if (!value)
			{
				_downedDesertScourge = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedDesertScourge, -1);
			}
		}
	}

	public static bool downedCrabulon
	{
		get
		{
			return _downedCrabulon;
		}
		set
		{
			if (!value)
			{
				_downedCrabulon = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCrabulon, -1);
			}
		}
	}

	public static bool downedHiveMind
	{
		get
		{
			return _downedHiveMind;
		}
		set
		{
			if (!value)
			{
				_downedHiveMind = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedHiveMind, -1);
			}
		}
	}

	public static bool downedPerforator
	{
		get
		{
			return _downedPerforator;
		}
		set
		{
			if (!value)
			{
				_downedPerforator = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedPerforator, -1);
			}
		}
	}

	public static bool downedSlimeGod
	{
		get
		{
			return _downedSlimeGod;
		}
		set
		{
			if (!value)
			{
				_downedSlimeGod = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedSlimeGod, -1);
			}
		}
	}

	public static bool downedDreadnautilus
	{
		get
		{
			return _downedDreadnautilus;
		}
		set
		{
			if (!value)
			{
				_downedDreadnautilus = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedDreadnautilus, -1);
			}
		}
	}

	public static bool downedCryogen
	{
		get
		{
			return _downedCryogen;
		}
		set
		{
			if (!value)
			{
				_downedCryogen = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCryogen, -1);
			}
		}
	}

	public static bool downedAquaticScourge
	{
		get
		{
			return _downedAquaticScourge;
		}
		set
		{
			if (!value)
			{
				_downedAquaticScourge = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedAquaticScourge, -1);
			}
		}
	}

	public static bool downedBrimstoneElemental
	{
		get
		{
			return _downedBrimstoneElemental;
		}
		set
		{
			if (!value)
			{
				_downedBrimstoneElemental = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedBrimstoneElemental, -1);
			}
		}
	}

	public static bool downedCalamitasClone
	{
		get
		{
			return _downedCalamitasClone;
		}
		set
		{
			if (!value)
			{
				_downedCalamitasClone = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCalamitasClone, -1);
			}
		}
	}

	public static bool downedLeviathan
	{
		get
		{
			return _downedLeviathan;
		}
		set
		{
			if (!value)
			{
				_downedLeviathan = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedLeviathan, -1);
			}
		}
	}

	public static bool downedAstrumAureus
	{
		get
		{
			return _downedAstrumAureus;
		}
		set
		{
			if (!value)
			{
				_downedAstrumAureus = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedAstrumAureus, -1);
			}
		}
	}

	public static bool downedBetsy
	{
		get
		{
			return _downedBetsy;
		}
		set
		{
			if (!value)
			{
				_downedBetsy = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedBetsy, -1);
			}
		}
	}

	public static bool downedPlaguebringer
	{
		get
		{
			return _downedPlaguebringer;
		}
		set
		{
			if (!value)
			{
				_downedPlaguebringer = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedPlaguebringer, -1);
			}
		}
	}

	public static bool downedRavager
	{
		get
		{
			return _downedRavager;
		}
		set
		{
			if (!value)
			{
				_downedRavager = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedRavager, -1);
			}
		}
	}

	public static bool downedAstrumDeus
	{
		get
		{
			return _downedAstrumDeus;
		}
		set
		{
			if (!value)
			{
				_downedAstrumDeus = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedAstrumDeus, -1);
			}
		}
	}

	public static bool downedGuardians
	{
		get
		{
			return _downedGuardians;
		}
		set
		{
			if (!value)
			{
				_downedGuardians = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedGuardians, -1);
			}
		}
	}

	public static bool downedDragonfolly
	{
		get
		{
			return _downedDragonfolly;
		}
		set
		{
			if (!value)
			{
				_downedDragonfolly = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedDragonfolly, -1);
			}
		}
	}

	public static bool downedProvidence
	{
		get
		{
			return _downedProvidence;
		}
		set
		{
			if (!value)
			{
				_downedProvidence = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedProvidence, -1);
			}
		}
	}

	public static bool downedCeaselessVoid
	{
		get
		{
			return _downedCeaselessVoid;
		}
		set
		{
			if (!value)
			{
				_downedCeaselessVoid = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCeaselessVoid, -1);
			}
		}
	}

	public static bool downedStormWeaver
	{
		get
		{
			return _downedStormWeaver;
		}
		set
		{
			if (!value)
			{
				_downedStormWeaver = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedStormWeaver, -1);
			}
		}
	}

	public static bool downedSignus
	{
		get
		{
			return _downedSignus;
		}
		set
		{
			if (!value)
			{
				_downedSignus = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedSignus, -1);
			}
		}
	}

	public static bool downedPolterghast
	{
		get
		{
			return _downedPolterghast;
		}
		set
		{
			if (!value)
			{
				_downedPolterghast = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedPolterghast, -1);
			}
		}
	}

	public static bool downedBoomerDuke
	{
		get
		{
			return _downedBoomerDuke;
		}
		set
		{
			if (!value)
			{
				_downedBoomerDuke = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedBoomerDuke, -1);
			}
		}
	}

	public static bool downedDoG
	{
		get
		{
			return _downedDoG;
		}
		set
		{
			if (!value)
			{
				_downedDoG = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedDoG, -1);
			}
		}
	}

	public static bool downedYharon
	{
		get
		{
			return _downedYharon;
		}
		set
		{
			if (!value)
			{
				_downedYharon = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedYharon, -1);
			}
		}
	}

	public static bool downedExoMechs
	{
		get
		{
			return _downedExoMechs;
		}
		set
		{
			if (!value)
			{
				_downedExoMechs = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedExoMechs, -1);
			}
		}
	}

	public static bool downedCalamitas
	{
		get
		{
			return _downedCalamitas;
		}
		set
		{
			if (!value)
			{
				_downedCalamitas = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCalamitas, -1);
			}
		}
	}

	public static bool downedPrimordialWyrm
	{
		get
		{
			return _downedPrimordialWyrm;
		}
		set
		{
			if (!value)
			{
				_downedPrimordialWyrm = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedPrimordialWyrm, -1);
			}
		}
	}

	public static bool downedCLAM
	{
		get
		{
			return _downedCLAM;
		}
		set
		{
			if (!value)
			{
				_downedCLAM = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCLAM, -1);
			}
		}
	}

	public static bool downedCLAMHardMode
	{
		get
		{
			return _downedCLAMHardMode;
		}
		set
		{
			if (!value)
			{
				_downedCLAMHardMode = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCLAMHardMode, -1);
			}
		}
	}

	public static bool downedCragmawMire
	{
		get
		{
			return _downedCragmawMire;
		}
		set
		{
			if (!value)
			{
				_downedCragmawMire = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedCragmawMire, -1);
			}
		}
	}

	public static bool downedGSS
	{
		get
		{
			return _downedGSS;
		}
		set
		{
			if (!value)
			{
				_downedGSS = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedGSS, -1);
			}
		}
	}

	public static bool downedMauler
	{
		get
		{
			return _downedMauler;
		}
		set
		{
			if (!value)
			{
				_downedMauler = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedMauler, -1);
			}
		}
	}

	public static bool downedNuclearTerror
	{
		get
		{
			return _downedNuclearTerror;
		}
		set
		{
			if (!value)
			{
				_downedNuclearTerror = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedNuclearTerror, -1);
			}
		}
	}

	public static bool downedSecondSentinels
	{
		get
		{
			return _downedSecondSentinels;
		}
		set
		{
			_downedSecondSentinels = value;
		}
	}

	public static bool downedAres
	{
		get
		{
			return _downedAres;
		}
		set
		{
			_downedAres = value;
		}
	}

	public static bool downedThanatos
	{
		get
		{
			return _downedThanatos;
		}
		set
		{
			_downedThanatos = value;
		}
	}

	public static bool downedArtemisAndApollo
	{
		get
		{
			return _downedArtemisAndApollo;
		}
		set
		{
			_downedArtemisAndApollo = value;
		}
	}

	public static bool downedEoCAcidRain
	{
		get
		{
			return _downedEoCAcidRain;
		}
		set
		{
			if (!value)
			{
				_downedEoCAcidRain = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedEoCAcidRain, -1);
			}
		}
	}

	public static bool downedAquaticScourgeAcidRain
	{
		get
		{
			return _downedAquaticScourgeAcidRain;
		}
		set
		{
			if (!value)
			{
				_downedAquaticScourgeAcidRain = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedAquaticScourgeAcidRain, -1);
			}
		}
	}

	public static bool startedBossRushAtLeastOnce
	{
		get
		{
			return _startedBossRushAtLeastOnce;
		}
		set
		{
			_startedBossRushAtLeastOnce = value;
		}
	}

	public static bool downedBossRush
	{
		get
		{
			return _downedBossRush;
		}
		set
		{
			if (!value)
			{
				_downedBossRush = false;
			}
			else
			{
				NPC.SetEventFlagCleared(ref _downedBossRush, -1);
			}
		}
	}

	internal static void ResetAllFlags()
	{
		downedDesertScourge = false;
		downedCrabulon = false;
		downedHiveMind = false;
		downedPerforator = false;
		downedSlimeGod = false;
		downedDreadnautilus = false;
		downedCryogen = false;
		downedAquaticScourge = false;
		downedBrimstoneElemental = false;
		downedCalamitasClone = false;
		downedLeviathan = false;
		downedAstrumAureus = false;
		downedBetsy = false;
		downedPlaguebringer = false;
		downedRavager = false;
		downedAstrumDeus = false;
		downedGuardians = false;
		downedDragonfolly = false;
		downedProvidence = false;
		downedCeaselessVoid = false;
		downedStormWeaver = false;
		downedSignus = false;
		downedPolterghast = false;
		downedBoomerDuke = false;
		downedDoG = false;
		downedYharon = false;
		downedAres = false;
		downedThanatos = false;
		downedArtemisAndApollo = false;
		downedExoMechs = false;
		downedCalamitas = false;
		downedPrimordialWyrm = false;
		downedSecondSentinels = false;
		downedCLAM = false;
		downedEoCAcidRain = false;
		downedCLAMHardMode = false;
		downedCragmawMire = false;
		downedAquaticScourgeAcidRain = false;
		downedGSS = false;
		downedMauler = false;
		downedNuclearTerror = false;
		startedBossRushAtLeastOnce = false;
		downedBossRush = false;
	}

	public override void OnWorldLoad()
	{
		ResetAllFlags();
	}

	public override void OnWorldUnload()
	{
		ResetAllFlags();
	}

	public override void SaveWorldData(TagCompound tag)
	{
		List<string> downed = new List<string>();
		if (downedDesertScourge)
		{
			downed.Add("desertScourge");
		}
		if (downedCrabulon)
		{
			downed.Add("crabulon");
		}
		if (downedHiveMind)
		{
			downed.Add("hiveMind");
		}
		if (downedPerforator)
		{
			downed.Add("perforator");
		}
		if (downedSlimeGod)
		{
			downed.Add("slimeGod");
		}
		if (downedDreadnautilus)
		{
			downed.Add("dreadnautilus");
		}
		if (downedCryogen)
		{
			downed.Add("cryogen");
		}
		if (downedAquaticScourge)
		{
			downed.Add("aquaticScourge");
		}
		if (downedBrimstoneElemental)
		{
			downed.Add("brimstoneElemental");
		}
		if (downedCalamitasClone)
		{
			downed.Add("calamitas");
		}
		if (downedLeviathan)
		{
			downed.Add("leviathan");
		}
		if (downedAstrumAureus)
		{
			downed.Add("astrageldon");
		}
		if (downedBetsy)
		{
			downed.Add("betsy");
		}
		if (downedPlaguebringer)
		{
			downed.Add("plaguebringerGoliath");
		}
		if (downedRavager)
		{
			downed.Add("scavenger");
		}
		if (downedAstrumDeus)
		{
			downed.Add("starGod");
		}
		if (downedGuardians)
		{
			downed.Add("guardians");
		}
		if (downedDragonfolly)
		{
			downed.Add("bumblebirb");
		}
		if (downedProvidence)
		{
			downed.Add("providence");
		}
		if (downedCeaselessVoid)
		{
			downed.Add("ceaselessVoid");
		}
		if (downedStormWeaver)
		{
			downed.Add("stormWeaver");
		}
		if (downedSignus)
		{
			downed.Add("signus");
		}
		if (downedSecondSentinels)
		{
			downed.Add("secondSentinels");
		}
		if (downedPolterghast)
		{
			downed.Add("polterghast");
		}
		if (downedBoomerDuke)
		{
			downed.Add("oldDuke");
		}
		if (downedDoG)
		{
			downed.Add("devourerOfGods");
		}
		if (downedYharon)
		{
			downed.Add("yharon");
		}
		if (downedThanatos)
		{
			downed.Add("thanatos");
		}
		if (downedArtemisAndApollo)
		{
			downed.Add("artemisAndApollo");
		}
		if (downedAres)
		{
			downed.Add("ares");
		}
		if (downedExoMechs)
		{
			downed.Add("exoMechs");
		}
		if (downedCalamitas)
		{
			downed.Add("supremeCalamitas");
		}
		if (downedPrimordialWyrm)
		{
			downed.Add("adultEidolonWyrm");
		}
		if (downedCLAM)
		{
			downed.Add("clam");
		}
		if (downedEoCAcidRain)
		{
			downed.Add("eocRain");
		}
		if (downedCLAMHardMode)
		{
			downed.Add("clamHardmode");
		}
		if (downedCragmawMire)
		{
			downed.Add("cragmawMire");
		}
		if (downedAquaticScourgeAcidRain)
		{
			downed.Add("hmRain");
		}
		if (downedGSS)
		{
			downed.Add("greatSandShark");
		}
		if (downedMauler)
		{
			downed.Add("mauler");
		}
		if (downedNuclearTerror)
		{
			downed.Add("nuclearTerror");
		}
		if (startedBossRushAtLeastOnce)
		{
			downed.Add("startedBossRush");
		}
		if (downedBossRush)
		{
			downed.Add("bossRush");
		}
		tag["downedFlags"] = downed;
	}

	public override void LoadWorldData(TagCompound tag)
	{
		IList<string> list = tag.GetList<string>("downedFlags");
		downedDesertScourge = list.Contains("desertScourge");
		downedAquaticScourge = list.Contains("aquaticScourge");
		downedCrabulon = list.Contains("crabulon");
		downedHiveMind = list.Contains("hiveMind");
		downedPerforator = list.Contains("perforator");
		downedSlimeGod = list.Contains("slimeGod");
		downedDreadnautilus = list.Contains("dreadnautilus");
		downedCryogen = list.Contains("cryogen");
		downedBrimstoneElemental = list.Contains("brimstoneElemental");
		downedCalamitasClone = list.Contains("calamitas");
		downedLeviathan = list.Contains("leviathan");
		downedAstrumAureus = list.Contains("astrageldon");
		downedBetsy = list.Contains("betsy");
		downedPlaguebringer = list.Contains("plaguebringerGoliath");
		downedRavager = list.Contains("scavenger");
		downedAstrumDeus = list.Contains("starGod");
		downedGuardians = list.Contains("guardians");
		downedDragonfolly = list.Contains("bumblebirb");
		downedProvidence = list.Contains("providence");
		downedCeaselessVoid = list.Contains("ceaselessVoid");
		downedStormWeaver = list.Contains("stormWeaver");
		downedSignus = list.Contains("signus");
		downedPolterghast = list.Contains("polterghast");
		downedBoomerDuke = list.Contains("oldDuke");
		downedSecondSentinels = list.Contains("secondSentinels");
		downedDoG = list.Contains("devourerOfGods");
		downedYharon = list.Contains("yharon");
		downedThanatos = list.Contains("thanatos");
		downedArtemisAndApollo = list.Contains("artemisAndApollo");
		downedAres = list.Contains("ares");
		downedExoMechs = list.Contains("exoMechs");
		downedCalamitas = list.Contains("supremeCalamitas");
		downedPrimordialWyrm = list.Contains("adultEidolonWyrm");
		downedCLAM = list.Contains("clam");
		downedEoCAcidRain = list.Contains("eocRain");
		downedCLAMHardMode = list.Contains("clamHardmode");
		downedCragmawMire = list.Contains("cragmawMire");
		downedAquaticScourgeAcidRain = list.Contains("hmRain");
		downedGSS = list.Contains("greatSandShark");
		downedMauler = list.Contains("mauler");
		downedNuclearTerror = list.Contains("nuclearTerror");
		startedBossRushAtLeastOnce = list.Contains("startedBossRush");
		downedBossRush = list.Contains("bossRush");
	}
}
