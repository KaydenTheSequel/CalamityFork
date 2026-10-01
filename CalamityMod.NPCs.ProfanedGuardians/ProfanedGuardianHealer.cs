using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Tools;
using CalamityMod.NPCs.Providence;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Utilities;
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

namespace CalamityMod.NPCs.ProfanedGuardians;

[AutoloadBossHead]
public class ProfanedGuardianHealer : ModNPC
{
	private enum Phase
	{
		CrystalShards,
		Stars
	}

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> Texture_Glow2;

	public static Asset<Texture2D> TextureNight_Glow;

	public static int CrystalDamage = 48;

	public static int StarDamage = 54;

	public static int StarHeal = (Main.expertMode ? 50 : 35);

	private float AIState
	{
		get
		{
			return base.NPC.localAI[0];
		}
		set
		{
			base.NPC.localAI[0] = value;
		}
	}

	private float AITimer
	{
		get
		{
			return base.NPC.ai[3];
		}
		set
		{
			base.NPC.ai[3] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 10;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.Scale = 0.75f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 25f;
		value.Position.Y += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			Texture_Glow2 = ModContent.Request<Texture2D>(Texture + "Glow2", (AssetRequestMode)2);
			TextureNight_Glow = ModContent.Request<Texture2D>(Texture + "GlowNight", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 0;
		base.NPC.npcSlots = 3f;
		base.NPC.aiStyle = -1;
		base.NPC.width = 228;
		base.NPC.height = 164;
		base.NPC.defense = 30;
		base.NPC.DR_NERD(0.2f);
		base.NPC.LifeMaxNERB(48000, 72000, 50000);
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.AIType = -1;
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<ProfanedGuardianCommander>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ProfanedGuardianHealer")
		});
	}

	public float GetStarShootSlowDownGateValue()
	{
		if (!CalamityWorld.death)
		{
			if (!CalamityWorld.revenge)
			{
				if (!Main.expertMode)
				{
					return 270f;
				}
				return 240f;
			}
			return 225f;
		}
		return 210f;
	}

