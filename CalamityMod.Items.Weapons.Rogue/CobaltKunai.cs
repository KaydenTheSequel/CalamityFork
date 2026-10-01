using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CobaltKunai : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.4f;

	public override float StealthVelocityMultiplier => 0.9f;

	public override float StealthKnockbackMultiplier => 0.5f;

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 38;
		base.Item.damage = 56;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.useStyle = 1;
		base.Item.knockBack = 2.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.shoot = ModContent.ProjectileType<CobaltKunaiProjectile>();
		base.Item.shootSpeed = 14f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			for (float i = -1.5f; i <= 1.5f; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(i * 7f));
				int stealth = Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<CobaltEnergy>(), damage, knockback, player.whoAmI);
				if (stealth.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[stealth].Calamity().stealthStrike = true;
				}
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(381, 10).AddTile(16).Register();
	}
}
