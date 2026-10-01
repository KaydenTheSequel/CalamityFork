using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

[AutoloadBossHead]
public class SupremeCatastrophe : ModNPC
{
	public int VerticalOffset;

	public int CurrentFrame;

	public bool SlashingFromRight;

	public int HorizontalOffset;

	public const int SlashCounterLimit = 50;

	public const int DartBurstCounterLimit = 300;

	public const int PreBigAttackPause = 50;

	public int BigAttackLimit;

	public bool targetSide;

	public int dashAttackTimer;

	public int dashes;

	public Vector2 offset;

	public bool MovingUp;

	public bool EnrageRoar;

	public int accSlashCounter;

	public bool setMovement;

	public bool broIsAlive;

	public bool isDeathmode;

	public static Asset<Texture2D> GlowTexture;

	public static int SlashDamage = 100;

	public static int TrailedSlashDamage = 120;

	public Player Target => Main.player[base.NPC.target];

	public ref float SlashCounter => ref base.NPC.ai[1];

	public ref float DartBurstCounter => ref base.NPC.ai[2];

	public ref float ElapsedVerticalDistance => ref base.NPC.ai[3];

	public ref float AttackDelayTimer => ref base.NPC.localAI[0];

	public ref float BigAttackTimer => ref base.NPC.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.3f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 56f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 120;
		base.NPC.height = 120;
		base.NPC.defense = 80;
		base.NPC.DR_NERD(0.25f);
		base.NPC.lifeMax = 138000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SupremeCalamitas.BrotherHit;
		base.NPC.DeathSound = SupremeCalamitas.BrotherDeath;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.localAI[1] = 500f;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<SupremeCalamitas>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SupremeCatastrophe")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(VerticalOffset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		VerticalOffset = reader.ReadInt32();
	}

	public override void FindFrame(int frameHeight)
	{
		float slashCounter = SlashCounter + (SlashingFromRight ? 0f : 50f);
		float slashInterpolant = Utils.GetLerpValue(0f, 100f, slashCounter, clamped: true);
		if (AttackDelayTimer < 120f)
		{
			base.NPC.frameCounter += 0.15000000596046448;
			if (base.NPC.frameCounter >= 1.0)
			{
				CurrentFrame = (CurrentFrame + 1) % 6;
				base.NPC.frameCounter = 0.0;
			}
		}
		else
		{
			CurrentFrame = (int)Math.Round(MathHelper.Lerp(6f, 15f, slashInterpolant));
		}
		int xFrame = CurrentFrame / Main.npcFrameCount[base.Type];
		int yFrame = CurrentFrame % Main.npcFrameCount[base.Type];
		base.NPC.frame.Width = 400;
		base.NPC.frame.Height = 230;
		base.NPC.frame.X = xFrame * base.NPC.frame.Width;
		base.NPC.frame.Y = yFrame * base.NPC.frame.Height;
	}

	public override void AI()
	{
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1009: Unknown result type (might be due to invalid IL or missing references)
		//IL_1022: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Unknown result type (might be due to invalid IL or missing references)
		//IL_102f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1034: Unknown result type (might be due to invalid IL or missing references)
		//IL_103b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1040: Unknown result type (might be due to invalid IL or missing references)
		//IL_104a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1054: Unknown result type (might be due to invalid IL or missing references)
		//IL_105a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1087: Unknown result type (might be due to invalid IL or missing references)
		//IL_108c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_1147: Unknown result type (might be due to invalid IL or missing references)
		//IL_114c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_117a: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_118a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1194: Unknown result type (might be due to invalid IL or missing references)
		//IL_1199: Unknown result type (might be due to invalid IL or missing references)
		//IL_119e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1162: Unknown result type (might be due to invalid IL or missing references)
		//IL_1167: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1201: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1300: Unknown result type (might be due to invalid IL or missing references)
		//IL_1307: Unknown result type (might be due to invalid IL or missing references)
		//IL_130c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1219: Unknown result type (might be due to invalid IL or missing references)
		//IL_121e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1209: Unknown result type (might be due to invalid IL or missing references)
		//IL_1228: Unknown result type (might be due to invalid IL or missing references)
		//IL_128e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_1366: Unknown result type (might be due to invalid IL or missing references)
		//IL_1371: Unknown result type (might be due to invalid IL or missing references)
		//IL_1383: Unknown result type (might be due to invalid IL or missing references)
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_1396: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1402: Unknown result type (might be due to invalid IL or missing references)
		//IL_1413: Unknown result type (might be due to invalid IL or missing references)
		//IL_1418: Unknown result type (might be due to invalid IL or missing references)
		//IL_1422: Unknown result type (might be due to invalid IL or missing references)
		//IL_1430: Unknown result type (might be due to invalid IL or missing references)
		//IL_1449: Unknown result type (might be due to invalid IL or missing references)
		//IL_1465: Unknown result type (might be due to invalid IL or missing references)
		if (setMovement)
		{
			MovingUp = base.NPC.ai[0] == 1f;
			setMovement = false;
		}
		base.NPC.direction = base.NPC.spriteDirection;
		if (BigAttackTimer > 0f)
		{
			BigAttackTimer--;
		}
		CalamityGlobalNPC.SCalCatastrophe = base.NPC.whoAmI;
		if (CalamityGlobalNPC.SCal < 0 || !Main.npc[CalamityGlobalNPC.SCal].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		if (!Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().respawnBro && isDeathmode && !broIsAlive && (float)base.NPC.life > (float)base.NPC.lifeMax * 0.65f)
		{
			base.NPC.life = (int)((float)base.NPC.lifeMax * 0.65f);
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (CalamityWorld.revenge)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		base.NPC.Calamity().DR = 0.25f;
		if (Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().protectionBoost)
		{
			base.NPC.Calamity().DR = SupremeCalamitas.enragedDR;
		}
		float totalLifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (CalamityGlobalNPC.SCalCataclysm != -1 && Main.npc[CalamityGlobalNPC.SCalCataclysm].active)
		{
			totalLifeRatio += (float)Main.npc[CalamityGlobalNPC.SCalCataclysm].life / (float)Main.npc[CalamityGlobalNPC.SCalCataclysm].lifeMax;
		}
		totalLifeRatio *= 0.5f;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Target.dead || !Target.active)
		{
			base.NPC.TargetClosest();
		}
		if (!base.NPC.WithinRange(Target.Center, 3200f))
		{
			base.NPC.TargetClosest();
		}
		Utils.Remap(AttackDelayTimer, 0f, 120f, 2f, 4f);
		int verticalSpeed = (int)Math.Round(MathHelper.Lerp(2f, 6.5f, 1f - totalLifeRatio));
		bool Phase2 = !broIsAlive;
		if ((double)((float)base.NPC.life / (float)base.NPC.lifeMax) < (death ? 0.6 : 0.4) && !Phase2)
		{
			if (EnrageRoar)
			{
				EnrageRoar = false;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerLimbLoss2");
				style.Volume = 1.5f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.NPC.Center);
			}
			Phase2 = true;
		}
		if (Phase2 && BigAttackTimer > 400f)
		{
			BigAttackTimer = 400f;
		}
		if (Phase2 && BigAttackTimer > 0f && BigAttackLimit < 11)
		{
			BigAttackLimit = 11;
		}
		else if (BigAttackTimer > 0f && BigAttackLimit < 7)
		{
			BigAttackLimit = 7;
		}
		if (BigAttackTimer > 50f)
		{
			if (Phase2)
			{
				if (MovingUp)
				{
					if (VerticalOffset < 400)
					{
						VerticalOffset += (int)((float)verticalSpeed * 1.5f);
					}
					else
					{
						MovingUp = false;
					}
				}
				else if (VerticalOffset > -400)
				{
					VerticalOffset -= (int)((float)verticalSpeed * 1.5f);
				}
				else
				{
					MovingUp = true;
				}
				_ = base.NPC.SafeDirectionTo(Target.Center + new Vector2((float)(-HorizontalOffset * ((!targetSide) ? 1 : (-1))), (float)VerticalOffset)) * (Phase2 ? Utils.Remap(AttackDelayTimer, 0f, 120f, 15f, 50f) : Utils.Remap(AttackDelayTimer, 60f, 120f, 0f, 50f));
				if (SlashCounter <= 15.000001f && dashAttackTimer == 0)
				{
					if (AttackDelayTimer == 120f && Main.rand.NextBool())
					{
						for (int i = 0; i < 6; i++)
						{
							Dust dust = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(base.NPC.width, base.NPC.height) - base.NPC.velocity * 0.5f, 66, -base.NPC.velocity * Main.rand.NextFloat(0.1f, 0.6f));
							dust.noGravity = true;
							dust.scale = Main.rand.NextFloat(0.7f, 1.3f);
							dust.color = Color.Cyan;
						}
					}
					CalamityUtils.SmoothMovement(base.NPC, 0f, Target.Center + new Vector2((float)(-HorizontalOffset * ((!targetSide) ? 1 : (-1))), (float)VerticalOffset) - base.NPC.Center, Utils.Remap(AttackDelayTimer, 10f, 120f, 20f, 80f), 1f, useSimpleFlyMovement: false);
				}
				else if (dashAttackTimer == 0)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.85f;
				}
				else
				{
					dashAttackTimer--;
					int type = ModContent.ProjectileType<SupremeCatastropheSlash>();
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, base.NPC.velocity.SafeNormalize(Vector2.UnitY) * 0.1f, type, TrailedSlashDamage, 0f, Main.myPlayer, 0f, SlashingFromRight.ToInt(), 4 + dashes);
					dashes++;
					if (dashes == 30 && !broIsAlive)
					{
						SoundStyle style;
						if (Main.zenithWorld)
						{
							style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash");
							style.Volume = 0.85f;
							style.Pitch = -0.5f;
							SoundEngine.PlaySound(in style, base.NPC.Center);
							base.NPC.velocity = base.NPC.DirectionTo(Target.Center) * 150f;
							for (int j = 0; j < 30; j++)
							{
								Vector2 vel = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
								Dust dust2 = Dust.NewDustPerfect(base.NPC.Center + vel * 2f, 279, vel);
								dust2.noGravity = true;
								dust2.scale = Main.rand.NextFloat(1.2f, 1.8f);
								dust2.color = Color.DeepSkyBlue;
							}
							dashes = 0;
							dashAttackTimer = 30;
							BigAttackTimer = 500f;
							AttackDelayTimer = 120f;
							if (base.NPC.life > 10001)
							{
								base.NPC.life -= 10000;
							}
							return;
						}
						style = new SoundStyle("CalamityMod/Sounds/Item/MurasamaBigSwing");
						style.Volume = 0.55f;
						style.Pitch = -0.3f;
						SoundEngine.PlaySound(in style, base.NPC.Center);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + base.NPC.DirectionTo(Target.Center).RotatedBy(0.3400000035762787) * 130f, base.NPC.DirectionTo(Target.Center).RotatedBy(0.3400000035762787) * 90f, type, TrailedSlashDamage, 0f, Main.myPlayer, 0f, 0f, 50f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + base.NPC.DirectionTo(Target.Center).RotatedBy(-0.3400000035762787) * 130f, base.NPC.DirectionTo(Target.Center).RotatedBy(-0.3400000035762787) * 90f, type, TrailedSlashDamage, 0f, Main.myPlayer, 0f, 0f, 50f);
						for (int k = 0; k < 30; k++)
						{
							Vector2 vel2 = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
							Dust dust3 = Dust.NewDustPerfect(base.NPC.Center + vel2 * 2f, 279, vel2);
							dust3.noGravity = true;
							dust3.scale = Main.rand.NextFloat(1.2f, 1.8f);
							dust3.color = Color.DeepSkyBlue;
						}
					}
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 0.975f;
				}
			}
			else
			{
				if (MovingUp)
				{
					if (VerticalOffset < 400)
					{
						VerticalOffset += verticalSpeed;
					}
					else
					{
						MovingUp = false;
					}
				}
				else if (VerticalOffset > -400)
				{
					VerticalOffset -= verticalSpeed;
				}
				else
				{
					MovingUp = true;
				}
				if (SlashCounter <= 15.000001f)
				{
					offset = base.NPC.DirectionTo(Target.Center) * 70f * (float)((!(BigAttackTimer <= 60f)) ? 1 : (-4));
				}
				else
				{
					offset *= 0.9f;
				}
				CalamityUtils.SmoothMovement(base.NPC, 0f, Target.Center + new Vector2((float)(-HorizontalOffset * ((!targetSide) ? 1 : (-1))), (float)VerticalOffset) - base.NPC.Center - offset, 17f, 2f, useSimpleFlyMovement: false);
			}
		}
		else
		{
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.9f;
		}
		base.NPC.rotation = 0f;
		base.NPC.spriteDirection = (Target.Center.X > base.NPC.Center.X).ToDirectionInt();
		if (AttackDelayTimer < 120f)
		{
			AttackDelayTimer += (death ? 1.5f : 1f);
			return;
		}
		if (BigAttackTimer > 50f)
		{
			float fireRate = MathHelper.Lerp(1.5f, 2f, 1f - totalLifeRatio) * (broIsAlive ? 1f : (death ? 1.25f : 1.05f));
			SlashCounter += fireRate;
			if (!(SlashCounter >= 50f))
			{
				return;
			}
			SlashCounter = 0f;
			SoundStyle style = SupremeCalamitas.CatastropheSwing with
			{
				Volume = 0.5f,
				Pitch = (SlashingFromRight ? 0.2f : (-0.2f))
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
			int type2 = ModContent.ProjectileType<SupremeCatastropheSlash>();
			if (Main.zenithWorld)
			{
				type2 = ModContent.ProjectileType<SupremeCataclysmFist>();
			}
			int damage = (Main.zenithWorld ? SupremeCataclysm.FistDamage : SlashDamage);
			Vector2 slashSpawnPosition = base.NPC.Center + Vector2.UnitX * 125f * (float)base.NPC.direction;
			if (Main.netMode != 1)
			{
				Vector2 firingVelocity = ((!broIsAlive) ? (base.NPC.DirectionTo(Target.Center) + Target.velocity * 0.0305f).SafeNormalize(Vector2.UnitY) : base.NPC.DirectionTo(Target.Center));
				if (Phase2)
				{
					if (accSlashCounter < 2)
					{
						accSlashCounter++;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slashSpawnPosition, firingVelocity * 5.5f, type2, damage, 0f, Main.myPlayer, 0f, SlashingFromRight.ToInt(), 2f);
					}
					else
					{
						style = new SoundStyle("CalamityMod/Sounds/Item/MurasamaBigSwing");
						style.Volume = 0.55f;
						style.Pitch = 0.4f;
						SoundEngine.PlaySound(in style, base.NPC.Center);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slashSpawnPosition, firingVelocity * 0.2f, type2, damage, 0f, Main.myPlayer, 0f, SlashingFromRight.ToInt(), (!Main.zenithWorld) ? 3 : 0);
						accSlashCounter = 0;
					}
				}
				else
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slashSpawnPosition, base.NPC.DirectionTo(Target.Center) * 8f, type2, damage, 0f, Main.myPlayer, 0f, SlashingFromRight.ToInt(), 2f);
				}
			}
			SlashingFromRight = !SlashingFromRight;
			CurrentFrame = 0;
			broIsAlive = NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>());
			return;
		}
		if (BigAttackTimer > 0f)
		{
			if (BigAttackTimer == 50f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerPillarSummon");
				style.Volume = 0.85f;
				style.Pitch = 0.6f;
				SoundEngine.PlaySound(in style, base.NPC.Center);
			}
			for (int l = 0; l < 7; l++)
			{
				Vector2 vel3 = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
				Dust dust4 = Dust.NewDustPerfect(base.NPC.Center + vel3 * 2f, 279, vel3);
				dust4.noGravity = true;
				dust4.scale = Main.rand.NextFloat(1.2f, 1.8f);
				dust4.color = Color.DeepSkyBlue;
			}
			BigAttackTimer--;
			return;
		}
		float fireRate2 = MathHelper.Lerp(2.5f, 3f, 1f - totalLifeRatio) * ((!broIsAlive) ? 1.35f : 1f);
		if (Phase2 && BigAttackLimit == 0)
		{
			fireRate2 = 1f;
		}
		SlashCounter += fireRate2;
		if (!(SlashCounter >= 50f))
		{
			return;
		}
		SlashCounter = 0f;
		int type3 = ModContent.ProjectileType<SupremeCatastropheSlash>();
		if (Main.zenithWorld)
		{
			type3 = ModContent.ProjectileType<SupremeCataclysmFist>();
		}
		int damage2 = (Main.zenithWorld ? SupremeCataclysm.FistDamage : SlashDamage);
		Vector2 slashSpawnPosition2 = base.NPC.Center;
		Vector2 firingVelocity2 = ((!broIsAlive) ? (base.NPC.DirectionTo(Target.Center) + Target.velocity * 0.032f).SafeNormalize(Vector2.UnitY) : base.NPC.DirectionTo(Target.Center));
		if ((broIsAlive ? (BigAttackLimit == 1) : (BigAttackLimit <= 3 && BigAttackLimit > 0)) & death)
		{
			if (!Main.zenithWorld)
			{
				damage2 = TrailedSlashDamage;
			}
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slashSpawnPosition2 + firingVelocity2 * 130f, (Phase2 ? base.NPC.DirectionTo(Target.Center) : firingVelocity2) * 90f, ModContent.ProjectileType<SupremeCatastropheSlash>(), damage2, 0f, Main.myPlayer, 0f, 5f, 50f);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MurasamaBigSwing");
			style.Volume = 0.55f;
			style.Pitch = -0.3f - (float)BigAttackLimit * 0.1f;
			SoundEngine.PlaySound(in style, base.NPC.Center);
		}
		else if ((BigAttackLimit == 0) & Phase2)
		{
			if (Main.netMode != 1)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash");
				style.Volume = 0.85f;
				style.Pitch = -0.5f;
				SoundEngine.PlaySound(in style, base.NPC.Center);
				base.NPC.velocity = firingVelocity2 * 90f;
				dashAttackTimer = 30;
				dashes = 0;
			}
		}
		else if (Main.netMode != 1)
		{
			SoundStyle style = SupremeCalamitas.CatastropheSwing with
			{
				Volume = 0.5f,
				Pitch = (SlashingFromRight ? 0.2f : (-0.2f))
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), slashSpawnPosition2, base.NPC.DirectionTo(Target.Center) * 8f, type3, damage2, 0f, Main.myPlayer, 0f, SlashingFromRight.ToInt(), 1f);
			for (int m = 0; m < 7; m++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center + base.NPC.DirectionTo(Target.Center) * 10f, (base.NPC.DirectionTo(Target.Center) * 30f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.4f, 1.1f), affectedByGravity: false, 40, Main.rand.NextFloat(0.75f, 2.25f), Color.Cyan));
			}
		}
		SlashingFromRight = !SlashingFromRight;
		CurrentFrame = 0;
		if (BigAttackLimit > 0)
		{
			BigAttackLimit--;
			return;
		}
		BigAttackTimer = 500f;
		AttackDelayTimer = 0f;
		BigAttackLimit = 7;
		targetSide = !targetSide;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 origin = base.NPC.frame.Size() * 0.5f;
		int afterimageCount = 4;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageCount; i += 2)
			{
				Color afterimageColor = base.NPC.GetAlpha(Color.Lerp(drawColor, Color.White, 0.5f)) * ((float)(afterimageCount - i) / 15f);
				Vector2 drawPosition = base.NPC.oldPos[i] + base.NPC.Size * 0.5f - screenPos;
				spriteBatch.Draw(texture, drawPosition, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 mainDrawPosition = base.NPC.Center - screenPos;
		spriteBatch.Draw(texture, mainDrawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		texture = GlowTexture.Value;
		Color baseGlowmaskColor = Color.Lerp(Color.White, Color.Cyan, 0.35f);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageCount; j++)
			{
				Color afterimageColor2 = Color.Lerp(baseGlowmaskColor, Color.White, 0.5f) * ((float)(afterimageCount - j) / 15f);
				Vector2 drawPosition2 = base.NPC.oldPos[j] + base.NPC.Size * 0.5f - screenPos;
				spriteBatch.Draw(texture, drawPosition2, (Rectangle?)base.NPC.frame, afterimageColor2, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture, mainDrawPosition, (Rectangle?)base.NPC.frame, baseGlowmaskColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
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
		npcLoot.Add(ModContent.ItemType<SupremeCatastropheTrophy>(), 10);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int brimDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[brimDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[brimDust].scale = 0.5f;
				Main.dust[brimDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[brimDust2].noGravity = true;
			Dust obj2 = Main.dust[brimDust2];
			obj2.velocity *= 5f;
			brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[brimDust2];
			obj3.velocity *= 2f;
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()))
		{
			NPC scal = Main.npc[CalamityGlobalNPC.SCal];
			if (scal.ModNPC<SupremeCalamitas>().respawnBro && isDeathmode && !broIsAlive)
			{
				for (int k = 0; k < 45; k++)
				{
					Vector2 vel = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
					Dust dust = Dust.NewDustPerfect(base.NPC.Center + vel * 2f, 279, vel);
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(1.2f, 1.8f);
					dust.color = Color.Red;
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 25));
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 28));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerLimbLoss3");
				style.Volume = 0.9f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.NPC.Center);
				CalamityUtils.SpawnBossBetter(base.NPC.Center, ModContent.NPCType<SupremeCataclysm>(), null, (!MovingUp) ? 1 : (-1));
				scal.ModNPC<SupremeCalamitas>().respawnBro = false;
			}
		}
		DeathAshParticle.CreateAshesFromNPC(base.NPC, Vector2.Zero);
	}

	public SupremeCatastrophe()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		HorizontalOffset = 750;
		BigAttackLimit = 7;
		offset = Vector2.Zero;
		EnrageRoar = true;
		setMovement = true;
		broIsAlive = true;
		isDeathmode = CalamityWorld.death || BossRushEvent.BossRushActive;
		base._002Ector();
	}
}
