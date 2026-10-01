using System;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ArianeFakeDust : Particle
{
	private Projectile Projectile;

	private Vector2 RelativePosition;

	private float Spin;

	private float Opacity;

	private bool Big;

	public override string Texture => "CalamityMod/Particles/FakeDust";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public ArianeFakeDust(Projectile proj, Vector2 relativePosition, Vector2 velocity, Color color, float scale, int lifeTime, float rotationSpeed = 1f, bool bigSize = false)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Projectile = proj;
		RelativePosition = relativePosition;
		Velocity = velocity;
		Color = color;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		Big = bigSize;
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (Projectile != null && Projectile.active && Projectile.type == ModContent.ProjectileType<LiliesOfFinalityAoE>())
		{
			Position = Projectile.Center + RelativePosition;
		}
		Velocity *= 0.95f;
		Scale *= 0.98f;
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI);
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
		spriteBatch.Draw(dustTexture, Position - Main.screenPosition, (Rectangle?)frame, Color * Opacity, Rotation, frame.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
