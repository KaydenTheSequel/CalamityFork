using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class RecitationoftheBeast : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 54;
		base.Item.damage = 60;
		base.Item.mana = 17;
		base.Item.noMelee = true;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.useTime = 22;
		base.Item.knockBack = 8.5f;
		base.Item.UseSound = SoundID.Item8;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<BeastScythe>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Magic;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Vector2 circleVel = ((float)Math.PI * 2f * (float)i / 6f + velocity.ToRotation()).ToRotationVector2() * 2.2f;
			Projectile.NewProjectile(source, player.Center, circleVel, type, damage, knockback, Main.myPlayer);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(272).AddIngredient<BloodstoneCore>(6).AddIngredient<EssenceofHavoc>(6)
			.AddTile(101)
			.Register();
	}
}
