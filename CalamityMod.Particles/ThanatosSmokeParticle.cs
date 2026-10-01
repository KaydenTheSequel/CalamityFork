using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class ThanatosSmokeParticle : Particle
{
	public float RelativePower;

	public float BaseMoveRotation;

	public override bool SetLifetime => true;

	public override int FrameVariants => 3;

	public override string Texture => "CalamityMod/Particles/MediumSmoke";

	public ThanatosSmokeParticle(Vector2 relativePosition, int lifetime, float scale, float relativePower, float baseMoveRotation)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		RelativeOffset = relativePosition;
		Velocity = Vector2.Zero;
		Scale = scale;
		Variant = Main.rand.Next(3);
		Lifetime = lifetime;
		RelativePower = relativePower;
		BaseMoveRotation = baseMoveRotation;
	}

	public override void Update()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		if (Scale < 1.25f)
		{
			Scale += RelativePower * 0.04f;
		}
		RelativeOffset -= (BaseMoveRotation + Main.rand.NextFloat(-0.18f, 0.18f)).ToRotationVector2() * RelativePower * 4.5f;
		Color = Color.DarkRed;
		Color = Color.Lerp(Color, new Color(154, 139, 138), (float)Math.Pow(Utils.GetLerpValue(0f, 0.57f, base.LifetimeCompletion, clamped: true), 2.0) * 0.5f + 0.5f);
		((Color)(ref Color)).A = 92;
		float opacity = Utils.GetLerpValue(1f, 0.85f, base.LifetimeCompletion, clamped: true);
		Color *= opacity;
	}
}
