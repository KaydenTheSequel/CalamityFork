using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "FlashBullet" })]
public class FlashRound : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 12;
		base.Item.height = 18;
		base.Item.damage = 6;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 6f;
		base.Item.value = Item.sellPrice(0, 0, 0, 1);
		base.Item.rare = 1;
		base.Item.shoot = ModContent.ProjectileType<FlashRoundProj>();
		base.Item.shootSpeed = 10f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void AddRecipes()
	{
		CreateRecipe(70).AddRecipeGroup("AnyCopperBar").AddIngredient(170).AddTile(16)
			.Register();
	}
}
