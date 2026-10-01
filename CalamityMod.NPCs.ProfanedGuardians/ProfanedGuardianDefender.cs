using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Items.Tools;
using CalamityMod.NPCs.Providence;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Utilities;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ProfanedGuardians;

[AutoloadBossHead]
public class ProfanedGuardianDefender : ModNPC
{
	private int healTimer;

	private const float TimeForShieldDespawn = 120f;

	public static readonly SoundStyle DashSound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianDash");

	public static readonly SoundStyle RockShieldSpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRockShieldActivate");

	public static readonly SoundStyle ShieldDeathSound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianShieldDeactivate");

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> TextureNight_Glow;

	public static int FireDamage = 36;

	public static int BlobDamage = 36;

	public static int FireSentryDamage = 54;

	public static int MoltenBlastDamage = 54;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 10;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.Scale = 0.75f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 25f;
		value.Position.Y += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			TextureNight_Glow = ModContent.Request<Texture2D>(Texture + "GlowNight", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 108;
		base.NPC.npcSlots = 3f;
		base.NPC.aiStyle = -1;
		base.NPC.width = 228;
		base.NPC.height = 164;
		base.NPC.defense = 50;
		base.NPC.DR_NERD(0.4f);
		base.NPC.LifeMaxNERB(32000, 48000, 35000);
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.AIType = -1;
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<ProfanedGuardianCommander>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ProfanedGuardianDefender")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(healTimer);
		writer.Write(base.NPC.chaseable);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		healTimer = reader.ReadInt32();
		base.NPC.chaseable = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.12f + ((Vector2)(ref base.NPC.velocity)).Length() / 120f;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec9: Unknown result type (might be due to invalid IL or missing references)
		//IL_121b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f21: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1298: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d84: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d38: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d47: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1103: Unknown result type (might be due to invalid IL or missing references)
		//IL_110e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1113: Unknown result type (might be due to invalid IL or missing references)
		//IL_1118: Unknown result type (might be due to invalid IL or missing references)
		//IL_111a: Unknown result type (might be due to invalid IL or missing references)
		//IL_112d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1132: Unknown result type (might be due to invalid IL or missing references)
		//IL_1139: Unknown result type (might be due to invalid IL or missing references)
		//IL_113e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_160b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1616: Unknown result type (might be due to invalid IL or missing references)
		//IL_161b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1631: Unknown result type (might be due to invalid IL or missing references)
		//IL_1636: Unknown result type (might be due to invalid IL or missing references)
		//IL_163b: Unknown result type (might be due to invalid IL or missing references)
		//IL_163d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1641: Unknown result type (might be due to invalid IL or missing references)
		//IL_1646: Unknown result type (might be due to invalid IL or missing references)
		//IL_164e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1652: Unknown result type (might be due to invalid IL or missing references)
		//IL_1657: Unknown result type (might be due to invalid IL or missing references)
		//IL_1667: Unknown result type (might be due to invalid IL or missing references)
		//IL_1672: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1501: Unknown result type (might be due to invalid IL or missing references)
		//IL_1506: Unknown result type (might be due to invalid IL or missing references)
		//IL_1508: Unknown result type (might be due to invalid IL or missing references)
		//IL_151b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1520: Unknown result type (might be due to invalid IL or missing references)
		//IL_1527: Unknown result type (might be due to invalid IL or missing references)
		//IL_152c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_189c: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1599: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1336: Unknown result type (might be due to invalid IL or missing references)
		//IL_133b: Unknown result type (might be due to invalid IL or missing references)
		//IL_133c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1341: Unknown result type (might be due to invalid IL or missing references)
		//IL_1348: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_168a: Unknown result type (might be due to invalid IL or missing references)
		//IL_168f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1694: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16de: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1705: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_1750: Unknown result type (might be due to invalid IL or missing references)
		//IL_1755: Unknown result type (might be due to invalid IL or missing references)
		//IL_1363: Unknown result type (might be due to invalid IL or missing references)
		//IL_1364: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_118d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1192: Unknown result type (might be due to invalid IL or missing references)
		//IL_1194: Unknown result type (might be due to invalid IL or missing references)
		//IL_119b: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1950: Unknown result type (might be due to invalid IL or missing references)
		//IL_1965: Unknown result type (might be due to invalid IL or missing references)
		//IL_196a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1097: Unknown result type (might be due to invalid IL or missing references)
		//IL_1098: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_19bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_156b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1578: Unknown result type (might be due to invalid IL or missing references)
		//IL_157d: Unknown result type (might be due to invalid IL or missing references)
		//IL_157f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1586: Unknown result type (might be due to invalid IL or missing references)
		//IL_158b: Unknown result type (might be due to invalid IL or missing references)
		//IL_149e: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_14af: Unknown result type (might be due to invalid IL or missing references)
		//IL_1baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1404: Unknown result type (might be due to invalid IL or missing references)
		//IL_140a: Unknown result type (might be due to invalid IL or missing references)
		//IL_140c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1411: Unknown result type (might be due to invalid IL or missing references)
		//IL_1413: Unknown result type (might be due to invalid IL or missing references)
		//IL_141b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1421: Unknown result type (might be due to invalid IL or missing references)
		//IL_1423: Unknown result type (might be due to invalid IL or missing references)
		//IL_1428: Unknown result type (might be due to invalid IL or missing references)
		//IL_142a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1435: Unknown result type (might be due to invalid IL or missing references)
		//IL_143a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1452: Unknown result type (might be due to invalid IL or missing references)
		//IL_1459: Unknown result type (might be due to invalid IL or missing references)
		//IL_1460: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1470: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.doughnutBossDefender = base.NPC.whoAmI;
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1.1f, 0.9f, 0f);
		if (CalamityGlobalNPC.doughnutBoss < 0 || !Main.npc[CalamityGlobalNPC.doughnutBoss].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		Vector2 dustAndProjectileOffset = default(Vector2);
		((Vector2)(ref dustAndProjectileOffset))._002Ector(40f * (float)base.NPC.direction, 20f);
		Vector2 shootFrom = base.NPC.Center + dustAndProjectileOffset;
		base.NPC.rotation = base.NPC.velocity.X * 0.005f;
		bool healerAlive = false;
		if (CalamityGlobalNPC.doughnutBossHealer != -1 && Main.npc[CalamityGlobalNPC.doughnutBossHealer].active)
		{
			healerAlive = true;
		}
		if (healerAlive)
		{
			float distanceFromHealer = Vector2.Distance(Main.npc[CalamityGlobalNPC.doughnutBossHealer].Center, base.NPC.Center);
			if (distanceFromHealer > 2000f || Main.npc[CalamityGlobalNPC.doughnutBossHealer].justHit || base.NPC.life == base.NPC.lifeMax)
			{
				healTimer = 0;
			}
			else
			{
				float healGateValue = 60f;
				healTimer++;
				if ((float)healTimer >= healGateValue)
				{
					SoundEngine.PlaySound(in SoundID.Item8, shootFrom);
					int maxHealDustIterations = (int)distanceFromHealer;
					int maxDust = 100;
					int dustDivisor = maxHealDustIterations / maxDust;
					if (dustDivisor < 2)
					{
						dustDivisor = 2;
					}
					Vector2 healDustOffset = default(Vector2);
					((Vector2)(ref healDustOffset))._002Ector(40f * (float)Main.npc[CalamityGlobalNPC.doughnutBossHealer].direction, 20f);
					Vector2 dustLineStart = Main.npc[CalamityGlobalNPC.doughnutBossHealer].Center + healDustOffset;
					Vector2 dustLineEnd = shootFrom;
					Vector2 currentDustPos = default(Vector2);
					Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
					Vector2 value5 = default(Vector2);
					((Vector2)(ref value5))._002Ector(2.1f, 2f);
					int dustSpawned = 0;
					for (int i = 0; i < maxHealDustIterations; i++)
					{
						if (i % dustDivisor == 0)
						{
							currentDustPos = Vector2.Lerp(dustLineStart, dustLineEnd, (float)i / (float)maxHealDustIterations);
							Color dustColor = Main.hslToRgb(Main.rgbToHsl(new Color(255, 200, Math.Abs(Main.DiscoB - (int)((float)dustSpawned * 2.55f)))).X, 1f, 0.5f);
							((Color)(ref dustColor)).A = byte.MaxValue;
							int dust = Dust.NewDust(currentDustPos, 0, 0, 267, 0f, 0f, 0, dustColor);
							Main.dust[dust].position = currentDustPos;
							Main.dust[dust].velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * (float)i / (float)maxHealDustIterations) * value5 * (0.8f + Main.rand.NextFloat() * 0.4f) + base.NPC.velocity;
							Main.dust[dust].noGravity = true;
							Main.dust[dust].scale = 1f;
							Main.dust[dust].fadeIn = Main.rand.NextFloat() * 2f;
							Dust dust2 = DustExtensions.BetterCloneDust(dust);
							dust2.scale /= 2f;
							dust2.fadeIn /= 2f;
							dust2.color = new Color(255, 255, 255, 255);
							dustSpawned++;
						}
					}
					healTimer = 0;
					if (Main.netMode != 1)
					{
						int healAmt = base.NPC.lifeMax / 10;
						if (healAmt > base.NPC.lifeMax - base.NPC.life)
						{
							healAmt = base.NPC.lifeMax - base.NPC.life;
						}
						if (healAmt > 0)
						{
							base.NPC.life += healAmt;
							base.NPC.HealEffect(healAmt);
							base.NPC.netUpdate = true;
						}
					}
				}
			}
			if (Main.npc[CalamityGlobalNPC.doughnutBossHealer].ai[0] == 599f && Main.zenithWorld && Main.netMode != 1)
			{
				base.NPC.lifeMax += 7500;
				base.NPC.life += base.NPC.lifeMax - base.NPC.life;
				base.NPC.HealEffect(base.NPC.lifeMax - base.NPC.life);
				base.NPC.netUpdate = true;
			}
		}
		if (Main.npc[CalamityGlobalNPC.doughnutBoss].ai[3] == -1f)
		{
			base.NPC.velocity = Main.npc[CalamityGlobalNPC.doughnutBoss].velocity;
			return;
		}
		Player player = Main.player[Main.npc[CalamityGlobalNPC.doughnutBoss].target];
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase1 = healerAlive;
		base.NPC.chaseable = !phase1;
		float commanderGuardPhase2Duration = (death ? 480f : (revenge ? 510f : (expertMode ? 540f : 600f)));
		float timeBeforeRocksRespawnInPhase2 = 90f;
		float throwRocksGateValue = 60f;
		float distanceInFrontOfCommander = 160f;
		float chargeVelocityMult = 0.25f;
		float maxChargeVelocity = (death ? 22f : (revenge ? 20.5f : (expertMode ? 19f : 16f)));
		if (Main.getGoodWorld)
		{
			maxChargeVelocity *= 1.15f;
		}
		bool commanderUsingLaser = Main.npc[CalamityGlobalNPC.doughnutBoss].ai[0] == 5f;
		float moveToOtherSideInPhase1GateValue = 900f;
		float timeBeforeMoveToOtherSideInPhase1Reset = moveToOtherSideInPhase1GateValue * 2f;
		float goLowDurationPhase1 = 240f * 0.5f;
		float roundedGoLowPhase1Check = (float)Math.Round((double)goLowDurationPhase1 * 0.5);
		bool commanderGoingLowOrHighInPhase1 = (Main.npc[CalamityGlobalNPC.doughnutBoss].localAI[3] > moveToOtherSideInPhase1GateValue - goLowDurationPhase1 && Main.npc[CalamityGlobalNPC.doughnutBoss].localAI[3] <= moveToOtherSideInPhase1GateValue + roundedGoLowPhase1Check) || Main.npc[CalamityGlobalNPC.doughnutBoss].localAI[3] > timeBeforeMoveToOtherSideInPhase1Reset - goLowDurationPhase1 || Main.npc[CalamityGlobalNPC.doughnutBoss].localAI[3] <= 0f - roundedGoLowPhase1Check;
		float moveToOtherSideInPhase2GateValue = commanderGuardPhase2Duration - 120f;
		float timeBeforeMoveToOtherSideInPhase2Reset = moveToOtherSideInPhase2GateValue * 2f;
		float goLowDurationPhase2 = 210f * 0.5f;
		float roundedGoLowPhase2Check = (float)Math.Round((double)goLowDurationPhase2 * 0.5);
		bool commanderGoingLowOrHighInPhase2 = (Main.npc[CalamityGlobalNPC.doughnutBoss].Calamity().newAI[1] > moveToOtherSideInPhase2GateValue - goLowDurationPhase2 && Main.npc[CalamityGlobalNPC.doughnutBoss].Calamity().newAI[1] <= moveToOtherSideInPhase2GateValue + roundedGoLowPhase2Check) || Main.npc[CalamityGlobalNPC.doughnutBoss].Calamity().newAI[1] > timeBeforeMoveToOtherSideInPhase2Reset - goLowDurationPhase2 || Main.npc[CalamityGlobalNPC.doughnutBoss].Calamity().newAI[1] <= 0f - roundedGoLowPhase2Check;
		if (commanderGoingLowOrHighInPhase1 | commanderGoingLowOrHighInPhase2)
		{
			base.NPC.localAI[3] = 1f;
		}
		else
		{
			base.NPC.localAI[3] = 0f;
		}
		bool respawnRocksInPhase2 = base.NPC.ai[1] == 0f - commanderGuardPhase2Duration + timeBeforeRocksRespawnInPhase2 && !commanderGoingLowOrHighInPhase2;
		int rockTypes = 6;
		int maxRocks = (respawnRocksInPhase2 ? 18 : 36);
		int rockRings = 3;
		int totalRocksPerRing = maxRocks / rockRings;
		int spacing = 360 / totalRocksPerRing;
		int distance2 = 200;
		bool justSpawnedRocks = false;
		if ((base.NPC.localAI[0] == 0f) | respawnRocksInPhase2)
		{
			justSpawnedRocks = true;
			base.NPC.localAI[0] = 1f;
			if (Main.netMode != 1)
			{
				for (int j = 0; j < totalRocksPerRing; j++)
				{
					int rockType = Main.rand.Next(rockTypes) + 1;
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(j * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(j * spacing) * (double)distance2), ModContent.NPCType<ProfanedRocks>(), base.NPC.whoAmI, j * spacing, 0f, rockType);
					rockType = Main.rand.Next(rockTypes) + 1;
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(j * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(j * spacing) * (double)distance2), ModContent.NPCType<ProfanedRocks>(), base.NPC.whoAmI, j * spacing, 1f, rockType);
					rockType = Main.rand.Next(rockTypes) + 1;
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(j * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(j * spacing) * (double)distance2), ModContent.NPCType<ProfanedRocks>(), base.NPC.whoAmI, j * spacing, 2f, rockType);
				}
			}
		}
		if (phase1)
		{
			int minRocks = maxRocks / 2;
			if (NPC.CountNPCS(ModContent.NPCType<ProfanedRocks>()) < minRocks)
			{
				justSpawnedRocks = true;
				totalRocksPerRing = minRocks / rockRings;
				spacing = 360 / totalRocksPerRing;
				if (Main.netMode != 1)
				{
					for (int k = 0; k < totalRocksPerRing; k++)
					{
						int rockType2 = Main.rand.Next(rockTypes) + 1;
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(k * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(k * spacing) * (double)distance2), ModContent.NPCType<ProfanedRocks>(), base.NPC.whoAmI, k * spacing, 0f, rockType2);
						rockType2 = Main.rand.Next(rockTypes) + 1;
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(k * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(k * spacing) * (double)distance2), ModContent.NPCType<ProfanedRocks>(), base.NPC.whoAmI, k * spacing, 1f, rockType2);
						rockType2 = Main.rand.Next(rockTypes) + 1;
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(k * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(k * spacing) * (double)distance2), ModContent.NPCType<ProfanedRocks>(), base.NPC.whoAmI, k * spacing, 2f, rockType2);
					}
				}
			}
		}
		else if (base.NPC.localAI[1] < 120f)
		{
			if (base.NPC.localAI[1] == 0f)
			{
				SoundEngine.PlaySound(in ShieldDeathSound, base.NPC.Center);
			}
			base.NPC.localAI[1]++;
		}
		if (justSpawnedRocks)
		{
			SoundEngine.PlaySound(in RockShieldSpawnSound, base.NPC.Center);
			int totalDust = maxRocks;
			for (int l = 0; l < rockRings; l++)
			{
				for (int m = 0; m < totalDust; m++)
				{
					Vector2 val = (base.NPC.velocity.SafeNormalize(Vector2.UnitY) * new Vector2((float)distance2, (float)distance2)).RotatedBy((float)(m - (totalDust / 2 - 1)) * ((float)Math.PI * 2f) / (float)totalDust) + base.NPC.Center;
					Vector2 dustVelocity = val - base.NPC.Center;
					Color dustColor2 = Main.hslToRgb(Main.rgbToHsl(Color.Orange).X, 1f, 0.5f);
					((Color)(ref dustColor2)).A = byte.MaxValue;
					int dust3 = Dust.NewDust(val + dustVelocity, 0, 0, 267, dustVelocity.X, dustVelocity.Y, 0, dustColor2, 1.4f);
					Main.dust[dust3].noGravity = true;
					Main.dust[dust3].noLight = true;
					Main.dust[dust3].velocity = dustVelocity * ((float)l * 0.1f + 0.1f);
				}
			}
		}
		float moveVelocity = (death ? 22f : (revenge ? 21f : (expertMode ? 20f : 18f)));
		if (Main.getGoodWorld)
		{
			moveVelocity *= 1.25f;
		}
		if (healerAlive)
		{
			moveVelocity *= 0.8f;
		}
		float distanceToStayAwayFromTarget = 800f;
		if (Vector2.Distance(base.NPC.Center, player.Center) > distanceToStayAwayFromTarget + 160f)
		{
			moveVelocity *= 2f;
		}
		if (commanderGoingLowOrHighInPhase2)
		{
			moveVelocity *= 2f;
		}
		else if (commanderGoingLowOrHighInPhase1)
		{
			moveVelocity *= 1.66f;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			if (!phase1)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
				if (((Vector2)(ref base.NPC.velocity)).Length() <= 2f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] >= throwRocksGateValue)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
			if (!commanderUsingLaser)
			{
				float projectileShootGateValue = (death ? 480f : (revenge ? 510f : (expertMode ? 540f : 600f)));
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= projectileShootGateValue)
				{
					base.NPC.ai[1] = 0f;
					if (Main.netMode != 1)
					{
						float projectileVelocityY = base.NPC.velocity.Y;
						if (projectileVelocityY < 0f)
						{
							projectileVelocityY = 0f;
						}
						projectileVelocityY += (expertMode ? 4f : 3f);
						Vector2 projectileVelocity = default(Vector2);
						((Vector2)(ref projectileVelocity))._002Ector(base.NPC.velocity.X * 0.25f, projectileVelocityY);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, projectileVelocity, ModContent.ProjectileType<HolyBomb>(), FireSentryDamage, 0f, Main.myPlayer);
					}
				}
			}
			Vector2 distanceFromDestination = Main.npc[CalamityGlobalNPC.doughnutBoss].Center + Vector2.UnitX * ((commanderUsingLaser | commanderGoingLowOrHighInPhase1) ? 0f : distanceInFrontOfCommander) * (float)Main.npc[CalamityGlobalNPC.doughnutBoss].direction - base.NPC.Center;
			Vector2 desiredVelocity = distanceFromDestination.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * moveVelocity;
			if (((Vector2)(ref distanceFromDestination)).Length() > 40f)
			{
				float inertia = ((commanderUsingLaser | commanderGoingLowOrHighInPhase1) ? 10f : 15f);
				if (Main.getGoodWorld)
				{
					inertia *= 0.8f;
				}
				base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + desiredVelocity) / inertia;
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.9f;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation2 = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			if (!commanderGoingLowOrHighInPhase2)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= 0f - throwRocksGateValue)
			{
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 0.8f;
				if (Main.getGoodWorld)
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.5f;
				}
			}
			else
			{
				int moltenBlastsDivisor = 4;
				float shootMoltenBlastsGateValue = commanderGuardPhase2Duration / (float)moltenBlastsDivisor;
				if (base.NPC.ai[1] % shootMoltenBlastsGateValue == 0f && !commanderGoingLowOrHighInPhase2)
				{
					float moltenBlastVelocity = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f)));
					int projTimeLeft = (int)(2400f / moltenBlastVelocity);
					Vector2 velocity = Vector2.Normalize(player.Center - shootFrom) * moltenBlastVelocity;
					if (Main.netMode != 1)
					{
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, velocity, ModContent.ProjectileType<MoltenBlast>(), MoltenBlastDamage, 0f, Main.myPlayer, player.position.X, player.position.Y, 1f);
						Main.projectile[proj].timeLeft = projTimeLeft;
					}
					for (int n = 0; n < 50; n++)
					{
						int num = Main.rand.Next(6);
						int dustID = (((uint)num > 3u) ? 158 : 244);
						float num2 = Main.rand.NextFloat(moltenBlastVelocity * 0.5f, moltenBlastVelocity);
						float angleRandom = 0.06f;
						Vector2 dustVel = Utils.RotatedBy(new Vector2(num2, 0f), (double)velocity.ToRotation(), default(Vector2));
						dustVel = dustVel.RotatedBy(0f - angleRandom);
						dustVel = dustVel.RotatedByRandom(2f * angleRandom);
						float scale = Main.rand.NextFloat(1f, 2f);
						int idx = Dust.NewDust(shootFrom, 42, 42, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
						Main.dust[idx].noGravity = true;
					}
					base.NPC.velocity = -velocity * 0.5f;
				}
				Vector2 distanceFromDestination2 = Main.npc[CalamityGlobalNPC.doughnutBoss].Center + (commanderGoingLowOrHighInPhase2 ? Vector2.Zero : (Vector2.UnitX * distanceInFrontOfCommander * (float)Main.npc[CalamityGlobalNPC.doughnutBoss].direction)) - base.NPC.Center;
				Vector2 desiredVelocity2 = distanceFromDestination2.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * moveVelocity;
				if (((Vector2)(ref distanceFromDestination2)).Length() > 40f)
				{
					float inertia2 = (commanderGoingLowOrHighInPhase2 ? 8f : 15f);
					if (Main.getGoodWorld)
					{
						inertia2 *= 0.8f;
					}
					base.NPC.velocity = (base.NPC.velocity * (inertia2 - 1f) + desiredVelocity2) / inertia2;
				}
				else
				{
					NPC nPC5 = base.NPC;
					nPC5.velocity *= 0.9f;
				}
			}
			if (base.NPC.ai[1] >= 0f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.ai[0] = 2f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				Vector2 velocity2 = (player.Center - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f));
				velocity2 *= maxChargeVelocity;
				base.NPC.velocity = velocity2 * chargeVelocityMult;
				SoundEngine.PlaySound(in DashSound, base.NPC.Center);
				int totalDust2 = 36;
				for (int num3 = 0; num3 < totalDust2; num3++)
				{
					Vector2 val2 = (base.NPC.velocity.SafeNormalize(Vector2.UnitY) * new Vector2(160f, 160f)).RotatedBy((float)(num3 - (totalDust2 / 2 - 1)) * ((float)Math.PI * 2f) / (float)totalDust2) + shootFrom;
					Vector2 dustVelocity2 = val2 - shootFrom;
					int dust4 = Dust.NewDust(val2 + dustVelocity2, 0, 0, 244, dustVelocity2.X, dustVelocity2.Y);
					Main.dust[dust4].noGravity = true;
					Main.dust[dust4].noLight = true;
					Main.dust[dust4].scale = 3f;
					Main.dust[dust4].velocity = dustVelocity2 * 0.3f;
				}
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (Math.Sign(base.NPC.velocity.X) != 0)
			{
				base.NPC.spriteDirection = -Math.Sign(base.NPC.velocity.X);
			}
			base.NPC.spriteDirection = Math.Sign(base.NPC.velocity.X);
			base.NPC.ai[1]++;
			float phaseGateValue = (death ? 140f : (revenge ? 150f : (expertMode ? 160f : 180f)));
			if (base.NPC.ai[1] >= phaseGateValue)
			{
				base.NPC.ai[0] = 3f;
				float slowDownDurationAfterCharge = (revenge ? ((float)Main.rand.Next(30, 61)) : 60f);
				base.NPC.ai[1] = slowDownDurationAfterCharge;
				base.NPC.localAI[2] = 0f;
				NPC nPC6 = base.NPC;
				nPC6.velocity /= 2f;
				base.NPC.netUpdate = true;
			}
			else
			{
				Vector2 targetVector = (player.Center - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f));
				if (base.NPC.localAI[2] == 0f)
				{
					if (((Vector2)(ref base.NPC.velocity)).Length() < maxChargeVelocity)
					{
						float velocityMult = (death ? 1.036667f : (revenge ? 1.035f : (expertMode ? 1.033333f : 1.03f)));
						base.NPC.velocity = targetVector * (((Vector2)(ref base.NPC.velocity)).Length() * velocityMult);
						if (((Vector2)(ref base.NPC.velocity)).Length() > maxChargeVelocity)
						{
							base.NPC.localAI[2] = 1f;
							base.NPC.velocity = base.NPC.velocity.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * maxChargeVelocity;
						}
					}
				}
				else if (base.NPC.localAI[2] == 1f)
				{
					float inertia3 = (death ? 63f : (revenge ? 66f : (expertMode ? 69f : 75f)));
					base.NPC.velocity = (base.NPC.velocity * (inertia3 - 1f) + targetVector * (((Vector2)(ref base.NPC.velocity)).Length() + 0.11111112f * inertia3)) / inertia3;
					if (base.NPC.Distance(player.Center) < 160f * base.NPC.scale)
					{
						base.NPC.localAI[2] = 2f;
					}
				}
				else if (base.NPC.Distance(player.Center) >= 240f * base.NPC.scale || base.NPC.localAI[2] == 3f)
				{
					if (base.NPC.localAI[2] != 3f)
					{
						base.NPC.localAI[2] = 3f;
					}
					NPC nPC7 = base.NPC;
					nPC7.velocity *= 0.98f;
				}
				int projectileGateValue = (int)(phaseGateValue * 0.4f);
				if (base.NPC.ai[1] % (float)projectileGateValue == 0f && Main.netMode != 1)
				{
					float projectileVelocityY2 = base.NPC.velocity.Y;
					if (projectileVelocityY2 < 0f)
					{
						projectileVelocityY2 = 0f;
					}
					projectileVelocityY2 += (expertMode ? 4f : 3f);
					Vector2 projectileVelocity2 = default(Vector2);
					((Vector2)(ref projectileVelocity2))._002Ector(base.NPC.velocity.X * 0.25f, projectileVelocityY2);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, projectileVelocity2, ModContent.ProjectileType<HolyBomb>(), FireSentryDamage, 0f, Main.myPlayer);
				}
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			if (Math.Sign(base.NPC.velocity.X) != 0)
			{
				base.NPC.spriteDirection = -Math.Sign(base.NPC.velocity.X);
			}
			base.NPC.spriteDirection = Math.Sign(base.NPC.velocity.X);
			base.NPC.ai[1]--;
			if (base.NPC.ai[1] <= 0f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[2]++;
				float totalCharges = (revenge ? ((float)Main.rand.Next(2) + 1f) : 2f);
				bool dontCharge = base.NPC.ai[2] >= totalCharges;
				base.NPC.ai[1] = (dontCharge ? (0f - commanderGuardPhase2Duration) : 0f);
				if (dontCharge)
				{
					base.NPC.ai[2] = 0f;
				}
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
			NPC nPC8 = base.NPC;
			nPC8.velocity *= 0.97f;
		}
		if (Main.zenithWorld)
		{
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation3 = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			Vector2 center = Main.npc[CalamityGlobalNPC.doughnutBoss].Center;
			Vector2 playerPos = player.Center;
			Vector2 midPoint = (center - playerPos) / 1.25f + playerPos;
			base.NPC.position = midPoint;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		float useLaserGateValue = 120f;
		float num = ((CalamityWorld.revenge || BossRushEvent.BossRushActive) ? 235f : 315f);
		float maxIntensity = 45f;
		float increaseIntensityGateValue = useLaserGateValue - maxIntensity;
		float decreaseIntensityGateValue = num - maxIntensity;
		if (!base.NPC.IsABestiaryIconDummy)
		{
			if (Main.npc[CalamityGlobalNPC.doughnutBoss].ai[0] == 5f)
			{
				_ = Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1];
				float burnIntensity = ((Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1] > decreaseIntensityGateValue) ? Utils.GetLerpValue(0f, maxIntensity, maxIntensity - (Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1] - decreaseIntensityGateValue), clamped: true) : Utils.GetLerpValue(0f, maxIntensity, Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1], clamped: true));
				int totalGuardiansToDraw = (int)MathHelper.Lerp(1f, 30f, burnIntensity);
				for (int i = 0; i < totalGuardiansToDraw; i++)
				{
					float num2 = (float)Math.PI * 2f * (float)i * 2f / (float)totalGuardiansToDraw;
					float drawOffsetFactor = (float)Math.Sin(num2 * 6f + Main.GlobalTimeWrappedHourly * (float)Math.PI);
					drawOffsetFactor *= (float)Math.Pow(burnIntensity, 3.0) * 50f;
					Vector2 drawOffset = num2.ToRotationVector2() * drawOffsetFactor;
					Color baseColor = Color.White * (MathHelper.Lerp(0.4f, 0.8f, burnIntensity) / (float)totalGuardiansToDraw * 1.5f);
					((Color)(ref baseColor)).A = 0;
					baseColor = Color.Lerp(Color.White, baseColor, burnIntensity);
					drawGuardianInstance(drawOffset, (totalGuardiansToDraw == 1) ? ((Color?)null) : new Color?(baseColor));
				}
			}
			else
			{
				drawGuardianInstance(Vector2.Zero, null);
			}
		}
		else
		{
			drawGuardianInstance(Vector2.Zero, null);
		}
		if (base.NPC.IsABestiaryIconDummy)
		{
			return false;
		}
		if (base.NPC.localAI[1] < 120f)
		{
			float maxOscillation = 60f;
			float minScale = 0.8f;
			float maxPulseScale = 1f - minScale;
			float minOpacity = 0.5f;
			float maxOpacityScale = 1f - minOpacity;
			float currentOscillation = MathHelper.Lerp(0f, maxOscillation, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI) + 1f) * 0.5f);
			float shieldOpacity = minOpacity + maxOpacityScale * Utils.Remap(currentOscillation, 0f, maxOscillation, 1f, 0f);
			float oscillationRatio = currentOscillation / maxOscillation;
			float invertedOscillationRatio = 1f - (1f - oscillationRatio) * (1f - oscillationRatio);
			float oscillationScale = 1f - (1f - invertedOscillationRatio) * (1f - invertedOscillationRatio);
			float num3 = Utils.Remap(currentOscillation, maxOscillation - 15f, maxOscillation, 0f, 1f);
			float twoOscillationsMultipliedTogetherForScaleCalculation = num3 * num3;
			float invertedOscillationUsedForScale = MathHelper.Lerp(minScale, 1f, 1f - twoOscillationsMultipliedTogetherForScaleCalculation);
			float shieldScale = (minScale + maxPulseScale * oscillationScale) * invertedOscillationUsedForScale;
			float smallerRemappedOscillation = Utils.Remap(currentOscillation, 20f, maxOscillation, 0f, 1f);
			float invertedSmallerOscillationRatio = 1f - (1f - smallerRemappedOscillation) * (1f - smallerRemappedOscillation);
			float smallerOscillationScale = 1f - (1f - invertedSmallerOscillationRatio) * (1f - invertedSmallerOscillationRatio);
			float shieldScale2 = (minScale + maxPulseScale * smallerOscillationScale) * invertedOscillationUsedForScale;
			Texture2D shieldTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleOpenCircle", (AssetRequestMode)2).Value;
			Rectangle shieldFrame = shieldTexture.Frame();
			Vector2 origin = shieldFrame.Size() * 0.5f;
			Vector2 shieldDrawPos = base.NPC.Center - screenPos;
			shieldDrawPos -= new Vector2((float)shieldTexture.Width, (float)shieldTexture.Height) * base.NPC.scale / 2f;
			shieldDrawPos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
			float minHue = 0.06f;
			float maxHue = minHue + 0.12f;
			float opacityScaleDuringShieldDespawn = (120f - base.NPC.localAI[1]) / 120f;
			float scaleDuringShieldDespawnScale = 1.8f;
			float scaleDuringShieldDespawn = (1f - opacityScaleDuringShieldDespawn) * scaleDuringShieldDespawnScale;
			float colorScale = MathHelper.Lerp(0f, shieldOpacity, opacityScaleDuringShieldDespawn);
			Color color = Main.hslToRgb(MathHelper.Lerp(maxHue - minHue, maxHue, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f)) + 1f) * 0.5f), 1f, 0.5f) * colorScale;
			Color color2 = Main.hslToRgb(MathHelper.Lerp(minHue, maxHue - minHue, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI * 3f) + 1f) * 0.5f), 1f, 0.5f) * colorScale;
			((Color)(ref color2)).A = 0;
			color *= 0.6f;
			color2 *= 0.6f;
			float scaleMult = 1.2f + scaleDuringShieldDespawn;
			float noiseScale = MathHelper.Lerp(0.4f, 0.8f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.3f) * 0.5f + 0.5f);
			Effect shieldEffect = Terraria.Graphics.Effects.Filters.Scene["CalamityMod:RoverDriveShield"].GetShader().Shader;
			shieldEffect.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.058f);
			shieldEffect.Parameters["blowUpPower"].SetValue(2.8f);
			shieldEffect.Parameters["blowUpSize"].SetValue(0.4f);
			shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
			shieldEffect.Parameters["shieldOpacity"].SetValue(opacityScaleDuringShieldDespawn);
			shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
			Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, color, color2);
			shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref color)).ToVector3());
			shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
			Matrix matrix = Main.GameViewMatrix.TransformationMatrix;
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color, base.NPC.rotation, origin, shieldScale2 * scaleMult, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color2, base.NPC.rotation, origin, shieldScale2 * scaleMult * 0.95f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color, base.NPC.rotation, origin, shieldScale * scaleMult, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color2, base.NPC.rotation, origin, shieldScale * scaleMult * 0.95f, (SpriteEffects)0, 0f);
			using (Main.spriteBatch.Scope())
			{
				Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, shieldEffect, matrix);
				Texture2D heatTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons2", (AssetRequestMode)2).Value;
				_ = base.NPC.Center + base.NPC.gfxOffY * Vector2.UnitY - Main.screenPosition;
				Main.spriteBatch.Draw(heatTex, shieldDrawPos, (Rectangle?)null, Color.White, 0f, heatTex.Size() / 2f, shieldScale * scaleMult * 0.5f, (SpriteEffects)0, 0f);
				Main.spriteBatch.End();
			}
		}
		return false;
		void drawGuardianInstance(Vector2 val, Color? colorOverride)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_0257: Unknown result type (might be due to invalid IL or missing references)
			//IL_025c: Unknown result type (might be due to invalid IL or missing references)
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0260: Unknown result type (might be due to invalid IL or missing references)
			//IL_026c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0281: Unknown result type (might be due to invalid IL or missing references)
			//IL_0286: Unknown result type (might be due to invalid IL or missing references)
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0291: Unknown result type (might be due to invalid IL or missing references)
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_029f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0304: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0313: Unknown result type (might be due to invalid IL or missing references)
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_032c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0331: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0343: Unknown result type (might be due to invalid IL or missing references)
			//IL_04da: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0502: Unknown result type (might be due to invalid IL or missing references)
			//IL_050a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0517: Unknown result type (might be due to invalid IL or missing references)
			//IL_0527: Unknown result type (might be due to invalid IL or missing references)
			//IL_0533: Unknown result type (might be due to invalid IL or missing references)
			//IL_035c: Unknown result type (might be due to invalid IL or missing references)
			//IL_035e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0360: Unknown result type (might be due to invalid IL or missing references)
			//IL_0362: Unknown result type (might be due to invalid IL or missing references)
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0371: Unknown result type (might be due to invalid IL or missing references)
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_037b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0380: Unknown result type (might be due to invalid IL or missing references)
			//IL_0382: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Unknown result type (might be due to invalid IL or missing references)
			//IL_0395: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_0421: Unknown result type (might be due to invalid IL or missing references)
			//IL_042b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Unknown result type (might be due to invalid IL or missing references)
			//IL_0435: Unknown result type (might be due to invalid IL or missing references)
			//IL_0437: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Unknown result type (might be due to invalid IL or missing references)
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_045f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0464: Unknown result type (might be due to invalid IL or missing references)
			//IL_0465: Unknown result type (might be due to invalid IL or missing references)
			//IL_046a: Unknown result type (might be due to invalid IL or missing references)
			//IL_046f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0478: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0497: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
			Vector2 drawPos = base.NPC.Center - screenPos;
			Vector2 halfSizeTexture = default(Vector2);
			((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
			int afterimageAmt = 5;
			if (base.NPC.ai[0] == 2f)
			{
				afterimageAmt = 10;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int j = 1; j < afterimageAmt; j += 2)
				{
					Color afterimageColor = drawColor;
					afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
					afterimageColor = base.NPC.GetAlpha(afterimageColor);
					afterimageColor *= (float)(afterimageAmt - j) / 15f;
					if (colorOverride.HasValue)
					{
						afterimageColor = colorOverride.Value;
					}
					Vector2 afterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			Vector2 drawLocation = drawPos;
			drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
			drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, (Color)(((_003F?)colorOverride) ?? base.NPC.GetAlpha(drawColor)), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			texture2D15 = Texture_Glow.Value;
			Color timeBasedGlowColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
			if (Main.zenithWorld)
			{
				texture2D15 = TextureNight_Glow.Value;
				timeBasedGlowColor = Main.DiscoColor;
			}
			if (colorOverride.HasValue)
			{
				timeBasedGlowColor = colorOverride.Value;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int k = 1; k < afterimageAmt; k++)
				{
					Color timeBasedAfterimageColor = timeBasedGlowColor;
					timeBasedAfterimageColor = Color.Lerp(timeBasedAfterimageColor, Color.White, 0.5f);
					timeBasedAfterimageColor = base.NPC.GetAlpha(timeBasedAfterimageColor);
					timeBasedAfterimageColor *= (float)(afterimageAmt - k) / 15f;
					if (colorOverride.HasValue)
					{
						timeBasedAfterimageColor = colorOverride.Value;
					}
					Vector2 timeBasedAfterimagePos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					timeBasedAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					timeBasedAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture2D15, timeBasedAfterimagePos, (Rectangle?)base.NPC.frame, timeBasedAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			base.NPC.DrawBackglow((Color)(Main.zenithWorld ? Main.DiscoColor : new Color(255, 64, 0, 0)), 4f, spriteEffects, base.NPC.frame, Main.screenPosition, texture2D15);
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, ProvUtils.GetColorBasedOnEnrage(Night: false, 0), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<RelicOfResilience>(), 4);
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
		return minDist <= 80f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (Main.zenithWorld && !projectile.minion)
		{
			if (projectile.penetrate <= -1 || projectile.penetrate > 5)
			{
				modifiers.SourceDamage *= 2.5f;
			}
			else
			{
				modifiers.SourceDamage *= (float)projectile.penetrate / 2f;
			}
			projectile.active = false;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossT").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossT2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossT3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossT4").Type);
			}
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
		}
	}
}
