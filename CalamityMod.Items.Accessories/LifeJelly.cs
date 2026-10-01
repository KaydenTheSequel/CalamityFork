using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class LifeJelly : ModItem, ILocalizedModType, IModType
{
	public static int AuraLifetime = 1800;

	public static int AuraRegenBoost = 4;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AuraLifetime.FramesToSeconds(), AuraRegenBoost.ToRegenPerSecond());

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
		player.Calamity().lifejelly = true;
	}
}
