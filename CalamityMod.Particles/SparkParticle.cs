using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SparkParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public bool FadeIn;

	public float FadeInScale;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => true;

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public SparkParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, bool fadeIn = false, bool affectedByLight = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		AffectedByGravity = affectedByGravity;
		AffectedByLight = affectedByLight;
		Scale = scale;
		FadeInScale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		FadeIn = fadeIn;
		if (FadeIn)
		{
			Scale = 0f;
		}
	}

	public override void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		if (!FadeIn)
		{
			Scale *= 0.95f;
			Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		}
		else if ((float)Time / (float)Lifetime < 0.5f)
		{
			Scale = MathHelper.Lerp(Scale, FadeInScale, 0.2f);
		}
		else
		{
			Scale = MathHelper.Lerp(Scale, FadeInScale, -0.21f);
		}
		Velocity *= 0.95f;
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = new Vector2(0.5f, 1.6f) * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Color col = Color;
		if (AffectedByLight)
		{
			col = Lighting.GetColor((Position / 16f).ToPoint()).MultiplyRGB(Color);
		}
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, col, Rotation, texture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, col, Rotation, texture.Size() * 0.5f, scale * new Vector2(0.45f, 1f), (SpriteEffects)0, 0f);
	}
}
