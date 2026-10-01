using System;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public static class CalamityRegularEnemyAI
{
	public static void GemCrawlerAI(NPC npc, Mod mod, float speedDetect, float speedAdditive)
	{
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		int turnAroundDelay = 30;
		bool isRunning = false;
		bool shouldTurnAround = false;
		if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction > 0) || (npc.velocity.X < 0f && npc.direction < 0)))
		{
			isRunning = true;
			npc.ai[3]++;
		}
		if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)turnAroundDelay) | isRunning)
		{
			npc.ai[3]++;
			shouldTurnAround = true;
		}
		else if (npc.ai[3] > 0f)
		{
			npc.ai[3]--;
		}
		if (npc.ai[3] > (float)(turnAroundDelay * 10))
		{
			npc.ai[3] = 0f;
		}
		if (npc.justHit)
		{
			npc.ai[3] = 0f;
		}
		if (npc.ai[3] == (float)turnAroundDelay)
		{
			npc.netUpdate = true;
		}
		Vector2 npcPos = default(Vector2);
		((Vector2)(ref npcPos))._002Ector(npc.Center.X, npc.Center.Y);
		float num = Main.player[npc.target].Center.X - npcPos.X;
		float yDist = Main.player[npc.target].Center.Y - npcPos.Y;
		if ((float)Math.Sqrt(num * num + yDist * yDist) < 200f && !shouldTurnAround)
		{
			npc.ai[3] = 0f;
		}
		if (npc.ai[3] < (float)turnAroundDelay)
		{
			npc.TargetClosest();
		}
		else
		{
			if (npc.velocity.X == 0f)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.ai[0]++;
					if (npc.ai[0] >= 2f)
					{
						npc.direction *= -1;
						npc.spriteDirection = -npc.direction;
						npc.ai[0] = 0f;
					}
				}
			}
			else
			{
				npc.ai[0] = 0f;
			}
			npc.directionY = -1;
			if (npc.direction == 0)
			{
				npc.direction = 1;
			}
		}
		if (0 == 0 && (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction > 0) || (npc.velocity.X >= 0f && npc.direction < 0)))
		{
			if (npc.velocity.X < 0f - speedDetect || npc.velocity.X > speedDetect)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= 0.8f;
				}
			}
			else if (npc.velocity.X < speedDetect && npc.direction == -1)
			{
				npc.velocity.X = npc.velocity.X + speedAdditive;
				if (npc.velocity.X > speedDetect)
				{
					npc.velocity.X = speedDetect;
				}
			}
			else if (npc.velocity.X > 0f - speedDetect && npc.direction == 1)
			{
				npc.velocity.X = npc.velocity.X - speedAdditive;
				if (npc.velocity.X < 0f - speedDetect)
				{
					npc.velocity.X = 0f - speedDetect;
				}
			}
		}
		if (!(npc.velocity.Y >= 0f))
		{
			return;
		}
		int faceDirection = 0;
		if (npc.velocity.X < 0f)
		{
			faceDirection = -1;
		}
		if (npc.velocity.X > 0f)
		{
			faceDirection = 1;
		}
		Vector2 position = npc.position;
		position.X += npc.velocity.X;
		int x = (int)((position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * faceDirection)) / 16f);
		int y = (int)((position.Y + (float)npc.height - 1f) / 16f);
		if (!((float)(x * 16) < position.X + (float)npc.width) || !((float)(x * 16 + 16) > position.X) || ((!Main.tile[x, y].HasUnactuatedTile || Main.tile[x, y].TopSlope || Main.tile[x, y - 1].TopSlope || !Main.tileSolid[Main.tile[x, y].TileType] || Main.tileSolidTop[Main.tile[x, y].TileType]) && (!Main.tile[x, y - 1].IsHalfBlock || !Main.tile[x, y - 1].HasUnactuatedTile)) || (Main.tile[x, y - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[x, y - 1].TileType] && !Main.tileSolidTop[Main.tile[x, y - 1].TileType] && (!Main.tile[x, y - 1].IsHalfBlock || (Main.tile[x, y - 4].HasUnactuatedTile && Main.tileSolid[Main.tile[x, y - 4].TileType] && !Main.tileSolidTop[Main.tile[x, y - 4].TileType]))) || (Main.tile[x, y - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[x, y - 2].TileType] && !Main.tileSolidTop[Main.tile[x, y - 2].TileType]) || (Main.tile[x, y - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[x, y - 3].TileType] && !Main.tileSolidTop[Main.tile[x, y - 3].TileType]) || (Main.tile[x - faceDirection, y - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[x - faceDirection, y - 3].TileType]))
		{
			return;
		}
		float npcBottom = y * 16;
		if (Main.tile[x, y].IsHalfBlock)
		{
			npcBottom += 8f;
		}
		if (Main.tile[x, y - 1].IsHalfBlock)
		{
			npcBottom -= 8f;
		}
		if (!(npcBottom < position.Y + (float)npc.height))
		{
			return;
		}
		float percentageTileRisen = position.Y + (float)npc.height - npcBottom;
		if (percentageTileRisen <= 16.1f)
		{
			npc.gfxOffY += npc.position.Y + (float)npc.height - npcBottom;
			npc.position.Y = npcBottom - (float)npc.height;
			if (percentageTileRisen < 9f)
			{
				npc.stepSpeed = 1f;
			}
			else
			{
				npc.stepSpeed = 2f;
			}
		}
	}

	public static void DungeonSpiritAI(NPC npc, Mod mod, float speed, float rotation, bool lantern = false)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		npc.TargetClosest();
		Vector2 npcPos = default(Vector2);
		((Vector2)(ref npcPos))._002Ector(npc.Center.X, npc.Center.Y);
		float xDist = Main.player[npc.target].Center.X - npcPos.X;
		float yDist = Main.player[npc.target].Center.Y - npcPos.Y;
		float targetDist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
		float homingSpeed = speed;
		if (lantern)
		{
			if (npc.localAI[0] < 85f)
			{
				homingSpeed = 0.1f;
				targetDist = homingSpeed / targetDist;
				xDist *= targetDist;
				yDist *= targetDist;
				npc.velocity = (npc.velocity * 100f + new Vector2(xDist, yDist)) / 101f;
				npc.localAI[0]++;
				return;
			}
			npc.dontTakeDamage = false;
		}
		targetDist = homingSpeed / targetDist;
		xDist *= targetDist;
		yDist *= targetDist;
		npc.velocity.X = (npc.velocity.X * 100f + xDist) / 101f;
		npc.velocity.Y = (npc.velocity.Y * 100f + yDist) / 101f;
		if (lantern)
		{
			npc.rotation = npc.velocity.X * 0.08f;
			npc.spriteDirection = ((npc.direction > 0) ? 1 : (-1));
		}
		else
		{
			npc.rotation = (float)Math.Atan2(yDist, xDist) + rotation;
		}
	}

	public static void UnicornAI(NPC npc, Mod mod, bool spin, float bounciness, float speedDetect, float speedAdditive, float bouncy1 = -8.5f, float bouncy2 = -7.5f, float bouncy3 = -7f, float bouncy4 = -6f, float bouncy5 = -8f)
	{
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		bool DogPhase1 = npc.type == ModContent.NPCType<Rimehound>() && (double)npc.life > (double)npc.lifeMax * (CalamityWorld.death ? 0.9 : (CalamityWorld.revenge ? 0.7 : 0.5));
		bool DogPhase2 = npc.type == ModContent.NPCType<Rimehound>() && (double)npc.life <= (double)npc.lifeMax * (CalamityWorld.death ? 0.9 : (CalamityWorld.revenge ? 0.7 : 0.5));
		int turnAroundDelay = 30;
		bool isRunning = false;
		bool shouldTurnAround = false;
		if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
		{
			isRunning = true;
			npc.ai[3]++;
		}
		int turnAroundDelayMult = (DogPhase1 ? 10 : 4);
		if (!DogPhase1)
		{
			bool noYVelocity = npc.velocity.Y == 0f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (i != npc.whoAmI && Main.npc[i].active && Main.npc[i].type == npc.type && Math.Abs(npc.position.X - Main.npc[i].position.X) + Math.Abs(npc.position.Y - Main.npc[i].position.Y) < (float)npc.width)
				{
					if (npc.position.X < Main.npc[i].position.X)
					{
						npc.velocity.X -= 0.05f;
					}
					else
					{
						npc.velocity.X += 0.05f;
					}
					if (npc.position.Y < Main.npc[i].position.Y)
					{
						npc.velocity.Y -= 0.05f;
					}
					else
					{
						npc.velocity.Y += 0.05f;
					}
				}
			}
			if (noYVelocity)
			{
				npc.velocity.Y = 0f;
			}
		}
		if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)turnAroundDelay) | isRunning)
		{
			npc.ai[3]++;
			shouldTurnAround = true;
		}
		else if (npc.ai[3] > 0f)
		{
			npc.ai[3]--;
		}
		if (npc.ai[3] > (float)(turnAroundDelay * turnAroundDelayMult))
		{
			npc.ai[3] = 0f;
		}
		if (npc.justHit)
		{
			npc.ai[3] = 0f;
		}
		if (npc.ai[3] == (float)turnAroundDelay)
		{
			npc.netUpdate = true;
		}
		Vector2 npcPos = default(Vector2);
		((Vector2)(ref npcPos))._002Ector(npc.Center.X, npc.Center.Y);
		float num = Main.player[npc.target].Center.X - npcPos.X;
		float yDist = Main.player[npc.target].Center.Y - npcPos.Y;
		if ((float)Math.Sqrt(num * num + yDist * yDist) < 200f && !shouldTurnAround)
		{
			npc.ai[3] = 0f;
		}
		if (!DogPhase1 && npc.velocity.Y == 0f && Math.Abs(npc.velocity.X) > 3f && ((npc.Center.X < Main.player[npc.target].Center.X && npc.velocity.X > 0f) || (npc.Center.X > Main.player[npc.target].Center.X && npc.velocity.X < 0f)))
		{
			npc.velocity.Y -= bounciness;
			if (npc.type == ModContent.NPCType<DespairStone>())
			{
				SoundEngine.PlaySound(in SoundID.Item14, npc.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 235, 0f, -1f);
				}
				if (Main.zenithWorld)
				{
					float screenShakePower = 2f * Utils.GetLerpValue(1300f, 0f, npc.Distance(Main.LocalPlayer.Center), clamped: true);
					Main.LocalPlayer.SetScreenshake(screenShakePower);
				}
			}
			if (npc.type == ModContent.NPCType<Bohldohr>())
			{
				SoundEngine.PlaySound(in SoundID.NPCHit7, npc.Center);
			}
			if (DogPhase2)
			{
				for (int j = 0; j < 5; j++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 33, 0f, -1f);
				}
			}
			if (npc.type == ModContent.NPCType<AquaticUrchin>())
			{
				for (int l = 0; l < 5; l++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 33, 0f, -1f);
				}
			}
		}
		if (npc.ai[3] < (float)turnAroundDelay)
		{
			npc.TargetClosest();
		}
		else
		{
			if (npc.velocity.X == 0f)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.ai[0]++;
					if (npc.ai[0] >= 2f)
					{
						npc.direction *= -1;
						npc.spriteDirection = npc.direction;
						npc.ai[0] = 0f;
					}
				}
			}
			else
			{
				npc.ai[0] = 0f;
			}
			npc.directionY = -1;
			if (npc.direction == 0)
			{
				npc.direction = 1;
			}
		}
		if (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction < 0) || (npc.velocity.X >= 0f && npc.direction > 0))
		{
			if (Math.Sign(npc.velocity.X) != npc.direction && !DogPhase1)
			{
				npc.velocity.X *= 0.92f;
			}
			MathHelper.Lerp(0.6f, 1f, Math.Abs(Main.windSpeedCurrent));
			Math.Sign(Main.windSpeedCurrent);
			_ = Main.player[npc.target].ZoneSandstorm;
			if (npc.velocity.X < 0f - speedDetect || npc.velocity.X > speedDetect)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= 0.8f;
				}
			}
			else if (npc.velocity.X < speedDetect && npc.direction == 1)
			{
				npc.velocity.X += speedAdditive;
				if (npc.velocity.X > speedDetect)
				{
					npc.velocity.X = speedDetect;
				}
			}
			else if (npc.velocity.X > 0f - speedDetect && npc.direction == -1)
			{
				npc.velocity.X -= speedAdditive;
				if (npc.velocity.X < 0f - speedDetect)
				{
					npc.velocity.X = 0f - speedDetect;
				}
			}
		}
		if (npc.velocity.Y >= 0f)
		{
			int faceDirection = 0;
			if (npc.velocity.X < 0f)
			{
				faceDirection = -1;
			}
			if (npc.velocity.X > 0f)
			{
				faceDirection = 1;
			}
			Vector2 position = npc.position;
			position.X += npc.velocity.X;
			int x = (int)((position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * faceDirection)) / 16f);
			int y = (int)((position.Y + (float)npc.height - 1f) / 16f);
			Tile t_xy = CalamityUtils.ParanoidTileRetrieval(x, y);
			Tile t_xy2 = CalamityUtils.ParanoidTileRetrieval(x, y - 1);
			Tile t_xy3 = CalamityUtils.ParanoidTileRetrieval(x, y - 2);
			Tile t_xy4 = CalamityUtils.ParanoidTileRetrieval(x, y - 3);
			Tile t_xOffY3 = CalamityUtils.ParanoidTileRetrieval(x - faceDirection, y - 3);
			Tile t_xy5 = CalamityUtils.ParanoidTileRetrieval(x, y - 4);
			bool num2 = (float)(x * 16) < position.X + (float)npc.width && (float)(x * 16 + 16) > position.X;
			bool tileSolidityCheck1 = t_xy.HasUnactuatedTile && !t_xy.TopSlope && !t_xy2.TopSlope && Main.tileSolid[t_xy.TileType] && !Main.tileSolidTop[t_xy.TileType];
			bool oneBelowIsSolidHalf = t_xy2.IsHalfBlock && t_xy2.HasUnactuatedTile;
			bool canFallThrough = !t_xy2.HasUnactuatedTile || !Main.tileSolid[t_xy2.TileType] || Main.tileSolidTop[t_xy2.TileType] || (t_xy2.IsHalfBlock && (!t_xy5.HasUnactuatedTile || !Main.tileSolid[t_xy5.TileType] || Main.tileSolidTop[t_xy5.TileType]));
			bool twoDownIsNonSolid = !t_xy3.HasUnactuatedTile || !Main.tileSolid[t_xy3.TileType] || Main.tileSolidTop[t_xy3.TileType];
			bool threeDownIsNonSolid = !t_xy4.HasUnactuatedTile || !Main.tileSolid[t_xy4.TileType] || Main.tileSolidTop[t_xy4.TileType];
			bool threeDownOffsetIsNonSolid = !t_xOffY3.HasUnactuatedTile || !Main.tileSolid[t_xOffY3.TileType];
			if ((num2 && (tileSolidityCheck1 | oneBelowIsSolidHalf)) & canFallThrough & twoDownIsNonSolid & threeDownIsNonSolid & threeDownOffsetIsNonSolid)
			{
				float tilePixelPosition = y * 16;
				if (Main.tile[x, y].IsHalfBlock)
				{
					tilePixelPosition += 8f;
				}
				if (Main.tile[x, y - 1].IsHalfBlock)
				{
					tilePixelPosition -= 8f;
				}
				if (tilePixelPosition < position.Y + (float)npc.height)
				{
					float percentageTileRisen = position.Y + (float)npc.height - tilePixelPosition;
					if ((double)percentageTileRisen <= 16.1)
					{
						npc.gfxOffY += npc.position.Y + (float)npc.height - tilePixelPosition;
						npc.position.Y = tilePixelPosition - (float)npc.height;
						if (percentageTileRisen < 9f)
						{
							npc.stepSpeed = 1f;
						}
						else
						{
							npc.stepSpeed = 2f;
						}
					}
				}
			}
		}
		if (npc.velocity.Y == 0f)
		{
			int npcTileX = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 2) * npc.direction) + npc.velocity.X * 5f) / 16f);
			int npcTileY = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
			int spriteDirection = npc.spriteDirection;
			spriteDirection *= -1;
			if ((npc.velocity.X < 0f && spriteDirection == -1) || (npc.velocity.X > 0f && spriteDirection == 1))
			{
				if (Main.tile[npcTileX, npcTileY - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, npcTileY - 2].TileType])
				{
					if (Main.tile[npcTileX, npcTileY - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, npcTileY - 3].TileType])
					{
						npc.velocity.Y = bouncy1;
						npc.netUpdate = true;
					}
					else
					{
						npc.velocity.Y = bouncy2;
						npc.netUpdate = true;
					}
				}
				else if (Main.tile[npcTileX, npcTileY - 1].HasUnactuatedTile && !Main.tile[npcTileX, npcTileY - 1].TopSlope && Main.tileSolid[Main.tile[npcTileX, npcTileY - 1].TileType])
				{
					npc.velocity.Y = bouncy3;
					npc.netUpdate = true;
				}
				else if (npc.position.Y + (float)npc.height - (float)(npcTileY * 16) > 20f && Main.tile[npcTileX, npcTileY].HasUnactuatedTile && !Main.tile[npcTileX, npcTileY].TopSlope && Main.tileSolid[Main.tile[npcTileX, npcTileY].TileType])
				{
					npc.velocity.Y = bouncy4;
					npc.netUpdate = true;
				}
				else if ((npc.directionY < 0 || Math.Abs(npc.velocity.X) > 3f) && (!Main.tile[npcTileX, npcTileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[npcTileX, npcTileY + 1].TileType]) && (!Main.tile[npcTileX, npcTileY + 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[npcTileX, npcTileY + 2].TileType]) && (!Main.tile[npcTileX + npc.direction, npcTileY + 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[npcTileX + npc.direction, npcTileY + 3].TileType]))
				{
					npc.velocity.Y = bouncy5;
					npc.netUpdate = true;
				}
			}
		}
		if (spin)
		{
			npc.rotation += npc.velocity.X * 0.05f;
			npc.spriteDirection = -npc.direction;
		}
	}

	public static void PassiveSwimmingAI(NPC npc, Mod mod, int passiveness, float detectRange, float xSpeed, float ySpeed, float speedLimitX, float speedLimitY, float rotation, bool spriteFacesLeft = true)
	{
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		if (spriteFacesLeft)
		{
			npc.spriteDirection = ((npc.direction > 0) ? 1 : (-1));
		}
		else
		{
			npc.spriteDirection = ((npc.direction <= 0) ? 1 : (-1));
		}
		npc.noGravity = true;
		if (npc.direction == 0)
		{
			npc.TargetClosest();
		}
		Player target = Main.player[npc.target];
		if (npc.justHit && passiveness != 3)
		{
			npc.chaseable = true;
		}
		bool hasWetTarget;
		Vector2 val;
		if (npc.wet)
		{
			hasWetTarget = npc.chaseable;
			npc.TargetClosest(faceTarget: false);
			target = Main.player[npc.target];
			if (passiveness != 2)
			{
				if (npc.type == ModContent.NPCType<Frogfish>() && target.wet && !target.dead)
				{
					hasWetTarget = true;
					npc.chaseable = true;
				}
				if (npc.type == ModContent.NPCType<Sulflounder>())
				{
					if (!target.dead)
					{
						hasWetTarget = true;
						npc.chaseable = true;
					}
				}
				else
				{
					if (target.wet && !target.dead)
					{
						val = target.Center - npc.Center;
						if (((Vector2)(ref val)).Length() < detectRange && Collision.CanHit(npc.position, npc.width, npc.height, target.position, target.width, target.height))
						{
							hasWetTarget = true;
							npc.chaseable = true;
							goto IL_013e;
						}
					}
					if (passiveness == 1)
					{
						hasWetTarget = false;
					}
				}
			}
			goto IL_013e;
		}
		if (npc.velocity.Y == 0f)
		{
			npc.velocity.X *= 0.94f;
			if (npc.velocity.X > -0.2f && npc.velocity.X < 0.2f)
			{
				npc.velocity.X = 0f;
			}
		}
		npc.velocity.Y += 0.4f;
		if (npc.velocity.Y > 12f)
		{
			npc.velocity.Y = 12f;
		}
		npc.ai[0] = 1f;
		goto IL_0a56;
		IL_013e:
		if ((target.dead || !Collision.CanHit(npc.position, npc.width, npc.height, target.position, target.width, target.height)) & hasWetTarget)
		{
			hasWetTarget = false;
		}
		if (!hasWetTarget || passiveness == 2)
		{
			if (passiveness == 0)
			{
				npc.TargetClosest(faceTarget: false);
				target = Main.player[npc.target];
			}
			if (npc.collideX)
			{
				npc.velocity.X *= -1f;
				npc.direction *= -1;
				npc.netUpdate = true;
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = Math.Abs(npc.velocity.Y) * -1f;
					npc.directionY = -1;
					npc.ai[0] = -1f;
				}
				else if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y = Math.Abs(npc.velocity.Y);
					npc.directionY = 1;
					npc.ai[0] = 1f;
				}
			}
		}
		if (hasWetTarget && passiveness != 2)
		{
			npc.TargetClosest();
			target = Main.player[npc.target];
			if (passiveness == 3)
			{
				npc.velocity.X = npc.velocity.X - (float)npc.direction * xSpeed;
				npc.velocity.Y = npc.velocity.Y - (float)npc.directionY * ySpeed;
			}
			else
			{
				npc.velocity.X = npc.velocity.X + (float)npc.direction * (CalamityWorld.death ? (2f * xSpeed) : (CalamityWorld.revenge ? (1.5f * xSpeed) : xSpeed));
				npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * (CalamityWorld.death ? (2f * ySpeed) : (CalamityWorld.revenge ? (1.5f * ySpeed) : ySpeed));
			}
			float velocityCapX = ((CalamityWorld.death && passiveness != 3) ? (2f * speedLimitX) : (CalamityWorld.revenge ? (1.5f * speedLimitX) : speedLimitX));
			float velocityCapY = ((CalamityWorld.death && passiveness != 3) ? (2f * speedLimitY) : (CalamityWorld.revenge ? (1.5f * speedLimitY) : speedLimitY));
			npc.velocity.X = MathHelper.Clamp(npc.velocity.X, 0f - velocityCapX, velocityCapX);
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, 0f - velocityCapY, velocityCapY);
			if (npc.justHit)
			{
				npc.localAI[0] = 0f;
			}
			if (npc.type == ModContent.NPCType<Laserfish>())
			{
				npc.localAI[0] += (CalamityWorld.death ? 2f : (CalamityWorld.revenge ? 1.5f : 1f));
				if (Main.netMode != 1 && npc.localAI[0] >= 120f)
				{
					npc.localAI[0] = 0f;
					if (Collision.CanHit(npc.position, npc.width, npc.height, target.position, target.width, target.height))
					{
						Vector2 vector = default(Vector2);
						((Vector2)(ref vector))._002Ector(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
						float velX = target.Center.X - vector.X + Main.rand.NextFloat(-20f, 20f);
						float velY = target.Center.Y - vector.Y + Main.rand.NextFloat(-20f, 20f);
						float dist = (float)Math.Sqrt(velX * velX + velY * velY);
						dist = 5f / dist;
						velX *= dist;
						velY *= dist;
						int damage = (Main.masterMode ? 34 : (Main.expertMode ? 40 : 50));
						int beam = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X + ((npc.spriteDirection == 1) ? 25f : (-25f)), npc.Center.Y + ((target.position.Y > npc.Center.Y) ? 5f : (-5f)), velX, velY, 259, damage, 0f, Main.myPlayer);
						Main.projectile[beam].tileCollide = true;
					}
				}
			}
			if (npc.type == ModContent.NPCType<Sulflounder>())
			{
				val = target.Center - npc.Center;
				if (((Vector2)(ref val)).Length() < 350f)
				{
					npc.localAI[0] += (CalamityWorld.death ? 3f : (CalamityWorld.revenge ? 2f : 1f));
					if (Main.netMode != 1 && npc.localAI[0] >= 180f)
					{
						npc.localAI[0] = 0f;
						if (Collision.CanHit(npc.position, npc.width, npc.height, target.position, target.width, target.height))
						{
							Vector2 vector2 = default(Vector2);
							((Vector2)(ref vector2))._002Ector(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
							float velX2 = target.Center.X - vector2.X + Main.rand.NextFloat(-20f, 20f);
							float velY2 = target.Center.Y - vector2.Y + Main.rand.NextFloat(-20f, 20f);
							float dist2 = (float)Math.Sqrt(velX2 * velX2 + velY2 * velY2);
							dist2 = 4f / dist2;
							velX2 *= dist2;
							velY2 *= dist2;
							int damage2 = (Main.masterMode ? 21 : (Main.expertMode ? 25 : 35));
							Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X + ((npc.spriteDirection == 1) ? 10f : (-10f)), npc.Center.Y, velX2, velY2, ModContent.ProjectileType<SulphuricAcidMist>(), damage2, 0f, Main.myPlayer);
						}
					}
				}
			}
			if (npc.type == ModContent.NPCType<SeaMinnow>())
			{
				npc.direction *= -1;
			}
		}
		else
		{
			npc.velocity.X += (float)npc.direction * 0.1f;
			if (npc.velocity.X < -2.5f || npc.velocity.X > 2.5f)
			{
				npc.velocity.X *= 0.95f;
			}
			if (npc.ai[0] == -1f)
			{
				npc.velocity.Y -= 0.01f;
				if (npc.velocity.Y < -0.3f)
				{
					npc.ai[0] = 1f;
				}
			}
			else
			{
				npc.velocity.Y += 0.01f;
				if (npc.velocity.Y > 0.3f)
				{
					npc.ai[0] = -1f;
				}
			}
		}
		int npcTileX = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
		int npcTileY = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
		if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount > 128)
		{
			if (Main.tile[npcTileX, npcTileY + 1].HasTile)
			{
				npc.ai[0] = -1f;
			}
			else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
			{
				npc.ai[0] = -1f;
			}
		}
		if (npc.velocity.Y > 0.4f || npc.velocity.Y < -0.4f)
		{
			npc.velocity.Y *= 0.95f;
		}
		goto IL_0a56;
		IL_0a56:
		npc.rotation = npc.velocity.Y * (float)npc.direction * rotation;
		float rotationLimit = 2f * rotation;
		npc.rotation = MathHelper.Clamp(npc.rotation, 0f - rotationLimit, rotationLimit);
	}
}
