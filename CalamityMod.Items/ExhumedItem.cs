using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Items;

public abstract class ExhumedItem : ModItem, IHoldShiftTooltipItem
{
	public string ExtensionIndicatorKey => "Items.Misc.ExhumeShortTooltip";

	public Color? ExtensionIndicatorColor => null;

	public string TooltipExtensionKey => "Calamitas";

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(198, 27, 64);
		}
	}
}
