using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.UI;

[Autoload(true, Side = ModSide.Client)]
public sealed class InvasionProgressUIManager : ModSystem
{
	internal static readonly List<InvasionProgressUI> gUIs = new List<InvasionProgressUI>();

	public static int TotalGUIsActive => gUIs.Count((InvasionProgressUI gui) => gui.IsActive);

	public static bool AnyGUIsActive => TotalGUIsActive > 0;

	public static InvasionProgressUI GetActiveGUI => gUIs.FirstOrDefault((InvasionProgressUI gui) => gui.IsActive);

	public static void UpdateAndDraw(SpriteBatch spriteBatch)
	{
		if (AnyGUIsActive && GetActiveGUI != null)
		{
			GetActiveGUI.Draw(spriteBatch);
		}
	}

	public override void Unload()
	{
		gUIs?.Clear();
	}
}
