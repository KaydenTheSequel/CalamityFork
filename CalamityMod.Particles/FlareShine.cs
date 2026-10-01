using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class FlareShine : Particle
{
	private float Spin;

	private float opacity;

	private Color Bloom;

	private float BloomScale;

	private float HueShift;

	private Vector2 OriginalScale;

	private Vector2 FinalScale;

	private int SpawnDelay;

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

	public FlareShine(Vector2 position, Vector2 velocity, Color color, Color bloom, float angle, Vector2 scale, Vector2 finalScale, int lifeTime, float rotationSpeed = 0f, float bloomScale = 1f, float hueShift = 0f, int spawnDelay = 0)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Bloom = bloom;
		OriginalScale = scale;
		FinalScale = finalScale;
		Scale = 1f;
		Lifetime = lifeTime;
		Rotation = angle % (float)Math.PI;
		Spin = rotationSpeed;
		BloomScale = bloomScale;
		HueShift = hueShift;
		SpawnDelay = spawnDelay;
	}

	public override void Update()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		if (SpawnDelay > 0)
		{
			Time--;
			Position -= Velocity;
			SpawnDelay--;
			return;
		}
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (SpawnDelay <= 0)
		{
			Texture2D sparkTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
			Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			float properBloomSize = (float)sparkTexture.Height / (float)bloomTexture.Height;
			Vector2 squish = Vector2.Lerp(OriginalScale, FinalScale, CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4)));
			spriteBatch.Draw(bloomTexture, Position - Main.screenPosition, (Rectangle?)null, Bloom * opacity * 0.5f, 0f, bloomTexture.Size() / 2f, squish * BloomScale * properBloomSize, (SpriteEffects)0, 0f);
			spriteBatch.Draw(sparkTexture, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, sparkTexture.Size() / 2f, squish, (SpriteEffects)0, 0f);
		}
	}
}
