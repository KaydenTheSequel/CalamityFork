using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class WulfrumDroidEmote : Particle
{
	public Rectangle Frame;

	public override string Texture => "CalamityMod/Particles/WulfrumDroidEmotes";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public WulfrumDroidEmote(Vector2 position, Vector2 velocity, int lifeTime, float scale = 1f, int variant = -1)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = Color.White;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = velocity.ToRotation() + (float)Math.PI / 2f;
		if (variant == -1)
		{
			variant = Main.rand.Next(15);
		}
		Frame = new Rectangle(16 * (variant % 8), 16 * (variant / 8), 16, 16);
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		Velocity *= 0.96f;
		Scale *= 0.97f;
		Color lightColor = ((Frame.Y > 0) ? new Color(112, 244, 244) : new Color(194, 255, 62));
		Lighting.AddLight(Position, ((Color)(ref lightColor)).ToVector3());
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Texture2D emoteTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)Frame.Width / 2f, (float)Frame.Height);
		float opacity = 1f - (float)Math.Pow(base.LifetimeCompletion, 4.0);
		spriteBatch.Draw(emoteTexture, Position - Main.screenPosition, (Rectangle?)Frame, Color * opacity, Rotation, origin, Scale, (SpriteEffects)0, 0f);
	}
}
