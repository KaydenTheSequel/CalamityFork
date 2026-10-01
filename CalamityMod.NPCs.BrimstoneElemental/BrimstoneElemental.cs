using System;
using System.IO;
using CalamityMod.BiomeManagers;
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
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.BrimstoneElemental;

[AutoloadBossHead]
public class BrimstoneElemental : ModNPC
{
	public enum Elemental
	{
		Brimstone,
		Sand,
		Rare,
		Cloud,
		Water
	}

	public int currentMode;

	public static readonly SoundStyle TeleportSound = new SoundStyle("CalamityMod/Sounds/Custom/BrimstoneElemental/Teleport");

	public static readonly SoundStyle HellfireballSound = new SoundStyle("CalamityMod/Sounds/Custom/BrimstoneElemental/Hellfireball", 3);

	public static readonly SoundStyle DartSound = new SoundStyle("CalamityMod/Sounds/Custom/BrimstoneElemental/BrimstoneDartRing", 3);

	public static readonly SoundStyle HideInShellSound = new SoundStyle("CalamityMod/Sounds/Custom/BrimstoneElemental/ShellTransform");

	public static readonly SoundStyle ShellFireSound = new SoundStyle("CalamityMod/Sounds/Custom/BrimstoneElemental/ShellProjectiles", 3);

	public static int DartDamage = 19;

	public static int HellblastDamage = 24;

	public static int HellfireballDamage = 24;

