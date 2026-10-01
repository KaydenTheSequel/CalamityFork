using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Tiles.Ores;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Yharon;

[AutoloadBossHead]
public class Yharon : ModNPC
{
	private Rectangle safeBox;

	private bool enraged;

	private bool protectionBoost;

	private bool moveCloser;

	private bool useTornado = true;

	private int secondPhasePhase = 1;

	private int teleportLocation;

	private bool startSecondAI;

	private bool spawnArena;

	private int invincibilityCounter;

	private int fastChargeTelegraphTime = 120;

	private const float AI2GateValue = 0.55f;

	private const int Phase2InvincibilityTime = 300;

	private const float EnragedDR = 0.9f;

	public static readonly SoundStyle RoarSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonRoar");

	public static readonly SoundStyle ShortRoarSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonRoarShort");

	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonFire");

	public static readonly SoundStyle OrbSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonFireOrb");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/YharonHurt");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/YharonDeath");

	public SlotId RoarSoundSlot;

	public static Asset<Texture2D> GlowTextureGreen;

	public static Asset<Texture2D> GlowTextureOrange;

	public static Asset<Texture2D> GlowTexturePurple;

	public static int FlareDamage = 66;

	public static int FireballDamage = 66;

	public static int TornadoDamage = 85;

	public static int BordernadoDamage = 125;

	public static int VortexDamage = 85;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.3f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.4f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -16f;
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 26f;
		value.Position.Y -= 14f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTextureGreen = ModContent.Request<Texture2D>(Texture + "GlowGreen", (AssetRequestMode)2);
			GlowTextureOrange = ModContent.Request<Texture2D>(Texture + "GlowOrange", (AssetRequestMode)2);
			GlowTexturePurple = ModContent.Request<Texture2D>(Texture + "GlowPurple", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 210;
		base.NPC.npcSlots = 50f;
		base.NPC.width = 200;
		base.NPC.height = 200;
		base.NPC.defense = 90;
		base.NPC.LifeMaxNERB(1000000, 1560000, 740000);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(2, 50);
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Yharon")
		});
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (startSecondAI)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.YharonPhase2");
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(new BitsByte
		{
			[0] = enraged,
			[1] = protectionBoost,
			[2] = moveCloser,
			[3] = useTornado,
			[4] = startSecondAI,
			[5] = base.NPC.dontTakeDamage
		});
		writer.Write(secondPhasePhase);
		writer.Write(teleportLocation);
		writer.Write(invincibilityCounter);
		writer.Write(fastChargeTelegraphTime);
		writer.Write(safeBox.X);
		writer.Write(safeBox.Y);
		writer.Write(safeBox.Width);
		writer.Write(safeBox.Height);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		BitsByte bb = reader.ReadByte();
		enraged = bb[0];
		protectionBoost = bb[1];
		moveCloser = bb[2];
		useTornado = bb[3];
		startSecondAI = bb[4];
		base.NPC.dontTakeDamage = bb[5];
		secondPhasePhase = reader.ReadInt32();
		teleportLocation = reader.ReadInt32();
		invincibilityCounter = reader.ReadInt32();
		fastChargeTelegraphTime = reader.ReadInt32();
		safeBox.X = reader.ReadInt32();
		safeBox.Y = reader.ReadInt32();
		safeBox.Width = reader.ReadInt32();
		safeBox.Height = reader.ReadInt32();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c88: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1109: Unknown result type (might be due to invalid IL or missing references)
		//IL_110e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1113: Unknown result type (might be due to invalid IL or missing references)
		//IL_111a: Unknown result type (might be due to invalid IL or missing references)
		//IL_111f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1127: Unknown result type (might be due to invalid IL or missing references)
		//IL_112c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10af: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1988: Unknown result type (might be due to invalid IL or missing references)
		//IL_1992: Unknown result type (might be due to invalid IL or missing references)
		//IL_1997: Unknown result type (might be due to invalid IL or missing references)
		//IL_1733: Unknown result type (might be due to invalid IL or missing references)
		//IL_174a: Unknown result type (might be due to invalid IL or missing references)
		//IL_174f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1754: Unknown result type (might be due to invalid IL or missing references)
		//IL_1756: Unknown result type (might be due to invalid IL or missing references)
		//IL_175e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1763: Unknown result type (might be due to invalid IL or missing references)
		//IL_176e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1773: Unknown result type (might be due to invalid IL or missing references)
		//IL_1778: Unknown result type (might be due to invalid IL or missing references)
		//IL_177f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1784: Unknown result type (might be due to invalid IL or missing references)
		//IL_178c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1791: Unknown result type (might be due to invalid IL or missing references)
		//IL_1714: Unknown result type (might be due to invalid IL or missing references)
		//IL_171b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1720: Unknown result type (might be due to invalid IL or missing references)
		//IL_116b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1175: Unknown result type (might be due to invalid IL or missing references)
		//IL_117a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17be: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_1191: Unknown result type (might be due to invalid IL or missing references)
		//IL_1159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1866: Unknown result type (might be due to invalid IL or missing references)
		//IL_1876: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1830: Unknown result type (might be due to invalid IL or missing references)
		//IL_1832: Unknown result type (might be due to invalid IL or missing references)
		//IL_288b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2895: Unknown result type (might be due to invalid IL or missing references)
		//IL_289a: Unknown result type (might be due to invalid IL or missing references)
		//IL_274f: Unknown result type (might be due to invalid IL or missing references)
		//IL_275a: Unknown result type (might be due to invalid IL or missing references)
		//IL_275f: Unknown result type (might be due to invalid IL or missing references)
		//IL_276f: Unknown result type (might be due to invalid IL or missing references)
		//IL_277a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_132a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1335: Unknown result type (might be due to invalid IL or missing references)
		//IL_133a: Unknown result type (might be due to invalid IL or missing references)
		//IL_133f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_292e: Unknown result type (might be due to invalid IL or missing references)
		//IL_293e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c51: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e43: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e64: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e73: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e88: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e30: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_391b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3925: Unknown result type (might be due to invalid IL or missing references)
		//IL_392a: Unknown result type (might be due to invalid IL or missing references)
		//IL_37f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3803: Unknown result type (might be due to invalid IL or missing references)
		//IL_3808: Unknown result type (might be due to invalid IL or missing references)
		//IL_3818: Unknown result type (might be due to invalid IL or missing references)
		//IL_3823: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ee0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eef: Unknown result type (might be due to invalid IL or missing references)
		//IL_204d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2058: Unknown result type (might be due to invalid IL or missing references)
		//IL_205d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2062: Unknown result type (might be due to invalid IL or missing references)
		//IL_2069: Unknown result type (might be due to invalid IL or missing references)
		//IL_206e: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1547: Unknown result type (might be due to invalid IL or missing references)
		//IL_154c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1551: Unknown result type (might be due to invalid IL or missing references)
		//IL_1558: Unknown result type (might be due to invalid IL or missing references)
		//IL_155f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1564: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_397e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3989: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ef6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b36: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_39be: Unknown result type (might be due to invalid IL or missing references)
		//IL_39ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2172: Unknown result type (might be due to invalid IL or missing references)
		//IL_217d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2195: Unknown result type (might be due to invalid IL or missing references)
		//IL_219a: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21df: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f14: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f30: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f42: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ed1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3daf: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2560: Unknown result type (might be due to invalid IL or missing references)
		//IL_256b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2570: Unknown result type (might be due to invalid IL or missing references)
		//IL_2575: Unknown result type (might be due to invalid IL or missing references)
		//IL_257c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2581: Unknown result type (might be due to invalid IL or missing references)
		//IL_240c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2417: Unknown result type (might be due to invalid IL or missing references)
		//IL_241c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f65: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f74: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f53: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_2484: Unknown result type (might be due to invalid IL or missing references)
		//IL_248f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2494: Unknown result type (might be due to invalid IL or missing references)
		//IL_2499: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_30ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_30b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_30bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_30c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_30c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_30cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f99: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_222f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2234: Unknown result type (might be due to invalid IL or missing references)
		//IL_4017: Unknown result type (might be due to invalid IL or missing references)
		//IL_4027: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fe1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fe3: Unknown result type (might be due to invalid IL or missing references)
		//IL_31d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_31db: Unknown result type (might be due to invalid IL or missing references)
		//IL_334d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3352: Unknown result type (might be due to invalid IL or missing references)
		//IL_31f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_31f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_31ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_3220: Unknown result type (might be due to invalid IL or missing references)
		//IL_3225: Unknown result type (might be due to invalid IL or missing references)
		//IL_3229: Unknown result type (might be due to invalid IL or missing references)
		//IL_322e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3230: Unknown result type (might be due to invalid IL or missing references)
		//IL_3235: Unknown result type (might be due to invalid IL or missing references)
		//IL_323d: Unknown result type (might be due to invalid IL or missing references)
		//IL_35be: Unknown result type (might be due to invalid IL or missing references)
		//IL_35c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_35ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_35da: Unknown result type (might be due to invalid IL or missing references)
		//IL_35df: Unknown result type (might be due to invalid IL or missing references)
		//IL_346a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3475: Unknown result type (might be due to invalid IL or missing references)
		//IL_347a: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_34f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_34fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_3505: Unknown result type (might be due to invalid IL or missing references)
		//IL_350a: Unknown result type (might be due to invalid IL or missing references)
		//IL_328d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3292: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		CalamityGlobalNPC.yharon = base.NPC.whoAmI;
		CalamityGlobalNPC.yharonP2 = -1;
		int setDamage = base.NPC.defDamage;
		if (startSecondAI)
		{
			Yharon_AI2(expertMode, revenge, death, lifeRatio, calamityGlobalNPC, setDamage);
			return;
		}
		float phase2GateValue = (revenge ? 0.9f : (expertMode ? 0.85f : 0.8f));
		bool phase2Check = death || lifeRatio <= phase2GateValue;
		bool phase3Check = lifeRatio <= (death ? 0.8f : (revenge ? 0.75f : (expertMode ? 0.7f : 0.65f)));
		bool phase4Check = lifeRatio <= 0.55f;
		bool phase1Change = base.NPC.ai[0] > -1f;
		bool phase2Change = base.NPC.ai[0] > 5f;
		bool phase3Change = base.NPC.ai[0] > 12f;
		int phaseSwitchTimer = (expertMode ? 36 : 40);
		float acceleration = (expertMode ? 0.75f : 0.7f);
		float velocity = (expertMode ? 12f : 11f);
		if (phase3Change)
		{
			acceleration = (expertMode ? 0.85f : 0.8f);
			velocity = (expertMode ? 14f : 13f);
			phaseSwitchTimer = (expertMode ? 25 : 28);
			fastChargeTelegraphTime = 100;
		}
		else if (phase2Change)
		{
			acceleration = (expertMode ? 0.8f : 0.75f);
			velocity = (expertMode ? 13f : 12f);
			phaseSwitchTimer = (expertMode ? 32 : 36);
			fastChargeTelegraphTime = 110;
		}
		else
		{
			phaseSwitchTimer = 25;
		}
		float reduceSpeedChargeDistance = 540f;
		int chargeTime = (expertMode ? 40 : 45);
		float chargeSpeed = (expertMode ? 28f : 26f);
		float fastChargeVelocityMultiplier = 1.5f;
		bool playFastChargeRoarSound = base.NPC.localAI[1] == (float)fastChargeTelegraphTime * 0.5f;
		bool doFastCharge = base.NPC.localAI[1] > (float)fastChargeTelegraphTime;
		if (phase3Change)
		{
			chargeTime = 35;
			chargeSpeed = 30f;
		}
		else if (phase2Change)
		{
			chargeTime = (expertMode ? 38 : 43);
			if (expertMode)
			{
				chargeSpeed = 28.5f;
			}
		}
		if (revenge)
		{
			int chargeTimeDecrease = (death ? 4 : 2);
			float velocityMult = (death ? 1.1f : 1.05f);
			phaseSwitchTimer -= chargeTimeDecrease;
			acceleration *= velocityMult;
			velocity *= velocityMult;
			chargeTime -= chargeTimeDecrease;
			chargeSpeed *= velocityMult;
		}
		float reduceSpeedFlareBombDistance = 570f;
		int flareBombPhaseTimer = (death ? 40 : 60);
		int flareBombSpawnDivisor = flareBombPhaseTimer / 20;
		float flareBombPhaseAcceleration = (death ? 0.92f : 0.8f);
		float flareBombPhaseVelocity = (death ? 14f : 12f);
		int fireTornadoPhaseTimer = 90;
		int newPhaseTimer = 180;
		int flareDustPhaseTimer = (death ? 200 : 240);
		int flareDustPhaseTimer2 = (death ? 100 : 120);
		float spinTime = flareDustPhaseTimer / 2;
		int flareDustSpawnDivisor = flareDustPhaseTimer / 10;
		int flareDustSpawnDivisor2 = flareDustPhaseTimer2 / 30;
		int flareDustSpawnDivisor3 = flareDustPhaseTimer / 25;
		float spinPhaseVelocity = 25f;
		float spinPhaseRotation = (float)Math.PI * 6f / spinTime;
		float increasedIdleTimeAfterBulletHell = (death ? 120f : 180f) + (float)phaseSwitchTimer;
		bool num = base.NPC.ai[2] < 0f;
		bool slowChargeAfterBulletHell = base.NPC.ai[2] == -1f;
		if (num)
		{
			float reducedMovementMultiplier = MathHelper.Lerp(0.1f, death ? 1f : 0.75f, (base.NPC.ai[2] + increasedIdleTimeAfterBulletHell) / increasedIdleTimeAfterBulletHell);
			acceleration *= reducedMovementMultiplier;
			velocity *= reducedMovementMultiplier;
			chargeSpeed *= reducedMovementMultiplier;
		}
		float teleportPhaseTimer = 30f;
		int spawnPhaseTimer = 75;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (player.dead || !player.active)
		{
			base.NPC.TargetClosest();
			player = Main.player[base.NPC.target];
			if (player.dead || !player.active)
			{
				base.NPC.velocity.Y -= 0.4f;
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.ai[0] > 12f)
				{
					base.NPC.ai[0] = 13f;
				}
				else if (base.NPC.ai[0] > 5f)
				{
					base.NPC.ai[0] = 6f;
				}
				else
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.ai[2] = 0f;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		int xPos = 60 * base.NPC.direction;
		Vector2 vector = Vector2.Normalize(player.Center - base.NPC.Center) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
		Vector2 fromMouth = default(Vector2);
		((Vector2)(ref fromMouth))._002Ector((float)((int)vector.X + xPos), (float)((int)vector.Y - 15));
		if (!spawnArena)
		{
			spawnArena = true;
			enraged = false;
			if (Main.netMode != 1)
			{
				safeBox.X = (int)(player.Center.X - (Main.zenithWorld ? 1500f : (Main.getGoodWorld ? 1000f : (revenge ? 3000f : 3500f))));
				safeBox.Y = (int)Main.topWorld;
				safeBox.Width = (Main.zenithWorld ? 3000 : (Main.getGoodWorld ? 2000 : (revenge ? 6000 : 7000)));
				safeBox.Height = Main.maxTilesY * 16;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X + (Main.zenithWorld ? 1500f : (Main.getGoodWorld ? 1000f : (revenge ? 3000f : 3500f))), player.Center.Y + 100f, 0f, 0f, ModContent.ProjectileType<SkyFlareRevenge>(), 0, 0f, Main.myPlayer);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X - (Main.zenithWorld ? 1500f : (Main.getGoodWorld ? 1000f : (revenge ? 3000f : 3500f))), player.Center.Y + 100f, 0f, 0f, ModContent.ProjectileType<SkyFlareRevenge>(), 0, 0f, Main.myPlayer);
			}
			base.NPC.netUpdate = true;
		}
		else
		{
			Rectangle hitbox = player.Hitbox;
			enraged = !((Rectangle)(ref hitbox)).Intersects(safeBox);
			base.NPC.Calamity().CurrentlyEnraged = enraged;
			if (enraged)
			{
				phaseSwitchTimer = 15;
				protectionBoost = true;
				setDamage *= 5;
				chargeSpeed += 25f;
			}
			else
			{
				protectionBoost = false;
			}
		}
		if (Main.getGoodWorld)
		{
			phaseSwitchTimer /= 2;
		}
		if (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 13f)
		{
			_ = base.NPC.localAI[1] > 0f;
		}
		else
			_ = 0;
		bool bulletHell = base.NPC.ai[0] == 8f || base.NPC.ai[0] == 15f;
		calamityGlobalNPC.DR = (protectionBoost ? 0.9f : 0f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = protectionBoost;
		if (!protectionBoost)
		{
			if (phase3Change)
			{
				calamityGlobalNPC.DR = (phase4Check ? 0.7f : 0f);
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phase4Check;
			}
			else if (phase2Change)
			{
				calamityGlobalNPC.DR = (phase3Check ? 0.7f : 0f);
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phase3Check;
			}
			else if (phase1Change)
			{
				calamityGlobalNPC.DR = (phase2Check ? 0.7f : 0f);
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phase2Check;
			}
		}
		base.NPC.dontTakeDamage = bulletHell;
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.Opacity = 0f;
			base.NPC.rotation = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.netUpdate = true;
			}
		}
		float npcRotation = (float)Math.Atan2(player.Center.Y - base.NPC.Center.Y, player.Center.X - base.NPC.Center.X);
		if (base.NPC.spriteDirection == 1)
		{
			npcRotation += (float)Math.PI;
		}
		if (npcRotation < 0f)
		{
			npcRotation += (float)Math.PI * 2f;
		}
		if (npcRotation > (float)Math.PI * 2f)
		{
			npcRotation -= (float)Math.PI * 2f;
		}
		if (base.NPC.ai[0] == -1f || base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f || base.NPC.ai[0] == 10f || base.NPC.ai[0] == 16f)
		{
			npcRotation = 0f;
		}
		float npcRotationSpeed = 0.04f;
		if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 5f || base.NPC.ai[0] == 7f || base.NPC.ai[0] == 11f || base.NPC.ai[0] == 12f || base.NPC.ai[0] == 14f || base.NPC.ai[0] == 18f || base.NPC.ai[0] == 19f)
		{
			npcRotationSpeed = 0f;
		}
		if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f || base.NPC.ai[0] == 16f)
		{
			npcRotationSpeed = 0.01f;
		}
		if (npcRotationSpeed != 0f)
		{
			base.NPC.rotation = base.NPC.rotation.AngleTowards(npcRotation, npcRotationSpeed);
		}
		if (base.NPC.ai[0] != -1f && !bulletHell && ((base.NPC.ai[0] != 6f && base.NPC.ai[0] != 13f) || base.NPC.ai[2] <= (float)phaseSwitchTimer))
		{
			if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.Opacity -= 0.1f;
			}
			else
			{
				base.NPC.Opacity += 0.1f;
			}
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
			if (base.NPC.Opacity < 0.6f)
			{
				base.NPC.Opacity = 0.6f;
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.damage = 0;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.98f;
			int playerFacingDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (playerFacingDirection != 0)
			{
				base.NPC.direction = playerFacingDirection;
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (base.NPC.ai[2] > 20f)
			{
				base.NPC.velocity.Y = -2f;
				base.NPC.Opacity += 0.1f;
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.Opacity -= 0.1f;
				}
				if (base.NPC.Opacity > 1f)
				{
					base.NPC.Opacity = 1f;
				}
				if (base.NPC.Opacity < 0.6f)
				{
					base.NPC.Opacity = 0.6f;
				}
			}
			if (base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				int dustAmt = 72;
				for (int i = 0; i < dustAmt; i++)
				{
					Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)base.NPC.width / 2f, (float)base.NPC.height) * 0.75f * 0.5f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.NPC.Center;
					Vector2 dustDirection = val - base.NPC.Center;
					int orangeDust = Dust.NewDust(val + dustDirection, 0, 0, 244, dustDirection.X * 2f, dustDirection.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[orangeDust].noGravity = true;
					Main.dust[orangeDust].noLight = true;
					Main.dust[orangeDust].velocity = Vector2.Normalize(dustDirection) * 3f;
				}
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)spawnPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = Main.rand.Next(4);
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 0f && !player.dead)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 destination = player.Center + new Vector2(base.NPC.ai[1], 0f);
			Vector2 desiredVelocity = Vector2.Normalize(destination - base.NPC.Center - base.NPC.velocity) * velocity;
			if (Vector2.Distance(base.NPC.Center, destination) > reduceSpeedChargeDistance && base.NPC.localAI[1] <= (float)fastChargeTelegraphTime * 0.5f)
			{
				base.NPC.SimpleFlyMovement(desiredVelocity, acceleration);
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.98f;
			}
			int phaseSwitchFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (phaseSwitchFaceDirection != 0)
			{
				if (base.NPC.ai[2] == 0f && phaseSwitchFaceDirection != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = phaseSwitchFaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (!(base.NPC.ai[2] >= (float)phaseSwitchTimer))
			{
				return;
			}
			int aiState = 0;
			switch ((int)base.NPC.ai[3])
			{
			case 0:
			case 1:
			case 2:
				aiState = 1;
				break;
			case 3:
				aiState = 5;
				break;
			case 4:
				base.NPC.ai[3] = 1f;
				aiState = 2;
				break;
			case 5:
				base.NPC.ai[3] = 0f;
				aiState = 3;
				break;
			}
			if (phase2Check)
			{
				aiState = 4;
			}
			switch (aiState)
			{
			case 1:
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeSpeed;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (phaseSwitchFaceDirection != 0)
				{
					base.NPC.direction = phaseSwitchFaceDirection;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				break;
			case 2:
				base.NPC.ai[0] = 2f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 3:
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 4:
				base.NPC.ai[0] = 4f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 5:
				if (playFastChargeRoarSound)
				{
					RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				}
				if (doFastCharge)
				{
					base.NPC.ai[0] = 5f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.localAI[1] = 0f;
					base.NPC.netUpdate = true;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeSpeed * fastChargeVelocityMultiplier;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (phaseSwitchFaceDirection != 0)
					{
						base.NPC.direction = phaseSwitchFaceDirection;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
				}
				else
				{
					base.NPC.localAI[1]++;
				}
				break;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = setDamage;
			ChargeDust(7);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 destination2 = player.Center + new Vector2(base.NPC.ai[1], 0f);
			Vector2 flareSpeed = Vector2.Normalize(destination2 - base.NPC.Center - base.NPC.velocity) * flareBombPhaseVelocity;
			if (Vector2.Distance(base.NPC.Center, destination2) > reduceSpeedFlareBombDistance)
			{
				base.NPC.SimpleFlyMovement(flareSpeed, flareBombPhaseAcceleration);
			}
			else
			{
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 0.98f;
			}
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			if (base.NPC.ai[2] % (float)flareBombSpawnDivisor == 0f && Main.netMode != 1)
			{
				int type = ModContent.ProjectileType<FlareBomb>();
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fromMouth, Vector2.Zero, type, FlareDamage, 0f, Main.myPlayer, base.NPC.target, 1f);
			}
			int playerFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (playerFaceDirection != 0)
			{
				base.NPC.direction = playerFaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)flareBombPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, base.NPC.direction * 4, 8f, ModContent.ProjectileType<Flare>(), 0, 0f, Main.myPlayer);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (0f - (float)base.NPC.direction) * 4f, 8f, ModContent.ProjectileType<Flare>(), 0, 0f, Main.myPlayer);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)fireTornadoPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			NPC nPC5 = base.NPC;
			nPC5.velocity *= 0.9f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(newPhaseTimer - 60))
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)newPhaseTimer)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = Main.rand.Next(5);
				base.NPC.localAI[1] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = setDamage;
			ChargeDust(14);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 6f && !player.dead)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 destination3 = player.Center + new Vector2(base.NPC.ai[1], 0f);
			Vector2 desiredVelocity2 = Vector2.Normalize(destination3 - base.NPC.Center - base.NPC.velocity) * velocity;
			if (Vector2.Distance(base.NPC.Center, destination3) > reduceSpeedChargeDistance && base.NPC.localAI[1] <= (float)fastChargeTelegraphTime * 0.5f)
			{
				base.NPC.SimpleFlyMovement(desiredVelocity2, acceleration);
			}
			else
			{
				NPC nPC6 = base.NPC;
				nPC6.velocity *= 0.98f;
			}
			int playerFaceDirectionFurtherPhases = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (playerFaceDirectionFurtherPhases != 0)
			{
				if (base.NPC.ai[2] == 0f && playerFaceDirectionFurtherPhases != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = playerFaceDirectionFurtherPhases;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (!((base.NPC.ai[2] >= (float)phaseSwitchTimer) | slowChargeAfterBulletHell))
			{
				return;
			}
			int aiState2 = 0;
			switch ((int)base.NPC.ai[3])
			{
			case 0:
			case 1:
			case 2:
			case 3:
				aiState2 = 1;
				break;
			case 4:
				aiState2 = 5;
				break;
			case 5:
				aiState2 = 6;
				break;
			case 6:
				aiState2 = 2;
				break;
			case 7:
				base.NPC.ai[3] = 0f;
				aiState2 = 3;
				break;
			}
			if (phase3Check)
			{
				aiState2 = 4;
			}
			switch (aiState2)
			{
			case 1:
				base.NPC.ai[0] = 7f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeSpeed;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (playerFaceDirectionFurtherPhases != 0)
				{
					base.NPC.direction = playerFaceDirectionFurtherPhases;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				break;
			case 2:
				if (base.NPC.Opacity > 0f)
				{
					base.NPC.Opacity -= 0.2f;
					if (base.NPC.Opacity < 0f)
					{
						base.NPC.Opacity = 0f;
					}
				}
				if (base.NPC.ai[2] == (float)phaseSwitchTimer + 15f)
				{
					SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						float bulletHellTeleportLocationDistance = 540f;
						Vector2 teleportLocation = -Vector2.UnitY * bulletHellTeleportLocationDistance * (float)(player.velocity.Y >= 0f).ToDirectionInt();
						Vector2 center = player.Center + teleportLocation;
						base.NPC.Center = center;
						int type2 = ModContent.ProjectileType<YharonBulletHellVortex>();
						int damage = (Main.zenithWorld ? VortexDamage : 0);
						float bulletHellVortexDuration = (float)flareDustPhaseTimer + teleportPhaseTimer - 15f;
						int extraTime = (Main.zenithWorld ? 300 : 0);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type2, damage, 0f, Main.myPlayer, bulletHellVortexDuration + (float)extraTime, base.NPC.whoAmI);
						int damageAmt = (int)((float)base.NPC.lifeMax * (bulletHellVortexDuration / (float)calamityGlobalNPC.KillTime));
						base.NPC.life -= damageAmt;
						if (base.NPC.life < 1)
						{
							base.NPC.life = 1;
						}
						base.NPC.DamageEffect(damageAmt);
						base.NPC.netUpdate = true;
					}
				}
				if (base.NPC.ai[2] >= (float)phaseSwitchTimer + 15f)
				{
					base.NPC.dontTakeDamage = true;
					base.NPC.velocity = Vector2.Zero;
				}
				if (!(base.NPC.ai[2] < (float)phaseSwitchTimer + teleportPhaseTimer))
				{
					base.NPC.ai[0] = 8f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 1f;
					base.NPC.netUpdate = true;
				}
				break;
			case 3:
				base.NPC.ai[0] = 9f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 4:
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 5:
				if (playFastChargeRoarSound)
				{
					RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				}
				if (doFastCharge)
				{
					base.NPC.ai[0] = 11f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.localAI[1] = 0f;
					base.NPC.netUpdate = true;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeSpeed * fastChargeVelocityMultiplier;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (playerFaceDirectionFurtherPhases != 0)
					{
						base.NPC.direction = playerFaceDirectionFurtherPhases;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
				}
				else
				{
					base.NPC.localAI[1]++;
				}
				break;
			case 6:
				base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * spinPhaseVelocity;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (playerFaceDirectionFurtherPhases != 0)
				{
					base.NPC.direction = playerFaceDirectionFurtherPhases;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				base.NPC.ai[0] = 12f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			}
		}
		else if (base.NPC.ai[0] == 7f)
		{
			base.NPC.damage = setDamage;
			ChargeDust(7);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 8f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				SoundEngine.PlaySound(in OrbSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] % (float)flareDustSpawnDivisor == 0f && Main.netMode != 1)
			{
				int ringReduction = (int)MathHelper.Lerp(0f, 14f, base.NPC.ai[2] / (float)flareDustPhaseTimer);
				int totalProjectiles = 38 - ringReduction;
				DoFlareDustBulletHell(0, flareDustSpawnDivisor, FlareDamage, totalProjectiles, 0f, 0f, phase2: false);
			}
			if (base.NPC.ai[2] >= (float)flareDustPhaseTimer)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f - increasedIdleTimeAfterBulletHell;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 9f)
		{
			base.NPC.damage = 0;
			NPC nPC7 = base.NPC;
			nPC7.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<BigFlare>(), 0, 0f, Main.myPlayer, 1f, base.NPC.target + 1);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)fireTornadoPhaseTimer)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 10f)
		{
			base.NPC.damage = 0;
			NPC nPC8 = base.NPC;
			nPC8.velocity *= 0.9f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(newPhaseTimer - 60))
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)newPhaseTimer)
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = Main.rand.Next(5);
				base.NPC.localAI[1] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 11f)
		{
			base.NPC.damage = setDamage;
			ChargeDust(14);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 12f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] % (float)flareDustSpawnDivisor2 == 0f && Main.netMode != 1)
			{
				Vector2 projectileVelocity = base.NPC.velocity;
				((Vector2)(ref projectileVelocity)).Normalize();
				int type3 = ModContent.ProjectileType<FlareDust2>();
				float finalVelocity = 12f;
				float projectileAcceleration = 1.1f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fromMouth, projectileVelocity, type3, FlareDamage, 0f, Main.myPlayer, finalVelocity, projectileAcceleration);
			}
			base.NPC.velocity = base.NPC.velocity.RotatedBy((0.0 - (double)spinPhaseRotation) * (double)(float)base.NPC.direction);
			base.NPC.rotation -= spinPhaseRotation * (float)base.NPC.direction;
			if (base.NPC.ai[2] >= (float)flareDustPhaseTimer2)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 13f && !player.dead)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 destination4 = player.Center + new Vector2(base.NPC.ai[1], 0f);
			Vector2 desiredVelocity3 = Vector2.Normalize(destination4 - base.NPC.Center - base.NPC.velocity) * velocity;
			if (Vector2.Distance(base.NPC.Center, destination4) > reduceSpeedChargeDistance && base.NPC.localAI[1] <= (float)fastChargeTelegraphTime * 0.5f)
			{
				base.NPC.SimpleFlyMovement(desiredVelocity3, acceleration);
			}
			else
			{
				NPC nPC9 = base.NPC;
				nPC9.velocity *= 0.98f;
			}
			int playerFaceDirectionFurtherPhases2 = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (playerFaceDirectionFurtherPhases2 != 0)
			{
				if (base.NPC.ai[2] == 0f && playerFaceDirectionFurtherPhases2 != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = playerFaceDirectionFurtherPhases2;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (!((base.NPC.ai[2] >= (float)phaseSwitchTimer) | slowChargeAfterBulletHell))
			{
				return;
			}
			int aiState3 = 0;
			switch ((int)base.NPC.ai[3])
			{
			case 0:
			case 1:
				aiState3 = 1;
				break;
			case 2:
			case 3:
			case 4:
				aiState3 = 5;
				break;
			case 5:
				aiState3 = 3;
				break;
			case 6:
				aiState3 = 6;
				break;
			case 7:
				base.NPC.ai[3] = 1f;
				aiState3 = 7;
				break;
			case 8:
				aiState3 = 2;
				break;
			}
			if (phase4Check)
			{
				aiState3 = 4;
			}
			switch (aiState3)
			{
			case 1:
				base.NPC.ai[0] = 14f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeSpeed;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (playerFaceDirectionFurtherPhases2 != 0)
				{
					base.NPC.direction = playerFaceDirectionFurtherPhases2;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				break;
			case 2:
				if (base.NPC.Opacity > 0f)
				{
					base.NPC.Opacity -= 0.2f;
					if (base.NPC.Opacity < 0f)
					{
						base.NPC.Opacity = 0f;
					}
				}
				if (base.NPC.ai[2] == (float)phaseSwitchTimer + 15f)
				{
					SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						float bulletHellTeleportLocationDistance2 = 540f;
						Vector2 teleportLocation2 = -Vector2.UnitY * bulletHellTeleportLocationDistance2 * (float)(player.velocity.Y >= 0f).ToDirectionInt();
						Vector2 center2 = player.Center + teleportLocation2;
						base.NPC.Center = center2;
						int type4 = ModContent.ProjectileType<YharonBulletHellVortex>();
						int damage2 = (Main.zenithWorld ? VortexDamage : 0);
						float bulletHellVortexDuration2 = (float)flareDustPhaseTimer + teleportPhaseTimer - 15f;
						int extraTime2 = (Main.zenithWorld ? 300 : 0);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type4, damage2, 0f, Main.myPlayer, bulletHellVortexDuration2 + (float)extraTime2, base.NPC.whoAmI);
						int damageAmt2 = (int)((float)base.NPC.lifeMax * (bulletHellVortexDuration2 / (float)calamityGlobalNPC.KillTime));
						base.NPC.life -= damageAmt2;
						if (base.NPC.life < 1)
						{
							base.NPC.life = 1;
						}
						base.NPC.DamageEffect(damageAmt2);
						base.NPC.netUpdate = true;
					}
				}
				if (base.NPC.ai[2] >= (float)phaseSwitchTimer + 15f)
				{
					base.NPC.dontTakeDamage = true;
					base.NPC.velocity = Vector2.Zero;
				}
				if (!(base.NPC.ai[2] < (float)phaseSwitchTimer + teleportPhaseTimer))
				{
					base.NPC.ai[0] = 15f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				break;
			case 3:
				base.NPC.ai[0] = 16f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 4:
				base.NPC.ai[0] = 17f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 5:
				if (playFastChargeRoarSound)
				{
					RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				}
				if (doFastCharge)
				{
					base.NPC.ai[0] = 18f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.localAI[1] = 0f;
					base.NPC.netUpdate = true;
					base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeSpeed * fastChargeVelocityMultiplier;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (playerFaceDirectionFurtherPhases2 != 0)
					{
						base.NPC.direction = playerFaceDirectionFurtherPhases2;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
				}
				else
				{
					base.NPC.localAI[1]++;
				}
				break;
			case 6:
				base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * spinPhaseVelocity;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (playerFaceDirectionFurtherPhases2 != 0)
				{
					base.NPC.direction = playerFaceDirectionFurtherPhases2;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				base.NPC.ai[0] = 19f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			case 7:
				base.NPC.ai[0] = 20f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			}
		}
		else if (base.NPC.ai[0] == 14f)
		{
			base.NPC.damage = setDamage;
			ChargeDust(7);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 15f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				SoundEngine.PlaySound(in OrbSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] % (float)flareDustSpawnDivisor3 == 0f && Main.netMode != 1)
			{
				DoFlareDustBulletHell(1, flareDustPhaseTimer, FlareDamage, 8, 13f, 3.6f, phase2: false);
			}
			if (base.NPC.ai[2] >= (float)flareDustPhaseTimer)
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f - increasedIdleTimeAfterBulletHell;
				base.NPC.localAI[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 16f)
		{
			base.NPC.damage = 0;
			NPC nPC10 = base.NPC;
			nPC10.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)(fireTornadoPhaseTimer - 30))
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<BigFlare>(), 0, 0f, Main.myPlayer, 1f, base.NPC.target + 1);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)fireTornadoPhaseTimer)
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 3f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 17f)
		{
			base.NPC.damage = 0;
			NPC nPC11 = base.NPC;
			nPC11.velocity *= 0.9f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(newPhaseTimer - 60))
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)newPhaseTimer)
			{
				startSecondAI = true;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 18f)
		{
			base.NPC.damage = setDamage;
			ChargeDust(14);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 19f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] % (float)flareDustSpawnDivisor2 == 0f && Main.netMode != 1)
			{
				Vector2 projectileVelocity2 = base.NPC.velocity;
				((Vector2)(ref projectileVelocity2)).Normalize();
				int type5 = ModContent.ProjectileType<FlareDust2>();
				float finalVelocity2 = 15f;
				float projectileAcceleration2 = 1.11f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fromMouth, projectileVelocity2, type5, FlareDamage, 0f, Main.myPlayer, finalVelocity2, projectileAcceleration2);
			}
			base.NPC.velocity = base.NPC.velocity.RotatedBy((0.0 - (double)spinPhaseRotation) * (double)(float)base.NPC.direction);
			base.NPC.rotation -= spinPhaseRotation * (float)base.NPC.direction;
			if (base.NPC.ai[2] >= (float)(flareDustPhaseTimer2 - 50))
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3]++;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 20f)
			{
				return;
			}
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 destination5 = player.Center + new Vector2(base.NPC.ai[1], 0f);
			Vector2 flareSpeed2 = Vector2.Normalize(destination5 - base.NPC.Center - base.NPC.velocity) * flareBombPhaseVelocity;
			if (Vector2.Distance(base.NPC.Center, destination5) > reduceSpeedFlareBombDistance)
			{
				base.NPC.SimpleFlyMovement(flareSpeed2, flareBombPhaseAcceleration);
			}
			else
			{
				NPC nPC12 = base.NPC;
				nPC12.velocity *= 0.98f;
			}
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			if (base.NPC.ai[2] % (float)flareBombSpawnDivisor == 0f && Main.netMode != 1)
			{
				int type6 = ModContent.ProjectileType<FlareBomb>();
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fromMouth, Vector2.Zero, type6, FlareDamage, 0f, Main.myPlayer, base.NPC.target, 1f);
			}
			int playerFaceDirection2 = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (playerFaceDirection2 != 0)
			{
				base.NPC.direction = playerFaceDirection2;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)(flareBombPhaseTimer - 15))
			{
				base.NPC.ai[0] = 13f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
	}

	public void Yharon_AI2(bool expertMode, bool revenge, bool death, float lifeRatio, CalamityGlobalNPC calamityGlobalNPC, int contactDamage)
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_268c: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13de: Unknown result type (might be due to invalid IL or missing references)
		//IL_131c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1327: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_172a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1736: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_142d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1432: Unknown result type (might be due to invalid IL or missing references)
		//IL_1437: Unknown result type (might be due to invalid IL or missing references)
		//IL_1439: Unknown result type (might be due to invalid IL or missing references)
		//IL_1441: Unknown result type (might be due to invalid IL or missing references)
		//IL_1446: Unknown result type (might be due to invalid IL or missing references)
		//IL_1451: Unknown result type (might be due to invalid IL or missing references)
		//IL_1456: Unknown result type (might be due to invalid IL or missing references)
		//IL_145b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1462: Unknown result type (might be due to invalid IL or missing references)
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_176e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1781: Unknown result type (might be due to invalid IL or missing references)
		//IL_1786: Unknown result type (might be due to invalid IL or missing references)
		//IL_178b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1793: Unknown result type (might be due to invalid IL or missing references)
		//IL_179f: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17be: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1516: Unknown result type (might be due to invalid IL or missing references)
		//IL_151b: Unknown result type (might be due to invalid IL or missing references)
		//IL_152c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1536: Unknown result type (might be due to invalid IL or missing references)
		//IL_153b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1543: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1473: Unknown result type (might be due to invalid IL or missing references)
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_219d: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df8: Unknown result type (might be due to invalid IL or missing references)
		//IL_195b: Unknown result type (might be due to invalid IL or missing references)
		//IL_196a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1996: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1862: Unknown result type (might be due to invalid IL or missing references)
		//IL_1867: Unknown result type (might be due to invalid IL or missing references)
		//IL_1878: Unknown result type (might be due to invalid IL or missing references)
		//IL_1882: Unknown result type (might be due to invalid IL or missing references)
		//IL_1887: Unknown result type (might be due to invalid IL or missing references)
		//IL_1899: Unknown result type (might be due to invalid IL or missing references)
		//IL_189b: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18be: Unknown result type (might be due to invalid IL or missing references)
		//IL_149b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1489: Unknown result type (might be due to invalid IL or missing references)
		//IL_249e: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_24bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24db: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22af: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1810: Unknown result type (might be due to invalid IL or missing references)
		//IL_1818: Unknown result type (might be due to invalid IL or missing references)
		//IL_1560: Unknown result type (might be due to invalid IL or missing references)
		//IL_2302: Unknown result type (might be due to invalid IL or missing references)
		//IL_230d: Unknown result type (might be due to invalid IL or missing references)
		//IL_18db: Unknown result type (might be due to invalid IL or missing references)
		//IL_1597: Unknown result type (might be due to invalid IL or missing references)
		//IL_159b: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1912: Unknown result type (might be due to invalid IL or missing references)
		//IL_1916: Unknown result type (might be due to invalid IL or missing references)
		//IL_191b: Unknown result type (might be due to invalid IL or missing references)
		//IL_192b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1936: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_203b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2046: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1621: Unknown result type (might be due to invalid IL or missing references)
		//IL_1640: Unknown result type (might be due to invalid IL or missing references)
		//IL_1653: Unknown result type (might be due to invalid IL or missing references)
		//IL_1659: Unknown result type (might be due to invalid IL or missing references)
		//IL_165b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1660: Unknown result type (might be due to invalid IL or missing references)
		//IL_1665: Unknown result type (might be due to invalid IL or missing references)
		//IL_166d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1672: Unknown result type (might be due to invalid IL or missing references)
		//IL_168e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_2360: Unknown result type (might be due to invalid IL or missing references)
		//IL_2367: Unknown result type (might be due to invalid IL or missing references)
		//IL_236c: Unknown result type (might be due to invalid IL or missing references)
		//IL_20db: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_23de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_1116: Unknown result type (might be due to invalid IL or missing references)
		//IL_1120: Unknown result type (might be due to invalid IL or missing references)
		//IL_1125: Unknown result type (might be due to invalid IL or missing references)
		//IL_112d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1198: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1227: Unknown result type (might be due to invalid IL or missing references)
		//IL_122c: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1243: Unknown result type (might be due to invalid IL or missing references)
		//IL_1254: Unknown result type (might be due to invalid IL or missing references)
		//IL_125e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1263: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Unknown result type (might be due to invalid IL or missing references)
		//IL_114a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1288: Unknown result type (might be due to invalid IL or missing references)
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_1185: Unknown result type (might be due to invalid IL or missing references)
		//IL_118a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.yharonP2 = base.NPC.whoAmI;
		float phase2GateValue = (revenge ? 0.45f : (expertMode ? 0.35f : 0.3f));
		bool phase2 = death || lifeRatio <= phase2GateValue;
		bool phase3 = lifeRatio <= (death ? 0.35f : (revenge ? 0.3f : (expertMode ? 0.2f : 0.15f)));
		bool phase4 = (lifeRatio <= (death ? 0.2f : 0.15f)) & revenge;
		if (base.NPC.ai[0] != 5f && base.NPC.ai[0] != 8f)
		{
			base.NPC.Opacity += 0.1f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
		}
		if (!moveCloser)
		{
			moveCloser = true;
			Color messageColor = Color.Orange;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.FlameText", messageColor);
		}
		int setDamage = contactDamage;
		base.NPC.dontTakeDamage = false;
		if (invincibilityCounter < 300)
		{
			if (Main.zenithWorld)
			{
				if (base.NPC.life < base.NPC.lifeMax)
				{
					base.NPC.life += (int)((float)base.NPC.lifeMax * 0.01f);
					base.NPC.HealEffect((int)((float)base.NPC.lifeMax * 0.01f));
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.life = base.NPC.lifeMax;
				}
			}
			base.NPC.dontTakeDamage = true;
			phase2 = (phase3 = (phase4 = false));
			invincibilityCounter++;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
			base.NPC.netUpdate = true;
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
			base.NPC.netUpdate = true;
		}
		Player targetData = Main.player[base.NPC.target];
		bool targetDead = false;
		if (targetData.dead || !targetData.active)
		{
			base.NPC.TargetClosest();
			base.NPC.netUpdate = true;
			targetData = Main.player[base.NPC.target];
			if (targetData.dead || !targetData.active)
			{
				targetDead = true;
				base.NPC.velocity.Y -= 0.4f;
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		Rectangle hitbox = targetData.Hitbox;
		enraged = !((Rectangle)(ref hitbox)).Intersects(safeBox);
		if (enraged)
		{
			protectionBoost = true;
			setDamage *= 5;
		}
		else
		{
			protectionBoost = false;
		}
		bool bulletHell = base.NPC.ai[0] == 5f;
		base.NPC.dontTakeDamage = bulletHell;
		calamityGlobalNPC.DR = (protectionBoost ? 0.9f : 0f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = protectionBoost;
		if (!protectionBoost)
		{
			switch (secondPhasePhase)
			{
			case 1:
				calamityGlobalNPC.DR = (phase2 ? 0.7f : 0f);
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phase2;
				break;
			case 2:
				calamityGlobalNPC.DR = (phase3 ? 0.7f : 0f);
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phase3;
				break;
			case 3:
				calamityGlobalNPC.DR = (phase4 ? 0.7f : 0f);
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phase4;
				break;
			}
			if (base.NPC.ai[0] == 9f)
			{
				calamityGlobalNPC.DR = 0.7f;
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = true;
			}
		}
		float reduceSpeedChargeDistance = 500f;
		float reduceSpeedFireballSpitChargeDistance = 800f;
		float phaseSwitchTimer = (expertMode ? 30f : 32f);
		float acceleration = (expertMode ? 0.92f : 0.9f);
		float velocity = (expertMode ? 14.5f : 14f);
		float chargeTime = (expertMode ? 32f : 35f);
		float chargeSpeed = (expertMode ? 32f : 30f);
		float fastChargeVelocityMultiplier = 1.5f;
		fastChargeTelegraphTime = (protectionBoost ? 60 : (100 - secondPhasePhase * 10));
		bool playFastChargeRoarSound = base.NPC.localAI[1] == (float)fastChargeTelegraphTime * 0.5f;
		bool doFastChargeTelegraph = base.NPC.localAI[1] <= (float)fastChargeTelegraphTime;
		float fireballBreathTimer = 60f;
		float fireballBreathPhaseTimer = fireballBreathTimer + 80f;
		float fireballBreathPhaseVelocity = (expertMode ? 28f : 25f);
		float splittingFireballBreathTimer = 40f;
		float splittingFireballBreathPhaseVelocity = 22f;
		int splittingFireballBreathDivisor = 10;
		int splittingFireballBreathTimer2 = 10 * splittingFireballBreathDivisor;
		float splittingFireballBreathYVelocityTimer = 40f;
		float splittingFireballBreathPhaseTimer = splittingFireballBreathTimer + (float)splittingFireballBreathTimer2 + splittingFireballBreathYVelocityTimer;
		int spinPhaseTimer = ((secondPhasePhase != 4) ? (death ? 150 : 180) : (death ? 100 : 120));
		int flareDustSpawnDivisor = spinPhaseTimer / 10;
		int flareDustSpawnDivisor2 = spinPhaseTimer / 15 + ((secondPhasePhase == 4) ? (spinPhaseTimer / 60) : 0);
		float increasedIdleTimeAfterBulletHell = (death ? 120f : 180f) + phaseSwitchTimer;
		bool num = base.NPC.ai[1] < 0f;
		bool slowChargeAfterBulletHell = base.NPC.ai[1] == -1f;
		if (num)
		{
			float reducedMovementMultiplier = MathHelper.Lerp(0.1f, death ? 1f : 0.75f, (base.NPC.ai[1] + increasedIdleTimeAfterBulletHell) / increasedIdleTimeAfterBulletHell);
			acceleration *= reducedMovementMultiplier;
			velocity *= reducedMovementMultiplier;
			chargeSpeed *= reducedMovementMultiplier;
		}
		float flareSpawnDecelerationTimer = (death ? 75f : 90f);
		int flareSpawnPhaseTimerReduction = (revenge ? ((int)(flareSpawnDecelerationTimer * (0.55f - lifeRatio))) : 0);
		float flareSpawnPhaseTimer = (death ? 150f : 180f) - (float)flareSpawnPhaseTimerReduction;
		float teleportPhaseTimer = 45f;
		if (revenge)
		{
			float chargeTimeDecrease = (death ? 4f : 2f);
			float velocityMult = (death ? 1.1f : 1.05f);
			acceleration *= velocityMult;
			velocity *= velocityMult;
			chargeTime -= chargeTimeDecrease;
			chargeSpeed *= velocityMult;
		}
		if (Main.getGoodWorld)
		{
			phaseSwitchTimer *= 0.5f;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 10f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 1f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[2] = ((base.NPC.Center.X < targetData.Center.X) ? 1 : (-1));
			}
			Vector2 destination = targetData.Center + new Vector2(0f - base.NPC.ai[2], 0f);
			Vector2 desiredVelocity = base.NPC.SafeDirectionTo(destination) * velocity;
			if (!targetDead)
			{
				if (Vector2.Distance(base.NPC.Center, destination) > reduceSpeedChargeDistance && base.NPC.localAI[1] <= (float)fastChargeTelegraphTime * 0.5f)
				{
					base.NPC.SimpleFlyMovement(desiredVelocity, acceleration);
				}
				else
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.98f;
				}
			}
			int spriteDirection = ((base.NPC.Center.X < targetData.Center.X) ? 1 : (-1));
			base.NPC.direction = (base.NPC.spriteDirection = spriteDirection);
			base.NPC.ai[1]++;
			if ((base.NPC.ai[1] >= phaseSwitchTimer) | slowChargeAfterBulletHell)
			{
				int phase2AttackType = 1;
				if (phase4)
				{
					switch ((int)base.NPC.ai[3])
					{
					case 0:
						phase2AttackType = 8;
						break;
					case 1:
					case 2:
						phase2AttackType = 7;
						break;
					case 3:
						phase2AttackType = 5;
						break;
					}
				}
				else if (phase3)
				{
					switch ((int)base.NPC.ai[3])
					{
					case 0:
						phase2AttackType = 6;
						break;
					case 1:
						phase2AttackType = 7;
						break;
					case 2:
						phase2AttackType = 8;
						break;
					case 3:
						phase2AttackType = 7;
						break;
					case 4:
						phase2AttackType = 5;
						break;
					case 5:
						phase2AttackType = (Main.rand.NextBool() ? 3 : 4);
						break;
					case 6:
						phase2AttackType = 7;
						break;
					case 7:
						phase2AttackType = 8;
						break;
					case 8:
						phase2AttackType = 7;
						break;
					case 9:
						phase2AttackType = (Main.rand.NextBool() ? 4 : 3);
						break;
					case 10:
						phase2AttackType = 6;
						break;
					case 11:
						phase2AttackType = 7;
						break;
					case 12:
						phase2AttackType = 8;
						break;
					case 13:
						phase2AttackType = 7;
						break;
					case 14:
						phase2AttackType = 5;
						break;
					case 15:
						phase2AttackType = (Main.rand.NextBool() ? 3 : 4);
						break;
					}
				}
				else if (phase2)
				{
					switch ((int)base.NPC.ai[3])
					{
					case 0:
						phase2AttackType = 6;
						break;
					case 1:
						phase2AttackType = 7;
						break;
					case 2:
						phase2AttackType = 2;
						break;
					case 3:
						phase2AttackType = 5;
						break;
					case 4:
						phase2AttackType = (Main.rand.NextBool() ? 3 : 4);
						break;
					case 5:
						phase2AttackType = 7;
						break;
					case 6:
						phase2AttackType = 2;
						break;
					case 7:
						phase2AttackType = (Main.rand.NextBool() ? 4 : 3);
						break;
					case 8:
						phase2AttackType = 7;
						break;
					case 9:
						phase2AttackType = 2;
						break;
					case 10:
						phase2AttackType = 5;
						break;
					}
				}
				else
				{
					switch ((int)base.NPC.ai[3])
					{
					case 0:
						phase2AttackType = 6;
						break;
					case 1:
					case 2:
						phase2AttackType = 2;
						break;
					case 3:
						phase2AttackType = (Main.rand.NextBool() ? 3 : 4);
						break;
					case 4:
					case 5:
						phase2AttackType = 7;
						break;
					case 6:
						phase2AttackType = (Main.rand.NextBool() ? 4 : 3);
						break;
					case 7:
					case 8:
						phase2AttackType = 2;
						break;
					case 9:
						phase2AttackType = 5;
						break;
					}
				}
				if (phase2AttackType == 5 && base.NPC.ai[1] < phaseSwitchTimer + teleportPhaseTimer)
				{
					float newRotation = base.NPC.AngleTo(targetData.Center);
					float amount = 0.04f;
					if (base.NPC.spriteDirection == -1)
					{
						newRotation += (float)Math.PI;
					}
					if (amount != 0f)
					{
						base.NPC.rotation = base.NPC.rotation.AngleTowards(newRotation, amount);
					}
					if (base.NPC.Opacity > 0f)
					{
						base.NPC.Opacity -= 0.2f;
						if (base.NPC.Opacity < 0f)
						{
							base.NPC.Opacity = 0f;
						}
					}
					float timeBeforeTeleport = teleportPhaseTimer - 15f;
					if (base.NPC.ai[1] == phaseSwitchTimer + timeBeforeTeleport)
					{
						SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							float bulletHellTeleportLocationDistance = 540f;
							Vector2 teleportLocation = -Vector2.UnitY * bulletHellTeleportLocationDistance * (float)(targetData.velocity.Y >= 0f).ToDirectionInt();
							Vector2 center = targetData.Center + teleportLocation;
							base.NPC.Center = center;
							int type = ModContent.ProjectileType<YharonBulletHellVortex>();
							int damage = (Main.zenithWorld ? VortexDamage : 0);
							float bulletHellVortexDuration = (float)spinPhaseTimer + 15f;
							int extraTime = (Main.zenithWorld ? 300 : 0);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type, damage, 0f, Main.myPlayer, bulletHellVortexDuration + (float)extraTime, base.NPC.whoAmI);
							int damageAmt = (int)((float)base.NPC.lifeMax * (bulletHellVortexDuration / (float)calamityGlobalNPC.KillTime));
							base.NPC.life -= damageAmt;
							if (base.NPC.life < 1)
							{
								base.NPC.life = 1;
							}
							base.NPC.DamageEffect(damageAmt);
							base.NPC.netUpdate = true;
						}
					}
					if (base.NPC.ai[1] >= phaseSwitchTimer + timeBeforeTeleport)
					{
						base.NPC.dontTakeDamage = true;
						base.NPC.velocity = Vector2.Zero;
					}
					return;
				}
				if ((phase2AttackType == 7) & doFastChargeTelegraph)
				{
					float newRotation2 = base.NPC.AngleTo(targetData.Center);
					float amount2 = 0.04f;
					if (base.NPC.spriteDirection == -1)
					{
						newRotation2 += (float)Math.PI;
					}
					if (amount2 != 0f)
					{
						base.NPC.rotation = base.NPC.rotation.AngleTowards(newRotation2, amount2);
					}
					if (playFastChargeRoarSound)
					{
						RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
					}
					base.NPC.localAI[1]++;
					return;
				}
				base.NPC.ai[0] = phase2AttackType;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3]++;
				base.NPC.localAI[1] = 0f;
				switch (secondPhasePhase)
				{
				case 1:
					if (phase2)
					{
						secondPhasePhase = 2;
						base.NPC.ai[0] = 9f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = Main.rand.Next(11);
					}
					break;
				case 2:
					if (phase3)
					{
						secondPhasePhase = 3;
						base.NPC.ai[0] = 9f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = Main.rand.Next(16);
					}
					break;
				case 3:
					if (phase4)
					{
						secondPhasePhase = 4;
						base.NPC.ai[0] = 9f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
					}
					break;
				}
				base.NPC.netUpdate = true;
				float aiLimit = 10f;
				if (phase4)
				{
					aiLimit = 4f;
				}
				else if (phase3)
				{
					aiLimit = 16f;
				}
				else if (phase2)
				{
					aiLimit = 11f;
				}
				if (base.NPC.ai[3] >= aiLimit)
				{
					base.NPC.ai[3] = 0f;
				}
				switch (phase2AttackType)
				{
				case 2:
				{
					Vector2 vector2 = base.NPC.SafeDirectionTo(targetData.Center, Vector2.UnitX * (float)base.NPC.spriteDirection);
					base.NPC.spriteDirection = ((vector2.X > 0f) ? 1 : (-1));
					base.NPC.rotation = vector2.ToRotation();
					if (base.NPC.spriteDirection == -1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.velocity = vector2 * chargeSpeed;
					break;
				}
				case 3:
				{
					Vector2 fireSpitFaceDirection = default(Vector2);
					((Vector2)(ref fireSpitFaceDirection))._002Ector((float)((targetData.Center.X > base.NPC.Center.X) ? 1 : (-1)), 0f);
					base.NPC.spriteDirection = ((fireSpitFaceDirection.X > 0f) ? 1 : (-1));
					base.NPC.velocity = fireSpitFaceDirection * -2f;
					break;
				}
				case 5:
					base.NPC.dontTakeDamage = true;
					base.NPC.localAI[3] = Main.rand.Next(2);
					base.NPC.velocity = Vector2.Zero;
					break;
				case 7:
				{
					Vector2 vector = base.NPC.SafeDirectionTo(targetData.Center, Vector2.UnitX * (float)base.NPC.spriteDirection);
					base.NPC.spriteDirection = ((vector.X > 0f) ? 1 : (-1));
					base.NPC.rotation = vector.ToRotation();
					if (base.NPC.spriteDirection == -1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.velocity = vector * chargeSpeed * fastChargeVelocityMultiplier;
					break;
				}
				}
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = setDamage;
			if (base.NPC.ai[1] == 1f)
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
			}
			ChargeDust(7);
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= chargeTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			int fireballFaceDirection = ((base.NPC.Center.X < targetData.Center.X) ? 1 : (-1));
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] < fireballBreathTimer)
			{
				Vector2 destination2 = targetData.Center + new Vector2((float)fireballFaceDirection, 0f);
				Vector2 desiredVelocity2 = Vector2.Normalize(destination2 - base.NPC.Center - base.NPC.velocity) * velocity;
				if (!targetDead)
				{
					if (Vector2.Distance(base.NPC.Center, destination2) > reduceSpeedFireballSpitChargeDistance)
					{
						base.NPC.SimpleFlyMovement(desiredVelocity2, acceleration);
					}
					else
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.98f;
					}
				}
				base.NPC.direction = (base.NPC.spriteDirection = fireballFaceDirection);
				if (Vector2.Distance(destination2, base.NPC.Center) < 32f)
				{
					base.NPC.ai[1] = fireballBreathTimer - 1f;
				}
			}
			if (base.NPC.ai[1] == fireballBreathTimer)
			{
				Vector2 vector3 = base.NPC.SafeDirectionTo(targetData.Center, Vector2.UnitX * (float)base.NPC.spriteDirection);
				base.NPC.spriteDirection = ((vector3.X > 0f) ? 1 : (-1));
				base.NPC.rotation = vector3.ToRotation();
				if (base.NPC.spriteDirection == -1)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.velocity = vector3 * fireballBreathPhaseVelocity;
				SoundEngine.PlaySound(in FireSound, base.NPC.Center);
			}
			if (base.NPC.ai[1] >= fireballBreathTimer)
			{
				base.NPC.damage = setDamage;
				if (base.NPC.ai[1] % (expertMode ? 6f : 8f) == 0f && Main.netMode != 1)
				{
					float xOffset = 30f;
					Vector2 position = base.NPC.Center + Utils.RotatedBy(new Vector2((110f + xOffset) * (float)base.NPC.direction, -20f), (double)base.NPC.rotation, default(Vector2));
					Vector2 projectileVelocity = base.NPC.velocity;
					((Vector2)(ref projectileVelocity)).Normalize();
					int type2 = ModContent.ProjectileType<FlareDust2>();
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), position, projectileVelocity, type2, FlareDamage, 0f, Main.myPlayer);
				}
			}
			if (base.NPC.ai[1] >= fireballBreathPhaseTimer)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.TargetClosest();
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			int splitFireFaceDirection = ((base.NPC.Center.X < targetData.Center.X) ? 1 : (-1));
			base.NPC.ai[2] = splitFireFaceDirection;
			if (base.NPC.ai[1] < splittingFireballBreathTimer)
			{
				Vector2 splitFireDestination = targetData.Center + new Vector2((float)splitFireFaceDirection * -750f, -300f);
				Vector2 splitFireFinalVelocity = base.NPC.SafeDirectionTo(splitFireDestination) * splittingFireballBreathPhaseVelocity;
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, splitFireFinalVelocity, 1f / 30f);
				int direction = ((base.NPC.Center.X < targetData.Center.X) ? 1 : (-1));
				base.NPC.direction = (base.NPC.spriteDirection = direction);
				if (Vector2.Distance(splitFireDestination, base.NPC.Center) < 32f)
				{
					base.NPC.ai[1] = splittingFireballBreathTimer - 1f;
				}
			}
			else if (base.NPC.ai[1] == splittingFireballBreathTimer)
			{
				Vector2 yharonFireballMoveDirection = base.NPC.SafeDirectionTo(targetData.Center, Vector2.UnitX * (float)base.NPC.spriteDirection);
				yharonFireballMoveDirection.Y *= 0.15f;
				yharonFireballMoveDirection = yharonFireballMoveDirection.SafeNormalize(Vector2.UnitX * (float)base.NPC.direction);
				base.NPC.spriteDirection = ((yharonFireballMoveDirection.X > 0f) ? 1 : (-1));
				base.NPC.rotation = yharonFireballMoveDirection.ToRotation();
				if (base.NPC.spriteDirection == -1)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.velocity = yharonFireballMoveDirection * splittingFireballBreathPhaseVelocity;
				SoundEngine.PlaySound(in FireSound, base.NPC.Center);
			}
			else
			{
				base.NPC.position.X += base.NPC.SafeDirectionTo(targetData.Center).X * 7f;
				base.NPC.position.Y += base.NPC.SafeDirectionTo(targetData.Center + new Vector2(0f, -400f)).Y * 6f;
				float xOffset2 = 30f;
				Vector2 position2 = base.NPC.Center + Utils.RotatedBy(new Vector2((110f + xOffset2) * (float)base.NPC.direction, -20f), (double)base.NPC.rotation, default(Vector2));
				int yharonFireballTimer = (int)(base.NPC.ai[1] - splittingFireballBreathTimer + 1f);
				int type3 = ModContent.ProjectileType<YharonFireball>();
				if (yharonFireballTimer <= splittingFireballBreathTimer2 && yharonFireballTimer % splittingFireballBreathDivisor == 0 && Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), position2, base.NPC.velocity, type3, FireballDamage, 0f, Main.myPlayer);
				}
			}
			if (base.NPC.ai[1] > splittingFireballBreathPhaseTimer - splittingFireballBreathYVelocityTimer)
			{
				base.NPC.velocity.Y -= 0.1f;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= splittingFireballBreathPhaseTimer)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 1f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				SoundEngine.PlaySound(in OrbSound, base.NPC.Center);
			}
			base.NPC.ai[1]++;
			if (Main.netMode != 1)
			{
				if (secondPhasePhase >= 3)
				{
					if (base.NPC.ai[1] % (float)flareDustSpawnDivisor2 == 0f)
					{
						int totalProjectiles = ((secondPhasePhase == 4) ? 12 : 10);
						float projectileVelocity2 = ((secondPhasePhase == 4) ? 17f : 13f);
						float radialOffset = ((secondPhasePhase == 4) ? 2.8f : 3.2f);
						if (base.NPC.localAI[3] == 0f)
						{
							DoFlareDustBulletHell(1, spinPhaseTimer, FlareDamage, totalProjectiles, projectileVelocity2, radialOffset, phase2: true);
						}
						else
						{
							int ringReduction = (int)MathHelper.Lerp(0f, 12f, base.NPC.ai[1] / (float)spinPhaseTimer);
							int totalProjectiles2 = 38 - ringReduction;
							DoFlareDustBulletHell(0, flareDustSpawnDivisor2, FlareDamage, totalProjectiles2, 0f, 0f, phase2: true);
						}
					}
				}
				else if (base.NPC.ai[1] % (float)flareDustSpawnDivisor == 0f)
				{
					int ringReduction2 = (int)MathHelper.Lerp(0f, 12f, base.NPC.ai[1] / (float)spinPhaseTimer);
					int totalProjectiles3 = 38 - ringReduction2;
					DoFlareDustBulletHell(0, flareDustSpawnDivisor, FlareDamage, totalProjectiles3, 0f, 0f, phase2: true);
				}
				if (base.NPC.ai[1] == 210f && secondPhasePhase == 4 && useTornado)
				{
					useTornado = false;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<BigFlare2>(), 0, 0f, Main.myPlayer, 1f, base.NPC.target + 1);
				}
			}
			if (base.NPC.ai[1] >= (float)spinPhaseTimer)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f - increasedIdleTimeAfterBulletHell;
				base.NPC.ai[2] = 0f;
				base.NPC.localAI[2] = 0f;
				base.NPC.TargetClosest();
				NPC nPC3 = base.NPC;
				nPC3.velocity /= 2f;
			}
		}
		else if (base.NPC.ai[0] == 6f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				Vector2 destination3 = targetData.Center + new Vector2(0f, -200f);
				Vector2 desiredVelocity3 = base.NPC.SafeDirectionTo(destination3) * velocity * 1.5f;
				base.NPC.SimpleFlyMovement(desiredVelocity3, acceleration * 1.5f);
				int flareRingFaceDirection = ((base.NPC.Center.X < targetData.Center.X) ? 1 : (-1));
				base.NPC.direction = (base.NPC.spriteDirection = flareRingFaceDirection);
				base.NPC.ai[2]++;
				if (base.NPC.Distance(targetData.Center) < 600f || base.NPC.ai[2] >= 180f)
				{
					base.NPC.ai[1] = 1f;
					base.NPC.netUpdate = true;
				}
			}
			else
			{
				if (base.NPC.ai[1] < flareSpawnDecelerationTimer)
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.95f;
				}
				else
				{
					NPC nPC5 = base.NPC;
					nPC5.velocity *= 0.98f;
				}
				if (base.NPC.ai[1] == flareSpawnDecelerationTimer)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y /= 3f;
					}
					base.NPC.velocity.Y -= 3f;
				}
				if (Main.netMode != 1 && (base.NPC.ai[1] == 20f || base.NPC.ai[1] == 80f || base.NPC.ai[1] == 140f))
				{
					SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
					DoFireRing(expertMode ? 300 : 180, FlareDamage, base.NPC.target, 1f);
				}
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= flareSpawnPhaseTimer)
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<BigFlare2>(), 0, 0f, Main.myPlayer, 1f, base.NPC.target + 1);
				}
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
			}
		}
		else if (base.NPC.ai[0] == 7f)
		{
			base.NPC.damage = setDamage;
			if (base.NPC.ai[1] == 1f)
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
			}
			ChargeDust(14);
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= chargeTime)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
			}
		}
		else if (base.NPC.ai[0] == 8f)
		{
			base.NPC.damage = 0;
			if (base.NPC.Opacity > 0f)
			{
				base.NPC.Opacity -= 0.1f;
				if (base.NPC.Opacity < 0f)
				{
					base.NPC.Opacity = 0f;
				}
			}
			NPC nPC6 = base.NPC;
			nPC6.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == 15f)
			{
				SoundEngine.PlaySound(in ShortRoarSound, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == 15f)
			{
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = 450 * Math.Sign((base.NPC.Center - targetData.Center).X);
				}
				this.teleportLocation = ((!Main.rand.NextBool()) ? (revenge ? (-500) : (-600)) : (revenge ? 500 : 600));
				Vector2 center2 = targetData.Center + new Vector2(0f - base.NPC.ai[1], (float)this.teleportLocation);
				base.NPC.Center = center2;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= teleportPhaseTimer)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.localAI[1] = (float)fastChargeTelegraphTime + 1f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 9f)
		{
			base.NPC.damage = 0;
			NPC nPC7 = base.NPC;
			nPC7.velocity *= 0.9f;
			Vector2 vector4 = base.NPC.SafeDirectionTo(targetData.Center, -Vector2.UnitY);
			base.NPC.spriteDirection = ((vector4.X > 0f) ? 1 : (-1));
			base.NPC.rotation = vector4.ToRotation();
			if (base.NPC.spriteDirection == -1)
			{
				base.NPC.rotation += (float)Math.PI;
			}
			if (base.NPC.ai[2] == 120f)
			{
				if (secondPhasePhase == 4)
				{
					for (int x = 0; x < Main.maxProjectiles; x++)
					{
						Projectile projectile = Main.projectile[x];
						if (!projectile.active)
						{
							continue;
						}
						if (projectile.type == ModContent.ProjectileType<Infernado2>())
						{
							if (projectile.timeLeft >= 300)
							{
								projectile.active = false;
							}
							else if (projectile.timeLeft > 5)
							{
								projectile.timeLeft = (int)(5f * projectile.ai[1]);
							}
						}
						else if (projectile.type == ModContent.ProjectileType<BigFlare2>())
						{
							projectile.active = false;
						}
					}
				}
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 180f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		float facingAngle = base.NPC.AngleTo(targetData.Center);
		float rotationSpeed = 0.04f;
		switch ((int)base.NPC.ai[0])
		{
		case 2:
		case 7:
		case 8:
		case 9:
			rotationSpeed = 0f;
			break;
		case 3:
			if (base.NPC.ai[1] >= fireballBreathTimer)
			{
				rotationSpeed = 0f;
			}
			break;
		case 4:
			rotationSpeed = 0.01f;
			facingAngle = (float)Math.PI;
			if (base.NPC.spriteDirection == 1)
			{
				facingAngle += (float)Math.PI;
			}
			break;
		case 6:
			rotationSpeed = 0.02f;
			facingAngle = 0f;
			if (base.NPC.spriteDirection == -1)
			{
				facingAngle -= (float)Math.PI;
			}
			break;
		}
		if (base.NPC.spriteDirection == -1)
		{
			facingAngle += (float)Math.PI;
		}
		if (rotationSpeed != 0f)
		{
			base.NPC.rotation = base.NPC.rotation.AngleTowards(facingAngle, rotationSpeed);
		}
	}

	private void ChargeDust(int dustAmt)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < dustAmt; i++)
		{
			Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * (float)Math.PI / (float)dustAmt) + base.NPC.Center;
			Vector2 dustVel = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
			int chargeDust = Dust.NewDust(val + dustVel, 0, 0, 244, dustVel.X * 2f, dustVel.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[chargeDust].noGravity = true;
			Main.dust[chargeDust].noLight = true;
			Dust obj = Main.dust[chargeDust];
			obj.velocity /= 4f;
			Dust obj2 = Main.dust[chargeDust];
			obj2.velocity -= base.NPC.velocity;
		}
	}

	private void DoFlareDustBulletHell(int attackType, int timer, int projectileDamage, int totalProjectiles, float projectileVelocity, float radialOffset, bool phase2)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item20, base.NPC.Center);
		float aiVariableUsed = (phase2 ? base.NPC.ai[1] : base.NPC.ai[2]);
		switch (attackType)
		{
		case 0:
		{
			float offsetAngle = 360 / totalProjectiles;
			int totalSpaces = totalProjectiles / 5;
			int spaceStart = Main.rand.Next(totalProjectiles - totalSpaces);
			float ai0 = ((aiVariableUsed % (float)(timer * 2) == 0f) ? 1f : 0f);
			int spacesMade = 0;
			for (int j = 0; j < totalProjectiles; j++)
			{
				if (j >= spaceStart && spacesMade < totalSpaces)
				{
					spacesMade++;
				}
				else
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<FlareDust>(), projectileDamage, 0f, Main.myPlayer, ai0, (float)j * offsetAngle);
				}
			}
			break;
		}
		case 1:
		{
			double radians = (float)Math.PI * 2f / (float)totalProjectiles;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(0f - base.NPC.localAI[2], 0f - projectileVelocity));
			for (int i = 0; i < totalProjectiles; i++)
			{
				Vector2 fireSpitFaceDirection = spinningPoint.RotatedBy(radians * (double)i) * projectileVelocity;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, fireSpitFaceDirection, ModContent.ProjectileType<FlareDust>(), projectileDamage, 0f, Main.myPlayer, 2f);
			}
			float newRadialOffset = (((float)((int)aiVariableUsed / (timer / 4)) % 2f == 0f) ? radialOffset : (0f - radialOffset));
			base.NPC.localAI[2] += newRadialOffset;
			break;
		}
		}
	}

	public void DoFireRing(int timeLeft, int damage, float ai0, float ai1)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1)
		{
			float velocity = ((ai1 == 0f) ? 10f : 5f);
			int totalProjectiles = 50;
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			for (int i = 0; i < totalProjectiles; i++)
			{
				Vector2 flareRotationAmt = Utils.RotatedBy(new Vector2(0f, 0f - velocity), (double)(radians * (float)i), default(Vector2));
				int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, flareRotationAmt, ModContent.ProjectileType<FlareBomb>(), damage, 0f, Main.myPlayer, ai0, ai1);
				Main.projectile[proj].timeLeft = timeLeft;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0822: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_101b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_103a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_106e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1076: Unknown result type (might be due to invalid IL or missing references)
		//IL_1080: Unknown result type (might be due to invalid IL or missing references)
		//IL_108d: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa2: Unknown result type (might be due to invalid IL or missing references)
		bool num = (!startSecondAI && (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 13f)) || (startSecondAI && (base.NPC.ai[0] == 5f || base.NPC.ai[0] < 2f));
		bool chargingOrSpawnPhases = (!startSecondAI && (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 5f || base.NPC.ai[0] == 7f || base.NPC.ai[0] == 11f || base.NPC.ai[0] == 14f || base.NPC.ai[0] == 18f)) || (startSecondAI && (base.NPC.ai[0] == 6f || base.NPC.ai[0] == 2f || base.NPC.ai[0] == 3f || base.NPC.ai[0] == 7f));
		bool tornadoPhase = !startSecondAI && (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 9f || base.NPC.ai[0] == -1f || base.NPC.ai[0] == 16f);
		bool newPhasePhase = (!startSecondAI && (base.NPC.ai[0] == 4f || base.NPC.ai[0] == 10f || base.NPC.ai[0] == 17f)) || (startSecondAI && base.NPC.ai[0] == 9f);
		bool pauseAfterTeleportPhase = startSecondAI && base.NPC.ai[0] == 8f;
		bool ai2 = startSecondAI;
		SpriteEffects spriteEffects = (SpriteEffects)(ai2 ? 1 : 0);
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)(!ai2);
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[base.Type] / 2));
		Color color = drawColor;
		Color invincibleColor = default(Color);
		((Color)(ref invincibleColor))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
		Color lerpEndColor = Color.White;
		float lerpInterpolateValue = 0f;
		bool invincible = ai2 && invincibilityCounter < 300;
		bool enteredSubphase2 = base.NPC.ai[0] > 5f;
		bool enteredSubphase3 = base.NPC.ai[0] > 12f;
		bool enteredPhase2 = startSecondAI;
		int afterimageTimer = 120;
		int afterimageColorDivisor = 60;
		if (enteredPhase2)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.9f, 0.7f, 0.3f, 1f);
		}
		else if (enteredSubphase3)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.8f, 0.7f, 0.4f, 1f);
		}
		else if (enteredSubphase2)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.7f, 0.7f, 0.5f, 1f);
		}
		else if (base.NPC.ai[0] == 4f && base.NPC.ai[2] > (float)afterimageTimer)
		{
			float buffColorMult = base.NPC.ai[2] - (float)afterimageTimer;
			buffColorMult /= (float)afterimageColorDivisor;
			color = CalamityGlobalNPC.buffColor(color, 1f - 0.3f * buffColorMult, 1f - 0.3f * buffColorMult, 1f - 0.5f * buffColorMult, 1f);
		}
		int afterimageAmt = 10;
		int afterimageIncrement = 2;
		if (base.NPC.ai[0] == -1f)
		{
			afterimageAmt = 0;
		}
		if (num)
		{
			afterimageAmt = 7;
		}
		if (invincible)
		{
			lerpEndColor = invincibleColor;
		}
		else if (chargingOrSpawnPhases)
		{
			lerpEndColor = Color.Red;
			lerpInterpolateValue = 0.5f;
		}
		else
		{
			color = drawColor;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += afterimageIncrement)
			{
				Color afterimageColor = color;
				afterimageColor = Color.Lerp(afterimageColor, lerpEndColor, lerpInterpolateValue);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		int additionalAfterimageAmt = 0;
		float additionalAfterimageOpacity = 0f;
		float afterimageScale = 0f;
		if (base.NPC.ai[0] == -1f)
		{
			additionalAfterimageAmt = 0;
		}
		if (tornadoPhase && base.NPC.ai[2] > 60f)
		{
			additionalAfterimageAmt = 6;
			additionalAfterimageOpacity = 1f - (float)Math.Cos((base.NPC.ai[2] - 60f) / 30f * ((float)Math.PI * 2f));
			additionalAfterimageOpacity /= 3f;
			afterimageScale = 40f;
		}
		if (newPhasePhase && base.NPC.ai[2] > (float)afterimageTimer)
		{
			additionalAfterimageAmt = 6;
			additionalAfterimageOpacity = 1f - (float)Math.Cos((base.NPC.ai[2] - (float)afterimageTimer) / (float)afterimageColorDivisor * ((float)Math.PI * 2f));
			additionalAfterimageOpacity /= 3f;
			afterimageScale = 60f;
		}
		if (pauseAfterTeleportPhase)
		{
			additionalAfterimageAmt = 6;
			additionalAfterimageOpacity = 1f - (float)Math.Cos(base.NPC.ai[2] / 30f * ((float)Math.PI * 2f));
			additionalAfterimageOpacity /= 3f;
			afterimageScale = 20f;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int k = 0; k < additionalAfterimageAmt; k++)
			{
				Color additionalAfterimageColor = drawColor;
				additionalAfterimageColor = Color.Lerp(additionalAfterimageColor, lerpEndColor, lerpInterpolateValue);
				additionalAfterimageColor = base.NPC.GetAlpha(additionalAfterimageColor);
				additionalAfterimageColor *= 1f - additionalAfterimageOpacity;
				Vector2 additionalAfterimagePos = base.NPC.Center + ((float)k / (float)additionalAfterimageAmt * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * afterimageScale * additionalAfterimageOpacity - screenPos;
				additionalAfterimagePos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				additionalAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, additionalAfterimagePos, (Rectangle?)base.NPC.frame, additionalAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, invincible ? invincibleColor : base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		if (enteredSubphase2 || base.NPC.ai[0] == 4f || startSecondAI)
		{
			texture = GlowTextureOrange.Value;
			Color orangeGlowColor = Color.Lerp(Color.White, invincible ? invincibleColor : Color.Orange, 0.5f) * base.NPC.Opacity;
			lerpEndColor = (invincible ? invincibleColor : Color.Orange);
			Texture2D texture2 = GlowTextureGreen.Value;
			Color greenGlowColorOpacity = Color.Lerp(Color.White, invincible ? invincibleColor : Color.Chartreuse, 0.5f) * base.NPC.Opacity;
			Color greenGlowColor = (invincible ? invincibleColor : Color.Chartreuse);
			Texture2D texture3 = GlowTexturePurple.Value;
			Color blueGlowColorOpacity = Color.Lerp(Color.White, invincible ? invincibleColor : Color.BlueViolet, 0.5f) * base.NPC.Opacity;
			Color blueGlowColor = (invincible ? invincibleColor : Color.BlueViolet);
			lerpInterpolateValue = 1f;
			additionalAfterimageOpacity = 0.5f;
			afterimageScale = 10f;
			afterimageIncrement = 1;
			if (newPhasePhase)
			{
				float glowColorAmplifier = base.NPC.ai[2] - (float)afterimageTimer;
				glowColorAmplifier /= (float)afterimageColorDivisor;
				lerpEndColor *= glowColorAmplifier;
				orangeGlowColor *= glowColorAmplifier;
				if (enteredSubphase3 || base.NPC.ai[0] == 10f || startSecondAI)
				{
					greenGlowColorOpacity *= glowColorAmplifier;
					greenGlowColor *= glowColorAmplifier;
				}
				if (enteredPhase2 || base.NPC.ai[0] == 17f)
				{
					blueGlowColorOpacity *= glowColorAmplifier;
					blueGlowColor *= glowColorAmplifier;
				}
			}
			if (pauseAfterTeleportPhase)
			{
				float teleportGlowColorScaler = base.NPC.ai[2];
				teleportGlowColorScaler /= 30f;
				if (teleportGlowColorScaler > 0.5f)
				{
					teleportGlowColorScaler = 1f - teleportGlowColorScaler;
				}
				teleportGlowColorScaler *= 2f;
				teleportGlowColorScaler = 1f - teleportGlowColorScaler;
				lerpEndColor *= teleportGlowColorScaler;
				orangeGlowColor *= teleportGlowColorScaler;
				greenGlowColorOpacity *= teleportGlowColorScaler;
				greenGlowColor *= teleportGlowColorScaler;
				blueGlowColorOpacity *= teleportGlowColorScaler;
				blueGlowColor *= teleportGlowColorScaler;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int l = 1; l < afterimageAmt; l += afterimageIncrement)
				{
					Color orangeAfterimageColor = orangeGlowColor;
					orangeAfterimageColor = Color.Lerp(orangeAfterimageColor, lerpEndColor, lerpInterpolateValue);
					orangeAfterimageColor *= (float)(afterimageAmt - l) / 15f;
					Vector2 glowmaskAfterimagePos = base.NPC.oldPos[l] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					glowmaskAfterimagePos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					glowmaskAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					spriteBatch.Draw(texture, glowmaskAfterimagePos, (Rectangle?)base.NPC.frame, orangeAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					if (enteredSubphase3 || base.NPC.ai[0] == 10f || startSecondAI)
					{
						Color greenAfterimageColor = greenGlowColorOpacity;
						greenAfterimageColor = Color.Lerp(greenAfterimageColor, greenGlowColor, lerpInterpolateValue);
						greenAfterimageColor *= (float)(afterimageAmt - l) / 15f;
						spriteBatch.Draw(texture2, glowmaskAfterimagePos, (Rectangle?)base.NPC.frame, greenAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					}
					if (enteredPhase2 || base.NPC.ai[0] == 17f)
					{
						Color blueAfterimageColor = blueGlowColorOpacity;
						blueAfterimageColor = Color.Lerp(blueAfterimageColor, blueGlowColor, lerpInterpolateValue);
						blueAfterimageColor *= (float)(afterimageAmt - l) / 15f;
						spriteBatch.Draw(texture3, glowmaskAfterimagePos, (Rectangle?)base.NPC.frame, blueAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					}
				}
				for (int m = 1; m < additionalAfterimageAmt; m++)
				{
					Color additionalOrangeColor = orangeGlowColor;
					additionalOrangeColor = Color.Lerp(additionalOrangeColor, lerpEndColor, lerpInterpolateValue);
					additionalOrangeColor = base.NPC.GetAlpha(additionalOrangeColor);
					additionalOrangeColor *= 1f - additionalAfterimageOpacity;
					Vector2 additionalGlowmaskPos = base.NPC.Center + ((float)m / (float)additionalAfterimageAmt * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * afterimageScale * additionalAfterimageOpacity - screenPos;
					additionalGlowmaskPos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					additionalGlowmaskPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					spriteBatch.Draw(texture, additionalGlowmaskPos, (Rectangle?)base.NPC.frame, additionalOrangeColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					if (enteredSubphase3 || base.NPC.ai[0] == 10f || startSecondAI)
					{
						Color additionalGreenColor = greenGlowColorOpacity;
						additionalGreenColor = Color.Lerp(additionalGreenColor, greenGlowColor, lerpInterpolateValue);
						additionalGreenColor = base.NPC.GetAlpha(additionalGreenColor);
						additionalGreenColor *= 1f - additionalAfterimageOpacity;
						spriteBatch.Draw(texture2, additionalGlowmaskPos, (Rectangle?)base.NPC.frame, additionalGreenColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					}
					if (enteredPhase2 || base.NPC.ai[0] == 17f)
					{
						Color additionalBlueColor = blueGlowColorOpacity;
						additionalBlueColor = Color.Lerp(additionalBlueColor, blueGlowColor, lerpInterpolateValue);
						additionalBlueColor = base.NPC.GetAlpha(additionalBlueColor);
						additionalBlueColor *= 1f - additionalAfterimageOpacity;
						spriteBatch.Draw(texture3, additionalGlowmaskPos, (Rectangle?)base.NPC.frame, additionalBlueColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					}
				}
			}
			spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, orangeGlowColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			if (enteredSubphase3 || base.NPC.ai[0] == 10f || startSecondAI)
			{
				spriteBatch.Draw(texture2, drawLocation, (Rectangle?)base.NPC.frame, greenGlowColorOpacity, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
			if (enteredPhase2 || base.NPC.ai[0] == 17f)
			{
				spriteBatch.Draw(texture3, drawLocation, (Rectangle?)base.NPC.frame, blueGlowColorOpacity, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<YharonBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[8]
		{
			ModContent.ItemType<DragonRage>(),
			ModContent.ItemType<TheBurningSky>(),
			ModContent.ItemType<DragonsBreath>(),
			ModContent.ItemType<ChickenCannon>(),
			ModContent.ItemType<PhoenixFlameBarrage>(),
			ModContent.ItemType<YharonsKindleStaff>(),
			ModContent.ItemType<Wrathwing>(),
			ModContent.ItemType<TheFinalDawn>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<YharimsCrystal>(), 10);
		normalOnly.Add(ModContent.ItemType<YharonMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ForgottenDragonEgg>(), 10);
		normalOnly.Add(ModContent.ItemType<McNuggets>(), 10);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<YharonSoulFragment>(), 1, 35, 40));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<WingsofRebirth>()));
		npcLoot.Add(ModContent.ItemType<YharonTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<YharonRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<YharonEgg>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedYharon, ModContent.ItemType<LoreYharon>(), ui: true, DropHelper.FirstKillText);
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override void OnKill()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<Bandit>() }, DownedBossSystem.downedYharon);
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		if (!DownedBossSystem.downedYharon)
		{
			CalamityUtils.SpawnOre(ModContent.TileType<AuricOre>(), 2E-05, 0.75f, 0.9f, 10, 20);
			Color messageColor = Color.Gold;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.AuricOreText", messageColor);
		}
		DownedBossSystem.downedYharon = true;
		CalamityNetcode.SyncWorld();
		if (Main.netMode != 1 && Main.zenithWorld)
		{
			for (int i = 0; i < 4; i++)
			{
				int type = ModContent.ProjectileType<YharonBulletHellVortex>();
				Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Vector2.Zero, type, VortexDamage, 0f, Main.myPlayer, 360f, base.NPC.whoAmI);
			}
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 2f;
		return null;
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

	public override void FindFrame(int frameHeight)
	{
		bool idlePhases = (!startSecondAI && (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 8f || base.NPC.ai[0] == 13f || base.NPC.ai[0] == 15f)) || (startSecondAI && (base.NPC.ai[0] == 5f || base.NPC.ai[0] < 2f));
		bool chargingOrSpawnPhases = (!startSecondAI && (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 5f || base.NPC.ai[0] == 7f || base.NPC.ai[0] == 11f || base.NPC.ai[0] == 14f || base.NPC.ai[0] == 18f)) || (startSecondAI && (base.NPC.ai[0] == 6f || base.NPC.ai[0] == 2f || base.NPC.ai[0] == 7f));
		bool projectileOrCirclePhases = (!startSecondAI && (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 12f || base.NPC.ai[0] == 19f || base.NPC.ai[0] == 20f)) || (startSecondAI && (base.NPC.ai[0] == 4f || base.NPC.ai[0] == 3f || base.NPC.ai[0] == 8f));
		bool tornadoPhase = !startSecondAI && (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 9f || base.NPC.ai[0] == -1f || base.NPC.ai[0] == 16f);
		bool newPhasePhase = (!startSecondAI && (base.NPC.ai[0] == 4f || base.NPC.ai[0] == 10f || base.NPC.ai[0] == 17f)) || (startSecondAI && base.NPC.ai[0] == 9f);
		if ((!startSecondAI && (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 13f) && base.NPC.localAI[1] > 0f) || (startSecondAI && base.NPC.ai[0] < 2f && base.NPC.localAI[1] > 0f))
		{
			float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
			bool num = base.NPC.localAI[1] < (float)fastChargeTelegraphTime * 0.5f || base.NPC.localAI[1] > (float)fastChargeTelegraphTime - (float)fastChargeTelegraphTime / 6f;
			bool doTelegraphRoarAnimation = base.NPC.localAI[1] > (float)fastChargeTelegraphTime - (float)fastChargeTelegraphTime * 0.4f && base.NPC.localAI[1] < (float)fastChargeTelegraphTime - (float)fastChargeTelegraphTime * 0.2f;
			bool phase4 = startSecondAI && lifeRatio <= ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 0.2f : 0.15f) && (CalamityWorld.revenge || BossRushEvent.BossRushActive);
			if (num)
			{
				base.NPC.frameCounter += (phase4 ? 2.0 : 1.0);
				if (base.NPC.frameCounter > 5.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y += frameHeight;
				}
				if (base.NPC.frame.Y >= frameHeight * 5)
				{
					base.NPC.frame.Y = 0;
				}
			}
			else
			{
				base.NPC.frame.Y = frameHeight * 5;
				if (doTelegraphRoarAnimation)
				{
					base.NPC.frame.Y = frameHeight * 6;
				}
			}
			return;
		}
		if (idlePhases)
		{
			int frameTimer = 5;
			if (!startSecondAI && (base.NPC.ai[0] == 6f || base.NPC.ai[0] == 13f))
			{
				frameTimer = 4;
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > (double)frameTimer)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = 0;
			}
		}
		if (chargingOrSpawnPhases)
		{
			base.NPC.frame.Y = frameHeight * 5;
		}
		if (projectileOrCirclePhases)
		{
			base.NPC.frame.Y = frameHeight * 5;
		}
		if (tornadoPhase)
		{
			int tornadoFrameTimer = 90;
			if (base.NPC.ai[2] < (float)(tornadoFrameTimer - 30) || base.NPC.ai[2] > (float)(tornadoFrameTimer - 10))
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 5.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y += frameHeight;
				}
				if (base.NPC.frame.Y >= frameHeight * 5)
				{
					base.NPC.frame.Y = 0;
				}
			}
			else
			{
				base.NPC.frame.Y = frameHeight * 5;
				if (base.NPC.ai[2] > (float)(tornadoFrameTimer - 20) && base.NPC.ai[2] < (float)(tornadoFrameTimer - 15))
				{
					base.NPC.frame.Y = frameHeight * 6;
				}
			}
		}
		if (!newPhasePhase)
		{
			return;
		}
		int newPhaseFrameTimer = 180;
		if (base.NPC.ai[2] < (float)(newPhaseFrameTimer - 60) || base.NPC.ai[2] > (float)(newPhaseFrameTimer - 20))
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 5.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = 0;
			}
		}
		else
		{
			base.NPC.frame.Y = frameHeight * 5;
			if (base.NPC.ai[2] > (float)(newPhaseFrameTimer - 50) && base.NPC.ai[2] < (float)(newPhaseFrameTimer - 25))
			{
				base.NPC.frame.Y = frameHeight * 6;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = Main.rand.Next(16, 20);
			SoundEngine.PlaySound(in HitSound, base.NPC.Center);
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		DoFireRing(300, (Main.expertMode || BossRushEvent.BossRushActive) ? 125 : 150, -1f, 0f);
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 300;
		base.NPC.height = 280;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int fieryDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[fieryDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[fieryDust].scale = 0.5f;
				Main.dust[fieryDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int fieryDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fieryDust2].noGravity = true;
			Dust obj2 = Main.dust[fieryDust2];
			obj2.velocity *= 5f;
			fieryDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fieryDust2];
			obj3.velocity *= 2f;
		}
		if (base.NPC.life <= 0)
		{
			DeathAshParticle.CreateAshesFromNPC(base.NPC, Vector2.Zero);
		}
	}

	public override void PostAI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(RoarSoundSlot, out ActiveSound roarSound) && roarSound.IsPlaying)
		{
			roarSound.Position = base.NPC.Center;
		}
	}
}
