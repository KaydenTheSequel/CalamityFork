using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "GammaFusillade" })]
public class Biofusillade : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 118;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 4;
		base.Item.useTime = 3;
		base.Item.useAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.UseSound = SoundID.Item33;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<GammaLaser>();
		base.Item.shootSpeed = 20f;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddIngredient<UelibloomBar>(8).AddTile(101)
			.Register();
	}
}
