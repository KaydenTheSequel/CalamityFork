using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Alluvion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 90;
		base.Item.damage = 96;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 15;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
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
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 source = player.RotatedRelativePoint(player.MountedCenter);
		float tenthPi = (float)Math.PI / 10f;
		int totalProjectiles = 6;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 35f;
		bool canHit = Collision.CanHit(source, 0, 0, source + velocity, 0, 0);
		for (int i = 0; i < totalProjectiles; i++)
		{
			float arrowOffset = (float)i - ((float)totalProjectiles - 1f) / 2f;
			Vector2 offset = velocity.RotatedBy(tenthPi * arrowOffset);
			if (!canHit)
			{
				offset -= velocity;
			}
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				int newType = type;
				switch (i)
				{
				case 0:
				case 5:
					newType = ModContent.ProjectileType<TyphoonArrow>();
					break;
				case 1:
				case 4:
					newType = ModContent.ProjectileType<MiniSharkron>();
					break;
				case 2:
				case 3:
					newType = ModContent.ProjectileType<TorrentialArrow>();
					break;
				}
				int proj = Projectile.NewProjectile(spawnSource, source.X + offset.X, source.Y + offset.Y, velocity.X, velocity.Y, newType, (int)((float)damage * 1.35f), knockback, player.whoAmI);
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
		CreateRecipe().AddIngredient<Monsoon>().AddIngredient<CosmiliteBar>(8).AddIngredient<EndothermicEnergy>(20)
			.AddIngredient<Lumenyl>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
