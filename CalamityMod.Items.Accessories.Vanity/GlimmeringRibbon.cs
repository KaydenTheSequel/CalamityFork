using CalamityMod.Items.BaseItems;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

internal class GlimmeringRibbon : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[3]
	{
		(EquipType.Head, "Charlotte", null),
		(EquipType.Body, "Charlotte", null),
		(EquipType.Legs, "Charlotte", null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 46;
		base.Item.accessory = true;
		base.Item.vanity = true;
		base.Item.rare = 1;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.Calamity().devItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(225, 10).AddIngredient(75, 3).AddTile(86)
			.Register();
	}
}
