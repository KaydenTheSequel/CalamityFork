using CalamityMod.Items.BaseItems;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class SharkyPlush : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[3]
	{
		(EquipType.Head, "Shark", null),
		(EquipType.Body, "Shark", null),
		(EquipType.Legs, "Shark", null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 24;
		base.Item.accessory = true;
		base.Item.vanity = true;
		base.Item.rare = 1;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.Calamity().devItem = true;
	}
}
