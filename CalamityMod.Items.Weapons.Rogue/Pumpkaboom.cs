using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Pumpkaboom : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 34;
		base.Item.damage = 17;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 70);
		base.Item.useStyle = 1;
		base.Item.knockBack = 2f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.shoot = ModContent.ProjectileType<PumpkaboomSmall>();
		base.Item.shootSpeed = 14f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool CanUseItem(Player player)
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (projectile.active && projectile.type == base.Item.shoot && projectile.localAI[1] < 5f && projectile.owner == player.whoAmI)
			{
				return false;
			}
		}
		return true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int proj = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PumpkaboomBig>(), damage, knockback, player.whoAmI);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(168, 15).AddIngredient(1725, 30).AddIngredient(1828, 5)
			.AddTile(16)
			.Register();
	}
}
