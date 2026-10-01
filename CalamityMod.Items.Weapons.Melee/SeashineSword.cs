using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SeashineSword : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.CloneDefaults(989);
		base.Item.width = 40;
		base.Item.height = 40;
		base.Item.useTime = 30;
		base.Item.damage = 25;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.knockBack = 4f;
		base.Item.shootSpeed = 12f;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<SeashineSwordProj>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PearlShard>(3).AddIngredient<SeaPrism>(7).AddIngredient<Navystone>(10)
			.AddTile(16)
			.Register();
	}
}
