using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class Baroclaw : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public static int ThornsDamage => 150.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().baroclaw = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CrawCarapace>().AddIngredient<DepthCells>(12).AddTile(134)
			.Register();
	}
}
