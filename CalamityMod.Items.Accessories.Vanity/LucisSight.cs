using CalamityMod.Items.BaseItems;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class LucisSight : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[1] { (EquipType.Face, "LucisSight", null) };

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
		base.Item.value = Item.buyPrice(0, 10);
		base.Item.rare = 5;
		base.Item.accessory = true;
		base.Item.vanity = true;
		base.Item.Calamity().donorItem = true;
	}
}
