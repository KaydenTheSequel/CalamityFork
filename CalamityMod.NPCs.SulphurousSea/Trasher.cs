using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SulphurousSea;

public class Trasher : ModNPC
{
	private bool hasBeenHit;

	public override void SetStaticDefaults()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		nPCBestiaryDrawModifiers.Scale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		nPCBestiaryDrawModifiers.Position = Vector2.UnitX * 20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 50;
		base.NPC.width = 150;
		base.NPC.height = 40;
		base.NPC.defense = 22;
		base.NPC.lifeMax = 200;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 3);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath5;
		base.NPC.knockBackResist = 0.15f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<TrasherBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SulphurousSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Trasher")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf2: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.spriteDirection = ((base.NPC.direction <= 0) ? 1 : (-1));
		base.NPC.chaseable = hasBeenHit;
		if (base.NPC.justHit)
		{
			hasBeenHit = true;
		}
		if (base.NPC.wet)
		{
			if (base.NPC.direction == 0)
			{
				base.NPC.TargetClosest();
			}
			base.NPC.noTileCollide = false;
			bool canAttack = hasBeenHit || Main.zenithWorld;
			base.NPC.TargetClosest(faceTarget: false);
			if (Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (((Vector2)(ref val)).Length() < 200f)
				{
					canAttack = true;
				}
			}
			if (Main.player[base.NPC.target].dead & canAttack)
			{
				canAttack = false;
			}
			if (!canAttack)
			{
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * -1f;
					base.NPC.direction *= -1;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.collideY)
				{
					base.NPC.netUpdate = true;
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
						base.NPC.directionY = -1;
						base.NPC.ai[0] = -1f;
					}
					else if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
						base.NPC.directionY = 1;
						base.NPC.ai[0] = 1f;
					}
				}
			}
			if (canAttack)
			{
				base.NPC.TargetClosest();
				if (Main.zenithWorld)
				{
					base.NPC.noTileCollide = true;
				}
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * (CalamityWorld.death ? 0.6f : (CalamityWorld.revenge ? 0.45f : 0.3f));
				base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * (CalamityWorld.death ? 0.2f : (CalamityWorld.revenge ? 0.15f : 0.1f));
				float velocityX = (CalamityWorld.death ? 20f : (CalamityWorld.revenge ? 15f : 10f));
				float velocityY = (CalamityWorld.death ? 10f : (CalamityWorld.revenge ? 7.5f : 5f));
				if (base.NPC.velocity.X > velocityX)
				{
					base.NPC.velocity.X = velocityX;
				}
				if (base.NPC.velocity.X < 0f - velocityX)
				{
					base.NPC.velocity.X = 0f - velocityX;
				}
				if (base.NPC.velocity.Y > velocityY)
				{
					base.NPC.velocity.Y = velocityY;
				}
				if (base.NPC.velocity.Y < 0f - velocityY)
				{
					base.NPC.velocity.Y = 0f - velocityY;
				}
			}
			else
			{
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
				if (base.NPC.velocity.X < -1.5f || base.NPC.velocity.X > 1.5f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
				}
				if (base.NPC.ai[0] == -1f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
					if ((double)base.NPC.velocity.Y < -0.3)
					{
						base.NPC.ai[0] = 1f;
					}
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
					if ((double)base.NPC.velocity.Y > 0.3)
					{
						base.NPC.ai[0] = -1f;
					}
				}
			}
			int npcTileX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
			int npcTileY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
			if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount > 128)
			{
				if (Main.tile[npcTileX, npcTileY + 1].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
				else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
			}
			if ((double)base.NPC.velocity.Y > 0.4 || (double)base.NPC.velocity.Y < -0.4)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.95f;
			}
		}
		else
		{
			if (Collision.WetCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.noTileCollide = false;
				base.NPC.netUpdate = true;
				return;
			}
			base.NPC.noTileCollide = false;
			float velocityBoost = 1f;
			base.NPC.TargetClosest();
			bool closeToTargetX = false;
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5 || CalamityWorld.death)
			{
				velocityBoost = 1.5f;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25 || CalamityWorld.death)
			{
				velocityBoost = 2.5f;
			}
			if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) < 20f)
			{
				closeToTargetX = true;
			}
			if (closeToTargetX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			else
			{
				if (base.NPC.direction > 0)
				{
					base.NPC.velocity.X = (base.NPC.velocity.X * 20f + velocityBoost) / 21f;
				}
				if (base.NPC.direction < 0)
				{
					base.NPC.velocity.X = (base.NPC.velocity.X * 20f - velocityBoost) / 21f;
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
				Vector2 trashPosition = base.NPC.position;
				trashPosition.X += base.NPC.velocity.X;
				int xTileBelow = (int)((trashPosition.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 1) * fallFaceDirection)) / 16f);
				int yTileBelow = (int)((trashPosition.Y + (float)base.NPC.height - 1f) / 16f);
				if ((float)(xTileBelow * 16) < trashPosition.X + (float)base.NPC.width && (float)(xTileBelow * 16 + 16) > trashPosition.X && ((Main.tile[xTileBelow, yTileBelow].HasUnactuatedTile && !Main.tile[xTileBelow, yTileBelow].TopSlope && !Main.tile[xTileBelow, yTileBelow - 1].TopSlope && Main.tileSolid[Main.tile[xTileBelow, yTileBelow].TileType] && !Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow].TileType]) || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile)) && (!Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 1].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 1].TileType] || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && (!Main.tile[xTileBelow, yTileBelow - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 4].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 4].TileType]))) && (!Main.tile[xTileBelow, yTileBelow - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 2].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 2].TileType]) && (!Main.tile[xTileBelow, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow, yTileBelow - 3].TileType] || Main.tileSolidTop[Main.tile[xTileBelow, yTileBelow - 3].TileType]) && (!Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].TileType]))
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
					if (yPixelDistance < trashPosition.Y + (float)base.NPC.height)
					{
						float percentageTileRisen = trashPosition.Y + (float)base.NPC.height - yPixelDistance;
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
				if (base.NPC.oldPosition == base.NPC.position)
				{
					base.NPC.velocity.Y = -10f;
					base.NPC.netUpdate = true;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + (Main.zenithWorld ? 0.5f : 0.55f);
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
		}
		base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.05f;
		base.NPC.rotation = MathHelper.Clamp(base.NPC.rotation, -0.1f, 0.1f);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (hasBeenHit ? 1.25 : 1.0);
		if (base.NPC.frameCounter < 6.0)
		{
			base.NPC.frame.Y = 0;
		}
		else if (base.NPC.frameCounter < 12.0)
		{
			base.NPC.frame.Y = frameHeight;
		}
		else if (base.NPC.frameCounter < 18.0)
		{
			base.NPC.frame.Y = frameHeight * 2;
		}
		else if (base.NPC.frameCounter < 24.0)
		{
			base.NPC.frame.Y = frameHeight * 3;
		}
		else
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = 0;
		}
		if (!base.NPC.wet)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight * 4;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe)
		{
			return 0f;
		}
		if (spawnInfo.Player.Calamity().ZoneSulphur || (spawnInfo.Player.Calamity().ZoneSulphur && spawnInfo.Water))
		{
			return 0.05f;
		}
		return 0f;
	}

	public override void OnKill()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!NPC.savedAngler && Main.netMode != 1 && !NPC.AnyNPCs(369) && !NPC.AnyNPCs(376))
		{
			NPC.NewNPC(base.NPC.GetSource_Death(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 369);
			NPC.savedAngler = true;
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(268, 20);
		npcLoot.Add(ModContent.ItemType<TrashmanTrashcan>(), 20);
		npcLoot.AddIf(() => Main.hardMode, 2270, 10);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Trasher").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Trasher2").Type);
			}
		}
	}
}
