using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class StatChangeArrow : Particle
{
	public bool AffectedByGravity;

	public Color StartColor;

	public Color EndColor;

	public override string Texture => "CalamityMod/Particles/StatChangeArrow";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public StatChangeArrow(Vector2 position, Vector2 velocity, float rotation, Color startColor, Color endColor, float scale, int lifetime)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = startColor;
		StartColor = startColor;
		EndColor = endColor;
		Scale = scale;
		Lifetime = lifetime;
		Rotation = rotation;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Color = Color.Lerp(StartColor, EndColor, base.LifetimeCompletion);
		Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * 0.2f);
		if (((Vector2)(ref Velocity)).Length() < 12f && AffectedByGravity)
		{
			Velocity.X *= 0.94f;
			Velocity.Y += 0.25f;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 DrawSize = default(Vector2);
		((Vector2)(ref DrawSize))._002Ector(Scale);
		Vector2 Origin = new Vector2((float)texture.Width, (float)texture.Height) / 2f;
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, Origin, DrawSize, (SpriteEffects)0, 0f);
	}
}
