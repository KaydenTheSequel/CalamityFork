using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SurgeDriverHoldout : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SurgeDriver>();

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float ShootCountdown => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 192;
		base.Projectile.height = 52;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		armPosition += base.Projectile.velocity.SafeNormalize((float)Owner.direction * Vector2.UnitX) * 32f;
		armPosition.Y -= 12f;
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
		if (Owner.CantUseHoldout() && ShootCountdown < 0f)
		{
			base.Projectile.Kill();
		}
		else if (ShootCountdown < 0f && Owner.HasAmmo(Owner.HeldItem))
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				ShootProjectiles(armPosition);
				ShootCountdown = Owner.HeldItem.useAnimation - 1;
				base.Projectile.netUpdate = true;
			}
			SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.Projectile.Center);
		}
		ShootCountdown--;
	}

	public void ShootProjectiles(Vector2 armPosition)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Item heldItem = Owner.HeldItem;
			Owner.PickAmmo(heldItem, out var projectileType, out var shootSpeed, out var damage, out var knockback, out var _);
			damage *= 4;
			shootSpeed = heldItem.shootSpeed * base.Projectile.scale * 0.64f;
			projectileType = ModContent.ProjectileType<PrismaticEnergyBlast>();
			knockback = Owner.GetWeaponKnockback(heldItem, knockback);
			Vector2 shootDirection = (Main.MouseWorld - base.Projectile.Center).SafeNormalize(-Vector2.UnitY);
			Vector2 shootVelocity = shootDirection * shootSpeed;
			if (shootDirection.X != base.Projectile.velocity.X || shootDirection.Y != base.Projectile.velocity.Y)
			{
				base.Projectile.netUpdate = true;
			}
			Vector2 gunTip = armPosition + shootDirection * heldItem.scale * 130f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), gunTip, shootVelocity, projectileType, damage, knockback, base.Projectile.owner);
			base.Projectile.velocity = shootDirection;
		}
	}

	public void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
	}

	public void ManipulatePlayerVariables()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
