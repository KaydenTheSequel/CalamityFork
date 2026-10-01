using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class FrostcrushValari : RogueWeapon
{
	public static float Speed = 16f;

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 58;
		base.Item.damage = 81;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useTime = (base.Item.useAnimation = 19);
		base.Item.knockBack = 12f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = Speed;
		base.Item.shoot = ModContent.ProjectileType<ValariBoomerang>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].Calamity().stealthStrike = true;
				Main.projectile[proj].penetrate = 4;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Kylie>().AddIngredient<CryonicBar>(6).AddIngredient<Voidstone>(40)
			.AddIngredient(1508, 5)
			.AddTile(134)
			.Register();
	}
}
