using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class PlagueTaintedSMG : ModItem, ILocalizedModType, IModType
{
	private const float Spread = 0.15f;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 98;
		base.Item.height = 50;
		base.Item.damage = 78;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 12);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.shoot = ModContent.ProjectileType<PlagueTaintedProjectile>();
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
		base.Item.UseSound = ((player.altFunctionUse == 2) ? SoundID.Item61 : SoundID.Item11);
		return base.CanUseItem(player);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 0.2f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Vector2 barrelPosition = position + Vector2.Normalize(velocity) * 70f;
		if (player.altFunctionUse == 2)
		{
			for (int i = 0; i < 3; i++)
			{
				Projectile.NewProjectile(source, barrelPosition, velocity.RotatedBy(-0.15f * (float)(i + 1)), ModContent.ProjectileType<PlagueTaintedDrone>(), damage, knockback, player.whoAmI, 1f, player.Calamity().alchFlask ? 1f : 0f);
				Projectile.NewProjectile(source, barrelPosition, velocity.RotatedBy(0.15f * (float)(i + 1)), ModContent.ProjectileType<PlagueTaintedDrone>(), damage, knockback, player.whoAmI, 1f, player.Calamity().alchFlask ? 1f : 0f);
			}
		}
		else
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-5, 6) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-5, 6) * 0.05f;
			Vector2 newVelocity = default(Vector2);
			((Vector2)(ref newVelocity))._002Ector(SpeedX, SpeedY);
			Projectile.NewProjectile(source, barrelPosition, newVelocity, ModContent.ProjectileType<PlagueTaintedProjectile>(), damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlackHawkRemote>().AddIngredient(1265).AddIngredient<Helstorm>()
			.AddIngredient<InfectedArmorPlating>(5)
			.AddTile(134)
			.Register();
	}
}
