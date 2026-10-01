using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class BloomParticle : Particle
{
	private float OriginalScale;

	private float FinalScale;

	private float opacity;

	private Color BaseColor;

	private bool Fade;

	public override string Texture => "CalamityMod/Particles/BloomCircle";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public BloomParticle(Vector2 position, Vector2 velocity, Color color, float originalScale, float finalScale, int lifeTime, bool fade = true)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		BaseColor = color;
		OriginalScale = originalScale;
		FinalScale = finalScale;
		Scale = originalScale;
		Lifetime = lifeTime;
		Fade = fade;
	}

	public override void Update()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		float pulseProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4));
		Scale = MathHelper.Lerp(OriginalScale, FinalScale, pulseProgress);
		if (Fade)
		{
			opacity = (float)Math.Sin((float)Math.PI / 2f + base.LifetimeCompletion * ((float)Math.PI / 2f));
		}
		Color = BaseColor * (Fade ? opacity : 1f);
		Lighting.AddLight(Position, (float)(int)((Color)(ref Color)).R / 255f, (float)(int)((Color)(ref Color)).G / 255f, (float)(int)((Color)(ref Color)).B / 255f);
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * (Fade ? opacity : 1f), 0f, tex.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
