using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class SandyDustParticle : Particle
{
	private float Spin;

	private float opacity;

	private Vector2 Gravity;

	public Rectangle Frame;

	public override string Texture => "CalamityMod/Particles/SandyDust";

	public override bool UseHalfTransparency => false;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public SandyDustParticle(Vector2 position, Vector2 velocity, Color color, float scale, int lifeTime, float rotationSpeed = 1f, Vector2? gravity = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Gravity = (gravity ?? new Vector2?(Vector2.Zero)).Value;
		Variant = Main.rand.Next(12);
		Frame = new Rectangle(Variant % 6 * 12, 12 + Variant / 6 * 12, 10, 10);
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Velocity += Gravity;
		opacity = (float)Math.Sin(base.LifetimeCompletion * ((float)Math.PI / 2f) + (float)Math.PI / 2f);
		Velocity *= 0.95f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Scale *= 0.98f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		Texture2D dustTexture = GeneralParticleHandler.GetTexture(Type);
		spriteBatch.Draw(dustTexture, Position - Main.screenPosition, (Rectangle?)Frame, Color * opacity, Rotation, Frame.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
