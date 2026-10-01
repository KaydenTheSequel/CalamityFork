using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CalamityMod.Dialogues;
using CalamityMod.Packets;
using CalamityMod.UI.DialogueDisplay.DisplayEffects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityMod.UI.DialogueDisplay;

public class DialogueDisplaySystem : ModSystem
{
	public enum DisplayEffectID
	{
		Invalid = -1,
		None,
		AlwaysOnScreen,
		BossText,
		Built,
		WhisperingPearls
	}

	internal static DialogueDisplayUI State;

	internal static UserInterface UI;

	public static DisplayEffectID GetID(object obj)
	{
		if (!(obj is DisplayEffect))
		{
			return DisplayEffectID.Invalid;
		}
		if (!(obj is AlwaysOnScreen))
		{
			if (!(obj is BossText))
			{
				if (!(obj is BuiltEffect))
				{
					if (obj is WhisperingPearlEffects)
					{
						return DisplayEffectID.WhisperingPearls;
					}
					return DisplayEffectID.None;
				}
				return DisplayEffectID.Built;
			}
			return DisplayEffectID.BossText;
		}
		return DisplayEffectID.AlwaysOnScreen;
	}

	public static DisplayEffect GetEffect(DisplayEffectID id)
	{
		return id switch
		{
			DisplayEffectID.AlwaysOnScreen => new AlwaysOnScreen(), 
			DisplayEffectID.BossText => new BossText(), 
			DisplayEffectID.Built => new BuiltEffect(), 
			DisplayEffectID.WhisperingPearls => new WhisperingPearlEffects(), 
			_ => new DisplayEffect(), 
		};
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			UI = new UserInterface();
			State = new DialogueDisplayUI();
			State.Activate();
		}
	}

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
	{
		int preInventory = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Interface Logic 2");
		if (preInventory != -1)
		{
			layers.Insert(preInventory, new LegacyGameInterfaceLayer("Dialogue Display", delegate
			{
				//IL_000a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Expected O, but got Unknown
				UI.Draw(Main.spriteBatch, new GameTime());
				return true;
			}));
		}
	}

	public override void UpdateUI(GameTime gameTime)
	{
		if (UI?.CurrentState != null)
		{
			UI?.Update(gameTime);
		}
	}

	public static Color GetColorFromHex(string hex)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Color color = ColorTranslator.FromHtml("#" + hex);
		short num = Convert.ToInt16(color.R);
		int g = Convert.ToInt16(color.G);
		int b = Convert.ToInt16(color.B);
		return new Color((int)num, g, b);
	}

	public static int GetSlot(string name)
	{
		foreach (KeyValuePair<int, (string, DialogueDisplay, DialogueTextData, Entity, int)> pair in DialogueDisplayUI.Dialogues)
		{
			if (pair.Value.Item1 == name)
			{
				return pair.Key;
			}
		}
		return -1;
	}

	public static void ProgressDialogue(int slot)
	{
		if (!DialogueDisplayUI.Dialogues.TryGetValue(slot, out (string, DialogueDisplay, DialogueTextData, Entity, int) val))
		{
			return;
		}
		DialogueDisplay display = val.Item2;
		if (!display.SwitchingPage)
		{
			if (display.textIndex < display.Text.Length - 1)
			{
				display.textIndex = display.Text.Length - 1;
			}
			else
			{
				display.SwitchingPage = true;
			}
		}
	}

	public static void EndDialogue(int slot)
	{
		if (DialogueDisplayUI.Dialogues.TryGetValue(slot, out (string, DialogueDisplay, DialogueTextData, Entity, int) val))
		{
			val.Item2.ClosingDialogue = true;
		}
	}

	public static void RemoveDialogue(int slot)
	{
		DialogueDisplayUI.DialoguesToRemove.Add(slot);
	}

	public static bool ContainsDialogueKey(string key)
	{
		return DialogueDisplayUI.Dialogues.Any((KeyValuePair<int, (string name, DialogueDisplay ui, DialogueTextData data, Entity entity, int upTime)> d) => d.Value.name == key);
	}

	public static int StartDialogue(string name, Vector2 startPosition, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1f, int toClient = -1, int ignoreClient = -1)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			StartDialogueDisplayPacket.Send(name, progressDialogue, startPosition, startIndex, Uptime, GetID(effects), wrapWidth, toClient, ignoreClient);
			return -1;
		}
		if (Main.netMode == 0)
		{
			return StartDialogueOnClient(name, startPosition, startIndex, Uptime, progressDialogue, effects, wrapWidth);
		}
		return -1;
	}

	public static int StartDialogueOnClient(string name, Vector2 startPosition, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1f)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return -1;
		}
		if (UI == null)
		{
			UI = new UserInterface();
		}
		if (State == null)
		{
			State = new DialogueDisplayUI();
		}
		if (effects == null)
		{
			effects = new DisplayEffect();
		}
		if (!DialogueLoader.TryGetDialogue(name, out var textData))
		{
			CalamityMod.Log.Error((object)("Unable to find Dialogue Data for given name: '" + name + "'"));
			return -1;
		}
		if (startIndex >= textData.PageCount)
		{
			startIndex = textData.PageCount - 1;
		}
		DialogueDisplay display = new DialogueDisplay(textData.Pages[startIndex], effects, 0, screenLocked: false, wrapWidth)
		{
			Position = startPosition,
			ProgressDialogue = progressDialogue
		};
		int slot;
		for (slot = 0; slot <= DialogueDisplayUI.Dialogues.Count && DialogueDisplayUI.Dialogues.ContainsKey(slot); slot++)
		{
		}
		DialogueDisplayUI.Dialogues.Add(slot, (name, display, textData, null, Uptime));
		State.Append(display);
		display.Activate();
		if (UI.CurrentState != State)
		{
			UI?.SetState(State);
		}
		return slot;
	}

	public static int StartDialogue(string name, Entity entity, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1f, int toClient = -1, int ignoreClient = -1)
	{
		if (Main.dedServ)
		{
			StartDialogueDisplayPacket.Send(name, progressDialogue, (!(entity is NPC)) ? ((entity is Player) ? StartDialogueDisplayPacket.EntityType.Player : StartDialogueDisplayPacket.EntityType.Projectile) : StartDialogueDisplayPacket.EntityType.NPC, (entity is Projectile p) ? p.identity : entity.whoAmI, startIndex, Uptime, GetID(effects), wrapWidth, toClient, ignoreClient);
			return -1;
		}
		if (Main.netMode == 0)
		{
			return StartDialogueOnClient(name, entity, startIndex, Uptime, progressDialogue, effects, wrapWidth);
		}
		return -1;
	}

	public static int StartDialogueOnClient(string name, Entity entity, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1f)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return -1;
		}
		if (UI == null)
		{
			UI = new UserInterface();
		}
		if (State == null)
		{
			State = new DialogueDisplayUI();
		}
		if (effects == null)
		{
			effects = new DisplayEffect();
		}
		if (!DialogueLoader.TryGetDialogue(name, out var textData))
		{
			CalamityMod.Log.Error((object)("Unable to find Dialogue Data for given name: '" + name + "'"));
			return -1;
		}
		if (startIndex >= textData.PageCount)
		{
			startIndex = textData.PageCount - 1;
		}
		DialogueDisplay display = new DialogueDisplay(textData[startIndex], effects, 0, screenLocked: false, wrapWidth)
		{
			Position = entity.Center,
			ProgressDialogue = progressDialogue
		};
		int slot;
		for (slot = 0; slot <= DialogueDisplayUI.Dialogues.Count && DialogueDisplayUI.Dialogues.ContainsKey(slot); slot++)
		{
		}
		DialogueDisplayUI.Dialogues.Add(slot, (name, display, textData, entity, Uptime));
		State.Append(display);
		display.Activate();
		if (UI.CurrentState != State)
		{
			UI?.SetState(State);
		}
		return slot;
	}

	public static void EndAllDialogue()
	{
		DialogueDisplayUI.Dialogues.Clear();
		State.RemoveAllChildren();
		UI?.SetState(null);
	}
}
