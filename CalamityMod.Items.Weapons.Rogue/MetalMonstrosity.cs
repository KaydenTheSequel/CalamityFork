using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class MetalMonstrosity : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.damage = 28;
		base.Item.useAnimation = (base.Item.useTime = 45);
		base.Item.knockBack = 12f;
		base.Item.shoot = ModContent.ProjectileType<MetalChunk>();
		base.Item.shootSpeed = 7f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MetalChunk>(), damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = true;
			Main.projectile[proj].penetrate = 3;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(161, 500).AddIngredient(147, 80).AddTile(16)
			.Register();
	}
}
