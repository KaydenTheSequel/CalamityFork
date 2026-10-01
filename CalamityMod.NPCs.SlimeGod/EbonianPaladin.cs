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
public class EbonianPaladin : ModNPC
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
		base.NPC.damage = 40;
		base.NPC.width = 150;
		base.NPC.height = 92;
		base.NPC.scale = 1.1f;
		base.NPC.defense = 10;
		base.NPC.LifeMaxNERB(8000, 9600, 220000);
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.AnimationType = 50;
		base.NPC.value = 0f;
		base.NPC.Opacity = 0.8f;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
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
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_101c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Unknown result type (might be due to invalid IL or missing references)
		//IL_102c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1031: Unknown result type (might be due to invalid IL or missing references)
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_103d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1042: Unknown result type (might be due to invalid IL or missing references)
		//IL_1051: Unknown result type (might be due to invalid IL or missing references)
		//IL_1058: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_125b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1260: Unknown result type (might be due to invalid IL or missing references)
		//IL_1272: Unknown result type (might be due to invalid IL or missing references)
		//IL_127a: Unknown result type (might be due to invalid IL or missing references)
		//IL_127f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1284: Unknown result type (might be due to invalid IL or missing references)
		//IL_1074: Unknown result type (might be due to invalid IL or missing references)
		//IL_107f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1503: Unknown result type (might be due to invalid IL or missing references)
		//IL_139a: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_140d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1600: Unknown result type (might be due to invalid IL or missing references)
		//IL_160b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1610: Unknown result type (might be due to invalid IL or missing references)
		//IL_1615: Unknown result type (might be due to invalid IL or missing references)
		//IL_161e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1622: Unknown result type (might be due to invalid IL or missing references)
		//IL_1627: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1302: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_131a: Unknown result type (might be due to invalid IL or missing references)
		//IL_131f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1321: Unknown result type (might be due to invalid IL or missing references)
		//IL_132b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1330: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1436: Unknown result type (might be due to invalid IL or missing references)
		//IL_143b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1449: Unknown result type (might be due to invalid IL or missing references)
		//IL_1453: Unknown result type (might be due to invalid IL or missing references)
		//IL_1458: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1464: Unknown result type (might be due to invalid IL or missing references)
		//IL_1469: Unknown result type (might be due to invalid IL or missing references)
		//IL_1388: Unknown result type (might be due to invalid IL or missing references)
		//IL_138a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2018: Unknown result type (might be due to invalid IL or missing references)
		//IL_2022: Unknown result type (might be due to invalid IL or missing references)
		//IL_2027: Unknown result type (might be due to invalid IL or missing references)
		//IL_2029: Unknown result type (might be due to invalid IL or missing references)
		//IL_2033: Unknown result type (might be due to invalid IL or missing references)
		//IL_2038: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2005: Unknown result type (might be due to invalid IL or missing references)
		//IL_200a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_164e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1666: Unknown result type (might be due to invalid IL or missing references)
		//IL_166c: Unknown result type (might be due to invalid IL or missing references)
		//IL_166e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1673: Unknown result type (might be due to invalid IL or missing references)
		//IL_1687: Unknown result type (might be due to invalid IL or missing references)
		//IL_168c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1696: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_2187: Unknown result type (might be due to invalid IL or missing references)
		//IL_218c: Unknown result type (might be due to invalid IL or missing references)
		//IL_210a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1104: Unknown result type (might be due to invalid IL or missing references)
		//IL_1118: Unknown result type (might be due to invalid IL or missing references)
		//IL_111d: Unknown result type (might be due to invalid IL or missing references)
		//IL_111f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1124: Unknown result type (might be due to invalid IL or missing references)
		//IL_112e: Unknown result type (might be due to invalid IL or missing references)
		//IL_113e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1143: Unknown result type (might be due to invalid IL or missing references)
		//IL_1148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2377: Unknown result type (might be due to invalid IL or missing references)
		//IL_237c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba6: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2206: Unknown result type (might be due to invalid IL or missing references)
		//IL_222b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2235: Unknown result type (might be due to invalid IL or missing references)
		//IL_223a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2398: Unknown result type (might be due to invalid IL or missing references)
		//IL_239d: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_241b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2425: Unknown result type (might be due to invalid IL or missing references)
		//IL_242a: Unknown result type (might be due to invalid IL or missing references)
		//IL_177e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1789: Unknown result type (might be due to invalid IL or missing references)
		//IL_178e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1793: Unknown result type (might be due to invalid IL or missing references)
		//IL_179a: Unknown result type (might be due to invalid IL or missing references)
		//IL_179f: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17de: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.slimeGodPurple = base.NPC.whoAmI;
		bool bossRush = BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode | bossRush;
		bool revenge = CalamityWorld.revenge | bossRush;
		bool death = (CalamityWorld.death || base.NPC.localAI[1] == 1f) | bossRush;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		base.NPC.defense = base.NPC.defDefense;
		int setDamage = base.NPC.defDamage;
		if (base.NPC.localAI[1] == 1f)
		{
			base.NPC.defense = base.NPC.defDefense + 20;
			setDamage += 22;
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
		if (base.NPC.ai[0] != 4f)
		{
			if (player.dead || !player.active)
			{
				base.NPC.TargetClosest();
				player = Main.player[base.NPC.target];
				if (player.dead || !player.active)
				{
					base.NPC.ai[0] = 4f;
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
				int type = ModContent.ProjectileType<UnstableEbonianGlob>();
				for (int i = 0; i < 30; i++)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1199, 1200) * 0.01f, (float)Main.rand.Next(-1199, 1200) * 0.01f, type, 35, 0f);
				}
			}
			SoundEngine.PlaySound(in SoundID.NPCDeath1, base.NPC.Center);
			Vector2 spawnAt = base.NPC.Center + new Vector2(0f, (float)base.NPC.height / 2f);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnAt.X - 30, (int)spawnAt.Y, ModContent.NPCType<SplitEbonianPaladin>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnAt.X + 30, (int)spawnAt.Y, ModContent.NPCType<SplitEbonianPaladin>());
			if (Main.zenithWorld && NPC.CountNPCS(ModContent.NPCType<SplitCrimulanPaladin>()) < 3)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnAt.X, (int)spawnAt.Y - 30, ModContent.NPCType<SplitEbonianPaladin>());
			}
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		bool enraged = true;
		bool hyperMode = base.NPC.localAI[1] == 1f;
		if (CalamityGlobalNPC.slimeGodRed != -1 && Main.npc[CalamityGlobalNPC.slimeGodRed].active)
		{
			enraged = false;
		}
		float teleportGateValue = 720f;
		if (!player.dead && base.NPC.timeLeft > 10 && calamityGlobalNPC.newAI[0] >= teleportGateValue && base.NPC.ai[0] == 0f && base.NPC.velocity.Y == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.X *= 0.5f;
			base.NPC.ai[0] = 6f;
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
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1] += (hyperMode ? 2f : 1f);
				float jumpGateValue = 60f;
				float velocityX = (death ? 8f : (revenge ? 7f : (expertMode ? 6f : 4f)));
				if (revenge)
				{
					float moveBoost = (death ? (60f * (1f - lifeRatio)) : (40f * (1f - lifeRatio)));
					float speedBoost = (death ? (6f * (1f - lifeRatio)) : (4f * (1f - lifeRatio)));
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
				float velocityY = 5f;
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
						velocityY *= 1.25f;
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
				float phaseSwitchGateValue = 420f;
				if (base.NPC.ai[2] >= phaseSwitchGateValue)
				{
					if (Main.netMode != 1)
					{
						switch ((int)base.NPC.localAI[2])
						{
						default:
							base.NPC.ai[0] = (Main.rand.NextBool() ? 2f : 3f);
							break;
						case 2:
							base.NPC.ai[0] = (Main.rand.NextBool() ? 3f : (hyperMode ? 2f : 1f));
							break;
						case 3:
							base.NPC.ai[0] = ((!Main.rand.NextBool()) ? 2f : (hyperMode ? 3f : 1f));
							break;
						}
						if (base.NPC.ai[0] == 2f)
						{
							base.NPC.damage = setDamage;
							base.NPC.noTileCollide = true;
							base.NPC.velocity.Y = (death ? (-9f) : (revenge ? (-8f) : (expertMode ? (-7f) : (-6f))));
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
			base.NPC.velocity.X *= 0.85f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 30f)
			{
				if (revenge)
				{
					float slimeShotVelocity = 6f;
					Vector2 projectileVelocity = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * slimeShotVelocity;
					float minFiringDistance = 160f;
					if (Vector2.Distance(base.NPC.Center, player.Center) > minFiringDistance)
					{
						SoundEngine.PlaySound(in SlimeGodCore.BigShotSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int type2 = ModContent.ProjectileType<UnstableEbonianGlob>();
							int damage = GlobDamage;
							int numProj = (death ? 6 : 4);
							float rotation = MathHelper.ToRadians((float)(death ? 15 : 10));
							for (int j = 0; j < numProj; j++)
							{
								Vector2 randomVelocity = Main.rand.NextVector2CircularEdge(3f, 3f);
								Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)j / (float)(numProj - 1))) + randomVelocity;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 30f * base.NPC.scale, perturbedSpeed, type2, damage, 0f, Main.myPlayer);
							}
						}
					}
				}
				base.NPC.localAI[2] = base.NPC.ai[0];
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
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
				targetDist *= (death ? 9f : (revenge ? 8f : (expertMode ? 7f : 6f)));
				base.NPC.velocity = (base.NPC.velocity * 4f + targetDist) / 5f;
				if (base.NPC.ai[1] > 12f)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[0] = 2.1f;
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
				targetDist *= (death ? 11f : (revenge ? 10f : (expertMode ? 9f : 8f))) + distanceSpeedBoost;
				base.NPC.velocity = (base.NPC.velocity * 5f + targetDist) / 6f;
			}
		}
		else if (base.NPC.ai[0] == 2.1f)
		{
			base.NPC.damage = setDamage;
			bool atTargetPosition = base.NPC.position.Y + (float)base.NPC.height >= player.position.Y;
			if (((base.NPC.ai[2] == 0f) & atTargetPosition) && Collision.CanHit(base.NPC.Center, 1, 1, player.Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
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
						float projectileVelocity2 = 4f;
						int type3 = ModContent.ProjectileType<UnstableEbonianGlob>();
						int damage2 = GlobDamage;
						Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
						((Vector2)(ref destination)).Normalize();
						destination *= projectileVelocity2;
						int numProj2 = 11;
						float rotation2 = MathHelper.ToRadians(90f);
						for (int k = 0; k < numProj2; k++)
						{
							if (k < 4 || k > 6)
							{
								Vector2 perturbedSpeed2 = destination.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)k / (float)(numProj2 - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitY * 30f * base.NPC.scale + Vector2.Normalize(perturbedSpeed2) * 30f * base.NPC.scale, perturbedSpeed2, type3, damage2, 0f, Main.myPlayer);
							}
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
								Vector2 projFireDirection = Vector2.Normalize(Main.player[t].Center - base.NPC.Center) * projectileVelocity2;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projFireDirection) * 30f * base.NPC.scale, projFireDirection, type3, damage2, 0f, Main.myPlayer);
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
		else if (base.NPC.ai[0] == 3f)
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
					base.NPC.velocity.Y -= 6f;
					if (player.position.Y + (float)player.height < base.NPC.Center.Y)
					{
						base.NPC.velocity.Y -= 1.2f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 40f)
					{
						base.NPC.velocity.Y -= 1.4f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 80f)
					{
						base.NPC.velocity.Y -= 1.7f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 120f)
					{
						base.NPC.velocity.Y -= 2f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 160f)
					{
						base.NPC.velocity.Y -= 2.2f;
					}
					if (player.position.Y + (float)player.height < base.NPC.Center.Y - 200f)
					{
						base.NPC.velocity.Y -= 2.4f;
					}
					if (!Collision.CanHit(base.NPC.Center, 1, 1, player.Center, 1, 1))
					{
						base.NPC.velocity.Y -= 2f;
					}
					base.NPC.velocity.X = ((death ? 11f : (revenge ? 10f : (expertMode ? 9f : 8f))) + distanceSpeedBoost) * (float)base.NPC.direction;
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
				float velocityLimit2 = (death ? 6.5f : (revenge ? 6f : (expertMode ? 5.5f : 5f)));
				if (base.NPC.direction < 0 && base.NPC.velocity.X > 0f - velocityLimit2)
				{
					base.NPC.velocity.X = 0f - velocityLimit2;
				}
				if (base.NPC.direction > 0 && base.NPC.velocity.X < velocityLimit2)
				{
					base.NPC.velocity.X = velocityLimit2;
				}
			}
			if (base.NPC.ai[2] >= 2f && base.NPC.velocity.Y == 0f)
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
		else if (base.NPC.ai[0] == 4f)
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
		else if (base.NPC.ai[0] == 5f)
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
		else if (base.NPC.ai[0] == 6f)
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
				base.NPC.ai[0] = 7f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			if (Main.netMode == 1 && base.NPC.ai[1] >= teleportTime * 2f)
			{
				base.NPC.ai[0] = 7f;
				base.NPC.ai[1] = 0f;
			}
			Color dustColor = Color.Lavender;
			((Color)(ref dustColor)).A = 150;
			for (int l = 0; l < 10; l++)
			{
				int corruptDust = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, base.NPC.alpha, dustColor, 2f);
				Main.dust[corruptDust].noGravity = true;
				Dust obj = Main.dust[corruptDust];
				obj.velocity *= 0.5f;
			}
		}
		else if (base.NPC.ai[0] == 7f)
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
			Color dustColor2 = Color.Lavender;
			((Color)(ref dustColor2)).A = 150;
			for (int m = 0; m < 10; m++)
			{
				int corruptDust2 = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, base.NPC.alpha, dustColor2, 2f);
				Main.dust[corruptDust2].noGravity = true;
				Dust obj2 = Main.dust[corruptDust2];
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
		int slimeSpawnThreshold = (int)((double)base.NPC.lifeMax * 0.2);
		if (!((float)(base.NPC.life + slimeSpawnThreshold) < bossLife))
		{
			return;
		}
		bossLife = base.NPC.life;
		int randSlimeAmt = Main.rand.Next(1, 3);
		for (int n = 0; n < randSlimeAmt; n++)
		{
			int x = (int)(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width - 32));
			int y = (int)(base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height - 32));
			int slimeType = ModContent.NPCType<CorruptSlimeSpawn>();
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
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Color lightColor = default(Color);
		((Color)(ref lightColor))._002Ector(200, 150, Main.DiscoB, base.NPC.alpha);
		return ((base.NPC.localAI[1] == 1f) ? lightColor : drawColor) * base.NPC.Opacity;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Color dustColor = Color.Lavender;
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
