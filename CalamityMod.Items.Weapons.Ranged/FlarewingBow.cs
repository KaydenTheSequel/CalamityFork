using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FlarewingBow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 72;
		base.Item.damage = 31;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 41;
		base.Item.useAnimation = 41;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 18f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float tenthPi = (float)Math.PI / 10f;
		Vector2 arrowVel = velocity;
		((Vector2)(ref arrowVel)).Normalize();
		arrowVel *= 50f;
		bool arrowHitsTiles = Collision.CanHit(vector2, 0, 0, vector2 + arrowVel, 0, 0);
		for (int i = 0; i < 3; i++)
		{
			float piOffsetValue = (float)i - 1f;
			Vector2 offsetSpawn = arrowVel.RotatedBy(tenthPi * piOffsetValue);
			if (!arrowHitsTiles)
			{
				offsetSpawn -= arrowVel;
			}
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				int arrowSpawn = Projectile.NewProjectile(source, vector2.X + offsetSpawn.X, vector2.Y + offsetSpawn.Y, velocity.X, velocity.Y, ModContent.ProjectileType<FlareBat>(), damage, knockback, player.whoAmI);
				Main.projectile[arrowSpawn].noDropItem = true;
			}
			else
			{
				int arrowSpawn2 = Projectile.NewProjectile(source, vector2.X + offsetSpawn.X, vector2.Y + offsetSpawn.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
				Main.projectile[arrowSpawn2].noDropItem = true;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3019).AddIngredient<EssenceofSunlight>(5).AddIngredient(2701, 50)
			.AddIngredient(173, 10)
			.AddTile(16)
			.Register();
	}
}
