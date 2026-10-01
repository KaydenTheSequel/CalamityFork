using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class BloodParticle : Particle
{
	public Color InitialColor;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => true;

	public override string Texture => "CalamityMod/Particles/Blood";

	public BloodParticle(Vector2 relativePosition, Vector2 velocity, int lifetime, float scale, Color color)
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
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Scale *= 0.98f;
		Velocity.X *= 0.97f;
		Velocity.Y = MathHelper.Clamp(Velocity.Y + 0.9f, -22f, 22f);
		Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		Rotation = Velocity.ToRotation() + (float)Math.PI / 2f;
		if (!ChildSafety.Disabled)
		{
			Color = Main.DiscoColor;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		float verticalStretch = Utils.GetLerpValue(0f, 24f, Math.Abs(Velocity.Y), clamped: true) * 0.84f;
		float brightness = (float)Math.Pow(Lighting.Brightness((int)(Position.X / 16f), (int)(Position.Y / 16f)), 0.15);
		Vector2 scale = new Vector2(1f, verticalStretch + 1f) * Scale * 0.1f;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color * brightness, Rotation, texture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}
}
