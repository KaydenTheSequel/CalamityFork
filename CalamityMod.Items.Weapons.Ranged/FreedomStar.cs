using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FreedomStar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		CalamityGlobalItem calamityGlobalItem = base.Item.Calamity();
		base.Item.width = 54;
		base.Item.height = 28;
		base.Item.damage = 100;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 10;
		base.Item.useAnimation = 10;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		calamityGlobalItem.donorItem = true;
		base.Item.UseSound = SoundID.Item75;
		base.Item.shoot = ModContent.ProjectileType<FreedomStarHoldout>();
		base.Item.shootSpeed = 12f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = velocity.SafeNormalize(Vector2.UnitX * (float)player.direction);
		Projectile.NewProjectile(source, position, shootDirection, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(12).AddIngredient<DubiousPlating>(18).AddIngredient<UelibloomBar>(8)
			.AddIngredient(3467, 4)
			.AddTile(134)
			.Register();
	}
}
