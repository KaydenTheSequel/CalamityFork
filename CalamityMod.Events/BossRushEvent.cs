using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Enums;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Skies;
using CalamityMod.Systems;
using CalamityMod.UI.DraedonSummoning;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Events;

public sealed class BossRushEvent : ModSystem
{
	public enum TimeChangeContext
	{
		None = 0,
		Day = 1,
		Night = -1
	}

	public struct Boss
	{
		public delegate void OnSpawnContext(int type);

		public int EntityID;

		public int SpecialSpawnCountdown;

		public float DimnessFactor;

		public bool UsesSpecialSound;

		public TimeChangeContext ToChangeTimeTo;

		public OnSpawnContext SpawnContext;

		public List<int> HostileNPCsToNotDelete;

		public Boss(int id, TimeChangeContext toChangeTimeTo = TimeChangeContext.None, OnSpawnContext spawnContext = null, int specialSpawnCountdown = -1, bool usesSpecialSound = false, float dimnessFactor = 0f, params int[] permittedNPCs)
		{
			if (spawnContext == null)
			{
				spawnContext = delegate(int type)
				{
					NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
				};
			}
			EntityID = id;
			SpecialSpawnCountdown = specialSpawnCountdown;
			UsesSpecialSound = usesSpecialSound;
			ToChangeTimeTo = toChangeTimeTo;
			SpawnContext = spawnContext;
			DimnessFactor = dimnessFactor;
			HostileNPCsToNotDelete = permittedNPCs.ToList();
			if (!HostileNPCsToNotDelete.Contains(id))
			{
				HostileNPCsToNotDelete.Add(id);
			}
			if (BossIDsAfterDeath.TryGetValue(id, out var deathThings))
			{
				HostileNPCsToNotDelete.AddRange(deathThings);
			}
		}
	}

	public static int HostileProjectileKillCounter;

	public static bool BossRushActive;

	public static bool DeactivateStupidFuckingBullshit;

	public static int BossRushStage;

	public static int BossRushSpawnCountdown;

	public static List<Boss> Bosses;

	public static Dictionary<int, int[]> BossIDsAfterDeath;

	public static Dictionary<int, Action<NPC>> BossDeathEffects;

	public static int StartTimer;

	public static int EndTimer;

	public static float WhiteDimness;

	public static readonly Color XerocTextColor;

	public const int StartEffectTotalTime = 120;

	public const int EndVisualEffectTime = 340;

	public static readonly SoundStyle BossSummonSound;

	public static readonly SoundStyle TeleportSound;

	public static readonly SoundStyle TerminusActivationSound;

	public static readonly SoundStyle StartBuildupSound;

	public static readonly SoundStyle TerminusDeactivationSound;

	public static readonly SoundStyle Tier2TransitionSound;

	public static readonly SoundStyle Tier3TransitionSound;

	public static readonly SoundStyle Tier4TransitionSound;

	public static readonly SoundStyle Tier5TransitionSound;

	public static readonly SoundStyle VictorySound;

	internal static IEntitySource Source => new EntitySource_WorldEvent("CalamityMod_BossRush");