	public static int RayDamage = 35;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 12;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.64f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y -= 24f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 64f;
		base.NPC.damage = 56;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.width = 100;
		base.NPC.height = 150;
		base.NPC.defense = 15;
		base.NPC.value = Item.buyPrice(0, 12);
		base.NPC.LifeMaxNERB(30000, 49200, 500000);
		base.NPC.DR_NERD(0.2f);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.HitSound = SoundID.NPCHit23;
		base.NPC.DeathSound = SoundID.NPCDeath39;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BrimstoneElemental")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(currentMode);
		writer.Write(base.NPC.chaseable);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		currentMode = reader.ReadInt32();
		base.NPC.chaseable = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1022: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18de: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_1542: Unknown result type (might be due to invalid IL or missing references)
		//IL_1547: Unknown result type (might be due to invalid IL or missing references)
		//IL_154c: Unknown result type (might be due to invalid IL or missing references)
		//IL_155c: Unknown result type (might be due to invalid IL or missing references)
		//IL_155e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1563: Unknown result type (might be due to invalid IL or missing references)
		//IL_156a: Unknown result type (might be due to invalid IL or missing references)
		//IL_156f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1571: Unknown result type (might be due to invalid IL or missing references)
		//IL_1573: Unknown result type (might be due to invalid IL or missing references)
		//IL_1575: Unknown result type (might be due to invalid IL or missing references)
		//IL_1577: Unknown result type (might be due to invalid IL or missing references)
		//IL_157c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1581: Unknown result type (might be due to invalid IL or missing references)
		//IL_1583: Unknown result type (might be due to invalid IL or missing references)
		//IL_1587: Unknown result type (might be due to invalid IL or missing references)
		//IL_158c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1abe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_169b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15db: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1601: Unknown result type (might be due to invalid IL or missing references)
		//IL_1603: Unknown result type (might be due to invalid IL or missing references)
		//IL_1608: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_170f: Unknown result type (might be due to invalid IL or missing references)
		//IL_116f: Unknown result type (might be due to invalid IL or missing references)
		//IL_117a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1156: Unknown result type (might be due to invalid IL or missing references)
		//IL_1161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1723: Unknown result type (might be due to invalid IL or missing references)
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_118c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1191: Unknown result type (might be due to invalid IL or missing references)
		//IL_1196: Unknown result type (might be due to invalid IL or missing references)
		//IL_119b: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11de: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_205e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2072: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_174a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1750: Unknown result type (might be due to invalid IL or missing references)
		//IL_1752: Unknown result type (might be due to invalid IL or missing references)
		//IL_1757: Unknown result type (might be due to invalid IL or missing references)
		//IL_176b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1770: Unknown result type (might be due to invalid IL or missing references)
		//IL_1772: Unknown result type (might be due to invalid IL or missing references)
		//IL_1777: Unknown result type (might be due to invalid IL or missing references)
		//IL_1781: Unknown result type (might be due to invalid IL or missing references)
		//IL_1786: Unknown result type (might be due to invalid IL or missing references)
		//IL_178b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2091: Unknown result type (might be due to invalid IL or missing references)
		//IL_2098: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_129c: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1283: Unknown result type (might be due to invalid IL or missing references)
		//IL_128e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2164: Unknown result type (might be due to invalid IL or missing references)
		//IL_216b: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12be: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1806: Unknown result type (might be due to invalid IL or missing references)
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_180e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1813: Unknown result type (might be due to invalid IL or missing references)
		//IL_1827: Unknown result type (might be due to invalid IL or missing references)
		//IL_182c: Unknown result type (might be due to invalid IL or missing references)
		//IL_182e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1833: Unknown result type (might be due to invalid IL or missing references)
		//IL_183d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1842: Unknown result type (might be due to invalid IL or missing references)
		//IL_1847: Unknown result type (might be due to invalid IL or missing references)
		//IL_184e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1326: Unknown result type (might be due to invalid IL or missing references)
		//IL_133e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1344: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_135f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1364: Unknown result type (might be due to invalid IL or missing references)
		//IL_1366: Unknown result type (might be due to invalid IL or missing references)
		//IL_136b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1375: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_137f: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.brimstoneElemental = base.NPC.whoAmI;
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1.2f, 0f, 0f);
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool despawnDistance = Vector2.Distance(player.Center, base.NPC.Center) > 5600f;
		if ((!player.active || player.dead) | despawnDistance)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if ((!player.active || player.dead) | despawnDistance)
			{
				base.NPC.rotation = base.NPC.velocity.X * 0.04f;
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
				if (base.NPC.ai[0] != 0f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.localAI[0] = 0f;
					base.NPC.localAI[1] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		base.NPC.defense = base.NPC.defDefense;
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = base.NPC.ai[0] == 4f;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase2 = (lifeRatio < 0.5f) & revenge;
		bool phase3 = lifeRatio < 0.33f;
		if ((!player.ZoneUnderworldHeight || !player.Calamity().ZoneCalamity) && !BossRushEvent.BossRushActive)
		{
			if (calamityGlobalNPC.newAI[3] > 0f)
			{
				calamityGlobalNPC.newAI[3]--;
			}
		}
		else
		{
			calamityGlobalNPC.newAI[3] = 300f;
		}
		bool num = calamityGlobalNPC.newAI[3] <= 0f;
		float enrageScale = 0f;
		if (num && !player.ZoneUnderworldHeight)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 0.5f;
		}
		if (num && !player.Calamity().ZoneCalamity)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale++;
		}
		base.NPC.Calamity().DR = ((base.NPC.ai[0] == 4f) ? 0.6f : 0.2f);
		int dustAmt = ((base.NPC.ai[0] != 2f) ? 1 : 2);
		int size = ((base.NPC.ai[0] == 2f) ? 50 : 35);
		if (base.NPC.ai[0] != 1f)
		{
			for (int i = 0; i < 2; i++)
			{
				if (Main.rand.Next(3) < dustAmt)
				{
					int dust = Dust.NewDust(base.NPC.Center - new Vector2((float)size), size * 2, size * 2, 235, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Dust obj = Main.dust[dust];
					obj.velocity *= 0.2f;
					Main.dust[dust].fadeIn = 1f;
				}
			}
		}
		float movementDistanceGateValue = 100f;
		float baseVelocity = (death ? 6f : (revenge ? 5.5f : (expertMode ? 5f : 4.5f))) * ((base.NPC.ai[0] == 5f) ? 0.05f : ((base.NPC.ai[0] == 3f) ? 1.5f : 1f));
		baseVelocity += 3f * enrageScale;
		if (expertMode)
		{
			baseVelocity += (death ? (3f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
		}
		float baseAcceleration = (death ? 0.12f : 0.1f) * ((base.NPC.ai[0] == 5f) ? 0.5f : ((base.NPC.ai[0] == 3f) ? 1.5f : 1f));
		baseAcceleration += 0.06f * enrageScale;
		if (expertMode)
		{
			baseAcceleration += 0.03f * (1f - lifeRatio);
		}
		Vector2 distanceFromDestination = (Vector2)((base.NPC.ai[0] != 3f) ? player.Center : new Vector2(player.Center.X, player.Center.Y - 300f)) - base.NPC.Center;
		if (base.NPC.ai[0] != 4f)
		{
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, baseAcceleration, useSimpleFlyMovement: true);
		}
		if (base.NPC.ai[0] <= 2f || base.NPC.ai[0] == 5f)
		{
			base.NPC.rotation = base.NPC.velocity.X * 0.04f;
			if (base.NPC.ai[0] != 5f || (base.NPC.ai[1] < 180f && base.NPC.ai[0] == 5f))
			{
				float playerLocation = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
		}
		if (Main.zenithWorld)
		{
			int newMode = ((lifeRatio <= 0.8f && lifeRatio > 0.6f) ? 1 : ((lifeRatio <= 0.6f && lifeRatio > 0.4f) ? 2 : ((lifeRatio <= 0.4f && lifeRatio > 0.2f) ? 3 : ((lifeRatio <= 0.2f) ? 4 : 0))));
			if (newMode != currentMode)
			{
				SoundEngine.PlaySound(in SoundID.Item29, base.NPC.Center);
			}
			currentMode = newMode;
		}
		if (base.NPC.ai[0] == -1f)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			int random = (phase2 ? 6 : 5);
			int phase4;
			while (true)
			{
				phase4 = Main.rand.Next(random);
				if ((float)phase4 == base.NPC.ai[1] || ((phase4 == 0) & phase3 & revenge))
				{
					continue;
				}
				switch (phase4)
				{
				case 1:
				case 2:
					continue;
				case 4:
					if (base.NPC.localAI[3] != 0f)
					{
						continue;
					}
					break;
				}
				break;
			}
			base.NPC.ai[0] = phase4;
			base.NPC.ai[1] = 0f;
			if (base.NPC.localAI[3] > 0f)
			{
				base.NPC.localAI[3]--;
			}
			else if (phase4 == 4)
			{
				base.NPC.localAI[3] = 3f;
				SoundEngine.PlaySound(in HideInShellSound, player.Center);
			}
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		else if (base.NPC.ai[0] == 0f)
		{
			base.NPC.chaseable = true;
			if (Main.netMode == 1)
			{
				return;
			}
			base.NPC.localAI[1]++;
			if (!(base.NPC.localAI[1] >= (death ? 120f : 180f)))
			{
				return;
			}
			base.NPC.TargetClosest();
			base.NPC.localAI[1] = 0f;
			int timer = 0;
			int playerPosX;
			int playerPosY;
			while (true)
			{
				timer++;
				playerPosX = (int)player.Center.X / 16;
				playerPosY = (int)player.Center.Y / 16;
				int min = 12;
				int max = 16;
				playerPosX = ((!Main.rand.NextBool()) ? (playerPosX - Main.rand.Next(min, max)) : (playerPosX + Main.rand.Next(min, max)));
				playerPosY = ((!Main.rand.NextBool()) ? (playerPosY - Main.rand.Next(min, max)) : (playerPosY + Main.rand.Next(min, max)));
				if (!WorldGen.SolidTile(playerPosX, playerPosY))
				{
					break;
				}
				if (timer > 100)
				{
					return;
				}
			}
			base.NPC.ai[0] = 1f;
			base.NPC.ai[1] = playerPosX;
			base.NPC.ai[2] = playerPosY;
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			base.NPC.chaseable = true;
			Vector2 position = default(Vector2);
			((Vector2)(ref position))._002Ector(base.NPC.ai[1] * 16f - (float)(base.NPC.width / 2), base.NPC.ai[2] * 16f - (float)(base.NPC.height / 2));
			for (int m = 0; m < 5; m++)
			{
				int dust2 = Dust.NewDust(position, base.NPC.width, base.NPC.height, 235, 0f, -1f, 90, default(Color), 2f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].fadeIn = 1f;
			}
			base.NPC.alpha += (death ? 5 : (revenge ? 4 : (expertMode ? 3 : 2)));
			if (base.NPC.alpha < 255)
			{
				return;
			}
			int spawnType = ((currentMode == 3) ? 250 : ModContent.NPCType<Brimling>());
			int enemyCount = ((currentMode != 3) ? 1 : 3);
			if (((Main.netMode != 1 && NPC.CountNPCS(spawnType) < (death ? 1 : 2)) & revenge) && currentMode != 2)
			{
				for (int j = 0; j < enemyCount; j++)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, spawnType);
				}
			}
			SoundEngine.PlaySound(in TeleportSound, base.NPC.Center);
			base.NPC.alpha = 255;
			base.NPC.position = position;
			for (int n = 0; n < 15; n++)
			{
				int warpDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, -1f, 90, default(Color), 3f);
				Main.dust[warpDust].noGravity = true;
			}
			base.NPC.ai[0] = 2f;
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.ai[0] == 2f)
		{
			if (base.NPC.alpha >= 255 && Main.zenithWorld)
			{
				SoundEngine.PlaySound(in SoundID.Item68, base.NPC.Center);
				int type = ModContent.ProjectileType<BrimstoneRay>();
				int damage = RayDamage;
				Vector2 pos = base.NPC.Center;
				for (int k = 0; k < 4; k++)
				{
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), pos, Vector2.UnitY.RotatedBy(MathHelper.Lerp(0f, (float)Math.PI * 2f, (float)k / 4f)), type, damage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
				}
				if (currentMode >= 1 && currentMode <= 3)
				{
					int tornadoType = ((currentMode == 3) ? ModContent.ProjectileType<StormMarkHostile>() : 658);
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), pos, Vector2.Zero, tornadoType, damage, 0f, Main.myPlayer);
					}
				}
				if (currentMode == 2)
				{
					int healAmt = base.NPC.lifeMax / 25;
					if (healAmt > 0)
					{
						base.NPC.life += healAmt;
						base.NPC.HealEffect(healAmt);
						base.NPC.netUpdate = true;
					}
				}
			}
			base.NPC.alpha -= 50;
			if (base.NPC.alpha <= 0)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.chaseable = true;
				base.NPC.ai[3]++;
				base.NPC.alpha = 0;
				if (((base.NPC.ai[3] >= 2f) | phase2) || Main.getGoodWorld)
				{
					base.NPC.ai[0] = -1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
				}
				else
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.chaseable = true;
			base.NPC.rotation = base.NPC.velocity.X * 0.04f;
			float playerLocation2 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.ai[1]++;
			float divisor = (expertMode ? ((death ? 80f : (revenge ? 45f : 50f)) - (float)Math.Ceiling(10f * (1f - lifeRatio))) : 50f);
			divisor -= 3f * enrageScale;
			float divisor2 = divisor * 2f;
			if (base.NPC.ai[1] % divisor == divisor - 1f)
			{
				float velocity = (death ? 7f : (revenge ? 6f : 5f)) + 2f * enrageScale + (expertMode ? (3f * (1f - lifeRatio)) : 0f);
				int type2 = ModContent.ProjectileType<BrimstoneHellfireball>();
				int damage2 = HellfireballDamage;
				if (currentMode == 4)
				{
					type2 = ModContent.ProjectileType<FrostMist>();
					SoundEngine.PlaySound(in SoundID.Item30, player.Center);
				}
				else
				{
					SoundEngine.PlaySound(in HellfireballSound, player.Center);
				}
				Vector2 projectileVelocity = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * velocity;
				if (Main.netMode != 1)
				{
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * 5f, projectileVelocity, type2, damage2, 0f, Main.myPlayer, player.position.X, player.position.Y);
					Main.projectile[proj].timeLeft = 240;
				}
				if (base.NPC.ai[1] % divisor2 == divisor2 - 1f)
				{
					velocity = (death ? 5f : 4f) + 2f * enrageScale;
					type2 = ModContent.ProjectileType<BrimstoneBarrage>();
					damage2 = DartDamage;
					if (currentMode == 4)
					{
						type2 = ModContent.ProjectileType<WaterSpear>();
						SoundEngine.PlaySound(in SoundID.Item21, player.Center);
					}
					else
					{
						SoundEngine.PlaySound(in DartSound, player.Center);
					}
					projectileVelocity = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * velocity;
					int numProj = (death ? 8 : 4);
					int spread = (death ? 90 : 45);
					if (Main.getGoodWorld)
					{
						numProj *= 3;
						spread *= 2;
					}
					if (Main.netMode != 1)
					{
						float rotation = MathHelper.ToRadians((float)spread);
						float projectileVelocityToPass = velocity * 3f;
						for (int l = 0; l < numProj; l++)
						{
							Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)l / (float)(numProj - 1)));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 5f, perturbedSpeed, type2, damage2, 0f, Main.myPlayer, 1f, 0f, projectileVelocityToPass);
						}
					}
				}
			}
			if (base.NPC.ai[1] >= divisor * (death ? 5f : 10f))
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 3f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.defense = base.NPC.defDefense * 4;
			base.NPC.chaseable = false;
			base.NPC.localAI[0]++;
			if (Main.getGoodWorld)
			{
				base.NPC.localAI[0] += 2f;
			}
			if (expertMode)
			{
				base.NPC.localAI[0] += 1f - lifeRatio;
			}
			base.NPC.localAI[0] += enrageScale;
			if (base.NPC.localAI[0] >= 120f)
			{
				base.NPC.localAI[0] = 0f;
				float projectileSpeed = (death ? 9f : (revenge ? 8f : 6f));
				projectileSpeed += 2f * enrageScale;
				Vector2 projectileVelocity2 = player.Center - base.NPC.Center;
				float radialOffset = 0.2f;
				float diameter = 80f;
				projectileVelocity2 = projectileVelocity2.SafeNormalize(Vector2.UnitY) * projectileSpeed;
				Vector2 velocity2 = projectileVelocity2;
				velocity2 = velocity2.SafeNormalize(Vector2.UnitY);
				velocity2 *= diameter;
				int totalProjectiles = 6;
				float offsetAngle = (float)Math.PI * radialOffset;
				int type3 = ModContent.ProjectileType<BrimstoneHellblast>();
				int damage3 = HellblastDamage;
				if (Main.netMode != 1)
				{
					for (int num2 = 0; num2 < totalProjectiles; num2++)
					{
						float radians = (float)num2 - ((float)totalProjectiles - 1f) / 2f;
						Vector2 offset = velocity2.RotatedBy(offsetAngle * radians);
						int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset, projectileVelocity2, type3, damage3, 0f, Main.myPlayer, 1f);
						Main.projectile[proj2].timeLeft = 300;
						Main.projectile[proj2].tileCollide = false;
					}
				}
				totalProjectiles = 12;
				float radians2 = (float)Math.PI * 2f / (float)totalProjectiles;
				type3 = ModContent.ProjectileType<BrimstoneBarrage>();
				damage3 = DartDamage;
				if (currentMode == 4)
				{
					type3 = ModContent.ProjectileType<SirenSong>();
					SoundEngine.PlaySound(in SoundID.Item26, player.Center);
				}
				else
				{
					SoundEngine.PlaySound(in ShellFireSound, player.Center);
				}
				double angleA = (double)radians2 * 0.5;
				double angleB = (double)MathHelper.ToRadians(90f) - angleA;
				float velocityX = (float)((double)projectileSpeed * Math.Sin(angleA) / Math.Sin(angleB));
				Vector2 spinningPoint = ((base.NPC.localAI[2] % 2f == 0f) ? new Vector2(0f, 0f - projectileSpeed) : new Vector2(0f - velocityX, 0f - projectileSpeed));
				if (Main.netMode != 1)
				{
					float projectileVelocityToPass2 = projectileSpeed * 3f;
					for (int num3 = 0; num3 < totalProjectiles; num3++)
					{
						Vector2 vector255 = spinningPoint.RotatedBy(radians2 * (float)num3);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + vector255.SafeNormalize(Vector2.UnitY) * 5f, vector255, type3, damage3, 0f, Main.myPlayer, 1f, 0f, projectileVelocityToPass2);
					}
					if (death)
					{
						spinningPoint = ((base.NPC.localAI[2] % 2f == 0f) ? new Vector2(0f - velocityX, 0f - projectileSpeed) : new Vector2(0f, 0f - projectileSpeed));
						for (int num4 = 0; num4 < totalProjectiles; num4++)
						{
							Vector2 vector256 = spinningPoint.RotatedBy(radians2 * (float)num4);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + vector256.SafeNormalize(Vector2.UnitY) * 5f, vector256 * 0.75f, type3, damage3, 0f, Main.myPlayer, 1f, 0f, projectileVelocityToPass2);
						}
					}
				}
				base.NPC.localAI[2]++;
			}
			NPC nPC = base.NPC;
			nPC.velocity *= 0.95f;
			base.NPC.rotation = base.NPC.velocity.X * 0.04f;
			float playerLocation3 = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (death ? 240f : 300f))
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 4f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 5f)
			{
				return;
			}
			base.NPC.chaseable = true;
			base.NPC.defense = base.NPC.defDefense * 2;
			Vector2 source = default(Vector2);
			((Vector2)(ref source))._002Ector(base.NPC.Center.X + ((base.NPC.spriteDirection > 0) ? 34f : (-34f)), base.NPC.Center.Y - 74f);
			Vector2 val = player.Center + player.velocity * 20f;
			float aimResponsiveness = (((base.NPC.ai[2] == 1f) | death) ? 0.1f : 0.25f);
			Vector2 aimVector = (val - source).SafeNormalize(Vector2.UnitY);
			if (aimVector.HasNaNs())
			{
				aimVector = -Vector2.UnitY;
			}
			aimVector = Vector2.Lerp(aimVector, base.NPC.velocity.SafeNormalize(Vector2.UnitY), aimResponsiveness).SafeNormalize(Vector2.UnitY);
			aimVector *= 6f;
			Vector2 laserVelocity = aimVector.SafeNormalize(Vector2.UnitY);
			if (laserVelocity.HasNaNs())
			{
				laserVelocity = -Vector2.UnitY;
			}
			calamityGlobalNPC.newAI[1] = laserVelocity.X;
			calamityGlobalNPC.newAI[2] = laserVelocity.Y;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 240f)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[2]++;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				if (base.NPC.ai[2] >= (death ? 1f : 2f))
				{
					base.NPC.ai[0] = -1f;
					base.NPC.ai[1] = 5f;
					base.NPC.ai[2] = 0f;
					calamityGlobalNPC.newAI[0] = 0f;
				}
				else
				{
					base.NPC.ai[1] = 0f;
					calamityGlobalNPC.newAI[0] = 0f;
				}
				return;
			}
			if (base.NPC.ai[1] >= 180f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.95f;
				if (base.NPC.ai[1] != 180f)
				{
					return;
				}
				SoundEngine.PlaySound(in SoundID.Item68, source);
				if (Main.netMode != 1)
				{
					Vector2 laserVelocity2 = default(Vector2);
					((Vector2)(ref laserVelocity2))._002Ector(base.NPC.localAI[0], base.NPC.localAI[1]);
					laserVelocity2 = laserVelocity2.SafeNormalize(Vector2.UnitY);
					int type4 = ModContent.ProjectileType<BrimstoneRay>();
					int damage4 = RayDamage;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, laserVelocity2, type4, damage4, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					if (Main.getGoodWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, -laserVelocity2, type4, damage4, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
					if (Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, new Vector2(0f - laserVelocity2.X, laserVelocity2.Y), type4, damage4, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, new Vector2(laserVelocity2.X, 0f - laserVelocity2.Y), type4, damage4, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
				}
				return;
			}
			float playSoundTimer = 30f;
			if (base.NPC.ai[1] < 150f)
			{
				switch ((int)base.NPC.ai[2])
				{
				case 0:
					base.NPC.ai[1] += 0.5f;
					break;
				case 1:
					base.NPC.ai[1]++;
					playSoundTimer = 40f;
					break;
				}
				if (death)
				{
					base.NPC.ai[1] += 0.5f;
					playSoundTimer += 10f;
				}
			}
			if (base.NPC.ai[1] % playSoundTimer == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item20, base.NPC.Center);
			}
			if (base.NPC.ai[1] < 150f && calamityGlobalNPC.newAI[0] == 0f)
			{
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, laserVelocity, ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					if (Main.getGoodWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, -laserVelocity, ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
					if (Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, new Vector2(0f - laserVelocity.X, laserVelocity.Y), ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, new Vector2(laserVelocity.X, 0f - laserVelocity.Y), ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
				}
				calamityGlobalNPC.newAI[0] = 1f;
			}
			else
			{
				if (base.NPC.ai[1] != 150f)
				{
					return;
				}
				base.NPC.localAI[0] = laserVelocity.X;
				base.NPC.localAI[1] = laserVelocity.Y;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source.X, source.Y, base.NPC.localAI[0], base.NPC.localAI[1], ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 1f, base.NPC.whoAmI);
					if (Main.getGoodWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source.X, source.Y, 0f - base.NPC.localAI[0], 0f - base.NPC.localAI[1], ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 1f, base.NPC.whoAmI);
					}
					if (Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source.X, source.Y, 0f - base.NPC.localAI[0], base.NPC.localAI[1], ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 1f, base.NPC.whoAmI);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source.X, source.Y, base.NPC.localAI[0], 0f - base.NPC.localAI[1], ModContent.ProjectileType<BrimstoneTargetRay>(), 0, 0f, Main.myPlayer, 1f, base.NPC.whoAmI);
					}
				}
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.ai[0] <= 2f)
		{
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 4)
			{
				base.NPC.frame.Y = 0;
			}
		}
		else if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 5f)
		{
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y < frameHeight * 4)
			{
				base.NPC.frame.Y = frameHeight * 4;
			}
			if (base.NPC.frame.Y >= frameHeight * 8)
			{
				base.NPC.frame.Y = frameHeight * 4;
			}
		}
		else
		{
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y < frameHeight * 8)
			{
				base.NPC.frame.Y = frameHeight * 8;
			}
			if (base.NPC.frame.Y >= frameHeight * 12)
			{
				base.NPC.frame.Y = frameHeight * 8;
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BrimstoneElementalBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[4]
		{
			ModContent.ItemType<Brimlance>(),
			ModContent.ItemType<SeethingDischarge>(),
			ModContent.ItemType<DormantBrimseeker>(),
			ModContent.ItemType<Hellborn>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<EssenceofHavoc>(), 1, 8, 10);
		int[] accs = new int[1] { ModContent.ItemType<RoseStone>() };
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, accs));
		normalOnly.Add(ModContent.ItemType<BrimstoneElementalMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<BrimstoneElementalTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<BrimstoneElementalRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<HeartoftheElements>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedBrimstoneElemental, ModContent.ItemType<LoreAzafure>(), ui: true, DropHelper.FirstKillText);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedBrimstoneElemental, ModContent.ItemType<LoreBrimstoneElemental>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedBrimstoneElemental = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 200;
		base.NPC.height = 150;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int brimDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[brimDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[brimDust].scale = 0.5f;
				Main.dust[brimDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[brimDust2].noGravity = true;
			Dust obj2 = Main.dust[brimDust2];
			obj2.velocity *= 5f;
			brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[brimDust2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("BrimstoneGore1").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("BrimstoneGore2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("BrimstoneGore3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("BrimstoneGore4").Type);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D npcTexture = TextureAssets.Npc[base.Type].Value;
		Vector2 frameLocation = default(Vector2);
		((Vector2)(ref frameLocation))._002Ector((float)(npcTexture.Width / 2), (float)(npcTexture.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 npcOffset = base.NPC.Center - screenPos;
		npcOffset -= new Vector2((float)npcTexture.Width, (float)(npcTexture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		npcOffset += frameLocation * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		if (Main.zenithWorld)
		{
			Color baseColor = Color.Red;
			switch (currentMode)
			{
			case 0:
				baseColor = Color.Red;
				break;
			case 1:
				baseColor = Color.Tan;
				break;
			case 2:
				baseColor = Color.Lime;
				break;
			case 3:
				baseColor = Color.Gray;
				break;
			case 4:
				baseColor = Color.Blue;
				break;
			}
			spriteBatch.EnterShaderRegion();
			Color outlineColor = Color.Lerp(baseColor, Color.White, 0.4f);
			outlineColor *= base.NPC.Opacity;
			Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
			float outlineThickness = MathHelper.Clamp(2f, 0f, 3f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - outlineHSL.X, outlineHSL.Y, outlineHSL.Z));
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
			for (float i = 0f; i < 1f; i += 0.125f)
			{
				spriteBatch.Draw(npcTexture, npcOffset + (i * ((float)Math.PI * 2f)).ToRotationVector2() * outlineThickness, (Rectangle?)base.NPC.frame, outlineColor, base.NPC.rotation, frameLocation, base.NPC.scale, spriteEffects, 0f);
			}
			spriteBatch.ExitShaderRegion();
		}
		spriteBatch.Draw(npcTexture, npcOffset, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, frameLocation, base.NPC.scale, spriteEffects, 0f);
		return false;
	}
}
