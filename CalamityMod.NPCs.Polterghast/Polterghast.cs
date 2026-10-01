using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Polterghast;

public class Polterghast : ModNPC
{
	public static int phase1IconIndex;

	public static int phase3IconIndex;

	private const int DespawnTimerMax = 900;

	private int despawnTimer = 900;

	private int soundTimer;

	private bool reachedChargingPoint;

	private bool threeAM;

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PolterghastHit");

	public static readonly SoundStyle P2Sound = new SoundStyle("CalamityMod/Sounds/Custom/Polterghast/PolterghastP2Transition");

	public static readonly SoundStyle P3Sound = new SoundStyle("CalamityMod/Sounds/Custom/Polterghast/PolterghastP3Transition");

	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/Polterghast/PolterghastSpawn");

	public static readonly SoundStyle PhantomSound = new SoundStyle("CalamityMod/Sounds/Custom/Polterghast/PolterghastPhantomSpawn");

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> Texture_Glow2;

	public static List<SoundStyle> creepySounds = new List<SoundStyle>
	{
		DevourerofGodsHead.AttackSound,
		global::CalamityMod.NPCs.Providence.Providence.HolyRaySound,
		AresBody.EnragedSound,
		AresBody.LaserStartSound,
		ThanatosHead.VentSound,
		global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas.SepulcherSummonSound,
		global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas.SpawnSound,
		RavagerBody.LimbLossSound,
		global::CalamityMod.NPCs.HiveMind.HiveMind.RoarSound,
		global::CalamityMod.NPCs.Yharon.Yharon.RoarSound,
		DesertScourgeHead.RoarSound,
		global::CalamityMod.NPCs.OldDuke.OldDuke.RoarSound,
		ReaperShark.SearchRoarSound,
		ReaperShark.EnragedRoarSound,
		LuminousCorvina.ScreamSound,
		DevilFish.MaskBreakSound,
		PrimordialWyrmHead.ChargeSound,
		global::CalamityMod.NPCs.GreatSandShark.GreatSandShark.RoarSound,
		Mauler.RoarSound,
		AstrumDeusHead.DeathSound,
		global::CalamityMod.NPCs.AstrumAureus.AstrumAureus.HitSound,
		SoundID.ScaryScream,
		SoundID.DD2_KoboldFlyerHurt
	};

	public static float Phase2ContactDamageMult = 1.25f;

	public static float Phase3ContactDamageMult = 1.5f;

	public static int BlueShotDamage = 60;

	public static int BlueBlastDamage = 65;

	public static int RedShotDamage = 65;

	public static int RedBlastDamage = 70;

