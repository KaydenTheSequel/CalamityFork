using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.LoreItems;
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
using CalamityMod.Particles;
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

namespace CalamityMod.NPCs.Perforator;

[AutoloadBossHead]
public class PerforatorHive : ModNPC
{
	public static readonly SoundStyle GeyserShoot = new SoundStyle("CalamityMod/Sounds/Custom/Perforator/PerfHiveShoot", 3);

	public static readonly SoundStyle IchorShoot = new SoundStyle("CalamityMod/Sounds/Custom/Perforator/PerfHiveIchorShoot");

	public static readonly SoundStyle WormSpawn = new SoundStyle("CalamityMod/Sounds/Custom/Perforator/PerfHiveWormSpawn");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfHiveHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfHiveDeath");

	public static Asset<Texture2D> GlowTexture;

	private const int Width = 110;

	private const int Height = 100;

	private bool smallSpawned;

	private bool mediumSpawned;

	private bool largeSpawned;

	private int wormsAlive;

	private float addedStretch;

	private int squashTimer;

	private const int squashInterval = 24;

	private const float maxSquash = 0.3f;

	private float wormSpawnStateTimer;

	public static int BloodGeyserDamage = 12;

	public static int IchorShotDamage = 12;

	public static int IchorBlobDamage = 12;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 10;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
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
		base.NPC.npcSlots = 18f;
		base.NPC.damage = 30;
		base.NPC.width = 110;
		base.NPC.height = 100;
		base.NPC.defense = 4;
		base.NPC.LifeMaxNERB(4000, 5750, 270000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 5);
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PerforatorHive")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(wormsAlive);
		writer.Write(smallSpawned);
		writer.Write(mediumSpawned);
		writer.Write(largeSpawned);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(wormSpawnStateTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		wormsAlive = reader.ReadInt32();
		smallSpawned = reader.ReadBoolean();
		mediumSpawned = reader.ReadBoolean();
		largeSpawned = reader.ReadBoolean();
		base.NPC.localAI[2] = reader.ReadSingle();
		wormSpawnStateTimer = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1141: Unknown result type (might be due to invalid IL or missing references)
		//IL_114c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_117d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b21: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11db: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_122d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1234: Unknown result type (might be due to invalid IL or missing references)
		//IL_1239: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Unknown result type (might be due to invalid IL or missing references)
		//IL_128d: Unknown result type (might be due to invalid IL or missing references)
		//IL_128f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1294: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12de: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_130b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_131a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1320: Unknown result type (might be due to invalid IL or missing references)
		//IL_1322: Unknown result type (might be due to invalid IL or missing references)
		//IL_1327: Unknown result type (might be due to invalid IL or missing references)
		//IL_1329: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_1337: Unknown result type (might be due to invalid IL or missing references)
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_133e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1340: Unknown result type (might be due to invalid IL or missing references)
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_142c: Unknown result type (might be due to invalid IL or missing references)
		//IL_142e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1445: Unknown result type (might be due to invalid IL or missing references)
		//IL_1359: Unknown result type (might be due to invalid IL or missing references)
		//IL_135b: Unknown result type (might be due to invalid IL or missing references)
		//IL_135d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1362: Unknown result type (might be due to invalid IL or missing references)
		//IL_136a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1384: Unknown result type (might be due to invalid IL or missing references)
		//IL_138a: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1022: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Unknown result type (might be due to invalid IL or missing references)
		//IL_1031: Unknown result type (might be due to invalid IL or missing references)
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_103b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1055: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.perfHive = base.NPC.whoAmI;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float blobPhaseGateValue = 600f;
		bool floatAboveToFireBlobs = base.NPC.ai[2] >= blobPhaseGateValue - 120f;
		Player player = Main.player[base.NPC.target];
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < 0.7f;
		bool spawnSmall = (Main.getGoodWorld ? (lifeRatio < 0.85f) : (lifeRatio < 0.75f));
		bool spawnMedium = (Main.getGoodWorld ? (lifeRatio < 0.6f) : (lifeRatio < 0.5f));
		bool spawnLarge = (Main.getGoodWorld ? (lifeRatio < 0.45f) : (lifeRatio < 0.25f));
		if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f || (!player.ZoneCrimson && !BossRushEvent.BossRushActive))
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f || (!player.ZoneCrimson && !BossRushEvent.BossRushActive))
			{
				base.NPC.rotation = base.NPC.velocity.X * 0.04f;
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
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.localAI[1] >= 6f)
		{
			int type = (Main.rand.NextBool() ? ModContent.ProjectileType<IchorShot>() : ModContent.ProjectileType<BloodGeyser>());
			int damage = ((type == ModContent.ProjectileType<IchorShot>()) ? IchorShotDamage : BloodGeyserDamage);
			int spread = Main.rand.Next(-45, 46);
			Vector2 baseVelocity = Vector2.UnitY * Main.rand.NextFloat(-12.5f, -5f);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, baseVelocity.RotatedBy(MathHelper.ToRadians((float)spread)), type, damage, 0f, Main.myPlayer, 0f, player.Center.Y);
			if (Main.netMode != 1)
			{
				int healAmt = base.NPC.lifeMax / 1000;
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
			base.NPC.localAI[1] = 0f;
		}
		bool largeWormAlive = NPC.AnyNPCs(ModContent.NPCType<PerforatorHeadLarge>());
		bool mediumWormAlive = NPC.AnyNPCs(ModContent.NPCType<PerforatorHeadMedium>());
		bool smallWormAlive = NPC.AnyNPCs(ModContent.NPCType<PerforatorHeadSmall>());
		if (largeWormAlive & mediumWormAlive & smallWormAlive)
		{
			wormsAlive = 3;
		}
		else if ((largeWormAlive & mediumWormAlive) || (largeWormAlive & smallWormAlive) || (mediumWormAlive & smallWormAlive))
		{
			wormsAlive = 2;
		}
		else if (largeWormAlive | mediumWormAlive | smallWormAlive)
		{
			wormsAlive = 1;
		}
		else
		{
			wormsAlive = 0;
		}
		base.NPC.Calamity().DR = (float)wormsAlive * 0.5f;
		if (wormsAlive >= 1)
		{
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = true;
		}
		if (base.NPC.Calamity().DR >= 0.999f)
		{
			base.NPC.Calamity().DR = 0.999f;
			base.NPC.Calamity().unbreakableDR = true;
		}
		if (Main.netMode != 1 && wormSpawnStateTimer == 0f && ((!smallSpawned & spawnSmall) || (!mediumSpawned & spawnMedium) || (!largeSpawned & spawnLarge)))
		{
			wormSpawnStateTimer = 1f;
			base.NPC.netUpdate = true;
		}
		if (wormSpawnStateTimer > 0f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.94f;
			base.NPC.rotation = base.NPC.velocity.X * 0.04f;
			base.NPC.damage = 0;
			wormSpawnStateTimer++;
			int slowDownDuration = 20;
			int waitBeforeSpawnDuration = 40;
			int totalStateDuration = slowDownDuration + waitBeforeSpawnDuration;
			if (wormSpawnStateTimer >= (float)slowDownDuration && wormSpawnStateTimer < (float)totalStateDuration)
			{
				if (Main.rand.NextBool(7))
				{
					int bloodLifetime = Main.rand.Next(20, 45);
					float bloodScale = Main.rand.NextFloat(0.5f, 1f);
					Color bloodColor = Color.Lerp(Color.Yellow, Color.DarkRed, Main.rand.NextFloat(0.7f));
					float randomSpeedMultiplier = Main.rand.NextFloat(0.8f, 1.6f);
					Vector2 bloodVelocity = Main.rand.NextVector2Unit(5f) * 1.5f * randomSpeedMultiplier;
					bloodVelocity.Y -= 8f;
					Vector2 randomOffset = Main.rand.NextVector2Unit() * Main.rand.NextFloat(25f, 50f);
					GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center + randomOffset, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
				}
			}
			else
			{
				if (!(wormSpawnStateTimer >= (float)totalStateDuration))
				{
					return;
				}
				Color bloodColor2 = Color.Lerp(Color.Crimson, Color.DarkRed, Main.rand.NextFloat(0.9f));
				int bloodAmt = 0;
				float minScale = 1f;
				float maxScale = 1.8f;
				int wormType = -1;
				if (!smallSpawned)
				{
					smallSpawned = true;
					wormType = ModContent.NPCType<PerforatorHeadSmall>();
					bloodAmt = 12;
				}
				else if (!mediumSpawned & spawnMedium)
				{
					mediumSpawned = true;
					wormType = ModContent.NPCType<PerforatorHeadMedium>();
					bloodColor2 = Color.Lerp(Color.Yellow, Color.Orange, Main.rand.NextFloat(0.9f));
					bloodAmt = 12;
					minScale = 1.5f;
					maxScale = 2.4f;
				}
				else if (!largeSpawned & spawnLarge)
				{
					largeSpawned = true;
					wormType = ModContent.NPCType<PerforatorHeadLarge>();
					bloodAmt = 18;
					minScale = 1.4f;
					maxScale = 2.2f;
				}
				for (int i = 0; i < bloodAmt; i++)
				{
					int bloodLifetime2 = Main.rand.Next(80, 140);
					float bloodScale2 = Main.rand.NextFloat(minScale, maxScale);
					float randomSpeedMultiplier2 = Main.rand.NextFloat(1.4f, 2.2f);
					Vector2 bloodVelocity2 = Main.rand.NextVector2Unit() * 4f * randomSpeedMultiplier2;
					bloodVelocity2.Y -= 5f;
					GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center, bloodVelocity2, bloodLifetime2, bloodScale2, bloodColor2));
				}
				if (Main.getGoodWorld && lifeRatio < 0.5f && Main.netMode != 1)
				{
					if (lifeRatio > 0.35f)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-25, 26), (int)base.NPC.Center.Y + Main.rand.Next(-25, 26), ModContent.NPCType<PerforatorHeadLarge>(), 1);
					}
					else if (lifeRatio > 0.2f)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-25, 26), (int)base.NPC.Center.Y + Main.rand.Next(-25, 26), ModContent.NPCType<PerforatorHeadMedium>(), 1);
					}
					else if (lifeRatio > 0.05f)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-25, 26), (int)base.NPC.Center.Y + Main.rand.Next(-25, 26), ModContent.NPCType<PerforatorHeadSmall>(), 1);
					}
				}
				if (wormType != -1)
				{
					squashTimer = 24;
					if (Main.netMode != 1)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-25, 26), (int)base.NPC.Center.Y + Main.rand.Next(-25, 26), wormType, 1);
						if (death && wormType == ModContent.NPCType<PerforatorHeadSmall>())
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + Main.rand.Next(-25, 26), (int)base.NPC.Center.Y + Main.rand.Next(-25, 26), wormType, 1);
						}
					}
				}
				base.NPC.TargetClosest();
				SoundEngine.PlaySound(in WormSpawn, base.NPC.Center);
				wormSpawnStateTimer = 0f;
				base.NPC.netUpdate = true;
			}
			return;
		}
		if (squashTimer > 0)
		{
			squashTimer--;
			addedStretch = MathHelper.Lerp(0f, 0.3f, (float)squashTimer / 24f);
		}
		else
		{
			addedStretch = 0f;
		}
		if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
		{
			float playerLocation = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
		}
		base.NPC.rotation = base.NPC.velocity.X * 0.04f;
		if (phase2 && (((wormsAlive == 0 || largeSpawned) | floatAboveToFireBlobs) || Main.getGoodWorld))
		{
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= blobPhaseGateValue)
			{
				if (base.NPC.ai[2] < blobPhaseGateValue + 300f)
				{
					if (((Vector2)(ref base.NPC.velocity)).Length() > 0.5f)
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.96f;
					}
					else
					{
						base.NPC.ai[2] = blobPhaseGateValue + 300f;
					}
				}
				if (base.NPC.ai[2] < blobPhaseGateValue + 180f)
				{
					if (Main.rand.NextBool(4))
					{
						int bloodLifetime3 = Main.rand.Next(25, 35);
						float bloodScale3 = Main.rand.NextFloat(0.6f, 0.95f);
						Color bloodColor3 = Color.Lerp(Color.Yellow, Color.Orange, Main.rand.NextFloat(0.8f));
						float randomSpeedMultiplier3 = Main.rand.NextFloat(1.5f, 2.5f);
						Vector2 bloodVelocity3 = Main.rand.NextVector2Unit() * randomSpeedMultiplier3;
						Vector2 spawnPosition = base.NPC.Center;
						spawnPosition.Y += 42f;
						GeneralParticleHandler.SpawnParticle(new BloodParticle(spawnPosition, bloodVelocity3, bloodLifetime3, bloodScale3, bloodColor3));
					}
					return;
				}
				base.NPC.ai[2] = 0f;
				SoundEngine.PlaySound(in IchorShoot, base.NPC.Center);
				for (int j = 0; j < 32; j++)
				{
					int ichorDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 170);
					float dustVelocityYAdd = Math.Abs(Main.dust[ichorDust].velocity.Y) * 0.5f;
					if (Main.dust[ichorDust].velocity.Y < 0f)
					{
						Main.dust[ichorDust].velocity.Y = 2f + dustVelocityYAdd;
					}
					if (Main.rand.NextBool())
					{
						Main.dust[ichorDust].scale = 0.5f;
					}
				}
				bool ichorBlobBigWormPhase = wormsAlive > 0 && largeSpawned;
				int numBlobs = ((!expertMode) ? (ichorBlobBigWormPhase ? 2 : 4) : (ichorBlobBigWormPhase ? 4 : 6));
				if (Main.getGoodWorld)
				{
					numBlobs *= 2;
				}
				int type2 = ModContent.ProjectileType<IchorBlob>();
				int blobSpread = ((!expertMode) ? (ichorBlobBigWormPhase ? 33 : 66) : (ichorBlobBigWormPhase ? 66 : 100));
				Vector2 blobVelocity = default(Vector2);
				for (int k = 0; k < numBlobs; k++)
				{
					((Vector2)(ref blobVelocity))._002Ector((float)Main.rand.Next(-blobSpread, blobSpread + 1), (float)Main.rand.Next(-blobSpread, blobSpread + 1));
					((Vector2)(ref blobVelocity)).Normalize();
					blobVelocity *= (float)Main.rand.Next(400, 801) * 0.01f;
					if (Main.getGoodWorld)
					{
						blobVelocity *= Main.rand.NextFloat() + 1f;
					}
					float blobVelocityYAdd = Math.Abs(blobVelocity.Y) * 0.25f;
					if (blobVelocity.Y < 2f)
					{
						blobVelocity.Y = 2f + blobVelocityYAdd;
					}
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitY * 50f, blobVelocity, type2, IchorBlobDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
				}
				return;
			}
		}
		if (floatAboveToFireBlobs)
		{
			base.NPC.damage = 0;
			if (revenge)
			{
				Movement(player, 9f, 0.3f, 450f);
			}
			else
			{
				Movement(player, 6f, 0.2f, 450f);
			}
			return;
		}
		if (Main.netMode != 1)
		{
			base.NPC.localAI[0]++;
			if (base.NPC.localAI[0] >= (revenge ? 200f : 250f) + (float)wormsAlive * 150f && base.NPC.position.Y + (float)base.NPC.height < player.position.Y && Vector2.Distance(player.Center, base.NPC.Center) > 80f)
			{
				base.NPC.localAI[0] = 0f;
				SoundEngine.PlaySound(in GeyserShoot, base.NPC.Center);
				int numProj = (death ? 16 : (revenge ? 14 : (expertMode ? 12 : 10)));
				if (Main.getGoodWorld)
				{
					numProj *= 2;
				}
				float velocity = 8f;
				Vector2 projectileVelocity = default(Vector2);
				((Vector2)(ref projectileVelocity))._002Ector(Utils.DirectionTo(Target: (wormsAlive > 0) ? player.Center : (base.NPC.Center - Vector2.UnitY * 100f), Origin: base.NPC.Center).X * velocity, 0f - velocity);
				float rotation = MathHelper.ToRadians(75f);
				Vector2 dustSpawnBox = default(Vector2);
				((Vector2)(ref dustSpawnBox))._002Ector(12f, 12f);
				Vector2 dustSpawnOffset = dustSpawnBox * 0.5f;
				Vector2 randomVelocity = default(Vector2);
				for (int l = 0; l < numProj; l++)
				{
					bool num = Main.rand.NextBool();
					int type3 = (num ? ModContent.ProjectileType<IchorShot>() : ModContent.ProjectileType<BloodGeyser>());
					int damage2 = (num ? IchorShotDamage : BloodGeyserDamage);
					Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)l / (float)(numProj - 1)));
					((Vector2)(ref randomVelocity))._002Ector(Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() - 0.5f);
					Vector2 projectileSpawnLocation = base.NPC.Center + Vector2.Normalize(perturbedSpeed) * 50f;
					Vector2 projectileVelocityRandomized = perturbedSpeed + randomVelocity;
					float num2 = Main.rand.NextFloat(3f, 9f);
					float angleRandom = 0.05f;
					Vector2 dustVelocity = Utils.RotatedBy(new Vector2(num2, 0f), (double)projectileVelocityRandomized.ToRotation(), default(Vector2));
					dustVelocity = dustVelocity.RotatedBy(0f - angleRandom);
					dustVelocity = dustVelocity.RotatedByRandom(2f * angleRandom);
					if (num)
					{
						for (int m = 0; m < 4; m++)
						{
							int ichorDust2 = Dust.NewDust(projectileSpawnLocation - dustSpawnOffset, (int)dustSpawnBox.X, (int)dustSpawnBox.Y, 170);
							Main.dust[ichorDust2].velocity = dustVelocity;
						}
					}
					else
					{
						for (int n = 0; n < 4; n++)
						{
							int bloodDust = Dust.NewDust(projectileSpawnLocation - dustSpawnOffset, (int)dustSpawnBox.X, (int)dustSpawnBox.Y, 5);
							Main.dust[bloodDust].velocity = dustVelocity;
							Main.dust[bloodDust].scale = 2f;
						}
					}
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawnLocation, projectileVelocityRandomized, type3, damage2, 0f, Main.myPlayer, 0f, player.Center.Y);
				}
			}
		}
		if (revenge)
		{
			switch (wormsAlive)
			{
			case 0:
				base.NPC.damage = base.NPC.defDamage;
				if (largeSpawned | death)
				{
					Movement(player, 13f, death ? 0.115f : 0.1f, 20f);
				}
				else if (mediumSpawned)
				{
					Movement(player, 12f, death ? 0.11f : 0.095f, 30f);
				}
				else if (smallSpawned)
				{
					Movement(player, 11f, death ? 0.105f : 0.09f, 40f);
				}
				else
				{
					Movement(player, 10f, death ? 0.1f : 0.085f, 50f);
				}
				break;
			case 1:
				base.NPC.damage = 0;
				Movement(player, 8f, 0.2f, 350f);
				break;
			case 2:
				base.NPC.damage = 0;
				Movement(player, 8f, 0.2f, 275f);
				break;
			case 3:
				base.NPC.damage = 0;
				Movement(player, 8f, 0.2f, 200f);
				break;
			}
		}
		else
		{
			base.NPC.damage = 0;
			Movement(player, 6f, 0.15f, 350f);
		}
	}

	private void Movement(Player target, float velocity, float acceleration, float y)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		float movementDistanceGateValue = 100f;
		Vector2 distanceFromDestination = new Vector2(target.Center.X, target.Center.Y - y) - base.NPC.Center;
		CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, velocity, acceleration, useSimpleFlyMovement: true);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.NPC.spriteDirection == 1);
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Color drawColorAlpha = base.NPC.GetAlpha(drawColor);
		Vector2 scaleStretch = new Vector2(1f - addedStretch, 1f + addedStretch) * base.NPC.scale;
		float yOffset = addedStretch * 0.5f * (float)base.NPC.height;
		spriteBatch.Draw(texture, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY - yOffset), (Rectangle?)base.NPC.frame, drawColorAlpha, base.NPC.rotation, base.NPC.frame.Size() * 0.5f, scaleStretch, spriteEffects, 0f);
		texture = GlowTexture.Value;
		Color glowmaskColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
		spriteBatch.Draw(texture, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY - yOffset), (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, base.NPC.frame.Size() * 0.5f, scaleStretch, spriteEffects, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.8f);
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 188;
	}

	public override void OnKill()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			if (!DownedBossSystem.downedHiveMind && !DownedBossSystem.downedPerforator)
			{
				Color messageColor = Color.Cyan;
				AerialiteOreGen.Enchant();
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.SkyOreText", messageColor);
			}
			DownedBossSystem.downedPerforator = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PerforatorBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, new WeightedItemStack[7]
		{
			ModContent.ItemType<VeinBurster>(),
			ModContent.ItemType<SausageMaker>(),
			ModContent.ItemType<Aorta>(),
			ModContent.ItemType<Eviscerator>(),
			ModContent.ItemType<BloodBath>(),
			ModContent.ItemType<FleshOfInfidelity>(),
			ModContent.ItemType<ToothBall>()
		}));
		normalOnly.Add(1257, 1, 10, 15);
		normalOnly.Add(1330, 1, 10, 15);
		normalOnly.Add(2171, 1, 10, 15);
		normalOnly.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(), 1332, 1, 10, 20));
		normalOnly.Add(ModContent.ItemType<BloodstainedGlove>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<PerforatorMask>(), 7);
		normalOnly.Add(ModContent.ItemType<BloodyVein>(), 10);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<PerforatorTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<PerforatorsRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<Bloodfin>(), 1, 1, 9999), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedPerforator, ModContent.ItemType<LorePerforators>(), ui: true, DropHelper.FirstKillText);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; (double)k < (double)(hit.Damage / base.NPC.lifeMax) * 100.0; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Hive").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Hive2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Hive3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Hive4").Type);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 12; i++)
		{
			int ichorDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[ichorDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[ichorDust].scale = 0.5f;
				Main.dust[ichorDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 20; j++)
		{
			int bloodDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
			Main.dust[bloodDust].noGravity = true;
			Dust obj2 = Main.dust[bloodDust];
			obj2.velocity *= 5f;
			bloodDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[bloodDust];
			obj3.velocity *= 2f;
		}
		for (int l = 0; l < 24; l++)
		{
			int bloodLifetime = Main.rand.Next(120, 200);
			float bloodScale = Main.rand.NextFloat(1.3f, 2.6f);
			Color bloodColor = Color.Lerp(Color.Crimson, Color.DarkRed, Main.rand.NextFloat(0.9f));
			float randomSpeedMultiplier = Main.rand.NextFloat(4.5f, 9f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit(6f) * 2.5f * randomSpeedMultiplier;
			bloodVelocity.Y -= 14f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
	}
}
