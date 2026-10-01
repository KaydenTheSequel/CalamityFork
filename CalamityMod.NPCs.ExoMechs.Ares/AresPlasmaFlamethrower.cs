using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs.Ares;

[HasPierceResist(false)]
public class AresPlasmaFlamethrower : ModNPC
{
	public enum Phase
	{
		Nothing,
		PlasmaBolts
	}

	public ThanatosSmokeParticleSet SmokeDrawer;

	public AresCannonChargeParticleSet EnergyDrawer;

	public const int maxFramesX = 6;

	public const int maxFramesY = 8;

	public int frameX;

	public int frameY;

	public const int normalFrameLimit = 11;

	public const int firstStagePlasmaBoltChargeFrameLimit = 23;

	public const int secondStagePlasmaBoltChargeFrameLimit = 35;

	public const int finalStagePlasmaBoltChargeFrameLimit = 47;

	public const float defaultLifeRatio = 5f;

	public const float plasmaBoltTelegraphDuration = 144f;

	public const float plasmaBoltDuration = 120f;

	public SlotId TelegraphSoundSlot;

	public static readonly SoundStyle TelSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresPlasmaArmCharge");

	public static Asset<Texture2D> GlowTexture;

	public static int FireballDamage = 80;

	public static int BoltDamage = 70;

	public float AIState
	{
		get
		{
			return base.NPC.Calamity().newAI[0];
		}
		set
		{
			base.NPC.Calamity().newAI[0] = value;
		}
	}

