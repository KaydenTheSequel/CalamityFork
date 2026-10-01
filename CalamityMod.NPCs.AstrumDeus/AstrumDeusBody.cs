using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AstrumDeus;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(AstrumDeusHead))]
public class AstrumDeusBody : ModNPC
{
	public static Asset<Texture2D> AltTexture;

	public static Asset<Texture2D> TextureGlow1;

	public static Asset<Texture2D> TextureGlow2;

	public static Asset<Texture2D> TextureGlow3;

	public static Asset<Texture2D> TextureGlow4;

	public static Asset<Texture2D> TextureFlash;

	public static Asset<Texture2D> TextureFlash2;

	public static Asset<Texture2D> TextureAltGlow1;

	public static Asset<Texture2D> TextureAltGlow2;

	public static Asset<Texture2D> TextureAltFlash;

	public static int LaserDamage = 30;

	public static int HelixLaserDamage = 40;

	public static int MineDamage = 40;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.AstrumDeusHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			AltTexture = ModContent.Request<Texture2D>(Texture + "AltSpectral", (AssetRequestMode)2);
			TextureGlow1 = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			TextureGlow2 = ModContent.Request<Texture2D>(Texture + "Glow2", (AssetRequestMode)2);
			TextureGlow3 = ModContent.Request<Texture2D>(Texture + "Glow3", (AssetRequestMode)2);
			TextureGlow4 = ModContent.Request<Texture2D>(Texture + "Glow4", (AssetRequestMode)2);
			TextureFlash = ModContent.Request<Texture2D>(Texture + "GlowFlash", (AssetRequestMode)2);
			TextureFlash2 = ModContent.Request<Texture2D>(Texture + "GlowFlash2", (AssetRequestMode)2);
			TextureAltGlow1 = ModContent.Request<Texture2D>(Texture + "AltGlow", (AssetRequestMode)2);
			TextureAltGlow2 = ModContent.Request<Texture2D>(Texture + "AltGlow2", (AssetRequestMode)2);
			TextureAltFlash = ModContent.Request<Texture2D>(Texture + "AltGlowFlash", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 70;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 38;
		base.NPC.height = 44;
		base.NPC.defense = 35;
		base.NPC.DR_NERD(0.25f);
		base.NPC.LifeMaxNERB(200000, 240000, 650000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.4f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.35f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.2f;
		}
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = AstrumDeusHead.HitSound;
		base.NPC.DeathSound = AstrumDeusHead.DeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.dontCountMe = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[3]);
		writer.Write(base.NPC.dontTakeDamage);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[3] = reader.ReadSingle();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (calamityGlobalNPC.newAI[1] < 180f || base.NPC.dontTakeDamage)
		{
			base.NPC.damage = 0;
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		Vector2.Distance(player.Center, base.NPC.Center);
		Vector2.Distance(player.Center, base.NPC.Center);
		if (revenge && !Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
		{
			Main.LocalPlayer.AddBuff(ModContent.BuffType<DoGExtremeGravity>(), 2);
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool doubleWormPhase = calamityGlobalNPC.newAI[0] != 0f;
		bool startFlightPhase = (lifeRatio < 0.8f) | death | doubleWormPhase;
		bool phase2 = (lifeRatio < 0.5f) & doubleWormPhase & expertMode;
		bool splittingMines = lifeRatio < 0.7f;
		bool movingMines = (lifeRatio < 0.3f) & doubleWormPhase & expertMode;
		bool deathModeEnragePhase_Head = calamityGlobalNPC.newAI[0] == 3f;
		bool deathModeEnragePhase_BodyAndTail = false;
		float resistanceTime = (doubleWormPhase ? 300f : 600f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = calamityGlobalNPC.newAI[1] < resistanceTime;
		float aiSwitchTimer = ((!doubleWormPhase) ? (Main.getGoodWorld ? 900f : 1800f) : (Main.getGoodWorld ? 600f : 1200f));
		calamityGlobalNPC.newAI[3]++;
		if (calamityGlobalNPC.newAI[3] >= aiSwitchTimer)
		{
			calamityGlobalNPC.newAI[3] = 0f;
		}
		if (doubleWormPhase && calamityGlobalNPC.newAI[3] % aiSwitchTimer == 0f && !(deathModeEnragePhase_Head | deathModeEnragePhase_BodyAndTail))
		{
			SoundStyle style = AstrumDeusHead.SplitSound with
			{
				Pitch = -0.2f,
				Volume = 0.9f
			};
			SoundEngine.PlaySound(in style, player.Center);
		}
		bool flyAtTarget = (calamityGlobalNPC.newAI[3] >= aiSwitchTimer * 0.5f) & startFlightPhase;
		int phase1Length = (death ? 80 : (revenge ? 70 : (expertMode ? 60 : 50)));
		int phase2Length = (death ? 40 : (revenge ? 35 : (expertMode ? 30 : 25)));
		int gfbLength = (death ? 8 : (revenge ? 7 : (expertMode ? 6 : 5)));
		if (!(Main.zenithWorld & doubleWormPhase))
		{
		}
		int gfbMaxWormCount = 10;
		int gfbWormCount = 0;
		if (Main.zenithWorld)
		{
			gfbWormCount = NPC.CountNPCS(ModContent.NPCType<AstrumDeusHead>());
		}
		if (gfbWormCount > gfbMaxWormCount)
		{
			gfbWormCount = gfbMaxWormCount;
		}
		base.NPC.dontTakeDamage = Main.npc[(int)base.NPC.ai[2]].dontTakeDamage;
		base.NPC.Opacity = Main.npc[(int)base.NPC.ai[2]].Opacity;
		deathModeEnragePhase_BodyAndTail = Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] == 3f;
		if (deathModeEnragePhase_BodyAndTail)
		{
			base.NPC.defense = 25;
			calamityGlobalNPC.DR = 0.15f;
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128 && !base.NPC.dontTakeDamage)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		bool shouldDespawn = true;
		int headType = ModContent.NPCType<AstrumDeusHead>();
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			if (Main.npc[i].type == headType && Main.npc[i].active)
			{
				shouldDespawn = false;
				break;
			}
		}
		if (shouldDespawn && Main.npc.IndexInRange((int)base.NPC.ai[1]) && Main.npc[(int)base.NPC.ai[1]].active && Main.npc[(int)base.NPC.ai[1]].life > 0)
		{
			shouldDespawn = false;
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		float segmentVelocity = ((calamityGlobalNPC.newAI[1] < resistanceTime * 0.4f && !doubleWormPhase) ? 25f : (deathModeEnragePhase_Head ? 19f : (death ? 17.5f : 16f)));
		float segmentVelocityBoost = 5f * (1f - lifeRatio);
		segmentVelocity += segmentVelocityBoost;
		if (gfbWormCount > 0)
		{
			segmentVelocity += (float)(gfbMaxWormCount - gfbWormCount) * 0.444f;
		}
		if (revenge)
		{
			float revMultiplier = 1.1f;
			segmentVelocity *= revMultiplier;
		}
		base.NPC.localAI[0]++;
		_ = doubleWormPhase & expertMode;
		float shootProjectile = ((doubleWormPhase & expertMode) ? 200 : 400);
		float divisor = base.NPC.ai[0] + 15f + shootProjectile;
		bool num = base.NPC.Opacity >= 1f;
		bool shootGodRays = phase2 | deathModeEnragePhase_BodyAndTail;
		Vector2 center;
		if (num)
		{
			if (!flyAtTarget | deathModeEnragePhase_BodyAndTail)
			{
				float laserDivisor = ((phase2 && !deathModeEnragePhase_BodyAndTail) ? 4f : 2f);
				if (base.NPC.localAI[0] % divisor == 0f && base.NPC.ai[0] % laserDivisor == 0f)
				{
					base.NPC.TargetClosest();
					if (Main.netMode != 1 && deathModeEnragePhase_BodyAndTail)
					{
						Vector2 velocity = Vector2.Zero;
						if (movingMines)
						{
							Vector2 randomMineMovement = default(Vector2);
							((Vector2)(ref randomMineMovement))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							((Vector2)(ref randomMineMovement)).Normalize();
							randomMineMovement *= (float)Main.rand.Next(90, 121) * 0.01f;
							velocity = randomMineMovement;
						}
						int type = ModContent.ProjectileType<DeusMine>();
						float split = ((splittingMines && base.NPC.ai[0] % 3f == 0f) ? 1f : 0f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, type, MineDamage, 0f, Main.myPlayer, split);
					}
					if (Vector2.Distance(player.Center, base.NPC.Center) > 80f && Main.netMode != 1)
					{
						float deusLaserSpeed = (death ? 16f : (revenge ? 14f : 13f));
						if (gfbWormCount > 0)
						{
							deusLaserSpeed += (float)(gfbMaxWormCount - gfbWormCount) * 0.444f;
						}
						Vector2 deusLaserCenter = base.NPC.Center;
						float deusLaserTargetX = player.Center.X - deusLaserCenter.X;
						float deusLaserTargetY = player.Center.Y - deusLaserCenter.Y;
						float deusLaserTargetDist = (float)Math.Sqrt(deusLaserTargetX * deusLaserTargetX + deusLaserTargetY * deusLaserTargetY);
						deusLaserTargetDist = deusLaserSpeed / deusLaserTargetDist;
						deusLaserTargetX *= deusLaserTargetDist;
						deusLaserTargetY *= deusLaserTargetDist;
						deusLaserCenter.X += deusLaserTargetX * 5f;
						deusLaserCenter.Y += deusLaserTargetY * 5f;
						Vector2 shootDirection = Utils.SafeNormalize(new Vector2(deusLaserTargetX, deusLaserTargetY), Vector2.UnitY);
						Vector2 laserVelocity = shootDirection * deusLaserSpeed;
						int type2 = (shootGodRays ? ModContent.ProjectileType<AstralGodRay>() : ModContent.ProjectileType<AstralShot2>());
						int damage = (shootGodRays ? HelixLaserDamage : LaserDamage);
						if (shootGodRays)
						{
							SoundEngine.PlaySound(in AstrumDeusHead.GodRaySound, base.NPC.Center);
							float waveSideOffset = Main.rand.NextFloat(9f, 14f);
							center = default(Vector2);
							Vector2 perp = shootDirection.RotatedBy(-1.5707963705062866, center) * waveSideOffset;
							for (int j = -1; j <= 1; j += 2)
							{
								Vector2 laserStartPos = deusLaserCenter + (float)j * perp + Main.rand.NextVector2CircularEdge(6f, 6f);
								Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), laserStartPos, laserVelocity, type2, damage, 0f, Main.myPlayer, player.Center.X, player.Center.Y).localAI[1] = (float)j * 0.5f;
							}
						}
						else
						{
							SoundEngine.PlaySound(in AstrumDeusHead.LaserSound, base.NPC.Center);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), deusLaserCenter, laserVelocity, type2, damage, 0f, Main.myPlayer, player.Center.X, player.Center.Y);
						}
					}
				}
			}
			else if (base.NPC.localAI[0] % divisor == 0f && base.NPC.ai[0] % 2f == 0f)
			{
				Vector2 velocity2 = Vector2.Zero;
				if (movingMines)
				{
					Vector2 randomMineMovement2 = default(Vector2);
					((Vector2)(ref randomMineMovement2))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					((Vector2)(ref randomMineMovement2)).Normalize();
					randomMineMovement2 *= (float)Main.rand.Next(30, 121) * 0.01f;
					velocity2 = randomMineMovement2;
				}
				int type3 = ModContent.ProjectileType<DeusMine>();
				float split2 = ((splittingMines && base.NPC.ai[0] % 3f == 0f) ? 1f : 0f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, type3, MineDamage, 0f, Main.myPlayer, split2);
			}
		}
		Vector2 segmentCenter = base.NPC.Center;
		float segmentTargetX = player.Center.X;
		float segmentTargetY = player.Center.Y;
		segmentTargetX = (int)(segmentTargetX / 16f) * 16;
		segmentTargetY = (int)(segmentTargetY / 16f) * 16;
		segmentCenter.X = (int)(segmentCenter.X / 16f) * 16;
		segmentCenter.Y = (int)(segmentCenter.Y / 16f) * 16;
		segmentTargetX -= segmentCenter.X;
		segmentTargetY -= segmentCenter.Y;
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentCenter = base.NPC.Center;
				segmentTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - segmentCenter.X;
				segmentTargetY = Main.npc[(int)base.NPC.ai[1]].Center.Y - segmentCenter.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(segmentTargetY, segmentTargetX) + (float)Math.PI / 2f;
			float segmentTargetDist = (float)Math.Sqrt(segmentTargetX * segmentTargetX + segmentTargetY * segmentTargetY);
			int segmentWidth = base.NPC.width;
			segmentTargetDist = (segmentTargetDist - (float)segmentWidth) / segmentTargetDist;
			segmentTargetX *= segmentTargetDist;
			segmentTargetY *= segmentTargetDist;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + segmentTargetX;
			base.NPC.position.Y = base.NPC.position.Y + segmentTargetY;
			if (segmentTargetX < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (segmentTargetX > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
		if (calamityGlobalNPC.newAI[1] == 0f && !doubleWormPhase)
		{
			SoundEngine.PlaySound(in AstrumDeusHead.SpawnSound, base.NPC.Center);
			calamityGlobalNPC.newAI[1] = 1f;
		}
		if (calamityGlobalNPC.newAI[1] < resistanceTime)
		{
			center = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref center)).Length() > 2f || calamityGlobalNPC.newAI[1] > 1f)
			{
				calamityGlobalNPC.newAI[1]++;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		bool altBodyTextures = base.NPC.localAI[3] == 1f;
		bool deathModeEnragePhase = Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] == 3f;
		bool doubleWormPhase = base.NPC.Calamity().newAI[0] != 0f && !deathModeEnragePhase;
		int segmentAmt = ((!Main.zenithWorld) ? (CalamityWorld.death ? 40 : (CalamityWorld.revenge ? 35 : (Main.expertMode ? 30 : 25))) : (CalamityWorld.death ? 8 : (CalamityWorld.revenge ? 7 : (Main.expertMode ? 6 : 5))));
		float cyanThreshold = (Main.getGoodWorld ? 300f : 600f);
		float transitionStart = MathHelper.Lerp(cyanThreshold * 0.75f, cyanThreshold * 0.95f, 1f - base.NPC.ai[3] / (float)segmentAmt);
		float transitionEnd = MathHelper.Lerp(cyanThreshold * 0.8f, cyanThreshold, 1f - base.NPC.ai[3] / (float)segmentAmt);
		bool drawCyan = base.NPC.Calamity().newAI[3] >= transitionEnd && base.NPC.Calamity().newAI[3] <= cyanThreshold + transitionEnd;
		bool inColorTrans = doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold >= transitionStart && base.NPC.Calamity().newAI[3] % cyanThreshold <= transitionEnd;
		Texture2D mainWormTex = (altBodyTextures ? AltTexture.Value : TextureAssets.Npc[base.Type].Value);
		Texture2D secondWormTex = TextureGlow2.Value;
		Texture2D mainFlashTex = (altBodyTextures ? TextureAltFlash.Value : TextureFlash.Value);
		Vector2 halfSizeTex = default(Vector2);
		((Vector2)(ref halfSizeTex))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)mainWormTex.Width, (float)mainWormTex.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTex * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(mainWormTex, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
		mainWormTex = (altBodyTextures ? TextureAltGlow1.Value : TextureGlow1.Value);
		float colorOpacity = Utils.GetLerpValue(transitionStart, transitionEnd, base.NPC.Calamity().newAI[3] % cyanThreshold, clamped: true);
		Color phaseColor = (drawCyan ? Color.Cyan : Color.Orange);
		Color otherPhaseColor = (drawCyan ? Color.Orange : Color.Cyan);
		Texture2D otherMainTex;
		Texture2D otherSecondTex;
		if (doubleWormPhase)
		{
			mainWormTex = (drawCyan ? mainWormTex : (altBodyTextures ? TextureAltGlow2.Value : TextureGlow3.Value));
			otherMainTex = ((!drawCyan) ? mainWormTex : (altBodyTextures ? TextureAltGlow2.Value : TextureGlow3.Value));
			secondWormTex = (drawCyan ? TextureGlow4.Value : secondWormTex);
			otherSecondTex = (drawCyan ? secondWormTex : TextureGlow4.Value);
		}
		else
		{
			otherMainTex = mainWormTex;
			otherSecondTex = secondWormTex;
		}
		Color mainWormColorLerp = Color.Lerp(Color.White, doubleWormPhase ? phaseColor : Color.Cyan, 0.5f) * (deathModeEnragePhase ? 1f : base.NPC.Opacity);
		Color secondWormColorLerp = Color.Lerp(Color.White, doubleWormPhase ? phaseColor : Color.Orange, 0.5f) * (deathModeEnragePhase ? 1f : base.NPC.Opacity);
		int timesToDraw = (deathModeEnragePhase ? 3 : (drawCyan ? 1 : 2));
		for (int i = 0; i < timesToDraw; i++)
		{
			spriteBatch.Draw(mainWormTex, drawLocation, (Rectangle?)base.NPC.frame, mainWormColorLerp * (inColorTrans ? (1f - colorOpacity) : 1f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			if (inColorTrans)
			{
				spriteBatch.Draw(otherMainTex, drawLocation, (Rectangle?)base.NPC.frame, Color.Lerp(Color.White, otherPhaseColor, 0.5f) * colorOpacity, base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
			if (doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold < 25f)
			{
				spriteBatch.Draw(mainFlashTex, drawLocation, (Rectangle?)base.NPC.frame, Color.White * MathHelper.Lerp(1f, 0f, base.NPC.Calamity().newAI[3] % cyanThreshold / 25f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
		}
		if (!altBodyTextures)
		{
			timesToDraw = (deathModeEnragePhase ? 3 : ((!drawCyan) ? 1 : 2));
			for (int j = 0; j < timesToDraw; j++)
			{
				spriteBatch.Draw(secondWormTex, drawLocation, (Rectangle?)base.NPC.frame, secondWormColorLerp * (inColorTrans ? (1f - colorOpacity) : 1f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
				if (inColorTrans)
				{
					spriteBatch.Draw(otherSecondTex, drawLocation, (Rectangle?)base.NPC.frame, Color.Lerp(Color.White, otherPhaseColor, 0.5f) * colorOpacity, base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
				}
				if (doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold < 25f)
				{
					spriteBatch.Draw(TextureFlash2.Value, drawLocation, (Rectangle?)base.NPC.frame, Color.White * MathHelper.Lerp(1f, 0f, base.NPC.Calamity().newAI[3] % cyanThreshold / 25f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
				}
			}
		}
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		return !base.NPC.dontTakeDamage;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0 || Main.zenithWorld)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 5; i++)
		{
			int purpleDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[purpleDust].scale = 0.5f;
				Main.dust[purpleDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
			Main.dust[astralDust].noGravity = true;
			Dust obj2 = Main.dust[astralDust];
			obj2.velocity *= 5f;
			astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[astralDust];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			if (base.NPC.localAI[3] == 1f)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusAltBody1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusAltBody2").Type);
			}
			else
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusBody1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusBody2").Type);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
