using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class LiliesOfFinalityHeartParticle : Particle
{
	public override string Texture => "CalamityMod/Particles/LiliesOfFinalityHeartParticle";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public LiliesOfFinalityHeartParticle(Vector2 position, Vector2 velocity, int lifeTime, float scale = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Lifetime = lifeTime;
		Scale = scale;
		Rotation = velocity.ToRotation() + (float)Math.PI / 2f;
		Color = Color.White;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Velocity *= 0.96f;
		Scale *= 0.97f;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		Texture2D emoteTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 origin = emoteTexture.Size() * 0.5f;
		float opacity = 1f - MathF.Pow(base.LifetimeCompletion, 4f);
		spriteBatch.Draw(emoteTexture, Position - Main.screenPosition, (Rectangle?)null, Color * opacity * 0.7f, Rotation, origin, Scale, (SpriteEffects)0, 0f);
	}
}
