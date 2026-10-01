using CalamityMod.Items.SummonItems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer;

public class BloodwormFishPowerWarning : GlobalInfoDisplay
{
	public override void ModifyDisplayParameters(InfoDisplay currentDisplay, ref string displayValue, ref string displayName, ref Color displayColor, ref Color displayShadowColor)
	{
		if (currentDisplay != InfoDisplay.FishFinder)
		{
			return;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == Main.myPlayer && p.bobber)
			{
				return;
			}
		}
		if (Main.LocalPlayer.GetFishingConditions().BaitItemType == ModContent.ItemType<BloodwormItem>())
		{
			displayValue = Language.GetTextValue("GameUI.FishingWarning");
		}
	}
}
