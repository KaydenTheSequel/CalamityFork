using System;
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
public class PerforatorHeadMedium : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfMediumHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfMediumDeath");

	public static Asset<Texture2D> GlowTexture;

	public override LocalizedText DeathMessage => CalamityUtils.GetText("NPCs.PerforatorMedium");

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 60f;
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
		base.NPC.damage = 24;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 58;
		base.NPC.height = 68;
		base.NPC.defense = 2;
		base.NPC.LifeMaxNERB(130, 170, 7000);
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
		base.NPC.Calamity().SplittingWorm = true;
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

	public override void AI()
	{
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1300: Unknown result type (might be due to invalid IL or missing references)
		//IL_1305: Unknown result type (might be due to invalid IL or missing references)
		//IL_130a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1311: Unknown result type (might be due to invalid IL or missing references)
		//IL_1316: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1542: Unknown result type (might be due to invalid IL or missing references)
		//IL_154d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1552: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1501: Unknown result type (might be due to invalid IL or missing references)
		//IL_1507: Unknown result type (might be due to invalid IL or missing references)
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float totalSegments = GetMediumPerforatorSegmentsCount();
		float lifeRatio = MathHelper.Clamp((float)NPC.CountNPCS(ModContent.NPCType<PerforatorBodyMedium>()) / totalSegments, 0f, 1f);
		float speed = 0.125f;
		float turnSpeed = 0.085f;
		if (expertMode)
		{
			float velocityScale = (death ? 0.125f : 0.085f);
			speed += velocityScale * (1f - lifeRatio);
			float accelerationScale = (death ? 0.085f : 0.06f);
			turnSpeed += accelerationScale * (1f - lifeRatio);
		}
		base.NPC.realLife = -1;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (Main.netMode != 1)
		{
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[2] = totalSegments;
				base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), ModContent.NPCType<PerforatorBodyMedium>(), base.NPC.whoAmI);
				Main.npc[(int)base.NPC.ai[0]].ai[1] = base.NPC.whoAmI;
				Main.npc[(int)base.NPC.ai[0]].ai[2] = base.NPC.ai[2] - 1f;
				base.NPC.netUpdate = true;
			}
			bool spawnedBlob = false;
			if (!Main.npc[(int)base.NPC.ai[1]].active && !Main.npc[(int)base.NPC.ai[0]].active)
			{
				if (death)
				{
					spawnedBlob = true;
					int type = ModContent.ProjectileType<IchorBlob>();
					Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity + Main.rand.NextVector2CircularEdge(3f, 3f), type, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer, 0f, base.NPC.Center.Y);
				}
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.checkDead();
				base.NPC.active = false;
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
			if (!Main.npc[(int)base.NPC.ai[0]].active)
			{
				if (death && !spawnedBlob)
				{
					int type2 = ModContent.ProjectileType<IchorBlob>();
					Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity + Main.rand.NextVector2CircularEdge(3f, 3f), type2, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer, 0f, base.NPC.Center.Y);
				}
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.checkDead();
				base.NPC.active = false;
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
			if (!base.NPC.active && Main.dedServ)
			{
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
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
			Vector2 vector = default(Vector2);
			for (int i = tilePositionX; i < tileWidthPosX; i++)
			{
				for (int j = tilePositionY; j < tileWidthPosY; j++)
				{
					if (!(Main.tile[i, j] != null) || ((!Main.tile[i, j].HasUnactuatedTile || (!Main.tileSolid[Main.tile[i, j].TileType] && (!Main.tileSolidTop[Main.tile[i, j].TileType] || Main.tile[i, j].TileFrameY != 0))) && Main.tile[i, j].LiquidAmount <= 64))
					{
						continue;
					}
					vector.X = i * 16;
					vector.Y = j * 16;
					if (base.NPC.position.X + (float)base.NPC.width > vector.X && base.NPC.position.X < vector.X + 16f && base.NPC.position.Y + (float)base.NPC.height > vector.Y && base.NPC.position.Y < vector.Y + 16f)
					{
						shouldFly = true;
						if (Main.rand.NextBool(100) && Main.tile[i, j].HasUnactuatedTile)
						{
							WorldGen.KillTile(i, j, fail: true, effectOnly: true);
						}
					}
				}
			}
		}
		if (!shouldFly)
		{
			Rectangle rectangle = default(Rectangle);
			((Rectangle)(ref rectangle))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
			int stopFlyingRadius = (death ? 320 : (revenge ? 400 : (expertMode ? 480 : 600)));
			bool outsideFlyRadius = true;
			Rectangle rectangle2 = default(Rectangle);
			for (int k = 0; k < 255; k++)
			{
				if (Main.player[k].active)
				{
					((Rectangle)(ref rectangle2))._002Ector((int)Main.player[k].position.X - stopFlyingRadius, (int)Main.player[k].position.Y - stopFlyingRadius, stopFlyingRadius * 2, stopFlyingRadius * 2);
					if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
					{
						outsideFlyRadius = false;
						break;
					}
				}
			}
			if (outsideFlyRadius)
			{
				shouldFly = true;
			}
		}
		float maxChargeSpeed = 16f;
		if (player.dead || CalamityGlobalNPC.perfHive < 0 || !Main.npc[CalamityGlobalNPC.perfHive].active)
		{
			base.NPC.TargetClosest(faceTarget: false);
			shouldFly = false;
			base.NPC.velocity.Y++;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y++;
				maxChargeSpeed *= 2f;
			}
			if ((double)base.NPC.position.Y > Main.rockLayer * 16.0)
			{
				for (int p = 0; p < Main.maxNPCs; p++)
				{
					if (Main.npc[p].type == base.NPC.type || Main.npc[p].type == ModContent.NPCType<PerforatorBodyMedium>() || Main.npc[p].type == ModContent.NPCType<PerforatorTailMedium>())
					{
						Main.npc[p].active = false;
					}
				}
			}
		}
		float speedCopy = speed;
		float turnSpeedCopy = turnSpeed;
		Vector2 vector2 = base.NPC.Center;
		float targetX = player.Center.X;
		float targetY = player.Center.Y;
		targetX = (int)(targetX / 16f) * 16;
		targetY = (int)(targetY / 16f) * 16;
		vector2.X = (int)(vector2.X / 16f) * 16;
		vector2.Y = (int)(vector2.Y / 16f) * 16;
		targetX -= vector2.X;
		targetY -= vector2.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		if (base.NPC.Calamity().newAI[1] < 3f)
		{
			base.NPC.Calamity().newAI[1]++;
			base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * (speedCopy * (death ? 0.3f : 0.2f));
		}
		if (!shouldFly)
		{
			base.NPC.velocity.Y += 0.15f;
			if (base.NPC.velocity.Y > maxChargeSpeed)
			{
				base.NPC.velocity.Y = maxChargeSpeed;
			}
			bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > turnSpeedCopy;
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChargeSpeed * 0.4)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X -= turnSpeedCopy * 1.1f;
				}
				else
				{
					base.NPC.velocity.X += turnSpeedCopy * 1.1f;
				}
			}
			else if (base.NPC.velocity.Y == maxChargeSpeed)
			{
				if (slowXVelocity)
				{
					if (base.NPC.velocity.X < targetX)
					{
						base.NPC.velocity.X += turnSpeedCopy;
					}
					else if (base.NPC.velocity.X > targetX)
					{
						base.NPC.velocity.X -= turnSpeedCopy;
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
						base.NPC.velocity.X += turnSpeedCopy * 0.9f;
					}
					else
					{
						base.NPC.velocity.X -= turnSpeedCopy * 0.9f;
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
			targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			float absoluteTargetX = Math.Abs(targetX);
			float absoluteTargetY = Math.Abs(targetY);
			float timeToReachTarget = maxChargeSpeed / targetDistance;
			targetX *= timeToReachTarget;
			targetY *= timeToReachTarget;
			if (((base.NPC.velocity.X > 0f && targetX > 0f) || (base.NPC.velocity.X < 0f && targetX < 0f)) && ((base.NPC.velocity.Y > 0f && targetY > 0f) || (base.NPC.velocity.Y < 0f && targetY < 0f)))
			{
				if (base.NPC.velocity.X < targetX)
				{
					base.NPC.velocity.X += turnSpeedCopy;
				}
				else if (base.NPC.velocity.X > targetX)
				{
					base.NPC.velocity.X -= turnSpeedCopy;
				}
				if (base.NPC.velocity.Y < targetY)
				{
					base.NPC.velocity.Y += turnSpeedCopy;
				}
				else if (base.NPC.velocity.Y > targetY)
				{
					base.NPC.velocity.Y -= turnSpeedCopy;
				}
			}
			if ((base.NPC.velocity.X > 0f && targetX > 0f) || (base.NPC.velocity.X < 0f && targetX < 0f) || (base.NPC.velocity.Y > 0f && targetY > 0f) || (base.NPC.velocity.Y < 0f && targetY < 0f))
			{
				if (base.NPC.velocity.X < targetX)
				{
					base.NPC.velocity.X += speedCopy;
				}
				else if (base.NPC.velocity.X > targetX)
				{
					base.NPC.velocity.X -= speedCopy;
				}
				if (base.NPC.velocity.Y < targetY)
				{
					base.NPC.velocity.Y += speedCopy;
				}
				else if (base.NPC.velocity.Y > targetY)
				{
					base.NPC.velocity.Y -= speedCopy;
				}
				if ((double)Math.Abs(targetY) < (double)maxChargeSpeed * 0.2 && ((base.NPC.velocity.X > 0f && targetX < 0f) || (base.NPC.velocity.X < 0f && targetX > 0f)))
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y += speedCopy * 2f;
					}
					else
					{
						base.NPC.velocity.Y -= speedCopy * 2f;
					}
				}
				if ((double)Math.Abs(targetX) < (double)maxChargeSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && targetY < 0f) || (base.NPC.velocity.Y < 0f && targetY > 0f)))
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X += speedCopy * 2f;
					}
					else
					{
						base.NPC.velocity.X -= speedCopy * 2f;
					}
				}
			}
			else if (absoluteTargetX > absoluteTargetY)
			{
				if (base.NPC.velocity.X < targetX)
				{
					base.NPC.velocity.X += speedCopy * 1.1f;
				}
				else if (base.NPC.velocity.X > targetX)
				{
					base.NPC.velocity.X -= speedCopy * 1.1f;
				}
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChargeSpeed * 0.5)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y += speedCopy;
					}
					else
					{
						base.NPC.velocity.Y -= speedCopy;
					}
				}
			}
			else
			{
				if (base.NPC.velocity.Y < targetY)
				{
					base.NPC.velocity.Y += speedCopy * 1.1f;
				}
				else if (base.NPC.velocity.Y > targetY)
				{
					base.NPC.velocity.Y -= speedCopy * 1.1f;
				}
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)maxChargeSpeed * 0.5)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X += speedCopy;
					}
					else
					{
						base.NPC.velocity.X -= speedCopy;
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
				int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, Main.rand.NextBool() ? 170 : 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = true;
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

	public static int GetMediumPerforatorSegmentsCount()
	{
		if (!Main.getGoodWorld)
		{
			if (!CalamityWorld.death && !BossRushEvent.BossRushActive)
			{
				if (!CalamityWorld.revenge)
				{
					if (!Main.expertMode)
					{
						return 10;
					}
					return 12;
				}
				return 13;
			}
			return 14;
		}
		return 20;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<PerforatorBodyMedium>()].Value, 5, 34, 0.3f, Vector2.Zero, 5, 6f);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MediumPerf").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MediumPerf2").Type, base.NPC.scale);
			}
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(4) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
		if (Main.netMode != 1 && Main.zenithWorld)
		{
			int type = ModContent.ProjectileType<IchorBlob>();
			Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Vector2.UnitY, type, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer);
			for (int i = -1; i < 2; i++)
			{
				int type2 = ModContent.ProjectileType<IchorShot>();
				Vector2 baseVelocity = Vector2.UnitY * Main.rand.NextFloat(-12.5f, -5f);
				int spread = Main.rand.Next(16, 36);
				Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, baseVelocity.RotatedBy(MathHelper.ToRadians((float)(spread * i))), type2, PerforatorHive.IchorShotDamage, 0f, Main.myPlayer);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 188;
	}

	public override bool SpecialOnKill()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, base.NPC.type, ModContent.NPCType<PerforatorBodyMedium>(), ModContent.NPCType<PerforatorTailMedium>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(69, 360);
		}
	}
}
