using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class VoltaicJelly : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 2;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().voltaicJelly = true;
	}
}
