using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

[Autoload(true, Side = ModSide.Client)]
public sealed class PopupGUIManager : ModSystem
{
	internal static readonly List<PopupGUI> gUIs = new List<PopupGUI>();

	public static bool AnyGUIsActive => gUIs.Any(GUIActive);

	public static PopupGUI GetActiveGUI => gUIs.FirstOrDefault(GUIActive);

	public static bool GUIActive(PopupGUI gui)
	{
		if (!gui.Active)
		{
			return gui.FadeTime > 0;
		}
		return true;
	}

	public static void SuspendAll()
	{
		for (int i = 0; i < gUIs.Count; i++)
		{
			gUIs[i].Active = false;
			gUIs[i].FadeTime = 0;
		}
	}

	public static void UpdateAndDraw(SpriteBatch spriteBatch)
	{
		if (Main.ingameOptionsWindow || Main.inFancyUI || Main.InGameUI.IsVisible)
		{
			SuspendAll();
		}
		else
		{
			if (!AnyGUIsActive)
			{
				return;
			}
			Main.playerInventory = false;
			if (Main.LocalPlayer.sign > 0 || Main.LocalPlayer.talkNPC > 0)
			{
				Main.CloseNPCChatOrSign();
			}
			GetActiveGUI.Update();
			if (GetActiveGUI != null)
			{
				if (GetActiveGUI.FadeTime == 1 && !GetActiveGUI.Active)
				{
					spriteBatch.End();
					spriteBatch.Begin();
				}
				else
				{
					GetActiveGUI.Draw(spriteBatch);
				}
			}
		}
	}

	public static void FlipActivityOfGUIWithType(Type type)
	{
		if (gUIs.Any((PopupGUI gui) => gui.GetType() == type))
		{
			gUIs.First((PopupGUI gui) => gui.GetType() == type).Active = !gUIs.First((PopupGUI gui) => gui.GetType() == type).Active;
		}
	}

	public override void Unload()
	{
		gUIs?.Clear();
	}
}
