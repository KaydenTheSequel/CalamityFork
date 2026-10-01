using Microsoft.Xna.Framework;

namespace CalamityMod.Items;

public interface IHoldShiftTooltipItem
{
	internal const string ExtensionTooltipID = "CalamityMod:HoldShiftTooltip";

	internal const string ExtensionIndicatorTooltipID = "CalamityMod:HoldShiftExtensionIndicator";

	internal const string FlavorTooltipID = "CalamityMod:FlavorTooltip";

	const string DefaultExtensionIndicatorKey = "UI.HoldShiftTooltipExtensionIndicator";

	const string DefaultReplacementIndicatorKey = "UI.HoldShiftTooltipReplacementIndicator";

	static readonly Color DefaultExtensionIndicatorColor;

	const string DefaultTooltipExtensionKey = "HoldShiftTooltip";

	const string DefaultFlavorTooltipKey = "FlavorTooltip";

	bool HidesNormalTooltip => false;

	bool ShowExtensionIndicator => true;

	string ExtensionIndicatorKey
	{
		get
		{
			if (!HidesNormalTooltip)
			{
				return "UI.HoldShiftTooltipExtensionIndicator";
			}
			return "UI.HoldShiftTooltipReplacementIndicator";
		}
	}

	Color? ExtensionIndicatorColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return DefaultExtensionIndicatorColor;
		}
	}

	string TooltipExtensionKey => "HoldShiftTooltip";

	Color? TooltipExtensionColor => null;

	bool HasFlavorTooltip => false;

	string FlavorTooltipKey => "FlavorTooltip";

	Color? FlavorTooltipColor => null;

	static IHoldShiftTooltipItem()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		DefaultExtensionIndicatorColor = new Color(184, 184, 184);
	}
}
