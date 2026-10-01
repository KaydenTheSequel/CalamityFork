using Microsoft.Xna.Framework;

namespace CalamityMod.Particles;

public class GenericBubbleParticle : Particle
{
	public override string Texture => "CalamityMod/Particles/Bubble";

	public override bool UseHalfTransparency => true;

	public override bool SetLifetime => true;

	public override bool Important => true;

	public GenericBubbleParticle(Vector2 position, Vector2 velocity, float scale, float rotation, int lifeTime = 50)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = Color.White;
		Scale = scale;
		Rotation = rotation;
		Lifetime = lifeTime;
	}
}
