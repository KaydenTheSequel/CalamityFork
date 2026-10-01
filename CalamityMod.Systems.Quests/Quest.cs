using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Systems.Quests;

public abstract class Quest<TEnum> : ModSystem where TEnum : Enum
{
	public static bool QuestStarted { get; private set; }

	public static bool QuestFinished { get; private set; }

	public static Dictionary<TEnum, bool> QuestProgression { get; private set; } = ResetQuestProgression();

	public static event Action OnQuestStarted;

	public static event Action OnQuestFinish;

	public static event Action<TEnum> OnQuestProgression;

	public override void Unload()
	{
		QuestProgression = null;
	}

	public override void ClearWorld()
	{
		QuestStarted = false;
		QuestFinished = false;
		QuestProgression = ResetQuestProgression();
	}

	public override void LoadWorldData(TagCompound tag)
	{
		QuestStarted = tag.ContainsKey("questStarted");
		QuestFinished = tag.ContainsKey("questFinished");
		QuestProgression = ResetQuestProgression();
		if (tag.TryGet<List<bool>>("questProgression", out var value))
		{
			QuestProgression = QuestProgression.Keys.Zip(value, (TEnum k, bool v) => new
			{
				Key = k,
				Value = v
			}).ToDictionary(x => x.Key, x => x.Value);
		}
	}

	public override void SaveWorldData(TagCompound tag)
	{
		if (QuestStarted)
		{
			tag.Add("questStarted", QuestStarted);
		}
		if (QuestFinished)
		{
			tag.Add("questFinished", QuestFinished);
		}
		if (QuestProgression.ContainsValue(value: true))
		{
			tag.Add("questProgression", QuestProgression.Values.ToList());
		}
	}

	public static void StartQuest()
	{
		if (!QuestStarted)
		{
			QuestStarted = true;
			OnQuestStarted?.Invoke();
		}
	}

	public static void FinishQuest()
	{
		if (!QuestFinished)
		{
			QuestFinished = true;
			OnQuestFinish?.Invoke();
		}
	}

	public static void ProgressQuest(TEnum progressionPoint)
	{
		if (!QuestProgression[progressionPoint])
		{
			QuestProgression[progressionPoint] = true;
			OnQuestProgression?.Invoke(progressionPoint);
		}
	}

	private static Dictionary<TEnum, bool> ResetQuestProgression()
	{
		Dictionary<TEnum, bool> questProgression = new Dictionary<TEnum, bool>();
		foreach (TEnum progressionPoint in Enum.GetValues(typeof(TEnum)))
		{
			questProgression.Add(progressionPoint, value: false);
		}
		return questProgression;
	}
}
