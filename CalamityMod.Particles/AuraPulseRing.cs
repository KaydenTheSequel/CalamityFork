using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class AuraPulseRing : Particle
{
	private Vector2 OriginalScale;

	private Vector2 FinalScale;

	public Vector2 CurrentScale;

	private float opacity;

	private Color BaseColor;

	public NPC StuckTo;

	public override string Texture => "CalamityMod/Particles/HollowCircleHardEdge";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public AuraPulseRing(Color color, Vector2 originalScale, Vector2 finalScale, int lifeTime, NPC stuckTo)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		BaseColor = color;
		OriginalScale = originalScale;
		FinalScale = finalScale;
		CurrentScale = originalScale;
		Lifetime = lifeTime;
		StuckTo = stuckTo;
	}

	public override void Update()
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		float pulseProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0f, 0f, 1f));
		float heightProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.SineInOutEasing, 0f, 0f, 1f));
		opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI) * 0.8f + 0.2f;
		Color = BaseColor * opacity;
		Lighting.AddLight(Position, (float)(int)((Color)(ref Color)).R / 255f, (float)(int)((Color)(ref Color)).G / 255f, (float)(int)((Color)(ref Color)).B / 255f);
		if (StuckTo != null && StuckTo.active)
		{
			CurrentScale = Vector2.Lerp(OriginalScale, FinalScale, pulseProgress) * StuckTo.scale;
			RelativeOffset = -Vector2.UnitY.RotatedBy(StuckTo.rotation) * (float)StuckTo.height / 2f + Vector2.UnitY.RotatedBy(StuckTo.rotation) * (float)StuckTo.height * heightProgress;
			Rotation = StuckTo.rotation;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch, Vector2 basePosition)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/HollowCircleHardEdge", (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, basePosition + RelativeOffset - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, tex.Size() / 2f, CurrentScale, (SpriteEffects)0, 0f);
	}
}
