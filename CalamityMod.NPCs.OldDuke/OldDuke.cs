using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Systems;
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

namespace CalamityMod.NPCs.OldDuke;

[AutoloadBossHead]
public class OldDuke : ModNPC
{
	public static Color GlowColor;

	public const float LifePercentagePhase2_Normal = 0.5f;

	public const float LifePercentagePhase2_Revenge = 0.7f;

	public const float LifePercentagePhase2_Death = 0.8f;

	public const float LifePercentagePhase3_Expert = 0.2f;

	public const float LifePercentagePhase3_Revenge = 0.35f;

	public const float LifePercentagePhase3_Death = 0.5f;

	public int Phase;

	public float NuclearOverlayVisual;

	public static readonly SoundStyle HuffSound;

	public static readonly SoundStyle RoarSound;

	public static readonly SoundStyle VomitSound;

	public static readonly SoundStyle VortexSpawnSound;

	public static readonly SoundStyle DashSound;

	public static readonly SoundStyle DashSoundP3;

	private static Color FireGreen;

	private SlotId RoarSoundSlot;

	public static Asset<Texture2D> GlowTexture;

	public float shake;

	public static float Phase2ContactDamageMult;

	public static float Phase3ContactDamageMult;

	public static int GoreDamage;

	public static int VortexDamage;

