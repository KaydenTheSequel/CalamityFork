using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ShatteredDawn : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.2f;

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 66;
		base.Item.damage = 80;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 12;
		base.Item.useStyle = 1;
		base.Item.useTime = 12;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<ShatteredDawnKnife>();
		base.Item.shootSpeed = 25f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
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
		CreateRecipe().AddIngredient<RadiantStar>().AddIngredient<DivineGeode>(6).AddTile(134)
			.Register();
	}
}
