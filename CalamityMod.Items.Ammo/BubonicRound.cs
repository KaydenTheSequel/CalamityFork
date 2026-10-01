using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "AcidBullet", "AcidRound" })]
public class BubonicRound : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 8;
		base.Item.height = 8;
		base.Item.damage = 16;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 16);
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<BubonicRoundProj>();
		base.Item.shootSpeed = 10f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void AddRecipes()
	{
		CreateRecipe(150).AddIngredient(97, 150).AddIngredient<PlagueCellCanister>().AddTile(134)
			.Register();
	}
}
