using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SlagMagnum : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 24;
		base.Item.damage = 24;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item41;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<SlagRound>();
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (type == 14)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SlagRound>(), damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}
}
