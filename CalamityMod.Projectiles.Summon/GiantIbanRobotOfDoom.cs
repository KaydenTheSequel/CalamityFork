using System;
using CalamityMod.Buffs.Mounts;
using CalamityMod.Projectiles.Summon.AndromedaUI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GiantIbanRobotOfDoom : ModProjectile, ILocalizedModType, IModType
{
	public int FrameX;

	public int FrameY;

	public bool LeftBracketActive;

	public bool RightBracketActive = true;

	public bool BottomBracketActive;

	public bool LeftIconActive;

	public bool TopIconActive;

	public int RightIconCooldown;

	public const int RightIconAttackTime = 480;

	public const int RightIconCooldownMax = 960;

	public const float RightIconLungeSpeed = 28f;

	public int LaserCooldown;

	public const int LaserBaseDamage = 4200;

	public const int RegicideBaseDamageSmall = 1897;

	public const int RegicideBaseDamageLarge = 5200;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int CurrentFrame
	{
		get
		{
			return FrameY + FrameX * 7;
		}
		set
		{
			FrameX = value / 7;
			FrameY = value % 7;
		}
	}

	public float ClickCooldown
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public bool RightIconActive
	{
		get
		{
			if (!RightBracketActive)
			{
				return BottomBracketActive;
			}
			return true;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 152;
		base.Projectile.height = 212;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 18000;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		Player player = Main.player[base.Projectile.owner];
		HandleCooldowns(player);
		ManipulatePlayerValues(player);
		RegisterRightClick(player);
		SetFrames(player);
		SetSpriteDirection(player);
		player.AddBuff(LeftIconActive ? ModContent.BuffType<AndromedaSmallBuff>() : ModContent.BuffType<AndromedaBuff>(), 2);
	}

	public void HandleCooldowns(Player player)
	{
		if (RightIconCooldown > 0 && RightIconActive)
		{
			RightIconCooldown--;
		}
		if (LaserCooldown > 0)
		{
			LaserCooldown--;
			FireLaserBeam(player);
		}
	}

	public void ManipulatePlayerValues(Player player)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		if (!player.active || player.dead)
		{
			player.Calamity().andromedaState = AndromedaPlayerState.Inactive;
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = player.Center + Vector2.UnitY * (6f + player.gfxOffY);
		player.Calamity().andromedaState = (LeftIconActive ? AndromedaPlayerState.SmallRobot : AndromedaPlayerState.LargeRobot);
		player.channel = false;
		if (RightIconCooldown > 480)
		{
			player.Calamity().andromedaState = AndromedaPlayerState.SpecialAttack;
			for (int i = 0; i < 4; i++)
			{
				Vector2 spinningPoint = Utils.RotatedBy(new Vector2(0f, -28f), (double)((float)RightIconCooldown / 60f * ((float)Math.PI * 2f)), default(Vector2)).RotatedBy(base.Projectile.velocity.ToRotation()).RotatedBy(MathHelper.Lerp(MathHelper.ToRadians(-40f), MathHelper.ToRadians(40f), (float)i / 4f));
				Vector2 center = player.Center + Utils.RotatedBy(new Vector2(6f, -2f), (double)player.velocity.ToRotation(), default(Vector2));
				int idx = Dust.NewDust(center, 0, 0, 226, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].position = center + spinningPoint;
				Main.dust[idx].velocity = Vector2.Zero;
				spinningPoint *= -1f;
				idx = Dust.NewDust(center, 0, 0, 226, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].position = center + spinningPoint;
				Main.dust[idx].velocity = Vector2.Zero;
			}
			if ((player.velocity.X == 0f || player.velocity.Y == 0f) && (float)RightIconCooldown < 930f)
			{
				ExitChargeModeEarly(player);
			}
			player.velocity = Vector2.Lerp(player.velocity, base.Projectile.SafeDirectionTo(Main.MouseWorld, Vector2.UnitY) * 28f, 0.225f);
			base.Projectile.rotation = player.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		if (player.mount != null)
		{
			player.mount.Dismount(player);
		}
	}

	public void RegisterRightClick(Player player)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		if (Main.mouseRight && ClickCooldown <= 0f)
		{
			ClickCooldown = 30f;
			if (RightIconCooldown > 480)
			{
				ExitChargeModeEarly(player);
			}
			else if (player.ownedProjectileCounts[ModContent.ProjectileType<AndromedaUI_Background>()] > 0)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if (p.type == ModContent.ProjectileType<AndromedaUI_Background>() && p.owner == player.whoAmI)
					{
						p.Kill();
					}
				}
			}
			else if (Main.myPlayer == player.whoAmI)
			{
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AndromedaUI_Background>(), 0, 0f, player.whoAmI);
				projectile.localAI[0] = Projectile.GetByUUID(base.Projectile.owner, base.Projectile.whoAmI);
				projectile.netUpdate = true;
			}
		}
		else if (ClickCooldown > 0f)
		{
			ClickCooldown--;
		}
	}

	public void SetFrames(Player player)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (RightIconCooldown <= 480)
		{
			if (Math.Abs(player.velocity.Y) != 0f)
			{
				SetFlyingFrames(player);
			}
			else if (Math.Abs(player.velocity.X) > 3f)
			{
				SetWalkingFrames();
			}
			if (player.velocity == Vector2.Zero)
			{
				CurrentFrame = 0;
			}
		}
	}

	public void SetFlyingFrames(Player player)
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		if (player.velocity.Y > 0f)
		{
			CurrentFrame = 1;
			return;
		}
		if (CurrentFrame == 0)
		{
			if (base.Projectile.frameCounter >= 4)
			{
				CurrentFrame++;
				base.Projectile.frameCounter = 0;
			}
		}
		else if (base.Projectile.frameCounter % 4 == 3)
		{
			CurrentFrame++;
			if (CurrentFrame >= 6)
			{
				CurrentFrame = 2;
			}
		}
		Vector2 dustOffset = default(Vector2);
		((Vector2)(ref dustOffset))._002Ector(94f, 58f);
		if (base.Projectile.spriteDirection == -1)
		{
			dustOffset.X = 214f - dustOffset.X;
		}
		if (!Main.dedServ && player.Calamity().andromedaState == AndromedaPlayerState.LargeRobot)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.position + dustOffset, 263);
				dust.velocity = Vector2.Normalize(dust.position - base.Projectile.Top).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(4f, 7f) + base.Projectile.velocity;
				dust.color = Color.SkyBlue;
				dust.scale = Main.rand.NextFloat(0.9f, 1.35f);
				dust.noGravity = true;
			}
		}
		if (CurrentFrame >= 6)
		{
			CurrentFrame = 0;
		}
	}

	public void SetWalkingFrames()
	{
		Player player = Main.player[base.Projectile.owner];
		int walkInterval = (int)MathHelper.Clamp(8f - Math.Abs(player.velocity.X) / 3.5f, 1f, 8f);
		if (player.velocity.X == 0f)
		{
			CurrentFrame = 0;
		}
		else if (base.Projectile.frameCounter >= walkInterval)
		{
			base.Projectile.frameCounter = 0;
			CurrentFrame++;
			if (CurrentFrame >= 13)
			{
				CurrentFrame = 6;
			}
		}
		if (CurrentFrame >= 14 || CurrentFrame <= 6)
		{
			CurrentFrame = 6;
		}
	}

	public void SetSpriteDirection(Player player)
	{
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		if (RightIconCooldown > 480)
		{
			base.Projectile.spriteDirection = (Math.Cos(base.Projectile.rotation) > 0.0).ToDirectionInt();
			return;
		}
		if (player.velocity.X != 0f)
		{
			base.Projectile.spriteDirection = (player.velocity.X > 0f).ToDirectionInt();
		}
		int slashIndex = -1;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<AndromedaRegislash>() && p.owner == base.Projectile.owner)
			{
				slashIndex = p.whoAmI;
				break;
			}
		}
		if (slashIndex != -1 && Main.projectile[slashIndex].frameCounter > 0)
		{
			base.Projectile.spriteDirection = (Math.Cos(Main.projectile[slashIndex].rotation) > 0.0).ToDirectionInt();
		}
		int laserBeamIndex = -1;
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Projectile p2 = enumerator2.Current;
			if (p2.type == ModContent.ProjectileType<AndromedaDeathRay>() && p2.owner == base.Projectile.owner)
			{
				laserBeamIndex = p2.whoAmI;
				break;
			}
		}
		if (laserBeamIndex != -1)
		{
			base.Projectile.spriteDirection = (Math.Cos(Main.projectile[laserBeamIndex].velocity.ToRotation()) > 0.0).ToDirectionInt();
		}
	}

	public void FireLaserBeam(Player player)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if (LaserCooldown % 15 == 14 && base.Projectile.owner == Main.myPlayer)
		{
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/MechGaussRifle"), base.Projectile.Center);
			int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(4200f);
			Vector2 laserVelocity = (Main.MouseWorld - (Main.player[base.Projectile.owner].Center + new Vector2((base.Projectile.spriteDirection == 1) ? 48f : 22f, -28f))).SafeNormalize(Vector2.UnitX * (float)base.Projectile.spriteDirection);
			Projectile deathLaser = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, laserVelocity, ModContent.ProjectileType<AndromedaDeathRay>(), damage, 8f, base.Projectile.owner, base.Projectile.whoAmI);
			deathLaser.originalDamage = 4200;
			if (player.HeldItem != null && deathLaser.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				deathLaser.DamageType = DamageClass.Summon;
			}
		}
	}

	public void ExitChargeModeEarly(Player player)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		RightIconCooldown = 480;
		SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/MechGaussRifle"), base.Projectile.Center);
		SpecialAttackExplosionDust(player);
	}

	public void SpecialAttackExplosionDust(Player player)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int angleInterval = 0; angleInterval < 10; angleInterval++)
		{
			for (int outwardness = 190; outwardness < 360; outwardness += 8)
			{
				for (int speedSign = -1; speedSign <= 1; speedSign += 2)
				{
					float angle = MathHelper.Lerp(0f, (float)Math.PI / 5f, ((float)outwardness - 190f) / 170f);
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 221);
					dust.noGravity = true;
					dust.scale = 1.6f;
					dust.position = player.Center + (float)outwardness * ((float)Math.PI / 5f * (float)angleInterval).ToRotationVector2().RotatedBy(angle);
					dust.velocity = player.SafeDirectionTo(dust.position) * 8f * (float)speedSign;
					dust = Dust.NewDustPerfect(base.Projectile.Center, 221);
					dust.noGravity = true;
					dust.scale = 1.6f;
					dust.position = player.Center + (float)outwardness * ((float)Math.PI / 5f * (float)angleInterval).ToRotationVector2().RotatedBy(0f - angle);
					dust.velocity = player.SafeDirectionTo(dust.position) * 8f * (float)speedSign;
				}
			}
		}
		int pointsOnStar = 6;
		for (int k = 0; k < 2; k++)
		{
			for (int i = 0; i < pointsOnStar; i++)
			{
				float f = 4.712389f - (float)i * ((float)Math.PI * 2f) / (float)pointsOnStar;
				float nextAngle = 4.712389f - (float)((i + 3) % pointsOnStar) * ((float)Math.PI * 2f) / (float)pointsOnStar;
				if (k == 1)
				{
					nextAngle = 4.712389f - (float)(i + 2) * ((float)Math.PI * 2f) / (float)pointsOnStar;
				}
				Vector2 start = f.ToRotationVector2();
				Vector2 end = nextAngle.ToRotationVector2();
				int pointsOnStarSegment = 24;
				for (int j = 0; j < pointsOnStarSegment; j++)
				{
					Dust dust2 = Dust.NewDustPerfect(player.Center, 221);
					dust2.noGravity = true;
					dust2.scale = 1.9f;
					dust2.velocity = Vector2.Lerp(start, end, (float)j / (float)pointsOnStarSegment) * 13f * new Vector2(1.414f, 1f);
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		Player obj = Main.player[base.Projectile.owner];
		obj.width = 20;
		obj.height = 42;
	}
}
