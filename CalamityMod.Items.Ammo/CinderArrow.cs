using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "NapalmArrow" })]
public class CinderArrow : ModItem, ILocalizedModType, IModType
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
		base.Item.damage = 12;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 12);
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<CinderArrowProj>();
		base.Item.shootSpeed = 13f;
		base.Item.ammo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		CreateRecipe(250).AddIngredient(265, 250).AddIngredient<UnholyCore>().AddTile(134)
			.Register();
	}
}
