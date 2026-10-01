using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class EldritchTome : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 32;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useTime = 7;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item103;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<EldritchTentacle>();
		base.Item.shootSpeed = 12f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spreadVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(18f)) * Main.rand.NextFloat(0.8f, 1.2f);
		float tentacleYDirection = Main.rand.NextFloat(0.01f, 0.05f);
		if (Main.rand.NextBool())
		{
			tentacleYDirection *= -1f;
		}
		float tentacleXDirection = Main.rand.NextFloat(0.01f, 0.05f);
		if (Main.rand.NextBool())
		{
			tentacleXDirection *= -1f;
		}
		Projectile.NewProjectile(source, position, spreadVelocity, type, damage, knockback, Main.myPlayer, tentacleXDirection, tentacleYDirection);
		return false;
	}
}
