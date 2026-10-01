using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.Ores;
using CalamityMod.UI.VanillaBossBars;
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

namespace CalamityMod.NPCs.Cryogen;

[LongDistanceNetSync]
public class Cryogen : ModNPC
{
	private int currentPhase = 1;

	private int teleportLocationX;

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenHit", 3);

	public static readonly SoundStyle TransitionSound = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenPhaseTransitionCrack");

	public static readonly SoundStyle ShieldRegenSound = new SoundStyle("CalamityMod/Sounds/Custom/CryogenShieldRegenerate");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/CryogenDeath");

	public static Asset<Texture2D> Phase2Texture;

	public static Asset<Texture2D> Phase3Texture;

	public static Asset<Texture2D> Phase4Texture;

	public static Asset<Texture2D> Phase5Texture;

	public static Asset<Texture2D> Phase6Texture;

	public FireParticleSet FireDrawer;

	public static int cryoIconIndex;

	public static int pyroIconIndex;

	public static int IceBlastDamage = 23;

	public static int IceRainDamage = 23;

	public static int IceBombDamage = 28;

	public static Color BackglowColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return new Color(24, 100, 255, 80) * 0.6f;
		}
	}

	public override string Texture => "CalamityMod/NPCs/Cryogen/Cryogen_Phase1";

	public override void Load()
	{
		string cryoIconPath = "CalamityMod/NPCs/Cryogen/Cryogen_Phase1_Head_Boss";
		string pyroIconPath = "CalamityMod/NPCs/Cryogen/Pyrogen_Head_Boss";
		cryoIconIndex = CalamityMod.Instance.AddBossHeadTexture(cryoIconPath);
		pyroIconIndex = CalamityMod.Instance.AddBossHeadTexture(pyroIconPath);
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Phase2Texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Cryogen_Phase2", (AssetRequestMode)2);
			Phase3Texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Cryogen_Phase3", (AssetRequestMode)2);
			Phase4Texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Cryogen_Phase4", (AssetRequestMode)2);
			Phase5Texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Cryogen_Phase5", (AssetRequestMode)2);
			Phase6Texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Cryogen_Phase6", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 69;
		base.NPC.npcSlots = 24f;
		base.NPC.width = 86;
		base.NPC.height = 88;
		base.NPC.defense = 15;
		base.NPC.DR_NERD(0.3f);
		base.NPC.LifeMaxNERB(25000, 48000, 300000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 6);
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<CryogenBossBar>();
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.coldDamage = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.8f;
		}
		if (Main.zenithWorld)
		{
			base.NPC.Calamity().VulnerableToHeat = false;
			base.NPC.Calamity().VulnerableToCold = true;
			base.NPC.Calamity().VulnerableToWater = true;
		}
		else
		{
			base.NPC.Calamity().VulnerableToHeat = true;
			base.NPC.Calamity().VulnerableToCold = false;
			base.NPC.Calamity().VulnerableToSickness = false;
		}
	}

	public override void BossHeadSlot(ref int index)
	{
		if (Main.zenithWorld)
		{
			index = pyroIconIndex;
		}
		else
		{
			index = cryoIconIndex;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Cryogen")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(teleportLocationX);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.localAI[1]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		teleportLocationX = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.localAI[1] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b29: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_220e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2218: Unknown result type (might be due to invalid IL or missing references)
		//IL_2223: Unknown result type (might be due to invalid IL or missing references)
		//IL_222d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1998: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1855: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Unknown result type (might be due to invalid IL or missing references)
		//IL_1472: Unknown result type (might be due to invalid IL or missing references)
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_1494: Unknown result type (might be due to invalid IL or missing references)
		//IL_149f: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1429: Unknown result type (might be due to invalid IL or missing references)
		//IL_1433: Unknown result type (might be due to invalid IL or missing references)
		//IL_1438: Unknown result type (might be due to invalid IL or missing references)
		//IL_1209: Unknown result type (might be due to invalid IL or missing references)
		//IL_1225: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1daa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c43: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2aaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ab4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1602: Unknown result type (might be due to invalid IL or missing references)
		//IL_1607: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_320c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3217: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f23: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1502: Unknown result type (might be due to invalid IL or missing references)
		//IL_125d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_328b: Unknown result type (might be due to invalid IL or missing references)
		//IL_327c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_210a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1532: Unknown result type (might be due to invalid IL or missing references)
		//IL_1539: Unknown result type (might be due to invalid IL or missing references)
		//IL_153e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1712: Unknown result type (might be due to invalid IL or missing references)
		//IL_1723: Unknown result type (might be due to invalid IL or missing references)
		//IL_3290: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fec: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ff3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ff8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2357: Unknown result type (might be due to invalid IL or missing references)
		//IL_2368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3297: Unknown result type (might be due to invalid IL or missing references)
		//IL_32a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_32a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_32aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_32af: Unknown result type (might be due to invalid IL or missing references)
		//IL_32c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_32c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_32ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_32d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_32d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_32de: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2510: Unknown result type (might be due to invalid IL or missing references)
		//IL_2516: Unknown result type (might be due to invalid IL or missing references)
		//IL_2911: Unknown result type (might be due to invalid IL or missing references)
		//IL_291c: Unknown result type (might be due to invalid IL or missing references)
		//IL_257f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2956: Unknown result type (might be due to invalid IL or missing references)
		//IL_2967: Unknown result type (might be due to invalid IL or missing references)
		//IL_1918: Unknown result type (might be due to invalid IL or missing references)
		//IL_1923: Unknown result type (might be due to invalid IL or missing references)
		//IL_1929: Unknown result type (might be due to invalid IL or missing references)
		//IL_192b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1930: Unknown result type (might be due to invalid IL or missing references)
		//IL_1944: Unknown result type (might be due to invalid IL or missing references)
		//IL_1949: Unknown result type (might be due to invalid IL or missing references)
		//IL_194b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1955: Unknown result type (might be due to invalid IL or missing references)
		//IL_195a: Unknown result type (might be due to invalid IL or missing references)
		//IL_195f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2591: Unknown result type (might be due to invalid IL or missing references)
		//IL_25bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_260c: Unknown result type (might be due to invalid IL or missing references)
		//IL_216e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2179: Unknown result type (might be due to invalid IL or missing references)
		//IL_217f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2181: Unknown result type (might be due to invalid IL or missing references)
		//IL_2186: Unknown result type (might be due to invalid IL or missing references)
		//IL_219a: Unknown result type (might be due to invalid IL or missing references)
		//IL_219f: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1354: Unknown result type (might be due to invalid IL or missing references)
		//IL_1345: Unknown result type (might be due to invalid IL or missing references)
		//IL_1359: Unknown result type (might be due to invalid IL or missing references)
		//IL_2656: Unknown result type (might be due to invalid IL or missing references)
		//IL_2661: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_242c: Unknown result type (might be due to invalid IL or missing references)
		//IL_243d: Unknown result type (might be due to invalid IL or missing references)
		//IL_240b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2413: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1360: Unknown result type (might be due to invalid IL or missing references)
		//IL_136b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1371: Unknown result type (might be due to invalid IL or missing references)
		//IL_1373: Unknown result type (might be due to invalid IL or missing references)
		//IL_1378: Unknown result type (might be due to invalid IL or missing references)
		//IL_138c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_1393: Unknown result type (might be due to invalid IL or missing references)
		//IL_139d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c92: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_2729: Unknown result type (might be due to invalid IL or missing references)
		//IL_271a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_272e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c80: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_2735: Unknown result type (might be due to invalid IL or missing references)
		//IL_2740: Unknown result type (might be due to invalid IL or missing references)
		//IL_2746: Unknown result type (might be due to invalid IL or missing references)
		//IL_2748: Unknown result type (might be due to invalid IL or missing references)
		//IL_274d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2761: Unknown result type (might be due to invalid IL or missing references)
		//IL_2766: Unknown result type (might be due to invalid IL or missing references)
		//IL_2768: Unknown result type (might be due to invalid IL or missing references)
		//IL_2772: Unknown result type (might be due to invalid IL or missing references)
		//IL_2777: Unknown result type (might be due to invalid IL or missing references)
		//IL_277c: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0f, 1f, 1f);
		if (FireDrawer != null)
		{
			FireDrawer.Update();
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = (num < (revenge ? 0.85f : 0.8f)) | death;
		bool phase3 = num < (death ? 0.8f : (revenge ? 0.7f : 0.6f));
		bool phase4 = num < (death ? 0.6f : (revenge ? 0.55f : 0.4f));
		bool phase5 = num < (death ? 0.5f : (revenge ? 0.45f : 0.3f));
		bool phase6 = (num < (death ? 0.35f : 0.25f)) & revenge;
		bool phase7 = (num < (death ? 0.25f : 0.15f)) & revenge;
		int iceBlast = (Main.zenithWorld ? ModContent.ProjectileType<BrimstoneBarrage>() : ModContent.ProjectileType<IceBlast>());
		int iceBomb = (Main.zenithWorld ? ModContent.ProjectileType<SCalBrimstoneFireblast>() : ModContent.ProjectileType<IceBomb>());
		int iceRain = (Main.zenithWorld ? ModContent.ProjectileType<BrimstoneBarrage>() : ModContent.ProjectileType<IceRain>());
		int dustType = (Main.zenithWorld ? 235 : 67);
		if (!Main.zenithWorld)
		{
			_ = SoundID.Item28;
		}
		else
		{
			_ = SoundID.Item20;
		}
		base.NPC.HitSound = (Main.zenithWorld ? SoundID.NPCHit41 : HitSound);
		base.NPC.DeathSound = (Main.zenithWorld ? SoundID.NPCDeath14 : DeathSound);
		if ((int)base.NPC.ai[0] + 1 > currentPhase)
		{
			HandlePhaseTransition((int)base.NPC.ai[0] + 1);
		}
		if (base.NPC.ai[2] == 0f && base.NPC.localAI[1] == 0f)
		{
			base.NPC.localAI[1] = 1f;
			SoundEngine.PlaySound(in ShieldRegenSound, base.NPC.Center);
			if (Main.netMode != 1)
			{
				int shieldSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<CryogenShield>(), base.NPC.whoAmI);
				base.NPC.ai[2] = shieldSpawn + 1;
				base.NPC.netUpdate = true;
				Main.npc[shieldSpawn].ai[0] = base.NPC.whoAmI;
				Main.npc[shieldSpawn].netUpdate = true;
			}
		}
		int shieldTracker = (int)base.NPC.ai[2] - 1;
		if (shieldTracker != -1 && Main.npc[shieldTracker].active && Main.npc[shieldTracker].type == ModContent.NPCType<CryogenShield>())
		{
			base.NPC.dontTakeDamage = true;
		}
		else
		{
			base.NPC.dontTakeDamage = false;
			base.NPC.ai[2] = 0f;
		}
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		else if (!Main.raining && !BossRushEvent.BossRushActive)
		{
			CalamityWorld.StartRain();
		}
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				if (base.NPC.velocity.Y > 3f)
				{
					base.NPC.velocity.Y = 3f;
				}
				base.NPC.velocity.Y -= 0.1f;
				if (base.NPC.velocity.Y < -12f)
				{
					base.NPC.velocity.Y = -12f;
				}
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.ai[1] != 0f)
				{
					base.NPC.ai[1] = 0f;
					teleportLocationX = 0;
					calamityGlobalNPC.newAI[2] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (Main.getGoodWorld)
		{
			int spawnType = (Main.zenithWorld ? 156 : 243);
			if (!NPC.AnyNPCs(spawnType))
			{
				for (int i = 0; i < 1000; i++)
				{
					int enemySpawnX = (int)(base.NPC.Center.X / 16f) + Main.rand.Next(-50, 51);
					int enemySpawnY;
					for (enemySpawnY = (int)(base.NPC.Center.Y / 16f) + Main.rand.Next(-50, 51); enemySpawnY < Main.maxTilesY - 10 && !WorldGen.SolidTile(enemySpawnX, enemySpawnY); enemySpawnY++)
					{
					}
					enemySpawnY--;
					if (!WorldGen.SolidTile(enemySpawnX, enemySpawnY))
					{
						int legendEnemySpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), enemySpawnX * 16 + 8, enemySpawnY * 16, spawnType);
						if (Main.dedServ && legendEnemySpawn < Main.maxNPCs)
						{
							NetMessage.SendData(23, -1, -1, null, legendEnemySpawn);
						}
						break;
					}
				}
			}
		}
		float chargePhaseGateValue = 360f;
		float chargeDuration = 60f;
		float chargeTelegraphTime = ((base.NPC.ai[0] != 2f) ? (Main.getGoodWorld ? 90f : 120f) : (Main.getGoodWorld ? 60f : 80f));
		float chargeTelegraphRotationIncrement = 1f / chargeTelegraphTime;
		float chargeSlowDownTime = 15f;
		float chargeVelocityMin = (Main.getGoodWorld ? 24f : 12f);
		float chargeVelocityMax = (Main.getGoodWorld ? 42f : 30f);
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.7f;
			chargeDuration *= 0.8f;
		}
		float chargeGateValue = chargePhaseGateValue + chargeTelegraphTime;
		float chargeSlownDownPhaseGateValue = chargeGateValue + chargeSlowDownTime;
		bool chargePhase = base.NPC.ai[1] >= chargePhaseGateValue;
		if (expertMode && (base.NPC.ai[0] < 5f || !phase6) && !chargePhase)
		{
			calamityGlobalNPC.newAI[3]++;
			if (calamityGlobalNPC.newAI[3] >= 900f)
			{
				calamityGlobalNPC.newAI[3] = 0f;
				SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int totalProjectiles = 3;
					float radians = (float)Math.PI * 2f / (float)totalProjectiles;
					int type = iceBomb;
					float velocity = 2f + base.NPC.ai[0];
					double angleA = (double)radians * 0.5;
					double angleB = (double)MathHelper.ToRadians(90f) - angleA;
					float velocityX = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
					Vector2 spinningPoint = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX, 0f - velocity));
					for (int k = 0; k < totalProjectiles; k++)
					{
						Vector2 projSpreadRotation = spinningPoint.RotatedBy(radians * (float)k);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation) * 30f, projSpreadRotation, type, IceBombDamage, 0f, Main.myPlayer);
					}
				}
			}
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.rotation = base.NPC.velocity.X * 0.1f;
			base.NPC.localAI[0]++;
			if (base.NPC.localAI[0] >= 120f)
			{
				base.NPC.localAI[0] = 0f;
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int totalProjectiles2 = 16;
						float radians2 = (float)Math.PI * 2f / (float)totalProjectiles2;
						int type2 = iceBlast;
						float velocity2 = (death ? 9.5f : 9f);
						float projectileVelocityToPass = 0f;
						if (type2 == ModContent.ProjectileType<BrimstoneBarrage>())
						{
							projectileVelocityToPass = velocity2 * 2f;
						}
						Vector2 spinningPoint2 = default(Vector2);
						((Vector2)(ref spinningPoint2))._002Ector(0f, 0f - velocity2);
						for (int j = 0; j < totalProjectiles2; j++)
						{
							Vector2 projSpreadRotation2 = spinningPoint2.RotatedBy(radians2 * (float)j);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation2) * 30f, projSpreadRotation2, type2, IceBlastDamage, 0f, Main.myPlayer, 0f, 0f, projectileVelocityToPass);
						}
					}
				}
			}
			Vector2 cryogenCenter = default(Vector2);
			((Vector2)(ref cryogenCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float playerXDist = player.Center.X - cryogenCenter.X;
			float playerYDist = player.Center.Y - cryogenCenter.Y;
			float playerDistance = (float)Math.Sqrt(playerXDist * playerXDist + playerYDist * playerYDist);
			playerDistance = (death ? 7f : (revenge ? 5f : 4f)) / playerDistance;
			playerXDist *= playerDistance;
			playerYDist *= playerDistance;
			float inertia = 50f;
			if (Main.getGoodWorld)
			{
				inertia *= 0.5f;
			}
			base.NPC.velocity.X = (base.NPC.velocity.X * inertia + playerXDist) / (inertia + 1f);
			base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia + playerYDist) / (inertia + 1f);
			if (phase2)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 1f;
				base.NPC.localAI[0] = 0f;
				base.NPC.netUpdate = true;
				SoundEngine.PlaySound(in ShieldRegenSound, base.NPC.Center);
				if (base.NPC.ai[2] == 0f && Main.netMode != 1)
				{
					int shieldSpawn2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<CryogenShield>(), base.NPC.whoAmI);
					base.NPC.ai[2] = shieldSpawn2 + 1;
					Main.npc[shieldSpawn2].ai[0] = base.NPC.whoAmI;
					Main.npc[shieldSpawn2].netUpdate = true;
				}
			}
			return;
		}
		if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] < chargePhaseGateValue)
			{
				base.NPC.ai[1]++;
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= 120f)
				{
					base.NPC.localAI[0] = 0f;
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
					{
						SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int totalProjectiles3 = 12;
							float radians3 = (float)Math.PI * 2f / (float)totalProjectiles3;
							int type3 = iceBlast;
							float velocity3 = (death ? 9.5f : 9f);
							float projectileVelocityToPass2 = 0f;
							if (type3 == ModContent.ProjectileType<BrimstoneBarrage>())
							{
								projectileVelocityToPass2 = velocity3 * 2f;
							}
							Vector2 spinningPoint3 = default(Vector2);
							((Vector2)(ref spinningPoint3))._002Ector(0f, 0f - velocity3);
							for (int l = 0; l < totalProjectiles3; l++)
							{
								Vector2 projSpreadRotation3 = spinningPoint3.RotatedBy(radians3 * (float)l);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation3) * 30f, projSpreadRotation3, type3, IceBlastDamage, 0f, Main.myPlayer, 0f, 0f, projectileVelocityToPass2);
							}
						}
					}
				}
				float velocity4 = (death ? 3.1f : (revenge ? 3.5f : 4f));
				float acceleration = (death ? 0.185f : 0.15f);
				if (base.NPC.position.Y > player.position.Y - 375f)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.98f;
					}
					base.NPC.velocity.Y -= acceleration;
					if (base.NPC.velocity.Y > velocity4)
					{
						base.NPC.velocity.Y = velocity4;
					}
				}
				else if (base.NPC.position.Y < player.position.Y - 425f)
				{
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= 0.98f;
					}
					base.NPC.velocity.Y += acceleration;
					if (base.NPC.velocity.Y < 0f - velocity4)
					{
						base.NPC.velocity.Y = 0f - velocity4;
					}
				}
				if (base.NPC.position.X + (float)(base.NPC.width / 2) > player.position.X + (float)(player.width / 2) + 300f)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X *= 0.98f;
					}
					base.NPC.velocity.X -= acceleration;
					if (base.NPC.velocity.X > velocity4)
					{
						base.NPC.velocity.X = velocity4;
					}
				}
				if (base.NPC.position.X + (float)(base.NPC.width / 2) < player.position.X + (float)(player.width / 2) - 300f)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X *= 0.98f;
					}
					base.NPC.velocity.X += acceleration;
					if (base.NPC.velocity.X < 0f - velocity4)
					{
						base.NPC.velocity.X = 0f - velocity4;
					}
				}
			}
			else if (base.NPC.ai[1] < chargeGateValue)
			{
				base.NPC.ai[1]++;
				float totalSpreads = 3f;
				if ((base.NPC.ai[1] - chargePhaseGateValue) % (chargeTelegraphTime / totalSpreads) == 0f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type4 = iceRain;
						float maxVelocity = (death ? 9.5f : 9f);
						float velocity5 = maxVelocity - calamityGlobalNPC.newAI[0] * maxVelocity * 0.5f;
						int maxTotalProjectileReductionBasedOnRotationSpeed = 7;
						int totalProjectilesShot = 10 - (int)Math.Round(calamityGlobalNPC.newAI[0] * (float)maxTotalProjectileReductionBasedOnRotationSpeed);
						for (int m = 0; m < 2; m++)
						{
							float radians4 = (float)Math.PI * 2f / (float)totalProjectilesShot;
							float newVelocity = velocity5 - velocity5 * 0.5f * (float)m;
							float projectileVelocityToPass3 = 0f;
							if (type4 == ModContent.ProjectileType<BrimstoneBarrage>())
							{
								projectileVelocityToPass3 = velocity5 * 2f;
							}
							double angleA2 = (double)radians4 * 0.5;
							double angleB2 = (double)MathHelper.ToRadians(90f) - angleA2;
							float velocityX2 = (float)((double)newVelocity * Math.Sin(angleA2) / Math.Sin(angleB2));
							Vector2 spinningPoint4 = ((m == 0) ? new Vector2(0f, 0f - newVelocity) : new Vector2(0f - velocityX2, 0f - newVelocity));
							for (int n = 0; n < totalProjectilesShot; n++)
							{
								Vector2 projSpreadRotation4 = spinningPoint4.RotatedBy(radians4 * (float)n);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation4) * 30f, projSpreadRotation4, type4, IceRainDamage, 0f, Main.myPlayer, 0f, (type4 == ModContent.ProjectileType<BrimstoneBarrage>()) ? 0f : velocity5, projectileVelocityToPass3);
							}
						}
					}
				}
				calamityGlobalNPC.newAI[0] += chargeTelegraphRotationIncrement;
				base.NPC.rotation += calamityGlobalNPC.newAI[0];
				NPC nPC = base.NPC;
				nPC.velocity *= 0.98f;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.ai[1] == chargeGateValue)
				{
					float chargeVelocity = Vector2.Distance(base.NPC.Center, player.Center) / chargeDuration * 2f;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * (chargeVelocity + (death ? 1f : 0f));
					if (((Vector2)(ref base.NPC.velocity)).Length() < chargeVelocityMin)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC2 = base.NPC;
						nPC2.velocity *= chargeVelocityMin;
					}
					if (((Vector2)(ref base.NPC.velocity)).Length() > chargeVelocityMax)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC3 = base.NPC;
						nPC3.velocity *= chargeVelocityMax;
					}
					base.NPC.ai[1] = chargeGateValue + chargeDuration;
					calamityGlobalNPC.newAI[0] = 0f;
				}
				base.NPC.ai[1]--;
				if (base.NPC.ai[1] == chargeGateValue)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[0] = 0f;
					base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				}
				else if (base.NPC.ai[1] <= chargeSlownDownPhaseGateValue)
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.95f;
					base.NPC.rotation = base.NPC.velocity.X * 0.15f;
				}
				else
				{
					base.NPC.rotation += (float)base.NPC.direction * 0.5f;
				}
			}
			if (phase3)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 2f;
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[0] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.netUpdate = true;
				SoundEngine.PlaySound(in ShieldRegenSound, base.NPC.Center);
				if (base.NPC.ai[2] == 0f && Main.netMode != 1)
				{
					int shieldSpawn3 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<CryogenShield>(), base.NPC.whoAmI);
					base.NPC.ai[2] = shieldSpawn3 + 1;
					Main.npc[shieldSpawn3].ai[0] = base.NPC.whoAmI;
					Main.npc[shieldSpawn3].netUpdate = true;
				}
			}
			return;
		}
		if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] < chargePhaseGateValue)
			{
				base.NPC.ai[1]++;
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= 120f)
				{
					base.NPC.localAI[0] = 0f;
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
					{
						SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int totalProjectiles4 = 12;
							float radians5 = (float)Math.PI * 2f / (float)totalProjectiles4;
							int type5 = iceBlast;
							float velocity6 = (death ? 9.5f : 9f);
							float projectileVelocityToPass4 = 0f;
							if (type5 == ModContent.ProjectileType<BrimstoneBarrage>())
							{
								projectileVelocityToPass4 = velocity6 * 2f;
							}
							Vector2 spinningPoint5 = default(Vector2);
							((Vector2)(ref spinningPoint5))._002Ector(0f, 0f - velocity6);
							for (int num2 = 0; num2 < totalProjectiles4; num2++)
							{
								Vector2 projSpreadRotation5 = spinningPoint5.RotatedBy(radians5 * (float)num2);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation5) * 30f, projSpreadRotation5, type5, IceBlastDamage, 0f, Main.myPlayer, 0f, 0f, projectileVelocityToPass4);
							}
						}
					}
				}
				Vector2 cryogenCenter2 = default(Vector2);
				((Vector2)(ref cryogenCenter2))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
				float playerXDist2 = player.Center.X - cryogenCenter2.X;
				float playerYDist2 = player.Center.Y - cryogenCenter2.Y;
				float playerDistance2 = (float)Math.Sqrt(playerXDist2 * playerXDist2 + playerYDist2 * playerYDist2);
				playerDistance2 = (death ? 9f : (revenge ? 7f : 6f)) / playerDistance2;
				playerXDist2 *= playerDistance2;
				playerYDist2 *= playerDistance2;
				float inertia2 = 50f;
				if (Main.getGoodWorld)
				{
					inertia2 *= 0.5f;
				}
				base.NPC.velocity.X = (base.NPC.velocity.X * inertia2 + playerXDist2) / (inertia2 + 1f);
				base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia2 + playerYDist2) / (inertia2 + 1f);
			}
			else if (base.NPC.ai[1] < chargeGateValue)
			{
				base.NPC.ai[1]++;
				float totalSpreads2 = 2f;
				if ((base.NPC.ai[1] - chargePhaseGateValue) % (chargeTelegraphTime / totalSpreads2) == 0f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type6 = iceRain;
						float maxVelocity2 = (death ? 9.5f : 9f);
						float velocity7 = maxVelocity2 - calamityGlobalNPC.newAI[0] * maxVelocity2 * 0.5f;
						int num3 = ((calamityGlobalNPC.newAI[1] == 0f) ? 8 : 4);
						int maxTotalProjectileReductionBasedOnRotationSpeed2 = (int)((float)num3 * 0.4f);
						int totalProjectilesShot2 = num3 - (int)Math.Round(calamityGlobalNPC.newAI[0] * (float)maxTotalProjectileReductionBasedOnRotationSpeed2);
						for (int num4 = 0; num4 < 3; num4++)
						{
							float radians6 = (float)Math.PI * 2f / (float)totalProjectilesShot2;
							float newVelocity2 = velocity7 - velocity7 * 0.33f * (float)num4;
							float projectileVelocityToPass5 = 0f;
							if (type6 == ModContent.ProjectileType<BrimstoneBarrage>())
							{
								projectileVelocityToPass5 = velocity7 * 2f;
							}
							double angleA3 = (double)radians6 * 0.5;
							double angleB3 = (double)MathHelper.ToRadians(90f) - angleA3;
							float velocityX3 = (float)((double)newVelocity2 * Math.Sin(angleA3) / Math.Sin(angleB3));
							Vector2 spinningPoint6 = ((num4 == 1) ? new Vector2(0f, 0f - newVelocity2) : new Vector2(0f - velocityX3, 0f - newVelocity2));
							for (int num5 = 0; num5 < totalProjectilesShot2; num5++)
							{
								Vector2 projSpreadRotation6 = spinningPoint6.RotatedBy(radians6 * (float)num5);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation6) * 30f, projSpreadRotation6, type6, IceRainDamage, 0f, Main.myPlayer, 0f, (type6 == ModContent.ProjectileType<BrimstoneBarrage>()) ? 0f : velocity7, projectileVelocityToPass5);
							}
						}
					}
				}
				calamityGlobalNPC.newAI[0] += chargeTelegraphRotationIncrement;
				base.NPC.rotation += calamityGlobalNPC.newAI[0];
				NPC nPC5 = base.NPC;
				nPC5.velocity *= 0.98f;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.ai[1] == chargeGateValue)
				{
					float chargeVelocity2 = Vector2.Distance(base.NPC.Center, player.Center) / chargeDuration * 2f;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * (chargeVelocity2 + (death ? 1f : 0f));
					if (((Vector2)(ref base.NPC.velocity)).Length() < chargeVelocityMin)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC6 = base.NPC;
						nPC6.velocity *= chargeVelocityMin;
					}
					if (((Vector2)(ref base.NPC.velocity)).Length() > chargeVelocityMax)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC7 = base.NPC;
						nPC7.velocity *= chargeVelocityMax;
					}
					base.NPC.ai[1] = chargeGateValue + chargeDuration;
					calamityGlobalNPC.newAI[0] = 0f;
				}
				base.NPC.ai[1]--;
				if (base.NPC.ai[1] == chargeGateValue)
				{
					calamityGlobalNPC.newAI[1]++;
					if (calamityGlobalNPC.newAI[1] > 1f)
					{
						base.NPC.TargetClosest();
						base.NPC.ai[1] = 0f;
						base.NPC.localAI[0] = 0f;
						calamityGlobalNPC.newAI[1] = 0f;
					}
					else
					{
						base.NPC.ai[1] = chargePhaseGateValue;
					}
					base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				}
				else if (base.NPC.ai[1] <= chargeSlownDownPhaseGateValue)
				{
					NPC nPC8 = base.NPC;
					nPC8.velocity *= 0.95f;
					base.NPC.rotation = base.NPC.velocity.X * 0.15f;
				}
				else
				{
					base.NPC.rotation += (float)base.NPC.direction * 0.5f;
				}
			}
			if (phase4)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[0] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.netUpdate = true;
			}
			return;
		}
		if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			base.NPC.rotation = base.NPC.velocity.X * 0.1f;
			base.NPC.localAI[0]++;
			if (base.NPC.localAI[0] >= 90f && base.NPC.Opacity == 1f)
			{
				base.NPC.localAI[0] = 0f;
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int totalProjectiles5 = 12;
						float radians7 = (float)Math.PI * 2f / (float)totalProjectiles5;
						int type7 = iceBlast;
						float velocity8 = (death ? 10.5f : 10f);
						float projectileVelocityToPass6 = 0f;
						if (type7 == ModContent.ProjectileType<BrimstoneBarrage>())
						{
							projectileVelocityToPass6 = velocity8 * 2f;
						}
						Vector2 spinningPoint7 = default(Vector2);
						((Vector2)(ref spinningPoint7))._002Ector(0f, 0f - velocity8);
						for (int num6 = 0; num6 < totalProjectiles5; num6++)
						{
							Vector2 projSpreadRotation7 = spinningPoint7.RotatedBy(radians7 * (float)num6);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation7) * 30f, projSpreadRotation7, type7, IceBlastDamage, 0f, Main.myPlayer, 0f, 0f, projectileVelocityToPass6);
						}
					}
				}
			}
			Vector2 cryogenCenter3 = default(Vector2);
			((Vector2)(ref cryogenCenter3))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float playerXDist3 = player.Center.X - cryogenCenter3.X;
			float playerYDist3 = player.Center.Y - cryogenCenter3.Y;
			float playerDistance3 = (float)Math.Sqrt(playerXDist3 * playerXDist3 + playerYDist3 * playerYDist3);
			playerDistance3 = (death ? 7f : (revenge ? 5.5f : 5f)) / playerDistance3;
			playerXDist3 *= playerDistance3;
			playerYDist3 *= playerDistance3;
			float inertia3 = 50f;
			if (Main.getGoodWorld)
			{
				inertia3 *= 0.5f;
			}
			base.NPC.velocity.X = (base.NPC.velocity.X * inertia3 + playerXDist3) / (inertia3 + 1f);
			base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia3 + playerYDist3) / (inertia3 + 1f);
			if (base.NPC.ai[1] == 0f)
			{
				if (Main.netMode != 1)
				{
					base.NPC.localAI[2]++;
					if (base.NPC.localAI[2] >= 180f)
					{
						base.NPC.localAI[2] = 0f;
						int attackTimer = 0;
						int playerTileX = (int)player.Center.X / 16;
						int playerTileY = (int)player.Center.Y / 16;
						while (attackTimer <= 100)
						{
							attackTimer++;
							int min = 16;
							int max = 20;
							playerTileX = ((!Main.rand.NextBool()) ? (playerTileX - Main.rand.Next(min, max)) : (playerTileX + Main.rand.Next(min, max)));
							playerTileY = ((!Main.rand.NextBool()) ? (playerTileY - Main.rand.Next(min, max)) : (playerTileY + Main.rand.Next(min, max)));
							if (!WorldGen.SolidTile(playerTileX, playerTileY) && Collision.CanHit(new Vector2((float)(playerTileX * 16), (float)(playerTileY * 16)), 1, 1, player.position, player.width, player.height))
							{
								break;
							}
							playerTileX = (int)player.Center.X / 16;
							playerTileY = (int)player.Center.Y / 16;
						}
						base.NPC.ai[1] = 1f;
						teleportLocationX = playerTileX;
						calamityGlobalNPC.newAI[2] = playerTileY;
						base.NPC.netUpdate = true;
					}
				}
			}
			else if (base.NPC.ai[1] == 1f)
			{
				Vector2 position = default(Vector2);
				((Vector2)(ref position))._002Ector((float)teleportLocationX * 16f - (float)(base.NPC.width / 2), calamityGlobalNPC.newAI[2] * 16f - (float)(base.NPC.height / 2));
				for (int num7 = 0; num7 < 5; num7++)
				{
					int dust = Dust.NewDust(position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
				}
				base.NPC.Opacity -= 0.008f;
				if (base.NPC.Opacity <= 0f)
				{
					base.NPC.Opacity = 0f;
					base.NPC.position = position;
					for (int num8 = 0; num8 < 15; num8++)
					{
						int iceDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 3f);
						Main.dust[iceDust].noGravity = true;
					}
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
					{
						base.NPC.localAI[0] = 0f;
						SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int type8 = iceRain;
							float velocity9 = (death ? 9.5f : 9f);
							for (int num9 = 0; num9 < 3; num9++)
							{
								int totalProjectiles6 = 6;
								float radians8 = (float)Math.PI * 2f / (float)totalProjectiles6;
								float newVelocity3 = velocity9 - velocity9 * 0.33f * (float)num9;
								float projectileVelocityToPass7 = 0f;
								if (type8 == ModContent.ProjectileType<BrimstoneBarrage>())
								{
									projectileVelocityToPass7 = velocity9 * 2f;
								}
								float velocityX4 = 0f;
								if (num9 > 0)
								{
									double angleA4 = (double)radians8 * 0.33 * (double)(3 - num9);
									double angleB4 = (double)MathHelper.ToRadians(90f) - angleA4;
									velocityX4 = (float)((double)newVelocity3 * Math.Sin(angleA4) / Math.Sin(angleB4));
								}
								Vector2 spinningPoint8 = ((num9 == 0) ? new Vector2(0f, 0f - newVelocity3) : new Vector2(0f - velocityX4, 0f - newVelocity3));
								for (int num10 = 0; num10 < totalProjectiles6; num10++)
								{
									Vector2 projSpreadRotation8 = spinningPoint8.RotatedBy(radians8 * (float)num10);
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation8) * 30f, projSpreadRotation8, type8, IceRainDamage, 0f, Main.myPlayer, 0f, (type8 == ModContent.ProjectileType<BrimstoneBarrage>()) ? 0f : velocity9, projectileVelocityToPass7);
								}
							}
						}
					}
					base.NPC.TargetClosest();
					base.NPC.ai[1] = 2f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[1] == 2f)
			{
				base.NPC.Opacity += 0.2f;
				if (base.NPC.Opacity >= 1f)
				{
					base.NPC.Opacity = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			if (!phase5)
			{
				return;
			}
			base.NPC.TargetClosest();
			base.NPC.ai[0] = 4f;
			base.NPC.ai[1] = 150f;
			base.NPC.ai[3] = 0f;
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[2] = 0f;
			base.NPC.Opacity = 1f;
			teleportLocationX = 0;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.netUpdate = true;
			if (death)
			{
				SoundEngine.PlaySound(in ShieldRegenSound, base.NPC.Center);
				if (base.NPC.ai[2] == 0f && Main.netMode != 1)
				{
					int shieldSpawn4 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<CryogenShield>(), base.NPC.whoAmI);
					base.NPC.ai[2] = shieldSpawn4 + 1;
					Main.npc[shieldSpawn4].ai[0] = base.NPC.whoAmI;
					Main.npc[shieldSpawn4].netUpdate = true;
				}
			}
			int chance = 100;
			if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
			{
				chance = 20;
			}
			if (Main.zenithWorld)
			{
				chance = 1;
			}
			if (Main.rand.NextBool(chance))
			{
				string key = (Main.zenithWorld ? "Mods.CalamityMod.Status.Boss.PyrogenBossText" : "Mods.CalamityMod.Status.Boss.CryogenBossText");
				Color messageColor = (Main.zenithWorld ? Color.Orange : Color.Cyan);
				CalamityUtils.BroadcastLocalizedText(key, messageColor);
			}
			return;
		}
		if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			if (phase6)
			{
				if (base.NPC.ai[1] == 60f)
				{
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * (death ? 19f : 18f);
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
					{
						SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int type9 = iceBlast;
							float velocity10 = (death ? 1.75f : 1.5f);
							int totalSpreads3 = (phase7 ? 3 : 2);
							for (int num11 = 0; num11 < totalSpreads3; num11++)
							{
								int totalProjectiles7 = 2;
								float radians9 = (float)Math.PI * 2f / (float)totalProjectiles7;
								float newVelocity4 = velocity10 - velocity10 * (phase7 ? 0.25f : 0.5f) * (float)num11;
								float projectileVelocityToPass8 = 0f;
								if (type9 == ModContent.ProjectileType<BrimstoneBarrage>())
								{
									projectileVelocityToPass8 = velocity10 * 12f;
								}
								float velocityX5 = 0f;
								float ai = (Main.zenithWorld ? 2f : ((float)base.NPC.target));
								if (num11 > 0)
								{
									double angleA5 = (double)radians9 * (phase7 ? 0.25 : 0.5) * (double)(totalSpreads3 - num11);
									double angleB5 = (double)MathHelper.ToRadians(90f) - angleA5;
									velocityX5 = (float)((double)newVelocity4 * Math.Sin(angleA5) / Math.Sin(angleB5));
								}
								Vector2 spinningPoint9 = ((num11 == 0) ? new Vector2(0f, 0f - newVelocity4) : new Vector2(0f - velocityX5, 0f - newVelocity4));
								for (int num12 = 0; num12 < totalProjectiles7; num12++)
								{
									Vector2 projSpreadRotation9 = spinningPoint9.RotatedBy(radians9 * (float)num12);
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation9) * 30f, projSpreadRotation9, type9, IceBlastDamage, 0f, Main.myPlayer, ai, (type9 == ModContent.ProjectileType<BrimstoneBarrage>()) ? 0f : 1f, projectileVelocityToPass8);
								}
							}
						}
					}
				}
				base.NPC.ai[1]--;
				if (base.NPC.ai[1] <= 0f)
				{
					base.NPC.ai[3]++;
					if (base.NPC.ai[3] > 2f)
					{
						base.NPC.ai[0] = 5f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[3] = 0f;
						calamityGlobalNPC.newAI[3] = 0f;
					}
					else
					{
						base.NPC.ai[1] = 60f;
					}
					base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				}
				else if (base.NPC.ai[1] <= 15f)
				{
					NPC nPC9 = base.NPC;
					nPC9.velocity *= 0.95f;
					base.NPC.rotation = base.NPC.velocity.X * 0.15f;
				}
				else if (base.NPC.ai[1] > 60f)
				{
					NPC nPC10 = base.NPC;
					nPC10.velocity *= 0.98f;
					base.NPC.rotation += (150f - base.NPC.ai[1]) * 0.01f * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.rotation += (float)base.NPC.direction * 0.5f;
				}
				return;
			}
			float chargeVelMult = (death ? 19f : 18f);
			Vector2 chargeDirection = default(Vector2);
			((Vector2)(ref chargeDirection))._002Ector(base.NPC.Center.X + (float)(base.NPC.direction * 20), base.NPC.Center.Y + 6f);
			float playerchargeXDist = player.position.X + (float)player.width * 0.5f - chargeDirection.X;
			float playerchargeYDist = player.Center.Y - chargeDirection.Y;
			float playerDistance4 = (float)Math.Sqrt(playerchargeXDist * playerchargeXDist + playerchargeYDist * playerchargeYDist);
			float chargeSpeed = chargeVelMult / playerDistance4;
			playerchargeXDist *= chargeSpeed;
			playerchargeYDist *= chargeSpeed;
			calamityGlobalNPC.newAI[2]--;
			float chargeStartDistance = 300f;
			float chargeCooldown = 30f;
			if (playerDistance4 < chargeStartDistance || calamityGlobalNPC.newAI[2] > 0f)
			{
				base.NPC.damage = base.NPC.defDamage;
				if (playerDistance4 < chargeStartDistance)
				{
					calamityGlobalNPC.newAI[2] = chargeCooldown;
				}
				if (((Vector2)(ref base.NPC.velocity)).Length() < chargeVelMult)
				{
					((Vector2)(ref base.NPC.velocity)).Normalize();
					NPC nPC11 = base.NPC;
					nPC11.velocity *= chargeVelMult;
				}
				base.NPC.rotation += (float)base.NPC.direction * 0.5f;
				return;
			}
			float inertia4 = 30f;
			if (Main.getGoodWorld)
			{
				inertia4 *= 0.5f;
			}
			base.NPC.velocity.X = (base.NPC.velocity.X * inertia4 + playerchargeXDist) / (inertia4 + 1f);
			base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia4 + playerchargeYDist) / (inertia4 + 1f);
			if (playerDistance4 < chargeStartDistance + 200f)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 9f + playerchargeXDist) / 10f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 9f + playerchargeYDist) / 10f;
			}
			if (playerDistance4 < chargeStartDistance + 100f)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 4f + playerchargeXDist) / 5f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 4f + playerchargeYDist) / 5f;
			}
			base.NPC.rotation = base.NPC.velocity.X * 0.15f;
			return;
		}
		base.NPC.damage = 0;
		base.NPC.rotation = base.NPC.velocity.X * 0.1f;
		calamityGlobalNPC.newAI[3]++;
		if (calamityGlobalNPC.newAI[3] >= 75f)
		{
			calamityGlobalNPC.newAI[3] = 0f;
			SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCHit41 : HitSound, base.NPC.Center);
			int totalProjectiles8 = 2;
			float radians10 = (float)Math.PI * 2f / (float)totalProjectiles8;
			int type10 = iceBomb;
			float velocity11 = 6f;
			double angleA6 = (double)radians10 * 0.5;
			double angleB6 = (double)MathHelper.ToRadians(90f) - angleA6;
			float velocityX6 = (float)((double)velocity11 * Math.Sin(angleA6) / Math.Sin(angleB6));
			Vector2 spinningPoint10 = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity11) : new Vector2(velocityX6, 0f - velocity11));
			for (int num13 = 0; num13 < totalProjectiles8; num13++)
			{
				Vector2 projSpreadRotation10 = spinningPoint10.RotatedBy(radians10 * (float)num13);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projSpreadRotation10) * 30f, projSpreadRotation10, type10, IceBombDamage, 0f, Main.myPlayer);
			}
		}
		base.NPC.ai[1]++;
		if (base.NPC.ai[1] >= 180f)
		{
			base.NPC.TargetClosest();
			base.NPC.ai[0] = 4f;
			base.NPC.ai[1] = 60f;
			calamityGlobalNPC.newAI[3] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.netUpdate = true;
		}
		float velocity12 = (death ? 4.5f : (revenge ? 5f : 6f));
		float acceleration2 = (death ? 0.535f : 0.2f);
		if (base.NPC.position.Y > player.position.Y - 375f)
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y *= 0.98f;
			}
			base.NPC.velocity.Y -= acceleration2;
			if (base.NPC.velocity.Y > velocity12)
			{
				base.NPC.velocity.Y = velocity12;
			}
		}
		else if (base.NPC.position.Y < player.position.Y - 400f)
		{
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y *= 0.98f;
			}
			base.NPC.velocity.Y += acceleration2;
			if (base.NPC.velocity.Y < 0f - velocity12)
			{
				base.NPC.velocity.Y = 0f - velocity12;
			}
		}
		if (base.NPC.position.X + (float)(base.NPC.width / 2) > player.position.X + (float)(player.width / 2) + 350f)
		{
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.velocity.X *= 0.98f;
			}
			base.NPC.velocity.X -= acceleration2;
			if (base.NPC.velocity.X > velocity12)
			{
				base.NPC.velocity.X = velocity12;
			}
		}
		if (base.NPC.position.X + (float)(base.NPC.width / 2) < player.position.X + (float)(player.width / 2) - 350f)
		{
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.velocity.X *= 0.98f;
			}
			base.NPC.velocity.X += acceleration2;
			if (base.NPC.velocity.X < 0f - velocity12)
			{
				base.NPC.velocity.X = 0f - velocity12;
			}
		}
	}

	private void HandlePhaseTransition(int newPhase)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(Main.zenithWorld ? SoundID.NPCDeath14 : TransitionSound, base.NPC.Center);
		if (!Main.dedServ && !Main.zenithWorld)
		{
			int chipGoreAmount = ((newPhase >= 5) ? 3 : ((newPhase < 3) ? 1 : 2));
			for (int i = 1; i < chipGoreAmount; i++)
			{
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("CryoChipGore" + i).Type, base.NPC.scale);
			}
		}
		currentPhase = newPhase;
		switch (currentPhase)
		{
		case 2:
			base.NPC.defense = 13;
			base.NPC.Calamity().DR = 0.27f;
			break;
		case 3:
			base.NPC.defense = 10;
			base.NPC.Calamity().DR = 0.21f;
			break;
		case 4:
			base.NPC.defense = 6;
			base.NPC.Calamity().DR = 0.12f;
			break;
		case 5:
		case 6:
			base.NPC.defense = 0;
			base.NPC.Calamity().DR = 0f;
			break;
		case 0:
		case 1:
			break;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			float compactness = (float)base.NPC.width * 0.6f;
			if (compactness < 10f)
			{
				compactness = 10f;
			}
			float power = (float)base.NPC.height / 100f;
			if (power > 2.75f)
			{
				power = 2.75f;
			}
			if (FireDrawer == null)
			{
				FireDrawer = new FireParticleSet(int.MaxValue, 1, Color.Red * 1.25f, Color.Red, compactness, power);
			}
			else
			{
				FireDrawer.DrawSet(base.NPC.Bottom - Vector2.UnitY * (12f - base.NPC.gfxOffY));
			}
		}
		else
		{
			FireDrawer = null;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		texture = (Texture2D)(currentPhase switch
		{
			2 => Phase2Texture.Value, 
			3 => Phase3Texture.Value, 
			4 => Phase4Texture.Value, 
			5 => Phase5Texture.Value, 
			6 => Phase6Texture.Value, 
			_ => TextureAssets.Npc[base.Type].Value, 
		});
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		base.NPC.DrawBackglow(Main.zenithWorld ? Color.Red : BackglowColor, 4f, spriteEffects, base.NPC.frame, screenPos);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 drawPos = base.NPC.Center - screenPos;
		drawPos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawPos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color overlay = (Main.zenithWorld ? Color.Red : drawColor);
		spriteBatch.Draw(texture, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(overlay), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (Main.zenithWorld)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.Pyrogen");
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		int dusttype = (Main.zenithWorld ? 235 : 67);
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int icyDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[icyDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[icyDust].scale = 0.5f;
				Main.dust[icyDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int icyDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, 0f, 0f, 100, default(Color), 3f);
			Main.dust[icyDust2].noGravity = true;
			Dust obj2 = Main.dust[icyDust2];
			obj2.velocity *= 5f;
			icyDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[icyDust2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ && !Main.zenithWorld)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			for (int l = 1; l < 4; l++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CryoDeathGore" + l).Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CryoChipGore" + l).Type, base.NPC.scale);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CryogenBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[4]
		{
			ModContent.ItemType<Avalanche>(),
			ModContent.ItemType<HoarfrostBow>(),
			ModContent.ItemType<SnowstormStaff>(),
			ModContent.ItemType<Icebreaker>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<GlacialEmbrace>(), 10);
		normalOnly.Add(ModContent.ItemType<CryogenMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(ModContent.ItemType<EssenceofEleum>(), 1, 8, 10);
		normalOnly.Add(ModContent.ItemType<CryoStone>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<FrostFlare>(), DropHelper.NormalWeaponDropRateFraction);
		npcLoot.Add(ModContent.ItemType<CryogenTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<CryogenRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<BloodflareCore>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedCryogen, ModContent.ItemType<LoreArchmage>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnKill()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		if (NPC.FindFirstNPC(ModContent.NPCType<Archmage>()) == -1 && !BossRushEvent.BossRushActive)
		{
			NPC.NewNPC(base.NPC.GetSource_Death(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<Archmage>());
		}
		if (!DownedBossSystem.downedCryogen)
		{
			int num = 7;
			List<int> list = new List<int>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<int> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = 147;
			num2++;
			span[num2] = 161;
			num2++;
			span[num2] = 163;
			num2++;
			span[num2] = 200;
			num2++;
			span[num2] = 164;
			num2++;
			span[num2] = ModContent.TileType<AstralSnow>();
			num2++;
			span[num2] = ModContent.TileType<AstralIce>();
			List<int> tileTypes = list;
			if (Main.drunkWorld)
			{
				tileTypes.Add(60);
			}
			CalamityUtils.SpawnOre(ModContent.TileType<CryonicOre>(), 0.00016, 0.45f, 0.7f, 6, 11, tileTypes);
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.IceOreText", Color.LightSkyBlue);
		}
		DownedBossSystem.downedCryogen = true;
		CalamityNetcode.SyncWorld();
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		return minDist <= 40f * base.NPC.scale;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			if (Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
			}
			else
			{
				target.AddBuff(46, 120);
			}
		}
	}
}
