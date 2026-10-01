using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class AcidwoodBow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.width = 20;
		base.Item.height = 50;
		base.Item.useTime = 27;
		base.Item.useAnimation = 27;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 0f;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 6.6f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(10).AddTile(18).Register();
	}
}
