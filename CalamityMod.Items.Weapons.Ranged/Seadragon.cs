using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Seadragon : ModItem, ILocalizedModType, IModType
{
	private int shotCounter = 1;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 90;
		base.Item.height = 38;
		base.Item.damage = 60;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 5;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MegalodonShot>();
		base.Item.shootSpeed = 16f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
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
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPos = position + Utils.RotatedBy(new Vector2(74f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-2f) : (-8f))), (double)velocity.ToRotation(), default(Vector2));
		Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
		int projectileToFire = ((shotCounter % 2 == 0) ? base.Item.shoot : type);
		Projectile.NewProjectile(source, newPos, newVel, projectileToFire, damage, knockback, player.whoAmI);
		int waterRingDamage = (int)((float)damage * 0.5f);
		float boostedKB = knockback + 7f;
		Projectile.NewProjectile(source, newPos, newVel * Main.rand.NextFloat(0.45f, 0.65f), ModContent.ProjectileType<ArcherfishRing>(), waterRingDamage, boostedKB, player.whoAmI);
		bool num = shotCounter == 9;
		bool muzzleBlast = shotCounter == 17;
		if (num)
		{
			int rocketDamage = damage * 3;
			Projectile.NewProjectile(source, newPos, newVel * 1.2f, ModContent.ProjectileType<SeaDragonRocket>(), rocketDamage, knockback, player.whoAmI);
			SoundEngine.PlaySound(in SoundID.Item109, player.Center);
		}
		if (muzzleBlast)
		{
			int muzzleBlastDamage = damage * 12;
			float muzzleBlastKB = knockback + 12f;
			Projectile.NewProjectile(source, newPos, velocity, ModContent.ProjectileType<SeaDragonMuzzleBlast>(), muzzleBlastDamage, muzzleBlastKB, player.whoAmI);
			SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, position);
		}
		shotCounter++;
		if (shotCounter > 17)
		{
			shotCounter = 2;
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return shotCounter % 2 == 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Megalodon>().AddIngredient<Necroplasm>(9).AddIngredient<SeaPrism>(10)
			.AddIngredient<ArmoredShell>(3)
			.AddTile(134)
			.Register();
	}
}
