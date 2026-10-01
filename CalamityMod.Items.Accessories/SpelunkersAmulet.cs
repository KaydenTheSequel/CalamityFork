using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "ChaosAmulet" })]
public class SpelunkersAmulet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.pickSpeed -= 0.1f;
		player.findTreasure = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1348, 7).AddIngredient(296, 7).AddTile(16)
			.Register();
	}
}
