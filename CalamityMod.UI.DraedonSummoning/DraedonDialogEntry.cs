using System;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace CalamityMod.UI.DraedonSummoning;

public class DraedonDialogEntry
{
	public string Inquiry;

	public string Response;

	public float BloomOpacity;

	public Func<bool> Condition;

	public ulong ID
	{
		get
		{
			ulong result = 0uL;
			byte[] bytes = Encoding.UTF8.GetBytes(Response);
			for (int i = 0; i < bytes.Length; i++)
			{
				result += (ulong)bytes[i] << i * 8;
			}
			return result;
		}
	}

	public bool HasBeenSeen => Main.LocalPlayer.Calamity().SeenDraedonDialogs.Contains(ID);

	internal DraedonDialogEntry(string inquiry, string response, Func<bool> condition = null)
	{
		Condition = condition ?? ((Func<bool>)(() => true));
		Inquiry = inquiry;
		Response = response;
	}

	public DraedonDialogEntry(string localizationKey, Func<bool> condition = null)
		: this(Language.GetTextValue(localizationKey + ".Inquiry"), Language.GetTextValue(localizationKey + ".Response"), condition)
	{
	}

	public void Update()
	{
		if (!HasBeenSeen)
		{
			BloomOpacity = 1f;
		}
		else
		{
			BloomOpacity = MathHelper.Clamp(BloomOpacity - 0.04f, 0f, 1f);
		}
	}
}
