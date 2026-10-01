using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class MarksmanRound : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 12;
		base.Item.height = 26;
		base.Item.damage = 12;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 2.25f;
		base.Item.value = Item.sellPrice(0, 0, 0, 10);
		base.Item.rare = 4;
		base.Item.shoot = ModContent.ProjectileType<MarksmanShot>();
		base.Item.shootSpeed = 1f;
		base.Item.ammo = AmmoID.Bullet;
	}

	public override void AddRecipes()
	{
		CreateRecipe(999).AddIngredient(1352, 999).AddIngredient(73).AddTile(16)
			.Register();
	}
}
