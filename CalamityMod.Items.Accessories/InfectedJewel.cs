using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "CelestialJewel" })]
public class InfectedJewel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.defense = 4;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().infectedJewel = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CrownJewel>().AddIngredient<AureusCell>(10).AddIngredient<StarblightSoot>(25)
			.AddTile(134)
			.Register();
	}
}
