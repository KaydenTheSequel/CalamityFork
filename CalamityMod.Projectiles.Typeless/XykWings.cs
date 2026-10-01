using System;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class XykWings : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int flightTimer;

	public float wingFlapHeight;

	public Vector2 expectedWingPosition1;

	public Vector2 expectedWingPosition2;

	public Vector2 WingVel1;

	public Vector2 WingVel2;

	public float backWingRot;

	public int lastDir;

	public float spawnFade;

	public int deathFadeTimer;

	public bool iAmTopWing;

	public Vector2 playerCenterPoint;

	public int groundTime;

	public int dashTimer;

	public bool doDashEffect = true;

	public float dashfx;

	public int timeFalling;

	public bool BreakApart;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public Color drawColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Calamity().XykFXColor;
		}
	}

	public float spawnFadePow
	{
		get
		{
			if (base.Projectile.ai[1] != 5f)
			{
				return (float)Math.Pow(spawnFade, 4.0);
			}
			return (float)Math.Pow(spawnFade, 1.0);
		}
	}

	public ref float wingNum => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 1);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.scale = 0f;
	}

	public override void AI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d90: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff3: Unknown result type (might be due to invalid IL or missing references)
		//IL_100c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1017: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_102a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		float intendedScale = 0.85f;
		float spawnAnimTime = 12f + wingNum;
		if (time == 0)
		{
			expectedWingPosition1 = Owner.Center;
			expectedWingPosition2 = Owner.Center;
			playerCenterPoint = Owner.MountedCenter;
		}
		if ((float)time <= spawnAnimTime)
		{
			spawnFade = Utils.GetLerpValue(0f, spawnAnimTime, time, clamped: true);
			base.Projectile.scale = intendedScale * Math.Min(spawnFade + 0.5f, 1f);
			if ((float)time == spawnAnimTime)
			{
				base.Projectile.scale = 1.6f;
			}
		}
		else if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, intendedScale, 0.08f);
		}
		if (moddedOwner.XykVisualsBlue || moddedOwner.XykVisualsOrange)
		{
			base.Projectile.timeLeft++;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
		}
		playerCenterPoint = Owner.MountedCenter;
		if (Owner.wingTime == (float)Owner.wingTimeMax && Owner.velocity.Y == 0f && Owner.dashDelay != -1)
		{
			groundTime++;
		}
		else
		{
			groundTime = 0;
		}
		if (((groundTime >= 180 || Owner.wingTime <= 0f) && wingNum == (float)(checkActiveWings() - 1)) || base.Projectile.ai[1] == 5f)
		{
			if (base.Projectile.ai[1] == 0f)
			{
				iAmTopWing = true;
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if (p.type == ModContent.ProjectileType<XykWings>() && p.owner == Owner.whoAmI && p.ai[1] == 0f)
					{
						p.ai[1] = 5f;
					}
				}
				return;
			}
			if (deathFadeTimer == 0)
			{
				BreakApart = Owner.wingTime <= 0f;
			}
			if (BreakApart)
			{
				base.Projectile.extraUpdates = 1;
				int startTime = (int)(6f * (wingNum + 1f));
				if (deathFadeTimer == startTime)
				{
					WingVel1 = Vector2.UnitX * (0f - Main.rand.NextFloat(4f, 7f)) * (float)lastDir + Vector2.UnitY.RotatedByRandom(0.30000001192092896) * (0f - Main.rand.NextFloat(5f, 7f));
					WingVel2 = Vector2.UnitX * Main.rand.NextFloat(4f, 7f) * (float)lastDir + Vector2.UnitY.RotatedByRandom(0.30000001192092896) * (0f - Main.rand.NextFloat(5f, 7f));
				}
				if (deathFadeTimer > startTime)
				{
					WingVel1.X *= 0.97f;
					if (WingVel1.Y < 15f)
					{
						WingVel1.Y += 0.2f;
					}
					if (WingVel1.Y < 5f)
					{
						WingVel1.Y *= 0.97f;
					}
					WingVel2.X *= 0.97f;
					if (WingVel2.Y < 15f)
					{
						WingVel2.Y += 0.2f;
					}
					if (WingVel2.Y < 5f)
					{
						WingVel2.Y *= 0.97f;
					}
					expectedWingPosition1 += WingVel1;
					expectedWingPosition2 += WingVel2;
					base.Projectile.rotation += 0.01f + wingNum * 0.005f;
					backWingRot -= 0.01f + wingNum * 0.005f;
				}
				else
				{
					expectedWingPosition1 += Owner.velocity * 0.65f;
					expectedWingPosition2 += Owner.velocity * 0.65f;
				}
			}
			else
			{
				int startTime2 = (int)(2f * wingNum);
				if (deathFadeTimer == startTime2)
				{
					WingVel1 = (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 15f;
					WingVel2 = (backWingRot + (float)Math.PI / 2f).ToRotationVector2() * 7f;
				}
				if (deathFadeTimer > startTime2)
				{
					expectedWingPosition1 += WingVel1;
					expectedWingPosition2 += WingVel2;
					WingVel1 *= 0.955f;
					WingVel2 *= 0.955f;
				}
				else
				{
					expectedWingPosition1 += Owner.velocity * 0.85f;
					expectedWingPosition2 += Owner.velocity * 0.85f;
				}
			}
			dashfx = MathHelper.Lerp(dashfx, 0f, 0.07f);
			deathFadeTimer++;
			int fadeTime = (BreakApart ? 90 : 20);
			spawnFade = Utils.GetLerpValue(fadeTime, 0f, deathFadeTimer, clamped: true);
			base.Projectile.scale = intendedScale * spawnFadePow;
			if (deathFadeTimer >= fadeTime)
			{
				base.Projectile.Kill();
			}
			return;
		}
		bool isFlying = Owner.controlJump;
		bool isFalling = Owner.controlDown && !isFlying && Owner.velocity.Y > 0f;
		bool isUpBoosting = Owner.wingTime > 0f && Owner.controlJump && Owner.controlUp;
		bool isHovering = Owner.wingTime > 0f && Owner.controlDown && Owner.controlJump && !isUpBoosting;
		if (isFalling || timeFalling < 0)
		{
			timeFalling++;
		}
		else if (timeFalling > 0)
		{
			timeFalling -= 3;
		}
		float fallingLerp = (float)Math.Pow(Utils.GetLerpValue(0f, 40f, timeFalling, clamped: true), 3.0);
		float topWingNum = checkActiveWings() - 1;
		float sine = (float)Math.Sin(((float)time * 0.35f * (isFalling ? 3f : ((isHovering | isUpBoosting) ? 3.2f : (isFlying ? 2f : 1f))) + wingNum * ((isHovering | isUpBoosting) ? 3f : 2f)) / (float)Math.PI);
		if (((Owner.dashDelay == -1) | isUpBoosting | isHovering) || Owner.Calamity().adrenalineModeActive || Owner.Calamity().rageModeActive || (isFalling && Owner.velocity.Y > 16f))
		{
			dashfx = MathHelper.Lerp(dashfx, 1f, 0.25f);
			float bonusScale = 1f;
			if (Owner.Calamity().rageModeActive)
			{
				bonusScale += 0.25f;
			}
			if (Owner.Calamity().adrenalineModeActive)
			{
				bonusScale += 0.5f;
			}
			if (base.Projectile.scale < bonusScale)
			{
				base.Projectile.scale = bonusScale;
			}
		}
		else
		{
			dashfx = MathHelper.Lerp(dashfx, 0f, 0.03f);
			if (dashfx > 0f)
			{
				dashfx -= 0.01f;
			}
			else
			{
				dashfx = 0f;
			}
		}
		float flapSpeed = MathHelper.Lerp(0.06f * (isHovering ? 2.5f : (isFlying ? 2f : 1f)), 0.55f * (isHovering ? 2.5f : (isFlying ? 2f : 1f)), (float)Math.Pow(Utils.GetLerpValue(-1f, 1f, sine), 3.0));
		wingFlapHeight = MathHelper.Lerp(wingFlapHeight, sine, flapSpeed) * spawnFadePow;
		for (int i = -1; i <= 1; i += 2)
		{
			float dir = i * lastDir;
			Vector2 flightMovement = Vector2.UnitY.RotatedBy(isFalling ? (MathHelper.ToRadians(13f) * dir * fallingLerp) : 0f) * (-15f * base.Projectile.scale + 10f * (isFalling ? 0.35f : (isUpBoosting ? 5f : (isHovering ? 1.5f : (isFlying ? 2.5f : 1f)))) * wingFlapHeight) * (float)((Owner.velocity.Y != 0f) ? 1 : 0);
			float wingPartDist = 1.6f / (float)(checkActiveWings() + 1) * wingNum * (isFalling ? (1f - 0.3f * fallingLerp) : (isHovering ? 0.5f : 1f));
			Vector2 destination = playerCenterPoint - (Vector2.UnitX.RotatedBy(isFalling ? (MathHelper.ToRadians(13f) * dir * fallingLerp) : 0f) * (22f + 7f * wingFlapHeight * ((i == -1) ? 0.5f : 1f)) * base.Projectile.scale).RotatedBy((-0.8f + (isFalling ? (0.7f * fallingLerp) : 0f) + wingPartDist + ((wingNum == topWingNum) ? 0.1f : ((wingNum == 0f) ? (-0.1f) : 0f))) * dir) * dir + (Vector2.One * (float)((!isFlying) ? 1 : 0)).RotatedBy(((float)time * 0.12f + wingNum * 0.9f) * dir) + flightMovement;
			float lerpSpeed = 1f + wingNum * (0.1f + 0.15f * fallingLerp) / (Owner.moveSpeed * 0.3f + 1f);
			float spawnDistance = 150f;
			if (i == 1)
			{
				expectedWingPosition1 += (destination + destination.DirectionFrom(playerCenterPoint) * Math.Max(1f, spawnDistance * (1f - spawnFadePow)) - expectedWingPosition1) / lerpSpeed;
			}
			else
			{
				expectedWingPosition2 += (destination + Vector2.UnitX * -6f * (float)lastDir + destination.DirectionFrom(playerCenterPoint) * Math.Max(1f, spawnDistance * (1f - spawnFadePow)) - expectedWingPosition2) / lerpSpeed;
			}
		}
		base.Projectile.Center = Owner.Center;
		base.Projectile.rotation = expectedWingPosition1.DirectionFrom(playerCenterPoint).ToRotation() - (float)Math.PI / 2f;
		backWingRot = expectedWingPosition2.DirectionFrom(playerCenterPoint).ToRotation() - (float)Math.PI / 2f;
		Color newColor;
		if (wingNum == 0f)
		{
			Vector2 center = Owner.Center;
			newColor = Color.Lerp(drawColor, Color.White, 0.5f);
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 1.1f * spawnFadePow);
		}
		if (dashfx > 0.3f && (wingNum == (float)(checkActiveWings() - 1) || wingNum == 0f))
		{
			for (int j = -2; j <= 2; j++)
			{
				if (j == 0)
				{
					j++;
				}
				bool front = j > 0;
				Vector2 pos1 = expectedWingPosition1 + (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (45f + 5f * wingNum) * (base.Projectile.scale + 0.15f);
				Vector2 pos2 = expectedWingPosition2 + (backWingRot + (float)Math.PI / 2f).ToRotationVector2() * (15f + 3f * wingNum) * (base.Projectile.scale + 0.15f);
				if (Owner.Calamity().XykVisualsBlue)
				{
					bool circle = Main.rand.NextBool(3);
					Vector2 position = (front ? pos1 : pos2);
					int type = (circle ? ModContent.DustType<SquashDustHollow>() : ModContent.DustType<SquashDust>());
					Vector2? velocity = Owner.velocity.RotatedByRandom(circle ? 0.5f : 0f) * Main.rand.NextFloat(-0.5f, -0.2f) * dashfx;
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
					dust.scale = Main.rand.NextFloat(1.3f, 1.4f) * (front ? 1f : 0.6f) * dashfx * (circle ? 0.6f : 1f) * (float)Math.Pow(base.Projectile.scale, 0.4000000059604645);
					dust.noGravity = true;
					dust.color = drawColor;
					dust.noLightEmittence = true;
					dust.fadeIn = 1.5f * (circle ? 0f : 1f);
				}
				else
				{
					bool square = Main.rand.NextBool(3);
					Vector2 position2 = (front ? pos1 : pos2);
					int type2 = (square ? ModContent.DustType<SquareDust>() : ModContent.DustType<SquashDust>());
					Vector2? velocity2 = Owner.velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(-0.5f, -0.2f) * dashfx;
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity2, 0, newColor);
					dust2.scale = Main.rand.NextFloat(0.8f, 1f) * (front ? 1f : 0.6f) * dashfx * (square ? 1f : 1.5f) * (float)Math.Pow(base.Projectile.scale, 0.4000000059604645);
					dust2.noGravity = true;
					dust2.color = drawColor;
					dust2.noLightEmittence = true;
					dust2.fadeIn = 0.1f * (square ? 1f : 3f);
				}
			}
		}
		time++;
	}

	public override void PostDraw(Color lightColor)
	{
		if (!BreakApart)
		{
			lastDir = Owner.direction;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		float topWingNum = checkActiveWings() - 1;
		float width = 1f;
		float height = 1f;
		Texture2D tex;
		if (wingNum == 0f)
		{
			tex = (moddedOwner.XykVisualsOrange ? ModContent.Request<Texture2D>("CalamityMod/Particles/XykWingOrange2", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Particles/XykWingBlue2", (AssetRequestMode)2).Value);
		}
		else if (wingNum == topWingNum || iAmTopWing)
		{
			height = 1.5f;
			tex = (moddedOwner.XykVisualsOrange ? ModContent.Request<Texture2D>("CalamityMod/Particles/XykWingOrange1", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Particles/XykWingBlue1", (AssetRequestMode)2).Value);
		}
		else
		{
			width = 2f;
			height = 1f + 0.1f * wingNum;
			tex = (moddedOwner.XykVisualsOrange ? ModContent.Request<Texture2D>("CalamityMod/Particles/XykWingOrange3", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Particles/XykWingBlue3", (AssetRequestMode)2).Value);
		}
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 scale = new Vector2(width, height * spawnFadePow) * 0.12f * base.Projectile.scale;
		Vector2 scaleBack = new Vector2(width * 1.5f, height * 0.7f * spawnFadePow) * 0.07f * base.Projectile.scale;
		Color val;
		if (dashfx > 0f)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 position = expectedWingPosition1 - Main.screenPosition;
				val = ((i < 2) ? Color.White : drawColor);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(glow, position, null, val * 0.65f, base.Projectile.rotation + ((i % 2 == 0) ? ((float)Math.PI / 2f) : 0f) + (float)Math.PI / 4f, glow.Size() * 0.5f, new Vector2(0.2f, 1.2f) * ((i < 2) ? 0.7f : 1f) * base.Projectile.scale * (Main.rand.NextFloat(0.25f, 0.35f) + 0.02f * wingNum) * (float)Math.Pow(dashfx, 7.0), (SpriteEffects)0);
				Vector2 position2 = expectedWingPosition2 - Main.screenPosition;
				val = ((i < 2) ? Color.White : drawColor);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(glow, position2, null, val * 0.65f, backWingRot + ((i % 2 == 0) ? ((float)Math.PI / 2f) : 0f) + (float)Math.PI / 4f, glow.Size() * 0.5f, new Vector2(0.2f, 1.2f) * ((i < 2) ? 0.3f : 0.6f) * base.Projectile.scale * (Main.rand.NextFloat(0.25f, 0.35f) + 0.02f * wingNum) * (float)Math.Pow(dashfx, 7.0), (SpriteEffects)0);
			}
			int draws = 12;
			for (int j = 0; j < draws; j++)
			{
				Vector2 addedPos = (((float)Math.PI * 2f * (float)j / (float)draws).ToRotationVector2() * 2.5f + Main.rand.NextVector2Circular(2f, 2f)) * dashfx;
				Texture2D texture = tex;
				Vector2 position3 = expectedWingPosition1 - Main.screenPosition + addedPos;
				val = drawColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, position3, null, val * spawnFadePow * 0.3f * dashfx, base.Projectile.rotation, new Vector2((float)tex.Width * 0.5f, 0f), scale, (SpriteEffects)(lastDir != 1));
				Texture2D texture2 = tex;
				Vector2 position4 = expectedWingPosition2 - Main.screenPosition + addedPos;
				val = drawColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture2, position4, null, val * spawnFadePow * 0.3f * dashfx, backWingRot, new Vector2((float)tex.Width * 0.5f, 0f), scaleBack, (SpriteEffects)(lastDir != -1));
			}
		}
		Texture2D texture3 = tex;
		Vector2 position5 = expectedWingPosition1 - Main.screenPosition;
		val = Color.Lerp(drawColor, Color.White, dashfx);
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(texture3, position5, null, val * spawnFadePow * MathHelper.Lerp(1f, 0.6f, dashfx), base.Projectile.rotation, new Vector2((float)tex.Width * 0.5f, 0f), scale, (SpriteEffects)(lastDir != 1));
		Texture2D texture4 = tex;
		Vector2 position6 = expectedWingPosition2 - Main.screenPosition;
		val = Color.Lerp(drawColor, Color.White, dashfx);
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(texture4, position6, null, val * spawnFadePow * MathHelper.Lerp(1f, 0.6f, dashfx), backWingRot, new Vector2((float)tex.Width * 0.5f, 0f), scaleBack, (SpriteEffects)(lastDir != -1));
		return false;
	}

	public int checkActiveWings()
	{
		int numOfActiveWings = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<XykWings>() && p.owner == Owner.whoAmI && p.ai[1] == 0f)
			{
				numOfActiveWings++;
			}
		}
		if (base.Projectile.ai[1] == 5f)
		{
			numOfActiveWings = 7;
		}
		return numOfActiveWings;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
