using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class VeeringWind : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.damage = 19;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 14;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 10f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item66;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<VeeringWindAirWave>();
		base.Item.shootSpeed = 6f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		base.Item.UseSound = ((player.altFunctionUse == 2) ? SoundID.Item43 : SoundID.Item66);
		return base.CanUseItem(player);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 0.5f;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.altFunctionUse == 2)
		{
			mult *= 2f;
		}
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			type = ModContent.ProjectileType<VeeringWindFrostWave>();
			knockback *= 0.2f;
			velocity *= 0.5f;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		int totalProjectiles = 12;
		for (int i = 0; i < totalProjectiles; i++)
		{
			Vector2 waveVelocity = ((float)Math.PI * 2f * (float)i / (float)totalProjectiles + velocity.ToRotation()).ToRotationVector2() * ((Vector2)(ref velocity)).Length();
			Projectile.NewProjectile(source, position, waveVelocity, type, damage, knockback, Main.myPlayer);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(664, 30).AddIngredient(320, 3).AddIngredient(75, 5)
			.AddIngredient(751, 10)
			.AddTile(101)
			.Register();
	}
}
