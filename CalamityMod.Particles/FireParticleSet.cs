using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Particles;

public class FireParticleSet : BaseParticleSet
{
	public float SpawnAreaCompactness;

	public float RelativePower;

	public Color BrightColor;

	public Color DarkColor;

	public override int ParticleLifetime => 50;

	public FireParticleSet(int setLifetime, int particleSpawnRate, Color brightColor, Color darkColor, float spawnAreaCompactness, float relativePower)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector(setLifetime, particleSpawnRate);
		BrightColor = brightColor;
		DarkColor = darkColor;
		SpawnAreaCompactness = spawnAreaCompactness;
		RelativePower = relativePower;
	}

	public override Particle SpawnParticle()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		return new FireParticle(Main.rand.NextVector2Circular(1f, 1f) * SpawnAreaCompactness, ParticleLifetime, 0.06f, RelativePower, BrightColor, DarkColor);
	}
}
