using System;
using System.IO;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AtlasMunitionsAutocannon : ModProjectile, ILocalizedModType, IModType
{
	public int GeneralTimer;

	public bool CannonIsMounted = true;

	public bool TransitioningFromOverdriveMode;

	public ThanatosSmokeParticleSet SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);

	public new string LocalizationCategory => "Projectiles.Summon";

	public Vector2 CannonCenter
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center - Vector2.UnitY * base.Projectile.scale * 12f;
		}
	}

	public bool InOverdriveMode
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public ref float CannonFrame => ref base.Projectile.localAI[0];

	public ref float HeatInterpolant => ref base.Projectile.localAI[1];

	public ref float CannonDirection => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 108;
		base.Projectile.height = 108;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.netImportant = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.Opacity = 0f;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HeatInterpolant);
		writer.Write(CannonIsMounted);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HeatInterpolant = reader.ReadSingle();
		CannonIsMounted = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 8f, GeneralTimer, clamped: true);
		bool frameChangeInterval = base.Projectile.frameCounter % 6 == 5;
		base.Projectile.frameCounter++;
		if (frameChangeInterval && base.Projectile.frame < Main.projFrames[base.Type] - 1)
		{
			base.Projectile.frame++;
		}
		bool canShoot = false;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2400f, Owner);
		if (!CannonIsMounted)
		{
			potentialTarget = null;
		}
		SmokeDrawer.ParticleSpawnRate = ((HeatInterpolant > 0.7f) ? 3 : 9999999);
		SmokeDrawer.BaseMoveRotation = (float)Math.PI / 2f + (float)base.Projectile.spriteDirection * (base.Projectile.position.X - base.Projectile.oldPosition.X) * 0.04f;
		SmokeDrawer.Update();
		if (frameChangeInterval && CannonFrame < 5f)
		{
			CannonFrame++;
		}
		else
		{
			if (!InOverdriveMode && potentialTarget != null && base.Projectile.WithinRange(potentialTarget.Center, 720f))
			{
				InOverdriveMode = true;
				base.Projectile.netUpdate = true;
			}
			int minFrame = 0;
			int maxFrame = 8;
			int frameToResetToAtLimit = 5;
			canShoot = potentialTarget != null;
			if (!canShoot)
			{
				maxFrame = 5;
			}
			if (InOverdriveMode)
			{
				minFrame = 9;
				maxFrame = 16;
				frameToResetToAtLimit = 14;
			}
			if (TransitioningFromOverdriveMode)
			{
				minFrame = 15;
				maxFrame = 19;
				frameToResetToAtLimit = 6;
			}
			if (CannonFrame < (float)minFrame)
			{
				CannonFrame = minFrame;
			}
			if (frameChangeInterval)
			{
				CannonFrame++;
				if (CannonFrame >= (float)maxFrame)
				{
					CannonFrame = frameToResetToAtLimit;
					if (TransitioningFromOverdriveMode)
					{
						InOverdriveMode = false;
						TransitioningFromOverdriveMode = false;
						base.Projectile.netUpdate = true;
					}
				}
			}
			bool targetIsInvalidForOverdriveMode = potentialTarget == null || !base.Projectile.WithinRange(potentialTarget.Center, 1152f);
			if (InOverdriveMode & targetIsInvalidForOverdriveMode)
			{
				TransitioningFromOverdriveMode = true;
				base.Projectile.netUpdate = true;
			}
		}
		if (canShoot)
		{
			CannonDirection = CannonCenter.AngleTo(potentialTarget.Center);
			float wrappedAttackTimer = GeneralTimer % (InOverdriveMode ? 23 : 9);
			int laserCount = ((!InOverdriveMode) ? 1 : 3);
			int laserShootRate = 3;
			if (wrappedAttackTimer == 1f)
			{
				SoundStyle style = CommonCalamitySounds.LaserCannonSound with
				{
					Volume = 0.25f
				};
				SoundEngine.PlaySound(in style, CannonCenter);
			}
			if (wrappedAttackTimer < (float)(laserCount * laserShootRate) && wrappedAttackTimer % (float)laserShootRate == 1f && Main.myPlayer == base.Projectile.owner)
			{
				FireLaserAtTarget(potentialTarget, wrappedAttackTimer / ((float)(laserCount * laserShootRate) - 1f));
			}
		}
		else
		{
			CannonDirection = CannonDirection.AngleLerp(0f, 0.06f);
		}
		if (!InOverdriveMode)
		{
			HeatInterpolant = MathHelper.Clamp(HeatInterpolant - 1f / 180f, 0f, 1f);
		}
		base.Projectile.velocity.Y = MathHelper.Clamp(base.Projectile.velocity.Y + 0.3f, 0f, 20f);
		GeneralTimer++;
		bool rightClick = Main.mouseRight && Main.mouseRightRelease;
		if ((Main.LocalPlayer.WithinRange(base.Projectile.Center, 200f) & rightClick) && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<AtlasMunitionsBeacon>() && CannonIsMounted)
		{
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Main.LocalPlayer.Center, Vector2.UnitX, ModContent.ProjectileType<AtlasMunitionsAutocannonHeld>(), base.Projectile.damage, base.Projectile.knockBack, Main.myPlayer);
			projectile.ModProjectile<AtlasMunitionsAutocannonHeld>().HeatInterpolant = HeatInterpolant * 0.65f;
			projectile.originalDamage = base.Projectile.originalDamage;
			projectile.ai[2] = base.Projectile.ai[2];
			CannonIsMounted = false;
			base.Projectile.netUpdate = true;
			return;
		}
		if (!CannonIsMounted)
		{
			int detachedCannonID = ModContent.ProjectileType<AtlasMunitionsAutocannonHeld>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == detachedCannonID && p.owner == base.Projectile.owner)
				{
					Rectangle hitbox = p.Hitbox;
					if (((Rectangle)(ref hitbox)).Intersects(base.Projectile.Hitbox) && !p.ModProjectile<AtlasMunitionsAutocannonHeld>().BeingHeld)
					{
						p.Kill();
						CannonIsMounted = true;
						HeatInterpolant = p.ModProjectile<AtlasMunitionsAutocannonHeld>().HeatInterpolant;
						base.Projectile.netUpdate = true;
						break;
					}
				}
			}
		}
		else if (!base.Projectile.WithinRange(Owner.Center, 7200f))
		{
			int podID = ModContent.ProjectileType<AtlasMunitionsDropPod>();
			int podUpperID = ModContent.ProjectileType<AtlasMunitionsDropPodUpper>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile p2 = enumerator2.Current;
				if ((p2.type == podID || p2.type == podUpperID) && p2.owner == base.Projectile.owner)
				{
					p2.Kill();
				}
			}
			base.Projectile.Kill();
		}
		Projectile parent = Main.projectile[(int)base.Projectile.ai[2]];
		if (parent.type != ModContent.ProjectileType<AtlasMunitionsDropPod>() || !parent.active)
		{
			base.Projectile.Kill();
		}
	}

	public void FireLaserAtTarget(NPC target, float laserOffsetInterpolant)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		int laserCount = 1;
		int laserDamage = base.Projectile.damage;
		int laserID = ModContent.ProjectileType<AtlasMunitionsLaser>();
		float offsetAngleMax = 0.0001f;
		if (InOverdriveMode)
		{
			laserCount = 3;
			offsetAngleMax = 0.05f;
			laserDamage = (int)((float)laserDamage * 1.18f);
			laserID = ModContent.ProjectileType<AtlasMunitionsLaserOverdrive>();
			HeatInterpolant = MathHelper.Clamp(HeatInterpolant + 0.01f, 0f, 1f);
			base.Projectile.netUpdate = true;
		}
		Vector2 laserVelocity = (target.Center - CannonCenter).SafeNormalize(Vector2.UnitY).RotatedByRandom(offsetAngleMax) * 7f;
		Vector2 laserSpawnOffset = CannonDirection.ToRotationVector2() * 66f - (CannonDirection + (float)Math.PI / 2f).ToRotationVector2() * (float)Math.Sign(Math.Cos(CannonDirection)) * 10f;
		if (laserCount >= 2)
		{
			laserSpawnOffset += ((float)Math.PI * 2f * laserOffsetInterpolant + (float)Math.PI / 2f / (float)laserCount).ToRotationVector2() * 14f;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), CannonCenter + laserSpawnOffset, laserVelocity, laserID, laserDamage, 0f, base.Projectile.owner);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Invalid comparison between Unknown and I4
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsAutocannon", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsAutocannonGlow", (AssetRequestMode)2).Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float cannonRotation = CannonDirection;
		SpriteEffects cannonDirection = (SpriteEffects)0;
		if (Math.Cos(cannonRotation) < 0.0)
		{
			cannonRotation += (float)Math.PI;
			cannonDirection = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, cannonDirection);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, cannonDirection);
		if (!CannonIsMounted)
		{
			return false;
		}
		texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsTurret", (AssetRequestMode)2).Value;
		glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsTurretGlow", (AssetRequestMode)2).Value;
		frame = texture.Frame(1, 20, 0, (int)CannonFrame);
		drawPosition = CannonCenter - Main.screenPosition;
		Vector2 cannonOrigin = new Vector2(0.39f, 0.5f) * frame.Size();
		if ((int)cannonDirection == 1)
		{
			cannonOrigin.X = (float)frame.Width - cannonOrigin.X;
		}
		SmokeDrawer.DrawSet(drawPosition + Main.screenPosition);
		for (int i = 0; i < 12; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 12f + Main.GlobalTimeWrappedHourly * 2.3f).ToRotationVector2() * (float)Math.Pow(HeatInterpolant, 2.3) * 6f;
			Main.EntitySpriteDraw(texture, drawPosition + drawOffset, frame, AtlasMunitionsBeacon.HeatGlowColor * base.Projectile.Opacity * 0.5f, cannonRotation, cannonOrigin, base.Projectile.scale, cannonDirection);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, Color.Lerp(lightColor, AtlasMunitionsBeacon.HeatGlowColor, HeatInterpolant * 0.45f) * base.Projectile.Opacity, cannonRotation, cannonOrigin, base.Projectile.scale, cannonDirection);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, base.Projectile.GetAlpha(Color.White), cannonRotation, cannonOrigin, base.Projectile.scale, cannonDirection);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}
}
