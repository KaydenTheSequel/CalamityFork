using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class RealmRavager : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 76;
		base.Item.height = 32;
		base.Item.damage = 54;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item38;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 30f;
		base.Item.shoot = ModContent.ProjectileType<RealmRavagerBullet>();
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		int numBullets = 4;
		for (int index = 0; index < numBullets; index++)
		{
			float SpeedX = velocity.X + ((index == 0) ? 0f : ((float)Main.rand.Next(-75, 76) * 0.05f));
			float SpeedY = velocity.Y + ((index == 0) ? 0f : ((float)Main.rand.Next(-75, 76) * 0.05f));
			if (type == 14)
			{
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<RealmRavagerBullet>(), damage, knockback, player.whoAmI);
			}
			else
			{
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			}
		}
		return false;
	}
}
