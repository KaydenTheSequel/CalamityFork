using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

[LegacyName(new string[] { "TerraArrow" })]
public class SproutingArrow : ModItem, ILocalizedModType, IModType
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
		base.Item.damage = 14;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 20);
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<SproutingArrowMain>();
		base.Item.shootSpeed = 15f;
		base.Item.ammo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		CreateRecipe(250).AddIngredient(40, 250).AddIngredient<LivingShard>().AddTile(134)
			.Register();
	}
}
