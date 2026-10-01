using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class LunarKunai : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 38;
		base.Item.damage = 102;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 10;
		base.Item.useStyle = 1;
		base.Item.useTime = 10;
		base.Item.knockBack = 2f;
		base.Item.UseSound = SoundID.Item39;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<LunarKunaiProj>();
		base.Item.shootSpeed = 22f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = (player.Calamity().StealthStrikeAvailable() ? 10 : 3);
		for (int i = 0; i < projAmt; i++)
		{
			Vector2 spreadVel = velocity.RotatedByRandom(0.15707963705062866);
			int stealth = Projectile.NewProjectile(source, position, spreadVel, ModContent.ProjectileType<LunarKunaiProj>(), damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles) && player.Calamity().StealthStrikeAvailable())
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3467, 5).AddTile(412).Register();
	}
}
