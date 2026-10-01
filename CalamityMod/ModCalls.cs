using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Systems;
using CalamityMod.Systems.Collections;
using CalamityMod.UI;
using CalamityMod.UI.CalamitasEnchants;
using CalamityMod.UI.DraedonSummoning;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace CalamityMod;

public class ModCalls
{
	public static bool GetBossDowned(string boss)
	{
		switch (boss.ToLower())
		{
		default:
			return false;
		case "acid rain":
		case "acidrain1":
		case "acidrain":
		case "acid rain 1":
		case "acidraineoc":
		case "acidrain 1":
		case "acid rain eoc":
		case "acidrain eoc":
			return DownedBossSystem.downedEoCAcidRain;
		case "desertscourge":
		case "desert scourge":
			return DownedBossSystem.downedDesertScourge;
		case "giantclam":
		case "giant clam":
		case "clam":
			return DownedBossSystem.downedCLAM;
		case "crabulon":
			return DownedBossSystem.downedCrabulon;
		case "hive mind":
		case "hivemind":
		case "thehivemind":
		case "the hive mind":
			return DownedBossSystem.downedHiveMind;
		case "perforators":
		case "perforator":
		case "theperforators":
		case "perforatorhive":
		case "the perforators":
		case "perforator hive":
		case "theperforatorhive":
		case "the perforator hive":
			return DownedBossSystem.downedPerforator;
		case "slime god":
		case "slimegod":
		case "theslimegod":
		case "the slime god":
			return DownedBossSystem.downedSlimeGod;
		case "hmgiantclam":
		case "giantclamhm":
		case "hardmode clam":
		case "hm giant clam":
		case "giant clam hm":
		case "hardmodeclam":
		case "hardmodegiantclam":
		case "hardmode giant clam":
		case "hmclam":
		case "clamhm":
		case "hm clam":
		case "clam hm":
			return DownedBossSystem.downedCLAMHardMode;
		case "cryogen":
			return DownedBossSystem.downedCryogen;
		case "acidrain2":
		case "acid rain 2":
		case "acidrain 2":
		case "acidrainscourge":
		case "acid rain scourge":
		case "acid rain aquatic scourge":
		case "acid rain aquaticscourge":
		case "acidrain aquatic scourge":
		case "acidrain scourge":
		case "acidrain aquaticscourge":
		case "acidrainaquaticscourge":
			return DownedBossSystem.downedAquaticScourgeAcidRain;
		case "aquaticscourge":
		case "aquatic scourge":
			return DownedBossSystem.downedAquaticScourge;
		case "cragmawmire":
		case "cragmaw mire":
		case "mire":
		case "cragmaw":
			return DownedBossSystem.downedCragmawMire;
		case "brimstone elemental":
		case "brimstoneelemental":
			return DownedBossSystem.downedBrimstoneElemental;
		case "clonelamitas":
		case "calamitasclone":
		case "calamitas clone":
		case "calamitas doppelganger":
		case "clone":
		case "calamitasdoppelganger":
			return DownedBossSystem.downedCalamitasClone;
		case "greatsandshark":
		case "great sand shark":
		case "gss":
			return DownedBossSystem.downedGSS;
		case "the siren":
		case "leviathan":
		case "thesiren":
		case "the leviathan":
		case "theleviathan":
		case "sirenleviathan":
		case "siren leviathan":
		case "sirenandleviathan":
		case "anahita leviathan":
		case "siren and leviathan":
		case "anahitaandleviathan":
		case "anahita":
		case "anahita and the leviathan":
		case "anahitaleviathan":
		case "siren":
		case "anahita and leviathan":
		case "the siren and the leviathan":
			return DownedBossSystem.downedLeviathan;
		case "astrum aureus":
		case "astrumaureus":
		case "aureus":
			return DownedBossSystem.downedAstrumAureus;
		case "plaguebringer":
		case "the plaguebringer goliath":
		case "theplaguebringergoliath":
		case "plaguebringer goliath":
		case "pbg":
		case "plaguebringergoliath":
			return DownedBossSystem.downedPlaguebringer;
		case "scavenger":
		case "ravager":
			return DownedBossSystem.downedRavager;
		case "star god":
		case "astrum deus":
		case "astrumdeus":
		case "stargod":
			return DownedBossSystem.downedAstrumDeus;
		case "guardians":
		case "profanedguardians":
		case "donuts":
		case "the profaned guardians":
		case "profaned guardians":
		case "theprofanedguardians":
			return DownedBossSystem.downedGuardians;
		case "dragonfolly":
		case "bumblebirb":
		case "bumblefuck":
		case "the dragonfolly":
			return DownedBossSystem.downedDragonfolly;
		case "providence":
		case "providencetheprofanedgoddess":
		case "providence the profaned goddess":
		case "providence, the profaned goddess":
			return DownedBossSystem.downedProvidence;
		case "polterghast":
		case "necroghast":
		case "necroplasm":
			return DownedBossSystem.downedPolterghast;
		case "mauler":
			return DownedBossSystem.downedMauler;
		case "nuclearterror":
		case "nuclear terror":
			return DownedBossSystem.downedNuclearTerror;
		case "acidrain3":
		case "old duke":
		case "acid rain 3":
		case "boomer duke":
		case "sulfur duke":
		case "sulphurduke":
		case "acidrain 3":
		case "theoldduke":
		case "boomerduke":
		case "sulfurduke":
		case "acidrain duke":
		case "acidrainduke":
		case "the old duke":
		case "sulphur duke":
		case "acid rain duke":
		case "oldduke":
			return DownedBossSystem.downedBoomerDuke;
		case "sentinel1":
		case "ceaselessvoid":
		case "ceaseless void":
		case "void":
			return DownedBossSystem.downedCeaselessVoid;
		case "sentinel2":
		case "stormweaver":
		case "storm weaver":
			return DownedBossSystem.downedStormWeaver;
		case "sentinel3":
		case "cosmic wraith":
		case "cosmicwraith":
		case "signus":
		case "signusenvoyofthedevourer":
		case "signus envoy of the devourer":
		case "signus, envoy of the devourer":
			return DownedBossSystem.downedSignus;
		case "sentinel":
		case "sentinelany":
		case "anysentinel":
		case "onesentinel":
		case "any sentinel":
		case "one sentinel":
			if (!DownedBossSystem.downedCeaselessVoid && !DownedBossSystem.downedStormWeaver)
			{
				return DownedBossSystem.downedSignus;
			}
			return true;
		case "sentinels":
		case "sentinelall":
		case "allsentinel":
		case "all sentinels":
		case "allsentinels":
			if (DownedBossSystem.downedCeaselessVoid && DownedBossSystem.downedStormWeaver)
			{
				return DownedBossSystem.downedSignus;
			}
			return false;
		case "devourerofgods":
		case "thedevourerofgods":
		case "devourer of gods":
		case "dog":
		case "the devourer of gods":
			return DownedBossSystem.downedDoG;
		case "yharon":
		case "yharon, dragon of rebirth":
		case "yharon dragon of rebirth":
		case "yharonresplendentphoenix":
		case "jungledragonyharon":
		case "yharondragonofrebirth":
		case "jungle dragon, yharon":
		case "yharon, resplendent phoenix":
		case "jungle dragon yharon":
		case "yharon resplendent phoenix":
			return DownedBossSystem.downedYharon;
		case "exo mechs":
		case "exomechs":
		case "the exo mechs":
		case "draedon":
			return DownedBossSystem.downedExoMechs;
		case "thanatos":
			return DownedBossSystem.downedThanatos;
		case "ares":
			return DownedBossSystem.downedAres;
		case "exo twins":
		case "exotwins":
		case "apollo":
		case "artemis":
			return DownedBossSystem.downedArtemisAndApollo;
		case "calamitas":
		case "scal":
		case "supreme calamitas":
		case "supreme witch, calamitas":
		case "supremecalamitas":
		case "supreme witch calamitas":
		case "supremewitchcalamitas":
			return DownedBossSystem.downedCalamitas;
		case "adultwyrm":
		case "adult wyrm":
		case "adult eidolon":
		case "adulteidolon":
		case "primordialwyrm":
		case "primordial wyrm":
		case "adulteidolonwyrm":
		case "adult eidolon wyrm":
			return DownedBossSystem.downedPrimordialWyrm;
		case "boss rush":
		case "bossrush":
		case "terminus":
			return DownedBossSystem.downedBossRush;
		}
	}

