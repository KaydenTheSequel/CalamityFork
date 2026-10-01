using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class VitalJelly : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.moveSpeed += 0.12f;
		player.jumpSpeedBoost += 0.6f;
	}
}
