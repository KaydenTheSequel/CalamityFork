using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
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
public class PerforatorHeadSmall : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfSmallHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfSmallDeath");

	public static Asset<Texture2D> GlowTexture;

	private bool TailSpawned;

	public override LocalizedText DeathMessage => CalamityUtils.GetText("NPCs.PerforatorSmall");

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.8f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.8f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 60f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 60f;
		value.Position.Y += 50f;
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
		base.NPC.damage = 22;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 42;
		base.NPC.height = 62;
		base.NPC.LifeMaxNERB(900, 1150, 50000);
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

	public override void AI()
	{
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0958: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1204: Unknown result type (might be due to invalid IL or missing references)
		//IL_120f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1214: Unknown result type (might be due to invalid IL or missing references)
		//IL_1219: Unknown result type (might be due to invalid IL or missing references)
		//IL_121e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1225: Unknown result type (might be due to invalid IL or missing references)
		//IL_122a: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Unknown result type (might be due to invalid IL or missing references)
		//IL_143d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1448: Unknown result type (might be due to invalid IL or missing references)
		//IL_144d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1452: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1402: Unknown result type (might be due to invalid IL or missing references)
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float speed = (revenge ? 0.2f : 0.15f);
		float turnSpeed = (revenge ? 0.15f : 0.1f);
		if (expertMode)
		{
			float velocityScale = (death ? 0.2f : 0.14f);
			speed += velocityScale * (1f - lifeRatio);
			float accelerationScale = (death ? 0.15f : 0.1f);
			turnSpeed += accelerationScale * (1f - lifeRatio);
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (Main.netMode != 1 && !TailSpawned)
		{
			int Previous = base.NPC.whoAmI;
			int maxLength = (death ? 9 : (revenge ? 8 : (expertMode ? 7 : 5)));
			for (int segments = 0; segments < maxLength; segments++)
			{
				int lol = ((segments < 0 || segments >= maxLength - 1) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PerforatorTailSmall>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<PerforatorBodySmall>(), base.NPC.whoAmI));
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
			for (int k = tilePositionX; k < tileWidthPosX; k++)
			{
				for (int l = tilePositionY; l < tileWidthPosY; l++)
				{
					if (Main.tile[k, l] != null && ((Main.tile[k, l].HasUnactuatedTile && (Main.tileSolid[Main.tile[k, l].TileType] || (Main.tileSolidTop[Main.tile[k, l].TileType] && Main.tile[k, l].TileFrameY == 0))) || Main.tile[k, l].LiquidAmount > 64))
					{
						vector2.X = k * 16;
						vector2.Y = l * 16;
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
			int stopFlyingRadius = (death ? 320 : (revenge ? 400 : (expertMode ? 480 : 600)));
			bool outsideFlyRadius = true;
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
						outsideFlyRadius = false;
						break;
					}
				}
				if (outsideFlyRadius)
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
					if (Main.npc[a].type == ModContent.NPCType<PerforatorHeadSmall>() || Main.npc[a].type == ModContent.NPCType<PerforatorBodySmall>() || Main.npc[a].type == ModContent.NPCType<PerforatorTailSmall>())
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		if (Main.zenithWorld && CalamityGlobalNPC.perfHive >= 0)
		{
			base.NPC.ai[0]++;
		}
		Vector2 center2;
		if (base.NPC.ai[0] >= 300f && CalamityGlobalNPC.perfHive >= 0)
		{
			shouldFly = false;
			NPC Hive = Main.npc[CalamityGlobalNPC.perfHive];
			Vector2 targetDistance = Hive.Center - base.NPC.Center;
			float velocity = 16f;
			float acceleration = 0.15f;
			if (((Vector2)(ref targetDistance)).Length() > 32f)
			{
				CalamityUtils.SmoothMovement(base.NPC, 0f, targetDistance, velocity, acceleration, useSimpleFlyMovement: true);
			}
			else
			{
				NPC nPC = base.NPC;
				Vector2 center = Hive.Center;
				Vector2 spinningpoint = Vector2.UnitY * 4f;
				double radians = base.NPC.rotation;
				center2 = default(Vector2);
				nPC.Center = center + spinningpoint.RotatedBy(radians, center2);
				Hive.localAI[1]++;
			}
			if (base.NPC.ai[0] >= 600f)
			{
				base.NPC.ai[0] = 0f;
				Hive.localAI[1] = 0f;
			}
		}
		else
		{
			float speedCopy = speed;
			float turnSpeedCopy = turnSpeed;
			Vector2 npcCenter = base.NPC.Center;
			float targetX = player.Center.X;
			float targetY = player.Center.Y;
			targetX = (int)(targetX / 16f) * 16;
			targetY = (int)(targetY / 16f) * 16;
			npcCenter.X = (int)(npcCenter.X / 16f) * 16;
			npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
			targetX -= npcCenter.X;
			targetY -= npcCenter.Y;
			float targetDistance2 = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
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
						if (base.NPC.velocity.X < targetX)
						{
							base.NPC.velocity.X += speedCopy;
						}
						else if (base.NPC.velocity.X > targetX)
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
					float soundDelay = targetDistance2 / 40f;
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
				targetDistance2 = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
				float absoluteTargetX = Math.Abs(targetX);
				float absoluteTargetY = Math.Abs(targetY);
				float timeToReachTarget = maxChargeSpeed / targetDistance2;
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
		}
		if (base.NPC.Distance(player.Center) > 1280f)
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity += (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * turnSpeed;
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
				int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = true;
			}
		}
		center2 = base.NPC.position - base.NPC.oldPosition;
		if (((Vector2)(ref center2)).Length() > 2f)
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
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<PerforatorBodySmall>()].Value, 5, 22, 0.6f, new Vector2(-20f, -20f), 4, 10f);
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 glowmaskDrawLocation = base.NPC.Center - screenPos;
		glowmaskDrawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		glowmaskDrawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, glowmaskDrawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color glowmaskColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
		spriteBatch.Draw(texture2D15, glowmaskDrawLocation, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
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
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SmallPerf").Type, base.NPC.scale);
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
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<PerforatorHeadSmall>(), ModContent.NPCType<PerforatorBodySmall>(), ModContent.NPCType<PerforatorTailSmall>());
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
			target.AddBuff(ModContent.BuffType<BurningBlood>(), 240);
		}
	}
}
