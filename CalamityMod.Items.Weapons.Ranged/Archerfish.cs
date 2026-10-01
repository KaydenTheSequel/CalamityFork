using System;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Archerfish : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 33;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 36;
		base.Item.damage = 16;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 11;
		base.Item.useAnimation = 11;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item85;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ArcherfishShot>();
		base.Item.shootSpeed = 11f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, -5f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPos = position + Utils.RotatedBy(new Vector2(60f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-2f) : (-8f))), (double)velocity.ToRotation(), default(Vector2));
		for (int i = 0; i < 4; i++)
		{
			Gore gore = Gore.NewGorePerfect(source, newPos, velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * 0.5f, 411);
			gore.timeLeft = 6 + Main.rand.Next(4);
			gore.scale = Main.rand.NextFloat(0.6f, 0.8f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
		if (type == 14)
		{
			Projectile.NewProjectile(source, newPos, velocity, base.Item.shoot, damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, newPos, velocity, type, damage, knockback, player.whoAmI);
		}
		int waterRingDamage = (int)((float)damage * 0.5f);
		float boostedKB = knockback + 5f;
		Projectile.NewProjectile(source, newPos, velocity * 0.5f, ModContent.ProjectileType<ArcherfishRing>(), waterRingDamage, boostedKB, player.whoAmI);
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}
}
