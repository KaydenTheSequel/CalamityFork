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
public class AresGaussNuke : ModNPC
{
	public enum Phase
	{
		Nothing,
		GaussNuke,
		Reload
	}

	public ThanatosSmokeParticleSet SmokeDrawer;

	public AresCannonChargeParticleSet EnergyDrawer;

	public SlotId TelegraphSoundSlot;

	public const int maxFramesX = 9;

	public const int maxFramesY = 12;

	public int frameX;

	public int frameY;

	public const int normalFrameLimit = 11;

	public const int firstStageGaussNukeChargeFrameLimit = 23;

	public const int secondStageGaussNukeChargeFrameLimit = 35;

	public const int finalStageGaussNukeChargeFrameLimit = 47;

	public const int reloadFrameLimit = 107;

	public const float defaultLifeRatio = 5f;

	public const float gaussNukeTelegraphDuration = 216f;

	public const float gaussNukeReloadDuration = 360f;

	public static readonly SoundStyle TelSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresGaussNukeArmCharge")
	{
		Volume = 1.1f
	};

	public static readonly SoundStyle NukeExplosionSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresGaussNukeExplosion")
	{
		Volume = 1.45f
	};

	public static Asset<Texture2D> GlowTexture;

	public static int NukeDamage = 135;

	public static int SparkDamage = 70;

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
			return base.NPC.Center + (float)base.NPC.spriteDirection * base.NPC.rotation.ToRotationVector2() * 35f + (base.NPC.rotation + (float)Math.PI / 2f).ToRotationVector2() * 5f;
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
		base.NPC.width = 170;
		base.NPC.height = 120;
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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
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
					goto IL_01ac;
				}
			}
			else
			{
				num = 0;
			}
			num2 = 0;
			goto IL_01b4;
		}
		num = 1;
		goto IL_01ac;
		IL_01b4:
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
		if (Main.player[targetIndex].dead)
		{
			AIState = 0f;
			calamityGlobalNPC.newAI[1] = 0f;
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
				if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>() || Main.npc[a].type == ModContent.NPCType<AresBody>() || Main.npc[a].type == ModContent.NPCType<AresLaserCannon>() || Main.npc[a].type == ModContent.NPCType<AresPlasmaFlamethrower>() || Main.npc[a].type == ModContent.NPCType<AresTeslaCannon>() || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>() || Main.npc[a].type == ModContent.NPCType<ThanatosHead>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody1>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody2>() || Main.npc[a].type == ModContent.NPCType<ThanatosTail>())
				{
					Main.npc[a].active = false;
				}
			}
			return;
		}
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector(560f, 20f * (float)Math.Sin(base.NPC.Calamity().newAI[3] * (float)Math.PI / 120f));
		Vector2 offset2 = default(Vector2);
		((Vector2)(ref offset2))._002Ector(540f, 540f);
		switch ((int)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ai[3])
		{
		case 1:
		case 2:
		case 5:
			offset.X *= -1f;
			offset2 *= -1f;
			break;
		}
		Vector2 destination = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Center + ((calamityGlobalNPC_Body.newAI[0] == 1f) ? offset2 : offset);
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.25f : 0f) + (death ? 1.1f : (revenge ? 1.075f : (expertMode ? 1.05f : 1f)));
		float baseVelocity = (enraged ? 38f : 30f) * baseVelocityMult;
		baseVelocity *= 1f + Main.npc[(int)base.NPC.ai[2]].localAI[2];
		Vector2 distanceFromDestination = destination - base.NPC.Center;
		float movementDistanceGateValue = 50f;
		float gaussNukePhaseGateValue = 750f;
		if (enraged)
		{
			gaussNukePhaseGateValue *= 0.05f;
		}
		else if (lastMechAlive)
		{
			gaussNukePhaseGateValue *= 0.1f;
		}
		else if (shouldGetBuffedByBerserkPhase)
		{
			gaussNukePhaseGateValue *= 0.15f;
		}
		if ((calamityGlobalNPC_Body.newAI[0] == 1f || calamityGlobalNPC_Body.newAI[1] == 2f) && AIState != 2f)
		{
			AIState = 0f;
			calamityGlobalNPC.newAI[1] = 0f;
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
			if (calamityGlobalNPC.newAI[1] >= gaussNukePhaseGateValue)
			{
				AIState = 1f;
				calamityGlobalNPC.newAI[1] = 0f;
			}
			break;
		case 1:
		{
			calamityGlobalNPC.newAI[2]++;
			float telegraphDuration = (enraged ? 108f : 216f);
			if (calamityGlobalNPC.newAI[2] < telegraphDuration)
			{
				if (calamityGlobalNPC.newAI[2] == 1f)
				{
					TelegraphSoundSlot = SoundEngine.PlaySound(in TelSound, base.NPC.Center);
				}
				if (calamityGlobalNPC.newAI[2] == 1f)
				{
					base.NPC.frameCounter = 0.0;
					frameX = 1;
					frameY = 0;
				}
				if (frameX * 12 + frameY == 41 && calamityGlobalNPC.newAI[1] == 0f)
				{
					calamityGlobalNPC.newAI[1] = 1f;
					SoundEngine.PlaySound(in CommonCalamitySounds.LargeWeaponFireSound, base.NPC.Center);
					Vector2 gaussNukeVelocity = Vector2.Normalize(rotationVector) * projectileVelocity;
					if (Main.netMode != 1)
					{
						int type = ModContent.ProjectileType<AresGaussNukeProjectile>();
						float nukeOffset = 40f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(gaussNukeVelocity) * nukeOffset, gaussNukeVelocity, type, NukeDamage, 0f, Main.myPlayer, 0f, Main.player[targetIndex].Center.Y);
					}
					NPC nPC = base.NPC;
					nPC.velocity -= gaussNukeVelocity * 2f;
				}
				EnergyDrawer.ParticleSpawnRate = 5;
				EnergyDrawer.SpawnAreaCompactness = 100f;
				EnergyDrawer.chargeProgress = calamityGlobalNPC.newAI[2] / telegraphDuration;
			}
			else
			{
				AIState = 2f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.frameCounter = 0.0;
				frameX = 4;
				frameY = 0;
			}
			if (calamityGlobalNPC.newAI[2] % (float)Math.Floor(telegraphDuration / 5f) == (float)Math.Floor(telegraphDuration / 5f) - 1f && calamityGlobalNPC.newAI[2] <= telegraphDuration)
			{
				float pulseCounter = (float)Math.Floor(calamityGlobalNPC.newAI[2] / (telegraphDuration / 5f)) + 1f;
				EnergyDrawer.AddPulse(pulseCounter);
			}
			break;
		}
		case 2:
		{
			calamityGlobalNPC.newAI[2]++;
			float reloadDuration = (enraged ? 180f : 360f);
			if (calamityGlobalNPC.newAI[2] >= reloadDuration)
			{
				AIState = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
			}
			break;
		}
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
		IL_01ac:
		num2 = ((otherExoMechsAlive == 0) ? 1 : 0);
		goto IL_01b4;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		double frameTime = ((Main.npc[(int)base.NPC.ai[2]].localAI[1] == 1f) ? 3.0 : 6.0);
		if (AIState == 0f)
		{
			if (base.NPC.frameCounter >= frameTime)
			{
				base.NPC.frameCounter = 0.0;
				frameY++;
				if (frameY == 12)
				{
					frameX++;
					frameY = 0;
				}
				if (frameX * 12 + frameY > 11)
				{
					frameX = (frameY = 0);
				}
			}
		}
		else if (base.NPC.frameCounter >= frameTime)
		{
			base.NPC.frameCounter = 0.0;
			frameY++;
			if (frameY == 12)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX * 12 + frameY > 107)
			{
				frameX = (frameY = 0);
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
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
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
				afterimageCenter -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2(9f, 12f) * base.NPC.scale / 2f;
				afterimageCenter += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimageCenter, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.oldRot[i], vector, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 center = base.NPC.Center - screenPos;
		float telegraphDuration = ((Main.npc[(int)base.NPC.ai[2]].localAI[1] == 1f) ? 108f : 216f);
		if (base.NPC.Calamity().newAI[2] < telegraphDuration && AIState == 1f)
		{
			spriteBatch.EnterShaderRegion();
			Color outlineColor = Color.Lerp(Color.Yellow, Color.White, base.NPC.Calamity().newAI[2] / telegraphDuration);
			Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
			float outlineThickness = MathHelper.Clamp(base.NPC.Calamity().newAI[2] / telegraphDuration * 4f, 0f, 3f);
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
				afterimageCenter2 -= new Vector2((float)glowTexture.Width, (float)glowTexture.Height) / new Vector2(9f, 12f) * base.NPC.scale / 2f;
				afterimageCenter2 += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(glowTexture, afterimageCenter2, (Rectangle?)base.NPC.frame, afterimageColor2, base.NPC.oldRot[j], vector, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture, center, (Rectangle?)frame, afterimageBaseColor * base.NPC.Opacity, base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (base.NPC.Calamity().newAI[2] < telegraphDuration && AIState == 1f)
		{
			float pulseRatio = base.NPC.Calamity().newAI[2] % (telegraphDuration / 5f) / (telegraphDuration / 5f);
			float pulseSize = MathHelper.Lerp(0.1f, 0.6f, (float)Math.Floor(base.NPC.Calamity().newAI[2] / (telegraphDuration / 5f)) / 4f);
			float pulseOpacity = MathHelper.Clamp((float)Math.Floor(base.NPC.Calamity().newAI[2] / (telegraphDuration / 5f)) * 0.3f, 1f, 2f);
			spriteBatch.Draw(texture, center, (Rectangle?)frame, Color.Yellow * MathHelper.Lerp(1f, 0f, pulseRatio) * pulseOpacity, base.NPC.rotation, vector, base.NPC.scale + pulseRatio * pulseSize, spriteEffects, 0f);
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
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresGaussNuke1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresGaussNuke2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresGaussNuke3").Type);
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

	public AresGaussNuke()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);
		EnergyDrawer = new AresCannonChargeParticleSet(-1, 15, 40f, Color.Yellow);
		base._002Ector();
	}
}
