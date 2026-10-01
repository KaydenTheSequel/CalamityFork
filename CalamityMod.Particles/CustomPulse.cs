using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class CustomPulse : Particle
{
	public bool UseAltVisual;

	private string NewTexture;

	private float OriginalScale;

	private float FinalScale;

	private float BaseOpacity;

	private float opacity;

	private bool FadeOut;

	private Vector2 Squish;

	private Color BaseColor;

	private float MakeLight;

	private SpriteEffects Effects;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool UseAdditiveBlend => UseAltVisual;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public CustomPulse(Vector2 position, Vector2 velocity, Color color, string texture, Vector2 squish, float rotation, float originalScale, float finalScale, int lifeTime, bool UseAdditiveBlend = true, float baseOpacity = 1f, bool fade = true, float makeLight = 1f, SpriteEffects effects = (SpriteEffects)0)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		UseAltVisual = true;
		base._002Ector();
		Position = position;
		Velocity = velocity;
		BaseColor = color;
		NewTexture = texture;
		OriginalScale = originalScale;
		FinalScale = finalScale;
		Scale = originalScale;
		Lifetime = lifeTime;
		BaseOpacity = baseOpacity;
		FadeOut = fade;
		Squish = squish;
		Effects = effects;
		Rotation = rotation;
		UseAltVisual = UseAdditiveBlend;
		MakeLight = makeLight;
	}

	public override void Update()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		float pulseProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4));
		Scale = MathHelper.Lerp(OriginalScale, FinalScale, pulseProgress);
		opacity = (FadeOut ? ((float)Math.Sin((float)Math.PI / 2f + base.LifetimeCompletion * ((float)Math.PI / 2f))) : 1f) * BaseOpacity;
		Color = BaseColor * opacity;
		if (MakeLight > 0f)
		{
			Lighting.AddLight(Position, (float)(int)((Color)(ref Color)).R / 255f * MakeLight, (float)(int)((Color)(ref Color)).G / 255f * MakeLight, (float)(int)((Color)(ref Color)).B / 255f * MakeLight);
		}
		Velocity *= 0.95f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(NewTexture, (AssetRequestMode)2).Value;
		float scaleMult = 1f;
		if (Main.zenithWorld && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
		{
			Texture2D joke = ModContent.Request<Texture2D>("CalamityMod/Particles/MammothParticle", (AssetRequestMode)2).Value;
			scaleMult = MathHelper.Lerp(tex.Size().X / joke.Size().X, tex.Size().Y / joke.Size().Y, 0.5f);
			tex = joke;
			UseAltVisual = true;
		}
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, tex.Size() / 2f, Scale * Squish * scaleMult, Effects, 0f);
	}
}
