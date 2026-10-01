using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "SeashellBoomerang" })]
public class FishboneBoomerang : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 34;
		base.Item.damage = 27;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 1;
		base.Item.useTime = 30;
		base.Item.knockBack = 5.5f;
		base.Item.UseSound = null;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.shoot = ModContent.ProjectileType<FishboneBoomerangProjectile>();
		base.Item.shootSpeed = 3f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] < 3;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(2).AddTile(16).Register();
	}
}
