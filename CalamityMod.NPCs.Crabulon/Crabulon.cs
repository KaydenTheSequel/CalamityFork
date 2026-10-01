using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crabulon;

[AutoloadBossHead]
public class Crabulon : ModNPC
{
	private bool stomping;

	private const float TelegraphTimeBeforeBigJump = 20f;

	private const float DelayBeforeBigJump = 50f;

	public static Asset<Texture2D> AltTexture;

	public static Asset<Texture2D> AttackTexture;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> AltTexture_Glow;

	public static Asset<Texture2D> AttackTexture_Glow;

	public static readonly SoundStyle JumpSound = new SoundStyle("CalamityMod/Sounds/Custom/Crabulon/CrabJump");

	public static readonly SoundStyle SlamSound = new SoundStyle("CalamityMod/Sounds/Custom/Crabulon/CrabSlam", 2);

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/CrabulonHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/CrabulonDeath");

	public static int MushroomShotDamage = 9;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.32f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.55f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 54f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 80f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			AltTexture = ModContent.Request<Texture2D>(Texture + "Alt", (AssetRequestMode)2);
			AttackTexture = ModContent.Request<Texture2D>(Texture + "Attack", (AssetRequestMode)2);
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			AltTexture_Glow = ModContent.Request<Texture2D>(Texture + "AltGlow", (AssetRequestMode)2);
			AttackTexture_Glow = ModContent.Request<Texture2D>(Texture + "AttackGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 40;
		base.NPC.npcSlots = 14f;
		base.NPC.width = 196;
		base.NPC.height = 196;
		base.NPC.defense = 8;
		base.NPC.LifeMaxNERB(3500, 4400, 500000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.boss = true;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 5);
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 1.5f;
			base.NPC.defense += 12;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Crabulon")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
		writer.Write(stomping);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
		stomping = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_180e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1843: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1207: Unknown result type (might be due to invalid IL or missing references)
		//IL_2536: Unknown result type (might be due to invalid IL or missing references)
		//IL_2541: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1894: Unknown result type (might be due to invalid IL or missing references)
		//IL_18af: Unknown result type (might be due to invalid IL or missing references)
		//IL_1583: Unknown result type (might be due to invalid IL or missing references)
		//IL_158f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_2867: Unknown result type (might be due to invalid IL or missing references)
		//IL_2873: Unknown result type (might be due to invalid IL or missing references)
		//IL_2596: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_25af: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_25bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1901: Unknown result type (might be due to invalid IL or missing references)
		//IL_1233: Unknown result type (might be due to invalid IL or missing references)
		//IL_1243: Unknown result type (might be due to invalid IL or missing references)
		//IL_1253: Unknown result type (might be due to invalid IL or missing references)
		//IL_125e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1263: Unknown result type (might be due to invalid IL or missing references)
		//IL_1268: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_127a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2baf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_273f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2746: Unknown result type (might be due to invalid IL or missing references)
		//IL_274b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2753: Unknown result type (might be due to invalid IL or missing references)
		//IL_2762: Unknown result type (might be due to invalid IL or missing references)
		//IL_2767: Unknown result type (might be due to invalid IL or missing references)
		//IL_276c: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_277f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2794: Unknown result type (might be due to invalid IL or missing references)
		//IL_2799: Unknown result type (might be due to invalid IL or missing references)
		//IL_279e: Unknown result type (might be due to invalid IL or missing references)
		//IL_27a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_2603: Unknown result type (might be due to invalid IL or missing references)
		//IL_2605: Unknown result type (might be due to invalid IL or missing references)
		//IL_261e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2623: Unknown result type (might be due to invalid IL or missing references)
		//IL_2628: Unknown result type (might be due to invalid IL or missing references)
		//IL_2636: Unknown result type (might be due to invalid IL or missing references)
		//IL_2648: Unknown result type (might be due to invalid IL or missing references)
		//IL_264d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2652: Unknown result type (might be due to invalid IL or missing references)
		//IL_265b: Unknown result type (might be due to invalid IL or missing references)
		//IL_265d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2662: Unknown result type (might be due to invalid IL or missing references)
		//IL_2667: Unknown result type (might be due to invalid IL or missing references)
		//IL_2688: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e38: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1403: Unknown result type (might be due to invalid IL or missing references)
		//IL_1413: Unknown result type (might be due to invalid IL or missing references)
		//IL_1423: Unknown result type (might be due to invalid IL or missing references)
		//IL_142e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1433: Unknown result type (might be due to invalid IL or missing references)
		//IL_1438: Unknown result type (might be due to invalid IL or missing references)
		//IL_1441: Unknown result type (might be due to invalid IL or missing references)
		//IL_1445: Unknown result type (might be due to invalid IL or missing references)
		//IL_144a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12af: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1320: Unknown result type (might be due to invalid IL or missing references)
		//IL_132a: Unknown result type (might be due to invalid IL or missing references)
		//IL_132f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_134e: Unknown result type (might be due to invalid IL or missing references)
		//IL_290c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2930: Unknown result type (might be due to invalid IL or missing references)
		//IL_2936: Unknown result type (might be due to invalid IL or missing references)
		//IL_294d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2957: Unknown result type (might be due to invalid IL or missing references)
		//IL_295c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_280b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2810: Unknown result type (might be due to invalid IL or missing references)
		//IL_2815: Unknown result type (might be due to invalid IL or missing references)
		//IL_281e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2820: Unknown result type (might be due to invalid IL or missing references)
		//IL_2825: Unknown result type (might be due to invalid IL or missing references)
		//IL_282a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1645: Unknown result type (might be due to invalid IL or missing references)
		//IL_1669: Unknown result type (might be due to invalid IL or missing references)
		//IL_166f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1686: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_1695: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2207: Unknown result type (might be due to invalid IL or missing references)
		//IL_220d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2224: Unknown result type (might be due to invalid IL or missing references)
		//IL_222e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2233: Unknown result type (might be due to invalid IL or missing references)
		//IL_2059: Unknown result type (might be due to invalid IL or missing references)
		//IL_2060: Unknown result type (might be due to invalid IL or missing references)
		//IL_2065: Unknown result type (might be due to invalid IL or missing references)
		//IL_206d: Unknown result type (might be due to invalid IL or missing references)
		//IL_207c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2081: Unknown result type (might be due to invalid IL or missing references)
		//IL_2086: Unknown result type (might be due to invalid IL or missing references)
		//IL_1473: Unknown result type (might be due to invalid IL or missing references)
		//IL_148b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1491: Unknown result type (might be due to invalid IL or missing references)
		//IL_1493: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_14df: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1512: Unknown result type (might be due to invalid IL or missing references)
		//IL_2099: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_20cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_1063: Unknown result type (might be due to invalid IL or missing references)
		//IL_2119: Unknown result type (might be due to invalid IL or missing references)
		//IL_2137: Unknown result type (might be due to invalid IL or missing references)
		//IL_213c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2141: Unknown result type (might be due to invalid IL or missing references)
		//IL_214a: Unknown result type (might be due to invalid IL or missing references)
		//IL_214c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2151: Unknown result type (might be due to invalid IL or missing references)
		//IL_2156: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_111b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1126: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.NPC.Center, 0f, 0.3f, 0.7f);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		base.NPC.spriteDirection = base.NPC.direction;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = (lifeRatio < 0.66f) & expertMode;
		bool phase3 = (lifeRatio < 0.33f) & expertMode;
		bool phase4 = (lifeRatio < 0.15f) & death;
		int despawnDistanceInTiles = 500;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!player.active || player.dead || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistanceInTiles)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistanceInTiles)
			{
				base.NPC.noTileCollide = true;
				if (base.NPC.velocity.Y < -3f)
				{
					base.NPC.velocity.Y = -3f;
				}
				base.NPC.velocity.Y += 0.1f;
				if (base.NPC.velocity.Y > 12f)
				{
					base.NPC.velocity.Y = 12f;
				}
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.ai[0] != 0f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.ai[0] < 2f)
		{
			int mushBombAmt = (phase4 ? 6 : (phase3 ? 3 : ((!phase2) ? 1 : 2)));
			float fireRate = (phase4 ? 6f : (phase3 ? 3f : (phase2 ? 2f : 1f)));
			base.NPC.localAI[3] += fireRate;
			if (base.NPC.ai[3] == 0f)
			{
				float shootMushroomsGateValue = (revenge ? 120f : (expertMode ? 200f : 300f));
				if (base.NPC.localAI[3] > shootMushroomsGateValue)
				{
					base.NPC.ai[3] = 1f;
					base.NPC.localAI[3] = 0f;
				}
			}
			else if (base.NPC.localAI[3] > 30f)
			{
				base.NPC.localAI[3] = 0f;
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] >= (float)mushBombAmt)
				{
					base.NPC.ai[3] = 0f;
				}
				float mushBombSpeed = (phase4 ? 16f : (phase3 ? 14f : (phase2 ? 12f : 10f)));
				int type = ModContent.ProjectileType<MushBomb>();
				SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
				if (Main.netMode != 1)
				{
					float yVelocity = (death ? 1f : (expertMode ? 2.5f : 4f));
					Vector2 projectileVelocity = Vector2.Normalize(player.Center - base.NPC.Center) * mushBombSpeed - Vector2.UnitY * yVelocity;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity, type, MushroomShotDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
				}
			}
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.98f;
			base.NPC.ai[1]++;
			if (phase2)
			{
				base.NPC.ai[1]++;
			}
			if (phase3)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.Distance(player.Center) < 160f)
			{
				base.NPC.ai[1] += (death ? 4f : (expertMode ? 2f : 1f));
			}
			float idleTime = (phase4 ? 480f : (death ? 60f : (expertMode ? 90f : 120f)));
			if (base.NPC.ai[1] >= idleTime)
			{
				bool deathModeTripleStomp = Main.rand.NextBool() & death & phase2;
				base.NPC.TargetClosest();
				base.NPC.noGravity = !deathModeTripleStomp;
				base.NPC.noTileCollide = !deathModeTripleStomp;
				base.NPC.ai[0] = (deathModeTripleStomp ? 5f : 1f);
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			float walkingVelocity = (death ? (5f + 1f * (1f - lifeRatio)) : (expertMode ? 5f : 3.5f));
			if (phase2)
			{
				walkingVelocity += 0.5f;
			}
			if (phase3)
			{
				walkingVelocity += 0.75f;
			}
			if (phase4)
			{
				walkingVelocity++;
			}
			if (Main.getGoodWorld)
			{
				walkingVelocity *= 2f;
			}
			bool shouldWalkSlower = false;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) < 50f)
			{
				shouldWalkSlower = true;
			}
			if (shouldWalkSlower)
			{
				base.NPC.velocity.X *= 0.9f;
				if (Math.Abs(base.NPC.velocity.X) < 0.1f)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			else
			{
				float playerLocation = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				float inertia = (revenge ? 10f : 20f);
				base.NPC.velocity.X = (base.NPC.velocity.X * inertia + walkingVelocity * (float)base.NPC.direction) / (inertia + 1f);
			}
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && player.position.Y <= base.NPC.position.Y + (float)base.NPC.height && !base.NPC.collideX)
			{
				base.NPC.noGravity = false;
				base.NPC.noTileCollide = false;
			}
			else
			{
				base.NPC.noGravity = true;
				base.NPC.noTileCollide = true;
				Vector2 collisionCheckPosition = default(Vector2);
				((Vector2)(ref collisionCheckPosition))._002Ector(base.NPC.Center.X - 40f, base.NPC.position.Y + (float)base.NPC.height - 20f);
				bool num = base.NPC.position.X < player.position.X && base.NPC.position.X + (float)base.NPC.width > player.position.X + (float)player.width && base.NPC.position.Y + (float)base.NPC.height < player.position.Y + (float)player.height - 16f;
				float acceleration = (death ? 0.075f : (expertMode ? 0.05f : 0.03f));
				float acceleration2 = (death ? 0.6f : (expertMode ? 0.4f : 0.25f));
				if (num)
				{
					float fallSpeed = (death ? 1.5f : (expertMode ? 1f : 0.5f));
					base.NPC.velocity.Y += fallSpeed;
				}
				else if (Collision.SolidCollision(collisionCheckPosition, 80, 20))
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = 0f;
					}
					if (base.NPC.velocity.Y > -0.2f)
					{
						base.NPC.velocity.Y -= acceleration;
					}
					else
					{
						base.NPC.velocity.Y -= acceleration2;
					}
					float upwardSpeedCap = (death ? 9f : (expertMode ? 6f : 4f));
					if (base.NPC.velocity.Y < 0f - upwardSpeedCap)
					{
						base.NPC.velocity.Y = 0f - upwardSpeedCap;
					}
				}
				else
				{
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y = 0f;
					}
					if (base.NPC.velocity.Y < 0.1f)
					{
						base.NPC.velocity.Y += acceleration;
					}
					else
					{
						base.NPC.velocity.Y += 0.5f;
					}
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.Distance(player.Center) < 160f)
			{
				base.NPC.ai[1] += (death ? 4f : (expertMode ? 2f : 1f));
			}
			float stompPhaseGateValue = (revenge ? 150f : (expertMode ? 240f : 360f)) - (death ? (60f * (1f - lifeRatio)) : 0f);
			if (base.NPC.ai[1] >= stompPhaseGateValue)
			{
				base.NPC.noGravity = false;
				base.NPC.noTileCollide = false;
				base.NPC.ai[0] = ((Main.rand.NextBool() & revenge & phase2) ? 4f : 2f);
				base.NPC.ai[1] = 0f;
				base.NPC.ForceNetUpdate();
			}
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			base.NPC.noTileCollide = false;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] % 15f == 14f)
				{
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[1] > 0f)
				{
					if (revenge)
					{
						switch ((int)base.NPC.ai[3])
						{
						case 1:
						case 2:
							base.NPC.ai[1] += 2f;
							break;
						case 3:
							base.NPC.ai[1] += 4f;
							break;
						}
					}
					if (phase2)
					{
						base.NPC.ai[1] += ((!revenge) ? 2f : 1f);
					}
					if (phase3)
					{
						base.NPC.ai[1] += ((!revenge) ? 2f : 1f);
					}
					if (phase4)
					{
						base.NPC.ai[1]++;
					}
				}
				float jumpGateValue = (expertMode ? 60f : 120f);
				if (base.NPC.ai[1] >= jumpGateValue)
				{
					base.NPC.ai[1] = -20f;
				}
				else if (base.NPC.ai[1] == -1f)
				{
					float maxVelocityXIncrease = (death ? 4f : 3f);
					float maxVelocityYIncrease = (death ? 3f : 2f);
					float velocityX = 6f + (expertMode ? (maxVelocityXIncrease * (1f - lifeRatio)) : 0f);
					float velocityY = 12f + (expertMode ? (maxVelocityYIncrease * (1f - lifeRatio)) : 0f);
					float distanceBelowTarget = base.NPC.position.Y - (player.position.Y + 80f);
					float speedMult = 1f;
					if (revenge)
					{
						float velocityXAdjustment = velocityX;
						float velocityYAdjustment = velocityY / 3f;
						if ((lifeRatio < 0.5f) & death)
						{
							switch ((int)base.NPC.ai[3])
							{
							case 0:
								velocityX += velocityXAdjustment * 0.5f;
								velocityY -= velocityYAdjustment;
								break;
							case 1:
								velocityX += velocityXAdjustment * 0.5f;
								break;
							case 2:
								velocityX += velocityXAdjustment * 0.5f;
								velocityY -= velocityYAdjustment * 2f;
								break;
							case 3:
								velocityX += velocityXAdjustment * 1.5f;
								velocityY -= velocityYAdjustment * 2f;
								break;
							}
						}
						else
						{
							switch ((int)base.NPC.ai[3])
							{
							case 1:
								velocityY += velocityYAdjustment;
								break;
							case 2:
								velocityY -= velocityYAdjustment;
								break;
							case 3:
								velocityX += velocityXAdjustment;
								velocityY -= velocityYAdjustment;
								break;
							}
						}
						if (distanceBelowTarget > 0f)
						{
							speedMult += distanceBelowTarget * 0.001f;
						}
						if (speedMult > 2f)
						{
							speedMult = 2f;
						}
						velocityY *= speedMult;
					}
					if (expertMode)
					{
						if (player.position.Y < base.NPC.Bottom.Y)
						{
							base.NPC.velocity.Y = 0f - velocityY;
						}
						else
						{
							base.NPC.velocity.Y = 1f;
						}
						base.NPC.noTileCollide = true;
					}
					else
					{
						base.NPC.velocity.Y = 0f - velocityY;
					}
					float playerLocation2 = base.NPC.Center.X - player.Center.X;
					base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
					base.NPC.velocity.X = velocityX * (float)base.NPC.direction;
					SoundEngine.PlaySound(in JumpSound, base.NPC.Center);
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				SoundEngine.PlaySound(in SlamSound, base.NPC.Center);
				int type2 = ModContent.ProjectileType<MushBombFall>();
				if (((base.NPC.ai[2] % 2f == 0f) | death) && ((phase2 & revenge) || (phase3 & expertMode)))
				{
					SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
					if (Main.netMode != 1)
					{
						float projectileVelocity2 = (CalamityWorld.death ? 15f : 10f);
						Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
						((Vector2)(ref destination)).Normalize();
						destination *= projectileVelocity2;
						int numProj = (phase4 ? 14 : ((!CalamityWorld.death) ? 12 : (phase3 ? 10 : 16)));
						float rotation = MathHelper.ToRadians(90f);
						Vector2 randomVelocityVector = default(Vector2);
						for (int i = 0; i < numProj; i++)
						{
							Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
							((Vector2)(ref randomVelocityVector))._002Ector((Main.rand.NextFloat() - 0.5f) * 4f, (Main.rand.NextFloat() - 0.5f) * 4f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, new Vector2(perturbedSpeed.X, 0f - projectileVelocity2) + randomVelocityVector, type2, MushroomShotDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
						}
					}
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (phase2 ? 4f : 3f))
				{
					if (revenge && (!phase2 || (phase3 & death)))
					{
						SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
						if (Main.netMode != 1)
						{
							float projectileVelocity3 = (CalamityWorld.death ? 15f : 10f);
							Vector2 destination2 = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
							((Vector2)(ref destination2)).Normalize();
							destination2 *= projectileVelocity3;
							int numProj2 = (phase4 ? 8 : ((phase3 & death) ? 6 : 8));
							float rotation2 = MathHelper.ToRadians(60f);
							Vector2 randomVelocityVector2 = default(Vector2);
							for (int j = 0; j < numProj2; j++)
							{
								Vector2 perturbedSpeed2 = destination2.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)j / (float)(numProj2 - 1)));
								((Vector2)(ref randomVelocityVector2))._002Ector((Main.rand.NextFloat() - 0.5f) * 4f, (Main.rand.NextFloat() - 0.5f) * 4f);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, new Vector2(perturbedSpeed2.X, 0f - projectileVelocity3) + randomVelocityVector2, type2, MushroomShotDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
							}
						}
					}
					base.NPC.ai[0] = 0f;
					base.NPC.ai[2] = 0f;
					if (revenge)
					{
						base.NPC.ai[3] = 0f;
					}
					base.NPC.netUpdate = true;
				}
				else
				{
					float playerLocation3 = base.NPC.Center.X - player.Center.X;
					base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
					base.NPC.ai[0] = 2f;
					if (revenge)
					{
						base.NPC.ai[3]++;
					}
					base.NPC.netUpdate = true;
				}
				for (int k = (int)base.NPC.position.X - 20; k < (int)base.NPC.position.X + base.NPC.width + 40; k += 20)
				{
					for (int l = 0; l < 4; l++)
					{
						int stompDust = Dust.NewDust(new Vector2(base.NPC.position.X - 20f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 20, 4, 56, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj = Main.dust[stompDust];
						obj.velocity *= 0.2f;
					}
					if (!Main.zenithWorld)
					{
						continue;
					}
					int x = k / 16;
					int y = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
					Tile groundTile = CalamityUtils.ParanoidTileRetrieval(x, y);
					Tile walkTile = CalamityUtils.ParanoidTileRetrieval(x, y - 1);
					if (walkTile.HasTile || walkTile.LiquidAmount != 0 || !(groundTile != null) || !WorldGen.SolidTile(groundTile))
					{
						continue;
					}
					walkTile.TileFrameY = 0;
					walkTile.Get<TileWallWireStateData>().Slope = SlopeType.Solid;
					walkTile.Get<TileWallWireStateData>().IsHalfBlock = false;
					if (groundTile.TileType == 70 || groundTile.TileType == 59)
					{
						walkTile.Get<TileWallWireStateData>().HasTile = true;
						walkTile.TileType = 71;
						walkTile.TileFrameX = (short)(Main.rand.Next(5) * 18);
						if (Main.netMode == 1)
						{
							NetMessage.SendTileSquare(-1, x, y - 1, 1);
						}
					}
				}
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (!player.dead & expertMode)
				{
					if ((player.position.Y > base.NPC.Bottom.Y && base.NPC.velocity.Y > 0f) || (player.position.Y < base.NPC.Bottom.Y && base.NPC.velocity.Y < 0f))
					{
						base.NPC.noTileCollide = true;
					}
					else if ((base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > Main.player[base.NPC.target].Top.Y) || (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height)))
					{
						base.NPC.noTileCollide = false;
					}
				}
				if (base.NPC.position.X < player.position.X && base.NPC.position.X + (float)base.NPC.width > player.position.X + (float)player.width)
				{
					float slowDownMultiplier = (death ? 0.9f : (expertMode ? 0.93f : 0.96f));
					base.NPC.velocity.X *= slowDownMultiplier;
					float fallSpeedIncrease = (phase4 ? 0.2f : (death ? 0.15f : (expertMode ? 0.12f : 0.09f)));
					base.NPC.velocity.Y += fallSpeedIncrease;
				}
				else
				{
					float velocityX2 = (death ? 0.15f : (expertMode ? 0.125f : 0.1f));
					if (base.NPC.direction < 0)
					{
						base.NPC.velocity.X -= velocityX2;
					}
					else if (base.NPC.direction > 0)
					{
						base.NPC.velocity.X += velocityX2;
					}
					float maxVelocityXIncrease2 = (death ? 4f : 3f);
					float maxVelocityX = 6f + (expertMode ? (maxVelocityXIncrease2 * (1f - lifeRatio)) : 0f);
					if (revenge)
					{
						float velocityXAdjustment2 = maxVelocityX;
						if ((lifeRatio < 0.5f) & death)
						{
							switch ((int)base.NPC.ai[3])
							{
							case 0:
							case 1:
							case 2:
								maxVelocityX += velocityXAdjustment2 * 0.5f;
								break;
							case 3:
								maxVelocityX += velocityXAdjustment2 * 1.5f;
								break;
							}
						}
						else
						{
							int num2 = (int)base.NPC.ai[3];
							if ((uint)num2 > 2u && num2 == 3)
							{
								maxVelocityX += velocityXAdjustment2;
							}
						}
					}
					if (Math.Abs(base.NPC.velocity.X) > maxVelocityX)
					{
						base.NPC.velocity.X = ((base.NPC.velocity.X < 0f) ? (0f - maxVelocityX) : maxVelocityX);
					}
				}
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			if (base.NPC.velocity.Y == 0f || base.NPC.ai[2] == 1f)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= 50f)
			{
				if (base.NPC.ai[1] == 50f && base.NPC.ai[2] == 0f)
				{
					Vector2 center = base.NPC.Center;
					if (!player.dead && player.active && Math.Abs(base.NPC.Center.X - player.Center.X) / 16f <= (float)despawnDistanceInTiles)
					{
						center = player.Center;
					}
					center.Y -= 320f + Math.Abs(player.Center.Y - base.NPC.Center.Y);
					center.X += Math.Abs(player.Center.X - base.NPC.Center.X) * (float)((player.Center.X > base.NPC.Center.X) ? 1 : (-1));
					base.NPC.ai[2] = 1f;
					base.NPC.ai[3] = base.NPC.Bottom.Y;
					base.NPC.noTileCollide = true;
					float leapVelocity = (death ? 18f : 16f);
					base.NPC.velocity = center - base.NPC.Center;
					base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.Zero);
					NPC nPC2 = base.NPC;
					nPC2.velocity *= leapVelocity;
					base.NPC.velocity.X *= 0.6f;
					float velocityMinY = 0f - leapVelocity;
					if (base.NPC.velocity.Y > velocityMinY)
					{
						base.NPC.velocity.Y = velocityMinY;
					}
					base.NPC.ForceNetUpdate();
				}
				else
				{
					float mushroomFireRate = 15f;
					if (base.NPC.ai[1] % mushroomFireRate == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int type3 = ModContent.ProjectileType<MushBomb>();
							float yVelocity2 = (death ? 3f : 2f);
							if (death)
							{
								int numProj3 = 3;
								Vector2 initialVelocity = (base.NPC.Center + Vector2.UnitY * 10f - base.NPC.Center).SafeNormalize(Vector2.UnitY);
								float rotation3 = MathHelper.ToRadians(8f);
								for (int m = 0; m < numProj3; m++)
								{
									Vector2 perturbedSpeed3 = initialVelocity.RotatedBy(MathHelper.Lerp(0f - rotation3, rotation3, (float)m / (float)(numProj3 - 1)));
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed3, type3, MushroomShotDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
								}
							}
							else
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.UnitY * yVelocity2, type3, MushroomShotDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
							}
						}
					}
					if (base.NPC.velocity.Y >= 0f && base.NPC.Bottom.Y >= base.NPC.ai[3] - (float)base.NPC.height)
					{
						base.NPC.noTileCollide = false;
					}
					if (base.NPC.Bottom.Y >= base.NPC.ai[3] || base.NPC.velocity.Y == 0f)
					{
						SoundEngine.PlaySound(in SlamSound, base.NPC.Center);
						base.NPC.ai[0] = 0f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
						int type4 = ModContent.ProjectileType<MushBombGround>();
						if (Main.netMode != 1)
						{
							float xVelocity = (death ? 2f : 1f);
							int numProj4 = (death ? 5 : 3);
							Vector2 initialVelocity2 = Vector2.UnitX * xVelocity;
							Vector2 initialSpawnLocation = base.NPC.Bottom - new Vector2(0f, 8f);
							for (int n = 0; n < numProj4; n++)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), initialSpawnLocation + new Vector2((float)Main.rand.Next(0, 81), (float)Main.rand.Next(-20, 1)), initialVelocity2 - (float)n / (float)numProj4 * initialVelocity2, type4, MushroomShotDamage, 0f, Main.myPlayer);
							}
							for (int num3 = 0; num3 < numProj4; num3++)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), initialSpawnLocation + new Vector2((float)Main.rand.Next(-81, 0), (float)Main.rand.Next(-20, 1)), -(initialVelocity2 - (float)num3 / (float)numProj4 * initialVelocity2), type4, MushroomShotDamage, 0f, Main.myPlayer);
							}
						}
						for (int num4 = (int)base.NPC.position.X - 20; num4 < (int)base.NPC.position.X + base.NPC.width + 40; num4 += 20)
						{
							for (int num5 = 0; num5 < 4; num5++)
							{
								int stompDust2 = Dust.NewDust(new Vector2(base.NPC.position.X - 20f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 20, 4, 56, 0f, 0f, 100, default(Color), 1.5f);
								Dust obj2 = Main.dust[stompDust2];
								obj2.velocity *= 0.2f;
							}
							if (!Main.zenithWorld)
							{
								continue;
							}
							int x2 = num4 / 16;
							int y2 = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
							Tile groundTile2 = CalamityUtils.ParanoidTileRetrieval(x2, y2);
							Tile walkTile2 = CalamityUtils.ParanoidTileRetrieval(x2, y2 - 1);
							if (walkTile2.HasTile || walkTile2.LiquidAmount != 0 || !(groundTile2 != null) || !WorldGen.SolidTile(groundTile2))
							{
								continue;
							}
							walkTile2.TileFrameY = 0;
							walkTile2.Get<TileWallWireStateData>().Slope = SlopeType.Solid;
							walkTile2.Get<TileWallWireStateData>().IsHalfBlock = false;
							if (groundTile2.TileType == 70 || groundTile2.TileType == 59)
							{
								walkTile2.Get<TileWallWireStateData>().HasTile = true;
								walkTile2.TileType = 71;
								walkTile2.TileFrameX = (short)(Main.rand.Next(5) * 18);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, x2, y2 - 1, 1);
								}
							}
						}
					}
				}
			}
			else
			{
				base.NPC.velocity.X *= 0.8f;
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = 0;
			base.NPC.noTileCollide = false;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] % 15f == 14f)
				{
					base.NPC.netUpdate = true;
				}
				float jumpGateValue2 = 10f;
				if (base.NPC.ai[1] >= jumpGateValue2)
				{
					base.NPC.ai[1] = -20f;
				}
				else if (base.NPC.ai[1] == -1f)
				{
					base.NPC.velocity.Y = 0f - (2f + base.NPC.ai[2]);
					SoundEngine.PlaySound(in JumpSound, base.NPC.Center);
					base.NPC.ai[0] = 6f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 6f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				SoundEngine.PlaySound(in SlamSound, base.NPC.Center);
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 3f)
				{
					if (Main.netMode != 1)
					{
						int type5 = ModContent.ProjectileType<MushBombFall>();
						int maxColumns = 5;
						int mushroomsPerColumn = 8;
						Vector2 initialSpawnLocation2 = base.NPC.Bottom - new Vector2(210f, 8f);
						Vector2 initialVelocity3 = Vector2.UnitY * 24f;
						for (int num6 = 0; num6 < maxColumns; num6++)
						{
							initialVelocity3 -= Vector2.UnitY * 8f * Math.Abs(0.5f - (float)num6 / (float)(maxColumns - 1));
							for (int num7 = 0; num7 < mushroomsPerColumn; num7++)
							{
								initialVelocity3 += Vector2.UnitX * Main.rand.NextFloat(-0.5f, 0.5f);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), initialSpawnLocation2 + new Vector2(70f * (float)(num6 + 1), 0f), -(initialVelocity3 - (float)num7 / (float)mushroomsPerColumn * initialVelocity3), type5, MushroomShotDamage, 0f, Main.myPlayer, 1f, base.NPC.Bottom.Y - 16f);
							}
							initialVelocity3 = Vector2.UnitY * 16f;
						}
					}
					base.NPC.ai[0] = 1f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				else
				{
					if (Main.netMode != 1)
					{
						int type6 = ModContent.ProjectileType<MushBombGround>();
						float xVelocity2 = (death ? 3f : 1.5f);
						int numProj5 = 3;
						Vector2 initialVelocity4 = Vector2.UnitX * xVelocity2;
						Vector2 initialSpawnLocation3 = base.NPC.Bottom - new Vector2(0f, 8f);
						for (int num8 = 0; num8 < numProj5; num8++)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), initialSpawnLocation3 + new Vector2((float)Main.rand.Next(0, 41), 0f), initialVelocity4 - (float)num8 / (float)numProj5 * initialVelocity4, type6, MushroomShotDamage, 0f, Main.myPlayer);
						}
						for (int num9 = 0; num9 < numProj5; num9++)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), initialSpawnLocation3 - new Vector2((float)Main.rand.Next(0, 41), 0f), -(initialVelocity4 - (float)num9 / (float)numProj5 * initialVelocity4), type6, MushroomShotDamage, 0f, Main.myPlayer);
						}
					}
					float playerLocation4 = base.NPC.Center.X - player.Center.X;
					base.NPC.direction = ((playerLocation4 < 0f) ? 1 : (-1));
					base.NPC.ai[0] = 5f;
					base.NPC.netUpdate = true;
				}
				for (int num10 = (int)base.NPC.position.X - 20; num10 < (int)base.NPC.position.X + base.NPC.width + 40; num10 += 20)
				{
					for (int num11 = 0; num11 < 4; num11++)
					{
						int stompDust3 = Dust.NewDust(new Vector2(base.NPC.position.X - 20f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 20, 4, 56, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj3 = Main.dust[stompDust3];
						obj3.velocity *= 0.2f;
					}
					if (!Main.zenithWorld)
					{
						continue;
					}
					int x3 = num10 / 16;
					int y3 = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
					Tile groundTile3 = CalamityUtils.ParanoidTileRetrieval(x3, y3);
					Tile walkTile3 = CalamityUtils.ParanoidTileRetrieval(x3, y3 - 1);
					if (walkTile3.HasTile || walkTile3.LiquidAmount != 0 || !(groundTile3 != null) || !WorldGen.SolidTile(groundTile3))
					{
						continue;
					}
					walkTile3.TileFrameY = 0;
					walkTile3.Get<TileWallWireStateData>().Slope = SlopeType.Solid;
					walkTile3.Get<TileWallWireStateData>().IsHalfBlock = false;
					if (groundTile3.TileType == 70 || groundTile3.TileType == 59)
					{
						walkTile3.Get<TileWallWireStateData>().HasTile = true;
						walkTile3.TileType = 71;
						walkTile3.TileFrameX = (short)(Main.rand.Next(5) * 18);
						if (Main.netMode == 1)
						{
							NetMessage.SendTileSquare(-1, x3, y3 - 1, 1);
						}
					}
				}
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (!player.dead)
				{
					if ((player.position.Y > base.NPC.Bottom.Y && base.NPC.velocity.Y > 0f) || (player.position.Y < base.NPC.Bottom.Y && base.NPC.velocity.Y < 0f))
					{
						base.NPC.noTileCollide = true;
					}
					else if ((base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > Main.player[base.NPC.target].Top.Y) || (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height)))
					{
						base.NPC.noTileCollide = false;
					}
				}
			}
		}
		if (base.NPC.localAI[0] == 0f && base.NPC.life > 0)
		{
			base.NPC.localAI[0] = base.NPC.lifeMax;
		}
		if (base.NPC.life <= 0 || Main.netMode == 1)
		{
			return;
		}
		int crabShroomSpawnFreq = (int)((double)base.NPC.lifeMax * (Main.getGoodWorld ? 0.02 : 0.05));
		if (!((float)(base.NPC.life + crabShroomSpawnFreq) < base.NPC.localAI[0]))
		{
			return;
		}
		base.NPC.localAI[0] = base.NPC.life;
		int crabShroomAmt = (death ? 4 : (expertMode ? 3 : 2));
		for (int mush = 0; mush < crabShroomAmt; mush++)
		{
			int x4 = (int)(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width - 32));
			int y4 = (int)(base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height - 32));
			int npcType = ModContent.NPCType<CrabShroom>();
			int crabShroom = NPC.NewNPC(base.NPC.GetSource_FromAI(), x4, y4, npcType);
			Main.npc[crabShroom].SetDefaults(npcType);
			Main.npc[crabShroom].velocity.X = (float)Main.rand.Next(-50, 51) * (Main.getGoodWorld ? 0.2f : 0.1f);
			Main.npc[crabShroom].velocity.Y = (float)Main.rand.Next(-50, -31) * (Main.getGoodWorld ? 0.2f : 0.1f);
			if (Main.dedServ && crabShroom < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, crabShroom);
			}
		}
	}

	public override bool? CanFallThroughPlatforms()
	{
		return base.NPC.target >= 0 && Main.player[base.NPC.target].position.Y > base.NPC.position.Y + (float)base.NPC.height;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcCenter = base.NPC.Center;
		Rectangle leftHitbox = default(Rectangle);
		((Rectangle)(ref leftHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 2f + 6f * base.NPC.scale), (int)(npcCenter.Y - (float)base.NPC.height / 4f), base.NPC.width / 4, base.NPC.height / 2);
		Rectangle bodyHitbox = default(Rectangle);
		((Rectangle)(ref bodyHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 4f), (int)(npcCenter.Y - (float)base.NPC.height / 2f), base.NPC.width / 2, base.NPC.height);
		Rectangle rightHitbox = default(Rectangle);
		((Rectangle)(ref rightHitbox))._002Ector((int)(npcCenter.X + (float)base.NPC.width / 4f - 6f * base.NPC.scale), (int)(npcCenter.Y - (float)base.NPC.height / 4f), base.NPC.width / 4, base.NPC.height / 2);
		Vector2 val = new Vector2((float)(leftHitbox.X + leftHitbox.Width / 2), (float)(leftHitbox.Y + leftHitbox.Height / 2));
		Vector2 bodyHitboxCenter = default(Vector2);
		((Vector2)(ref bodyHitboxCenter))._002Ector((float)(bodyHitbox.X + bodyHitbox.Width / 2), (float)(bodyHitbox.Y + bodyHitbox.Height / 2));
		Vector2 rightHitboxCenter = default(Vector2);
		((Vector2)(ref rightHitboxCenter))._002Ector((float)(rightHitbox.X + rightHitbox.Width / 2), (float)(rightHitbox.Y + rightHitbox.Height / 2));
		Rectangle targetHitbox = target.Hitbox;
		float leftDist1 = Vector2.Distance(val, targetHitbox.TopLeft());
		float leftDist2 = Vector2.Distance(val, targetHitbox.TopRight());
		float leftDist3 = Vector2.Distance(val, targetHitbox.BottomLeft());
		float leftDist4 = Vector2.Distance(val, targetHitbox.BottomRight());
		float minLeftDist = leftDist1;
		if (leftDist2 < minLeftDist)
		{
			minLeftDist = leftDist2;
		}
		if (leftDist3 < minLeftDist)
		{
			minLeftDist = leftDist3;
		}
		if (leftDist4 < minLeftDist)
		{
			minLeftDist = leftDist4;
		}
		bool num = minLeftDist <= 45f * base.NPC.scale;
		float bodyDist1 = Vector2.Distance(bodyHitboxCenter, targetHitbox.TopLeft());
		float bodyDist2 = Vector2.Distance(bodyHitboxCenter, targetHitbox.TopRight());
		float bodyDist3 = Vector2.Distance(bodyHitboxCenter, targetHitbox.BottomLeft());
		float bodyDist4 = Vector2.Distance(bodyHitboxCenter, targetHitbox.BottomRight());
		float minBodyDist = bodyDist1;
		if (bodyDist2 < minBodyDist)
		{
			minBodyDist = bodyDist2;
		}
		if (bodyDist3 < minBodyDist)
		{
			minBodyDist = bodyDist3;
		}
		if (bodyDist4 < minBodyDist)
		{
			minBodyDist = bodyDist4;
		}
		bool insideBodyHitbox = minBodyDist <= 90f * base.NPC.scale;
		float rightDist1 = Vector2.Distance(rightHitboxCenter, targetHitbox.TopLeft());
		float rightDist2 = Vector2.Distance(rightHitboxCenter, targetHitbox.TopRight());
		float rightDist3 = Vector2.Distance(rightHitboxCenter, targetHitbox.BottomLeft());
		float rightDist4 = Vector2.Distance(rightHitboxCenter, targetHitbox.BottomRight());
		float minRightDist = rightDist1;
		if (rightDist2 < minRightDist)
		{
			minRightDist = rightDist2;
		}
		if (rightDist3 < minRightDist)
		{
			minRightDist = rightDist3;
		}
		if (rightDist4 < minRightDist)
		{
			minRightDist = rightDist4;
		}
		bool insideRightHitbox = minRightDist <= 45f * base.NPC.scale;
		return num | insideBodyHitbox | insideRightHitbox;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[0] > 1f)
		{
			if (base.NPC.velocity.Y == 0f && ((base.NPC.ai[1] >= 0f && (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 5f)) || (base.NPC.ai[1] < 30f && base.NPC.ai[0] == 4f && base.NPC.ai[2] == 0f)))
			{
				if (stomping)
				{
					stomping = false;
				}
				base.NPC.frameCounter += 0.15;
				base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
				int frame = (int)base.NPC.frameCounter;
				base.NPC.frame.Y = frame * frameHeight;
				return;
			}
			if (base.NPC.velocity.Y <= 0f || (base.NPC.ai[1] < 50f && base.NPC.ai[0] == 4f && base.NPC.ai[2] == 0f) || base.NPC.ai[1] < 0f)
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 12.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight)
				{
					base.NPC.frame.Y = frameHeight;
				}
				return;
			}
			if (!stomping)
			{
				stomping = true;
				base.NPC.frameCounter = 0.0;
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 8.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = frameHeight * 5;
			}
		}
		else
		{
			if (stomping)
			{
				stomping = false;
			}
			base.NPC.frameCounter += 0.15;
			base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
			int frame2 = (int)base.NPC.frameCounter;
			base.NPC.frame.Y = frame2 * frameHeight;
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.zenithWorld)
		{
			return null;
		}
		return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, (int)((Color)(ref drawColor)).A) * base.NPC.Opacity;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D textureIdle = TextureAssets.Npc[base.Type].Value;
		Texture2D glowIdle = Texture_Glow.Value;
		Texture2D textureWalk = AltTexture.Value;
		Texture2D glowWalk = AltTexture_Glow.Value;
		Texture2D textureAttack = AttackTexture.Value;
		Texture2D glowAttack = AttackTexture_Glow.Value;
		Color colorToShift = (Color)(Main.zenithWorld ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) : Color.Cyan);
		Color glowColor = Color.Lerp(Color.White, colorToShift, 0.5f);
		int ClonesOnEachSide = (Main.zenithWorld ? 2 : 0);
		Vector2 drawOrigin = default(Vector2);
		for (int c = -ClonesOnEachSide; c < 1 + ClonesOnEachSide; c++)
		{
			((Vector2)(ref drawOrigin))._002Ector((float)(textureIdle.Width / 2), (float)(textureIdle.Height / Main.npcFrameCount[base.Type] / 2));
			Vector2 drawPos = base.NPC.Center - screenPos + Vector2.UnitX * (float)textureIdle.Width * (float)c * 1.6f;
			if (base.NPC.ai[0] > 2f && base.NPC.ai[0] != 5f && base.NPC.velocity.Y != 0f)
			{
				((Vector2)(ref drawOrigin))._002Ector((float)(textureAttack.Width / 2), (float)(textureAttack.Height / 2));
				drawPos -= new Vector2((float)textureAttack.Width, (float)(textureAttack.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				drawPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(textureAttack, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
				spriteBatch.Draw(glowAttack, drawPos, (Rectangle?)base.NPC.frame, glowColor, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
			}
			else if (base.NPC.ai[0] == 1f)
			{
				((Vector2)(ref drawOrigin))._002Ector((float)(textureWalk.Width / 2), (float)(textureWalk.Height / 2));
				drawPos -= new Vector2((float)textureWalk.Width, (float)(textureWalk.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				drawPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(textureWalk, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
				spriteBatch.Draw(glowWalk, drawPos, (Rectangle?)base.NPC.frame, glowColor, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
			}
			else
			{
				drawPos -= new Vector2((float)textureIdle.Width, (float)(textureIdle.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				drawPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(textureIdle, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
				spriteBatch.Draw(glowIdle, drawPos, (Rectangle?)base.NPC.frame, glowColor, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		return false;
	}

	public override void BossHeadSlot(ref int index)
	{
		if (Main.zenithWorld)
		{
			index = -1;
		}
	}

	public override void ModifyHoverBoundingBox(ref Rectangle boundingBox)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			boundingBox = Rectangle.Empty;
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		if (!Main.zenithWorld)
		{
			return base.DrawHealthBar(hbPosition, ref scale, ref position);
		}
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CrabulonBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[6]
		{
			ModContent.ItemType<MycelialClaws>(),
			ModContent.ItemType<Fungicide>(),
			ModContent.ItemType<HyphaeRod>(),
			ModContent.ItemType<Mycoroot>(),
			ModContent.ItemType<InfestedClawmerang>(),
			ModContent.ItemType<PuffShroom>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<CrabulonMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<CrabulonTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<CrabulonRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<OddMushroom>(), 1, 1, 9999), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedCrabulon, ModContent.ItemType<LoreCrabulon>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnKill()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		if (!NPC.downedGoblins && Main.netMode != 1 && !Main.snowMoon && !Main.pumpkinMoon && !DD2Event.Ongoing && !Main.ShouldNormalEventsBeAbleToStart() && Main.invasionType != 1)
		{
			Main.StartInvasion();
		}
		DownedBossSystem.downedCrabulon = true;
		CalamityNetcode.SyncWorld();
		if (Main.zenithWorld && Main.netMode != 1)
		{
			for (int i = 0; i < Main.rand.Next(10, 23); i++)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-base.NPC.width / 2, base.NPC.width / 2), (int)base.NPC.Center.Y + Main.rand.Next(-base.NPC.height / 2, base.NPC.height / 2), 67);
			}
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.8f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 56, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (int)(200f * base.NPC.scale);
		base.NPC.height = (int)(100f * base.NPC.scale);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int j = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 56, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[j];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[j].scale = 0.5f;
				Main.dust[j].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int l = 0; l < 70; l++)
		{
			int stompDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 56, 0f, 0f, 100, default(Color), 3f);
			Main.dust[stompDust].noGravity = true;
			Dust obj2 = Main.dust[stompDust];
			obj2.velocity *= 5f;
			stompDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 56, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[stompDust];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon3").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon4").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon5").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon6").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Crabulon7").Type, base.NPC.scale);
		}
	}
}
