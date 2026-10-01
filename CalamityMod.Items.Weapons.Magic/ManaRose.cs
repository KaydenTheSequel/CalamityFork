using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ManaRose : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 38;
		base.Item.damage = 20;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 8;
		base.Item.useTime = 38;
		base.Item.useAnimation = 38;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.25f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ManaBolt>();
		base.Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(223).AddIngredient(109).AddIngredient(314, 5)
			.AddTile(16)
			.Register();
		CreateRecipe().AddIngredient(208).AddIngredient(109).AddIngredient(314, 5)
			.AddTile(16)
			.Register();
	}
}
