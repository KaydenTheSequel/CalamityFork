using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class YharonMask : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			ArmorIDs.Head.Sets.DrawHead[base.Item.headSlot] = false;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.rare = 1;
		base.Item.value = Item.sellPrice(0, 0, 75);
		base.Item.vanity = true;
	}
}
