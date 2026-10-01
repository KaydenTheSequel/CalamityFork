using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamityMod.Projectiles.Melee;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Basher : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 60;
		base.Item.damage = 40;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.knockBack = 7f;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<BasherHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(15).AddIngredient<SulphuricScale>(12).AddTile(16)
			.Register();
	}
}
