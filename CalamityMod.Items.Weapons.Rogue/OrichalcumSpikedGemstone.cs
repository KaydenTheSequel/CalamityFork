using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class OrichalcumSpikedGemstone : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 34;
		base.Item.damage = 40;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 13);
		base.Item.useStyle = 1;
		base.Item.knockBack = 2f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.shoot = ModContent.ProjectileType<OrichalcumSpikedGemstoneProjectile>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int gemstone = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (gemstone.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[gemstone].Calamity().stealthStrike = true;
				Main.projectile[gemstone].timeLeft = 600;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1191, 10).AddTile(134).Register();
	}
}
