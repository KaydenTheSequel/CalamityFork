using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Exorcism : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.65f;

	public override void SetDefaults()
	{
		base.Item.width = 109;
		base.Item.height = 128;
		base.Item.damage = 222;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 96);
		base.Item.useStyle = 1;
		base.Item.knockBack = 12f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.shoot = ModContent.ProjectileType<ExorcismProj>();
		base.Item.shootSpeed = 6f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1f, damage);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0.25f, damage);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1225, 20).AddIngredient(547, 3).AddIngredient(548, 3)
			.AddIngredient(549, 3)
			.AddTile(134)
			.Register();
	}
}
