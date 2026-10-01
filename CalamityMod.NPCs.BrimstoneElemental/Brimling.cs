using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.BrimstoneElemental;

public class Brimling : ModNPC
{
	private bool boostDR;

	public static float normalDR = 0.15f;

	public static float boostedDR = 0.6f;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 60;
		base.NPC.height = 60;
		base.NPC.defense = 0;
		base.NPC.DR_NERD(normalDR);
		base.NPC.lifeMax = 1000;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit23;
		base.NPC.DeathSound = SoundID.NPCDeath39;
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 10000;
		}
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 0.7f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<BrimstoneElemental>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Brimling")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(boostDR);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		boostDR = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1f, 0f, 0f);
		if (CalamityGlobalNPC.brimstoneElemental < 0 || !Main.npc[CalamityGlobalNPC.brimstoneElemental].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)Main.npc[CalamityGlobalNPC.brimstoneElemental].life / (float)Main.npc[CalamityGlobalNPC.brimstoneElemental].lifeMax;
		bool num = Main.npc[CalamityGlobalNPC.brimstoneElemental].Calamity().newAI[3] <= 0f;
		float enrageScale = 0f;
		if (num && !Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].ZoneUnderworldHeight)
		{
			enrageScale++;
		}
		if (num && !Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].Calamity().ZoneCalamity)
		{
			enrageScale++;
		}
		bool brimIsAboutToTeleport = Main.npc[CalamityGlobalNPC.brimstoneElemental].ai[0] == 1f && Main.npc[CalamityGlobalNPC.brimstoneElemental].alpha == 0;
		bool brimIsFlyingAboveAndShooting = Main.npc[CalamityGlobalNPC.brimstoneElemental].ai[0] == 3f;
		bool num2 = Main.npc[CalamityGlobalNPC.brimstoneElemental].ai[0] == 4f;
		bool brimIsFiringLaser = Main.npc[CalamityGlobalNPC.brimstoneElemental].ai[0] == 5f;
		base.NPC.alpha = Main.npc[CalamityGlobalNPC.brimstoneElemental].alpha;
		if (num2)
		{
			boostDR = true;
			base.NPC.chaseable = false;
		}
		else
		{
			boostDR = false;
			base.NPC.chaseable = true;
		}
		base.NPC.Calamity().DR = (boostDR ? boostedDR : normalDR);
		if (brimIsFlyingAboveAndShooting | brimIsFiringLaser)
		{
			float shootDivisor = (death ? 45f : 60f);
			if (brimIsFlyingAboveAndShooting)
			{
				shootDivisor = ((death ? 80f : 45f) - (float)Math.Ceiling(10f * (1f - lifeRatio)) - 5f * enrageScale) * 2f;
			}
			base.NPC.ai[1]++;
			if (Main.netMode != 1 && base.NPC.ai[1] % shootDivisor == 0f)
			{
				float projectileVelocity = 5f;
				int type = ModContent.ProjectileType<BrimstoneBarrage>();
				int damage = BrimstoneElemental.DartDamage;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Normalize(Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].Center - base.NPC.Center) * projectileVelocity, type, damage, 0f, Main.myPlayer, 1f, 0f, projectileVelocity * 3f);
			}
		}
		else
		{
			base.NPC.ai[1] = 0f;
		}
		if (Math.Abs(base.NPC.Center.X - Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].Center.X) > 10f)
		{
			float playerLocation = base.NPC.Center.X - Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].Center.X;
			base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
		}
		base.NPC.rotation = Math.Abs(base.NPC.velocity.X) * (float)base.NPC.direction * 0.05f;
		float movementVelocity = (death ? 12f : 10f) * (brimIsFlyingAboveAndShooting ? 1.5f : (boostDR ? 0.5f : 1f));
		movementVelocity += 5f * enrageScale;
		Vector2 distanceFromMother = Main.npc[CalamityGlobalNPC.brimstoneElemental].Center - base.NPC.Center;
		if (((Vector2)(ref distanceFromMother)).Length() > 120f)
		{
			Vector2 farFromBrimVelocityMult = distanceFromMother;
			if (((Vector2)(ref farFromBrimVelocityMult)).Length() > movementVelocity)
			{
				((Vector2)(ref farFromBrimVelocityMult)).Normalize();
				farFromBrimVelocityMult *= movementVelocity;
			}
			int inertia = 20;
			base.NPC.velocity = (base.NPC.velocity * (float)(inertia - 1) + farFromBrimVelocityMult) / (float)inertia;
		}
		else
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.96f;
		}
		float pushVelocity = 0.5f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n.Center) < 40f * base.NPC.scale)
			{
				if (base.NPC.position.X < n.position.X)
				{
					base.NPC.velocity.X -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.X += pushVelocity;
				}
				if (base.NPC.position.Y < n.position.Y)
				{
					base.NPC.velocity.Y -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.Y += pushVelocity;
				}
			}
		}
		if (base.NPC.ai[2] != 0f && base.NPC.ai[3] != 0f)
		{
			for (int i = 0; i < 20; i++)
			{
				int deepRedDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, Color.Transparent);
				Dust obj = Main.dust[deepRedDust];
				obj.velocity *= 3f;
				Main.dust[deepRedDust].noGravity = true;
				Main.dust[deepRedDust].scale = 2.5f;
			}
			base.NPC.Center = new Vector2(base.NPC.ai[2] * 16f, base.NPC.ai[3] * 16f);
			base.NPC.velocity = Vector2.Zero;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			SoundEngine.PlaySound(in BrimstoneElemental.TeleportSound, base.NPC.Center);
			for (int j = 0; j < 20; j++)
			{
				int deepRedDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, Color.Transparent);
				Dust obj2 = Main.dust[deepRedDust2];
				obj2.velocity *= 3f;
				Main.dust[deepRedDust2].noGravity = true;
				Main.dust[deepRedDust2].scale = 2.5f;
			}
		}
		if (brimIsAboutToTeleport && Main.netMode != 1)
		{
			float projectileVelocity2 = 5f;
			int type2 = ModContent.ProjectileType<BrimstoneHellfireball>();
			int damage2 = BrimstoneElemental.HellfireballDamage;
			SoundEngine.PlaySound(in BrimstoneElemental.HellfireballSound, base.NPC.Center);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Normalize(Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].Center - base.NPC.Center) * projectileVelocity2, type2, damage2, 0f, Main.myPlayer, Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].position.X, Main.player[Main.npc[CalamityGlobalNPC.brimstoneElemental].target].position.Y);
		}
		if (Main.npc[CalamityGlobalNPC.brimstoneElemental].alpha != 255 || Main.netMode == 1)
		{
			return;
		}
		Point npcTileCoords = base.NPC.Center.ToTileCoordinates();
		Point brimmyTileCoords = Main.npc[CalamityGlobalNPC.brimstoneElemental].Center.ToTileCoordinates();
		int babTeleportRadius = 3;
		int brimmyTeleportRadius = 4;
		int solidTileRadius = 1;
		int increment = 0;
		while (increment < 100)
		{
			increment++;
			int randXOffset = Main.rand.Next(brimmyTileCoords.X - 6, brimmyTileCoords.X + 7);
			int randYOffset = Main.rand.Next(brimmyTileCoords.Y - 6, brimmyTileCoords.Y + 7);
			if ((randYOffset < brimmyTileCoords.Y - brimmyTeleportRadius || randYOffset > brimmyTileCoords.Y + brimmyTeleportRadius || randXOffset < brimmyTileCoords.X - brimmyTeleportRadius || randXOffset > brimmyTileCoords.X + brimmyTeleportRadius) && (randYOffset < npcTileCoords.Y - babTeleportRadius || randYOffset > npcTileCoords.Y + babTeleportRadius || randXOffset < npcTileCoords.X - babTeleportRadius || randXOffset > npcTileCoords.X + babTeleportRadius) && !Main.tile[randXOffset, randYOffset].HasUnactuatedTile)
			{
				bool canTeleport = true;
				if (canTeleport && Main.tile[randXOffset, randYOffset].LiquidType == 1)
				{
					canTeleport = false;
				}
				if (canTeleport && Collision.SolidTiles(randXOffset - solidTileRadius, randXOffset + solidTileRadius, randYOffset - solidTileRadius, randYOffset + solidTileRadius))
				{
					canTeleport = false;
				}
				if (canTeleport)
				{
					base.NPC.ai[2] = randXOffset;
					base.NPC.ai[3] = randYOffset;
					break;
				}
			}
		}
		base.NPC.netUpdate = true;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (!boostDR)
		{
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 4)
			{
				base.NPC.frame.Y = 0;
			}
			return;
		}
		if (base.NPC.frameCounter > 12.0)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y < frameHeight * 4)
		{
			base.NPC.frame.Y = frameHeight * 4;
		}
		if (base.NPC.frame.Y >= frameHeight * 8)
		{
			base.NPC.frame.Y = frameHeight * 4;
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(4) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
			}
		}
	}
}
