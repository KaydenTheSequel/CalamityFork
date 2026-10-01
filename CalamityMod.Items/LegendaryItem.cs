using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Items;

public abstract class LegendaryItem : ModItem, IHoldShiftTooltipItem
{
	public string ExtensionIndicatorKey => "Items.Misc.LegendaryShortTooltip";

	public Color? ExtensionIndicatorColor => null;

	public string TooltipExtensionKey => "LegendaryText";

	public virtual Color? TooltipExtensionColor => null;
}
