using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using ReLogic.OS;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.UI;

public class WindowTitles : ModSystem
{
	private static LocalizedText _calamityModifiedText;

	private static bool loaded;

	public override void PostSetupContent()
	{
		if (Main.dedServ)
		{
			return;
		}
		Main.QueueMainThreadAction(delegate
		{
			List<LocalizedText> collection = Language.FindAll(new Regex("^GameTitle\\.")).ToList();
			List<LocalizedText> collection2 = Language.FindAll(new Regex("^Mods\\.CalamityMod\\.UI\\.WindowTitle\\.")).ToList();
			List<LocalizedText> list = new List<LocalizedText>();
			list.AddRange(collection);
			list.AddRange(collection2);
			if (_calamityModifiedText == null)
			{
				_calamityModifiedText = list[Main.rand.Next(list.Count)];
			}
			Platform.Get<IWindowService>().SetUnicodeTitle(((Game)Main.instance).Window, _calamityModifiedText.Value);
			Platform.Get<IWindowService>().SetIcon(((Game)Main.instance).Window);
			loaded = true;
		});
	}

	public override void Unload()
	{
		if (!Main.dedServ)
		{
			Main.QueueMainThreadAction(delegate
			{
				Platform.Get<IWindowService>().SetUnicodeTitle(((Game)Main.instance).Window, Lang.GetRandomGameTitle());
				Platform.Get<IWindowService>().SetIcon(((Game)Main.instance).Window);
				_calamityModifiedText = null;
				loaded = false;
			});
			base.Unload();
		}
	}
}
