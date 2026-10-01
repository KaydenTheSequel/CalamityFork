using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class LiliesOfFinalityElster : BaseMinionProjectile
{
	private enum EyeFrame
	{
		EyeOpen,
		EyeHalfClosed,
		EyeClosed
	}

	private enum EyeState
	{
		NormalBlinking,
		InStorm
	}

	public enum AIState
	{
		Idle,
		ReturnToOwner,
		AttackOnGround,
		AttackFlying
	}

	public enum AnimationState
	{
		Still,
		Walk,
		Fly,
		Shoot,
		FlyShoot
	}

	private EyeState state;

	private int timeInState;

	private bool HasShotOnce;

	private int FrameAmount = 1;

	public int EyeFrameToShow { get; private set; }

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
			HasShotOnce = false;
			base.Projectile.tileCollide = value == AIState.Idle || value == AIState.AttackOnGround;
			base.Projectile.rotation = 0f;
		}
	}

	public AnimationState Animation
	{
		get
		{
			return (AnimationState)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = (float)value;
			switch (value)
			{
			case AnimationState.Still:
				base.Projectile.width = 32;
				base.Projectile.frame = 0;
				FrameAmount = 1;
				break;
			case AnimationState.Walk:
				base.Projectile.width = 32;
				FrameAmount = 9;
				break;
			case AnimationState.Fly:
				base.Projectile.width = 36;
				FrameAmount = 4;
				break;
			case AnimationState.Shoot:
			case AnimationState.FlyShoot:
				base.Projectile.width = 56;
				FrameAmount = 7;
				break;
			}
			if (Animation != value || base.Projectile.frame + 1 > FrameAmount)
			{
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
			}
		}
	}

	private ref float Timer => ref base.Projectile.ai[2];

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<LiliesOfFinalityElster>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<LiliesOfFinalityBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.LiliesOfFinalityBool;

	public override float EnemyDistanceDetection => LiliesOfFinality.MaxEnemyDistanceDetection;

	public override int AnimationFrames => 28;

	public override bool Grounded => true;

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 56;
		base.SetDefaults();
	}

	public override void CheckMinionExistence()
	{
		base.CheckMinionExistence();
		if (Timer > 10f && Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<LiliesOfFinalityAriane>()] == 0)
		{
			base.Projectile.Kill();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (State == AIState.Idle)
		{
			fallThrough = base.Projectile.Bottom.Y < base.Owner.Top.Y;
		}
		return true;
	}

	public override void MinionAI()
	{
		SetStateByPlayerInfo(base.Owner);
		UpdateEyeFrameToShow(base.Owner);
		timeInState++;
		switch (State)
		{
		case AIState.Idle:
			IdleState();
			break;
		case AIState.ReturnToOwner:
			ReturnToOwnerState();
			break;
		case AIState.AttackOnGround:
			AttackOnGroundState();
			break;
		case AIState.AttackFlying:
			AttackFlyingState();
			break;
		}
		Timer++;
		base.Projectile.ForceNetUpdate();
	}

	private void IdleState()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (Timer > 15f)
		{
			if (base.Target != null)
			{
				State = AIState.AttackOnGround;
				return;
			}
			if (IsTileBetweenOwnerAndMinionVertically() || !Collision.CanHitLine(base.Projectile.Center, 1, 1, base.Owner.Center, 1, 1) || !base.Projectile.WithinRange(base.Owner.Center, 960f))
			{
				State = AIState.ReturnToOwner;
				return;
			}
			Vector2 idlePosition = base.Owner.Center - Vector2.UnitX * 60f * (float)base.Owner.direction;
			if (!base.Projectile.WithinRange(idlePosition, 8f))
			{
				int walkDirection = MathF.Sign(idlePosition.X - base.Projectile.Center.X);
				float maxSpeed = Utils.Remap(MathF.Abs(idlePosition.X - base.Projectile.Center.X), 160f, 0f, 8f, 0f);
				base.Projectile.velocity.X += 0.08f * (float)walkDirection;
				if (MathF.Abs(base.Projectile.velocity.X) > maxSpeed)
				{
					base.Projectile.velocity.X = maxSpeed * (float)walkDirection;
				}
			}
			else
			{
				base.Projectile.velocity.X = 0f;
			}
		}
		DoGravity();
		Collision.StepUp(ref base.Projectile.position, ref base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ref base.Projectile.stepSpeed, ref base.Projectile.gfxOffY);
		base.Projectile.spriteDirection = MathF.Sign(base.Projectile.velocity.X);
		Animation = ((!(MathF.Abs(base.Projectile.velocity.X) < 0.04f) && !IsMinionFacingTile()) ? AnimationState.Walk : AnimationState.Still);
	}

	private void ReturnToOwnerState()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			State = AIState.AttackOnGround;
		}
		else if (!IsTileBetweenOwnerAndMinionVertically() && Collision.CanHitLine(base.Projectile.Center, 1, 1, base.Owner.Center, 1, 1) && base.Projectile.WithinRange(base.Owner.Center, 960f))
		{
			State = AIState.Idle;
			base.Projectile.velocity.Y = 5f * (float)MathF.Sign(base.Projectile.velocity.Y);
		}
		else
		{
			FlyTowardsPlace(((Vector2)(ref base.Owner.velocity)).Length() + 8f, base.Owner.Center, MathF.Sign(base.Projectile.velocity.X), AnimationState.Fly);
			base.Projectile.rotation = MathHelper.ToRadians(((Vector2)(ref base.Projectile.velocity)).Length()) * (float)MathF.Sign(base.Projectile.velocity.X);
			ElevationDust(shootingState: false);
		}
	}

	private void AttackOnGroundState()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			if (base.Projectile.Center.Y > base.Target.Bottom.Y || base.Projectile.Center.Y < base.Target.Top.Y)
			{
				State = AIState.AttackFlying;
				return;
			}
			if (base.Projectile.frame == 2 && !HasShotOnce)
			{
				ShootBullet();
			}
			if (base.Projectile.frame != 2 && HasShotOnce)
			{
				HasShotOnce = false;
			}
			DoGravity();
			base.Projectile.velocity.X = 0f;
			base.Projectile.spriteDirection = MathF.Sign(base.Target.Center.X - base.Projectile.Center.X);
			Animation = AnimationState.Shoot;
		}
		else
		{
			State = AIState.Idle;
		}
	}

	private void AttackFlyingState()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			Vector2 targetSpot = base.Target.Center - Vector2.UnitX * (LiliesOfFinality.Elster_DistanceFromTarget + (float)base.Target.width / 2f) * (float)MathF.Sign(base.Target.Center.X - base.Projectile.Center.X);
			if (!base.Projectile.WithinRange(targetSpot, 5f))
			{
				FlyTowardsPlace(LiliesOfFinality.Elster_TargettingFlySpeed, targetSpot, MathF.Sign(base.Target.Center.X - base.Projectile.Center.X), AnimationState.FlyShoot);
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.8f;
			}
			if (base.Projectile.frame == 2 && !HasShotOnce)
			{
				ShootBullet();
			}
			if (base.Projectile.frame != 2 && HasShotOnce)
			{
				HasShotOnce = false;
			}
			ElevationDust(shootingState: true);
		}
		else
		{
			State = AIState.Idle;
		}
	}

	private void FlyTowardsPlace(float speed, Vector2 place, int spriteDirection, AnimationState spriteAnimation)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(place) * speed) / 21f;
		base.Projectile.spriteDirection = spriteDirection;
		Animation = spriteAnimation;
	}

	private void ShootBullet()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 bulletSpawnPosition = base.Projectile.Center - Vector2.UnitX * 15f * (float)base.Projectile.spriteDirection;
			Vector2 bulletVelocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(bulletSpawnPosition, base.Target, LiliesOfFinality.Elster_BulletProjectileSpeed, LiliesOfFinality.Elster_BulletMaxUpdates);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), bulletSpawnPosition, bulletVelocity, ModContent.ProjectileType<LiliesOfFinalityBullet>(), (int)((float)base.Projectile.damage * 1.2f), base.Projectile.knockBack, base.Projectile.owner);
			HasShotOnce = true;
			if (!Main.dedServ)
			{
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center + Vector2.UnitX * (float)base.Projectile.width / 2f * (float)base.Projectile.spriteDirection, Vector2.Zero, Color.Goldenrod, new Vector2(0.5f, 1f), 0f, 0.05f, 0.25f, 7));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ElsterShot", 4);
				style.Volume = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
	}

	private void ElevationDust(bool shootingState)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			Vector2 leftLeg = (shootingState ? (base.Projectile.BottomLeft + Vector2.UnitX * ((base.Projectile.spriteDirection == -1) ? 28f : 10f)) : base.Projectile.BottomLeft);
			Vector2 rightLeg = (shootingState ? (base.Projectile.BottomRight - Vector2.UnitX * ((base.Projectile.spriteDirection == -1) ? 10f : 28f)) : base.Projectile.BottomRight);
			if (Main.rand.NextBool())
			{
				float interpolant = Main.rand.NextFloat();
				Vector2 position = Vector2.Lerp(leftLeg, rightLeg, interpolant);
				int commonDustID = LiliesOfFinality.CommonDustID;
				Vector2? velocity = Vector2.UnitY.RotatedByRandom(MathHelper.Lerp(0f - MathHelper.ToRadians(15f), MathHelper.ToRadians(15f), interpolant) * Main.rand.NextFloat(8f, 12f));
				float scale = Main.rand.NextFloat(1f, 1.2f);
				Dust.NewDustPerfect(position, commonDustID, velocity, 0, default(Color), scale).noGravity = true;
			}
			if (Main.rand.NextBool(4))
			{
				float interpolant2 = Main.rand.NextFloat();
				Vector2 position2 = Vector2.Lerp(leftLeg, rightLeg, interpolant2);
				int commonDustID2 = LiliesOfFinality.CommonDustID;
				Vector2? velocity2 = -Vector2.UnitY * Main.rand.NextFloat(3f, 6f);
				float scale = Main.rand.NextFloat(1f, 1.2f);
				Dust.NewDustPerfect(position2, commonDustID2, velocity2, 0, default(Color), scale).noGravity = true;
			}
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HasShotOnce);
		writer.Write(FrameAmount);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HasShotOnce = reader.ReadBoolean();
		FrameAmount = reader.ReadInt32();
	}

	public override void DoAnimation()
	{
		if (Animation != AnimationState.Still)
		{
			base.FramesUntilNextAnimationFrame = ((Animation == AnimationState.Walk) ? ((int)Utils.Remap(MathF.Abs(base.Projectile.velocity.X), 0f, 6f, 8f, 4f)) : 5);
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= base.FramesUntilNextAnimationFrame)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame = (base.Projectile.frame + 1) % FrameAmount;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(5, 9, (int)Animation, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, flipSprite);
		if (Animation == AnimationState.Still || Animation == AnimationState.Walk)
		{
			float yOffset = -2f;
			if (Animation == AnimationState.Walk)
			{
				switch (base.Projectile.frame)
				{
				case 0:
				case 1:
					yOffset = 0f;
					break;
				case 6:
				case 7:
					yOffset = -4f;
					break;
				}
			}
			Texture2D blinkTexture_FirstEye = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/ArianeAndElsterBlink_FirstEye", (AssetRequestMode)1).Value;
			Texture2D blinkTexture_SecondEye = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/ArianeAndElsterBlink_SecondEye", (AssetRequestMode)1).Value;
			Rectangle blinkFrame = blinkTexture_FirstEye.Frame(1, 3, 0, EyeFrameToShow);
			drawPosition += new Vector2((base.Projectile.spriteDirection == -1) ? 2f : 6f, yOffset);
			Vector2 blinkDrawPos = default(Vector2);
			((Vector2)(ref blinkDrawPos))._002Ector(drawPosition.X, drawPosition.Y);
			Color skinColor = default(Color);
			((Color)(ref skinColor))._002Ector(186, 144, 113, 255);
			Main.EntitySpriteDraw((base.Projectile.spriteDirection == -1) ? blinkTexture_SecondEye : blinkTexture_FirstEye, blinkDrawPos, blinkFrame, skinColor, base.Projectile.rotation, origin, base.Projectile.scale, flipSprite);
			blinkFrame = blinkTexture_SecondEye.Frame(1, 3, 0, EyeFrameToShow);
			((Vector2)(ref blinkDrawPos))._002Ector(drawPosition.X + 8f, drawPosition.Y);
			Main.EntitySpriteDraw((base.Projectile.spriteDirection == -1) ? blinkTexture_FirstEye : blinkTexture_SecondEye, blinkDrawPos, blinkFrame, skinColor, base.Projectile.rotation, origin, base.Projectile.scale, flipSprite);
		}
		return false;
	}

	private void UpdateEyeFrameToShow(Player player)
	{
		EyeFrame eyeFrameToShow = EyeFrame.EyeOpen;
		switch (state)
		{
		case EyeState.NormalBlinking:
		{
			int eyeFrameChoiceBasedOnTime = timeInState % 300 - 294;
			eyeFrameToShow = ((eyeFrameChoiceBasedOnTime >= 4) ? EyeFrame.EyeHalfClosed : ((eyeFrameChoiceBasedOnTime >= 2) ? EyeFrame.EyeClosed : ((eyeFrameChoiceBasedOnTime >= 0) ? EyeFrame.EyeHalfClosed : EyeFrame.EyeOpen)));
			break;
		}
		case EyeState.InStorm:
			eyeFrameToShow = ((timeInState % 150 - 144 < 0) ? EyeFrame.EyeHalfClosed : EyeFrame.EyeClosed);
			break;
		}
		EyeFrameToShow = (int)eyeFrameToShow;
	}

	private void SetStateByPlayerInfo(Player player)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		bool storming = player.ZoneSandstorm || (player.ZoneSnow && Main.IsItRaining);
		bool behindBackWall = false;
		Tile tileSafely = Framing.GetTileSafely(base.Projectile.Center);
		if (tileSafely != null)
		{
			behindBackWall = tileSafely.WallType > 0;
		}
		if (behindBackWall)
		{
			storming = false;
		}
		if (storming)
		{
			SwitchToState(EyeState.InStorm);
		}
		else
		{
			SwitchToState(EyeState.NormalBlinking);
		}
	}

	private void SwitchToState(EyeState newState, bool resetStateTimerEvenIfAlreadyInState = false)
	{
		if ((state != newState) | resetStateTimerEvenIfAlreadyInState)
		{
			state = newState;
			timeInState = 0;
		}
	}
}
