using System;
using System.Collections.Generic;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Systems;

public class WorldSelectionDifficultySystem : ModSystem
{
	public record WorldDifficulty(string name, Func<AWorldListItem, bool> function, Color color);

	public static List<WorldDifficulty> WorldDifficulties = new List<WorldDifficulty>();

	public override void PostSetupContent()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (ExternalMods.luminance == null)
		{
			WorldDifficulties.Add(new WorldDifficulty(CalamityUtils.GetTextValue("UI.Revengeance"), GetRevengeance, new Color(211, 42, 42)));
			WorldDifficulties.Add(new WorldDifficulty(CalamityUtils.GetTextValue("UI.Death"), GetDeath, new Color(192, 64, 219)));
			WorldDifficulties.Add(new WorldDifficulty(CalamityUtils.GetTextValue("UI.Malice"), GetMalice, new Color(240, 128, 128)));
		}
	}

	public override void SaveWorldHeader(TagCompound tag)
	{
		tag["RevengeanceMode"] = CalamityWorld.revenge;
		tag["DeathMode"] = CalamityWorld.death;
	}

	public static bool GetRevengeance(AWorldListItem item)
	{
		if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out var tag) && tag.ContainsKey("RevengeanceMode") && tag.GetBool("RevengeanceMode"))
		{
			return true;
		}
		return false;
	}

	public static bool GetDeath(AWorldListItem item)
	{
		WorldFileData worldData = item._data;
		if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out var tag))
		{
			if (tag.ContainsKey("DeathMode") && tag.GetBool("DeathMode"))
			{
				return true;
			}
			if (tag.ContainsKey("RevengeanceMode") && tag.GetBool("RevengeanceMode"))
			{
				return worldData.ForTheWorthy;
			}
		}
		return false;
	}

	public static bool GetMalice(AWorldListItem item)
	{
		WorldFileData data = item._data;
		int trueGameMode = data.GameMode;
		if (data.ForTheWorthy)
		{
			trueGameMode++;
		}
		if (item.Data.TryGetHeaderData<WorldSelectionDifficultySystem>(out var tag) && tag.ContainsKey("DeathMode") && tag.GetBool("DeathMode") && trueGameMode == 3)
		{
			return true;
		}
		return false;
	}
}
