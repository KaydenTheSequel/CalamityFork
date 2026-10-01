using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ShadowboltStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 56;
		base.Item.damage = 285;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useTime = 8;
		base.Item.useAnimation = 24;
		base.Item.reuseDelay = 30;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<Shadowbolt>();
		base.Item.shootSpeed = 5f;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1444).AddIngredient<RuinousSoul>(2).AddIngredient<ArmoredShell>()
			.AddTile(134)
			.Register();
	}
}
