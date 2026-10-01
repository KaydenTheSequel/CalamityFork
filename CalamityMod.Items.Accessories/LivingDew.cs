using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class LivingDew : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statLifeMax2 += 25;
		if (!player.HasBuff(48))
		{
			player.AddBuff(48, 2);
		}
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.honeyDewHalveDebuffs = true;
		calamityPlayer.livingDewHalveDebuffs = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<HoneyDew>().AddIngredient<LivingShard>(6).AddIngredient<EssenceofSunlight>(5)
			.AddTile(134)
			.Register();
	}
}
