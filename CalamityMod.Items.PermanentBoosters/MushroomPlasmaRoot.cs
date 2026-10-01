using System.Collections.Generic;
using CalamityMod.Balancing;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.PermanentBoosters;

public class MushroomPlasmaRoot : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Misc";

	public override LocalizedText Tooltip => CalamityUtils.GetText(LocalizationCategory + ".RageBoosterTooltip").WithFormatArgs(BalancingConstants.RageDurationPerBooster.FramesToSeconds());

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.consumable = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.UseSound = SoundID.Item122;
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 2;
		base.Item.SetRevExclusive();
	}

	public static bool HasConsumedBefore(Player player)
	{
		return player.Calamity().rageBoostOne;
	}

	public override bool CanUseItem(Player player)
	{
		if (HasConsumedBefore(player))
		{
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (player.itemAnimation > 0 && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			player.Calamity().rageBoostOne = true;
		}
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		if (HasConsumedBefore(Main.LocalPlayer))
		{
			list.AddConsumedTooltip("Tooltip0");
		}
	}
}
