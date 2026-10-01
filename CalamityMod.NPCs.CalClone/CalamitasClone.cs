using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Graphics.Metaballs;
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
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Systems.Mechanic;
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

namespace CalamityMod.NPCs.CalClone;

[AutoloadBossHead]
public class CalamitasClone : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public static readonly SoundStyle BulletHellWarning = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/BulletHellEnding");

	public static readonly SoundStyle BulletHellEnd = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/BulletHellEnd");

	public static readonly SoundStyle ChargeSound = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/CalCloneDash", 3);

	public static readonly SoundStyle CalamitousFireballSound = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/CalClone_BigFireballBit", 4)
	{
		MaxInstances = 4
	};

	public static readonly SoundStyle CalamitousExplosionSound = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/CalClone_Explosion", 3)
	{
		MaxInstances = 4
	};

	public SlotId BulletHellWarnSlot;

	public ArenaWallSystem.Box ArenaBox;

	public static int DartDamage = 22;

	public static int HellblastDamage = 25;

	public static int HellfireballDamage = 25;

	public static int FireblastDamage = 35;

	private void UpdateArena(ArenaWallSystem.Box box)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		if (!(box.borderColor == Color.Gray) && !(box.oldData.borderColor == Color.Gray))
		{
			for (int i2 = 0; (float)i2 < box.Size.Y / 400f; i2++)
			{
				Vector2 p = Vector2.Lerp(box.BottomRight, box.TopRight, Main.rand.NextFloat());
				Dust.NewDustPerfect(p, 114, p.DirectionFrom(box.Center) * Main.rand.NextFloat(0f, 5f), 0, Scale: Main.rand.NextFloat(0.1f, 1f), newColor: box.borderColor);
				p = Vector2.Lerp(box.TopLeft, box.BottomLeft, Main.rand.NextFloat());
				Dust.NewDustPerfect(p, 114, p.DirectionFrom(box.Center) * Main.rand.NextFloat(0f, 5f), 0, Scale: Main.rand.NextFloat(0.1f, 1f), newColor: box.borderColor);
			}
			for (int j = 0; (float)j < box.Size.X / 400f; j++)
			{
				Vector2 p2 = Vector2.Lerp(box.TopLeft, box.TopRight, Main.rand.NextFloat());
				Dust.NewDustPerfect(p2, 114, p2.DirectionFrom(box.Center) * Main.rand.NextFloat(0f, 5f), 0, Scale: Main.rand.NextFloat(0.1f, 1f), newColor: box.borderColor);
				p2 = Vector2.Lerp(box.BottomRight, box.BottomLeft, Main.rand.NextFloat());
				Dust.NewDustPerfect(p2, 114, p2.DirectionFrom(box.Center) * Main.rand.NextFloat(0f, 5f), 0, Scale: Main.rand.NextFloat(0.1f, 1f), newColor: box.borderColor);
			}
		}
	}

	private void DrawArena(ArenaWallSystem.Box box)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		_ = Color.Black * 0.75f;
		box.DrawBoxWithOffset(box.borderThickness * 0.5f, box.borderThickness, Color.Black * 0.75f);
		box.DrawBoxWithOffset(4f, 8f, box.borderColor);
		float amount = 4f;
		float totalDistance = 64f;
		for (float i = Main.GlobalTimeWrappedHourly % 1f; i < amount; i++)
		{
			box.DrawBoxWithOffset(totalDistance * (i / amount) + 4f, 4f, box.borderColor * (1f - i / amount));
		}
		box.DrawBoxWithOffset(box.borderThickness - 4f, 4f, box.borderColor);
	}

	public static Vector4 GetArenaSize(bool brothersActive = false, float lifeRatio = 0f, bool inBulletHell = false)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Vector4 baseSize = default(Vector4);
		((Vector4)(ref baseSize))._002Ector(1600f, 800f, 0f, 800f);
		if (brothersActive)
		{
			baseSize *= 1.25f;
		}
		if (NPC.AnyNPCs(ModContent.NPCType<SoulSeeker>()))
		{
			baseSize *= new Vector4(1.5f, 0.75f, 1.5f, 0.75f);
		}
		if (!CalamityWorld.death)
		{
			baseSize *= 1.25f;
		}
		if (lifeRatio < 0.1f && !inBulletHell && CalamityWorld.death)
		{
			baseSize *= MathHelper.Lerp(Main.getGoodWorld ? 0.22f : 0.4f, 1f, lifeRatio * 10f);
		}
		return baseSize + new Vector4(-22f, 0f, 22f, 0f);
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.65f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y -= 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 70;
		base.NPC.npcSlots = 14f;
		base.NPC.width = 120;
		base.NPC.height = 120;
		base.NPC.defense = 25;
		base.NPC.value = Item.buyPrice(0, 15);
		base.NPC.LifeMaxNERB(30000, 46875, 520000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Calamitas")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1623: Unknown result type (might be due to invalid IL or missing references)
		//IL_162e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1563: Unknown result type (might be due to invalid IL or missing references)
		//IL_156e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1573: Unknown result type (might be due to invalid IL or missing references)
		//IL_158b: Unknown result type (might be due to invalid IL or missing references)
		//IL_221c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2227: Unknown result type (might be due to invalid IL or missing references)
		//IL_222c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2231: Unknown result type (might be due to invalid IL or missing references)
		//IL_2236: Unknown result type (might be due to invalid IL or missing references)
		//IL_223e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2242: Unknown result type (might be due to invalid IL or missing references)
		//IL_2247: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2304: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f41: Unknown result type (might be due to invalid IL or missing references)
		//IL_23bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_240a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2418: Unknown result type (might be due to invalid IL or missing references)
		//IL_2422: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fea: Unknown result type (might be due to invalid IL or missing references)
		//IL_2073: Unknown result type (might be due to invalid IL or missing references)
		//IL_2078: Unknown result type (might be due to invalid IL or missing references)
		//IL_207a: Unknown result type (might be due to invalid IL or missing references)
		//IL_207f: Unknown result type (might be due to invalid IL or missing references)
		//IL_20af: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_201d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2022: Unknown result type (might be due to invalid IL or missing references)
		//IL_2024: Unknown result type (might be due to invalid IL or missing references)
		//IL_2029: Unknown result type (might be due to invalid IL or missing references)
		//IL_2673: Unknown result type (might be due to invalid IL or missing references)
		//IL_2681: Unknown result type (might be due to invalid IL or missing references)
		//IL_269a: Unknown result type (might be due to invalid IL or missing references)
		//IL_269f: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2702: Unknown result type (might be due to invalid IL or missing references)
		//IL_2707: Unknown result type (might be due to invalid IL or missing references)
		//IL_270e: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2503: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1def: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e38: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1f, 0f, 0f);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = (lifeRatio < 0.7f) | death;
		bool phase3 = lifeRatio < 0.35f;
		bool phase4 = (lifeRatio <= 0.1f) & death;
		base.NPC.dontTakeDamage = calamityGlobalNPC.newAI[2] > 0f;
		bool brotherAlive = false;
		CalamityGlobalNPC.calamitas = base.NPC.whoAmI;
		if ((calamityGlobalNPC.newAI[1] == 0f) & phase3 & expertMode)
		{
			SoundEngine.PlaySound(in SoundID.Item72, base.NPC.Center);
			if (Main.netMode != 1)
			{
				int seekerAmt = (death ? 10 : 5);
				int seekerSpread = 360 / seekerAmt;
				int seekerDistance = (death ? 180 : 150);
				for (int i = 0; i < seekerAmt; i++)
				{
					int spawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(i * seekerSpread) * (double)seekerDistance), (int)((double)base.NPC.Center.Y + Math.Cos(i * seekerSpread) * (double)seekerDistance), ModContent.NPCType<SoulSeeker>(), base.NPC.whoAmI, 0f, 0f, 0f, -1f);
					Main.npc[spawn].ai[0] = i * seekerSpread;
				}
			}
			Color messageColor = Color.Orange;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.CalamitasBossText3", messageColor);
			calamityGlobalNPC.newAI[1] = 1f;
		}
		if (calamityGlobalNPC.newAI[0] == 0f && base.NPC.life > 0)
		{
			calamityGlobalNPC.newAI[0] = 1f;
		}
		if (base.NPC.life > 0)
		{
			int calClonePhaseThreshold = (int)((double)base.NPC.lifeMax * 0.3);
			if ((float)(base.NPC.life + calClonePhaseThreshold) / (float)base.NPC.lifeMax < calamityGlobalNPC.newAI[0])
			{
				calamityGlobalNPC.newAI[0] -= 0.3f;
				if (calamityGlobalNPC.newAI[0] <= 0.1f)
				{
					SoundEngine.PlaySound(in SoundID.Item109, base.NPC.Center);
					calamityGlobalNPC.newAI[2] = 2f;
					if (Main.zenithWorld)
					{
						calamityGlobalNPC.newAI[3] = 0f;
					}
					SpawnDust();
				}
				else if (calamityGlobalNPC.newAI[0] <= 0.4f)
				{
					if (Main.netMode != 1)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.position.Y + base.NPC.height, ModContent.NPCType<Cataclysm>(), base.NPC.whoAmI);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.position.Y + base.NPC.height, ModContent.NPCType<Catastrophe>(), base.NPC.whoAmI);
					}
					Color messageColor2 = Color.Orange;
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.CalamitasBossText2", messageColor2);
					SpawnDust();
				}
				else
				{
					SoundEngine.PlaySound(in SoundID.Item109, base.NPC.Center);
					calamityGlobalNPC.newAI[2] = 1f;
					if (Main.zenithWorld)
					{
						calamityGlobalNPC.newAI[3] = 0f;
					}
					SpawnDust();
				}
			}
		}
		if (CalamityGlobalNPC.cataclysm != -1 && Main.npc[CalamityGlobalNPC.cataclysm].active)
		{
			brotherAlive = true;
		}
		if (CalamityGlobalNPC.catastrophe != -1 && Main.npc[CalamityGlobalNPC.catastrophe].active)
		{
			brotherAlive = true;
		}
		if (brotherAlive)
		{
			base.NPC.dontTakeDamage = true;
		}
		bool inBulletHell = calamityGlobalNPC.newAI[2] > 0f;
		if (revenge)
		{
			if (ArenaBox == null)
			{
				ArenaBox = new ArenaWallSystem.Box
				{
					position = Main.player[base.NPC.FindClosestPlayer()].Center,
					boxDimensions = new Vector4(2000f),
					borderThickness = 2000f,
					RemovalCondition = () => !Main.npc[base.NPC.whoAmI].active || Main.npc[base.NPC.whoAmI].type != base.Type,
					UpdateBox = UpdateArena,
					DrawBox = DrawArena,
					DespawnAction = delegate(ArenaWallSystem.Box box)
					{
						//IL_0002: Unknown result type (might be due to invalid IL or missing references)
						//IL_000c: Unknown result type (might be due to invalid IL or missing references)
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						//IL_0016: Unknown result type (might be due to invalid IL or missing references)
						//IL_001c: Unknown result type (might be due to invalid IL or missing references)
						box.boxDimensions += new Vector4(64f);
						return box.Size.X > 5000f;
					}
				};
				ArenaWallSystem.ActiveBoxes.Add(ArenaBox);
			}
			ArenaBox.NewDimensions = Vector4.Lerp(ArenaBox.boxDimensions, GetArenaSize(brotherAlive, lifeRatio, inBulletHell), (lifeRatio > 0.9f) ? 0.1f : 0.025f);
			if (ArenaBox.oldData != null)
			{
				ArenaBox.oldData.borderColor = Color.White;
			}
			if (brotherAlive)
			{
				ArenaBox.borderColor = Color.Lerp(ArenaBox.borderColor, Color.Lerp(new Color(0, 255, 255), new Color(255, 0, 229), (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.5f) + 1f) * 0.5f), 0.03f);
			}
			else if (NPC.AnyNPCs(ModContent.NPCType<SoulSeeker>()))
			{
				ArenaBox.borderColor = Color.Lerp(ArenaBox.borderColor, Color.Lerp(Color.Crimson, new Color(255, 106, 0), (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.5f) + 1f) * 0.5f), 0.03f);
			}
			else if (base.NPC.dontTakeDamage)
			{
				ArenaBox.borderColor = Color.Lerp(ArenaBox.borderColor, Color.Gray, 0.1f);
				ArenaBox.oldData.borderColor = Color.Gray;
			}
			else
			{
				ArenaBox.borderColor = Color.Lerp(ArenaBox.borderColor, Color.Lerp(Color.Crimson, Color.IndianRed, (MathF.Sin(Main.GlobalTimeWrappedHourly * 0.5f) + 1f) * 0.25f), 0.03f);
			}
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (ArenaBox != null && !Collision.CheckAABBvAABBCollision(ArenaBox.TopLeft, ArenaBox.Size, player.position, player.Size))
		{
			base.NPC.dontTakeDamage = true;
		}
		Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.position.Y + (float)base.NPC.height - 59f);
		Vector2 lookAt = default(Vector2);
		((Vector2)(ref lookAt))._002Ector(player.position.X - (float)(player.width / 2), player.position.Y - (float)(player.height / 2));
		Vector2 rotationVector = val - lookAt;
		float rotation = (float)Math.Atan2(rotationVector.Y, rotationVector.X) + (float)Math.PI / 2f;
		if (rotation < 0f)
		{
			rotation += (float)Math.PI * 2f;
		}
		else if (rotation > (float)Math.PI * 2f)
		{
			rotation -= (float)Math.PI * 2f;
		}
		float rotationAmt = 0.1f;
		if (base.NPC.rotation < rotation)
		{
			if (rotation - base.NPC.rotation > (float)Math.PI)
			{
				base.NPC.rotation -= rotationAmt;
			}
			else
			{
				base.NPC.rotation += rotationAmt;
			}
		}
		else if (base.NPC.rotation > rotation)
		{
			if (base.NPC.rotation - rotation > (float)Math.PI)
			{
				base.NPC.rotation += rotationAmt;
			}
			else
			{
				base.NPC.rotation -= rotationAmt;
			}
		}
		if (base.NPC.rotation > rotation - rotationAmt && base.NPC.rotation < rotation + rotationAmt)
		{
			base.NPC.rotation = rotation;
		}
		if (base.NPC.rotation < 0f)
		{
			base.NPC.rotation += (float)Math.PI * 2f;
		}
		else if (base.NPC.rotation > (float)Math.PI * 2f)
		{
			base.NPC.rotation -= (float)Math.PI * 2f;
		}
		if (base.NPC.rotation > rotation - rotationAmt && base.NPC.rotation < rotation + rotationAmt)
		{
			base.NPC.rotation = rotation;
		}
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				if (SoundEngine.TryGetActiveSound(BulletHellWarnSlot, out ActiveSound warningSound) && warningSound.IsPlaying)
				{
					warningSound.Stop();
				}
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
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
					calamityGlobalNPC.newAI[3] = 0f;
					base.NPC.alpha = 0;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		float movementDistanceGateValue = 100f;
		float baseVelocity = (expertMode ? 10f : 8.5f) * ((base.NPC.ai[1] == 4f) ? 1.4f : 1f);
		float baseAcceleration = (expertMode ? 0.18f : 0.155f) * ((base.NPC.ai[1] == 4f) ? 1.4f : 1f);
		if (revenge)
		{
			baseVelocity += 1.5f * (1f - lifeRatio);
			baseAcceleration += 0.03f * (1f - lifeRatio);
		}
		if (death)
		{
			baseVelocity += 1.5f * (1f - lifeRatio);
			baseAcceleration += 0.03f * (1f - lifeRatio);
		}
		if (Main.getGoodWorld)
		{
			baseVelocity *= 1.15f;
			baseAcceleration *= 1.15f;
		}
		int xPos = 1;
		if (base.NPC.Center.X < player.Center.X)
		{
			xPos = -1;
		}
		float averageDistance = 400f;
		float chargeDistance = (phase4 ? 300f : 400f);
		Vector2 destination = ((calamityGlobalNPC.newAI[2] > 0f || base.NPC.ai[1] == 0f) ? new Vector2(player.Center.X, player.Center.Y - averageDistance) : ((base.NPC.ai[1] == 1f) ? new Vector2(player.Center.X + averageDistance * (float)xPos, player.Center.Y) : new Vector2(player.Center.X + chargeDistance * (float)xPos, player.Center.Y)));
		if (base.NPC.localAI[0] == 1f)
		{
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[2] = Main.rand.Next(-300, 301);
			base.NPC.netUpdate = true;
		}
		if (death)
		{
			if (base.NPC.ai[1] == 0f)
			{
				destination.X += base.NPC.localAI[2];
			}
			else
			{
				destination.Y += base.NPC.localAI[2];
			}
		}
		Vector2 distanceFromDestination = destination - base.NPC.Center;
		if (base.NPC.ai[1] == 0f || base.NPC.ai[1] == 1f || base.NPC.ai[1] == 4f || calamityGlobalNPC.newAI[2] > 0f)
		{
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, baseAcceleration, useSimpleFlyMovement: true);
		}
		if (calamityGlobalNPC.newAI[2] > 0f)
		{
			base.NPC.damage = 0;
			SoundStyle style;
			if (calamityGlobalNPC.newAI[3] < 900f)
			{
				calamityGlobalNPC.newAI[3]++;
				base.NPC.dontTakeDamage = true;
				base.NPC.alpha = 255;
				float rotX = player.Center.X - base.NPC.Center.X;
				float rotY = player.Center.Y - base.NPC.Center.Y;
				base.NPC.rotation = (float)Math.Atan2(rotY, rotX) - (float)Math.PI / 2f;
				if (Main.netMode != 1 && calamityGlobalNPC.newAI[2] == 2f)
				{
					int type = ModContent.ProjectileType<BurningFireblast>();
					int damage = FireblastDamage;
					if (Main.zenithWorld)
					{
						type = ModContent.ProjectileType<BurningGigablast>();
					}
					float gigaBlastFrequency = (Main.getGoodWorld ? 120f : (expertMode ? 180f : 240f));
					float projSpeed = 5f;
					if (calamityGlobalNPC.newAI[3] <= 300f)
					{
						if (calamityGlobalNPC.newAI[3] % gigaBlastFrequency == 0f)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed, type, damage, 0f, Main.myPlayer);
						}
					}
					else if (calamityGlobalNPC.newAI[3] <= 600f && calamityGlobalNPC.newAI[3] > 300f)
					{
						if (calamityGlobalNPC.newAI[3] % gigaBlastFrequency == 0f)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - projSpeed, 0f, type, damage, 0f, Main.myPlayer);
						}
					}
					else if (calamityGlobalNPC.newAI[3] > 600f && calamityGlobalNPC.newAI[3] % gigaBlastFrequency == 0f)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed, type, damage, 0f, Main.myPlayer);
					}
				}
				base.NPC.ai[0]++;
				float hellblastGateValue = (expertMode ? 12f : 16f);
				if (base.NPC.ai[0] >= hellblastGateValue)
				{
					base.NPC.ai[0] = 0f;
					if (Main.netMode != 1)
					{
						int type2 = ModContent.ProjectileType<CalamitousDart>();
						int damage2 = HellblastDamage;
						float projSpeed2 = 4f;
						if (calamityGlobalNPC.newAI[3] % (hellblastGateValue * 6f) == 0f && calamityGlobalNPC.newAI[2] != 2f)
						{
							float distance = (Main.rand.NextBool() ? (-1000f) : 1000f);
							float velocity = ((distance == -1000f) ? projSpeed2 : (0f - projSpeed2));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, type2, damage2, 0f, Main.myPlayer, 2f);
						}
						if (calamityGlobalNPC.newAI[3] < 300f)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed2, type2, damage2, 0f, Main.myPlayer, 2f);
						}
						else if (calamityGlobalNPC.newAI[3] < 600f)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - (projSpeed2 - 0.5f), 0f, type2, damage2, 0f, Main.myPlayer, 2f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), projSpeed2 - 0.5f, 0f, type2, damage2, 0f, Main.myPlayer, 2f);
						}
						else
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed2 - 1f, type2, damage2, 0f, Main.myPlayer, 2f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - (projSpeed2 - 1f), 0f, type2, damage2, 0f, Main.myPlayer, 2f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), projSpeed2 - 1f, 0f, type2, damage2, 0f, Main.myPlayer, 2f);
						}
					}
				}
				if (calamityGlobalNPC.newAI[3] == 540f)
				{
					style = BulletHellWarning with
					{
						Volume = 0.75f
					};
					BulletHellWarnSlot = SoundEngine.PlaySound(in style, player.Center);
				}
				if (calamityGlobalNPC.newAI[3] > 540f && SoundEngine.TryGetActiveSound(BulletHellWarnSlot, out ActiveSound warningSound2) && warningSound2.IsPlaying)
				{
					warningSound2.Position = player.Center;
				}
				return;
			}
			base.NPC.ai[0] = 0f;
			base.NPC.ai[3] = 0f;
			base.NPC.localAI[1] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			style = BulletHellEnd with
			{
				Volume = 0.75f
			};
			SoundEngine.PlaySound(in style, player.Center);
			if (phase4)
			{
				base.NPC.ai[1] = 4f;
				base.NPC.ai[2] = -105f;
				base.NPC.TargetClosest();
			}
			else
			{
				if (death)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 1:
						base.NPC.ai[1] = 1f;
						base.NPC.ai[2] = 0f;
						break;
					case 2:
						base.NPC.ai[1] = 4f;
						base.NPC.ai[2] = -105f;
						base.NPC.TargetClosest();
						break;
					}
				}
				else
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
				}
				if (death)
				{
					base.NPC.localAI[0] = 1f;
				}
			}
			base.NPC.netUpdate = true;
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile = Main.projectile[x];
				if (!projectile.active)
				{
					continue;
				}
				if (projectile.type == ModContent.ProjectileType<CalamitousDart>() || projectile.type == ModContent.ProjectileType<BurningBolt>())
				{
					if (projectile.timeLeft > 60)
					{
						projectile.timeLeft = 60;
					}
				}
				else if (projectile.type == ModContent.ProjectileType<BurningFireblast>())
				{
					projectile.ai[2] = 1f;
					if (projectile.timeLeft > 60)
					{
						projectile.timeLeft = 60;
					}
				}
			}
			return;
		}
		if (Main.zenithWorld)
		{
			if (calamityGlobalNPC.newAI[3] < 900f)
			{
				calamityGlobalNPC.newAI[3]++;
			}
			else
			{
				calamityGlobalNPC.newAI[3] = 0f;
			}
			base.NPC.ai[0]++;
			float hellblastGateValue2 = 30f;
			if (base.NPC.ai[0] >= hellblastGateValue2)
			{
				base.NPC.ai[0] = 0f;
				if (Main.netMode != 1)
				{
					int type3 = ModContent.ProjectileType<CalamitousDart>();
					int damage3 = HellblastDamage;
					float projSpeed3 = 4f;
					if (calamityGlobalNPC.newAI[3] % (hellblastGateValue2 * 6f) == 0f)
					{
						float distance2 = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity2 = ((distance2 == -1000f) ? projSpeed3 : (0f - projSpeed3));
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance2, player.position.Y, velocity2, 0f, type3, damage3, 0f, Main.myPlayer, 2f);
					}
					if (calamityGlobalNPC.newAI[3] < 300f)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed3, type3, damage3, 0f, Main.myPlayer, 2f);
					}
					else if (calamityGlobalNPC.newAI[3] < 600f)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - (projSpeed3 - 0.5f), 0f, type3, damage3, 0f, Main.myPlayer, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), projSpeed3 - 0.5f, 0f, type3, damage3, 0f, Main.myPlayer, 2f);
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed3 - 1f, type3, damage3, 0f, Main.myPlayer, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - (projSpeed3 - 1f), 0f, type3, damage3, 0f, Main.myPlayer, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), projSpeed3 - 1f, 0f, type3, damage3, 0f, Main.myPlayer, 2f);
					}
				}
			}
		}
		base.NPC.alpha = (base.NPC.dontTakeDamage ? 255 : 0);
		if (base.NPC.ai[1] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[2]++;
			float phaseTimer = 400f - (death ? (120f * (1f - lifeRatio)) : 0f);
			if ((base.NPC.ai[2] >= phaseTimer) | phase4)
			{
				if (!brotherAlive)
				{
					base.NPC.ai[1] = ((death && !phase4 && Main.rand.NextBool()) ? 4f : 1f);
				}
				base.NPC.ai[2] = 0f;
				if (death)
				{
					base.NPC.localAI[0] = 1f;
				}
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1 && !brotherAlive)
			{
				base.NPC.localAI[1]++;
				if (expertMode)
				{
					base.NPC.localAI[1] += (death ? (2f * (1f - lifeRatio)) : (1f - lifeRatio));
				}
				if (revenge)
				{
					base.NPC.localAI[1] += 0.5f;
				}
				if (base.NPC.localAI[1] >= 120f)
				{
					base.NPC.localAI[1] = 0f;
					SoundEngine.PlaySound(in CalamitousFireballSound, base.NPC.Center);
					float projectileVelocity = (expertMode ? 14f : 12.5f);
					int type4 = ModContent.ProjectileType<CalamitousFireball>();
					Vector2 predictionVector = (Main.getGoodWorld ? (player.velocity * 20f) : Vector2.Zero);
					Vector2 fireballVelocity = Vector2.Normalize(player.Center + predictionVector - base.NPC.Center) * projectileVelocity;
					Vector2 offset = Vector2.Normalize(fireballVelocity) * 40f;
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset, fireballVelocity, type4, HellfireballDamage, 0f, Main.myPlayer, player.position.X, player.position.Y);
					Main.projectile[proj].netUpdate = true;
				}
			}
			return;
		}
		if (base.NPC.ai[1] == 1f)
		{
			base.NPC.damage = 0;
			if (Main.netMode != 1 && !brotherAlive)
			{
				base.NPC.localAI[1]++;
				if (revenge)
				{
					base.NPC.localAI[1] += 0.5f;
				}
				if (expertMode)
				{
					base.NPC.localAI[1] += 0.5f;
				}
				if (base.NPC.localAI[1] >= 50f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					base.NPC.localAI[1] = 0f;
					float projectileVelocity2 = (expertMode ? 12.5f : 11f);
					int type5 = ModContent.ProjectileType<CalamitousDart>();
					int damage4 = HellblastDamage;
					Vector2 fireballVelocity2 = Vector2.Normalize(player.Center - base.NPC.Center) * projectileVelocity2;
					Vector2 offset2 = Vector2.Normalize(fireballVelocity2) * 40f;
					if (!Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
					{
						type5 = ModContent.ProjectileType<CalamitousFireball>();
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset2, fireballVelocity2, type5, HellfireballDamage, 0f, Main.myPlayer, player.position.X, player.position.Y);
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset2, fireballVelocity2, type5, damage4, 0f, Main.myPlayer, 1f);
						SoundEngine.PlaySound(in CalamitousFireballSound, base.NPC.Center);
					}
				}
			}
			base.NPC.ai[2]++;
			float phaseTimer2 = 240f - (death ? (60f * (1f - lifeRatio)) : 0f);
			if ((base.NPC.ai[2] >= phaseTimer2) | phase4)
			{
				if (brotherAlive)
				{
					base.NPC.ai[1] = 0f;
				}
				else if (death && !phase4 && Main.rand.NextBool())
				{
					base.NPC.ai[1] = 0f;
				}
				else
				{
					base.NPC.ai[1] = ((phase2 & revenge) ? 4f : 0f);
				}
				base.NPC.ai[2] = 0f;
				if (death)
				{
					base.NPC.localAI[0] = 1f;
				}
				base.NPC.netUpdate = true;
			}
			return;
		}
		if (base.NPC.ai[1] == 2f)
		{
			base.NPC.damage = base.NPC.defDamage;
			SoundEngine.PlaySound(in ChargeSound, base.NPC.Center);
			base.NPC.rotation = rotation;
			float chargeVelocity = (phase4 ? 30f : (death ? 28f : 25f));
			Vector2 vector = Vector2.Normalize(player.Center - base.NPC.Center);
			base.NPC.velocity = vector * chargeVelocity;
			base.NPC.ai[1] = 3f;
			base.NPC.netUpdate = true;
			return;
		}
		if (base.NPC.ai[1] == 3f)
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.ai[2]++;
			float chargeTime = (phase4 ? 35f : (death ? 40f : 45f));
			if (base.NPC.ai[2] >= chargeTime)
			{
				base.NPC.damage = 0;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
				{
					base.NPC.velocity.Y = 0f;
				}
			}
			else
			{
				for (int i2 = 0; i2 < 5; i2++)
				{
					CalamitasMetaball.SpawnParticle(base.NPC.Center + base.NPC.velocity + (base.NPC.rotation + (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(1.0) * 64f, base.NPC.velocity.RotatedByRandom(0.25) * 0.5f, 32f).SizeScaling = 0.9f;
				}
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) - (float)Math.PI / 2f;
				if (((Main.netMode != 1) & death & phase4) && base.NPC.ai[2] % 6f == 0f)
				{
					int type6 = ModContent.ProjectileType<CalamitousDart>();
					Vector2 fireballVelocity3 = (Main.getGoodWorld ? Main.rand.NextVector2CircularEdge(0.02f, 0.02f) : (base.NPC.velocity * 0.01f));
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, fireballVelocity3, type6, HellblastDamage, 0f, Main.myPlayer, 1f, 0f, 2f);
				}
			}
			if (base.NPC.ai[2] >= chargeTime + 10f)
			{
				if (!phase4)
				{
					base.NPC.ai[3]++;
				}
				base.NPC.ai[2] = 0f;
				base.NPC.rotation = rotation;
				base.NPC.netUpdate = true;
				if (base.NPC.ai[3] >= 2f)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[1] = 0f;
					base.NPC.ai[3] = 0f;
				}
				else
				{
					base.NPC.ai[1] = 4f;
				}
			}
			return;
		}
		base.NPC.damage = 0;
		base.NPC.ai[2]++;
		float telegraphDuration = (phase4 ? 15f : 30f);
		float startTelegraphTime = (phase4 ? (-25f) : (-10f));
		if (base.NPC.ai[2] >= startTelegraphTime && base.NPC.ai[2] < telegraphDuration && Main.netMode != 1 && Main.rand.NextBool(3))
		{
			Vector2 dustVel2 = Vector2.UnitX.RotatedByRandom(100.0) * Main.rand.NextFloat(23f, 28f);
			CalamitasMetaball.Particle particle = CalamitasMetaball.SpawnParticle(base.NPC.Center + dustVel2.SafeNormalize(Vector2.UnitX) * 420f, -dustVel2 * 1.2f, 16f * Main.rand.NextFloat(1.4f, 2.35f));
			particle.Scale = new Vector2(1f, 0.33f);
			particle.rotation = particle.Velocity.ToRotation();
			particle.SizeScaling = 0.95f;
		}
		if (base.NPC.ai[2] >= telegraphDuration)
		{
			base.NPC.ai[1] = 2f;
			base.NPC.ai[2] = 0f;
			if (death)
			{
				base.NPC.localAI[0] = 1f;
			}
			base.NPC.netUpdate = true;
		}
		void SpawnDust()
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			int dustAmt = 50;
			int random = 3;
			for (int j = 0; j < 10; j++)
			{
				random += j;
				int dustAmtSpawned = 0;
				int scale = random * 6;
				float dustPositionX = base.NPC.Center.X - (float)(scale / 2);
				float dustPositionY = base.NPC.Center.Y - (float)(scale / 2);
				for (; dustAmtSpawned < dustAmt; dustAmtSpawned++)
				{
					float dustVelocityX = Main.rand.Next(-random, random);
					float dustVelocityY = Main.rand.Next(-random, random);
					float num = (float)random * 2f;
					float dustVelocity = (float)Math.Sqrt(dustVelocityX * dustVelocityX + dustVelocityY * dustVelocityY);
					dustVelocity = num / dustVelocity;
					dustVelocityX *= dustVelocity;
					dustVelocityY *= dustVelocity;
					int dust = Dust.NewDust(new Vector2(dustPositionX, dustPositionY), scale, scale, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position.X = base.NPC.Center.X;
					Main.dust[dust].position.Y = base.NPC.Center.Y;
					Main.dust[dust].position.X += Main.rand.Next(-10, 11);
					Main.dust[dust].position.Y += Main.rand.Next(-10, 11);
					Main.dust[dust].velocity.X = dustVelocityX;
					Main.dust[dust].velocity.Y = dustVelocityY;
				}
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[base.Type] / 2));
		Color white = Color.White;
		float colorLerpAmt = 0.5f;
		int afterimageAmt = 6;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase4 = ((float)base.NPC.life / (float)base.NPC.lifeMax <= 0.1f) & death;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, white, colorLerpAmt);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 offset = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				offset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				offset += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, offset, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 npcOffset = base.NPC.Center - screenPos;
		npcOffset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		npcOffset += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, npcOffset, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		texture = GlowTexture.Value;
		Color color = Color.Lerp(Color.White, Color.Red, 0.5f);
		if (Main.zenithWorld)
		{
			color = Color.CornflowerBlue;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color extraAfterimageColor = color;
				extraAfterimageColor = Color.Lerp(extraAfterimageColor, white, colorLerpAmt);
				extraAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 offset2 = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				offset2 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				offset2 += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, offset2, (Rectangle?)base.NPC.frame, extraAfterimageColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		if (base.NPC.ai[1] == 4f)
		{
			float telegraphDuration = (phase4 ? 15f : 30f);
			float startTelegraphTime = (phase4 ? (-25f) : (-10f));
			float glowTimeElapsed = base.NPC.ai[2] - startTelegraphTime;
			float timeForMaxGlow = telegraphDuration - startTelegraphTime;
			float lifeFadeIn = Utils.GetLerpValue(0f, timeForMaxGlow, glowTimeElapsed, clamped: true);
			float glowSine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 10f);
			float finalGlowIntensity = MathHelper.Lerp(0.7f, 1f, glowSine) * lifeFadeIn;
			for (int k = 0; k < 20; k++)
			{
				Vector2 glowOffset = ((float)Math.PI * 2f * (float)k / 15f).ToRotationVector2() * (3f + glowSine * 1f) * finalGlowIntensity;
				SpriteBatch spriteBatch2 = Main.spriteBatch;
				Texture2D obj = texture;
				Vector2 val = base.NPC.Center - screenPos + glowOffset;
				Rectangle? val2 = base.NPC.frame;
				Color red = Color.Red;
				((Color)(ref red)).A = 150;
				spriteBatch2.Draw(obj, val, val2, red * finalGlowIntensity, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture, npcOffset, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CalamitasCloneBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] items = new int[4]
		{
			ModContent.ItemType<Oblivion>(),
			ModContent.ItemType<Animosity>(),
			ModContent.ItemType<LashesofChaos>(),
			ModContent.ItemType<EntropysVigil>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, items));
		normalOnly.Add(ModContent.ItemType<ChaosStone>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<Regenerator>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<EssenceofHavoc>(), 1, 8, 10);
		normalOnly.Add(ModContent.ItemType<AshesofCalamity>(), 1, 25, 30);
		normalOnly.Add(ModContent.ItemType<CalamitasCloneMask>(), 7);
		IItemDropRule calVanity = ItemDropRule.Common(ModContent.ItemType<HoodOfCalamity>(), 10);
		calVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RobesOfCalamity>()));
		normalOnly.Add(calVanity);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<CalamitasCloneTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<CalamitasCloneRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<AshesofAnnihilation>(), 1, 6, 9), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedCalamitasClone, ModContent.ItemType<LoreCalamitasClone>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<Bandit>() }, DownedBossSystem.downedCalamitasClone);
			DownedBossSystem.downedCalamitasClone = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Calamitas").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Calamitas2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Calamitas3").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Calamitas4").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Calamitas5").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Calamitas6").Type, base.NPC.scale);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 100;
		base.NPC.height = 100;
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
		for (int j = 0; j < 70; j++)
		{
			int brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[brimDust2].noGravity = true;
			Dust obj2 = Main.dust[brimDust2];
			obj2.velocity *= 5f;
			brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[brimDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
		}
	}
}
