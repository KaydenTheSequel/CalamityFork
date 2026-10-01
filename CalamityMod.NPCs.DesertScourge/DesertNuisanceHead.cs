using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DesertScourge;

[AutoloadBossHead]
[LongDistanceNetSync]
public class DesertNuisanceHead : ModNPC
{
	private int biomeEnrageTimer = 300;

	public bool flies;

	private bool tailSpawned;

	public const float SegmentVelocity_Expert = 11f;

	public const float SegmentVelocity_Death = 13.5f;

	public const float SegmentVelocity_GoodWorld = 19f;

	public const float SegmentVelocity_ZenithSeed = 22f;

	public const float OpenMouthForBiteDistance = 220f;

	private const int OpenMouthStopFrame = 4;

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
		value.Position.X += 0f;
		value.Position.Y -= 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 25;
		base.NPC.defense = 3;
		if (Main.getGoodWorld)
		{
			base.NPC.defense += 19;
		}
		base.NPC.width = 88;
		base.NPC.height = 88;
		base.NPC.LifeMaxNERB(1500, 1800, 40000);
		if (Main.getGoodWorld)
		{
			base.NPC.lifeMax *= 2;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.netAlways = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 2f;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(biomeEnrageTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		biomeEnrageTimer = reader.ReadInt32();
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<DesertScourgeHead>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.DesertNuisance")
		});
	}

	public override void AI()
	{
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1319: Unknown result type (might be due to invalid IL or missing references)
		//IL_131e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1326: Unknown result type (might be due to invalid IL or missing references)
		//IL_132b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1330: Unknown result type (might be due to invalid IL or missing references)
		//IL_1335: Unknown result type (might be due to invalid IL or missing references)
		//IL_133c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1341: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool getMad = (!NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHeadYoung>()) & revenge) | death;
		if (!Main.player[base.NPC.target].ZoneDesert && !BossRushEvent.BossRushActive)
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
		float enrageScale = (getMad ? 0.5f : 0f);
		if (num)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 2f;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float speed = (death ? 0.105f : 0.085f);
		float turnSpeed = (death ? 0.21f : 0.17f);
		speed += speed * 0.4f * (1f - lifeRatio);
		turnSpeed += turnSpeed * 0.4f * (1f - lifeRatio);
		speed += 0.085f * enrageScale;
		turnSpeed += 0.17f * enrageScale;
		if (Main.getGoodWorld)
		{
			speed *= 1.1f;
			turnSpeed *= 1.2f;
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (Main.netMode != 1 && !tailSpawned && base.NPC.ai[0] == 0f)
		{
			int previous = base.NPC.whoAmI;
			int minLength = 8;
			if (Main.getGoodWorld)
			{
				minLength *= 2;
			}
			int bodyTypeAIVariable = 0;
			for (int i = 0; i < minLength + 1; i++)
			{
				int lol;
				if (i >= 0 && i < minLength)
				{
					bodyTypeAIVariable = ((i != 0) ? ((i == minLength - 1) ? 30 : ((i % 2 != 0) ? 10 : 20)) : 0);
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<DesertNuisanceBody>(), base.NPC.whoAmI);
					Main.npc[lol].ai[3] = bodyTypeAIVariable;
				}
				else
				{
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<DesertNuisanceTail>(), base.NPC.whoAmI);
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
			int directChaseDistance = (death ? 400 : (revenge ? 500 : 1000));
			bool shouldDirectlyChase = true;
			if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y)
			{
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				Rectangle rectangle2 = default(Rectangle);
				while (enumerator.MoveNext())
				{
					Player plr = enumerator.Current;
					((Rectangle)(ref rectangle2))._002Ector((int)plr.position.X - directChaseDistance, (int)plr.position.Y - directChaseDistance, directChaseDistance * 2, directChaseDistance * 2);
					if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
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
		float maxChaseSpeed = (Main.zenithWorld ? 22f : (Main.getGoodWorld ? 19f : (death ? 13.5f : 11f)));
		maxChaseSpeed += maxChaseSpeed * 0.2f * (1f - lifeRatio);
		if (death)
		{
			maxChaseSpeed += maxChaseSpeed * 0.2f * (1f - lifeRatio);
		}
		if (Main.player[base.NPC.target].dead)
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
					if (Main.npc[a].type == ModContent.NPCType<DesertNuisanceHead>() || Main.npc[a].type == ModContent.NPCType<DesertNuisanceBody>() || Main.npc[a].type == ModContent.NPCType<DesertNuisanceTail>())
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		Vector2 npcCenter = base.NPC.Center;
		float playerX = Main.player[base.NPC.target].Center.X;
		float targettingPosition = Main.player[base.NPC.target].Center.Y;
		playerX = (int)(playerX / 16f) * 16;
		targettingPosition = (int)(targettingPosition / 16f) * 16;
		npcCenter.X = (int)(npcCenter.X / 16f) * 16;
		npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
		playerX -= npcCenter.X;
		targettingPosition -= npcCenter.Y;
		float targetDistance = (float)Math.Sqrt(playerX * playerX + targettingPosition * targettingPosition);
		if (!shouldFly)
		{
			base.NPC.velocity.Y += (death ? 0.125f : 0.1f);
			if (base.NPC.velocity.Y > 0f && Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) > 180f)
			{
				base.NPC.velocity.Y += 0.05f;
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
		Vector2 destination = Main.player[base.NPC.target].Center;
		if (base.NPC.Distance(destination) > (getMad ? 750f : 1000f))
		{
			NPC nPC = base.NPC;
			nPC.velocity += (destination - base.NPC.Center).SafeNormalize(Vector2.UnitY) * turnSpeed;
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
		return minDist <= 50f * base.NPC.scale;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeNuisanceHead").Type, base.NPC.scale);
			}
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
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
		else if (openMouth)
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
			int animationSpeed = 8;
			float wormTimer = base.NPC.Calamity().bestiaryWormTimer;
			for (int i = 3; i > 0; i--)
			{
				float bodyOffset = (float)(i * segmentSpacing) - (float)segmentSpacing * 0.5f;
				Texture2D toUse = ((i == 1) ? TextureAssets.Npc[ModContent.NPCType<DesertNuisanceBody>()].Value : DesertNuisanceBody.BodyTexture2.Value);
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
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.7f * balance);
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
			return Color.Orange * (float)(int)((Color)(ref drawColor)).A * base.NPC.Opacity;
		}
		return null;
	}
}
