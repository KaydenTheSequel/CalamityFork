using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SealedSingularity : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.75f;

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 34);
		base.Item.damage = 260;
		base.Item.knockBack = 5f;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.useStyle = 1;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SealedSingularityProj>();
		base.Item.shootSpeed = 14f;
		base.Item.noMelee = (base.Item.noUseGraphic = true);
		base.Item.UseSound = SoundID.Item106;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
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
		CreateRecipe().AddIngredient<DuststormInABottle>().AddIngredient<DarkPlasma>(3).AddTile(134)
			.Register();
	}
}
