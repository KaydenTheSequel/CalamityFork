using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class TheBee : ModItem, ILocalizedModType, IModType
{
	public static int CooldownLength = 360;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().theBee = true;
	}
}
