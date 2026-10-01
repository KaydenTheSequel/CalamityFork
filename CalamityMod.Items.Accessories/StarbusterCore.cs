using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class StarbusterCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().starbusterCore = true;
	}
}
