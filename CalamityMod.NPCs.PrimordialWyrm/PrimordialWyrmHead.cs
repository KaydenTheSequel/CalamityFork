using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.PrimordialWyrm;

[AutoloadBossHead]
[LongDistanceNetSync]
public class PrimordialWyrmHead : ModNPC
{
	public enum Phase
	{
		ChargeOne,
		LightningRain,
		FastCharge,
		EidolonWyrmSpawn,
		ChargeTwo,
		IceMist,
		ShadowFireballSpin,
		AncientDoomSummon,
		LightningCharge,
		EidolistSpawn,
		FinalPhase
	}

	public static float PWHeadVelocity;

	private const float baseDistance = 1000f;

	private const float baseAttackTriggerDistance = 80f;

	private const float soundDistance = 2800f;

	private const int minLength = 40;

	private const int maxLength = 41;

	private bool TailSpawned;

	private int rotationDirection;

	private float chargeVelocityScalar;

	private const float fastChargeGateValue = 120f;

	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/Scare");

	public static readonly SoundStyle ChargeSound = new SoundStyle("CalamityMod/Sounds/Custom/PrimordialWyrmCharge");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PrimordialWyrmDeath");

	public static Asset<Texture2D> GlowTexture;

	public static int IceMistDamage = 110;

	public static int LightningDamage = 140;

	public static float LightDamageMult = 2.445f;

	public static float DoomDamageMult = 2.445f;

