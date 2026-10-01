using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
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
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.PlaguebringerGoliath;

[AutoloadBossHead]
public class PlaguebringerGoliath : ModNPC
{
	private int biomeEnrageTimer = 300;

	private const float MissileAngleSpread = 60f;

	private const int MissileProjectiles = 8;

	private int MissileCountdown;

	private int despawnTimer = 120;

	private int chargeDistance;

	private bool charging;

	private bool halfLife;

	private bool canDespawn;

	private bool flyingFrame2;

	private int curTex = 1;

	public static Asset<Texture2D> ChargeTexture;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> ChargeTexture_Glow;

	public static readonly SoundStyle NukeWarningSound = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGNukeWarning");

	public static readonly SoundStyle AttackSwitchSound = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGAttackSwitch", 2);

	public static readonly SoundStyle DashSound = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGDash");

	public static readonly SoundStyle BarrageLaunchSound = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGBarrageLaunch");

	public static int StingerDamage = 28;

	public static int NukeDamage = 35;

	public static Color BackglowColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 100, 24, 80);
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = -56f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -8f;
		nPCBestiaryDrawModifiers.SpriteDirection = -1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X -= 48f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			ChargeTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliathChargeTex", (AssetRequestMode)2);
			Texture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliathGlow", (AssetRequestMode)2);
			ChargeTexture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliathChargeTexGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 90;
		base.NPC.npcSlots = 64f;
		base.NPC.width = 198;
		base.NPC.height = 198;
		base.NPC.defense = 50;
		base.NPC.DR_NERD(0.3f);
		base.NPC.LifeMaxNERB(70000, 105000, 370000);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.boss = true;
		base.NPC.value = Item.buyPrice(0, 25);
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PlaguebringerGoliath")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(biomeEnrageTimer);
		writer.Write(halfLife);
		writer.Write(canDespawn);
		writer.Write(flyingFrame2);
		writer.Write(MissileCountdown);
		writer.Write(despawnTimer);
		writer.Write(chargeDistance);
		writer.Write(charging);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		biomeEnrageTimer = reader.ReadInt32();
		halfLife = reader.ReadBoolean();
		canDespawn = reader.ReadBoolean();
		flyingFrame2 = reader.ReadBoolean();
		MissileCountdown = reader.ReadInt32();
		despawnTimer = reader.ReadInt32();
		chargeDistance = reader.ReadInt32();
		charging = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_1515: Unknown result type (might be due to invalid IL or missing references)
		//IL_151a: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1503: Unknown result type (might be due to invalid IL or missing references)
		//IL_1344: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1949: Unknown result type (might be due to invalid IL or missing references)
		//IL_194e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1932: Unknown result type (might be due to invalid IL or missing references)
		//IL_1937: Unknown result type (might be due to invalid IL or missing references)
		//IL_152a: Unknown result type (might be due to invalid IL or missing references)
		//IL_152f: Unknown result type (might be due to invalid IL or missing references)
		//IL_155e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1565: Unknown result type (might be due to invalid IL or missing references)
		//IL_1572: Unknown result type (might be due to invalid IL or missing references)
		//IL_157b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_195e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1963: Unknown result type (might be due to invalid IL or missing references)
		//IL_1992: Unknown result type (might be due to invalid IL or missing references)
		//IL_1999: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1646: Unknown result type (might be due to invalid IL or missing references)
		//IL_1651: Unknown result type (might be due to invalid IL or missing references)
		//IL_138d: Unknown result type (might be due to invalid IL or missing references)
		//IL_139d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13be: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1abe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_167e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1686: Unknown result type (might be due to invalid IL or missing references)
		//IL_2324: Unknown result type (might be due to invalid IL or missing references)
		//IL_2330: Unknown result type (might be due to invalid IL or missing references)
		//IL_2357: Unknown result type (might be due to invalid IL or missing references)
		//IL_2363: Unknown result type (might be due to invalid IL or missing references)
		//IL_237b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2387: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1727: Unknown result type (might be due to invalid IL or missing references)
		//IL_172f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1419: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_2971: Unknown result type (might be due to invalid IL or missing references)
		//IL_297d: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25db: Unknown result type (might be due to invalid IL or missing references)
		//IL_23db: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_29da: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2407: Unknown result type (might be due to invalid IL or missing references)
		//IL_240c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2411: Unknown result type (might be due to invalid IL or missing references)
		//IL_241a: Unknown result type (might be due to invalid IL or missing references)
		//IL_241e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2423: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_272a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2736: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1835: Unknown result type (might be due to invalid IL or missing references)
		//IL_1841: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2769: Unknown result type (might be due to invalid IL or missing references)
		//IL_2775: Unknown result type (might be due to invalid IL or missing references)
		//IL_2441: Unknown result type (might be due to invalid IL or missing references)
		//IL_2448: Unknown result type (might be due to invalid IL or missing references)
		//IL_244d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bed: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_2547: Unknown result type (might be due to invalid IL or missing references)
		//IL_254c: Unknown result type (might be due to invalid IL or missing references)
		//IL_246e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2475: Unknown result type (might be due to invalid IL or missing references)
		//IL_247a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2124: Unknown result type (might be due to invalid IL or missing references)
		//IL_2130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2495: Unknown result type (might be due to invalid IL or missing references)
		//IL_249a: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24df: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21de: Unknown result type (might be due to invalid IL or missing references)
		//IL_1128: Unknown result type (might be due to invalid IL or missing references)
		//IL_1134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_2afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_119e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_101a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1061: Unknown result type (might be due to invalid IL or missing references)
		//IL_107a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1201: Unknown result type (might be due to invalid IL or missing references)
		//IL_1206: Unknown result type (might be due to invalid IL or missing references)
		//IL_1220: Unknown result type (might be due to invalid IL or missing references)
		//IL_122a: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Unknown result type (might be due to invalid IL or missing references)
		//IL_124e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1258: Unknown result type (might be due to invalid IL or missing references)
		//IL_125d: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		base.NPC.gfxOffY = (charging ? (-40) : (-50));
		base.NPC.width = base.NPC.frame.Width / 2;
		base.NPC.height = (int)((float)base.NPC.frame.Height * (charging ? 1.5f : 1.8f));
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < 0.75f;
		bool phase3 = lifeRatio < 0.5f;
		bool phase4 = lifeRatio < 0.25f;
		bool phase5 = lifeRatio < 0.1f;
		float challengeAmt = (1f - lifeRatio) * 100f;
		float nukeBarrageChallengeAmt = (0.5f - lifeRatio) * 200f;
		if (Main.getGoodWorld)
		{
			challengeAmt *= 1.5f;
			nukeBarrageChallengeAmt *= 1.5f;
		}
		bool immuneToSlowingDebuffs = base.NPC.ai[0] == 0f || base.NPC.ai[0] == 4f;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.3f, 0.7f, 0f);
		if (!halfLife & phase3 & expertMode)
		{
			Color messageColor = Color.Lime;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.PlagueBossText", messageColor);
			SoundEngine.PlaySound(in NukeWarningSound, base.NPC.Center);
			halfLife = true;
		}
		if (halfLife && MissileCountdown == 0)
		{
			MissileCountdown = (Main.getGoodWorld ? 300 : 600);
		}
		if (MissileCountdown > 1)
		{
			MissileCountdown--;
		}
		int activePlayers = Main.CurrentFrameFlags.ActivePlayersCount;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		Vector2 distFromPlayer = player.Center - base.NPC.Center;
		if (!player.ZoneJungle && !BossRushEvent.BossRushActive)
		{
			if (biomeEnrageTimer > 0)
			{
				biomeEnrageTimer--;
			}
		}
		else
		{
			biomeEnrageTimer = 300;
		}
		bool num = biomeEnrageTimer <= 0;
		float enrageScale = (death ? 0.5f : 0f);
		if (num)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 1.5f;
		}
		if (enrageScale > 1.5f)
		{
			enrageScale = 1.5f;
		}
		if (Main.getGoodWorld)
		{
			enrageScale += 0.5f;
		}
		bool diagonalDash = revenge & phase2;
		if (base.NPC.ai[0] != 0f && base.NPC.ai[0] != 4f)
		{
			base.NPC.rotation = base.NPC.velocity.X * 0.02f;
		}
		if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if ((!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f) && despawnTimer > 0)
			{
				despawnTimer--;
			}
		}
		else
		{
			despawnTimer = 120;
		}
		canDespawn = despawnTimer <= 0;
		if (canDespawn)
		{
			base.NPC.damage = 0;
			if (base.NPC.velocity.Y > 3f)
			{
				base.NPC.velocity.Y = 3f;
			}
			base.NPC.velocity.Y -= 0.2f;
			if (base.NPC.velocity.Y < -16f)
			{
				base.NPC.velocity.Y = -16f;
			}
			if (base.NPC.timeLeft > 60)
			{
				base.NPC.timeLeft = 60;
			}
			if (base.NPC.ai[0] != -1f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				MissileCountdown = 0;
				chargeDistance = 0;
				base.NPC.netUpdate = true;
			}
			return;
		}
		if (calamityGlobalNPC.newAI[3] == 0f)
		{
			calamityGlobalNPC.newAI[3] = 1f;
			base.NPC.ai[0] = 2f;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.ai[0] == -1f)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			int attackSwitch;
			do
			{
				attackSwitch = ((MissileCountdown == 1) ? 4 : Main.rand.Next(4));
			}
			while ((float)attackSwitch == base.NPC.ai[1] || attackSwitch == 1);
			if (((attackSwitch == 0) & diagonalDash) && ((Vector2)(ref distFromPlayer)).Length() < 1800f)
			{
				do
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						chargeDistance = 0;
						break;
					case 1:
						chargeDistance = 400;
						break;
					case 2:
						chargeDistance = -400;
						break;
					}
				}
				while ((float)chargeDistance == base.NPC.ai[3]);
				base.NPC.ai[3] = -chargeDistance;
			}
			base.NPC.ai[0] = attackSwitch;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.TargetClosest();
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
		}
		else if (base.NPC.ai[0] == 0f)
		{
			int chargeDistanceX = (revenge ? 525 : 550);
			if (phase4)
			{
				chargeDistanceX = (revenge ? 450 : 475);
			}
			else if (phase3)
			{
				chargeDistanceX = (revenge ? 475 : 500);
			}
			else if (phase2)
			{
				chargeDistanceX = (revenge ? 500 : 525);
			}
			chargeDistanceX -= (int)(25f * enrageScale);
			float chargeSpeed = (revenge ? 28f : 26f);
			if (phase2)
			{
				chargeSpeed++;
			}
			if (phase3)
			{
				chargeSpeed++;
			}
			if (phase4)
			{
				chargeSpeed++;
			}
			if (phase5)
			{
				chargeSpeed++;
			}
			chargeSpeed += 2f * enrageScale;
			int phaseSwitchTimer = (int)Math.Ceiling(2f + enrageScale);
			if ((base.NPC.ai[1] > (float)(2 * phaseSwitchTimer) && base.NPC.ai[1] % 2f == 0f) || ((Vector2)(ref distFromPlayer)).Length() > 1800f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
				return;
			}
			if (base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.damage = 0;
				float playerLocation = base.NPC.Center.X - player.Center.X;
				float chargeDistanceY = 20f;
				chargeDistanceY += 20f * enrageScale;
				float distanceFromTargetX = Math.Abs(base.NPC.Center.X - player.Center.X);
				if (Math.Abs(base.NPC.Center.Y - (player.Center.Y - (float)chargeDistance)) < chargeDistanceY && distanceFromTargetX >= (float)chargeDistanceX)
				{
					base.NPC.damage = base.NPC.defDamage;
					if (diagonalDash)
					{
						switch (Main.rand.Next(3))
						{
						case 0:
							chargeDistance = 0;
							break;
						case 1:
							chargeDistance = 400;
							break;
						case 2:
							chargeDistance = -400;
							break;
						}
					}
					charging = true;
					base.NPC.frameCounter = 4.0;
					base.NPC.ai[1]++;
					base.NPC.ai[2] = 0f;
					float targetX = player.Center.X - base.NPC.Center.X;
					float targetY = player.Center.Y - base.NPC.Center.Y;
					float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
					targetDistance = chargeSpeed / targetDistance;
					base.NPC.velocity.X = targetX * targetDistance;
					base.NPC.velocity.Y = targetY * targetDistance;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					calamityGlobalNPC.newAI[1] = base.NPC.velocity.X;
					calamityGlobalNPC.newAI[2] = base.NPC.velocity.Y;
					base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
					base.NPC.spriteDirection = base.NPC.direction;
					if (base.NPC.spriteDirection != 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.ForceNetUpdate();
					SoundEngine.PlaySound(in DashSound, base.NPC.Center);
					return;
				}
				base.NPC.rotation = base.NPC.velocity.X * 0.02f;
				charging = false;
				float maxLineUpSpeed = (revenge ? 20f : 16f);
				float lineUpAccel = (revenge ? 0.4f : 0.3f);
				if (phase2)
				{
					maxLineUpSpeed += 3f;
					lineUpAccel += 0.15f;
				}
				if (phase4)
				{
					maxLineUpSpeed += 3f;
					lineUpAccel += 0.15f;
				}
				maxLineUpSpeed += 7f * enrageScale;
				lineUpAccel += 0.35f * enrageScale;
				if (base.NPC.Center.Y < player.Center.Y - (float)chargeDistance - chargeDistanceY)
				{
					base.NPC.velocity.Y += lineUpAccel;
				}
				else if (base.NPC.Center.Y > player.Center.Y - (float)chargeDistance + chargeDistanceY)
				{
					base.NPC.velocity.Y -= lineUpAccel;
				}
				else
				{
					base.NPC.velocity.Y *= 0.7f;
				}
				if (base.NPC.velocity.Y < 0f - maxLineUpSpeed)
				{
					base.NPC.velocity.Y = 0f - maxLineUpSpeed;
				}
				if (base.NPC.velocity.Y > maxLineUpSpeed)
				{
					base.NPC.velocity.Y = maxLineUpSpeed;
				}
				float distanceXMax = 100f;
				float distanceXMin = 20f;
				if (distanceFromTargetX > (float)chargeDistanceX + distanceXMax)
				{
					base.NPC.velocity.X += lineUpAccel * (float)base.NPC.direction;
				}
				else if (distanceFromTargetX < (float)chargeDistanceX + distanceXMin)
				{
					base.NPC.velocity.X -= lineUpAccel * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.velocity.X *= 0.7f;
				}
				if (base.NPC.velocity.X < 0f - maxLineUpSpeed)
				{
					base.NPC.velocity.X = 0f - maxLineUpSpeed;
				}
				if (base.NPC.velocity.X > maxLineUpSpeed)
				{
					base.NPC.velocity.X = maxLineUpSpeed;
				}
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.ForceNetUpdate();
				return;
			}
			base.NPC.damage = base.NPC.defDamage;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			int chargeDirectionXSign = 1;
			if (base.NPC.Center.X < player.Center.X)
			{
				chargeDirectionXSign = -1;
			}
			if (base.NPC.direction == chargeDirectionXSign && Math.Abs(base.NPC.Center.X - player.Center.X) > (float)chargeDistanceX)
			{
				base.NPC.ai[2] = 1f;
			}
			if (Math.Abs(base.NPC.Center.Y - player.Center.Y) > (float)chargeDistanceX * 1.5f)
			{
				base.NPC.ai[2] = 1f;
			}
			if (enrageScale > 0f && base.NPC.ai[2] == 1f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.95f;
			}
			if (base.NPC.ai[2] != 1f)
			{
				charging = true;
				base.NPC.frameCounter = 4.0;
				if (((Vector2)(ref base.NPC.velocity)).Length() < chargeSpeed)
				{
					base.NPC.velocity = new Vector2(calamityGlobalNPC.newAI[1], calamityGlobalNPC.newAI[2]);
				}
				calamityGlobalNPC.newAI[0]++;
				if (calamityGlobalNPC.newAI[0] > 90f)
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 1.01f;
				}
				if (Main.zenithWorld && calamityGlobalNPC.newAI[0] % 6f == 0f && Main.netMode != 1)
				{
					try
					{
						int tilePositionX = (int)(base.NPC.Center.X / 16f);
						int tilePositionY = (int)(base.NPC.Center.Y / 16f);
						if (!WorldGen.SolidTile(tilePositionX, tilePositionY) && Main.tile[tilePositionX, tilePositionY].LiquidAmount == 0)
						{
							Main.tile[tilePositionX, tilePositionY].LiquidAmount = (byte)Main.rand.Next(50, 150);
							Main.tile[tilePositionX, tilePositionY].Get<LiquidData>().LiquidType = 2;
							WorldGen.SquareTileFrame(tilePositionX, tilePositionY);
						}
					}
					catch
					{
					}
				}
				base.NPC.netUpdate = true;
				return;
			}
			base.NPC.damage = 0;
			float playerLocation2 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = base.NPC.velocity.X * 0.02f;
			charging = false;
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.9f;
			float slowedVelocityThreshold = (revenge ? 0.12f : 0.1f);
			if (phase2)
			{
				NPC nPC4 = base.NPC;
				nPC4.velocity *= 0.98f;
				slowedVelocityThreshold += 0.05f;
			}
			if (phase3)
			{
				NPC nPC5 = base.NPC;
				nPC5.velocity *= 0.98f;
				slowedVelocityThreshold += 0.05f;
			}
			if (phase4)
			{
				NPC nPC6 = base.NPC;
				nPC6.velocity *= 0.98f;
				slowedVelocityThreshold += 0.05f;
			}
			if (enrageScale > 0f)
			{
				NPC nPC7 = base.NPC;
				nPC7.velocity *= 0.95f;
			}
			if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < slowedVelocityThreshold)
			{
				base.NPC.ai[2] = 0f;
				base.NPC.ai[1]++;
				calamityGlobalNPC.newAI[0] = 0f;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			float playerLocation3 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			bool canHitTarget = Collision.CanHit(base.NPC.Center, 1, 1, player.position, player.width, player.height);
			float distanceAboveTarget = ((!canHitTarget) ? 0f : 400f);
			float distanceAwayFromTargetX = ((!canHitTarget) ? 80f : 240f);
			float distanceAwayFromTargetY = player.Center.Y - base.NPC.Center.Y;
			float distanceAwayFromTargetYLeeway = ((!canHitTarget) ? 16f : 80f);
			bool num2 = Math.Abs(player.Center.X - base.NPC.Center.X) > distanceAwayFromTargetX;
			bool tooFarY = distanceAwayFromTargetY > distanceAboveTarget + distanceAwayFromTargetYLeeway || distanceAwayFromTargetY < distanceAboveTarget - distanceAwayFromTargetYLeeway;
			bool tooFar = num2 | tooFarY;
			calamityGlobalNPC.newAI[0]++;
			if (((Vector2.Distance(base.NPC.Center, player.Center) < 640f) & canHitTarget) || calamityGlobalNPC.newAI[0] >= 180f)
			{
				base.NPC.ai[0] = (phase3 ? 5f : 1f);
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
			}
			else if (tooFar)
			{
				Movement(distanceAboveTarget, player, enrageScale);
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			charging = false;
			Vector2 missileSpawnPos = default(Vector2);
			((Vector2)(ref missileSpawnPos))._002Ector((base.NPC.direction == 1) ? base.NPC.getRect().BottomLeft().X : base.NPC.getRect().BottomRight().X, base.NPC.getRect().Bottom().Y + 20f);
			missileSpawnPos.X += base.NPC.direction * 120;
			bool num3 = Collision.CanHit(new Vector2(missileSpawnPos.X, missileSpawnPos.Y - 30f), 1, 1, player.position, player.width, player.height);
			base.NPC.ai[1]++;
			base.NPC.ai[1] += activePlayers / 2;
			if (phase2)
			{
				base.NPC.ai[1] += 0.5f;
			}
			bool shouldSpawnMissiles = false;
			if (base.NPC.ai[1] > 40f - 12f * enrageScale)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				shouldSpawnMissiles = true;
			}
			if (shouldSpawnMissiles)
			{
				SoundEngine.PlaySound(in SoundID.NPCHit8, base.NPC.Center);
				if (Main.netMode != 1)
				{
					if (expertMode && NPC.CountNPCS(ModContent.NPCType<PlagueMine>()) < 2)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)missileSpawnPos.X, (int)missileSpawnPos.Y, ModContent.NPCType<PlagueMine>(), 0, 0f, 0f, 0f, challengeAmt);
					}
					float num4 = (revenge ? 9f : 7f) + enrageScale * 2f;
					float projXDist = player.Center.X - missileSpawnPos.X;
					float projYDist = player.Center.Y - missileSpawnPos.Y;
					float projDistance = (float)Math.Sqrt(projXDist * projXDist + projYDist * projYDist);
					projDistance = num4 / projDistance;
					projXDist *= projDistance;
					projYDist *= projDistance;
					int plagueMissile = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)missileSpawnPos.X, (int)missileSpawnPos.Y, ModContent.NPCType<PlagueHomingMissile>(), 0, 0f, 0f, 0f, challengeAmt);
					Main.npc[plagueMissile].velocity.X = projXDist;
					Main.npc[plagueMissile].velocity.Y = projYDist;
					Main.npc[plagueMissile].netUpdate = true;
				}
			}
			float distanceAboveTarget2 = ((!num3) ? 0f : 400f);
			float distanceAwayFromTargetX2 = ((!num3) ? 80f : 240f);
			float distanceAwayFromTargetY2 = player.Center.Y - base.NPC.Center.Y;
			float distanceAwayFromTargetYLeeway2 = ((!num3) ? 16f : 80f);
			bool num5 = Math.Abs(player.Center.X - base.NPC.Center.X) > distanceAwayFromTargetX2;
			bool tooFarY2 = distanceAwayFromTargetY2 > distanceAboveTarget2 + distanceAwayFromTargetYLeeway2 || distanceAwayFromTargetY2 < distanceAboveTarget2 - distanceAwayFromTargetYLeeway2;
			if (num5 | tooFarY2)
			{
				Movement(distanceAboveTarget2, player, enrageScale);
			}
			float playerLocation4 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation4 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.ai[2] > 3f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 2f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = 0;
			charging = false;
			Vector2 missileSpawnPos2 = default(Vector2);
			((Vector2)(ref missileSpawnPos2))._002Ector((base.NPC.direction == 1) ? base.NPC.getRect().BottomLeft().X : base.NPC.getRect().BottomRight().X, base.NPC.getRect().Bottom().Y + 20f);
			missileSpawnPos2.X += base.NPC.direction * 120;
			bool num6 = Collision.CanHit(new Vector2(missileSpawnPos2.X, missileSpawnPos2.Y - 30f), 1, 1, player.position, player.width, player.height);
			base.NPC.ai[1]++;
			base.NPC.ai[1] += activePlayers / 2;
			bool shouldSpawnMissiles2 = false;
			if (phase4)
			{
				base.NPC.ai[1] += 0.5f;
			}
			if (phase5)
			{
				base.NPC.ai[1] += 0.5f;
			}
			if (base.NPC.ai[1] % 20f == 19f)
			{
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[1] > 30f - 12f * enrageScale)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				shouldSpawnMissiles2 = true;
			}
			if (shouldSpawnMissiles2)
			{
				SoundEngine.PlaySound(in SoundID.Item88, base.NPC.Center);
				if (Main.netMode != 1)
				{
					if (expertMode && NPC.CountNPCS(ModContent.NPCType<PlagueMine>()) < 3)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)missileSpawnPos2.X, (int)missileSpawnPos2.Y, ModContent.NPCType<PlagueMine>(), 0, 0f, 0f, 0f, challengeAmt);
					}
					float num7 = (revenge ? 10f : 8f) + enrageScale * 2f;
					float projXDist2 = player.Center.X - missileSpawnPos2.X;
					float projYDist2 = player.Center.Y - missileSpawnPos2.Y;
					float projDistance2 = (float)Math.Sqrt(projXDist2 * projXDist2 + projYDist2 * projYDist2);
					projDistance2 = num7 / projDistance2;
					projXDist2 *= projDistance2;
					projYDist2 *= projDistance2;
					projXDist2 += (float)Main.rand.Next(-20, 21) * 0.05f;
					projYDist2 += (float)Main.rand.Next(-20, 21) * 0.05f;
					int plagueMissile2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)missileSpawnPos2.X, (int)missileSpawnPos2.Y, ModContent.NPCType<PlagueHomingMissile>(), 0, 0f, 0f, 0f, challengeAmt);
					Main.npc[plagueMissile2].velocity.X = projXDist2;
					Main.npc[plagueMissile2].velocity.Y = projYDist2;
					Main.npc[plagueMissile2].netUpdate = true;
				}
			}
			float distanceAboveTarget3 = ((!num6) ? 0f : 400f);
			float distanceAwayFromTargetX3 = ((!num6) ? 80f : 240f);
			float distanceAwayFromTargetY3 = player.Center.Y - base.NPC.Center.Y;
			float distanceAwayFromTargetYLeeway3 = ((!num6) ? 16f : 80f);
			bool num8 = Math.Abs(player.Center.X - base.NPC.Center.X) > distanceAwayFromTargetX3;
			bool tooFarY3 = distanceAwayFromTargetY3 > distanceAboveTarget3 + distanceAwayFromTargetYLeeway3 || distanceAwayFromTargetY3 < distanceAboveTarget3 - distanceAwayFromTargetYLeeway3;
			if (num8 | tooFarY3)
			{
				Movement(distanceAboveTarget3, player, enrageScale);
			}
			float playerLocation5 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation5 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.ai[2] > 5f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 2f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			Vector2 stingerSpawnPos = default(Vector2);
			((Vector2)(ref stingerSpawnPos))._002Ector((base.NPC.direction == 1) ? base.NPC.getRect().BottomLeft().X : base.NPC.getRect().BottomRight().X, base.NPC.getRect().Bottom().Y + 20f);
			stingerSpawnPos.X += base.NPC.direction * 120;
			bool canHitTarget2 = Collision.CanHit(new Vector2(stingerSpawnPos.X, stingerSpawnPos.Y - 30f), 1, 1, player.position, player.width, player.height);
			base.NPC.ai[1]++;
			int stingerFireDelay = (phase5 ? 20 : (phase3 ? 25 : 30));
			stingerFireDelay -= (int)Math.Ceiling(5f * enrageScale);
			if (base.NPC.ai[1] % (float)stingerFireDelay == (float)(stingerFireDelay - 1) && base.NPC.Center.Y < player.position.Y)
			{
				SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
				if (Main.netMode != 1)
				{
					float num9 = (revenge ? 6f : 5f) + 2f * enrageScale;
					float projXDist3 = player.Center.X - stingerSpawnPos.X;
					float projYDist3 = player.Center.Y - stingerSpawnPos.Y;
					float projDistance3 = (float)Math.Sqrt(projXDist3 * projXDist3 + projYDist3 * projYDist3);
					projDistance3 = num9 / projDistance3;
					projXDist3 *= projDistance3;
					projYDist3 *= projDistance3;
					int type = ModContent.ProjectileType<PlagueStingerGoliathV2>();
					switch ((int)base.NPC.ai[2])
					{
					case 2:
					case 3:
						if (expertMode)
						{
							type = ModContent.ProjectileType<PlagueStingerGoliath>();
						}
						break;
					case 4:
						type = ModContent.ProjectileType<HiveBombGoliath>();
						break;
					}
					if (Main.zenithWorld)
					{
						type = ModContent.ProjectileType<HiveBombGoliath>();
					}
					int damage = ((type == ModContent.ProjectileType<HiveBombGoliath>()) ? NukeDamage : StingerDamage);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), stingerSpawnPos.X, stingerSpawnPos.Y, projXDist3, projYDist3, type, damage, 0f, Main.myPlayer, challengeAmt, player.position.Y);
					base.NPC.netUpdate = true;
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] > 4f)
				{
					base.NPC.ai[2] = 0f;
				}
			}
			float distanceAboveTarget4 = ((!canHitTarget2) ? 0f : 400f);
			float distanceAwayFromTargetX4 = ((!canHitTarget2) ? 80f : 240f);
			float distanceAwayFromTargetY4 = player.Center.Y - base.NPC.Center.Y;
			float distanceAwayFromTargetYLeeway4 = ((!canHitTarget2) ? 16f : 80f);
			bool num10 = Math.Abs(player.Center.X - base.NPC.Center.X) > distanceAwayFromTargetX4;
			bool tooFarY4 = distanceAwayFromTargetY4 > distanceAboveTarget4 + distanceAwayFromTargetYLeeway4 || distanceAwayFromTargetY4 < distanceAboveTarget4 - distanceAwayFromTargetYLeeway4;
			if (num10 | tooFarY4)
			{
				Movement(distanceAboveTarget4, player, enrageScale);
			}
			float playerLocation6 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation6 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.ai[1] > (float)stingerFireDelay * 10f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 3f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
			}
		}
		else
		{
			if (base.NPC.ai[0] != 4f)
			{
				return;
			}
			float missileVelocity = (revenge ? 6f : 5f);
			missileVelocity += 2f * enrageScale;
			int type2 = ModContent.ProjectileType<HiveBombGoliath>();
			int damage2 = NukeDamage;
			int chargeDistanceX2 = 600;
			float chargeSpeed2 = (revenge ? 28f : 26f);
			chargeSpeed2 += 3f * enrageScale;
			int phaseSwitchTimer2 = (int)Math.Ceiling(2f + enrageScale);
			if (base.NPC.ai[1] > (float)(2 * phaseSwitchTimer2) && base.NPC.ai[1] % 2f == 0f)
			{
				MissileCountdown = 0;
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = -1f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				SoundEngine.PlaySound(in AttackSwitchSound, base.NPC.Center);
				return;
			}
			if (base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.damage = 0;
				float playerLocation7 = base.NPC.Center.X - player.Center.X;
				float chargeDistanceY2 = 20f;
				chargeDistanceY2 += 20f * enrageScale;
				float distanceFromTargetX2 = Math.Abs(base.NPC.Center.X - player.Center.X);
				if (Math.Abs(base.NPC.Center.Y - (player.Center.Y - 500f)) < chargeDistanceY2 && distanceFromTargetX2 >= (float)chargeDistanceX2)
				{
					base.NPC.damage = base.NPC.defDamage;
					if (MissileCountdown == 1)
					{
						SoundEngine.PlaySound(in BarrageLaunchSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							bool gaussMode = false;
							Vector2 baseVelocity = player.Center - base.NPC.Center;
							((Vector2)(ref baseVelocity)).Normalize();
							baseVelocity *= missileVelocity;
							if (Main.rand.NextBool(10) && Main.zenithWorld)
							{
								type2 = ModContent.ProjectileType<AresGaussNukeProjectile>();
								baseVelocity *= 0.75f;
								gaussMode = true;
							}
							else if (Main.rand.NextBool() && Main.zenithWorld)
							{
								type2 = ModContent.ProjectileType<PeanutRocket>();
								baseVelocity *= 0.4f;
							}
							int spread = 24;
							if (!gaussMode)
							{
								for (int i = 0; i < 8; i++)
								{
									Vector2 spawn = base.NPC.Center;
									spawn.X += i * (int)((double)spread * 1.125) - 8 * (spread / 2);
									Vector2 velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-30f + 60f * (float)i / 8f));
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawn, velocity, type2, damage2, 0f, Main.myPlayer, nukeBarrageChallengeAmt, player.position.Y);
								}
							}
							else
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, baseVelocity, type2, damage2, 0f, Main.myPlayer);
							}
						}
					}
					charging = true;
					base.NPC.ai[1]++;
					base.NPC.ai[2] = 0f;
					float targetX2 = player.Center.X - base.NPC.Center.X;
					float targetY2 = player.Center.Y - 500f - base.NPC.Center.Y;
					float targetDistance2 = (float)Math.Sqrt(targetX2 * targetX2 + targetY2 * targetY2);
					targetDistance2 = chargeSpeed2 / targetDistance2;
					base.NPC.velocity.X = targetX2 * targetDistance2;
					base.NPC.velocity.Y = targetY2 * targetDistance2;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					base.NPC.direction = ((playerLocation7 < 0f) ? 1 : (-1));
					base.NPC.spriteDirection = base.NPC.direction;
					if (base.NPC.spriteDirection != 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.rotation = base.NPC.velocity.X * 0.02f;
					charging = false;
					float maxLineUpSpeed2 = (revenge ? 26f : 22f);
					float lineUpAccel2 = (revenge ? 0.7f : 0.6f);
					maxLineUpSpeed2 += 7f * enrageScale;
					lineUpAccel2 += 0.35f * enrageScale;
					if (base.NPC.Center.Y < player.Center.Y - 500f - chargeDistanceY2)
					{
						base.NPC.velocity.Y += lineUpAccel2;
					}
					else if (base.NPC.Center.Y > player.Center.Y - 500f + chargeDistanceY2)
					{
						base.NPC.velocity.Y -= lineUpAccel2;
					}
					else
					{
						base.NPC.velocity.Y *= 0.7f;
					}
					if (base.NPC.velocity.Y < 0f - maxLineUpSpeed2)
					{
						base.NPC.velocity.Y = 0f - maxLineUpSpeed2;
					}
					if (base.NPC.velocity.Y > maxLineUpSpeed2)
					{
						base.NPC.velocity.Y = maxLineUpSpeed2;
					}
					float distanceXMax2 = 100f;
					float distanceXMin2 = 20f;
					if (distanceFromTargetX2 > (float)chargeDistanceX2 + distanceXMax2)
					{
						base.NPC.velocity.X += lineUpAccel2 * (float)base.NPC.direction;
					}
					else if (distanceFromTargetX2 < (float)chargeDistanceX2 + distanceXMin2)
					{
						base.NPC.velocity.X -= lineUpAccel2 * (float)base.NPC.direction;
					}
					else
					{
						base.NPC.velocity.X *= 0.7f;
					}
					if (base.NPC.velocity.X < 0f - maxLineUpSpeed2)
					{
						base.NPC.velocity.X = 0f - maxLineUpSpeed2;
					}
					if (base.NPC.velocity.X > maxLineUpSpeed2)
					{
						base.NPC.velocity.X = maxLineUpSpeed2;
					}
					base.NPC.direction = ((playerLocation7 < 0f) ? 1 : (-1));
					base.NPC.spriteDirection = base.NPC.direction;
				}
				return;
			}
			base.NPC.damage = base.NPC.defDamage;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			int chargeDirectionXSign2 = 1;
			if (base.NPC.Center.X < player.Center.X)
			{
				chargeDirectionXSign2 = -1;
			}
			if (base.NPC.direction == chargeDirectionXSign2 && Math.Abs(base.NPC.Center.X - player.Center.X) > (float)chargeDistanceX2)
			{
				base.NPC.ai[2] = 1f;
			}
			if (Math.Abs(base.NPC.Center.Y - player.Center.Y) > (float)chargeDistanceX2 * 3f)
			{
				base.NPC.ai[2] = 1f;
			}
			if (enrageScale > 0f && base.NPC.ai[2] == 1f)
			{
				NPC nPC8 = base.NPC;
				nPC8.velocity *= 0.95f;
			}
			if (base.NPC.ai[2] != 1f)
			{
				charging = true;
				if (((Vector2)(ref base.NPC.velocity)).Length() < chargeSpeed2)
				{
					base.NPC.velocity.X = chargeSpeed2 * (float)base.NPC.direction;
				}
				calamityGlobalNPC.newAI[0]++;
				if (calamityGlobalNPC.newAI[0] > 90f)
				{
					base.NPC.velocity.X *= 1.01f;
				}
				if (!death)
				{
					return;
				}
				float missileGateValue = 20f;
				if (calamityGlobalNPC.newAI[0] % missileGateValue == 0f && Collision.CanHit(base.NPC.Center, 1, 1, player.position, player.width, player.height))
				{
					SoundEngine.PlaySound(in SoundID.Item42, base.NPC.Center);
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * missileVelocity, type2, damage2, 0f, Main.myPlayer, nukeBarrageChallengeAmt, player.position.Y);
					}
				}
				return;
			}
			base.NPC.damage = 0;
			base.NPC.rotation = base.NPC.velocity.X * 0.02f;
			charging = false;
			NPC nPC9 = base.NPC;
			nPC9.velocity *= 0.9f;
			float slowedVelocityThreshold2 = (revenge ? 0.12f : 0.1f);
			if (phase3)
			{
				NPC nPC10 = base.NPC;
				nPC10.velocity *= 0.9f;
				slowedVelocityThreshold2 += 0.05f;
			}
			if (phase4)
			{
				NPC nPC11 = base.NPC;
				nPC11.velocity *= 0.9f;
				slowedVelocityThreshold2 += 0.05f;
			}
			if (phase5)
			{
				NPC nPC12 = base.NPC;
				nPC12.velocity *= 0.9f;
				slowedVelocityThreshold2 += 0.05f;
			}
			if (enrageScale > 0f)
			{
				NPC nPC13 = base.NPC;
				nPC13.velocity *= 0.95f;
			}
			if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < slowedVelocityThreshold2)
			{
				base.NPC.ai[2] = 0f;
				base.NPC.ai[1]++;
				calamityGlobalNPC.newAI[0] = 0f;
			}
		}
	}

	private void Movement(float distanceAboveTarget, Player player, float enrageScale)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		float acceleration = ((base.NPC.ai[0] == 1f || base.NPC.ai[0] == 5f) ? 0.12f : ((base.NPC.ai[0] == 2f) ? 0.15f : 0.18f));
		float velocity = ((base.NPC.ai[0] == 1f || base.NPC.ai[0] == 5f) ? 12f : ((base.NPC.ai[0] == 2f) ? 15f : 18f));
		acceleration *= 0.5f * enrageScale + 1f;
		velocity *= 1f + enrageScale * 0.5f;
		Vector2 hoverDestination = player.Center - Vector2.UnitY * distanceAboveTarget;
		Vector2 idealVelocity = base.NPC.SafeDirectionTo(hoverDestination) * velocity;
		base.NPC.SimpleFlyMovement(idealVelocity, acceleration);
	}

	public override bool CheckActive()
	{
		return canDespawn;
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
		return minDist <= 100f * base.NPC.scale;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 2; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			for (int i = 1; i < 7; i++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PlaguebringerGoliathGore" + i).Type, base.NPC.scale);
			}
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 200);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int j = 0; j < 40; j++)
		{
			int plagueDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[plagueDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[plagueDust].scale = 0.5f;
				Main.dust[plagueDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int l = 0; l < 70; l++)
		{
			int plagueDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, 0f, 0f, 100, default(Color), 3f);
			Main.dust[plagueDust2].noGravity = true;
			Dust obj2 = Main.dust[plagueDust2];
			obj2.velocity *= 5f;
			plagueDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[plagueDust2];
			obj3.velocity *= 2f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowTexture = Texture_Glow.Value;
		if (curTex != ((!charging) ? 1 : 2))
		{
			base.NPC.frame.X = 0;
			base.NPC.frame.Y = 0;
		}
		if (charging)
		{
			curTex = 2;
			texture = ChargeTexture.Value;
			glowTexture = ChargeTexture_Glow.Value;
		}
		else
		{
			curTex = 1;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		int frameCount = 3;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(base.NPC.frame.X, base.NPC.frame.Y, texture.Width / 2, texture.Height / frameCount);
		Vector2 halfSizeTexture = rectangle.Size() / 2f;
		Vector2 posOffset = default(Vector2);
		((Vector2)(ref posOffset))._002Ector((float)(charging ? 175 : 125), 0f);
		int chargeAfterimageAmount = 10;
		if (CalamityClientConfig.Instance.Afterimages && (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 4f) && charging)
		{
			for (int j = 1; j < chargeAfterimageAmount; j += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(chargeAfterimageAmount - j) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture.Width, (float)(texture.Height / frameCount)) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + posOffset;
				spriteBatch.Draw(texture, afterimagePos, (Rectangle?)rectangle, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture.Width, (float)(texture.Height / frameCount)) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + posOffset;
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)rectangle, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		Color redLerpColor = Color.Lerp(Color.White, Color.Red, 0.5f);
		if (CalamityClientConfig.Instance.Afterimages && (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 4f) && charging)
		{
			for (int k = 1; k < chargeAfterimageAmount; k++)
			{
				Color otherAfterimageColor = redLerpColor;
				otherAfterimageColor = Color.Lerp(otherAfterimageColor, Color.White, 0.5f);
				otherAfterimageColor *= (float)(chargeAfterimageAmount - k) / 15f;
				Vector2 otherAfterimagePos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				otherAfterimagePos -= new Vector2((float)glowTexture.Width, (float)(glowTexture.Height / frameCount)) * base.NPC.scale / 2f;
				otherAfterimagePos += halfSizeTexture * base.NPC.scale + posOffset;
				spriteBatch.Draw(glowTexture, otherAfterimagePos, (Rectangle?)rectangle, otherAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(glowTexture, drawLocation, (Rectangle?)rectangle, redLerpColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		int width = ((!charging) ? 266 : 322);
		int height = ((!charging) ? 256 : 212);
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 4.0)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + height;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= height * 3)
		{
			base.NPC.frame.Y = 0;
			base.NPC.frame.X = ((base.NPC.frame.X == 0) ? width : 0);
			if (charging)
			{
				flyingFrame2 = !flyingFrame2;
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedPlaguebringer = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PlaguebringerGoliathBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[7]
		{
			ModContent.ItemType<Virulence>(),
			ModContent.ItemType<TheHive>(),
			ModContent.ItemType<Malevolence>(),
			ModContent.ItemType<PlagueStaff>(),
			ModContent.ItemType<FuelCellBundle>(),
			ModContent.ItemType<InfectedRemote>(),
			ModContent.ItemType<TheSyringe>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<Malachite>(), 10);
		normalOnly.Add(209, 1, 3, 5);
		normalOnly.Add(ModContent.ItemType<PlagueCellCanister>(), 1, 15, 20);
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<InfectedArmorPlating>(), 1, 30, 40));
		normalOnly.Add(ModContent.ItemType<PlaguebringerGoliathMask>(), 7);
		normalOnly.Add(ModContent.ItemType<PlagueCaller>(), 10);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<PlaguebringerGoliathTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<PlaguebringerGoliathRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(5302), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedPlaguebringer, ModContent.ItemType<LorePlaguebringerGoliath>(), ui: true, DropHelper.FirstKillText);
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			if (Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 480);
				target.AddBuff(20, 480);
				target.AddBuff(70, 480);
			}
			target.AddBuff(ModContent.BuffType<Plague>(), 360);
		}
	}
}
