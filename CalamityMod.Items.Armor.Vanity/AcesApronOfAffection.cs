using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
[LegacyName(new string[] { "ApronOfAffection" })]
public class AcesApronOfAffection : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 1;
		base.Item.Calamity().donorItem = true;
		base.Item.vanity = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(262).AddIngredient(2352, 10).AddIngredient(29)
			.AddTile(86)
			.Register();
	}
}
