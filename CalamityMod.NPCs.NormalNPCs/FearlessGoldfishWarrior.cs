using System;
using System.Collections.Generic;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class FearlessGoldfishWarrior : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = (Main.hardMode ? 100 : 30);
		base.NPC.width = 36;
		base.NPC.height = 32;
		base.NPC.defense = (Main.hardMode ? 10 : 2);
		base.NPC.lifeMax = (Main.hardMode ? 150 : 50);
		base.NPC.knockBackResist = (Main.hardMode ? 0.2f : 0.5f);
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<FearlessGoldfishWarriorBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.FearlessGoldfishWarrior")
		});
	}

	public override void AI()
	{
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		bool noXMovement = false;
		if (base.NPC.velocity.X == 0f)
		{
			noXMovement = true;
		}
		if (base.NPC.justHit)
		{
			noXMovement = false;
		}
		bool isWalking = false;
		bool unusedFlag = false;
		int backUpTimer = 60;
		if (base.NPC.velocity.Y == 0f && ((base.NPC.velocity.X > 0f && base.NPC.direction < 0) || (base.NPC.velocity.X < 0f && base.NPC.direction > 0)))
		{
			isWalking = true;
		}
		if ((base.NPC.position.X == base.NPC.oldPosition.X || base.NPC.ai[3] >= (float)backUpTimer) | isWalking)
		{
			base.NPC.ai[3]++;
		}
		else if ((double)Math.Abs(base.NPC.velocity.X) > 0.9 && base.NPC.ai[3] > 0f)
		{
			base.NPC.ai[3]--;
		}
		if (base.NPC.ai[3] > (float)(backUpTimer * 10))
		{
			base.NPC.ai[3] = 0f;
		}
		if (base.NPC.justHit)
		{
			base.NPC.ai[3] = 0f;
		}
		if (base.NPC.ai[3] == (float)backUpTimer)
		{
			base.NPC.netUpdate = true;
		}
		if (base.NPC.ai[3] < (float)backUpTimer)
		{
			base.NPC.TargetClosest();
		}
		else if (base.NPC.ai[2] <= 0f)
		{
			if (base.NPC.velocity.X == 0f)
			{
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.ai[0]++;
					if (base.NPC.ai[0] >= 2f)
					{
						base.NPC.direction *= -1;
						base.NPC.spriteDirection = base.NPC.direction;
						base.NPC.ai[0] = 0f;
					}
				}
			}
			else
			{
				base.NPC.ai[0] = 0f;
			}
			if (base.NPC.direction == 0)
			{
				base.NPC.direction = 1;
			}
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 18f)
		{
			base.NPC.ai[3] = 0f;
			base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
			{
				base.NPC.velocity.X = 0f;
			}
			return;
		}
		float maxVelocity = (CalamityWorld.death ? 3f : (CalamityWorld.revenge ? 2f : 1f));
		float acceleration = (CalamityWorld.death ? 0.28f : (CalamityWorld.revenge ? 0.18f : 0.08f));
		maxVelocity += (1f - (float)base.NPC.life / (float)base.NPC.lifeMax) * 2f;
		acceleration += (1f - (float)base.NPC.life / (float)base.NPC.lifeMax) * 0.2f;
		if (base.NPC.velocity.X < 0f - maxVelocity || base.NPC.velocity.X > maxVelocity)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.7f;
			}
		}
		else if (base.NPC.velocity.X < maxVelocity && base.NPC.direction == 1)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
			if (base.NPC.velocity.X > maxVelocity)
			{
				base.NPC.velocity.X = maxVelocity;
			}
		}
		else if (base.NPC.velocity.X > 0f - maxVelocity && base.NPC.direction == -1)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
			if (base.NPC.velocity.X < 0f - maxVelocity)
			{
				base.NPC.velocity.X = 0f - maxVelocity;
			}
		}
		bool isOnSolidTile = false;
		if (base.NPC.velocity.Y == 0f)
		{
			int yTile = (int)(base.NPC.position.Y + (float)base.NPC.height + 7f) / 16;
			int num = (int)base.NPC.position.X / 16;
			int maxXTile = (int)(base.NPC.position.X + (float)base.NPC.width) / 16;
			for (int xTile = num; xTile <= maxXTile; xTile++)
			{
				if (Main.tile[xTile, yTile] == null)
				{
					return;
				}
				if (Main.tile[xTile, yTile].HasUnactuatedTile && Main.tileSolid[Main.tile[xTile, yTile].TileType])
				{
					isOnSolidTile = true;
					break;
				}
			}
		}
		if (base.NPC.velocity.Y >= 0f)
		{
			int fallFaceDirection = 0;
			if (base.NPC.velocity.X < 0f)
			{
				fallFaceDirection = -1;
			}
			if (base.NPC.velocity.X > 0f)
			{
				fallFaceDirection = 1;
			}
			Vector2 fishePosition = base.NPC.position;
			fishePosition.X += base.NPC.velocity.X;
			int xTileBelow = (int)((fishePosition.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 1) * fallFaceDirection)) / 16f);
			int yTileBelow = (int)((fishePosition.Y + (float)base.NPC.height - 1f) / 16f);
			if ((float)(xTileBelow * 16) < fishePosition.X + (float)base.NPC.width && (float)(xTileBelow * 16 + 16) > fishePosition.X && ((Main.tile[xTileBelow, yTileBelow].HasUnactuatedTile && !Main.tile[xTileBelow, yTileBelow].TopSlope && !Main.tile[xTileBelow, yTileBelow - 1].TopSlope && Main.tileSolid[Main.tile[xTileBelow, yTileBelow].TileType] && !Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow].TileType]) || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile)) && (!Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 1].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 1].TileType] || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && (!Main.tile[xTileBelow, yTileBelow - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 4].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 4].TileType]))) && (!Main.tile[xTileBelow, yTileBelow - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 2].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 2].TileType]) && (!Main.tile[xTileBelow, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 3].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 3].TileType]) && (!Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].TileType]))
			{
				float yPixelDistance = yTileBelow * 16;
				if (Main.tile[xTileBelow, yTileBelow].IsHalfBlock)
				{
					yPixelDistance += 8f;
				}
				if (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock)
				{
					yPixelDistance -= 8f;
				}
				if (yPixelDistance < fishePosition.Y + (float)base.NPC.height)
				{
					float percentageTileRisen = fishePosition.Y + (float)base.NPC.height - yPixelDistance;
					float fullTileAmt = 16.1f;
					if (percentageTileRisen <= fullTileAmt)
					{
						base.NPC.gfxOffY += base.NPC.position.Y + (float)base.NPC.height - yPixelDistance;
						base.NPC.position.Y = yPixelDistance - (float)base.NPC.height;
						if (percentageTileRisen < 9f)
						{
							base.NPC.stepSpeed = 1f;
						}
						else
						{
							base.NPC.stepSpeed = 2f;
						}
					}
				}
			}
		}
		if (isOnSolidTile)
		{
			int doorCheckX = (int)((base.NPC.position.X + (float)(base.NPC.width / 2) + (float)(15 * base.NPC.direction)) / 16f);
			int doorCheckY = (int)((base.NPC.position.Y + (float)base.NPC.height - 15f) / 16f);
			if ((Main.tile[doorCheckX, doorCheckY - 1].HasUnactuatedTile && (Main.tile[doorCheckX, doorCheckY - 1].TileType == 10 || Main.tile[doorCheckX, doorCheckY - 1].TileType == 388)) & unusedFlag)
			{
				base.NPC.ai[2]++;
				base.NPC.ai[3] = 0f;
				if (!(base.NPC.ai[2] >= 60f))
				{
					return;
				}
				base.NPC.velocity.X = 0.5f * (0f - (float)base.NPC.direction);
				int doorOpenInc = 5;
				if (Main.tile[doorCheckX, doorCheckY - 1].TileType == 388)
				{
					doorOpenInc = 2;
				}
				base.NPC.ai[1] += doorOpenInc;
				base.NPC.ai[2] = 0f;
				bool letMeIn = false;
				if (base.NPC.ai[1] >= 10f)
				{
					letMeIn = true;
					base.NPC.ai[1] = 10f;
				}
				WorldGen.KillTile(doorCheckX, doorCheckY - 1, fail: true);
				if (!((Main.netMode != 1 || !letMeIn) & letMeIn) || Main.netMode == 1)
				{
					return;
				}
				if (Main.tile[doorCheckX, doorCheckY - 1].TileType == 10)
				{
					bool canOpenDoor = WorldGen.OpenDoor(doorCheckX, doorCheckY - 1, base.NPC.direction);
					if (!canOpenDoor)
					{
						base.NPC.ai[3] = backUpTimer;
						base.NPC.netUpdate = true;
					}
					if (Main.dedServ & canOpenDoor)
					{
						NetMessage.SendData(19, -1, -1, null, 0, doorCheckX, doorCheckY - 1, base.NPC.direction);
					}
				}
				if (Main.tile[doorCheckX, doorCheckY - 1].TileType == 388)
				{
					bool canOpenTallGate = WorldGen.ShiftTallGate(doorCheckX, doorCheckY - 1, closing: false);
					if (!canOpenTallGate)
					{
						base.NPC.ai[3] = backUpTimer;
						base.NPC.netUpdate = true;
					}
					if (Main.dedServ & canOpenTallGate)
					{
						NetMessage.SendData(19, -1, -1, null, 4, doorCheckX, doorCheckY - 1);
					}
				}
				return;
			}
			int faceDirection = base.NPC.spriteDirection;
			if ((!(base.NPC.velocity.X < 0f) || faceDirection != -1) && (!(base.NPC.velocity.X > 0f) || faceDirection != 1))
			{
				return;
			}
			if (base.NPC.height >= 32 && Main.tile[doorCheckX, doorCheckY - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[doorCheckX, doorCheckY - 2].TileType])
			{
				if (Main.tile[doorCheckX, doorCheckY - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[doorCheckX, doorCheckY - 3].TileType])
				{
					base.NPC.velocity.Y = -8f;
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.velocity.Y = -7f;
					base.NPC.netUpdate = true;
				}
			}
			else if (Main.tile[doorCheckX, doorCheckY - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[doorCheckX, doorCheckY - 1].TileType])
			{
				base.NPC.velocity.Y = -6f;
				base.NPC.netUpdate = true;
			}
			else if (base.NPC.position.Y + (float)base.NPC.height - (float)(doorCheckY * 16) > 20f && Main.tile[doorCheckX, doorCheckY].HasUnactuatedTile && !Main.tile[doorCheckX, doorCheckY].TopSlope && Main.tileSolid[Main.tile[doorCheckX, doorCheckY].TileType])
			{
				base.NPC.velocity.Y = -5f;
				base.NPC.netUpdate = true;
			}
			else if (base.NPC.directionY < 0 && (!Main.tile[doorCheckX, doorCheckY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[doorCheckX, doorCheckY + 1].TileType]) && (!Main.tile[doorCheckX + base.NPC.direction, doorCheckY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[doorCheckX + base.NPC.direction, doorCheckY + 1].TileType]))
			{
				base.NPC.velocity.Y = -8f;
				base.NPC.velocity.X = base.NPC.velocity.X * 1.5f;
				base.NPC.netUpdate = true;
			}
			else if (unusedFlag)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
			}
			if (((base.NPC.velocity.Y == 0f) & noXMovement) && base.NPC.ai[3] == 1f)
			{
				base.NPC.velocity.Y = -5f;
			}
		}
		else if (unusedFlag)
		{
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 18f || base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 6.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			}
			if (base.NPC.frame.Y < frameHeight * 5)
			{
				base.NPC.frame.Y = frameHeight * 5;
			}
			if (base.NPC.frame.Y > frameHeight * 9)
			{
				base.NPC.frame.Y = frameHeight * 5;
			}
			return;
		}
		base.NPC.frameCounter += Math.Abs(base.NPC.velocity.X);
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
		}
		if (base.NPC.velocity.Y == 0f)
		{
			if (base.NPC.direction == 1)
			{
				base.NPC.spriteDirection = 1;
			}
			if (base.NPC.direction == -1)
			{
				base.NPC.spriteDirection = -1;
			}
			if (base.NPC.velocity.X == 0f)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = 0;
			}
			else if (base.NPC.frame.Y > frameHeight * 4)
			{
				base.NPC.frame.Y = 0;
			}
		}
		else
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = frameHeight;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !spawnInfo.Player.ZoneRain || spawnInfo.Player.ZoneDesert || spawnInfo.Player.Calamity().ZoneSulphur)
		{
			return 0f;
		}
		return SpawnCondition.OverworldDayRain.Chance * 0.05f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		bool instakill = false;
		foreach (string item in new List<string> { "LordMetarex", "Metarex" })
		{
			if (item.ToLower() == target.name.ToLower())
			{
				instakill = true;
				break;
			}
		}
		if (instakill)
		{
			target.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.Goldfish").ToNetworkText(target.name)), 1000.0, 0);
			modifiers.FinalDamage *= (float)target.statLifeMax2 * Main.rand.NextFloat(2f, 3.5f);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(687);
		npcLoot.AddIf(() => Main.hardMode, 517);
	}
}