	public static bool GetInZone(Player p, string zone)
	{
		CalamityPlayer mp = p.Calamity();
		switch (zone.ToLower())
		{
		default:
			return false;
		case "calamity":
		case "calamitybiome":
		case "profaned crag":
		case "profanedcrags":
		case "brimstonecrag":
		case "calamity biome":
		case "profaned crags":
		case "brimstone crag":
		case "brimstonecrags":
		case "crag":
		case "crags":
		case "profanedcrag":
		case "brimstone":
		case "brimstone crags":
			return mp.ZoneCalamity;
		case "astral biome":
		case "astralinfection":
		case "astral":
		case "astralbiome":
		case "astral infection":
			return mp.ZoneAstral;
		case "the sunken sea":
		case "thesunkensea":
		case "sunkensea":
		case "sunken sea":
			return mp.ZoneSunkenSea;
		case "sulfurous sea":
		case "sulphuroussea":
		case "sulphurous sea":
		case "sulfuroussea":
		case "sulfursea":
		case "sulfur":
		case "sulphur sea":
		case "sulfur sea":
		case "sulphursea":
		case "sulphur":
			return mp.ZoneSulphur;
		case "theabyss":
		case "anyabyss":
		case "abyssany":
		case "abyss":
		case "any abyss":
		case "the abyss":
		case "any abyss layer":
			return mp.ZoneAbyss;
		case "sulfur depths":
		case "sulphurdepths":
		case "abyss layer 1":
		case "sulphur depths":
		case "sulfuricdepths":
		case "sulfurdepths":
		case "sulphuricdepths":
		case "sulfurousdepths":
		case "sulfuric depths":
		case "abyss1":
		case "layer1":
		case "abysslayer1":
		case "sulfurous depths":
		case "sulphurousdepths":
		case "sulphuric depths":
		case "abyss 1":
		case "abyss_1":
		case "layer 1":
		case "layer_1":
		case "sulphurous depths":
			return mp.ZoneAbyssLayer1;
		case "abyss layer 2":
		case "murky waters":
		case "abyss2":
		case "layer2":
		case "abysslayer2":
		case "murky water":
		case "murkywaters":
		case "murkywater":
		case "abyss 2":
		case "abyss_2":
		case "layer 2":
		case "layer_2":
			return mp.ZoneAbyssLayer2;
		case "abyss layer 3":
		case "thermal vents":
		case "vents":
		case "thermalvents":
		case "abyss3":
		case "layer3":
		case "abysslayer3":
		case "abyss 3":
		case "abyss_3":
		case "layer 3":
		case "layer_3":
			return mp.ZoneAbyssLayer3;
		case "the void":
		case "abyss layer 4":
		case "void":
		case "abyss4":
		case "layer4":
		case "abysslayer4":
		case "abyss 4":
		case "abyss_4":
		case "layer 4":
		case "layer_4":
		case "thevoid":
			return mp.ZoneAbyssLayer4;
		}
	}

	public static bool GetDifficultyActive(string difficulty)
	{
		switch (difficulty.ToLower())
		{
		default:
			return false;
		case "revengeancemode":
		case "revengeance mode":
		case "revengeance":
		case "rev":
			return CalamityWorld.revenge;
		case "deathmode":
		case "death mode":
		case "death":
			return CalamityWorld.death;
		case "boss rush active":
		case "boss rush":
		case "bossrush":
		case "bossrushactive":
		case "br":
			return BossRushEvent.BossRushActive;
		case "armageddon mode":
		case "instakill":
		case "armageddon":
		case "instagib":
		case "armageddonmode":
		case "arma":
			return CalamityWorld.armageddon;
		}
	}

	public static bool SetDifficultyActive(string difficulty, bool enabled)
	{
		switch (difficulty.ToLower())
		{
		default:
			return false;
		case "revengeancemode":
		case "revengeance mode":
		case "revengeance":
		case "rev":
			return CalamityWorld.revenge = enabled;
		case "deathmode":
		case "death mode":
		case "death":
			return CalamityWorld.death = enabled;
		case "boss rush active":
		case "boss rush":
		case "bossrush":
		case "bossrushactive":
		case "br":
			return BossRushEvent.BossRushActive = enabled;
		case "armageddon mode":
		case "instakill":
		case "armageddon":
		case "instagib":
		case "armageddonmode":
		case "arma":
			return CalamityWorld.armageddon = enabled;
		}
	}

	public static void AddCustomDifficulty(DifficultyMode newMode)
	{
		DifficultyModeSystem.Difficulties.Add(newMode);
		DifficultyModeSystem.CalculateDifficultyData();
	}

