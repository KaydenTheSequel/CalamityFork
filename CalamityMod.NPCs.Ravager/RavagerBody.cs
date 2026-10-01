using System;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
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

namespace CalamityMod.NPCs.Ravager;

[AutoloadBossHead]
public class RavagerBody : ModNPC
{
	private float velocityY = -16f;

	public static readonly SoundStyle JumpSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerJump", 2);

	public static readonly SoundStyle StompSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerStomp", 2);

	public static readonly SoundStyle FistSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerPunch", 2);

	public static readonly SoundStyle LimbLossSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerLimbLoss", 4);

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerHurt", 4);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerDeath", 2);

	public static readonly SoundStyle PillarSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerPillarSummon");

	public static readonly SoundStyle MissileSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerMissileLaunch");

	public static Asset<Texture2D> GlowTexture;

	public static Asset<Texture2D> ChainTexture;

	public static int BlasterDamage = 92;

	public static int RockDamage = 60;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -40f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.6f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y -= 50f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			ChainTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Ravager/RavagerChain", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 90;
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = true;
		base.NPC.npcSlots = 20f;
		base.NPC.aiStyle = -1;
		base.NPC.width = 332;
		base.NPC.height = 214;
		base.NPC.defense = 55;
		base.NPC.value = Item.buyPrice(0, 25);
		base.NPC.DR_NERD(0.35f);
		base.NPC.LifeMaxNERB(30000, 54000, 460000);
		if (DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive)
		{
			base.NPC.damage = (int)((double)base.NPC.damage * 1.5);
			base.NPC.defense *= 2;
			base.NPC.lifeMax *= 4;
			base.NPC.value *= 4f;
		}
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<RavagerBossBar>();
		base.NPC.netAlways = true;
		base.NPC.alpha = 255;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Ravager")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(velocityY);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		velocityY = reader.ReadSingle();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Unknown result type (might be due to invalid IL or missing references)
		//IL_111b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1152: Unknown result type (might be due to invalid IL or missing references)
		//IL_115c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1161: Unknown result type (might be due to invalid IL or missing references)
		//IL_101f: Unknown result type (might be due to invalid IL or missing references)
		//IL_102f: Unknown result type (might be due to invalid IL or missing references)
		//IL_103f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1054: Unknown result type (might be due to invalid IL or missing references)
		//IL_105a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1292: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1305: Unknown result type (might be due to invalid IL or missing references)
		//IL_130f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_120d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1213: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1428: Unknown result type (might be due to invalid IL or missing references)
		//IL_143e: Unknown result type (might be due to invalid IL or missing references)
		//IL_144e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1464: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_149b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1376: Unknown result type (might be due to invalid IL or missing references)
		//IL_138c: Unknown result type (might be due to invalid IL or missing references)
		//IL_139c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_15be: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1600: Unknown result type (might be due to invalid IL or missing references)
		//IL_1637: Unknown result type (might be due to invalid IL or missing references)
		//IL_1641: Unknown result type (might be due to invalid IL or missing references)
		//IL_1646: Unknown result type (might be due to invalid IL or missing references)
		//IL_150c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1522: Unknown result type (might be due to invalid IL or missing references)
		//IL_1532: Unknown result type (might be due to invalid IL or missing references)
		//IL_1547: Unknown result type (might be due to invalid IL or missing references)
		//IL_154d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_22cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_29f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2372: Unknown result type (might be due to invalid IL or missing references)
		//IL_237d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_218d: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2517: Unknown result type (might be due to invalid IL or missing references)
		//IL_222c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2233: Unknown result type (might be due to invalid IL or missing references)
		//IL_2239: Unknown result type (might be due to invalid IL or missing references)
		//IL_225e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2268: Unknown result type (might be due to invalid IL or missing references)
		//IL_226d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_2544: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_277a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2786: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_204c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2061: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2618: Unknown result type (might be due to invalid IL or missing references)
		//IL_261d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2899: Unknown result type (might be due to invalid IL or missing references)
		//IL_28a5: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		Lighting.AddLight((int)(base.NPC.Center.X - 110f) / 16, (int)(base.NPC.Center.Y - 30f) / 16, 0f, 0.5f, 2f);
		Lighting.AddLight((int)(base.NPC.Center.X + 110f) / 16, (int)(base.NPC.Center.Y - 30f) / 16, 0f, 0.5f, 2f);
		Lighting.AddLight((int)(base.NPC.Center.X - 40f) / 16, (int)(base.NPC.Center.Y - 60f) / 16, 0f, 0.25f, 1f);
		Lighting.AddLight((int)(base.NPC.Center.X + 40f) / 16, (int)(base.NPC.Center.Y - 60f) / 16, 0f, 0.25f, 1f);
		CalamityGlobalNPC.scavenger = base.NPC.whoAmI;
		if (base.NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			base.NPC.localAI[0] = 1f;
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X - 70, (int)base.NPC.Center.Y + 88, ModContent.NPCType<RavagerLegLeft>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + 70, (int)base.NPC.Center.Y + 88, ModContent.NPCType<RavagerLegRight>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X - 120, (int)base.NPC.Center.Y + 50, ModContent.NPCType<RavagerClawLeft>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + 120, (int)base.NPC.Center.Y + 50, ModContent.NPCType<RavagerClawRight>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + 1, (int)base.NPC.Center.Y - 20, ModContent.NPCType<RavagerHead>());
		}
		if (base.NPC.target >= 0 && Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest();
			if (Main.player[base.NPC.target].dead)
			{
				base.NPC.noTileCollide = true;
			}
		}
		Player player = Main.player[base.NPC.target];
		if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 10;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			base.NPC.ai[1] = 0f;
		}
		bool leftLegActive = false;
		bool rightLegActive = false;
		bool headActive = false;
		bool rightClawActive = false;
		bool leftClawActive = false;
		bool freeHeadActive = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC current = enumerator.Current;
			if (current.type == ModContent.NPCType<RavagerHead>())
			{
				headActive = true;
			}
			if (current.type == ModContent.NPCType<RavagerClawRight>())
			{
				rightClawActive = true;
			}
			if (current.type == ModContent.NPCType<RavagerClawLeft>())
			{
				leftClawActive = true;
			}
			if (current.type == ModContent.NPCType<RavagerLegRight>())
			{
				rightLegActive = true;
			}
			if (current.type == ModContent.NPCType<RavagerLegLeft>())
			{
				leftLegActive = true;
			}
			if (current.type == ModContent.NPCType<RavagerHead2>())
			{
				freeHeadActive = true;
			}
		}
		bool anyHeadActive = headActive | freeHeadActive;
		bool immunePhase = headActive | rightClawActive | leftClawActive | rightLegActive | leftLegActive;
		bool finalPhase = (!leftClawActive && !rightClawActive && !headActive && !leftLegActive && !rightLegActive) & expertMode;
		bool phase2 = base.NPC.ai[0] == 2f;
		bool reduceFallSpeed = base.NPC.velocity.Y > 0f && Collision.SolidCollision(base.NPC.position + Vector2.UnitY * 1.1f * base.NPC.velocity.Y, base.NPC.width, base.NPC.height);
		if (immunePhase)
		{
			base.NPC.dontTakeDamage = true;
		}
		else
		{
			base.NPC.dontTakeDamage = false;
			ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Player p = enumerator2.Current;
				if (!p.dead & revenge)
				{
					p.AddBuff(ModContent.BuffType<WeakPetrification>(), 2);
				}
			}
		}
		if (Main.zenithWorld)
		{
			bool num = lifeRatio < 0.2f;
			base.NPC.localAI[1]++;
			Vector2 Pos = player.Center;
			int type = ModContent.ProjectileType<RavagerBlaster>();
			int damage = BlasterDamage;
			if (num)
			{
				Vector2 circleOffset = Pos + (Vector2.UnitY * 640f).RotatedBy(MathHelper.ToRadians(base.NPC.localAI[1] * 3f));
				if (base.NPC.localAI[1] % 5f == 0f)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), circleOffset, Pos, type, damage, 0f, Main.myPlayer, 0f, 0.8f);
				}
			}
			else if (base.NPC.localAI[1] >= 8000f)
			{
				float randOffsetX = Main.rand.NextFloat(240f, 800f) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
				float randOffsetY = Main.rand.NextFloat(240f, 640f) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
				if (base.NPC.localAI[1] > 8300f)
				{
					base.NPC.localAI[1] = 0f;
				}
				else if (base.NPC.localAI[1] % 20f == 0f)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - randOffsetX, Pos.Y - randOffsetY, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 1f);
				}
			}
			else if (base.NPC.localAI[1] >= 6000f)
			{
				float plusOffset = 640f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - plusOffset, Pos.Y, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 4f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X + plusOffset, Pos.Y, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 4f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X, Pos.Y - plusOffset, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 4f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X, Pos.Y + plusOffset, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 4f);
				base.NPC.localAI[1] = 0f;
			}
			else if (base.NPC.localAI[1] >= 4000f)
			{
				float crossOffset = 400f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - crossOffset, Pos.Y - crossOffset, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 2f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - crossOffset, Pos.Y + crossOffset, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 2f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X + crossOffset, Pos.Y - crossOffset, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 2f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X + crossOffset, Pos.Y + crossOffset, Pos.X, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 2f);
				base.NPC.localAI[1] = 0f;
			}
			else if (base.NPC.localAI[1] >= 2000f)
			{
				float gridOffset1 = 480f;
				float gridOffset2 = 560f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - gridOffset2, Pos.Y - gridOffset1, Pos.X, Pos.Y - gridOffset1, type, damage, 0f, Main.myPlayer, 0f, 1f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - gridOffset1, Pos.Y - gridOffset2, Pos.X - gridOffset1, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 1f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X + gridOffset2, Pos.Y + gridOffset1, Pos.X, Pos.Y + gridOffset1, type, damage, 0f, Main.myPlayer, 0f, 1f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X + gridOffset1, Pos.Y + gridOffset2, Pos.X + gridOffset1, Pos.Y, type, damage, 0f, Main.myPlayer, 0f, 1f);
				base.NPC.localAI[1] = 0f;
			}
			else if (base.NPC.localAI[1] >= 1000f)
			{
				float lineOffset = 800f * (float)((!Main.rand.NextBool()) ? 1 : (-1));
				if (base.NPC.localAI[1] > 1180f)
				{
					base.NPC.localAI[1] = 0f;
				}
				else if (base.NPC.localAI[1] % 60f == 0f)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Pos.X - lineOffset, Pos.Y, Pos.X, Pos.Y, type, damage * 2, 0f, Main.myPlayer, 0f, 4f);
				}
			}
			else if (base.NPC.localAI[1] >= 300f)
			{
				base.NPC.localAI[1] = 1000f * (float)Main.rand.Next(1, 6 + ((!immunePhase) ? 4 : 0));
			}
			else if (base.NPC.localAI[1] >= 60f && Main.rand.NextBool(1200))
			{
				int laser = ModContent.ProjectileType<RavagerBlast>();
				float blueOffset1 = 2400f;
				float blueOffset2 = 800f;
				Vector2 position = default(Vector2);
				((Vector2)(ref position))._002Ector(Pos.X - blueOffset2, Pos.Y - blueOffset1 * (float)((!Main.rand.NextBool()) ? 1 : (-1)));
				Vector2 destination = default(Vector2);
				((Vector2)(ref destination))._002Ector(Pos.X - blueOffset2, Pos.Y);
				int movement = Main.rand.Next(-4, 0);
				switch (movement)
				{
				case -2:
					position.X = Pos.X + blueOffset2;
					((Vector2)(ref destination))._002Ector(Pos.X + blueOffset2, Pos.Y);
					break;
				case -3:
					position.X = Pos.X - blueOffset1 * (float)((!Main.rand.NextBool()) ? 1 : (-1));
					position.Y = Pos.Y - blueOffset2;
					((Vector2)(ref destination))._002Ector(Pos.X, Pos.Y - blueOffset2);
					break;
				case -4:
					position.X = Pos.X - blueOffset1 * (float)((!Main.rand.NextBool()) ? 1 : (-1));
					position.Y = Pos.Y + blueOffset2;
					((Vector2)(ref destination))._002Ector(Pos.X, Pos.Y + blueOffset2);
					break;
				}
				Vector2 velocity = (destination - position).SafeNormalize(Vector2.Zero);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), position, velocity, laser, damage, 0f, Main.myPlayer, 1f, movement);
				base.NPC.localAI[1] = 0f;
			}
		}
		if (!headActive)
		{
			int rightDust = Dust.NewDust(new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 30f), 8, 8, 5, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[rightDust].alpha += Main.rand.Next(100);
			Dust obj = Main.dust[rightDust];
			obj.velocity *= 0.2f;
			Main.dust[rightDust].velocity.Y -= 3f + (float)Main.rand.Next(10) * 0.1f;
			Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
			if (Main.rand.NextBool(10))
			{
				rightDust = Dust.NewDust(new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 30f), 8, 8, 6, 0f, 0f, 0, default(Color), 1.5f);
				if (!Main.rand.NextBool(20))
				{
					Main.dust[rightDust].noGravity = true;
					Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
					Main.dust[rightDust].velocity.Y -= 4f;
				}
			}
		}
		if (!rightClawActive)
		{
			int rightDust2 = Dust.NewDust(new Vector2(base.NPC.Center.X + 80f, base.NPC.Center.Y + 45f), 8, 8, 5, 0f, 0f, 100, default(Color), 3f);
			Main.dust[rightDust2].alpha += Main.rand.Next(100);
			Dust obj2 = Main.dust[rightDust2];
			obj2.velocity *= 0.2f;
			Main.dust[rightDust2].velocity.X += 3f + (float)Main.rand.Next(10) * 0.1f;
			Main.dust[rightDust2].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
			if (Main.rand.NextBool(10))
			{
				rightDust2 = Dust.NewDust(new Vector2(base.NPC.Center.X + 80f, base.NPC.Center.Y + 45f), 8, 8, 6, 0f, 0f, 0, default(Color), 2f);
				if (!Main.rand.NextBool(20))
				{
					Main.dust[rightDust2].noGravity = true;
					Main.dust[rightDust2].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
					Main.dust[rightDust2].velocity.X += 4f;
				}
			}
		}
		if (!leftClawActive)
		{
			int leftDust = Dust.NewDust(new Vector2(base.NPC.Center.X - 80f, base.NPC.Center.Y + 45f), 8, 8, 5, 0f, 0f, 100, default(Color), 3f);
			Dust obj3 = Main.dust[leftDust];
			obj3.alpha += Main.rand.Next(100);
			obj3.velocity *= 0.2f;
			obj3.velocity.X -= 3f + (float)Main.rand.Next(10) * 0.1f;
			obj3.fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
			if (Main.rand.NextBool(10))
			{
				leftDust = Dust.NewDust(new Vector2(base.NPC.Center.X - 80f, base.NPC.Center.Y + 45f), 8, 8, 6, 0f, 0f, 0, default(Color), 2f);
				if (!Main.rand.NextBool(20))
				{
					Dust obj4 = Main.dust[leftDust];
					obj4.noGravity = true;
					obj4.scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
					obj4.velocity.X -= 4f;
				}
			}
		}
		if (!rightLegActive)
		{
			int rightDust3 = Dust.NewDust(new Vector2(base.NPC.Center.X + 60f, base.NPC.Center.Y + 60f), 8, 8, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj5 = Main.dust[rightDust3];
			obj5.alpha += Main.rand.Next(100);
			obj5.velocity *= 0.2f;
			obj5.velocity.Y += 0.5f + (float)Main.rand.Next(10) * 0.1f;
			obj5.fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
			if (Main.rand.NextBool(10))
			{
				rightDust3 = Dust.NewDust(new Vector2(base.NPC.Center.X + 60f, base.NPC.Center.Y + 60f), 8, 8, 6, 0f, 0f, 0, default(Color), 1.5f);
				if (!Main.rand.NextBool(20))
				{
					Dust obj6 = Main.dust[rightDust3];
					obj6.noGravity = true;
					obj6.scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
					obj6.velocity.Y++;
				}
			}
		}
		if (!leftLegActive)
		{
			int leftDust2 = Dust.NewDust(new Vector2(base.NPC.Center.X - 60f, base.NPC.Center.Y + 60f), 8, 8, 5, 0f, 0f, 100, default(Color), 2f);
			Main.dust[leftDust2].alpha += Main.rand.Next(100);
			Dust obj7 = Main.dust[leftDust2];
			obj7.velocity *= 0.2f;
			Main.dust[leftDust2].velocity.Y += 0.5f + (float)Main.rand.Next(10) * 0.1f;
			Main.dust[leftDust2].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
			if (Main.rand.NextBool(10))
			{
				leftDust2 = Dust.NewDust(new Vector2(base.NPC.Center.X - 60f, base.NPC.Center.Y + 60f), 8, 8, 6, 0f, 0f, 0, default(Color), 1.5f);
				if (!Main.rand.NextBool(20))
				{
					Main.dust[leftDust2].noGravity = true;
					Main.dust[leftDust2].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
					Main.dust[leftDust2].velocity.Y++;
				}
			}
		}
		if (base.NPC.noTileCollide && !player.dead)
		{
			if (base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > player.Top.Y)
			{
				base.NPC.noTileCollide = false;
			}
			else if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.noTileCollide = false;
			}
		}
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > 0f)
				{
					if (revenge)
					{
						if (calamityGlobalNPC.newAI[0] % 3f == 0f)
						{
							base.NPC.ai[1]++;
						}
						else if (calamityGlobalNPC.newAI[0] % 2f == 0f)
						{
							base.NPC.ai[1]++;
						}
					}
					if (!rightClawActive && !leftClawActive)
					{
						base.NPC.ai[1]++;
					}
					if (!headActive)
					{
						base.NPC.ai[1]++;
					}
					if (!rightLegActive && !leftLegActive)
					{
						base.NPC.ai[1]++;
					}
				}
				float jumpGateValue = (Main.getGoodWorld ? 0f : 180f);
				if (base.NPC.ai[1] >= jumpGateValue)
				{
					base.NPC.ai[1] = -20f;
				}
				else if (base.NPC.ai[1] == -1f)
				{
					if (!finalPhase)
					{
						base.NPC.damage = base.NPC.defDamage;
					}
					base.NPC.noTileCollide = true;
					base.NPC.TargetClosest();
					player = Main.player[base.NPC.target];
					bool shouldFall = player.position.Y >= base.NPC.Bottom.Y;
					float velocityXBoost = ((!anyHeadActive) ? 6f : (4f * (1f - lifeRatio)));
					float velocityX = 4f + velocityXBoost;
					if (velocityY != 16f)
					{
						SoundEngine.PlaySound(in JumpSound, base.NPC.Center);
					}
					velocityY = -16f;
					float distanceBelowTarget = base.NPC.position.Y - (player.position.Y + 80f);
					if (revenge)
					{
						float multiplier = 0.0015f;
						if (distanceBelowTarget > 0f)
						{
							calamityGlobalNPC.newAI[1] += 1f + distanceBelowTarget * multiplier;
						}
						float speedMultLimit = 2f;
						if (calamityGlobalNPC.newAI[1] > speedMultLimit)
						{
							calamityGlobalNPC.newAI[1] = speedMultLimit;
						}
						if (calamityGlobalNPC.newAI[1] > 1f)
						{
							velocityY *= calamityGlobalNPC.newAI[1];
						}
					}
					if (expertMode && !finalPhase)
					{
						if (shouldFall)
						{
							velocityY = 1f;
						}
						if (calamityGlobalNPC.newAI[0] % 3f == 0f)
						{
							velocityX *= 2f;
							if (!shouldFall)
							{
								velocityY *= 0.75f;
							}
						}
						else if (calamityGlobalNPC.newAI[0] % 2f == 0f)
						{
							velocityX *= 1.5f;
							if (!shouldFall)
							{
								velocityY *= 1f;
							}
						}
					}
					if (finalPhase)
					{
						calamityGlobalNPC.newAI[2] = player.direction;
					}
					float playerLocation = base.NPC.Center.X - player.Center.X;
					base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
					base.NPC.ai[2] = base.NPC.direction;
					base.NPC.velocity.X = velocityX * (float)base.NPC.direction;
					base.NPC.velocity.Y = velocityY;
					base.NPC.ai[0] = (finalPhase ? 2f : 1f);
					base.NPC.ai[1] = 0f;
				}
			}
			if (base.NPC.ai[0] != 1f)
			{
				CustomGravity();
			}
		}
		else if (base.NPC.ai[0] >= 1f)
		{
			if (base.NPC.velocity.Y == 0f && (base.NPC.ai[1] == 31f || base.NPC.ai[0] == 1f))
			{
				base.NPC.damage = 0;
				SoundEngine.PlaySound(in StompSound, base.NPC.Center);
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				if (Main.netMode != 1 && expertMode)
				{
					ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						NPC n = enumerator3.Current;
						if (n.type == ModContent.NPCType<RockPillar>() && n.ai[0] == 0f)
						{
							n.ai[1]++;
							n.direction = base.NPC.direction;
							n.netUpdate = true;
						}
					}
					int spawnDistance = 360;
					SoundEngine.PlaySound(in PillarSound, base.NPC.Center);
					if (death & phase2)
					{
						spawnDistance = 300;
						int index = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)player.Center.X, (int)player.Center.Y - spawnDistance - 100, ModContent.NPCType<RockPillar>());
						if (Main.npc.IndexInRange(index))
						{
							NPC npc = Main.npc[index];
							(int, int) H_W = (npc.height, npc.width);
							npc.position = npc.Center;
							npc.height = H_W.Item2;
							npc.width = H_W.Item1;
							npc.rotation = -(float)Math.PI / 2f;
							npc.Center = npc.position;
							npc.ModNPC.DrawOffsetY = npc.width / 2 - npc.height / 2;
						}
					}
					bool num2 = Main.npc.Any((NPC x) => x.active && x.type == ModContent.NPCType<RockPillar>() && x.ai[1] < 2f);
					bool anyFlamePillars = NPC.AnyNPCs(ModContent.NPCType<FlamePillar>());
					if (!num2 | phase2)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.Center.X - (float)spawnDistance * 1.25f), (int)player.Center.Y - 100, ModContent.NPCType<RockPillar>());
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.Center.X + (float)spawnDistance * 1.25f), (int)player.Center.Y - 100, ModContent.NPCType<RockPillar>());
					}
					if (!anyFlamePillars && !phase2)
					{
						float distanceMultiplier = (finalPhase ? 2.5f : 2f);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)player.Center.X - (int)((float)spawnDistance * distanceMultiplier), (int)player.Center.Y - 100, ModContent.NPCType<FlamePillar>());
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)player.Center.X + (int)((float)spawnDistance * distanceMultiplier), (int)player.Center.Y - 100, ModContent.NPCType<FlamePillar>());
					}
				}
				if (revenge)
				{
					calamityGlobalNPC.newAI[0]++;
				}
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				base.NPC.TargetClosest();
				for (int stompDustArea = (int)base.NPC.position.X - 30; stompDustArea < (int)base.NPC.position.X + base.NPC.width + 60; stompDustArea += 30)
				{
					for (int stompDustAmount = 0; stompDustAmount < 6; stompDustAmount++)
					{
						int stompDust = Dust.NewDust(new Vector2(base.NPC.position.X - 30f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 30, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj8 = Main.dust[stompDust];
						obj8.velocity *= 0.2f;
					}
					if (!Main.dedServ)
					{
						int stompGore = Gore.NewGore(base.NPC.GetSource_FromAI(), new Vector2((float)(stompDustArea - 30), base.NPC.position.Y + (float)base.NPC.height - 12f), default(Vector2), Main.rand.Next(61, 64));
						Gore obj9 = Main.gore[stompGore];
						obj9.velocity *= 0.4f;
					}
				}
			}
			else
			{
				if (!phase2)
				{
					base.NPC.damage = base.NPC.defDamage;
				}
				Vector2 targetVector = player.position;
				float aimY = targetVector.Y - 640f;
				float distanceFromTargetPos = Math.Abs(base.NPC.Top.Y - aimY);
				bool inRange = base.NPC.Top.Y <= aimY + 160f && base.NPC.Top.Y >= aimY - 16f;
				if (phase2 && base.NPC.ai[1] == 0f)
				{
					if (calamityGlobalNPC.newAI[3] == 0f)
					{
						SoundEngine.PlaySound(in JumpSound, base.NPC.Center);
					}
					base.NPC.noTileCollide = true;
					calamityGlobalNPC.newAI[3]++;
					if (inRange)
					{
						base.NPC.velocity.Y = 0f;
					}
					else if (base.NPC.Top.Y > aimY)
					{
						base.NPC.velocity.Y -= 0.2f + distanceFromTargetPos * 0.001f;
					}
					else
					{
						base.NPC.velocity.Y += 0.2f + distanceFromTargetPos * 0.001f;
					}
					if (base.NPC.velocity.Y < velocityY)
					{
						base.NPC.velocity.Y = velocityY;
					}
					if (base.NPC.velocity.Y > 0f - velocityY)
					{
						base.NPC.velocity.Y = 0f - velocityY;
					}
				}
				if (Math.Abs(player.velocity.X) < 0.5f)
				{
					calamityGlobalNPC.newAI[2] = 0f;
				}
				else
				{
					calamityGlobalNPC.newAI[2] = player.direction;
				}
				float maxOffset = 240f * (1f - lifeRatio);
				float offset = (phase2 ? (maxOffset * calamityGlobalNPC.newAI[2]) : 0f);
				int quarterWidth = (int)((float)base.NPC.width * 0.25f);
				if ((base.NPC.position.X + (float)quarterWidth < targetVector.X + offset && base.NPC.position.X + (float)base.NPC.width - (float)quarterWidth > targetVector.X + (float)player.width + offset && (inRange || base.NPC.ai[0] != 2f)) || base.NPC.ai[1] > 0f || calamityGlobalNPC.newAI[3] >= 90f)
				{
					if (phase2)
					{
						float stopBeforeFallTime = 30f;
						if (!anyHeadActive)
						{
							stopBeforeFallTime -= 15f;
						}
						else if (expertMode)
						{
							stopBeforeFallTime -= (death ? (15f * (1f - lifeRatio)) : (10f * (1f - lifeRatio)));
						}
						if (base.NPC.ai[1] < stopBeforeFallTime)
						{
							base.NPC.ai[1]++;
							base.NPC.velocity = Vector2.Zero;
						}
						else
						{
							base.NPC.damage = base.NPC.defDamage;
							float fallSpeedBoost = ((!anyHeadActive) ? 1.8f : (death ? (1.8f * (1f - lifeRatio)) : (1.2f * (1f - lifeRatio))));
							float fallSpeed = 1.2f + fallSpeedBoost;
							if (calamityGlobalNPC.newAI[1] > 1f)
							{
								fallSpeed *= calamityGlobalNPC.newAI[1];
							}
							base.NPC.velocity.Y += fallSpeed;
							base.NPC.ai[1] = 31f;
						}
					}
					else
					{
						base.NPC.velocity.X *= 0.8f;
						if (base.NPC.Bottom.Y < player.position.Y)
						{
							float fallSpeedBoost2 = ((!anyHeadActive) ? 0.9f : (0.6f * (1f - lifeRatio)));
							float fallSpeed2 = 0.6f + fallSpeedBoost2;
							if (calamityGlobalNPC.newAI[1] > 1f)
							{
								fallSpeed2 *= calamityGlobalNPC.newAI[1];
							}
							base.NPC.velocity.Y += fallSpeed2;
						}
					}
				}
				else
				{
					float velocityMult = 1.8f;
					float velocityXChange = 0.2f + Math.Abs(base.NPC.Center.X - player.Center.X) * 0.001f;
					float velocityXBoost2 = ((!anyHeadActive) ? 6f : (4f * (1f - lifeRatio)));
					float velocityXCap = 8f + velocityXBoost2 + Math.Abs(base.NPC.Center.X - player.Center.X) * 0.001f;
					if (!rightClawActive)
					{
						velocityXCap++;
					}
					if (!leftClawActive)
					{
						velocityXCap++;
					}
					if (!headActive)
					{
						velocityXCap++;
					}
					if (!rightLegActive)
					{
						velocityXCap++;
					}
					if (!leftLegActive)
					{
						velocityXCap++;
					}
					if (phase2)
					{
						velocityXChange *= velocityMult;
						velocityXCap *= velocityMult;
					}
					if (base.NPC.direction < 0)
					{
						base.NPC.velocity.X -= velocityXChange;
					}
					else if (base.NPC.direction > 0)
					{
						base.NPC.velocity.X += velocityXChange;
					}
					if ((float)((base.NPC.Center.X - player.Center.X < 0f) ? 1 : (-1)) != base.NPC.ai[2])
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
				CustomGravity();
			}
		}
		player = Main.player[base.NPC.target];
		if (base.NPC.target <= 0 || base.NPC.target == 255 || player.dead || !player.active)
		{
			base.NPC.TargetClosest();
			player = Main.player[base.NPC.target];
		}
		int distanceFromTarget = (player.dead ? 1600 : 5600);
		if (Vector2.Distance(base.NPC.Center, player.Center) > (float)distanceFromTarget)
		{
			base.NPC.TargetClosest();
			player = Main.player[base.NPC.target];
			if (Vector2.Distance(base.NPC.Center, player.Center) > (float)distanceFromTarget)
			{
				base.NPC.active = false;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
		void CustomGravity()
		{
			float gravity = (phase2 ? 0f : 0.45f);
			float maxFallSpeed = (reduceFallSpeed ? 12f : (phase2 ? 24f : 15f));
			if (calamityGlobalNPC.newAI[1] > 1f && !reduceFallSpeed)
			{
				maxFallSpeed *= calamityGlobalNPC.newAI[1];
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

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = default(Vector2);
		((Vector2)(ref center))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
		if (base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.Draw(TextureAssets.Npc[ModContent.NPCType<RavagerClawLeft>()].Value, new Vector2(center.X - screenPos.X - base.NPC.scale * 180f, center.Y - screenPos.Y + 50f), (Rectangle?)new Rectangle(0, 0, TextureAssets.Npc[ModContent.NPCType<RavagerClawLeft>()].Value.Width, TextureAssets.Npc[ModContent.NPCType<RavagerClawLeft>()].Value.Height), Color.White, 0f, default(Vector2), base.NPC.scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(TextureAssets.Npc[ModContent.NPCType<RavagerClawRight>()].Value, new Vector2(center.X - screenPos.X + base.NPC.scale * 110f, center.Y - screenPos.Y + 50f), (Rectangle?)new Rectangle(0, 0, TextureAssets.Npc[ModContent.NPCType<RavagerClawRight>()].Value.Width, TextureAssets.Npc[ModContent.NPCType<RavagerClawRight>()].Value.Height), Color.White, 0f, default(Vector2), base.NPC.scale, (SpriteEffects)0, 0f);
		}
		return true;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Vector2 center = default(Vector2);
		((Vector2)(ref center))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 glowmaskPosition = center - screenPos;
		glowmaskPosition -= new Vector2((float)GlowTexture.Value.Width, (float)(GlowTexture.Value.Height / Main.npcFrameCount[base.Type])) * 1f / 2f;
		glowmaskPosition += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
		Color glowmaskColor = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.Blue);
		spriteBatch.Draw(GlowTexture.Value, glowmaskPosition, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		float legOffset = 20f;
		float headOffset = 75f;
		Color drawLighting = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
		if (base.NPC.IsABestiaryIconDummy)
		{
			drawLighting = Color.White;
			legOffset = 60f;
			headOffset = 0f;
		}
		spriteBatch.Draw(TextureAssets.Npc[ModContent.NPCType<RavagerLegRight>()].Value, new Vector2(center.X - screenPos.X + base.NPC.scale * 28f, center.Y - screenPos.Y + legOffset), (Rectangle?)new Rectangle(0, 0, TextureAssets.Npc[ModContent.NPCType<RavagerLegRight>()].Value.Width, TextureAssets.Npc[ModContent.NPCType<RavagerLegRight>()].Value.Height), drawLighting, 0f, default(Vector2), base.NPC.scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(TextureAssets.Npc[ModContent.NPCType<RavagerLegLeft>()].Value, new Vector2(center.X - screenPos.X - base.NPC.scale * 112f, center.Y - screenPos.Y + legOffset), (Rectangle?)new Rectangle(0, 0, TextureAssets.Npc[ModContent.NPCType<RavagerLegLeft>()].Value.Width, TextureAssets.Npc[ModContent.NPCType<RavagerLegLeft>()].Value.Height), drawLighting, 0f, default(Vector2), base.NPC.scale, (SpriteEffects)0, 0f);
		if (NPC.AnyNPCs(ModContent.NPCType<RavagerHead>()) || base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.Draw(TextureAssets.Npc[ModContent.NPCType<RavagerHead>()].Value, new Vector2(center.X - screenPos.X - base.NPC.scale * 70f, center.Y - screenPos.Y - base.NPC.scale * headOffset), (Rectangle?)new Rectangle(0, 0, TextureAssets.Npc[ModContent.NPCType<RavagerHead>()].Value.Width, TextureAssets.Npc[ModContent.NPCType<RavagerHead>()].Value.Height), drawLighting, 0f, default(Vector2), base.NPC.scale, (SpriteEffects)0, 0f);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 2f);
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerBody").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerBody2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerBody3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerBody4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerBody5").Type);
			}
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 2f);
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, hit.HitDirection, -1f);
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcCenter = base.NPC.Center;
		Rectangle leftHitbox = default(Rectangle);
		((Rectangle)(ref leftHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 2f + 8f), (int)(npcCenter.Y - (float)base.NPC.height / 4f), base.NPC.width / 4, base.NPC.height / 2);
		Rectangle bodyHitbox = default(Rectangle);
		((Rectangle)(ref bodyHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 4f), (int)(npcCenter.Y - (float)base.NPC.height / 2f + 8f), base.NPC.width / 2, base.NPC.height);
		Rectangle rightHitbox = default(Rectangle);
		((Rectangle)(ref rightHitbox))._002Ector((int)(npcCenter.X + (float)base.NPC.width / 4f - 8f), (int)(npcCenter.Y - (float)base.NPC.height / 4f), base.NPC.width / 4, base.NPC.height / 2);
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
		bool num = minLeftDist <= 55f;
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
		bool insideBodyHitbox = minBodyDist <= 110f;
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
		bool insideRightHitbox = minRightDist <= 55f;
		return num | insideBodyHitbox | insideRightHitbox;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 480);
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
			DownedBossSystem.downedRavager = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RavagerBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[5]
		{
			ModContent.ItemType<UltimusCleaver>(),
			ModContent.ItemType<RealmRavager>(),
			ModContent.ItemType<Hematemesis>(),
			ModContent.ItemType<SpikecragStaff>(),
			ModContent.ItemType<CraniumSmasher>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<Vesuvius>(), 10);
		normalOnly.Add(ModContent.ItemType<CorpusAvertor>(), 20);
		normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(() => !DownedBossSystem.downedProvidence), ModContent.ItemType<FleshyGeode>()), DownedBossSystem.downedProvidence);
		normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(() => DownedBossSystem.downedProvidence), ModContent.ItemType<NecromanticGeode>()), !DownedBossSystem.downedProvidence);
		normalOnly.Add(ModContent.ItemType<FleshTotem>(), 3);
		normalOnly.Add(ItemDropRule.ByCondition(DropHelper.PostProv(), ModContent.ItemType<BloodflareCore>()));
		normalOnly.Add(ModContent.ItemType<RavagerMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<RavagerTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<RavagerRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(1274), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(154, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4025, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<AncientBoneDust>(), 1, 1, 9999), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedRavager, ModContent.ItemType<LoreRavager>(), ui: true, DropHelper.FirstKillText);
	}
}
