using System.Collections.Generic;
using System.IO;
using Terraria;

namespace CalamityMod.CustomRecipes;

public static class RecipeUnlockHandler
{
	public static bool HasUnlockedT1ArsenalRecipes;

	public static bool HasUnlockedT2ArsenalRecipes;

	public static bool HasUnlockedT3ArsenalRecipes;

	public static bool HasUnlockedT4ArsenalRecipes;

	public static bool HasUnlockedT5ArsenalRecipes;

	public static bool HasFoundSunkenSeaSchematic;

	public static bool HasFoundPlanetoidSchematic;

	public static bool HasFoundJungleSchematic;

	public static bool HasFoundHellSchematic;

	public static bool HasFoundIceSchematic;

	public static void Save(List<string> boolTagContainer)
	{
		boolTagContainer.AddWithCondition("HasUnlockedT1ArsenalRecipes", HasUnlockedT1ArsenalRecipes);
		boolTagContainer.AddWithCondition("HasUnlockedT2ArsenalRecipes", HasUnlockedT2ArsenalRecipes);
		boolTagContainer.AddWithCondition("HasUnlockedT3ArsenalRecipes", HasUnlockedT3ArsenalRecipes);
		boolTagContainer.AddWithCondition("HasUnlockedT4ArsenalRecipes", HasUnlockedT4ArsenalRecipes);
		boolTagContainer.AddWithCondition("HasUnlockedT5ArsenalRecipes", HasUnlockedT5ArsenalRecipes);
		boolTagContainer.AddWithCondition("HasFoundSunkenSeaSchematic", HasFoundSunkenSeaSchematic);
		boolTagContainer.AddWithCondition("HasFoundPlanetoidSchematic", HasFoundPlanetoidSchematic);
		boolTagContainer.AddWithCondition("HasFoundJungleSchematic", HasFoundJungleSchematic);
		boolTagContainer.AddWithCondition("HasFoundHellSchematic", HasFoundHellSchematic);
		boolTagContainer.AddWithCondition("HasFoundIceSchematic", HasFoundIceSchematic);
	}

	public static void Load(IList<string> boolTagContainer)
	{
		HasUnlockedT1ArsenalRecipes = boolTagContainer.Contains("HasUnlockedT1ArsenalRecipes");
		HasUnlockedT2ArsenalRecipes = boolTagContainer.Contains("HasUnlockedT2ArsenalRecipes");
		HasUnlockedT3ArsenalRecipes = boolTagContainer.Contains("HasUnlockedT3ArsenalRecipes");
		HasUnlockedT4ArsenalRecipes = boolTagContainer.Contains("HasUnlockedT4ArsenalRecipes");
		HasUnlockedT5ArsenalRecipes = boolTagContainer.Contains("HasUnlockedT5ArsenalRecipes");
		HasFoundSunkenSeaSchematic = boolTagContainer.Contains("HasFoundSunkenSeaSchematic");
		HasFoundPlanetoidSchematic = boolTagContainer.Contains("HasFoundPlanetoidSchematic");
		HasFoundJungleSchematic = boolTagContainer.Contains("HasFoundJungleSchematic");
		HasFoundHellSchematic = boolTagContainer.Contains("HasFoundHellSchematic");
		HasFoundIceSchematic = boolTagContainer.Contains("HasFoundIceSchematic");
	}

	public static void SendData(BinaryWriter writer)
	{
		BitsByte flags = new BitsByte
		{
			[0] = HasUnlockedT1ArsenalRecipes,
			[1] = HasUnlockedT2ArsenalRecipes,
			[2] = HasUnlockedT3ArsenalRecipes,
			[3] = HasUnlockedT4ArsenalRecipes,
			[4] = HasUnlockedT5ArsenalRecipes,
			[5] = HasFoundSunkenSeaSchematic,
			[6] = HasFoundPlanetoidSchematic,
			[7] = HasFoundJungleSchematic
		};
		BitsByte flags2 = new BitsByte
		{
			[0] = HasFoundHellSchematic,
			[1] = HasFoundIceSchematic
		};
		writer.Write(flags);
		writer.Write(flags2);
	}

	public static void ReceiveData(BinaryReader reader)
	{
		BitsByte flags = reader.ReadByte();
		HasUnlockedT1ArsenalRecipes = flags[0];
		HasUnlockedT2ArsenalRecipes = flags[1];
		HasUnlockedT3ArsenalRecipes = flags[2];
		HasUnlockedT4ArsenalRecipes = flags[3];
		HasUnlockedT5ArsenalRecipes = flags[4];
		HasFoundSunkenSeaSchematic = flags[5];
		HasFoundPlanetoidSchematic = flags[6];
		HasFoundJungleSchematic = flags[7];
		BitsByte flags2 = reader.ReadByte();
		HasFoundHellSchematic = flags2[0];
		HasFoundIceSchematic = flags2[1];
	}
}
