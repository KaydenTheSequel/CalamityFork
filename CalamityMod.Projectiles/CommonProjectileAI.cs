using System;
using CalamityMod.Enums;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles;

internal static class CommonProjectileAI
{
	public static void ChargingMinionAI(this Projectile projectile, float range, float maxPlayerDist, float extraMaxPlayerDist, float safeDist, int initialUpdates, float chargeDelayTime, float goToSpeed, float goBackSpeed, Vector2 returnOffset, float chargeCounterMax, float chargeSpeed, bool tileVision, bool ignoreTilesWhenCharging, int updateDifference = 1)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		player.Calamity();
		projectile.MinionAntiClump();
		bool chargeDelay = false;
		if (projectile.ai[0] == 2f)
		{
			projectile.ai[1]++;
			projectile.extraUpdates = initialUpdates + updateDifference;
			if (projectile.ai[1] > chargeDelayTime)
			{
				projectile.ai[1] = 1f;
				projectile.ai[0] = 0f;
				projectile.extraUpdates = initialUpdates;
				projectile.netUpdate = true;
			}
			else
			{
				chargeDelay = true;
			}
		}
		if (chargeDelay)
		{
			return;
		}
		float maxDist = range;
		Vector2 targetVec = projectile.position;
		bool foundTarget = false;
		bool isButterfly = projectile.type == ModContent.ProjectileType<PurpleButterfly>();
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			bool fishronCheck = (npc.type == 370 && npc.active) & isButterfly;
			if (npc.CanBeChasedBy(projectile) | fishronCheck)
			{
				float extraDist = npc.width / 2 + npc.height / 2;
				float targetDist = Vector2.Distance(npc.Center, projectile.Center);
				bool canHit = true;
				if (extraDist < maxDist && !tileVision)
				{
					canHit = Collision.CanHit(projectile.Center, 1, 1, npc.Center, 1, 1);
				}
				if ((!foundTarget && targetDist < maxDist + extraDist) & canHit)
				{
					maxDist = targetDist;
					targetVec = npc.Center;
					foundTarget = true;
				}
			}
		}
		if (!foundTarget)
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				NPC npc2 = Main.npc[npcIndex];
				bool fishronCheck2 = (npc2.type == 370 && npc2.active) & isButterfly;
				if (npc2.CanBeChasedBy(projectile) | fishronCheck2)
				{
					float extraDist2 = npc2.width / 2 + npc2.height / 2;
					float targetDist2 = Vector2.Distance(npc2.Center, projectile.Center);
					bool canHit2 = true;
					if (extraDist2 < maxDist && !tileVision)
					{
						canHit2 = Collision.CanHit(projectile.Center, 1, 1, npc2.Center, 1, 1);
					}
					if ((!foundTarget && targetDist2 < maxDist + extraDist2) & canHit2)
					{
						maxDist = targetDist2;
						targetVec = npc2.Center;
						foundTarget = true;
					}
				}
			}
		}
		float distBeforeForcedReturn = maxPlayerDist;
		if (foundTarget)
		{
			distBeforeForcedReturn = extraMaxPlayerDist;
		}
		if (Vector2.Distance(player.Center, projectile.Center) > distBeforeForcedReturn)
		{
			projectile.ai[0] = 1f;
			projectile.netUpdate = true;
		}
		if (foundTarget && projectile.ai[0] == 0f)
		{
			projectile.tileCollide = !ignoreTilesWhenCharging;
			Vector2 targetSpot = targetVec - projectile.Center;
			float num = ((Vector2)(ref targetSpot)).Length();
			((Vector2)(ref targetSpot)).Normalize();
			if (num > 200f)
			{
				targetSpot *= goToSpeed;
				projectile.velocity = (projectile.velocity * 40f + targetSpot) / 41f;
			}
			else
			{
				float speed = 0f - goBackSpeed;
				targetSpot *= speed;
				projectile.velocity = (projectile.velocity * 40f + targetSpot) / 41f;
			}
		}
		else
		{
			projectile.tileCollide = false;
			bool returningToPlayer = false;
			if (!returningToPlayer)
			{
				returningToPlayer = projectile.ai[0] == 1f;
			}
			Vector2 playerVec = player.Center - projectile.Center + returnOffset;
			float num2 = ((Vector2)(ref playerVec)).Length();
			float playerHomeSpeed = 6f;
			if (returningToPlayer)
			{
				playerHomeSpeed = 15f;
			}
			if (num2 > 200f && playerHomeSpeed < 8f)
			{
				playerHomeSpeed = 8f;
			}
			if (((num2 < safeDist) & returningToPlayer) && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
			{
				projectile.ai[0] = 0f;
				projectile.netUpdate = true;
			}
			if (num2 > 2000f)
			{
				projectile.position.X = player.Center.X - (float)(projectile.width / 2);
				projectile.position.Y = player.Center.Y - (float)(projectile.height / 2);
				projectile.netUpdate = true;
			}
			if (num2 > 70f)
			{
				((Vector2)(ref playerVec)).Normalize();
				playerVec *= playerHomeSpeed;
				projectile.velocity = (projectile.velocity * 40f + playerVec) / 41f;
			}
			else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
			{
				projectile.velocity.X = -0.15f;
				projectile.velocity.Y = -0.05f;
			}
		}
		if (projectile.ai[1] > 0f)
		{
			projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (projectile.ai[1] > chargeCounterMax)
		{
			projectile.ai[1] = 0f;
			projectile.netUpdate = true;
		}
		if (projectile.ai[0] == 0f && ((projectile.ai[1] == 0f) & foundTarget) && maxDist < 500f)
		{
			projectile.ai[1]++;
			if (Main.myPlayer == projectile.owner)
			{
				projectile.ai[0] = 2f;
				Vector2 targetPos = targetVec - projectile.Center;
				((Vector2)(ref targetPos)).Normalize();
				projectile.velocity = targetPos * chargeSpeed;
				projectile.netUpdate = true;
			}
		}
	}

	public static void FloatingPetAI(this Projectile projectile, bool faceRight, float tiltFloat, bool lightPet = false)
	{
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		float SAImovement = 0.05f;
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile otherProj = Main.projectile[k];
			if (!otherProj.active || otherProj.owner != projectile.owner || !Main.projPet[otherProj.type] || k == projectile.whoAmI)
			{
				continue;
			}
			bool num = Main.projPet[otherProj.type];
			float taxicabDist = Math.Abs(projectile.position.X - otherProj.position.X) + Math.Abs(projectile.position.Y - otherProj.position.Y);
			if (num && taxicabDist < (float)projectile.width)
			{
				if (projectile.position.X < otherProj.position.X)
				{
					projectile.velocity.X -= SAImovement;
				}
				else
				{
					projectile.velocity.X += SAImovement;
				}
				if (projectile.position.Y < otherProj.position.Y)
				{
					projectile.velocity.Y -= SAImovement;
				}
				else
				{
					projectile.velocity.Y += SAImovement;
				}
			}
		}
		float passiveMvtFloat = 0.5f;
		projectile.tileCollide = false;
		float range = 100f;
		Vector2 projPos = projectile.Center;
		float xDist = player.Center.X - projPos.X;
		float yDist = player.Center.Y - projPos.Y;
		yDist += Main.rand.NextFloat(-10f, 20f);
		xDist += Main.rand.NextFloat(-10f, 20f);
		xDist += 60f * (lightPet ? ((float)player.direction) : (0f - (float)player.direction));
		yDist -= 60f;
		Vector2 playerVector = default(Vector2);
		((Vector2)(ref playerVector))._002Ector(xDist, yDist);
		float playerDist = ((Vector2)(ref playerVector)).Length();
		float returnSpeed = 18f;
		if (playerDist < range && player.velocity.Y == 0f && projectile.Bottom.Y <= player.Bottom.Y && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height) && projectile.velocity.Y < -6f)
		{
			projectile.velocity.Y = -6f;
		}
		if (playerDist > 2000f)
		{
			projectile.position.X = player.Center.X - (float)(projectile.width / 2);
			projectile.position.Y = player.Center.Y - (float)(projectile.height / 2);
			projectile.netUpdate = true;
		}
		if (playerDist < 50f)
		{
			if (Math.Abs(projectile.velocity.X) > 2f || Math.Abs(projectile.velocity.Y) > 2f)
			{
				projectile.velocity *= 0.99f;
			}
			passiveMvtFloat = 0.01f;
		}
		else
		{
			if (playerDist < 100f)
			{
				passiveMvtFloat = 0.1f;
			}
			if (playerDist > 300f)
			{
				passiveMvtFloat = 1f;
			}
			playerDist = returnSpeed / playerDist;
			playerVector.X *= playerDist;
			playerVector.Y *= playerDist;
		}
		if (projectile.velocity.X < playerVector.X)
		{
			projectile.velocity.X += passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && projectile.velocity.X < 0f)
			{
				projectile.velocity.X += passiveMvtFloat;
			}
		}
		if (projectile.velocity.X > playerVector.X)
		{
			projectile.velocity.X -= passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && projectile.velocity.X > 0f)
			{
				projectile.velocity.X -= passiveMvtFloat;
			}
		}
		if (projectile.velocity.Y < playerVector.Y)
		{
			projectile.velocity.Y += passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && projectile.velocity.Y < 0f)
			{
				projectile.velocity.Y += passiveMvtFloat * 2f;
			}
		}
		if (projectile.velocity.Y > playerVector.Y)
		{
			projectile.velocity.Y -= passiveMvtFloat;
			if (passiveMvtFloat > 0.05f && projectile.velocity.Y > 0f)
			{
				projectile.velocity.Y -= passiveMvtFloat * 2f;
			}
		}
		if (projectile.velocity.X >= 0.25f)
		{
			projectile.direction = (faceRight ? 1 : (-1));
		}
		else if (projectile.velocity.X < -0.25f)
		{
			projectile.direction = ((!faceRight) ? 1 : (-1));
		}
		projectile.spriteDirection = projectile.direction;
		projectile.rotation = projectile.velocity.X * tiltFloat;
	}

	public static void HealingProjectile(this Projectile projectile, int healing, int playerToHeal, float homingVelocity, float inertia, bool autoHomes = true, int timeCheck = 120)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[playerToHeal];
		float homingSpeed = homingVelocity;
		if (player.lifeMagnet)
		{
			homingSpeed *= 1.5f;
		}
		Vector2 playerVector = player.Center - projectile.Center;
		float playerDist = ((Vector2)(ref playerVector)).Length();
		if (playerDist < 50f && projectile.position.X < player.position.X + (float)player.width && projectile.position.X + (float)projectile.width > player.position.X && projectile.position.Y < player.position.Y + (float)player.height && projectile.position.Y + (float)projectile.height > player.position.Y)
		{
			if (projectile.owner == Main.myPlayer && !Main.LocalPlayer.moonLeech)
			{
				player.HealPlayer(healing, HealTextType.Local);
				NetMessage.SendData(66, -1, -1, null, playerToHeal, healing);
			}
			projectile.Kill();
		}
		if (autoHomes)
		{
			playerDist = homingSpeed / playerDist;
			playerVector.X *= playerDist;
			playerVector.Y *= playerDist;
			projectile.velocity.X = (projectile.velocity.X * inertia + playerVector.X) / (inertia + 1f);
			projectile.velocity.Y = (projectile.velocity.Y * inertia + playerVector.Y) / (inertia + 1f);
		}
		else if (player.lifeMagnet && projectile.timeLeft < timeCheck)
		{
			playerDist = homingVelocity / playerDist;
			playerVector.X *= playerDist;
			playerVector.Y *= playerDist;
			projectile.velocity.X = (projectile.velocity.X * inertia + playerVector.X) / (inertia + 1f);
			projectile.velocity.Y = (projectile.velocity.Y * inertia + playerVector.Y) / (inertia + 1f);
		}
	}

	public static void MinionAntiClump(this Projectile projectile, float pushForce = 0.05f)
	{
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile otherProj = Main.projectile[k];
			if (!otherProj.active || otherProj.owner != projectile.owner || !otherProj.minion || k == projectile.whoAmI)
			{
				continue;
			}
			bool num = otherProj.type == projectile.type;
			float taxicabDist = Math.Abs(projectile.position.X - otherProj.position.X) + Math.Abs(projectile.position.Y - otherProj.position.Y);
			if (num && taxicabDist < (float)projectile.width)
			{
				if (projectile.position.X < otherProj.position.X)
				{
					projectile.velocity.X -= pushForce;
				}
				else
				{
					projectile.velocity.X += pushForce;
				}
				if (projectile.position.Y < otherProj.position.Y)
				{
					projectile.velocity.Y -= pushForce;
				}
				else
				{
					projectile.velocity.Y += pushForce;
				}
			}
		}
	}

	public static void ModifyHitNPCSticky(this Projectile projectile, int maxStick)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		Rectangle myRect = projectile.Hitbox;
		if (projectile.owner != Main.myPlayer)
		{
			return;
		}
		for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
		{
			NPC npc = Main.npc[npcIndex];
			if (!npc.active || npc.dontTakeDamage || ((!projectile.friendly || (npc.friendly && (npc.type != 22 || projectile.owner >= 255 || !player.killGuide) && (npc.type != 54 || projectile.owner >= 255 || !player.killClothier))) && (!projectile.hostile || !npc.friendly || npc.dontTakeDamageFromHostiles)) || (projectile.owner >= 0 && npc.immune[projectile.owner] != 0 && projectile.maxPenetrate != 1) || (!npc.noTileCollide && projectile.ownerHitCheck))
			{
				continue;
			}
			bool stickingToNPC;
			if (npc.type == 414)
			{
				Rectangle rect = npc.Hitbox;
				int crawltipedeHitboxMod = 8;
				rect.X -= crawltipedeHitboxMod;
				rect.Y -= crawltipedeHitboxMod;
				rect.Width += crawltipedeHitboxMod * 2;
				rect.Height += crawltipedeHitboxMod * 2;
				stickingToNPC = projectile.Colliding(myRect, rect);
			}
			else
			{
				stickingToNPC = projectile.Colliding(myRect, npc.Hitbox);
			}
			if (!stickingToNPC)
			{
				continue;
			}
			if (npc.reflectsProjectiles && projectile.CanBeReflected())
			{
				npc.ReflectProjectile(projectile);
				break;
			}
			projectile.ai[0] = 1f;
			projectile.ai[1] = npcIndex;
			projectile.velocity = npc.Center - projectile.Center;
			projectile.netUpdate = true;
			Point[] array2 = (Point[])(object)new Point[maxStick];
			int projCount = 0;
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (projIndex != projectile.whoAmI && proj.active && proj.owner == Main.myPlayer && proj.type == projectile.type && proj.ai[0] == 1f && proj.ai[1] == (float)npcIndex)
				{
					array2[projCount++] = new Point(projIndex, proj.timeLeft);
					if (projCount >= array2.Length)
					{
						break;
					}
				}
			}
			if (projCount < array2.Length)
			{
				continue;
			}
			int stuckProjAmt = 0;
			for (int m = 1; m < array2.Length; m++)
			{
				if (array2[m].Y < array2[stuckProjAmt].Y)
				{
					stuckProjAmt = m;
				}
			}
			Main.projectile[array2[stuckProjAmt].X].Kill();
		}
	}

	public static void StickToTiles(this Projectile projectile, bool ignorePlatforms, bool stickToEverything)
	{
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			int xLeft = (int)(projectile.position.X / 16f) - 1;
			int xRight = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
			int yBottom = (int)(projectile.position.Y / 16f) - 1;
			int yTop = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
			if (xLeft < 0)
			{
				xLeft = 0;
			}
			if (xRight > Main.maxTilesX)
			{
				xRight = Main.maxTilesX;
			}
			if (yBottom < 0)
			{
				yBottom = 0;
			}
			if (yTop > Main.maxTilesY)
			{
				yTop = Main.maxTilesY;
			}
			Vector2 tileSize = default(Vector2);
			for (int x = xLeft; x < xRight; x++)
			{
				for (int y = yBottom; y < yTop; y++)
				{
					Tile tile = Main.tile[x, y];
					bool platformCheck = true;
					if (ignorePlatforms)
					{
						platformCheck = !TileID.Sets.Platforms[tile.TileType] && tile.TileType != 380;
					}
					bool tableCheck = false;
					if (stickToEverything)
					{
						tableCheck = Main.tileSolidTop[tile.TileType] && tile.TileFrameY == 0;
					}
					if (((tile != null && tile.HasUnactuatedTile) & platformCheck) && (Main.tileSolid[tile.TileType] | tableCheck))
					{
						tileSize.X = x * 16;
						tileSize.Y = y * 16;
						if (projectile.position.X + (float)projectile.width - 4f > tileSize.X && projectile.position.X + 4f < tileSize.X + 16f && projectile.position.Y + (float)projectile.height - 4f > tileSize.Y && projectile.position.Y + 4f < tileSize.Y + 16f)
						{
							projectile.velocity.X = 0f;
							projectile.velocity.Y = -0.2f;
						}
					}
				}
			}
		}
		catch
		{
		}
	}

	public static void StickyProjAI(this Projectile projectile, int timeLeft, bool findNewNPC = false)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.ai[0] != 1f)
		{
			return;
		}
		bool killProj = false;
		bool spawnDust = false;
		projectile.tileCollide = false;
		projectile.localAI[0]++;
		if (projectile.localAI[0] % 30f == 0f)
		{
			spawnDust = true;
		}
		int npcIndex = (int)projectile.ai[1];
		NPC npc = Main.npc[npcIndex];
		if (projectile.localAI[0] >= (float)(60 * timeLeft))
		{
			killProj = true;
		}
		else if (!npcIndex.WithinBounds(Main.maxNPCs))
		{
			killProj = true;
		}
		else if (npc.active && !npc.dontTakeDamage)
		{
			projectile.Center = npc.Center - projectile.velocity * 2f;
			projectile.gfxOffY = npc.gfxOffY;
			if (spawnDust)
			{
				npc.HitEffect(0, 1.0);
			}
		}
		else
		{
			killProj = true;
		}
		if (killProj)
		{
			if (findNewNPC)
			{
				projectile.ai[0] = 0f;
			}
			else
			{
				projectile.Kill();
			}
		}
	}
}
