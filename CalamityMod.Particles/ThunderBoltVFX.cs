using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ThunderBoltVFX : Particle
{
	public Func<Vector2> FollowFunction;

	private float opacity;

	private float shakePower;

	public Vector2 Squish;

	public override string Texture => "CalamityMod/Particles/ThunderBolt";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public float Opacity
	{
		get
		{
			return opacity;
		}
		set
		{
			opacity = Math.Clamp(value, 0f, 1f);
		}
	}

	public float ShakePower
	{
		get
		{
			return shakePower;
		}
		set
		{
			shakePower = Math.Abs(value);
		}
	}

	public ThunderBoltVFX(Vector2 position, float rotation, float scale, Color color, int lifeTime, float shakePower, float opacity = 1f, Vector2? squish = null)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		this.opacity = 1f;
		Squish = Vector2.One;
		base._002Ector();
		Position = position;
		Rotation = rotation;
		Scale = scale;
		Color = color;
		Opacity = opacity;
		Lifetime = lifeTime;
		ShakePower = shakePower;
		if (squish.HasValue)
		{
			Squish = squish.Value;
		}
	}

	public ThunderBoltVFX(Func<Vector2> followFunction, float rotation, float scale, Color color, int lifeTime, float shakePower, float opacity = 1f, Vector2? squish = null)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		this.opacity = 1f;
		Squish = Vector2.One;
		base._002Ector();
		FollowFunction = followFunction;
		Rotation = rotation;
		Scale = scale;
		Color = color;
		Opacity = opacity;
		Lifetime = lifeTime;
		ShakePower = shakePower;
		if (squish.HasValue)
		{
			Squish = squish.Value;
		}
	}

	public override void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * 3f);
		float fadeFactor = 1f - 0.05f * MathHelper.Clamp((float)(Time - 10) / 10f, 0f, 1f);
		Opacity *= fadeFactor;
		Squish.X *= fadeFactor;
		if (FollowFunction != null)
		{
			Position = FollowFunction();
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 Shake = Vector2.One.RotatedByRandom(6.2831854820251465) * (1f - (float)Time / (float)Lifetime) * ShakePower;
		Vector2 Origin = default(Vector2);
		((Vector2)(ref Origin))._002Ector((float)tex.Width / 2f, (float)tex.Height);
		Color drawColor = Color.Lerp(Color.White, Color, (float)Time / (float)Lifetime);
		SpriteEffects flip = (SpriteEffects)(!(Main.GlobalTimeWrappedHourly % 30f < 15f));
		spriteBatch.Draw(tex, Position + Shake - Main.screenPosition, (Rectangle?)null, Color * Opacity * 0.6f, Rotation, Origin, Squish * Scale, flip, 0f);
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, drawColor * Opacity, Rotation, Origin, Squish * Scale, flip, 0f);
	}
}
