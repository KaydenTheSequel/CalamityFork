using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ConsecratedWater : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 24;
		base.Item.damage = 48;
		base.Item.useAnimation = (base.Item.useTime = 29);
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ConsecratedWaterProjectile>();
		base.Item.shootSpeed = 15f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		float strikeValue = player.Calamity().StealthStrikeAvailable().ToInt();
		int p = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ConsecratedWaterProjectile>(), damage, knockback, player.whoAmI, 0f, strikeValue);
		if (player.Calamity().StealthStrikeAvailable() && p.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[p].Calamity().stealthStrike = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(422, 100).AddRecipeGroup("AnyAdamantiteBar", 5).AddIngredient(502, 10)
			.AddIngredient(520, 7)
			.AddTile(134)
			.Register();
	}
}
