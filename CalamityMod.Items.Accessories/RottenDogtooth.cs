using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RottenDogtooth : ModItem, ILocalizedModType, IModType
{
	internal const int ArmorCrunchDebuffTime = 150;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().rottenDogTooth = true;
	}
}
