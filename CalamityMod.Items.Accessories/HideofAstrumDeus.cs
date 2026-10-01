using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class HideofAstrumDeus : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int BlazeDamage => 50.ScaleWithDifficulty();

	public static int StarDamage => 75;

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.hideOfDeus = true;
		if (calamityPlayer.hideOfDeusMeleeBoostTimer > 0)
		{
			player.GetDamage<TrueMeleeDamageClass>() += 0.3f;
		}
	}
}
