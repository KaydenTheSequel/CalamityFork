using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

public abstract class LoreItem : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Lore";

	public bool HidesNormalTooltip => true;

	public string ExtensionIndicatorKey => LocalizationCategory + ".ShortTooltip";

	public Color? ExtensionIndicatorColor => null;

	public string TooltipExtensionKey => "Lore";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemNoGravity[base.Type] = true;
	}

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)12000;
	}
}
