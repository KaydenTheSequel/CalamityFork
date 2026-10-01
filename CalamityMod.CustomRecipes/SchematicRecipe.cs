using System;
using Terraria.Localization;

namespace CalamityMod.CustomRecipes;

public static class SchematicRecipe
{
	public static LocalizedText ConstructRecipeCondition(string schematicName, out Func<bool> condition)
	{
		switch (schematicName)
		{
		default:
			condition = () => RecipeUnlockHandler.HasFoundSunkenSeaSchematic;
			return Language.GetOrRegister("Mods.CalamityMod.Misc.SunkenSeaSchematicRecipeCondition");
		case "Planetoid":
			condition = () => RecipeUnlockHandler.HasFoundPlanetoidSchematic;
			return Language.GetOrRegister("Mods.CalamityMod.Misc.PlanetoidSchematicRecipeCondition");
		case "Jungle":
			condition = () => RecipeUnlockHandler.HasFoundJungleSchematic;
			return Language.GetOrRegister("Mods.CalamityMod.Misc.JungleSchematicRecipeCondition");
		case "Hell":
			condition = () => RecipeUnlockHandler.HasFoundHellSchematic;
			return Language.GetOrRegister("Mods.CalamityMod.Misc.UnderworldSchematicRecipeCondition");
		case "Ice":
			condition = () => RecipeUnlockHandler.HasFoundIceSchematic;
			return Language.GetOrRegister("Mods.CalamityMod.Misc.IceSchematicRecipeCondition");
		}
	}
}
