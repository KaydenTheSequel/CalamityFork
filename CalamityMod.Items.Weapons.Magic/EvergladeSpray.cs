using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class EvergladeSpray : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 30;
		base.Item.damage = 65;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useTime = 6;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item13;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<EvergladeSprayProjectile>();
		base.Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1336).AddIngredient<PerennialBar>(3).AddTile(101)
			.Register();
		CreateRecipe().AddIngredient(519).AddIngredient<PerennialBar>(3).AddTile(101)
			.Register();
	}
}
