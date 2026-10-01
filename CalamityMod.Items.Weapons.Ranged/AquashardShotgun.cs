using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class AquashardShotgun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 26;
		base.Item.damage = 12;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item61;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<Aquashard>();
		base.Item.shootSpeed = 30f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 6f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = Main.rand.Next(2, 4);
		for (int index = 0; index < projAmt; index++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-40, 41) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-40, 41) * 0.05f;
			if (type == 14)
			{
				int projectile = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<Aquashard>(), damage, knockback, player.whoAmI);
				Main.projectile[projectile].timeLeft = 200;
			}
			else
			{
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(964).AddIngredient<AerialiteBar>(2).AddIngredient<SeaPrism>(10)
			.AddTile(16)
			.Register();
	}
}
