using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Potions.Food;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Other;

[AutoloadBossHead]
public class THELORDE : ModNPC
{
	public int aiSwitchCounter = 420;

	public int deathrayCounter;

	public int invincibleCounter;

	public int squintTimer;

	public int cutsceneAnimation = -1;

	public int frameToUse;

	public bool ajitPaiDidNothingWrong;

	public bool canDespawn;

	public bool urAMemeNow;

	public bool hasBeenPlantera;

	public bool hasBeenGolem;

	public bool Dying;

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/Lordeath");

	public static Asset<Texture2D> DeathAnimationTexture;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		NPCID.Sets.ImmuneToRegularBuffs[base.Type] = true;
		NPCID.Sets.ShouldBeCountedAsBoss[base.Type] = true;
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 7;
		if (!Main.dedServ)
		{
			DeathAnimationTexture = ModContent.Request<Texture2D>(Texture + "DEATH", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.NPC.damage = 69;
		base.NPC.width = 200;
		base.NPC.height = 200;
		base.NPC.defense = 100;
		base.NPC.lifeMax = 2500000;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(100);
		base.NPC.HitSound = SoundID.NPCHit13;
		base.NPC.DeathSound = null;
		base.NPC.boss = true;
		base.Music = 38;
		base.NPC.Calamity().canBreakPlayerDefense = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write(ajitPaiDidNothingWrong);
		writer.Write(canDespawn);
		writer.Write(urAMemeNow);
		writer.Write(hasBeenPlantera);
		writer.Write(hasBeenGolem);
		writer.Write(Dying);
		writer.Write(invincibleCounter);
		writer.Write(aiSwitchCounter);
		writer.Write(deathrayCounter);
		writer.Write(squintTimer);
		writer.Write(cutsceneAnimation);
		writer.Write(frameToUse);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		ajitPaiDidNothingWrong = reader.ReadBoolean();
		canDespawn = reader.ReadBoolean();
		urAMemeNow = reader.ReadBoolean();
		hasBeenPlantera = reader.ReadBoolean();
		hasBeenGolem = reader.ReadBoolean();
		Dying = reader.ReadBoolean();
		invincibleCounter = reader.ReadInt32();
		aiSwitchCounter = reader.ReadInt32();
		deathrayCounter = reader.ReadInt32();
		squintTimer = reader.ReadInt32();
		cutsceneAnimation = reader.ReadInt32();
		frameToUse = reader.ReadInt32();
	}

	public override void OnSpawn(IEntitySource source)
	{
		Projectile[] projectile = Main.projectile;
		foreach (Projectile proj in projectile)
		{
			if (proj.active && proj != null && !proj.hostile)
			{
				proj.active = false;
			}
		}
	}

	public override void AI()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f38: Unknown result type (might be due to invalid IL or missing references)
		aiSwitchCounter++;
		if (ajitPaiDidNothingWrong && invincibleCounter < 6000)
		{
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = true;
			invincibleCounter++;
		}
		base.NPC.alpha -= 100;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (Dying)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.9f;
			base.NPC.rotation = MathHelper.Lerp(base.NPC.rotation, 0f, 1f);
			if (frameToUse >= 18)
			{
				base.NPC.active = false;
				base.NPC.HitEffect();
				base.NPC.NPCLoot();
				SoundStyle style = DeathSound with
				{
					Volume = 2f
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				base.NPC.netUpdate = true;
			}
			return;
		}
		if (base.NPC.life <= 1 && invincibleCounter >= 6000)
		{
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = false;
			base.NPC.life = 1;
			if (!Dying)
			{
				frameToUse = 0;
				Dying = true;
			}
			base.NPC.dontTakeDamage = true;
			base.NPC.netUpdate = true;
			return;
		}
		if (Main.rand.NextBool(50))
		{
			SoundStyle style = global::CalamityMod.NPCs.Polterghast.Polterghast.creepySounds[Main.rand.Next(1, global::CalamityMod.NPCs.Polterghast.Polterghast.creepySounds.Count)]with
			{
				PitchVariance = 2f
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
		}
		Player playerLOL = Main.player[base.NPC.target];
		playerLOL.velocity.X *= 0.99f;
		playerLOL.velocity.Y *= 0.99f;
		if (!DownedBossSystem.downedCalamitas && !DownedBossSystem.downedExoMechs && !Main.dedServ && !playerLOL.dead && playerLOL.active)
		{
			playerLOL.AddBuff(ModContent.BuffType<NOU>(), 2);
		}
		if (playerLOL.mount.Active)
		{
			playerLOL.mount.Dismount(playerLOL);
		}
		if (!playerLOL.active || playerLOL.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			playerLOL = Main.player[base.NPC.target];
			if (!playerLOL.active || playerLOL.dead)
			{
				canDespawn = true;
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
			}
		}
		else
		{
			canDespawn = false;
		}
		if (urAMemeNow)
		{
			base.NPC.rotation += 5f;
			base.NPC.TargetClosest();
			Vector2 lordePosition = default(Vector2);
			((Vector2)(ref lordePosition))._002Ector(base.NPC.Center.X + (float)(base.NPC.direction * 20), base.NPC.Center.Y + 6f);
			float targetXDist = playerLOL.position.X + (float)playerLOL.width * 0.5f - lordePosition.X;
			float targetYDist = playerLOL.Center.Y - lordePosition.Y;
			float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
			float velocityMult = 69f / targetDistance;
			targetXDist *= velocityMult;
			targetYDist *= velocityMult;
			base.NPC.ai[0]--;
			if (targetDistance < 50f || base.NPC.ai[0] > 0f)
			{
				if (targetDistance < 50f)
				{
					base.NPC.ai[0] = 20f;
				}
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				return;
			}
			base.NPC.velocity.X = (base.NPC.velocity.X * 50f + targetXDist) / 51f;
			base.NPC.velocity.Y = (base.NPC.velocity.Y * 50f + targetYDist) / 51f;
			if (targetDistance < 150f)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 10f + targetXDist) / 11f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 10f + targetYDist) / 11f;
			}
			if (targetDistance < 100f)
			{
				base.NPC.velocity.X = (base.NPC.velocity.X * 7f + targetXDist) / 8f;
				base.NPC.velocity.Y = (base.NPC.velocity.Y * 7f + targetYDist) / 8f;
			}
			return;
		}
		if (aiSwitchCounter >= 600)
		{
			int aiChoice = 1;
			switch (Main.rand.Next(33))
			{
			case 0:
				aiChoice = 1;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 1:
				aiChoice = 5;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 2:
				aiChoice = 8;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 3:
				aiChoice = 10;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 4:
				aiChoice = 14;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 5:
				aiChoice = 15;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 420;
				break;
			case 6:
				aiChoice = 17;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 7:
				aiChoice = 19;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 8:
				aiChoice = 22;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 9:
				aiChoice = 23;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 10:
				aiChoice = 25;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 11:
				aiChoice = 26;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 13:
				aiChoice = 41;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 14:
				aiChoice = 43;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			case 15:
				aiChoice = 44;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 16:
				aiChoice = 49;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 17:
				aiChoice = 2;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 18:
				aiChoice = 56;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 19:
				aiChoice = 69;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 0;
				break;
			case 20:
				aiChoice = 85;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 21:
				aiChoice = 91;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 22:
				aiChoice = 96;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 23:
				aiChoice = 97;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 0;
				break;
			case 24:
				aiChoice = 6;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 480;
				break;
			case 25:
				aiChoice = 3;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 26:
				aiChoice = 4;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			case 27:
				aiChoice = 11;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			case 28:
				aiChoice = 16;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 480;
				break;
			case 29:
				aiChoice = 30;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			case 30:
				aiChoice = 31;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			case 31:
				aiChoice = 32;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			case 32:
				aiChoice = 121;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 420;
				break;
			}
			if (Main.rand.NextBool(5) && !hasBeenPlantera)
			{
				aiChoice = 51;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				aiSwitchCounter = 0;
				hasBeenPlantera = true;
			}
			if (Main.rand.NextBool(5) && !hasBeenGolem)
			{
				aiChoice = 45;
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				aiSwitchCounter = 0;
				hasBeenGolem = true;
			}
			if (Vector2.Distance(playerLOL.Center, base.NPC.Center) > 4200f)
			{
				urAMemeNow = true;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
			}
			base.NPC.knockBackResist = 0f;
			base.NPC.dontTakeDamage = false;
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[1] = 0f;
			base.NPC.localAI[2] = 0f;
			base.NPC.localAI[3] = 0f;
			base.NPC.ai[0] = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			base.NPC.aiStyle = (urAMemeNow ? (-1) : aiChoice);
			base.NPC.netUpdate = true;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.type == ModContent.ProjectileType<AresDeathBeamStart>() || proj.type == ModContent.ProjectileType<AresDeathBeamTelegraph>())
				{
					proj.Kill();
				}
			}
			deathrayCounter = 0;
		}
		int totalProjectiles = 8;
		float radians = (float)Math.PI * 2f / (float)totalProjectiles;
		float velocity = 6f;
		double angleA = (double)radians * 0.5;
		double angleB = (double)MathHelper.ToRadians(90f) - angleA;
		float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
		Vector2 spinningPoint = default(Vector2);
		((Vector2)(ref spinningPoint))._002Ector(0f - velocityX2, 0f - velocity);
		((Vector2)(ref spinningPoint)).Normalize();
		base.NPC.Calamity().newAI[2] = 200f;
		if (!playerLOL.Calamity().lordePet && !ajitPaiDidNothingWrong)
		{
			return;
		}
		if (deathrayCounter == 0)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.NPC.Center);
			int type = ModContent.ProjectileType<AresDeathBeamTelegraph>();
			Vector2 spawnPoint = base.NPC.Center + new Vector2(-1f, 23f);
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 laserVelocity = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPoint + Vector2.Normalize(laserVelocity) * 17f, laserVelocity, type, 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
			}
		}
		if (deathrayCounter == 100)
		{
			SoundEngine.PlaySound(in AresBody.LaserStartSound, base.NPC.Center);
			if (Main.netMode != 1)
			{
				int type2 = ModContent.ProjectileType<AresDeathBeamStart>();
				int damage = 69;
				Vector2 spawnPoint2 = base.NPC.Center + new Vector2(-1f, 23f);
				for (int i = 0; i < 8; i++)
				{
					Vector2 laserVelocity2 = spinningPoint.RotatedBy(radians * (float)i);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPoint2 + Vector2.Normalize(laserVelocity2) * 35f, laserVelocity2, type2, damage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
				}
			}
		}
		deathrayCounter++;
		if (deathrayCounter > 600)
		{
			deathrayCounter = 0;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (Dying)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 4.0 && frameToUse < 18)
			{
				frameToUse++;
				base.NPC.frameCounter = 0.0;
			}
			return;
		}
		if (squintTimer > 0)
		{
			squintTimer--;
		}
		if (cutsceneAnimation > 0)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 6.0)
			{
				frameToUse++;
			}
			if (frameToUse > 13 || frameToUse < 6)
			{
				frameToUse = 6;
			}
			cutsceneAnimation--;
		}
		else
		{
			frameToUse = 0;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Rectangle frameUsed = texture.Frame(2, 7, 0, 1);
		Rectangle squintFrame = texture.Frame(2, 7);
		int columnAmount = (Dying ? 3 : 2);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(texture.Width / (2 * columnAmount)), (float)(texture.Height / 14));
		Vector2 npcOffset = base.NPC.Center - screenPos;
		npcOffset -= new Vector2((float)texture.Width / (float)columnAmount, (float)texture.Height / 7f) * base.NPC.scale / 2f;
		npcOffset += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		if (Dying)
		{
			texture = DeathAnimationTexture.Value;
			int xFrame = 0;
			int yFrame = frameToUse;
			if (frameToUse > 13)
			{
				xFrame = 2;
				yFrame = frameToUse - 12;
			}
			else if (frameToUse > 6)
			{
				xFrame = 1;
				yFrame = frameToUse - 6;
			}
			frameUsed = texture.Frame(3, 7, xFrame, yFrame);
		}
		else if (cutsceneAnimation > 0)
		{
			int xFrame2 = 0;
			int yFrame2 = 6;
			if (frameToUse > 6)
			{
				xFrame2 = 1;
				yFrame2 = frameToUse - 6;
			}
			frameUsed = texture.Frame(2, 7, xFrame2, yFrame2);
		}
		else if (squintTimer > 0)
		{
			frameUsed = squintFrame;
		}
		spriteBatch.Draw(texture, npcOffset, (Rectangle?)frameUsed, drawColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		modifiers.SetMaxDamage(base.NPC.life - 1);
		if ((double)base.NPC.life <= (double)base.NPC.lifeMax * 0.009999999776482582 && invincibleCounter == 0 && !ajitPaiDidNothingWrong)
		{
			cutsceneAnimation = 240;
			ajitPaiDidNothingWrong = true;
			modifiers.SetMaxDamage(1);
			base.NPC.life++;
		}
	}

	public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		int antiButcherLimit = base.NPC.lifeMax / 250;
		if ((int)(1.05f * (float)modifiers.GetDamage(item.damage, crit: true)) > antiButcherLimit)
		{
			Color messageColor = Color.Cyan;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.EdgyBossText8", messageColor);
			modifiers.SetMaxDamage(1);
			base.NPC.life++;
			squintTimer = 120;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		int antiButcherLimit = base.NPC.lifeMax / 250;
		if ((int)(1.05f * (float)modifiers.GetDamage(projectile.damage, crit: true)) > antiButcherLimit)
		{
			Color messageColor = Color.Cyan;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.EdgyBossText8", messageColor);
			modifiers.SetMaxDamage(1);
			base.NPC.life++;
			squintTimer = 120;
		}
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		if (!ajitPaiDidNothingWrong || invincibleCounter >= 6000)
		{
			return null;
		}
		return false;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (!ajitPaiDidNothingWrong || invincibleCounter >= 6000)
		{
			return null;
		}
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void OnHitPlayer(Player player, Player.HurtInfo hitinfo)
	{
		player.AddBuff(ModContent.BuffType<NOU>(), 1337);
	}

	public override bool CheckActive()
	{
		return canDespawn;
	}

	public override bool CheckDead()
	{
		base.NPC.life = 1;
		if (!Dying && invincibleCounter >= 6000)
		{
			frameToUse = 0;
			Dying = true;
		}
		base.NPC.active = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.netUpdate = true;
		return false;
	}

	public override void OnKill()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1)
		{
			Vector2 speed = default(Vector2);
			for (int i = 0; i < 2; i++)
			{
				((Vector2)(ref speed))._002Ector(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f));
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, speed, ModContent.ProjectileType<GooglyEye>(), 0, 0f, Main.myPlayer);
			}
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float bossLifeScale, float anotherthing)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.5f * bossLifeScale);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.5f);
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		double pisquaredover6 = Math.Pow(3.1415927410125732, 2.0) / 6.0;
		npcLoot.Add(ModContent.ItemType<SuspiciousLookingNOU>());
		npcLoot.Add(ModContent.ItemType<DeliciousMeat>(), 1, 22, (int)(pisquaredover6 * 100.0));
	}
}
