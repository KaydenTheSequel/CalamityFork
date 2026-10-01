using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class TheComb : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 42;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.vanity = true;
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().combHair = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			player.Calamity().combHair = true;
		}
	}
}
