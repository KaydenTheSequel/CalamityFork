using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SlimeGod;

[AutoloadBossHead]
public class CrimulanPaladin : ModNPC
{
	private float bossLife;

	public static int GlobDamage = 15;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 42;
		base.NPC.width = 150;
		base.NPC.height = 92;
		base.NPC.scale = 1.1f;
		base.NPC.defense = 12;
		base.NPC.LifeMaxNERB(7500, 9000, 160000);
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.knockBackResist = 0f;
		base.AnimationType = 50;
		base.NPC.value = 0f;
		base.NPC.Opacity = 0.8f;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_127d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Unknown result type (might be due to invalid IL or missing references)
		//IL_1121: Unknown result type (might be due to invalid IL or missing references)
		//IL_1034: Unknown result type (might be due to invalid IL or missing references)
		//IL_103f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1044: Unknown result type (might be due to invalid IL or missing references)
		//IL_1049: Unknown result type (might be due to invalid IL or missing references)
		//IL_1052: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_177a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1337: Unknown result type (might be due to invalid IL or missing references)
		//IL_1342: Unknown result type (might be due to invalid IL or missing references)
		//IL_1291: Unknown result type (might be due to invalid IL or missing references)
		//IL_1188: Unknown result type (might be due to invalid IL or missing references)
		//IL_113e: Unknown result type (might be due to invalid IL or missing references)
		//IL_114a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_137e: Unknown result type (might be due to invalid IL or missing references)
		//IL_138e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1399: Unknown result type (might be due to invalid IL or missing references)
		//IL_139e: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1078: Unknown result type (might be due to invalid IL or missing references)
		//IL_107d: Unknown result type (might be due to invalid IL or missing references)
		//IL_108b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1800: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11df: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1103: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1846: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1407: Unknown result type (might be due to invalid IL or missing references)
		//IL_140c: Unknown result type (might be due to invalid IL or missing references)
		//IL_140e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1418: Unknown result type (might be due to invalid IL or missing references)
		//IL_1428: Unknown result type (might be due to invalid IL or missing references)
		//IL_142d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1432: Unknown result type (might be due to invalid IL or missing references)
		//IL_1439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d64: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_188c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_20dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1903: Unknown result type (might be due to invalid IL or missing references)
		//IL_190c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2103: Unknown result type (might be due to invalid IL or missing references)
		//IL_210d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2112: Unknown result type (might be due to invalid IL or missing references)
		//IL_215c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2181: Unknown result type (might be due to invalid IL or missing references)
		//IL_218b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2190: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1500: Unknown result type (might be due to invalid IL or missing references)
		//IL_1505: Unknown result type (might be due to invalid IL or missing references)
		//IL_1519: Unknown result type (might be due to invalid IL or missing references)
		//IL_151e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1520: Unknown result type (might be due to invalid IL or missing references)
		//IL_152a: Unknown result type (might be due to invalid IL or missing references)
		//IL_153a: Unknown result type (might be due to invalid IL or missing references)
		//IL_153f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.slimeGodRed = base.NPC.whoAmI;
		bool bossRush = BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode | bossRush;
		bool revenge = CalamityWorld.revenge | bossRush;
		bool death = (CalamityWorld.death || base.NPC.localAI[1] == 1f) | bossRush;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		base.NPC.defense = base.NPC.defDefense;
		int setDamage = base.NPC.defDamage;
		if (base.NPC.localAI[1] == 1f)
		{
			base.NPC.defense = base.NPC.defDefense + 24;
			setDamage += 25;
		}
		float scale = ((CalamityWorld.LegendaryMode && CalamityWorld.revenge) ? 0.6f : (Main.getGoodWorld ? 0.8f : 1f));
		base.NPC.aiAction = 0;
		base.NPC.noTileCollide = false;
		base.NPC.noGravity = false;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (base.NPC.ai[0] != 3f)
		{
			if (player.dead || !player.active)
			{
				base.NPC.TargetClosest();
				player = Main.player[base.NPC.target];
				if (player.dead || !player.active)
				{
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					calamityGlobalNPC.newAI[0] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.timeLeft < 1800)
			{
				base.NPC.timeLeft = 1800;
			}
		}
		if ((lifeRatio <= 0.5f && Main.netMode != 1) & expertMode)
		{
			if (Main.zenithWorld)
			{
				int type = ModContent.ProjectileType<UnstableCrimulanGlob>();
				for (int i = 0; i < 30; i++)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1499, 1500) * 0.01f, (float)Main.rand.Next(-1499, 1500) * 0.01f, type, 35, 0f);
				}
			}
			SoundEngine.PlaySound(in SoundID.NPCDeath1, base.NPC.Center);
			Vector2 spawnAt = base.NPC.Center + new Vector2(0f, (float)base.NPC.height / 2f);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnAt.X - 30, (int)spawnAt.Y, ModContent.NPCType<SplitCrimulanPaladin>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnAt.X + 30, (int)spawnAt.Y, ModContent.NPCType<SplitCrimulanPaladin>());
			if (Main.zenithWorld && NPC.CountNPCS(ModContent.NPCType<SplitEbonianPaladin>()) < 3)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnAt.X, (int)spawnAt.Y - 30, ModContent.NPCType<SplitCrimulanPaladin>());
			}
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		bool enraged = true;
		if (CalamityGlobalNPC.slimeGodPurple != -1 && Main.npc[CalamityGlobalNPC.slimeGodPurple].active)
		{
			enraged = false;
		}
		if (base.NPC.localAI[1] != 1f && enraged)
		{
			base.NPC.defense = base.NPC.defDefense * 2;
		}
		float teleportGateValue = 720f;
		if (!player.dead && base.NPC.timeLeft > 10 && calamityGlobalNPC.newAI[0] >= teleportGateValue && base.NPC.ai[0] == 0f && base.NPC.velocity.Y == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.X *= 0.5f;
			base.NPC.ai[0] = 5f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.netUpdate = true;
				base.NPC.TargetClosest(faceTarget: false);
				player = Main.player[base.NPC.target];
				float distanceAhead = 800f;
				Vector2 randomDefault = (Main.rand.NextBool() ? Vector2.UnitX : (-Vector2.UnitX));
				Point predictiveTeleportPoint = (player.Center + Utils.SafeNormalize(new Vector2((float)Math.Round(player.velocity.X), 0f), randomDefault) * distanceAhead).ToTileCoordinates();
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
						if (canTeleportToTile && !Collision.CanHitLine(base.NPC.Center, 0, 0, predictiveTeleportPoint.ToVector2() * 16f, 0, 0))
						{
							canTeleportToTile = false;
						}
						if (canTeleportToTile)
						{
							base.NPC.localAI[0] = teleportTileX * 16 + 8;
							base.NPC.localAI[3] = teleportTileY * 16 + 16;
							calamityGlobalNPC.newAI[0] = 0f;
							break;
						}
						predictiveTeleportPoint.X += (((float)predictiveTeleportPoint.X < 0f) ? 1 : (-1));
					}
					else
					{
						predictiveTeleportPoint.X += (((float)predictiveTeleportPoint.X < 0f) ? 1 : (-1));
					}
				}
				if (teleportTries >= 100)
				{
					Vector2 bottom = Main.player[Player.FindClosest(base.NPC.position, base.NPC.width, base.NPC.height)].Bottom;
					base.NPC.localAI[0] = bottom.X;
					base.NPC.localAI[3] = bottom.Y;
					calamityGlobalNPC.newAI[0] = 0f;
				}
			}
		}
		if (calamityGlobalNPC.newAI[0] < teleportGateValue)
		{
			if (!Collision.CanHitLine(base.NPC.Center, 0, 0, player.Center, 0, 0) || Math.Abs(base.NPC.Top.Y - player.Bottom.Y) > 320f)
			{
				calamityGlobalNPC.newAI[0] += (death ? 3f : 2f);
			}
			else
			{
				calamityGlobalNPC.newAI[0]++;
			}
		}
		float distanceSpeedBoost = Vector2.Distance(player.Center, base.NPC.Center) * 0.005f;
		if (base.NPC.ai[0] == 0f)
		{
			bool phaseThroughTilesToReachTarget = Vector2.Distance(player.Center, base.NPC.Center) > 2400f || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height);
			if ((Main.netMode != 1) & phaseThroughTilesToReachTarget)
			{
				base.NPC.damage = setDamage;
				base.NPC.ai[0] = 4f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			else if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1]++;
				float jumpGateValue = 50f;
				float velocityX = (death ? 10f : (revenge ? 9f : (expertMode ? 8f : 6f)));
				if (revenge)
				{
					float moveBoost = (death ? (45f * (1f - lifeRatio)) : (30f * (1f - lifeRatio)));
					float speedBoost = (death ? (4.5f * (1f - lifeRatio)) : (3f * (1f - lifeRatio)));
					jumpGateValue -= moveBoost;
					velocityX += speedBoost;
				}
				float distanceBelowTarget = base.NPC.position.Y - (player.position.Y + 80f);
				float speedMult = 1f;
				if (distanceBelowTarget > 0f)
				{
					speedMult += distanceBelowTarget * 0.002f;
				}
				if (speedMult > 2f)
				{
					speedMult = 2f;
				}
				float velocityY = 4f;
				if (!Collision.CanHit(base.NPC.Center, 1, 1, player.Center, 1, 1))
				{
					velocityY += 2f;
				}
				if (base.NPC.ai[1] > jumpGateValue)
				{
					base.NPC.damage = setDamage;
					base.NPC.ai[3]++;
					if (base.NPC.ai[3] >= 2f)
					{
						base.NPC.ai[3] = 0f;
						velocityX *= 1.25f;
					}
					base.NPC.ai[1] = 0f;
					base.NPC.velocity.Y -= velocityY * speedMult;
					base.NPC.velocity.X = (velocityX + distanceSpeedBoost) * (float)base.NPC.direction;
					base.NPC.noTileCollide = true;
					base.NPC.netUpdate = true;
				}
				else if (base.NPC.ai[1] >= jumpGateValue - 30f)
				{
					base.NPC.aiAction = 1;
				}
			}
			else
			{
				base.NPC.damage = setDamage;
				base.NPC.velocity.X *= 0.99f;
				if (base.NPC.direction < 0 && base.NPC.velocity.X > -1f)
				{
					base.NPC.velocity.X = -1f;
				}
				if (base.NPC.direction > 0 && base.NPC.velocity.X < 1f)
				{
					base.NPC.velocity.X = 1f;
				}
				if (!player.dead)
				{
					if (base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > player.Top.Y)
					{
						base.NPC.noTileCollide = false;
					}
					else if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.noTileCollide = false;
					}
					else
					{
						base.NPC.noTileCollide = true;
					}
				}
			}
			base.NPC.ai[2]++;
			if (revenge)
			{
				base.NPC.ai[2] += (death ? (4f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				float phaseSwitchGateValue = 360f;
				if (base.NPC.ai[2] >= phaseSwitchGateValue)
				{
					if (Main.netMode != 1)
					{
						int num = (int)base.NPC.localAI[2];
						if (num == 1 || num != 2)
						{
							base.NPC.ai[0] = 2f;
						}
						else
						{
							base.NPC.ai[0] = 1f;
						}
						if (base.NPC.ai[0] == 1f)
						{
							base.NPC.damage = setDamage;
							base.NPC.noTileCollide = true;
							base.NPC.velocity.Y = (death ? (-10f) : (revenge ? (-9f) : (expertMode ? (-8f) : (-7f))));
						}
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
				}
				else if (base.NPC.ai[1] >= phaseSwitchGateValue - 30f)
				{
					base.NPC.aiAction = 1;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.TargetClosest();
			Vector2 targetCenter = player.Center;
			targetCenter.Y -= 350f;
			Vector2 targetDist = targetCenter - base.NPC.Center;
			if (base.NPC.ai[2] == 1f)
			{
				base.NPC.ai[1]++;
				targetDist = player.Center - base.NPC.Center;
				((Vector2)(ref targetDist)).Normalize();
				targetDist *= (death ? 10f : (revenge ? 9f : (expertMode ? 8f : 7f)));
				base.NPC.velocity = (base.NPC.velocity * 4f + targetDist) / 5f;
				if (base.NPC.ai[1] > 12f)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[0] = 1.1f;
					base.NPC.ai[2] = 0f;
					base.NPC.velocity = targetDist;
				}
			}
			else
			{
				if (Math.Abs(base.NPC.Center.X - player.Center.X) < 40f && base.NPC.Center.Y < player.Center.Y - 300f)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 1f;
					return;
				}
				((Vector2)(ref targetDist)).Normalize();
				targetDist *= (death ? 12f : (revenge ? 11f : (expertMode ? 10f : 9f))) + distanceSpeedBoost;
				base.NPC.velocity = (base.NPC.velocity * 5f + targetDist) / 6f;
			}
		}
		else if (base.NPC.ai[0] == 1.1f)
		{
			base.NPC.damage = setDamage;
			bool atTargetPosition = base.NPC.position.Y + (float)base.NPC.height >= player.position.Y;
			if (base.NPC.ai[2] == 0f && (atTargetPosition || base.NPC.localAI[1] == 0f) && Collision.CanHit(base.NPC.Center, 1, 1, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.ai[2] = 1f;
				base.NPC.netUpdate = true;
			}
			if (atTargetPosition || base.NPC.velocity.Y <= 0f)
			{
				base.NPC.damage = 0;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > 10f)
				{
					SoundEngine.PlaySound(in SlimeGodCore.BigShotSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						float projectileVelocity = 8f;
						int type2 = ModContent.ProjectileType<UnstableCrimulanGlob>();
						int damage = GlobDamage;
						Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
						((Vector2)(ref destination)).Normalize();
						destination *= projectileVelocity;
						int numProj = 5;
						float rotation = MathHelper.ToRadians(45f);
						for (int j = 0; j < numProj; j++)
						{
							Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)j / (float)(numProj - 1)));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(perturbedSpeed) * 30f * base.NPC.scale, perturbedSpeed * 1.5f, type2, damage, 0f, Main.myPlayer, 1f);
						}
						if (enraged & expertMode)
						{
							List<int> targets = new List<int>();
							ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
							while (enumerator.MoveNext())
							{
								Player plr = enumerator.Current;
								if (!plr.dead)
								{
									targets.Add(plr.whoAmI);
								}
								if (targets.Count > 1)
								{
									break;
								}
							}
							foreach (int t in targets)
							{
								Vector2 projFireDirection = Vector2.Normalize(Main.player[t].Center - base.NPC.Center) * projectileVelocity;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projFireDirection) * 30f * base.NPC.scale, projFireDirection, type2, damage, 0f, Main.myPlayer);
							}
						}
					}
					base.NPC.localAI[2] = base.NPC.ai[0] - 0.1f;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 0f)
			{
				base.NPC.noTileCollide = true;
			}
			base.NPC.noGravity = true;
			base.NPC.velocity.Y += 0.5f;
			float velocityLimit = (death ? 15f : (revenge ? 14f : (expertMode ? 13f : 12f)));
			if (base.NPC.velocity.Y > velocityLimit)
			{
				base.NPC.velocity.Y = velocityLimit;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > 15f)
				{
					base.NPC.damage = setDamage;
					base.NPC.ai[1] = 0f;
					base.NPC.velocity.Y -= 3f;
					if (player.position.Y + (float)player.height < base.NPC.Center.Y)
					{
						base.NPC.velocity.Y -= 1.25f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 40f)
					{
						base.NPC.velocity.Y -= 1.5f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 80f)
					{
						base.NPC.velocity.Y -= 1.75f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 120f)
					{
						base.NPC.velocity.Y -= 2f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 160f)
					{
						base.NPC.velocity.Y -= 2.25f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 200f)
					{
						base.NPC.velocity.Y -= 2.5f;
					}
					if (!Collision.CanHit(base.NPC.Center, 1, 1, player.Center, 1, 1))
					{
						base.NPC.velocity.Y -= 2f;
					}
					base.NPC.velocity.X = ((death ? 14f : (revenge ? 13f : (expertMode ? 12f : 11f))) + distanceSpeedBoost) * (float)base.NPC.direction;
					base.NPC.ai[2]++;
				}
				else
				{
					base.NPC.aiAction = 1;
				}
			}
			else
			{
				base.NPC.damage = setDamage;
				base.NPC.velocity.X *= 0.98f;
				float velocityLimit2 = (death ? 5.5f : (revenge ? 5f : (expertMode ? 4.5f : 4f)));
				if (base.NPC.direction < 0 && base.NPC.velocity.X > 0f - velocityLimit2)
				{
					base.NPC.velocity.X = 0f - velocityLimit2;
				}
				if (base.NPC.direction > 0 && base.NPC.velocity.X < velocityLimit2)
				{
					base.NPC.velocity.X = velocityLimit2;
				}
			}
			if (base.NPC.ai[2] >= 3f && base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.localAI[2] = base.NPC.ai[0];
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			base.NPC.noTileCollide = true;
			base.NPC.Opacity -= 0.03f;
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
			base.NPC.velocity.X *= 0.98f;
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = setDamage;
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.direction = 1;
			}
			else
			{
				base.NPC.direction = -1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			base.NPC.knockBackResist = 0f;
			Vector2 distanceFromTarget = player.Center - base.NPC.Center;
			distanceFromTarget.Y -= 16f;
			if (Main.netMode != 1 && ((Vector2)(ref distanceFromTarget)).Length() < 500f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
			{
				base.NPC.damage = 0;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			if (((Vector2)(ref distanceFromTarget)).Length() > 12f)
			{
				((Vector2)(ref distanceFromTarget)).Normalize();
				distanceFromTarget *= 12f;
			}
			base.NPC.velocity = (base.NPC.velocity * 4f + distanceFromTarget) / 5f;
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = 0;
			base.NPC.aiAction = 1;
			base.NPC.ai[1]++;
			float teleportTime = (death ? 30f : 40f);
			scale = MathHelper.Clamp((teleportTime - base.NPC.ai[1]) / teleportTime, 0f, 1f);
			scale = 0.5f + scale * 0.5f;
			if (base.NPC.ai[1] >= teleportTime && Main.netMode != 1)
			{
				base.NPC.Bottom = new Vector2(base.NPC.localAI[0], base.NPC.localAI[3]);
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			if (Main.netMode == 1 && base.NPC.ai[1] >= teleportTime * 2f)
			{
				base.NPC.ai[0] = 6f;
				base.NPC.ai[1] = 0f;
			}
			Color dustColor = Color.Crimson;
			((Color)(ref dustColor)).A = 150;
			for (int k = 0; k < 10; k++)
			{
				int crimsonDust = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, base.NPC.alpha, dustColor, 2f);
				Main.dust[crimsonDust].noGravity = true;
				Dust obj = Main.dust[crimsonDust];
				obj.velocity *= 0.5f;
			}
		}
		else if (base.NPC.ai[0] == 6f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[1]++;
			float teleportEndTime = (death ? 15f : 20f);
			scale = MathHelper.Clamp(base.NPC.ai[1] / teleportEndTime, 0f, 1f);
			scale = 0.5f + scale * 0.5f;
			if (base.NPC.ai[1] >= teleportEndTime && Main.netMode != 1)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.TargetClosest();
			}
			if (Main.netMode == 1 && base.NPC.ai[1] >= teleportEndTime * 2f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.TargetClosest();
			}
			Color dustColor2 = Color.Crimson;
			((Color)(ref dustColor2)).A = 150;
			for (int l = 0; l < 10; l++)
			{
				int crimsonDust2 = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, base.NPC.alpha, dustColor2, 2f);
				Main.dust[crimsonDust2].noGravity = true;
				Dust obj2 = Main.dust[crimsonDust2];
				obj2.velocity *= 0.5f;
			}
		}
		if (bossLife == 0f && base.NPC.life > 0)
		{
			bossLife = base.NPC.lifeMax;
		}
		if (base.NPC.life <= 0)
		{
			return;
		}
		float scaleRatio = lifeRatio;
		scaleRatio = scaleRatio * 0.5f + 0.75f;
		scaleRatio *= scale;
		if (scaleRatio != base.NPC.scale)
		{
			base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y + (float)base.NPC.height;
			base.NPC.scale = scaleRatio;
			base.NPC.width = (int)(150f * base.NPC.scale);
			base.NPC.height = (int)(92f * base.NPC.scale);
			base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y - (float)base.NPC.height;
		}
		if (Main.netMode == 1)
		{
			return;
		}
		int slimeSpawnThreshold = (int)((double)base.NPC.lifeMax * 0.15);
		if ((float)(base.NPC.life + slimeSpawnThreshold) < bossLife)
		{
			bossLife = base.NPC.life;
			int x = (int)(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width - 32));
			int y = (int)(base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height - 32));
			int slimeType = ModContent.NPCType<CrimsonSlimeSpawn>();
			if (Main.rand.NextBool(3))
			{
				slimeType = ModContent.NPCType<CrimsonSlimeSpawn2>();
			}
			int slimeSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), x, y, slimeType);
			Main.npc[slimeSpawn].SetDefaults(slimeType);
			Main.npc[slimeSpawn].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
			Main.npc[slimeSpawn].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
			Main.npc[slimeSpawn].ai[0] = -1000 * Main.rand.Next(3);
			Main.npc[slimeSpawn].ai[1] = 0f;
			if (Main.dedServ && slimeSpawn < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, slimeSpawn);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		Color lightColor = default(Color);
		((Color)(ref lightColor))._002Ector(Main.DiscoR, 100, 150, base.NPC.alpha);
		return ((base.NPC.localAI[1] == 1f) ? lightColor : drawColor) * base.NPC.Opacity;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Color dustColor = Color.Crimson;
		((Color)(ref dustColor)).A = 150;
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
