using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class SmallSmokeParticle : Particle
{
	private float Opacity;

	private Color ColorFire;

	private Color ColorFade;

	private float Spin;

	public override string Texture => "CalamityMod/Particles/SmallSmoke";

	public SmallSmokeParticle(Vector2 position, Vector2 velocity, Color colorFire, Color colorFade, float scale, float opacity, float rotationSpeed = 0f, bool affectedByLight = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		ColorFire = colorFire;
		ColorFade = colorFade;
		Scale = scale;
		Opacity = opacity;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		Spin = rotationSpeed;
		AffectedByLight = affectedByLight;
	}

	public override void Update()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		Velocity *= 0.85f;
		if (Opacity > 90f)
		{
			Lighting.AddLight(Position, ((Color)(ref Color)).ToVector3() * 0.1f);
			Scale += 0.01f;
			Opacity -= 3f;
		}
		else
		{
			Scale *= 0.975f;
			Opacity -= 2f;
		}
		if (Opacity < 0f)
		{
			Kill();
		}
		Color = Color.Lerp(ColorFire, ColorFade, MathHelper.Clamp((255f - Opacity - 100f) / 80f, 0f, 1f)) * (Opacity / 255f);
		if (AffectedByLight)
		{
			Color = Lighting.GetColor((Position / 16f).ToPoint()).MultiplyRGBA(Color);
		}
	}
}
