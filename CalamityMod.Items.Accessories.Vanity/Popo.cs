using CalamityMod.Items.BaseItems;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class Popo : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[5]
	{
		(EquipType.Head, "Popo", null),
		(EquipType.Head, "PopoNoseless", "PopoNoseless"),
		(EquipType.Body, "Popo", null),
		(EquipType.Legs, "Popo", null),
		(EquipType.Face, null, null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 44;
		base.Item.accessory = true;
		base.Item.value = Item.buyPrice(5);
		base.Item.rare = 5;
		base.Item.Calamity().devItem = true;
	}

	public override bool CustomSetEquipType(Player player, EquipType type, Mod mod, string name)
	{
		if (type == EquipType.Head)
		{
			player.head = EquipLoader.GetEquipSlot(base.Mod, player.Calamity().snowmanNoseless ? "PopoNoseless" : "Popo", EquipType.Head);
			return true;
		}
		return false;
	}
}
