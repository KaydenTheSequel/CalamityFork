using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class BloodfireArrow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 36;
		base.Item.damage = 19;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 24);
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<BloodfireArrowProj>();
		base.Item.shootSpeed = 10f;
		base.Item.ammo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		CreateRecipe(333).AddIngredient<BloodstoneCore>().AddTile(134).Register();
	}
}
