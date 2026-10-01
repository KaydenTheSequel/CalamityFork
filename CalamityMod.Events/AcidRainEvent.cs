using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Boss;
using CalamityMod.UI;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace CalamityMod.Events;

public class AcidRainEvent : ModSystem
{
	public static int MaxNuclearToadCount;

	public static readonly Color TextColor;

	public const int InvasionNoKillPersistTime = 9000;

	public const float BloodwormSpawnRate = 0.1f;

	public static Dictionary<int, AcidRainSpawnData> PossibleEnemiesPreHM;

	public static Dictionary<int, AcidRainSpawnData> PossibleEnemiesAS;

	public static Dictionary<int, AcidRainSpawnData> PossibleEnemiesPolter;

	public static Dictionary<int, AcidRainSpawnData> PossibleMinibossesAS;

	public static Dictionary<int, AcidRainSpawnData> PossibleMinibossesPolter;

	public static bool AcidRainEventIsOngoing;

	public static int AccumulatedKillPoints;

	public static bool HasTriedToSummonOldDuke;

	public static bool HasStartedAcidicDownpour;

	public static bool HasBeenForceStartedByEoCDefeat;

	public static bool OldDukeHasBeenEncountered;

	public static int CountdownUntilForcedAcidRain;

	public static int TimeSinceLastAcidRainKill;

	public static int TimeSinceEventStarted;

