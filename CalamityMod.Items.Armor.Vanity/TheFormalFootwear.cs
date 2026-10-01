using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class TheFormalFootwear : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.vanity = true;
		base.Item.rare = 1;
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(225, 5).AddIngredient(259, 2).AddRecipeGroup("AnyGoldBar")
			.AddIngredient(1015)
			.AddTile(86)
			.Register();
	}
}
