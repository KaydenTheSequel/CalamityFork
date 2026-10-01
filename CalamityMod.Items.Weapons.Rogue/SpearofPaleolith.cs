using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SpearofPaleolith : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 54;
		base.Item.damage = 65;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 27;
		base.Item.useStyle = 1;
		base.Item.useTime = 27;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<SpearofPaleolithProj>();
		base.Item.shootSpeed = 35f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stabDevice = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stabDevice.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stabDevice].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3783, 2).AddRecipeGroup("AnyAdamantiteBar", 4).AddTile(134)
			.Register();
	}
}
