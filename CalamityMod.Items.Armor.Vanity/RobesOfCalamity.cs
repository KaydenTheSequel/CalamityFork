using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
[LegacyName(new string[] { "CalamityRobes" })]
public class RobesOfCalamity : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Vanity/RobesOfCalamity_Legs", EquipType.Legs, this);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 20;
		base.Item.rare = 7;
		base.Item.vanity = true;
		base.Item.Calamity().donorItem = true;
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
	{
		robes = true;
		equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Legs);
	}
}
