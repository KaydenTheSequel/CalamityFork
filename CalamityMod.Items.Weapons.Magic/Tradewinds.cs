using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Tradewinds : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 23;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 5;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item7;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<TradewindsProjectile>();
		base.Item.shootSpeed = 25f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(6).AddIngredient(824, 5).AddTile(101)
			.Register();
	}
}
