using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class FlameParticle : Particle
{
	public float RelativePower;

	public Color BrightColor;

	public Color DarkColor;

	public override string Texture => "CalamityMod/Particles/Flames";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public FlameParticle(Vector2 position, int lifetime, float scale, float relativePower, Color brightColor, Color darkColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = Vector2.Zero;
		Velocity.X = Main.rand.NextFloat(1f, -1f);
		Scale = scale;
		Variant = Main.rand.Next(3);
		Lifetime = lifetime;
		RelativePower = relativePower;
		BrightColor = brightColor;
		DarkColor = darkColor;
	}

	public override void Update()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Scale += RelativePower * 0.01f;
		Position.Y -= RelativePower * 1.25f;
		Scale *= 0.97f;
		Color = Color.Lerp(BrightColor, DarkColor, base.LifetimeCompletion);
		Color = Color.Lerp(Color, Color.White, Utils.GetLerpValue(0.1f, 0.25f, base.LifetimeCompletion, clamped: true) * Utils.GetLerpValue(0.4f, 0.25f, base.LifetimeCompletion, clamped: true) * 0.7f);
		Color *= Utils.GetLerpValue(0f, 0.15f, base.LifetimeCompletion, clamped: true) * Utils.GetLerpValue(1f, 0.8f, base.LifetimeCompletion, clamped: true) * 0.6f;
		Color *= 1.5f;
		((Color)(ref Color)).A = 50;
	}
}
