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
public class SupremeCataclysm : ModNPC
{
	public int VerticalOffset;

	public int CurrentFrame;

	public bool PunchingFromRight;

	public int HorizontalOffset;

	public const int PunchCounterLimit = 50;

	public const int DartBurstCounterLimit = 300;

	public const int PreBigAttackPause = 50;

	public const float NormalBrothersDR = 0.25f;

	public int BigAttackLimit;

	public bool targetSide;

	public int secondOrbTimer;

	public Vector2 offset;

	public bool MovingUp;

	public bool EnrageRoar;

	public int doublePunchCounter;

	public bool setMovement;

	public bool broIsAlive;

	public bool isDeathmode;

	public static Asset<Texture2D> GlowTexture;

	public static int FistDamage = 100;

	public Player Target => Main.player[base.NPC.target];

	public ref float PunchCounter => ref base.NPC.ai[1];

	public ref float DartBurstCounter => ref base.NPC.ai[2];

	public ref float ElapsedVerticalDistance => ref base.NPC.ai[3];

	public ref float AttackDelayTimer => ref base.NPC.localAI[0];

	public ref float BigAttackTimer => ref base.NPC.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 9;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.3f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 36f;
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SupremeCataclysm")
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
		float punchInterpolant = Utils.GetLerpValue(10f, 100f, PunchCounter + (PunchingFromRight ? 0f : 50f), clamped: true);
		if (AttackDelayTimer < 120f)
		{
			base.NPC.frameCounter += 0.15000000596046448;
			if (base.NPC.frameCounter >= 1.0)
			{
				CurrentFrame = (CurrentFrame + 1) % 12;
				base.NPC.frameCounter = 0.0;
			}
		}
		else
		{
			CurrentFrame = (int)Math.Round(MathHelper.Lerp(12f, 21f, punchInterpolant));
		}
		int xFrame = CurrentFrame / Main.npcFrameCount[base.Type];
		int yFrame = CurrentFrame % Main.npcFrameCount[base.Type];
		base.NPC.frame.Width = 212;
		base.NPC.frame.Height = 208;
		base.NPC.frame.X = xFrame * base.NPC.frame.Width;
		base.NPC.frame.Y = yFrame * base.NPC.frame.Height;
	}

	public override void AI()
	{
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0958: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_110f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Unknown result type (might be due to invalid IL or missing references)
		//IL_1117: Unknown result type (might be due to invalid IL or missing references)
		//IL_1130: Unknown result type (might be due to invalid IL or missing references)
		//IL_1135: Unknown result type (might be due to invalid IL or missing references)
		//IL_1149: Unknown result type (might be due to invalid IL or missing references)
		//IL_114e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1150: Unknown result type (might be due to invalid IL or missing references)
		//IL_1161: Unknown result type (might be due to invalid IL or missing references)
		//IL_1166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_1195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1002: Unknown result type (might be due to invalid IL or missing references)
		//IL_1007: Unknown result type (might be due to invalid IL or missing references)
		//IL_1016: Unknown result type (might be due to invalid IL or missing references)
		//IL_1047: Unknown result type (might be due to invalid IL or missing references)
		//IL_104c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1051: Unknown result type (might be due to invalid IL or missing references)
		//IL_1056: Unknown result type (might be due to invalid IL or missing references)
		//IL_1060: Unknown result type (might be due to invalid IL or missing references)
		//IL_106f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1208: Unknown result type (might be due to invalid IL or missing references)
		//IL_120d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1217: Unknown result type (might be due to invalid IL or missing references)
		//IL_1225: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_125f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5c: Unknown result type (might be due to invalid IL or missing references)
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
		CalamityGlobalNPC.SCalCataclysm = base.NPC.whoAmI;
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
		if (CalamityGlobalNPC.SCalCatastrophe != -1 && Main.npc[CalamityGlobalNPC.SCalCatastrophe].active)
		{
			totalLifeRatio += (float)Main.npc[CalamityGlobalNPC.SCalCatastrophe].life / (float)Main.npc[CalamityGlobalNPC.SCalCatastrophe].lifeMax;
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
		if (!broIsAlive && BigAttackTimer > 0f && BigAttackLimit < 29)
		{
			BigAttackLimit = 29;
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
				_ = base.NPC.SafeDirectionTo(Target.Center + new Vector2((float)(HorizontalOffset * ((!targetSide) ? 1 : (-1))), (float)VerticalOffset)) * Utils.Remap(AttackDelayTimer, 60f, 120f, 0f, 50f);
				if (PunchCounter <= 15.000001f)
				{
					if (AttackDelayTimer == 120f && Main.rand.NextBool())
					{
						for (int i = 0; i < 6; i++)
						{
							Dust dust = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(base.NPC.width, base.NPC.height) - base.NPC.velocity * 0.5f, 66, -base.NPC.velocity * Main.rand.NextFloat(0.1f, 0.6f));
							dust.noGravity = true;
							dust.scale = Main.rand.NextFloat(0.7f, 1.3f);
							dust.color = Color.Lerp(Color.Red, Color.Magenta, 0.5f);
						}
					}
					CalamityUtils.SmoothMovement(base.NPC, 0f, Target.Center + new Vector2((float)(-HorizontalOffset * (targetSide ? 1 : (-1))), (float)VerticalOffset) - base.NPC.Center, Utils.Remap(AttackDelayTimer, 10f, 120f, 20f, 80f), 1f, useSimpleFlyMovement: false);
				}
				else
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.85f;
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
				if (PunchCounter <= 15.000001f)
				{
					offset = base.NPC.DirectionTo(Target.Center) * 70f * (float)((!(BigAttackTimer <= 60f)) ? 1 : (-4));
				}
				else
				{
					offset *= 0.9f;
				}
				CalamityUtils.SmoothMovement(base.NPC, 0f, Target.Center + new Vector2((float)(-HorizontalOffset * (targetSide ? 1 : (-1))), (float)VerticalOffset) - base.NPC.Center - offset, 17f, 2f, useSimpleFlyMovement: false);
			}
		}
		else
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.9f;
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
			float fireRate = MathHelper.Lerp(1.5f, 2f, 1f - totalLifeRatio) * (broIsAlive ? 1f : (death ? 1.32f : 1.1f));
			PunchCounter += fireRate;
			if (!(PunchCounter >= 50f))
			{
				return;
			}
			PunchCounter = 0f;
			SoundEngine.PlaySound(in SupremeCalamitas.HellblastSound, base.NPC.Center);
			int type = ModContent.ProjectileType<SupremeCataclysmFist>();
			if (Main.zenithWorld)
			{
				type = ModContent.ProjectileType<SupremeCatastropheSlash>();
			}
			int damage = (Main.zenithWorld ? SupremeCatastrophe.SlashDamage : FistDamage);
			if (Main.netMode != 1)
			{
				Vector2 fistSpawnPosition = base.NPC.Center + Vector2.UnitX * 74f * (float)base.NPC.direction;
				if (Phase2)
				{
					if (doublePunchCounter < 2)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition, base.NPC.DirectionTo(Target.Center) * 15f, type, damage, 0f, Main.myPlayer, 0f, PunchingFromRight.ToInt(), 2f);
						doublePunchCounter++;
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition, base.NPC.DirectionTo(Target.Center).RotatedBy((!broIsAlive) ? 0.48f : 0.55f) * 15f, type, damage, 0f, Main.myPlayer, 0f, PunchingFromRight.ToInt(), 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition, base.NPC.DirectionTo(Target.Center).RotatedBy((!broIsAlive) ? (-0.48f) : (-0.55f)) * 15f, type, damage, 0f, Main.myPlayer, 0f, PunchingFromRight.ToInt(), 2f);
						doublePunchCounter = 0;
					}
				}
				else
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition, base.NPC.DirectionTo(Target.Center) * 15f, type, damage, 0f, Main.myPlayer, 0f, PunchingFromRight.ToInt(), 2f);
				}
			}
			PunchingFromRight = !PunchingFromRight;
			CurrentFrame = 0;
			broIsAlive = NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>());
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
			for (int j = 0; j < 7; j++)
			{
				Vector2 vel = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
				Dust dust2 = Dust.NewDustPerfect(base.NPC.Center + vel * 2f, 279, vel);
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(1.2f, 1.8f);
				dust2.color = Color.Red;
			}
			BigAttackTimer--;
			return;
		}
		float fireRate2 = MathHelper.Lerp(2.5f, 3f, 1f - totalLifeRatio);
		if (!broIsAlive)
		{
			fireRate2 = MathHelper.Lerp(3f, 4f + (float)(29 - BigAttackLimit) * 0.45f, 1f - totalLifeRatio) * 1.2f;
		}
		if (Phase2 && BigAttackLimit == 0)
		{
			fireRate2 = 1f;
		}
		PunchCounter += fireRate2;
		if (!(PunchCounter >= 50f))
		{
			return;
		}
		PunchCounter = 0f;
		int type2 = ModContent.ProjectileType<SupremeCataclysmFist>();
		if (Main.zenithWorld)
		{
			type2 = ModContent.ProjectileType<SupremeCatastropheSlash>();
		}
		int damage2 = (Main.zenithWorld ? SupremeCatastrophe.SlashDamage : FistDamage);
		Vector2 fistSpawnPosition2 = base.NPC.Center + Vector2.UnitX * 74f * (float)base.NPC.direction;
		if ((broIsAlive ? (BigAttackLimit == 1) : (BigAttackLimit <= 3 && BigAttackLimit > 0)) & death)
		{
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition2, (base.NPC.DirectionTo(Target.Center) * (Main.zenithWorld ? 1f : 11f)).RotatedBy(0.6f - (float)BigAttackLimit * 0.16f), type2, damage2, 0f, Main.myPlayer, 0f, 0f, Main.zenithWorld ? 3 : 2);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition2, (base.NPC.DirectionTo(Target.Center) * (Main.zenithWorld ? 1f : 11f)).RotatedBy(-0.6f + (float)BigAttackLimit * 0.16f), type2, damage2, 0f, Main.myPlayer, 0f, 0f, Main.zenithWorld ? 3 : 2);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ThanatosHitOpen1");
			style.Volume = 0.4f;
			style.Pitch = -0.8f - (float)BigAttackLimit * 0.1f;
			SoundEngine.PlaySound(in style, base.NPC.Center);
		}
		else if ((BigAttackLimit == 0) & Phase2)
		{
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fistSpawnPosition2, base.NPC.DirectionTo(Target.Center) * 9.5f, ModContent.ProjectileType<SupremeCataclysmFist>(), FistDamage, 0f, Main.myPlayer, 0f, PunchingFromRight.ToInt(), 3f);
			}
			SoundEngine.PlaySound(SupremeCalamitas.BrimstoneShotSound with
			{
				Volume = 1.8f,
				Pitch = 0.5f
			}, base.NPC.Center);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ScorchedEarthShot", 3);
			style.Volume = 0.65f;
			style.Pitch = -0.75f;
			SoundEngine.PlaySound(in style, base.NPC.Center);
			for (int k = 0; k < 40; k++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(fistSpawnPosition2, (base.NPC.DirectionTo(Target.Center) * 50f).RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.4f, 1.1f), affectedByGravity: false, 120, Main.rand.NextFloat(1.55f, 3.75f), Color.Lerp(Color.Red, Color.Magenta, 0.3f), AddativeBlend: true, needed: true));
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Red, new Vector2(1f, 1f), 0f, 0.045f, 5f, 15));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.025f, 4f, 18));
		}
		else if (Main.netMode != 1)
		{
			SoundStyle style = SupremeCalamitas.BrimstoneShotSound with
			{
				Volume = 1.2f,
				Pitch = 0.4f
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
			Vector2 randPos = (base.NPC.DirectionTo(Target.Center) * 1.5f).RotatedBy(MathHelper.ToRadians(90f)) * Main.rand.NextFloat(-25f, 25f);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + randPos, base.NPC.DirectionTo(Target.Center) * (7.5f - (Phase2 ? ((float)(29 - BigAttackLimit) * 0.13f) : ((float)(8 - BigAttackLimit) * 0.15f))), type2, damage2, 0f, Main.myPlayer, 0f, PunchingFromRight.ToInt(), 1f);
			for (int l = 0; l < 7; l++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.NPC.Center + base.NPC.DirectionTo(Target.Center) * 10f, (base.NPC.DirectionTo(Target.Center) * 30f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.4f, 1.1f), affectedByGravity: false, 50, Main.rand.NextFloat(1.75f, 3.25f), Color.Lerp(Color.Red, Color.Magenta, 0.5f)));
			}
		}
		PunchingFromRight = !PunchingFromRight;
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
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
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
		Color primarycolor = (Main.zenithWorld ? Color.Blue : Color.Red);
		Color baseGlowmaskColor = (base.NPC.IsABestiaryIconDummy ? Color.White : Color.Lerp(Color.White, primarycolor, 0.5f));
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
		npcLoot.Add(ModContent.ItemType<SupremeCataclysmTrophy>(), 10);
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
					dust.color = Color.DeepSkyBlue;
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Cyan, new Vector2(1f, 1f), 0f, 0.1f, 5f, 25));
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Lerp(Color.Cyan, Color.DodgerBlue, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 28));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerLimbLoss3");
				style.Volume = 0.9f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.NPC.Center);
				CalamityUtils.SpawnBossBetter(base.NPC.Center, ModContent.NPCType<SupremeCatastrophe>(), null, (!MovingUp) ? 1 : (-1));
				scal.ModNPC<SupremeCalamitas>().respawnBro = false;
			}
		}
		DeathAshParticle.CreateAshesFromNPC(base.NPC, Vector2.Zero);
	}

	public SupremeCataclysm()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		HorizontalOffset = 750;
		BigAttackLimit = 7;
		offset = Vector2.Zero;
		MovingUp = true;
		EnrageRoar = true;
		setMovement = true;
		broIsAlive = true;
		isDeathmode = CalamityWorld.death || BossRushEvent.BossRushActive;
		base._002Ector();
	}
}
