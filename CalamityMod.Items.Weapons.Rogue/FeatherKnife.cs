using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class FeatherKnife : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.7f;

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 32;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.damage = 25;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.knockBack = 2f;
		base.Item.autoReuse = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<FeatherKnifeProjectile>();
		base.Item.shootSpeed = 25f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int spread = 6;
			for (int i = 0; i < 4; i++)
			{
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-3, 4), velocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int proj = Projectile.NewProjectile(source, position, perturbedspeed, type, damage, knockback, player.whoAmI);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].Calamity().stealthStrike = true;
				}
				spread -= Main.rand.Next(2, 6);
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(6).AddIngredient(824, 4).AddTile(16)
			.Register();
	}
}
