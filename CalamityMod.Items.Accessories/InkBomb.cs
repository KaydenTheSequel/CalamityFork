using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class InkBomb : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int InkDamage => 16.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 50;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.inkBomb = true;
		calamityPlayer.stealthGenStandstill += 0.07f;
		calamityPlayer.stealthGenMoving += 0.07f;
	}
}
