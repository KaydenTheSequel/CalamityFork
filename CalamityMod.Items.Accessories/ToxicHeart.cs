using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ToxicHeart : ModItem, ILocalizedModType, IModType
{
	public float pulseCounter;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.toxicHeart = true;
		calamityPlayer.toxicHeartVisuals = !hideVisual;
		calamityPlayer.SicknessDebuffMultiplier += 0.5f;
	}
}
