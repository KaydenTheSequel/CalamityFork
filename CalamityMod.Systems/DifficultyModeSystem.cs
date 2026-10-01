using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CalamityMod.UI.ModeIndicator;
using CalamityMod.World;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Systems;

public class DifficultyModeSystem : ModSystem
{
	internal static bool _hasCheckedItOutYet = false;

	internal static int _newGameModeID = 0;

	public static List<DifficultyMode> Difficulties = new List<DifficultyMode>();

	public static List<DifficultyMode[]> DifficultyTiers;

	public static int MostAlternateDifficulties;

	public static MethodInfo journeyDifficultyUpdateMethod;

	private static readonly Dictionary<int, (float, float)> compatitableValueRanges = new Dictionary<int, (float, float)>
	{
		{
			0,
			(0.33f, 0.66f)
		},
		{
			1,
			(0.66f, 1f)
		},
		{
			2,
			(1f, 1f)
		}
	};

	public static DifficultyMode GetCurrentDifficulty
	{
		get
		{
			DifficultyMode mode = Difficulties[0];
			for (int i = 1; i < Difficulties.Count; i++)
			{
				if (Difficulties[i].Enabled)
				{
					mode = Difficulties[i];
				}
			}
			return mode;
		}
	}

	public override void OnModLoad()
	{
		MostAlternateDifficulties = 1;
		Difficulties = new List<DifficultyMode>
		{
			ModContent.GetInstance<NoDifficulty>(),
			ModContent.GetInstance<ExpertDifficulty>(),
			ModContent.GetInstance<RevengeanceDifficulty>(),
			ModContent.GetInstance<MasterDifficulty>(),
			ModContent.GetInstance<DeathDifficulty>(),
			ModContent.GetInstance<LegendaryDifficulty>(),
			ModContent.GetInstance<MaliceDifficulty>()
		};
		journeyDifficultyUpdateMethod = typeof(CreativePowers.DifficultySliderPower).GetMethod("UpdateInfoFromSliderValueCache", BindingFlags.Instance | BindingFlags.NonPublic);
		CalculateDifficultyData();
	}

	public override void PostWorldLoad()
	{
		CalculateDifficultyData();
	}

	public override void OnModUnload()
	{
		Difficulties = null;
	}

	public override void PostUpdateWorld()
	{
		if (Main.GameMode == 3)
		{
			CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
			if (power.GetIsUnlocked())
			{
				float sliderValue = ((CreativePowers.ASharedSliderPower)power)._sliderCurrentValueCache;
				int effectiveVanillaDifficulty = ((sliderValue == 1f) ? 2 : ((sliderValue >= 0.66f) ? 1 : 0));
				HandleExternalGameModeChange(effectiveVanillaDifficulty);
			}
		}
		else
		{
			HandleExternalGameModeChange(Main.GameMode);
		}
		static void HandleExternalGameModeChange(int tier)
		{
			switch (tier)
			{
			case 0:
				if (Main.getGoodWorld)
				{
					if (GetCurrentDifficulty.CountAs<DeathDifficulty>())
					{
						ModeIndicatorUI.SwitchToDifficulty(ModContent.GetInstance<RevengeanceDifficulty>(), broadcast: true);
					}
				}
				else if (GetCurrentDifficulty.CountAs<DeathDifficulty>() || GetCurrentDifficulty.CountAs<RevengeanceDifficulty>())
				{
					ModeIndicatorUI.SwitchToDifficulty(ModContent.GetInstance<NoDifficulty>(), broadcast: true);
				}
				break;
			case 1:
				if (Main.getGoodWorld)
				{
					if (GetCurrentDifficulty.CountAs<LegendaryDifficulty>())
					{
						ModeIndicatorUI.SwitchToDifficulty(ModContent.GetInstance<DeathDifficulty>(), broadcast: true);
					}
				}
				else if (GetCurrentDifficulty.CountAs<DeathDifficulty>())
				{
					ModeIndicatorUI.SwitchToDifficulty(ModContent.GetInstance<RevengeanceDifficulty>(), broadcast: true);
				}
				break;
			case 2:
				if (Main.getGoodWorld)
				{
					if (!GetCurrentDifficulty.CountAs<MaliceDifficulty>() && CalamityWorld.revenge)
					{
						ModeIndicatorUI.SwitchToDifficulty(ModContent.GetInstance<MaliceDifficulty>(), broadcast: true);
					}
				}
				else if (!GetCurrentDifficulty.CountAs<DeathDifficulty>() && CalamityWorld.revenge)
				{
					ModeIndicatorUI.SwitchToDifficulty(ModContent.GetInstance<DeathDifficulty>(), broadcast: true);
				}
				break;
			}
		}
	}

