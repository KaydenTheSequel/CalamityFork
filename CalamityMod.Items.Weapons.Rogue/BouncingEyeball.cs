using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class BouncingEyeball : RogueWeapon
{
	public override float StealthVelocityMultiplier => 2f;

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.damage = 16;
		base.Item.useTime = 23;
		base.Item.useAnimation = 23;
		base.Item.useStyle = 1;
		base.Item.knockBack = 3.5f;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<BouncingEyeballProjectile>();
		base.Item.shootSpeed = 10f;
		base.Item.autoReuse = true;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.bloodMoon)
		{
			knockback *= 3f;
		}
		if (!player.Calamity().StealthStrikeAvailable())
		{
			velocity *= Main.rand.NextFloat(0.85f, 1.3f);
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10f));
		}
		else
		{
			type = ModContent.ProjectileType<BouncingEyeballProjectileStealthStrike>();
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (p.WithinBounds(Main.maxProjectiles) && player.Calamity().StealthStrikeAvailable())
		{
			Main.projectile[p].Calamity().stealthStrike = true;
		}
		return false;
	}
}
