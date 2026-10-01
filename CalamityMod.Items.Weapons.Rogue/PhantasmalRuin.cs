using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class PhantasmalRuin : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.2f;

	public override void SetDefaults()
	{
		base.Item.width = 108;
		base.Item.height = 114;
		base.Item.damage = 140;
		base.Item.knockBack = 8f;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 30);
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<PhantasmalRuinProj>();
		base.Item.UseSound = SoundID.Item1;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
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
			Main.projectile[stealth].penetrate = 3;
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3261, 15).AddIngredient(154, 35).AddTile(134)
			.Register();
	}
}
