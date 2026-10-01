using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class GenericSparkle : Particle
{
	public bool imporant;

	private float Spin;

	private float opacity;

	private Color Bloom;

	private float BloomScale;

	public override string Texture => "CalamityMod/Particles/Sparkle";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool Important => imporant;

	private Color LightColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return Bloom * opacity;
		}
	}

	public GenericSparkle(Vector2 position, Vector2 velocity, Color color, Color bloom, float scale, int lifeTime, float rotationSpeed = 1f, float bloomScale = 1f, bool needed = false)
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
		imporant = needed;
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
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D starTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float properBloomSize = (float)starTexture.Height / (float)bloomTexture.Height;
		spriteBatch.Draw(bloomTexture, Position - Main.screenPosition, (Rectangle?)null, Bloom * opacity * 0.5f, 0f, bloomTexture.Size() / 2f, Scale * BloomScale * properBloomSize, (SpriteEffects)0, 0f);
		spriteBatch.Draw(starTexture, Position - Main.screenPosition, (Rectangle?)null, Color * opacity * 0.5f, Rotation + (float)Math.PI / 4f, starTexture.Size() / 2f, Scale * 0.75f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(starTexture, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, starTexture.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
