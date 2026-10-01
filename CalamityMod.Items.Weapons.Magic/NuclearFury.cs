using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class NuclearFury : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 40;
		base.Item.damage = 108;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 22;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item84;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<NuclearFuryProjectile>();
		base.Item.shootSpeed = 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 8; i++)
		{
			Vector2 ringVelocity = ((float)Math.PI * 2f * (float)i / 8f + velocity.ToRotation()).ToRotationVector2() * ((Vector2)(ref velocity)).Length() * 0.5f;
			Projectile.NewProjectile(source, position, ringVelocity, type, damage, knockback, Main.myPlayer);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2622).AddIngredient<Poseidon>().AddIngredient(3467, 5)
			.AddTile(101)
			.Register();
	}
}
