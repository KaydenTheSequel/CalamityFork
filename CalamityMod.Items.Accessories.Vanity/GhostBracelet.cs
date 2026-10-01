using CalamityMod.Items.BaseItems;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

[LegacyName(new string[] { "RedBow" })]
public class GhostBracelet : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[3]
	{
		(EquipType.Head, "Dandy", null),
		(EquipType.Body, "Dandy", null),
		(EquipType.Legs, "Dandy", null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 38;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.vanity = true;
		base.Item.Calamity().devItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(225, 20).AddTile(86).Register();
	}
}
