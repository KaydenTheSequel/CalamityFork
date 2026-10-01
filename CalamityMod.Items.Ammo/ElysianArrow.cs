using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class ElysianArrow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 36;
		base.Item.damage = 15;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 3f;
		base.Item.value = Item.sellPrice(0, 0, 0, 24);
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<ElysianArrowProj>();
		base.Item.shootSpeed = 10f;
		base.Item.ammo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		CreateRecipe(150).AddIngredient(516, 150).AddIngredient<UnholyEssence>().AddTile(134)
			.Register();
	}
}
