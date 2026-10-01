using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Perforator;

[AutoloadBossHead]
[HasPierceResist(false)]
[LongDistanceNetSync]
public class PerforatorHeadLarge : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfLargeHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");

	public static Asset<Texture2D> GlowTexture;

	private bool TailSpawned;

	public static int LaserWallDamage = 10;

	public override LocalizedText DeathMessage => CalamityUtils.GetText("NPCs.PerforatorLarge");

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.75f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 70f;
		value.Position.Y += 40f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 40;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 70;
		base.NPC.height = 84;
		base.NPC.defense = 4;
		base.NPC.LifeMaxNERB(2000, 2600, 80000);
		if (Main.zenithWorld)
		{
			base.NPC.lifeMax *= 4;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.netAlways = true;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.2f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.15f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.1f;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<PerforatorHive>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Perforator")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1174: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Unknown result type (might be due to invalid IL or missing references)
		//IL_1889: Unknown result type (might be due to invalid IL or missing references)
		//IL_188f: Unknown result type (might be due to invalid IL or missing references)
		//IL_189a: Unknown result type (might be due to invalid IL or missing references)
		//IL_189f: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1add: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8d: Unknown result type (might be due to invalid IL or missing references)
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float speed = 0.1f;
		float turnSpeed = 0.07f;
		if (expertMode)
		{
			float velocityScale = (death ? 0.1f : 0.07f);
			speed += velocityScale * (1f - lifeRatio);
			float accelerationScale = (death ? 0.07f : 0.05f);
			turnSpeed += accelerationScale * (1f - lifeRatio);
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		float spitDistance = 960f;
		float tooCloseToSpitDistance = 320f;
		bool isInRangeToSpit = base.NPC.Distance(player.Center) <= spitDistance && base.NPC.Distance(player.Center) > tooCloseToSpitDistance;
		bool headIsTurnedTowardsTarget = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation().AngleTowards(base.NPC.velocity.ToRotation(), (float)Math.PI / 4f) == base.NPC.velocity.ToRotation();
		bool canHitTarget = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height);
		bool alwaysAbleToSpit = base.NPC.Calamity().newAI[1] == 1f;
		if ((isInRangeToSpit & headIsTurnedTowardsTarget & canHitTarget) | alwaysAbleToSpit)
		{
			float spitGateValue = 120f;
			if (base.NPC.Calamity().newAI[0] < spitGateValue)
			{
				base.NPC.Calamity().newAI[0]++;
			}
			bool spit = (base.NPC.Calamity().newAI[0] >= spitGateValue) & isInRangeToSpit & headIsTurnedTowardsTarget & canHitTarget;
			float telegraphSpitGateValue = spitGateValue - 30f;
			bool num = base.NPC.Calamity().newAI[0] >= telegraphSpitGateValue;
			Vector2 spitLocation = base.NPC.Center + base.NPC.velocity.SafeNormalize(Vector2.UnitY) * 20f;
			if (num)
			{
				base.NPC.Calamity().newAI[1] = 1f;
				int dustType = (Main.rand.NextBool() ? 170 : 5);
				for (int k = 0; k < 10; k++)
				{
					int dust = Dust.NewDust(spitLocation, 1, 1, dustType);
					Main.dust[dust].position = spitLocation + Main.rand.NextVector2CircularEdge(25f, 25f);
					Main.dust[dust].velocity = (spitLocation - Main.dust[dust].position).SafeNormalize(Vector2.UnitY) * 2f;
					Main.dust[dust].scale = ((dustType == 170) ? 1f : 2f);
					Main.dust[dust].noGravity = true;
				}
			}
			if (spit)
			{
				base.NPC.Calamity().newAI[0] = 0f;
				base.NPC.Calamity().newAI[1] = 0f;
				SoundEngine.PlaySound(in SoundID.NPCDeath13, spitLocation);
				if (Main.netMode != 1)
				{
					int spitProjectileAmount = 8;
					float spitProjectileBaseVelocity = 16f;
					float spitProjectileRandomVelocityLimit = 3f;
					for (int i = 0; i < spitProjectileAmount; i++)
					{
						int type = (Main.rand.NextBool() ? ModContent.ProjectileType<IchorShot>() : ModContent.ProjectileType<BloodGeyser>());
						int damage = ((type == ModContent.ProjectileType<IchorShot>()) ? PerforatorHive.IchorShotDamage : PerforatorHive.BloodGeyserDamage);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spitLocation + Main.rand.NextVector2CircularEdge(8f, 8f), base.NPC.velocity.SafeNormalize(Vector2.UnitY) * spitProjectileBaseVelocity + Main.rand.NextVector2CircularEdge(spitProjectileRandomVelocityLimit, spitProjectileRandomVelocityLimit), type, damage, 0f, Main.myPlayer, 0f, player.Center.Y);
					}
					if (death)
					{
						int spitBlobAmount = 3;
						float spitBlobBaseVelocity = 8f;
						float spitBlobRandomVelocityLimit = 2f;
						for (int j = 0; j < spitBlobAmount; j++)
						{
							int type2 = ModContent.ProjectileType<IchorBlob>();
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spitLocation + Main.rand.NextVector2CircularEdge(8f, 8f), base.NPC.velocity.SafeNormalize(Vector2.UnitY) * spitBlobBaseVelocity + Main.rand.NextVector2CircularEdge(spitBlobRandomVelocityLimit, spitBlobRandomVelocityLimit), type2, PerforatorHive.IchorShotDamage, 0f, Main.myPlayer, 0f, player.Center.Y);
						}
					}
				}
			}
		}
		if (Main.netMode != 1 && !TailSpawned)
		{
			int Previous = base.NPC.whoAmI;
			int maxLength = (death ? 27 : (revenge ? 24 : (expertMode ? 21 : 15)));
			for (int segments = 0; segments < maxLength; segments++)
			{
				int lol = ((segments < 0 || segments >= maxLength - 1) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PerforatorTailLarge>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PerforatorBodyLarge>(), base.NPC.whoAmI));
				if (segments % 2 == 0)
				{
					Main.npc[lol].localAI[3] = 1f;
				}
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
			}
			TailSpawned = true;
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
		bool shouldFly = false;
		if (!shouldFly)
		{
			Vector2 vector2 = default(Vector2);
			for (int l = tilePositionX; l < tileWidthPosX; l++)
			{
				for (int m = tilePositionY; m < tileWidthPosY; m++)
				{
					if (Main.tile[l, m] != null && ((Main.tile[l, m].HasUnactuatedTile && (Main.tileSolid[Main.tile[l, m].TileType] || (Main.tileSolidTop[Main.tile[l, m].TileType] && Main.tile[l, m].TileFrameY == 0))) || Main.tile[l, m].LiquidAmount > 64))
					{
						vector2.X = l * 16;
						vector2.Y = m * 16;
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
			int stopFlyingRadius = (death ? 160 : (revenge ? 200 : (expertMode ? 240 : 300)));
			bool outsideFlyingRadius = true;
			if (base.NPC.position.Y > player.position.Y)
			{
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				Rectangle rectangle2 = default(Rectangle);
				while (enumerator.MoveNext())
				{
					Player plr = enumerator.Current;
					((Rectangle)(ref rectangle2))._002Ector((int)plr.position.X - stopFlyingRadius, (int)plr.position.Y - stopFlyingRadius, stopFlyingRadius * 2, stopFlyingRadius * 2);
					if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
					{
						outsideFlyingRadius = false;
						break;
					}
				}
				if (outsideFlyingRadius)
				{
					shouldFly = true;
				}
			}
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		float maxChargeSpeed = 16f;
		if (player.dead || CalamityGlobalNPC.perfHive < 0 || !Main.npc[CalamityGlobalNPC.perfHive].active)
		{
			shouldFly = false;
			base.NPC.velocity.Y++;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y++;
				maxChargeSpeed *= 2f;
			}
			if ((double)base.NPC.position.Y > Main.rockLayer * 16.0)
			{
				for (int a = 0; a < Main.maxNPCs; a++)
				{
					if (Main.npc[a].type == ModContent.NPCType<PerforatorHeadLarge>() || Main.npc[a].type == ModContent.NPCType<PerforatorBodyLarge>() || Main.npc[a].type == ModContent.NPCType<PerforatorTailLarge>())
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		if (Main.zenithWorld)
		{
			float laserOffset = 1500f;
			float laserVelocity = 4f;
			int type3 = ModContent.ProjectileType<DoGDeath>();
			int damage2 = LaserWallDamage;
			base.NPC.Calamity().newAI[3]++;
			if (base.NPC.Calamity().newAI[3] > 180f)
			{
				if (base.NPC.Calamity().newAI[3] % 60f == 59f)
				{
					SoundEngine.PlaySound(in SoundID.Item12, player.Center);
					for (int n = -7; n < 8; n++)
					{
						float laserGap = (float)n * 128f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X + laserOffset, player.Center.Y + laserGap, 0f - laserVelocity, 0f, type3, damage2, 0f, Main.myPlayer);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X - laserOffset, player.Center.Y + laserGap, laserVelocity, 0f, type3, damage2, 0f, Main.myPlayer);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X + laserGap, player.Center.Y + laserOffset, 0f, 0f - laserVelocity, type3, damage2, 0f, Main.myPlayer);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X + laserGap, player.Center.Y + laserOffset, 0f, laserVelocity, type3, damage2, 0f, Main.myPlayer);
					}
				}
				if (base.NPC.Calamity().newAI[3] >= 300f)
				{
					base.NPC.Calamity().newAI[3] = -300f;
				}
			}
		}
		float speedCopy = speed;
		float turnSpeedCopy = turnSpeed;
		Vector2 npcCenter = base.NPC.Center;
		float playerX = player.Center.X;
		float targettingPosition = player.Center.Y;
		playerX = (int)(playerX / 16f) * 16;
		targettingPosition = (int)(targettingPosition / 16f) * 16;
		npcCenter.X = (int)(npcCenter.X / 16f) * 16;
		npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
		playerX -= npcCenter.X;
		targettingPosition -= npcCenter.Y;
		float targetDistance = (float)Math.Sqrt(playerX * playerX + targettingPosition * targettingPosition);
		if (!shouldFly)
		{
			base.NPC.TargetClosest();
			base.NPC.velocity.Y += 0.15f;
			if (base.NPC.velocity.Y > maxChargeSpeed)
			{
				base.NPC.velocity.Y = maxChargeSpeed;
			}
			bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > speedCopy;
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChargeSpeed * 0.4)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X -= speedCopy * 1.1f;
				}
				else
				{
					base.NPC.velocity.X += speedCopy * 1.1f;
				}
			}
			else if (base.NPC.velocity.Y == maxChargeSpeed)
			{
				if (slowXVelocity)
				{
					if (base.NPC.velocity.X < playerX)
					{
						base.NPC.velocity.X += speedCopy;
					}
					else if (base.NPC.velocity.X > playerX)
					{
						base.NPC.velocity.X -= speedCopy;
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
						base.NPC.velocity.X += speedCopy * 0.9f;
					}
					else
					{
						base.NPC.velocity.X -= speedCopy * 0.9f;
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
			float absoluteTargetX = Math.Abs(playerX);
			float absoluteTargetPos = Math.Abs(targettingPosition);
			float timeToReachTarget = maxChargeSpeed / targetDistance;
			playerX *= timeToReachTarget;
			targettingPosition *= timeToReachTarget;
			if (((base.NPC.velocity.X > 0f && playerX > 0f) || (base.NPC.velocity.X < 0f && playerX < 0f)) && ((base.NPC.velocity.Y > 0f && targettingPosition > 0f) || (base.NPC.velocity.Y < 0f && targettingPosition < 0f)))
			{
				if (base.NPC.velocity.X < playerX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + turnSpeedCopy;
				}
				else if (base.NPC.velocity.X > playerX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - turnSpeedCopy;
				}
				if (base.NPC.velocity.Y < targettingPosition)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + turnSpeedCopy;
				}
				else if (base.NPC.velocity.Y > targettingPosition)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - turnSpeedCopy;
				}
			}
			if ((base.NPC.velocity.X > 0f && playerX > 0f) || (base.NPC.velocity.X < 0f && playerX < 0f) || (base.NPC.velocity.Y > 0f && targettingPosition > 0f) || (base.NPC.velocity.Y < 0f && targettingPosition < 0f))
			{
				if (base.NPC.velocity.X < playerX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + speedCopy;
				}
				else if (base.NPC.velocity.X > playerX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - speedCopy;
				}
				if (base.NPC.velocity.Y < targettingPosition)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + speedCopy;
				}
				else if (base.NPC.velocity.Y > targettingPosition)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - speedCopy;
				}
				if ((double)Math.Abs(targettingPosition) < (double)maxChargeSpeed * 0.2 && ((base.NPC.velocity.X > 0f && playerX < 0f) || (base.NPC.velocity.X < 0f && playerX > 0f)))
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y + speedCopy * 2f;
					}
					else
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - speedCopy * 2f;
					}
				}
				if ((double)Math.Abs(playerX) < (double)maxChargeSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && targettingPosition < 0f) || (base.NPC.velocity.Y < 0f && targettingPosition > 0f)))
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + speedCopy * 2f;
					}
					else
					{
						base.NPC.velocity.X = base.NPC.velocity.X - speedCopy * 2f;
					}
				}
			}
			else if (absoluteTargetX > absoluteTargetPos)
			{
				if (base.NPC.velocity.X < playerX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + speedCopy * 1.1f;
				}
				else if (base.NPC.velocity.X > playerX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - speedCopy * 1.1f;
				}
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChargeSpeed * 0.5)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y + speedCopy;
					}
					else
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - speedCopy;
					}
				}
			}
			else
			{
				if (base.NPC.velocity.Y < targettingPosition)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + speedCopy * 1.1f;
				}
				else if (base.NPC.velocity.Y > targettingPosition)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - speedCopy * 1.1f;
				}
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChargeSpeed * 0.5)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + speedCopy;
					}
					else
					{
						base.NPC.velocity.X = base.NPC.velocity.X - speedCopy;
					}
				}
			}
		}
		if (base.NPC.Distance(player.Center) > 1280f)
		{
			NPC nPC = base.NPC;
			nPC.velocity += (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * turnSpeed;
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
		if (base.NPC.alpha > 0 && base.NPC.life > 0)
		{
			for (int dustIndex = 0; dustIndex < 2; dustIndex++)
			{
				int dust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].noLight = true;
			}
		}
		Vector2 val = base.NPC.position - base.NPC.oldPosition;
		if (((Vector2)(ref val)).Length() > 2f)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<PerforatorBodyLarge>()].Value, PerforatorBodyLarge.AltTexture.Value, 3, 40, 0.3f, Vector2.Zero, 3, 10f);
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color glowmaskColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LargePerf").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LargePerf2").Type, base.NPC.scale);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 188;
	}

	public override bool SpecialOnKill()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<PerforatorHeadLarge>(), ModContent.NPCType<PerforatorBodyLarge>(), ModContent.NPCType<PerforatorTailLarge>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void OnKill()
	{
		int heartAmt = Main.rand.Next(3) + 3;
		for (int i = 0; i < heartAmt; i++)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BurningBlood>(), 300);
		}
	}
}
