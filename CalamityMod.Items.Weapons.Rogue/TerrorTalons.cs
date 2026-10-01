using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class TerrorTalons : RogueWeapon
{
	private float sign = 1f;

	public override float StealthDamageMultiplier => 1.8f;

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 24;
		base.Item.damage = 47;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 7);
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item39;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<TalonSmallProj>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<TalonLargeProj>(), damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<TalonSmallProj>(), damage, knockback, player.whoAmI, 0f, sign);
			sign = 0f - sign;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1006, 5).AddIngredient(259, 10).AddTile(134)
			.Register();
	}
}
