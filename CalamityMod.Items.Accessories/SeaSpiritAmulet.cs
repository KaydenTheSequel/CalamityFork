using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "AbyssalAmulet" })]
[AutoloadEquip(new EquipType[] { EquipType.Neck })]
public class SeaSpiritAmulet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.sSpiritAmulet = true;
		calamityPlayer.sSpiritAmuletVisual = !hideVisual;
		calamityPlayer.WaterDebuffMultiplier += 0.35f;
	}
}
