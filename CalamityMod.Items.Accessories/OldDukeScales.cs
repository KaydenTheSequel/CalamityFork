using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "DukeScales" })]
public class OldDukeScales : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public static int RecoverTime = 90;

	public static int DashFatigueIncrease = 240;

	public static int MaxFatigue = 1200;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 26;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<OldDukeScalesPlayer>().OldDukeScalesOn = true;
		player.noKnockback = true;
	}
}
