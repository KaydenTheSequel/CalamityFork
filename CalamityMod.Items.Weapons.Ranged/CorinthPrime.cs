using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class CorinthPrime : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 106;
		base.Item.height = 42;
		base.Item.damage = 140;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item38;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.shoot = ModContent.ProjectileType<RealmRavagerBullet>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-20f, 5f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.useTime = 90;
			base.Item.useAnimation = 90;
			base.Item.UseSound = SoundID.Item66;
			base.Item.shootSpeed = 8f;
		}
		else
		{
			base.Item.useTime = 30;
			base.Item.useAnimation = 30;
			base.Item.UseSound = SoundID.Item38;
			base.Item.shootSpeed = 12f;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position + Vector2.Normalize(velocity) * 60f, velocity, ModContent.ProjectileType<CorinthPrimeAirburstGrenade>(), damage, knockback, player.whoAmI);
		}
		else
		{
			int numBullets = 6;
			for (int index = 0; index < numBullets; index++)
			{
				float SpeedX = velocity.X + (float)Main.rand.Next(-30, 31) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-30, 31) * 0.05f;
				int proj = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, (type == 14) ? ModContent.ProjectileType<RealmRavagerBullet>() : type, damage, knockback, player.whoAmI);
				Main.projectile[proj].extraUpdates++;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RealmRavager>().AddIngredient(3475).AddIngredient(324)
			.AddIngredient<ArmoredShell>(3)
			.AddTile(134)
			.Register();
	}
}
