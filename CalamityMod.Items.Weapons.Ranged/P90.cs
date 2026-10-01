using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class P90 : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 50;

	public bool fireShot = true;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 28;
		base.Item.damage = 6;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 2);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = Item.buyPrice(0, 35);
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item11 with
		{
			Volume = 0.6f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<P90Round>();
		base.Item.shootSpeed = 9f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-14f, -1f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (fireShot)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.029999999329447746), ModContent.ProjectileType<P90Round>(), damage, knockback, player.whoAmI);
		}
		fireShot = !fireShot;
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (fireShot)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}
}
