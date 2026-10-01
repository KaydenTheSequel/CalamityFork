using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SquareParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public float ExtraRotation;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => true;

	public override string Texture => "CalamityMod/Particles/Square";

	public SquareParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, float extraRotation = 0f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		AffectedByGravity = affectedByGravity;
		Scale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
		ExtraRotation = extraRotation;
	}

	public override void Update()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Scale *= 0.95f;
		Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		Velocity *= 0.95f;
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f + ExtraRotation;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, texture.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
	}
}
