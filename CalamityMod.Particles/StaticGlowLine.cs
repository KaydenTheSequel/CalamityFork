using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class StaticGlowLine : Particle
{
	internal Asset<Texture2D> texAsset;

	private const float approximateSparkHeight = 1660f;

	internal Vector2 destination;

	internal Color InitialColor;

	internal bool usesGlow;

	internal float xScale;

	internal float xShrink;

	private float currentYScale;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => true;

	public override string Texture => "CalamityMod/Particles/GlowSpark";

	public StaticGlowLine(Vector2 start, Vector2 dest, Vector2 velocity, int lifetime, float xScale, float xShrink, Color color, bool glow = true)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		this.xScale = 1f;
		this.xShrink = 0.99f;
		currentYScale = 1f;
		base._002Ector();
		Position = start;
		destination = dest;
		Velocity = velocity;
		Lifetime = lifetime;
		Scale = 1f;
		this.xScale = xScale;
		this.xShrink = xShrink;
		Color = (InitialColor = color);
		usesGlow = glow;
		Rotation = (dest - start).ToRotation() + (float)Math.PI / 2f;
		texAsset = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		xScale *= xShrink;
		Vector2 delta = destination - Position;
		currentYScale = ((Vector2)(ref delta)).Length() / 1660f;
		Rotation = (destination - Position).ToRotation() + (float)Math.PI / 2f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = texAsset.Value;
		Vector2 texSize = texture.Size() * 0.5f;
		Vector2 scaleThisFrame = default(Vector2);
		((Vector2)(ref scaleThisFrame))._002Ector(xScale, currentYScale);
		float adjustmentDistance = 2f * currentYScale * ((float)texture.Height - 1660f);
		Vector2 toDest = (destination - Position).SafeNormalize(-Vector2.UnitY);
		Vector2 drawPos = Position + toDest * adjustmentDistance - Main.screenPosition;
		spriteBatch.Draw(texture, drawPos, (Rectangle?)null, Color, Rotation, texSize, scaleThisFrame, (SpriteEffects)0, 0f);
		if (usesGlow)
		{
			float glowInteriorWidth = 0.45f;
			Vector2 glowScale = default(Vector2);
			((Vector2)(ref glowScale))._002Ector(glowInteriorWidth, 1f);
			spriteBatch.Draw(texture, drawPos, (Rectangle?)null, Color.White, Rotation, texSize, scaleThisFrame * glowScale, (SpriteEffects)0, 0f);
		}
	}
}
