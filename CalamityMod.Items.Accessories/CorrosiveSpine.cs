using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class CorrosiveSpine : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 46;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.corrosiveSpine = true;
		player.moveSpeed += 0.05f;
		calamityPlayer.WaterDebuffMultiplier += 0.25f;
		calamityPlayer.SicknessDebuffMultiplier += 0.25f;
	}
}
