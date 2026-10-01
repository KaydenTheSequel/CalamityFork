using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ManaDrainStreak : Particle
{
	public Player Owner;

	public float StartDistanceFromPlayer;

	public float FinalDistanceFromPlayer;

	public Color StartColor;

	public Color EndColor;

	public Vector2 OverridePosition;

	public bool FadeOut;

	public override string Texture => "CalamityMod/Particles/DrainLineBloom";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public ManaDrainStreak(Player owner, float thickness, Vector2 startVector, float finalDistance, Color colorStart, Color colorEnd, int lifetime, Vector2 overridePosition = default(Vector2), bool fadeOut = false)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Owner = owner;
		Scale = thickness;
		Velocity = Vector2.Zero;
		Rotation = startVector.ToRotation();
		StartDistanceFromPlayer = ((Vector2)(ref startVector)).Length();
		FinalDistanceFromPlayer = finalDistance;
		StartColor = colorStart;
		EndColor = colorEnd;
		Color = colorStart;
		Lifetime = lifetime;
		OverridePosition = overridePosition;
		FadeOut = fadeOut;
	}

	public override void Update()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		if (Owner != null && Owner.active && !Owner.dead)
		{
			Vector2 setPosition = ((OverridePosition != default(Vector2)) ? OverridePosition : Owner.MountedCenter);
			Position = setPosition + Rotation.ToRotationVector2() * MathHelper.Lerp(StartDistanceFromPlayer, FinalDistanceFromPlayer, (float)Math.Pow(base.LifetimeCompletion, 2.0));
			Color startColor = StartColor;
			Color val;
			if (!FadeOut)
			{
				val = EndColor;
			}
			else
			{
				Color endColor = EndColor;
				((Color)(ref endColor)).A = 0;
				val = endColor;
			}
			Color = Color.Lerp(startColor, val, base.LifetimeCompletion);
			Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * 0.2f);
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		float num = MathHelper.Lerp(StartDistanceFromPlayer, FinalDistanceFromPlayer, (float)Math.Pow(base.LifetimeCompletion, 2.0));
		float earlierDisplace = MathHelper.Lerp(StartDistanceFromPlayer, FinalDistanceFromPlayer, (float)Math.Pow(Math.Clamp(base.LifetimeCompletion - 0.2f, 0f, 1f), 2.0));
		float Length = (num - earlierDisplace) / (float)tex.Height;
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(Scale, Length);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)tex.Width / 2f, (float)tex.Height);
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation - (float)Math.PI / 2f, origin, scale, (SpriteEffects)0, 0f);
	}
}
