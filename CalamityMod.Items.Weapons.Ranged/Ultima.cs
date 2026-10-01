using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Ultima : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 66;

	public const float FullChargeTime = 420f;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 58;
		base.Item.damage = 115;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 8);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.UseSound = SoundID.Item158;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<UltimaBowProjectile>();
		base.Item.shootSpeed = 18f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.channel = true;
		base.Item.useTurn = false;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity.SafeNormalize(Vector2.UnitX * (float)player.direction), ModContent.ProjectileType<UltimaBowProjectile>(), 0, 0f, player.whoAmI);
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2223).AddIngredient(514).AddIngredient<TheStorm>()
			.AddIngredient<CosmiliteBar>(8)
			.AddIngredient<DarksunFragment>(8)
			.AddIngredient<ExodiumCluster>(15)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
