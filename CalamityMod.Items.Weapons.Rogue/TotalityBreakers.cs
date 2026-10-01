using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class TotalityBreakers : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.3f;

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 42;
		base.Item.damage = 64;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 28;
		base.Item.useStyle = 1;
		base.Item.useTime = 28;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<TotalityFlask>();
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
		CreateRecipe().AddIngredient(2590, 50).AddIngredient<SolarVeil>(10).AddTile(134)
			.Register();
	}
}
