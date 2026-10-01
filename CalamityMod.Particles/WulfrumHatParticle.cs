using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class WulfrumHatParticle : Particle
{
	public int Direction;

	public override bool SetLifetime => true;

	public override string Texture => "CalamityMod/Particles/WulfrumHat";

	public override bool UseCustomDraw => true;

	public WulfrumHatParticle(Player owner, Vector2 velocity, int lifetime)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = owner.Center - Vector2.UnitY * 20f;
		Direction = owner.direction;
		Scale = 1f;
		Color = Color.White;
		Velocity = velocity;
		Rotation = 0f;
		Lifetime = lifetime;
	}

	public override void Update()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		Rotation += 0.05f * (float)Math.Sign(Velocity.X);
		Velocity *= 0.95f;
		Velocity.Y += 0.22f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Texture2D baseTex = GeneralParticleHandler.GetTexture(Type);
		Color lightColor = Lighting.GetColor(Position.ToTileCoordinates());
		SpriteEffects spriteEffect = (SpriteEffects)0;
		if (Direction < 0)
		{
			spriteEffect = (SpriteEffects)1;
		}
		float opacity = Math.Clamp(1f - (float)Math.Pow(base.LifetimeCompletion, 3.0), 0f, 1f);
		spriteBatch.Draw(baseTex, Position - Main.screenPosition, (Rectangle?)null, lightColor * opacity, Rotation, baseTex.Size() / 2f, Scale, spriteEffect, 0f);
	}
}
