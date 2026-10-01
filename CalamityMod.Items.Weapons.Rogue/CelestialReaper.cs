using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CelestialReaper : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.9f;

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 76;
		base.Item.damage = 140;
		base.Item.useAnimation = 31;
		base.Item.useTime = 31;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 6f;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item71;
		base.Item.autoReuse = true;
		base.Item.value = Item.buyPrice(3);
		base.Item.shoot = ModContent.ProjectileType<CelestialReaperProjectile>();
		base.Item.shootSpeed = 20f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		float strikeValue = player.Calamity().StealthStrikeAvailable().ToInt();
		int p = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CelestialReaperProjectile>(), damage, knockback, player.whoAmI, strikeValue);
		if (player.Calamity().StealthStrikeAvailable() && p.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[p].Calamity().stealthStrike = true;
		}
		return false;
	}
}
