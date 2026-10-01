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
public class SplitEbonianPaladin : ModNPC
{
	private float bossLife;

	public static int GlobDamage = 15;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 6;
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
		base.NPC.LifeMaxNERB(2000, 2400, 110000);
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.damage = 36;
		base.NPC.width = 150;
		base.NPC.height = 92;
		base.NPC.scale = 0.8f;
		base.NPC.defense = 8;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.AnimationType = 50;
		base.NPC.Opacity = 0.8f;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
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
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1264: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1281: Unknown result type (might be due to invalid IL or missing references)
		//IL_1119: Unknown result type (might be due to invalid IL or missing references)
		//IL_1125: Unknown result type (might be due to invalid IL or missing references)
		//IL_1039: Unknown result type (might be due to invalid IL or missing references)
		//IL_1044: Unknown result type (might be due to invalid IL or missing references)
		//IL_1049: Unknown result type (might be due to invalid IL or missing references)
		//IL_104e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1057: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_177f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1327: Unknown result type (might be due to invalid IL or missing references)
		//IL_1332: Unknown result type (might be due to invalid IL or missing references)
		//IL_118c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1142: Unknown result type (might be due to invalid IL or missing references)
		//IL_114e: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_135e: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_137e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1389: Unknown result type (might be due to invalid IL or missing references)
		//IL_138e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1393: Unknown result type (might be due to invalid IL or missing references)
		//IL_139c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_107c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1081: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_10af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c35: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1805: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1107: Unknown result type (might be due to invalid IL or missing references)
		//IL_1109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_184b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1891: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1405: Unknown result type (might be due to invalid IL or missing references)
		//IL_140a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1414: Unknown result type (might be due to invalid IL or missing references)
		//IL_1424: Unknown result type (might be due to invalid IL or missing references)
		//IL_1429: Unknown result type (might be due to invalid IL or missing references)
		//IL_142e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1430: Unknown result type (might be due to invalid IL or missing references)
		//IL_143a: Unknown result type (might be due to invalid IL or missing references)
		//IL_144a: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1454: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1908: Unknown result type (might be due to invalid IL or missing references)
		//IL_1911: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_20db: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_2139: Unknown result type (might be due to invalid IL or missing references)
		//IL_215e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2168: Unknown result type (might be due to invalid IL or missing references)
		//IL_216d: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1507: Unknown result type (might be due to invalid IL or missing references)
		//IL_150c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1511: Unknown result type (might be due to invalid IL or missing references)
		//IL_1518: Unknown result type (might be due to invalid IL or missing references)
		//IL_151d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1531: Unknown result type (might be due to invalid IL or missing references)
		//IL_1536: Unknown result type (might be due to invalid IL or missing references)
		//IL_1538: Unknown result type (might be due to invalid IL or missing references)
		//IL_1542: Unknown result type (might be due to invalid IL or missing references)
		//IL_1552: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_155c: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.slimeGodPurple < 0 || !Main.npc[CalamityGlobalNPC.slimeGodPurple].active)
		{
			CalamityGlobalNPC.slimeGodPurple = base.NPC.whoAmI;
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
			base.NPC.defense = base.NPC.defDefense + 16;
			setDamage += 20;
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
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.timeLeft < 1800)
			{
				base.NPC.timeLeft = 1800;
			}
		}
		bool enraged = true;
		bool hyperMode = base.NPC.localAI[1] == 1f;
		if (CalamityGlobalNPC.slimeGodRed != -1 && Main.npc[CalamityGlobalNPC.slimeGodRed].active)
		{
			enraged = false;
		}
		float teleportGateValue = 720f;
		if (!player.dead && base.NPC.timeLeft > 10 && base.NPC.ai[3] >= teleportGateValue && base.NPC.ai[0] == 0f && base.NPC.velocity.Y == 0f)
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
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				base.NPC.velocity.X *= 0.8f;
				base.NPC.ai[1] += (hyperMode ? 2f : 1f);
				float jumpGateValue = 40f;
				float velocityX = (death ? 10f : (revenge ? 9f : (expertMode ? 8f : 6f)));
				if (revenge)
				{
					float moveBoost = (death ? (15f * (1f - lifeRatio)) : (10f * (1f - lifeRatio)));
					float speedBoost = (death ? (3f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
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
					velocityY *= 1.25f;
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
				float phaseSwitchGateValue = 210f;
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
							base.NPC.velocity.Y = (death ? (-10f) : (revenge ? (-9f) : (expertMode ? (-8f) : (-7f))));
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
			base.NPC.velocity.X *= 0.8f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 15f)
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
							int type = ModContent.ProjectileType<UnstableEbonianGlob>();
							int damage = GlobDamage;
							int numProj = (death ? 5 : 3);
							float rotation = MathHelper.ToRadians((float)(death ? 12 : 8));
							for (int j = 0; j < numProj; j++)
							{
								Vector2 randomVelocity = Main.rand.NextVector2CircularEdge(3f, 3f);
								Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)j / (float)(numProj - 1))) + randomVelocity;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 30f * base.NPC.scale, perturbedSpeed, type, damage, 0f, Main.myPlayer);
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
				targetDist *= (death ? 10f : (revenge ? 9f : (expertMode ? 8f : 7f)));
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
				targetDist *= (death ? 12f : (revenge ? 11f : (expertMode ? 10f : 9f))) + distanceSpeedBoost;
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
						int type2 = ModContent.ProjectileType<UnstableEbonianGlob>();
						int damage2 = GlobDamage;
						Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
						((Vector2)(ref destination)).Normalize();
						destination *= projectileVelocity2;
						int numProj2 = 9;
						float rotation2 = MathHelper.ToRadians(90f);
						for (int i = 0; i < numProj2; i++)
						{
							if (i < 3 || i > 5)
							{
								Vector2 perturbedSpeed2 = destination.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)i / (float)(numProj2 - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitY * 30f * base.NPC.scale + Vector2.Normalize(perturbedSpeed2) * 30f * base.NPC.scale, perturbedSpeed2, type2, damage2, 0f, Main.myPlayer);
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
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projFireDirection) * 30f * base.NPC.scale, projFireDirection, type2, damage2, 0f, Main.myPlayer);
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
			for (int k = 0; k < 10; k++)
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
			for (int l = 0; l < 10; l++)
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
		int slimeSpawnThreshold = (int)((double)base.NPC.lifeMax * 0.2);
		if ((float)(base.NPC.life + slimeSpawnThreshold) < bossLife)
		{
			bossLife = base.NPC.life;
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

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Color lightColor = default(Color);
		((Color)(ref lightColor))._002Ector(200, 150, Main.DiscoB, base.NPC.alpha);
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
		Color dustColor = Color.Lavender;
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
			int corruptionDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, dustColor, 2f);
			Dust obj = Main.dust[corruptionDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[corruptionDust].scale = 0.5f;
				Main.dust[corruptionDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int corruptionDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, dustColor, 3f);
			Main.dust[corruptionDust2].noGravity = true;
			Dust obj2 = Main.dust[corruptionDust2];
			obj2.velocity *= 5f;
			corruptionDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, dustColor, 2f);
			Dust obj3 = Main.dust[corruptionDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
