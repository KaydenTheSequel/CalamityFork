using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class MidnightSunBeacon : ModItem, ILocalizedModType, IModType
{
	public const float MachineGunRate = 18f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 32);
		base.Item.damage = 176;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item90;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MidnightSunBeaconProj>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Item.damage;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2749).AddIngredient(3569).AddIngredient<AuricBar>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