	public static int ClosestPlayerToWorldCenter
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return Player.FindClosest(new Vector2((float)Main.maxTilesX, (float)Main.maxTilesY) * 16f * 0.5f, 1, 1);
		}
	}

	public static int CurrentlyFoughtBoss => Bosses[BossRushStage].EntityID;

	public static int NextBossToFight => Bosses[BossRushStage + 1].EntityID;

	public static int CurrentTier
	{
		get
		{
			if (BossRushStage > Bosses.FindIndex((Boss boss) => boss.EntityID == ModContent.NPCType<DevourerofGodsHead>()))
			{
				return 5;
			}
			if (BossRushStage > Bosses.FindIndex((Boss boss) => boss.EntityID == 398))
			{
				return 4;
			}
			if (BossRushStage > Bosses.FindIndex((Boss boss) => boss.EntityID == 262))
			{
				return 3;
			}
			if (BossRushStage > Bosses.FindIndex((Boss boss) => boss.EntityID == 113))
			{
				return 2;
			}
			return 1;
		}
	}

	public static int MusicToPlay
	{
		get
		{
			if (!BossRushActive)
			{
				return -1;
			}
			int tier = CurrentTier;
			if (ExternalMods.MusicAvailable)
			{
				if (tier > 4)
				{
					tier = 4;
				}
				return CalamityMod.Instance.GetMusicFromMusicMod($"BossRushTier{tier}").GetValueOrDefault();
			}
			return CurrentTier switch
			{
				1 => 5, 
				2 => 17, 
				3 => 12, 
				4 => 13, 
				5 => 38, 
				_ => 0, 
			};
		}
	}

	public override void OnModLoad()
	{
		BossIDsAfterDeath = new Dictionary<int, int[]>();
		List<Boss> list = new List<Boss>();
		Boss.OnSpawnContext spawnContext = delegate(int type)
		{
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
			DownedBossSystem.startedBossRushAtLeastOnce = true;
		};
		int[] obj = new int[12]
		{
			1, -9, -7, -8, -3, -8, 147, 225, -4, 535,
			244, 0
		};
		obj[11] = ModContent.NPCType<KingSlimeJewelRuby>();
		list.Add(new Boss(50, TimeChangeContext.None, spawnContext, -1, usesSpecialSound: false, 0f, obj));
		list.Add(new Boss(ModContent.NPCType<DesertScourgeHead>(), TimeChangeContext.None, delegate
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in DesertMedallion.SummonSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, ModContent.NPCType<DesertScourgeHead>());
		}, -1, true, 0f, ModContent.NPCType<DesertScourgeBody>(), ModContent.NPCType<DesertScourgeTail>(), ModContent.NPCType<DesertNuisanceHead>(), ModContent.NPCType<DesertNuisanceBody>(), ModContent.NPCType<DesertNuisanceTail>(), ModContent.NPCType<DesertNuisanceHeadYoung>(), ModContent.NPCType<DesertNuisanceBodyYoung>(), ModContent.NPCType<DesertNuisanceTailYoung>()));
		list.Add(new Boss(4, TimeChangeContext.Night, null, -1, false, 0f, 5));
		list.Add(new Boss(ModContent.NPCType<Crabulon>(), TimeChangeContext.Day, delegate(int type)
		{
			Player player = Main.player[ClosestPlayerToWorldCenter];
			int num = NPC.NewNPC(Source, (int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, false, 0f, ModContent.NPCType<CrabShroom>()));
		list.Add(new Boss(13, TimeChangeContext.None, null, -1, false, 0f, 14, 15, 666));
		list.Add(new Boss(266, TimeChangeContext.None, null, -1, false, 0f, 267, ModContent.NPCType<BrainIllusion>(), ModContent.NPCType<FalseBrain>()));
		list.Add(new Boss(ModContent.NPCType<HiveMind>(), TimeChangeContext.None, null, -1, false, 0f, ModContent.NPCType<DankCreeper>(), ModContent.NPCType<DarkHeart>(), ModContent.NPCType<HiveBlob>()));
		list.Add(new Boss(ModContent.NPCType<PerforatorHive>(), TimeChangeContext.None, null, -1, false, 0f, ModContent.NPCType<PerforatorHeadLarge>(), ModContent.NPCType<PerforatorBodyLarge>(), ModContent.NPCType<PerforatorTailLarge>(), ModContent.NPCType<PerforatorHeadMedium>(), ModContent.NPCType<PerforatorBodyMedium>(), ModContent.NPCType<PerforatorTailMedium>(), ModContent.NPCType<PerforatorHeadSmall>(), ModContent.NPCType<PerforatorBodySmall>(), ModContent.NPCType<PerforatorTailSmall>()));
		list.Add(new Boss(222, TimeChangeContext.None, null, -1, false, 0f, 210, 211, -58, 232, -59));
		list.Add(new Boss(668, TimeChangeContext.None, null, -1, false, 0f));
		list.Add(new Boss(35, TimeChangeContext.Night, delegate(int type)
		{
			Player player = Main.player[ClosestPlayerToWorldCenter];
			int num = NPC.NewNPC(Source, (int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, false, 0f, 36));
		list.Add(new Boss(ModContent.NPCType<SlimeGodCore>(), TimeChangeContext.Day, null, -1, false, 0f, ModContent.NPCType<EbonianPaladin>(), ModContent.NPCType<CrimulanPaladin>(), ModContent.NPCType<SplitEbonianPaladin>(), ModContent.NPCType<SplitCrimulanPaladin>(), ModContent.NPCType<CorruptSlimeSpawn>(), ModContent.NPCType<CorruptSlimeSpawn2>(), ModContent.NPCType<CrimsonSlimeSpawn>(), ModContent.NPCType<CrimsonSlimeSpawn2>()));
		list.Add(new Boss(113, TimeChangeContext.None, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			NPC.SpawnWOF(Main.player[ClosestPlayerToWorldCenter].position);
		}, -1, false, 0f, 114, 117, 118, 119, 115, 116));
		list.Add(new Boss(657, TimeChangeContext.None, null, 120, false, 0f, 658, 659, 660));
		list.Add(new Boss(ModContent.NPCType<Cryogen>(), TimeChangeContext.None, null, -1, false, 0f, ModContent.NPCType<CryogenShield>()));
		list.Add(new Boss(126, TimeChangeContext.Night, delegate
		{
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, 126);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, 125);
		}, -1, false, 0f, 125));
		list.Add(new Boss(ModContent.NPCType<AquaticScourgeHead>(), TimeChangeContext.Day, null, -1, false, 0f, ModContent.NPCType<AquaticScourgeBody>(), ModContent.NPCType<AquaticScourgeBodyAlt>(), ModContent.NPCType<AquaticScourgeTail>()));
		list.Add(new Boss(134, TimeChangeContext.Night, null, -1, false, 0f, 135, 136, 139));
		list.Add(new Boss(ModContent.NPCType<BrimstoneElemental>(), TimeChangeContext.Day, null, -1, false, 0f, ModContent.NPCType<Brimling>()));
		list.Add(new Boss(127, TimeChangeContext.Night, null, -1, false, 0f, 128, 129, 130, 131));
		list.Add(new Boss(ModContent.NPCType<CalamitasClone>(), TimeChangeContext.Night, null, -1, false, 0f, ModContent.NPCType<Cataclysm>(), ModContent.NPCType<Catastrophe>(), ModContent.NPCType<SoulSeeker>()));
		int[] obj2 = new int[4] { 264, 0, 263, 265 };
		obj2[1] = ModContent.NPCType<PlanterasFreeTentacle>();
		list.Add(new Boss(262, TimeChangeContext.Day, null, -1, usesSpecialSound: false, 0f, obj2));
		list.Add(new Boss(ModContent.NPCType<Anahita>(), TimeChangeContext.None, null, 120, false, 0f, ModContent.NPCType<Leviathan>(), ModContent.NPCType<AquaticAberration>(), ModContent.NPCType<AnahitasIceShield>(), 371));
		list.Add(new Boss(ModContent.NPCType<AstrumAureus>(), TimeChangeContext.Night, null, -1, false, 0f, ModContent.NPCType<AureusSpawn>()));
		list.Add(new Boss(245, TimeChangeContext.Day, delegate(int type)
		{
			int num = NPC.NewNPC(Source, (int)(Main.player[ClosestPlayerToWorldCenter].position.X + (float)Main.rand.Next(-100, 101)), (int)(Main.player[ClosestPlayerToWorldCenter].position.Y - 600f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, false, 0f, 247, 248, 246, 249));
		list.Add(new Boss(ModContent.NPCType<PlaguebringerGoliath>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in Abombination.UseSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<PlagueHomingMissile>(), ModContent.NPCType<PlagueMine>()));
		list.Add(new Boss(636, TimeChangeContext.Night, null, -1, false, 0f));
		list.Add(new Boss(370, TimeChangeContext.Day, delegate(int type)
		{
			Player player = Main.player[ClosestPlayerToWorldCenter];
			int num = NPC.NewNPC(Source, (int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, false, 0f, 371, 372, 373));
		list.Add(new Boss(ModContent.NPCType<RavagerBody>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in SoundID.ScaryScream, player.Center);
			int num = NPC.NewNPC(Source, (int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 600f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, true, 0f, ModContent.NPCType<FlamePillar>(), ModContent.NPCType<RockPillar>(), ModContent.NPCType<RavagerLegLeft>(), ModContent.NPCType<RavagerLegRight>(), ModContent.NPCType<RavagerClawLeft>(), ModContent.NPCType<RavagerClawRight>(), ModContent.NPCType<RavagerHead>(), ModContent.NPCType<RavagerHead2>()));
		list.Add(new Boss(439, TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			int num = NPC.NewNPC(Source, (int)player.Center.X, (int)player.Center.Y - 400, type, 1);
			Main.npc[num].direction = (Main.npc[num].spriteDirection = Math.Sign(player.Center.X - player.Center.X - 90f));
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, false, 0f, 440, 454, 455, 456, 457, 458, 459, 521, 522, 523));
		list.Add(new Boss(ModContent.NPCType<AstrumDeusHead>(), TimeChangeContext.Night, delegate(int type)
		{
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				bool flag = Main.npc[i].type == 493 || Main.npc[i].type == 422 || Main.npc[i].type == 507 || Main.npc[i].type == 517;
				if (Main.npc[i].active & flag)
				{
					Main.npc[i].active = false;
					Main.npc[i].netUpdate = true;
				}
			}
			SoundEngine.PlaySound(in AstrumDeusHead.SpawnSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<AstrumDeusBody>(), ModContent.NPCType<AstrumDeusTail>()));
		list.Add(new Boss(398, TimeChangeContext.None, delegate(int type)
		{
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, false, 0f, 401, 397, 396, 400));
		list.Add(new Boss(ModContent.NPCType<ProfanedGuardianCommander>(), TimeChangeContext.Day, delegate(int type)
		{
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, false, 0f, ModContent.NPCType<ProfanedGuardianDefender>(), ModContent.NPCType<ProfanedGuardianHealer>(), ModContent.NPCType<ProfanedRocks>()));
		list.Add(new Boss(ModContent.NPCType<Dragonfolly>(), TimeChangeContext.None, null, -1, false, 0f, ModContent.NPCType<DraconicSwarmer>()));
		list.Add(new Boss(ModContent.NPCType<Providence>(), TimeChangeContext.Day, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in Providence.SpawnSound, player.Center);
			int num = NPC.NewNPC(Source, (int)(player.position.X + (float)Main.rand.Next(-500, 501)), (int)(player.position.Y - 250f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, true, 0f, ModContent.NPCType<ProvSpawnOffense>(), ModContent.NPCType<ProvSpawnHealer>(), ModContent.NPCType<ProvSpawnDefense>()));
		list.Add(new Boss(ModContent.NPCType<CeaselessVoid>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player current = enumerator.Current;
				if (current.FindBuffIndex(ModContent.BuffType<IcarusFolly>()) > -1)
				{
					current.ClearBuff(ModContent.BuffType<IcarusFolly>());
				}
			}
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in MarkofProvidence.CVSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<DarkEnergy>()));
		list.Add(new Boss(ModContent.NPCType<StormWeaverHead>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in MarkofProvidence.StormSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<StormWeaverBody>(), ModContent.NPCType<StormWeaverTail>()));
		list.Add(new Boss(ModContent.NPCType<Signus>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in MarkofProvidence.SignutSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<CosmicLantern>(), ModContent.NPCType<CosmicMine>()));
		list.Add(new Boss(ModContent.NPCType<Polterghast>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in Polterghast.SpawnSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<PhantomFuckYou>(), ModContent.NPCType<PolterghastHook>(), ModContent.NPCType<PolterPhantom>()));
		list.Add(new Boss(ModContent.NPCType<OldDuke>(), TimeChangeContext.None, delegate(int type)
		{
			Player player = Main.player[ClosestPlayerToWorldCenter];
			int num = NPC.NewNPC(Source, (int)(player.position.X + (float)Main.rand.Next(-100, 101)), (int)(player.position.Y - 400f), type, 1);
			Main.npc[num].timeLeft *= 20;
			CalamityUtils.BossAwakenMessage(num);
		}, -1, false, 0f, ModContent.NPCType<OldDukeToothBall>(), ModContent.NPCType<SulphurousSharkron>()));
		list.Add(new Boss(ModContent.NPCType<DevourerofGodsHead>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in DevourerofGodsHead.SpawnSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f, ModContent.NPCType<DevourerofGodsBody>(), ModContent.NPCType<DevourerofGodsTail>()));
		list.Add(new Boss(ModContent.NPCType<Yharon>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			Player player = Main.player[ClosestPlayerToWorldCenter];
			SoundEngine.PlaySound(in Yharon.FireSound, player.Center);
			NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
		}, -1, true, 0f));
		list.Add(new Boss(ModContent.NPCType<Draedon>(), TimeChangeContext.None, delegate
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			if (!NPC.AnyNPCs(ModContent.NPCType<Draedon>()))
			{
				Player player = Main.player[ClosestPlayerToWorldCenter];
				SoundEngine.PlaySound(in CodebreakerUI.SummonSound, player.Center);
				Vector2 val = player.Center + new Vector2(-8f, -100f);
				int num = NPC.NewNPC(Source, (int)val.X, (int)val.Y, ModContent.NPCType<Draedon>());
				Main.npc[num].timeLeft *= 20;
			}
		}, -1, true, 0f, ModContent.NPCType<Apollo>(), ModContent.NPCType<AresBody>(), ModContent.NPCType<AresGaussNuke>(), ModContent.NPCType<AresLaserCannon>(), ModContent.NPCType<AresPlasmaFlamethrower>(), ModContent.NPCType<AresTeslaCannon>(), ModContent.NPCType<Artemis>(), ModContent.NPCType<ThanatosBody1>(), ModContent.NPCType<ThanatosBody2>(), ModContent.NPCType<ThanatosHead>(), ModContent.NPCType<ThanatosTail>()));
		list.Add(new Boss(ModContent.NPCType<SupremeCalamitas>(), TimeChangeContext.None, delegate(int type)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			SoundEngine.PlaySound(in SupremeCalamitas.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
			CalamityUtils.SpawnBossBetter(Main.player[ClosestPlayerToWorldCenter].Top - new Vector2(42f, 84f), type);
		}, 840, false, 0f, ModContent.NPCType<SepulcherArm>(), ModContent.NPCType<SepulcherHead>(), ModContent.NPCType<SepulcherBody>(), ModContent.NPCType<SepulcherBodyEnergyBall>(), ModContent.NPCType<SepulcherTail>(), ModContent.NPCType<SoulSeekerSupreme>(), ModContent.NPCType<BrimstoneHeart>(), ModContent.NPCType<SupremeCataclysm>(), ModContent.NPCType<SupremeCatastrophe>()));
		Bosses = list;
		BossDeathEffects = new Dictionary<int, Action<NPC>>
		{
			[113] = delegate
			{
				//IL_0085: Unknown result type (might be due to invalid IL or missing references)
				//IL_0090: Unknown result type (might be due to invalid IL or missing references)
				//IL_0042: Unknown result type (might be due to invalid IL or missing references)
				CreateTierAnimation(2);
				BossRushDialogueSystem.StartDialogue(BossRushDialoguePhase.TierOneComplete);
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player current = enumerator.Current;
					if (current.Calamity().BossRushReturnPosition.HasValue)
					{
						CalamityPlayer.ModTeleport(current, current.Calamity().BossRushReturnPosition.Value, playSound: false, 2);
						current.Calamity().BossRushReturnPosition = null;
					}
					current.Calamity().BossRushReturnPosition = null;
					SoundEngine.PlaySound(TeleportSound with
					{
						Volume = 1.6f
					}, current.Center);
				}
			},
			[262] = delegate
			{
				CreateTierAnimation(3);
				BossRushDialogueSystem.StartDialogue(BossRushDialoguePhase.TierTwoComplete);
			},
			[398] = delegate
			{
				CreateTierAnimation(4);
				BossRushDialogueSystem.StartDialogue(BossRushDialoguePhase.TierThreeComplete);
			},
			[ModContent.NPCType<DevourerofGodsHead>()] = delegate
			{
				CreateTierAnimation(5);
				BossRushDialogueSystem.StartDialogue(BossRushDialoguePhase.TierFourComplete);
			},
			[ModContent.NPCType<SupremeCalamitas>()] = delegate
			{
				CalamityUtils.KillAllHostileProjectiles();
				HostileProjectileKillCounter = 3;
				BossRushDialogueSystem.StartDialogue(DownedBossSystem.downedBossRush ? BossRushDialoguePhase.EndRepeat : BossRushDialoguePhase.End);
			}
		};
	}

	public override void Unload()
	{
		Bosses = null;
		BossIDsAfterDeath = null;
		BossDeathEffects = null;
	}

	internal static void MiscUpdateEffects()
	{
		if (!BossRushActive)
		{
			return;
		}
		BossRushDialogueSystem.Tick();
		if (BossRushSpawnCountdown == 179 && EndTimer == 0 && CurrentlyFoughtBoss == 50)
		{
			CreateTierAnimation(1);
		}
		if (CreditsRollEvent.IsEventOngoing)
		{
			CreditsRollEvent.SetRemainingTimeDirect(1);
		}
		if (NPC.MoonLordCountdown > 0)
		{
			NPC.MoonLordCountdown = 0;
		}
		if (HostileProjectileKillCounter > 0)
		{
			HostileProjectileKillCounter--;
			if (HostileProjectileKillCounter == 1)
			{
				CalamityUtils.KillAllHostileProjectiles();
			}
			if (Main.dedServ)
			{
				BRHostileProjKillSyncPacket.Send();
			}
		}
	}

	internal static void Update()
	{
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		if (!BossRushActive)
		{
			BossRushSpawnCountdown = 180;
			BossRushSky.CurrentInterestMin = 0f;
			if (BossRushStage != 0)
			{
				BossRushStage = 0;
				if (Main.dedServ)
				{
					BossRushStagePacket.Send();
				}
			}
			return;
		}
		MiscUpdateEffects();
		if (!CalamityPlayer.areThereAnyDamnBosses)
		{
			if (BossRushSpawnCountdown > 0)
			{
				BossRushSpawnCountdown--;
			}
			if (BossRushSpawnCountdown <= 0 && BossRushStage < Bosses.Count)
			{
				BossRushSpawnCountdown = 60;
				if (BossRushStage >= Bosses.FindIndex((Boss boss) => boss.EntityID == 398))
				{
					BossRushSpawnCountdown += 180;
				}
				if (BossRushStage < Bosses.Count - 1 && Bosses[BossRushStage + 1].SpecialSpawnCountdown != -1)
				{
					BossRushSpawnCountdown = Bosses[BossRushStage + 1].SpecialSpawnCountdown;
				}
				if (Bosses[BossRushStage].ToChangeTimeTo != TimeChangeContext.None)
				{
					CalamityWorld.ResetTime(Bosses[BossRushStage].ToChangeTimeTo == TimeChangeContext.Day);
				}
				if (!Bosses[BossRushStage].UsesSpecialSound)
				{
					SoundEngine.PlaySound(in BossSummonSound, Main.player[ClosestPlayerToWorldCenter].Center);
				}
				Bosses[BossRushStage].SpawnContext(CurrentlyFoughtBoss);
			}
		}
		if (BossRushStage >= 0 && BossRushStage < Bosses.Count)
		{
			WhiteDimness = MathHelper.Lerp(WhiteDimness, Bosses[BossRushStage].DimnessFactor, 0.1f);
			if (MathHelper.Distance(WhiteDimness, Bosses[BossRushStage].DimnessFactor) < 0.004f)
			{
				WhiteDimness = Bosses[BossRushStage].DimnessFactor;
			}
		}
		if (EndTimer > 0)
		{
			BossRushSky.CurrentInterest = MathHelper.Lerp(0.5f, 0.75f, Utils.GetLerpValue(5f, 145f, EndTimer, clamped: true));
		}
		BossRushSky.CurrentInterestMin = MathHelper.Lerp(0f, 0.5f, (float)Math.Pow((float)BossRushStage / (float)Bosses.Count, 5.0));
	}

	public static void End()
	{
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.Calamity().BossRushReturnPosition = null;
		}
		if (Main.netMode == 0)
		{
			EndEffects();
		}
		else
		{
			EndBossRushPacket.Send();
		}
	}

	internal static void EndEffects()
	{
		for (int doom = 0; doom < Main.maxNPCs; doom++)
		{
			NPC n = Main.npc[doom];
			if (n.active && (n.boss || n.type == 13 || n.type == 14 || n.type == 15 || n.type == ModContent.NPCType<Draedon>()))
			{
				n.active = false;
				n.netUpdate = true;
			}
		}
		BossRushActive = false;
		BossRushStage = 0;
		StartTimer = 0;
		EndTimer = 0;
		CalamityUtils.KillAllHostileProjectiles();
		CalamityNetcode.SyncWorld();
		if (Main.dedServ)
		{
			BossRushStagePacket.Send();
			BossRushStartTimerPacket.Send();
			BossRushEndTimerPacket.Send();
		}
	}

	internal static void OnBossKill(NPC npc, Mod mod)
	{
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		if (npc.type == 13 || npc.type == 14 || npc.type == 15)
		{
			if (npc.boss)
			{
				BossRushStage++;
				CalamityUtils.KillAllHostileProjectiles();
				HostileProjectileKillCounter = 3;
			}
		}
		else if (npc.type == ModContent.NPCType<Anahita>() || npc.type == ModContent.NPCType<Leviathan>())
		{
			if (!NPC.AnyNPCs((npc.type == ModContent.NPCType<Anahita>()) ? ModContent.NPCType<Leviathan>() : ModContent.NPCType<Anahita>()))
			{
				BossRushStage++;
				CalamityUtils.KillAllHostileProjectiles();
				HostileProjectileKillCounter = 3;
			}
		}
		else if (npc.type == ModContent.NPCType<AstrumDeusHead>() && npc.Calamity().newAI[0] != 0f)
		{
			BossRushStage++;
			CalamityUtils.KillAllHostileProjectiles();
			HostileProjectileKillCounter = 3;
		}
		else if (npc.type == ModContent.NPCType<SlimeGodCore>())
		{
			BossRushStage++;
			CalamityUtils.KillAllHostileProjectiles();
			HostileProjectileKillCounter = 3;
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player p = enumerator.Current;
				p.Calamity().BossRushReturnPosition = p.Center;
				Vector2? underworld = CalamityPlayer.GetUnderworldPosition(p);
				if (!underworld.HasValue)
				{
					break;
				}
				CalamityPlayer.ModTeleport(p, underworld.Value, playSound: false, 2);
				SoundEngine.PlaySound(TeleportSound with
				{
					Volume = 1.6f
				}, p.Center);
			}
		}
		else if ((Bosses.Any((Boss boss) => boss.EntityID == npc.type) && !BossIDsAfterDeath.ContainsKey(npc.type)) || BossIDsAfterDeath.Values.Any((int[] killList) => killList.Contains(npc.type)))
		{
			BossRushStage++;
			CalamityUtils.KillAllHostileProjectiles();
			HostileProjectileKillCounter = 3;
			if (BossDeathEffects.ContainsKey(npc.type))
			{
				BossDeathEffects[npc.type](npc);
			}
			if (npc.type == Bosses[Bosses.Count - 1].EntityID)
			{
				DownedBossSystem.downedBossRush = true;
				CalamityNetcode.SyncWorld();
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(Source, npc.Center, Vector2.Zero, ModContent.ProjectileType<BossRushEndEffectThing>(), 0, 0f, Main.myPlayer);
				}
			}
		}
		if (Main.dedServ)
		{
			BossRushStagePacket.Send();
			BRHostileProjKillSyncPacket.Send();
		}
		BossRushSky.CurrentInterest = 0.85f;
	}

	public static void CreateTierAnimation(int tier)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			if (!p.dead)
			{
				int animation = Projectile.NewProjectile(new EntitySource_WorldEvent(), p.Center, Vector2.Zero, ModContent.ProjectileType<BossRushTierAnimation>(), 0, 0f, p.whoAmI);
				if (Main.projectile.IndexInRange(animation))
				{
					Main.projectile[animation].ai[0] = tier;
				}
			}
		}
	}

	public static void SyncStartTimer(int time)
	{
		StartTimer = time;
		if (Main.dedServ)
		{
			BossRushStartTimerPacket.Send();
		}
	}

	public static void SyncEndTimer(int time)
	{
		EndTimer = time;
		if (Main.dedServ)
		{
			BossRushEndTimerPacket.Send();
		}
	}

	static BossRushEvent()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		BossRushActive = false;
		DeactivateStupidFuckingBullshit = false;
		BossRushStage = 0;
		BossRushSpawnCountdown = 180;
		Bosses = new List<Boss>();
		BossIDsAfterDeath = new Dictionary<int, int[]>();
		BossDeathEffects = new Dictionary<int, Action<NPC>>();
		XerocTextColor = new Color(250, 213, 77);
		BossSummonSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushSummon", 2);
		TeleportSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTeleport");
		TerminusActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTerminusActivate");
		StartBuildupSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTerminusCharge");
		TerminusDeactivationSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTerminusDeactivate");
		Tier2TransitionSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTier2Transition");
		Tier3TransitionSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTier3Transition");
		Tier4TransitionSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTier4Transition");
		Tier5TransitionSound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushTier5Transition");
		VictorySound = new SoundStyle("CalamityMod/Sounds/Custom/BossRush/BossRushVictory");
	}
}
