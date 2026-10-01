using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class EnchantedParticle : Particle
{
	public float RelativePower;

	public float InterpolationSpeed;

	public float EdgeOffset;

	public Color EdgeColor;

	public Color CenterColor;

	public override bool SetLifetime => true;

	public override string Texture => "CalamityMod/Particles/Light";

	public EnchantedParticle(Vector2 relativePosition, int lifetime, float scale, Color edgeColor, Color centerColor, float interpolationSpeed, float edgeOffset)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		RelativeOffset = relativePosition;
		Velocity = Vector2.Zero;
		Scale = scale;
		Lifetime = lifetime;
		EdgeColor = edgeColor;
		CenterColor = centerColor;
		InterpolationSpeed = interpolationSpeed;
		EdgeOffset = edgeOffset;
	}

	public override void Update()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		float distanceToCenter = ((Vector2)(ref RelativeOffset)).Length();
		Scale = MathHelper.SmoothStep(0.05f, 0.125f, Utils.GetLerpValue(EdgeOffset, 6f, distanceToCenter, clamped: true));
		Scale *= Utils.GetLerpValue(Lifetime, (float)Lifetime - 10f, Time, clamped: true);
		if (distanceToCenter > 4.5f)
		{
			RelativeOffset = Vector2.Lerp(RelativeOffset, Vector2.Zero, InterpolationSpeed);
		}
		Color = Color.Lerp(EdgeColor, CenterColor, Utils.GetLerpValue(0f, 0.67f, base.LifetimeCompletion, clamped: true));
		((Color)(ref Color)).A = 50;
	}
}
