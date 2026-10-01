using System;
using System.Collections.Generic;
using CalamityMod.BiomeManagers;
using CalamityMod.Enums;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

public sealed class SunkenSeaBiomeCorrespondentDict : ModSystem
{
	public static IDictionary<SunkenSeaBiomeFlags, (Func<NPCSpawnInfo, bool> SpawnCondition, int BiomeType)> Dict { get; private set; }

	public override void OnModLoad()
	{
		Dict = new Dictionary<SunkenSeaBiomeFlags, (Func<NPCSpawnInfo, bool>, int)>
		{
			{
				SunkenSeaBiomeFlags.UndergroundDesert,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneDesert, -1)
			},
			{
				SunkenSeaBiomeFlags.TimelessShores,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.Calamity().ZoneTimelessShores, ModContent.GetInstance<TimelessShoresBiome>().Type)
			},
			{
				SunkenSeaBiomeFlags.RadiantReefs,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.Calamity().ZoneRadiantReefs, ModContent.GetInstance<RadiantReefsBiome>().Type)
			},
			{
				SunkenSeaBiomeFlags.PolypForest,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.Calamity().ZonePolypForest, ModContent.GetInstance<PolypForestBiome>().Type)
			},
			{
				SunkenSeaBiomeFlags.GleamingBurrows,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.Calamity().ZoneGleamingBurrows, ModContent.GetInstance<GleamingBurrowsBiome>().Type)
			},
			{
				SunkenSeaBiomeFlags.BasaltGully,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.Calamity().ZoneBasaltGully, ModContent.GetInstance<BasaltGullyBiome>().Type)
			},
			{
				SunkenSeaBiomeFlags.ClamDen,
				((NPCSpawnInfo spawnInfo) => spawnInfo.Player.Calamity().ZoneClamDen, ModContent.GetInstance<ClamDenBiome>().Type)
			}
		};
	}

	public override void Unload()
	{
		Dict?.Clear();
		Dict = null;
	}

	public static bool TryGet(SunkenSeaBiomeFlags flags, out Func<NPCSpawnInfo, bool> spawnCondition, out int biomeType)
	{
		if (!Dict.TryGetValue(flags, out (Func<NPCSpawnInfo, bool>, int) biomeInfoTuple))
		{
			spawnCondition = null;
			biomeType = 0;
			return false;
		}
		(spawnCondition, biomeType) = biomeInfoTuple;
		return true;
	}
}
