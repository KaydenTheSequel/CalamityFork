using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BloodPact : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.rare = 8;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.bloodPact = true;
		calamityPlayer.healingPotionMultiplier += 0.33f;
		player.buffImmune[30] = true;
		player.buffImmune[ModContent.BuffType<BurningBlood>()] = true;
		player.buffImmune[ModContent.BuffType<HeavyBleeding>()] = true;
	}
}
