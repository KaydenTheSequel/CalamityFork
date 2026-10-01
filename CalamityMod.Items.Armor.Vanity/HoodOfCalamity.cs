using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "CalamityHood" })]
public class HoodOfCalamity : ModItem, ILocalizedModType, IModType
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
		base.Item.width = 26;
		base.Item.height = 24;
		base.Item.rare = 7;
		base.Item.vanity = true;
		base.Item.Calamity().donorItem = true;
		base.Item.value = Item.sellPrice(0, 2);
	}
}
