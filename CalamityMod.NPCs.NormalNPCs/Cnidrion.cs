using System;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class Cnidrion : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		Main.npcFrameCount[base.Type] = 10;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.8f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 48f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 3f;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.width = 160;
		base.NPC.height = 80;
		base.NPC.defense = 6;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.lifeMax = 280;
		base.NPC.knockBackResist = 0.05f;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 25);
		base.NPC.HitSound = SoundID.NPCHit12;
		base.NPC.DeathSound = SoundID.NPCDeath18;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CnidrionBanner>();
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Cnidrion")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.PillarZone() || spawnInfo.Player.InAstral() || spawnInfo.Player.ZoneCorrupt || spawnInfo.Player.ZoneCrimson || spawnInfo.Player.ZoneOldOneArmy || spawnInfo.Player.ZoneSkyHeight || spawnInfo.PlayerSafe || !spawnInfo.Player.ZoneDesert || !spawnInfo.Player.ZoneOverworldHeight || Main.eclipse || Main.snowMoon || Main.pumpkinMoon || Main.invasionType != 0)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		return 0.05f;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.10000000149011612;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c21: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.NPC.target];
		base.NPC.spriteDirection = ((base.NPC.direction > 0) ? 1 : (-1));
		float movementSpeed = 1f;
		base.NPC.TargetClosest();
		bool stopMoving = false;
		int offsetX = 80;
		int projectileDamage = (Main.masterMode ? 8 : (Main.expertMode ? 9 : 12));
		if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.33 && CalamityWorld.death)
		{
			movementSpeed = 2f;
		}
		if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.1 && CalamityWorld.death)
		{
			movementSpeed = 4f;
		}
		if (Main.zenithWorld)
		{
			movementSpeed = 8f;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.ai[1]++;
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.33 && CalamityWorld.death)
			{
				base.NPC.ai[1]++;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.1 && CalamityWorld.death)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= 300f && Main.netMode != 1)
			{
				base.NPC.ai[1] = 0f;
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
				{
					base.NPC.ai[0] = Main.rand.Next(3, 5);
				}
				else if (CalamityWorld.death && (double)base.NPC.life < (double)base.NPC.lifeMax * 0.6)
				{
					base.NPC.ai[0] = Main.rand.Next(1, 5);
				}
				else
				{
					base.NPC.ai[0] = Main.rand.Next(1, 3);
				}
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			stopMoving = true;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] % 30f == 0f)
			{
				Vector2 npcPosition = default(Vector2);
				((Vector2)(ref npcPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + 20f);
				npcPosition.X += offsetX * base.NPC.direction;
				float targetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - npcPosition.X;
				float targetYDist = Main.player[base.NPC.target].position.Y - npcPosition.Y;
				float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
				targetDistance = 6f / targetDistance;
				targetXDist *= targetDistance;
				targetYDist *= targetDistance;
				targetXDist *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
				targetYDist *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcPosition.X, npcPosition.Y, targetXDist, targetYDist, ModContent.ProjectileType<HorsWaterBlast>(), projectileDamage, 0f, Main.myPlayer);
			}
			if (base.NPC.ai[1] >= 120f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 0f;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			stopMoving = true;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > 60f && base.NPC.ai[1] < 240f && base.NPC.ai[1] % 16f == 0f)
			{
				Vector2 npcPosition2 = default(Vector2);
				((Vector2)(ref npcPosition2))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + 20f);
				npcPosition2.X += offsetX * base.NPC.direction;
				float targetXDist2 = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - npcPosition2.X;
				float targetYDist2 = Main.player[base.NPC.target].position.Y - npcPosition2.Y;
				float targetDistance2 = (float)Math.Sqrt(targetXDist2 * targetXDist2 + targetYDist2 * targetYDist2);
				targetDistance2 = 8f / targetDistance2;
				targetXDist2 *= targetDistance2;
				targetYDist2 *= targetDistance2;
				targetXDist2 *= 1f + (float)Main.rand.Next(-15, 16) * 0.01f;
				targetYDist2 *= 1f + (float)Main.rand.Next(-15, 16) * 0.01f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcPosition2.X, npcPosition2.Y, targetXDist2, targetYDist2, ModContent.ProjectileType<HorsWaterBlast>(), projectileDamage, 0f, Main.myPlayer);
			}
			if (base.NPC.ai[1] >= 300f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 0f;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			movementSpeed = 4f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] % 30f == 0f)
			{
				Vector2 npcPosition3 = default(Vector2);
				((Vector2)(ref npcPosition3))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + 20f);
				npcPosition3.X += offsetX * base.NPC.direction;
				float fastTargetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - npcPosition3.X;
				float fastTargetYDist = Main.player[base.NPC.target].position.Y - npcPosition3.Y;
				float fastTargetDistance = (float)Math.Sqrt(fastTargetXDist * fastTargetXDist + fastTargetYDist * fastTargetYDist);
				fastTargetDistance = 10f / fastTargetDistance;
				fastTargetXDist *= fastTargetDistance;
				fastTargetYDist *= fastTargetDistance;
				fastTargetXDist *= 1f + (float)Main.rand.Next(-10, 11) * 0.001f;
				fastTargetYDist *= 1f + (float)Main.rand.Next(-10, 11) * 0.001f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcPosition3.X, npcPosition3.Y, fastTargetXDist, fastTargetYDist, ModContent.ProjectileType<HorsWaterBlast>(), projectileDamage, 0f, Main.myPlayer);
			}
			if (base.NPC.ai[1] >= 120f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 0f;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			movementSpeed = 4f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] % 20f == 0f)
			{
				Vector2 npcPosition4 = default(Vector2);
				((Vector2)(ref npcPosition4))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + 20f);
				npcPosition4.X += offsetX * base.NPC.direction;
				float targetXDist3 = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - npcPosition4.X;
				float targetYDist3 = Main.player[base.NPC.target].position.Y - npcPosition4.Y;
				float targetDistance3 = (float)Math.Sqrt(targetXDist3 * targetXDist3 + targetYDist3 * targetYDist3);
				targetDistance3 = 11f / targetDistance3;
				targetXDist3 *= targetDistance3;
				targetYDist3 *= targetDistance3;
				targetXDist3 *= 1f + (float)Main.rand.Next(-5, 6) * 0.01f;
				targetYDist3 *= 1f + (float)Main.rand.Next(-5, 6) * 0.01f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), npcPosition4.X, npcPosition4.Y, targetXDist3, targetYDist3, ModContent.ProjectileType<HorsWaterBlast>(), projectileDamage, 0f, Main.myPlayer);
			}
			if (base.NPC.ai[1] >= 240f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 0f;
			}
		}
		if (Math.Abs(base.NPC.Center.X - player.Center.X) < 50f)
		{
			stopMoving = true;
		}
		if (stopMoving)
		{
			base.NPC.velocity.X *= 0.9f;
			if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
			{
				base.NPC.velocity.X = 0f;
			}
		}
		else
		{
			float playerLocation = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
			if (base.NPC.direction > 0)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 20f + movementSpeed) / 21f;
			}
			if (base.NPC.direction < 0)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 20f - movementSpeed) / 21f;
			}
		}
		if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && player.position.Y <= base.NPC.position.Y + (float)base.NPC.height && !base.NPC.collideX)
		{
			base.NPC.noGravity = false;
			base.NPC.noTileCollide = false;
			return;
		}
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		int collisionWidth = 80;
		int collisionHeight = 20;
		if (Collision.SolidCollision(new Vector2(base.NPC.Center.X - (float)(collisionWidth / 2), base.NPC.position.Y + (float)base.NPC.height - (float)collisionHeight), collisionWidth, collisionHeight))
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y = 0f;
			}
			if (base.NPC.velocity.Y > -0.2f)
			{
				base.NPC.velocity.Y -= 0.025f;
			}
			else
			{
				base.NPC.velocity.Y -= 0.2f;
			}
			if (base.NPC.velocity.Y < -2f)
			{
				base.NPC.velocity.Y = -2f;
			}
		}
		else
		{
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y = 0f;
			}
			if (base.NPC.velocity.Y < 0.5f)
			{
				base.NPC.velocity.Y += 0.025f;
			}
			else
			{
				base.NPC.velocity.Y += 0.25f;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 2f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<IlmerisSpark>(), 4);
		npcLoot.Add(3380, 1, 4, 5);
	}
}
