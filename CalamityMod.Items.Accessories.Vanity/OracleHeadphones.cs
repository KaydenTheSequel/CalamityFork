using CalamityMod.Items.BaseItems;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class OracleHeadphones : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[4]
	{
		(EquipType.Head, "Mishiro", null),
		(EquipType.Body, "Mishiro", null),
		(EquipType.Legs, "Mishiro", null),
		(EquipType.Back, "Mishiro", null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 30;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.vanity = true;
		base.Item.Calamity().devItem = true;
	}
}
