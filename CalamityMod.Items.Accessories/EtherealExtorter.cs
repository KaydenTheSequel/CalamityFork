using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class EtherealExtorter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.accessory = true;
		base.Item.value = Item.buyPrice(1);
		base.Item.rare = 8;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.etherealExtorter = true;
		player.GetDamage<ThrowingDamageClass>() += 0.08f;
		calamityPlayer.rogueStealthMax += 0.05f;
	}
}
