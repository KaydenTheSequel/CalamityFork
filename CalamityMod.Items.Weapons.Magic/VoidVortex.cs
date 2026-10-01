using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class VoidVortex : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 130;
		base.Item.height = 130;
		base.Item.damage = 210;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 60;
		base.Item.useAnimation = (base.Item.useTime = 49);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<VoidVortexProj>();
		base.Item.shootSpeed = 12f;
		base.Item.UseSound = SoundID.Item20;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		int numOrbs = 12;
		Vector2 clickPos = player.ClampedMouseWorld();
		float orbDistance = 90f;
		float orbSpeed = 4f;
		float spinCoinflip = (Main.rand.NextBool() ? (-1f) : 1f);
		Vector2 dir = Main.rand.NextVector2Unit();
		for (int i = 0; i < numOrbs; i++)
		{
			Vector2 orbPos = clickPos + dir * orbDistance;
			Vector2 vel = dir.RotatedBy(spinCoinflip * (-(float)Math.PI / 2f)) * orbSpeed;
			Projectile.NewProjectile(source, orbPos, -vel, type, damage, knockback, player.whoAmI, i * 4, spinCoinflip);
			dir = dir.RotatedBy((float)Math.PI * 2f / (float)numOrbs);
		}
		Projectile.NewProjectile(source, clickPos, Vector2.Zero, type, damage * 4, 10f, player.whoAmI, 0f, spinCoinflip, 1f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<VoltaicClimax>().AddIngredient<AuricBar>(5).AddTile<CosmicAnvil>()
			.Register();
	}
}
