using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Monsoon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 78;
		base.Item.damage = 156;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo spawnSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 source = player.RotatedRelativePoint(player.MountedCenter);
		float piOver10 = (float)Math.PI / 10f;
		int totalProjectiles = 5;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 40f;
		bool canHit = Collision.CanHit(source, 0, 0, source + velocity, 0, 0);
		for (int p = 0; p < totalProjectiles; p++)
		{
			float offsetAmt = (float)p - ((float)totalProjectiles - 1f) / 2f;
			Vector2 offset = velocity.RotatedBy(piOver10 * offsetAmt);
			if (!canHit)
			{
				offset -= velocity;
			}
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				int newType = type;
				switch (p)
				{
				case 0:
				case 1:
				case 3:
				case 4:
					newType = ModContent.ProjectileType<MiniSharkron>();
					break;
				case 2:
					newType = ModContent.ProjectileType<TyphoonArrow>();
					break;
				}
				int proj = Projectile.NewProjectile(spawnSource, source.X + offset.X, source.Y + offset.Y, velocity.X, velocity.Y, newType, (int)((double)damage * 1.1), knockback, player.whoAmI);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].extraUpdates++;
				}
			}
			else
			{
				int proj2 = Projectile.NewProjectile(spawnSource, source.X + offset.X, source.Y + offset.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
				Main.projectile[proj2].noDropItem = true;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2624).AddIngredient<FlarewingBow>().AddIngredient<ReaperTooth>(6)
			.AddIngredient<DepthCells>(10)
			.AddTile(134)
			.Register();
	}
}
