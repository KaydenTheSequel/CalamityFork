using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class FleshTotem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.rare = 8;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().fleshTotem = true;
	}
}
