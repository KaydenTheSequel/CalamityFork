using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "TriactisTruePaladinianMageHammerofMightMelee" })]
public class TriactisTruePaladinianMageHammerofMight : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 168;
		base.Item.height = 168;
		base.Item.damage = 2000;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.useStyle = 1;
		base.Item.knockBack = 14f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.shoot = ModContent.ProjectileType<TriactisHammerProj>();
		base.Item.shootSpeed = 25f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GalaxySmasher>().AddIngredient<ShadowspecBar>(5).AddIngredient<LifeAlloy>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
