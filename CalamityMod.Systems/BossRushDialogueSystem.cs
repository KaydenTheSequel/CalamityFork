using System;
using System.Collections.Generic;
using CalamityMod.Enums;
using CalamityMod.Events;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class BossRushDialogueSystem : ModSystem
{
	internal struct BossRushDialogueEvent
	{
		private const int DefaultFrameDelay = 180;

		internal int FrameDelay;

		internal string LocalizationKey;

		internal Func<bool> skipCondition;

		public BossRushDialogueEvent()
		{
			FrameDelay = 180;
			LocalizationKey = null;
			skipCondition = null;
		}

		public BossRushDialogueEvent(string key)
		{
			LocalizationKey = key;
			FrameDelay = 180;
			skipCondition = null;
		}

		public BossRushDialogueEvent(string key, int delay = 180, Func<bool> skipFunc = null)
		{
			LocalizationKey = key;
			FrameDelay = delay;
			skipCondition = skipFunc;
		}

		public readonly bool ShouldDisplay()
		{
			if (skipCondition == null)
			{
				return true;
			}
			return !skipCondition();
		}
	}

	public static bool GottaGoFast = false;

	public static int GottaGoFastSpeed = 5;

	public static BossRushDialoguePhase Phase = BossRushDialoguePhase.None;

	private static BossRushDialogueEvent[] currentSequence = null;

	public static int currentSequenceIndex = 0;

	public static int CurrentDialogueDelay = 0;

	internal static Dictionary<BossRushDialoguePhase, BossRushDialogueEvent[]> BossRushDialogue;

	public override void Load()
	{
		BossRushDialogueEvent[] startDialogues = new BossRushDialogueEvent[15]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_1", 330),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_2", 405),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_3", 210),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_4", 405),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_5", 450),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_6", 240),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_7", 435),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_8", 450),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_9", 300),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_DoG", 150, () => !DownedBossSystem.downedDoG),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_Yharon", 210, () => !DownedBossSystem.downedYharon),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_DraedonSCal", 285, () => !DownedBossSystem.downedExoMechs || !DownedBossSystem.downedCalamitas),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_10", 390),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_11", 390),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_12", 240)
		};
		BossRushDialogueEvent[] startDialoguesShort = new BossRushDialogueEvent[1]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushStartText_Repeat", 90)
		};
		BossRushDialogueEvent[] tierOneDialogues = new BossRushDialogueEvent[2]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierOneEndText_1", 330),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierOneEndText_2", 225)
		};
		BossRushDialogueEvent[] tierTwoDialogues = new BossRushDialogueEvent[2]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierTwoEndText_1", 225),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierTwoEndText_2", 270)
		};
		BossRushDialogueEvent[] tierThreeDialogues = new BossRushDialogueEvent[2]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierThreeEndText_1", 435),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierThreeEndText_2", 300)
		};
		BossRushDialogueEvent[] tierFourDialogues = new BossRushDialogueEvent[3]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierFourEndText_1", 135),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierFourEndText_2", 300),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushTierFourEndText_3", 270)
		};
		BossRushDialogueEvent[] endDialogues = new BossRushDialogueEvent[8]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_1", 510),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_2", 435),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_3", 330),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_4", 255),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_5", 390),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_6", 390),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_7", 195),
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_8", 105)
		};
		BossRushDialogueEvent[] endDialoguesShort = new BossRushDialogueEvent[1]
		{
			new BossRushDialogueEvent("Mods.CalamityMod.Events.BossRushEndText_Repeat", 480)
		};
		BossRushDialogue = new Dictionary<BossRushDialoguePhase, BossRushDialogueEvent[]>
		{
			{
				BossRushDialoguePhase.Start,
				startDialogues
			},
			{
				BossRushDialoguePhase.StartRepeat,
				startDialoguesShort
			},
			{
				BossRushDialoguePhase.TierOneComplete,
				tierOneDialogues
			},
			{
				BossRushDialoguePhase.TierTwoComplete,
				tierTwoDialogues
			},
			{
				BossRushDialoguePhase.TierThreeComplete,
				tierThreeDialogues
			},
			{
				BossRushDialoguePhase.TierFourComplete,
				tierFourDialogues
			},
			{
				BossRushDialoguePhase.End,
				endDialogues
			},
			{
				BossRushDialoguePhase.EndRepeat,
				endDialoguesShort
			}
		};
	}

	public override void Unload()
	{
		BossRushDialogue = null;
	}

	public static void StartDialogue(BossRushDialoguePhase phaseToRun)
	{
		Phase = phaseToRun;
		if (BossRushDialogue.TryGetValue(Phase, out var dialogueListToUse))
		{
			currentSequence = dialogueListToUse;
			currentSequenceIndex = 0;
		}
		CurrentDialogueDelay = 4;
	}

	internal static void Tick()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (Phase == BossRushDialoguePhase.None)
		{
			return;
		}
		if (currentSequenceIndex < currentSequence.Length)
		{
			if (CurrentDialogueDelay == 0 && currentSequenceIndex < currentSequence.Length)
			{
				if (GetNextUnskippedDialogue(currentSequence, currentSequenceIndex, out var currentIndex))
				{
					BossRushDialogueEvent line = currentSequence[currentSequenceIndex];
					if (line.skipCondition == null || !line.skipCondition())
					{
						CalamityUtils.BroadcastLocalizedText(line.LocalizationKey, BossRushEvent.XerocTextColor);
						CurrentDialogueDelay = line.FrameDelay;
					}
					currentSequenceIndex = currentIndex + 1;
				}
			}
			else
			{
				CurrentDialogueDelay--;
			}
			if (BossRushEvent.BossRushSpawnCountdown < 180)
			{
				BossRushEvent.BossRushSpawnCountdown = CurrentDialogueDelay + 180;
			}
			if (GottaGoFast && CurrentDialogueDelay > GottaGoFastSpeed)
			{
				CurrentDialogueDelay = GottaGoFastSpeed;
			}
		}
		else
		{
			CurrentDialogueDelay = 0;
		}
		if (!BossRushEvent.BossRushActive)
		{
			Phase = BossRushDialoguePhase.None;
			currentSequence = null;
			currentSequenceIndex = 0;
			CurrentDialogueDelay = 0;
		}
	}

	private static bool GetNextUnskippedDialogue(BossRushDialogueEvent[] sequence, int index, out int newIndex)
	{
		for (int tryIndex = index; tryIndex < sequence.Length; tryIndex++)
		{
			BossRushDialogueEvent lineToTry = currentSequence[tryIndex];
			if (lineToTry.skipCondition == null || !lineToTry.skipCondition())
			{
				newIndex = tryIndex;
				return true;
			}
		}
		newIndex = -1;
		return false;
	}
}
