using System;
using System.IO;
using System.Threading;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Mounts;
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
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.VanillaNPCAIOverrides.RegularEnemies;
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

namespace CalamityMod.NPCs.AstrumAureus;

[AutoloadBossHead]
[HasPierceResist(true)]
public class AstrumAureus : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/AureusHit", 4);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/AureusDeath");

	public static readonly SoundStyle LaserSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumAureus/AureusShoot");

	public static readonly SoundStyle FlameCrystalSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumAureus/AureusShootCrystal");

	public static readonly SoundStyle StompSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumAureus/LegStomp");

	public static readonly SoundStyle JumpSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumAureus/AureusJump");

	public static readonly SoundStyle TeleportSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumAureus/AureusTeleport");

	public static Asset<Texture2D> JumpTexture;

	public static Asset<Texture2D> RechargeTexture;

	public static Asset<Texture2D> StompTexture;

	public static Asset<Texture2D> WalkTexture;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> JumpTexture_Glow;

	public static Asset<Texture2D> StompTexture_Glow;

	public static Asset<Texture2D> WalkTexture_Glow;

	private bool stomping;

	public int slimeProjCounter;

	public int slimePhase;

	public RevengeanceAndDeathAI.MimicAI ZenithSeedMimicAI;

	public static int LaserDamage = 25;

	public static int CrystalDamage = 30;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.27f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.45f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -24f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y -= 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			JumpTexture = ModContent.Request<Texture2D>(Texture + "Jump", (AssetRequestMode)2);
			RechargeTexture = ModContent.Request<Texture2D>(Texture + "Recharge", (AssetRequestMode)2);
			StompTexture = ModContent.Request<Texture2D>(Texture + "Stomp", (AssetRequestMode)2);
			WalkTexture = ModContent.Request<Texture2D>(Texture + "Walk", (AssetRequestMode)2);
			JumpTexture_Glow = ModContent.Request<Texture2D>(Texture + "JumpGlow", (AssetRequestMode)2);
			StompTexture_Glow = ModContent.Request<Texture2D>(Texture + "StompGlow", (AssetRequestMode)2);
			WalkTexture_Glow = ModContent.Request<Texture2D>(Texture + "WalkGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = true;
		base.NPC.npcSlots = 15f;
		base.NPC.damage = 80;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.width = 374;
		base.NPC.height = 374;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.1f);
		base.NPC.LifeMaxNERB(75000, 120000, 740000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 15);
		base.NPC.boss = true;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
		if (Main.getGoodWorld)
		{
			base.NPC.scale = 0.7f;
		}
		if (Main.zenithWorld)
		{
			base.NPC.scale = 1.5f;
		}
		ZenithSeedMimicAI = new RevengeanceAndDeathAI.MimicAI();
		ZenithSeedMimicAI.NPC = base.NPC;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AstrumAureus")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(stomping);
		writer.Write(base.NPC.alpha);
		writer.Write(slimePhase);
		writer.Write(slimeProjCounter);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		stomping = reader.ReadBoolean();
		base.NPC.alpha = reader.ReadInt32();
		slimePhase = reader.ReadInt32();
		slimeProjCounter = reader.ReadInt32();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f17: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_105e: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1104: Unknown result type (might be due to invalid IL or missing references)
		//IL_1110: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff3: Unknown result type (might be due to invalid IL or missing references)
		//IL_228b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2297: Unknown result type (might be due to invalid IL or missing references)
		//IL_2028: Unknown result type (might be due to invalid IL or missing references)
		//IL_1953: Unknown result type (might be due to invalid IL or missing references)
		//IL_195e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_135f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1985: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b39: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_2782: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2079: Unknown result type (might be due to invalid IL or missing references)
		//IL_2094: Unknown result type (might be due to invalid IL or missing references)
		//IL_14be: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_27fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_292d: Unknown result type (might be due to invalid IL or missing references)
		//IL_293e: Unknown result type (might be due to invalid IL or missing references)
		//IL_256b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2597: Unknown result type (might be due to invalid IL or missing references)
		//IL_259c: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_29be: Unknown result type (might be due to invalid IL or missing references)
		//IL_29cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2810: Unknown result type (might be due to invalid IL or missing references)
		//IL_2857: Unknown result type (might be due to invalid IL or missing references)
		//IL_285d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2882: Unknown result type (might be due to invalid IL or missing references)
		//IL_288c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2891: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2399: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1805: Unknown result type (might be due to invalid IL or missing references)
		//IL_1815: Unknown result type (might be due to invalid IL or missing references)
		//IL_1820: Unknown result type (might be due to invalid IL or missing references)
		//IL_1825: Unknown result type (might be due to invalid IL or missing references)
		//IL_182a: Unknown result type (might be due to invalid IL or missing references)
		//IL_182f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1836: Unknown result type (might be due to invalid IL or missing references)
		//IL_183b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1847: Unknown result type (might be due to invalid IL or missing references)
		//IL_1872: Unknown result type (might be due to invalid IL or missing references)
		//IL_1877: Unknown result type (might be due to invalid IL or missing references)
		//IL_187c: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_2664: Unknown result type (might be due to invalid IL or missing references)
		//IL_2685: Unknown result type (might be due to invalid IL or missing references)
		//IL_268a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2698: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1def: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f64: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f84: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.astrumAureus = base.NPC.whoAmI;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < (revenge ? 0.85f : (expertMode ? 0.8f : 0.75f));
		bool phase3 = lifeRatio < (revenge ? 0.7f : (expertMode ? 0.6f : 0.5f));
		bool phase4 = (lifeRatio < (revenge ? 0.5f : 0.4f)) & expertMode;
		bool phase5 = (lifeRatio < 0.3f) & revenge;
		bool exhausted = base.NPC.ai[2] >= (phase3 ? 2f : 1f);
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		float noProjectileOrPhaseIncrementTime = 240f;
		bool dontAttack = base.NPC.localAI[3] > 0f;
		if (dontAttack)
		{
			base.NPC.localAI[3]--;
			if (base.NPC.Distance(player.Center) < 240f)
			{
				base.NPC.localAI[3] -= (death ? 4f : (expertMode ? 2f : 1f));
			}
		}
		float astralFlameBarrageTimerIncrement = 1f;
		if (expertMode)
		{
			astralFlameBarrageTimerIncrement += (death ? ((float)Math.Round(3f * (1f - lifeRatio))) : ((float)Math.Round(2f * (1f - lifeRatio))));
		}
		float walkingVelocity = (phase5 ? 7f : 5f);
		if (expertMode)
		{
			walkingVelocity += 1.5f * (1f - lifeRatio);
		}
		if (revenge)
		{
			walkingVelocity += Math.Abs(base.NPC.Center.X - player.Center.X) * 0.0025f;
		}
		if (Main.getGoodWorld)
		{
			walkingVelocity *= 1.15f;
		}
		float walkingProjectileVelocity = walkingVelocity * 0.8f;
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		bool reduceFallSpeed = base.NPC.velocity.Y > 0f && Collision.SolidCollision(base.NPC.position + Vector2.UnitY * 1.1f * base.NPC.velocity.Y, base.NPC.width, base.NPC.height) && base.NPC.ai[0] == 4f;
		bool despawnDistance = Vector2.Distance(player.Center, base.NPC.Center) > 5600f;
		if ((!player.active || player.dead) | despawnDistance)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if ((!player.active || player.dead) | despawnDistance)
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
					base.NPC.localAI[2] = 0f;
					base.NPC.localAI[3] = 0f;
					calamityGlobalNPC.newAI[0] = 0f;
					calamityGlobalNPC.newAI[1] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		bool geldonPhase1 = lifeRatio > 0.6f && lifeRatio <= 0.7f;
		bool geldonPhase2 = lifeRatio <= 0.1f;
		if (Main.zenithWorld && (geldonPhase1 | geldonPhase2))
		{
			slimeProjCounter++;
			if (slimeProjCounter % 180 == 0)
			{
				SoundEngine.PlaySound(in SoundID.Item33, base.NPC.Center);
				if (slimePhase == 1)
				{
					if (Main.netMode != 1)
					{
						int type = ModContent.ProjectileType<AstralFlame>();
						int totalProjectiles = (death ? 12 : (revenge ? 10 : (expertMode ? 8 : 6)));
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						float velocity = 10f;
						Vector2 spinningPoint = default(Vector2);
						((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
						for (int k = 0; k < totalProjectiles; k++)
						{
							Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, type, CrystalDamage, 0f, Main.myPlayer, 0f, 1f);
						}
					}
					slimePhase = 0;
				}
				else
				{
					if (Main.netMode != 1)
					{
						int type2 = ModContent.ProjectileType<AstralLaser>();
						float aureusLaserTargetX = player.Center.X - base.NPC.Center.X;
						float aureusLaserTargetY = player.Center.Y - base.NPC.Center.Y;
						float aureusLaserTargetDist = (float)Math.Sqrt(aureusLaserTargetX * aureusLaserTargetX + aureusLaserTargetY * aureusLaserTargetY);
						aureusLaserTargetDist = 7f / aureusLaserTargetDist;
						aureusLaserTargetX *= aureusLaserTargetDist;
						aureusLaserTargetY *= aureusLaserTargetDist;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, aureusLaserTargetX, aureusLaserTargetY, type2, LaserDamage, 0f, Main.myPlayer);
						Vector2 offset = default(Vector2);
						for (int i = 0; i < 4; i++)
						{
							((Vector2)(ref offset))._002Ector((float)Main.rand.Next(-6, 7), (float)Main.rand.Next(-6, 7));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, aureusLaserTargetX + offset.X, aureusLaserTargetY + offset.Y, type2, LaserDamage, 0f, Main.myPlayer);
						}
					}
					slimePhase = 1;
				}
			}
			ZenithSeedMimicAI.AI(base.Mod);
			base.NPC.noGravity = false;
			base.NPC.noTileCollide = false;
			return;
		}
		base.NPC.noGravity = true;
		if (base.NPC.ai[0] != 1f)
		{
			Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1.3f, 0.5f, 0f);
		}
		if (base.NPC.ai[0] == 2f || base.NPC.ai[0] >= 5f)
		{
			if (!dontAttack)
			{
				base.NPC.localAI[0] += ((base.NPC.ai[0] == 2f) ? 1f : astralFlameBarrageTimerIncrement);
			}
			float astralFlameBarrageGateValue = (phase4 ? 30f : 60f);
			if (base.NPC.localAI[0] >= astralFlameBarrageGateValue && base.NPC.ai[0] >= 5f && base.NPC.ai[0] != 7f)
			{
				base.NPC.localAI[0] = 0f;
				SoundEngine.PlaySound(in FlameCrystalSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					float velocity3 = (death ? (8f + base.NPC.localAI[2] * 0.025f) : 7f);
					int type3 = ModContent.ProjectileType<AstralFlame>();
					float spreadLimit = (phase4 ? 100f : 50f);
					float randomSpread = (Main.rand.NextFloat() - 0.5f) * spreadLimit;
					Vector2 spawnVector = default(Vector2);
					((Vector2)(ref spawnVector))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 80f * base.NPC.scale);
					Vector2 destination = default(Vector2);
					((Vector2)(ref destination))._002Ector(spawnVector.X + randomSpread, spawnVector.Y - 100f * base.NPC.scale);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector, Vector2.Normalize(destination - spawnVector) * velocity3, type3, CrystalDamage, 0f, Main.myPlayer);
				}
			}
			float laserBarrageGateValue = (phase5 ? 160f : (phase3 ? 120f : (phase2 ? 80f : 60f)));
			if (base.NPC.localAI[0] >= laserBarrageGateValue && base.NPC.ai[0] == 2f)
			{
				base.NPC.localAI[0] = 0f;
				SoundEngine.PlaySound(in LaserSound, base.NPC.Center);
				if (calamityGlobalNPC.newAI[2] == 0f)
				{
					calamityGlobalNPC.newAI[2] = 1f;
					if (Main.netMode != 1)
					{
						int maxProjectiles = ((!phase2) ? 3 : 5);
						int num = ((!phase2) ? 8 : 10);
						int type4 = ModContent.ProjectileType<AstralLaser>();
						Vector2 projectileVelocity = Vector2.Normalize(player.Center - base.NPC.Center) * walkingProjectileVelocity;
						float rotation = MathHelper.ToRadians((float)num);
						for (int j = 0; j < maxProjectiles; j++)
						{
							Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)j / (float)(maxProjectiles - 1)));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed, type4, LaserDamage, 0f, Main.myPlayer, 0f, walkingProjectileVelocity * 2f);
						}
						if (phase3)
						{
							float flameVelocity = walkingProjectileVelocity;
							maxProjectiles = 2;
							type4 = ModContent.ProjectileType<AstralFlame>();
							projectileVelocity = Vector2.Normalize(player.Center - base.NPC.Center) * flameVelocity;
							rotation = MathHelper.ToRadians(45f);
							for (int l = 0; l < maxProjectiles; l++)
							{
								Vector2 perturbedSpeed2 = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)l / (float)(maxProjectiles - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed2, type4, CrystalDamage, 0f, Main.myPlayer);
							}
						}
					}
				}
				else
				{
					calamityGlobalNPC.newAI[2] = 0f;
					if (Main.netMode != 1)
					{
						int maxProjectiles2 = (phase3 ? (death ? 17 : 15) : (death ? 11 : 9));
						int spread = (phase3 ? (death ? 22 : 20) : (death ? 18 : 16));
						int type5 = ModContent.ProjectileType<AstralLaser>();
						int centralLaser = maxProjectiles2 / 2;
						int[] lasersToNotFire = new int[6]
						{
							centralLaser - 3,
							centralLaser - 2,
							centralLaser - 1,
							centralLaser + 1,
							centralLaser + 2,
							centralLaser + 3
						};
						Vector2 projectileVelocity2 = Vector2.Normalize(player.Center - base.NPC.Center) * walkingProjectileVelocity;
						float rotation2 = MathHelper.ToRadians((float)spread);
						for (int m = 0; m < maxProjectiles2; m++)
						{
							if (m != lasersToNotFire[0] && m != lasersToNotFire[1] && m != lasersToNotFire[2] && m != lasersToNotFire[3] && m != lasersToNotFire[4] && m != lasersToNotFire[5])
							{
								Vector2 perturbedSpeed3 = projectileVelocity2.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)m / (float)(maxProjectiles2 - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed3, type5, LaserDamage, 0f, Main.myPlayer, 0f, walkingProjectileVelocity * 2f);
							}
						}
					}
				}
			}
		}
		else
		{
			base.NPC.localAI[0] = 0f;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[0] = 1f;
			base.NPC.netUpdate = true;
			CustomGravity();
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.X *= 0.8f;
			base.NPC.ai[1]++;
			if (base.NPC.Distance(player.Center) < 240f)
			{
				base.NPC.ai[1] += (death ? 4f : (expertMode ? 2f : 1f));
			}
			if (base.NPC.ai[1] >= 180f)
			{
				base.NPC.TargetClosest();
				switch (Main.rand.Next(phase3 ? 3 : 2))
				{
				case 0:
					base.NPC.ai[0] = 2f;
					break;
				case 1:
					base.NPC.ai[0] = 3f;
					break;
				case 2:
					base.NPC.ai[0] = 5f;
					break;
				}
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.noTileCollide = base.NPC.ai[0] == 2f;
				base.NPC.netUpdate = true;
			}
			else
			{
				CustomGravity();
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) < 200f * base.NPC.scale)
			{
				base.NPC.velocity.X *= 0.8f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			else
			{
				float playerLocation = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				if (base.NPC.direction > 0)
				{
					base.NPC.velocity.X = (base.NPC.velocity.X * 20f + walkingVelocity) / 21f;
				}
				if (base.NPC.direction < 0)
				{
					base.NPC.velocity.X = (base.NPC.velocity.X * 20f - walkingVelocity) / 21f;
				}
			}
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && player.position.Y <= base.NPC.position.Y + (float)base.NPC.height && !base.NPC.collideX)
			{
				CustomGravity();
				base.NPC.noTileCollide = false;
			}
			else
			{
				base.NPC.noTileCollide = true;
				int aureusHitboxWidth = 80;
				int aureusHitboxHeight = 20;
				Vector2 aureusHitboxTileCollideSize = default(Vector2);
				((Vector2)(ref aureusHitboxTileCollideSize))._002Ector(base.NPC.Center.X - (float)(aureusHitboxWidth / 2), base.NPC.position.Y + (float)base.NPC.height - (float)aureusHitboxHeight);
				bool nearPlayerWalkingThroughTiles = false;
				if (base.NPC.position.X < player.position.X && base.NPC.position.X + (float)base.NPC.width > player.position.X + (float)player.width && base.NPC.position.Y + (float)base.NPC.height < player.position.Y + (float)player.height - 16f)
				{
					nearPlayerWalkingThroughTiles = true;
				}
				if (nearPlayerWalkingThroughTiles)
				{
					base.NPC.velocity.Y += 0.5f;
				}
				else if (Collision.SolidCollision(aureusHitboxTileCollideSize, aureusHitboxWidth, aureusHitboxHeight))
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = 0f;
					}
					if ((double)base.NPC.velocity.Y > -0.2)
					{
						base.NPC.velocity.Y -= 0.025f;
					}
					else
					{
						base.NPC.velocity.Y -= 0.2f;
					}
					if (base.NPC.velocity.Y < -4f)
					{
						base.NPC.velocity.Y = -4f;
					}
				}
				else
				{
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y = 0f;
					}
					if ((double)base.NPC.velocity.Y < 0.1)
					{
						base.NPC.velocity.Y += 0.025f;
					}
					else
					{
						base.NPC.velocity.Y += 0.5f;
					}
				}
			}
			if (!dontAttack)
			{
				base.NPC.ai[1]++;
				if (base.NPC.Distance(player.Center) < 240f)
				{
					base.NPC.ai[1] += (death ? 4f : (expertMode ? 2f : 1f));
				}
			}
			if (base.NPC.ai[1] >= 360f - (death ? (90f * (1f - lifeRatio)) : 0f))
			{
				base.NPC.noTileCollide = false;
				base.NPC.TargetClosest();
				base.NPC.ai[0] = (exhausted ? 1f : 3f);
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			base.NPC.noTileCollide = false;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X *= 0.8f;
				if (!dontAttack)
				{
					base.NPC.ai[1]++;
				}
				if (base.NPC.ai[1] >= 30f)
				{
					base.NPC.ai[1] = -20f;
				}
				else if (base.NPC.ai[1] == -1f)
				{
					base.NPC.damage = base.NPC.defDamage;
					float distanceFromPlayerOnXAxis = base.NPC.Center.X - player.Center.X;
					base.NPC.direction = ((distanceFromPlayerOnXAxis < 0f) ? 1 : (-1));
					calamityGlobalNPC.newAI[3] = base.NPC.direction;
					float speedMultLimit = 1f;
					float multiplier = 0.0005f;
					float distanceAwayFromTarget = Math.Abs(distanceFromPlayerOnXAxis);
					float distanceGateValue = 400f;
					if ((distanceAwayFromTarget > distanceGateValue) & expertMode)
					{
						calamityGlobalNPC.newAI[0] = (distanceAwayFromTarget - distanceGateValue) * multiplier;
						if (calamityGlobalNPC.newAI[0] > speedMultLimit)
						{
							calamityGlobalNPC.newAI[0] = speedMultLimit;
						}
					}
					float distanceBelowTarget = base.NPC.position.Y - (player.position.Y + 80f);
					if ((distanceBelowTarget > 0f) & revenge)
					{
						calamityGlobalNPC.newAI[1] = distanceBelowTarget * multiplier;
						if (calamityGlobalNPC.newAI[1] > speedMultLimit)
						{
							calamityGlobalNPC.newAI[1] = speedMultLimit;
						}
					}
					float velocity4 = 20f;
					if (expertMode)
					{
						velocity4 += (death ? (6f * (1f - lifeRatio)) : (4f * (1f - lifeRatio)));
					}
					if (Main.getGoodWorld)
					{
						velocity4 *= 1.15f;
					}
					base.NPC.velocity = (new Vector2(player.Center.X, player.Center.Y - 500f) - base.NPC.Center).SafeNormalize(Vector2.Zero) * velocity4;
					NPC nPC = base.NPC;
					nPC.velocity *= new Vector2(calamityGlobalNPC.newAI[0] + 1f, calamityGlobalNPC.newAI[1] + 1f);
					base.NPC.noTileCollide = true;
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
					SoundEngine.PlaySound(in JumpSound, base.NPC.Center);
					base.NPC.netUpdate = true;
				}
			}
			if (base.NPC.ai[0] != 4f)
			{
				CustomGravity();
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				SoundEngine.PlaySound(Main.zenithWorld ? AresGaussNuke.NukeExplosionSound : StompSound, base.NPC.Center);
				if (Main.zenithWorld)
				{
					float screenShakePower = 16f * Utils.GetLerpValue(1300f, 0f, base.NPC.Distance(Main.LocalPlayer.Center), clamped: true);
					Main.LocalPlayer.SetScreenshake(screenShakePower);
				}
				base.NPC.TargetClosest();
				base.NPC.localAI[1]++;
				float maxStompAmt = (phase5 ? 5f : (phase3 ? 2f : 3f));
				if (base.NPC.localAI[1] >= maxStompAmt)
				{
					base.NPC.ai[0] = (exhausted ? 1f : (phase3 ? 5f : 2f));
					base.NPC.localAI[1] = 0f;
					base.NPC.ai[2]++;
					base.NPC.ai[3] = 0f;
					base.NPC.noTileCollide = false;
					base.NPC.netUpdate = true;
				}
				else
				{
					float playerLocation2 = base.NPC.Center.X - player.Center.X;
					base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
					base.NPC.ai[0] = 3f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int n = (int)base.NPC.position.X - 20; n < (int)base.NPC.position.X + base.NPC.width + 40; n += 20)
				{
					for (int num2 = 0; num2 < 4; num2++)
					{
						int stompDust = Dust.NewDust(new Vector2(base.NPC.position.X - 20f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 20, 4, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
						Dust obj = Main.dust[stompDust];
						obj.velocity *= 0.2f;
					}
				}
				SoundEngine.PlaySound(in LaserSound, base.NPC.Center);
				if (Main.zenithWorld)
				{
					if (Main.netMode != 1)
					{
						bool num3 = Main.rand.NextBool();
						int type6 = (num3 ? ModContent.ProjectileType<AstralFlame>() : ModContent.ProjectileType<AstralLaser>());
						int damage = (num3 ? CrystalDamage : LaserDamage);
						int totalProjectiles2 = (death ? 12 : (revenge ? 10 : (expertMode ? 8 : 6)));
						float radians2 = (float)Math.PI * 2f / (float)totalProjectiles2;
						float velocity5 = 10f;
						Vector2 spinningPoint2 = default(Vector2);
						((Vector2)(ref spinningPoint2))._002Ector(0f, 0f - velocity5);
						for (int num4 = 0; num4 < totalProjectiles2; num4++)
						{
							Vector2 velocity6 = spinningPoint2.RotatedBy(radians2 * (float)num4);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity6, type6, damage, 0f, Main.myPlayer, 0f, 1f);
						}
					}
					return;
				}
				if ((calamityGlobalNPC.newAI[2] == 0f) & phase2)
				{
					calamityGlobalNPC.newAI[2] = 1f;
					if (Main.netMode != 1)
					{
						float flameVelocity2 = 6f;
						int maxProjectiles3 = (death ? 3 : 2);
						int num5 = (death ? 28 : 20);
						int type7 = ModContent.ProjectileType<AstralFlame>();
						Vector2 spawnVector2 = default(Vector2);
						((Vector2)(ref spawnVector2))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 80f * base.NPC.scale);
						Vector2 projectileVelocity3 = Vector2.Normalize(new Vector2(spawnVector2.X, spawnVector2.Y + 100f * base.NPC.scale) - spawnVector2) * flameVelocity2;
						float rotation3 = MathHelper.ToRadians((float)num5);
						for (int num6 = 0; num6 < maxProjectiles3; num6++)
						{
							Vector2 perturbedSpeed4 = projectileVelocity3.RotatedBy(MathHelper.Lerp(0f - rotation3, rotation3, (float)num6 / (float)(maxProjectiles3 - 1)));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector2 + Vector2.Normalize(perturbedSpeed4) * 100f, perturbedSpeed4, type7, CrystalDamage, 0f, Main.myPlayer);
						}
					}
					return;
				}
				calamityGlobalNPC.newAI[2] = 0f;
				if (Main.netMode == 1)
				{
					return;
				}
				float laserVelocity = (Main.getGoodWorld ? 7f : (death ? 6f : 5f));
				int maxProjectiles4 = (phase3 ? (death ? 15 : 13) : (death ? 11 : 9));
				int spread2 = (phase3 ? (death ? 22 : 20) : (death ? 18 : 16));
				int type8 = ModContent.ProjectileType<AstralLaser>();
				int[] lasersToNotFire2 = new int[4]
				{
					1,
					3,
					maxProjectiles4 - 2,
					maxProjectiles4 - 4
				};
				Vector2 projectileVelocity4 = Vector2.Normalize(player.Center - base.NPC.Center) * laserVelocity;
				float rotation4 = MathHelper.ToRadians((float)spread2);
				for (int num7 = 0; num7 < maxProjectiles4; num7++)
				{
					if (num7 != lasersToNotFire2[0] && num7 != lasersToNotFire2[1] && num7 != lasersToNotFire2[2] && num7 != lasersToNotFire2[3])
					{
						Vector2 perturbedSpeed5 = projectileVelocity4.RotatedBy(MathHelper.Lerp(0f - rotation4, rotation4, (float)num7 / (float)(maxProjectiles4 - 1)));
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed5, type8, LaserDamage, 0f, Main.myPlayer, 0f, laserVelocity * 2f);
					}
				}
				return;
			}
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
			if (base.NPC.position.X < player.position.X && base.NPC.position.X + (float)base.NPC.width > player.position.X + (float)player.width)
			{
				if (base.NPC.ai[3] < 30f)
				{
					base.NPC.ai[3] = 30f;
				}
				base.NPC.velocity.X *= 0.8f;
				if (base.NPC.Bottom.Y < player.position.Y)
				{
					if (base.NPC.velocity.Y < -3f)
					{
						base.NPC.velocity.Y = -3f;
					}
					float fallSpeed = 1.2f;
					if (expertMode)
					{
						fallSpeed += (death ? (0.36f * (1f - lifeRatio)) : (0.24f * (1f - lifeRatio)));
					}
					if (Main.getGoodWorld)
					{
						fallSpeed += 0.5f;
					}
					if (calamityGlobalNPC.newAI[1] > 0f)
					{
						fallSpeed *= calamityGlobalNPC.newAI[1] + 1f;
					}
					base.NPC.velocity.Y += fallSpeed;
				}
			}
			else
			{
				float velocityXChange = 0.2f + Math.Abs(base.NPC.Center.X - player.Center.X) * 0.0001f;
				if (calamityGlobalNPC.newAI[0] > 0f)
				{
					velocityXChange *= calamityGlobalNPC.newAI[0] + 1f;
				}
				if (base.NPC.direction < 0)
				{
					base.NPC.velocity.X -= velocityXChange;
				}
				else if (base.NPC.direction > 0)
				{
					base.NPC.velocity.X += velocityXChange;
				}
				float velocityXCap = 12f;
				if (expertMode)
				{
					velocityXCap += (death ? (3.6f * (1f - lifeRatio)) : (2.4f * (1f - lifeRatio)));
				}
				if (Main.getGoodWorld)
				{
					velocityXCap += 5f;
				}
				if (calamityGlobalNPC.newAI[0] > 0f)
				{
					velocityXCap *= calamityGlobalNPC.newAI[0] + 1f;
				}
				if ((float)((base.NPC.Center.X - player.Center.X < 0f) ? 1 : (-1)) != calamityGlobalNPC.newAI[3])
				{
					velocityXCap *= 0.333f;
				}
				if (base.NPC.velocity.X < 0f - velocityXCap)
				{
					base.NPC.velocity.X = 0f - velocityXCap;
				}
				if (base.NPC.velocity.X > velocityXCap)
				{
					base.NPC.velocity.X = velocityXCap;
				}
			}
			base.NPC.ai[3]++;
			if (base.NPC.ai[3] > 30f)
			{
				CustomGravity();
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.X *= 0.8f;
			if (Main.netMode != 1)
			{
				base.NPC.localAI[1]++;
				if (death)
				{
					base.NPC.localAI[2] += 1.25f;
				}
				if (phase4)
				{
					base.NPC.localAI[1]++;
					if (death)
					{
						base.NPC.localAI[2] += 1.25f;
					}
				}
				if (base.NPC.localAI[1] >= (death ? 180f : 240f))
				{
					base.NPC.TargetClosest();
					base.NPC.localAI[1] = 0f;
					Point point4 = (Main.player[base.NPC.target].Center + Utils.SafeNormalize(new Vector2((float)Math.Round(Main.player[base.NPC.target].velocity.X), 0f), Vector2.Zero) * 1000f).ToTileCoordinates();
					int teleportTries = 0;
					while (teleportTries < 100)
					{
						teleportTries++;
						int teleportTileX = Main.rand.Next(point4.X - 5, point4.X + 6);
						int teleportTileY = Main.rand.Next(point4.Y - 5, point4.Y);
						if (!Main.tile[teleportTileX, teleportTileY].HasUnactuatedTile)
						{
							base.NPC.ai[1] = teleportTileX * 16 + 8;
							base.NPC.ai[3] = teleportTileY * 16 + 16;
							break;
						}
					}
					if (teleportTries >= 100)
					{
						Vector2 bottom = Main.player[Player.FindClosest(base.NPC.position, base.NPC.width, base.NPC.height)].Bottom;
						base.NPC.ai[1] = bottom.X;
						base.NPC.ai[3] = bottom.Y;
					}
					base.NPC.ai[0] = 6f;
					base.NPC.netUpdate = true;
				}
			}
			CustomGravity();
		}
		else if (base.NPC.ai[0] == 6f)
		{
			base.NPC.damage = 0;
			if (death)
			{
				base.NPC.localAI[2] += 1.25f;
			}
			if (phase4 && death)
			{
				base.NPC.localAI[2] += 1.25f;
			}
			base.NPC.alpha += 10;
			if (base.NPC.alpha >= 255)
			{
				base.NPC.Bottom = new Vector2(base.NPC.ai[1], base.NPC.ai[3]);
				base.NPC.alpha = 255;
				base.NPC.ai[0] = 7f;
				base.NPC.localAI[2] = 0f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.soundDelay == 0)
			{
				base.NPC.soundDelay = 15;
				SoundEngine.PlaySound(in TeleportSound, base.NPC.Center);
			}
			for (int num8 = 0; num8 < 10; num8++)
			{
				int teleportDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), base.NPC.velocity.X, base.NPC.velocity.Y, 255, default(Color), 2f);
				Main.dust[teleportDust].noGravity = true;
				Dust obj2 = Main.dust[teleportDust];
				obj2.velocity *= 0.5f;
			}
			CustomGravity();
		}
		else
		{
			if (base.NPC.ai[0] != 7f)
			{
				return;
			}
			base.NPC.damage = 0;
			base.NPC.alpha -= 10;
			if (base.NPC.alpha <= 0)
			{
				bool spawnFlag = expertMode;
				if (NPC.CountNPCS(ModContent.NPCType<AureusSpawn>()) >= 2)
				{
					spawnFlag = false;
				}
				if (spawnFlag && Main.netMode != 1)
				{
					int aureusSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y - 25f * base.NPC.scale), ModContent.NPCType<AureusSpawn>());
					Main.npc[aureusSpawn].velocity.Y = -10f;
					Main.npc[aureusSpawn].netUpdate = true;
					if (revenge)
					{
						aureusSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y - 25f * base.NPC.scale), ModContent.NPCType<AureusSpawn>());
						Main.npc[aureusSpawn].velocity.Y = -15f;
						Main.npc[aureusSpawn].netUpdate = true;
					}
					if (death)
					{
						int damageAmt = base.NPC.lifeMax / 50;
						base.NPC.life -= damageAmt;
						if (base.NPC.life < 1)
						{
							base.NPC.life = 1;
						}
						base.NPC.DamageEffect(damageAmt);
					}
				}
				base.NPC.alpha = 0;
				base.NPC.ai[0] = (exhausted ? 1f : 2f);
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				base.NPC.localAI[3] = noProjectileOrPhaseIncrementTime;
				base.NPC.noTileCollide = base.NPC.ai[0] == 2f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.soundDelay == 0)
			{
				base.NPC.soundDelay = 15;
				SoundEngine.PlaySound(in SoundID.Item109, base.NPC.Center);
			}
			for (int num9 = 0; num9 < 10; num9++)
			{
				int teleportDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), base.NPC.velocity.X, base.NPC.velocity.Y, 255, default(Color), 2f);
				Main.dust[teleportDust2].noGravity = true;
				Dust obj3 = Main.dust[teleportDust2];
				obj3.velocity *= 0.5f;
			}
			CustomGravity();
		}
		void CustomGravity()
		{
			float gravity = 0.36f;
			float maxFallSpeed = 12f;
			if (calamityGlobalNPC.newAI[1] > 0f && !reduceFallSpeed)
			{
				maxFallSpeed *= calamityGlobalNPC.newAI[1] + 1f;
			}
			if (Main.getGoodWorld && !reduceFallSpeed)
			{
				gravity *= 1.15f;
				maxFallSpeed *= 1.15f;
			}
			base.NPC.velocity.Y += gravity;
			if (base.NPC.velocity.Y > maxFallSpeed)
			{
				base.NPC.velocity.Y = maxFallSpeed;
			}
		}
	}

	public override bool? CanFallThroughPlatforms()
	{
		return base.NPC.target >= 0 && Main.player[base.NPC.target].position.Y > base.NPC.position.Y + (float)base.NPC.height;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f)
		{
			if (base.NPC.velocity.Y == 0f && base.NPC.ai[1] >= 0f && base.NPC.ai[0] == 3f)
			{
				if (stomping)
				{
					stomping = false;
				}
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 12.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight * 6)
				{
					base.NPC.frame.Y = 0;
				}
				return;
			}
			if (base.NPC.velocity.Y <= 0f || base.NPC.ai[1] < 0f)
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 12.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight * 5)
				{
					base.NPC.frame.Y = frameHeight * 5;
				}
				return;
			}
			if (!stomping)
			{
				stomping = true;
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = 0;
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = frameHeight * 5;
			}
		}
		else if (base.NPC.ai[0] >= 5f)
		{
			if (stomping)
			{
				stomping = false;
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 12.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight * 6)
				{
					base.NPC.frame.Y = 0;
				}
			}
			else
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 12.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight * 5)
				{
					base.NPC.frame.Y = frameHeight * 5;
				}
			}
		}
		else
		{
			if (stomping)
			{
				stomping = false;
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 8.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 6)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool slimePhaseHP = lifeRatio <= 0.1f || (lifeRatio > 0.6f && lifeRatio <= 0.7f);
		Texture2D NPCTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D GlowMaskTexture = TextureAssets.Npc[base.Type].Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		if (base.NPC.ai[0] == 0f || (slimePhaseHP && Main.zenithWorld))
		{
			NPCTexture = TextureAssets.Npc[base.Type].Value;
			GlowMaskTexture = Texture_Glow.Value;
		}
		else if (base.NPC.ai[0] == 1f)
		{
			NPCTexture = RechargeTexture.Value;
		}
		else if (base.NPC.ai[0] == 2f)
		{
			NPCTexture = WalkTexture.Value;
			GlowMaskTexture = WalkTexture_Glow.Value;
		}
		else if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f)
		{
			if (base.NPC.velocity.Y == 0f && base.NPC.ai[1] >= 0f && base.NPC.ai[0] == 3f)
			{
				NPCTexture = TextureAssets.Npc[base.Type].Value;
				GlowMaskTexture = Texture_Glow.Value;
			}
			else if (base.NPC.velocity.Y <= 0f || base.NPC.ai[1] < 0f)
			{
				NPCTexture = JumpTexture.Value;
				GlowMaskTexture = JumpTexture_Glow.Value;
			}
			else
			{
				NPCTexture = StompTexture.Value;
				GlowMaskTexture = StompTexture_Glow.Value;
			}
		}
		else if (base.NPC.ai[0] >= 5f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				NPCTexture = TextureAssets.Npc[base.Type].Value;
				GlowMaskTexture = Texture_Glow.Value;
			}
			else
			{
				NPCTexture = JumpTexture.Value;
				GlowMaskTexture = JumpTexture_Glow.Value;
			}
		}
		int frameCount = Main.npcFrameCount[base.Type];
		Vector2 originalDrawSize = default(Vector2);
		((Vector2)(ref originalDrawSize))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / frameCount / 2));
		Rectangle frame = base.NPC.frame;
		float scale = base.NPC.scale;
		float rotation = base.NPC.rotation;
		float offsetY = base.NPC.gfxOffY;
		Color slimeColor = Color.White;
		if (Main.zenithWorld & slimePhaseHP)
		{
			slimeColor = ((slimePhase == 0) ? Color.Yellow : Color.Violet);
		}
		float colorLerpAmt = 0.5f;
		int afterimageAmt = 7;
		if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f)
		{
			afterimageAmt = 10;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, slimeColor, colorLerpAmt);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)NPCTexture.Width, (float)(NPCTexture.Height / frameCount)) * scale / 2f;
				afterimagePos += originalDrawSize * scale + new Vector2(0f, 4f + offsetY);
				spriteBatch.Draw(NPCTexture, afterimagePos, (Rectangle?)frame, afterimageColor, rotation, originalDrawSize, scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)NPCTexture.Width, (float)(NPCTexture.Height / frameCount)) * scale / 2f;
		drawLocation += originalDrawSize * scale + new Vector2(0f, 4f + offsetY);
		Color toUse = ((Main.zenithWorld & slimePhaseHP) ? slimeColor : drawColor);
		spriteBatch.Draw(NPCTexture, drawLocation, (Rectangle?)frame, base.NPC.GetAlpha(toUse), rotation, originalDrawSize, scale, spriteEffects, 0f);
		if (base.NPC.ai[0] != 1f || (slimePhaseHP && Main.zenithWorld))
		{
			Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.Gold);
			Color attackingColor = Color.Lerp(Color.White, color, 0.5f);
			if (Main.zenithWorld & slimePhaseHP)
			{
				attackingColor = ((slimePhase == 0) ? Color.Violet : Color.Yellow);
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int j = 1; j < afterimageAmt; j++)
				{
					Color attackingAfterimageColor = attackingColor;
					attackingAfterimageColor = Color.Lerp(attackingAfterimageColor, slimeColor, colorLerpAmt);
					attackingAfterimageColor = base.NPC.GetAlpha(attackingAfterimageColor);
					attackingAfterimageColor *= (float)(afterimageAmt - j) / 15f;
					Vector2 attackAfterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					attackAfterimagePos -= new Vector2((float)GlowMaskTexture.Width, (float)(GlowMaskTexture.Height / frameCount)) * scale / 2f;
					attackAfterimagePos += originalDrawSize * scale + new Vector2(0f, 4f + offsetY);
					spriteBatch.Draw(GlowMaskTexture, attackAfterimagePos, (Rectangle?)frame, attackingAfterimageColor, rotation, originalDrawSize, scale, spriteEffects, 0f);
				}
			}
			spriteBatch.Draw(GlowMaskTexture, drawLocation, (Rectangle?)frame, attackingColor, rotation, originalDrawSize, scale, spriteEffects, 0f);
		}
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AstrumAureusBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[5]
		{
			ModContent.ItemType<Nebulash>(),
			ModContent.ItemType<AuroraBlazer>(),
			ModContent.ItemType<AlulaAustralis>(),
			ModContent.ItemType<BorealisBomber>(),
			ModContent.ItemType<AuroradicalThrow>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<AstrumAureusMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<AureusCell>(), 1, 9, 12));
		normalOnly.Add(ModContent.ItemType<LeonidProgenitor>(), 10);
		normalOnly.Add(ModContent.ItemType<SuspiciousLookingJellyBean>());
		npcLoot.Add(ModContent.ItemType<AstrumAureusTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<AstrumAureusRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(1634, 1, 1, 9999), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedAstrumAureus, ModContent.ItemType<LoreAstrumAureus>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnKill()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			if (!DownedBossSystem.downedAstrumAureus)
			{
				string key2 = "Mods.CalamityMod.Status.Progression.AureusBossText2";
				Color messageColor = Color.Gold;
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.AureusBossText", messageColor);
				CalamityUtils.BroadcastLocalizedText(key2, messageColor);
			}
			ThreadPool.QueueUserWorkItem(delegate
			{
				AstralBiome.PlaceAstralMeteor();
			});
			DownedBossSystem.downedAstrumAureus = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 16;
			SoundEngine.PlaySound(in HitSound, base.NPC.Center);
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (int)(150f * base.NPC.scale);
		base.NPC.height = (int)(100f * base.NPC.scale);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int r = 0; r < 30; r++)
		{
			int aureusDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[aureusDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[aureusDust].scale = 0.5f;
				Main.dust[aureusDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int s = 0; s < 60; s++)
		{
			int aureusDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
			Main.dust[aureusDust2].noGravity = true;
			Dust obj2 = Main.dust[aureusDust2];
			obj2.velocity *= 5f;
			aureusDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[aureusDust2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus1").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus4").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus5").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus6").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus7").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus8").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus9").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus10").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("Aureus11").Type);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcCenter = base.NPC.Center;
		Rectangle leftHitbox = default(Rectangle);
		((Rectangle)(ref leftHitbox))._002Ector((int)(npcCenter.X - 92f * base.NPC.scale), (int)(npcCenter.Y + 28f * base.NPC.scale), 10, 10);
		Rectangle bodyHitbox = default(Rectangle);
		((Rectangle)(ref bodyHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 4f), (int)(npcCenter.Y - (float)base.NPC.height / 2f + 24f * base.NPC.scale), base.NPC.width / 2, base.NPC.height);
		Rectangle rightHitbox = default(Rectangle);
		((Rectangle)(ref rightHitbox))._002Ector((int)(npcCenter.X + 92f * base.NPC.scale), (int)(npcCenter.Y + 28f * base.NPC.scale), 10, 10);
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
		bool num = minLeftDist <= 120f * base.NPC.scale;
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
		bool insideBodyHitbox = minBodyDist <= 160f * base.NPC.scale;
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
		bool insideRightHitbox = minRightDist <= 120f * base.NPC.scale;
		if ((num | insideBodyHitbox | insideRightHitbox) && base.NPC.alpha == 0)
		{
			return base.NPC.ai[0] > 1f;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 360);
		}
	}
}
