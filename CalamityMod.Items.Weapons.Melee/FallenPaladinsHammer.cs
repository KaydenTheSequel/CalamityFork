using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "TruePaladinsHammer", "TruePaladinsHammerMelee", "TruePaladinsHammerRogue" })]
public class FallenPaladinsHammer : ModItem, ILocalizedModType, IModType
{
	public static float Speed = 27f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 28;
		base.Item.damage = 368;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = (base.Item.useTime = 57);
		base.Item.useStyle = 1;
		base.Item.knockBack = 20f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<FallenPaladinsHammerProj>();
		base.Item.shootSpeed = Speed;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1513).AddIngredient<Pwnagehammer>().AddIngredient<ScoriaBar>(5)
			.AddIngredient<AshesofCalamity>(5)
			.AddTile(134)
			.Register();
	}
}
