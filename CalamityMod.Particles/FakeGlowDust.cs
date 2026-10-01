using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class FakeGlowDust : Particle
{
	private float Spin;

	private float opacity;

	private bool Big;

	private bool EmitsLight;

	private Vector2 Gravity;

	public override string Texture => "CalamityMod/Particles/FakeDust";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public FakeGlowDust(Vector2 position, Vector2 velocity, Color color, float scale, int lifeTime, float rotationSpeed = 1f, bool bigSize = false, bool emitsLight = false, Vector2? gravity = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Big = bigSize;
		EmitsLight = emitsLight;
		Gravity = (gravity ?? new Vector2?(Vector2.Zero)).Value;
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Velocity += Gravity;
		opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI);
		if (EmitsLight)
		{
			Lighting.AddLight(Position, opacity * (float)(int)((Color)(ref Color)).R / 255f, opacity * (float)(int)((Color)(ref Color)).G / 255f, opacity * (float)(int)((Color)(ref Color)).B / 255f);
		}
		Velocity *= 0.95f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Scale *= 0.98f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Texture2D dustTexture = (Big ? ModContent.Request<Texture2D>("CalamityMod/Particles/FakeDustBig", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, (Big ? 8 : 6) * Variant, Big ? 8 : 6, Big ? 8 : 6);
		spriteBatch.Draw(dustTexture, Position - Main.screenPosition, (Rectangle?)frame, Color * opacity, Rotation, frame.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
