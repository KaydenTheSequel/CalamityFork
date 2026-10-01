using System;
using System.IO;
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
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DesertScourge;

[AutoloadBossHead]
[HasPierceResist(false)]
[LongDistanceNetSync]
public class DesertScourgeHead : ModNPC
{
	private int biomeEnrageTimer = 300;

	private bool tailSpawned;

	public bool playRoarSound;

	public const float SegmentVelocity_Normal = 10f;

	public const float SegmentVelocity_Expert = 12.5f;

	public const float SegmentVelocity_Death = 15f;

	public const float SegmentVelocity_GoodWorld = 21f;

	public const float SegmentVelocity_ZenithSeed = 24f;

	public const float SpitGateValue = 300f;

	public const float SpitGateValue_Death = 180f;

	public const float BurrowTimeGateValue = 600f;

	public const float BurrowResetTimeGateValue = 1200f;

	public const float LungeUpwardDistanceOffset = 600f;

	public const float LungeUpwardCutoffDistance = 420f;

	public const float BurrowDistance_Hide = 1080f;

	public const float BurrowDistance = 800f;

	public const float OpenMouthForBiteDistance = 220f;

	private const int OpenMouthStopFrame = 4;

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/DesertScourgeHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/DesertScourgeDeath");

	public static readonly SoundStyle RoarSound = new SoundStyle("CalamityMod/Sounds/Custom/DesertScourge/DesertScourgeRoar");

	public static readonly SoundStyle SandBlastSound = new SoundStyle("CalamityMod/Sounds/Custom/DesertScourge/DesertScourgeSandBlast");

