using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class FancyStars : Particle
{
	private readonly float _rotationSpeed;

	private readonly int _variant;

	public override string Texture => "CalamityMod/Particles/FancyStars";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool UseAdditiveBlend => true;

	public FancyStars(Vector2 position, float rotation, float scale, Vector2 velocity, float rotationSpeed, int lifeTime, Color color)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Rotation = rotation;
		Scale = Math.Max(0f, scale);
		Velocity = velocity;
		_rotationSpeed = rotationSpeed;
		Lifetime = lifeTime;
		Color = color;
		_variant = Main.rand.Next(5);
	}

	public override void Update()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		float interpolator = MathF.Pow(Utils.GetLerpValue(0f, 60f, Lifetime), 0.56f);
		Velocity *= interpolator;
		Rotation += _rotationSpeed * interpolator;
		Scale -= 0.01f * interpolator;
		Scale = Math.Max(0f, Scale);
		Color *= interpolator;
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Rectangle starVariant = texture.Frame(5, 1, _variant);
		spriteBatch.Draw(texture, Position - Main.screenPosition, (Rectangle?)starVariant, Color, Rotation, starVariant.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
	}
}
