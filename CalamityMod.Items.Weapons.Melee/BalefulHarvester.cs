using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class BalefulHarvester : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 92;
		base.Item.height = 106;
		base.Item.damage = 150;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useStyle = 5;
		base.Item.useAnimation = (base.Item.useTime = 90);
		base.Item.useTurn = true;
		base.Item.knockBack = 8f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BalefulHarvesterHoldout>();
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1826).AddIngredient(3459, 12).AddTile(412)
			.Register();
	}
}
