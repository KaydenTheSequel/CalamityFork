using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class NightsRay : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 54;
		base.Item.damage = 20;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.25f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item72;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<NightsRayBeam>();
		base.Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(64).AddIngredient(113).AddIngredient(5147)
			.AddIngredient(3377)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.AddCondition(Condition.NotRemixWorld)
			.Register();
		CreateRecipe().AddIngredient(1256).AddIngredient(113).AddIngredient(5147)
			.AddIngredient(3377)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.AddCondition(Condition.NotRemixWorld)
			.Register();
		CreateRecipe().AddIngredient(64).AddIngredient(113).AddIngredient(517)
			.AddIngredient(3377)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.AddCondition(Condition.RemixWorld)
			.Register();
		CreateRecipe().AddIngredient(1256).AddIngredient(113).AddIngredient(517)
			.AddIngredient(3377)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.AddCondition(Condition.RemixWorld)
			.Register();
	}
}
