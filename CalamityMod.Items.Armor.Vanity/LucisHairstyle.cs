using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class LucisHairstyle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.value = Item.buyPrice(0, 10);
		base.Item.rare = 5;
		base.Item.vanity = true;
		base.Item.Calamity().donorItem = true;
	}
}
