using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ChargeUpLineVFX : Particle
{
	public float BaseOpacity;

	public float BaseScale;

	public float Opacity;

	public float LineDirection;

	public float LineLength;

	private Vector2 PrevOffset;

	private Vector2 BasePosition;

	public bool Telegraph;

	public float FullFadeInPoint;

	public float MinDistanceFromOrigin;

	public CalamityUtils.CurveSegment goBack;

	public CalamityUtils.CurveSegment goForward;

	public CalamityUtils.CurveSegment noSquish;

	public CalamityUtils.CurveSegment squishSpeed;

	public override string Texture => "CalamityMod/Particles/Light";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool Important => Telegraph;

	public ChargeUpLineVFX(Vector2 startPoint, float lineDirection, float thickness, Color color, int lifetime, float opacity = 1f, bool telegraph = true, float fullFadeInPoint = 0.5f, float minDistanceFromOrigin = 8f)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		PrevOffset = Vector2.Zero;
		goBack = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0f, 1f, 0.25f);
		goForward = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineIn, 0.35f, 1.25f, -1.25f);
		noSquish = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0f, 1f, 0f);
		squishSpeed = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineIn, 0.35f, 1f, 0.8f);
		base._002Ector();
		RelativeOffset = startPoint;
		BasePosition = startPoint;
		LineDirection = lineDirection + (float)Math.PI;
		Scale = thickness;
		BaseScale = thickness;
		Color = color;
		BaseOpacity = opacity;
		Telegraph = telegraph;
		FullFadeInPoint = fullFadeInPoint;
		MinDistanceFromOrigin = minDistanceFromOrigin;
		Velocity = Vector2.Zero;
		Rotation = 0f;
		Lifetime = lifetime;
	}

	public float offsetPosition()
	{
		return CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, goBack, goForward);
	}

	public float Squish()
	{
		return CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, noSquish, squishSpeed);
	}

	public override void Update()
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		Opacity = ((base.LifetimeCompletion > FullFadeInPoint) ? BaseOpacity : ((float)Math.Sin((float)Time / (FullFadeInPoint * (float)Lifetime) * ((float)Math.PI / 2f)) * BaseOpacity));
		if (base.LifetimeCompletion != 0f && base.LifetimeCompletion != 1f)
		{
			if (!((double)base.LifetimeCompletion < 0.5))
			{
				_ = (0f - ((float)Math.Cos((float)Math.PI * base.LifetimeCompletion) - 1f)) / 2f;
			}
			else
			{
				_ = (float)Math.Pow(2.0, 20f * base.LifetimeCompletion - 10f) / 2f;
			}
		}
		Scale = BaseScale - (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI) * BaseScale * 0.5f;
		RelativeOffset = offsetPosition() * LineDirection.ToRotationVector2() * ((Vector2)(ref BasePosition)).Length();
		Vector2 center = BasePosition - RelativeOffset;
		LineLength = ((Vector2)(ref center)).Length();
		Vector2 relativeOffset = RelativeOffset;
		center = default(Vector2);
		RelativeOffset = relativeOffset.RotatedBy(0.04908738657832146, center);
		if (((Vector2)(ref RelativeOffset)).Length() < MinDistanceFromOrigin)
		{
			RelativeOffset = LineDirection.ToRotationVector2() * MinDistanceFromOrigin;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch, Vector2 basePosition)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
		float rot = LineDirection + (float)Math.PI / 2f;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)tex.Width / 2f, (float)tex.Height);
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(Scale - Scale * Squish() * 0.3f, Scale * Squish());
		Vector2 drawPosition = basePosition - Main.screenPosition + RelativeOffset;
		Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color * Opacity * 0.8f, rot, origin, scale * 1.1f, (SpriteEffects)0, 0f);
		Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White * Opacity, rot, origin, scale, (SpriteEffects)0, 0f);
	}
}
