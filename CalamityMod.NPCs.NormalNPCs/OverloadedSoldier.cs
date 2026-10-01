using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class OverloadedSoldier : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 14;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = (NPC.downedMoonlord ? 84 : 42);
		base.NPC.width = 18;
		base.NPC.height = 40;
		base.NPC.defense = 18;
		base.NPC.lifeMax = (NPC.downedMoonlord ? 3000 : 300);
		base.NPC.knockBackResist = 0.3f;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.HitSound = SoundID.NPCHit2;
		base.NPC.DeathSound = SoundID.NPCDeath2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<OverloadedSoldierBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.OverloadedSoldier")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 6.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 13)
			{
				base.NPC.frame.Y = frameHeight;
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
				return;
			}
			if (base.NPC.frame.Y < frameHeight)
			{
				base.NPC.frame.Y = frameHeight;
			}
			if (base.NPC.frame.Y > frameHeight * 13)
			{
				base.NPC.frame.Y = frameHeight;
			}
		}
		else
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = 0;
		}
	}

	public override void AI()
	{
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.35f, 0f, 0.15f);
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
		float maxVelocity = 1.5f;
		float acceleration = 0.1f;
		if (CalamityWorld.death)
		{
			maxVelocity *= 1.5f;
			acceleration *= 1.5f;
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 400f)
		{
			float num = maxVelocity;
			float num2 = (CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6f : 4f));
			Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
			maxVelocity = num + (num2 - ((Vector2)(ref val)).Length() * 0.01f);
		}
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
			int num3 = (int)base.NPC.position.X / 16;
			int maxXTile = (int)(base.NPC.position.X + (float)base.NPC.width) / 16;
			for (int xTile = num3; xTile <= maxXTile; xTile++)
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
			Vector2 ghostPosition = base.NPC.position;
			ghostPosition.X += base.NPC.velocity.X;
			int xTileBelow = (int)((ghostPosition.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 1) * fallFaceDirection)) / 16f);
			int yTileBelow = (int)((ghostPosition.Y + (float)base.NPC.height - 1f) / 16f);
			if ((float)(xTileBelow * 16) < ghostPosition.X + (float)base.NPC.width && (float)(xTileBelow * 16 + 16) > ghostPosition.X && ((Main.tile[xTileBelow, yTileBelow].HasUnactuatedTile && !Main.tile[xTileBelow, yTileBelow].TopSlope && !Main.tile[xTileBelow, yTileBelow - 1].TopSlope && Main.tileSolid[Main.tile[xTileBelow, yTileBelow].TileType] && !Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow].TileType]) || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile)) && (!Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 1].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 1].TileType] || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && (!Main.tile[xTileBelow, yTileBelow - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 4].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 4].TileType]))) && (!Main.tile[xTileBelow, yTileBelow - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 2].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 2].TileType]) && (!Main.tile[xTileBelow, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 3].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 3].TileType]) && (!Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].TileType]))
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
				if (yPixelDistance < ghostPosition.Y + (float)base.NPC.height)
				{
					float percentageTileRisen = ghostPosition.Y + (float)base.NPC.height - yPixelDistance;
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

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.Calamity().ZoneAbyss || spawnInfo.Player.Calamity().ZoneSunkenSea)
		{
			return 0f;
		}
		return SpawnCondition.Underground.Chance * 0.02f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<AncientBoneDust>());
		npcLoot.DefineConditionalDropSet(DropHelper.PostML()).Add(ModContent.ItemType<Necroplasm>());
	}
}
