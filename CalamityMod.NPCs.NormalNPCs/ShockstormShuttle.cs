using System;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class ShockstormShuttle : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 3f;
		base.NPC.damage = 0;
		base.NPC.width = 64;
		base.NPC.height = 38;
		base.NPC.defense = 15;
		base.NPC.lifeMax = 250;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ShockstormShuttleBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ShockstormShuttle")
		});
	}

	public override void AI()
	{
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_092d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_0940: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.justHit)
		{
			base.NPC.localAI[0] = 0f;
		}
		if (Main.netMode != 1)
		{
			base.NPC.localAI[0]++;
			if (base.NPC.localAI[0] >= 60f)
			{
				base.NPC.localAI[0] = 0f;
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					float projSpeed = 12f;
					Vector2 npcPos = base.NPC.Center;
					float targetX = Main.player[base.NPC.target].Center.X - npcPos.X;
					float YAdjust = Math.Abs(targetX) * 0.1f;
					float targetY = Main.player[base.NPC.target].Center.Y - npcPos.Y - YAdjust;
					Vector2 velocity = default(Vector2);
					((Vector2)(ref velocity))._002Ector(targetX, targetY);
					float targetDist = ((Vector2)(ref velocity)).Length();
					targetDist = projSpeed / targetDist;
					velocity.X *= targetDist;
					velocity.Y *= targetDist;
					int projDmg = (Main.expertMode ? 22 : 30);
					int projType = 435;
					if (Main.rand.NextBool(8))
					{
						projType = 449;
					}
					npcPos.X += velocity.X;
					npcPos.Y += velocity.Y;
					int spread = 20;
					for (int i = 0; i < 2; i++)
					{
						velocity = Main.player[base.NPC.target].Center - npcPos;
						targetDist = ((Vector2)(ref velocity)).Length();
						targetDist = projSpeed / targetDist;
						velocity.X += Main.rand.Next(-spread, spread + 1);
						velocity.Y += Main.rand.Next(-spread, spread + 1);
						velocity.X *= targetDist;
						velocity.Y *= targetDist;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcPos, velocity, projType, projDmg, 0f, Main.myPlayer);
					}
				}
			}
		}
		if (base.NPC.localAI[3] == 0f && Main.netMode != 1)
		{
			base.NPC.localAI[3] = 1f;
		}
		Vector2 shuttleCenter = base.NPC.Center;
		Player targetedPlayer = Main.player[base.NPC.target];
		if (base.NPC.target < 0 || base.NPC.target == 255 || targetedPlayer.dead || !targetedPlayer.active)
		{
			base.NPC.TargetClosest();
			targetedPlayer = Main.player[base.NPC.target];
			base.NPC.netUpdate = true;
		}
		if ((targetedPlayer.dead || Vector2.Distance(targetedPlayer.Center, shuttleCenter) > 3200f) && base.NPC.ai[0] != 1f)
		{
			base.NPC.ai[0] = -1f;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - 0.4f;
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
			if (!targetedPlayer.dead)
			{
				base.NPC.timeLeft = 300;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 0f)
			{
				return;
			}
			int movementPattern = 0;
			if (base.NPC.ai[3] >= 580f)
			{
				movementPattern = 0;
			}
			else if (base.NPC.ai[3] >= 440f)
			{
				movementPattern = 5;
			}
			else if (base.NPC.ai[3] >= 420f)
			{
				movementPattern = 4;
			}
			else if (base.NPC.ai[3] >= 280f)
			{
				movementPattern = 3;
			}
			else if (base.NPC.ai[3] >= 260f)
			{
				movementPattern = 2;
			}
			else if (base.NPC.ai[3] >= 20f)
			{
				movementPattern = 1;
			}
			base.NPC.ai[3]++;
			if (base.NPC.ai[3] >= 600f)
			{
				base.NPC.ai[3] = 0f;
			}
			int patternCompare = movementPattern;
			if (base.NPC.ai[3] >= 580f)
			{
				movementPattern = 0;
			}
			else if (base.NPC.ai[3] >= 440f)
			{
				movementPattern = 5;
			}
			else if (base.NPC.ai[3] >= 420f)
			{
				movementPattern = 4;
			}
			else if (base.NPC.ai[3] >= 280f)
			{
				movementPattern = 3;
			}
			else if (base.NPC.ai[3] >= 260f)
			{
				movementPattern = 2;
			}
			else if (base.NPC.ai[3] >= 20f)
			{
				movementPattern = 1;
			}
			if (movementPattern != patternCompare)
			{
				if (movementPattern == 0)
				{
					base.NPC.ai[2] = 0f;
				}
				if (movementPattern == 1)
				{
					base.NPC.ai[2] = ((Math.Sign((targetedPlayer.Center - shuttleCenter).X) == 1) ? 1 : (-1));
				}
				if (movementPattern == 2)
				{
					base.NPC.ai[2] = 0f;
				}
				base.NPC.netUpdate = true;
			}
			if (movementPattern == 0)
			{
				if (base.NPC.ai[2] == 0f)
				{
					base.NPC.ai[2] = -600 * Math.Sign((shuttleCenter - targetedPlayer.Center).X);
				}
				Vector2 targetingDirection = targetedPlayer.Center + new Vector2(base.NPC.ai[2], -250f) - shuttleCenter;
				if (((Vector2)(ref targetingDirection)).Length() < 50f)
				{
					base.NPC.ai[3] = 19f;
				}
				else
				{
					((Vector2)(ref targetingDirection)).Normalize();
					base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, targetingDirection * 16f, 0.1f);
				}
			}
			if (movementPattern == 1)
			{
				int npcTileX = (int)base.NPC.Center.X / 16;
				int npcTileY = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
				int upwardMoveAmt = 0;
				if (Main.tile[npcTileX, npcTileY].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, npcTileY].TileType] && !Main.tileSolidTop[Main.tile[npcTileX, npcTileY].TileType])
				{
					upwardMoveAmt = 1;
				}
				else
				{
					for (; upwardMoveAmt < 150 && npcTileY + upwardMoveAmt < Main.maxTilesY; upwardMoveAmt++)
					{
						int npcTileYUp = npcTileY + upwardMoveAmt;
						if (Main.tile[npcTileX, npcTileYUp].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, npcTileYUp].TileType] && !Main.tileSolidTop[Main.tile[npcTileX, npcTileYUp].TileType])
						{
							upwardMoveAmt--;
							break;
						}
					}
				}
				float pixelsUpAmt = upwardMoveAmt * 16;
				if (pixelsUpAmt < 250f)
				{
					float velocityUpwards = -4f;
					if (0f - velocityUpwards > pixelsUpAmt)
					{
						velocityUpwards = 0f - pixelsUpAmt;
					}
					base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, velocityUpwards, 0.05f);
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y * 0.95f;
				}
				base.NPC.velocity.X = 3.5f * base.NPC.ai[2];
			}
			switch (movementPattern)
			{
			case 2:
			{
				if (base.NPC.ai[2] == 0f)
				{
					base.NPC.ai[2] = 300 * Math.Sign((shuttleCenter - targetedPlayer.Center).X);
				}
				Vector2 targetingCenter2 = targetedPlayer.Center + new Vector2(base.NPC.ai[2], -170f) - shuttleCenter;
				int npcTileX4 = (int)base.NPC.Center.X / 16;
				int npcTileY4 = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
				int j = 0;
				if (Main.tile[npcTileX4, npcTileY4].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX4, npcTileY4].TileType] && !Main.tileSolidTop[Main.tile[npcTileX4, npcTileY4].TileType])
				{
					j = 1;
				}
				else
				{
					for (; j < 150 && npcTileY4 + j < Main.maxTilesY; j++)
					{
						int npcTileYUp4 = npcTileY4 + j;
						if (Main.tile[npcTileX4, npcTileYUp4].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX4, npcTileYUp4].TileType] && !Main.tileSolidTop[Main.tile[npcTileX4, npcTileYUp4].TileType])
						{
							j--;
							break;
						}
					}
				}
				float pixelsUpAmt2 = j * 16;
				if (pixelsUpAmt2 < 170f)
				{
					targetingCenter2.Y -= 170f - pixelsUpAmt2;
				}
				if (((Vector2)(ref targetingCenter2)).Length() < 70f)
				{
					base.NPC.ai[3] = 279f;
					break;
				}
				((Vector2)(ref targetingCenter2)).Normalize();
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, targetingCenter2 * 20f, 0.1f);
				break;
			}
			case 3:
			{
				float decelerationMult = 0.85f;
				int npcTileX3 = (int)base.NPC.Center.X / 16;
				int npcTileY3 = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
				int upwardsMovement3 = 0;
				if (Main.tile[npcTileX3, npcTileY3].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX3, npcTileY3].TileType] && !Main.tileSolidTop[Main.tile[npcTileX3, npcTileY3].TileType])
				{
					upwardsMovement3 = 1;
				}
				else
				{
					for (; upwardsMovement3 < 150 && npcTileY3 + upwardsMovement3 < Main.maxTilesY; upwardsMovement3++)
					{
						int npcTileYUp3 = npcTileY3 + upwardsMovement3;
						if (Main.tile[npcTileX3, npcTileYUp3].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX3, npcTileYUp3].TileType] && !Main.tileSolidTop[Main.tile[npcTileX3, npcTileYUp3].TileType])
						{
							upwardsMovement3--;
							break;
						}
					}
				}
				float pixelUpAmt3 = upwardsMovement3 * 16;
				if (pixelUpAmt3 < 170f)
				{
					float velocityUpwards3 = -4f;
					if (0f - velocityUpwards3 > pixelUpAmt3)
					{
						velocityUpwards3 = 0f - pixelUpAmt3;
					}
					base.NPC.velocity.Y = MathHelper.Lerp(base.NPC.velocity.Y, velocityUpwards3, 0.05f);
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y * decelerationMult;
				}
				base.NPC.velocity.X = base.NPC.velocity.X * decelerationMult;
				break;
			}
			}
			switch (movementPattern)
			{
			case 4:
			{
				Vector2 targetingCenter4 = targetedPlayer.Center + new Vector2(0f, -250f) - shuttleCenter;
				if (((Vector2)(ref targetingCenter4)).Length() < 50f)
				{
					base.NPC.ai[3] = 439f;
					break;
				}
				((Vector2)(ref targetingCenter4)).Normalize();
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, targetingCenter4 * 16f, 0.1f);
				break;
			}
			case 5:
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.85f;
				break;
			}
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 234, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 234, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ShockstormShuttle").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ShockstormShuttle2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ShockstormShuttle3").Type);
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !Main.hardMode)
		{
			return 0f;
		}
		return SpawnCondition.Sky.Chance * 0.1f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.AddIf(() => NPC.downedGolemBoss, 2860, 1, 10, 30);
		npcLoot.Add(ModContent.ItemType<EssenceofSunlight>(), 2);
		npcLoot.Add(ModContent.ItemType<OracleHeadphones>(), (DateTime.Now.Day == 21 && DateTime.Now.Month == 8) ? 9 : 20);
		npcLoot.DefineConditionalDropSet(() => NPC.downedMoonlord).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<ExodiumCluster>(), 1, 8, 12, 11, 16));
	}
}
