using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "ArcticArrow", "VeriumBullet" })]
public class VeriumBolt : ModItem, ILocalizedModType, IModType
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
		base.Item.damage = 11;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 1.25f;
		base.Item.value = Item.sellPrice(0, 0, 0, 12);
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<VeriumBoltProj>();
		base.Item.shootSpeed = 16f;
		base.Item.ammo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		CreateRecipe(100).AddIngredient(40, 100).AddIngredient<CryonicBar>().AddTile(134)
			.Register();
	}
}
