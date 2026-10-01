namespace CalamityMod.Events;

public struct AcidRainSpawnData
{
	public int InvasionContributionPoints { get; set; }

	public float SpawnRate { get; set; }

	public AcidRainSpawnRequirement SpawnRequirement { get; set; }

	public AcidRainSpawnData(int totalPoints, float spawnRate, AcidRainSpawnRequirement spawnRequirement)
	{
		InvasionContributionPoints = totalPoints;
		SpawnRate = spawnRate;
		SpawnRequirement = spawnRequirement;
	}
}
