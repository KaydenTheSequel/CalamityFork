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

public class Drataliornus : ModItem, ILocalizedModType, IModType
{
	private const double RightClickDamageRatio = 0.6;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 84;
		base.Item.damage = 129;
		base.Item.knockBack = 1f;
		base.Item.shootSpeed = 18f;
		base.Item.useStyle = 5;
		base.Item.useTime = 12;
		base.Item.useAnimation = 24;
		base.Item.reuseDelay = 48;
		base.Item.useLimitPerAnimation = 2;
		base.Item.UseSound = SoundID.Item5;
		base.Item.shoot = ModContent.ProjectileType<DrataliornusBow>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.channel = true;
		base.Item.useTurn = false;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.autoReuse = true;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.noUseGraphic = false;
		}
		else
		{
			base.Item.noUseGraphic = true;
			if (player.ownedProjectileCounts[base.Item.shoot] > 0)
			{
				return false;
			}
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			int flameID = ModContent.ProjectileType<DrataliornusFlame>();
			int flameDamage = (int)((double)damage * 0.6);
			Vector2 spinningpoint = velocity;
			((Vector2)(ref spinningpoint)).Normalize();
			spinningpoint *= 36f;
			for (int i = 0; i < 5; i++)
			{
				float piArrowOffset = i - 2;
				Vector2 offsetSpawn = spinningpoint.RotatedBy((float)Math.PI * 3f / 20f * piArrowOffset);
				Projectile.NewProjectile(source, position.X + offsetSpawn.X, position.Y + offsetSpawn.Y, velocity.X, velocity.Y, flameID, flameDamage, knockback, player.whoAmI, 1f);
			}
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DrataliornusBow>(), 0, 0f, player.whoAmI);
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(4f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlossomFlux>().AddIngredient<AuricBar>(5).AddIngredient<YharonSoulFragment>(4)
			.AddIngredient<EffulgentFeather>(12)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
