using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class EnchantedAxe : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 36;
		base.Item.damage = 19;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 19);
		base.Item.useStyle = 1;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.maxStack = 1;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<EnchantedAxeProj>();
		base.Item.shootSpeed = 30f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<IronFrancisca>().AddIngredient(75, 5).AddIngredient<PearlShard>(10)
			.AddIngredient(154, 30)
			.AddTile(16)
			.Register();
		CreateRecipe().AddIngredient<LeadTomahawk>().AddIngredient(75, 5).AddIngredient<PearlShard>(10)
			.AddIngredient(154, 30)
			.AddTile(16)
			.Register();
	}
}
