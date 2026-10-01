using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ForbiddenSun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 80;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 33;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ForbiddenSunProjectile>();
		base.Item.shootSpeed = 13f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddIngredient<ScoriaBar>(6).AddTile(101)
			.Register();
	}
}
