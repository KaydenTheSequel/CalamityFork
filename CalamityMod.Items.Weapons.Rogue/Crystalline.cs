using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Crystalline : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.7f;

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 16;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 1;
		base.Item.useTime = 18;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.shoot = ModContent.ProjectileType<CrystallineProj>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 4f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
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
		CreateRecipe().AddIngredient<WulfrumKnife>().AddIngredient(182, 3).AddIngredient(75, 3)
			.AddTile(16)
			.Register();
	}
}
