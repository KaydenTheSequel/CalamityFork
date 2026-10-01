using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.CalClone;

[AutoloadBossHead]
public class Cataclysm : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/CataclysmHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/CataclysmDeath");

	public static readonly SoundStyle FlamethrowerStart = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/BrimstoneFlamethrowerCast");

	public static readonly SoundStyle FlamethrowerLoop = new SoundStyle("CalamityMod/Sounds/Custom/CalamitasClone/BrimstoneFlamethrowerLoop");

	public static int FlamethrowerDamage = 30;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitScale = 0.8f;
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
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
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 54;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 120;
		base.NPC.height = 120;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.2f;
		}
		base.NPC.defense = 10;
		base.NPC.LifeMaxNERB(7000, 10000, 80000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<CalamitasClone>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Cataclysm")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c35: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.Calamity();
		if (CalamityGlobalNPC.calamitas < 0 || !Main.npc[CalamityGlobalNPC.calamitas].active)
		{
			if (base.NPC.alpha < 255)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
				base.NPC.alpha += 2;
				if (base.NPC.alpha > 255)
				{
					base.NPC.alpha = 255;
				}
				int dustAmount = (int)Math.Round(MathHelper.Lerp(1f, 5f, (float)(255 - base.NPC.alpha) / 255f));
				for (int i = 0; i < dustAmount; i++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, -1f, 90, default(Color), Main.rand.NextFloat(0.5f, 2f));
					Main.dust[dust].noGravity = true;
					Main.dust[dust].fadeIn = 1f;
				}
			}
			else
			{
				base.NPC.active = false;
			}
			base.NPC.active = false;
			return;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		CalamityGlobalNPC.cataclysm = base.NPC.whoAmI;
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1f, 0f, 0f);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		float calCloneBroPlayerXDist = base.NPC.position.X + (float)(base.NPC.width / 2) - player.position.X - (float)(player.width / 2);
		float calCloneBroRotation = (float)Math.Atan2(base.NPC.position.Y + (float)base.NPC.height - 59f - player.position.Y - (float)(player.height / 2), calCloneBroPlayerXDist) + (float)Math.PI / 2f;
		if (calCloneBroRotation < 0f)
		{
			calCloneBroRotation += (float)Math.PI * 2f;
		}
		else if (calCloneBroRotation > (float)Math.PI * 2f)
		{
			calCloneBroRotation -= (float)Math.PI * 2f;
		}
		float calCloneBroRotationSpeed = 0.15f;
		if (base.NPC.rotation < calCloneBroRotation)
		{
			if (calCloneBroRotation - base.NPC.rotation > (float)Math.PI)
			{
				base.NPC.rotation -= calCloneBroRotationSpeed;
			}
			else
			{
				base.NPC.rotation += calCloneBroRotationSpeed;
			}
		}
		else if (base.NPC.rotation > calCloneBroRotation)
		{
			if (base.NPC.rotation - calCloneBroRotation > (float)Math.PI)
			{
				base.NPC.rotation += calCloneBroRotationSpeed;
			}
			else
			{
				base.NPC.rotation -= calCloneBroRotationSpeed;
			}
		}
		if (base.NPC.rotation > calCloneBroRotation - calCloneBroRotationSpeed && base.NPC.rotation < calCloneBroRotation + calCloneBroRotationSpeed)
		{
			base.NPC.rotation = calCloneBroRotation;
		}
		if (base.NPC.rotation < 0f)
		{
			base.NPC.rotation += (float)Math.PI * 2f;
		}
		else if (base.NPC.rotation > (float)Math.PI * 2f)
		{
			base.NPC.rotation -= (float)Math.PI * 2f;
		}
		if (base.NPC.rotation > calCloneBroRotation - calCloneBroRotationSpeed && base.NPC.rotation < calCloneBroRotation + calCloneBroRotationSpeed)
		{
			base.NPC.rotation = calCloneBroRotation;
		}
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				if (base.NPC.velocity.Y > 3f)
				{
					base.NPC.velocity.Y = 3f;
				}
				base.NPC.velocity.Y -= 0.1f;
				if (base.NPC.velocity.Y < -12f)
				{
					base.NPC.velocity.Y = -12f;
				}
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.ai[1] != 0f)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		if (base.NPC.ai[1] == 0f)
		{
			float calCloneBroProjAttackMaxSpeed = 5f;
			float calCloneBroProjAttackAccel = 0.1f;
			if (Main.getGoodWorld)
			{
				calCloneBroProjAttackMaxSpeed *= 1.15f;
				calCloneBroProjAttackAccel *= 1.15f;
			}
			int calCloneBroProjAttackDirection = 1;
			if (base.NPC.position.X + (float)(base.NPC.width / 2) < player.position.X + (float)player.width)
			{
				calCloneBroProjAttackDirection = -1;
			}
			Vector2 calCloneBroProjLocation = default(Vector2);
			((Vector2)(ref calCloneBroProjLocation))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float calCloneBroProjTargetX = player.position.X + (float)(player.width / 2) + (float)(calCloneBroProjAttackDirection * 180) - calCloneBroProjLocation.X;
			float calCloneBroProjTargetY = player.position.Y + (float)(player.height / 2) - calCloneBroProjLocation.Y;
			float calCloneBroProjTargetDist = (float)Math.Sqrt(calCloneBroProjTargetX * calCloneBroProjTargetX + calCloneBroProjTargetY * calCloneBroProjTargetY);
			if (expertMode)
			{
				if (calCloneBroProjTargetDist > 300f)
				{
					calCloneBroProjAttackMaxSpeed += 0.5f;
				}
				if (calCloneBroProjTargetDist > 400f)
				{
					calCloneBroProjAttackMaxSpeed += 0.5f;
				}
				if (calCloneBroProjTargetDist > 500f)
				{
					calCloneBroProjAttackMaxSpeed += 0.55f;
				}
				if (calCloneBroProjTargetDist > 600f)
				{
					calCloneBroProjAttackMaxSpeed += 0.55f;
				}
				if (calCloneBroProjTargetDist > 700f)
				{
					calCloneBroProjAttackMaxSpeed += 0.6f;
				}
				if (calCloneBroProjTargetDist > 800f)
				{
					calCloneBroProjAttackMaxSpeed += 0.6f;
				}
			}
			calCloneBroProjTargetDist = calCloneBroProjAttackMaxSpeed / calCloneBroProjTargetDist;
			calCloneBroProjTargetX *= calCloneBroProjTargetDist;
			calCloneBroProjTargetY *= calCloneBroProjTargetDist;
			if (base.NPC.velocity.X < calCloneBroProjTargetX)
			{
				base.NPC.velocity.X += calCloneBroProjAttackAccel;
				if (base.NPC.velocity.X < 0f && calCloneBroProjTargetX > 0f)
				{
					base.NPC.velocity.X += calCloneBroProjAttackAccel;
				}
			}
			else if (base.NPC.velocity.X > calCloneBroProjTargetX)
			{
				base.NPC.velocity.X -= calCloneBroProjAttackAccel;
				if (base.NPC.velocity.X > 0f && calCloneBroProjTargetX < 0f)
				{
					base.NPC.velocity.X -= calCloneBroProjAttackAccel;
				}
			}
			if (base.NPC.velocity.Y < calCloneBroProjTargetY)
			{
				base.NPC.velocity.Y += calCloneBroProjAttackAccel;
				if (base.NPC.velocity.Y < 0f && calCloneBroProjTargetY > 0f)
				{
					base.NPC.velocity.Y += calCloneBroProjAttackAccel;
				}
			}
			else if (base.NPC.velocity.Y > calCloneBroProjTargetY)
			{
				base.NPC.velocity.Y -= calCloneBroProjAttackAccel;
				if (base.NPC.velocity.Y > 0f && calCloneBroProjTargetY < 0f)
				{
					base.NPC.velocity.Y -= calCloneBroProjAttackAccel;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] >= 240f - (death ? (120f * (1f - lifeRatio)) : 0f))
			{
				base.NPC.ai[1] = 1f;
				base.NPC.ai[2] = 0f;
				base.NPC.target = 255;
				base.NPC.netUpdate = true;
			}
			bool fireDelay = base.NPC.ai[2] > 120f || (double)base.NPC.life < (double)base.NPC.lifeMax * 0.9;
			if (!(Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height) & fireDelay))
			{
				return;
			}
			if (base.NPC.localAI[2] == 0f)
			{
				SoundEngine.PlaySound(in FlamethrowerStart, base.NPC.Center);
			}
			base.NPC.localAI[2]++;
			if (base.NPC.localAI[2] > 30f)
			{
				base.NPC.localAI[2] = 1f;
				SoundEngine.PlaySound(in FlamethrowerLoop, base.NPC.Center);
			}
			if (Main.netMode != 1)
			{
				base.NPC.localAI[1] += 3f;
				if (revenge)
				{
					base.NPC.localAI[1]++;
				}
				if (base.NPC.localAI[1] > 12f)
				{
					base.NPC.localAI[1] = 0f;
					float num = (NPC.AnyNPCs(ModContent.NPCType<Catastrophe>()) ? 4f : 6f);
					int type = ModContent.ProjectileType<CataclysmicFlame>();
					((Vector2)(ref calCloneBroProjLocation))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
					calCloneBroProjTargetX = player.position.X + (float)(player.width / 2) - calCloneBroProjLocation.X;
					calCloneBroProjTargetY = player.position.Y + (float)(player.height / 2) - calCloneBroProjLocation.Y;
					calCloneBroProjTargetDist = (float)Math.Sqrt(calCloneBroProjTargetX * calCloneBroProjTargetX + calCloneBroProjTargetY * calCloneBroProjTargetY);
					calCloneBroProjTargetDist = num / calCloneBroProjTargetDist;
					calCloneBroProjTargetX *= calCloneBroProjTargetDist;
					calCloneBroProjTargetY *= calCloneBroProjTargetDist;
					calCloneBroProjTargetY += base.NPC.velocity.Y * 0.5f;
					calCloneBroProjTargetX += base.NPC.velocity.X * 0.5f;
					calCloneBroProjLocation.X -= calCloneBroProjTargetX;
					calCloneBroProjLocation.Y -= calCloneBroProjTargetY;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), calCloneBroProjLocation.X, calCloneBroProjLocation.Y, calCloneBroProjTargetX, calCloneBroProjTargetY, type, FlamethrowerDamage, 0f, Main.myPlayer);
				}
			}
		}
		else if (base.NPC.ai[1] == 1f)
		{
			SoundEngine.PlaySound(in SoundID.Roar, base.NPC.Center);
			base.NPC.rotation = calCloneBroRotation;
			float calCloneBroChargeSpeed = 14f + (death ? (4f * (1f - lifeRatio)) : 0f);
			if (expertMode)
			{
				calCloneBroChargeSpeed += 2f;
			}
			if (revenge)
			{
				calCloneBroChargeSpeed += 2f;
			}
			if (Main.getGoodWorld)
			{
				calCloneBroChargeSpeed *= 1.25f;
			}
			Vector2 calCloneBroChargeCenter = base.NPC.Center;
			float calCloneBroChargeTargetXDist = player.Center.X - calCloneBroChargeCenter.X;
			float calCloneBroChargeTargetYDist = player.Center.Y - calCloneBroChargeCenter.Y;
			float calCloneBroChargeTargetDistance = (float)Math.Sqrt(calCloneBroChargeTargetXDist * calCloneBroChargeTargetXDist + calCloneBroChargeTargetYDist * calCloneBroChargeTargetYDist);
			calCloneBroChargeTargetDistance = calCloneBroChargeSpeed / calCloneBroChargeTargetDistance;
			base.NPC.velocity.X = calCloneBroChargeTargetXDist * calCloneBroChargeTargetDistance;
			base.NPC.velocity.Y = calCloneBroChargeTargetYDist * calCloneBroChargeTargetDistance;
			base.NPC.ai[1] = 2f;
			if (Main.zenithWorld)
			{
				SoundEngine.PlaySound(in global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas.BrimstoneShotSound, base.NPC.Center);
				int type2 = ModContent.ProjectileType<BurningBolt>();
				int totalProjectiles = (death ? 10 : (revenge ? 8 : (expertMode ? 6 : 4)));
				float radians = (float)Math.PI * 2f / (float)totalProjectiles;
				float velocity = 5f;
				Vector2 spinningPoint = default(Vector2);
				((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
				float projectileVelocityToPass = velocity * 3f;
				for (int k = 0; k < totalProjectiles; k++)
				{
					Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, type2, CalamitasClone.DartDamage, 0f, Main.myPlayer, 0f, 0f, projectileVelocityToPass);
				}
				for (int j = 0; j < 6; j++)
				{
					Dust.NewDust(base.NPC.position + base.NPC.velocity, base.NPC.width, base.NPC.height, 235);
				}
			}
		}
		else
		{
			if (base.NPC.ai[1] != 2f)
			{
				return;
			}
			base.NPC.ai[2] += 1f + (death ? (0.5f * (1f - lifeRatio)) : 0f);
			if (expertMode)
			{
				base.NPC.ai[2] += 0.25f;
			}
			if (revenge)
			{
				base.NPC.ai[2] += 0.25f;
			}
			if (base.NPC.ai[2] >= 75f)
			{
				base.NPC.velocity.X *= 0.93f;
				base.NPC.velocity.Y *= 0.93f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
				{
					base.NPC.velocity.Y = 0f;
				}
			}
			else
			{
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) - (float)Math.PI / 2f;
			}
			if (base.NPC.ai[2] >= 105f)
			{
				base.NPC.ai[3]++;
				base.NPC.ai[2] = 0f;
				base.NPC.target = 255;
				base.NPC.rotation = calCloneBroRotation;
				if (base.NPC.ai[3] >= 3f)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[3] = 0f;
				}
				else
				{
					base.NPC.ai[1] = 1f;
				}
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		int afterimageAmt = 7;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimageDrawPos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimageDrawPos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color pinkLerp = Color.Lerp(Color.White, Color.Red, 0.5f);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color extraAfterimageColor = pinkLerp;
				extraAfterimageColor = Color.Lerp(extraAfterimageColor, Color.White, 0.5f);
				extraAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 extraAfterimageDrawPos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				extraAfterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				extraAfterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, extraAfterimageDrawPos, (Rectangle?)base.NPC.frame, extraAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, pinkLerp, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * balance);
	}

	public override bool CheckActive()
	{
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
		IItemDropRuleCondition KilledLast = DropHelper.If(() => !NPC.AnyNPCs(ModContent.NPCType<Catastrophe>()), ui: true, DropHelper.CataclysmKilledLast);
		npcLoot.Add(ItemDropRule.ByCondition(KilledLast, ModContent.ItemType<CataclysmTrophy>(), 5));
		npcLoot.Add(ItemDropRule.ByCondition(KilledLast, ModContent.ItemType<HavocsBreath>()));
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cataclysm").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cataclysm2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cataclysm3").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cataclysm4").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Cataclysm5").Type, base.NPC.scale);
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
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
		}
	}
}
