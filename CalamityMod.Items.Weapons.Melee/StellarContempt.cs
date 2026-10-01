using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "StellarContemptMelee", "StellarContemptRogue" })]
public class StellarContempt : ModItem, ILocalizedModType, IModType
{
	public static float Speed = 30f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 74;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 650;
		base.Item.knockBack = 28f;
		base.Item.useTime = 45;
		base.Item.useAnimation = 45;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<StellarContemptHammer>();
		base.Item.shootSpeed = Speed;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FallenPaladinsHammer>().AddIngredient(3467, 5).AddIngredient<GalacticaSingularity>(5)
			.AddTile(412)
			.Register();
	}
}