	public static List<int> AllMinibosses => PossibleMinibossesAS.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key).Concat(PossibleMinibossesPolter.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key)).Distinct()
		.ToList();

	public static bool AnyRainMinibosses
	{
		get
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc = enumerator.Current;
				if (PossibleMinibossesAS.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key).Contains(npc.type) || PossibleMinibossesPolter.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key).Contains(npc.type))
				{
					return true;
				}
			}
			return false;
		}
	}

	public static float AcidRainCompletionRatio => MathHelper.Clamp((float)AccumulatedKillPoints / (float)NeededEnemyKills, 0f, 1f);

	public static int NeededEnemyKills
	{
		get
		{
			int playerCount = Main.CurrentFrameFlags.ActivePlayersCount;
			if (DownedBossSystem.downedPolterghast)
			{
				return (int)(Math.Log((double)playerCount + Math.E - 1.0) * 170.0);
			}
			if (DownedBossSystem.downedAquaticScourge)
			{
				return (int)(Math.Log((double)playerCount + Math.E - 1.0) * 135.0);
			}
			return (int)(Math.Log((double)playerCount + Math.E - 1.0) * 110.0);
		}
	}

	public static void BroadcastEventText(string localizationKey)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.BroadcastLocalizedText(localizationKey, TextColor);
	}

	public override void OnModLoad()
	{
		PossibleEnemiesPreHM = new Dictionary<int, AcidRainSpawnData>
		{
			{
				ModContent.NPCType<NuclearToad>(),
				new AcidRainSpawnData(1, 0.75f, AcidRainSpawnRequirement.Anywhere)
			},
			{
				ModContent.NPCType<AcidEel>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Radiator>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Skyfin>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			}
		};
		PossibleEnemiesAS = new Dictionary<int, AcidRainSpawnData>
		{
			{
				ModContent.NPCType<Radiator>(),
				new AcidRainSpawnData(0, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<AcidEel>(),
				new AcidRainSpawnData(0, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<NuclearToad>(),
				new AcidRainSpawnData(0, 0.75f, AcidRainSpawnRequirement.Anywhere)
			},
			{
				ModContent.NPCType<FlakCrab>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Orthocera>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Skyfin>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Trilobite>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<SulphurousSkater>(),
				new AcidRainSpawnData(1, 0.4f, AcidRainSpawnRequirement.Anywhere)
			}
		};
		PossibleEnemiesPolter = new Dictionary<int, AcidRainSpawnData>
		{
			{
				ModContent.NPCType<Radiator>(),
				new AcidRainSpawnData(0, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<AcidEel>(),
				new AcidRainSpawnData(0, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<NuclearToad>(),
				new AcidRainSpawnData(0, 0.75f, AcidRainSpawnRequirement.Anywhere)
			},
			{
				ModContent.NPCType<FlakCrab>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Orthocera>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Skyfin>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Trilobite>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<SulphurousSkater>(),
				new AcidRainSpawnData(1, 0.4f, AcidRainSpawnRequirement.Anywhere)
			},
			{
				ModContent.NPCType<GammaSlime>(),
				new AcidRainSpawnData(1, 1f, AcidRainSpawnRequirement.Anywhere)
			}
		};
		PossibleMinibossesAS = new Dictionary<int, AcidRainSpawnData> { 
		{
			ModContent.NPCType<CragmawMire>(),
			new AcidRainSpawnData(5, 0.08f, AcidRainSpawnRequirement.Water)
		} };
		PossibleMinibossesPolter = new Dictionary<int, AcidRainSpawnData>
		{
			{
				ModContent.NPCType<CragmawMire>(),
				new AcidRainSpawnData(4, 0.08f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<Mauler>(),
				new AcidRainSpawnData(4, 0.07f, AcidRainSpawnRequirement.Water)
			},
			{
				ModContent.NPCType<NuclearTerror>(),
				new AcidRainSpawnData(8, 0.045f, AcidRainSpawnRequirement.Anywhere)
			}
		};
		BossHealthBarManager.MinibossHPBarList.AddRange(AllMinibosses);
	}

	public override void Unload()
	{
		PossibleEnemiesPreHM = new Dictionary<int, AcidRainSpawnData>();
		PossibleEnemiesAS = new Dictionary<int, AcidRainSpawnData>();
		PossibleEnemiesPolter = new Dictionary<int, AcidRainSpawnData>();
		PossibleMinibossesAS = new Dictionary<int, AcidRainSpawnData>();
		PossibleMinibossesPolter = new Dictionary<int, AcidRainSpawnData>();
	}

	public static void TryStartEvent(bool forceRain = false)
	{
		if (AcidRainEventIsOngoing || (!NPC.downedBoss1 && !Main.hardMode && !DownedBossSystem.downedAquaticScourge && !DownedBossSystem.downedPolterghast) || BossRushEvent.BossRushActive || CreativePowerManager.Instance.GetPower<CreativePowers.FreezeRainPower>().Enabled)
		{
			return;
		}
		if (Main.CurrentFrameFlags.ActivePlayersCount > 0)
		{
			AcidRainEventIsOngoing = true;
			AccumulatedKillPoints = NeededEnemyKills;
			if (forceRain)
			{
				Main.raining = true;
				Main.cloudBGActive = 1f;
				Main.numCloudsTemp = 200;
				Main.numClouds = Main.numCloudsTemp;
				Main.windSpeedCurrent = 0.72f;
				Main.windSpeedTarget = Main.windSpeedCurrent;
				Main.weatherCounter = 36000;
				Main.rainTime = Main.weatherCounter;
				Main.maxRaining = 0.89f;
				CalamityNetcode.SyncWorld();
			}
			HasTriedToSummonOldDuke = false;
			TimeSinceLastAcidRainKill = 0;
		}
		UpdateInvasion();
		BroadcastEventText("Mods.CalamityMod.Events.AcidRainStart");
	}

	public static void Update()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		TimeSinceLastAcidRainKill++;
		TimeSinceEventStarted++;
		if (TimeSinceLastAcidRainKill >= 9000)
		{
			AccumulatedKillPoints = 0;
			HasTriedToSummonOldDuke = false;
			UpdateInvasion(win: false);
		}
		if (!HasStartedAcidicDownpour)
		{
			int sulphSeaWidth = SulphurousSea.BiomeWidth;
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player player = enumerator.Current;
				if (((player.Center.X <= ((float)sulphSeaWidth + 60f) * 16f && Abyss.AtLeftSideOfWorld) || (player.Center.X >= ((float)Main.maxTilesX - ((float)sulphSeaWidth + 60f)) * 16f && !Abyss.AtLeftSideOfWorld)) && player.Calamity().ZoneSulphur)
				{
					HasStartedAcidicDownpour = true;
					CalamityNetcode.SyncWorld();
					break;
				}
			}
		}
		if (!Main.raining && TimeSinceEventStarted > 20)
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<OldDuke>()))
			{
				AccumulatedKillPoints = 0;
				HasTriedToSummonOldDuke = false;
				UpdateInvasion(win: false);
			}
		}
		else if (TimeSinceEventStarted < 20)
		{
			Main.raining = true;
			Main.cloudBGActive = 1f;
			Main.numCloudsTemp = 200;
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedTarget = 0.72f;
			Main.windSpeedCurrent = Main.windSpeedTarget;
			Main.weatherCounter = 36000;
			Main.rainTime = Main.weatherCounter;
			Main.maxRaining = 0.89f;
		}
		if (!DownedBossSystem.downedPolterghast || AccumulatedKillPoints != 1 || CalamityUtils.CountProjectiles(ModContent.ProjectileType<OverlyDramaticDukeSummoner>()) > 0 || NPC.AnyNPCs(ModContent.NPCType<OldDuke>()))
		{
			return;
		}
		if (HasTriedToSummonOldDuke)
		{
			AccumulatedKillPoints = 0;
			HasTriedToSummonOldDuke = false;
			UpdateInvasion(win: false);
			return;
		}
		EntitySource_WorldEvent source = new EntitySource_WorldEvent();
		int playerClosestToAbyss = Player.FindClosest(new Vector2((float)((!Abyss.AtLeftSideOfWorld) ? (Main.maxTilesX * 16) : 0), (float)(int)Main.worldSurface), 0, 0);
		Player closestToAbyss = Main.player[playerClosestToAbyss];
		if (Main.netMode != 1 && Math.Abs(closestToAbyss.Center.X - (float)((!Abyss.AtLeftSideOfWorld) ? (Main.maxTilesX * 16) : 0)) <= 12000f)
		{
			Projectile.NewProjectile(source, closestToAbyss.Center + Vector2.UnitY * 160f, Vector2.Zero, ModContent.ProjectileType<OverlyDramaticDukeSummoner>(), 0, 8f, Main.myPlayer);
		}
	}

	public static void UpdateInvasion(bool win = true)
	{
		if (!AcidRainEventIsOngoing)
		{
			return;
		}
		if (AccumulatedKillPoints <= 0)
		{
			AcidRainEventIsOngoing = false;
			BroadcastEventText("Mods.CalamityMod.Events.AcidRainEnd");
			Main.numCloudsTemp = Main.rand.Next(5, 21);
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedCurrent = Main.rand.NextFloat(0.04f, 0.25f);
			Main.windSpeedTarget = Main.windSpeedCurrent;
			Main.maxRaining = 0f;
			if (win)
			{
				DownedBossSystem.downedEoCAcidRain = true;
				DownedBossSystem.downedAquaticScourgeAcidRain = DownedBossSystem.downedAquaticScourge;
			}
			HasTriedToSummonOldDuke = false;
			CalamityWorld.StopRain();
		}
		CalamityNetcode.SyncWorld();
		if (Main.dedServ)
		{
			AcidRainSyncPacket.Send();
			AcidRainOldDukeSummonSyncPacket.Send();
			EncounteredOldDukeSyncPacket.Send();
		}
	}

	public static void TryToStartEventNaturally()
	{
		bool increasedEventChance = !DownedBossSystem.downedEoCAcidRain || (!DownedBossSystem.downedAquaticScourgeAcidRain && DownedBossSystem.downedAquaticScourge) || (!DownedBossSystem.downedBoomerDuke && DownedBossSystem.downedPolterghast);
		if ((int)Main.time == 32399 && !Main.dayTime && Main.rand.NextBool(increasedEventChance ? 3 : 300))
		{
			bool shouldNotStartEvent = false;
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Calamity().noStupidNaturalARSpawns)
				{
					shouldNotStartEvent = true;
					break;
				}
			}
			if (!shouldNotStartEvent)
			{
				TryStartEvent();
				CalamityNetcode.SyncWorld();
			}
		}
		if (NPC.downedBoss1 && !DownedBossSystem.downedEoCAcidRain && !HasBeenForceStartedByEoCDefeat)
		{
			ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.Calamity().ZoneSulphur)
				{
					HasBeenForceStartedByEoCDefeat = true;
					TryStartEvent();
					CalamityNetcode.SyncWorld();
				}
			}
		}
		if (CountdownUntilForcedAcidRain == 1)
		{
			TryStartEvent();
			CalamityNetcode.SyncWorld();
		}
		if (CountdownUntilForcedAcidRain > 0)
		{
			CountdownUntilForcedAcidRain--;
		}
	}

	public static void OnEnemyKill(NPC npc)
	{
		Dictionary<int, AcidRainSpawnData> possibleEnemies = PossibleEnemiesPreHM;
		if (DownedBossSystem.downedAquaticScourge)
		{
			possibleEnemies = PossibleEnemiesAS;
		}
		if (DownedBossSystem.downedPolterghast)
		{
			possibleEnemies = PossibleEnemiesPolter;
		}
		if (AcidRainEventIsOngoing)
		{
			if (possibleEnemies.Select((KeyValuePair<int, AcidRainSpawnData> enemy) => enemy.Key).Contains(npc.type))
			{
				AccumulatedKillPoints -= possibleEnemies[npc.type].InvasionContributionPoints;
				if (DownedBossSystem.downedPolterghast)
				{
					AccumulatedKillPoints = (int)MathHelper.Max(1f, (float)AccumulatedKillPoints);
				}
				Main.rainTime += Main.rand.Next(240, 301);
			}
			Dictionary<int, AcidRainSpawnData> possibleMinibosses = (DownedBossSystem.downedPolterghast ? PossibleMinibossesPolter : PossibleMinibossesAS);
			if (possibleMinibosses.Select((KeyValuePair<int, AcidRainSpawnData> miniboss) => miniboss.Key).Contains(npc.type))
			{
				AccumulatedKillPoints -= possibleMinibosses[npc.type].InvasionContributionPoints;
				if (DownedBossSystem.downedPolterghast)
				{
					AccumulatedKillPoints = (int)MathHelper.Max(1f, (float)AccumulatedKillPoints);
				}
				Main.rainTime += Main.rand.Next(1800, 2101);
			}
		}
		AccumulatedKillPoints = (int)MathHelper.Max(0f, (float)AccumulatedKillPoints);
		if (AcidRainEventIsOngoing && DownedBossSystem.downedPolterghast && npc.type == ModContent.NPCType<OldDuke>() && (float)AccumulatedKillPoints <= 2f)
		{
			HasTriedToSummonOldDuke = false;
			AccumulatedKillPoints = 0;
		}
		TimeSinceLastAcidRainKill = 0;
		UpdateInvasion();
	}

	static AcidRainEvent()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		MaxNuclearToadCount = 5;
		TextColor = new Color(115, 194, 147);
		PossibleEnemiesPreHM = new Dictionary<int, AcidRainSpawnData>();
		PossibleEnemiesAS = new Dictionary<int, AcidRainSpawnData>();
		PossibleEnemiesPolter = new Dictionary<int, AcidRainSpawnData>();
		PossibleMinibossesAS = new Dictionary<int, AcidRainSpawnData>();
		PossibleMinibossesPolter = new Dictionary<int, AcidRainSpawnData>();
	}
}
