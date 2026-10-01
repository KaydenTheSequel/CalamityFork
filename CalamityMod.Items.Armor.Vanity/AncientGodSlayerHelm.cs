using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class AncientGodSlayerHelm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.vanity = true;
		base.Item.value = Item.sellPrice(0, 15);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<AncientGodSlayerChestplate>())
		{
			return legs.type == ModContent.ItemType<AncientGodSlayerLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}
}
