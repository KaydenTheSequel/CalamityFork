using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Shredder : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 24;
		base.Item.damage = 48;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 4;
		base.Item.useAnimation = 32;
		base.Item.reuseDelay = 35;
		base.Item.useLimitPerAnimation = 8;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item31;
		base.Item.autoReuse = true;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 5f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.consumeAmmoOnLastShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		int shotType = ((player.altFunctionUse == 2) ? type : ModContent.ProjectileType<ChargedBlast>());
		int bulletAmt = 3;
		Vector2 newPosition = position + velocity.SafeNormalize(Vector2.UnitY) * 50f;
		for (int index = 0; index < bulletAmt; index++)
		{
			Projectile.NewProjectile(source, newPosition, (velocity * Main.rand.NextFloat(0.9f, 1.1f)).RotatedByRandom(0.20000000298023224), shotType, damage, knockback, player.whoAmI);
		}
		if (player.itemAnimation <= 1)
		{
			player.altFunctionUse = 0;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FrostbiteBlaster>().AddIngredient<BulletFilledShotgun>().AddIngredient(3467, 5)
			.AddTile(134)
			.Register();
	}
}
