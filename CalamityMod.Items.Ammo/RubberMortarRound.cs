using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class RubberMortarRound : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 14;
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 7f;
		base.Item.value = Item.sellPrice(0, 0, 0, 20);
		base.Item.rare = 8;
		base.Item.ammo = AmmoID.Bullet;
		base.Item.shoot = ModContent.ProjectileType<RubberMortarRoundProj>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(100).AddIngredient<MortarRound>(100).AddIngredient(3111, 5).AddTile(18)
			.Register();
	}
}
