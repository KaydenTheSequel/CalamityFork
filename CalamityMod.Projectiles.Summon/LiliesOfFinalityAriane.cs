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

public class LiliesOfFinalityAriane : BaseMinionProjectile
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
		Attack
	}

	public enum AnimationState
	{
		Still,
		Walk,
		Fly
	}

	private EyeState state;

	private int timeInState;

	private int FrameAmount = 1;

	private Projectile Elster;

	public int EyeFrameToShow { get; private set; }

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			base.Projectile.ai[0] = (float)value;
			base.Projectile.tileCollide = value == AIState.Idle;
			base.Projectile.rotation = 0f;
			if (value == AIState.Attack && Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LiliesOfFinalityAoE>(), base.Projectile.damage, 0f, base.Projectile.owner, base.Projectile.whoAmI);
			}
			Timer = 0f;
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
				FrameAmount = 1;
				break;
			case AnimationState.Walk:
				base.Projectile.width = 32;
				FrameAmount = 9;
				break;
			case AnimationState.Fly:
				base.Projectile.width = 56;
				FrameAmount = 8;
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

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<LiliesOfFinalityAriane>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<LiliesOfFinalityBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.LiliesOfFinalityBool;

	public override float EnemyDistanceDetection => LiliesOfFinality.MaxEnemyDistanceDetection;

	public override int AnimationFrames => 18;

	public override bool Grounded => true;

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 66;
		base.SetDefaults();
	}

	public override void CheckMinionExistence()
	{
		if (Elster == null)
		{
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile p = Main.projectile[i];
				if (p != null && p.active && p.owner == base.Projectile.owner && p.type == ModContent.ProjectileType<LiliesOfFinalityElster>())
				{
					Elster = p;
					break;
				}
			}
		}
		base.Projectile.timeLeft = 2;
		if (Elster == null || !Elster.active || Elster.owner != base.Projectile.owner || Elster.type != ModContent.ProjectileType<LiliesOfFinalityElster>())
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
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
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
		case AIState.Attack:
			AttackState();
			break;
		}
		Timer++;
		base.Projectile.ForceNetUpdate();
		if (!Main.dedServ && (Elster.ModProjectile<LiliesOfFinalityElster>().State == LiliesOfFinalityElster.AIState.ReturnToOwner || Elster.ModProjectile<LiliesOfFinalityElster>().State == LiliesOfFinalityElster.AIState.AttackFlying))
		{
			Vector2 position = Vector2.Lerp(base.Projectile.Center, Elster.Center, Main.rand.NextFloat()) + Main.rand.NextVector2Circular(5f, 5f);
			int commonDustID = LiliesOfFinality.CommonDustID;
			Vector2? velocity = Vector2.Zero;
			float scale = Main.rand.NextFloat(0.8f, 1f);
			Dust.NewDustPerfect(position, commonDustID, velocity, 0, default(Color), scale).noGravity = true;
		}
	}

	private void IdleState()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		if (Timer > 15f)
		{
			if (base.Target != null)
			{
				State = AIState.Attack;
				return;
			}
			if (IsTileBetweenOwnerAndMinionVertically() || !Collision.CanHitLine(base.Projectile.Center, 1, 1, base.Owner.Center, 1, 1) || !base.Projectile.WithinRange(base.Owner.Center, 960f))
			{
				State = AIState.ReturnToOwner;
				return;
			}
			Rectangle rect = base.Projectile.getRect();
			if (!((Rectangle)(ref rect)).Intersects(Elster.getRect()))
			{
				int walkDirection = MathF.Sign(Elster.Center.X - base.Projectile.Center.X);
				float maxSpeed = Utils.Remap(MathF.Abs(Elster.Center.X - base.Projectile.Center.X), 160f, 0f, 8f, 0f);
				base.Projectile.velocity.X += 0.08f * (float)walkDirection;
				if (MathF.Abs(base.Projectile.velocity.X) > maxSpeed)
				{
					base.Projectile.velocity.X = maxSpeed * (float)walkDirection;
				}
			}
			else
			{
				base.Projectile.velocity.X *= 0.8f;
				base.Projectile.spriteDirection = MathF.Sign(Elster.Center.X - base.Projectile.Center.X);
				Elster.spriteDirection = MathF.Sign(base.Projectile.Center.X - Elster.Center.X);
				if (!Main.dedServ && Main.rand.NextBool(100))
				{
					Vector2 emoteDirection = -Vector2.UnitY.RotatedByRandom(1.0995573997497559);
					GeneralParticleHandler.SpawnParticle(new LiliesOfFinalityHeartParticle((Main.rand.NextBool() ? base.Projectile.Center : Elster.Center) + emoteDirection * 15f, emoteDirection * Main.rand.NextFloat(1f, 2f), Main.rand.Next(30, 45), Main.rand.NextFloat(0.6f, 1f)));
				}
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
			State = AIState.Attack;
		}
		else if (!IsTileBetweenOwnerAndMinionVertically() && Collision.CanHitLine(base.Projectile.Center, 1, 1, base.Owner.Center, 1, 1) && base.Projectile.WithinRange(base.Owner.Center, 960f))
		{
			State = AIState.Idle;
			base.Projectile.velocity.Y = 5f * (float)MathF.Sign(base.Projectile.velocity.Y);
		}
		else
		{
			FlyTowardsPlace(((Vector2)(ref base.Owner.velocity)).Length() + 8f, base.Owner.Center, MathF.Sign(base.Projectile.velocity.X));
			Animation = AnimationState.Fly;
		}
	}

	private void AttackState()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			Vector2 targetSpot = Elster.Center + new Vector2(-100f * (float)base.Projectile.spriteDirection, -60f);
			if (!base.Projectile.WithinRange(targetSpot, 8f))
			{
				FlyTowardsPlace(LiliesOfFinality.Ariane_TargettingFlySpeed, targetSpot, MathF.Sign(base.Target.Center.X - base.Projectile.Center.X));
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.8f;
			}
			if (Timer >= LiliesOfFinality.Ariane_BoltFireRate)
			{
				ShootBolt();
			}
			Animation = AnimationState.Fly;
		}
		else
		{
			State = AIState.Idle;
		}
	}

	private void ShootBolt()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		Vector2 shootDirection = base.Projectile.SafeDirectionTo(base.Target.Center).RotatedByRandom(0.7853981852531433);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootDirection * LiliesOfFinality.Ariane_BoltProjectileSpeed, ModContent.ProjectileType<LiliesOfFinalityBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		Timer = 0f;
		if (!Main.dedServ)
		{
			Vector2 center = base.Projectile.Center;
			Vector2 center2 = base.Projectile.Size;
			Vector2 visualBoltSpawn = center + shootDirection * ((Vector2)(ref center2)).Length() / 2f;
			for (int i = 0; i < 10; i++)
			{
				Vector2 spinningpoint = Vector2.UnitY * MathHelper.Lerp(-8f, 8f, Main.rand.NextFloat());
				double radians = visualBoltSpawn.ToRotation();
				center2 = default(Vector2);
				Vector2 position = visualBoltSpawn + spinningpoint.RotatedBy(radians, center2);
				int commonDustID = LiliesOfFinality.CommonDustID;
				Vector2? velocity = shootDirection * Main.rand.NextFloat(3f, 6f);
				float scale = Main.rand.NextFloat(0.6f, 0.8f);
				Dust.NewDustPerfect(position, commonDustID, velocity, 0, default(Color), scale).noGravity = true;
			}
			Particle outerRing = new DirectionalPulseRing(visualBoltSpawn, Vector2.Zero, Color.Red, new Vector2(0.4f, 1f), shootDirection.ToRotation(), 0.05f, 0.3f, 20);
			DirectionalPulseRing particle = new DirectionalPulseRing(visualBoltSpawn, Vector2.Zero, Color.Fuchsia, new Vector2(0.5f, 1f), shootDirection.ToRotation(), 0.05f, 0.15f, 20);
			GeneralParticleHandler.SpawnParticle(outerRing);
			GeneralParticleHandler.SpawnParticle(particle);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneHellblastSound");
			style.Pitch = 0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	private void FlyTowardsPlace(float speed, Vector2 place, int spriteDirection)
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
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(FrameAmount);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
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
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>(GlowTexture, (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(3, 9, (int)Animation, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glowTexture, drawPosition, frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, flipSprite);
		if (Animation == AnimationState.Still || Animation == AnimationState.Walk)
		{
			float yOffset = 12f;
			if (Animation == AnimationState.Walk)
			{
				int frame2 = base.Projectile.frame;
				if ((uint)(frame2 - 1) <= 1u || (uint)(frame2 - 5) <= 2u)
				{
					yOffset = 14f;
				}
			}
			Texture2D blinkTexture_FirstEye = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/ArianeAndElsterBlink_FirstEye", (AssetRequestMode)1).Value;
			Texture2D blinkTexture_SecondEye = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/ArianeAndElsterBlink_SecondEye", (AssetRequestMode)1).Value;
			Rectangle blinkFrame = blinkTexture_FirstEye.Frame(1, 3, 0, EyeFrameToShow);
			drawPosition += new Vector2((base.Projectile.spriteDirection == -1) ? 7f : 11f, yOffset);
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
			int eyeFrameChoiceBasedOnTime = timeInState % 240 - 234;
			eyeFrameToShow = ((eyeFrameChoiceBasedOnTime >= 4) ? EyeFrame.EyeHalfClosed : ((eyeFrameChoiceBasedOnTime >= 2) ? EyeFrame.EyeClosed : ((eyeFrameChoiceBasedOnTime >= 0) ? EyeFrame.EyeHalfClosed : EyeFrame.EyeOpen)));
			break;
		}
		case EyeState.InStorm:
			eyeFrameToShow = ((timeInState % 120 - 114 < 0) ? EyeFrame.EyeHalfClosed : EyeFrame.EyeClosed);
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
