using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class PlagueHumidifierMist : Particle
{
	public override bool SetLifetime => true;

	public override int FrameVariants => 7;

	public override string Texture => "CalamityMod/Particles/PlagueHumidifierMist";

	public PlagueHumidifierMist(Vector2 relativePosition, int lifetime, float scale, Vector2 speed)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Velocity = speed;
		Scale = scale;
		Variant = Main.rand.Next(7);
		Lifetime = lifetime;
		Position = relativePosition;
	}

	public override void Update()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Color = Color.Lerp(Lighting.GetColor((Position / 16f).ToPoint()), Color.Transparent, base.LifetimeCompletion);
	}
}
