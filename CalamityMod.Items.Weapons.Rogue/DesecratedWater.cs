using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class DesecratedWater : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 24;
		base.Item.damage = 55;
		base.Item.useAnimation = 29;
		base.Item.useTime = 29;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DesecratedWaterProj>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyEvilWater", 100).AddRecipeGroup("AnyAdamantiteBar", 5).AddRecipeGroup("CursedFlameIchor", 5)
			.AddIngredient(521, 7)
			.AddTile(134)
			.Register();
	}
}