	public static void AddWorldScreenDifficulty(string name, Func<AWorldListItem, bool> function, Color color, int index = -1)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		WorldSelectionDifficultySystem.WorldDifficulty difficulty = new WorldSelectionDifficultySystem.WorldDifficulty(name, function, color);
		if (index == -1)
		{
			WorldSelectionDifficultySystem.WorldDifficulties.Add(difficulty);
		}
		else
		{
			WorldSelectionDifficultySystem.WorldDifficulties.Insert(index, difficulty);
		}
	}

	public static float GetRogueVelocity(Player p)
	{
		return p?.Calamity()?.rogueVelocity ?? 1f;
	}

	public static float AddRogueVelocity(Player p, float add)
	{
		if (p != null)
		{
			return p.Calamity().rogueVelocity += add;
		}
		return 1f;
	}

	public static float GetCurrentStealth(Player p)
	{
		return (p?.Calamity()?.rogueStealth).GetValueOrDefault();
	}

	public static float GetMaxStealth(Player p)
	{
		return (p?.Calamity()?.rogueStealthMax).GetValueOrDefault();
	}

	public static float AddMaxStealth(Player p, float add)
	{
		if (p != null)
		{
			return p.Calamity().rogueStealthMax += add;
		}
		return 0f;
	}

	public static bool CanStealthStrike(Player p)
	{
		return p?.Calamity()?.StealthStrikeAvailable() == true;
	}

	public static void SetStealthProjectile(Projectile projectile, bool enabled)
	{
		if (projectile != null)
		{
			projectile.Calamity().stealthStrike = enabled;
		}
	}

	public static bool GetStealthProjectile(Projectile projectile)
	{
		return projectile?.Calamity()?.stealthStrike == true;
	}

	public static void ConsumeStealth(Player player)
	{
		player?.Calamity()?.ConsumeStealthByAttacking();
	}

	public static float GetRage(Player p)
	{
		return (p?.Calamity()?.rage).GetValueOrDefault();
	}

	public static float GetAdrenaline(Player p)
	{
		return (p?.Calamity()?.adrenaline).GetValueOrDefault();
	}

	public static float GetRageMax(Player p)
	{
		return (p?.Calamity()?.rageMax).GetValueOrDefault();
	}

	public static float GetAdrenalineMax(Player p)
	{
		return (p?.Calamity()?.adrenalineMax).GetValueOrDefault();
	}

	public static bool GetChargeable(Item i)
	{
		return i?.Calamity()?.UsesCharge == true;
	}

	public static void SetChargeable(Item i, bool chargeable)
	{
		if (i != null)
		{
			i.Calamity().UsesCharge = chargeable;
		}
	}

	public static float GetCharge(Item i)
	{
		return (i?.Calamity()?.Charge).GetValueOrDefault();
	}

	public static void SetCharge(Item i, float charge)
	{
		if (i != null)
		{
			i.Calamity().Charge = charge;
		}
	}

	public static float GetMaxCharge(Item i)
	{
		return (i?.Calamity()?.MaxCharge).GetValueOrDefault();
	}

	public static void SetMaxCharge(Item i, float chargeMax)
	{
		if (i != null)
		{
			i.Calamity().MaxCharge = chargeMax;
		}
	}

	public static float GetChargePerUse(Item i)
	{
		return (i?.Calamity()?.ChargePerUse).GetValueOrDefault();
	}

	public static void SetChargePerUse(Item i, float chargeUse)
	{
		if (i != null)
		{
			i.Calamity().ChargePerUse = chargeUse;
		}
	}

	public static float GetChargePerAltUse(Item i)
	{
		return (i?.Calamity()?.ChargePerAltUse).GetValueOrDefault();
	}

	public static void SetChargePerAltUse(Item i, float chargeAltUse)
	{
		if (i != null)
		{
			i.Calamity().ChargePerAltUse = chargeAltUse;
		}
	}

	public static bool GetRightClickListener(Player p)
	{
		return p?.Calamity()?.rightClickListener == true;
	}

	public static bool GetMouseWorldListener(Player p)
	{
		return p?.Calamity()?.mouseWorldListener == true;
	}

	public static bool GetRightClick(Player p)
	{
		return p?.Calamity()?.mouseRight == true;
	}

	public static Vector2 GetMouseWorld(Player p)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		return (Vector2)(((_003F?)p?.Calamity()?.mouseWorld) ?? Vector2.Zero);
	}

	public static void SetRightClickListener(Player p, bool active)
	{
		if (p != null)
		{
			p.Calamity().rightClickListener = active;
		}
	}

	public static void SetMouseWorldListener(Player p, bool active)
	{
		if (p != null)
		{
			p.Calamity().mouseWorldListener = active;
		}
	}

	public static float GetDarknessIntensity(Player p)
	{
		return p?.Calamity().darknessIntensity ?? 0f;
	}

	public static void AddAbyssLightStrength(Player p, float add)
	{
		if (p != null)
		{
			p.Calamity().abyssDarkness -= add;
		}
	}

	public static void AddBreathTick(Player p, float add)
	{
		if (p != null)
		{
			p.Calamity().externalBreathTickBoost += add;
		}
	}

	public static void AddFlightTimeMult(Player p, float add)
	{
		if (p != null)
		{
			p.Calamity().externalFlightTimeMultBoost += add;
		}
	}

	public static void ToggleInfiniteFlight(Player p, bool enabled)
	{
		if (p != null)
		{
			p.Calamity().infiniteFlight = enabled;
		}
	}

	public static bool GetWearingRogueArmor(Player p)
	{
		return p?.Calamity()?.wearingRogueArmor == true;
	}

	public static void SetWearingRogueArmor(Player p, bool enabled)
	{
		if (p != null)
		{
			p.Calamity().wearingRogueArmor = enabled;
		}
	}

	public static bool GetWearingPostMLSummonerArmor(Player p)
	{
		return p?.Calamity()?.WearingPostMLSummonerSet == true;
	}

	public static void SetWearingPostMLSummonerArmor(Player p, bool enabled)
	{
		if (p != null)
		{
			p.Calamity().WearingPostMLSummonerSet = enabled;
		}
	}

	public static bool SetPlayerColdImmune(Player p, bool cold)
	{
		if (p != null)
		{
			return p.Calamity().externalColdImmunity = cold;
		}
		return false;
	}

	public static bool SetPlayerHeatImmune(Player p, bool heat)
	{
		if (p != null)
		{
			return p.Calamity().externalHeatImmunity = heat;
		}
		return false;
	}

	public static bool SetPlayerDefenseDamageImmune(Player p, bool dd)
	{
		if (p != null)
		{
			return p.Calamity().externalDefenseDamageImmunity = dd;
		}
		return false;
	}

	public static float SetDamageReduction(int npcID, float dr)
	{
		CalamityGlobalNPC.DRValues.TryGetValue(npcID, out var oldDR);
		CalamityGlobalNPC.DRValues.Remove(npcID);
		CalamityGlobalNPC.DRValues.Add(npcID, dr);
		return oldDR;
	}

	public static void SetDamageReductionSpecific(NPC npc, float dr)
	{
		if (npc != null)
		{
			npc.Calamity().DR = dr;
		}
	}

	public static float GetDamageReduction(NPC npc)
	{
		return (npc?.Calamity()?.DR).GetValueOrDefault();
	}

	public static void SetDefenseDamageNPC(NPC npc, bool enabled)
	{
		if (npc != null)
		{
			npc.Calamity().canBreakPlayerDefense = enabled;
		}
	}

	public static bool GetDefenseDamageNPC(NPC npc)
	{
		return npc?.Calamity()?.canBreakPlayerDefense == true;
	}

	public static void SetDefenseDamageProjectile(Projectile projectile, bool enabled)
	{
		if (projectile != null)
		{
			projectile.Calamity().DealsDefenseDamage = enabled;
		}
	}

	public static bool GetDefenseDamageProjectile(Projectile projectile)
	{
		return projectile?.Calamity()?.DealsDefenseDamage == true;
	}

	public static void SetDebuffVulnerability(NPC npc, string debuffName, bool? enabled)
	{
		if (npc == null)
		{
			return;
		}
		string text = debuffName.ToLower();
		if (text == null)
		{
			return;
		}
		switch (text.Length)
		{
		default:
			return;
		case 4:
		{
			char c = text[0];
			if ((uint)c <= 102u)
			{
				if (c == 'c')
				{
					if (!(text == "cold"))
					{
						return;
					}
					goto IL_0268;
				}
				if (c != 'f' || !(text == "fire"))
				{
					return;
				}
			}
			else
			{
				if (c != 'h')
				{
					if (c != 's' || !(text == "sick"))
					{
						return;
					}
					goto IL_028f;
				}
				if (!(text == "heat"))
				{
					return;
				}
			}
			goto IL_0282;
		}
		case 3:
		{
			char c = text[0];
			if (c != 'h')
			{
				if (c != 'i')
				{
					if (c != 'w' || !(text == "wet"))
					{
						return;
					}
					break;
				}
				if (!(text == "ice"))
				{
					return;
				}
				goto IL_0268;
			}
			if (!(text == "hot"))
			{
				return;
			}
			goto IL_0282;
		}
		case 6:
		{
			char c = text[0];
			if (c != 'f')
			{
				if (c != 'p' || !(text == "poison"))
				{
					return;
				}
				goto IL_028f;
			}
			if (!(text == "frozen"))
			{
				return;
			}
			goto IL_0268;
		}
		case 8:
			switch (text[0])
			{
			default:
				return;
			case 'f':
				break;
			case 'e':
				goto IL_01c2;
			case 's':
				goto IL_01d3;
			case 'p':
				goto IL_01e4;
			case 'd':
				goto IL_01f5;
			}
			if (!(text == "freezing"))
			{
				return;
			}
			goto IL_0268;
		case 7:
		{
			char c = text[0];
			if (c != 'b')
			{
				if (c != 't' || !(text == "thunder"))
				{
					return;
				}
				goto IL_0275;
			}
			if (!(text == "burning"))
			{
				return;
			}
			goto IL_0282;
		}
		case 5:
		{
			char c = text[0];
			if (c != 'd')
			{
				if (c != 'v')
				{
					if (c != 'w' || !(text == "water"))
					{
						return;
					}
					break;
				}
				if (!(text == "venom"))
				{
					return;
				}
				goto IL_028f;
			}
			if (!(text == "drown"))
			{
				return;
			}
			break;
		}
		case 11:
			if (!(text == "electricity"))
			{
				return;
			}
			goto IL_0275;
		case 9:
			if (!(text == "lightning"))
			{
				return;
			}
			goto IL_0275;
		case 10:
			return;
			IL_01c2:
			if (!(text == "electric"))
			{
				return;
			}
			goto IL_0275;
			IL_028f:
			npc.Calamity().VulnerableToSickness = enabled;
			return;
			IL_0275:
			npc.Calamity().VulnerableToElectricity = enabled;
			return;
			IL_0268:
			npc.Calamity().VulnerableToCold = enabled;
			return;
			IL_0282:
			npc.Calamity().VulnerableToHeat = enabled;
			return;
			IL_01f5:
			if (!(text == "drowning"))
			{
				return;
			}
			break;
			IL_01e4:
			if (!(text == "poisoned"))
			{
				return;
			}
			goto IL_028f;
			IL_01d3:
			if (!(text == "sickness"))
			{
				return;
			}
			goto IL_028f;
		}
		npc.Calamity().VulnerableToWater = enabled;
	}

	public static bool? GetDebuffVulnerability(NPC npc, string debuffName)
	{
		if (npc != null)
		{
			switch (debuffName.ToLower())
			{
			default:
				return false;
			case "cold":
			case "ice":
			case "frozen":
			case "freezing":
				return npc?.Calamity()?.VulnerableToCold ?? ((bool?)null);
			case "electric":
			case "thunder":
			case "electricity":
			case "lightning":
				return npc?.Calamity()?.VulnerableToElectricity ?? ((bool?)null);
			case "heat":
			case "fire":
			case "hot":
			case "burning":
				return npc?.Calamity()?.VulnerableToHeat ?? ((bool?)null);
			case "sick":
			case "poison":
			case "sickness":
			case "poisoned":
			case "venom":
				return npc?.Calamity()?.VulnerableToSickness ?? ((bool?)null);
			case "wet":
			case "drowning":
			case "drown":
			case "water":
				return npc?.Calamity()?.VulnerableToWater ?? ((bool?)null);
			}
		}
		return false;
	}

	public static float[] GetCalamityAI(NPC npc)
	{
		return npc?.Calamity()?.newAI ?? new float[0];
	}

	public static void SetCalamityAI(NPC npc, int aiSlot, float value)
	{
		if (npc != null)
		{
			npc.Calamity().newAI[aiSlot] = value;
		}
	}

	public static bool BossHealthBarVisible()
	{
		return Main.LocalPlayer.Calamity().drawBossHPBar;
	}

	public static bool SetBossHealthBarVisible(bool visible)
	{
		return Main.LocalPlayer.Calamity().drawBossHPBar = visible;
	}

	public static bool GetShouldCloseBossHealthBar(NPC npc)
	{
		return npc?.Calamity()?.ShouldCloseHPBar == true;
	}

	public static void SetShouldCloseBossHealthBar(NPC npc, bool enabled)
	{
		npc.Calamity().ShouldCloseHPBar = enabled;
	}

	public static bool AreDodgesDisabled()
	{
		return Main.LocalPlayer.Calamity().disableAllDodges;
	}

	public static bool DisableAllDodges(bool disable)
	{
		return Main.LocalPlayer.Calamity().disableAllDodges = disable;
	}

	public static bool SetAmalgamBuffList(int type, bool shouldBeListed)
	{
		if (shouldBeListed && !CalamityBuffSets.BuffedByAmalgam[type])
		{
			CalamityBuffSets.BuffedByAmalgam[type] = true;
			return true;
		}
		if (!shouldBeListed)
		{
			CalamityBuffSets.BuffedByAmalgam[type] = false;
			return false;
		}
		return false;
	}

	public static bool SetPersistentBuffList(int type, bool isPersistent)
	{
		if (isPersistent && !CalamityBuffSets.IsPersistentBuff[type])
		{
			CalamityBuffSets.IsPersistentBuff[type] = true;
			return true;
		}
		if (!isPersistent)
		{
			CalamityBuffSets.IsPersistentBuff[type] = false;
			return false;
		}
		return false;
	}

	public static bool IsOnAmalgamBuffList(int type)
	{
		return CalamityBuffSets.BuffedByAmalgam[type];
	}

	public static bool IsOnPersistentBuffList(int type)
	{
		return CalamityBuffSets.IsPersistentBuff[type];
	}

	public static bool AddToVeneratedLocketBanlist(int type)
	{
		if (!CalamityItemSets.DisablesVeneratedLocketEffect[type])
		{
			CalamityItemSets.DisablesVeneratedLocketEffect[type] = true;
			return true;
		}
		return false;
	}

	public static bool SetSummonerNerfDisabledByMinion(int type, bool disableNerf)
	{
		if (disableNerf && !CalamityProjectileSets.MinionWhichIgnoresSummonerNerf[type])
		{
			CalamityProjectileSets.MinionWhichIgnoresSummonerNerf[type] = true;
			return true;
		}
		if (!disableNerf)
		{
			CalamityProjectileSets.MinionWhichIgnoresSummonerNerf[type] = false;
			return false;
		}
		return false;
	}

	public static bool SetSummonerNerfDisabledByItem(int type, bool disableNerf)
	{
		if (disableNerf && !CalamityItemSets.ItemWhichDisablesSummonerNerf[type])
		{
			CalamityItemSets.ItemWhichDisablesSummonerNerf[type] = true;
			return true;
		}
		if (!disableNerf)
		{
			CalamityItemSets.ItemWhichDisablesSummonerNerf[type] = false;
			return false;
		}
		return false;
	}

	public static bool GetSummonerNerfDisabledByMinion(int type)
	{
		return CalamityProjectileSets.MinionWhichIgnoresSummonerNerf[type];
	}

	public static bool GetSummonerNerfDisabledByItem(int type)
	{
		return CalamityItemSets.ItemWhichDisablesSummonerNerf[type];
	}

	public static void RegisterDebuff(string texturePath, Predicate<NPC> debuffCheck)
	{
		if (!CalamityGlobalNPC.moddedDebuffTextureList.Contains((texturePath, debuffCheck)))
		{
			CalamityGlobalNPC.moddedDebuffTextureList.Add((texturePath, debuffCheck));
		}
	}

	public static void RegisterTownNPCShop(int id, Predicate<Player> getShop, Action<Player, bool> setShop)
	{
		if (!CalamityGlobalTownNPC.npcAlertList.Contains((id, getShop, setShop)))
		{
			CalamityGlobalTownNPC.npcAlertList.Add((id, getShop, setShop));
		}
	}

	public static bool HasPermanentPowerup(Player player, string powerupName)
	{
		return powerupName switch
		{
			"SanguineTangerine" => player.Calamity().sTangerine, 
			"MiracleFruit" => player.Calamity().mFruit, 
			"TaintedCloudberry" => player.Calamity().tCloudberry, 
			"SacredStrawberry" => player.Calamity().sStrawberry, 
			"CometShard" => player.Calamity().cShard, 
			"EtherealCore" => player.Calamity().eCore, 
			"PhantomHeart" => player.Calamity().pHeart, 
			"MushroomPlasmaRoot" => player.Calamity().rageBoostOne, 
			"InfernalBlood" => player.Calamity().rageBoostTwo, 
			"RedLightningContainer" => player.Calamity().rageBoostThree, 
			"ElectrolyteGelPack" => player.Calamity().adrenalineBoostOne, 
			"StarlightFuelCell" => player.Calamity().adrenalineBoostTwo, 
			"Ectoheart" => player.Calamity().adrenalineBoostThree, 
			"CelestialOnion" => player.Calamity().extraAccessoryML, 
			_ => false, 
		};
	}

	public static void SetPermanentPowerup(Player player, string powerupName, bool value)
	{
		if (powerupName == null)
		{
			return;
		}
		switch (powerupName.Length)
		{
		case 17:
			switch (powerupName[2])
			{
			case 'n':
				if (powerupName == "SanguineTangerine")
				{
					player.Calamity().sTangerine = value;
				}
				break;
			case 'i':
				if (powerupName == "TaintedCloudberry")
				{
					player.Calamity().tCloudberry = value;
				}
				break;
			case 'a':
				if (powerupName == "StarlightFuelCell")
				{
					player.Calamity().adrenalineBoostTwo = value;
				}
				break;
			}
			break;
		case 12:
			switch (powerupName[0])
			{
			case 'M':
				if (powerupName == "MiracleFruit")
				{
					player.Calamity().mFruit = value;
				}
				break;
			case 'E':
				if (powerupName == "EtherealCore")
				{
					player.Calamity().eCore = value;
				}
				break;
			case 'P':
				if (powerupName == "PhantomHeart")
				{
					player.Calamity().pHeart = value;
				}
				break;
			}
			break;
		case 18:
			switch (powerupName[0])
			{
			case 'M':
				if (powerupName == "MushroomPlasmaRoot")
				{
					player.Calamity().rageBoostOne = value;
				}
				break;
			case 'E':
				if (powerupName == "ElectrolyteGelPack")
				{
					player.Calamity().adrenalineBoostOne = value;
				}
				break;
			}
			break;
		case 16:
			if (powerupName == "SacredStrawberry")
			{
				player.Calamity().sStrawberry = value;
			}
			break;
		case 10:
			if (powerupName == "CometShard")
			{
				player.Calamity().cShard = value;
			}
			break;
		case 13:
			if (powerupName == "InfernalBlood")
			{
				player.Calamity().rageBoostTwo = value;
			}
			break;
		case 21:
			if (powerupName == "RedLightningContainer")
			{
				player.Calamity().rageBoostThree = value;
			}
			break;
		case 9:
			if (powerupName == "Ectoheart")
			{
				player.Calamity().adrenalineBoostThree = value;
			}
			break;
		case 14:
			if (powerupName == "CelestialOnion")
			{
				player.Calamity().extraAccessoryML = value;
			}
			break;
		case 11:
		case 15:
		case 19:
		case 20:
			break;
		}
	}

	public static bool AddToHPScaling(int type)
	{
		if (!CalamityNPCSets.ScalesHealthLikeBoss[type])
		{
			CalamityNPCSets.ScalesHealthLikeBoss[type] = true;
			return true;
		}
		return false;
	}

	public static object Call(params object[] args)
	{
		//IL_27c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce0: Unknown result type (might be due to invalid IL or missing references)
		if (args == null || args.Length == 0)
		{
			return new ArgumentNullException("ERROR: No function name specified. First argument must be a function name.");
		}
		if (!(args[0] is string))
		{
			return new ArgumentException("ERROR: First argument must be a string function name.");
		}
		switch (args[0].ToString())
		{
		case "Downed":
		case "GetDowned":
		case "BossDowned":
		case "GetBossDowned":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a boss or event name as a string.");
			}
			if (!(args[1] is string))
			{
				return new ArgumentException("ERROR: The argument to \"Downed\" must be a string.");
			}
			return GetBossDowned(args[1].ToString());
		case "InZone":
		case "GetInZone":
		case "GetZone":
		case "Zone":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and a zone name as a string.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify a zone name as a string.");
			}
			if (!(args[2] is string))
			{
				return new ArgumentException("ERROR: The second argument to \"InZone\" must be a string.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"InZone\" must be a Player or an int.");
			}
			return GetInZone(castPlayer(args[1]), args[2].ToString());
		case "Difficulty":
		case "GetDifficulty":
		case "DifficultyActive":
		case "GetDifficultyActive":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a difficulty mode name as a string.");
			}
			if (!(args[1] is string))
			{
				return new ArgumentException("ERROR: The argument to \"Difficulty\" must be a string.");
			}
			return GetDifficultyActive(args[1].ToString());
		case "SetDifficulty":
		case "SetDifficultyActive":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a difficulty mode name as a string and a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify a bool.");
			}
			if (!(args[2] is bool enabled))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDifficulty\" must be a bool.");
			}
			if (!(args[1] is string))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDifficulty\" must be a string.");
			}
			return SetDifficultyActive(args[1].ToString(), enabled);
		case "AddDifficultyToUI":
			if (args.Length < 2)
			{
				return new ArgumentException("ERROR: Not enough arguements provided");
			}
			if (!(args[1] is DifficultyMode mode))
			{
				return new ArgumentException("ERROR: A class inheriting from 'DifficultyMode' must be provided.");
			}
			AddCustomDifficulty(mode);
			return null;
		case "AddWorldScreenDifficulty":
		case "AddWorldSelectionDifficulty":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a difficulty mode name as a string.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify a Func<AWorldListItem, bool>.");
			}
			if (args.Length < 4)
			{
				return new ArgumentNullException("ERROR: Must specify a Color.");
			}
			if (args.Length >= 5 && !(args[4] is int))
			{
				return new ArgumentException("ERROR: The fourth argument to \"AddWorldScreenDifficulty\" must be an int.");
			}
			if (!(args[3] is Color color))
			{
				return new ArgumentException("ERROR: The third argument to \"AddWorldScreenDifficulty\" must be a Color.");
			}
			if (!(args[2] is Func<AWorldListItem, bool> worldFunction))
			{
				return new ArgumentException("ERROR: The second argument to \"AddWorldScreenDifficulty\" must be a Func<AWorldListItem, bool>.");
			}
			if (!(args[1] is string))
			{
				return new ArgumentException("ERROR: The first argument to \"AddWorldScreenDifficulty\" must be a string.");
			}
			if (args.Length >= 5)
			{
				AddWorldScreenDifficulty(args[1].ToString(), worldFunction, color, (int)args[4]);
			}
			else
			{
				AddWorldScreenDifficulty(args[1].ToString(), worldFunction, color);
			}
			return null;
		case "GetAbyssDarkness":
		case "GetDarknessIntensity":
		case "GetDarkness":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The argument to \"GetLightStrength\" must be a Player or an int.");
			}
			return GetDarknessIntensity(castPlayer(args[1]));
		case "AddLightLevel":
		case "AddAbyssLight":
		case "AddLightStrength":
		case "AddLight":
		case "AddAbyssLightLevel":
		case "AddAbyssLightStrength":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and light strength change as an int.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify light strength change as an int.");
			}
			if (!(args[2] is int light))
			{
				return new ArgumentException("ERROR: The second argument to \"AddLightStrength\" must be an int.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"AddLightStrength\" must be a Player or an int.");
			}
			AddAbyssLightStrength(castPlayer(args[1]), light);
			return null;
		case "BreathTick":
		case "AddBreathTick":
		case "AddAbyssBreathTick":
		case "AbyssBreathTick":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and Abyss breath tick change as a float.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify Abyss breath tick change as a float.");
			}
			if (!(args[2] is float breathTick))
			{
				return new ArgumentException("ERROR: The second argument to \"AddBreathTick\" must be a float.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"AddBreathTick\" must be a Player or an int.");
			}
			AddBreathTick(castPlayer(args[1]), breathTick);
			return null;
		case "FlightMult":
		case "AddFlightMult":
		case "AddFlightTimeMult":
		case "FlightTimeMult":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and flight mult change as a float.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify flight mult change as a float.");
			}
			if (!(args[2] is float flightMult))
			{
				return new ArgumentException("ERROR: The second argument to \"AddFlightMult\" must be a float.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"AddFlightMult\" must be a Player or an int.");
			}
			AddFlightTimeMult(castPlayer(args[1]), flightMult);
			return null;
		case "AddInfiniteFlight":
		case "EnableInfiniteFlight":
		case "ToggleInfiniteFlight":
		case "InfiniteFlight":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and if the player should gain infinite flight as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify if a player should gain infinite flight as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"InfiniteFlight\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"InfiniteFlight\" must be a Player or an int.");
			}
			bool fly = (bool)args[2];
			ToggleInfiniteFlight(castPlayer(args[1]), fly);
			return null;
		}
		case "GetRogueArmor":
		case "GetWearingRogueArmor":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The argument to \"GetRogueArmor\" must be a Player or an int.");
			}
			return GetWearingRogueArmor(castPlayer(args[1]));
		case "SetRogueArmor":
		case "SetWearingRogueArmor":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and if the player should be counted as wearing rogue armor as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify if a player should be counted as wearing rogue armor as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetRogueArmor\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetRogueArmor\" must be a Player or an int.");
			}
			bool roguearmor = (bool)args[2];
			SetWearingRogueArmor(castPlayer(args[1]), roguearmor);
			return null;
		}
		case "GetWearingPostMLSummonArmor":
		case "GetPostMLSummonArmor":
		case "GetPostMoonLordSummonArmor":
		case "GetWearingPostMoonLordSummonArmor":
		case "GetPostMLSummonerArmor":
		case "GetWearingPostMLSummonerArmor":
		case "GetPostMoonLordSummonerArmor":
		case "GetWearingPostMoonLordSummonerArmor":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The argument to \"GetPostMoonLordSummonerArmor\" must be a Player or an int.");
			}
			return GetWearingPostMLSummonerArmor(castPlayer(args[1]));
		case "SetWearingPostMLSummonArmor":
		case "SetPostMLSummonArmor":
		case "SetPostMoonLordSummonArmor":
		case "SetWearingPostMoonLordSummonArmor":
		case "SetPostMLSummonerArmor":
		case "SetWearingPostMLSummonerArmor":
		case "SetPostMoonLordSummonerArmor":
		case "SetWearingPostMoonLordSummonerArmor":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and if the player should be counted as wearing Post-Moon Lord summoner armor as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify if a player should be counted as wearing Post-Moon Lord summoner armor as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetPostMoonLordSummonerArmor\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetPostMoonLordSummonerArmor\" must be a Player or an int.");
			}
			bool summonarmor = (bool)args[2];
			SetWearingPostMLSummonerArmor(castPlayer(args[1]), summonarmor);
			return null;
		}
		case "GetRogueVelocity":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The argument to \"GetRogueVelocity\" must be a Player or an int.");
			}
			return GetRogueVelocity(castPlayer(args[1]));
		case "AddRogueVelocity":
		case "ModifyRogueVelocity":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and rogue velocity change as a float.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify rogue velocity change as a float.");
			}
			if (!(args[2] is float velocity))
			{
				return new ArgumentException("ERROR: The second argument to \"AddRogueVelocity\" must be a float.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"AddRogueVelocity\" must be a Player or an int.");
			}
			return AddRogueVelocity(castPlayer(args[1]), velocity);
		case "GetStealth":
		case "GetCurrentStealth":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetStealth\" must be a Player or an int.");
			}
			return GetCurrentStealth(castPlayer(args[1]));
		case "GetMaxStealth":
		case "GetStealthCap":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetMaxStealth\" must be a Player or an int.");
			}
			return GetMaxStealth(castPlayer(args[1]));
		case "AddMaxStealth":
		case "ModifyMaxStealth":
		case "ModifyStealthCap":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and rogue max stealth as a float.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify rogue max stealth as a float.");
			}
			if (!(args[2] is float maxStealth))
			{
				return new ArgumentException("ERROR: The second argument to \"AddMaxStealth\" must be a float.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"AddMaxStealth\" must be a Player or an int.");
			}
			return AddMaxStealth(castPlayer(args[1]), maxStealth);
		case "CanStealthStrike":
		case "StealthStrikeAvailable":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"CanStealthStrike\" must be a Player or an int.");
			}
			return CanStealthStrike(castPlayer(args[1]));
		case "SetProjectileStealth":
		case "SetStealthProjectile":
		case "SetProjectileStealthStrike":
		case "SetStealthStrikeProjectile":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Projectile and if the Projectile should be counted as a stealth strike as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify whether or not the projectile was created from a stealth strike as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetStealthProjectile\" must be a bool.");
			}
			if (!isValidProjectileArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetStealthProjectile\" must be a Projectile.");
			}
			bool ssEnabled = (bool)args[2];
			SetStealthProjectile(castProjectile(args[1]), ssEnabled);
			return null;
		}
		case "GetProjectileStealth":
		case "GetStealthProjectile":
		case "GetProjectileStealthStrike":
		case "GetStealthStrikeProjectile":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Projectile.");
			}
			if (!isValidProjectileArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetStealthProjectile\" must be a Projectile.");
			}
			return GetStealthProjectile(castProjectile(args[1]));
		case "UseStealth":
		case "ConsumeStealth":
		case "ConsumeStealthByAttacking":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"ConsumeStealth\" must be a Player or an int.");
			}
			ConsumeStealth(castPlayer(args[1]));
			return null;
		case "GetRage":
		case "GetRageCurrent":
		case "GetCurrentRage":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetRage\" must be a Player or an int.");
			}
			return GetRage(castPlayer(args[1]));
		case "GetAdrenaline":
		case "GetAdrenalineCurrent":
		case "GetCurrentAdrenaline":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetAdrenaline\" must be a Player or an int.");
			}
			return GetAdrenaline(castPlayer(args[1]));
		case "GetMaxRage":
		case "GetRageMax":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetMaxRage\" must be a Player or an int.");
			}
			return GetRageMax(castPlayer(args[1]));
		case "GetAdrenalineMax":
		case "GetMaxAdrenaline":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetMaxAdrenaline\" must be a Player or an int.");
			}
			return GetAdrenalineMax(castPlayer(args[1]));
		case "GetChargeMax":
		case "GetMaxCharge":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an Item object (or int index of an Item).");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetMaxCharge\" must be an Item or an int.");
			}
			return GetMaxCharge(castItem(args[1]));
		case "SetChargeMax":
		case "SetMaxCharge":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an Item and charge as a float or double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify charge as a float or double.");
			}
			if (!(args[2] is float) && !(args[2] is double))
			{
				return new ArgumentException("ERROR: The second argument to \"SetMaxCharge\" must be a float or a double.");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetMaxCharge\" must be an Item.");
			}
			float Charge = (float)args[2];
			SetMaxCharge(castItem(args[1]), Charge);
			return null;
		}
		case "GetCharge":
		case "GetCurrentCharge":
		case "GetChargeCurrent":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an Item object (or int index of an Item).");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetCharge\" must be an Item or an int.");
			}
			return GetCharge(castItem(args[1]));
		case "SetCharge":
		case "SetCurrentCharge":
		case "SetChargeCurrent":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an Item and charge as a float or double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify charge as a float or double.");
			}
			if (!(args[2] is float) && !(args[2] is double))
			{
				return new ArgumentException("ERROR: The second argument to \"SetCharge\" must be a float or a double.");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetCharge\" must be an Item.");
			}
			float Charge4 = (float)args[2];
			SetCharge(castItem(args[1]), Charge4);
			return null;
		}
		case "GetChargePerUse":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an Item object (or int index of an Item).");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetChargePerUse\" must be an Item or an int.");
			}
			return GetChargePerUse(castItem(args[1]));
		case "SetChargePerUse":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an Item and charge as a float or double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify charge as a float or double.");
			}
			if (!(args[2] is float) && !(args[2] is double))
			{
				return new ArgumentException("ERROR: The second argument to \"SetChargePerUse\" must be a float or a double.");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetChargePerUse\" must be an Item.");
			}
			float Charge2 = (float)args[2];
			SetChargePerUse(castItem(args[1]), Charge2);
			return null;
		}
		case "GetChargePerAltUse":
		case "GetChargePerUseAlt":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an Item object (or int index of an Item).");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetChargePerUse\" must be an Item or an int.");
			}
			return GetChargePerAltUse(castItem(args[1]));
		case "SetChargePerAltUse":
		case "SetChargeUseAlt":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an Item and charge as a float or double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify charge as a float or double.");
			}
			if (!(args[2] is float) && !(args[2] is double))
			{
				return new ArgumentException("ERROR: The second argument to \"SetChargePerUseAlt\" must be a float or a double.");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetChargePerUseAlt\" must be an Item.");
			}
			float Charge5 = (float)args[2];
			SetChargePerAltUse(castItem(args[1]), Charge5);
			return null;
		}
		case "GetChargeable":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an Item object (or int index of an Item).");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetChargeable\" must be an Item or an int.");
			}
			return GetChargeable(castItem(args[1]));
		case "SetChargeable":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an Item and if the item can be charged as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify the ability to charge as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetChargeable\" must be a bool.");
			}
			if (!isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetChargeable\" must be an Item.");
			}
			bool Charge3 = (bool)args[2];
			SetChargeable(castItem(args[1]), Charge3);
			return null;
		}
		case "GetRightClickListener":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetRightClickListener\" must be a Player or an int.");
			}
			return GetRightClickListener(castPlayer(args[1]));
		case "GetMouseWorldListener":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetMouseWorldListener\" must be a Player or an int.");
			}
			return GetMouseWorldListener(castPlayer(args[1]));
		case "GetMouseRight":
		case "GetRightClick":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetRightClick\" must be a Player or an int.");
			}
			return GetRightClick(castPlayer(args[1]));
		case "GetMouseWorld":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Player object (or int index of a Player).");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetMouseWorld\" must be a Player or an int.");
			}
			return GetMouseWorld(castPlayer(args[1]));
		case "SetRightClickListener":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player and whether or not the listener is active.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify status of the listener as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetRightClickListener\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetRightClickListener\" must be a Player.");
			}
			bool active2 = (bool)args[2];
			SetRightClickListener(castPlayer(args[1]), active2);
			return null;
		}
		case "SetMouseWorldListener":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player and whether or not the listener is active.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify status of the listener as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetMouseWorldListener\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetMouseWorldListener\" must be a Player.");
			}
			bool active = (bool)args[2];
			SetMouseWorldListener(castPlayer(args[1]), active);
			return null;
		}
		case "SetDamageReduction":
		case "SetDR":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both NPC ID as an int and damage reduction as a float or double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify damage reduction as a float or double.");
			}
			if (!(args[2] is float) && !(args[2] is double))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDamageReduction\" must be a float or a double.");
			}
			if (!castID(args[1], out var npcID))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDamageReduction\" must be an int or short ID.");
			}
			float DR2 = (float)args[2];
			return SetDamageReduction(npcID, DR2);
		}
		case "SetDRSpecific":
		case "SetDamageReductionSpecfic":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC and damage reduction as a float or double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify damage reduction as a float or double.");
			}
			if (!(args[2] is float) && !(args[2] is double))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDamageReduction\" must be a float or a double.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDamageReduction\" must be an NPC.");
			}
			float DR = (float)args[2];
			SetDamageReductionSpecific(castNPC(args[1]), DR);
			return null;
		}
		case "GetDRSpecific":
		case "GetDamageReduction":
		case "GetDamageReductionSpecific":
		case "GetDR":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetDamageReduction\" must be an NPC.");
			}
			return GetDamageReduction(castNPC(args[1]));
		case "SetNPCDefenseDamage":
		case "SetDefenseDamageNPC":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC and if the NPC can deal defense damage as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify the ability to deal defense damage as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDefenseDamageNPC\" must be a bool.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDefenseDamageNPC\" must be an NPC.");
			}
			bool ddEnabled2 = (bool)args[2];
			SetDefenseDamageNPC(castNPC(args[1]), ddEnabled2);
			return null;
		}
		case "GetNPCDefenseDamage":
		case "GetDefenseDamageNPC":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetDefenseDamageNPC\" must be an NPC.");
			}
			return GetDefenseDamageNPC(castNPC(args[1]));
		case "SetProjectileDefenseDamage":
		case "SetDefenseDamageProjectile":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Projectile and if the Projectile can deal defense damage as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify the ability to deal defense damage as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDefenseDamageProjectile\" must be a bool.");
			}
			if (!isValidProjectileArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDefenseDamageProjectile\" must be a Projectile.");
			}
			bool ddEnabled = (bool)args[2];
			SetDefenseDamageProjectile(castProjectile(args[1]), ddEnabled);
			return null;
		}
		case "GetProjectileDefenseDamage":
		case "GetDefenseDamageProjectile":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify a Projectile.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetDefenseDamageProjectile\" must be a Projectile.");
			}
			return GetDefenseDamageProjectile(castProjectile(args[1]));
		case "GetVulnerability":
		case "GetDebuffVulnerabilities":
		case "GetVulnerableDebuffs":
		case "GetVulnerabilities":
		case "GetDebuffVulnerability":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC and a debuff type as a string.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify a debuff type as a string.");
			}
			if (!(args[2] is string))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDebuffVulnerability\" must be a string.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDebuffVulnerability\" must be an NPC.");
			}
			return GetDebuffVulnerability(castNPC(args[1]), args[2].ToString());
		case "SetVulnerability":
		case "SetDebuffVulnerabilities":
		case "SetVulnerableDebuffs":
		case "SetVulnerabilities":
		case "SetDebuffVulnerability":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC, debuff type as a string, and whether to add or remove a vulnerability as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify a debuff type as a string, and whether to add or remove a vulnerability as a bool.");
			}
			if (args.Length < 4)
			{
				return new ArgumentNullException("ERROR: Must specify whether to add or remove a vulnerability as a bool.");
			}
			if (!(args[3] is bool) && args[3] != null)
			{
				return new ArgumentException("ERROR: The third argument to \"SetDebuffVulnerability\" must be a bool.");
			}
			if (!(args[2] is string))
			{
				return new ArgumentException("ERROR: The second argument to \"SetDebuffVulnerability\" must be a string.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetDebuffVulnerability\" must be an NPC.");
			}
			SetDebuffVulnerability(castNPC(args[1]), args[2].ToString(), (bool?)args[3]);
			return null;
		case "GetVanillaAIOverrideEnabled":
			return CalamityVanillaAIOverrideNPC.Enabled;
		case "SetVanillaAIOverrideEnabled":
			if (args.Length < 1)
			{
				return new ArgumentNullException("args", "ERROR: Must specify a bool parameter");
			}
			if (!(args[1] is bool aiOverrideEnabled))
			{
				return new ArgumentException("ERROR: The third argument to \"SetVanillaAIOverrideEnabled\" must be a bool.");
			}
			CalamityVanillaAIOverrideNPC.Enabled = aiOverrideEnabled;
			return null;
		case "GetCalamityAI":
		case "GetNewAI":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetCalamityAI\" must be an NPC.");
			}
			return GetCalamityAI(castNPC(args[1]));
		case "SetCalamityAI":
		case "SetNewAI":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC, an AI slot as an int, and a value for it as a float or a double.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify an AI slot as an int, and a value for it as a float or a double.");
			}
			if (args.Length < 4)
			{
				return new ArgumentNullException("ERROR: Must specify a value for the AI slot as a float or a double.");
			}
			if (!(args[3] is float) && !(args[3] is double))
			{
				return new ArgumentException("ERROR: The third argument to \"SetCalamityAI\" must be a float or a double.");
			}
			if (!(args[2] is int newValue))
			{
				return new ArgumentException("ERROR: The second argument to \"SetCalamityAI\" must be an int.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetCalamityAI\" must be an NPC.");
			}
			SetCalamityAI(castNPC(args[1]), newValue, (float)args[3]);
			return null;
		case "GetBossHealthBarsVisible":
		case "BossHealthBarVisible":
		case "BossHealthBarsVisible":
		case "GetBossHealthBarVisible":
			return BossHealthBarVisible();
		case "SetBossHealthBarsVisible":
		case "SetBossHealthBarVisible":
			if (args.Length < 2 || !(args[1] is bool bossBarEnabled))
			{
				return new ArgumentNullException("ERROR: Must specify a bool.");
			}
			return SetBossHealthBarVisible(bossBarEnabled);
		case "SetShouldCloseBossHealthBar":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC and whether or not the NPC's health bar should be closed as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify whether or not the NPC's health bar should be closed as a bool.");
			}
			if (!(args[2] is float) && !(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetShouldCloseBossHealthBar\" must be a bool.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetShouldCloseBossHealthBar\" must be an NPC.");
			}
			SetShouldCloseBossHealthBar(castNPC(args[1]), (bool)args[2]);
			return null;
		case "GetShouldCloseBossHealthbar":
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC.");
			}
			if (!isValidNPCArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"GetShouldCloseBossHealthBar\" must be an NPC.");
			}
			return GetShouldCloseBossHealthBar(castNPC(args[1]));
		case "GetDodgesDisabled":
		case "NoDodges":
		case "DodgesDisabled":
			return AreDodgesDisabled();
		case "DisableDodges":
		case "DisableAllDodges":
		case "SetDodgesDisabled":
			if (args.Length < 2 || !(args[1] is bool disableDodges))
			{
				return new ArgumentNullException("ERROR: Must specify a bool.");
			}
			return DisableAllDodges(disableDodges);
		case "SetPlayerColdImmune":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and if the player should be immune to Death Mode cold effects as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify if a player should be immune to Death Mode cold effects as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetPlayerColdImmune\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetPlayerColdImmune\" must be a Player or an int.");
			}
			bool coldImmune = (bool)args[2];
			SetPlayerColdImmune(castPlayer(args[1]), coldImmune);
			return null;
		}
		case "SetPlayerHeatImmune":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and if the player should be immune to Death Mode heat effects as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify if a player should be immune to Death Mode heat effects as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetPlayerHeatImmune\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetPlayerHeatImmune\" must be a Player or an int.");
			}
			bool heatImmune = (bool)args[2];
			SetPlayerHeatImmune(castPlayer(args[1]), heatImmune);
			return null;
		}
		case "SetPlayerDefenseDamageImmune":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify both a Player object (or int index of a Player) and if the player should be immune to defense damage as a bool.");
			}
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify if a player should be immune to defense damage as a bool.");
			}
			if (!(args[2] is bool))
			{
				return new ArgumentException("ERROR: The second argument to \"SetPlayerDefenseDamageImmune\" must be a bool.");
			}
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"SetPlayerDefenseDamageImmune\" must be a Player or an int.");
			}
			bool defenseDamageImmune = (bool)args[2];
			SetPlayerDefenseDamageImmune(castPlayer(args[1]), defenseDamageImmune);
			return null;
		}
		case "IsAcidRainActive":
		case "GetAcidRainActive":
		case "AcidRainActive":
			return AcidRainEvent.AcidRainEventIsOngoing;
		case "StartAcidRain":
			AcidRainEvent.TryStartEvent(forceRain: true);
			CalamityNetcode.SyncWorld();
			return true;
		case "StopAcidRain":
			if (AcidRainEvent.AcidRainEventIsOngoing)
			{
				AcidRainEvent.AccumulatedKillPoints = 0;
				AcidRainEvent.HasTriedToSummonOldDuke = false;
				AcidRainEvent.UpdateInvasion(win: false);
			}
			return true;
		case "AbominationnClearEvents":
		{
			bool acidRainEventIsOngoing = AcidRainEvent.AcidRainEventIsOngoing;
			bool canClear = Convert.ToBoolean(args[1]);
			if (acidRainEventIsOngoing & canClear)
			{
				AcidRainEvent.AccumulatedKillPoints = 0;
				AcidRainEvent.HasTriedToSummonOldDuke = false;
				AcidRainEvent.UpdateInvasion(win: false);
			}
			return acidRainEventIsOngoing;
		}
		case "RegisterEnchantment":
		case "CreateEnchantment":
			EnchantmentManager.ConstructFromModcall(args.Skip(1));
			return null;
		case "MakeItemExhumable":
		{
			if (args.Length != 3)
			{
				return new ArgumentNullException("ERROR: Must specify two Item types as an int.");
			}
			if (!castID(args[1], out var toExhume))
			{
				return new ArgumentException("ERROR: The first argument to \"MakeItemExhumable\" must be an int or short ID.");
			}
			if (!castID(args[2], out var result))
			{
				return new ArgumentException("ERROR: The second argument to \"MakeItemExhumable\" must be an int or short ID.");
			}
			EnchantmentManager.ItemUpgradeRelationship[toExhume] = result;
			return null;
		}
		case "DeclareMinibossForHealthBar":
		case "DeclareMiniboss":
		{
			if (args.Length != 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC type as an int.");
			}
			if (!castID(args[1], out var npcType11))
			{
				return new ArgumentException("ERROR: The first argument to \"DeclareMiniboss\" must be an int or short ID.");
			}
			BossHealthBarManager.MinibossHPBarList.Add(npcType11);
			return null;
		}
		case "ExcludeBossFromHealthBar":
		{
			if (args.Length != 2)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC type as an int.");
			}
			if (!castID(args[1], out var npcType10))
			{
				return new ArgumentException("ERROR: The first argument to \"ExcludeBossFromHealthBar\" must be an int or short ID.");
			}
			BossHealthBarManager.BossExclusionList.Add(npcType10);
			return null;
		}
		case "DeclareOneToManyRelationshipForHealthBar":
		{
			if (args.Length < 3)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC type as an int for the first argument and the other NPC types in the relationship as ints for the rest of the arguments.");
			}
			if (!args.Skip(1).All((object a) => castID(a, out var _)))
			{
				return new ArgumentException("ERROR: All secondary and onward arguments to \"DeclareOneToManyRelationshipForHealthBar\" must be int or short IDs.");
			}
			castID(args[1], out var npcType9);
			int[] npcsInRelationship = (from a in args.Skip(2)
				select (int)a).ToArray();
			BossHealthBarManager.OneToMany[npcType9] = npcsInRelationship;
			return null;
		}
		case "DeclareSpecialHPCalculationDecisionForHealthBar":
			if (args.Length != 3)
			{
				return new ArgumentNullException("ERROR: Must specify both a usage requirement as a Func<NPC, bool> and a health calculator function as a Func<NPC, bool, long>.");
			}
			if (!(args[1] is Func<NPC, bool> usageRequirement))
			{
				return new ArgumentException("ERROR: The first argument to \"DeclareSpecialHPCalculationDecisionForHealthBar\" must be a Func<NPC, bool>.");
			}
			if (!(args[2] is Func<NPC, bool, long> healthCalculatorFunction))
			{
				return new ArgumentException("ERROR: The first argument to \"DeclareSpecialHPCalculationDecisionForHealthBar\" must be a Func<NPC, bool, long>.");
			}
			BossHealthBarManager.SpecialHPRequirements[usageRequirement.Invoke] = healthCalculatorFunction.Invoke;
			return null;
		case "CreateNameExtensionHandlerForHealthBar":
		{
			if (args.Length < 4)
			{
				return new ArgumentNullException("ERROR: Must specify a extension name as a string, the main NPC type as an int, and the other NPC types to check for as ints the rest of the arguments.");
			}
			if (!(args[1] is LocalizedText name))
			{
				return new ArgumentException("ERROR: The first argument to \"CreateNameExtensionHandlerForHealthBar\" must be a LocalizedText.");
			}
			if (!castID(args[1], out var npcType8))
			{
				return new ArgumentException("ERROR: The second argument to \"CreateNameExtensionHandlerForHealthBar\" must be an int or short ID.");
			}
			if (!args.Skip(3).All((object a) => a is int))
			{
				return new ArgumentException("ERROR: All ternary and onward arguments to \"CreateNameExtensionHandlerForHealthBar\" must be ints.");
			}
			int[] npcsToCheckFor = (from a in args.Skip(3)
				select (int)a).ToArray();
			BossHealthBarManager.EntityExtensionHandler[npcType8] = new BossHealthBarManager.BossEntityExtension(name, npcsToCheckFor);
			return null;
		}
		case "GetBossRushEntries":
		{
			List<(int, int, Action<int>, int, bool, float, int[], int[])> entries3 = new List<(int, int, Action<int>, int, bool, float, int[], int[])>();
			{
				foreach (BossRushEvent.Boss boss in BossRushEvent.Bosses)
				{
					int[] deathEntries = (BossRushEvent.BossIDsAfterDeath.ContainsKey(boss.EntityID) ? BossRushEvent.BossIDsAfterDeath[boss.EntityID] : null);
					entries3.Add((boss.EntityID, (int)boss.ToChangeTimeTo, boss.SpawnContext.Invoke, boss.SpecialSpawnCountdown, boss.UsesSpecialSound, boss.DimnessFactor, boss.HostileNPCsToNotDelete.ToArray(), deathEntries));
				}
				return entries3;
			}
		}
		case "SetBossRushEntries":
			if (args.Length != 2)
			{
				return new ArgumentNullException("ERROR: Must specify a list of bosses as a List<(int, int, Action<int>, int, bool, int[], int[])>.");
			}
			if (!(args[1] is List<(int, int, Action<int>, int, bool, float, int[], int[])> entries2))
			{
				return new ArgumentException("ERROR: The first argument to \"SetBossRushEntries\" must be a List<(int, int, Action<int>, int, bool, int[], int[])>.");
			}
			BossRushEvent.Bosses.Clear();
			BossRushEvent.BossIDsAfterDeath.Clear();
			foreach (var entry in entries2)
			{
				if (entry.Rest.Item1 != null)
				{
					BossRushEvent.BossIDsAfterDeath[entry.Item1] = entry.Rest.Item1;
				}
				BossRushEvent.Bosses.Add(new BossRushEvent.Boss(entry.Item1, (BossRushEvent.TimeChangeContext)entry.Item2, entry.Item3.Invoke, entry.Item4, entry.Item5, entry.Item6, entry.Item7));
			}
			return null;
		case "CreateCustomDeathEffectForBossRush":
		{
			if (args.Length != 3)
			{
				return new ArgumentNullException("ERROR: Must specify both an NPC type and an Action<NPC> that determines what happens when the NPC is killed.");
			}
			if (!castID(args[1], out var npcType7))
			{
				return new ArgumentException("ERROR: The first argument to \"CreateCustomDeathEffectForBossRush\" must be an int or short ID.");
			}
			if (!(args[2] is Action<NPC> deathEffect))
			{
				return new ArgumentException("ERROR: The first argument to \"CreateCustomDeathEffectForBossRush\" must be an Action<NPC>.");
			}
			BossRushEvent.BossDeathEffects[npcType7] = deathEffect;
			return null;
		}
		case "LoadParticleInstances":
			CalamityMod.Log.Warn((object)"This mod call is deprecated. Calamity automatically registers particles.");
			return null;
		case "RegisterModCooldowns":
			CalamityMod.Log.Warn((object)"This mod call is deprecated. Calamity automatically registers cooldowns.");
			return null;
		case "GetSummonerNerfDisabledByItem":
			if (args.Length != 2 || !isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: Must specify a valid item to check status of.");
			}
			return GetSummonerNerfDisabledByItem(castItem(args[1]).type);
		case "GetSummonerNerfDisabledByMinion":
			if (args.Length != 2 || !isValidProjectileArg(args[1]))
			{
				return new ArgumentException("ERROR: Must specify a valid projectile to check status of.");
			}
			return GetSummonerNerfDisabledByMinion(castProjectile(args[1]).type);
		case "SetSummonerNerfDisabledByItem":
			if (args.Length < 2 || !isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: Must specify a valid item to set the status of.");
			}
			if (args.Length != 3 || !(args[2] is bool disableNerf3))
			{
				return new ArgumentException("ERROR: Must specify a bool that determines whether the summoner nerf is disabled.");
			}
			return SetSummonerNerfDisabledByItem(castItem(args[1]).type, disableNerf3);
		case "SetSummonerNerfDisabledByMinion":
			if (args.Length < 2 || !isValidItemArg(args[1]))
			{
				return new ArgumentException("ERROR: Must specify a valid projectile to set the status of.");
			}
			if (args.Length != 3 || !(args[2] is bool disableNerf2))
			{
				return new ArgumentException("ERROR: Must specify a bool that determines whether the summoner nerf is disabled.");
			}
			return SetSummonerNerfDisabledByItem(castItem(args[1]).type, disableNerf2);
		case "IsOnAmalgamBuffList":
		{
			if (args.Length != 2 || !castID(args[1], out var buffType7))
			{
				return new ArgumentException("ERROR: Must specify a valid buff ID to check status of.");
			}
			return IsOnAmalgamBuffList(buffType7);
		}
		case "IsPersistentBuff":
		case "IsOnPersistentBuffList":
		{
			if (args.Length != 2 || !castID(args[1], out var buffType6))
			{
				return new ArgumentException("ERROR: Must specify a valid buff ID to check status of.");
			}
			return IsOnPersistentBuffList(buffType6);
		}
		case "SetAmalgamBuffList":
		{
			if (args.Length < 2 || !castID(args[1], out var buffType5))
			{
				return new ArgumentException("ERROR: Must specify a valid buff ID to set the status of.");
			}
			if (args.Length != 3 || !(args[2] is bool shouldBeListed))
			{
				return new ArgumentException("ERROR: Must specify a bool that determines whether the amalgam should enable extend the duration of this buff.");
			}
			return SetAmalgamBuffList(buffType5, shouldBeListed);
		}
		case "SetPersistentBuffList":
		{
			if (args.Length < 2 || !castID(args[1], out var buffType4))
			{
				return new ArgumentException("ERROR: Must specify a valid buff ID to set the status of.");
			}
			if (args.Length != 3 || !(args[2] is bool isPersistent))
			{
				return new ArgumentException("ERROR: Must specify a bool that determines whether the buff is persistent after death for the Amalgam to properly reset.");
			}
			return SetPersistentBuffList(buffType4, isPersistent);
		}
		case "CreateCodebreakerDialogOption":
			if (args.Length == 4)
			{
				if (!(args[1] is string inquiry) || !(args[2] is string response) || !(args[3] is Func<bool> condition))
				{
					throw new ArgumentException("ERROR: Must specify a string that determines the inquiry, a string that determines the response, and a Func<bool> that determines the condition for the three argument call.");
				}
				DraedonDialogRegistry.DialogOptions.Add(new DraedonDialogEntry(inquiry, response, condition));
			}
			else
			{
				if (args.Length != 3)
				{
					throw new ArgumentException("ERROR: Must specify either two or three arguments.");
				}
				if (!(args[1] is string localizationKey) || !(args[2] is Func<bool> condition2))
				{
					throw new ArgumentException("ERROR: Must specify a string that determines the localization key and a Func<bool> that determines the condition for the two argument call.");
				}
				DraedonDialogRegistry.DialogOptions.Add(new DraedonDialogEntry(localizationKey, condition2));
			}
			return null;
		case "AddToVeneratedLocketBanlist":
			if (args.Length < 2)
			{
				return new ArgumentException("ERROR: Not enough arguments!");
			}
			if (!(args[1] is int itemType))
			{
				return new ArgumentException("ERROR: Must specify a valid item type as an int index of the item.");
			}
			return AddToVeneratedLocketBanlist(itemType);
		case "DebuffIcon":
		case "DisplayDebuff":
		case "AddDebuffDisplay":
		case "AddToDebuffDisplay":
		case "RegisterDebuff":
			if (args.Length < 2 || !(args[1] is string texturePath2))
			{
				return new ArgumentException("ERROR: The first argument to \"RegisterDebuff\" must be the texture path to the debuff sprite as a string");
			}
			if (args.Length != 3 || !(args[2] is Predicate<NPC> debuffCheck))
			{
				return new ArgumentException("ERROR: The second argument to \"RegisterDebuff\" Must be a Predicate<NPC> that checks if an NPC meets the conditions for the debuff.");
			}
			RegisterDebuff(texturePath2, debuffCheck);
			return null;
		case "AddNPCShop":
		case "AddShop":
		case "RegisterTownNPCShop":
		case "RegisterNPCShop":
		case "AddTownNPCShop":
		case "RegisterShop":
			if (args.Length < 2 || !(args[1] is int npc))
			{
				return new ArgumentException("ERROR: The first argument to \"RegisterTownNPCShop\" must be the id of the NPC");
			}
			if (args.Length < 3 || !(args[2] is Predicate<Player> shopCheck))
			{
				return new ArgumentException("ERROR: The second argument to \"RegisterTownNPCShop\" Must be a Predicate<Player> that checks if the new shop variable bool is true or not.");
			}
			if (args.Length != 4 || !(args[3] is Action<Player, bool> shopSet))
			{
				return new ArgumentException("ERROR: The third argument to \"RegisterTownNPCShop\" Must be a Action<Player, bool> that is able to get and set the player's shop variable bool.");
			}
			RegisterTownNPCShop(npc, shopCheck, shopSet);
			return null;
		case "SendNPCShopAlert":
		case "SetNewShopVariable":
		case "SendNPCAlert":
			if (args.Length < 2 || !(args[1] is int[] npcs))
			{
				return new ArgumentException("ERROR: The first argument to \"SetNewShopVariable\" must be an integer array of npc ids that should be alerted.");
			}
			if (args.Length != 3 || !(args[2] is bool alreadySet))
			{
				return new ArgumentException("ERROR: The second argument to \"SetNewShopVariable\" Must be a bool that determines if the shop alert should show.");
			}
			CalamityGlobalTownNPC.SetNewShopVariable(npcs, alreadySet);
			return null;
		case "BossHealthMultiplier":
		case "GetBossHealthBoost":
		case "BossHealthBoost":
		case "GetBossHealthMultiplier":
			return CalamityServerConfig.Instance.BossHealthBoost;
		case "GetPowerup":
		case "GetBooster":
		case "HasPermanentPowerup":
		case "GetPermanentPowerup":
		case "HasPermanentBooster":
		case "GetPermanentBooster":
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"HasPermanentPowerup\" must be a Player.");
			}
			if (!(args[2] is string powerupName2))
			{
				return new ArgumentException("ERROR: The second argument to \"HasPermanentPowerup\" must be a string.");
			}
			return HasPermanentPowerup(castPlayer(args[1]), powerupName2);
		case "SetPowerup":
		case "SetBooster":
		case "SetPermanentPowerup":
		case "SetPermanentBooster":
			if (!isValidPlayerArg(args[1]))
			{
				return new ArgumentException("ERROR: The first argument to \"HasPermanentPowerup\" must be a Player.");
			}
			if (!(args[2] is string powerupName))
			{
				return new ArgumentException("ERROR: The second argument to \"HasPermanentPowerup\" must be a string.");
			}
			if (!(args[3] is bool value))
			{
				return new ArgumentException("ERROR: The third argument to \"HasPermanentPowerup\" must be a bool.");
			}
			SetPermanentPowerup(castPlayer(args[1]), powerupName, value);
			return null;
		case "AddAndrombaSolution":
		case "RegisterAndroomba":
		case "AddAndroombaState":
		case "RegisterAndroombaState":
		case "RegisterAndroombaSolution":
		case "AddAndroomba":
			if (!(args[1] is int itemID))
			{
				return new ArgumentException("ERROR: The first argument to \"RegisterAndroombaSolution\" must be the ID of a solution as an int.");
			}
			if (!(args[2] is string texturePath))
			{
				return new ArgumentException("ERROR: The second argument to \"RegisterAndroombaSolution\" must be a string path.");
			}
			if (!(args[3] is Action<NPC> NPCaction))
			{
				return new ArgumentException("ERROR: The third argument to \"RegisterAndroombaSolution\" must be an Action<NPC>.");
			}
			AndroombaFriendly.customConversionTypes.Add((itemID, texturePath, NPCaction));
			return null;
		case "AddToBossHPScalingConfig":
		case "AddMinionToHPScalingConfig":
		case "AddMinibossToHPScalingConfig":
		{
			if (args.Length < 2)
			{
				return new ArgumentNullException("ERROR: Must specify an NPC id/type as an int or short ID. Example: NPCType<SomeBossMinion>()");
			}
			if (!castID(args[1], out var npcType6))
			{
				return new ArgumentException("ERROR: The first argument to \"AddToBossHPScalingConfig\" must be an int or short ID.");
			}
			return AddToHPScaling(npcType6);
		}
		default:
			return new ArgumentException("ERROR: Invalid method name.");
		}
		static bool castID(object o, out int id)
		{
			id = -1;
			if (!(o is int) && !(o is short))
			{
				return false;
			}
			if (o is short shortID)
			{
				id = shortID;
			}
			if (o is int intID)
			{
				id = intID;
			}
			return true;
		}
		static Item castItem(object o)
		{
			if (o is int i)
			{
				return Main.item[i];
			}
			if (o is Item it)
			{
				return it;
			}
			return null;
		}
		static NPC castNPC(object o)
		{
			if (o is int i)
			{
				return Main.npc[i];
			}
			if (o is NPC n)
			{
				return n;
			}
			return null;
		}
		static Player castPlayer(object o)
		{
			if (o is int i)
			{
				return Main.player[i];
			}
			if (o is Player p)
			{
				return p;
			}
			return null;
		}
		static Projectile castProjectile(object o)
		{
			if (o is int i)
			{
				return Main.projectile[i];
			}
			if (o is Projectile p)
			{
				return p;
			}
			return null;
		}
		static bool isValidItemArg(object o)
		{
			if (!(o is int))
			{
				return o is Item;
			}
			return true;
		}
		static bool isValidNPCArg(object o)
		{
			if (!(o is int))
			{
				return o is NPC;
			}
			return true;
		}
		static bool isValidPlayerArg(object o)
		{
			if (!(o is int))
			{
				return o is Player;
			}
			return true;
		}
		static bool isValidProjectileArg(object o)
		{
			if (!(o is int))
			{
				return o is Projectile;
			}
			return true;
		}
	}
}
