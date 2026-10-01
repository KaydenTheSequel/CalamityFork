using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class NastyCholla : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 18;
		base.Item.damage = 10;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.useTime = 20;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
		base.Item.shoot = ModContent.ProjectileType<NastyChollaBol>();
		base.Item.shootSpeed = 8f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int spread = 3;
			for (int i = 0; i < 4; i++)
			{
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-3, 4), velocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int proj = Projectile.NewProjectile(source, position.X, position.Y, perturbedspeed.X, perturbedspeed.Y, type, damage, knockback, player.whoAmI);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].originalDamage = damage;
					Main.projectile[proj].Calamity().stealthStrike = true;
				}
				spread -= Main.rand.Next(1, 4);
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(276, 8).AddTile(18).Register();
	}
}
