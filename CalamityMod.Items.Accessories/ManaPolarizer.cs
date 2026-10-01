using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "ManaOverloader" })]
public class ManaPolarizer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 30;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().manaOverloader = true;
		player.statManaMax2 += 50;
	}
}
