using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ElectricSpark : Particle
{
	private float Spin;

	private float opacity;

	private Color Bloom;

	private float BloomScale;

	private float MaxJumpRotation;

	private float JumpTime;

	private Vector2 OriginalSpeed;

	public override string Texture => "CalamityMod/Particles/ElectricSpark";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override int FrameVariants => 2;

	private Color LightColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return Bloom * opacity;
		}
	}

	public ElectricSpark(Vector2 position, Vector2 velocity, Color color, Color bloom, float scale, int lifeTime, float maxJumpRotation = (float)Math.PI / 4f, float jumpTime = 10f, float rotationSpeed = 1f, float bloomScale = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		OriginalSpeed = velocity;
		Color = color;
		Bloom = bloom;
		Scale = scale;
		Lifetime = lifeTime;
		MaxJumpRotation = maxJumpRotation;
		JumpTime = jumpTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		BloomScale = bloomScale;
		Variant = Main.rand.Next(2);
	}

	public override void Update()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		opacity -= JumpTime * 0.25f / (float)Lifetime;
		if ((float)Time % JumpTime == 0f)
		{
			opacity = 1f;
			Velocity = OriginalSpeed.RotatedByRandom(MaxJumpRotation);
		}
		Velocity *= 0.6f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f)) * (((double)base.LifetimeCompletion > 0.5) ? 1f : 0.5f);
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
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sparkTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 6 * Variant, 6, 6);
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float properBloomSize = (float)frame.Height / (float)bloomTexture.Height;
		spriteBatch.Draw(bloomTexture, Position - Main.screenPosition, (Rectangle?)null, Bloom * opacity * 0.5f, 0f, bloomTexture.Size() / 2f, Scale * BloomScale * properBloomSize, (SpriteEffects)0, 0f);
		spriteBatch.Draw(sparkTexture, Position - Main.screenPosition, (Rectangle?)frame, Color * opacity, Rotation, frame.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