	public override void Load()
	{
		string phase1IconPath = "CalamityMod/NPCs/Polterghast/Polterghast_Head_Boss";
		string phase3IconPath = "CalamityMod/NPCs/Polterghast/Necroplasm_Head_Boss";
		phase1IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase1IconPath);
		phase3IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase3IconPath);
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 12;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			Texture_Glow2 = ModContent.Request<Texture2D>(Texture + "Glow2", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 120;
		base.NPC.npcSlots = 50f;
		base.NPC.width = 90;
		base.NPC.height = 120;
		base.NPC.defense = 90;
		base.NPC.DR_NERD(0.2f);
		base.NPC.LifeMaxNERB(280000, 420000, 325000);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(1);
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = SoundID.NPCDeath39;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Polterghast")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if ((float)base.NPC.life / (float)base.NPC.lifeMax < (death ? 0.6f : (revenge ? 0.5f : (expertMode ? 0.35f : 0.2f))))
		{
			index = phase3IconIndex;
		}
		else
		{
			index = phase1IconIndex;
		}
	}

	public override void BossHeadRotation(ref float rotation)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.HasValidTarget && base.NPC.Calamity().newAI[3] == 0f)
		{
			rotation = (Main.player[base.NPC.TranslatedTargetIndex].Center - base.NPC.Center).ToRotation() + (float)Math.PI / 2f;
		}
		else
		{
			rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(despawnTimer);
		writer.Write(reachedChargingPoint);
		writer.Write(threeAM);
		CalamityGlobalNPC cgn = base.NPC.Calamity();
		writer.Write(cgn.newAI[0]);
		writer.Write(cgn.newAI[1]);
		writer.Write(cgn.newAI[2]);
		writer.Write(cgn.newAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		despawnTimer = reader.ReadInt32();
		reachedChargingPoint = reader.ReadBoolean();
		threeAM = reader.ReadBoolean();
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.newAI[0] = reader.ReadSingle();
		calamityGlobalNPC.newAI[1] = reader.ReadSingle();
		calamityGlobalNPC.newAI[2] = reader.ReadSingle();
		calamityGlobalNPC.newAI[3] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff1: Unknown result type (might be due to invalid IL or missing references)
		//IL_101f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1063: Unknown result type (might be due to invalid IL or missing references)
		//IL_104a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1088: Unknown result type (might be due to invalid IL or missing references)
		//IL_108a: Unknown result type (might be due to invalid IL or missing references)
		//IL_108b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1090: Unknown result type (might be due to invalid IL or missing references)
		//IL_1097: Unknown result type (might be due to invalid IL or missing references)
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10db: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_168a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1695: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1704: Unknown result type (might be due to invalid IL or missing references)
		//IL_173b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1746: Unknown result type (might be due to invalid IL or missing references)
		//IL_177d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1788: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_110f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1110: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_221f: Unknown result type (might be due to invalid IL or missing references)
		//IL_223c: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1828: Unknown result type (might be due to invalid IL or missing references)
		//IL_182e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1845: Unknown result type (might be due to invalid IL or missing references)
		//IL_184f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_11de: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_113c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1147: Unknown result type (might be due to invalid IL or missing references)
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2011: Unknown result type (might be due to invalid IL or missing references)
		//IL_2017: Unknown result type (might be due to invalid IL or missing references)
		//IL_202e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2038: Unknown result type (might be due to invalid IL or missing references)
		//IL_203d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1921: Unknown result type (might be due to invalid IL or missing references)
		//IL_192b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1930: Unknown result type (might be due to invalid IL or missing references)
		//IL_193b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1969: Unknown result type (might be due to invalid IL or missing references)
		//IL_196f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1986: Unknown result type (might be due to invalid IL or missing references)
		//IL_1990: Unknown result type (might be due to invalid IL or missing references)
		//IL_1995: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13be: Unknown result type (might be due to invalid IL or missing references)
		//IL_1158: Unknown result type (might be due to invalid IL or missing references)
		//IL_1186: Unknown result type (might be due to invalid IL or missing references)
		//IL_118c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20df: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_210a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2114: Unknown result type (might be due to invalid IL or missing references)
		//IL_2119: Unknown result type (might be due to invalid IL or missing references)
		//IL_2124: Unknown result type (might be due to invalid IL or missing references)
		//IL_2152: Unknown result type (might be due to invalid IL or missing references)
		//IL_2158: Unknown result type (might be due to invalid IL or missing references)
		//IL_216f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2179: Unknown result type (might be due to invalid IL or missing references)
		//IL_217e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2402: Unknown result type (might be due to invalid IL or missing references)
		//IL_240c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1547: Unknown result type (might be due to invalid IL or missing references)
		//IL_1556: Unknown result type (might be due to invalid IL or missing references)
		//IL_1563: Unknown result type (might be due to invalid IL or missing references)
		//IL_1568: Unknown result type (might be due to invalid IL or missing references)
		//IL_1570: Unknown result type (might be due to invalid IL or missing references)
		//IL_1575: Unknown result type (might be due to invalid IL or missing references)
		//IL_157c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1581: Unknown result type (might be due to invalid IL or missing references)
		//IL_1586: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1469: Unknown result type (might be due to invalid IL or missing references)
		//IL_1470: Unknown result type (might be due to invalid IL or missing references)
		//IL_1475: Unknown result type (might be due to invalid IL or missing references)
		//IL_147d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1482: Unknown result type (might be due to invalid IL or missing references)
		//IL_1489: Unknown result type (might be due to invalid IL or missing references)
		//IL_148e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1493: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b70: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2495: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2306: Unknown result type (might be due to invalid IL or missing references)
		//IL_230b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2310: Unknown result type (might be due to invalid IL or missing references)
		//IL_234d: Unknown result type (might be due to invalid IL or missing references)
		//IL_234f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2356: Unknown result type (might be due to invalid IL or missing references)
		//IL_235c: Unknown result type (might be due to invalid IL or missing references)
		//IL_235e: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.1f, 0.5f, 0.5f);
		CalamityGlobalNPC.ghostBoss = base.NPC.whoAmI;
		bool cloneAlive = false;
		if (CalamityGlobalNPC.ghostBossClone != -1)
		{
			cloneAlive = Main.npc[CalamityGlobalNPC.ghostBossClone].active;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		Vector2 vector = base.NPC.Center;
		bool speedBoost = false;
		bool despawnBoost = false;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool phase2 = lifeRatio < (death ? 0.9f : (revenge ? 0.8f : (expertMode ? 0.65f : 0.5f)));
		bool phase3 = lifeRatio < (death ? 0.6f : (revenge ? 0.5f : (expertMode ? 0.35f : 0.2f)));
		bool phase4 = lifeRatio < (death ? 0.45f : (revenge ? 0.35f : (expertMode ? 0.2f : 0.1f)));
		bool phase5 = lifeRatio < (death ? 0.2f : (revenge ? 0.15f : (expertMode ? 0.1f : 0.05f)));
		bool getPissed = !cloneAlive & phase3;
		calamityGlobalNPC.newAI[0]++;
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		bool chargePhase = calamityGlobalNPC.newAI[0] >= chargePhaseGateValue;
		int chargeAmt = (getPissed ? 4 : (phase3 ? 3 : ((!phase2) ? 1 : 2)));
		if (Main.zenithWorld)
		{
			chargeAmt = (phase4 ? int.MaxValue : (getPissed ? 6 : (phase3 ? 4 : (phase2 ? 3 : 2))));
		}
		float chargeVelocity = (getPissed ? 28f : (phase3 ? 24f : (phase2 ? 22f : 20f)));
		float chargeAcceleration = (getPissed ? 0.7f : (phase3 ? 0.6f : (phase2 ? 0.55f : 0.5f)));
		float chargeDistance = 480f;
		bool charging = base.NPC.ai[2] >= chargePhaseGateValue - 180f;
		bool reset = base.NPC.ai[2] >= chargePhaseGateValue + 120f;
		if ((Main.time >= 27000.0 && Main.time < 30600.0 && !Main.dayTime && Main.zenithWorld) || threeAM)
		{
			threeAM = true;
			chargeVelocity *= 2f;
			chargeAcceleration *= 2f;
			chargeDistance *= 3f;
			if (!phase4)
			{
				chargeAmt *= 2;
			}
		}
		if (!chargePhase)
		{
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.TargetClosest();
			}
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
			{
				base.NPC.TargetClosest();
			}
		}
		Player player = Main.player[base.NPC.target];
		float velocity = 15f;
		float acceleration = 0.075f;
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				speedBoost = true;
				despawnBoost = true;
				reachedChargingPoint = false;
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				if (cloneAlive)
				{
					Main.npc[CalamityGlobalNPC.ghostBossClone].ai[0] = 0f;
					Main.npc[CalamityGlobalNPC.ghostBossClone].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[3] = 0f;
				}
			}
		}
		if (Main.zenithWorld)
		{
			soundTimer++;
			int gate = (threeAM ? 60 : (phase4 ? 300 : (phase3 ? 420 : (phase2 ? 540 : 600))));
			if (soundTimer % gate == 0)
			{
				SoundStyle[] creepyArray = creepySounds.ToArray();
				SoundStyle selectedSound = creepyArray[Main.rand.Next(0, creepyArray.Length - 1)];
				SoundStyle style = selectedSound with
				{
					Pitch = selectedSound.Pitch - 0.8f,
					Volume = selectedSound.Volume - 0.2f
				};
				SoundEngine.PlaySound(in style, base.NPC.Center);
			}
		}
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			base.NPC.localAI[0] = 1f;
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<PolterghastHook>(), base.NPC.whoAmI);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<PolterghastHook>(), base.NPC.whoAmI);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<PolterghastHook>(), base.NPC.whoAmI);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<PolterghastHook>(), base.NPC.whoAmI);
			if (Main.zenithWorld)
			{
				for (int I = 0; I < 3; I++)
				{
					int spawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)vector.X + Math.Sin(I * 120) * 500.0), (int)((double)vector.Y + Math.Cos(I * 120) * 500.0), ModContent.NPCType<PhantomFuckYou>(), base.NPC.whoAmI, 0f, 0f, 0f, -1f);
					Main.npc[spawn].ai[0] = I * 120;
				}
			}
		}
		bool despawn = !player.ZoneDungeon && !BossRushEvent.BossRushActive && (double)player.position.Y < Main.worldSurface * 16.0;
		if (despawn)
		{
			despawnTimer--;
			if (despawnTimer <= 0)
			{
				despawnBoost = true;
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
			}
			speedBoost = true;
			velocity += 5f;
			acceleration += 0.05f;
		}
		else
		{
			despawnTimer++;
		}
		if (Vector2.Distance(player.Center, vector) > (despawnBoost ? 1500f : 6000f))
		{
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		if (phase2)
		{
			velocity += 2.5f;
			acceleration += 0.02f;
		}
		if (!phase3)
		{
			if (charging)
			{
				velocity += (phase2 ? 4.5f : 3.5f);
				acceleration += (phase2 ? 0.03f : 0.025f);
			}
		}
		else if (charging)
		{
			velocity += (phase5 ? 8.5f : 4.5f);
			acceleration += (phase5 ? 0.06f : 0.03f);
		}
		else if (phase5)
		{
			velocity += 1.5f;
			acceleration += 0.015f;
		}
		else if (phase4)
		{
			velocity++;
			acceleration += 0.01f;
		}
		else
		{
			velocity += 0.5f;
			acceleration += 0.005f;
		}
		if (expertMode)
		{
			chargeVelocity += (revenge ? 4f : 2f);
			velocity += (revenge ? 5f : 3.5f);
			acceleration += (revenge ? 0.035f : 0.025f);
		}
		base.NPC.Calamity().CurrentlyEnraged = despawn;
		base.NPC.ai[3] = 1.5f;
		float baseProjectileVelocity = (speedBoost ? 9.375f : 7.5f);
		Vector2 rotationVector = player.Center - vector;
		if (calamityGlobalNPC.newAI[3] == 0f)
		{
			float playerXDestination = player.Center.X - vector.X;
			float playerYDestination = player.Center.Y - vector.Y;
			base.NPC.rotation = (float)Math.Atan2(playerYDestination, playerXDestination) + (float)Math.PI / 2f;
		}
		else
		{
			base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		if (!chargePhase)
		{
			base.NPC.ai[2]++;
			if (reset)
			{
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
			float movementLimitX = 0f;
			float movementLimitY = 0f;
			int numHooks = 4;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.type == ModContent.NPCType<PolterghastHook>())
				{
					movementLimitX += n.Center.X;
					movementLimitY += n.Center.Y;
				}
			}
			movementLimitX /= (float)numHooks;
			movementLimitY /= (float)numHooks;
			Vector2 movementLimitVector = default(Vector2);
			((Vector2)(ref movementLimitVector))._002Ector(movementLimitX, movementLimitY);
			float movementLimitedXDist = player.Center.X - movementLimitVector.X;
			float movementLimitedYDist = player.Center.Y - movementLimitVector.Y;
			if (despawnBoost)
			{
				movementLimitedYDist *= -1f;
				movementLimitedXDist *= -1f;
				velocity += 10f;
			}
			float movementLimitedDistance = (float)Math.Sqrt(movementLimitedXDist * movementLimitedXDist + movementLimitedYDist * movementLimitedYDist);
			float maxDistanceFromHooks = (expertMode ? 650f : 500f);
			if (speedBoost)
			{
				maxDistanceFromHooks += 250f;
			}
			if (death)
			{
				maxDistanceFromHooks += maxDistanceFromHooks * 0.1f * (1f - lifeRatio);
			}
			if (death)
			{
				velocity += velocity * 0.15f * (1f - lifeRatio);
				acceleration += acceleration * 0.15f * (1f - lifeRatio);
			}
			if (movementLimitedDistance >= maxDistanceFromHooks)
			{
				movementLimitedDistance = maxDistanceFromHooks / movementLimitedDistance;
				movementLimitedXDist *= movementLimitedDistance;
				movementLimitedYDist *= movementLimitedDistance;
			}
			movementLimitX += movementLimitedXDist;
			movementLimitY += movementLimitedYDist;
			movementLimitedXDist = movementLimitX - vector.X;
			movementLimitedYDist = movementLimitY - vector.Y;
			movementLimitedDistance = (float)Math.Sqrt(movementLimitedXDist * movementLimitedXDist + movementLimitedYDist * movementLimitedYDist);
			if (movementLimitedDistance < velocity)
			{
				movementLimitedXDist = base.NPC.velocity.X;
				movementLimitedYDist = base.NPC.velocity.Y;
			}
			else
			{
				movementLimitedDistance = velocity / movementLimitedDistance;
				movementLimitedXDist *= movementLimitedDistance;
				movementLimitedYDist *= movementLimitedDistance;
			}
			if (base.NPC.velocity.X < movementLimitedXDist)
			{
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f && movementLimitedXDist > 0f)
				{
					base.NPC.velocity.X += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.X > movementLimitedXDist)
			{
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 0f && movementLimitedXDist < 0f)
				{
					base.NPC.velocity.X -= acceleration * 2f;
				}
			}
			if (base.NPC.velocity.Y < movementLimitedYDist)
			{
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f && movementLimitedYDist > 0f)
				{
					base.NPC.velocity.Y += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.Y > movementLimitedYDist)
			{
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > 0f && movementLimitedYDist < 0f)
				{
					base.NPC.velocity.Y -= acceleration * 2f;
				}
			}
		}
		else
		{
			if (calamityGlobalNPC.newAI[3] == 1f)
			{
				reachedChargingPoint = false;
				if (calamityGlobalNPC.newAI[1] == 0f)
				{
					base.NPC.velocity = Vector2.Normalize(rotationVector) * chargeVelocity;
					calamityGlobalNPC.newAI[1] = 1f;
				}
				else
				{
					calamityGlobalNPC.newAI[2]++;
					float totalChargeTime = chargeDistance * 4f / chargeVelocity;
					float slowDownTime = chargeVelocity;
					if (calamityGlobalNPC.newAI[2] >= totalChargeTime - slowDownTime)
					{
						NPC nPC = base.NPC;
						nPC.velocity *= 0.9f;
					}
					if (calamityGlobalNPC.newAI[2] >= totalChargeTime)
					{
						calamityGlobalNPC.newAI[1] = 0f;
						calamityGlobalNPC.newAI[2] = 0f;
						calamityGlobalNPC.newAI[3] = 0f;
						base.NPC.ai[1]++;
						if (base.NPC.ai[1] >= (float)chargeAmt)
						{
							calamityGlobalNPC.newAI[0] = 0f;
							base.NPC.ai[1] = 0f;
						}
						else
						{
							base.NPC.TargetClosest();
						}
					}
				}
			}
			else
			{
				if (vector.X >= player.Center.X)
				{
					calamityGlobalNPC.newAI[1] = player.Center.X + chargeDistance;
				}
				else
				{
					calamityGlobalNPC.newAI[1] = player.Center.X - chargeDistance;
				}
				if (vector.Y >= player.Center.Y)
				{
					calamityGlobalNPC.newAI[2] = player.Center.Y + chargeDistance;
				}
				else
				{
					calamityGlobalNPC.newAI[2] = player.Center.Y - chargeDistance;
				}
				Vector2 chargeVector = default(Vector2);
				((Vector2)(ref chargeVector))._002Ector(calamityGlobalNPC.newAI[1], calamityGlobalNPC.newAI[2]);
				Vector2 chargeLocationVelocity = Vector2.Normalize(chargeVector - vector) * chargeVelocity;
				Vector2 cloneChargeVector = (cloneAlive ? new Vector2(Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[1], Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[2]) : default(Vector2));
				float chargeDistanceGateValue = 40f;
				bool clonePositionCheck = !cloneAlive || Vector2.Distance(Main.npc[CalamityGlobalNPC.ghostBossClone].Center, cloneChargeVector) <= chargeDistanceGateValue;
				if (Vector2.Distance(vector, chargeVector) <= chargeDistanceGateValue || reachedChargingPoint)
				{
					if (!reachedChargingPoint)
					{
						SoundEngine.PlaySound(in SoundID.Item125, base.NPC.Center);
						for (int i = 0; i < 30; i++)
						{
							int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
							Main.dust[dust].noGravity = true;
							Dust obj = Main.dust[dust];
							obj.velocity *= 5f;
						}
					}
					reachedChargingPoint = true;
					base.NPC.velocity = Vector2.Zero;
					base.NPC.Center = chargeVector;
					if (clonePositionCheck)
					{
						calamityGlobalNPC.newAI[1] = 0f;
						calamityGlobalNPC.newAI[2] = 0f;
						calamityGlobalNPC.newAI[3] = 1f;
						if (cloneAlive)
						{
							Main.npc[CalamityGlobalNPC.ghostBossClone].ai[0] = 0f;
							Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[1] = 0f;
							Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[2] = 0f;
							Main.npc[CalamityGlobalNPC.ghostBossClone].Calamity().newAI[3] = 1f;
						}
					}
				}
				else
				{
					base.NPC.SimpleFlyMovement(chargeLocationVelocity, chargeAcceleration);
				}
			}
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			if (Main.dedServ)
			{
				NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
			}
		}
		bool isInChargePhase = charging & chargePhase;
		if (!phase2 && !phase3)
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.defense = base.NPC.defDefense;
			if (Main.netMode == 1 || isInChargePhase)
			{
				return;
			}
			base.NPC.localAI[1] += (expertMode ? 1.5f : 1f);
			if (speedBoost)
			{
				base.NPC.localAI[1] += 2f;
			}
			if (!(base.NPC.localAI[1] >= 120f))
			{
				return;
			}
			base.NPC.localAI[1] = 0f;
			bool notLiningUpCharge = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height);
			if (base.NPC.localAI[3] > 0f)
			{
				notLiningUpCharge = true;
				base.NPC.localAI[3] = 0f;
			}
			if (notLiningUpCharge)
			{
				int type = ModContent.ProjectileType<PhantomShot>();
				int damage = BlueShotDamage;
				if (Main.rand.NextBool(3))
				{
					base.NPC.localAI[1] = -30f;
					type = ModContent.ProjectileType<PhantomBlast>();
					damage = BlueBlastDamage;
				}
				Vector2 spreadVel = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * baseProjectileVelocity;
				Vector2 firingPos = base.NPC.Center + spreadVel * 3f;
				for (int j = 0; j < 6; j++)
				{
					float offset = MathHelper.ToRadians(MathHelper.Lerp(-32.5f, 32.5f, (float)j / 5f));
					Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), firingPos, spreadVel.RotatedBy(offset), type, damage, 0f, Main.myPlayer).timeLeft = ((type == ModContent.ProjectileType<PhantomBlast>()) ? 450 : 1800);
				}
			}
			else
			{
				int type2 = ModContent.ProjectileType<PhantomBlast>();
				Vector2 spreadVel2 = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (baseProjectileVelocity + 5f);
				Vector2 firingPos2 = base.NPC.Center + spreadVel2 * 3f;
				for (int k = 0; k < 6; k++)
				{
					float offset2 = MathHelper.ToRadians(MathHelper.Lerp(-40f, 40f, (float)k / 5f));
					Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), firingPos2, spreadVel2.RotatedBy(offset2), type2, BlueBlastDamage, 0f, Main.myPlayer).timeLeft = 450;
				}
			}
			return;
		}
		if (!phase3)
		{
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				SoundEngine.PlaySound(in P2Sound, base.NPC.Center);
				if (!Main.dedServ)
				{
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt").Type);
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt2").Type);
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt3").Type);
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt4").Type);
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt5").Type);
				}
				for (int l = 0; l < 10; l++)
				{
					int ghostDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
					Dust obj2 = Main.dust[ghostDust];
					obj2.velocity *= 3f;
					Main.dust[ghostDust].noGravity = true;
					if (Main.rand.NextBool())
					{
						Main.dust[ghostDust].scale = 0.5f;
						Main.dust[ghostDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int m = 0; m < 30; m++)
				{
					int ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
					Main.dust[ghostDust2].noGravity = true;
					Dust obj3 = Main.dust[ghostDust2];
					obj3.velocity *= 5f;
					ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
					Dust obj4 = Main.dust[ghostDust2];
					obj4.velocity *= 2f;
				}
			}
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * Phase2ContactDamageMult);
			base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 0.8);
			if (Main.netMode == 1 || isInChargePhase)
			{
				return;
			}
			base.NPC.localAI[1] += (expertMode ? 1.5f : 1f);
			if (speedBoost)
			{
				base.NPC.localAI[1] += 2f;
			}
			if (!(base.NPC.localAI[1] >= 200f))
			{
				return;
			}
			base.NPC.localAI[1] = 0f;
			bool notLiningUpCharge2 = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height);
			if (base.NPC.localAI[3] > 0f)
			{
				notLiningUpCharge2 = true;
				base.NPC.localAI[3] = 0f;
			}
			if (notLiningUpCharge2)
			{
				int type3 = ModContent.ProjectileType<PhantomShot2>();
				int damage2 = RedShotDamage;
				if (Main.rand.NextBool(3))
				{
					base.NPC.localAI[1] = -30f;
					type3 = ModContent.ProjectileType<PhantomBlast2>();
					damage2 = RedBlastDamage;
				}
				Vector2 spreadVel3 = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (baseProjectileVelocity + 1f);
				Vector2 firingPos3 = base.NPC.Center + spreadVel3 * 3f;
				for (int num = 0; num < 7; num++)
				{
					float offset3 = MathHelper.ToRadians(MathHelper.Lerp(-40f, 40f, (float)num / 6f));
					Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), firingPos3, spreadVel3.RotatedBy(offset3), type3, damage2, 0f, Main.myPlayer).timeLeft = ((type3 == ModContent.ProjectileType<PhantomBlast2>()) ? 450 : 1800);
				}
			}
			else
			{
				int type4 = ModContent.ProjectileType<PhantomBlast2>();
				Vector2 spreadVel4 = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (baseProjectileVelocity + 5f);
				Vector2 firingPos4 = base.NPC.Center + spreadVel4 * 3f;
				for (int num2 = 0; num2 < 7; num2++)
				{
					float offset4 = MathHelper.ToRadians(MathHelper.Lerp(-50f, 50f, (float)num2 / 6f));
					Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), firingPos4, spreadVel4.RotatedBy(offset4), type4, RedBlastDamage, 0f, Main.myPlayer).timeLeft = 450;
				}
			}
			return;
		}
		base.NPC.HitSound = SoundID.NPCHit36;
		if (base.NPC.ai[0] == 1f)
		{
			base.NPC.ai[0] = 2f;
			base.NPC.ai[1] = 0f;
			calamityGlobalNPC.newAI[0] = 0f;
			calamityGlobalNPC.newAI[1] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			if (Main.netMode != 1)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<PolterPhantom>());
				if (expertMode && !Main.zenithWorld)
				{
					for (int num3 = 0; num3 < 3; num3++)
					{
						int spawn2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)vector.X + Math.Sin(num3 * 120) * 500.0), (int)((double)vector.Y + Math.Cos(num3 * 120) * 500.0), ModContent.NPCType<PhantomFuckYou>(), base.NPC.whoAmI, 0f, 0f, 0f, -1f);
						Main.npc[spawn2].ai[0] = num3 * 120;
					}
				}
			}
			SoundEngine.PlaySound(in P3Sound, base.NPC.Center);
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt").Type);
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt2").Type);
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt3").Type);
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt4").Type);
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Polt5").Type);
			}
			for (int num4 = 0; num4 < 10; num4++)
			{
				int ghostDust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
				Dust obj5 = Main.dust[ghostDust3];
				obj5.velocity *= 3f;
				Main.dust[ghostDust3].noGravity = true;
				if (Main.rand.NextBool())
				{
					Main.dust[ghostDust3].scale = 0.5f;
					Main.dust[ghostDust3].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num5 = 0; num5 < 30; num5++)
			{
				int ghostDust4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
				Main.dust[ghostDust4].noGravity = true;
				Dust obj6 = Main.dust[ghostDust4];
				obj6.velocity *= 5f;
				ghostDust4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
				Dust obj7 = Main.dust[ghostDust4];
				obj7.velocity *= 2f;
			}
		}
		base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * Phase3ContactDamageMult);
		base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 0.5);
		base.NPC.localAI[1]++;
		if (base.NPC.localAI[1] >= (getPissed ? 200f : 280f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
		{
			base.NPC.localAI[1] = 0f;
			if (Main.netMode != 1 && !isInChargePhase)
			{
				int numProj = (getPissed ? 10 : 8);
				float maxSpread = (getPissed ? 125 : 110);
				bool num6 = Main.rand.NextBool();
				int type5 = (num6 ? ModContent.ProjectileType<PhantomShot2>() : ModContent.ProjectileType<PhantomShot>());
				int damage3 = (num6 ? RedShotDamage : BlueShotDamage);
				Vector2 spreadVel5 = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * baseProjectileVelocity;
				Vector2 firingPos5 = base.NPC.Center + spreadVel5 * 3f;
				for (int num7 = 0; num7 < numProj; num7++)
				{
					float offset5 = MathHelper.ToRadians(MathHelper.Lerp((0f - maxSpread) * 0.5f, maxSpread * 0.5f, (float)num7 / ((float)numProj - 1f)));
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), firingPos5, spreadVel5.RotatedBy(offset5), type5, damage3, 0f, Main.myPlayer);
				}
			}
		}
		if (!phase4)
		{
			return;
		}
		base.NPC.localAI[2]++;
		if (base.NPC.localAI[2] >= (getPissed ? 300f : 420f))
		{
			base.NPC.localAI[2] = 0f;
			Vector2 spiritSpawn = vector;
			float spiritXDist = player.Center.X - spiritSpawn.X;
			float spiritYDist = player.Center.Y - spiritSpawn.Y;
			float spiritDistance = (float)Math.Sqrt(spiritXDist * spiritXDist + spiritYDist * spiritYDist);
			spiritDistance = 6f / spiritDistance;
			spiritXDist *= spiritDistance;
			spiritYDist *= spiritDistance;
			spiritSpawn.X += spiritXDist * 3f;
			spiritSpawn.Y += spiritYDist * 3f;
			if (NPC.CountNPCS(ModContent.NPCType<PhantomSpiritL>()) < 2 && Main.netMode != 1 && !isInChargePhase)
			{
				SoundEngine.PlaySound(in PhantomSound, base.NPC.Center);
				int phantomSpirit = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<PhantomSpiritL>());
				Main.npc[phantomSpirit].velocity.X = spiritXDist;
				Main.npc[phantomSpirit].velocity.Y = spiritYDist;
				Main.npc[phantomSpirit].netUpdate = true;
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SupremeHealingPotion>();
	}

	public override void OnKill()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { 209 }, DownedBossSystem.downedPolterghast);
		if (!DownedBossSystem.downedPolterghast)
		{
			if (!Main.LocalPlayer.dead && Main.LocalPlayer.active)
			{
				SoundEngine.PlaySound(in ReaperShark.SearchRoarSound, Main.LocalPlayer.Center);
			}
			Color messageColor = Color.RoyalBlue;
			string sulfSeaBoostMessage = "Mods.CalamityMod.Status.Progression.GhostBossText4";
			Color sulfSeaBoostColor = AcidRainEvent.TextColor;
			if ((Main.rand.NextBool(20) && DateTime.Now.Month == 4 && DateTime.Now.Day == 1) || Main.zenithWorld)
			{
				sulfSeaBoostMessage = "Mods.CalamityMod.Status.Progression.AprilFools2";
			}
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.GhostBossText", messageColor);
			CalamityUtils.BroadcastLocalizedText(sulfSeaBoostMessage, sulfSeaBoostColor);
		}
		DownedBossSystem.downedPolterghast = true;
		CalamityNetcode.SyncWorld();
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PolterghastBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[7]
		{
			ModContent.ItemType<TerrorBlade>(),
			ModContent.ItemType<BansheeHook>(),
			ModContent.ItemType<DaemonsFlame>(),
			ModContent.ItemType<FatesReveal>(),
			ModContent.ItemType<GhastlyVisage>(),
			ModContent.ItemType<EtherealSubjugator>(),
			ModContent.ItemType<GhoulishGouger>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<RuinousSoul>(), 1, 20, 25));
		normalOnly.Add(ModContent.ItemType<Necroplasm>(), 1, 30, 40);
		normalOnly.Add(ModContent.ItemType<PolterghastMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<PolterghastTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<PolterghastRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(3124), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedPolterghast, ModContent.ItemType<LorePolterghast>(), ui: true, DropHelper.FirstKillText);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		if (threeAM)
		{
			int bloodBase = 120 - base.NPC.life / base.NPC.lifeMax;
			float roughBloodCount = (float)Math.Sqrt(0.8f * (float)bloodBase);
			int exactBloodCount = (int)roughBloodCount;
			if (Main.rand.NextFloat() < roughBloodCount - (float)exactBloodCount)
			{
				exactBloodCount++;
			}
			float velStackMult = 1f + (float)Math.Log(bloodBase);
			for (int i = 0; i < exactBloodCount; i++)
			{
				int bloodLifetime = Main.rand.Next(22, 36);
				float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
				Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
				bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
				if (Main.rand.NextBool(20))
				{
					bloodScale *= 2f;
				}
				float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 2.25f);
				Vector2 bloodVelocity = Main.rand.NextVector2Unit() * velStackMult * randomSpeedMultiplier;
				bloodVelocity.Y -= 5f;
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
			}
			for (int j = 0; j < exactBloodCount / 3; j++)
			{
				float bloodScale2 = Main.rand.NextFloat(0.2f, 0.33f);
				Color bloodColor2 = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.5f, 1f));
				Vector2 bloodVelocity2 = Main.rand.NextVector2Unit() * velStackMult * Main.rand.NextFloat(1f, 2f);
				bloodVelocity2.Y -= 2.3f;
				GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.NPC.Center, bloodVelocity2, 20, bloodScale2, bloodColor2));
			}
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Texture2D texture2D16 = Texture_Glow2.Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		int afterimageAmt = 7;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int k = 1; k < afterimageAmt; k += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - k) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 vector43 = base.NPC.Center - screenPos;
		vector43 -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		vector43 += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color c = (base.NPC.IsABestiaryIconDummy ? Color.White : base.NPC.GetAlpha(drawColor));
		spriteBatch.Draw(texture2D15, vector43, (Rectangle?)base.NPC.frame, c, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = Texture_Glow.Value;
		Color secondColorLerp = Color.Lerp(Color.White, Color.Cyan, 0.5f);
		Color lightRed = default(Color);
		((Color)(ref lightRed))._002Ector(255, 100, 100, 255);
		if (threeAM)
		{
			secondColorLerp = Color.Red;
			lightRed = Color.DarkRed;
		}
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		float timeToReachFullColor = 120f;
		float colorChangeTime = 180f;
		float changeColorGateValue = chargePhaseGateValue - colorChangeTime;
		if (base.NPC.Calamity().newAI[0] > changeColorGateValue)
		{
			secondColorLerp = Color.Lerp(secondColorLerp, lightRed, MathHelper.Clamp((base.NPC.Calamity().newAI[0] - changeColorGateValue) / timeToReachFullColor, 0f, 1f));
		}
		Color veinColor = Color.Lerp(Color.White, (base.NPC.ai[2] >= changeColorGateValue || base.NPC.Calamity().newAI[0] > changeColorGateValue) ? Color.Red : Color.Black, 0.5f);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int l = 1; l < afterimageAmt; l++)
			{
				Color otherAfterimageColor = secondColorLerp;
				otherAfterimageColor = Color.Lerp(otherAfterimageColor, Color.White, 0.5f);
				otherAfterimageColor = base.NPC.GetAlpha(otherAfterimageColor);
				otherAfterimageColor *= (float)(afterimageAmt - l) / 15f;
				Vector2 otherAfterimagePos = base.NPC.oldPos[l] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				otherAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				otherAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, otherAfterimagePos, (Rectangle?)base.NPC.frame, otherAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				Color veinAfterimageColor = veinColor;
				veinAfterimageColor = Color.Lerp(veinAfterimageColor, Color.White, 0.5f);
				veinAfterimageColor = base.NPC.GetAlpha(veinAfterimageColor);
				veinAfterimageColor *= (float)(afterimageAmt - l) / 15f;
				spriteBatch.Draw(texture2D16, otherAfterimagePos, (Rectangle?)base.NPC.frame, veinAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture2D15, vector43, (Rectangle?)base.NPC.frame, secondColorLerp, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		spriteBatch.Draw(texture2D16, vector43, (Rectangle?)base.NPC.frame, veinColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase2 = num < (death ? 0.9f : (revenge ? 0.8f : (expertMode ? 0.65f : 0.5f)));
		bool num2 = num < (death ? 0.6f : (revenge ? 0.5f : (expertMode ? 0.35f : 0.2f)));
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
		}
		if (num2)
		{
			if (base.NPC.frame.Y < frameHeight * 8)
			{
				base.NPC.frame.Y = frameHeight * 8;
			}
			if (base.NPC.frame.Y > frameHeight * 11)
			{
				base.NPC.frame.Y = frameHeight * 8;
			}
		}
		else if (phase2)
		{
			if (base.NPC.frame.Y < frameHeight * 4)
			{
				base.NPC.frame.Y = frameHeight * 4;
			}
			if (base.NPC.frame.Y > frameHeight * 7)
			{
				base.NPC.frame.Y = frameHeight * 4;
			}
		}
		else if (base.NPC.frame.Y > frameHeight * 3)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(145, 900);
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, hit.HitDirection, -1f);
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 90;
		base.NPC.height = 90;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 10; i++)
		{
			int ghostDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[ghostDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[ghostDust].scale = 0.5f;
				Main.dust[ghostDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
			Main.dust[ghostDust2].noGravity = true;
			Dust obj2 = Main.dust[ghostDust2];
			obj2.velocity *= 5f;
			ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[ghostDust2];
			obj3.velocity *= 2f;
		}
	}
}
