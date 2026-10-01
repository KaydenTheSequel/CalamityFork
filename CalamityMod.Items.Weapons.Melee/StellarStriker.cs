using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class StellarStriker : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 118;
		base.Item.damage = 123;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 28);
		base.Item.useTurn = true;
		base.Item.knockBack = 7.75f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<StellarStrikerHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CometQuasher>().AddIngredient(3467, 5).AddTile(412)
			.Register();
	}
}
