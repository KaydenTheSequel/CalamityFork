using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "GalaxySmasherMelee", "GalaxySmasherRogue" })]
public class GalaxySmasher : ModItem, ILocalizedModType, IModType
{
	public static float Speed = 35f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 72;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 1180;
		base.Item.knockBack = 14f;
		base.Item.useTime = (base.Item.useAnimation = 48);
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<GalaxySmasherHammer>();
		base.Item.shootSpeed = Speed;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StellarContempt>().AddIngredient<CosmiliteBar>(10).AddTile<CosmicAnvil>()
			.Register();
	}
}
