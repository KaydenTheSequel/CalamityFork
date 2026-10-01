using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Pumpler : ModItem, ILocalizedModType, IModType
{
	public const int MaxPumpkins = 5;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 34;
		base.Item.damage = 26;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.25f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 11f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-30f, 0f);
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<PumplerHoldout>()] <= 0;
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
		Projectile.NewProjectile(source, position, shootDirection, ModContent.ProjectileType<PumplerHoldout>(), 0, 0f, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(324).AddIngredient(1725, 30).AddIngredient(1828, 5)
			.AddTile(16)
			.Register();
	}
}
