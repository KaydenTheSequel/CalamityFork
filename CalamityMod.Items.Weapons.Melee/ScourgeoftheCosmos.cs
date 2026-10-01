using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class ScourgeoftheCosmos : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 64);
		base.Item.damage = 380;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 16);
		base.Item.useStyle = 5;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.shoot = ModContent.ProjectileType<ScourgeoftheCosmosProj>();
		base.Item.shootSpeed = 20f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1571).AddIngredient<Bonebreaker>().AddIngredient<CosmiliteBar>(10)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
