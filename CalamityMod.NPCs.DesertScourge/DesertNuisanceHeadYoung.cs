using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DesertScourge;

[AutoloadBossHead]
[LongDistanceNetSync]
public class DesertNuisanceHeadYoung : ModNPC
{
	private int biomeEnrageTimer = 300;

	public bool flies;

	private bool tailSpawned;

	public const float SegmentVelocity_Expert = 10f;

	public const float SegmentVelocity_Death = 12f;

	public const float SegmentVelocity_GoodWorld = 16f;

	public const float SegmentVelocity_ZenithSeed = 18f;

	public const float SpitGateValue = 180f;

	public const float OpenMouthForBiteDistance = 220f;

	private const int OpenMouthStopFrame = 4;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		this.HideFromBestiary();
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 25;
		base.NPC.defense = 2;
		if (Main.getGoodWorld)
		{
			base.NPC.defense += 18;
		}
		base.NPC.width = 78;
		base.NPC.height = 78;
		base.NPC.LifeMaxNERB(1300, 1560, 35000);
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
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		biomeEnrageTimer = reader.ReadInt32();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
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
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1527: Unknown result type (might be due to invalid IL or missing references)
		//IL_152c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1534: Unknown result type (might be due to invalid IL or missing references)
		//IL_1553: Unknown result type (might be due to invalid IL or missing references)
		//IL_1558: Unknown result type (might be due to invalid IL or missing references)
		//IL_1560: Unknown result type (might be due to invalid IL or missing references)
		//IL_1565: Unknown result type (might be due to invalid IL or missing references)
		//IL_156a: Unknown result type (might be due to invalid IL or missing references)
		//IL_156f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1576: Unknown result type (might be due to invalid IL or missing references)
		//IL_157b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1580: Unknown result type (might be due to invalid IL or missing references)
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool getMad = (!NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHead>()) & revenge) | death;
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
		float speed = (death ? 0.09f : 0.07f);
		float turnSpeed = (death ? 0.18f : 0.14f);
		speed += speed * 0.4f * (1f - lifeRatio);
		turnSpeed += turnSpeed * 0.4f * (1f - lifeRatio);
		speed += 0.07f * enrageScale;
		turnSpeed += 0.14f * enrageScale;
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
		if (base.NPC.Distance(Main.player[base.NPC.target].Center) > 360f)
		{
			if ((Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation().AngleTowards(base.NPC.velocity.ToRotation(), (float)Math.PI / 4f) == base.NPC.velocity.ToRotation())
			{
				base.NPC.Calamity().newAI[0]++;
			}
			if (base.NPC.Calamity().newAI[0] >= 180f)
			{
				base.NPC.Calamity().newAI[0] = 0f;
				SoundEngine.PlaySound(in SoundID.NPCDeath11, base.NPC.Center);
				if (Main.netMode != 1)
				{
					Vector2 projectileVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * (revenge ? 10f : 8f);
					int numProj = (death ? 6 : (getMad ? 4 : 3));
					float rotation = MathHelper.ToRadians((float)(death ? 28 : (getMad ? 22 : 20)));
					int type = ModContent.ProjectileType<DesertScourgeSpit>();
					for (int i = 0; i < numProj; i++)
					{
						Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
						for (int k = 0; k < 10; k++)
						{
							int dust = Dust.NewDust(base.NPC.Center + Vector2.Normalize(perturbedSpeed) * 5f, 10, 10, 75);
							Main.dust[dust].velocity = perturbedSpeed;
						}
						if (Main.netMode != 1)
						{
							int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 5f, perturbedSpeed, type, DesertScourgeHead.SpitDamage, 0f, Main.myPlayer);
							Main.projectile[proj].aiStyle = -1;
							Main.projectile[proj].netUpdate = true;
						}
					}
				}
			}
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
			for (int j = 0; j < minLength + 1; j++)
			{
				int lol;
				if (j >= 0 && j < minLength)
				{
					bodyTypeAIVariable = ((j != 0) ? ((j == minLength - 1) ? 30 : ((j % 2 != 0) ? 10 : 20)) : 0);
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<DesertNuisanceBodyYoung>(), base.NPC.whoAmI);
					Main.npc[lol].ai[3] = bodyTypeAIVariable;
				}
				else
				{
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<DesertNuisanceTailYoung>(), base.NPC.whoAmI);
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
			int directChaseDistance = (death ? 400 : (revenge ? 500 : 1000));
			bool shouldDirectlyChase = true;
			if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y)
			{
				Rectangle rectangle2 = default(Rectangle);
				for (int n = 0; n < 255; n++)
				{
					if (Main.player[n].active)
					{
						((Rectangle)(ref rectangle2))._002Ector((int)Main.player[n].position.X - directChaseDistance, (int)Main.player[n].position.Y - directChaseDistance, directChaseDistance * 2, directChaseDistance * 2);
						if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
						{
							shouldDirectlyChase = false;
							break;
						}
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
		float maxChaseSpeed = (Main.zenithWorld ? 18f : (Main.getGoodWorld ? 16f : (death ? 12f : 10f)));
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
					if (Main.npc[a].type == ModContent.NPCType<DesertNuisanceHeadYoung>() || Main.npc[a].type == ModContent.NPCType<DesertNuisanceBodyYoung>() || Main.npc[a].type == ModContent.NPCType<DesertNuisanceTailYoung>())
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
			base.NPC.velocity.Y += 0.1f;
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
		return minDist <= 45f;
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeNuisance2Head").Type, base.NPC.scale);
			}
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		bool aboutToSpitSpread = base.NPC.Calamity().newAI[0] > 150f;
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
