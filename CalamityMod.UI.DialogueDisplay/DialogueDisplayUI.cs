using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace CalamityMod.UI.DialogueDisplay;

internal class DialogueDisplayUI : UIState
{
	internal static readonly Dictionary<int, (string name, DialogueDisplay ui, DialogueTextData data, Entity entity, int upTime)> Dialogues = new Dictionary<int, (string, DialogueDisplay, DialogueTextData, Entity, int)>();

	internal static readonly List<int> DialoguesToRemove = new List<int>();

	public override void Update(GameTime gameTime)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		foreach (int index in DialoguesToRemove)
		{
			RemoveChild(Dialogues[index].ui);
			Dialogues.Remove(index);
		}
		DialoguesToRemove.Clear();
		foreach (KeyValuePair<int, (string, DialogueDisplay, DialogueTextData, Entity, int)> pair in Dialogues)
		{
			int slot = pair.Key;
			(string, DialogueDisplay, DialogueTextData, Entity, int) dialog = pair.Value;
			DialogueDisplay ui = dialog.Item2;
			DialogueTextData data = dialog.Item3;
			if (ui.DisplayEffects.FadeWhenTooFar && Vector2.Distance(Main.LocalPlayer.Center, ui.Position) > ui.DisplayEffects.FadeBuffer + ui.DisplayEffects.FadeDistance)
			{
				DialoguesToRemove.Add(slot);
				continue;
			}
			if (dialog.Item4 != null)
			{
				if (dialog.Item4.active)
				{
					dialog.Item2.Position = dialog.Item4.Center;
				}
				else if (ui.DisplayEffects.DespawnWithAttachedNPC)
				{
					dialog.Item2.ClosingDialogue = true;
				}
				else
				{
					dialog.Item2.Position = dialog.Item4.Center;
					dialog.Item4 = null;
				}
			}
			if (dialog.Item5 != -1 && ui.Uptime >= dialog.Item5)
			{
				if (ui.ProgressDialogue)
				{
					ui.SwitchingPage = true;
				}
				else
				{
					ui.ClosingDialogue = true;
				}
			}
			if (ui.DialoguePage.Event != null && ui.DialoguePage.Event.IsOver)
			{
				if (!ui.ProgressDialogue)
				{
					DialoguesToRemove.Add(slot);
					return;
				}
				if (++data.Page >= data.PageCount)
				{
					DialoguesToRemove.Add(slot);
					return;
				}
				ui.DialoguePage = data.Pages[data.Page];
				ui.SwitchingPage = false;
				ui.SwitchCounter = 0;
				Activate();
				return;
			}
			if (!ui.Switching)
			{
				continue;
			}
			if ((float)ui.SwitchCounter >= ui.DisplayEffects.TimeToDisappear)
			{
				if (ui.ClosingDialogue || !ui.ProgressDialogue)
				{
					DialoguesToRemove.Add(slot);
					continue;
				}
				if (++data.Page >= data.PageCount)
				{
					DialoguesToRemove.Add(slot);
					continue;
				}
				ui.DialoguePage = data.Pages[data.Page];
				ui.SwitchingPage = false;
				ui.SwitchCounter = 0;
				Activate();
			}
			else
			{
				ui.SwitchCounter++;
			}
		}
		base.Update(gameTime);
	}
}