	public Vector2 CoreSpritePosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			return base.NPC.Center + (float)base.NPC.spriteDirection * base.NPC.rotation.ToRotationVector2() * 30f + (base.NPC.rotation + (float)Math.PI / 2f).ToRotationVector2() * 15f;
		}
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.TrailingMode[base.Type] = 3;
		NPCID.Sets.TrailCacheLength[base.Type] = base.NPC.oldPos.Length;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 152;
		base.NPC.height = 90;
		base.NPC.defense = 100;
		base.NPC.DR_NERD(0.35f);
		base.NPC.LifeMaxNERB(1000000, 1495000, 650000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Opacity = 0f;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = CommonCalamitySounds.ExoDeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.hide = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(frameX);
		writer.Write(frameY);
		writer.Write(base.NPC.dontTakeDamage);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		frameX = reader.ReadInt32();
		frameY = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ceb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d14: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.draedonExoMechPrimePlasmaCannon = base.NPC.whoAmI;
		base.NPC.frame = new Rectangle(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
		if (CalamityGlobalNPC.draedonExoMechPrime < 0 || !Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float lifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].lifeMax;
		int otherExoMechsAlive = 0;
		bool exoWormAlive = false;
		bool exoTwinsAlive = false;
		if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
		{
			otherExoMechsAlive++;
			exoWormAlive = true;
		}
		if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			otherExoMechsAlive++;
			exoTwinsAlive = true;
		}
		bool nerfedAttacks = false;
		if (exoTwinsAlive)
		{
			nerfedAttacks = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[1] != 2f;
		}
		int num;
		int num2;
		if (!(lifeRatio < 0.4f))
		{
			if (otherExoMechsAlive == 0)
			{
				num = ((lifeRatio < 0.7f) ? 1 : 0);
				if (num != 0)
				{
					goto IL_01bc;
				}
			}
			else
			{
				num = 0;
			}
			num2 = 0;
			goto IL_01c4;
		}
		num = 1;
		goto IL_01bc;
		IL_01c4:
		bool lastMechAlive = (byte)num2 != 0;
		float exoWormLifeRatio = 5f;
		float exoTwinsLifeRatio = 5f;
		if (exoWormAlive)
		{
			exoWormLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].lifeMax;
		}
		if (exoTwinsAlive)
		{
			exoTwinsLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].lifeMax;
		}
		bool otherMechIsBerserk = exoWormLifeRatio < 0.4f || exoTwinsLifeRatio < 0.4f;
		bool shouldGetBuffedByBerserkPhase = num != 0 && !otherMechIsBerserk;
		int targetIndex = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].target;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		AresBody aresBody = Main.npc[(int)base.NPC.ai[2]].ModNPC<AresBody>();
		CalamityGlobalNPC calamityGlobalNPC_Body = Main.npc[(int)base.NPC.ai[2]].Calamity();
		bool num3 = calamityGlobalNPC_Body.newAI[1] == 1f;
		bool enraged = Main.npc[(int)base.NPC.ai[2]].localAI[1] == 1f;
		bool invisiblePhase = calamityGlobalNPC_Body.newAI[1] == 2f;
		base.NPC.dontTakeDamage = invisiblePhase || Main.npc[(int)base.NPC.ai[2]].dontTakeDamage;
		if (!invisiblePhase)
		{
			base.NPC.Opacity += 0.2f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
		}
		else
		{
			base.NPC.Opacity -= 0.05f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
		}
		base.NPC.Calamity().newAI[3]++;
		if (base.NPC.Calamity().newAI[3] >= 240f)
		{
			base.NPC.Calamity().newAI[3] = 0f;
		}
		float predictionAmt = (death ? 15f : (revenge ? 13.75f : (expertMode ? 12.5f : 10f)));
		if (nerfedAttacks)
		{
			predictionAmt *= 0.5f;
		}
		if (num3)
		{
			predictionAmt *= 0.5f;
		}
		Vector2 predictionVector = Main.player[targetIndex].velocity * predictionAmt;
		Vector2 rotationVector = Main.player[targetIndex].Center + predictionVector - base.NPC.Center;
		float projectileVelocity = (num3 ? 9.6f : 12f);
		if (lastMechAlive)
		{
			projectileVelocity *= 1.2f;
		}
		else if (shouldGetBuffedByBerserkPhase)
		{
			projectileVelocity *= 1.1f;
		}
		float rateOfRotation = ((AIState == 1f) ? 0.08f : 0.04f);
		Vector2 lookAt = Vector2.Normalize(rotationVector) * projectileVelocity;
		float rotation = (float)Math.Atan2(lookAt.Y, lookAt.X);
		if (base.NPC.spriteDirection == 1)
		{
			rotation += (float)Math.PI;
		}
		if (rotation < 0f)
		{
			rotation += (float)Math.PI * 2f;
		}
		if (rotation > (float)Math.PI * 2f)
		{
			rotation -= (float)Math.PI * 2f;
		}
		base.NPC.rotation = base.NPC.rotation.AngleTowards(rotation, rateOfRotation);
		int direction = Math.Sign(Main.player[targetIndex].Center.X - base.NPC.Center.X);
		if (direction != 0)
		{
			base.NPC.direction = direction;
			if (base.NPC.spriteDirection != -base.NPC.direction)
			{
				base.NPC.rotation += (float)Math.PI;
			}
			base.NPC.spriteDirection = -base.NPC.direction;
		}
		bool fireMoreBolts = calamityGlobalNPC_Body.newAI[0] == 1f;
		float plasmaBoltPhaseGateValue = (fireMoreBolts ? 120f : 270f);
		if (enraged)
		{
			plasmaBoltPhaseGateValue *= 0.1f;
		}
		else if (lastMechAlive)
		{
			plasmaBoltPhaseGateValue *= 0.4f;
		}
		else if (shouldGetBuffedByBerserkPhase)
		{
			plasmaBoltPhaseGateValue *= 0.7f;
		}
		float setTimerTo = (int)(plasmaBoltPhaseGateValue * 0.95f) - 1;
		if (Main.player[targetIndex].dead)
		{
			AIState = 0f;
			calamityGlobalNPC.newAI[1] = setTimerTo;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.dontTakeDamage = true;
			base.NPC.velocity.Y--;
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				base.NPC.velocity.Y--;
			}
			if (!((double)base.NPC.position.Y < (double)(Main.topWorld + 16f)))
			{
				return;
			}
			for (int a = 0; a < Main.maxNPCs; a++)
			{
				if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>() || Main.npc[a].type == ModContent.NPCType<AresBody>() || Main.npc[a].type == ModContent.NPCType<AresLaserCannon>() || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>() || Main.npc[a].type == ModContent.NPCType<AresTeslaCannon>() || Main.npc[a].type == ModContent.NPCType<AresGaussNuke>() || Main.npc[a].type == ModContent.NPCType<ThanatosHead>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody1>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody2>() || Main.npc[a].type == ModContent.NPCType<ThanatosTail>())
				{
					Main.npc[a].active = false;
				}
			}
			return;
		}
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector(375f, 160f + 20f * (float)Math.Sin(base.NPC.Calamity().newAI[3] * (float)Math.PI / 120f));
		Vector2 offset2 = default(Vector2);
		((Vector2)(ref offset2))._002Ector(540f, -540f);
		switch ((int)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ai[3])
		{
		case 2:
		case 3:
			offset.X *= -1f;
			offset2 *= -1f;
			break;
		}
		Vector2 val = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Center + ((calamityGlobalNPC_Body.newAI[0] == 1f) ? offset2 : offset);
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.25f : 0f) + (death ? 1.1f : (revenge ? 1.075f : (expertMode ? 1.05f : 1f)));
		float baseVelocity = (enraged ? 38f : 30f) * baseVelocityMult;
		baseVelocity *= 1f + Main.npc[(int)base.NPC.ai[2]].localAI[2];
		Vector2 distanceFromDestination = val - base.NPC.Center;
		float movementDistanceGateValue = 50f;
		bool boltsSplitLess = fireMoreBolts && !lastMechAlive;
		bool canFire = Vector2.Distance(base.NPC.Center, Main.player[targetIndex].Center) > 320f || calamityGlobalNPC_Body.newAI[0] != 1f;
		float deathrayTelegraphDuration = (death ? 90f : (revenge ? 105f : (expertMode ? 120f : 150f)));
		if (calamityGlobalNPC_Body.newAI[1] == 2f || (calamityGlobalNPC_Body.newAI[2] >= deathrayTelegraphDuration + 600f - 10f && calamityGlobalNPC_Body.newAI[0] == 1f) || (calamityGlobalNPC_Body.newAI[3] == 0f && calamityGlobalNPC_Body.newAI[0] == 1f))
		{
			AIState = 0f;
			calamityGlobalNPC.newAI[1] = setTimerTo;
			calamityGlobalNPC.newAI[2] = 0f;
		}
		SmokeDrawer.ParticleSpawnRate = 9999999;
		if (enraged)
		{
			SmokeDrawer.ParticleSpawnRate = 3;
			SmokeDrawer.BaseMoveRotation = base.NPC.rotation + (float)Math.PI;
			SmokeDrawer.SpawnAreaCompactness = 40f;
			base.NPC.Calamity().DR = 0.85f;
		}
		else
		{
			base.NPC.Calamity().DR = 0.35f;
		}
		SmokeDrawer.Update();
		EnergyDrawer.ParticleSpawnRate = 9999999;
		switch ((int)AIState)
		{
		case 0:
			calamityGlobalNPC.newAI[1]++;
			if (calamityGlobalNPC.newAI[1] >= plasmaBoltPhaseGateValue)
			{
				AIState = 1f;
				calamityGlobalNPC.newAI[1] = 0f;
			}
			break;
		case 1:
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] < 144f)
			{
				if (calamityGlobalNPC.newAI[2] == 1f)
				{
					TelegraphSoundSlot = SoundEngine.PlaySound(in TelSound, base.NPC.Center);
				}
				if (calamityGlobalNPC.newAI[2] == 1f)
				{
					base.NPC.frameCounter = 0.0;
					frameX = 1;
					frameY = 4;
				}
				EnergyDrawer.ParticleSpawnRate = 5;
				EnergyDrawer.SpawnAreaCompactness = 100f;
				EnergyDrawer.chargeProgress = calamityGlobalNPC.newAI[2] / 144f;
			}
			else if (calamityGlobalNPC.newAI[2] < 264f)
			{
				int numPlasmaBolts = (lastMechAlive ? 3 : 2);
				float divisor = 120f / (float)numPlasmaBolts;
				if (((calamityGlobalNPC.newAI[2] - 144f) % divisor == 0f) & canFire)
				{
					base.NPC.ai[3]++;
					SoundEngine.PlaySound(in CommonCalamitySounds.ExoPlasmaShootSound, base.NPC.Center);
					Vector2 plasmaBoltVelocity = Vector2.Normalize(rotationVector) * projectileVelocity;
					if (Main.netMode != 1)
					{
						int type = ModContent.ProjectileType<AresPlasmaFireball>();
						Vector2 plasmaOffset = Vector2.Normalize(plasmaBoltVelocity) * 40f + Vector2.UnitY * 16f;
						if (boltsSplitLess)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + plasmaOffset, plasmaBoltVelocity, type, FireballDamage, 0f, Main.myPlayer, -1f);
						}
						else
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + plasmaOffset, plasmaBoltVelocity, type, FireballDamage, 0f, Main.myPlayer, Main.player[targetIndex].Center.X, Main.player[targetIndex].Center.Y);
						}
					}
					NPC nPC = base.NPC;
					nPC.velocity -= plasmaBoltVelocity;
				}
			}
			if (calamityGlobalNPC.newAI[2] % (float)Math.Floor(28.799999237060547) == (float)Math.Floor(28.799999237060547) - 1f && calamityGlobalNPC.newAI[2] <= 144f)
			{
				float pulseCounter = (float)Math.Floor(calamityGlobalNPC.newAI[2] / 28.8f) + 1f;
				EnergyDrawer.AddPulse(pulseCounter);
			}
			if (calamityGlobalNPC.newAI[2] >= 264f)
			{
				AIState = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
			}
			break;
		}
		EnergyDrawer.Update();
		CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
		if (SoundEngine.TryGetActiveSound(TelegraphSoundSlot, out ActiveSound telSound) && telSound.IsPlaying)
		{
			telSound.Position = base.NPC.Center;
			if (aresBody.AIState == 1f && calamityGlobalNPC_Body.newAI[2] <= 10f)
			{
				telSound.Stop();
			}
		}
		return;
		IL_01bc:
		num2 = ((otherExoMechsAlive == 0) ? 1 : 0);
		goto IL_01c4;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (AIState == 0f)
		{
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frameCounter = 0.0;
				frameY++;
				if (frameY == 8)
				{
					frameX++;
					frameY = 0;
				}
				if (frameX * 8 + frameY > 11)
				{
					frameX = (frameY = 0);
				}
			}
		}
		else if (base.NPC.frameCounter >= 6.0)
		{
			base.NPC.frameCounter = 0.0;
			frameY++;
			if (frameY == 8)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX * 8 + frameY > 47)
			{
				frameX = (frameY = 4);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		SmokeDrawer.DrawSet(base.NPC.Center);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(base.NPC.width / 2), (float)(base.NPC.height / 2));
		Color afterimageBaseColor = ((Main.npc[(int)base.NPC.ai[2]].localAI[1] == 1f) ? Color.Red : Color.White);
		int numAfterimages = 5;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < numAfterimages; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, afterimageBaseColor, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(numAfterimages - i) / 15f;
				Vector2 afterimageCenter = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageCenter -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2(6f, 8f) * base.NPC.scale / 2f;
				afterimageCenter += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimageCenter, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.oldRot[i], vector, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 center = base.NPC.Center - screenPos;
		if (base.NPC.Calamity().newAI[2] < 144f && AIState == 1f)
		{
			spriteBatch.EnterShaderRegion();
			Color outlineColor = Color.Lerp(Color.GreenYellow, Color.White, base.NPC.Calamity().newAI[2] / 144f);
			Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
			float outlineThickness = MathHelper.Clamp(base.NPC.Calamity().newAI[2] / 144f * 4f, 0f, 3f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - outlineHSL.X, outlineHSL.Y, outlineHSL.Z));
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
			for (float i2 = 0f; i2 < 1f; i2 += 0.125f)
			{
				spriteBatch.Draw(texture, center + (i2 * ((float)Math.PI * 2f) + base.NPC.rotation).ToRotationVector2() * outlineThickness, (Rectangle?)frame, outlineColor, base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
			}
			spriteBatch.ExitShaderRegion();
		}
		spriteBatch.Draw(texture, center, (Rectangle?)frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		Texture2D glowTexture = GlowTexture.Value;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < numAfterimages; j += 2)
			{
				Color afterimageColor2 = drawColor;
				afterimageColor2 = Color.Lerp(afterimageColor2, afterimageBaseColor, 0.5f);
				afterimageColor2 = base.NPC.GetAlpha(afterimageColor2);
				afterimageColor2 *= (float)(numAfterimages - j) / 15f;
				Vector2 afterimageCenter2 = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageCenter2 -= new Vector2((float)glowTexture.Width, (float)glowTexture.Height) / new Vector2(6f, 8f) * base.NPC.scale / 2f;
				afterimageCenter2 += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(glowTexture, afterimageCenter2, (Rectangle?)base.NPC.frame, afterimageColor2, base.NPC.oldRot[j], vector, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(glowTexture, center, (Rectangle?)frame, afterimageBaseColor * base.NPC.Opacity, base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (base.NPC.Calamity().newAI[2] < 144f && AIState == 1f)
		{
			float pulseRatio = base.NPC.Calamity().newAI[2] % 28.8f / 28.8f;
			float pulseSize = MathHelper.Lerp(0.1f, 0.6f, (float)Math.Floor(base.NPC.Calamity().newAI[2] / 28.8f) / 4f);
			float pulseOpacity = MathHelper.Clamp((float)Math.Floor(base.NPC.Calamity().newAI[2] / 28.8f) * 0.3f, 1f, 2f);
			spriteBatch.Draw(texture, center, (Rectangle?)frame, Color.GreenYellow * MathHelper.Lerp(1f, 0f, pulseRatio) * pulseOpacity, base.NPC.rotation, vector, base.NPC.scale + pulseRatio * pulseSize, spriteEffects, 0f);
			EnergyDrawer.DrawBloom(CoreSpritePosition);
		}
		EnergyDrawer.DrawPulses(CoreSpritePosition);
		EnergyDrawer.DrawSet(CoreSpritePosition);
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void DrawBehind(int index)
	{
		Main.instance.DrawCacheNPCProjectiles.Add(index);
	}

	public override void ModifyTypeName(ref string typeName)
	{
		int index = CalamityGlobalNPC.draedonExoMechPrime;
		if (index >= 0 && index < Main.maxNPCs && Main.npc[index] != null && Main.npc[index].ModNPC<AresBody>().exoMechdusa)
		{
			typeName = this.GetLocalizedValue("HekateName");
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255));
		}
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 3;
			SoundEngine.PlaySound(in CommonCalamitySounds.ExoHitSound, base.NPC.Center);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			}
			for (int j = 0; j < 20; j++)
			{
				int plasmaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
				Main.dust[plasmaDust].noGravity = true;
				Dust obj = Main.dust[plasmaDust];
				obj.velocity *= 3f;
				plasmaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
				Dust obj2 = Main.dust[plasmaDust];
				obj2.velocity *= 2f;
				Main.dust[plasmaDust].noGravity = true;
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresPlasmaFlamethrower1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresPlasmaFlamethrower2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresHandBase1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresHandBase2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresHandBase3").Type);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public AresPlasmaFlamethrower()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);
		EnergyDrawer = new AresCannonChargeParticleSet(-1, 15, 40f, Color.GreenYellow);
		base._002Ector();
	}
}
