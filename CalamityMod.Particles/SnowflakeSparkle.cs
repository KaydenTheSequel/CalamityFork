using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SnowflakeSparkle : Particle
{
	private float Spin;

	private float opacity;

	private Color Bloom;

	private float BloomScale;

	private int Spokes;

	public override string Texture => "CalamityMod/Particles/HalfIceStar";

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

	public SnowflakeSparkle(Vector2 position, Vector2 velocity, Color color, Color bloom, float scale, int lifeTime, float rotationSpeed = 1f, float bloomScale = 1f, int spokes = 3)
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
		Spokes = spokes;
	}

	public override void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI);
		Vector2 position = Position;
		Color lightColor = LightColor;
		float r = (float)(int)((Color)(ref lightColor)).R / 255f;
		lightColor = LightColor;
		float g = (float)(int)((Color)(ref lightColor)).G / 255f;
		lightColor = LightColor;
		Lighting.AddLight(position, r, g, (float)(int)((Color)(ref lightColor)).B / 255f);
		Velocity *= 0.95f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Texture2D spokesTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float properBloomSize = (float)spokesTexture.Height / (float)bloomTexture.Height;
		float halvedOpacity = opacity * 0.5f;
		spriteBatch.Draw(bloomTexture, Position - Main.screenPosition, (Rectangle?)null, Bloom * halvedOpacity, 0f, bloomTexture.Size() / 2f, Scale * BloomScale * properBloomSize, (SpriteEffects)0, 0f);
		Color spokeColor = Color * halvedOpacity;
		Vector2 origin = spokesTexture.Size() / 2f;
		for (int i = 0; i < Spokes; i++)
		{
			float rotation = Rotation + MathHelper.Lerp(0f, (float)Math.PI, (float)i / (float)Spokes);
			spriteBatch.Draw(spokesTexture, Position - Main.screenPosition, (Rectangle?)null, spokeColor, Rotation + rotation, origin, Scale, (SpriteEffects)0, 0f);
		}
	}
}