	public float GetStarShootGateValue()
	{
		return GetStarShootSlowDownGateValue() + 60f;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.12f + ((Vector2)(ref base.NPC.velocity)).Length() / 120f;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.doughnutBossHealer = base.NPC.whoAmI;
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1.1f, 0.9f, 0f);
		if (CalamityGlobalNPC.doughnutBoss < 0 || !Main.npc[CalamityGlobalNPC.doughnutBoss].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		Vector2 dustAndProjectileOffset = default(Vector2);
		((Vector2)(ref dustAndProjectileOffset))._002Ector(40f * (float)base.NPC.direction, 20f);
		Vector2 shootFrom = base.NPC.Center + dustAndProjectileOffset;
		base.NPC.rotation = base.NPC.velocity.X * 0.005f;
		if (Main.npc[CalamityGlobalNPC.doughnutBoss].ai[3] == -1f)
		{
			base.NPC.velocity = Main.npc[CalamityGlobalNPC.doughnutBoss].velocity;
			return;
		}
		Player player = Main.player[Main.npc[CalamityGlobalNPC.doughnutBoss].target];
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (Main.zenithWorld)
		{
			base.NPC.ai[0]++;
		}
		if (base.NPC.ai[0] >= 300f)
		{
			base.NPC.ai[1] = 1f;
		}
		if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
		{
			float playerLocation = base.NPC.Center.X - player.Center.X;
			base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
		}
		if (base.NPC.ai[1] == 1f && Main.zenithWorld)
		{
			base.NPC.ai[2]++;
			base.NPC.velocity = Vector2.Zero;
			if (base.NPC.ai[2] >= 45f)
			{
				int type = ModContent.ProjectileType<HolyBurnOrb>();
				int totalProjectiles = 10;
				float radians = (float)Math.PI * 2f / (float)totalProjectiles;
				float projectileVelocity = 8f;
				Vector2 spinningPoint = default(Vector2);
				((Vector2)(ref spinningPoint))._002Ector(0f, 0f - projectileVelocity);
				for (int k = 0; k < totalProjectiles; k++)
				{
					Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, velocity2, type, 0, 0f, Main.myPlayer, 0f, StarHeal);
				}
				base.NPC.ai[2] = 0f;
			}
			if (base.NPC.ai[0] >= 600f)
			{
				SoundEngine.PlaySound(in SoundID.Item10, player.Center);
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[0] = 0f;
			}
			return;
		}
		bool useCrystalShards = AIState == 0f;
		float velocity3 = ((!useCrystalShards) ? (((Vector2)(ref Main.npc[CalamityGlobalNPC.doughnutBoss].velocity)).Length() + 5f) : (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f))));
		if (Main.getGoodWorld)
		{
			velocity3 *= 1.25f;
		}
		float idealDistanceFromDestination = (useCrystalShards ? 80f : 160f);
		Vector2 distanceFromDestination = player.Center + (useCrystalShards ? Vector2.Zero : (Vector2.UnitX * player.velocity.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)).X * 400f)) + Vector2.UnitY * -400f - base.NPC.Center;
		Vector2 desiredVelocity = distanceFromDestination.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * velocity3;
		bool num = Main.npc[CalamityGlobalNPC.doughnutBoss].ai[0] == 5f;
		if (num)
		{
			base.NPC.Calamity().DR = 0.9f;
			base.NPC.Calamity().unbreakableDR = true;
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = true;
		}
		else
		{
			base.NPC.Calamity().DR = 0.2f;
			base.NPC.Calamity().unbreakableDR = false;
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = false;
		}
		if (num)
		{
			AITimer = 0f;
			distanceFromDestination = Main.npc[CalamityGlobalNPC.doughnutBoss].Center - base.NPC.Center;
			desiredVelocity = distanceFromDestination.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * (((Vector2)(ref Main.npc[CalamityGlobalNPC.doughnutBoss].velocity)).Length() + 5f);
			if (((Vector2)(ref distanceFromDestination)).Length() > 40f)
			{
				float inertia = 10f;
				if (Main.getGoodWorld)
				{
					inertia *= 0.8f;
				}
				base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + desiredVelocity) / inertia;
			}
			else
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
			}
			return;
		}
		if (useCrystalShards)
		{
			AITimer++;
			float crystalShootGateValue = (death ? 180f : (revenge ? 200f : (expertMode ? 220f : 260f)));
			float crystalShootPhaseDuration = crystalShootGateValue + crystalShootGateValue * 0.25f;
			float dustGateValue = crystalShootGateValue * 0.5f;
			if (AITimer >= dustGateValue && AITimer < crystalShootGateValue)
			{
				int dustChance = (int)((crystalShootGateValue - AITimer) * 0.25f);
				if (dustChance < 2)
				{
					dustChance = 2;
				}
				int maxDust = 10;
				for (int i = 0; i < maxDust; i++)
				{
					if (Main.rand.NextBool(dustChance))
					{
						Color val = new Color(250, 150, 0);
						float brightness = 0.8f;
						Color dustColor = Color.Lerp(val, Color.White, brightness);
						Dust obj = Main.dust[Dust.NewDust(base.NPC.Top, 0, 0, 267, 0f, 0f, 100, dustColor)];
						obj.velocity.X = 0f;
						obj.noGravity = true;
						obj.fadeIn = 1f;
						obj.position = shootFrom + Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (4f * Main.rand.NextFloat() + 26f);
						obj.scale = 0.8f;
					}
				}
			}
			if (AITimer == crystalShootGateValue)
			{
				SoundEngine.PlaySound(in SoundID.Item109, shootFrom);
				if (Main.netMode != 1)
				{
					int type2 = ModContent.ProjectileType<ProvidenceCrystalShard>();
					int totalProjectiles2 = (death ? 16 : (revenge ? 14 : (expertMode ? 12 : 10)));
					float speedX = -12f;
					float speedAdjustment = Math.Abs(speedX * 2f / (float)(totalProjectiles2 - 1));
					float speedY = -4f;
					float randomVelocityMult = (death ? 2f : 1f);
					for (int j = 0; j < totalProjectiles2; j++)
					{
						float x4 = Main.rgbToHsl(new Color(255, 200, Main.DiscoB)).X;
						Vector2 randomizedVelocity = Vector2.Zero;
						if (revenge)
						{
							float randomFloatX = Main.rand.NextFloat() - 0.5f;
							float randomFloatY = Main.rand.NextFloat() - 0.5f;
							randomizedVelocity = (revenge ? (new Vector2(randomFloatX, randomFloatY) * randomVelocityMult) : Vector2.Zero);
						}
						Vector2 projectileVelocity2 = new Vector2(speedX + speedAdjustment * (float)j + distanceFromDestination.SafeNormalize(Vector2.Zero).X * Math.Abs(player.velocity.X), speedY) + randomizedVelocity;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, projectileVelocity2, type2, CrystalDamage, 0f, Main.myPlayer, x4);
					}
				}
			}
			if (AITimer >= crystalShootPhaseDuration)
			{
				AITimer = 0f;
				AIState = 1f;
			}
		}
		else
		{
			AITimer++;
			float starShootPhaseDuration = GetStarShootGateValue() + 60f;
			if (AITimer == GetStarShootGateValue())
			{
				SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, shootFrom);
				int totalFlameProjectiles = 16;
				int totalRings = (revenge ? 3 : 2);
				int healingStarChance = (revenge ? 8 : (expertMode ? 6 : 4));
				double radians2 = (float)Math.PI * 2f / (float)totalFlameProjectiles;
				double angleA = radians2 * 0.5;
				double angleB = (double)MathHelper.ToRadians(90f) - angleA;
				for (int l = 0; l < totalRings; l++)
				{
					bool num2 = l % 2 == 0;
					float starVelocity = (float)l + 2f;
					float velocityX = (float)((double)starVelocity * Math.Sin(angleA) / Math.Sin(angleB));
					Vector2 spinningPoint2 = (num2 ? new Vector2(0f - velocityX, 0f - starVelocity) : new Vector2(0f, 0f - starVelocity));
					for (int m = 0; m < totalFlameProjectiles; m++)
					{
						Vector2 vector2 = spinningPoint2.RotatedBy(radians2 * (double)m);
						int type3 = ModContent.ProjectileType<HolyBurnOrb>();
						if (Main.rand.NextBool(healingStarChance) && !death)
						{
							type3 = ModContent.ProjectileType<HolyLight>();
							if (Main.netMode != 1)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, vector2, type3, 0, 0f, Main.myPlayer, 0f, StarHeal);
							}
						}
						else if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, vector2, type3, StarDamage, 0f, Main.myPlayer);
						}
						Color dustColor2 = Main.hslToRgb(Main.rgbToHsl((type3 == ModContent.ProjectileType<HolyBurnOrb>()) ? Color.Orange : Color.Green).X, 1f, 0.5f);
						((Color)(ref dustColor2)).A = byte.MaxValue;
						int maxDust2 = 3;
						for (int n = 0; n < maxDust2; n++)
						{
							int dust = Dust.NewDust(shootFrom, 0, 0, 267, 0f, 0f, 0, dustColor2);
							Main.dust[dust].position = shootFrom;
							Main.dust[dust].velocity = vector2 * starVelocity * ((float)n * 0.5f + 1f);
							Main.dust[dust].noGravity = true;
							Main.dust[dust].scale = 1f + (float)n;
							Main.dust[dust].fadeIn = Main.rand.NextFloat() * 2f;
							Dust dust2 = DustExtensions.BetterCloneDust(dust);
							dust2.scale /= 2f;
							dust2.fadeIn /= 2f;
							dust2.color = new Color(255, 255, 255, 255);
						}
					}
				}
			}
			if (AITimer >= starShootPhaseDuration)
			{
				AITimer = 0f;
				AIState = 0f;
			}
		}
		if (((Vector2)(ref distanceFromDestination)).Length() > idealDistanceFromDestination)
		{
			float inertia2 = (death ? 32f : (revenge ? 34f : (expertMode ? 36f : 40f)));
			if (lifeRatio < 0.5f)
			{
				inertia2 *= 0.8f;
			}
			if (Main.getGoodWorld)
			{
				inertia2 *= 0.8f;
			}
			base.NPC.velocity = (base.NPC.velocity * (inertia2 - 1f) + desiredVelocity) / inertia2;
		}
		else
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.98f;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		float useLaserGateValue = 120f;
		float num = ((CalamityWorld.revenge || BossRushEvent.BossRushActive) ? 235f : 315f);
		float maxIntensity = 45f;
		float increaseIntensityGateValue = useLaserGateValue - maxIntensity;
		float decreaseIntensityGateValue = num - maxIntensity;
		if (!base.NPC.IsABestiaryIconDummy)
		{
			if (Main.npc[CalamityGlobalNPC.doughnutBoss].ai[0] == 5f)
			{
				_ = Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1];
				float burnIntensity = ((Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1] > decreaseIntensityGateValue) ? Utils.GetLerpValue(0f, maxIntensity, maxIntensity - (Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1] - decreaseIntensityGateValue), clamped: true) : Utils.GetLerpValue(0f, maxIntensity, Main.npc[CalamityGlobalNPC.doughnutBoss].ai[1], clamped: true));
				int totalGuardiansToDraw = (int)MathHelper.Lerp(1f, 30f, burnIntensity);
				for (int i = 0; i < totalGuardiansToDraw; i++)
				{
					float num2 = (float)Math.PI * 2f * (float)i * 2f / (float)totalGuardiansToDraw;
					float drawOffsetFactor = (float)Math.Sin(num2 * 6f + Main.GlobalTimeWrappedHourly * (float)Math.PI);
					drawOffsetFactor *= (float)Math.Pow(burnIntensity, 3.0) * 50f;
					Vector2 drawOffset = num2.ToRotationVector2() * drawOffsetFactor;
					Color baseColor = Color.White * (MathHelper.Lerp(0.4f, 0.8f, burnIntensity) / (float)totalGuardiansToDraw * 1.5f);
					((Color)(ref baseColor)).A = 0;
					baseColor = Color.Lerp(Color.White, baseColor, burnIntensity);
					drawGuardianInstance(drawOffset, (totalGuardiansToDraw == 1) ? ((Color?)null) : new Color?(baseColor));
				}
			}
			else
			{
				drawGuardianInstance(Vector2.Zero, null);
			}
		}
		else
		{
			drawGuardianInstance(Vector2.Zero, null);
		}
		return false;
		void drawGuardianInstance(Vector2 val, Color? colorOverride)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Unknown result type (might be due to invalid IL or missing references)
			//IL_020a: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0229: Unknown result type (might be due to invalid IL or missing references)
			//IL_0239: Unknown result type (might be due to invalid IL or missing references)
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_024d: Unknown result type (might be due to invalid IL or missing references)
			//IL_024f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0251: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0272: Unknown result type (might be due to invalid IL or missing references)
			//IL_0277: Unknown result type (might be due to invalid IL or missing references)
			//IL_027c: Unknown result type (might be due to invalid IL or missing references)
			//IL_027d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0282: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_0298: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0304: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Unknown result type (might be due to invalid IL or missing references)
			//IL_0333: Unknown result type (might be due to invalid IL or missing references)
			//IL_0338: Unknown result type (might be due to invalid IL or missing references)
			//IL_031d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0322: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Unknown result type (might be due to invalid IL or missing references)
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_019b: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01af: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_0345: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Unknown result type (might be due to invalid IL or missing references)
			//IL_034e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0353: Unknown result type (might be due to invalid IL or missing references)
			//IL_0574: Unknown result type (might be due to invalid IL or missing references)
			//IL_056d: Unknown result type (might be due to invalid IL or missing references)
			//IL_057e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0585: Unknown result type (might be due to invalid IL or missing references)
			//IL_058a: Unknown result type (might be due to invalid IL or missing references)
			//IL_059c: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_05df: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_060a: Unknown result type (might be due to invalid IL or missing references)
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_036e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0370: Unknown result type (might be due to invalid IL or missing references)
			//IL_0372: Unknown result type (might be due to invalid IL or missing references)
			//IL_037c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0381: Unknown result type (might be due to invalid IL or missing references)
			//IL_0389: Unknown result type (might be due to invalid IL or missing references)
			//IL_038b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Unknown result type (might be due to invalid IL or missing references)
			//IL_0392: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0402: Unknown result type (might be due to invalid IL or missing references)
			//IL_0404: Unknown result type (might be due to invalid IL or missing references)
			//IL_0421: Unknown result type (might be due to invalid IL or missing references)
			//IL_0431: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0440: Unknown result type (might be due to invalid IL or missing references)
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_0447: Unknown result type (might be due to invalid IL or missing references)
			//IL_0449: Unknown result type (might be due to invalid IL or missing references)
			//IL_0455: Unknown result type (might be due to invalid IL or missing references)
			//IL_046a: Unknown result type (might be due to invalid IL or missing references)
			//IL_046f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0474: Unknown result type (might be due to invalid IL or missing references)
			//IL_0475: Unknown result type (might be due to invalid IL or missing references)
			//IL_047a: Unknown result type (might be due to invalid IL or missing references)
			//IL_047f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0488: Unknown result type (might be due to invalid IL or missing references)
			//IL_0490: Unknown result type (might be due to invalid IL or missing references)
			//IL_049a: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04be: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04db: Unknown result type (might be due to invalid IL or missing references)
			//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0512: Unknown result type (might be due to invalid IL or missing references)
			//IL_051a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0524: Unknown result type (might be due to invalid IL or missing references)
			//IL_0531: Unknown result type (might be due to invalid IL or missing references)
			//IL_053d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0504: Unknown result type (might be due to invalid IL or missing references)
			//IL_0509: Unknown result type (might be due to invalid IL or missing references)
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
			Texture2D texture2D16 = Texture_Glow2.Value;
			Vector2 halfSizeTexture = default(Vector2);
			((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
			int afterimageAmt = 5;
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int j = 1; j < afterimageAmt; j += 2)
				{
					Color afterimageColor = drawColor;
					afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
					afterimageColor = base.NPC.GetAlpha(afterimageColor);
					afterimageColor *= (float)(afterimageAmt - j) / 15f;
					if (colorOverride.HasValue)
					{
						afterimageColor = colorOverride.Value;
					}
					Vector2 afterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			Vector2 drawLocation = base.NPC.Center - screenPos;
			drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
			drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, (Color)(((_003F?)colorOverride) ?? base.NPC.GetAlpha(drawColor)), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			texture2D15 = Texture_Glow.Value;
			Color timeBasedDrawColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
			if (Main.zenithWorld)
			{
				texture2D15 = TextureNight_Glow.Value;
				timeBasedDrawColor = Main.DiscoColor;
			}
			Color overrideColor = Color.Lerp(Color.White, Color.Violet, 0.5f);
			if (colorOverride.HasValue)
			{
				timeBasedDrawColor = colorOverride.Value;
				overrideColor = colorOverride.Value;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int k = 1; k < afterimageAmt; k++)
				{
					Color timeBasedAfterimageColor = timeBasedDrawColor;
					timeBasedAfterimageColor = Color.Lerp(timeBasedAfterimageColor, Color.White, 0.5f);
					timeBasedAfterimageColor = base.NPC.GetAlpha(timeBasedAfterimageColor);
					timeBasedAfterimageColor *= (float)(afterimageAmt - k) / 15f;
					if (colorOverride.HasValue)
					{
						timeBasedAfterimageColor = colorOverride.Value;
					}
					Vector2 timeBasedAfterimagePos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					timeBasedAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					timeBasedAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture2D15, timeBasedAfterimagePos, (Rectangle?)base.NPC.frame, timeBasedAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
					Color alphaOverrideColor = overrideColor;
					alphaOverrideColor = Color.Lerp(alphaOverrideColor, Color.White, 0.5f);
					alphaOverrideColor = base.NPC.GetAlpha(alphaOverrideColor);
					alphaOverrideColor *= (float)(afterimageAmt - k) / 15f;
					if (colorOverride.HasValue)
					{
						alphaOverrideColor = colorOverride.Value;
					}
					spriteBatch.Draw(texture2D16, timeBasedAfterimagePos, (Rectangle?)base.NPC.frame, alphaOverrideColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			base.NPC.DrawBackglow((Color)(Main.zenithWorld ? Main.DiscoColor : new Color(255, 64, 0, 0)), 4f, spriteEffects, base.NPC.frame, Main.screenPosition, texture2D15);
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, ProvUtils.GetColorBasedOnEnrage(Night: false, 0), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			spriteBatch.Draw(texture2D16, drawLocation, (Rectangle?)base.NPC.frame, overrideColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<RelicOfConvergence>(), 4);
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		cooldownSlot = 1;
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		return minDist <= 80f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossH").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossH2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossH3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossH4").Type);
			}
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
		}
	}
}
