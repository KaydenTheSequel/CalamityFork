using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class LeviathanTeeth : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 38;
		base.Item.damage = 43;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 9);
		base.Item.useStyle = 1;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<LeviathanTooth>();
		base.Item.shootSpeed = 7.5f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			Projectile.NewProjectileDirect(source, position, velocity.SafeNormalize(Vector2.UnitX), ModContent.ProjectileType<LeviathanToothStealth>(), damage, 0f, player.whoAmI).Calamity().stealthStrike = true;
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.05f + (float)i * 0.15f) * Main.rand.NextFloat(0.8f, 1f), type, damage, knockback, player.whoAmI, 0f, Main.rand.Next(1, 4));
			}
		}
		return false;
	}
}
