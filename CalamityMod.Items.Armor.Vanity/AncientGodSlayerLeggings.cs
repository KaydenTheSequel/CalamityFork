using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class AncientGodSlayerLeggings : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.vanity = true;
		base.Item.value = Item.sellPrice(0, 9);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}
}
