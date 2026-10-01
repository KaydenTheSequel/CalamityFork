using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseLaserbeamProjectile : ModProjectile
{
	public float RotationalSpeed
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

	public float Time
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public float LaserLength
	{
		get
		{
			return base.Projectile.localAI[1];
		}
		set
		{
			base.Projectile.localAI[1] = value;
		}
	}

	public abstract float Lifetime { get; }

	public abstract float MaxScale { get; }

	public abstract float MaxLaserLength { get; }

	public abstract Texture2D LaserBeginTexture { get; }

	public abstract Texture2D LaserMiddleTexture { get; }

	public abstract Texture2D LaserEndTexture { get; }

	public virtual float ScaleExpandRate => 4f;

	public virtual Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public virtual Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return Color.White * 0.9f;
		}
	}

	public virtual void Behavior()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		AttachToSomething();
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(-Vector2.UnitY);
		Time++;
		if (Time >= Lifetime)
		{
			base.Projectile.Kill();
			return;
		}
		DetermineScale();
		UpdateLaserMotion();
		float idealLaserLength = DetermineLaserLength();
		LaserLength = MathHelper.Lerp(LaserLength, idealLaserLength, 0.9f);
		if (LightCastColor != Color.Transparent)
		{
			Color lightCastColor = LightCastColor;
			DelegateMethods.v3_1 = ((Color)(ref lightCastColor)).ToVector3();
			Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * LaserLength, (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
		}
	}

	public virtual void UpdateLaserMotion()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		float updatedVelocityDirection = base.Projectile.velocity.ToRotation() + RotationalSpeed;
		base.Projectile.rotation = updatedVelocityDirection - (float)Math.PI / 2f;
		base.Projectile.velocity = updatedVelocityDirection.ToRotationVector2();
	}

	public virtual void DetermineScale()
	{
		base.Projectile.scale = (float)Math.Sin(Time / Lifetime * (float)Math.PI) * ScaleExpandRate * MaxScale;
		if (base.Projectile.scale > MaxScale)
		{
			base.Projectile.scale = MaxScale;
		}
	}

	public virtual void AttachToSomething()
	{
	}

	public virtual float DetermineLaserLength()
	{
		return MaxLaserLength;
	}

	public virtual void ExtraBehavior()
	{
	}

	public float DetermineLaserLength_CollideWithTiles()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.PreciseDistanceToTileCollisionHit(base.Projectile.Center, base.Projectile.velocity.ToRotation(), MaxLaserLength);
	}

	protected internal void DrawBeamWithColor(Color beamColor, float scale, int startFrame = 0, int middleFrame = 0, int endFrame = 0)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, startFrame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, middleFrame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, endFrame);
		Main.EntitySpriteDraw(LaserBeginTexture, base.Projectile.Center - Main.screenPosition, startFrameArea, beamColor, base.Projectile.rotation, LaserBeginTexture.Size() / 2f, scale, (SpriteEffects)0);
		float laserBodyLength = LaserLength;
		laserBodyLength -= (float)(startFrameArea.Height / 2 + endFrameArea.Height) * scale;
		Vector2 centerOnLaser = base.Projectile.Center;
		centerOnLaser += base.Projectile.velocity * scale * (float)startFrameArea.Height / 2f;
		if (laserBodyLength > 0f)
		{
			float laserOffset = (float)middleFrameArea.Height * scale;
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, middleFrameArea, beamColor, base.Projectile.rotation, (float)LaserMiddleTexture.Width * 0.5f * Vector2.UnitX, scale, (SpriteEffects)0);
				incrementalBodyLength += laserOffset;
				centerOnLaser += base.Projectile.velocity * laserOffset;
			}
		}
		if (Math.Abs(LaserLength - DetermineLaserLength()) < 30f)
		{
			Vector2 laserEndCenter = centerOnLaser - Main.screenPosition;
			Main.EntitySpriteDraw(LaserEndTexture, laserEndCenter, endFrameArea, beamColor, base.Projectile.rotation, LaserEndTexture.Frame().Top(), scale, (SpriteEffects)0);
		}
	}

	public override void AI()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
		Behavior();
		ExtraBehavior();
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackMelee;
		Vector2 center = base.Projectile.Center;
		Vector2 end = base.Projectile.Center + base.Projectile.velocity * LaserLength;
		Vector2 size = base.Projectile.Size;
		Utils.PlotTileLine(center, end, ((Vector2)(ref size)).Length() * base.Projectile.scale, DelegateMethods.CutTiles);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		DrawBeamWithColor(LaserOverlayColor, base.Projectile.scale);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = 0f;
		Vector2 objectPosition = targetHitbox.TopLeft();
		Vector2 objectDimensions = targetHitbox.Size();
		Vector2 center = base.Projectile.Center;
		Vector2 lineEnd = base.Projectile.Center + base.Projectile.velocity * LaserLength;
		Vector2 size = base.Projectile.Size;
		return Collision.CheckAABBvLineCollision(objectPosition, objectDimensions, center, lineEnd, ((Vector2)(ref size)).Length() * base.Projectile.scale, ref _);
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}
}
