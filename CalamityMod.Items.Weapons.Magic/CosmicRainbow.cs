using System;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class CosmicRainbow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 64;
		base.Item.damage = 117;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<CosmicRainbowFront>();
		base.Item.shootSpeed = 18f;
		base.Item.UseSound = SoundID.Item67 with
		{
			Volume = 0.7f
		};
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rainbowPos = player.Center + (Vector2.Normalize(velocity) * Main.rand.NextFloat(-36f, 36f)).RotatedBy(1.5707963705062866);
		Vector2 rainbowVel = Vector2.Normalize(Main.MouseWorld - rainbowPos) * base.Item.shootSpeed;
		Projectile.NewProjectile(source, rainbowPos, rainbowVel, type, damage, knockback, Main.myPlayer);
		double rotationOffset = Math.Sin((float)Main.GameUpdateCount / 60f * ((float)Math.PI * 2f / 3f)) * 0.4000000059604645;
		Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(rotationOffset), ModContent.ProjectileType<PrismaticWave>(), damage, knockback, Main.myPlayer, 0f, Main.rand.Next(12), 1f);
		projectile.DamageType = DamageClass.Magic;
		projectile.scale = 0.7f;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1260).AddIngredient(3467, 5).AddIngredient(502, 10)
			.AddIngredient(520, 10)
			.AddTile(412)
			.Register();
	}
}
