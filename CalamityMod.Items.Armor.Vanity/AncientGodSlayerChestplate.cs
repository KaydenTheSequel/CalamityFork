using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class AncientGodSlayerChestplate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.vanity = true;
		base.Item.value = Item.sellPrice(0, 12);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}
}
