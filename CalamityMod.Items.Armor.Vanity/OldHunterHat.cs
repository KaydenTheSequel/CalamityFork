using CalamityMod.Items.Armor.DesertProwler;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class OldHunterHat : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.vanity = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DesertProwlerHat>().AddIngredient(1119).AddTile(228)
			.Register();
	}
}
