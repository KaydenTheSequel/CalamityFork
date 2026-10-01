using System;
using System.IO;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class ImpiousImmolator : ModNPC
{
	public bool hasBeenHit;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.damage = 0;
		base.NPC.width = 60;
		base.NPC.height = 60;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 3000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 50);
		base.NPC.HitSound = SoundID.NPCHit5;
		base.NPC.DeathSound = SoundID.NPCDeath7;
		base.NPC.knockBackResist = 0.2f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ImpiousImmolatorBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ImpiousImmolator")
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
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		base.NPC.noGravity = true;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.justHit)
		{
			hasBeenHit = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (!base.NPC.wet)
		{
			bool canAttack = hasBeenHit;
			base.NPC.TargetClosest(faceTarget: false);
			if ((Main.player[base.NPC.target].wet || Main.player[base.NPC.target].dead) & canAttack)
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
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.2f;
				base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * 0.2f;
				float velocityMax = (CalamityWorld.death ? 16f : (CalamityWorld.revenge ? 14f : 12f));
				if (base.NPC.velocity.X > velocityMax)
				{
					base.NPC.velocity.X = velocityMax;
				}
				if (base.NPC.velocity.X < 0f - velocityMax)
				{
					base.NPC.velocity.X = 0f - velocityMax;
				}
				if (base.NPC.velocity.Y > velocityMax)
				{
					base.NPC.velocity.Y = velocityMax;
				}
				if (base.NPC.velocity.Y < 0f - velocityMax)
				{
					base.NPC.velocity.Y = 0f - velocityMax;
				}
				if (base.NPC.justHit)
				{
					base.NPC.localAI[0] = 0f;
				}
				base.NPC.localAI[0]++;
				if (Main.netMode != 1 && base.NPC.localAI[0] >= (CalamityWorld.death ? 50f : (CalamityWorld.revenge ? 70f : 90f)))
				{
					base.NPC.localAI[0] = 0f;
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						Vector2 beamPosition = default(Vector2);
						((Vector2)(ref beamPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)(base.NPC.height / 2));
						float targetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - beamPosition.X + (float)Main.rand.Next(-20, 21);
						float targetYDist = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - beamPosition.Y + (float)Main.rand.Next(-20, 21);
						float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
						targetDistance = 12f / targetDistance;
						targetXDist *= targetDistance;
						targetYDist *= targetDistance;
						int damage = (Main.masterMode ? 35 : (Main.expertMode ? 42 : 55));
						int beam = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, targetXDist, targetYDist, ModContent.ProjectileType<FlameBurstHostile>(), damage, 0f, Main.myPlayer);
						Main.projectile[beam].tileCollide = true;
					}
				}
			}
			else
			{
				base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.1f;
				if (base.NPC.velocity.X < -3f || base.NPC.velocity.X > 3f)
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
			int tileCheckX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
			int tileCheckY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
			if (Main.tile[tileCheckX, tileCheckY - 1].LiquidAmount < 128)
			{
				if (Main.tile[tileCheckX, tileCheckY + 1].HasTile)
				{
					base.NPC.ai[0] = -1f;
				}
				else if (Main.tile[tileCheckX, tileCheckY + 2].HasTile)
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
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.94f;
				if ((double)base.NPC.velocity.X > -0.2 && (double)base.NPC.velocity.X < 0.2)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.3f;
			if (base.NPC.velocity.Y > 5f)
			{
				base.NPC.velocity.Y = 5f;
			}
			base.NPC.ai[0] = 1f;
		}
		base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
		if ((double)base.NPC.rotation < -0.1)
		{
			base.NPC.rotation = -0.1f;
		}
		if ((double)base.NPC.rotation > 0.1)
		{
			base.NPC.rotation = 0.1f;
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (hasBeenHit ? 0.25f : 0.125f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedMoonlord || spawnInfo.Player.Calamity().ZoneCalamity || Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
		{
			return 0f;
		}
		if (SpawnCondition.Underworld.Chance > 0f)
		{
			return SpawnCondition.Underworld.Chance / 4f;
		}
		return SpawnCondition.OverworldHallow.Chance / 4f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<UnholyEssence>(), 1, 2, 4);
		npcLoot.Add(ModContent.ItemType<SanctifiedSpark>(), 15);
		npcLoot.Add(ModContent.ItemType<BlasphemousDonut>(), 20);
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
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ImpiousImmolator").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ImpiousImmolator2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ImpiousImmolator3").Type);
			}
		}
	}
}
