using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class DuststormInABottle : RogueWeapon
{
	public static int CloudLifetime = 200;

	public static float DustRadius = 11f;

	public static float MaxSize = 3.2f;

	public static float MaxSizeStealth = 3.6f;

	public static float GrowthRate = 0.025f;

	public static float StealthGrowthRate = 0.035f;

	public override float StealthDamageMultiplier => 0.6f;

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 24;
		base.Item.damage = 65;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 28;
		base.Item.useStyle = 1;
		base.Item.useTime = 28;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<DuststormInABottleProj>();
		base.Item.shootSpeed = 14f;
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
		CreateRecipe().AddIngredient(857).AddIngredient(3794, 5).AddIngredient<GrandScale>()
			.AddTile(134)
			.Register();
	}
}
