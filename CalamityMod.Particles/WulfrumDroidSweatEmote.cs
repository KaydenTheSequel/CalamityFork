using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class WulfrumDroidSweatEmote : Particle
{
	public override string Texture => "CalamityMod/Particles/WulfrumDroidSweatEmote";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public WulfrumDroidSweatEmote(Vector2 position, Vector2 velocity, int lifeTime, float scale = 1f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = Color.White;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Velocity *= 0.96f;
		Scale *= 0.97f;
		Velocity.Y += 0.06f;
		Vector2 position = Position;
		Color val = new Color(194, 255, 62);
		Lighting.AddLight(position, ((Color)(ref val)).ToVector3());
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D emoteTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)emoteTexture.Width / 2f, (float)emoteTexture.Height / 2f);
		float opacity = 1f - (float)Math.Pow(base.LifetimeCompletion, 4.0);
		SpriteEffects effect = (SpriteEffects)(!(Velocity.X > 0f));
		spriteBatch.Draw(emoteTexture, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, origin, Scale, effect, 0f);
	}
}
