using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SquishyLightParticle : Particle
{
	public float Opacity;

	public float SquishStrenght;

	public float MaxSquish;

	public float HueShift;

	public override string Texture => "CalamityMod/Particles/Light";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public SquishyLightParticle(Vector2 position, Vector2 velocity, float scale, Color color, int lifetime, float opacity = 1f, float squishStrenght = 1f, float maxSquish = 3f, float hueShift = 0f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Scale = scale;
		Color = color;
		Opacity = opacity;
		Rotation = 0f;
		Lifetime = lifetime;
		SquishStrenght = squishStrenght;
		MaxSquish = maxSquish;
		HueShift = hueShift;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		Velocity *= ((base.LifetimeCompletion >= 0.34f) ? 0.93f : 1.02f);
		Opacity = ((base.LifetimeCompletion > 0.5f) ? ((float)Math.Sin(base.LifetimeCompletion * (float)Math.PI) * 0.2f + 0.8f) : ((float)Math.Sin(base.LifetimeCompletion * (float)Math.PI)));
		Scale *= 0.95f;
		Color = Main.hslToRgb(Main.rgbToHsl(Color).X + HueShift, Main.rgbToHsl(Color).Y, Main.rgbToHsl(Color).Z);
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
		Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float squish = MathHelper.Clamp(((Vector2)(ref Velocity)).Length() / 10f * SquishStrenght, 1f, MaxSquish);
		float rot = Velocity.ToRotation() + (float)Math.PI / 2f;
		Vector2 origin = tex.Size() / 2f;
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(Scale - Scale * squish * 0.3f, Scale * squish);
		float properBloomSize = (float)tex.Height / (float)bloomTex.Height;
		Vector2 drawPosition = Position - Main.screenPosition;
		Main.spriteBatch.Draw(bloomTex, drawPosition, (Rectangle?)null, Color * Opacity * 0.8f, rot, bloomTex.Size() / 2f, scale * 2f * properBloomSize, (SpriteEffects)0, 0f);
		Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color * Opacity * 0.8f, rot, origin, scale * 1.1f, (SpriteEffects)0, 0f);
		Main.spriteBatch.Draw(tex, drawPosition, (Rectangle?)null, Color.White * Opacity * 0.9f, rot, origin, scale, (SpriteEffects)0, 0f);
	}
}
