using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[]
{
	EquipType.HandsOn,
	EquipType.HandsOff
})]
public class FilthyGlove : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 3;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().filthyGlove = true;
	}
}
