using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class HoneyDew : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statLifeMax2 += 10;
		if (!player.HasBuff(48))
		{
			player.AddBuff(48, 2);
		}
		player.Calamity().honeyDewHalveDebuffs = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1134, 10).AddIngredient(2431, 3).AddIngredient(331, 6)
			.AddTile(16)
			.Register();
	}
}
