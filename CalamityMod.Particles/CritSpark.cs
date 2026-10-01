using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class CritSpark : Particle
{
	private float Spin;

	private float opacity;

	private Color Bloom;

	private float BloomScale;

	private float HueShift;

	public override string Texture => "CalamityMod/Particles/ThinSparkle";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	private Color LightColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return Bloom * opacity;
		}
	}

	public CritSpark(Vector2 position, Vector2 velocity, Color color, Color bloom, float scale, int lifeTime, float rotationSpeed = 1f, float bloomScale = 1f, float hueShift = 0f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Bloom = bloom;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		BloomScale = bloomScale;
		HueShift = hueShift;
	}

	public override void Update()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		opacity = (float)Math.Sin((float)Math.PI / 2f + base.LifetimeCompletion * ((float)Math.PI / 2f));
		Velocity *= 0.8f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f)) * (((double)base.LifetimeCompletion > 0.5) ? 1f : 0.5f);
		Color = Main.hslToRgb((Main.rgbToHsl(Color).X + HueShift) % 1f, Main.rgbToHsl(Color).Y, Main.rgbToHsl(Color).Z);
		Bloom = Main.hslToRgb((Main.rgbToHsl(Bloom).X + HueShift) % 1f, Main.rgbToHsl(Bloom).Y, Main.rgbToHsl(Bloom).Z);
		Vector2 position = Position;
		Color lightColor = LightColor;
		float r = (float)(int)((Color)(ref lightColor)).R / 255f;
		lightColor = LightColor;
		float g = (float)(int)((Color)(ref lightColor)).G / 255f;
		lightColor = LightColor;
		Lighting.AddLight(position, r, g, (float)(int)((Color)(ref lightColor)).B / 255f);
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sparkTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float properBloomSize = (float)sparkTexture.Height / (float)bloomTexture.Height;
		spriteBatch.Draw(bloomTexture, Position - Main.screenPosition, (Rectangle?)null, Bloom * opacity * 0.5f, 0f, bloomTexture.Size() / 2f, Scale * BloomScale * properBloomSize, (SpriteEffects)0, 0f);
		spriteBatch.Draw(sparkTexture, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, sparkTexture.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
