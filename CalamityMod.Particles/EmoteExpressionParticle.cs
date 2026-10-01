using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class EmoteExpressionParticle : Particle
{
	public enum EmoteType
	{
		Exclamation,
		DoubleExclamation,
		QuestionExclamation,
		Question,
		Note,
		DoubleNote,
		Smile,
		BigSmile
	}

	private Rectangle Frame;

	public override string Texture => "CalamityMod/Particles/EmoteExpressions";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public EmoteExpressionParticle(Vector2 position, Vector2 velocity, float scale, Color color, int lifeTime, EmoteType emote)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Scale = scale;
		Velocity = velocity;
		Color = color;
		Lifetime = lifeTime;
		Frame = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value.Frame(8, 1, (int)emote);
		Rotation = velocity.ToRotation() + (float)Math.PI / 2f;
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
