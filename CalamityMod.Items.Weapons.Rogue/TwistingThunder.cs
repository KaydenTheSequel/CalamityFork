using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "DeificThunderbolt" })]
public class TwistingThunder : RogueWeapon
{
	public override float StealthVelocityMultiplier => 1.5f;

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 56;
		base.Item.damage = 466;
		base.Item.knockBack = 10f;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 21;
		base.Item.useAnimation = 21;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 13.69f;
		base.Item.shoot = ModContent.ProjectileType<TwistingThunderProj>();
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 12f;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (Main.raining)
		{
			velocity *= 1.5f;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int thunder = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && thunder.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[thunder].Calamity().stealthStrike = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormfrontRazor>().AddIngredient<UnholyEssence>(15).AddIngredient<ArmoredShell>(3)
			.AddTile(134)
			.Register();
	}
}
