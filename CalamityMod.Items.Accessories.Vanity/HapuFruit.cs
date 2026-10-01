using CalamityMod.Items.BaseItems;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class HapuFruit : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[4]
	{
		(EquipType.Head, "Heart", null),
		(EquipType.Body, "Heart", null),
		(EquipType.Legs, "Heart", null),
		(EquipType.Back, "Heart", null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 30;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.vanity = true;
		base.Item.Calamity().devItem = true;
	}
}
