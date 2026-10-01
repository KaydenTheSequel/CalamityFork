using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class FireParticle : Particle
{
	public float RelativePower;

	public Color BrightColor;

	public Color DarkColor;

	public override bool SetLifetime => true;

	public override int FrameVariants => 3;

	public override string Texture => "CalamityMod/Particles/Fire";

	public FireParticle(Vector2 relativePosition, int lifetime, float scale, float relativePower, Color brightColor, Color darkColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		RelativeOffset = relativePosition;
		Velocity = Vector2.Zero;
		Scale = scale;
		Variant = Main.rand.Next(3);
		Lifetime = lifetime;
		RelativePower = relativePower;
		BrightColor = brightColor;
		DarkColor = darkColor;
	}

	public override void Update()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		Scale += RelativePower * 0.01f;
		RelativeOffset.Y -= RelativePower * 3f;
		Color = Color.Lerp(BrightColor, DarkColor, base.LifetimeCompletion);
		Color = Color.Lerp(Color, Color.SaddleBrown, Utils.GetLerpValue(0.95f, 0.7f, base.LifetimeCompletion, clamped: true));
		Color = Color.Lerp(Color, Color.White, Utils.GetLerpValue(0.1f, 0.25f, base.LifetimeCompletion, clamped: true) * Utils.GetLerpValue(0.4f, 0.25f, base.LifetimeCompletion, clamped: true) * 0.7f);
		Color *= Utils.GetLerpValue(0f, 0.15f, base.LifetimeCompletion, clamped: true) * Utils.GetLerpValue(1f, 0.8f, base.LifetimeCompletion, clamped: true) * 0.6f;
		((Color)(ref Color)).A = 50;
	}
}
