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

[LegacyName(new string[] { "Climax" })]
public class VoltaicClimax : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 78;
		base.Item.damage = 215;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 32;
		base.Item.useTime = (base.Item.useAnimation = 34);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ClimaxProj>();
		base.Item.shootSpeed = 12f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		int numOrbs = 9;
		Vector2 clickPos = player.ClampedMouseWorld();
		float orbSpeed = 14f;
		Vector2 vel = Main.rand.NextVector2CircularEdge(orbSpeed, orbSpeed);
		for (int i = 0; i < numOrbs; i++)
		{
			float timingStagger = i * 2;
			Projectile.NewProjectile(source, clickPos, vel, type, damage, knockback, player.whoAmI, timingStagger);
			vel = vel.RotatedBy((float)Math.PI * 2f / (float)numOrbs);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MagneticMeltdown>().AddIngredient<CosmiliteBar>(8).AddIngredient<DarksunFragment>(8)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
