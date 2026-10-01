using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class BloodfireBullet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 30;
		base.Item.damage = 17;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 24);
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<BloodfireBulletProj>();
		base.Item.shootSpeed = 0.1f;
		base.Item.ammo = 97;
	}

	public override void AddRecipes()
	{
		CreateRecipe(333).AddIngredient<BloodstoneCore>().AddTile(134).Register();
	}
}
