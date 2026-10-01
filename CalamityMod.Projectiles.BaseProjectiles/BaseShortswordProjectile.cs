using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseShortswordProjectile : ModProjectile, ILocalizedModType, IModType
{
	public enum ShortswordType
	{
		TypicalShortsword
	}

	public CalamityUtils.CurveSegment ThrustSegment = new CalamityUtils.CurveSegment(CalamityUtils.LinearEasing, 0f, 0f, 1f, 3);

	public CalamityUtils.CurveSegment HoldSegment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.2f, 1f, 0.2f);

	public CalamityUtils.CurveSegment RetractSegment = new CalamityUtils.CurveSegment(CalamityUtils.PolyOutEasing, 0.76f, 1f, -0.8f, 3);

	public CalamityUtils.CurveSegment BumpSegment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.9f, 0.2f, 0.15f);

	public new string LocalizationCategory => "Projectiles.Melee";

	public virtual float FadeInDuration => 7f;

	public virtual float FadeOutDuration => 4f;

	public virtual float TotalDuration => 16f;

	public virtual Action<Projectile> EffectBeforePullback => null;

	public virtual ShortswordType ShortswordAIType => ShortswordType.TypicalShortsword;

	public float CollisionWidth => 4f * base.Projectile.scale;

	public float FullUse => (float)Timer / 14f;

	public Player Owner => Main.player[base.Projectile.owner];

	public int Timer
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	internal float DistanceFromPlayer => CalamityUtils.PiecewiseAnimation(FullUse, ThrustSegment, HoldSegment, RetractSegment, BumpSegment);

	public Vector2 OffsetFromPlayer
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.velocity * DistanceFromPlayer * 12f;
		}
	}

	public virtual void Behavior()
	{
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (ShortswordAIType != ShortswordType.TypicalShortsword || player.frozen)
		{
			return;
		}
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = player.itemAnimation;
		Timer++;
		if ((float)Timer >= TotalDuration)
		{
			base.Projectile.Kill();
			return;
		}
		player.heldProj = base.Projectile.whoAmI;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);
		if (base.Projectile.localAI[0] == 0f && EffectBeforePullback != null && Main.myPlayer == base.Projectile.owner)
		{
			base.Projectile.localAI[0] = 1f;
			EffectBeforePullback(base.Projectile);
		}
		Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
		base.Projectile.Center = playerCenter + base.Projectile.velocity * ((float)Timer - 1f);
		base.Projectile.Center = Owner.MountedCenter + OffsetFromPlayer;
		base.Projectile.scale = 1f + (float)Math.Sin(FullUse * (float)Math.PI) * 0.2f;
		base.Projectile.spriteDirection = (Vector2.Dot(base.Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f - (float)Math.PI / 4f * (float)base.Projectile.spriteDirection;
		SetVisualOffsets();
	}

	public virtual void SetVisualOffsets()
	{
	}

	public virtual void ExtraBehavior()
	{
	}

	public override void AI()
	{
		Behavior();
		ExtraBehavior();
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 center = base.Projectile.Center;
		Vector2 end = center + base.Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
		Utils.PlotTileLine(center, end, CollisionWidth, DelegateMethods.CutTiles);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 12f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.MountedCenter + OffsetFromPlayer, Owner.MountedCenter + OffsetFromPlayer + base.Projectile.velocity * bladeLength, 24f, ref collisionPoint);
	}
}
