using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class AresSummonCrateParticle : Particle
{
	public override bool SetLifetime => true;

	public override string Texture => "CalamityMod/Particles/AresSummonCrate";

	public override bool UseCustomDraw => true;

	public AresSummonCrateParticle(Player owner, Vector2 velocity, int lifetime)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = owner.Center - Vector2.UnitY * 4f;
		Scale = 1f;
		Color = Color.White;
		Velocity = velocity;
		Rotation = 0f;
		Lifetime = lifetime;
	}

	public override void Update()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Rotation += (float)Math.Sign(Velocity.X) * 0.05f;
		Velocity.X *= 0.95f;
		Velocity.Y += 0.24f;
		if (Collision.SolidCollision(Position - Vector2.One * Scale * 19f, (int)(Scale * 38f), (int)(Scale * 38f), acceptTopSurfaces: true))
		{
			Velocity.X *= 0.8f;
			Rotation = 0f;
			Velocity.Y = 0f;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = GeneralParticleHandler.GetTexture(Type);
		Color lightColor = Lighting.GetColor(Position.ToTileCoordinates());
		int frameY = (int)Math.Round(MathHelper.Lerp(0f, 2f, (float)Math.Pow(base.LifetimeCompletion, 0.1599999964237213)));
		Rectangle frame = texture.Frame(1, 3, 0, frameY);
		float opacity = MathHelper.Clamp(1f - (float)Math.Pow(base.LifetimeCompletion, 7.0), 0f, 1f);
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)frame, lightColor * opacity, Rotation, frame.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
	}
}
