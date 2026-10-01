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

public class EventHorizon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 46;
		base.Item.damage = 70;
		base.Item.knockBack = 3.5f;
		base.Item.noMelee = true;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 12;
		base.Item.useAnimation = (base.Item.useTime = 32);
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item84;
		base.Item.shoot = ModContent.ProjectileType<EventHorizonStar>();
		base.Item.shootSpeed = 25f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		for (float i = 0f; i < 8f; i++)
		{
			float angle = (float)Math.PI / 4f * i;
			Projectile.NewProjectile(source, player.Center, angle.ToRotationVector2() * 8f, type, damage, knockback, player.whoAmI, angle);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StarShower>().AddIngredient<NuclearFury>().AddIngredient<RelicofRuin>()
			.AddIngredient<DarkPlasma>(3)
			.AddTile(101)
			.Register();
	}
}