	public static int FartDamage;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		nPCBestiaryDrawModifiers.Scale = 0.45f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 14f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 140;
		base.NPC.alpha = 255;
		base.NPC.width = 150;
		base.NPC.height = 100;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.defense = 90;
		base.NPC.DR_NERD(0.5f);
		base.NPC.LifeMaxNERB(400000, 600000, 400000);
		base.NPC.knockBackResist = 0f;
		base.NPC.noTileCollide = true;
		base.NPC.noGravity = true;
		base.NPC.npcSlots = 15f;
		base.NPC.HitSound = SoundID.NPCHit14;
		base.NPC.DeathSound = SoundID.NPCDeath20;
		base.NPC.value = Item.buyPrice(1, 50);
		base.NPC.boss = true;
		base.NPC.netAlways = true;
		base.NPC.timeLeft = NPC.activeTime * 30;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SulphurousSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.OldDuke")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.rotation);
		writer.Write(base.NPC.spriteDirection);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.rotation = reader.ReadSingle();
		base.NPC.spriteDirection = reader.ReadInt32();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	private static void DoChargeBurst(NPC npc, int phase)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound((phase > 1) ? DashSoundP3 : DashSound, npc.Center);
		if (phase > 1)
		{
			for (int i = 0; i < 30; i++)
			{
				float fl = Main.rand.NextFloat(-60f, 60f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(npc.Center + Vector2.Zero.DirectionTo(npc.velocity) * 20f + Utils.RotatedBy(new Vector2(0f, fl), (double)Vector2.Zero.AngleTo(npc.velocity), default(Vector2)), (-npc.velocity * Main.rand.NextFloat(2f)).RotatedBy(MathHelper.ToRadians(fl)), affectedByGravity: false, 25, Main.rand.NextFloat(0.3f, 1.2f), FireGreen));
			}
			CalamityUtils.AddScreenshakeAt(npc.Center, 10f, 3000f);
			SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, npc.Center);
			SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, npc.Center);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(npc.Center, -npc.velocity / 3f, FireGreen, "CalamityMod/Particles/DustyCircleHardEdge", new Vector2(0.4f, 1.2f), Vector2.Zero.AngleTo(npc.velocity) + (Main.rand.NextBool() ? 0f : (-(float)Math.PI)), 0.05f, 0.2f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(npc.Center, -npc.velocity / 2f, FireGreen, "CalamityMod/Particles/FlameExplosion", new Vector2(0.4f, 1.2f), Vector2.Zero.AngleTo(npc.velocity) + (Main.rand.NextBool() ? 0f : (-(float)Math.PI)), 0.1f, 0.4f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}

	private static void DoChargeVisual(NPC npc, int phase)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		float progress = npc.ai[2];
		int chargeTime = 36;
		Color col = Color.Lerp((Color)((phase > 0) ? FireGreen : new Color(100, 100, 100, 55)), new Color(55, 55, 55, 55), progress / (float)chargeTime);
		if (phase > 1)
		{
			if (Math.Floor(VisualTimerSystem.GlobalVisualTimer % 6f) < 1.0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(npc.Center, npc.velocity / 3f, col, "CalamityMod/Particles/DustyCircleHardEdge", new Vector2(0.4f, 1f), Vector2.Zero.AngleTo(npc.velocity) + (Main.rand.NextBool() ? 0f : (-(float)Math.PI)), 0.04f, 0.1f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			(npc.ModNPC as OldDuke).NuclearOverlayVisual = 1f;
		}
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(npc.Center - npc.velocity * 2f, npc.velocity / 2f, "CalamityMod/Particles/ForwardSmear", affectedByGravity: false, 10, Main.rand.NextFloat(0.3f, 0.5f), col, new Vector2(1f, Main.rand.NextFloat(1.45f, 1.6f)), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(180f), fadeIn: true));
		}
	}

	public override void AI()
	{
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_094b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_110d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_103c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1046: Unknown result type (might be due to invalid IL or missing references)
		//IL_104b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1056: Unknown result type (might be due to invalid IL or missing references)
		//IL_105b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1060: Unknown result type (might be due to invalid IL or missing references)
		//IL_1067: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1103: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_149d: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_18db: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1902: Unknown result type (might be due to invalid IL or missing references)
		//IL_1907: Unknown result type (might be due to invalid IL or missing references)
		//IL_1912: Unknown result type (might be due to invalid IL or missing references)
		//IL_1917: Unknown result type (might be due to invalid IL or missing references)
		//IL_191c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1923: Unknown result type (might be due to invalid IL or missing references)
		//IL_1928: Unknown result type (might be due to invalid IL or missing references)
		//IL_1930: Unknown result type (might be due to invalid IL or missing references)
		//IL_193b: Unknown result type (might be due to invalid IL or missing references)
		//IL_194b: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_151b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1525: Unknown result type (might be due to invalid IL or missing references)
		//IL_152a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1548: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20db: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2084: Unknown result type (might be due to invalid IL or missing references)
		//IL_208b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2090: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e84: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f53: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1629: Unknown result type (might be due to invalid IL or missing references)
		//IL_1634: Unknown result type (might be due to invalid IL or missing references)
		//IL_1645: Unknown result type (might be due to invalid IL or missing references)
		//IL_1650: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_251a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2525: Unknown result type (might be due to invalid IL or missing references)
		//IL_2122: Unknown result type (might be due to invalid IL or missing references)
		//IL_212d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2132: Unknown result type (might be due to invalid IL or missing references)
		//IL_5224: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d96: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1687: Unknown result type (might be due to invalid IL or missing references)
		//IL_1697: Unknown result type (might be due to invalid IL or missing references)
		//IL_2851: Unknown result type (might be due to invalid IL or missing references)
		//IL_285c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2861: Unknown result type (might be due to invalid IL or missing references)
		//IL_238b: Unknown result type (might be due to invalid IL or missing references)
		//IL_239b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2184: Unknown result type (might be due to invalid IL or missing references)
		//IL_218f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2194: Unknown result type (might be due to invalid IL or missing references)
		//IL_2199: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_21df: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2200: Unknown result type (might be due to invalid IL or missing references)
		//IL_2204: Unknown result type (might be due to invalid IL or missing references)
		//IL_220b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2171: Unknown result type (might be due to invalid IL or missing references)
		//IL_217c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5243: Unknown result type (might be due to invalid IL or missing references)
		//IL_31c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_31cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_31d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2592: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_25fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2612: Unknown result type (might be due to invalid IL or missing references)
		//IL_2231: Unknown result type (might be due to invalid IL or missing references)
		//IL_2239: Unknown result type (might be due to invalid IL or missing references)
		//IL_2247: Unknown result type (might be due to invalid IL or missing references)
		//IL_224e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2273: Unknown result type (might be due to invalid IL or missing references)
		//IL_2275: Unknown result type (might be due to invalid IL or missing references)
		//IL_228a: Unknown result type (might be due to invalid IL or missing references)
		//IL_228f: Unknown result type (might be due to invalid IL or missing references)
		//IL_371f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3729: Unknown result type (might be due to invalid IL or missing references)
		//IL_372e: Unknown result type (might be due to invalid IL or missing references)
		//IL_32cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_32da: Unknown result type (might be due to invalid IL or missing references)
		//IL_32df: Unknown result type (might be due to invalid IL or missing references)
		//IL_32ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_32fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_330d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3318: Unknown result type (might be due to invalid IL or missing references)
		//IL_3333: Unknown result type (might be due to invalid IL or missing references)
		//IL_3339: Unknown result type (might be due to invalid IL or missing references)
		//IL_333b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3342: Unknown result type (might be due to invalid IL or missing references)
		//IL_334c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3351: Unknown result type (might be due to invalid IL or missing references)
		//IL_3356: Unknown result type (might be due to invalid IL or missing references)
		//IL_303a: Unknown result type (might be due to invalid IL or missing references)
		//IL_304a: Unknown result type (might be due to invalid IL or missing references)
		//IL_28cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_2933: Unknown result type (might be due to invalid IL or missing references)
		//IL_2953: Unknown result type (might be due to invalid IL or missing references)
		//IL_266e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2685: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3785: Unknown result type (might be due to invalid IL or missing references)
		//IL_3790: Unknown result type (might be due to invalid IL or missing references)
		//IL_3621: Unknown result type (might be due to invalid IL or missing references)
		//IL_363a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3640: Unknown result type (might be due to invalid IL or missing references)
		//IL_3642: Unknown result type (might be due to invalid IL or missing references)
		//IL_3647: Unknown result type (might be due to invalid IL or missing references)
		//IL_336c: Unknown result type (might be due to invalid IL or missing references)
		//IL_336e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3384: Unknown result type (might be due to invalid IL or missing references)
		//IL_338b: Unknown result type (might be due to invalid IL or missing references)
		//IL_30f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_30fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3104: Unknown result type (might be due to invalid IL or missing references)
		//IL_3109: Unknown result type (might be due to invalid IL or missing references)
		//IL_3114: Unknown result type (might be due to invalid IL or missing references)
		//IL_3119: Unknown result type (might be due to invalid IL or missing references)
		//IL_311e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3126: Unknown result type (might be due to invalid IL or missing references)
		//IL_3128: Unknown result type (might be due to invalid IL or missing references)
		//IL_312f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3134: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_29c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b39: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1730: Unknown result type (might be due to invalid IL or missing references)
		//IL_1743: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_174b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1750: Unknown result type (might be due to invalid IL or missing references)
		//IL_1783: Unknown result type (might be due to invalid IL or missing references)
		//IL_178e: Unknown result type (might be due to invalid IL or missing references)
		//IL_179b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b49: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_37ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_37b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_37c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_37c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_37db: Unknown result type (might be due to invalid IL or missing references)
		//IL_37e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_37f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_37f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_37fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_33ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_33f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_3407: Unknown result type (might be due to invalid IL or missing references)
		//IL_3411: Unknown result type (might be due to invalid IL or missing references)
		//IL_341c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3421: Unknown result type (might be due to invalid IL or missing references)
		//IL_3426: Unknown result type (might be due to invalid IL or missing references)
		//IL_342e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3433: Unknown result type (might be due to invalid IL or missing references)
		//IL_344d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3453: Unknown result type (might be due to invalid IL or missing references)
		//IL_3455: Unknown result type (might be due to invalid IL or missing references)
		//IL_345c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3461: Unknown result type (might be due to invalid IL or missing references)
		//IL_3465: Unknown result type (might be due to invalid IL or missing references)
		//IL_346c: Unknown result type (might be due to invalid IL or missing references)
		//IL_33d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_33e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3495: Unknown result type (might be due to invalid IL or missing references)
		//IL_349d: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_34b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_34f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_34f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3502: Unknown result type (might be due to invalid IL or missing references)
		//IL_3507: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2301: Unknown result type (might be due to invalid IL or missing references)
		//IL_2303: Unknown result type (might be due to invalid IL or missing references)
		//IL_230b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2311: Unknown result type (might be due to invalid IL or missing references)
		//IL_2313: Unknown result type (might be due to invalid IL or missing references)
		//IL_2318: Unknown result type (might be due to invalid IL or missing references)
		//IL_231a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2325: Unknown result type (might be due to invalid IL or missing references)
		//IL_232a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2342: Unknown result type (might be due to invalid IL or missing references)
		//IL_234a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2351: Unknown result type (might be due to invalid IL or missing references)
		//IL_235b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2361: Unknown result type (might be due to invalid IL or missing references)
		//IL_450d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4517: Unknown result type (might be due to invalid IL or missing references)
		//IL_451c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3961: Unknown result type (might be due to invalid IL or missing references)
		//IL_3975: Unknown result type (might be due to invalid IL or missing references)
		//IL_39c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_39db: Unknown result type (might be due to invalid IL or missing references)
		//IL_4941: Unknown result type (might be due to invalid IL or missing references)
		//IL_494b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4950: Unknown result type (might be due to invalid IL or missing references)
		//IL_466a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4674: Unknown result type (might be due to invalid IL or missing references)
		//IL_4679: Unknown result type (might be due to invalid IL or missing references)
		//IL_4385: Unknown result type (might be due to invalid IL or missing references)
		//IL_4395: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ed8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ee3: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ef4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ef9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f01: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e92: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bba: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_49a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_49b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_46cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_46d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_46dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_443e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4445: Unknown result type (might be due to invalid IL or missing references)
		//IL_444f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4454: Unknown result type (might be due to invalid IL or missing references)
		//IL_445f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4464: Unknown result type (might be due to invalid IL or missing references)
		//IL_4469: Unknown result type (might be due to invalid IL or missing references)
		//IL_4471: Unknown result type (might be due to invalid IL or missing references)
		//IL_4473: Unknown result type (might be due to invalid IL or missing references)
		//IL_447a: Unknown result type (might be due to invalid IL or missing references)
		//IL_447f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d20: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_2db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_4dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e38: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e43: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e64: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e77: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e81: Unknown result type (might be due to invalid IL or missing references)
		//IL_49ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_49d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_49e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_49e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_49fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_38a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_38a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3571: Unknown result type (might be due to invalid IL or missing references)
		//IL_3576: Unknown result type (might be due to invalid IL or missing references)
		//IL_3580: Unknown result type (might be due to invalid IL or missing references)
		//IL_3586: Unknown result type (might be due to invalid IL or missing references)
		//IL_3588: Unknown result type (might be due to invalid IL or missing references)
		//IL_358d: Unknown result type (might be due to invalid IL or missing references)
		//IL_358f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3597: Unknown result type (might be due to invalid IL or missing references)
		//IL_359d: Unknown result type (might be due to invalid IL or missing references)
		//IL_359f: Unknown result type (might be due to invalid IL or missing references)
		//IL_35a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_35a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_35b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_35b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_35ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_35dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_35e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_35ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_514c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5164: Unknown result type (might be due to invalid IL or missing references)
		//IL_516a: Unknown result type (might be due to invalid IL or missing references)
		//IL_516c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5171: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_4eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_4eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f51: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f80: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f90: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_4be9: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4753: Unknown result type (might be due to invalid IL or missing references)
		//IL_476a: Unknown result type (might be due to invalid IL or missing references)
		//IL_476f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4774: Unknown result type (might be due to invalid IL or missing references)
		//IL_477c: Unknown result type (might be due to invalid IL or missing references)
		//IL_477e: Unknown result type (might be due to invalid IL or missing references)
		//IL_477f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4786: Unknown result type (might be due to invalid IL or missing references)
		//IL_4788: Unknown result type (might be due to invalid IL or missing references)
		//IL_478c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4796: Unknown result type (might be due to invalid IL or missing references)
		//IL_4733: Unknown result type (might be due to invalid IL or missing references)
		//IL_473a: Unknown result type (might be due to invalid IL or missing references)
		//IL_473f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_501a: Unknown result type (might be due to invalid IL or missing references)
		//IL_501c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5023: Unknown result type (might be due to invalid IL or missing references)
		//IL_502d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5032: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f81: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_40f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_40f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4102: Unknown result type (might be due to invalid IL or missing references)
		//IL_4107: Unknown result type (might be due to invalid IL or missing references)
		//IL_4112: Unknown result type (might be due to invalid IL or missing references)
		//IL_4117: Unknown result type (might be due to invalid IL or missing references)
		//IL_411c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4124: Unknown result type (might be due to invalid IL or missing references)
		//IL_4126: Unknown result type (might be due to invalid IL or missing references)
		//IL_412d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4132: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ac4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_509c: Unknown result type (might be due to invalid IL or missing references)
		//IL_50a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_50ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_50b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_50b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_50b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_50ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_50c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_50c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_50ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_50cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_50d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_50dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_50e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_50f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_5101: Unknown result type (might be due to invalid IL or missing references)
		//IL_5108: Unknown result type (might be due to invalid IL or missing references)
		//IL_5112: Unknown result type (might be due to invalid IL or missing references)
		//IL_5118: Unknown result type (might be due to invalid IL or missing references)
		//IL_47f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_47fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4258: Unknown result type (might be due to invalid IL or missing references)
		//IL_4263: Unknown result type (might be due to invalid IL or missing references)
		//IL_4268: Unknown result type (might be due to invalid IL or missing references)
		//IL_426d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4274: Unknown result type (might be due to invalid IL or missing references)
		//IL_4279: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		shake = MathHelper.Lerp(shake, 0f, 0.1f);
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float exhaustionGateValue = 360f;
		if (Main.getGoodWorld)
		{
			exhaustionGateValue *= 0.5f;
		}
		float numberOfAttacksBeforeExhaustion = 12f;
		float exhaustionIncreasePerAttack = exhaustionGateValue * (1f / numberOfAttacksBeforeExhaustion);
		bool exhausted = calamityGlobalNPC.newAI[1] == 1f;
		bool phase2 = num <= (death ? 0.8f : (revenge ? 0.7f : 0.5f));
		bool phase3 = (num <= (death ? 0.5f : (revenge ? 0.35f : 0.2f))) & expertMode;
		bool phase2AI = base.NPC.ai[0] > 4f;
		bool phase3AI = base.NPC.ai[0] > 9f;
		Phase = (phase3 ? 2 : (phase2 ? 1 : 0));
		NuclearOverlayVisual = ((Phase == 2 && base.NPC.Calamity().newAI[1] != 1f) ? MathHelper.Lerp(NuclearOverlayVisual, 0.2f, 0.2f) : MathHelper.Lerp(NuclearOverlayVisual, 0f, 0.1f));
		bool charging = base.NPC.ai[3] < 10f;
		if (calamityGlobalNPC.newAI[0] >= exhaustionGateValue)
		{
			calamityGlobalNPC.newAI[1] = 1f;
		}
		float alphaScale = (float)(255 - base.NPC.alpha) / 255f;
		float redLight = (phase3AI ? 0.4f : (phase2AI ? 0.64f : 0.88f)) * alphaScale;
		float greenLight = (phase3AI ? 1.2f : (phase2AI ? 0.8f : 0.4f)) * alphaScale;
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), redLight, greenLight, 0f);
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		else if (!Main.raining && !BossRushEvent.BossRushActive)
		{
			CalamityWorld.StartRain();
		}
		base.NPC.damage = base.NPC.defDamage;
		calamityGlobalNPC.DR = (exhausted ? 0f : 0.5f);
		base.NPC.defense = ((!exhausted) ? base.NPC.defDefense : 0);
		if (phase3AI)
		{
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * Phase3ContactDamageMult);
			base.NPC.defense = ((!exhausted) ? (base.NPC.defDefense - 40) : 0);
		}
		else if (phase2AI)
		{
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * Phase2ContactDamageMult);
			base.NPC.defense = ((!exhausted) ? (base.NPC.defDefense - 20) : 0);
		}
		int idlePhaseTimer = (expertMode ? 55 : 60);
		float idlePhaseAcceleration = (expertMode ? 0.75f : 0.7f);
		float idlePhaseVelocity = (expertMode ? 14f : 13f);
		if (phase3AI)
		{
			idlePhaseAcceleration = (expertMode ? 0.6f : 0.55f);
			idlePhaseVelocity = (expertMode ? 12f : 11f);
		}
		else if (phase2AI & charging)
		{
			idlePhaseAcceleration = (expertMode ? 0.8f : 0.75f);
			idlePhaseVelocity = (expertMode ? 15f : 14f);
		}
		int chargeTime = (expertMode ? 34 : 36);
		float chargeVelocity = (expertMode ? 20f : 19f);
		if (phase3AI)
		{
			chargeTime = (expertMode ? 28 : 30);
			chargeVelocity = (expertMode ? 26f : 25f);
		}
		else if (charging & phase2AI)
		{
			chargeTime = (expertMode ? 31 : 33);
			chargeVelocity = (expertMode ? 24f : 23f);
		}
		if (death)
		{
			idlePhaseTimer = 51;
			idlePhaseAcceleration *= 1.05f;
			idlePhaseVelocity *= 1.05f;
			chargeTime -= 2;
			chargeVelocity *= 1.1f;
		}
		else if (revenge)
		{
			idlePhaseTimer = 53;
			idlePhaseAcceleration *= 1.025f;
			idlePhaseVelocity *= 1.025f;
			chargeTime--;
			chargeVelocity *= 1.05f;
		}
		if (Main.zenithWorld)
		{
			chargeVelocity *= 1.25f;
		}
		if (exhausted)
		{
			idlePhaseVelocity *= 0.25f;
		}
		int maxToothBallBelches = (death ? 4 : 3);
		int toothBallBelchPhaseDivisor = (death ? 30 : 40);
		int toothBallBelchPhaseTimer = toothBallBelchPhaseDivisor * maxToothBallBelches;
		float toothBallBelchPhaseAcceleration = (death ? 0.6f : 0.55f);
		float toothBallBelchPhaseVelocity = (death ? 10f : 9f);
		float toothBallFinalVelocity = (death ? 14f : (revenge ? 13f : 12f));
		float goreVelocityX = (death ? 8f : (revenge ? 7.5f : (expertMode ? 7f : 6f)));
		float goreVelocityY = (death ? 10.5f : (revenge ? 10f : (expertMode ? 9.5f : 8f)));
		float sharkronVelocity = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f)));
		int attackTimer = 120;
		int phaseTransitionTimer = 180;
		int teleportPauseTimer = 30;
		int toothBallSpinPhaseDivisor = (death ? 32 : 45);
		int toothBallSpinTimer = maxToothBallBelches * toothBallSpinPhaseDivisor;
		float spinTime = (float)toothBallSpinTimer / 2f;
		float toothBallSpinToothBallVelocity = (death ? 9.5f : 9f);
		float spinAttackSpeed = (Main.zenithWorld ? 44f : 22f);
		float spinSpeed = (float)Math.PI * 2f / spinTime;
		Player player = Main.player[base.NPC.target];
		if (base.NPC.target < 0 || base.NPC.target == 255 || player.dead || !player.active)
		{
			base.NPC.TargetClosest();
			player = Main.player[base.NPC.target];
			base.NPC.netUpdate = true;
		}
		if (Vector2.Distance(player.Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		if (player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 8800f)
		{
			base.NPC.TargetClosest();
			base.NPC.velocity.Y -= 0.4f;
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
			if (base.NPC.timeLeft == 1)
			{
				AcidRainEvent.AccumulatedKillPoints = 0;
				AcidRainEvent.HasTriedToSummonOldDuke = false;
				AcidRainEvent.UpdateInvasion(win: false);
				base.NPC.timeLeft = 0;
			}
			if (base.NPC.ai[0] > 4f)
			{
				base.NPC.ai[0] = 5f;
			}
			else
			{
				base.NPC.ai[0] = 0f;
			}
			base.NPC.ai[2] = 0f;
		}
		if (exhausted)
		{
			base.NPC.damage = 0;
			if (calamityGlobalNPC.newAI[0] % 60f == 0f && Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
			{
				SoundStyle style = HuffSound with
				{
					Volume = HuffSound.Volume * 1.25f
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				for (int i = 0; i < 40; i++)
				{
					float scale = Main.rand.NextFloat(1f, 3f);
					Vector2 position = base.NPC.Center + base.NPC.rotation.ToRotationVector2() * (Main.rand.NextBool() ? 100f : 80f) * (float)base.NPC.direction;
					Vector2 fumeVel = Vector2.UnitX * base.NPC.velocity.X + Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(12f)) * scale * -8f * Main.rand.NextFloat(1f, 1.25f);
					GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position, fumeVel, GlowColor, Color.DarkSlateGray, scale, 150f));
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center + base.NPC.rotation.ToRotationVector2() * 88f * (float)base.NPC.direction, Vector2.Zero, Color.White * 0.2f, "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.125f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			if (Main.zenithWorld)
			{
				float screenShakePower = 10f * Utils.GetLerpValue(800f, 0f, base.NPC.Distance(Main.LocalPlayer.Center), clamped: true);
				Main.LocalPlayer.SetScreenshake(screenShakePower);
				if (calamityGlobalNPC.newAI[0] == exhaustionGateValue)
				{
					SoundStyle style = SoundID.NPCDeath64 with
					{
						Pitch = SoundID.NPCDeath64.Pitch - 0.9f,
						Volume = SoundID.NPCDeath64.Volume + 0.4f
					};
					SoundEngine.PlaySound(in style, player.Center);
				}
				if (Main.netMode != 1 && calamityGlobalNPC.newAI[0] % 5f == 0f)
				{
					Vector2 dist = player.Center - base.NPC.Center;
					((Vector2)(ref dist)).Normalize();
					dist *= 3f;
					dist.X += Main.rand.NextFloat(-0.5f, 0.5f);
					dist.Y += Main.rand.NextFloat(-0.5f, 0.5f);
					int type = ModContent.ProjectileType<SandPoisonCloudOldDuke>();
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, -dist, type, FartDamage, 0f, Main.myPlayer);
				}
			}
			calamityGlobalNPC.newAI[0]--;
			if (calamityGlobalNPC.newAI[0] <= 0f)
			{
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
			}
		}
		bool enrage = !BossRushEvent.BossRushActive && (player.position.Y < 300f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 8000f && player.position.X < (float)(Main.maxTilesX * 16 - 8000)));
		if (Main.remixWorld)
		{
			enrage = !BossRushEvent.BossRushActive && (player.position.Y < (float)(Main.UnderworldLayer * 16) * 0.8f || player.position.Y > (float)(Main.UnderworldLayer * 16) || (player.position.X > 8000f && player.position.X < (float)(Main.maxTilesX * 16 - 8000)));
		}
		if (enrage)
		{
			if (base.NPC.localAI[1] > 0f)
			{
				base.NPC.localAI[1]--;
			}
		}
		else
		{
			base.NPC.localAI[1] = 300f;
		}
		bool biomeEnraged = base.NPC.localAI[1] <= 0f;
		base.NPC.Calamity().CurrentlyEnraged = biomeEnraged;
		if (!exhausted)
		{
			calamityGlobalNPC.DR = ((base.NPC.ai[0] == -1f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f) ? 0.75f : 0.5f);
		}
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = base.NPC.ai[0] == -1f || base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f;
		if (biomeEnraged)
		{
			toothBallBelchPhaseTimer = 60;
			toothBallBelchPhaseDivisor = 20;
			toothBallBelchPhaseAcceleration = 1f;
			toothBallBelchPhaseVelocity = 15f;
			goreVelocityX = 12f;
			goreVelocityY = 16f;
			sharkronVelocity = 20f;
			idlePhaseTimer = 20;
			idlePhaseAcceleration = 1.2f;
			idlePhaseVelocity = 20f;
			chargeTime = 25;
			chargeVelocity += 8f;
			toothBallSpinPhaseDivisor = 24;
			toothBallSpinToothBallVelocity = 15f;
			base.NPC.damage *= 2;
			base.NPC.defense = base.NPC.defDefense * 3;
		}
		if (Main.zenithWorld)
		{
			chargeTime *= 2;
		}
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.alpha = 255;
			base.NPC.velocity.Y = -12f;
			base.NPC.velocity.X = Main.rand.NextFloat(-2f, 2f);
			base.NPC.localAI[0] = 1f;
			base.NPC.rotation = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.netUpdate = true;
			}
		}
		float rateOfRotation = 0.04f;
		if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 7f || base.NPC.ai[0] == 14f)
		{
			rateOfRotation = 0f;
		}
		if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f)
		{
			rateOfRotation = 0.01f;
		}
		if (base.NPC.ai[0] == 8f || base.NPC.ai[0] == 13f)
		{
			rateOfRotation = 0.05f;
		}
		Vector2 rotationVector = player.Center - base.NPC.Center;
		if (calamityGlobalNPC.newAI[1] != 1f && !player.dead)
		{
			if (base.NPC.ai[0] == 0f && !phase2)
			{
				if (base.NPC.ai[3] < 6f)
				{
					rateOfRotation = 0.1f;
					rotationVector = Vector2.Normalize(player.Center + player.velocity * 20f - base.NPC.Center) * chargeVelocity;
				}
			}
			else if (base.NPC.ai[0] == 5f && !phase3)
			{
				if (base.NPC.ai[3] < 4f)
				{
					rateOfRotation = 0.1f;
					rotationVector = Vector2.Normalize(player.Center + player.velocity * 20f - base.NPC.Center) * chargeVelocity;
				}
			}
			else if (base.NPC.ai[0] == 10f && base.NPC.ai[3] < 8f && base.NPC.ai[3] != 1f && base.NPC.ai[3] != 4f)
			{
				rateOfRotation = 0.1f;
				rotationVector = Vector2.Normalize(player.Center + player.velocity * 20f - base.NPC.Center) * chargeVelocity;
			}
		}
		float dukeRotationSpeed = (float)Math.Atan2(rotationVector.Y, rotationVector.X);
		if (base.NPC.spriteDirection == 1)
		{
			dukeRotationSpeed += (float)Math.PI;
		}
		if (dukeRotationSpeed < 0f)
		{
			dukeRotationSpeed += (float)Math.PI * 2f;
		}
		if (dukeRotationSpeed > (float)Math.PI * 2f)
		{
			dukeRotationSpeed -= (float)Math.PI * 2f;
		}
		if (base.NPC.ai[0] == -1f || base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f)
		{
			dukeRotationSpeed = 0f;
		}
		if (base.NPC.ai[0] == 8f || base.NPC.ai[0] == 13f)
		{
			dukeRotationSpeed = (float)Math.PI / 6f * (float)base.NPC.spriteDirection;
		}
		if (base.NPC.rotation < dukeRotationSpeed)
		{
			if (dukeRotationSpeed - base.NPC.rotation > (float)Math.PI)
			{
				base.NPC.rotation -= rateOfRotation;
			}
			else
			{
				base.NPC.rotation += rateOfRotation;
			}
		}
		if (base.NPC.rotation > dukeRotationSpeed)
		{
			if (base.NPC.rotation - dukeRotationSpeed > (float)Math.PI)
			{
				base.NPC.rotation += rateOfRotation;
			}
			else
			{
				base.NPC.rotation -= rateOfRotation;
			}
		}
		if ((base.NPC.ai[0] != 8f && base.NPC.ai[0] != 13f) || base.NPC.spriteDirection == 1)
		{
			if (base.NPC.rotation > dukeRotationSpeed - rateOfRotation && base.NPC.rotation < dukeRotationSpeed + rateOfRotation)
			{
				base.NPC.rotation = dukeRotationSpeed;
			}
			if (base.NPC.rotation < 0f)
			{
				base.NPC.rotation += (float)Math.PI * 2f;
			}
			if (base.NPC.rotation > (float)Math.PI * 2f)
			{
				base.NPC.rotation -= (float)Math.PI * 2f;
			}
			if (base.NPC.rotation > dukeRotationSpeed - rateOfRotation && base.NPC.rotation < dukeRotationSpeed + rateOfRotation)
			{
				base.NPC.rotation = dukeRotationSpeed;
			}
		}
		if (base.NPC.ai[0] != -1f && (base.NPC.ai[0] < 9f || base.NPC.ai[0] > 12f))
		{
			if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.alpha += 15;
			}
			else
			{
				base.NPC.alpha -= 15;
			}
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			if (base.NPC.alpha > 150)
			{
				base.NPC.alpha = 150;
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.alpha = (int)MathHelper.Lerp((float)base.NPC.alpha, 0f, 0.2f);
			base.NPC.damage = 0;
			if (base.NPC.Calamity().newAI[3] == 0f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.98f;
			}
			int dukeFaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (dukeFaceDirection != 0)
			{
				base.NPC.direction = dukeFaceDirection;
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (base.NPC.ai[2] > 20f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.9f;
				base.NPC.alpha -= 5;
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.alpha += 15;
				}
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				if (base.NPC.alpha > 150)
				{
					base.NPC.alpha = 150;
				}
			}
			if (base.NPC.ai[2] == 75f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				CalamityUtils.AddScreenshakeAt(base.NPC.Center, 14f);
				SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath.WithPitchOffset(-0.5f), base.NPC.Center);
				SoundEngine.PlaySound(in DashSoundP3, base.NPC.Center);
				if (Main.netMode != 1)
				{
					CalamityUtils.BossAwakenMessage(base.NPC.whoAmI);
				}
			}
			if (base.NPC.ai[2] >= 75f)
			{
				float sd = ((player.Center.X > base.NPC.Center.X) ? (-1f) : 1f);
				shake = 2f;
				base.NPC.rotation = MathHelper.Lerp(MathHelper.WrapAngle(base.NPC.rotation), MathHelper.ToRadians((sd == 1f) ? 75f : (-75f)), 0.2f);
				if (base.NPC.ai[2] % 5f == 1f)
				{
					Vector2 position2 = base.NPC.Center + Utils.RotatedBy(new Vector2(-80f * sd, 0f), (double)base.NPC.rotation, default(Vector2));
					float factor = (base.NPC.ai[2] - 75f) / 50f;
					float a = MathHelper.Lerp(1f, 0f, factor);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(position2, Vector2.Zero, Utils.MultiplyRGBA(new Color(55, 55, 55), new Color(a, a, a, a)), "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.08f + 0.15f * factor, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 125f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 0f && !player.dead)
		{
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 500 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 phase1IdleSpeed = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -300f) - base.NPC.Center - base.NPC.velocity) * idlePhaseVelocity;
			base.NPC.SimpleFlyMovement(phase1IdleSpeed, idlePhaseAcceleration);
			int dukeLookAt = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (dukeLookAt != 0)
			{
				if (base.NPC.ai[2] == 0f && dukeLookAt != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = dukeLookAt;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (calamityGlobalNPC.newAI[1] != 1f || (phase2 && !phase2AI))
			{
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (float)idlePhaseTimer || (phase2 && !phase2AI))
				{
					int oldDukeAttackPicker = 0;
					switch ((int)base.NPC.ai[3])
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						oldDukeAttackPicker = 1;
						break;
					case 6:
						base.NPC.ai[3] = 1f;
						oldDukeAttackPicker = 2;
						break;
					case 7:
						base.NPC.ai[3] = 0f;
						oldDukeAttackPicker = 3;
						break;
					}
					if (phase2)
					{
						oldDukeAttackPicker = 4;
					}
					switch (oldDukeAttackPicker)
					{
					case 1:
					{
						base.NPC.ai[0] = 1f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						Vector2 distanceVector = player.Center + player.velocity * 20f - base.NPC.Center;
						base.NPC.velocity = Vector2.Normalize(distanceVector) * chargeVelocity;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
						DoChargeBurst(base.NPC, 0);
						if (dukeLookAt != 0)
						{
							base.NPC.direction = dukeLookAt;
							if (base.NPC.spriteDirection == 1)
							{
								base.NPC.rotation += (float)Math.PI;
							}
							base.NPC.spriteDirection = -base.NPC.direction;
						}
						break;
					}
					case 2:
						base.NPC.ai[0] = 2f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 3:
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 4:
						base.NPC.ai[0] = 4f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					}
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			if (Main.zenithWorld && base.NPC.ai[2] % 10f == 0f)
			{
				int dir = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (dir != 0)
				{
					if (base.NPC.ai[2] == 0f && dir != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.direction = dir;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				Vector2 distanceVector2 = player.Center + player.velocity * 20f - base.NPC.Center;
				base.NPC.velocity = Vector2.Normalize(distanceVector2) * chargeVelocity;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (dir != 0)
				{
					base.NPC.direction = dir;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
			}
			else
			{
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 1.01f;
			}
			Vector2 vel = base.NPC.velocity;
			((Vector2)(ref vel)).Normalize();
			vel *= 55f;
			DoChargeVisual(base.NPC, Phase);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center + vel + vel.RotatedBy(MathHelper.ToRadians(90f)), vel / 7f, affectedByGravity: false, 20, 0.75f, new Color(155, 155, 155, 55), fadeIn: true));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center + vel + vel.RotatedBy(MathHelper.ToRadians(-90f)), vel / 7f, affectedByGravity: false, 20, 0.75f, new Color(155, 155, 155, 55), fadeIn: true));
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
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
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 500 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 toothBallBelchPhaseSpeed = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -300f) - base.NPC.Center - base.NPC.velocity) * toothBallBelchPhaseVelocity;
			base.NPC.SimpleFlyMovement(toothBallBelchPhaseSpeed, toothBallBelchPhaseAcceleration);
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			if (base.NPC.ai[2] % (float)toothBallBelchPhaseDivisor == 0f)
			{
				if (base.NPC.ai[2] != 0f)
				{
					SoundEngine.PlaySound(in VomitSound, base.NPC.Center);
				}
				Vector2 toothBallDirection = Vector2.Normalize(player.Center - base.NPC.Center) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
				Vector2 toothBallVelocity = Vector2.Normalize(Main.player[base.NPC.target].Center - base.NPC.Center) * toothBallFinalVelocity;
				Vector2 toothBallSpawnPos = default(Vector2);
				((Vector2)(ref toothBallSpawnPos))._002Ector(toothBallDirection.X, toothBallDirection.Y + 45f);
				if (Main.netMode != 1)
				{
					int toothBall = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)toothBallSpawnPos.X, (int)toothBallSpawnPos.Y, ModContent.NPCType<OldDukeToothBall>(), 0, toothBallVelocity.X, toothBallVelocity.Y);
					Main.npc[toothBall].velocity = Vector2.Normalize(toothBallVelocity) * ((Vector2)(ref base.NPC.velocity)).Length();
					Main.npc[toothBall].netUpdate = true;
				}
				for (int j = 0; j < 50; j++)
				{
					int num2 = Main.rand.Next(6);
					int dustID = (((uint)num2 > 3u) ? 5 : 75);
					float num3 = Main.rand.NextFloat(3f, 12f);
					float angleRandom = 0.06f;
					Vector2 dustVel = Utils.RotatedBy(new Vector2(num3, 0f), (double)toothBallVelocity.ToRotation(), default(Vector2));
					dustVel = dustVel.RotatedBy(0f - angleRandom);
					dustVel = dustVel.RotatedByRandom(2f * angleRandom);
					float scale2 = Main.rand.NextFloat(1f, 2f);
					int idx = Dust.NewDust(toothBallSpawnPos, 40, 40, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale2);
					Main.dust[idx].noGravity = true;
				}
			}
			int toothBallLookAt = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (toothBallLookAt != 0)
			{
				base.NPC.direction = toothBallLookAt;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)toothBallBelchPhaseTimer)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(attackTimer - 30))
			{
				SoundEngine.PlaySound(in VomitSound, base.NPC.Center);
			}
			if (base.NPC.ai[2] >= (float)(attackTimer - 90) && base.NPC.ai[2] % 18f == 0f)
			{
				calamityGlobalNPC.newAI[2] += 100f;
				if (Main.netMode != 1)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 900f), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 900f), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					if (Main.getGoodWorld)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 1800f), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 1800f), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					}
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)attackTimer)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			NPC nPC5 = base.NPC;
			nPC5.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(phaseTransitionTimer - 60))
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			if (base.NPC.ai[2] >= (float)(phaseTransitionTimer - 60) && base.NPC.ai[2] % 18f == 0f)
			{
				calamityGlobalNPC.newAI[2] += 150f;
				if (Main.netMode != 1)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 50f + calamityGlobalNPC.newAI[2]), (int)(base.NPC.Center.Y + 540f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, 1f, 0f - sharkronVelocity);
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 50f - calamityGlobalNPC.newAI[2]), (int)(base.NPC.Center.Y + 540f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, -1f, 0f - sharkronVelocity);
					if (Main.getGoodWorld)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 50f + calamityGlobalNPC.newAI[2] * 0.5f), (int)(base.NPC.Center.Y + 270f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, 1f, 0f - sharkronVelocity);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 50f - calamityGlobalNPC.newAI[2] * 0.5f), (int)(base.NPC.Center.Y + 270f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, -1f, 0f - sharkronVelocity);
					}
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)phaseTransitionTimer)
			{
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 5f && !player.dead)
		{
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 500 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 dukePhase2IdleSpeed = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -300f) - base.NPC.Center - base.NPC.velocity) * idlePhaseVelocity;
			base.NPC.SimpleFlyMovement(dukePhase2IdleSpeed, idlePhaseAcceleration);
			int phase2FaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (phase2FaceDirection != 0)
			{
				if (base.NPC.ai[2] == 0f && phase2FaceDirection != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.direction = phase2FaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (calamityGlobalNPC.newAI[1] != 1f || (phase3 && !phase3AI))
			{
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (float)idlePhaseTimer || (phase3 && !phase3AI))
				{
					int phase2AttackPicker = 0;
					switch ((int)base.NPC.ai[3])
					{
					case 0:
					case 1:
					case 2:
					case 3:
						phase2AttackPicker = 1;
						break;
					case 4:
						base.NPC.ai[3] = 1f;
						phase2AttackPicker = 2;
						break;
					case 5:
						base.NPC.ai[3] = 0f;
						phase2AttackPicker = 3;
						break;
					}
					if (phase3)
					{
						phase2AttackPicker = 4;
					}
					switch (phase2AttackPicker)
					{
					case 1:
					{
						base.NPC.ai[0] = 6f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						Vector2 distanceVector3 = player.Center + player.velocity * 20f - base.NPC.Center;
						base.NPC.velocity = Vector2.Normalize(distanceVector3) * chargeVelocity;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
						if (phase2FaceDirection != 0)
						{
							base.NPC.direction = phase2FaceDirection;
							if (base.NPC.spriteDirection == 1)
							{
								base.NPC.rotation += (float)Math.PI;
							}
							base.NPC.spriteDirection = -base.NPC.direction;
						}
						DoChargeBurst(base.NPC, 1);
						break;
					}
					case 2:
						base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * spinAttackSpeed;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
						if (phase2FaceDirection != 0)
						{
							base.NPC.direction = phase2FaceDirection;
							if (base.NPC.spriteDirection == 1)
							{
								base.NPC.rotation += (float)Math.PI;
							}
							base.NPC.spriteDirection = -base.NPC.direction;
						}
						base.NPC.ai[0] = 7f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 3:
						base.NPC.ai[0] = 8f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 4:
						base.NPC.ai[0] = 9f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					}
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 6f)
		{
			if (Main.zenithWorld && base.NPC.ai[2] % 8f == 0f)
			{
				int dir2 = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (dir2 != 0)
				{
					if (base.NPC.ai[2] == 0f && dir2 != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.direction = dir2;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				Vector2 distanceVector4 = player.Center + player.velocity * 20f - base.NPC.Center;
				base.NPC.velocity = Vector2.Normalize(distanceVector4) * chargeVelocity;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (dir2 != 0)
				{
					base.NPC.direction = dir2;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
			}
			else
			{
				NPC nPC6 = base.NPC;
				nPC6.velocity *= 1.01f;
			}
			DoChargeVisual(base.NPC, Phase);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 7f)
		{
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				SoundEngine.PlaySound(in VortexSpawnSound, base.NPC.Center);
				int type2 = ModContent.ProjectileType<OldDukeVortex>();
				Vector2 vortexSpawn = base.NPC.Center + base.NPC.velocity.RotatedBy((float)Math.PI / 2f * (float)(-base.NPC.direction)) * spinTime / ((float)Math.PI * 2f);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), vortexSpawn, Vector2.Zero, type2, VortexDamage, 0f, Main.myPlayer, vortexSpawn.X, vortexSpawn.Y);
				}
			}
			if (base.NPC.ai[2] % (float)toothBallSpinPhaseDivisor == 0f)
			{
				if (base.NPC.ai[2] != 0f)
				{
					SoundEngine.PlaySound(in VomitSound, base.NPC.Center);
				}
				Vector2 phase2ToothBallDirection = Vector2.Normalize(base.NPC.velocity) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
				Vector2 toothBallVelocity2 = Vector2.Normalize(base.NPC.velocity).RotatedBy((float)Math.PI / 2f * (float)base.NPC.direction) * toothBallSpinToothBallVelocity;
				Vector2 toothBallSpawnPos2 = default(Vector2);
				((Vector2)(ref toothBallSpawnPos2))._002Ector(phase2ToothBallDirection.X, phase2ToothBallDirection.Y + 45f);
				if (Main.netMode != 1)
				{
					int toothBall2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)toothBallSpawnPos2.X, (int)toothBallSpawnPos2.Y, ModContent.NPCType<OldDukeToothBall>(), 0, toothBallVelocity2.X, toothBallVelocity2.Y);
					Main.npc[toothBall2].target = base.NPC.target;
					Main.npc[toothBall2].velocity = Vector2.Normalize(toothBallVelocity2) * toothBallSpinToothBallVelocity * 0.5f;
					Main.npc[toothBall2].netUpdate = true;
					Main.npc[toothBall2].ai[3] = 30f;
				}
				for (int k = 0; k < 50; k++)
				{
					int num2 = Main.rand.Next(6);
					int dustID2 = (((uint)num2 > 3u) ? 5 : 75);
					float num4 = Main.rand.NextFloat(3f, 12f);
					float angleRandom2 = 0.06f;
					Vector2 dustVel2 = Utils.RotatedBy(new Vector2(num4, 0f), (double)toothBallVelocity2.ToRotation(), default(Vector2));
					dustVel2 = dustVel2.RotatedBy(0f - angleRandom2);
					dustVel2 = dustVel2.RotatedByRandom(2f * angleRandom2);
					float scale3 = Main.rand.NextFloat(1f, 2f);
					int idx2 = Dust.NewDust(toothBallSpawnPos2, 40, 40, dustID2, dustVel2.X, dustVel2.Y, 0, default(Color), scale3);
					Main.dust[idx2].noGravity = true;
				}
			}
			base.NPC.velocity = base.NPC.velocity.RotatedBy((0.0 - (double)spinSpeed) * (double)(float)base.NPC.direction);
			base.NPC.rotation -= spinSpeed * (float)base.NPC.direction;
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)toothBallSpinTimer)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 8f)
		{
			NPC nPC7 = base.NPC;
			nPC7.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(attackTimer - 30))
			{
				SoundEngine.PlaySound(in VomitSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Vector2 phase2GoreDirection = base.NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)base.NPC.direction) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
					int type3 = ModContent.ProjectileType<OldDukeGore>();
					int totalGore = (Main.getGoodWorld ? 40 : 20);
					for (int l = 0; l < totalGore; l++)
					{
						float velocityX = (float)base.NPC.direction * goreVelocityX * (Main.rand.NextFloat(0.2f, 0.8f) + 0.5f);
						float velocityY = goreVelocityY * (Main.rand.NextFloat(0.2f, 0.8f) + 0.5f);
						if (Main.getGoodWorld)
						{
							velocityX *= Main.rand.NextFloat() + 0.5f;
							velocityY *= Main.rand.NextFloat() + 0.5f;
						}
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), phase2GoreDirection.X, phase2GoreDirection.Y, velocityX, 0f - velocityY, type3, GoreDamage, 0f, Main.myPlayer);
					}
				}
			}
			if (base.NPC.ai[2] >= (float)(attackTimer - 90) && base.NPC.ai[2] % 18f == 0f)
			{
				calamityGlobalNPC.newAI[2] += 100f;
				if (Main.netMode != 1)
				{
					float x = 900f - calamityGlobalNPC.newAI[2];
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + x), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - x), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)attackTimer)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 9f)
		{
			NPC nPC8 = base.NPC;
			nPC8.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(phaseTransitionTimer - 60))
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			if (base.NPC.ai[2] >= (float)(phaseTransitionTimer - 60) && base.NPC.ai[2] % 18f == 0f)
			{
				calamityGlobalNPC.newAI[2] += 200f;
				if (Main.netMode != 1)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 50f + calamityGlobalNPC.newAI[2]), (int)(base.NPC.Center.Y - 540f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, 1f, sharkronVelocity);
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 50f - calamityGlobalNPC.newAI[2]), (int)(base.NPC.Center.Y - 540f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, -1f, sharkronVelocity);
					if (Main.getGoodWorld)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 50f + calamityGlobalNPC.newAI[2] * 0.5f), (int)(base.NPC.Center.Y + 270f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, 1f, 0f - sharkronVelocity);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 50f - calamityGlobalNPC.newAI[2] * 0.5f), (int)(base.NPC.Center.Y + 270f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, -1f, 0f - sharkronVelocity);
					}
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)phaseTransitionTimer)
			{
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 10f && !player.dead)
		{
			base.NPC.alpha -= 25;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = 500 * Math.Sign((base.NPC.Center - player.Center).X);
			}
			Vector2 desiredVelocity = Vector2.Normalize(player.Center + new Vector2(0f - base.NPC.ai[1], -300f) - base.NPC.Center - base.NPC.velocity) * idlePhaseVelocity;
			base.NPC.SimpleFlyMovement(desiredVelocity, idlePhaseAcceleration);
			int phase3FaceDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (phase3FaceDirection != 0)
			{
				if (base.NPC.ai[2] == 0f && phase3FaceDirection != base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
					for (int m = 0; m < base.NPC.oldPos.Length; m++)
					{
						base.NPC.oldPos[m] = Vector2.Zero;
					}
				}
				base.NPC.direction = phase3FaceDirection;
				if (base.NPC.spriteDirection != -base.NPC.direction)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			if (calamityGlobalNPC.newAI[1] != 1f)
			{
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (float)idlePhaseTimer)
				{
					int phase3AttackPicker = 0;
					switch ((int)base.NPC.ai[3])
					{
					case 0:
					case 2:
					case 3:
					case 5:
					case 6:
					case 7:
						phase3AttackPicker = 1;
						break;
					case 1:
					case 8:
						phase3AttackPicker = 2;
						break;
					case 4:
						base.NPC.ai[3] = 1f;
						phase3AttackPicker = 3;
						break;
					case 9:
						base.NPC.ai[3] = 6f;
						phase3AttackPicker = 4;
						break;
					}
					switch (phase3AttackPicker)
					{
					case 1:
					{
						base.NPC.ai[0] = 11f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						Vector2 distanceVector5 = player.Center + player.velocity * 20f - base.NPC.Center;
						base.NPC.velocity = Vector2.Normalize(distanceVector5) * chargeVelocity;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
						if (phase3FaceDirection != 0)
						{
							base.NPC.direction = phase3FaceDirection;
							if (base.NPC.spriteDirection == 1)
							{
								base.NPC.rotation += (float)Math.PI;
							}
							base.NPC.spriteDirection = -base.NPC.direction;
						}
						DoChargeBurst(base.NPC, 2);
						break;
					}
					case 2:
						base.NPC.ai[0] = 12f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 3:
						base.NPC.ai[0] = 13f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					case 4:
						base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * spinAttackSpeed;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
						if (phase3FaceDirection != 0)
						{
							base.NPC.direction = phase3FaceDirection;
							if (base.NPC.spriteDirection == 1)
							{
								base.NPC.rotation += (float)Math.PI;
							}
							base.NPC.spriteDirection = -base.NPC.direction;
						}
						base.NPC.ai[0] = 14f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						break;
					}
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 11f)
		{
			if (Main.zenithWorld && base.NPC.ai[2] % 6f == 0f)
			{
				int dir3 = Math.Sign(player.Center.X - base.NPC.Center.X);
				if (dir3 != 0)
				{
					if (base.NPC.ai[2] == 0f && dir3 != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.direction = dir3;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				Vector2 distanceVector6 = player.Center + player.velocity * 20f - base.NPC.Center;
				base.NPC.velocity = Vector2.Normalize(distanceVector6) * chargeVelocity;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (dir3 != 0)
				{
					base.NPC.direction = dir3;
					if (base.NPC.spriteDirection == 1)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
			}
			else
			{
				NPC nPC9 = base.NPC;
				nPC9.velocity *= 1.01f;
			}
			DoChargeVisual(base.NPC, Phase);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)chargeTime)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				base.NPC.ai[0] = 10f;
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
			if (base.NPC.alpha < 255 && base.NPC.ai[2] >= (float)teleportPauseTimer - 15f)
			{
				base.NPC.alpha += 17;
				if (base.NPC.alpha > 255)
				{
					base.NPC.alpha = 255;
				}
			}
			NPC nPC10 = base.NPC;
			nPC10.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(teleportPauseTimer / 2))
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			if (Main.netMode != 1 && base.NPC.ai[2] == (float)teleportPauseTimer - 1f)
			{
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = 600 * Math.Sign((base.NPC.Center - player.Center).X);
				}
				Vector2 center = player.Center + new Vector2(base.NPC.ai[1], -300f);
				Vector2 val = (base.NPC.Center = center);
				Vector2 npcCenter = val;
				int phase3TeleportFaceDirection = Math.Sign(player.Center.X - npcCenter.X);
				if (phase3TeleportFaceDirection != 0)
				{
					if (base.NPC.ai[2] == 0f && phase3TeleportFaceDirection != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
						for (int n = 0; n < base.NPC.oldPos.Length; n++)
						{
							base.NPC.oldPos[n] = Vector2.Zero;
						}
					}
					base.NPC.direction = phase3TeleportFaceDirection;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)teleportPauseTimer)
			{
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] += 2f;
				if (base.NPC.ai[3] >= 9f)
				{
					base.NPC.ai[3] = 0f;
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 13f)
		{
			NPC nPC11 = base.NPC;
			nPC11.velocity *= 0.98f;
			base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, 0f, 0.02f);
			if (base.NPC.ai[2] == (float)(attackTimer - 30))
			{
				SoundEngine.PlaySound(in VomitSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Vector2 phase3GoreDirection = base.NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)base.NPC.direction) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
					int type4 = ModContent.ProjectileType<OldDukeGore>();
					int totalGore2 = (Main.getGoodWorld ? 40 : 20);
					for (int num5 = 0; num5 < totalGore2; num5++)
					{
						float velocityX2 = (float)base.NPC.direction * goreVelocityX * (Main.rand.NextFloat(0.2f, 0.8f) + 0.5f);
						float velocityY2 = goreVelocityY * (Main.rand.NextFloat(0.2f, 0.8f) + 0.5f);
						if (Main.getGoodWorld)
						{
							velocityX2 *= Main.rand.NextFloat() + 0.5f;
							velocityY2 *= Main.rand.NextFloat() + 0.5f;
						}
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), phase3GoreDirection.X, phase3GoreDirection.Y, velocityX2, 0f - velocityY2, type4, GoreDamage, 0f, Main.myPlayer);
					}
				}
			}
			if (base.NPC.ai[2] >= (float)(attackTimer - 90) && base.NPC.ai[2] % 18f == 0f)
			{
				calamityGlobalNPC.newAI[2] += 150f;
				if (Main.netMode != 1)
				{
					float x2 = 900f - calamityGlobalNPC.newAI[2];
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + x2), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - x2), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2]), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					if (Main.getGoodWorld)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + x2), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2] * 0.5f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - x2), (int)(base.NPC.Center.Y - calamityGlobalNPC.newAI[2] * 0.5f), ModContent.NPCType<SulphurousSharkron>(), 0, 0f, 0f, base.NPC.whoAmI);
					}
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)attackTimer)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 14f)
		{
			if (base.NPC.ai[2] == 0f)
			{
				RoarSoundSlot = SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				SoundEngine.PlaySound(in VortexSpawnSound, base.NPC.Center);
				int type5 = ModContent.ProjectileType<OldDukeVortex>();
				Vector2 vortexSpawn2 = base.NPC.Center + base.NPC.velocity.RotatedBy((float)Math.PI / 2f * (float)(-base.NPC.direction)) * spinTime / ((float)Math.PI * 2f);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), vortexSpawn2, Vector2.Zero, type5, VortexDamage, 0f, Main.myPlayer, vortexSpawn2.X, vortexSpawn2.Y);
				}
			}
			if (base.NPC.ai[2] % (float)toothBallSpinPhaseDivisor == 0f)
			{
				if (base.NPC.ai[2] != 0f)
				{
					SoundEngine.PlaySound(in VomitSound, base.NPC.Center);
				}
				Vector2 phase3ToothBallDirection = Vector2.Normalize(base.NPC.velocity) * (float)(base.NPC.width + 20) / 2f + base.NPC.Center;
				Vector2 toothBallVelocity3 = Vector2.Normalize(base.NPC.velocity).RotatedBy((float)Math.PI / 2f * (float)base.NPC.direction) * toothBallSpinToothBallVelocity;
				Vector2 toothBallSpawnPos3 = default(Vector2);
				((Vector2)(ref toothBallSpawnPos3))._002Ector(phase3ToothBallDirection.X, phase3ToothBallDirection.Y + 45f);
				if (Main.netMode != 1)
				{
					int toothBall3 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)toothBallSpawnPos3.X, (int)toothBallSpawnPos3.Y, ModContent.NPCType<OldDukeToothBall>(), 0, toothBallVelocity3.X, toothBallVelocity3.Y);
					Main.npc[toothBall3].target = base.NPC.target;
					Main.npc[toothBall3].velocity = Vector2.Normalize(toothBallVelocity3) * toothBallSpinToothBallVelocity * 0.5f;
					Main.npc[toothBall3].netUpdate = true;
					Main.npc[toothBall3].ai[3] = 60f;
				}
				for (int num6 = 0; num6 < 50; num6++)
				{
					int num2 = Main.rand.Next(6);
					int dustID3 = (((uint)num2 > 3u) ? 5 : 75);
					float num7 = Main.rand.NextFloat(3f, 12f);
					float angleRandom3 = 0.06f;
					Vector2 dustVel3 = Utils.RotatedBy(new Vector2(num7, 0f), (double)toothBallVelocity3.ToRotation(), default(Vector2));
					dustVel3 = dustVel3.RotatedBy(0f - angleRandom3);
					dustVel3 = dustVel3.RotatedByRandom(2f * angleRandom3);
					float scale4 = Main.rand.NextFloat(1f, 2f);
					int idx3 = Dust.NewDust(toothBallSpawnPos3, 40, 40, dustID3, dustVel3.X, dustVel3.Y, 0, default(Color), scale4);
					Main.dust[idx3].noGravity = true;
				}
			}
			base.NPC.velocity = base.NPC.velocity.RotatedBy((0.0 - (double)spinSpeed) * (double)base.NPC.direction);
			base.NPC.rotation -= spinSpeed * (float)base.NPC.direction;
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= (float)toothBallSpinTimer)
			{
				calamityGlobalNPC.newAI[0] += exhaustionIncreasePerAttack;
				base.NPC.ai[0] = 10f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		if (SoundEngine.TryGetActiveSound(RoarSoundSlot, out ActiveSound roarSound) && roarSound.IsPlaying)
		{
			roarSound.Position = base.NPC.Center;
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
			typeName = CalamityUtils.GetTextValue("NPCs.BoomerDuke");
		}
	}

	public override void FindFrame(int frameHeight)
	{
		bool tired = base.NPC.Calamity().newAI[1] == 1f;
		if (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 5f || base.NPC.ai[0] == 10f || base.NPC.ai[0] == 12f)
		{
			int frameChangeFrequency = (tired ? 14 : 7);
			if (base.NPC.ai[0] == 5f || base.NPC.ai[0] == 12f)
			{
				frameChangeFrequency = (tired ? 12 : 6);
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > (double)frameChangeFrequency)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * 6)
			{
				base.NPC.frame.Y = 0;
			}
		}
		if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 6f || base.NPC.ai[0] == 11f)
		{
			base.NPC.frame.Y = frameHeight * 2;
		}
		if (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 7f || base.NPC.ai[0] == 14f)
		{
			base.NPC.frame.Y = frameHeight * 6;
		}
		if (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 8f || base.NPC.ai[0] == 13f || base.NPC.ai[0] == -1f)
		{
			int frameChangeGateValue = 120;
			if (base.NPC.ai[2] < (float)(frameChangeGateValue - 50) || base.NPC.ai[2] > (float)(frameChangeGateValue - 10))
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 7.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y += frameHeight;
				}
				if (base.NPC.frame.Y >= frameHeight * 6)
				{
					base.NPC.frame.Y = 0;
				}
			}
			else
			{
				base.NPC.frame.Y = frameHeight * 5;
				if (base.NPC.ai[2] > (float)(frameChangeGateValue - 40) && base.NPC.ai[2] < (float)(frameChangeGateValue - 15))
				{
					base.NPC.frame.Y = frameHeight * 6;
				}
			}
		}
		if (base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f)
		{
			int secondFrameChangeGateValue = 180;
			if (base.NPC.ai[2] < (float)(secondFrameChangeGateValue - 60) || base.NPC.ai[2] > (float)(secondFrameChangeGateValue - 20))
			{
				base.NPC.frameCounter++;
				if (base.NPC.frameCounter > 7.0)
				{
					base.NPC.frameCounter = 0.0;
					base.NPC.frame.Y += frameHeight;
				}
				if (base.NPC.frame.Y >= frameHeight * 6)
				{
					base.NPC.frame.Y = 0;
				}
			}
			else
			{
				base.NPC.frame.Y = frameHeight * 5;
				if (base.NPC.ai[2] > (float)(secondFrameChangeGateValue - 50) && base.NPC.ai[2] < (float)(secondFrameChangeGateValue - 25))
				{
					base.NPC.frame.Y = frameHeight * 6;
				}
			}
		}
		if (base.NPC.ai[0] == -1f && base.NPC.ai[2] >= 75f)
		{
			base.NPC.frame.Y = frameHeight * 6;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
		Color finalDrawColor = Color.Lerp(drawColor, Color.White, NuclearOverlayVisual);
		Color overlayDrawColor = Color.Lerp(Color.Transparent, Color.LimeGreen.MultiplyRGBA(new Color(255, 255, 255, 0)), NuclearOverlayVisual);
		finalDrawColor = Color.Lerp(finalDrawColor, Color.LimeGreen.MultiplyRGBA(new Color(255, 255, 255, 0)), NuclearOverlayVisual);
		finalDrawColor = Color.Lerp(finalDrawColor, Color.Transparent, (float)base.NPC.alpha / 255f);
		overlayDrawColor = Color.Lerp(overlayDrawColor, Color.Transparent, (float)base.NPC.alpha / 255f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture2D15.Width / 2), (float)(texture2D15.Height / Main.npcFrameCount[base.Type] / 2));
		Color color = drawColor;
		Color drawLerpColor = Color.White;
		float drawLerpValue = 0f;
		bool halfTiredBuffColor = base.NPC.ai[0] > 4f;
		bool num = base.NPC.ai[0] > 9f && base.NPC.ai[0] <= 12f;
		int ai2Compare = 120;
		int buffColorDivisor = 60;
		if (num)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.4f, 0.8f, 0.4f, 1f);
		}
		else if (halfTiredBuffColor)
		{
			color = CalamityGlobalNPC.buffColor(color, 0.5f, 0.7f, 0.5f, 1f);
		}
		else if (base.NPC.ai[0] == 4f && base.NPC.ai[2] > (float)ai2Compare)
		{
			float buffColorDampener = base.NPC.ai[2] - (float)ai2Compare;
			buffColorDampener /= (float)buffColorDivisor;
			color = CalamityGlobalNPC.buffColor(color, 1f - 0.5f * buffColorDampener, 1f - 0.3f * buffColorDampener, 1f - 0.5f * buffColorDampener, 1f);
		}
		int afterimageAmt = 10;
		int afterimageIncrement = 2;
		if (base.NPC.ai[0] == -1f)
		{
			afterimageAmt = 0;
		}
		if (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 5f || base.NPC.ai[0] == 10f || base.NPC.ai[0] == 12f)
		{
			afterimageAmt = 7;
		}
		if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 6f || base.NPC.ai[0] > 9f)
		{
			drawLerpColor = Color.Lime;
			drawLerpValue = 0.5f;
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
				afterimageColor = Color.Lerp(afterimageColor, drawLerpColor, drawLerpValue);
				afterimageColor = Color.Lerp(afterimageColor, overlayDrawColor, NuclearOverlayVisual);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		int secondAfterimageAmt = 0;
		float afterimageOpacity = 0f;
		float afterimageScale = 0f;
		if (base.NPC.ai[0] == -1f)
		{
			secondAfterimageAmt = 0;
		}
		if ((base.NPC.ai[0] == 3f || base.NPC.ai[0] == 8f || base.NPC.ai[0] == 13f) && base.NPC.ai[2] > 60f)
		{
			secondAfterimageAmt = 6;
			afterimageOpacity = 1f - (float)Math.Cos((base.NPC.ai[2] - 60f) / 30f * ((float)Math.PI * 2f));
			afterimageOpacity /= 3f;
			afterimageScale = 40f;
		}
		if ((base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f) && base.NPC.ai[2] > (float)ai2Compare)
		{
			secondAfterimageAmt = 6;
			afterimageOpacity = 1f - (float)Math.Cos((base.NPC.ai[2] - (float)ai2Compare) / (float)buffColorDivisor * ((float)Math.PI * 2f));
			afterimageOpacity /= 3f;
			afterimageScale = 60f;
		}
		if (base.NPC.ai[0] == 12f)
		{
			secondAfterimageAmt = 6;
			afterimageOpacity = 1f - (float)Math.Cos(base.NPC.ai[2] / 30f * ((float)Math.PI * 2f));
			afterimageOpacity /= 3f;
			afterimageScale = 20f;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 0; j < secondAfterimageAmt; j++)
			{
				Color secondAfterimageColor = Color.Lerp(finalDrawColor, Color.Transparent, (float)j / (float)secondAfterimageAmt);
				Vector2 secondAfterimagePos = base.NPC.Center + ((float)j / (float)secondAfterimageAmt * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * afterimageScale * afterimageOpacity - screenPos;
				secondAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				secondAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, secondAfterimagePos, (Rectangle?)base.NPC.frame, secondAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		drawLocation += new Vector2(Main.rand.NextFloat(0f - shake, shake), Main.rand.NextFloat(0f - shake, shake));
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(finalDrawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(overlayDrawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		float auraOutset = 6f + (float)(Math.Sin(VisualTimerSystem.GlobalVisualTimer / 10f) * 10.0);
		for (float i2 = 0f; i2 < 360f; i2 += 90f)
		{
			spriteBatch.Draw(texture2D15, drawLocation + Utils.RotatedBy(new Vector2(auraOutset, 0f), (double)MathHelper.ToRadians(i2), default(Vector2)), (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(overlayDrawColor.MultiplyRGBA(new Color(0.4f, 0.4f, 0.4f, 0.4f))), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		if (base.NPC.ai[0] >= 4f && base.NPC.Calamity().newAI[1] != 1f)
		{
			texture2D15 = GlowTexture.Value;
			Color yellowLerpColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
			drawLerpColor = Color.Yellow;
			drawLerpValue = 1f;
			afterimageOpacity = 0.5f;
			afterimageScale = 10f;
			afterimageIncrement = 1;
			if (base.NPC.ai[0] == 4f || base.NPC.ai[0] == 9f)
			{
				float otherAfterimageOpacity = base.NPC.ai[2] - (float)ai2Compare;
				otherAfterimageOpacity /= (float)buffColorDivisor;
				drawLerpColor *= otherAfterimageOpacity;
				yellowLerpColor *= otherAfterimageOpacity;
			}
			if (base.NPC.ai[0] == 12f)
			{
				float ai2Opacity = base.NPC.ai[2];
				ai2Opacity /= 30f;
				if (ai2Opacity > 0.5f)
				{
					ai2Opacity = 1f - ai2Opacity;
				}
				ai2Opacity *= 2f;
				ai2Opacity = 1f - ai2Opacity;
				drawLerpColor *= ai2Opacity;
				yellowLerpColor *= ai2Opacity;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int k = 1; k < afterimageAmt; k += afterimageIncrement)
				{
					Color yellowAfterimageColor = yellowLerpColor;
					yellowAfterimageColor = Color.Lerp(yellowAfterimageColor, drawLerpColor, drawLerpValue);
					yellowAfterimageColor *= (float)(afterimageAmt - k) / 15f;
					Vector2 yellowAfterimagePos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					yellowAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					yellowAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					spriteBatch.Draw(texture2D15, yellowAfterimagePos, (Rectangle?)base.NPC.frame, yellowAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
				for (int l = 1; l < secondAfterimageAmt; l++)
				{
					Color secondYellowAfterimageColor = yellowLerpColor;
					secondYellowAfterimageColor = Color.Lerp(secondYellowAfterimageColor, drawLerpColor, drawLerpValue);
					secondYellowAfterimageColor = base.NPC.GetAlpha(secondYellowAfterimageColor);
					secondYellowAfterimageColor *= 1f - afterimageOpacity;
					Vector2 secondYellowAfterimagePos = base.NPC.Center + ((float)l / (float)secondAfterimageAmt * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * afterimageScale * afterimageOpacity - screenPos;
					secondYellowAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					secondYellowAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					spriteBatch.Draw(texture2D15, secondYellowAfterimagePos, (Rectangle?)base.NPC.frame, secondYellowAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, yellowLerpColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SupremeHealingPotion>();
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<SeaKing>() }, DownedBossSystem.downedBoomerDuke);
			DownedBossSystem.downedBoomerDuke = true;
			AcidRainEvent.OldDukeHasBeenEncountered = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<OldDukeBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] items = new int[7]
		{
			ModContent.ItemType<InsidiousImpaler>(),
			ModContent.ItemType<FetidEmesis>(),
			ModContent.ItemType<SepticSkewer>(),
			ModContent.ItemType<VitriolicViper>(),
			ModContent.ItemType<MutatedTruffle>(),
			ModContent.ItemType<CadaverousCarrion>(),
			ModContent.ItemType<ToxicantTwister>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, items));
		normalOnly.Add(ModContent.ItemType<TheOldReaper>(), 10);
		normalOnly.Add(ModContent.ItemType<OldDukeMask>(), 7);
		normalOnly.Add(ModContent.ItemType<EldenDiorama>(), 10);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<OldDukeTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<OldDukeRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<ShatteredCommunity>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedBoomerDuke, ModContent.ItemType<LoreOldDuke>(), ui: true, DropHelper.FirstKillText);
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
			target.AddBuff(ModContent.BuffType<Irradiated>(), (Phase == 2) ? 600 : ((Phase == 1) ? 480 : 360));
			target.AddBuff(ModContent.BuffType<HeavyBleeding>(), (Phase == 2) ? 300 : ((Phase == 1) ? 240 : 180));
			if (Main.zenithWorld)
			{
				target.AddBuff(148, Main.rand.Next(180, 601));
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int r = 0; r < 150; r++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, 2 * hit.HitDirection, -2f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center + Vector2.UnitX * 20f * (float)base.NPC.direction, base.NPC.velocity, base.Mod.Find<ModGore>("OldDukeGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center + Vector2.UnitX * 20f * (float)base.NPC.direction, base.NPC.velocity, base.Mod.Find<ModGore>("OldDukeGore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center - Vector2.UnitX * 20f * (float)base.NPC.direction, base.NPC.velocity, base.Mod.Find<ModGore>("OldDukeGore3").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center - Vector2.UnitX * 20f * (float)base.NPC.direction, base.NPC.velocity, base.Mod.Find<ModGore>("OldDukeGore4").Type, base.NPC.scale);
			}
		}
	}

	static OldDuke()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		GlowColor = new Color(55, 255, 25, 0);
		HuffSound = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeHuff");
		RoarSound = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeRoar");
		VomitSound = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeVomit");
		VortexSpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeVortexSpawn");
		DashSound = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeDash");
		DashSoundP3 = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeDashP3");
		FireGreen = new Color(155, 255, 55);
		Phase2ContactDamageMult = 1.1f;
		Phase3ContactDamageMult = 1.2f;
		GoreDamage = 55;
		VortexDamage = 105;
		FartDamage = 80;
	}
}
