using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class NanoParticle : Particle
{
	public bool UseAltVisual;

	private float opacity;

	private bool Big;

	private bool EmitsLight;

	private Vector2 Gravity;

	public override string Texture => "CalamityMod/Particles/NanoParticleSmall";

	public override bool UseAdditiveBlend => UseAltVisual;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public NanoParticle(Vector2 position, Vector2 velocity, Color color, float scale, int lifeTime, bool bigSize = false, bool emitsLight = false, bool AddativeBlend = true, Vector2? gravity = null)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		UseAltVisual = true;
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = 0f;
		Big = bigSize;
		EmitsLight = emitsLight;
		UseAltVisual = AddativeBlend;
		Gravity = (gravity ?? new Vector2?(Vector2.Zero)).Value;
		Variant = Main.rand.Next(3);
	}

	public override void Update()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Velocity += Gravity;
		opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI);
		if (EmitsLight)
		{
			Lighting.AddLight(Position, opacity * (float)(int)((Color)(ref Color)).R / 255f, opacity * (float)(int)((Color)(ref Color)).G / 255f, opacity * (float)(int)((Color)(ref Color)).B / 255f);
		}
		Velocity *= 0.95f;
		Scale *= 0.98f;
		if (Time % 3 == 0)
		{
			Position += Main.rand.NextVector2Circular(13f, 13f);
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		Texture2D nanoTexture = (Big ? ModContent.Request<Texture2D>("CalamityMod/Particles/NanoParticleBig", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, Big ? 8 : 6, Big ? 8 : 6, Big ? 8 : 6);
		spriteBatch.Draw(nanoTexture, Position - Main.screenPosition, (Rectangle?)frame, Color * opacity, 0f, frame.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
