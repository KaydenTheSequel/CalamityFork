using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[]
{
	EquipType.HandsOn,
	EquipType.HandsOff
})]
public class BloodstainedGlove : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 3;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().bloodyGlove = true;
	}
}
