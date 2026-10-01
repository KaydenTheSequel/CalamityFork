using CalamityMod.Items.Armor.Silva;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class SilvaHelm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 20;
		base.Item.vanity = true;
		base.Item.value = Item.buyPrice(0, 10);
		base.Item.rare = 1;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<SilvaArmor>())
		{
			return legs.type == ModContent.ItemType<SilvaLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}
}
