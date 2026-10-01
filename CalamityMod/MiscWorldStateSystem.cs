using System.Collections.Generic;
using System.IO;
using CalamityMod.CustomRecipes;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod;

public class MiscWorldStateSystem : ModSystem
{
	public override void ClearWorld()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.holyBoss = -1;
		CalamityGlobalNPC.doughnutBoss = -1;
		CalamityGlobalNPC.doughnutBossDefender = -1;
		CalamityGlobalNPC.doughnutBossHealer = -1;
		CalamityGlobalNPC.voidBoss = -1;
		CalamityGlobalNPC.energyFlame = -1;
		CalamityGlobalNPC.hiveMind = -1;
		CalamityGlobalNPC.astrumAureus = -1;
		CalamityGlobalNPC.scavenger = -1;
		for (int i = 0; i < CalamityGlobalNPC.bobbitWormBottom.Length; i++)
		{
			CalamityGlobalNPC.bobbitWormBottom[i] = -1;
		}
		CalamityGlobalNPC.DoGHead = -1;
		CalamityGlobalNPC.SCal = -1;
		CalamityGlobalNPC.ghostBoss = -1;
		CalamityGlobalNPC.laserEye = -1;
		CalamityGlobalNPC.fireEye = -1;
		CalamityGlobalNPC.brimstoneElemental = -1;
		CalamityGlobalNPC.signus = -1;
		CalamityGlobalNPC.draedonExoMechPrimePlasmaCannon = -1;
		CalamityGlobalNPC.draedonExoMechPrime = -1;
		CalamityGlobalNPC.draedonExoMechTwinGreen = -1;
		CalamityGlobalNPC.draedonExoMechTwinRed = -1;
		CalamityGlobalNPC.draedonExoMechWorm = -1;
		CalamityGlobalNPC.adultEidolonWyrmHead = -1;
		BossRushEvent.BossRushStage = 0;
		BossRushEvent.BossRushActive = false;
		BossRushEvent.BossRushSpawnCountdown = 180;
		BossRushEvent.HostileProjectileKillCounter = 0;
		CustomTemple.NewAlterPosition = Point.Zero;
		Abyss.AbyssChasmBottom = 0;
		SulphurousSea.YStart = 0;
		Abyss.AtLeftSideOfWorld = false;
		CalamityWorld.spawnedBandit = false;
		CalamityWorld.foundHomePermafrost = false;
		CalamityWorld.catName = false;
		CalamityWorld.dogName = false;
		CalamityWorld.bunnyName = false;
		CalamityWorld.revenge = false;
		CalamityWorld.TalkedToDraedon = false;
		CalamityWorld.death = false;
		CalamityWorld.armageddon = false;
		AcidRainEvent.AcidRainEventIsOngoing = false;
		AcidRainEvent.CountdownUntilForcedAcidRain = 0;
		CalamityWorld.HasGeneratedLuminitePlanetoids = false;
	}

	public override void SaveWorldData(TagCompound tag)
	{
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		List<string> downed = new List<string>();
		if (CalamityWorld.TalkedToDraedon)
		{
			downed.Add("TalkedToDraedon");
		}
		if (CalamityWorld.revenge)
		{
			downed.Add("revenge");
		}
		if (CalamityWorld.death)
		{
			downed.Add("death");
		}
		if (Abyss.AtLeftSideOfWorld)
		{
			downed.Add("abyssSide");
		}
		if (BossRushEvent.BossRushActive)
		{
			downed.Add("bossRushActive");
		}
		if (AcidRainEvent.AcidRainEventIsOngoing)
		{
			downed.Add("acidRain");
		}
		if (CalamityWorld.spawnedBandit)
		{
			downed.Add("bandit");
		}
		if (CalamityWorld.foundHomePermafrost)
		{
			downed.Add("archmageHome");
		}
		if (CalamityWorld.catName)
		{
			downed.Add("catName");
		}
		if (CalamityWorld.dogName)
		{
			downed.Add("dogName");
		}
		if (CalamityWorld.bunnyName)
		{
			downed.Add("bunnyName");
		}
		if (AcidRainEvent.HasTriedToSummonOldDuke)
		{
			downed.Add("spawnedBoomer");
		}
		if (AcidRainEvent.HasStartedAcidicDownpour)
		{
			downed.Add("startDownpour");
		}
		if (AcidRainEvent.HasBeenForceStartedByEoCDefeat)
		{
			downed.Add("forcedRain");
		}
		if (AcidRainEvent.OldDukeHasBeenEncountered)
		{
			downed.Add("encounteredOldDuke");
		}
		if (CalamityWorld.HasGeneratedLuminitePlanetoids)
		{
			downed.Add("HasGeneratedLuminitePlanetoids");
		}
		downed.AddWithCondition("IsWorldAfterDraedonUpdate", CalamityWorld.IsWorldAfterDraedonUpdate);
		RecipeUnlockHandler.Save(downed);
		tag["downed"] = downed;
		tag["abyssChasmBottom"] = Abyss.AbyssChasmBottom;
		tag["SulphSeaYStart"] = SulphurousSea.YStart;
		tag["AstralYStart"] = AstralBiome.YStart;
		tag["acidRainPoints"] = AcidRainEvent.AccumulatedKillPoints;
		tag["Reforges"] = CalamityWorld.Reforges;
		tag["MoneyStolenByBandit"] = CalamityWorld.MoneyStolenByBandit;
		tag["SunkenSeaLabCenter"] = CalamityWorld.SunkenSeaLabCenter;
		tag["PlanetoidLabCenter"] = CalamityWorld.PlanetoidLabCenter;
		tag["JungleLabCenter"] = CalamityWorld.JungleLabCenter;
		tag["HellLabCenter"] = CalamityWorld.HellLabCenter;
		tag["IceLabCenter"] = CalamityWorld.IceLabCenter;
		tag["CavernLabCenter"] = CalamityWorld.CavernLabCenter;
	}

	public override void LoadWorldData(TagCompound tag)
	{
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		IList<string> list = tag.GetList<string>("downed");
		CalamityWorld.TalkedToDraedon = list.Contains("TalkedToDraedon");
		CalamityWorld.revenge = list.Contains("revenge");
		CalamityWorld.death = list.Contains("death");
		Abyss.AtLeftSideOfWorld = list.Contains("abyssSide");
		BossRushEvent.BossRushActive = list.Contains("bossRushActive");
		AcidRainEvent.AcidRainEventIsOngoing = list.Contains("acidRain");
		CalamityWorld.spawnedBandit = list.Contains("bandit");
		CalamityWorld.foundHomePermafrost = list.Contains("archmageHome");
		CalamityWorld.catName = list.Contains("catName");
		CalamityWorld.dogName = list.Contains("dogName");
		CalamityWorld.bunnyName = list.Contains("bunnyName");
		AcidRainEvent.HasTriedToSummonOldDuke = list.Contains("spawnedBoomer");
		AcidRainEvent.HasStartedAcidicDownpour = list.Contains("startDownpour");
		AcidRainEvent.HasBeenForceStartedByEoCDefeat = list.Contains("forcedRain");
		AcidRainEvent.OldDukeHasBeenEncountered = list.Contains("encounteredOldDuke");
		CalamityWorld.HasGeneratedLuminitePlanetoids = list.Contains("HasGeneratedLuminitePlanetoids");
		CalamityWorld.IsWorldAfterDraedonUpdate = list.Contains("IsWorldAfterDraedonUpdate");
		RecipeUnlockHandler.Load(list);
		Abyss.AbyssChasmBottom = tag.GetInt("abyssChasmBottom");
		SulphurousSea.YStart = tag.GetInt("SulphSeaYStart");
		AstralBiome.YStart = tag.GetInt("AstralYStart");
		AcidRainEvent.AccumulatedKillPoints = tag.GetInt("acidRainPoints");
		CalamityWorld.Reforges = tag.GetInt("Reforges");
		CalamityWorld.MoneyStolenByBandit = tag.GetInt("MoneyStolenByBandit");
		CalamityWorld.SunkenSeaLabCenter = tag.Get<Vector2>("SunkenSeaLabCenter");
		CalamityWorld.PlanetoidLabCenter = tag.Get<Vector2>("PlanetoidLabCenter");
		CalamityWorld.JungleLabCenter = tag.Get<Vector2>("JungleLabCenter");
		CalamityWorld.HellLabCenter = tag.Get<Vector2>("HellLabCenter");
		CalamityWorld.IceLabCenter = tag.Get<Vector2>("IceLabCenter");
		CalamityWorld.CavernLabCenter = tag.Get<Vector2>("CavernLabCenter");
	}

	public override void NetSend(BinaryWriter writer)
	{
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		BitsByte flags = new BitsByte
		{
			[0] = DownedBossSystem.downedDesertScourge,
			[1] = DownedBossSystem.downedHiveMind,
			[2] = DownedBossSystem.downedPerforator,
			[3] = DownedBossSystem.downedSlimeGod,
			[4] = DownedBossSystem.downedCryogen,
			[5] = DownedBossSystem.downedBrimstoneElemental,
			[6] = DownedBossSystem.downedCalamitasClone,
			[7] = DownedBossSystem.downedLeviathan
		};
		BitsByte flags2 = new BitsByte
		{
			[0] = DownedBossSystem.downedDoG,
			[1] = DownedBossSystem.downedPlaguebringer,
			[2] = DownedBossSystem.downedGuardians,
			[3] = DownedBossSystem.downedProvidence,
			[4] = DownedBossSystem.downedCeaselessVoid,
			[5] = DownedBossSystem.downedStormWeaver,
			[6] = DownedBossSystem.downedSignus,
			[7] = DownedBossSystem.downedYharon
		};
		BitsByte flags3 = new BitsByte
		{
			[0] = DownedBossSystem.downedCalamitas,
			[1] = DownedBossSystem.downedDragonfolly,
			[2] = DownedBossSystem.downedCrabulon,
			[3] = DownedBossSystem.downedBetsy,
			[4] = DownedBossSystem.downedRavager,
			[5] = false,
			[6] = false,
			[7] = false
		};
		BitsByte flags4 = new BitsByte
		{
			[0] = false,
			[1] = false,
			[2] = false,
			[3] = false,
			[4] = false,
			[5] = false,
			[6] = false,
			[7] = CalamityWorld.revenge
		};
		BitsByte flags5 = new BitsByte
		{
			[0] = DownedBossSystem.downedAstrumDeus,
			[1] = CalamityWorld.spawnedBandit,
			[2] = false,
			[3] = AcidRainEvent.HasStartedAcidicDownpour,
			[4] = false,
			[5] = DownedBossSystem.downedPolterghast,
			[6] = CalamityWorld.death,
			[7] = DownedBossSystem.downedGSS
		};
		BitsByte flags6 = new BitsByte
		{
			[0] = Abyss.AtLeftSideOfWorld,
			[1] = DownedBossSystem.downedAquaticScourge,
			[2] = DownedBossSystem.downedAstrumAureus,
			[3] = false,
			[4] = CalamityWorld.armageddon,
			[5] = false,
			[6] = false,
			[7] = false
		};
		BitsByte flags7 = new BitsByte
		{
			[0] = BossRushEvent.BossRushActive,
			[1] = DownedBossSystem.downedBoomerDuke,
			[2] = DownedBossSystem.downedCLAM,
			[3] = false,
			[4] = AcidRainEvent.AcidRainEventIsOngoing,
			[5] = DownedBossSystem.downedEoCAcidRain,
			[6] = DownedBossSystem.downedAquaticScourgeAcidRain,
			[7] = AcidRainEvent.HasTriedToSummonOldDuke
		};
		BitsByte flags8 = new BitsByte
		{
			[0] = AcidRainEvent.HasBeenForceStartedByEoCDefeat,
			[1] = false,
			[2] = DownedBossSystem.downedSecondSentinels,
			[3] = CalamityWorld.foundHomePermafrost,
			[4] = DownedBossSystem.downedCLAMHardMode,
			[5] = CalamityWorld.catName,
			[6] = CalamityWorld.dogName,
			[7] = CalamityWorld.bunnyName
		};
		BitsByte flags9 = new BitsByte
		{
			[0] = false,
			[1] = false,
			[2] = false,
			[3] = false,
			[4] = false,
			[5] = false,
			[6] = false,
			[7] = false
		};
		BitsByte flags10 = new BitsByte
		{
			[0] = false,
			[1] = false,
			[2] = AcidRainEvent.OldDukeHasBeenEncountered,
			[3] = false,
			[4] = false,
			[5] = false,
			[6] = false,
			[7] = false
		};
		BitsByte flags11 = new BitsByte
		{
			[0] = false,
			[1] = CalamityWorld.HasGeneratedLuminitePlanetoids,
			[2] = DownedBossSystem.downedPrimordialWyrm,
			[3] = DownedBossSystem.downedExoMechs,
			[4] = DownedBossSystem.downedAres,
			[5] = DownedBossSystem.downedThanatos,
			[6] = DownedBossSystem.downedArtemisAndApollo,
			[7] = CalamityWorld.TalkedToDraedon
		};
		BitsByte flags12 = new BitsByte
		{
			[0] = DownedBossSystem.downedCragmawMire,
			[1] = DownedBossSystem.downedMauler,
			[2] = DownedBossSystem.downedNuclearTerror,
			[3] = DownedBossSystem.downedBossRush,
			[4] = CalamityWorld.DraedonMechdusa
		};
		writer.Write(flags);
		writer.Write(flags2);
		writer.Write(flags3);
		writer.Write(flags4);
		writer.Write(flags5);
		writer.Write(flags6);
		writer.Write(flags7);
		writer.Write(flags8);
		writer.Write(flags9);
		writer.Write(flags10);
		writer.Write(flags11);
		writer.Write(flags12);
		RecipeUnlockHandler.SendData(writer);
		writer.Write(Abyss.AbyssChasmBottom);
		writer.Write(SulphurousSea.YStart);
		writer.Write(AstralBiome.YStart);
		writer.Write(AcidRainEvent.AccumulatedKillPoints);
		writer.Write(CalamityWorld.Reforges);
		writer.Write(CalamityWorld.MoneyStolenByBandit);
		writer.Write(CalamityWorld.DraedonSummonCountdown);
		writer.Write((int)CalamityWorld.DraedonMechToSummon);
		writer.WriteVector2(CalamityWorld.DraedonSummonPosition);
		writer.WriteVector2(CalamityWorld.SunkenSeaLabCenter);
		writer.WriteVector2(CalamityWorld.PlanetoidLabCenter);
		writer.WriteVector2(CalamityWorld.JungleLabCenter);
		writer.WriteVector2(CalamityWorld.HellLabCenter);
		writer.WriteVector2(CalamityWorld.IceLabCenter);
		writer.WriteVector2(CalamityWorld.CavernLabCenter);
	}

	public override void NetReceive(BinaryReader reader)
	{
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		BitsByte flags = reader.ReadByte();
		DownedBossSystem.downedDesertScourge = flags[0];
		DownedBossSystem.downedHiveMind = flags[1];
		DownedBossSystem.downedPerforator = flags[2];
		DownedBossSystem.downedSlimeGod = flags[3];
		DownedBossSystem.downedCryogen = flags[4];
		DownedBossSystem.downedBrimstoneElemental = flags[5];
		DownedBossSystem.downedCalamitasClone = flags[6];
		DownedBossSystem.downedLeviathan = flags[7];
		BitsByte flags2 = reader.ReadByte();
		DownedBossSystem.downedDoG = flags2[0];
		DownedBossSystem.downedPlaguebringer = flags2[1];
		DownedBossSystem.downedGuardians = flags2[2];
		DownedBossSystem.downedProvidence = flags2[3];
		DownedBossSystem.downedCeaselessVoid = flags2[4];
		DownedBossSystem.downedStormWeaver = flags2[5];
		DownedBossSystem.downedSignus = flags2[6];
		DownedBossSystem.downedYharon = flags2[7];
		BitsByte flags3 = reader.ReadByte();
		DownedBossSystem.downedCalamitas = flags3[0];
		DownedBossSystem.downedDragonfolly = flags3[1];
		DownedBossSystem.downedCrabulon = flags3[2];
		DownedBossSystem.downedBetsy = flags3[3];
		DownedBossSystem.downedRavager = flags3[4];
		_ = flags3[5];
		_ = flags3[6];
		_ = flags3[7];
		BitsByte flags4 = reader.ReadByte();
		_ = flags4[0];
		_ = flags4[1];
		_ = flags4[2];
		_ = flags4[3];
		_ = flags4[4];
		_ = flags4[5];
		_ = flags4[6];
		CalamityWorld.revenge = flags4[7];
		BitsByte flags5 = reader.ReadByte();
		DownedBossSystem.downedAstrumDeus = flags5[0];
		CalamityWorld.spawnedBandit = flags5[1];
		_ = flags5[2];
		AcidRainEvent.HasStartedAcidicDownpour = flags5[3];
		_ = flags5[4];
		DownedBossSystem.downedPolterghast = flags5[5];
		CalamityWorld.death = flags5[6];
		DownedBossSystem.downedGSS = flags5[7];
		BitsByte flags6 = reader.ReadByte();
		Abyss.AtLeftSideOfWorld = flags6[0];
		DownedBossSystem.downedAquaticScourge = flags6[1];
		DownedBossSystem.downedAstrumAureus = flags6[2];
		_ = flags6[3];
		CalamityWorld.armageddon = flags6[4];
		_ = flags6[5];
		_ = flags6[6];
		_ = flags6[7];
		BitsByte flags7 = reader.ReadByte();
		BossRushEvent.BossRushActive = flags7[0];
		DownedBossSystem.downedBoomerDuke = flags7[1];
		DownedBossSystem.downedCLAM = flags7[2];
		_ = flags7[3];
		AcidRainEvent.AcidRainEventIsOngoing = flags7[4];
		DownedBossSystem.downedEoCAcidRain = flags7[5];
		DownedBossSystem.downedAquaticScourgeAcidRain = flags7[6];
		AcidRainEvent.HasTriedToSummonOldDuke = flags7[7];
		BitsByte flags8 = reader.ReadByte();
		AcidRainEvent.HasBeenForceStartedByEoCDefeat = flags8[0];
		_ = flags8[1];
		DownedBossSystem.downedSecondSentinels = flags8[2];
		CalamityWorld.foundHomePermafrost = flags8[3];
		DownedBossSystem.downedCLAMHardMode = flags8[4];
		CalamityWorld.catName = flags8[5];
		CalamityWorld.dogName = flags8[6];
		CalamityWorld.bunnyName = flags8[7];
		BitsByte flags9 = reader.ReadByte();
		_ = flags9[0];
		_ = flags9[1];
		_ = flags9[2];
		_ = flags9[3];
		_ = flags9[4];
		_ = flags9[5];
		_ = flags9[6];
		_ = flags9[7];
		BitsByte flags10 = reader.ReadByte();
		_ = flags10[0];
		_ = flags10[1];
		AcidRainEvent.OldDukeHasBeenEncountered = flags10[2];
		_ = flags10[3];
		_ = flags10[4];
		_ = flags10[5];
		_ = flags10[6];
		_ = flags10[7];
		BitsByte flags11 = reader.ReadByte();
		_ = flags11[0];
		CalamityWorld.HasGeneratedLuminitePlanetoids = flags11[1];
		DownedBossSystem.downedPrimordialWyrm = flags11[2];
		DownedBossSystem.downedExoMechs = flags11[3];
		DownedBossSystem.downedAres = flags11[4];
		DownedBossSystem.downedThanatos = flags11[5];
		DownedBossSystem.downedArtemisAndApollo = flags11[6];
		CalamityWorld.TalkedToDraedon = flags11[7];
		BitsByte flags12 = reader.ReadByte();
		DownedBossSystem.downedCragmawMire = flags12[0];
		DownedBossSystem.downedMauler = flags12[1];
		DownedBossSystem.downedNuclearTerror = flags12[2];
		DownedBossSystem.downedBossRush = flags12[3];
		CalamityWorld.DraedonMechdusa = flags12[4];
		RecipeUnlockHandler.ReceiveData(reader);
		Abyss.AbyssChasmBottom = reader.ReadInt32();
		SulphurousSea.YStart = reader.ReadInt32();
		AstralBiome.YStart = reader.ReadInt32();
		AcidRainEvent.AccumulatedKillPoints = reader.ReadInt32();
		CalamityWorld.Reforges = reader.ReadInt32();
		CalamityWorld.MoneyStolenByBandit = reader.ReadInt32();
		CalamityWorld.DraedonSummonCountdown = reader.ReadInt32();
		CalamityWorld.DraedonMechToSummon = (ExoMech)reader.ReadInt32();
		CalamityWorld.DraedonSummonPosition = reader.ReadVector2();
		CalamityWorld.SunkenSeaLabCenter = reader.ReadVector2();
		CalamityWorld.PlanetoidLabCenter = reader.ReadVector2();
		CalamityWorld.JungleLabCenter = reader.ReadVector2();
		CalamityWorld.HellLabCenter = reader.ReadVector2();
		CalamityWorld.IceLabCenter = reader.ReadVector2();
		CalamityWorld.CavernLabCenter = reader.ReadVector2();
	}
}
