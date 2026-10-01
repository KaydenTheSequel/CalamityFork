using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Spyker : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 26;
		base.Item.damage = 210;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 13;
		base.Item.useAnimation = 13;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item108;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 9f;
		base.Item.shoot = ModContent.ProjectileType<SpykerProj>();
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (type == 14)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SpykerProj>(), damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Needler>().AddIngredient(1258).AddIngredient<UelibloomBar>(5)
			.AddTile(134)
			.Register();
	}
}
