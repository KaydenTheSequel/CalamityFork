using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Mourningstar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.damage = 112;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 5;
		base.Item.knockBack = 2.5f;
		base.Item.UseSound = SoundID.Item116;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shootSpeed = 24f;
		base.Item.shoot = ModContent.ProjectileType<MourningstarFlail>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		float ai3 = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		float ai3X = (Main.rand.NextFloat() - 0.25f) * ((float)Math.PI / 4f);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ai3);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ai3X);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3473).AddIngredient<DivineGeode>(6).AddIngredient<EssenceofSunlight>(6)
			.AddIngredient<EssenceofHavoc>(6)
			.AddTile(134)
			.Register();
	}
}
