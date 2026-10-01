using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ConstellationRingVFX : Particle
{
	public Vector2 Squish;

	public int StarAmount;

	public float StarScale;

	public float SpinSpeed;

	public bool NeededVisual;

	public float Offset;

	public float Opacity;

	public override string Texture => "CalamityMod/Particles/HollowCircleSoftEdge";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool Important => NeededVisual;

	public ConstellationRingVFX(Vector2 position, Color color, float rotation, float scale, Vector2 squish, float opacity = 1f, int starAmount = 3, float starScale = 2f, float spinSpeed = 0.05f, bool important = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = Vector2.Zero;
		Rotation = rotation;
		Color = color;
		Scale = scale;
		Squish = squish;
		Opacity = opacity;
		StarAmount = starAmount;
		StarScale = starScale;
		SpinSpeed = spinSpeed;
		NeededVisual = important;
		Lifetime = 2;
		Offset = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		Texture2D ringTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D starTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		spriteBatch.Draw(ringTexture, Position - Main.screenPosition, (Rectangle?)null, Color * Opacity, Rotation, ringTexture.Size() / 2f, Squish * Scale, (SpriteEffects)0, 0f);
		float time = Main.GlobalTimeWrappedHourly * SpinSpeed;
		float starPosOffsetX = Squish.X * Scale * (float)ringTexture.Width * 0.45f;
		float starPosOffsetY = Squish.Y * Scale * (float)ringTexture.Height * 0.45f;
		float properBloomSize = (float)starTexture.Height / (float)bloomTexture.Height;
		Color starColor = Color * Opacity * 0.5f;
		Color starColor2 = Color.White * Opacity;
		Vector2 bloomOrigin = bloomTexture.Size() / 2f;
		float bloomScale = Scale * properBloomSize;
		Vector2 starOrigin = starTexture.Size() / 2f;
		float starScale = Scale * 0.75f;
		for (int i = 0; i < StarAmount; i++)
		{
			float starHeight = (float)Math.Sin(Offset + time + (float)i * ((float)Math.PI * 2f) / (float)StarAmount);
			float starWidth = (float)Math.Cos(Offset + time + (float)i * ((float)Math.PI * 2f) / (float)StarAmount);
			Vector2 starPos = Position + Rotation.ToRotationVector2() * starWidth * starPosOffsetX + (Rotation + (float)Math.PI / 2f).ToRotationVector2() * starHeight * starPosOffsetY;
			spriteBatch.Draw(bloomTexture, starPos - Main.screenPosition, (Rectangle?)null, starColor, 0f, bloomOrigin, bloomScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(starTexture, starPos - Main.screenPosition, (Rectangle?)null, starColor, Rotation + (float)Math.PI / 4f + (float)Math.PI / 4f * (float)i, starOrigin, starScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(starTexture, starPos - Main.screenPosition, (Rectangle?)null, starColor2, Rotation + (float)Math.PI / 4f * (float)i, starOrigin, Scale, (SpriteEffects)0, 0f);
		}
	}
}
