using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.BiomeManagers;
using CalamityMod.Events;
using CalamityMod.Items.Tools;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Systems.Collections;
using CalamityMod.UI.CalamitasEnchants;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.NPCs.TownNPCs;

[AutoloadHead]
[LegacyName(new string[] { "WITCH" })]
public class BrimstoneWitch : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 27;
		NPCID.Sets.ExtraFramesCount[base.Type] = 11;
		NPCID.Sets.AttackFrameCount[base.Type] = 6;
		NPCID.Sets.DangerDetectRange[base.Type] = 700;
		NPCID.Sets.AttackType[base.Type] = 1;
		NPCID.Sets.AttackTime[base.Type] = 30;
		NPCID.Sets.AttackAverageChance[base.Type] = 5;
		NPCID.Sets.ShimmerTownTransform[base.Type] = false;
		base.NPC.Happiness.SetBiomeAffection<ForestBiome>(AffectionLevel.Like).SetBiomeAffection<BrimstoneCragsBiome>(AffectionLevel.Dislike).SetNPCAffection(54, AffectionLevel.Like)
			.SetNPCAffection(208, AffectionLevel.Dislike);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Velocity = 1f;
		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.NPC.type, drawModifiers);
	}

	public override void SetDefaults()
	{
		base.NPC.townNPC = true;
		base.NPC.friendly = true;
		base.NPC.lavaImmune = true;
		base.NPC.width = 18;
		base.NPC.height = 40;
		base.NPC.aiStyle = 7;
		base.NPC.damage = 10;
		base.NPC.gfxOffY = -2f;
		base.NPC.lifeMax = 960000;
		base.NPC.defense = 120;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath6;
		base.NPC.knockBackResist = 0.8f;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BrimstoneWitch")
		});
	}

	public override bool CanTownNPCSpawn(int numTownNPCs)
	{
		if (DownedBossSystem.downedCalamitas)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>());
		}
		return false;
	}

	public override List<string> SetNPCNameList()
	{
		return new List<string> { this.GetLocalizedValue("Name.Calamitas") };
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		int extraFrameAmt = (base.NPC.isLikeATownNPC ? NPCID.Sets.ExtraFramesCount[base.Type] : 0);
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
			int nonAttackFrames = Main.npcFrameCount[base.Type] - NPCID.Sets.AttackFrameCount[base.Type];
			if (base.NPC.ai[0] == 23f)
			{
				base.NPC.frameCounter++;
				int currentFrameHeight = base.NPC.frame.Y / frameHeight;
				int currentFrame = nonAttackFrames - currentFrameHeight;
				if ((uint)(currentFrame - 1) > 1u && (uint)(currentFrame - 4) > 1u && currentFrameHeight != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int num239 = ((!(base.NPC.frameCounter < 6.0)) ? (nonAttackFrames - 4) : (nonAttackFrames - 5));
				if (base.NPC.ai[1] < 6f)
				{
					num239 = nonAttackFrames - 5;
				}
				base.NPC.frame.Y = frameHeight * num239;
			}
			else if (base.NPC.ai[0] >= 20f && base.NPC.ai[0] <= 22f)
			{
				int num240 = base.NPC.frame.Y / frameHeight;
				int num241 = (int)base.NPC.ai[0];
				_ = num241 - 20;
				_ = 2;
				base.NPC.frame.Y = num240 * frameHeight;
			}
			else if (base.NPC.ai[0] == 2f)
			{
				base.NPC.frameCounter++;
				if (base.NPC.frame.Y / frameHeight == nonAttackFrames - 1 && base.NPC.frameCounter >= 5.0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				else if (base.NPC.frame.Y / frameHeight == 0 && base.NPC.frameCounter >= 40.0)
				{
					base.NPC.frame.Y = frameHeight * (nonAttackFrames - 1);
					base.NPC.frameCounter = 0.0;
				}
				else if (base.NPC.frame.Y != 0 && base.NPC.frame.Y != frameHeight * (nonAttackFrames - 1))
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
			}
			else if (base.NPC.ai[0] == 5f)
			{
				base.NPC.frame.Y = frameHeight * (nonAttackFrames - 3);
				base.NPC.frameCounter = 0.0;
			}
			else if (base.NPC.ai[0] == 6f)
			{
				base.NPC.frameCounter++;
				int confettiFrameHeight = base.NPC.frame.Y / frameHeight;
				int currentFrame2 = nonAttackFrames - confettiFrameHeight;
				if ((uint)(currentFrame2 - 1) > 1u && (uint)(currentFrame2 - 4) > 1u && confettiFrameHeight != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int confettiFrame = ((!(base.NPC.frameCounter < 10.0)) ? ((base.NPC.frameCounter < 16.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 46.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 60.0) ? (nonAttackFrames - 5) : ((!(base.NPC.frameCounter < 66.0)) ? ((base.NPC.frameCounter < 72.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 102.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 108.0) ? (nonAttackFrames - 5) : ((!(base.NPC.frameCounter < 114.0)) ? ((base.NPC.frameCounter < 120.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 150.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 156.0) ? (nonAttackFrames - 5) : ((!(base.NPC.frameCounter < 162.0)) ? ((base.NPC.frameCounter < 168.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 198.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 204.0) ? (nonAttackFrames - 5) : ((!(base.NPC.frameCounter < 210.0)) ? ((base.NPC.frameCounter < 216.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 246.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 252.0) ? (nonAttackFrames - 5) : ((!(base.NPC.frameCounter < 258.0)) ? ((base.NPC.frameCounter < 264.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 294.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 300.0) ? (nonAttackFrames - 5) : 0))) : 0)))) : 0)))) : 0)))) : 0)))) : 0)))) : 0);
				if (confettiFrame == nonAttackFrames - 4 && confettiFrameHeight == nonAttackFrames - 5)
				{
					Vector2 vector4 = base.NPC.Center + new Vector2((float)(10 * base.NPC.direction), -4f);
					for (int n = 0; n < 8; n++)
					{
						int confettiDust = Main.rand.Next(139, 143);
						int partyTime = Dust.NewDust(vector4, 0, 0, confettiDust, base.NPC.velocity.X + (float)base.NPC.direction, base.NPC.velocity.Y - 2.5f, 0, default(Color), 1.2f);
						Main.dust[partyTime].velocity.X += (float)base.NPC.direction * 1.5f;
						Dust obj = Main.dust[partyTime];
						obj.position -= new Vector2(4f);
						Dust obj2 = Main.dust[partyTime];
						obj2.velocity *= 2f;
						Main.dust[partyTime].scale = 0.7f + Main.rand.NextFloat() * 0.3f;
					}
				}
				base.NPC.frame.Y = frameHeight * confettiFrame;
				if (base.NPC.frameCounter >= 300.0)
				{
					base.NPC.frameCounter = 0.0;
				}
			}
			else if (base.NPC.ai[0] == 7f || base.NPC.ai[0] == 19f)
			{
				base.NPC.frameCounter++;
				int playerTalkFrameHeight = base.NPC.frame.Y / frameHeight;
				int currentFrame3 = nonAttackFrames - playerTalkFrameHeight;
				if ((uint)(currentFrame3 - 1) > 1u && (uint)(currentFrame3 - 4) > 1u && playerTalkFrameHeight != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int playerTalkFrame = 0;
				if (base.NPC.frameCounter < 16.0)
				{
					playerTalkFrame = 0;
				}
				else if (base.NPC.frameCounter == 16.0)
				{
					EmoteBubble.NewBubbleNPC(new WorldUIAnchor((Entity)base.NPC), 112);
				}
				else if (base.NPC.frameCounter < 128.0)
				{
					playerTalkFrame = ((base.NPC.frameCounter % 16.0 < 8.0) ? (nonAttackFrames - 2) : 0);
				}
				else if (base.NPC.frameCounter < 160.0)
				{
					playerTalkFrame = 0;
				}
				else if (base.NPC.frameCounter != 160.0)
				{
					playerTalkFrame = ((base.NPC.frameCounter < 220.0) ? ((base.NPC.frameCounter % 12.0 < 6.0) ? (nonAttackFrames - 2) : 0) : 0);
				}
				else
				{
					EmoteBubble.NewBubbleNPC(new WorldUIAnchor((Entity)base.NPC), 60);
				}
				base.NPC.frame.Y = frameHeight * playerTalkFrame;
				if (base.NPC.frameCounter >= 220.0)
				{
					base.NPC.frameCounter = 0.0;
				}
			}
			else if (base.NPC.ai[0] == 9f)
			{
				base.NPC.frameCounter++;
				int num251 = base.NPC.frame.Y / frameHeight;
				int currentFrame4 = nonAttackFrames - num251;
				if ((uint)(currentFrame4 - 1) > 1u && (uint)(currentFrame4 - 4) > 1u && num251 != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int num252 = ((!(base.NPC.frameCounter < 10.0)) ? ((!(base.NPC.frameCounter < 16.0)) ? (nonAttackFrames - 4) : (nonAttackFrames - 5)) : 0);
				if (base.NPC.ai[1] < 16f)
				{
					num252 = nonAttackFrames - 5;
				}
				if (base.NPC.ai[1] < 10f)
				{
					num252 = 0;
				}
				base.NPC.frame.Y = frameHeight * num252;
			}
			else if (base.NPC.ai[0] == 18f)
			{
				base.NPC.frameCounter++;
				int num253 = base.NPC.frame.Y / frameHeight;
				int currentFrame5 = nonAttackFrames - num253;
				if ((uint)(currentFrame5 - 1) > 1u && (uint)(currentFrame5 - 4) > 1u && num253 != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int num254 = 0;
				if (base.NPC.frameCounter < 10.0)
				{
					num254 = 0;
				}
				else if (base.NPC.frameCounter < 16.0)
				{
					num254 = nonAttackFrames - 1;
				}
				else
				{
					num254 = nonAttackFrames - 2;
				}
				if (base.NPC.ai[1] < 16f)
				{
					num254 = nonAttackFrames - 1;
				}
				if (base.NPC.ai[1] < 10f)
				{
					num254 = 0;
				}
				num254 = Main.npcFrameCount[base.Type] - 2;
				base.NPC.frame.Y = frameHeight * num254;
			}
			else if (base.NPC.ai[0] == 10f || base.NPC.ai[0] == 13f)
			{
				base.NPC.frameCounter++;
				int attackFrameHeight = base.NPC.frame.Y / frameHeight;
				if ((uint)(attackFrameHeight - nonAttackFrames) > 3u && attackFrameHeight != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int attackTimingStart = 10;
				int attackFrameTiming = 6;
				int attackFrame = ((!(base.NPC.frameCounter < (double)attackTimingStart)) ? ((base.NPC.frameCounter < (double)(attackTimingStart + attackFrameTiming)) ? nonAttackFrames : ((base.NPC.frameCounter < (double)(attackTimingStart + attackFrameTiming * 2)) ? (nonAttackFrames + 1) : ((base.NPC.frameCounter < (double)(attackTimingStart + attackFrameTiming * 3)) ? (nonAttackFrames + 2) : ((base.NPC.frameCounter < (double)(attackTimingStart + attackFrameTiming * 4)) ? (nonAttackFrames + 3) : 0)))) : 0);
				base.NPC.frame.Y = frameHeight * attackFrame;
			}
			else if (base.NPC.ai[0] == 15f)
			{
				base.NPC.frameCounter++;
				int num259 = base.NPC.frame.Y / frameHeight;
				if ((uint)(num259 - nonAttackFrames) > 3u && num259 != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				float num260 = base.NPC.ai[1] / (float)NPCID.Sets.AttackTime[base.Type];
				int num261 = 0;
				num261 = ((num260 > 0.65f) ? nonAttackFrames : ((num260 > 0.5f) ? (nonAttackFrames + 1) : ((num260 > 0.35f) ? (nonAttackFrames + 2) : ((num260 > 0f) ? (nonAttackFrames + 3) : 0))));
				base.NPC.frame.Y = frameHeight * num261;
			}
			else if (base.NPC.ai[0] == 25f)
			{
				base.NPC.frame.Y = frameHeight;
			}
			else if (base.NPC.ai[0] == 12f)
			{
				base.NPC.frameCounter++;
				int num262 = base.NPC.frame.Y / frameHeight;
				if ((uint)(num262 - nonAttackFrames) > 4u && num262 != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int num263 = nonAttackFrames + base.NPC.GetShootingFrame(base.NPC.ai[2]);
				base.NPC.frame.Y = frameHeight * num263;
			}
			else if (base.NPC.ai[0] == 14f || base.NPC.ai[0] == 24f)
			{
				base.NPC.frameCounter++;
				int num264 = base.NPC.frame.Y / frameHeight;
				if ((uint)(num264 - nonAttackFrames) > 1u && num264 != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				int num265 = 12;
				int num266 = ((base.NPC.frameCounter % (double)num265 * 2.0 < (double)num265) ? nonAttackFrames : (nonAttackFrames + 1));
				base.NPC.frame.Y = frameHeight * num266;
				if (base.NPC.ai[0] == 24f)
				{
					if (base.NPC.frameCounter == 60.0)
					{
						EmoteBubble.NewBubble(87, new WorldUIAnchor((Entity)base.NPC), 60);
					}
					if (base.NPC.frameCounter == 150.0)
					{
						EmoteBubble.NewBubble(3, new WorldUIAnchor((Entity)base.NPC), 90);
					}
					if (base.NPC.frameCounter >= 240.0)
					{
						base.NPC.frame.Y = 0;
					}
				}
			}
			else if (base.NPC.ai[0] == 1001f)
			{
				base.NPC.frame.Y = frameHeight * (nonAttackFrames - 1);
				base.NPC.frameCounter = 0.0;
			}
			else if (base.NPC.CanTalk && (base.NPC.ai[0] == 3f || base.NPC.ai[0] == 4f))
			{
				base.NPC.frameCounter++;
				int npcTalkFrameHeight = base.NPC.frame.Y / frameHeight;
				int currentFrame6 = nonAttackFrames - npcTalkFrameHeight;
				if ((uint)(currentFrame6 - 1) > 1u && (uint)(currentFrame6 - 4) > 1u && npcTalkFrameHeight != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				bool displayEmote = base.NPC.ai[0] == 3f;
				int npcTalkFrame = 0;
				int npcTalkHandFrame = 0;
				int emoteDisplayTime = -1;
				int emoteDisplayTime2 = -1;
				if (base.NPC.frameCounter < 10.0)
				{
					npcTalkFrame = 0;
				}
				else if (base.NPC.frameCounter < 16.0)
				{
					npcTalkFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 46.0)
				{
					npcTalkFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 60.0)
				{
					npcTalkFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 216.0)
				{
					npcTalkFrame = 0;
				}
				else if (base.NPC.frameCounter == 216.0 && Main.netMode != 1)
				{
					emoteDisplayTime = 70;
				}
				else if (base.NPC.frameCounter < 286.0)
				{
					npcTalkFrame = ((base.NPC.frameCounter % 12.0 < 6.0) ? (nonAttackFrames - 2) : 0);
				}
				else if (base.NPC.frameCounter < 320.0)
				{
					npcTalkFrame = 0;
				}
				else if (base.NPC.frameCounter != 320.0 || Main.netMode == 1)
				{
					npcTalkFrame = ((base.NPC.frameCounter < 420.0) ? ((base.NPC.frameCounter % 16.0 < 8.0) ? (nonAttackFrames - 2) : 0) : 0);
				}
				else
				{
					emoteDisplayTime = 100;
				}
				if (base.NPC.frameCounter < 70.0)
				{
					npcTalkHandFrame = 0;
				}
				else if (base.NPC.frameCounter != 70.0 || Main.netMode == 1)
				{
					npcTalkHandFrame = ((!(base.NPC.frameCounter < 160.0)) ? ((base.NPC.frameCounter < 166.0) ? (nonAttackFrames - 5) : ((base.NPC.frameCounter < 186.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 200.0) ? (nonAttackFrames - 5) : ((!(base.NPC.frameCounter < 320.0)) ? ((base.NPC.frameCounter < 326.0) ? (nonAttackFrames - 1) : 0) : 0)))) : ((base.NPC.frameCounter % 16.0 < 8.0) ? (nonAttackFrames - 2) : 0));
				}
				else
				{
					emoteDisplayTime2 = 90;
				}
				if (displayEmote)
				{
					NPC nPC = Main.npc[(int)base.NPC.ai[2]];
					if (emoteDisplayTime != -1)
					{
						EmoteBubble.NewBubbleNPC(new WorldUIAnchor((Entity)base.NPC), emoteDisplayTime, new WorldUIAnchor((Entity)nPC));
					}
					if (emoteDisplayTime2 != -1 && nPC.CanTalk)
					{
						EmoteBubble.NewBubbleNPC(new WorldUIAnchor((Entity)nPC), emoteDisplayTime2, new WorldUIAnchor((Entity)base.NPC));
					}
				}
				base.NPC.frame.Y = frameHeight * (displayEmote ? npcTalkFrame : npcTalkHandFrame);
				if (base.NPC.frameCounter >= 420.0)
				{
					base.NPC.frameCounter = 0.0;
				}
			}
			else if (base.NPC.CanTalk && (base.NPC.ai[0] == 16f || base.NPC.ai[0] == 17f))
			{
				base.NPC.frameCounter++;
				int rpsFrameHeight = base.NPC.frame.Y / frameHeight;
				int currentFrame7 = nonAttackFrames - rpsFrameHeight;
				if ((uint)(currentFrame7 - 1) > 1u && (uint)(currentFrame7 - 4) > 1u && rpsFrameHeight != 0)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
				}
				bool controlsRPS = base.NPC.ai[0] == 16f;
				int rpsFrame = 0;
				int emoteDisplayTime3 = -1;
				if (base.NPC.frameCounter < 10.0)
				{
					rpsFrame = 0;
				}
				else if (base.NPC.frameCounter < 16.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 22.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 28.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 34.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 40.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter == 40.0 && Main.netMode != 1)
				{
					emoteDisplayTime3 = 45;
				}
				else if (base.NPC.frameCounter < 70.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 76.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 82.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 88.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 94.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 100.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter == 100.0 && Main.netMode != 1)
				{
					emoteDisplayTime3 = 45;
				}
				else if (base.NPC.frameCounter < 130.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 136.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 142.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 148.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter < 154.0)
				{
					rpsFrame = nonAttackFrames - 4;
				}
				else if (base.NPC.frameCounter < 160.0)
				{
					rpsFrame = nonAttackFrames - 5;
				}
				else if (base.NPC.frameCounter != 160.0 || Main.netMode == 1)
				{
					rpsFrame = ((base.NPC.frameCounter < 220.0) ? (nonAttackFrames - 4) : ((base.NPC.frameCounter < 226.0) ? (nonAttackFrames - 5) : 0));
				}
				else
				{
					emoteDisplayTime3 = 75;
				}
				if (controlsRPS && emoteDisplayTime3 != -1)
				{
					int npcPick = (int)base.NPC.localAI[2];
					int npcWins = (int)base.NPC.localAI[3];
					int opponentWins = (int)Main.npc[(int)base.NPC.ai[2]].localAI[3];
					int opponentPick = (int)Main.npc[(int)base.NPC.ai[2]].localAI[2];
					int rpsGameEnder = 3 - npcPick - npcWins;
					int numGamesPlayed = 0;
					if (base.NPC.frameCounter == 40.0)
					{
						numGamesPlayed = 1;
					}
					if (base.NPC.frameCounter == 100.0)
					{
						numGamesPlayed = 2;
					}
					if (base.NPC.frameCounter == 160.0)
					{
						numGamesPlayed = 3;
					}
					int gameCountdown = 3 - numGamesPlayed;
					int rockPaperScissorsResultType = -1;
					int gameFrameTimer = 0;
					while (rockPaperScissorsResultType < 0)
					{
						currentFrame7 = gameFrameTimer + 1;
						gameFrameTimer = currentFrame7;
						if (currentFrame7 >= 100)
						{
							break;
						}
						rockPaperScissorsResultType = Main.rand.Next(2);
						if (rockPaperScissorsResultType == 0 && opponentPick >= npcWins)
						{
							rockPaperScissorsResultType = -1;
						}
						if (rockPaperScissorsResultType == 1 && opponentWins >= npcPick)
						{
							rockPaperScissorsResultType = -1;
						}
						if (rockPaperScissorsResultType == -1 && gameCountdown <= rpsGameEnder)
						{
							rockPaperScissorsResultType = 2;
						}
					}
					if (rockPaperScissorsResultType == 0)
					{
						Main.npc[(int)base.NPC.ai[2]].localAI[3]++;
						opponentWins++;
					}
					if (rockPaperScissorsResultType == 1)
					{
						Main.npc[(int)base.NPC.ai[2]].localAI[2]++;
						opponentPick++;
					}
					int emoteType = Utils.SelectRandom<int>(Main.rand, 38, 37, 36);
					int emoteType2 = emoteType;
					switch (rockPaperScissorsResultType)
					{
					case 0:
						switch (emoteType)
						{
						case 38:
							emoteType2 = 37;
							break;
						case 37:
							emoteType2 = 36;
							break;
						case 36:
							emoteType2 = 38;
							break;
						}
						break;
					case 1:
						switch (emoteType)
						{
						case 38:
							emoteType2 = 36;
							break;
						case 37:
							emoteType2 = 38;
							break;
						case 36:
							emoteType2 = 37;
							break;
						}
						break;
					}
					if (gameCountdown == 0)
					{
						if (opponentWins >= 2)
						{
							emoteType -= 3;
						}
						if (opponentPick >= 2)
						{
							emoteType2 -= 3;
						}
					}
					EmoteBubble.NewBubble(emoteType, new WorldUIAnchor((Entity)base.NPC), emoteDisplayTime3);
					EmoteBubble.NewBubble(emoteType2, new WorldUIAnchor((Entity)Main.npc[(int)base.NPC.ai[2]]), emoteDisplayTime3);
				}
				base.NPC.frame.Y = frameHeight * (controlsRPS ? rpsFrame : rpsFrame);
				if (base.NPC.frameCounter >= 420.0)
				{
					base.NPC.frameCounter = 0.0;
				}
			}
			else if (base.NPC.velocity.X == 0f)
			{
				base.NPC.frame.Y = 0;
				base.NPC.frameCounter = 0.0;
			}
			else
			{
				base.NPC.frameCounter += Math.Abs(base.NPC.velocity.X) * 2f;
				base.NPC.frameCounter++;
				int walkFrameHeightLimit = frameHeight * 2;
				if (base.NPC.frame.Y < walkFrameHeightLimit)
				{
					base.NPC.frame.Y = walkFrameHeightLimit;
				}
				int walkFrameTimer = 6;
				if (base.NPC.frameCounter > (double)walkFrameTimer)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y / frameHeight >= Main.npcFrameCount[base.Type] - extraFrameAmt)
				{
					base.NPC.frame.Y = walkFrameHeightLimit;
				}
			}
		}
		else
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = frameHeight;
		}
	}

	public override string GetChat()
	{
		WeightedRandom<string> dialogue = new WeightedRandom<string>();
		if (Main.rand.NextBool(4444))
		{
			return this.GetLocalizedValue("Chat.EasterEgg");
		}
		if (base.NPC.homeless)
		{
			return this.GetLocalizedValue("Chat.Homeless" + Main.rand.Next(1, 3));
		}
		dialogue.Add(this.GetLocalizedValue("Chat.Normal1"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal2"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal3"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal4"));
		dialogue.Add(this.GetLocalizedValue("Chat.Normal5"));
		if (!Main.dayTime)
		{
			if (Main.bloodMoon)
			{
				dialogue.Add(this.GetLocalizedValue("Chat.BloodMoon1"), 5.15);
				dialogue.Add(this.GetLocalizedValue("Chat.BloodMoon2"), 5.15);
			}
			else
			{
				dialogue.Add(this.GetLocalizedValue("Chat.Night1"), 2.8);
				dialogue.Add(this.GetLocalizedValue("Chat.Night2"), 2.8);
			}
		}
		if (NPC.AnyNPCs(ModContent.NPCType<SeaKing>()))
		{
			dialogue.Add(this.GetLocalizedValue("Chat.SeaKing"), 1.45);
		}
		if (BirthdayParty.PartyIsUp)
		{
			dialogue.Add(this.GetLocalizedValue("Chat.Party"), 5.5);
		}
		return dialogue;
	}

	public override void SetChatButtons(ref string button, ref string button2)
	{
		button = this.GetLocalizedValue("EnchantButton");
		button2 = this.GetLocalizedValue("DonorButton");
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shopName)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (firstButton)
		{
			Main.playerInventory = true;
			CalamitasEnchantUI.NPCIndex = base.NPC.whoAmI;
			CalamitasEnchantUI.CurrentlyViewing = true;
			if (!Main.LocalPlayer.Calamity().GivenBrimstoneLocus)
			{
				Item.NewItem(base.NPC.GetSource_Loot(), base.NPC.Hitbox, ModContent.ItemType<BrimstoneLocus>());
				Main.LocalPlayer.Calamity().GivenBrimstoneLocus = true;
			}
		}
		else
		{
			Main.npcChatText = GetRandomDonors(25);
		}
	}

	public string GetRandomDonors(int numDonors)
	{
		IList<string> list = DonatorsNameList.List;
		int count = list.Count;
		List<string> list2 = new List<string>(count);
		CollectionsMarshal.SetCount(list2, count);
		Span<string> span = CollectionsMarshal.AsSpan(list2);
		int num = 0;
		foreach (string item in list)
		{
			span[num] = item;
			num++;
		}
		IList<string> pickingList = list2;
		string[] pickedDonors = new string[numDonors];
		for (int i = 0; i < numDonors; i++)
		{
			int idxSelected = Main.rand.Next(pickingList.Count);
			pickedDonors[i] = pickingList[idxSelected];
			pickingList.RemoveAt(idxSelected);
		}
		LocalizedText localization = this.GetLocalization("DonorShoutout");
		object[] args = pickedDonors;
		return localization.Format(args);
	}

	public override bool CanGoToStatue(bool toKingStatue)
	{
		return !toKingStatue;
	}

	public override void TownNPCAttackStrength(ref int damage, ref float knockback)
	{
		damage = 300;
		knockback = 10f;
	}

	public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
	{
		cooldown = 10;
		randExtraCooldown = 15;
	}

	public override bool PreAI()
	{
		if (NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>()) && !BossRushEvent.BossRushActive)
		{
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return false;
		}
		return true;
	}

	public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
	{
		projType = ModContent.ProjectileType<SeethingDischargeBrimstoneHellblast>();
		attackDelay = 1;
	}

	public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
	{
		multiplier = 2f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 50);
		base.NPC.position.X -= base.NPC.width / 2;
		base.NPC.position.Y -= base.NPC.height / 2;
		for (int i = 0; i < 5; i++)
		{
			int brimstone = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[brimstone];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[brimstone].scale = 0.5f;
				Main.dust[brimstone].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int fire = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fire].noGravity = true;
			Dust obj2 = Main.dust[fire];
			obj2.velocity *= 5f;
			fire = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
	}
}
