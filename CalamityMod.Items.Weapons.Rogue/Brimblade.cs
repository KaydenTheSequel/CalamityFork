using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Brimblade : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.damage = 28;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 1;
		base.Item.useTime = 18;
		base.Item.knockBack = 6.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<BrimbladeProj>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int blade = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (blade.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[blade].Calamity().stealthStrike = true;
			}
			for (int i = -6; i <= 6; i += 4)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians((float)i));
				int dart = Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<SeethingDischargeBrimstoneBarrage>(), damage, knockback * 0.5f, player.whoAmI);
				if (dart.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[dart].DamageType = RogueDamageClass.Instance;
				}
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UnholyCore>(4).AddTile(134).Register();
	}
}
