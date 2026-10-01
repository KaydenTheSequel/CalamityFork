using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class CrawCarapace : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int ThornsDamage => 20.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().crawCarapace = true;
	}
}
