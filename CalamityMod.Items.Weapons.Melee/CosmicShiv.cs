using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Shortswords;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class CosmicShiv : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 13;
		base.Item.damage = 155;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.shoot = ModContent.ProjectileType<CosmicShivProj>();
		base.Item.shootSpeed = 2.4f;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Lightspeed>().AddIngredient<CosmiliteBar>(8).AddTile<CosmicAnvil>()
			.Register();
	}
}
