using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "TerraBullet" })]
public class DryadsTear : ModItem, ILocalizedModType, IModType
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
		base.Item.damage = 10;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 1.25f;
		base.Item.value = Item.sellPrice(0, 0, 0, 16);
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<DryadsTearMain>();
		base.Item.shootSpeed = 2f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void AddRecipes()
	{
		CreateRecipe(100).AddIngredient(515, 100).AddIngredient<LivingShard>().AddTile(134)
			.Register();
	}
}
