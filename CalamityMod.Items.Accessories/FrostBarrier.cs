using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class FrostBarrier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 24;
		base.Item.defense = 10;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().fBarrier = true;
		player.buffImmune[46] = true;
		player.buffImmune[44] = true;
	}
}