	public float AIState
	{
		get
		{
			return base.NPC.Calamity().newAI[0];
		}
		set
		{
			base.NPC.Calamity().newAI[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 55f;
		value.Position.Y += 5f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "_Lightmask", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 300;
		base.NPC.npcSlots = 50f;
		base.NPC.width = 230;
		base.NPC.height = 138;
		base.NPC.LifeMaxNERB(2500000, 3000000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Opacity = 0f;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(5);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = DeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		if (Main.zenithWorld)
		{
			base.NPC.defense = 999;
			base.NPC.DR_NERD(0.9f);
		}
		else
		{
			base.NPC.defense = 100;
			base.NPC.DR_NERD(0.4f);
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PrimordialWyrm")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(rotationDirection);
		writer.Write(chargeVelocityScalar);
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
		rotationDirection = reader.ReadInt32();
		chargeVelocityScalar = reader.ReadSingle();
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
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0940: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1269: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1272: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2042: Unknown result type (might be due to invalid IL or missing references)
		//IL_2044: Unknown result type (might be due to invalid IL or missing references)
		//IL_2046: Unknown result type (might be due to invalid IL or missing references)
		//IL_204b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2645: Unknown result type (might be due to invalid IL or missing references)
		//IL_2647: Unknown result type (might be due to invalid IL or missing references)
		//IL_2649: Unknown result type (might be due to invalid IL or missing references)
		//IL_264e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2832: Unknown result type (might be due to invalid IL or missing references)
		//IL_2834: Unknown result type (might be due to invalid IL or missing references)
		//IL_2836: Unknown result type (might be due to invalid IL or missing references)
		//IL_283b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2082: Unknown result type (might be due to invalid IL or missing references)
		//IL_208a: Unknown result type (might be due to invalid IL or missing references)
		//IL_208f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2094: Unknown result type (might be due to invalid IL or missing references)
		//IL_2685: Unknown result type (might be due to invalid IL or missing references)
		//IL_268d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2692: Unknown result type (might be due to invalid IL or missing references)
		//IL_2697: Unknown result type (might be due to invalid IL or missing references)
		//IL_31e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_31e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_31e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_31ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a68: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3225: Unknown result type (might be due to invalid IL or missing references)
		//IL_322d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3232: Unknown result type (might be due to invalid IL or missing references)
		//IL_3237: Unknown result type (might be due to invalid IL or missing references)
		//IL_33c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_33dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_33e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_33ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_33f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_33ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_3404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1705: Unknown result type (might be due to invalid IL or missing references)
		//IL_170b: Unknown result type (might be due to invalid IL or missing references)
		//IL_170d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1718: Unknown result type (might be due to invalid IL or missing references)
		//IL_1725: Unknown result type (might be due to invalid IL or missing references)
		//IL_172b: Unknown result type (might be due to invalid IL or missing references)
		//IL_172d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1732: Unknown result type (might be due to invalid IL or missing references)
		//IL_173a: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_174e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1799: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_185c: Unknown result type (might be due to invalid IL or missing references)
		//IL_186c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1872: Unknown result type (might be due to invalid IL or missing references)
		//IL_1874: Unknown result type (might be due to invalid IL or missing references)
		//IL_187f: Unknown result type (might be due to invalid IL or missing references)
		//IL_188c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1892: Unknown result type (might be due to invalid IL or missing references)
		//IL_1894: Unknown result type (might be due to invalid IL or missing references)
		//IL_1899: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1900: Unknown result type (might be due to invalid IL or missing references)
		//IL_1919: Unknown result type (might be due to invalid IL or missing references)
		//IL_191e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1931: Unknown result type (might be due to invalid IL or missing references)
		//IL_1936: Unknown result type (might be due to invalid IL or missing references)
		//IL_1938: Unknown result type (might be due to invalid IL or missing references)
		//IL_193d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2380: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2408: Unknown result type (might be due to invalid IL or missing references)
		//IL_2415: Unknown result type (might be due to invalid IL or missing references)
		//IL_241b: Unknown result type (might be due to invalid IL or missing references)
		//IL_241d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2422: Unknown result type (might be due to invalid IL or missing references)
		//IL_242a: Unknown result type (might be due to invalid IL or missing references)
		//IL_242f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2439: Unknown result type (might be due to invalid IL or missing references)
		//IL_243e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2458: Unknown result type (might be due to invalid IL or missing references)
		//IL_2477: Unknown result type (might be due to invalid IL or missing references)
		//IL_247c: Unknown result type (might be due to invalid IL or missing references)
		//IL_247e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2483: Unknown result type (might be due to invalid IL or missing references)
		//IL_2498: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_251b: Unknown result type (might be due to invalid IL or missing references)
		//IL_252b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2531: Unknown result type (might be due to invalid IL or missing references)
		//IL_2533: Unknown result type (might be due to invalid IL or missing references)
		//IL_253e: Unknown result type (might be due to invalid IL or missing references)
		//IL_254b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2551: Unknown result type (might be due to invalid IL or missing references)
		//IL_2553: Unknown result type (might be due to invalid IL or missing references)
		//IL_2558: Unknown result type (might be due to invalid IL or missing references)
		//IL_2560: Unknown result type (might be due to invalid IL or missing references)
		//IL_2565: Unknown result type (might be due to invalid IL or missing references)
		//IL_256f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2574: Unknown result type (might be due to invalid IL or missing references)
		//IL_258e: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_25bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_341d: Unknown result type (might be due to invalid IL or missing references)
		//IL_123a: Unknown result type (might be due to invalid IL or missing references)
		//IL_123c: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1243: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2013: Unknown result type (might be due to invalid IL or missing references)
		//IL_2015: Unknown result type (might be due to invalid IL or missing references)
		//IL_2017: Unknown result type (might be due to invalid IL or missing references)
		//IL_201c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e49: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_3442: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_350c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_27a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_27cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e98: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eba: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ebf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3141: Unknown result type (might be due to invalid IL or missing references)
		//IL_315f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3467: Unknown result type (might be due to invalid IL or missing references)
		//IL_35a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_132d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1338: Unknown result type (might be due to invalid IL or missing references)
		//IL_28e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2907: Unknown result type (might be due to invalid IL or missing references)
		//IL_3347: Unknown result type (might be due to invalid IL or missing references)
		//IL_3360: Unknown result type (might be due to invalid IL or missing references)
		//IL_3366: Unknown result type (might be due to invalid IL or missing references)
		//IL_3368: Unknown result type (might be due to invalid IL or missing references)
		//IL_336d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3648: Unknown result type (might be due to invalid IL or missing references)
		//IL_3655: Unknown result type (might be due to invalid IL or missing references)
		//IL_3664: Unknown result type (might be due to invalid IL or missing references)
		//IL_3671: Unknown result type (might be due to invalid IL or missing references)
		//IL_348f: Unknown result type (might be due to invalid IL or missing references)
		//IL_353f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1353: Unknown result type (might be due to invalid IL or missing references)
		//IL_135e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2777: Unknown result type (might be due to invalid IL or missing references)
		//IL_2784: Unknown result type (might be due to invalid IL or missing references)
		//IL_2789: Unknown result type (might be due to invalid IL or missing references)
		//IL_3739: Unknown result type (might be due to invalid IL or missing references)
		//IL_3759: Unknown result type (might be due to invalid IL or missing references)
		//IL_375e: Unknown result type (might be due to invalid IL or missing references)
		//IL_36a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_36c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_36cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_35db: Unknown result type (might be due to invalid IL or missing references)
		//IL_3564: Unknown result type (might be due to invalid IL or missing references)
		//IL_2134: Unknown result type (might be due to invalid IL or missing references)
		//IL_2139: Unknown result type (might be due to invalid IL or missing references)
		//IL_213b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2143: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ef1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_3317: Unknown result type (might be due to invalid IL or missing references)
		//IL_3324: Unknown result type (might be due to invalid IL or missing references)
		//IL_3329: Unknown result type (might be due to invalid IL or missing references)
		//IL_3603: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13be: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_2161: Unknown result type (might be due to invalid IL or missing references)
		//IL_2163: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f40: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_13dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13de: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f89: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c40: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2200: Unknown result type (might be due to invalid IL or missing references)
		//IL_220b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2210: Unknown result type (might be due to invalid IL or missing references)
		//IL_2215: Unknown result type (might be due to invalid IL or missing references)
		//IL_221c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2221: Unknown result type (might be due to invalid IL or missing references)
		//IL_223c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2241: Unknown result type (might be due to invalid IL or missing references)
		//IL_226d: Unknown result type (might be due to invalid IL or missing references)
		//IL_227d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2284: Unknown result type (might be due to invalid IL or missing references)
		//IL_2289: Unknown result type (might be due to invalid IL or missing references)
		//IL_2294: Unknown result type (might be due to invalid IL or missing references)
		//IL_2299: Unknown result type (might be due to invalid IL or missing references)
		//IL_229e: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_22aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22be: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2304: Unknown result type (might be due to invalid IL or missing references)
		//IL_230b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2310: Unknown result type (might be due to invalid IL or missing references)
		//IL_2324: Unknown result type (might be due to invalid IL or missing references)
		//IL_2329: Unknown result type (might be due to invalid IL or missing references)
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_1477: Unknown result type (might be due to invalid IL or missing references)
		//IL_147e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1483: Unknown result type (might be due to invalid IL or missing references)
		//IL_148e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1493: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_151f: Unknown result type (might be due to invalid IL or missing references)
		//IL_152f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1536: Unknown result type (might be due to invalid IL or missing references)
		//IL_153b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1546: Unknown result type (might be due to invalid IL or missing references)
		//IL_154b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1550: Unknown result type (might be due to invalid IL or missing references)
		//IL_1561: Unknown result type (might be due to invalid IL or missing references)
		//IL_156c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1571: Unknown result type (might be due to invalid IL or missing references)
		//IL_1578: Unknown result type (might be due to invalid IL or missing references)
		//IL_157d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1591: Unknown result type (might be due to invalid IL or missing references)
		//IL_1596: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15db: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1601: Unknown result type (might be due to invalid IL or missing references)
		//IL_1606: Unknown result type (might be due to invalid IL or missing references)
		//IL_160d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1612: Unknown result type (might be due to invalid IL or missing references)
		//IL_1626: Unknown result type (might be due to invalid IL or missing references)
		//IL_162b: Unknown result type (might be due to invalid IL or missing references)
		//IL_163e: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.adultEidolonWyrmHead = base.NPC.whoAmI;
		bool death = CalamityWorld.death;
		bool revenge = CalamityWorld.revenge;
		bool expertMode = Main.expertMode;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < 0.8f;
		bool phase3 = lifeRatio < 0.6f;
		bool phase4 = lifeRatio < 0.4f;
		bool phase5 = lifeRatio < 0.2f;
		bool phase6 = lifeRatio < 0.05f;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool targetDownDeep = player.Calamity().ZoneAbyssLayer4;
		bool targetOnMount = player.mount.Active;
		base.NPC.Calamity().CurrentlyEnraged = !targetDownDeep;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (!TailSpawned && base.NPC.ai[0] == 0f && Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
		{
			SoundEngine.PlaySound(in SpawnSound, Main.LocalPlayer.Center);
		}
		if (Main.netMode != 1 && !TailSpawned && base.NPC.ai[0] == 0f)
		{
			int Previous = base.NPC.whoAmI;
			for (int i = 0; i < 41; i++)
			{
				int lol = ((i < 0 || i >= 40) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PrimordialWyrmTail>(), base.NPC.whoAmI) : ((i % 2 != 0) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PrimordialWyrmBodyAlt>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PrimordialWyrmBody>(), base.NPC.whoAmI)));
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
				Main.npc[Previous].ai[3] = i / 2;
			}
			TailSpawned = true;
		}
		bool targetDead = false;
		if (player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (player.dead)
			{
				targetDead = true;
				base.NPC.ai[3] = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				base.NPC.localAI[3] = 0f;
				AIState = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				chargeVelocityScalar = 0f;
				rotationDirection = 0;
				base.NPC.velocity.Y += 3f;
				if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
				{
					base.NPC.velocity.Y += 3f;
				}
				if ((double)base.NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int a = 0; a < Main.maxNPCs; a++)
					{
						if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<PrimordialWyrmBodyAlt>() || Main.npc[a].type == ModContent.NPCType<PrimordialWyrmBody>() || Main.npc[a].type == ModContent.NPCType<PrimordialWyrmTail>())
						{
							Main.npc[a].active = false;
						}
					}
				}
			}
		}
		float chargePhaseGateValue = (death ? 180f : (revenge ? 210f : (expertMode ? 240f : 300f)));
		float lightningRainDuration = 180f;
		float eidolonWyrmPhaseDuration = (death ? 120f : (revenge ? 135f : (expertMode ? 150f : 180f)));
		float iceMistDuration = 180f;
		float spinPhaseDuration = (death ? 240f : (revenge ? 255f : (expertMode ? 270f : 300f)));
		float ancientDoomPhaseGateValue = 30f;
		float ancientDoomGateValue = (death ? 95f : (revenge ? 100f : (expertMode ? 105f : 120f)));
		float lightningChargePhaseGateValue = (death ? 120f : (revenge ? 135f : (expertMode ? 150f : 180f)));
		if (Main.getGoodWorld)
		{
			lightningRainDuration *= 0.5f;
			eidolonWyrmPhaseDuration *= 0.25f;
			iceMistDuration *= 0.5f;
		}
		bool immuneToSlowingDebuffs = AIState == 10f || AIState == 6f;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		bool num = calamityGlobalNPC.newAI[2] >= chargePhaseGateValue && calamityGlobalNPC.newAI[2] <= chargePhaseGateValue + 1f && (AIState == 0f || AIState == 4f || AIState == 2f);
		bool invisiblePartOfLightningChargePhase = calamityGlobalNPC.newAI[2] >= lightningChargePhaseGateValue && calamityGlobalNPC.newAI[2] <= lightningChargePhaseGateValue + 1f && AIState == 8f;
		bool invisiblePhase = AIState == 1f || AIState == 5f || AIState == 7f;
		if (!num && !invisiblePartOfLightningChargePhase && !invisiblePhase)
		{
			base.NPC.Opacity += 0.2f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
		}
		else
		{
			base.NPC.Opacity -= 0.05f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
		}
		base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		int direction = base.NPC.direction;
		base.NPC.direction = (base.NPC.spriteDirection = ((base.NPC.velocity.X > 0f) ? 1 : (-1)));
		if (direction != base.NPC.direction)
		{
			base.NPC.netUpdate = true;
		}
		Vector2 destination = player.Center;
		Vector2 chargeVector = Vector2.Zero;
		float chargeDistance = 1000f;
		float chargeLocationDistance = 80f;
		switch ((int)calamityGlobalNPC.newAI[1])
		{
		case 0:
			chargeVector.X -= chargeDistance;
			break;
		case 1:
			chargeVector.X += chargeDistance;
			break;
		case 2:
			chargeVector.Y -= chargeDistance;
			break;
		case 3:
			chargeVector.Y += chargeDistance;
			break;
		case 4:
			chargeVector.X -= chargeDistance;
			chargeVector.Y -= chargeDistance;
			break;
		case 5:
			chargeVector.X += chargeDistance;
			chargeVector.Y += chargeDistance;
			break;
		case 6:
			chargeVector.X -= chargeDistance;
			chargeVector.Y += chargeDistance;
			break;
		case 7:
			chargeVector.X += chargeDistance;
			chargeVector.Y -= chargeDistance;
			break;
		}
		Vector2 chargeLocation = destination + chargeVector;
		Vector2 lightningRainLocation = default(Vector2);
		((Vector2)(ref lightningRainLocation))._002Ector(0f, -1000f);
		float lightningRainLocationDistance = 80f;
		Vector2 eidolonWyrmPhaseLocation = default(Vector2);
		((Vector2)(ref eidolonWyrmPhaseLocation))._002Ector(0f, 1000f);
		int eidolistScale = (death ? 3 : (revenge ? 2 : (expertMode ? 1 : 0)));
		int maxEidolists = (targetDownDeep ? 3 : 6) + eidolistScale;
		Vector2 iceMistLocation = default(Vector2);
		((Vector2)(ref iceMistLocation))._002Ector(0f, 1000f);
		float iceMistLocationDistance = 80f;
		float spinRadius = 1000f;
		Vector2 spinLocation = default(Vector2);
		((Vector2)(ref spinLocation))._002Ector(0f, 0f - spinRadius);
		float spinLocationDistance = 80f;
		Vector2 ancientDoomLocation = default(Vector2);
		((Vector2)(ref ancientDoomLocation))._002Ector(0f, -1000f);
		int ancientDoomScale = (death ? 3 : (revenge ? 2 : (expertMode ? 1 : 0)));
		int ancientDoomLimit = (targetDownDeep ? 4 : 8) + ancientDoomScale;
		int ancientDoomDistance = (death ? 520 : (revenge ? 535 : (expertMode ? 550 : 600)));
		float maxAncientDoomRings = 3f;
		Vector2 lightningChargeVector = ((base.NPC.localAI[2] == 0f) ? new Vector2(1000f, 0f) : new Vector2(-1000f, 0f));
		float lightningChargeLocationDistance = 80f;
		Vector2 lightningChargeLocation = destination + lightningChargeVector;
		float lightningSpawnY = 540f;
		Vector2 lightningSpawnLocation = default(Vector2);
		((Vector2)(ref lightningSpawnLocation))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - lightningSpawnY);
		int numLightningBolts = (death ? 10 : (revenge ? 8 : (expertMode ? 6 : 4)));
		float distanceBetweenBolts = lightningSpawnY * 2f / (float)numLightningBolts;
		float velocityScale = (death ? 1.8f : (revenge ? 1.5f : (expertMode ? 1.2f : 0f)));
		float baseVelocity = (targetDownDeep ? 10f : 15f) + (targetDownDeep ? velocityScale : (velocityScale * 1.5f));
		if (Main.getGoodWorld)
		{
			baseVelocity *= 1.15f;
		}
		float turnSpeed = baseVelocity * 0.015f;
		float normalChargeVelocityMult = MathHelper.Lerp(1f, 2f, chargeVelocityScalar);
		float normalChargeTurnSpeedMult = MathHelper.Lerp(1f, 4f, chargeVelocityScalar);
		float invisiblePhaseVelocityMult = MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
		float invisiblePhaseTurnSpeedMult = MathHelper.Lerp(1f, 3f, chargeVelocityScalar);
		float fastChargeVelocityMult = MathHelper.Lerp(1f, 3f, chargeVelocityScalar);
		float fastChargeTurnSpeedMult = MathHelper.Lerp(1f, 8f, chargeVelocityScalar);
		float chargeVelocityScalarIncrement = 0.005f;
		float totalChargeDistance = 3000f;
		bool lookingAtTarget = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation().AngleTowards(base.NPC.velocity.ToRotation(), (float)Math.PI / 4f) == base.NPC.velocity.ToRotation();
		if (!targetDownDeep)
		{
			calamityGlobalNPC.newAI[3]++;
			if (calamityGlobalNPC.newAI[3] >= 300f)
			{
				calamityGlobalNPC.newAI[3] = 0f;
				SoundEngine.PlaySound(in SoundID.Item117, player.Center);
				for (int j = 0; j < 40; j++)
				{
					Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, 113, 0f - player.velocity.X, 0f - player.velocity.Y, 1, Color.SkyBlue);
					((Vector2)(ref dust.velocity)).Normalize();
					dust.velocity *= (float)Main.rand.Next(20);
					dust.noGravity = true;
					if (Main.rand.NextBool())
					{
						dust.scale = 0.5f;
						dust.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int k = 0; k < 30; k++)
				{
					Vector2 velocityToNPC = player.DirectionTo(base.NPC.Center) * (float)Main.rand.Next(40);
					Dust.NewDustDirect(player.position, player.width, player.height, 127, velocityToNPC.X * 5f, velocityToNPC.Y * 5f, 1, default(Color), 3f).noGravity = true;
					Dust dust2 = Dust.NewDustDirect(player.position, player.width, player.height, 127, Main.rand.Next(5), Main.rand.Next(5), 1);
					dust2.fadeIn = 1f + (float)Main.rand.Next(8) * 0.1f;
					dust2.noGravity = true;
				}
				if (Main.netMode != 1 && ((Vector2)(ref player.velocity)).Length() > 0f)
				{
					Player player2 = player;
					player2.velocity *= -3f;
				}
			}
		}
		Vector2 center;
		switch ((int)AIState)
		{
		case 0:
			if (calamityGlobalNPC.newAI[2] >= chargePhaseGateValue)
			{
				ChargeDust();
				chargeVelocityScalar += chargeVelocityScalarIncrement;
				if (chargeVelocityScalar > 1f)
				{
					chargeVelocityScalar = 1f;
				}
				baseVelocity *= normalChargeVelocityMult;
				turnSpeed *= normalChargeTurnSpeedMult;
				center = chargeLocation - base.NPC.Center;
				if (((Vector2)(ref center)).Length() < chargeLocationDistance || calamityGlobalNPC.newAI[2] > chargePhaseGateValue)
				{
					if (chargeVelocityScalar < 1f)
					{
						chargeVelocityScalar = 1f;
					}
					if (calamityGlobalNPC.newAI[2] < chargePhaseGateValue + 1f)
					{
						calamityGlobalNPC.newAI[2]++;
					}
					if (!lookingAtTarget && calamityGlobalNPC.newAI[2] < chargePhaseGateValue + 2f)
					{
						baseVelocity /= normalChargeVelocityMult;
						turnSpeed /= normalChargeTurnSpeedMult;
						break;
					}
					if (calamityGlobalNPC.newAI[2] == chargePhaseGateValue + 1f && Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
					{
						SoundEngine.PlaySound(in ChargeSound, Main.LocalPlayer.Center);
					}
					calamityGlobalNPC.newAI[2]++;
					base.NPC.Opacity = 1f;
					float totalChargeTime = totalChargeDistance / baseVelocity + chargePhaseGateValue + 1f;
					if (calamityGlobalNPC.newAI[2] > totalChargeTime)
					{
						base.NPC.ai[3]++;
						float maxCharges = (phase4 ? 1 : (phase2 ? 2 : 3));
						if (base.NPC.ai[3] >= maxCharges)
						{
							base.NPC.ai[3] = 0f;
							AIState = (phase4 ? 6f : 1f);
						}
						else if (phase2)
						{
							AIState = 6f;
						}
						calamityGlobalNPC.newAI[1]++;
						if (calamityGlobalNPC.newAI[1] > 7f)
						{
							calamityGlobalNPC.newAI[1] = 0f;
						}
						calamityGlobalNPC.newAI[2] = 0f;
						chargeVelocityScalar = 0f;
						FinalPhaseCheck();
						base.NPC.TargetClosest();
					}
				}
				else
				{
					destination += chargeVector;
				}
			}
			else
			{
				calamityGlobalNPC.newAI[2]++;
			}
			break;
		case 1:
		{
			destination += lightningRainLocation;
			chargeVelocityScalar += chargeVelocityScalarIncrement;
			if (chargeVelocityScalar > 1f)
			{
				chargeVelocityScalar = 1f;
			}
			baseVelocity *= invisiblePhaseVelocityMult;
			turnSpeed *= invisiblePhaseTurnSpeedMult;
			center = destination - base.NPC.Center;
			if (!(((Vector2)(ref center)).Length() < lightningRainLocationDistance) && !(calamityGlobalNPC.newAI[2] > 0f))
			{
				break;
			}
			if (calamityGlobalNPC.newAI[2] % 30f == 0f && calamityGlobalNPC.newAI[2] < lightningRainDuration)
			{
				if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
				{
					SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, Main.LocalPlayer.Center);
				}
				if (Main.netMode != 1)
				{
					int[] whoAmIArray = new int[2];
					Vector2[] targetCenterArray = (Vector2[])(object)new Vector2[2];
					int numProjectiles = 0;
					float maxDistance2 = 2400f;
					ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Player plr = enumerator.Current;
						if (plr.dead)
						{
							continue;
						}
						Vector2 playerCenter2 = plr.Center;
						if (Vector2.Distance(playerCenter2, base.NPC.Center) < maxDistance2)
						{
							whoAmIArray[numProjectiles] = plr.whoAmI;
							targetCenterArray[numProjectiles] = playerCenter2;
							if (++numProjectiles >= targetCenterArray.Length)
							{
								break;
							}
						}
					}
					float predictionAmt = (targetDownDeep ? 45f : 60f);
					float lightningVelocityScale = (death ? 0.9f : (revenge ? 0.75f : (expertMode ? 0.6f : 0f)));
					float lightningVelocity = ((targetDownDeep && !targetOnMount) ? 6f : 9f) + ((targetDownDeep && !targetOnMount) ? lightningVelocityScale : (lightningVelocityScale * 1.5f));
					for (int n = 0; n < numProjectiles; n++)
					{
						Vector2 projectileDestination = targetCenterArray[n] + Main.player[whoAmIArray[n]].velocity * predictionAmt - base.NPC.Center;
						float ai3 = Main.rand.Next(100);
						Vector2 projectileVelocity = Vector2.Normalize(projectileDestination.RotatedByRandom(0.7853981852531433)) * lightningVelocity;
						int type = 466;
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity, type, LightningDamage, 0f, Main.myPlayer, projectileDestination.ToRotation(), ai3);
						Main.projectile[proj].tileCollide = false;
						projectileDestination = targetCenterArray[n] - Main.player[whoAmIArray[n]].velocity * predictionAmt - base.NPC.Center;
						ai3 = Main.rand.Next(100);
						projectileVelocity = Vector2.Normalize(projectileDestination.RotatedByRandom(0.7853981852531433)) * lightningVelocity;
						proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity, type, LightningDamage, 0f, Main.myPlayer, projectileDestination.ToRotation(), ai3);
						Main.projectile[proj].tileCollide = false;
						projectileDestination = targetCenterArray[n] - base.NPC.Center;
						ai3 = Main.rand.Next(100);
						projectileVelocity = Vector2.Normalize(projectileDestination.RotatedByRandom(0.7853981852531433)) * lightningVelocity;
						proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity, type, LightningDamage, 0f, Main.myPlayer, projectileDestination.ToRotation(), ai3);
						Main.projectile[proj].tileCollide = false;
					}
				}
			}
			calamityGlobalNPC.newAI[2]++;
			Lighting.AddLight(base.NPC.Center, 0.4f, 0.85f, 0.9f);
			float rotation = MathHelper.Clamp((float)Main.rand.NextDouble() * 1f - 0.5f, -0.5f, 0.5f);
			Vector2 spinningpoint = new Vector2((float)(-base.NPC.width) * 0.2f * base.NPC.scale, 0f);
			double radians3 = rotation * ((float)Math.PI * 2f);
			center = default(Vector2);
			Vector2 spinningpoint2 = Utils.RotatedBy(spinningpoint, radians3, center);
			double radians4 = base.NPC.velocity.ToRotation();
			center = default(Vector2);
			Vector2 dustPosition = spinningpoint2.RotatedBy(radians4, center);
			int dust3 = Dust.NewDust(base.NPC.Center - Vector2.One * 5f, 10, 10, 226, (0f - base.NPC.velocity.X) / 3f, (0f - base.NPC.velocity.Y) / 3f, 150, Color.Transparent, 0.7f);
			Main.dust[dust3].position = base.NPC.Center + dustPosition;
			Main.dust[dust3].velocity = Vector2.Normalize(Main.dust[dust3].position - base.NPC.Center) * 2f;
			Main.dust[dust3].noGravity = true;
			rotation = MathHelper.Clamp((float)Main.rand.NextDouble() * 1f - 0.5f, -0.5f, 0.5f);
			Vector2 spinningpoint3 = new Vector2((float)(-base.NPC.width) * 0.6f * base.NPC.scale, 0f);
			double radians5 = rotation * ((float)Math.PI * 2f);
			center = default(Vector2);
			Vector2 spinningpoint4 = Utils.RotatedBy(spinningpoint3, radians5, center);
			double radians6 = base.NPC.velocity.ToRotation();
			center = default(Vector2);
			dustPosition = spinningpoint4.RotatedBy(radians6, center);
			dust3 = Dust.NewDust(base.NPC.Center - Vector2.One * 5f, 10, 10, 226, (0f - base.NPC.velocity.X) / 3f, (0f - base.NPC.velocity.Y) / 3f, 150, Color.Transparent, 0.7f);
			Main.dust[dust3].velocity = Vector2.Zero;
			Main.dust[dust3].position = base.NPC.Center + dustPosition;
			Main.dust[dust3].noGravity = true;
			if (calamityGlobalNPC.newAI[2] >= lightningRainDuration)
			{
				base.NPC.localAI[0] = 0f;
				AIState = 2f;
				calamityGlobalNPC.newAI[2] = 120f;
				chargeVelocityScalar = 0f;
				FinalPhaseCheck();
				base.NPC.TargetClosest();
			}
			break;
		}
		case 2:
			if (calamityGlobalNPC.newAI[2] >= chargePhaseGateValue)
			{
				ChargeDust();
				chargeVelocityScalar += chargeVelocityScalarIncrement;
				if (chargeVelocityScalar > 1f)
				{
					chargeVelocityScalar = 1f;
				}
				baseVelocity *= fastChargeVelocityMult;
				turnSpeed *= fastChargeTurnSpeedMult;
				center = chargeLocation - base.NPC.Center;
				if (((Vector2)(ref center)).Length() < chargeLocationDistance || calamityGlobalNPC.newAI[2] > chargePhaseGateValue)
				{
					if (chargeVelocityScalar < 1f)
					{
						chargeVelocityScalar = 1f;
					}
					if (calamityGlobalNPC.newAI[2] < chargePhaseGateValue + 1f)
					{
						calamityGlobalNPC.newAI[2]++;
					}
					if (!lookingAtTarget && calamityGlobalNPC.newAI[2] < chargePhaseGateValue + 2f)
					{
						baseVelocity /= fastChargeVelocityMult;
						turnSpeed /= fastChargeTurnSpeedMult;
						break;
					}
					if (calamityGlobalNPC.newAI[2] == chargePhaseGateValue + 1f && Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
					{
						SoundEngine.PlaySound(in ChargeSound, Main.LocalPlayer.Center);
					}
					calamityGlobalNPC.newAI[2]++;
					base.NPC.Opacity = 1f;
					float totalChargeTime3 = totalChargeDistance / baseVelocity + chargePhaseGateValue + 1f;
					if (!(calamityGlobalNPC.newAI[2] > totalChargeTime3))
					{
						break;
					}
					if (!phase5)
					{
						AIState = ((base.NPC.localAI[0] == 0f) ? 3f : 9f);
						calamityGlobalNPC.newAI[2] = 0f;
					}
					else
					{
						base.NPC.ai[3]++;
						if (base.NPC.ai[3] >= 2f)
						{
							base.NPC.ai[3] = 0f;
							AIState = ((base.NPC.localAI[0] == 0f) ? 4f : 0f);
							calamityGlobalNPC.newAI[2] = 0f;
						}
						else
						{
							calamityGlobalNPC.newAI[2] = 120f;
						}
					}
					calamityGlobalNPC.newAI[1]++;
					if (calamityGlobalNPC.newAI[1] > 7f)
					{
						calamityGlobalNPC.newAI[1] = 0f;
					}
					chargeVelocityScalar = 0f;
					FinalPhaseCheck();
					base.NPC.TargetClosest();
				}
				else
				{
					destination += chargeVector;
				}
			}
			else
			{
				calamityGlobalNPC.newAI[2]++;
			}
			break;
		case 3:
			destination += eidolonWyrmPhaseLocation;
			if (Main.netMode != 1 && calamityGlobalNPC.newAI[2] == 0f && !NPC.AnyNPCs(ModContent.NPCType<EidolonWyrmHead>()))
			{
				NPC.SpawnOnPlayer(base.NPC.FindClosestPlayer(), ModContent.NPCType<EidolonWyrmHead>());
			}
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= eidolonWyrmPhaseDuration)
			{
				AIState = 4f;
				calamityGlobalNPC.newAI[2] = 0f;
				FinalPhaseCheck();
				base.NPC.TargetClosest();
			}
			break;
		case 4:
			if (calamityGlobalNPC.newAI[2] >= chargePhaseGateValue)
			{
				ChargeDust();
				chargeVelocityScalar += chargeVelocityScalarIncrement;
				if (chargeVelocityScalar > 1f)
				{
					chargeVelocityScalar = 1f;
				}
				baseVelocity *= normalChargeVelocityMult;
				turnSpeed *= normalChargeTurnSpeedMult;
				center = chargeLocation - base.NPC.Center;
				if (((Vector2)(ref center)).Length() < chargeLocationDistance || calamityGlobalNPC.newAI[2] > chargePhaseGateValue)
				{
					if (chargeVelocityScalar < 1f)
					{
						chargeVelocityScalar = 1f;
					}
					if (calamityGlobalNPC.newAI[2] < chargePhaseGateValue + 1f)
					{
						calamityGlobalNPC.newAI[2]++;
					}
					if (!lookingAtTarget && calamityGlobalNPC.newAI[2] < chargePhaseGateValue + 2f)
					{
						baseVelocity /= normalChargeVelocityMult;
						turnSpeed /= normalChargeTurnSpeedMult;
						break;
					}
					if (calamityGlobalNPC.newAI[2] == chargePhaseGateValue + 1f && Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
					{
						SoundEngine.PlaySound(in ChargeSound, Main.LocalPlayer.Center);
					}
					calamityGlobalNPC.newAI[2]++;
					base.NPC.Opacity = 1f;
					float totalChargeTime2 = totalChargeDistance / baseVelocity + chargePhaseGateValue + 1f;
					if (calamityGlobalNPC.newAI[2] > totalChargeTime2)
					{
						base.NPC.ai[3]++;
						float maxCharges2 = (phase4 ? 1 : (phase3 ? 2 : 3));
						if (base.NPC.ai[3] >= maxCharges2)
						{
							base.NPC.ai[3] = 0f;
							AIState = (phase4 ? 7f : 5f);
						}
						else if (phase3)
						{
							AIState = 7f;
						}
						calamityGlobalNPC.newAI[1]++;
						if (calamityGlobalNPC.newAI[1] > 7f)
						{
							calamityGlobalNPC.newAI[1] = 0f;
						}
						calamityGlobalNPC.newAI[2] = 0f;
						chargeVelocityScalar = 0f;
						FinalPhaseCheck();
						base.NPC.TargetClosest();
					}
				}
				else
				{
					destination += chargeVector;
				}
			}
			else
			{
				calamityGlobalNPC.newAI[2]++;
			}
			break;
		case 5:
		{
			destination += iceMistLocation;
			chargeVelocityScalar += chargeVelocityScalarIncrement;
			if (chargeVelocityScalar > 1f)
			{
				chargeVelocityScalar = 1f;
			}
			baseVelocity *= invisiblePhaseVelocityMult;
			turnSpeed *= invisiblePhaseTurnSpeedMult;
			center = destination - base.NPC.Center;
			if (!(((Vector2)(ref center)).Length() < iceMistLocationDistance) && !(calamityGlobalNPC.newAI[2] > 0f))
			{
				break;
			}
			if (calamityGlobalNPC.newAI[2] % 60f == 0f && calamityGlobalNPC.newAI[2] < iceMistDuration && Main.netMode != 1)
			{
				int[] whoAmIArray2 = new int[2];
				Vector2[] targetCenterArray2 = (Vector2[])(object)new Vector2[2];
				int numProjectiles2 = 0;
				float maxDistance3 = 2400f;
				ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Player plr2 = enumerator2.Current;
					if (plr2.dead)
					{
						continue;
					}
					Vector2 playerCenter3 = plr2.Center;
					if (Vector2.Distance(playerCenter3, base.NPC.Center) < maxDistance3)
					{
						whoAmIArray2[numProjectiles2] = plr2.whoAmI;
						targetCenterArray2[numProjectiles2] = playerCenter3;
						if (++numProjectiles2 >= targetCenterArray2.Length)
						{
							break;
						}
					}
				}
				float predictionAmt2 = (targetDownDeep ? 90f : 120f);
				float iceMistVelocityScale = (death ? 1.8f : (revenge ? 1.5f : (expertMode ? 1.2f : 0f)));
				float iceMistVelocity = (targetDownDeep ? 12f : 18f) + (targetDownDeep ? iceMistVelocityScale : (iceMistVelocityScale * 1.5f));
				for (int num2 = 0; num2 < numProjectiles2; num2++)
				{
					Vector2 projectileVelocity2 = Vector2.Normalize(targetCenterArray2[num2] + Main.player[whoAmIArray2[num2]].velocity * predictionAmt2 - base.NPC.Center) * iceMistVelocity;
					int type2 = 464;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity2, type2, IceMistDamage, 0f, Main.myPlayer, 0f, 1f);
					projectileVelocity2 = Vector2.Normalize(targetCenterArray2[num2] - Main.player[whoAmIArray2[num2]].velocity * predictionAmt2 - base.NPC.Center) * iceMistVelocity;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity2, type2, IceMistDamage, 0f, Main.myPlayer, 0f, 1f);
					projectileVelocity2 = Vector2.Normalize(targetCenterArray2[num2] - base.NPC.Center) * iceMistVelocity;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projectileVelocity2, type2, IceMistDamage, 0f, Main.myPlayer, 0f, 1f);
				}
			}
			calamityGlobalNPC.newAI[2]++;
			Lighting.AddLight(base.NPC.Center, 0.3f, 0.75f, 0.9f);
			float rotation2 = MathHelper.Clamp((float)Main.rand.NextDouble() * 1f - 0.5f, -0.5f, 0.5f);
			Vector2 spinningpoint5 = new Vector2((float)(-base.NPC.width) * 0.2f * base.NPC.scale, 0f);
			double radians7 = rotation2 * ((float)Math.PI * 2f);
			center = default(Vector2);
			Vector2 spinningpoint6 = Utils.RotatedBy(spinningpoint5, radians7, center);
			double radians8 = base.NPC.velocity.ToRotation();
			center = default(Vector2);
			Vector2 dustPosition2 = spinningpoint6.RotatedBy(radians8, center);
			int dust4 = Dust.NewDust(base.NPC.Center - Vector2.One * 5f, 10, 10, 197, 0f, 0f, 100, Color.Transparent);
			Main.dust[dust4].position = base.NPC.Center + dustPosition2;
			Main.dust[dust4].velocity = Vector2.Normalize(Main.dust[dust4].position - base.NPC.Center) * 2f;
			Main.dust[dust4].noGravity = true;
			rotation2 = MathHelper.Clamp((float)Main.rand.NextDouble() * 1f - 0.5f, -0.5f, 0.5f);
			Vector2 spinningpoint7 = new Vector2((float)(-base.NPC.width) * 0.6f * base.NPC.scale, 0f);
			double radians9 = rotation2 * ((float)Math.PI * 2f);
			center = default(Vector2);
			Vector2 spinningpoint8 = Utils.RotatedBy(spinningpoint7, radians9, center);
			double radians10 = base.NPC.velocity.ToRotation();
			center = default(Vector2);
			dustPosition2 = spinningpoint8.RotatedBy(radians10, center);
			dust4 = Dust.NewDust(base.NPC.Center - Vector2.One * 5f, 10, 10, 197, 0f, 0f, 100, Color.Transparent);
			Main.dust[dust4].velocity = Vector2.Zero;
			Main.dust[dust4].position = base.NPC.Center + dustPosition2;
			Main.dust[dust4].noGravity = true;
			if (calamityGlobalNPC.newAI[2] >= iceMistDuration)
			{
				base.NPC.localAI[0] = 1f;
				AIState = 2f;
				calamityGlobalNPC.newAI[2] = 120f;
				chargeVelocityScalar = 0f;
				FinalPhaseCheck();
				base.NPC.TargetClosest();
			}
			break;
		}
		case 6:
		{
			destination += spinLocation;
			chargeVelocityScalar += chargeVelocityScalarIncrement;
			if (chargeVelocityScalar > 1f)
			{
				chargeVelocityScalar = 1f;
			}
			baseVelocity *= invisiblePhaseVelocityMult;
			turnSpeed *= invisiblePhaseTurnSpeedMult;
			center = destination - base.NPC.Center;
			if (!(((Vector2)(ref center)).Length() < spinLocationDistance) && !(calamityGlobalNPC.newAI[2] > 0f))
			{
				break;
			}
			calamityGlobalNPC.newAI[2]++;
			float spinVelocityDivisor2 = (targetDownDeep ? 120f : 90f);
			if (rotationDirection == 0)
			{
				if (Main.player[base.NPC.target].velocity.X > 0f)
				{
					rotationDirection = 1;
				}
				else if (Main.player[base.NPC.target].velocity.X < 0f)
				{
					rotationDirection = -1;
				}
				else
				{
					rotationDirection = player.direction;
				}
				base.NPC.velocity.X = (float)Math.PI * spinRadius / spinVelocityDivisor2;
				NPC nPC3 = base.NPC;
				nPC3.velocity *= (float)(-rotationDirection);
				base.NPC.netUpdate = true;
				return;
			}
			NPC nPC4 = base.NPC;
			Vector2 velocity2 = base.NPC.velocity;
			double radians2 = (float)Math.PI / spinVelocityDivisor2 * (float)(-rotationDirection);
			center = default(Vector2);
			nPC4.velocity = velocity2.RotatedBy(radians2, center);
			if (calamityGlobalNPC.newAI[2] >= spinPhaseDuration)
			{
				rotationDirection = 0;
				AIState = (phase4 ? 8f : 0f);
				calamityGlobalNPC.newAI[2] = 0f;
				chargeVelocityScalar = 0f;
				FinalPhaseCheck();
				base.NPC.TargetClosest();
			}
			return;
		}
		case 7:
			destination += ancientDoomLocation;
			if (base.NPC.localAI[1] < maxAncientDoomRings)
			{
				calamityGlobalNPC.newAI[2]++;
				if (calamityGlobalNPC.newAI[2] >= ancientDoomPhaseGateValue)
				{
					float aiGateValue2 = calamityGlobalNPC.newAI[2] - ancientDoomPhaseGateValue;
					if (aiGateValue2 % ancientDoomGateValue == 0f)
					{
						if (Main.netMode != 1)
						{
							int ancientDoomScale3 = (int)(aiGateValue2 / ancientDoomGateValue);
							ancientDoomLimit += ancientDoomScale3;
							ancientDoomDistance += ancientDoomScale3 * 45;
							int ancientDoomDegrees2 = 360 / ancientDoomLimit;
							for (int num3 = 0; num3 < ancientDoomLimit; num3++)
							{
								float ai4 = num3 * ancientDoomDegrees2;
								NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.Center.X + (float)(Math.Sin(num3 * ancientDoomDegrees2) * (double)ancientDoomDistance)), (int)(player.Center.Y + (float)(Math.Cos(num3 * ancientDoomDegrees2) * (double)ancientDoomDistance)), 523, 0, base.NPC.whoAmI, 0f, ai4);
							}
						}
						base.NPC.localAI[1]++;
						base.NPC.TargetClosest();
					}
				}
			}
			if (base.NPC.localAI[1] >= maxAncientDoomRings)
			{
				base.NPC.localAI[1]++;
				if (base.NPC.localAI[1] >= ancientDoomGateValue + maxAncientDoomRings)
				{
					base.NPC.localAI[1] = 0f;
					AIState = (phase4 ? 8f : 4f);
					calamityGlobalNPC.newAI[2] = 0f;
					FinalPhaseCheck();
					base.NPC.TargetClosest();
				}
			}
			break;
		case 8:
			if (calamityGlobalNPC.newAI[2] >= lightningChargePhaseGateValue)
			{
				ChargeDust();
				chargeVelocityScalar += chargeVelocityScalarIncrement;
				if (chargeVelocityScalar > 1f)
				{
					chargeVelocityScalar = 1f;
				}
				baseVelocity *= normalChargeVelocityMult;
				turnSpeed *= normalChargeTurnSpeedMult;
				center = lightningChargeLocation - base.NPC.Center;
				if (((Vector2)(ref center)).Length() < lightningChargeLocationDistance || calamityGlobalNPC.newAI[2] > lightningChargePhaseGateValue)
				{
					if (chargeVelocityScalar < 1f)
					{
						chargeVelocityScalar = 1f;
					}
					if (calamityGlobalNPC.newAI[2] < lightningChargePhaseGateValue + 1f)
					{
						calamityGlobalNPC.newAI[2]++;
					}
					if (!lookingAtTarget && calamityGlobalNPC.newAI[2] < lightningChargePhaseGateValue + 2f)
					{
						baseVelocity /= normalChargeVelocityMult;
						turnSpeed /= normalChargeTurnSpeedMult;
						break;
					}
					if (calamityGlobalNPC.newAI[2] == lightningChargePhaseGateValue + 1f && Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
					{
						SoundEngine.PlaySound(in ChargeSound, Main.LocalPlayer.Center);
					}
					calamityGlobalNPC.newAI[2]++;
					base.NPC.Opacity = 1f;
					if (base.NPC.localAI[3] == 0f)
					{
						if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
						{
							SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, Main.LocalPlayer.Center);
						}
						base.NPC.localAI[3] = 1f;
						if (Main.netMode != 1)
						{
							int type3 = 466;
							for (int num4 = 0; num4 < numLightningBolts; num4++)
							{
								Vector2 projectileDestination2 = player.Center - lightningSpawnLocation;
								Vector2 projectileVelocity3 = Vector2.Normalize(projectileDestination2.RotatedByRandom(0.7853981852531433)) * baseVelocity * 0.5f;
								float ai5 = Main.rand.Next(100);
								int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), lightningSpawnLocation, projectileVelocity3, type3, LightningDamage, 0f, Main.myPlayer, projectileDestination2.ToRotation(), ai5);
								Main.projectile[proj2].tileCollide = false;
								lightningSpawnLocation.Y += distanceBetweenBolts;
								if (num4 == numLightningBolts / 2)
								{
									lightningSpawnLocation.Y += distanceBetweenBolts;
								}
							}
						}
					}
					float totalChargeTime4 = totalChargeDistance / baseVelocity + lightningChargePhaseGateValue + 1f;
					if (calamityGlobalNPC.newAI[2] > totalChargeTime4)
					{
						AIState = ((base.NPC.localAI[2] == 0f) ? 1f : 5f);
						calamityGlobalNPC.newAI[2] = 0f;
						base.NPC.localAI[2]++;
						if (base.NPC.localAI[2] > 1f)
						{
							base.NPC.localAI[2] = 0f;
						}
						base.NPC.localAI[3] = 0f;
						chargeVelocityScalar = 0f;
						FinalPhaseCheck();
						base.NPC.TargetClosest();
					}
				}
				else
				{
					destination += lightningChargeVector;
				}
			}
			else
			{
				calamityGlobalNPC.newAI[2]++;
			}
			break;
		case 9:
			destination += eidolonWyrmPhaseLocation;
			if (calamityGlobalNPC.newAI[2] == 0f && !NPC.AnyNPCs(ModContent.NPCType<Eidolist>()))
			{
				if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
				{
					SoundEngine.PlaySound(in Eidolist.DeathSound, Main.LocalPlayer.Center);
				}
				for (int m = 0; m < maxEidolists; m++)
				{
					Point npcCenter = base.NPC.Center.ToTileCoordinates();
					Point playerCenter = player.Center.ToTileCoordinates();
					Vector2 distance = player.Center - base.NPC.Center;
					int baseSpawnDistance = 60;
					int minDistance = 3;
					int maxDistance = 7;
					int collisionRange = 2;
					int iterations = 0;
					bool tooFar = ((Vector2)(ref distance)).Length() > 2800f;
					while (!tooFar && iterations < 100)
					{
						iterations++;
						int randomX = Main.rand.Next(playerCenter.X - baseSpawnDistance, playerCenter.X + baseSpawnDistance + 1);
						int randomY = Main.rand.Next(playerCenter.Y - baseSpawnDistance, playerCenter.Y + baseSpawnDistance + 1);
						if ((randomY < playerCenter.Y - maxDistance || randomY > playerCenter.Y + maxDistance || randomX < playerCenter.X - maxDistance || randomX > playerCenter.X + maxDistance) && (randomY < npcCenter.Y - minDistance || randomY > npcCenter.Y + minDistance || randomX < npcCenter.X - minDistance || randomX > npcCenter.X + minDistance) && !Main.tile[randomX, randomY].HasUnactuatedTile)
						{
							bool canSpawn = true;
							if (canSpawn && Collision.SolidTiles(randomX - collisionRange, randomX + collisionRange, randomY - collisionRange, randomY + collisionRange))
							{
								canSpawn = false;
							}
							if (canSpawn)
							{
								NPC.NewNPC(base.NPC.GetSource_FromAI(), randomX * 16 + 8, randomY * 16 + 8, ModContent.NPCType<Eidolist>());
								break;
							}
						}
					}
				}
			}
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= eidolonWyrmPhaseDuration)
			{
				AIState = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				FinalPhaseCheck();
				base.NPC.TargetClosest();
			}
			break;
		case 10:
		{
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= ancientDoomPhaseGateValue)
			{
				float aiGateValue = calamityGlobalNPC.newAI[2] - ancientDoomPhaseGateValue;
				if (aiGateValue % ancientDoomGateValue == 0f)
				{
					if (Main.netMode != 1)
					{
						int ancientDoomScale2 = (int)(aiGateValue / ancientDoomGateValue);
						ancientDoomLimit += ancientDoomScale2;
						ancientDoomDistance += ancientDoomScale2 * 45;
						int ancientDoomDegrees = 360 / ancientDoomLimit;
						for (int l = 0; l < ancientDoomLimit; l++)
						{
							float ai2 = l * ancientDoomDegrees;
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(player.Center.X + (float)(Math.Sin(l * ancientDoomDegrees) * (double)ancientDoomDistance)), (int)(player.Center.Y + (float)(Math.Cos(l * ancientDoomDegrees) * (double)ancientDoomDistance)), 523, 0, base.NPC.whoAmI, 0f, ai2);
						}
					}
					if (calamityGlobalNPC.newAI[2] >= 240f)
					{
						calamityGlobalNPC.newAI[2] = -90f;
					}
					base.NPC.TargetClosest();
				}
			}
			destination += spinLocation;
			chargeVelocityScalar += chargeVelocityScalarIncrement;
			if (chargeVelocityScalar > 1f)
			{
				chargeVelocityScalar = 1f;
			}
			baseVelocity *= invisiblePhaseVelocityMult;
			turnSpeed *= invisiblePhaseTurnSpeedMult;
			center = destination - base.NPC.Center;
			if (!(((Vector2)(ref center)).Length() < spinLocationDistance) && !(calamityGlobalNPC.newAI[1] > 0f))
			{
				break;
			}
			calamityGlobalNPC.newAI[1]++;
			float spinVelocityDivisor = (targetDownDeep ? 90f : 60f);
			if (rotationDirection == 0)
			{
				if (Main.player[base.NPC.target].velocity.X > 0f)
				{
					rotationDirection = 1;
				}
				else if (Main.player[base.NPC.target].velocity.X < 0f)
				{
					rotationDirection = -1;
				}
				else
				{
					rotationDirection = player.direction;
				}
				base.NPC.velocity.X = (float)Math.PI * spinRadius / spinVelocityDivisor;
				NPC nPC = base.NPC;
				nPC.velocity *= (float)(-rotationDirection);
				base.NPC.netUpdate = true;
				return;
			}
			NPC nPC2 = base.NPC;
			Vector2 velocity = base.NPC.velocity;
			double radians = (float)Math.PI / spinVelocityDivisor * (float)(-rotationDirection);
			center = default(Vector2);
			nPC2.velocity = velocity.RotatedBy(radians, center);
			if (calamityGlobalNPC.newAI[1] >= spinPhaseDuration)
			{
				calamityGlobalNPC.newAI[1] = 0f;
				chargeVelocityScalar = 0f;
				rotationDirection = 0;
				base.NPC.TargetClosest();
			}
			return;
		}
		}
		if (targetDead)
		{
			return;
		}
		base.NPC.velocity = base.NPC.velocity.ClampMagnitude(baseVelocity * 0.2f, baseVelocity * 1.3f);
		Vector2 idealVelocity = base.NPC.SafeDirectionTo(destination) * baseVelocity;
		if ((base.NPC.velocity.X > 0f && idealVelocity.X > 0f) || (base.NPC.velocity.X < 0f && idealVelocity.X < 0f) || (base.NPC.velocity.Y > 0f && idealVelocity.Y > 0f) || (base.NPC.velocity.Y < 0f && idealVelocity.Y < 0f))
		{
			base.NPC.velocity.X += (float)(base.NPC.velocity.X < idealVelocity.X).ToDirectionInt() * turnSpeed;
			base.NPC.velocity.Y += (float)(base.NPC.velocity.Y < idealVelocity.Y).ToDirectionInt() * turnSpeed;
			if ((double)Math.Abs(idealVelocity.Y) < (double)baseVelocity * 0.2 && ((base.NPC.velocity.X > 0f && idealVelocity.X < 0f) || (base.NPC.velocity.X < 0f && idealVelocity.X > 0f)))
			{
				base.NPC.velocity.Y += (float)base.NPC.velocity.Y.DirectionalSign() * turnSpeed * 2f;
			}
			if ((double)Math.Abs(idealVelocity.X) < (double)baseVelocity * 0.2 && ((base.NPC.velocity.Y > 0f && idealVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && idealVelocity.Y > 0f)))
			{
				base.NPC.velocity.X += (float)base.NPC.velocity.X.DirectionalSign() * turnSpeed * 2f;
			}
		}
		else if (MathHelper.Distance(destination.X, base.NPC.Center.X) > MathHelper.Distance(destination.Y, base.NPC.Center.Y))
		{
			base.NPC.velocity.X += (float)(base.NPC.velocity.X < idealVelocity.X).ToDirectionInt() * turnSpeed * 1.1f;
			if ((double)base.NPC.velocity.ManhattanDistance(Vector2.Zero) < (double)baseVelocity * 0.5)
			{
				base.NPC.velocity.Y += (float)base.NPC.velocity.Y.DirectionalSign() * turnSpeed;
			}
		}
		else
		{
			base.NPC.velocity.Y += (float)(base.NPC.velocity.Y < idealVelocity.Y).ToDirectionInt() * turnSpeed * 1.1f;
			if ((double)base.NPC.velocity.ManhattanDistance(Vector2.Zero) < (double)baseVelocity * 0.5)
			{
				base.NPC.velocity.X += (float)base.NPC.velocity.X.DirectionalSign() * turnSpeed;
			}
		}
		void FinalPhaseCheck()
		{
			if (phase6)
			{
				base.NPC.localAI[1] = 0f;
				AIState = 10f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				rotationDirection = 0;
			}
		}
	}

	private void ChargeDust()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		for (int dustCount = 0; dustCount < 5; dustCount++)
		{
			((Vector2)(ref Dust.NewDustPerfect(Utils.RotatedBy(new Vector2(base.NPC.Center.X - 184f, base.NPC.Center.Y + 77f), base.NPC.rotation, base.NPC.Center), 113, -base.NPC.velocity, 1, Color.SkyBlue).velocity)).Normalize();
			((Vector2)(ref Dust.NewDustPerfect(Utils.RotatedBy(new Vector2(base.NPC.Center.X + 184f, base.NPC.Center.Y + 77f), base.NPC.rotation, base.NPC.Center), 113, -base.NPC.velocity, 1, Color.SkyBlue).velocity)).Normalize();
			((Vector2)(ref Dust.NewDustPerfect(Utils.RotatedBy(new Vector2(base.NPC.Center.X - 119f, base.NPC.Center.Y + 47f), base.NPC.rotation, base.NPC.Center), 113, -base.NPC.velocity, 1, Color.SkyBlue, 0.7f).velocity)).Normalize();
			((Vector2)(ref Dust.NewDustPerfect(Utils.RotatedBy(new Vector2(base.NPC.Center.X + 119f, base.NPC.Center.Y + 47f), base.NPC.rotation, base.NPC.Center), 113, -base.NPC.velocity, 1, Color.SkyBlue, 0.7f).velocity)).Normalize();
		}
	}

	public static bool CanMinionsDropThings()
	{
		bool adultWyrmAlive = false;
		if (CalamityGlobalNPC.adultEidolonWyrmHead != -1 && Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active)
		{
			adultWyrmAlive = true;
		}
		return !adultWyrmAlive;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		cooldownSlot = 1;
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
		if (minDist <= 70f)
		{
			return base.NPC.Opacity == 1f;
		}
		return false;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 1.5f;
		return null;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<PrimordialWyrmBody>()].Value, TextureAssets.Npc[ModContent.NPCType<PrimordialWyrmBodyAlt>()].Value, 3, 36, 0.2f, new Vector2(130f, 60f), 3, 10f);
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 center = base.NPC.Center - screenPos;
		center -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		center += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		float brightness = 1f;
		brightness = MathF.Sin((float)Main.GameUpdateCount * 0.01f * (6f + (PWHeadVelocity = MathHelper.Clamp((float)(int)((Vector2)(ref base.NPC.velocity)).Length(), 6f, 8f))) - (float)base.NPC.whoAmI);
		brightness = MathHelper.Clamp(brightness, 0.25f, 1f);
		texture = GlowTexture.Value;
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, Color.White * (base.NPC.Opacity * brightness), base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override void OnKill()
	{
		DownedBossSystem.downedPrimordialWyrm = true;
		CalamityNetcode.SyncWorld();
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<EidolicWail>());
		npcLoot.Add(ModContent.ItemType<VoidEdge>());
		npcLoot.Add(ModContent.ItemType<HalibutCannon>());
		npcLoot.Add(ModContent.ItemType<AbyssShellFossil>());
		npcLoot.Add(ModContent.ItemType<Voidstone>(), 1, 80, 100);
		npcLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<Lumenyl>(), 1, 50, 108, 65, 135));
		npcLoot.Add(1508, 1, 21, 32);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int k = 0; k < 15; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
			}
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrimordialWyrm").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrimordialWyrm2").Type);
		}
	}

	public override bool CheckActive()
	{
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
			typeName = CalamityUtils.GetTextValue("NPCs.Jared");
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (base.NPC.Opacity == 1f && hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 1200);
		}
	}
}
