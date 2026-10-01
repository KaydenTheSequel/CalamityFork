using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class CrackParticle : Particle
{
	private float OriginalScale;

	private float FinalScale;

	private Vector2 Squish;

	private Color BaseColor;

	public float opacity;

	public int Stop;

	public override string Texture => "CalamityMod/Particles/Crack";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public CrackParticle(Vector2 position, Vector2 velocity, Color color, Vector2 squish, float rotation, float originalScale, float finalScale, int lifeTime)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		BaseColor = color;
		OriginalScale = originalScale;
		FinalScale = finalScale;
		Scale = originalScale;
		Lifetime = lifeTime;
		Squish = squish;
		Rotation = rotation;
	}

	public override void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (Velocity != Vector2.Zero)
		{
			Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
		}
		Stop++;
		float pulseProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4));
		Scale = MathHelper.Lerp(OriginalScale, FinalScale, pulseProgress);
		if (Stop > 1)
		{
			Velocity *= 0f;
		}
		Color = BaseColor;
		opacity = (float)Math.Sin((float)Math.PI / 2f + base.LifetimeCompletion * ((float)Math.PI / 2f));
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, tex.Size() / 2f, Scale * Squish / 3f, (SpriteEffects)0, 0f);
	}
}