	public static void CalculateDifficultyData()
	{
		MostAlternateDifficulties = 1;
		Difficulties = Difficulties.OrderBy((DifficultyMode d) => d.DifficultyScale).ToList();
		DifficultyTiers = new List<DifficultyMode[]>();
		float currentTier = -1f;
		int tierIndex = -1;
		for (int i = 0; i < Difficulties.Count; i++)
		{
			if ((!Main.getGoodWorld || Difficulties[i].GetForTheWorthyDisplay != DifficultyMode.FTWDisplayMode.NotForTheWorthy) && (Main.getGoodWorld || Difficulties[i].GetForTheWorthyDisplay != DifficultyMode.FTWDisplayMode.OnlyForTheWorthy))
			{
				if (currentTier != Difficulties[i].DifficultyScale)
				{
					DifficultyTiers.Add(new DifficultyMode[1] { Difficulties[i] });
					currentTier = Difficulties[i].DifficultyScale;
					tierIndex++;
				}
				else
				{
					DifficultyTiers[tierIndex] = DifficultyTiers[tierIndex].Append(Difficulties[i]).ToArray();
					MostAlternateDifficulties = Math.Max(DifficultyTiers[tierIndex].Length, MostAlternateDifficulties);
				}
				Difficulties[i]._difficultyTier = tierIndex;
			}
		}
	}

	public static void AlignJourneyDifficultySlider()
	{
		if (Main.GameMode != 3)
		{
			throw new ArgumentException("DifficultyModeSystemAlignJourneyDifficultySlider(): must be invoked in journey mode");
		}
		CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
		float oldValue = ((CreativePowers.ASharedSliderPower)power)._sliderCurrentValueCache;
		if (compatitableValueRanges.TryGetValue(_newGameModeID, out var value))
		{
			(float, float) tuple = value;
			float low = tuple.Item1;
			float high = tuple.Item2;
			float valueToSet = ((!(oldValue >= low) || !(oldValue < high)) ? low : oldValue);
			((CreativePowers.ASharedSliderPower)power)._sliderCurrentValueCache = valueToSet;
			journeyDifficultyUpdateMethod.Invoke(power, null);
			return;
		}
		throw new ArgumentException("DifficultyModeSystemAlignJourneyDifficultySlider(): _newGameModeID must be in GameModeID.Normal, Expert, Master");
	}

	public override void SaveWorldData(TagCompound tag)
	{
		tag["hasCheckedOutTheCoolDifficultyUI"] = _hasCheckedItOutYet;
	}

	public override void OnWorldLoad()
	{
		_hasCheckedItOutYet = false;
	}

	public override void OnWorldUnload()
	{
		_hasCheckedItOutYet = false;
	}

	public override void PostWorldGen()
	{
		_hasCheckedItOutYet = false;
	}

	public override void LoadWorldData(TagCompound tag)
	{
		_hasCheckedItOutYet = tag.GetBool("hasCheckedOutTheCoolDifficultyUI");
		if (CalamityWorld.revenge)
		{
			_hasCheckedItOutYet = true;
		}
	}
}
