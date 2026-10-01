using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
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
using CalamityMod.Packets;
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

namespace CalamityMod.NPCs.AstrumDeus;

[AutoloadBossHead]
[HasPierceResist(false)]
[LongDistanceNetSync]
public class AstrumDeusHead : ModNPC
{
	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/AstrumDeusSpawn");

	public static readonly SoundStyle LaserSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/AstrumDeusLaser")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle GodRaySound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/AstrumDeusGodRay")
	{
		Volume = 0.4f
	};

	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/AstrumDeusMine")
	{
		Volume = 0.4f
	};

	public static readonly SoundStyle SplitSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/AstrumDeusSplit");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/AstrumDeusHit", 2)
	{
		Volume = 0.7f
	};

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/AstrumDeusDeath");

	public static Asset<Texture2D> TextureGlow1;

	public static Asset<Texture2D> TextureGlow2;

	public static Asset<Texture2D> TextureGlow3;

	public static Asset<Texture2D> TextureGlow4;

	public static Asset<Texture2D> TextureFlash2;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 55f;
		value.Position.Y += 23f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			TextureGlow1 = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			TextureGlow2 = ModContent.Request<Texture2D>(Texture + "Glow2", (AssetRequestMode)2);
			TextureGlow3 = ModContent.Request<Texture2D>(Texture + "Glow3", (AssetRequestMode)2);
			TextureGlow4 = ModContent.Request<Texture2D>(Texture + "Glow4", (AssetRequestMode)2);
			TextureFlash2 = ModContent.Request<Texture2D>(Texture + "GlowFlash2", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 120;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 56;
		base.NPC.height = 56;
		base.NPC.defense = 20;
		base.NPC.DR_NERD(0.1f);
		base.NPC.LifeMaxNERB(150000, 240000, 650000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.4f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.35f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.2f;
		}
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<AstrumDeusBossBar>();
		base.NPC.value = Item.buyPrice(0, 50);
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.netAlways = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
		if (Main.zenithWorld)
		{
			if (CalamityWorld.death)
			{
				base.NPC.lifeMax /= 3;
			}
			base.NPC.value /= 5f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AstrumDeus")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1301: Unknown result type (might be due to invalid IL or missing references)
		//IL_132d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_135f: Unknown result type (might be due to invalid IL or missing references)
		//IL_136b: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16da: Unknown result type (might be due to invalid IL or missing references)
		//IL_16df: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef8: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (calamityGlobalNPC.newAI[1] < 180f || base.NPC.dontTakeDamage)
		{
			base.NPC.damage = 0;
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool increaseSpeed = Vector2.Distance(player.Center, base.NPC.Center) > 3200f;
		bool increaseSpeedMore = Vector2.Distance(player.Center, base.NPC.Center) > 5600f;
		if (increaseSpeedMore)
		{
			base.NPC.TargetClosest();
		}
		if (revenge && !Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
		{
			Main.LocalPlayer.AddBuff(ModContent.BuffType<DoGExtremeGravity>(), 2);
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool halfHealth = lifeRatio < 0.5f;
		bool doubleWormPhase = calamityGlobalNPC.newAI[0] != 0f;
		bool startFlightPhase = (lifeRatio < 0.8f) | death | doubleWormPhase;
		bool phase2 = (lifeRatio < 0.5f) & doubleWormPhase & expertMode;
		bool phase3 = (lifeRatio < 0.2f) & doubleWormPhase & expertMode;
		bool deathModeEnragePhase_Head = calamityGlobalNPC.newAI[0] == 3f;
		bool deathModeEnragePhase_BodyAndTail = false;
		float resistanceTime = (doubleWormPhase ? 300f : 600f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = calamityGlobalNPC.newAI[1] < resistanceTime;
		float aiSwitchTimer = ((!doubleWormPhase) ? (Main.getGoodWorld ? 900f : 1800f) : (Main.getGoodWorld ? 600f : 1200f));
		calamityGlobalNPC.newAI[3]++;
		if (calamityGlobalNPC.newAI[3] >= aiSwitchTimer)
		{
			calamityGlobalNPC.newAI[3] = 0f;
		}
		if (doubleWormPhase && calamityGlobalNPC.newAI[3] % aiSwitchTimer == 0f && !(deathModeEnragePhase_Head | deathModeEnragePhase_BodyAndTail))
		{
			SoundStyle style = SplitSound with
			{
				Pitch = -0.2f,
				Volume = 0.9f
			};
			SoundEngine.PlaySound(in style, player.Center);
		}
		bool flyAtTarget = (calamityGlobalNPC.newAI[3] >= aiSwitchTimer * 0.5f) & startFlightPhase;
		int phase1Length = (death ? 80 : (revenge ? 70 : (expertMode ? 60 : 50)));
		int phase2Length = (death ? 40 : (revenge ? 35 : (expertMode ? 30 : 25)));
		int gfbLength = (death ? 8 : (revenge ? 7 : (expertMode ? 6 : 5)));
		int maxLength = ((Main.zenithWorld & doubleWormPhase) ? gfbLength : (doubleWormPhase ? phase2Length : phase1Length));
		int gfbMaxWormCount = 10;
		int gfbWormCount = 0;
		if (Main.zenithWorld)
		{
			gfbWormCount = NPC.CountNPCS(ModContent.NPCType<AstrumDeusHead>());
		}
		if (gfbWormCount > gfbMaxWormCount)
		{
			gfbWormCount = gfbMaxWormCount;
		}
		float splitAnimationTime = 180f;
		bool oneWormAlive = NPC.CountNPCS(ModContent.NPCType<AstrumDeusHead>()) < 2;
		if ((death & doubleWormPhase & oneWormAlive) && calamityGlobalNPC.newAI[1] >= resistanceTime)
		{
			if (calamityGlobalNPC.newAI[0] != 3f)
			{
				SoundEngine.PlaySound(in SplitSound, player.Center);
				calamityGlobalNPC.newAI[0] = 3f;
				base.NPC.defense = 12;
				calamityGlobalNPC.DR = 0.075f;
				int bodyID = ModContent.NPCType<AstrumDeusBody>();
				int tailID = ModContent.NPCType<AstrumDeusTail>();
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC wormseg = Main.npc[i];
					if (wormseg.active && (wormseg.type == bodyID || wormseg.type == tailID) && Main.npc[(int)wormseg.ai[2]].Calamity().newAI[0] != 3f)
					{
						wormseg.life = 0;
						wormseg.active = false;
					}
				}
			}
			calamityGlobalNPC.newAI[2] += 10f;
			if (calamityGlobalNPC.newAI[2] > 162f)
			{
				calamityGlobalNPC.newAI[2] = 162f;
			}
			base.NPC.Opacity = MathHelper.Clamp(1f - calamityGlobalNPC.newAI[2] / splitAnimationTime, 0f, 1f);
		}
		bool despawnRemainingWorm = (doubleWormPhase & oneWormAlive) && !death;
		if ((halfHealth && calamityGlobalNPC.newAI[0] == 0f) | despawnRemainingWorm)
		{
			base.NPC.dontTakeDamage = true;
			calamityGlobalNPC.newAI[2] += (despawnRemainingWorm ? 10f : 1f);
			base.NPC.Opacity = MathHelper.Clamp(1f - calamityGlobalNPC.newAI[2] / splitAnimationTime, 0f, 1f);
			if (calamityGlobalNPC.newAI[2] == splitAnimationTime)
			{
				if (doubleWormPhase)
				{
					return;
				}
				int bodyID2 = ModContent.NPCType<AstrumDeusBody>();
				int tailID2 = ModContent.NPCType<AstrumDeusTail>();
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					NPC wormseg2 = Main.npc[j];
					if (wormseg2.active && (wormseg2.type == bodyID2 || wormseg2.type == tailID2))
					{
						wormseg2.life = 0;
						wormseg2.active = false;
					}
				}
				base.NPC.life = 0;
				if (Main.netMode == 1)
				{
					return;
				}
				int wormamt = ((!Main.zenithWorld) ? 1 : 5);
				for (int k = 0; k < wormamt; k++)
				{
					int startIndexHeadOne = 1;
					int headOneID = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, base.NPC.type, startIndexHeadOne);
					Main.npc[headOneID].Calamity().newAI[0] = 1f;
					Main.npc[headOneID].velocity = Vector2.Normalize(player.Center - Main.npc[headOneID].Center) * 16f;
					Main.npc[headOneID].timeLeft *= 20;
					Main.npc[headOneID].ForceNetUpdate();
					if (Main.dedServ)
					{
						SyncCalamityNPCAIArrayPacket.Send(Main.npc[headOneID]);
					}
					int startIndexHeadTwo = startIndexHeadOne + phase2Length + 1;
					int headTwoID = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, base.NPC.type, startIndexHeadTwo);
					Main.npc[headTwoID].Calamity().newAI[0] = 2f;
					Main.npc[headTwoID].Calamity().newAI[3] = (Main.getGoodWorld ? 300f : 600f);
					Main.npc[headTwoID].velocity = Vector2.Normalize(player.Center - Main.npc[headTwoID].Center) * 16f;
					Main.npc[headTwoID].timeLeft *= 20;
					Main.npc[headTwoID].ForceNetUpdate();
					if (Main.dedServ)
					{
						SyncCalamityNPCAIArrayPacket.Send(Main.npc[headTwoID]);
					}
				}
				SoundEngine.PlaySound(in SplitSound, player.Center);
				return;
			}
		}
		if (Main.zenithWorld && calamityGlobalNPC.newAI[1] < 10f)
		{
			float pushForce = 0.25f;
			for (int l = 0; l < Main.maxNPCs; l++)
			{
				NPC otherDeus = Main.npc[l];
				if (!otherDeus.active || l == base.NPC.whoAmI)
				{
					continue;
				}
				bool num = otherDeus.type == base.NPC.type;
				float taxicabDist = Vector2.Distance(base.NPC.Center, otherDeus.Center);
				float distancegate = 320f;
				if (num && taxicabDist < distancegate)
				{
					if (base.NPC.position.X < otherDeus.position.X)
					{
						base.NPC.velocity.X -= pushForce;
					}
					else
					{
						base.NPC.velocity.X += pushForce;
					}
					if (base.NPC.position.Y < otherDeus.position.Y)
					{
						base.NPC.velocity.Y -= pushForce;
					}
					else
					{
						base.NPC.velocity.Y += pushForce;
					}
				}
			}
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (Main.netMode != 1 && base.NPC.ai[0] == 0f)
		{
			int Previous = base.NPC.whoAmI;
			int bodyType = ModContent.NPCType<AstrumDeusBody>();
			int tailType = ModContent.NPCType<AstrumDeusTail>();
			for (int segments = 0; segments < maxLength; segments++)
			{
				int lol = ((segments < 0 || segments >= maxLength - 1) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, tailType, base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, bodyType, base.NPC.whoAmI));
				if (segments % 2 == 0)
				{
					Main.npc[lol].localAI[3] = 1f;
				}
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].Calamity().newAI[0] = Main.npc[Previous].Calamity().newAI[0];
				Main.npc[lol].Calamity().newAI[3] = Main.npc[Previous].Calamity().newAI[3];
				if (Main.dedServ)
				{
					SyncCalamityNPCAIArrayPacket.Send(Main.npc[lol]);
				}
				Main.npc[lol].ai[3] = segments + 1;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
			}
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		bool hasJustSpawned = calamityGlobalNPC.newAI[1] < resistanceTime * 0.4f && !doubleWormPhase;
		float segmentVelocity = (hasJustSpawned ? 25f : (deathModeEnragePhase_Head ? 19f : (death ? 17.5f : 16f)));
		float segmentVelocityBoost = 5f * (1f - lifeRatio);
		segmentVelocity += segmentVelocityBoost;
		if (gfbWormCount > 0)
		{
			segmentVelocity += (float)(gfbMaxWormCount - gfbWormCount) * 0.444f;
		}
		if (revenge)
		{
			float revMultiplier = 1.1f;
			segmentVelocity *= revMultiplier;
		}
		int headTilePositionX = (int)(base.NPC.position.X / 16f) - 1;
		int headTileWidthPosX = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f) + 2;
		int headTilePositionY = (int)(base.NPC.position.Y / 16f) - 1;
		int headTileWidthPosY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f) + 2;
		if (headTilePositionX < 0)
		{
			headTilePositionX = 0;
		}
		if (headTileWidthPosX > Main.maxTilesX)
		{
			headTileWidthPosX = Main.maxTilesX;
		}
		if (headTilePositionY < 0)
		{
			headTilePositionY = 0;
		}
		if (headTileWidthPosY > Main.maxTilesY)
		{
			headTileWidthPosY = Main.maxTilesY;
		}
		bool shouldFly = flyAtTarget;
		if (!shouldFly)
		{
			Vector2 vector2 = default(Vector2);
			for (int m = headTilePositionX; m < headTileWidthPosX; m++)
			{
				for (int n = headTilePositionY; n < headTileWidthPosY; n++)
				{
					if (Main.tile[m, n] != null && ((Main.tile[m, n].HasUnactuatedTile && (Main.tileSolid[Main.tile[m, n].TileType] || (Main.tileSolidTop[Main.tile[m, n].TileType] && Main.tile[m, n].TileFrameY == 0))) || Main.tile[m, n].LiquidAmount > 64))
					{
						vector2.X = (float)m * 16f;
						vector2.Y = (float)n * 16f;
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
			int noFlyZone = 200;
			int heightReduction = (death ? 130 : ((int)(130f * (1f - lifeRatio))));
			int height = 400 - heightReduction;
			bool outsideNoFlyZone = true;
			if (base.NPC.position.Y > player.position.Y)
			{
				Rectangle rectangle2 = default(Rectangle);
				for (int num2 = 0; num2 < 255; num2++)
				{
					if (Main.player[num2].active)
					{
						((Rectangle)(ref rectangle2))._002Ector((int)Main.player[num2].position.X - noFlyZone, (int)Main.player[num2].position.Y - noFlyZone, noFlyZone * 2, height);
						if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
						{
							outsideNoFlyZone = false;
							break;
						}
					}
				}
				if (outsideNoFlyZone)
				{
					shouldFly = true;
				}
			}
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		if (player.dead)
		{
			shouldFly = false;
			float velocity = 2f;
			base.NPC.velocity.Y -= velocity;
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				segmentVelocity *= 2f;
				base.NPC.velocity.Y -= velocity;
			}
			int headType = ModContent.NPCType<AstrumDeusHead>();
			int bodyType2 = ModContent.NPCType<AstrumDeusBody>();
			int tailType2 = ModContent.NPCType<AstrumDeusBody>();
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				for (int num3 = 0; num3 < Main.maxNPCs; num3++)
				{
					if (Main.npc[num3].type == headType || Main.npc[num3].type == bodyType2 || Main.npc[num3].type == tailType2)
					{
						Main.npc[num3].active = false;
						Main.npc[num3].ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
			}
		}
		float speedBoost = (death ? (0.1f * (1f - lifeRatio)) : (0.13f * (1f - lifeRatio)));
		float turnSpeedBoost = (death ? (0.18f * (1f - lifeRatio)) : (0.2f * (1f - lifeRatio)));
		float speed = (hasJustSpawned ? 0.26f : (deathModeEnragePhase_Head ? 0.2f : (death ? 0.18f : 0.13f))) + speedBoost;
		float turnSpeed = (hasJustSpawned ? 0.3f : (deathModeEnragePhase_Head ? 0.27f : (death ? 0.25f : 0.2f))) + turnSpeedBoost;
		if (gfbWormCount > 0)
		{
			speed += (float)(gfbMaxWormCount - gfbWormCount) * 0.00555f;
			turnSpeed += (float)(gfbMaxWormCount - gfbWormCount) * 0.00888f;
		}
		if (flyAtTarget)
		{
			float speedMultiplier = (deathModeEnragePhase_Head ? 1.25f : ((!doubleWormPhase) ? 1f : (phase3 ? 1.25f : (phase2 ? 1.2f : 1.15f))));
			speed *= speedMultiplier;
		}
		if (revenge)
		{
			float revMultiplier2 = 1.1f;
			speed *= revMultiplier2;
			turnSpeed *= revMultiplier2;
		}
		speed *= (increaseSpeedMore ? 2f : (increaseSpeed ? 1.5f : 1f));
		turnSpeed *= (increaseSpeedMore ? 2f : (increaseSpeed ? 1.5f : 1f));
		if (Main.getGoodWorld)
		{
			speed *= 1.15f;
			turnSpeed *= 1.15f;
		}
		Vector2 deusCenter = base.NPC.Center;
		float deusTargetX = player.Center.X;
		float deusTargetY = player.Center.Y;
		deusTargetX = (int)(deusTargetX / 16f) * 16;
		deusTargetY = (int)(deusTargetY / 16f) * 16;
		deusCenter.X = (int)(deusCenter.X / 16f) * 16;
		deusCenter.Y = (int)(deusCenter.Y / 16f) * 16;
		deusTargetX -= deusCenter.X;
		deusTargetY -= deusCenter.Y;
		if (!shouldFly)
		{
			base.NPC.velocity.Y += 0.15f;
			if (base.NPC.velocity.Y > segmentVelocity)
			{
				base.NPC.velocity.Y = segmentVelocity;
			}
			bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > speed;
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.4)
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
			else if (base.NPC.velocity.Y == segmentVelocity)
			{
				if (slowXVelocity)
				{
					if (base.NPC.velocity.X < deusTargetX)
					{
						base.NPC.velocity.X += speed;
					}
					else if (base.NPC.velocity.X > deusTargetX)
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
			float deusTargetDist = (float)Math.Sqrt(deusTargetX * deusTargetX + deusTargetY * deusTargetY);
			float deusAbsoluteTargetX = Math.Abs(deusTargetX);
			float deusAbsoluteTargetY = Math.Abs(deusTargetY);
			float deusTimeToReachTarget = segmentVelocity / deusTargetDist;
			deusTargetX *= deusTimeToReachTarget;
			deusTargetY *= deusTimeToReachTarget;
			bool speedUpWhileFlying = false;
			if (flyAtTarget)
			{
				if (((base.NPC.velocity.X > 0f && deusTargetX < 0f) || (base.NPC.velocity.X < 0f && deusTargetX > 0f) || (base.NPC.velocity.Y > 0f && deusTargetY < 0f) || (base.NPC.velocity.Y < 0f && deusTargetY > 0f)) && Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) > speed / 2f && deusTargetDist < 400f)
				{
					speedUpWhileFlying = true;
					if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < segmentVelocity)
					{
						NPC nPC = base.NPC;
						nPC.velocity *= 1.1f;
					}
				}
				if (base.NPC.position.Y > player.position.Y)
				{
					speedUpWhileFlying = true;
					if (Math.Abs(base.NPC.velocity.X) < segmentVelocity / 2f)
					{
						if (base.NPC.velocity.X == 0f)
						{
							base.NPC.velocity.X -= base.NPC.direction;
						}
						base.NPC.velocity.X *= 1.1f;
					}
					else if (base.NPC.velocity.Y > 0f - segmentVelocity)
					{
						base.NPC.velocity.Y -= speed;
					}
				}
			}
			if (!speedUpWhileFlying)
			{
				if (!flyAtTarget && ((base.NPC.velocity.X > 0f && deusTargetX > 0f) || (base.NPC.velocity.X < 0f && deusTargetX < 0f)) && ((base.NPC.velocity.Y > 0f && deusTargetY > 0f) || (base.NPC.velocity.Y < 0f && deusTargetY < 0f)))
				{
					if (base.NPC.velocity.X < deusTargetX)
					{
						base.NPC.velocity.X += turnSpeed;
					}
					else if (base.NPC.velocity.X > deusTargetX)
					{
						base.NPC.velocity.X -= turnSpeed;
					}
					if (base.NPC.velocity.Y < deusTargetY)
					{
						base.NPC.velocity.Y += turnSpeed;
					}
					else if (base.NPC.velocity.Y > deusTargetY)
					{
						base.NPC.velocity.Y -= turnSpeed;
					}
				}
				if ((base.NPC.velocity.X > 0f && deusTargetX > 0f) || (base.NPC.velocity.X < 0f && deusTargetX < 0f) || (base.NPC.velocity.Y > 0f && deusTargetY > 0f) || (base.NPC.velocity.Y < 0f && deusTargetY < 0f))
				{
					if (base.NPC.velocity.X < deusTargetX)
					{
						base.NPC.velocity.X += speed;
					}
					else if (base.NPC.velocity.X > deusTargetX)
					{
						base.NPC.velocity.X -= speed;
					}
					if (base.NPC.velocity.Y < deusTargetY)
					{
						base.NPC.velocity.Y += speed;
					}
					else if (base.NPC.velocity.Y > deusTargetY)
					{
						base.NPC.velocity.Y -= speed;
					}
					if ((double)Math.Abs(deusTargetY) < (double)segmentVelocity * 0.2 && ((base.NPC.velocity.X > 0f && deusTargetX < 0f) || (base.NPC.velocity.X < 0f && deusTargetX > 0f)))
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
					if ((double)Math.Abs(deusTargetX) < (double)segmentVelocity * 0.2 && ((base.NPC.velocity.Y > 0f && deusTargetY < 0f) || (base.NPC.velocity.Y < 0f && deusTargetY > 0f)))
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
				else if (deusAbsoluteTargetX > deusAbsoluteTargetY)
				{
					if (base.NPC.velocity.X < deusTargetX)
					{
						base.NPC.velocity.X += speed * 1.1f;
					}
					else if (base.NPC.velocity.X > deusTargetX)
					{
						base.NPC.velocity.X -= speed * 1.1f;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.5)
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
					if (base.NPC.velocity.Y < deusTargetY)
					{
						base.NPC.velocity.Y += speed * 1.1f;
					}
					else if (base.NPC.velocity.Y > deusTargetY)
					{
						base.NPC.velocity.Y -= speed * 1.1f;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.5)
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
		}
		if (shouldFly)
		{
			if (base.NPC.localAI[0] != 1f)
			{
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			base.NPC.localAI[0] = 1f;
		}
		else
		{
			if (base.NPC.localAI[0] != 0f)
			{
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			base.NPC.localAI[0] = 0f;
		}
		if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
		{
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
		if (calamityGlobalNPC.newAI[1] == 0f && !doubleWormPhase)
		{
			SoundEngine.PlaySound(in SpawnSound, base.NPC.Center);
			calamityGlobalNPC.newAI[1] = 1f;
		}
		if (calamityGlobalNPC.newAI[1] < resistanceTime)
		{
			Vector2 val = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val)).Length() > 2f || calamityGlobalNPC.newAI[1] > 1f)
			{
				calamityGlobalNPC.newAI[1]++;
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<AstrumDeusBody>()].Value, AstrumDeusBody.AltTexture.Value, 7, 26, 0.3f, new Vector2(0f, 10f), 5, 10f, 4f);
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		bool deathModeEnragePhase = base.NPC.Calamity().newAI[0] == 3f;
		bool doubleWormPhase = base.NPC.Calamity().newAI[0] != 0f && !deathModeEnragePhase;
		float cyanThreshold = (Main.getGoodWorld ? 300f : 600f);
		float transitionStart = cyanThreshold * 0.95f;
		bool drawCyan = base.NPC.Calamity().newAI[3] >= cyanThreshold;
		bool inColorTrans = doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold >= transitionStart;
		Texture2D mainWormTex = TextureAssets.Npc[base.Type].Value;
		Texture2D secondWormTex = TextureGlow2.Value;
		Vector2 halfSizeTex = default(Vector2);
		((Vector2)(ref halfSizeTex))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)mainWormTex.Width, (float)mainWormTex.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTex * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(mainWormTex, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
		mainWormTex = TextureGlow1.Value;
		float colorOpacity = Utils.GetLerpValue(transitionStart, cyanThreshold, base.NPC.Calamity().newAI[3] % cyanThreshold, clamped: true);
		Color phaseColor = (drawCyan ? Color.Cyan : Color.Orange);
		Color otherPhaseColor = (drawCyan ? Color.Orange : Color.Cyan);
		Texture2D otherMainTex;
		Texture2D otherSecondTex;
		if (doubleWormPhase)
		{
			mainWormTex = (drawCyan ? mainWormTex : TextureGlow3.Value);
			otherMainTex = (drawCyan ? TextureGlow3.Value : mainWormTex);
			secondWormTex = (drawCyan ? TextureGlow4.Value : secondWormTex);
			otherSecondTex = (drawCyan ? secondWormTex : TextureGlow4.Value);
		}
		else
		{
			otherMainTex = mainWormTex;
			otherSecondTex = secondWormTex;
		}
		Color mainWormColorLerp = Color.Lerp(Color.White, doubleWormPhase ? phaseColor : Color.Cyan, 0.5f) * (deathModeEnragePhase ? 1f : base.NPC.Opacity);
		Color secondWormColorLerp = Color.Lerp(Color.White, doubleWormPhase ? phaseColor : Color.Orange, 0.5f) * (deathModeEnragePhase ? 1f : base.NPC.Opacity);
		int timesToDraw = (deathModeEnragePhase ? 3 : (drawCyan ? 1 : 2));
		for (int i = 0; i < timesToDraw; i++)
		{
			spriteBatch.Draw(mainWormTex, drawLocation, (Rectangle?)base.NPC.frame, mainWormColorLerp * (inColorTrans ? (1f - colorOpacity) : 1f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			if (inColorTrans)
			{
				spriteBatch.Draw(otherMainTex, drawLocation, (Rectangle?)base.NPC.frame, Color.Lerp(Color.White, otherPhaseColor, 0.5f) * colorOpacity, base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
		}
		timesToDraw = (deathModeEnragePhase ? 3 : ((!drawCyan) ? 1 : 2));
		for (int j = 0; j < timesToDraw; j++)
		{
			spriteBatch.Draw(secondWormTex, drawLocation, (Rectangle?)base.NPC.frame, secondWormColorLerp * (inColorTrans ? (1f - colorOpacity) : 1f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			if (inColorTrans)
			{
				spriteBatch.Draw(otherSecondTex, drawLocation, (Rectangle?)base.NPC.frame, Color.Lerp(Color.White, otherPhaseColor, 0.5f) * colorOpacity, base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
			if (doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold < 25f)
			{
				spriteBatch.Draw(TextureFlash2.Value, drawLocation, (Rectangle?)base.NPC.frame, Color.White * MathHelper.Lerp(1f, 0f, base.NPC.Calamity().newAI[3] % cyanThreshold / 25f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0 || Main.zenithWorld || !Main.rand.NextBool(5))
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 5; i++)
		{
			int purpleDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[purpleDust].scale = 0.5f;
				Main.dust[purpleDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
			Main.dust[astralDust].noGravity = true;
			Dust obj2 = Main.dust[astralDust];
			obj2.velocity *= 5f;
			astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[astralDust];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusHead1").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusHead2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusHead3").Type);
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<StarblightSoot>();
	}

	public static bool ShouldNotDropThings(NPC NPC)
	{
		if (NPC.Calamity().newAI[0] != 0f)
		{
			if (CalamityWorld.death || BossRushEvent.BossRushActive)
			{
				return NPC.Calamity().newAI[0] != 3f;
			}
			return false;
		}
		return true;
	}

	public override bool SpecialOnKill()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (ShouldNotDropThings(base.NPC))
		{
			return false;
		}
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<AstrumDeusHead>(), ModContent.NPCType<AstrumDeusBody>(), ModContent.NPCType<AstrumDeusTail>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void OnKill()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (ShouldNotDropThings(base.NPC))
		{
			return;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC otherWormHead = enumerator.Current;
			if (otherWormHead.type == base.NPC.type)
			{
				otherWormHead.Calamity().newAI[0] = 0f;
				otherWormHead.life = 0;
				otherWormHead.checkDead();
				otherWormHead.netUpdate = true;
			}
		}
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			if (!DownedBossSystem.downedAstrumDeus)
			{
				Color messageColor = Color.Gold;
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.AstralBossText", messageColor);
			}
			DownedBossSystem.downedAstrumDeus = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule lastWorm = npcLoot.DefineConditionalDropSet((DropAttemptInfo info) => !ShouldNotDropThings(info.npc));
		lastWorm.Add(ItemDropRule.BossBag(ModContent.ItemType<AstrumDeusBag>()));
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		lastWorm.Add(normalOnly);
		int[] weapons = new int[5]
		{
			ModContent.ItemType<TheMicrowave>(),
			ModContent.ItemType<StarSputter>(),
			ModContent.ItemType<StarShower>(),
			ModContent.ItemType<StarspawnHelixStaff>(),
			ModContent.ItemType<RegulusRiot>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<AstrumDeusMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(ModContent.ItemType<ChromaticOrb>(), 5);
		normalOnly.Add(75, 1, 25, 40);
		normalOnly.Add(ModContent.ItemType<StarblightSoot>(), 1, 50, 80);
		npcLoot.DefineConditionalDropSet(() => true).Add(DropHelper.PerPlayer(3544, 1, 5, 15), hideLootReport: true);
		lastWorm.Add(ModContent.ItemType<AstrumDeusTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).AddIf((DropAttemptInfo info) => !ShouldNotDropThings(info.npc), ModContent.ItemType<AstrumDeusRelic>());
		lastWorm.Add(DropHelper.NormalVsExpertQuantity(3458, 1, 16, 24, 20, 32));
		lastWorm.Add(DropHelper.NormalVsExpertQuantity(3456, 1, 16, 24, 20, 32));
		lastWorm.Add(DropHelper.NormalVsExpertQuantity(3457, 1, 16, 24, 20, 32));
		lastWorm.Add(DropHelper.NormalVsExpertQuantity(3459, 1, 16, 24, 20, 32));
		lastWorm.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<MeldBlob>(), 1, 16, 24, 20, 32));
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(2002, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4345, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(5341, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(2673, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(3191, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4036, 1, 1, 9999), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(firstDeusKill, ModContent.ItemType<LoreAstrumDeus>(), ui: true, DropHelper.FirstKillText);
		npcLoot.AddConditionalPerPlayer(firstDeusKill, ModContent.ItemType<LoreAstralInfection>(), ui: true, DropHelper.FirstKillText);
		static bool firstDeusKill(DropAttemptInfo info)
		{
			if (!DownedBossSystem.downedAstrumDeus)
			{
				return !ShouldNotDropThings(info.npc);
			}
			return false;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 360);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
