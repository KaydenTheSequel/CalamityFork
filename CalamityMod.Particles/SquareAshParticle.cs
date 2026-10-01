using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SquareAshParticle : Particle
{
	public Color InitialColor;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => false;

	public override string Texture => "CalamityMod/Particles/Square";

	public SquareAshParticle(Vector2 relativePosition, Vector2 velocity, int lifetime, float scale, Color color)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = relativePosition;
		Velocity = velocity;
		Scale = scale;
		Lifetime = lifetime;
		Color = (InitialColor = color);
	}

	public override void Update()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (Time > Lifetime - 60)
		{
			Scale *= 0.95f;
		}
		Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		Rotation += ((Velocity.X > 0f) ? 0.8f : (-0.8f));
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = new Vector2(0.8f, 1.2f) * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, texture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}
}
