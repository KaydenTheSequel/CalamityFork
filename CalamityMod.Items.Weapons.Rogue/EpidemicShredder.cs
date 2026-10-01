using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class EpidemicShredder : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.damage = 80;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = 16;
		base.Item.useTime = 16;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<EpidemicShredderProjectile>();
		base.Item.shootSpeed = 18f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		float strikeValue = player.Calamity().StealthStrikeAvailable().ToInt();
		int projectileIndex = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EpidemicShredderProjectile>(), damage, knockback, player.whoAmI, 0f, strikeValue);
		if (player.Calamity().StealthStrikeAvailable() && projectileIndex.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[projectileIndex].Calamity().stealthStrike = strikeValue == 1f;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1006, 20).AddIngredient(1346, 150).AddIngredient<PlagueCellCanister>(15)
			.AddTile(134)
			.Register();
	}
}
