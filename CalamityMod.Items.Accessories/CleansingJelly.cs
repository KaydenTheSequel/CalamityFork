using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "ManaJelly" })]
public class CleansingJelly : ModItem, ILocalizedModType, IModType
{
	public static int AuraLifetime = 1800;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AuraLifetime.FramesToSeconds());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().cleansingjelly = true;
	}
}
