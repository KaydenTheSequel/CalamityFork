using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SlimeGod;

[AutoloadBossHead]
public class SplitCrimulanPaladin : ModNPC
{
	private float bossLife;

	public static int GlobDamage = 15;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitScale = 1f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.LifeMaxNERB(1875, 2250, 80000);
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.damage = 36;
		base.NPC.width = 150;
		base.NPC.height = 92;
		base.NPC.scale = 0.8f;
		base.NPC.defense = 10;
		base.NPC.knockBackResist = 0f;
		base.AnimationType = 50;
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

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SlimeGodPaladin")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_100d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1523: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_110a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Unknown result type (might be due to invalid IL or missing references)
		//IL_111a: Unknown result type (might be due to invalid IL or missing references)
		//IL_111f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1128: Unknown result type (might be due to invalid IL or missing references)
		//IL_112c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1999: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_15af: Unknown result type (might be due to invalid IL or missing references)
		//IL_114a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1162: Unknown result type (might be due to invalid IL or missing references)
		//IL_1168: Unknown result type (might be due to invalid IL or missing references)
		//IL_116a: Unknown result type (might be due to invalid IL or missing references)
		//IL_116f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1183: Unknown result type (might be due to invalid IL or missing references)
		//IL_1188: Unknown result type (might be due to invalid IL or missing references)
		//IL_118a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1194: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_163b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e23: Unknown result type (might be due to invalid IL or missing references)
		//IL_166c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1675: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e53: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1260: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1270: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_127c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1281: Unknown result type (might be due to invalid IL or missing references)
		//IL_1295: Unknown result type (might be due to invalid IL or missing references)
		//IL_129a: Unknown result type (might be due to invalid IL or missing references)
		//IL_129c: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c0: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.slimeGodRed < 0 || !Main.npc[CalamityGlobalNPC.slimeGodRed].active)
		{
			CalamityGlobalNPC.slimeGodRed = base.NPC.whoAmI;
		}
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
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.timeLeft < 1800)
			{
				base.NPC.timeLeft = 1800;
			}
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
		if (!player.dead && base.NPC.timeLeft > 10 && base.NPC.ai[3] >= teleportGateValue && base.NPC.ai[0] == 0f && base.NPC.velocity.Y == 0f)
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
				float distanceAhead = 960f;
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
							base.NPC.ai[3] = 0f;
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
					base.NPC.ai[3] = 0f;
				}
			}
		}
		if ((base.NPC.ai[3] < teleportGateValue) & revenge)
		{
			if (!Collision.CanHitLine(base.NPC.Center, 0, 0, player.Center, 0, 0) || Math.Abs(base.NPC.Top.Y - player.Bottom.Y) > 320f)
			{
				base.NPC.ai[3] += (death ? 3f : 2f);
			}
			else
			{
				base.NPC.ai[3]++;
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
				base.NPC.netUpdate = true;
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1]++;
				float jumpGateValue = 35f;
				float velocityX = (death ? 11.5f : (revenge ? 10.5f : (expertMode ? 9.5f : 7.5f)));
				if (revenge)
				{
					float moveBoost = (death ? (11f * (1f - lifeRatio)) : (7f * (1f - lifeRatio)));
					float speedBoost = (death ? (2.25f * (1f - lifeRatio)) : (1.5f * (1f - lifeRatio)));
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
					velocityX *= 1.25f;
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
				base.NPC.ai[2] += (death ? (1f * (1f - lifeRatio)) : (0.5f * (1f - lifeRatio)));
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				float phaseSwitchGateValue = 180f;
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
							base.NPC.velocity.Y = (death ? (-11f) : (revenge ? (-10f) : (expertMode ? (-9f) : (-8f))));
						}
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
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
				targetDist *= (death ? 11f : (revenge ? 10f : (expertMode ? 9f : 8f)));
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
				targetDist *= (death ? 13f : (revenge ? 12f : (expertMode ? 11f : 10f))) + distanceSpeedBoost;
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
						int type = ModContent.ProjectileType<UnstableCrimulanGlob>();
						int damage = GlobDamage;
						Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
						((Vector2)(ref destination)).Normalize();
						destination *= projectileVelocity;
						int numProj = 3;
						float rotation = MathHelper.ToRadians(45f);
						for (int i = 0; i < numProj; i++)
						{
							Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(perturbedSpeed) * 30f * base.NPC.scale, perturbedSpeed * 1.5f, type, damage, 0f, Main.myPlayer, 1f);
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
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projFireDirection) * 30f * base.NPC.scale, projFireDirection, type, damage, 0f, Main.myPlayer);
							}
						}
					}
					base.NPC.localAI[2] = base.NPC.ai[0] - 0.1f;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 0f)
			{
				base.NPC.noTileCollide = true;
			}
			base.NPC.noGravity = true;
			base.NPC.velocity.Y += 0.5f;
			float velocityLimit = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 13f)));
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
			for (int j = 0; j < 10; j++)
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
			for (int k = 0; k < 10; k++)
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
			base.NPC.scale = scaleRatio * 0.75f;
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

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		Color lightColor = default(Color);
		((Color)(ref lightColor))._002Ector(Main.DiscoR, 100, 150, base.NPC.alpha);
		return ((base.NPC.localAI[1] == 1f) ? lightColor : drawColor) * base.NPC.Opacity;
	}

	public override void OnKill()
	{
		int heartAmt = Main.rand.Next(3) + 3;
		for (int i = 0; i < heartAmt; i++)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(23, 1, 32, 48);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		Color dustColor = Color.Crimson;
		((Color)(ref dustColor)).A = 150;
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int crimDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, dustColor, 2f);
			Dust obj = Main.dust[crimDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[crimDust].scale = 0.5f;
				Main.dust[crimDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int crimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, dustColor, 3f);
			Main.dust[crimDust2].noGravity = true;
			Dust obj2 = Main.dust[crimDust2];
			obj2.velocity *= 5f;
			crimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, dustColor, 2f);
			Dust obj3 = Main.dust[crimDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
