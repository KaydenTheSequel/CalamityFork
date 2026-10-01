using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class CrescentMoon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.damage = 1000;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useAnimation = 18;
		base.Item.useTime = 18;
		base.Item.useStyle = 5;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item82;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.shootSpeed = 24f;
		base.Item.shoot = ModContent.ProjectileType<CrescentMoonFlail>();
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)player.Calamity().StratusStarburstResetTimer, 600f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		float ai3 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ai3);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Nebulash>().AddIngredient<Lumenyl>(8).AddIngredient<RuinousSoul>(3)
			.AddIngredient<ExodiumCluster>(16)
			.AddTile(134)
			.Register();
	}
}
