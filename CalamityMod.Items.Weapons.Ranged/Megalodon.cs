using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Megalodon : ModItem, ILocalizedModType, IModType
{
	private bool fireWater;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 32;
		base.Item.damage = 29;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 6;
		base.Item.useAnimation = 6;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MegalodonShot>();
		base.Item.shootSpeed = 16f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPos = position + Utils.RotatedBy(new Vector2(64f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-2f) : (-8f))), (double)velocity.ToRotation(), default(Vector2));
		Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(3f));
		int projectileToFire = (fireWater ? base.Item.shoot : type);
		Projectile.NewProjectile(source, newPos, newVel, projectileToFire, damage, knockback, player.whoAmI);
		int waterRingDamage = (int)((float)damage * 0.5f);
		float boostedKB = knockback + 7f;
		Projectile.NewProjectile(source, newPos, newVel * Main.rand.NextFloat(0.5f, 0.6f), ModContent.ProjectileType<ArcherfishRing>(), waterRingDamage, boostedKB, player.whoAmI);
		fireWater = !fireWater;
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return !fireWater;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(533).AddIngredient<Archerfish>().AddIngredient<Voidstone>(10)
			.AddIngredient<DepthCells>(10)
			.AddTile(134)
			.Register();
	}
}
