using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class SandCloak : ModItem, ILocalizedModType, IModType
{
	public static int SandVeilDefenseBoost = 4;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 44;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().sandCloak = true;
	}
}
