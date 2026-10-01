using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class RustyBeaconPrototype : ModItem, ILocalizedModType, IModType
{
	public const int PulseReleaseRate = 120;

	public const int PulseLifetime = 95;

	public const int IrradiatedDebuffTime = 120;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.mana = 10;
		base.Item.damage = 8;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 0.5f;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.UseSound = SoundID.Item15;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<RustyDrone>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI, 16f);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Item.damage;
		}
		player.UpdateMaxTurrets();
		return false;
	}
}
