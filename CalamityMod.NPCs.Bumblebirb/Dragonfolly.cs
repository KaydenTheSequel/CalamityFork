using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
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

namespace CalamityMod.NPCs.Bumblebirb;

[AutoloadBossHead]
public class Dragonfolly : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public static float DashDamageMult = 1.5f;

	public static int FeatherDamage = 36;

	public static int LightningDamage = 64;

	public override string Texture => "CalamityMod/NPCs/Bumblebirb/Birb";

	public override string BossHeadTexture => "CalamityMod/NPCs/Bumblebirb/Birb_Head_Boss";

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.85f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 14f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 20f;
		value.Position.Y += 8f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Bumblebirb/BirbGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 80;
		base.NPC.npcSlots = 32f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.width = 130;
		base.NPC.height = 100;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.1f);
		base.NPC.LifeMaxNERB(150000, 225000, 300000);
		base.NPC.knockBackResist = 0f;
		base.NPC.boss = true;
		base.NPC.noTileCollide = true;
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = true;
		base.NPC.value = Item.buyPrice(0, 50);
		base.NPC.HitSound = SoundID.NPCHit51;
		base.NPC.DeathSound = SoundID.NPCDeath46;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Bumblefuck")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_174e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1759: Unknown result type (might be due to invalid IL or missing references)
		//IL_175e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1763: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e38: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1802: Unknown result type (might be due to invalid IL or missing references)
		//IL_1807: Unknown result type (might be due to invalid IL or missing references)
		//IL_1815: Unknown result type (might be due to invalid IL or missing references)
		//IL_1822: Unknown result type (might be due to invalid IL or missing references)
		//IL_1827: Unknown result type (might be due to invalid IL or missing references)
		//IL_1829: Unknown result type (might be due to invalid IL or missing references)
		//IL_1830: Unknown result type (might be due to invalid IL or missing references)
		//IL_1835: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1911: Unknown result type (might be due to invalid IL or missing references)
		//IL_1927: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_093d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_098e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_200d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2018: Unknown result type (might be due to invalid IL or missing references)
		//IL_1993: Unknown result type (might be due to invalid IL or missing references)
		//IL_199e: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_274c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2756: Unknown result type (might be due to invalid IL or missing references)
		//IL_275b: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_21eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2209: Unknown result type (might be due to invalid IL or missing references)
		//IL_2213: Unknown result type (might be due to invalid IL or missing references)
		//IL_2218: Unknown result type (might be due to invalid IL or missing references)
		//IL_2220: Unknown result type (might be due to invalid IL or missing references)
		//IL_204a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2055: Unknown result type (might be due to invalid IL or missing references)
		//IL_2073: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_27bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2317: Unknown result type (might be due to invalid IL or missing references)
		//IL_2321: Unknown result type (might be due to invalid IL or missing references)
		//IL_2326: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2304: Unknown result type (might be due to invalid IL or missing references)
		//IL_2309: Unknown result type (might be due to invalid IL or missing references)
		//IL_20eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_27fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2813: Unknown result type (might be due to invalid IL or missing references)
		//IL_281d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2828: Unknown result type (might be due to invalid IL or missing references)
		//IL_282d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2832: Unknown result type (might be due to invalid IL or missing references)
		//IL_2294: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_28db: Unknown result type (might be due to invalid IL or missing references)
		//IL_28e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_286b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2872: Unknown result type (might be due to invalid IL or missing references)
		//IL_2916: Unknown result type (might be due to invalid IL or missing references)
		//IL_2936: Unknown result type (might be due to invalid IL or missing references)
		//IL_2951: Unknown result type (might be due to invalid IL or missing references)
		//IL_2956: Unknown result type (might be due to invalid IL or missing references)
		//IL_2958: Unknown result type (might be due to invalid IL or missing references)
		//IL_295d: Unknown result type (might be due to invalid IL or missing references)
		//IL_296e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2979: Unknown result type (might be due to invalid IL or missing references)
		//IL_297e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2988: Unknown result type (might be due to invalid IL or missing references)
		//IL_298d: Unknown result type (might be due to invalid IL or missing references)
		//IL_299b: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_29cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_23da: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2401: Unknown result type (might be due to invalid IL or missing references)
		//IL_240b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2410: Unknown result type (might be due to invalid IL or missing references)
		//IL_241b: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2604: Unknown result type (might be due to invalid IL or missing references)
		//IL_2609: Unknown result type (might be due to invalid IL or missing references)
		//IL_2613: Unknown result type (might be due to invalid IL or missing references)
		//IL_262e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2633: Unknown result type (might be due to invalid IL or missing references)
		//IL_2638: Unknown result type (might be due to invalid IL or missing references)
		//IL_263a: Unknown result type (might be due to invalid IL or missing references)
		//IL_263d: Unknown result type (might be due to invalid IL or missing references)
		//IL_243c: Unknown result type (might be due to invalid IL or missing references)
		//IL_245c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2477: Unknown result type (might be due to invalid IL or missing references)
		//IL_247c: Unknown result type (might be due to invalid IL or missing references)
		//IL_247e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2483: Unknown result type (might be due to invalid IL or missing references)
		//IL_2494: Unknown result type (might be due to invalid IL or missing references)
		//IL_249f: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_265a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2662: Unknown result type (might be due to invalid IL or missing references)
		//IL_1474: Unknown result type (might be due to invalid IL or missing references)
		//IL_147f: Unknown result type (might be due to invalid IL or missing references)
		//IL_102c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1037: Unknown result type (might be due to invalid IL or missing references)
		//IL_10af: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10df: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1104: Unknown result type (might be due to invalid IL or missing references)
		//IL_1112: Unknown result type (might be due to invalid IL or missing references)
		//IL_1114: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_1309: Unknown result type (might be due to invalid IL or missing references)
		//IL_130b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_1318: Unknown result type (might be due to invalid IL or missing references)
		//IL_131d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1322: Unknown result type (might be due to invalid IL or missing references)
		//IL_1325: Unknown result type (might be due to invalid IL or missing references)
		//IL_132a: Unknown result type (might be due to invalid IL or missing references)
		//IL_132c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_1338: Unknown result type (might be due to invalid IL or missing references)
		//IL_133d: Unknown result type (might be due to invalid IL or missing references)
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_122b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1238: Unknown result type (might be due to invalid IL or missing references)
		//IL_1246: Unknown result type (might be due to invalid IL or missing references)
		//IL_124c: Unknown result type (might be due to invalid IL or missing references)
		//IL_124e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1253: Unknown result type (might be due to invalid IL or missing references)
		//IL_125b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1260: Unknown result type (might be due to invalid IL or missing references)
		//IL_1265: Unknown result type (might be due to invalid IL or missing references)
		//IL_1268: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_127b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1280: Unknown result type (might be due to invalid IL or missing references)
		//IL_128e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1290: Unknown result type (might be due to invalid IL or missing references)
		//IL_1167: Unknown result type (might be due to invalid IL or missing references)
		//IL_1174: Unknown result type (might be due to invalid IL or missing references)
		//IL_1182: Unknown result type (might be due to invalid IL or missing references)
		//IL_1188: Unknown result type (might be due to invalid IL or missing references)
		//IL_118a: Unknown result type (might be due to invalid IL or missing references)
		//IL_118f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1197: Unknown result type (might be due to invalid IL or missing references)
		//IL_119c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15da: Unknown result type (might be due to invalid IL or missing references)
		//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1603: Unknown result type (might be due to invalid IL or missing references)
		//IL_1605: Unknown result type (might be due to invalid IL or missing references)
		//IL_1502: Unknown result type (might be due to invalid IL or missing references)
		//IL_150f: Unknown result type (might be due to invalid IL or missing references)
		//IL_151d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1523: Unknown result type (might be due to invalid IL or missing references)
		//IL_1525: Unknown result type (might be due to invalid IL or missing references)
		//IL_152a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1532: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Unknown result type (might be due to invalid IL or missing references)
		//IL_153f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		//IL_1546: Unknown result type (might be due to invalid IL or missing references)
		//IL_154b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1552: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_1565: Unknown result type (might be due to invalid IL or missing references)
		//IL_1567: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		float rotationMult = 3f;
		float rotationAmt = 0.03f;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool immuneToSlowingDebuffs = base.NPC.ai[0] == 3f || base.NPC.ai[0] == 3.1f || base.NPC.ai[0] == 3.2f;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		if (!player.ZoneJungle && !BossRushEvent.BossRushActive)
		{
			if (base.NPC.localAI[1] < 300f)
			{
				base.NPC.localAI[1]++;
			}
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		if (Vector2.Distance(player.Center, base.NPC.Center) > 1200f)
		{
			base.NPC.localAI[2] = 2f;
		}
		float enrageScale = (death ? 1.5f : 1f);
		if (base.NPC.localAI[1] >= 300f)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale++;
		}
		if (base.NPC.localAI[2] > 0f)
		{
			enrageScale++;
		}
		if (Main.getGoodWorld)
		{
			enrageScale += 0.5f;
		}
		if (enrageScale > 3f)
		{
			enrageScale = 3f;
		}
		if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
			{
				base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt) / 10f;
				if (base.NPC.velocity.Y > 3f)
				{
					base.NPC.velocity.Y = 3f;
				}
				base.NPC.velocity.Y -= 0.2f;
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
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = num < (revenge ? 0.75f : 0.5f);
		bool phase3 = (num < (death ? 0.4f : (revenge ? 0.25f : 0.1f))) & expertMode;
		float birbSpawnPhaseTimer = 180f;
		float newPhaseTimer = 180f;
		bool phaseSwitchPhase = (phase2 && calamityGlobalNPC.newAI[0] < newPhaseTimer && calamityGlobalNPC.newAI[2] != 1f) || (phase3 && calamityGlobalNPC.newAI[1] < newPhaseTimer && calamityGlobalNPC.newAI[3] != 1f);
		calamityGlobalNPC.DR = ((phaseSwitchPhase || base.NPC.ai[0] == 5f || enrageScale == 3f) ? 0.55f : 0.1f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = phaseSwitchPhase || base.NPC.ai[0] == 5f || enrageScale == 3f;
		if (phaseSwitchPhase)
		{
			base.NPC.damage = 0;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt) / 10f;
			if (phase3)
			{
				calamityGlobalNPC.newAI[1]++;
				if (calamityGlobalNPC.newAI[1] == newPhaseTimer - 60f)
				{
					float squawkpitch = (Main.zenithWorld ? 1.3f : 0.25f);
					SoundEngine.PlaySound(SoundID.DD2_BetsyScream with
					{
						Pitch = squawkpitch
					}, base.NPC.Center);
					if (Main.zenithWorld)
					{
						int spacing = 20;
						int amt = 5;
						SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.NPC.Center - Vector2.UnitY * 300f);
						if (Main.netMode != 1)
						{
							Vector2 fireFrom = default(Vector2);
							for (int i = 0; i < amt; i++)
							{
								((Vector2)(ref fireFrom))._002Ector(base.NPC.Center.X + (float)(spacing * i) - (float)(spacing * amt / 2), base.NPC.Center.Y - 900f);
								Vector2 ai0 = base.NPC.Center - fireFrom;
								float ai1 = Main.rand.Next(100);
								Vector2 velocity = Vector2.Normalize(ai0.RotatedByRandom(0.7853981852531433)) * 7f;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom.X, fireFrom.Y, velocity.X, velocity.Y, ModContent.ProjectileType<RedLightning>(), LightningDamage, 0f, Main.myPlayer, ai0.ToRotation(), ai1);
							}
						}
					}
				}
				if (calamityGlobalNPC.newAI[1] >= newPhaseTimer)
				{
					calamityGlobalNPC.newAI[1] = 0f;
					calamityGlobalNPC.newAI[2] = 1f;
					calamityGlobalNPC.newAI[3] = 1f;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.SyncExtraAI();
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			else
			{
				calamityGlobalNPC.newAI[0]++;
				if (calamityGlobalNPC.newAI[0] == newPhaseTimer - 60f)
				{
					float squawkpitch2 = (Main.zenithWorld ? 1.3f : 0.25f);
					SoundEngine.PlaySound(SoundID.DD2_BetsyScream with
					{
						Pitch = squawkpitch2
					}, base.NPC.Center);
					if (Main.zenithWorld)
					{
						int spacing2 = 20;
						int amt2 = 3;
						SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.NPC.Center - Vector2.UnitY * 300f);
						if (Main.netMode != 1)
						{
							Vector2 fireFrom2 = default(Vector2);
							for (int j = 0; j < amt2; j++)
							{
								((Vector2)(ref fireFrom2))._002Ector(base.NPC.Center.X + (float)(spacing2 * j) - (float)(spacing2 * amt2 / 2), base.NPC.Center.Y - 900f);
								Vector2 ai2 = base.NPC.Center - fireFrom2;
								float ai3 = Main.rand.Next(100);
								Vector2 velocity2 = Vector2.Normalize(ai2.RotatedByRandom(0.7853981852531433)) * 7f;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom2.X, fireFrom2.Y, velocity2.X, velocity2.Y, ModContent.ProjectileType<RedLightning>(), LightningDamage, 0f, Main.myPlayer, ai2.ToRotation(), ai3);
							}
						}
					}
				}
				if (calamityGlobalNPC.newAI[0] >= newPhaseTimer)
				{
					calamityGlobalNPC.newAI[0] = 0f;
					calamityGlobalNPC.newAI[2] = 1f;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.SyncExtraAI();
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			Vector2 follyTargetDirection = player.Center - base.NPC.Center;
			float follyTargetDistance = 4f + ((Vector2)(ref follyTargetDirection)).Length() / 100f;
			float follyVelocityMult = 25f;
			((Vector2)(ref follyTargetDirection)).Normalize();
			follyTargetDirection *= follyTargetDistance;
			base.NPC.velocity = (base.NPC.velocity * (follyVelocityMult - 1f) + follyTargetDirection) / follyVelocityMult;
			return;
		}
		int maxBirbs = (Main.zenithWorld ? 12 : (revenge ? 3 : 2));
		float chargeDistance = 600f;
		if (phase2)
		{
			chargeDistance -= 50f;
		}
		if (phase3)
		{
			chargeDistance -= 50f;
		}
		chargeDistance -= (enrageScale - 1f) * 100f;
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			if (base.NPC.Center.X < player.Center.X - 2f)
			{
				base.NPC.direction = 1;
			}
			if (base.NPC.Center.X > player.Center.X + 2f)
			{
				base.NPC.direction = -1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt * 1.25f) / 10f;
			Vector2 follyFlyTargetDirection = player.Center - base.NPC.Center;
			follyFlyTargetDirection.Y -= 200f;
			if (((Vector2)(ref follyFlyTargetDirection)).Length() > 2800f)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
			}
			else if (((Vector2)(ref follyFlyTargetDirection)).Length() > 240f)
			{
				float follyFlySpeed = 12f + (enrageScale - 1f) * 6f;
				float follyFlyVelocityMult = 30f;
				((Vector2)(ref follyFlyTargetDirection)).Normalize();
				follyFlyTargetDirection *= follyFlySpeed;
				base.NPC.velocity = (base.NPC.velocity * (follyFlyVelocityMult - 1f) + follyFlyTargetDirection) / follyFlyVelocityMult;
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() > 2f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.95f;
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 1.05f;
			}
			base.NPC.ai[1]++;
			if (!(base.NPC.ai[1] >= 30f))
			{
				return;
			}
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			while (base.NPC.ai[0] == 0f)
			{
				if (phase2)
				{
					base.NPC.localAI[0]++;
				}
				if (base.NPC.localAI[0] >= (float)(phase3 ? 7 : 9))
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 5f;
					base.NPC.localAI[0] = 0f;
					if (base.NPC.ai[3] > 0f)
					{
						base.NPC.ai[3]--;
					}
					if (base.NPC.localAI[2] > 0f)
					{
						base.NPC.localAI[2]--;
					}
					if (base.NPC.localAI[3] > 0f)
					{
						base.NPC.localAI[3]--;
					}
					continue;
				}
				int follyAttackPicker = (phase2 ? (Main.rand.Next(2) + 1) : Main.rand.Next(3));
				if (phase3)
				{
					follyAttackPicker = 1;
				}
				float featherVelocity = 2f + (enrageScale - 1f);
				int type = ModContent.ProjectileType<RedLightningFeather>();
				if (follyAttackPicker == 0 && base.NPC.localAI[3] == 0f)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 2f;
					if (base.NPC.ai[3] > 0f)
					{
						base.NPC.ai[3]--;
					}
					if (base.NPC.localAI[2] > 0f)
					{
						base.NPC.localAI[2]--;
					}
					base.NPC.localAI[3] = 1f;
				}
				else if (follyAttackPicker == 1)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 3f;
					if (base.NPC.localAI[2] > 0f)
					{
						base.NPC.localAI[2]--;
					}
					if (base.NPC.localAI[3] > 0f)
					{
						base.NPC.localAI[3]--;
					}
					if (phase2 && base.NPC.ai[3] == 0f)
					{
						base.NPC.ai[3] = 3f;
						SoundEngine.PlaySound(in SoundID.Item102, player.Center);
						if (Main.netMode == 1)
						{
							continue;
						}
						int totalProjectiles = 40;
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						int distance = 800;
						bool spawnRight = player.velocity.X > 0f;
						for (int k = 0; k < totalProjectiles; k++)
						{
							if (Main.getGoodWorld)
							{
								if (k >= (int)((double)totalProjectiles * 0.125) && k <= (int)((double)totalProjectiles * 0.375))
								{
									Vector2 spawnVector = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - featherVelocity), (double)(radians * (float)k), default(Vector2))) * (float)distance;
									Vector2 velocity3 = Vector2.Normalize(player.Center - spawnVector) * featherVelocity;
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector, velocity3, type, FeatherDamage, 0f, Main.myPlayer);
								}
								if (k >= (int)((double)totalProjectiles * 0.625) && k <= (int)((double)totalProjectiles * 0.875))
								{
									Vector2 spawnVector2 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - featherVelocity), (double)(radians * (float)k), default(Vector2))) * (float)distance;
									Vector2 velocity4 = Vector2.Normalize(player.Center - spawnVector2) * featherVelocity;
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector2, velocity4, type, FeatherDamage, 0f, Main.myPlayer);
								}
							}
							else if (spawnRight)
							{
								if (k >= (int)((double)totalProjectiles * 0.125) && k <= (int)((double)totalProjectiles * 0.375))
								{
									Vector2 spawnVector3 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - featherVelocity), (double)(radians * (float)k), default(Vector2))) * (float)distance;
									Vector2 velocity5 = Vector2.Normalize(player.Center - spawnVector3) * featherVelocity;
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector3, velocity5, type, FeatherDamage, 0f, Main.myPlayer);
								}
							}
							else if (k >= (int)((double)totalProjectiles * 0.625) && k <= (int)((double)totalProjectiles * 0.875))
							{
								Vector2 spawnVector4 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - featherVelocity), (double)(radians * (float)k), default(Vector2))) * (float)distance;
								Vector2 velocity6 = Vector2.Normalize(player.Center - spawnVector4) * featherVelocity;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector4, velocity6, type, FeatherDamage, 0f, Main.myPlayer);
							}
						}
					}
					else if (base.NPC.ai[3] > 0f)
					{
						base.NPC.ai[3]--;
					}
				}
				else
				{
					if (NPC.CountNPCS(ModContent.NPCType<DraconicSwarmer>()) >= maxBirbs || base.NPC.localAI[3] != 0f)
					{
						continue;
					}
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 4f;
					base.NPC.localAI[3] = 2f;
					if (base.NPC.localAI[2] > 0f)
					{
						base.NPC.localAI[2]--;
					}
					if (base.NPC.ai[3] == 0f)
					{
						base.NPC.ai[3] = 3f;
						SoundEngine.PlaySound(in SoundID.Item102, player.Center);
						if (Main.netMode == 1)
						{
							continue;
						}
						int totalProjectiles2 = (phase2 ? 40 : 48);
						if (Main.getGoodWorld)
						{
							totalProjectiles2 *= 2;
						}
						float radians2 = (float)Math.PI * 2f / (float)totalProjectiles2;
						int distance2 = (phase2 ? 1200 : 1320);
						if (Main.getGoodWorld)
						{
							distance2 *= 2;
						}
						bool spawnRight2 = player.velocity.X > 0f;
						for (int l = 0; l < totalProjectiles2; l++)
						{
							if (spawnRight2)
							{
								if (l >= totalProjectiles2 / 2)
								{
									break;
								}
								Vector2 spawnVector5 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - featherVelocity), (double)(radians2 * (float)l), default(Vector2))) * (float)distance2;
								Vector2 velocity7 = Vector2.Normalize(player.Center - spawnVector5) * featherVelocity;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector5, velocity7, type, FeatherDamage, 0f, Main.myPlayer);
							}
							else if (l >= totalProjectiles2 / 2)
							{
								Vector2 spawnVector6 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - featherVelocity), (double)(radians2 * (float)l), default(Vector2))) * (float)distance2;
								Vector2 velocity8 = Vector2.Normalize(player.Center - spawnVector6) * featherVelocity;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector6, velocity8, type, FeatherDamage, 0f, Main.myPlayer);
							}
						}
					}
					else if (base.NPC.ai[3] > 0f)
					{
						base.NPC.ai[3]--;
					}
				}
			}
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			base.NPC.SyncExtraAI();
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt) / 10f;
			Vector2 follyTargetDirection2 = player.Center - base.NPC.Center;
			if (((Vector2)(ref follyTargetDirection2)).Length() < 800f)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.SyncExtraAI();
			}
			float follyTargetDistance2 = 14f + (enrageScale - 1f) * 4f + ((Vector2)(ref follyTargetDirection2)).Length() / 100f;
			float follyVelocityMult2 = 25f;
			((Vector2)(ref follyTargetDirection2)).Normalize();
			follyTargetDirection2 *= follyTargetDistance2;
			base.NPC.velocity = (base.NPC.velocity * (follyVelocityMult2 - 1f) + follyTargetDirection2) / follyVelocityMult2;
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (base.NPC.target < 0 || !player.active || player.dead)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.SyncExtraAI();
			}
			if (player.Center.X - 10f < base.NPC.Center.X)
			{
				base.NPC.direction = -1;
			}
			else if (player.Center.X + 10f > base.NPC.Center.X)
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult * 0.5f + base.NPC.velocity.X * rotationAmt * 1.25f) / 5f;
			Vector2 follyQuickFlyTargetDirection = player.Center - base.NPC.Center;
			follyQuickFlyTargetDirection.Y -= 20f;
			base.NPC.ai[2] += 1f / 45f;
			if (expertMode)
			{
				base.NPC.ai[2] += 1f / 60f;
			}
			float follyQuickFlySpeed = 8f + (enrageScale - 1f) * 2f + base.NPC.ai[2] + ((Vector2)(ref follyQuickFlyTargetDirection)).Length() / 120f;
			if (Main.getGoodWorld)
			{
				follyQuickFlySpeed *= 2f;
			}
			float follyQuickFlyVelMult = 20f;
			((Vector2)(ref follyQuickFlyTargetDirection)).Normalize();
			follyQuickFlyTargetDirection *= follyQuickFlySpeed;
			base.NPC.velocity = (base.NPC.velocity * (follyQuickFlyVelMult - 1f) + follyQuickFlyTargetDirection) / follyQuickFlyVelMult;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (Main.getGoodWorld ? 90f : 180f))
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.SyncExtraAI();
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult * 0.5f + base.NPC.velocity.X * rotationAmt * 0.85f) / 5f;
			Vector2 follyLineUpTargetDirection = player.Center - base.NPC.Center;
			follyLineUpTargetDirection.Y -= 12f;
			if (base.NPC.Center.X > player.Center.X)
			{
				follyLineUpTargetDirection.X += chargeDistance;
			}
			else
			{
				follyLineUpTargetDirection.X -= chargeDistance;
			}
			float verticalDistanceGateValue = (phase3 ? 100f : 20f) + (enrageScale - 1f) * 20f;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > chargeDistance - 50f && Math.Abs(base.NPC.Center.Y - player.Center.Y) < verticalDistanceGateValue)
			{
				base.NPC.ai[0] = 3.1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.SyncExtraAI();
			}
			base.NPC.ai[1] += 1f / 30f;
			float follyLineUpSpeed = 16f + (enrageScale - 1f) * 4f + base.NPC.ai[1];
			float follyLineUpVelMult = 4f;
			((Vector2)(ref follyLineUpTargetDirection)).Normalize();
			follyLineUpTargetDirection *= follyLineUpSpeed;
			base.NPC.velocity = (base.NPC.velocity * (follyLineUpVelMult - 1f) + follyLineUpTargetDirection) / follyLineUpVelMult;
		}
		else if (base.NPC.ai[0] == 3.1f)
		{
			base.NPC.damage = 0;
			base.NPC.rotation = (base.NPC.rotation * rotationMult * 0.5f + base.NPC.velocity.X * rotationAmt * 0.85f) / 5f;
			Vector2 follyChargePrepareTargetDirection = player.Center - base.NPC.Center;
			follyChargePrepareTargetDirection.Y -= 12f;
			float follyChargePrepareSpeed = 28f + (enrageScale - 1f) * 4f;
			float follyChargePrepareVelMult = 8f;
			((Vector2)(ref follyChargePrepareTargetDirection)).Normalize();
			follyChargePrepareTargetDirection *= follyChargePrepareSpeed;
			base.NPC.velocity = (base.NPC.velocity * (follyChargePrepareVelMult - 1f) + follyChargePrepareTargetDirection) / follyChargePrepareVelMult;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > 10f)
			{
				base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * DashDamageMult);
				base.NPC.velocity = follyChargePrepareTargetDirection;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.ai[0] = 3.2f;
				base.NPC.ai[1] = base.NPC.direction;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				base.NPC.SyncExtraAI();
			}
		}
		else if (base.NPC.ai[0] == 3.2f)
		{
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * DashDamageMult);
			base.NPC.ai[2] += 1f / 30f;
			float velocity9 = 28f + (enrageScale - 1f) * 4f;
			base.NPC.velocity.X = (velocity9 + base.NPC.ai[2]) * base.NPC.ai[1];
			if ((base.NPC.ai[1] > 0f && base.NPC.Center.X > player.Center.X + (chargeDistance - 140f)) || (base.NPC.ai[1] < 0f && base.NPC.Center.X < player.Center.X - (chargeDistance - 140f)))
			{
				if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				else if (Math.Abs(base.NPC.Center.X - player.Center.X) > chargeDistance + 200f)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			base.NPC.rotation = (base.NPC.rotation * rotationMult * 0.5f + base.NPC.velocity.X * rotationAmt * 0.85f) / 5f;
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[1] == 0f)
			{
				Vector2 destination2 = player.Center + new Vector2(0f, -200f);
				Vector2 desiredVelocity2 = base.NPC.SafeDirectionTo(destination2, -Vector2.UnitY) * 18f;
				base.NPC.SimpleFlyMovement(desiredVelocity2, 1.5f);
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.ai[2]++;
				if (base.NPC.Distance(player.Center) < 600f || base.NPC.ai[2] >= 180f)
				{
					base.NPC.ai[1] = 1f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			else
			{
				if (base.NPC.ai[1] < 90f)
				{
					NPC nPC3 = base.NPC;
					nPC3.velocity *= 0.95f;
				}
				else
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.98f;
				}
				if (base.NPC.ai[1] == 90f)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y /= 3f;
					}
					base.NPC.velocity.Y -= 3f;
				}
				if (base.NPC.ai[1] == birbSpawnPhaseTimer - 60f)
				{
					float squawkpitch3 = (Main.zenithWorld ? 1.3f : 0.25f);
					SoundEngine.PlaySound(SoundID.DD2_BetsyScream with
					{
						Pitch = squawkpitch3
					}, base.NPC.Center);
					if (Main.zenithWorld)
					{
						int spacing3 = 30;
						int amt3 = 3;
						SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.NPC.Center - Vector2.UnitY * 300f);
						if (Main.netMode != 1)
						{
							Vector2 fireFrom3 = default(Vector2);
							for (int m = 0; m < amt3; m++)
							{
								((Vector2)(ref fireFrom3))._002Ector(base.NPC.Center.X + (float)(spacing3 * m) - (float)(spacing3 * amt3 / 2), base.NPC.Center.Y - 900f);
								Vector2 ai4 = base.NPC.Center - fireFrom3;
								float ai5 = Main.rand.Next(100);
								Vector2 velocity10 = Vector2.Normalize(ai4.RotatedByRandom(0.7853981852531433)) * 7f;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom3.X, fireFrom3.Y, velocity10.X, velocity10.Y, ModContent.ProjectileType<RedLightning>(), LightningDamage, 0f, Main.myPlayer, ai4.ToRotation(), ai5);
							}
						}
					}
				}
				if (Main.netMode != 1)
				{
					bool gfbSpawnFlag = Main.zenithWorld && (base.NPC.ai[1] == 145f || base.NPC.ai[1] == 150f || base.NPC.ai[1] == 160f || base.NPC.ai[1] == 165f);
					if (NPC.CountNPCS(ModContent.NPCType<DraconicSwarmer>()) < maxBirbs && ((base.NPC.ai[1] == 140f || (revenge && base.NPC.ai[1] == 155f) || base.NPC.ai[1] == 170f) | gfbSpawnFlag))
					{
						Vector2 follySpawnCenter = base.NPC.Center + ((float)Math.PI * 2f * Main.rand.NextFloat()).ToRotationVector2() * new Vector2(2f, 1f) * 50f * (0.6f + Main.rand.NextFloat() * 0.4f);
						if (Vector2.Distance(follySpawnCenter, player.Center) > 150f)
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)follySpawnCenter.X, (int)follySpawnCenter.Y, ModContent.NPCType<DraconicSwarmer>(), base.NPC.whoAmI);
						}
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= birbSpawnPhaseTimer)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
		else
		{
			if (base.NPC.ai[0] != 5f)
			{
				return;
			}
			base.NPC.damage = 0;
			NPC nPC5 = base.NPC;
			nPC5.velocity *= 0.98f;
			base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt) / 10f;
			float aiGateValue = 120f;
			if (base.NPC.ai[1] == aiGateValue - 30f)
			{
				SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Vector2 follySpawnCenter2 = base.NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)base.NPC.direction) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
					float ai6 = (phase3 ? 2f : 0f) + (enrageScale - 1f);
					if (ai6 > 3f)
					{
						ai6 = 3f;
					}
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), follySpawnCenter2.X, follySpawnCenter2.Y, 0f, 0f, ModContent.ProjectileType<BirbAuraFlare>(), 0, 0f, Main.myPlayer, ai6, base.NPC.target + 1);
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				if (Main.zenithWorld)
				{
					int spacing4 = 30;
					int amt4 = 3;
					SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.NPC.Center - Vector2.UnitY * 300f);
					if (Main.netMode != 1)
					{
						Vector2 fireFrom4 = default(Vector2);
						for (int n = 0; n < amt4; n++)
						{
							((Vector2)(ref fireFrom4))._002Ector(base.NPC.Center.X + (float)(spacing4 * n) - (float)(spacing4 * amt4 / 2), base.NPC.Center.Y - 900f);
							Vector2 ai7 = base.NPC.Center - fireFrom4;
							float ai8 = Main.rand.Next(100);
							Vector2 velocity11 = Vector2.Normalize(ai7.RotatedByRandom(0.7853981852531433)) * 7f;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom4.X, fireFrom4.Y, velocity11.X, velocity11.Y, ModContent.ProjectileType<RedLightning>(), LightningDamage, 0f, Main.myPlayer, ai7.ToRotation(), ai8);
						}
					}
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= aiGateValue)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = (num < (revenge ? 0.75f : 0.5f)) | death;
		bool phase3 = num < (death ? 0.4f : (revenge ? 0.25f : 0.1f));
		bool birbSpawn = base.NPC.ai[0] == 4f && base.NPC.ai[1] > 0f;
		if (Main.zenithWorld)
		{
			base.NPC.frameCounter += 4.0;
		}
		float newPhaseTimer = 180f;
		if (((phase2 && calamityGlobalNPC.newAI[0] < newPhaseTimer && calamityGlobalNPC.newAI[2] != 1f) || (phase3 && calamityGlobalNPC.newAI[1] < newPhaseTimer && calamityGlobalNPC.newAI[3] != 1f)) | birbSpawn)
		{
			float frameGateValue = (birbSpawn ? base.NPC.ai[1] : (phase3 ? calamityGlobalNPC.newAI[1] : calamityGlobalNPC.newAI[0]));
			int frameTimer = 180;
			if (frameGateValue < (float)(frameTimer - 60) || frameGateValue > (float)(frameTimer - 20))
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
				base.NPC.frame.Y = frameHeight * 4;
				if (frameGateValue > (float)(frameTimer - 50) && frameGateValue < (float)(frameTimer - 25))
				{
					base.NPC.frame.Y = frameHeight * 5;
				}
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			int otherFrameTimer = 120;
			if (base.NPC.ai[1] < (float)(otherFrameTimer - 50) || base.NPC.ai[1] > (float)(otherFrameTimer - 10))
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
				base.NPC.frame.Y = frameHeight * 4;
				if (base.NPC.ai[1] > (float)(otherFrameTimer - 40) && base.NPC.ai[1] < (float)(otherFrameTimer - 15))
				{
					base.NPC.frame.Y = frameHeight * 5;
				}
			}
		}
		else
		{
			base.NPC.frameCounter += ((base.NPC.ai[0] == 3.2f) ? 1.5 : 1.0);
			if (base.NPC.frameCounter > 4.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_0958: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_0988: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = (num < (revenge ? 0.75f : 0.5f)) | death;
		bool phase3 = num < (death ? 0.4f : (revenge ? 0.25f : 0.1f));
		float newPhaseTimer = 180f;
		bool phaseSwitchPhase = (phase2 && calamityGlobalNPC.newAI[0] < newPhaseTimer && calamityGlobalNPC.newAI[2] != 1f) || (phase3 && calamityGlobalNPC.newAI[1] < newPhaseTimer && calamityGlobalNPC.newAI[3] != 1f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Color color = drawColor;
		Color altColor = Color.White;
		float lerpDrawTransition = 0f;
		int newAITracker = 120;
		int buffColorDampener = 60;
		if (phase3 && calamityGlobalNPC.newAI[3] == 1f)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.9f, 0.6f, 0.2f, 1f);
		}
		else if (phase2 && calamityGlobalNPC.newAI[2] == 1f)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.7f, 0.7f, 0.3f, 1f);
		}
		else if (phase2 && calamityGlobalNPC.newAI[0] > (float)newAITracker)
		{
			float phase2TranBuff = calamityGlobalNPC.newAI[0] - (float)newAITracker;
			phase2TranBuff /= (float)buffColorDampener;
			color = CalamityGlobalNPC.buffColor(color, 1f - 0.3f * phase2TranBuff, 1f - 0.3f * phase2TranBuff, 1f - 0.7f * phase2TranBuff, 1f);
		}
		int afterimageAmt = 10;
		int afterimageIncrement = 2;
		if (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 3.1f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 4.2f)
		{
			afterimageAmt = 4;
		}
		if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4.1f)
		{
			afterimageAmt = 7;
		}
		if (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 3.2f || (phase2 && calamityGlobalNPC.newAI[2] == 1f))
		{
			altColor = Color.Yellow;
			lerpDrawTransition = 0.5f;
		}
		else
		{
			color = altColor;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += afterimageIncrement)
			{
				Color afterimageColor = color;
				afterimageColor = Color.Lerp(afterimageColor, altColor, lerpDrawTransition);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimageDrawPos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimageDrawPos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		int extraAfterimageAmt = 0;
		float extraAfterimageDampener = 0f;
		float afterimageScaler = 0f;
		if (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 3.1f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 4.2f)
		{
			extraAfterimageAmt = 4;
		}
		if (base.NPC.ai[0] == 5f && base.NPC.ai[1] > 60f)
		{
			extraAfterimageAmt = 6;
			extraAfterimageDampener = 1f - (float)Math.Cos((base.NPC.ai[1] - 60f) / 30f * ((float)Math.PI * 2f));
			extraAfterimageDampener /= 3f;
			afterimageScaler = 40f;
		}
		if (phaseSwitchPhase)
		{
			if (phase3 && calamityGlobalNPC.newAI[1] > (float)newAITracker)
			{
				extraAfterimageAmt = 6;
				extraAfterimageDampener = 1f - (float)Math.Cos((calamityGlobalNPC.newAI[1] - (float)newAITracker) / (float)buffColorDampener * ((float)Math.PI * 2f));
				extraAfterimageDampener /= 3f;
				afterimageScaler = 60f;
			}
			else if (phase2 && calamityGlobalNPC.newAI[0] > (float)newAITracker)
			{
				extraAfterimageAmt = 6;
				extraAfterimageDampener = 1f - (float)Math.Cos((calamityGlobalNPC.newAI[0] - (float)newAITracker) / (float)buffColorDampener * ((float)Math.PI * 2f));
				extraAfterimageDampener /= 3f;
				afterimageScaler = 60f;
			}
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 0; j < extraAfterimageAmt; j++)
			{
				Color extraAfterimageColor = altColor;
				extraAfterimageColor = Color.Lerp(extraAfterimageColor, altColor, lerpDrawTransition);
				extraAfterimageColor = base.NPC.GetAlpha(extraAfterimageColor);
				extraAfterimageColor *= 1f - extraAfterimageDampener;
				Vector2 extraAfterimageDrawPos = base.NPC.Center + ((float)j / (float)extraAfterimageAmt * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * afterimageScaler * extraAfterimageDampener - screenPos;
				extraAfterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				extraAfterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, extraAfterimageDrawPos, (Rectangle?)base.NPC.frame, extraAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Color mainDrawingColor = altColor;
		mainDrawingColor = Color.Lerp(mainDrawingColor, altColor, lerpDrawTransition);
		mainDrawingColor = base.NPC.GetAlpha(mainDrawingColor);
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, (phase3 && calamityGlobalNPC.newAI[3] == 1f) ? mainDrawingColor : base.NPC.GetAlpha(altColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		if (phase2)
		{
			texture2D15 = GlowTexture.Value;
			Color glowmaskColor = Color.Lerp(Color.White, Color.Red, 0.5f);
			altColor = Color.Red;
			lerpDrawTransition = 1f;
			extraAfterimageDampener = 0.5f;
			afterimageScaler = 10f;
			afterimageIncrement = 1;
			if (phaseSwitchPhase)
			{
				float glowmaskDampener = (phase3 ? calamityGlobalNPC.newAI[1] : calamityGlobalNPC.newAI[0]) - (float)newAITracker;
				glowmaskDampener /= (float)buffColorDampener;
				altColor *= glowmaskDampener;
				glowmaskColor *= glowmaskDampener;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int k = 1; k < afterimageAmt; k += afterimageIncrement)
				{
					Color glowmaskAfterimageColor = glowmaskColor;
					glowmaskAfterimageColor = Color.Lerp(glowmaskAfterimageColor, altColor, lerpDrawTransition);
					glowmaskAfterimageColor *= (float)(afterimageAmt - k) / 15f;
					Vector2 glowmaskAfterimageDrawPos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					glowmaskAfterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					glowmaskAfterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					spriteBatch.Draw(texture2D15, glowmaskAfterimageDrawPos, (Rectangle?)base.NPC.frame, glowmaskAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
				for (int l = 1; l < extraAfterimageAmt; l++)
				{
					Color extraGlowmaskAfterimageColor = glowmaskColor;
					extraGlowmaskAfterimageColor = Color.Lerp(extraGlowmaskAfterimageColor, altColor, lerpDrawTransition);
					extraGlowmaskAfterimageColor = base.NPC.GetAlpha(extraGlowmaskAfterimageColor);
					extraGlowmaskAfterimageColor *= 1f - extraAfterimageDampener;
					Vector2 extraGlowmaskAfterimageDrawPos = base.NPC.Center + ((float)l / (float)extraAfterimageAmt * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * afterimageScaler * extraAfterimageDampener - screenPos;
					extraGlowmaskAfterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					extraGlowmaskAfterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					spriteBatch.Draw(texture2D15, extraGlowmaskAfterimageDrawPos, (Rectangle?)base.NPC.frame, extraGlowmaskAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		return false;
	}

	private static Color buffColor(Color newColor, float R, float G, float B, float A)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref newColor)).R = (byte)((float)(int)((Color)(ref newColor)).R * R);
		((Color)(ref newColor)).G = (byte)((float)(int)((Color)(ref newColor)).G * G);
		((Color)(ref newColor)).B = (byte)((float)(int)((Color)(ref newColor)).B * B);
		((Color)(ref newColor)).A = (byte)((float)(int)((Color)(ref newColor)).A * A);
		return newColor;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 3544;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DragonfollyBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] items = new int[3]
		{
			ModContent.ItemType<GildedProboscis>(),
			ModContent.ItemType<GoldenEagle>(),
			ModContent.ItemType<RougeSlash>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, items));
		normalOnly.Add(ModContent.ItemType<EffulgentFeather>(), 1, 25, 30);
		normalOnly.Add(ModContent.ItemType<FollyFeed>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<BumblefuckMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<DragonfollyTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<DragonfollyRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<OmegaHealingPotion>(), 1, 50, 100), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedDragonfolly, ModContent.ItemType<LoreDragonfolly>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<VermillionFlux>(), Main.zenithWorld ? 360 : 180);
		}
	}

	public override void OnKill()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		DownedBossSystem.downedDragonfolly = true;
		CalamityNetcode.SyncWorld();
		if (!Main.zenithWorld)
		{
			return;
		}
		int spacing = 40;
		int amt = 7;
		SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.NPC.Center - Vector2.UnitY * 300f);
		if (Main.netMode != 1)
		{
			Vector2 fireFrom = default(Vector2);
			for (int i = 0; i < amt; i++)
			{
				((Vector2)(ref fireFrom))._002Ector(base.NPC.Center.X + (float)(spacing * i) - (float)(spacing * amt / 2), base.NPC.Center.Y - 900f);
				Vector2 ai0 = base.NPC.Center - fireFrom;
				float ai1 = Main.rand.Next(100);
				Vector2 velocity = Vector2.Normalize(ai0.RotatedByRandom(0.7853981852531433)) * 7f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom.X, fireFrom.Y, velocity.X, velocity.Y, ModContent.ProjectileType<RedLightning>(), LightningDamage, 0f, Main.myPlayer, ai0.ToRotation(), ai1);
			}
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (Main.zenithWorld)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.Bumblebirb");
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 50; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (!Main.dedServ)
		{
			for (int j = 0; j < 6; j++)
			{
				string gore = "Bumble";
				float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
				gore += ((j == 0) ? "Head" : ((j > 1) ? "Leg" : "Wing"));
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>(gore).Type);
			}
		}
	}
}
