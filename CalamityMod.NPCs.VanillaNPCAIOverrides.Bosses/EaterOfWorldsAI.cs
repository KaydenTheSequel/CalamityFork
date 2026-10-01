using System;
using CalamityMod.Events;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class EaterOfWorldsAI : VanillaAIOverride
{
	private const float ProjectileTelegraphDuration = 30f;

	private const int TotalDeathModeWorms = 4;

	public const float DRIncreaseTime = 600f;

	public static float HeadDamageMult = 1.25f;

	public static float BodyDamageMult = 1.5f;

	public static float TailDamageMult = 1.5f;

	public static int FireballDamage = 12;

	public override bool AI(Mod mod)
	{
		//IL_140f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1414: Unknown result type (might be due to invalid IL or missing references)
		//IL_214f: Unknown result type (might be due to invalid IL or missing references)
		//IL_215a: Unknown result type (might be due to invalid IL or missing references)
		//IL_215f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2164: Unknown result type (might be due to invalid IL or missing references)
		//IL_226c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2277: Unknown result type (might be due to invalid IL or missing references)
		//IL_227c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2281: Unknown result type (might be due to invalid IL or missing references)
		//IL_2200: Unknown result type (might be due to invalid IL or missing references)
		//IL_222b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2231: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_122a: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1263: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_124b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1272: Unknown result type (might be due to invalid IL or missing references)
		//IL_1277: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1488: Unknown result type (might be due to invalid IL or missing references)
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_133e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1354: Unknown result type (might be due to invalid IL or missing references)
		//IL_135e: Unknown result type (might be due to invalid IL or missing references)
		//IL_137c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1386: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14be: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_179e: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eea: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * ((base.NPC.type == 13) ? HeadDamageMult : ((base.NPC.type == 14) ? BodyDamageMult : TailDamageMult)));
		if ((((base.NPC.ai[2] % 2f == 0f && base.NPC.type == 14) || base.NPC.type == 13) & death) || Main.getGoodWorld)
		{
			calamityGlobalNPC.DR = 0.5f;
			base.NPC.defense = base.NPC.defDefense * 2;
		}
		if (Main.getGoodWorld && base.NPC.type == 13)
		{
			base.NPC.reflectsProjectiles = true;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		float totalSegments = GetEaterOfWorldsSegmentsCountRevDeath();
		float lifeRatio = MathHelper.Clamp((float)NPC.CountNPCS(14) / totalSegments, 0f, 1f);
		bool phase2 = (lifeRatio < 0.8f) | death;
		bool phase3 = (lifeRatio < 0.4f) | death;
		bool phase4 = lifeRatio < (death ? 0.5f : 0.2f);
		bool phase5 = (lifeRatio < 0.1f) & death;
		bool phase6 = (lifeRatio < 0.05f) & death;
		if (Main.netMode != 1)
		{
			if (base.NPC.type == 14)
			{
				if (Collision.CanHitLine(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.localAI[1]++;
				}
				else
				{
					base.NPC.localAI[1]--;
				}
				int vileSpitGateValue = (int)MathHelper.Lerp(death ? 45f : 90f, 900f, lifeRatio);
				if (Main.getGoodWorld)
				{
					vileSpitGateValue = (int)((float)vileSpitGateValue * 0.5f);
				}
				Vector2 vileSpitShootLocation = base.NPC.Center + base.NPC.velocity;
				if (base.NPC.localAI[1] >= (float)vileSpitGateValue)
				{
					base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
					if (Collision.CanHitLine(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vileSpitShootLocation.X, (int)vileSpitShootLocation.Y, 666, 0, 0f, 1f);
					}
					base.NPC.localAI[1] = 0f;
				}
				if (base.NPC.localAI[1] > (float)vileSpitGateValue - 30f)
				{
					Dust dust = Dust.NewDustDirect(vileSpitShootLocation + Main.rand.NextVector2CircularEdge(5f, 5f), 1, 1, 18, base.NPC.velocity.X * 0.1f, base.NPC.velocity.Y * 0.1f, 80, default(Color), 2f);
					dust.noGravity = true;
					dust.velocity *= 0.3f;
				}
			}
			else if (base.NPC.type == 13 && phase2)
			{
				float timer = 120f;
				float shootBoost = lifeRatio * 90f;
				timer += shootBoost;
				float showTelegraphGateValue = timer - 30f;
				if (Collision.CanHitLine(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					if ((base.NPC.justHit & death) && calamityGlobalNPC.newAI[0] < showTelegraphGateValue)
					{
						calamityGlobalNPC.newAI[0] += 10f;
						if (calamityGlobalNPC.newAI[0] > showTelegraphGateValue)
						{
							calamityGlobalNPC.newAI[0] = showTelegraphGateValue;
						}
					}
					else
					{
						calamityGlobalNPC.newAI[0]++;
					}
				}
				else
				{
					calamityGlobalNPC.newAI[0]--;
				}
				if (calamityGlobalNPC.newAI[0] >= timer && Collision.CanHitLine(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1) && (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation().AngleTowards(base.NPC.velocity.ToRotation(), (float)Math.PI / 4f) == base.NPC.velocity.ToRotation())
				{
					calamityGlobalNPC.newAI[0] = 0f;
					Vector2 cursedFlameDirection = base.NPC.Center.DirectionTo(Main.player[base.NPC.target].Center) * 7f + base.NPC.velocity * 0.5f;
					int type = ((death & phase3) ? ModContent.ProjectileType<ShadowflameFireball>() : 96);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + base.NPC.velocity, cursedFlameDirection, type, FireballDamage, 0f, Main.myPlayer);
				}
				if (calamityGlobalNPC.newAI[0] > showTelegraphGateValue)
				{
					Vector2 position = base.NPC.Center + Main.rand.NextVector2CircularEdge(10f, 10f);
					int dustType = ((death & phase3) ? 27 : 75);
					Dust dust2 = Dust.NewDustDirect(position, 1, 1, dustType, 0f, 0f, 0, default(Color), 3f);
					dust2.noGravity = true;
					dust2.velocity *= 0f;
				}
			}
		}
		if (Main.player[base.NPC.target].dead && base.NPC.timeLeft > 300)
		{
			base.NPC.timeLeft = 300;
		}
		if (Main.netMode != 1)
		{
			if ((base.NPC.type == 13 || base.NPC.type == 14) && base.NPC.ai[0] == 0f)
			{
				int spawnX = (int)base.NPC.position.X;
				int spawnY = (int)base.NPC.position.Y;
				if (base.NPC.type == 13)
				{
					int segmentSpawnAmount = (int)(death ? (totalSegments / 4f) : totalSegments);
					if (death)
					{
						Vector2 additionalWormSpawnLocation = default(Vector2);
						((Vector2)(ref additionalWormSpawnLocation))._002Ector((float)spawnX, (float)spawnY);
						int randomXLimit = 80;
						int randomYLimit = 80;
						for (int i = 1; i < 4; i++)
						{
							additionalWormSpawnLocation += new Vector2((float)(Main.rand.Next(randomXLimit + 1) + randomXLimit) * (Main.rand.NextBool() ? (-1f) : 1f), (float)(Main.rand.Next(randomYLimit + 1) + randomYLimit));
							int wormHead = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)additionalWormSpawnLocation.X, (int)additionalWormSpawnLocation.Y, 13, base.NPC.whoAmI + segmentSpawnAmount * i + 1);
							Main.npc[wormHead].ai[2] = segmentSpawnAmount;
							Main.npc[wormHead].ai[0] = NPC.NewNPC(Main.npc[wormHead].GetSource_FromAI(), (int)additionalWormSpawnLocation.X, (int)additionalWormSpawnLocation.Y, 14, Main.npc[wormHead].whoAmI);
							Main.npc[(int)Main.npc[wormHead].ai[0]].ai[1] = Main.npc[wormHead].whoAmI;
							Main.npc[(int)Main.npc[wormHead].ai[0]].ai[2] = Main.npc[wormHead].ai[2] - 1f;
							Main.npc[wormHead].netUpdate = true;
						}
					}
					base.NPC.ai[2] = segmentSpawnAmount;
					base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX, spawnY, 14, base.NPC.whoAmI);
				}
				else if (base.NPC.type == 14 && base.NPC.ai[2] > 0f)
				{
					base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX, spawnY, 14, base.NPC.whoAmI);
				}
				else
				{
					base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX, spawnY, 15, base.NPC.whoAmI);
				}
				Main.npc[(int)base.NPC.ai[0]].ai[1] = base.NPC.whoAmI;
				Main.npc[(int)base.NPC.ai[0]].ai[2] = base.NPC.ai[2] - 1f;
				base.NPC.netUpdate = true;
			}
			if (!Main.npc[(int)base.NPC.ai[1]].active && !Main.npc[(int)base.NPC.ai[0]].active)
			{
				DestroyThisSegment();
			}
			if (base.NPC.type == 13 && !Main.npc[(int)base.NPC.ai[0]].active)
			{
				DestroyThisSegment();
			}
			if (base.NPC.type == 15 && !Main.npc[(int)base.NPC.ai[1]].active)
			{
				DestroyThisSegment();
			}
			if (base.NPC.type == 14 && (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != base.NPC.aiStyle))
			{
				base.NPC.type = 13;
				float segmentLifeRatio = MathHelper.Lerp(0.5f, 1f, (float)base.NPC.life / (float)base.NPC.lifeMax);
				int whoAmI = base.NPC.whoAmI;
				float ai0Holdover = base.NPC.ai[0];
				float newAI1Holdover = calamityGlobalNPC.newAI[1];
				int slowingDebuffResistTimer = calamityGlobalNPC.debuffResistanceTimer;
				base.NPC.SetDefaultsKeepPlayerInteraction(base.NPC.type);
				base.NPC.life = (int)((float)base.NPC.lifeMax * segmentLifeRatio);
				base.NPC.whoAmI = whoAmI;
				base.NPC.ai[0] = ai0Holdover;
				CalamityGlobalNPC calamityGlobalNPC2 = base.NPC.Calamity();
				calamityGlobalNPC2.newAI[1] = newAI1Holdover;
				calamityGlobalNPC2.debuffResistanceTimer = slowingDebuffResistTimer;
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				base.NPC.ForceNetUpdate();
				base.NPC.alpha = 0;
			}
			if (base.NPC.type == 14 && (!Main.npc[(int)base.NPC.ai[0]].active || Main.npc[(int)base.NPC.ai[0]].aiStyle != base.NPC.aiStyle))
			{
				base.NPC.type = 15;
				float segmentLifeRatio2 = MathHelper.Lerp(0.5f, 1f, (float)base.NPC.life / (float)base.NPC.lifeMax);
				int whoAmI2 = base.NPC.whoAmI;
				float ai1Holdover = base.NPC.ai[1];
				int slowingDebuffResistTimer2 = calamityGlobalNPC.debuffResistanceTimer;
				base.NPC.SetDefaultsKeepPlayerInteraction(base.NPC.type);
				base.NPC.life = (int)((float)base.NPC.lifeMax * segmentLifeRatio2);
				base.NPC.whoAmI = whoAmI2;
				base.NPC.ai[1] = ai1Holdover;
				base.NPC.Calamity().debuffResistanceTimer = slowingDebuffResistTimer2;
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				base.NPC.ForceNetUpdate();
				base.NPC.alpha = 0;
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
		bool inTiles = false;
		if (!inTiles)
		{
			Vector2 vector = default(Vector2);
			for (int j = tilePositionX; j < tileWidthPosX; j++)
			{
				for (int k = tilePositionY; k < tileWidthPosY; k++)
				{
					if (!(Main.tile[j, k] != null) || ((!Main.tile[j, k].HasUnactuatedTile || (!Main.tileSolid[Main.tile[j, k].TileType] && (!Main.tileSolidTop[Main.tile[j, k].TileType] || Main.tile[j, k].TileFrameY != 0))) && Main.tile[j, k].LiquidAmount <= 64))
					{
						continue;
					}
					vector.X = j * 16;
					vector.Y = k * 16;
					if (base.NPC.position.X + (float)base.NPC.width > vector.X && base.NPC.position.X < vector.X + 16f && base.NPC.position.Y + (float)base.NPC.height > vector.Y && base.NPC.position.Y < vector.Y + 16f)
					{
						inTiles = true;
						if (Main.rand.NextBool(100) && Main.tile[j, k].HasUnactuatedTile)
						{
							WorldGen.KillTile(j, k, fail: true, effectOnly: true);
						}
					}
				}
			}
		}
		if (!inTiles && base.NPC.type == 13)
		{
			Rectangle rectangle = default(Rectangle);
			((Rectangle)(ref rectangle))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
			int noFlyZone = ((!death) ? 900 : (phase5 ? 400 : 600));
			bool freeMoveAnyway = true;
			Rectangle rectangle2 = default(Rectangle);
			for (int l = 0; l < 255; l++)
			{
				if (Main.player[l].active)
				{
					((Rectangle)(ref rectangle2))._002Ector((int)Main.player[l].position.X - noFlyZone, (int)Main.player[l].position.Y - noFlyZone, noFlyZone * 2, noFlyZone * 2);
					if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
					{
						freeMoveAnyway = false;
						break;
					}
				}
			}
			if (freeMoveAnyway)
			{
				inTiles = true;
			}
		}
		float velocityBoost = (death ? 4.8f : 2.4f) * (1f - lifeRatio);
		float accelerationBoost = (death ? 0.06f : 0.03f) * (1f - lifeRatio);
		float segmentVelocity = 12f + velocityBoost;
		float segmentAcceleration = 0.15f + accelerationBoost;
		if (phase6)
		{
			segmentVelocity += 2.4f;
			segmentAcceleration += 0.12f;
		}
		else if (phase5)
		{
			segmentVelocity += 1.8f;
			segmentAcceleration += 0.09f;
		}
		else if (phase4)
		{
			segmentVelocity += 1.2f;
			segmentAcceleration += 0.06f;
		}
		else if (phase3)
		{
			segmentVelocity += 0.6f;
			segmentAcceleration += 0.03f;
		}
		if (death)
		{
			segmentVelocity += (base.NPC.justHit ? 8f : 2f);
			segmentAcceleration += (base.NPC.justHit ? 0.16f : 0.04f);
		}
		if (Main.getGoodWorld)
		{
			segmentVelocity += 4f;
			segmentAcceleration += 0.05f;
		}
		Vector2 segmentDirection = base.NPC.Center;
		Vector2 val = Main.player[base.NPC.target].Center + (phase6 ? (Main.player[base.NPC.target].velocity * 20f) : Vector2.Zero);
		float targetPosX = val.X;
		float targetPosY = val.Y;
		targetPosX = (int)(targetPosX / 16f) * 16;
		targetPosY = (int)(targetPosY / 16f) * 16;
		segmentDirection.X = (int)(segmentDirection.X / 16f) * 16;
		segmentDirection.Y = (int)(segmentDirection.Y / 16f) * 16;
		targetPosX -= segmentDirection.X;
		targetPosY -= segmentDirection.Y;
		float targetDistance = (float)Math.Sqrt(targetPosX * targetPosX + targetPosY * targetPosY);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentDirection = base.NPC.Center;
				targetPosX = Main.npc[(int)base.NPC.ai[1]].Center.X - segmentDirection.X;
				targetPosY = Main.npc[(int)base.NPC.ai[1]].Center.Y - segmentDirection.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(targetPosY, targetPosX) + (float)Math.PI / 2f;
			targetDistance = (float)Math.Sqrt(targetPosX * targetPosX + targetPosY * targetPosY);
			int npcWidth = base.NPC.width;
			npcWidth = (int)((float)npcWidth * base.NPC.scale);
			if (Main.getGoodWorld)
			{
				npcWidth = 62;
			}
			targetDistance = (targetDistance - (float)npcWidth) / targetDistance;
			targetPosX *= targetDistance;
			targetPosY *= targetDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X += targetPosX;
			base.NPC.position.Y += targetPosY;
		}
		else
		{
			if (calamityGlobalNPC.newAI[2] < 3f)
			{
				calamityGlobalNPC.newAI[2]++;
				if (base.NPC.Distance(Main.player[base.NPC.target].Center) > segmentVelocity * 20f)
				{
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * (segmentVelocity * (death ? 0.75f : 0.5f));
				}
			}
			if (!inTiles)
			{
				base.NPC.velocity.Y += (death ? 0.1375f : 0.11f);
				if (death && base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y += 0.07f;
				}
				if (base.NPC.velocity.Y > segmentVelocity)
				{
					base.NPC.velocity.Y = segmentVelocity;
				}
				bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > segmentAcceleration;
				if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.4)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X -= segmentAcceleration * 1.1f;
					}
					else
					{
						base.NPC.velocity.X += segmentAcceleration * 1.1f;
					}
				}
				else if (base.NPC.velocity.Y == segmentVelocity)
				{
					if (slowXVelocity)
					{
						if (base.NPC.velocity.X < targetPosX)
						{
							base.NPC.velocity.X += segmentAcceleration;
						}
						else if (base.NPC.velocity.X > targetPosX)
						{
							base.NPC.velocity.X -= segmentAcceleration;
						}
					}
					else
					{
						base.NPC.velocity.X = 0f;
					}
				}
				else if (base.NPC.velocity.Y > (death ? 5f : 4f))
				{
					if (slowXVelocity)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X += segmentAcceleration * 0.9f;
						}
						else
						{
							base.NPC.velocity.X -= segmentAcceleration * 0.9f;
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
				targetDistance = (float)Math.Sqrt(targetPosX * targetPosX + targetPosY * targetPosY);
				float absoluteTargetX = Math.Abs(targetPosX);
				float absoluteTargetY = Math.Abs(targetPosY);
				float timeToReachTarget = segmentVelocity / targetDistance;
				targetPosX *= timeToReachTarget;
				targetPosY *= timeToReachTarget;
				if (base.NPC.type == 13 && (Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].ZoneCorrupt || !Main.player[base.NPC.target].ZoneCrimson) && !BossRushEvent.BossRushActive)
				{
					bool everyoneDead = true;
					ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Player p = enumerator.Current;
						if (!p.dead && p.ZoneCorrupt)
						{
							everyoneDead = false;
							break;
						}
					}
					if (everyoneDead)
					{
						if (Main.netMode != 1 && (double)(base.NPC.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
						{
							base.NPC.active = false;
							int segmentAmt = (int)base.NPC.ai[0];
							while (segmentAmt > 0 && segmentAmt < Main.maxNPCs && Main.npc[segmentAmt].active && Main.npc[segmentAmt].aiStyle == base.NPC.aiStyle)
							{
								int num = (int)Main.npc[segmentAmt].ai[0];
								Main.npc[segmentAmt].active = false;
								base.NPC.life = 0;
								if (Main.dedServ)
								{
									NetMessage.SendData(23, -1, -1, null, segmentAmt);
								}
								segmentAmt = num;
							}
							if (Main.dedServ)
							{
								NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
							}
						}
						targetPosX = 0f;
						targetPosY = segmentVelocity;
					}
				}
				if ((base.NPC.velocity.X > 0f && targetPosX > 0f) || (base.NPC.velocity.X < 0f && targetPosX < 0f) || (base.NPC.velocity.Y > 0f && targetPosY > 0f) || (base.NPC.velocity.Y < 0f && targetPosY < 0f))
				{
					if (base.NPC.velocity.X < targetPosX)
					{
						base.NPC.velocity.X += segmentAcceleration;
					}
					else if (base.NPC.velocity.X > targetPosX)
					{
						base.NPC.velocity.X -= segmentAcceleration;
					}
					if (base.NPC.velocity.Y < targetPosY)
					{
						base.NPC.velocity.Y += segmentAcceleration;
					}
					else if (base.NPC.velocity.Y > targetPosY)
					{
						base.NPC.velocity.Y -= segmentAcceleration;
					}
					if ((double)Math.Abs(targetPosY) < (double)segmentVelocity * 0.2 && ((base.NPC.velocity.X > 0f && targetPosX < 0f) || (base.NPC.velocity.X < 0f && targetPosX > 0f)))
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y += segmentAcceleration * 2f;
						}
						else
						{
							base.NPC.velocity.Y -= segmentAcceleration * 2f;
						}
					}
					if ((double)Math.Abs(targetPosX) < (double)segmentVelocity * 0.2 && ((base.NPC.velocity.Y > 0f && targetPosY < 0f) || (base.NPC.velocity.Y < 0f && targetPosY > 0f)))
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X += segmentAcceleration * 2f;
						}
						else
						{
							base.NPC.velocity.X -= segmentAcceleration * 2f;
						}
					}
				}
				else if (absoluteTargetX > absoluteTargetY)
				{
					if (base.NPC.velocity.X < targetPosX)
					{
						base.NPC.velocity.X += segmentAcceleration * 1.1f;
					}
					else if (base.NPC.velocity.X > targetPosX)
					{
						base.NPC.velocity.X -= segmentAcceleration * 1.1f;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.5)
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y += segmentAcceleration;
						}
						else
						{
							base.NPC.velocity.Y -= segmentAcceleration;
						}
					}
				}
				else
				{
					if (base.NPC.velocity.Y < targetPosY)
					{
						base.NPC.velocity.Y += segmentAcceleration * 1.1f;
					}
					else if (base.NPC.velocity.Y > targetPosY)
					{
						base.NPC.velocity.Y -= segmentAcceleration * 1.1f;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 0.5)
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X += segmentAcceleration;
						}
						else
						{
							base.NPC.velocity.X -= segmentAcceleration;
						}
					}
				}
			}
			if (death)
			{
				int numHeads = NPC.CountNPCS(base.NPC.type);
				if (numHeads > 0)
				{
					numHeads--;
					if (numHeads > 7)
					{
						numHeads = 7;
					}
					float num2 = 14f - (float)numHeads;
					float pushDistanceUpperLimit = 140f - (float)numHeads * 10f;
					float pushDistance = MathHelper.Lerp(num2, pushDistanceUpperLimit, 1f - lifeRatio) * base.NPC.scale;
					float pushVelocity = 0.25f;
					for (int m = 0; m < Main.maxNPCs; m++)
					{
						if (Main.npc[m].active && m != base.NPC.whoAmI && Main.npc[m].type == base.NPC.type && Vector2.Distance(base.NPC.Center, Main.npc[m].Center) < pushDistance)
						{
							if (base.NPC.position.X < Main.npc[m].position.X)
							{
								base.NPC.velocity.X -= pushVelocity;
							}
							else
							{
								base.NPC.velocity.X += pushVelocity;
							}
							if (base.NPC.position.Y < Main.npc[m].position.Y)
							{
								base.NPC.velocity.Y -= pushVelocity;
							}
							else
							{
								base.NPC.velocity.Y += pushVelocity;
							}
						}
					}
				}
			}
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
			if (base.NPC.type == 13)
			{
				if (inTiles)
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
		}
		Vector2 val2;
		if (calamityGlobalNPC.newAI[1] < 600f)
		{
			val2 = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val2)).Length() > 2f || calamityGlobalNPC.newAI[1] > 0f)
			{
				calamityGlobalNPC.newAI[1]++;
			}
		}
		if (base.NPC.type == 13 || (base.NPC.type != 13 && Main.npc[(int)base.NPC.ai[1]].alpha >= 85))
		{
			if (base.NPC.alpha > 0 && base.NPC.life > 0)
			{
				for (int dustIndex = 0; dustIndex < 2; dustIndex++)
				{
					int dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust3].noGravity = true;
					Main.dust[dust3].noLight = true;
				}
			}
			val2 = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val2)).Length() > 2f)
			{
				base.NPC.alpha -= 42;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
		}
		else if (base.NPC.type > 13 && base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		if (base.NPC.active && base.NPC.netUpdate && Main.dedServ)
		{
			SyncCalamityNPCAIArrayPacket.Send(base.NPC);
		}
		return false;
		void DestroyThisSegment()
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
		}
	}

	public static int GetEaterOfWorldsSegmentsCountRevDeath()
	{
		if (!Main.getGoodWorld)
		{
			if (!CalamityWorld.death && !BossRushEvent.BossRushActive)
			{
				return 62;
			}
			return 57;
		}
		return 100;
	}
}
