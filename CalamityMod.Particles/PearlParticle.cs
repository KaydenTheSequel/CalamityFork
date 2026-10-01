using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class PearlParticle : Particle
{
	public Color InitialColor;

	public bool AffectedByGravity;

	public float ShrinkSpeed;

	public float RotationSpeed;

	public bool HitTiles;

	public bool hasTileHit;

	public float pVelX;

	public float pVelY;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool UseAdditiveBlend => false;

	public override string Texture => "CalamityMod/Particles/PearlParticle";

	public PearlParticle(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color, float shrinkSpeed = 0.95f, float rotationSpeed = 0f, bool hitTiles = false)
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
		ShrinkSpeed = shrinkSpeed;
		RotationSpeed = rotationSpeed;
		HitTiles = hitTiles;
	}

	public override void Update()
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (HitTiles)
		{
			if (hasTileHit)
			{
				if (Velocity.X != pVelX)
				{
					Velocity.X = 0f - pVelX;
				}
				if (Velocity.Y != pVelY)
				{
					Velocity.Y = 0f - pVelY;
				}
				HitTiles = false;
			}
			if (Collision.SolidCollision(Position, (int)(7f * Scale), (int)(7f * Scale)))
			{
				hasTileHit = true;
				pVelX = Velocity.X;
				pVelY = Velocity.Y;
			}
		}
		Scale *= ShrinkSpeed;
		RotationSpeed *= ShrinkSpeed;
		Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0));
		Velocity *= 0.95f;
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
		Rotation += RotationSpeed;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = new Vector2(1f, 1f) * Scale;
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Particles/PearlParticleGlow", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture2, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, texture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color.Lerp(Color.White, Color.Transparent, (float)Math.Pow(base.LifetimeCompletion, 3.0)), Rotation, texture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
	}
}
