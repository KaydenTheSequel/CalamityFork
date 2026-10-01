using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class OldDie : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 26;
		base.Item.rare = 3;
		base.Item.value = Item.buyPrice(0, 20);
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().calamityBonusLuck += 0.2f;
	}
}
