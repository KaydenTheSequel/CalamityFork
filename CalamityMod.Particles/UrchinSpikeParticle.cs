using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class UrchinSpikeParticle : Particle
{
	public float Opacity;

	public override string Texture => "CalamityMod/Particles/UrchinSpikes";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override int FrameVariants => 6;

	public UrchinSpikeParticle(Vector2 position, Vector2 speed, float rotation, float scale = 1f, float opacity = 1f, int lifetime = 20)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Scale = scale;
		Color = Color.White;
		Opacity = opacity;
		Velocity = speed;
		Rotation = rotation;
		Lifetime = lifetime;
		Variant = Main.rand.Next(6);
	}

	public override void Update()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Opacity *= 0.98f;
		Color = Lighting.GetColor((int)Position.X / 16, (int)Position.Y / 16) * Opacity;
		Velocity *= 0.9f;
		if (((Vector2)(ref Velocity)).Length() <= 0.01f)
		{
			GeneralParticleHandler.RemoveParticle(this);
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = GeneralParticleHandler.GetTexture(Type);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(8 * Variant, 0, 6, 10);
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)frame, Color, Rotation, frame.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
	}
}
