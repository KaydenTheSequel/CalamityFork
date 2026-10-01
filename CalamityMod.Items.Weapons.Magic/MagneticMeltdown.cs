using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class MagneticMeltdown : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 78;
		base.Item.damage = 200;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 32;
		base.Item.useTime = 37;
		base.Item.useAnimation = 37;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MagneticOrb>();
		base.Item.shootSpeed = 18f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		float offset = 3f;
		Projectile.NewProjectile(source, position, velocity + offset * Vector2.UnitX, type, damage, knockback, player.whoAmI, 1f);
		Projectile.NewProjectile(source, position, velocity - offset * Vector2.UnitX, type, damage, knockback, player.whoAmI, 1f);
		Projectile.NewProjectile(source, position, velocity + offset * Vector2.UnitY, type, damage, knockback, player.whoAmI, 1f);
		Projectile.NewProjectile(source, position, velocity - offset * Vector2.UnitY, type, damage, knockback, player.whoAmI, 1f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1266).AddIngredient<TwistingNether>(3).AddTile(134)
			.Register();
	}
}
