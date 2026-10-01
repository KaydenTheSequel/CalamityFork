using System;
using CalamityMod.Events;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class KingSlimeAI : VanillaAIOverride
{
	public static readonly SoundStyle SpawnCrystalSound = new SoundStyle("CalamityMod/Sounds/Custom/KingSlimeJewelSpawn");

	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Custom/RedJewelFire");

	public override bool AI(Mod mod)
	{
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1422: Unknown result type (might be due to invalid IL or missing references)
		//IL_142c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1431: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1321: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1361: Unknown result type (might be due to invalid IL or missing references)
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float lifeRatio2 = lifeRatio;
		float teleportScale = 1f;
		bool teleporting = false;
		bool teleported = false;
		base.NPC.aiAction = 0;
		float teleportScaleSpeed = 2f;
		if (Main.getGoodWorld)
		{
			teleportScaleSpeed -= 1f - lifeRatio;
			teleportScale *= teleportScaleSpeed;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase2 = lifeRatio < 0.75f;
		bool rubySpawnPhaseActive = (death ? (lifeRatio < 0.75f) : (lifeRatio < 0.5f));
		bool redCrystalAlive = NPC.AnyNPCs(ModContent.NPCType<KingSlimeJewelRuby>());
		bool rubySpawnedForCurrentPhase = base.NPC.Calamity().newAI[0] == 1f;
		int setDamage = base.NPC.defDamage;
		base.NPC.defense = base.NPC.defDefense;
		if (rubySpawnPhaseActive && !redCrystalAlive && !rubySpawnedForCurrentPhase)
		{
			base.NPC.Calamity().newAI[0] = 1f;
			base.NPC.SyncExtraAI();
			Vector2 vector = base.NPC.Center + new Vector2(-40f, (0f - (float)base.NPC.height) / 2f) * base.NPC.scale;
			int totalDustPerCrystalSpawn = 20;
			for (int i = 0; i < totalDustPerCrystalSpawn; i++)
			{
				int rubyDust = Dust.NewDust(vector, base.NPC.width / 2, base.NPC.height / 2, 90, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[rubyDust];
				obj.velocity *= 2f;
				Main.dust[rubyDust].noGravity = true;
				if (Main.rand.NextBool())
				{
					Main.dust[rubyDust].scale = 0.5f;
					Main.dust[rubyDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			SoundEngine.PlaySound(SpawnCrystalSound with
			{
				Volume = 2f
			}, base.NPC.Center);
			if (Main.netMode != 1)
			{
				int jewel = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vector.X, (int)vector.Y, ModContent.NPCType<KingSlimeJewelRuby>());
				Main.npc[jewel].localAI[2] = base.NPC.whoAmI;
				Main.npc[jewel].velocity.Y = -6f;
			}
		}
		else if (rubySpawnedForCurrentPhase && !rubySpawnPhaseActive)
		{
			base.NPC.Calamity().newAI[0] = 0f;
			base.NPC.SyncExtraAI();
		}
		if (base.NPC.ai[3] == 0f && base.NPC.life > 0)
		{
			base.NPC.ai[3] = base.NPC.lifeMax;
		}
		if (base.NPC.localAI[3] == 0f)
		{
			base.NPC.localAI[3] = 1f;
			if (Main.netMode != 1)
			{
				base.NPC.ai[0] = -100f;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				base.NPC.netUpdate = true;
			}
		}
		int despawnDistance = 60;
		int forceDespawnDistance = 300;
		if ((Main.player[base.NPC.target].dead && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistance) || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)forceDespawnDistance)
		{
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			if ((Main.player[base.NPC.target].dead && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistance) || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)forceDespawnDistance)
			{
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				if (Main.player[base.NPC.target].Center.X < base.NPC.Center.X)
				{
					base.NPC.direction = 1;
				}
				else
				{
					base.NPC.direction = -1;
				}
			}
		}
		if (base.NPC.velocity.Y > 0f)
		{
			float fallSpeedBonus = (death ? 0.1f : 0f) + ((!redCrystalAlive) ? 0.1f : 0f);
			base.NPC.velocity.Y += fallSpeedBonus;
		}
		float teleportGateValue = 480f;
		if (!Main.player[base.NPC.target].dead && base.NPC.ai[2] >= teleportGateValue && base.NPC.ai[1] < 5f && base.NPC.velocity.Y == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[0] = 0f;
			base.NPC.ai[1] = 5f;
			if (Main.netMode != 1)
			{
				GetPlaceToTeleportTo(base.NPC);
			}
		}
		if (!Collision.CanHitLine(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0) || Math.Abs(base.NPC.Top.Y - Main.player[base.NPC.target].Bottom.Y) > 160f)
		{
			if (Main.netMode != 1)
			{
				base.NPC.localAI[0]++;
			}
		}
		else if (Main.netMode != 1)
		{
			base.NPC.localAI[0]--;
			if (base.NPC.localAI[0] < 0f)
			{
				base.NPC.localAI[0] = 0f;
			}
		}
		if (base.NPC.timeLeft < 10 && (base.NPC.ai[0] != 0f || base.NPC.ai[1] != 0f))
		{
			base.NPC.ai[0] = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.netUpdate = true;
			teleporting = false;
		}
		if (base.NPC.ai[2] < teleportGateValue)
		{
			if (!Collision.CanHitLine(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0) || Math.Abs(base.NPC.Top.Y - Main.player[base.NPC.target].Bottom.Y) > (death ? 160f : 320f))
			{
				base.NPC.ai[2] += (death ? 3f : 2f);
			}
			else
			{
				base.NPC.ai[2]++;
			}
		}
		if ((base.NPC.ai[1] == 5f || base.NPC.ai[1] == 6f) && Math.Abs(base.NPC.velocity.X) > 0.1f)
		{
			base.NPC.velocity.X *= 0.8f;
			if (Math.Abs(base.NPC.velocity.X) <= 0.1f)
			{
				base.NPC.velocity.X = 0f;
			}
		}
		if (base.NPC.ai[1] == 5f)
		{
			base.NPC.damage = 0;
			teleporting = true;
			base.NPC.aiAction = 1;
			float teleportRate = (redCrystalAlive ? 1f : 2f);
			if (death)
			{
				teleportRate *= 2f;
			}
			base.NPC.ai[0] += teleportRate;
			teleportScale = MathHelper.Clamp((60f - base.NPC.ai[0]) / 60f, 0f, 1f);
			teleportScale = 0.5f + teleportScale * 0.5f;
			if (Main.getGoodWorld)
			{
				teleportScale *= teleportScaleSpeed;
			}
			if (base.NPC.ai[0] >= 60f)
			{
				teleported = true;
			}
			if (base.NPC.ai[0] == 60f && !Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.Center + new Vector2(-40f, (0f - (float)base.NPC.height) / 2f), base.NPC.velocity, 734);
			}
			if (base.NPC.ai[0] >= 60f && Main.netMode != 1)
			{
				base.NPC.Bottom = new Vector2(base.NPC.localAI[1], base.NPC.localAI[2]);
				base.NPC.ai[1] = 6f;
				base.NPC.ai[0] = 0f;
				base.NPC.netUpdate = true;
			}
			if (Main.netMode == 1 && base.NPC.ai[0] >= 120f)
			{
				base.NPC.ai[1] = 6f;
				base.NPC.ai[0] = 0f;
			}
			if (!teleported)
			{
				for (int j = 0; j < 10; j++)
				{
					int slimeDust = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, 150, new Color(78, 136, 255, 80), 2f);
					Main.dust[slimeDust].noGravity = true;
					Dust obj2 = Main.dust[slimeDust];
					obj2.velocity *= 0.5f;
				}
			}
		}
		else if (base.NPC.ai[1] == 6f)
		{
			base.NPC.damage = 0;
			teleporting = true;
			base.NPC.aiAction = 0;
			float teleportRate2 = (redCrystalAlive ? 1f : 2f);
			if (death)
			{
				teleportRate2 *= 2f;
			}
			base.NPC.ai[0] += teleportRate2;
			teleportScale = MathHelper.Clamp(base.NPC.ai[0] / 30f, 0f, 1f);
			teleportScale = 0.5f + teleportScale * 0.5f;
			if (Main.getGoodWorld)
			{
				teleportScale *= teleportScaleSpeed;
			}
			if (base.NPC.ai[0] >= 30f && Main.netMode != 1)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = -15f;
				base.NPC.netUpdate = true;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			if (Main.netMode == 1 && base.NPC.ai[0] >= 60f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = -15f;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			for (int k = 0; k < 10; k++)
			{
				int slimyDust = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, 150, new Color(78, 136, 255, 80), 2f);
				Main.dust[slimyDust].noGravity = true;
				Dust obj3 = Main.dust[slimyDust];
				obj3.velocity *= 2f;
			}
		}
		base.NPC.noTileCollide = false;
		if (base.NPC.velocity.Y == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.X *= 0.8f;
			if (base.NPC.velocity.X > -0.1f && base.NPC.velocity.X < 0.1f)
			{
				base.NPC.velocity.X = 0f;
			}
			if (!teleporting)
			{
				base.NPC.ai[0] += MathHelper.Lerp(1f, 8f, 1f - lifeRatio);
				if (base.NPC.ai[0] >= 0f)
				{
					base.NPC.damage = setDamage;
					base.NPC.netUpdate = true;
					base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
					float distanceBelowTarget = base.NPC.position.Y - (Main.player[base.NPC.target].position.Y + 80f);
					float speedMult = 1f;
					if (distanceBelowTarget > 0f)
					{
						speedMult += distanceBelowTarget * 0.002f;
					}
					if (speedMult > 2f)
					{
						speedMult = 2f;
					}
					bool deathModeRapidHops = death && lifeRatio < 0.2f;
					if (deathModeRapidHops)
					{
						base.NPC.ai[1] = 2f;
					}
					if (base.NPC.ai[1] == 3f)
					{
						base.NPC.velocity.Y = -10f * speedMult;
						base.NPC.velocity.X += ((!phase2) ? 3.5f : (death ? 5.35f : 4.5f)) * (float)base.NPC.direction;
						base.NPC.ai[0] = -100f;
						base.NPC.ai[1] = 0f;
					}
					else if (base.NPC.ai[1] == 2f)
					{
						base.NPC.velocity.Y = -6f * speedMult;
						base.NPC.velocity.X += ((!phase2) ? 4.5f : (deathModeRapidHops ? 7.65f : (death ? 6.25f : 5.5f))) * (float)base.NPC.direction;
						base.NPC.ai[0] = -60f;
						if (!deathModeRapidHops)
						{
							base.NPC.ai[1]++;
						}
					}
					else
					{
						base.NPC.velocity.Y = -8f * speedMult;
						base.NPC.velocity.X += ((!phase2) ? 4f : (death ? 5.75f : 5f)) * (float)base.NPC.direction;
						base.NPC.ai[0] = -60f;
						base.NPC.ai[1]++;
					}
					if (death)
					{
						base.NPC.velocity.X *= 1.2f;
					}
					base.NPC.noTileCollide = true;
				}
				else if (base.NPC.ai[0] >= -30f)
				{
					base.NPC.aiAction = 1;
				}
			}
		}
		else if (base.NPC.target < 255)
		{
			float jumpVelocityLimit = (redCrystalAlive ? 3f : 4.5f);
			if (death)
			{
				jumpVelocityLimit += 2.25f;
			}
			if (Main.getGoodWorld)
			{
				jumpVelocityLimit = 8f;
			}
			if ((base.NPC.direction == 1 && base.NPC.velocity.X < jumpVelocityLimit) || (base.NPC.direction == -1 && base.NPC.velocity.X > 0f - jumpVelocityLimit))
			{
				if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.1) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.1))
				{
					base.NPC.velocity.X += (death ? 0.25f : 0.2f) * (float)base.NPC.direction;
					if (death)
					{
						base.NPC.velocity.X += 0.25f * (float)base.NPC.direction;
					}
				}
				else
				{
					base.NPC.velocity.X *= (death ? 0.92f : 0.93f);
					if (death)
					{
						base.NPC.velocity.X *= 0.9f;
					}
				}
			}
			if (!Main.player[base.NPC.target].dead)
			{
				if (base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > Main.player[base.NPC.target].Top.Y)
				{
					base.NPC.noTileCollide = false;
				}
				else if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.noTileCollide = false;
				}
				else
				{
					base.NPC.noTileCollide = true;
				}
			}
		}
		int idleSlimeDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, 255, new Color(0, 80, 255, 80), base.NPC.scale * 1.2f);
		Main.dust[idleSlimeDust].noGravity = true;
		Dust obj4 = Main.dust[idleSlimeDust];
		obj4.velocity *= 0.5f;
		if (base.NPC.life <= 0)
		{
			return false;
		}
		float num = (Main.getGoodWorld ? 3f : (death ? 2.5f : 1.5f));
		float minScale = 0.75f;
		float maxScaledValue = num - minScale;
		lifeRatio = ((!Main.getGoodWorld) ? (lifeRatio * maxScaledValue + minScale) : (maxScaledValue - lifeRatio * maxScaledValue + minScale));
		lifeRatio *= teleportScale;
		if (lifeRatio != base.NPC.scale)
		{
			base.NPC.position.X += base.NPC.width / 2;
			base.NPC.position.Y += base.NPC.height;
			base.NPC.scale = lifeRatio;
			base.NPC.width = (int)(98f * base.NPC.scale);
			base.NPC.height = (int)(92f * base.NPC.scale);
			base.NPC.position.X -= base.NPC.width / 2;
			base.NPC.position.Y -= base.NPC.height;
		}
		if (Main.netMode != 1)
		{
			int slimeSpawnThreshold = (int)((double)base.NPC.lifeMax * 0.03);
			if ((float)(base.NPC.life + slimeSpawnThreshold) < base.NPC.ai[3])
			{
				base.NPC.ai[3] = base.NPC.life;
				int slimeAmt = (death ? 1 : Main.rand.Next(1, 3));
				for (int l = 0; l < slimeAmt; l++)
				{
					int minTypeChoice = (int)MathHelper.Lerp(0f, 5f, 1f - lifeRatio2);
					int maxTypeChoice = (int)MathHelper.Lerp(2f, 7f, 1f - lifeRatio2);
					int npcType = Main.rand.Next(minTypeChoice, maxTypeChoice + 1) switch
					{
						0 => -3, 
						1 => 1, 
						2 => (!Main.raining) ? 1 : 225, 
						3 => -8, 
						4 => -7, 
						5 => -9, 
						_ => 535, 
					};
					if (Main.raining && Main.hardMode && Main.rand.NextBool(50))
					{
						npcType = 244;
					}
					if (death && Main.rand.NextBool())
					{
						npcType = 535;
					}
					if (Main.rand.NextBool(100))
					{
						npcType = -4;
					}
					if (Main.zenithWorld)
					{
						npcType = 244;
					}
					int offset = 16;
					int spawnZoneWidth = base.NPC.width - offset * 2;
					int spawnZoneHeight = base.NPC.height - offset * 2;
					int x = (int)(base.NPC.position.X + (float)offset + (float)Main.rand.Next(spawnZoneWidth));
					int y = (int)(base.NPC.position.Y + (float)offset + (float)Main.rand.Next(spawnZoneHeight));
					int slimeSpawns = NPC.NewNPC(base.NPC.GetSource_FromAI(), x, y, npcType);
					Main.npc[slimeSpawns].SetDefaults(npcType);
					Main.npc[slimeSpawns].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
					Main.npc[slimeSpawns].velocity.Y = (float)Main.rand.Next(-30, 31) * 0.1f;
					Main.npc[slimeSpawns].ai[0] = -1000 * Main.rand.Next(3);
					Main.npc[slimeSpawns].ai[1] = 0f;
					if (Main.dedServ && slimeSpawns < Main.maxNPCs)
					{
						NetMessage.SendData(23, -1, -1, null, slimeSpawns);
					}
				}
			}
		}
		return false;
	}

	public static void GetPlaceToTeleportTo(NPC npc)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		CalamityTargetingParameters options = CalamityTargetingParameters.Defaults;
		options.faceTarget = false;
		npc.CalamityTargeting(options);
		float distanceAhead = 800f;
		Vector2 randomDefault = (Main.rand.NextBool() ? Vector2.UnitX : (-Vector2.UnitX));
		Point predictiveTeleportPoint = (Main.player[npc.target].Center + Utils.SafeNormalize(new Vector2((float)Math.Round(Main.player[npc.target].velocity.X), 0f), randomDefault) * distanceAhead).ToTileCoordinates();
		if (predictiveTeleportPoint.X < 10)
		{
			predictiveTeleportPoint.X = 10;
		}
		if (predictiveTeleportPoint.X > Main.maxTilesX - 10)
		{
			predictiveTeleportPoint.X = Main.maxTilesX - 10;
		}
		if (predictiveTeleportPoint.Y < 10)
		{
			predictiveTeleportPoint.Y = 10;
		}
		if (predictiveTeleportPoint.Y > Main.maxTilesY - 10)
		{
			predictiveTeleportPoint.Y = Main.maxTilesY - 10;
		}
		int randomPredictiveTeleportOffset = 5;
		int teleportTries = 0;
		while (teleportTries < 100)
		{
			teleportTries++;
			int teleportTileX = Main.rand.Next(predictiveTeleportPoint.X - randomPredictiveTeleportOffset, predictiveTeleportPoint.X + randomPredictiveTeleportOffset + 1);
			int teleportTileY = Main.rand.Next(predictiveTeleportPoint.Y - randomPredictiveTeleportOffset, predictiveTeleportPoint.Y);
			if (!Main.tile[teleportTileX, teleportTileY].HasUnactuatedTile)
			{
				bool canTeleportToTile = true;
				if (canTeleportToTile && Main.tile[teleportTileX, teleportTileY].LiquidType == 1)
				{
					canTeleportToTile = false;
				}
				if (canTeleportToTile && !Collision.CanHitLine(npc.Center, 0, 0, predictiveTeleportPoint.ToVector2() * 16f, 0, 0))
				{
					canTeleportToTile = false;
				}
				if (canTeleportToTile)
				{
					npc.localAI[1] = teleportTileX * 16 + 8;
					npc.localAI[2] = teleportTileY * 16 + 16;
					break;
				}
				predictiveTeleportPoint.X += (((float)predictiveTeleportPoint.X < 0f) ? 1 : (-1));
				if (predictiveTeleportPoint.X < 10)
				{
					predictiveTeleportPoint.X = 10;
				}
				if (predictiveTeleportPoint.X > Main.maxTilesX - 10)
				{
					predictiveTeleportPoint.X = Main.maxTilesX - 10;
				}
			}
			else
			{
				predictiveTeleportPoint.X += (((float)predictiveTeleportPoint.X < 0f) ? 1 : (-1));
				if (predictiveTeleportPoint.X < 10)
				{
					predictiveTeleportPoint.X = 10;
				}
				if (predictiveTeleportPoint.X > Main.maxTilesX - 10)
				{
					predictiveTeleportPoint.X = Main.maxTilesX - 10;
				}
			}
		}
		if (teleportTries >= 100)
		{
			Vector2 bottom = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
			npc.localAI[1] = bottom.X;
			npc.localAI[2] = bottom.Y;
		}
	}
}
