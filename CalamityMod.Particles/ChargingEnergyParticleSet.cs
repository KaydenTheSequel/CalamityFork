using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class ChargingEnergyParticleSet : BaseParticleSet
{
	public float InterpolationSpeed;

	public float EdgeOffset;

	public Color EdgeColor;

	public Color CenterColor;

	public override int ParticleLifetime => 50;

	public ChargingEnergyParticleSet(int setLifetime, int particleSpawnRate, Color edgeColor, Color centerColor, float interpolationSpeed, float edgeOffset)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector(setLifetime, particleSpawnRate);
		EdgeColor = edgeColor;
		CenterColor = centerColor;
		InterpolationSpeed = interpolationSpeed;
		EdgeOffset = edgeOffset;
	}

	public override Particle SpawnParticle()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return new EnchantedParticle(Main.rand.NextVector2CircularEdge(1f, 1f) * EdgeOffset, ParticleLifetime, 0.1f, EdgeColor, CenterColor, InterpolationSpeed, EdgeOffset);
	}
}
