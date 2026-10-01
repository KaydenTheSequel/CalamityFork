using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class HealingPlus : Particle
{
	public Player Owner;

	public Color StartColor;

	public Color EndColor;

	public Vector2 OverridePosition;

	public float Opacity;

	public override string Texture => "CalamityMod/Particles/HealingPlus";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public HealingPlus(Vector2 position, float scale, Vector2 velocity, Color colorStart, Color colorEnd, int lifetime)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Scale = scale;
		Velocity = velocity;
		Rotation = 0f;
		StartColor = colorStart;
		EndColor = colorEnd;
		Color = colorStart;
		Lifetime = lifetime;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Color = Color.Lerp(StartColor, EndColor, base.LifetimeCompletion);
		Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * 0.2f);
		Opacity -= 0.5f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 Size = default(Vector2);
		((Vector2)(ref Size))._002Ector(Scale);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)tex.Width, (float)tex.Height);
		Vector2 PositionAdjust = default(Vector2);
		((Vector2)(ref PositionAdjust))._002Ector(-16f, -40f);
		spriteBatch.Draw(tex, Position - Main.screenPosition - PositionAdjust, (Rectangle?)null, Color, Rotation, origin, Size, (SpriteEffects)0, 0f);
	}
}
