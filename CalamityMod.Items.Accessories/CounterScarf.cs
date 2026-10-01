using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Neck })]
public class CounterScarf : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 38;
		base.Item.value = Item.buyPrice(0, 20);
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.dodgeScarf = true;
		calamityPlayer.DashID = CounterScarfDash.ID;
		player.dashType = 0;
	}
}