	public static int SpitDamage = 10;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 20f;
		value.Position.Y -= 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 40;
		base.NPC.defense = 4;
		base.NPC.npcSlots = 12f;
		base.NPC.width = 104;
		base.NPC.height = 104;
		base.NPC.LifeMaxNERB(4000, 5000, 1150000);
		if (Main.getGoodWorld)
		{
			base.NPC.lifeMax *= 2;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.boss = true;
		base.NPC.value = Item.buyPrice(0, 1);
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.netAlways = true;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.4f;
		}
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 4f;
		}
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void BossHeadSlot(ref int index)
	{
		if (NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHead>()) || NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHeadYoung>()))
		{
			index = -1;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.DesertScourge")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.alpha);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(biomeEnrageTimer);
		writer.Write(playRoarSound);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.alpha = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		biomeEnrageTimer = reader.ReadInt32();
		playRoarSound = reader.ReadBoolean();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1219: Unknown result type (might be due to invalid IL or missing references)
		//IL_121e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef9: Unknown result type (might be due to invalid IL or missing references)
		//IL_126c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1285: Unknown result type (might be due to invalid IL or missing references)
		//IL_129e: Unknown result type (might be due to invalid IL or missing references)
		//IL_12aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1238: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_134b: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_137e: Unknown result type (might be due to invalid IL or missing references)
		//IL_138a: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1326: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1700: Unknown result type (might be due to invalid IL or missing references)
		//IL_1705: Unknown result type (might be due to invalid IL or missing references)
		//IL_170a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1711: Unknown result type (might be due to invalid IL or missing references)
		//IL_1407: Unknown result type (might be due to invalid IL or missing references)
		//IL_1412: Unknown result type (might be due to invalid IL or missing references)
		//IL_141c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1421: Unknown result type (might be due to invalid IL or missing references)
		//IL_142c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1431: Unknown result type (might be due to invalid IL or missing references)
		//IL_1436: Unknown result type (might be due to invalid IL or missing references)
		//IL_143b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1442: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Unknown result type (might be due to invalid IL or missing references)
		//IL_1752: Unknown result type (might be due to invalid IL or missing references)
		//IL_175e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2017: Unknown result type (might be due to invalid IL or missing references)
		//IL_2023: Unknown result type (might be due to invalid IL or missing references)
		//IL_200e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2028: Unknown result type (might be due to invalid IL or missing references)
		//IL_2030: Unknown result type (might be due to invalid IL or missing references)
		//IL_148c: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2050: Unknown result type (might be due to invalid IL or missing references)
		//IL_2055: Unknown result type (might be due to invalid IL or missing references)
		//IL_205d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2062: Unknown result type (might be due to invalid IL or missing references)
		//IL_2067: Unknown result type (might be due to invalid IL or missing references)
		//IL_206c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2073: Unknown result type (might be due to invalid IL or missing references)
		//IL_2078: Unknown result type (might be due to invalid IL or missing references)
		//IL_207d: Unknown result type (might be due to invalid IL or missing references)
		//IL_14be: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1508: Unknown result type (might be due to invalid IL or missing references)
		//IL_150a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1535: Unknown result type (might be due to invalid IL or missing references)
		//IL_153a: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1546: Unknown result type (might be due to invalid IL or missing references)
		//IL_154b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1550: Unknown result type (might be due to invalid IL or missing references)
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool hide = NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHead>()) || NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHeadYoung>());
		if (hide)
		{
			base.NPC.Calamity().newAI[0] = 0f;
			base.NPC.Calamity().newAI[1] = 0f;
			base.NPC.Calamity().newAI[3] = 0f;
			base.NPC.localAI[3] = 0f;
			playRoarSound = false;
		}
		base.NPC.dontTakeDamage = hide;
		base.NPC.canDisplayBuffs = !hide;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!player.ZoneDesert && !BossRushEvent.BossRushActive)
		{
			if (biomeEnrageTimer > 0)
			{
				biomeEnrageTimer--;
			}
		}
		else
		{
			biomeEnrageTimer = 300;
		}
		bool num = biomeEnrageTimer <= 0;
		float enrageScale = 0f;
		if (num)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 2f;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (((lifeRatio < 0.5f) & expertMode) && base.NPC.localAI[2] == 0f)
		{
			base.NPC.localAI[2] = 1f;
			NPC.SpawnOnPlayer(base.NPC.FindClosestPlayer(), ModContent.NPCType<DesertNuisanceHead>());
			NPC.SpawnOnPlayer(base.NPC.FindClosestPlayer(), ModContent.NPCType<DesertNuisanceHeadYoung>());
		}
		if ((base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y || Math.Abs(base.NPC.Center.X - player.Center.X) > 480f) && (revenge || lifeRatio < (expertMode ? 0.75f : 0.5f)))
		{
			base.NPC.Calamity().newAI[0]++;
		}
		bool burrow = base.NPC.Calamity().newAI[0] >= 600f;
		bool resetTime = base.NPC.Calamity().newAI[0] >= 1200f;
		bool lungeUpward = burrow && base.NPC.Calamity().newAI[1] == 1f;
		bool quickFall = base.NPC.Calamity().newAI[1] == 2f;
		float burrowDistance = (hide ? 1080f : 800f);
		float speed = (death ? 0.105f : 0.085f);
		float turnSpeed = (death ? 0.21f : 0.17f);
		if (expertMode)
		{
			speed += speed * 0.4f * (1f - lifeRatio);
			turnSpeed += turnSpeed * 0.4f * (1f - lifeRatio);
		}
		if (revenge)
		{
			speed += (death ? 0.03f : 0.02f) * (1f - lifeRatio);
			turnSpeed += (death ? 0.06f : 0.04f) * (1f - lifeRatio);
		}
		speed += 0.085f * enrageScale;
		turnSpeed += 0.17f * enrageScale;
		if (Main.getGoodWorld)
		{
			speed *= 1.1f;
			turnSpeed *= 1.2f;
		}
		if (!quickFall)
		{
			if (lungeUpward)
			{
				if (base.NPC.localAI[3] == 0f)
				{
					Point headTileCenter = base.NPC.Top.ToTileCoordinates();
					bool hasUnactuatedTile = Framing.GetTileSafely(headTileCenter).HasUnactuatedTile;
					bool finsInSolidTile = Framing.GetTileSafely(Main.npc[(int)base.NPC.ai[0]].Center.ToTileCoordinates()).HasUnactuatedTile;
					if ((!hasUnactuatedTile & finsInSolidTile) && Collision.CanHit(base.NPC.Top, 1, 1, player.Center, 1, 1))
					{
						base.NPC.localAI[3] = 1f;
						SoundEngine.PlaySound(in SoundID.Item74, base.NPC.Center);
						int bestY = headTileCenter.Y;
						for (int j = 0; j < 20; j++)
						{
							if (bestY < 10)
							{
								break;
							}
							if (!WorldGen.SolidTile(headTileCenter.X, bestY))
							{
								break;
							}
							bestY--;
						}
						for (int k = 0; k < 20; k++)
						{
							if (bestY > Main.maxTilesY - 10)
							{
								break;
							}
							if (WorldGen.ActiveAndWalkableTile(headTileCenter.X, bestY))
							{
								break;
							}
							bestY++;
						}
						if (Main.netMode != 1)
						{
							Vector2 sandSplashSpawnPos = default(Vector2);
							((Vector2)(ref sandSplashSpawnPos))._002Ector((float)(headTileCenter.X * 16 + 8), (float)(bestY * 16 - 40));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sandSplashSpawnPos, Vector2.Zero, ModContent.ProjectileType<DesertScourgeDiveSplash>(), 0, 0f, Main.myPlayer);
							if (death)
							{
								int type = ModContent.ProjectileType<DesertScourgeSpit>();
								Vector2 sandSpitPos = default(Vector2);
								for (int i = 0; i < 7; i++)
								{
									((Vector2)(ref sandSpitPos))._002Ector((float)(i - 2) * 16f, 0f - Math.Abs((float)(i - 2) * 16f));
									Vector2 sandSpitVelocity = (sandSplashSpawnPos + Vector2.UnitY * 80f - (sandSplashSpawnPos + sandSpitPos)).SafeNormalize(Vector2.UnitY) * (0f - (float)(Math.Abs(i - 3) + 1) * 3f);
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sandSplashSpawnPos + sandSpitPos, sandSpitVelocity, type, SpitDamage, 0f, Main.myPlayer);
								}
							}
						}
					}
				}
			}
			else if (burrow && base.NPC.localAI[3] == 0f)
			{
				Point headTileCenter2 = base.NPC.Center.ToTileCoordinates();
				if (Framing.GetTileSafely(headTileCenter2).HasUnactuatedTile && Collision.CanHit(base.NPC.Top, 1, 1, player.Center, 1, 1))
				{
					base.NPC.localAI[3] = 1f;
					SoundEngine.PlaySound(in SoundID.Item74, base.NPC.Center);
					int bestY2 = headTileCenter2.Y;
					for (int l = 0; l < 20; l++)
					{
						if (bestY2 < 10)
						{
							break;
						}
						if (!WorldGen.SolidTile(headTileCenter2.X, bestY2))
						{
							break;
						}
						bestY2--;
					}
					for (int m = 0; m < 20; m++)
					{
						if (bestY2 > Main.maxTilesY - 10)
						{
							break;
						}
						if (WorldGen.ActiveAndWalkableTile(headTileCenter2.X, bestY2))
						{
							break;
						}
						bestY2++;
					}
					if (Main.netMode != 1)
					{
						Vector2 sandSplashSpawnPos2 = default(Vector2);
						((Vector2)(ref sandSplashSpawnPos2))._002Ector((float)(headTileCenter2.X * 16 + 8), (float)(bestY2 * 16 - 40));
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sandSplashSpawnPos2, Vector2.Zero, ModContent.ProjectileType<DesertScourgeDiveSplash>(), 0, 0f, Main.myPlayer);
						if (death)
						{
							int type2 = ModContent.ProjectileType<DesertScourgeSpit>();
							Vector2 sandSpitPos2 = default(Vector2);
							for (int n = 0; n < 7; n++)
							{
								((Vector2)(ref sandSpitPos2))._002Ector((float)(n - 2) * 16f, 0f - Math.Abs((float)(n - 2) * 16f));
								Vector2 sandSpitVelocity2 = (sandSplashSpawnPos2 + Vector2.UnitY * 80f - (sandSplashSpawnPos2 + sandSpitPos2)).SafeNormalize(Vector2.UnitY) * (0f - (float)(Math.Abs(n - 3) + 1) * 3f);
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), sandSplashSpawnPos2 + sandSpitPos2, sandSpitVelocity2, type2, SpitDamage, 0f, Main.myPlayer);
							}
						}
					}
				}
			}
		}
		if (lungeUpward | burrow)
		{
			speed *= 1.5f;
			turnSpeed *= 1.5f;
			if ((base.NPC.Calamity().newAI[3] == 0f) & lungeUpward)
			{
				base.NPC.Calamity().newAI[3] = player.Center.Y - 600f;
			}
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (hide)
		{
			base.NPC.alpha += 3;
			if (base.NPC.alpha > 255)
			{
				base.NPC.alpha = 255;
			}
			else
			{
				for (int dustIndex = 0; dustIndex < 2; dustIndex++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 85, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
				}
			}
		}
		else
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		if (Main.netMode != 1 && Main.zenithWorld && (player.ZoneBeach || player.Calamity().ZoneAbyss || player.Calamity().ZoneSunkenSea || player.Calamity().ZoneSulphur) && NPC.CountNPCS(ModContent.NPCType<AquaticScourgeHead>()) < 1)
		{
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<AquaticScourgeHead>());
			base.NPC.active = false;
		}
		if (Main.netMode != 1 && !tailSpawned && base.NPC.ai[0] == 0f)
		{
			int previous = base.NPC.whoAmI;
			int minLength = (death ? 24 : (revenge ? 21 : (expertMode ? 18 : 15)));
			if (Main.getGoodWorld)
			{
				minLength *= 3;
			}
			int bodyTypeAIVariable = 0;
			for (int num2 = 0; num2 < minLength + 1; num2++)
			{
				int lol;
				if (num2 >= 0 && num2 < minLength)
				{
					bodyTypeAIVariable = ((num2 != 0) ? ((num2 == minLength - 1) ? 30 : ((num2 % 2 != 0) ? 10 : 20)) : 0);
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<DesertScourgeBody>(), base.NPC.whoAmI);
					Main.npc[lol].ai[3] = bodyTypeAIVariable;
				}
				else
				{
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<DesertScourgeTail>(), base.NPC.whoAmI);
				}
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = previous;
				Main.npc[previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				previous = lol;
			}
			tailSpawned = true;
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		int tilePositionX = (int)(base.NPC.position.X / 16f) - 1;
		int tileWidthPosX = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f) + 2;
		int tilePositionY = (int)(base.NPC.position.Y / 16f) - 1;
		int tileWidthPosY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f) + 2;
		if (tilePositionX < 0)
		{
			tilePositionX = 0;
		}
		if (tileWidthPosX > Main.maxTilesX)
		{
			tileWidthPosX = Main.maxTilesX;
		}
		if (tilePositionY < 0)
		{
			tilePositionY = 0;
		}
		if (tileWidthPosY > Main.maxTilesY)
		{
			tileWidthPosY = Main.maxTilesY;
		}
		bool shouldFly = lungeUpward;
		if (!shouldFly)
		{
			Vector2 vector2 = default(Vector2);
			for (int num3 = tilePositionX; num3 < tileWidthPosX; num3++)
			{
				for (int num4 = tilePositionY; num4 < tileWidthPosY; num4++)
				{
					if (Main.tile[num3, num4] != null && ((Main.tile[num3, num4].HasUnactuatedTile && (Main.tileSolid[Main.tile[num3, num4].TileType] || (Main.tileSolidTop[Main.tile[num3, num4].TileType] && Main.tile[num3, num4].TileFrameY == 0))) || Main.tile[num3, num4].LiquidAmount > 64))
					{
						vector2.X = num3 * 16;
						vector2.Y = num4 * 16;
						if (base.NPC.position.X + (float)base.NPC.width > vector2.X && base.NPC.position.X < vector2.X + 16f && base.NPC.position.Y + (float)base.NPC.height > vector2.Y && base.NPC.position.Y < vector2.Y + 16f)
						{
							shouldFly = true;
							break;
						}
					}
				}
			}
		}
		if (!shouldFly)
		{
			base.NPC.localAI[1] = 1f;
			Rectangle rectangle = default(Rectangle);
			((Rectangle)(ref rectangle))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
			int directChaseDistance = (death ? 600 : (expertMode ? 800 : 1000));
			if (enrageScale > 0f)
			{
				directChaseDistance = 100;
			}
			bool shouldDirectlyChase = true;
			if (base.NPC.position.Y > player.position.Y)
			{
				int rectWidth = directChaseDistance * 2;
				int rectHeight = directChaseDistance * 2;
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				Rectangle directChaseRect = default(Rectangle);
				while (enumerator.MoveNext())
				{
					Player current = enumerator.Current;
					int rectX = (int)current.position.X - directChaseDistance;
					int rectY = (int)current.position.Y - directChaseDistance;
					((Rectangle)(ref directChaseRect))._002Ector(rectX, rectY, rectWidth, rectHeight);
					if (((Rectangle)(ref rectangle)).Intersects(directChaseRect))
					{
						shouldDirectlyChase = false;
						break;
					}
				}
				if (shouldDirectlyChase)
				{
					shouldFly = true;
				}
			}
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		float maxChaseSpeed = (Main.zenithWorld ? 24f : (Main.getGoodWorld ? 21f : (death ? 15f : (expertMode ? 12.5f : 10f))));
		if (burrow | lungeUpward)
		{
			maxChaseSpeed *= 1.5f;
		}
		if (expertMode)
		{
			maxChaseSpeed += maxChaseSpeed * 0.2f * (1f - lifeRatio);
		}
		if (player.dead)
		{
			shouldFly = false;
			base.NPC.velocity.Y++;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y++;
				maxChaseSpeed *= 2f;
			}
			if ((double)base.NPC.position.Y > Main.rockLayer * 16.0)
			{
				for (int a = 0; a < Main.maxNPCs; a++)
				{
					if (Main.npc[a].type == ModContent.NPCType<DesertScourgeHead>() || Main.npc[a].type == ModContent.NPCType<DesertScourgeBody>() || Main.npc[a].type == ModContent.NPCType<DesertScourgeTail>())
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		float burrowTarget = player.Center.Y + burrowDistance;
		float lungeTarget = base.NPC.Calamity().newAI[3];
		Vector2 npcCenter = base.NPC.Center;
		float playerX = player.Center.X;
		float targettingPosition = (lungeUpward ? lungeTarget : (burrow ? burrowTarget : player.Center.Y));
		playerX = (int)(playerX / 16f) * 16;
		targettingPosition = (int)(targettingPosition / 16f) * 16;
		npcCenter.X = (int)(npcCenter.X / 16f) * 16;
		npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
		playerX -= npcCenter.X;
		targettingPosition -= npcCenter.Y;
		float targetDistance = (float)Math.Sqrt(playerX * playerX + targettingPosition * targettingPosition);
		if (burrow && base.NPC.Center.Y >= burrowTarget - 16f && !lungeUpward && !quickFall)
		{
			base.NPC.Calamity().newAI[1] = 1f;
			base.NPC.localAI[3] = 0f;
			if (!playRoarSound)
			{
				SoundEngine.PlaySound(in RoarSound, player.Center);
				playRoarSound = true;
			}
		}
		if (lungeUpward && base.NPC.Center.Y <= base.NPC.Calamity().newAI[3] + 600f - 420f && Math.Abs(base.NPC.Center.X - player.Center.X) < 480f && !quickFall)
		{
			SoundEngine.PlaySound(in SandBlastSound, base.NPC.Center);
			float velocity = (Main.getGoodWorld ? 16f : (death ? 8.5f : (revenge ? 8f : (expertMode ? 7.5f : 6f))));
			int type3 = ModContent.ProjectileType<DesertScourgeSpit>();
			Vector2 projectileVelocity = (base.NPC.Center + base.NPC.velocity * 10f - base.NPC.Center).SafeNormalize(Vector2.UnitY) * velocity;
			int numProj = (death ? 24 : (revenge ? 21 : (expertMode ? 18 : 12)));
			if (Main.getGoodWorld)
			{
				numProj *= 2;
			}
			float rotation = MathHelper.ToRadians((float)(Main.getGoodWorld ? 120 : 90));
			for (int num5 = 0; num5 < numProj; num5++)
			{
				Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)num5 / (float)(numProj - 1)));
				for (int num6 = 0; num6 < 10; num6++)
				{
					int dust2 = Dust.NewDust(base.NPC.Center + Vector2.Normalize(perturbedSpeed) * 5f, 10, 10, 75);
					Main.dust[dust2].velocity = perturbedSpeed;
				}
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(perturbedSpeed) * 5f, perturbedSpeed, type3, SpitDamage, 0f, Main.myPlayer);
				}
			}
			base.NPC.TargetClosest();
			base.NPC.Calamity().newAI[1] = 2f;
			base.NPC.localAI[3] = 0f;
			playRoarSound = false;
		}
		if (quickFall)
		{
			base.NPC.velocity.Y += maxChaseSpeed * 0.02f;
			if (base.NPC.Center.Y >= base.NPC.Calamity().newAI[3] + 600f)
			{
				base.NPC.Calamity().newAI[0] = 0f;
				base.NPC.Calamity().newAI[1] = 0f;
				base.NPC.Calamity().newAI[3] = 0f;
				base.NPC.localAI[3] = 0f;
				playRoarSound = false;
			}
		}
		if (resetTime)
		{
			base.NPC.Calamity().newAI[0] = 0f;
			base.NPC.Calamity().newAI[1] = 0f;
			base.NPC.Calamity().newAI[3] = 0f;
			base.NPC.localAI[3] = 0f;
			playRoarSound = false;
		}
		if (hide && !player.dead)
		{
			base.NPC.SimpleFlyMovement((new Vector2(player.Center.X, burrowTarget) - base.NPC.Center).SafeNormalize(Vector2.UnitY) * maxChaseSpeed, turnSpeed);
		}
		else if (!shouldFly)
		{
			base.NPC.velocity.Y += (death ? 0.125f : 0.1f);
			if (base.NPC.Center.Y - player.Center.Y < -180f)
			{
				base.NPC.velocity.Y += 0.05f;
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y += 0.05f;
				}
			}
			if (base.NPC.velocity.Y > maxChaseSpeed)
			{
				base.NPC.velocity.Y = maxChaseSpeed;
			}
			bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > speed;
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChaseSpeed * 0.4)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X -= speed * 1.1f;
				}
				else
				{
					base.NPC.velocity.X += speed * 1.1f;
				}
			}
			else if (base.NPC.velocity.Y == maxChaseSpeed)
			{
				if (slowXVelocity)
				{
					if (base.NPC.velocity.X < playerX)
					{
						base.NPC.velocity.X += speed;
					}
					else if (base.NPC.velocity.X > playerX)
					{
						base.NPC.velocity.X -= speed;
					}
				}
				else
				{
					base.NPC.velocity.X = 0f;
				}
			}
			else if (base.NPC.velocity.Y > 4f)
			{
				if (slowXVelocity)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X += speed * 0.9f;
					}
					else
					{
						base.NPC.velocity.X -= speed * 0.9f;
					}
				}
				else
				{
					base.NPC.velocity.X = 0f;
				}
			}
		}
		else
		{
			if (base.NPC.soundDelay == 0)
			{
				float soundDelay = targetDistance / 40f;
				if (soundDelay < 10f)
				{
					soundDelay = 10f;
				}
				if (soundDelay > 20f)
				{
					soundDelay = 20f;
				}
				base.NPC.soundDelay = (int)soundDelay;
				SoundEngine.PlaySound(in SoundID.WormDig, base.NPC.Center);
			}
			targetDistance = (float)Math.Sqrt(playerX * playerX + targettingPosition * targettingPosition);
			float absolutePlayerX = Math.Abs(playerX);
			float absoluteTargetPos = Math.Abs(targettingPosition);
			float timeToReachTarget = maxChaseSpeed / targetDistance;
			playerX *= timeToReachTarget;
			targettingPosition *= timeToReachTarget;
			if (((base.NPC.velocity.X > 0f && playerX > 0f) || (base.NPC.velocity.X < 0f && playerX < 0f)) && ((base.NPC.velocity.Y > 0f && targettingPosition > 0f) || (base.NPC.velocity.Y < 0f && targettingPosition < 0f)))
			{
				if (base.NPC.velocity.X < playerX)
				{
					base.NPC.velocity.X += turnSpeed;
				}
				else if (base.NPC.velocity.X > playerX)
				{
					base.NPC.velocity.X -= turnSpeed;
				}
				if (base.NPC.velocity.Y < targettingPosition)
				{
					base.NPC.velocity.Y += turnSpeed;
				}
				else if (base.NPC.velocity.Y > targettingPosition)
				{
					base.NPC.velocity.Y -= turnSpeed;
				}
			}
			if ((base.NPC.velocity.X > 0f && playerX > 0f) || (base.NPC.velocity.X < 0f && playerX < 0f) || (base.NPC.velocity.Y > 0f && targettingPosition > 0f) || (base.NPC.velocity.Y < 0f && targettingPosition < 0f))
			{
				if (base.NPC.velocity.X < playerX)
				{
					base.NPC.velocity.X += speed;
				}
				else if (base.NPC.velocity.X > playerX)
				{
					base.NPC.velocity.X -= speed;
				}
				if (base.NPC.velocity.Y < targettingPosition)
				{
					base.NPC.velocity.Y += speed;
				}
				else if (base.NPC.velocity.Y > targettingPosition)
				{
					base.NPC.velocity.Y -= speed;
				}
				if ((double)Math.Abs(targettingPosition) < (double)maxChaseSpeed * 0.2 && ((base.NPC.velocity.X > 0f && playerX < 0f) || (base.NPC.velocity.X < 0f && playerX > 0f)))
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y += speed * 2f;
					}
					else
					{
						base.NPC.velocity.Y -= speed * 2f;
					}
				}
				if ((double)Math.Abs(playerX) < (double)maxChaseSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && targettingPosition < 0f) || (base.NPC.velocity.Y < 0f && targettingPosition > 0f)))
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X += speed * 2f;
					}
					else
					{
						base.NPC.velocity.X -= speed * 2f;
					}
				}
			}
			else if (absolutePlayerX > absoluteTargetPos)
			{
				if (base.NPC.velocity.X < playerX)
				{
					base.NPC.velocity.X += speed * 1.1f;
				}
				else if (base.NPC.velocity.X > playerX)
				{
					base.NPC.velocity.X -= speed * 1.1f;
				}
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChaseSpeed * 0.5)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y += speed;
					}
					else
					{
						base.NPC.velocity.Y -= speed;
					}
				}
			}
			else
			{
				if (base.NPC.velocity.Y < targettingPosition)
				{
					base.NPC.velocity.Y += speed * 1.1f;
				}
				else if (base.NPC.velocity.Y > targettingPosition)
				{
					base.NPC.velocity.Y -= speed * 1.1f;
				}
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChaseSpeed * 0.5)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X += speed;
					}
					else
					{
						base.NPC.velocity.X -= speed;
					}
				}
			}
		}
		if ((!burrow | lungeUpward) && !quickFall && !hide)
		{
			Vector2 destination = (Vector2)(lungeUpward ? new Vector2(player.Center.X, lungeTarget) : player.Center);
			if (base.NPC.Distance(destination) > (lungeUpward ? 1000f : 2000f))
			{
				NPC nPC = base.NPC;
				nPC.velocity += (destination - base.NPC.Center).SafeNormalize(Vector2.UnitY) * turnSpeed;
			}
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
		if (shouldFly)
		{
			if (base.NPC.localAI[0] != 1f)
			{
				base.NPC.netUpdate = true;
			}
			base.NPC.localAI[0] = 1f;
		}
		else
		{
			if (base.NPC.localAI[0] != 0f)
			{
				base.NPC.netUpdate = true;
			}
			base.NPC.localAI[0] = 0f;
		}
		if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
		{
			base.NPC.netUpdate = true;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
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
		if (minDist <= 60f * base.NPC.scale)
		{
			return base.NPC.alpha <= 0;
		}
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		bool aboutToSpitSpread = base.NPC.Calamity().newAI[0] >= 600f && base.NPC.Calamity().newAI[1] == 1f && base.NPC.Center.Y <= base.NPC.Calamity().newAI[3] + 600f - 105f;
		bool openMouth = base.NPC.Distance(Main.player[base.NPC.target].Center) < 220f && (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation().AngleTowards(base.NPC.velocity.ToRotation(), (float)Math.PI / 4f) == base.NPC.velocity.ToRotation() && base.NPC.ai[3] == 0f;
		if (base.NPC.ai[3] == 1f)
		{
			if (base.NPC.frame.Y < frameHeight * 4)
			{
				base.NPC.frame.Y = frameHeight * 4;
				base.NPC.frameCounter = 0.0;
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 4.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
			{
				base.NPC.ai[3] = 2f;
				base.NPC.ForceNetUpdate();
				base.NPC.frame.Y = 0;
			}
		}
		else if (openMouth | aboutToSpitSpread)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 4.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 4)
			{
				base.NPC.frame.Y = frameHeight * 4;
			}
		}
		else if (base.NPC.frame.Y > 0)
		{
			if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
			{
				base.NPC.frame.Y = 0;
				base.NPC.ai[3] = 0f;
				base.NPC.ForceNetUpdate();
				return;
			}
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 4.0)
			{
				base.NPC.frame.Y -= frameHeight;
				base.NPC.frameCounter = 0.0;
			}
		}
		else
		{
			base.NPC.ai[3] = 0f;
			base.NPC.ForceNetUpdate();
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 169;
	}

	public override void OnKill()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		if (!DownedBossSystem.downedDesertScourge)
		{
			string key = "Mods.CalamityMod.Status.Progression.OpenSunkenSea";
			Color messageColor = Color.Aquamarine;
			Color messageColor2 = Color.PaleGoldenrod;
			CalamityUtils.BroadcastLocalizedText(key, messageColor);
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.SandstormTrigger", messageColor2);
			if (!Sandstorm.Happening)
			{
				CalamityWorld.StartSandstorm();
			}
		}
		DownedBossSystem.downedDesertScourge = true;
		CalamityNetcode.SyncWorld();
	}

	public override bool SpecialOnKill()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<DesertScourgeHead>(), ModContent.NPCType<DesertScourgeBody>(), ModContent.NPCType<DesertScourgeTail>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DesertScourgeBag>()));
		npcLoot.DefineConditionalDropSet(() => true).Add(DropHelper.PerPlayer(28, 1, 5, 15), hideLootReport: true);
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] items = new int[5]
		{
			ModContent.ItemType<SaharaSlicers>(),
			ModContent.ItemType<Barinade>(),
			ModContent.ItemType<SandstreamScepter>(),
			ModContent.ItemType<BrittleStarStaff>(),
			ModContent.ItemType<ScourgeoftheDesert>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, items));
		normalOnly.Add(ModContent.ItemType<DesertScourgeMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(275, 1, 25, 30);
		normalOnly.Add(2625, 1, 25, 30);
		normalOnly.Add(2626, 1, 25, 30);
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<PearlShard>(), 1, 25, 30));
		normalOnly.Add(ModContent.ItemType<SandCloak>(), DropHelper.NormalWeaponDropRateFraction);
		npcLoot.Add(ModContent.ItemType<DesertScourgeTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<DesertScourgeRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<SandSharkToothNecklace>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedDesertScourge, ModContent.ItemType<LoreDesertScourge>(), ui: true, DropHelper.FirstKillText);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeHead").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeHead2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeHead3").Type, base.NPC.scale);
			}
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			Texture2D texture = TextureAssets.Npc[base.NPC.type].Value;
			base.NPC.Opacity = 1f;
			base.NPC.frame = texture.Frame();
			float offset = -0.2f;
			float startX = 60f;
			float startY = 70f;
			int segmentSpacing = 50;
			int animationSpeed = 4;
			float wormTimer = base.NPC.Calamity().bestiaryWormTimer;
			for (int i = 3; i > 0; i--)
			{
				float bodyOffset = (float)(i * segmentSpacing) - (float)segmentSpacing * 0.5f;
				Texture2D toUse = ((i == 1) ? TextureAssets.Npc[ModContent.NPCType<DesertScourgeBody>()].Value : DesertScourgeBody.BodyTexture2.Value);
				int frameCount = ((i != 1) ? 1 : 7);
				spriteBatch.Draw(toUse, base.NPC.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * (float)i) * (float)animationSpeed) * 2f + startY), (Rectangle?)toUse.Frame(1, frameCount), base.NPC.GetAlpha(drawColor), base.NPC.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer + offset * (float)i) * (float)animationSpeed) * ((float)Math.PI / 4f) * 0.075f, new Vector2((float)(toUse.Width / 2), (float)(toUse.Height / (frameCount * 2))), base.NPC.scale, (SpriteEffects)0, 0f);
			}
			spriteBatch.Draw(texture, base.NPC.position + new Vector2(startX + 18f, MathF.Sin(wormTimer * (float)animationSpeed) * 2f + startY), (Rectangle?)texture.Frame(1, 7), base.NPC.GetAlpha(drawColor), base.NPC.rotation - (float)Math.PI / 2f - MathF.Cos(wormTimer * (float)animationSpeed) * ((float)Math.PI / 4f) * 0.075f, new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / 7)), base.NPC.scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.8f);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			base.NPC.ai[3] = 1f;
			base.NPC.ForceNetUpdate();
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			return Color.MediumBlue * (float)(int)((Color)(ref drawColor)).A * base.NPC.Opacity;
		}
		return null;
	}
}
