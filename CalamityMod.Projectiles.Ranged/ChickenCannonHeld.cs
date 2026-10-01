using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChickenCannonHeld : ModProjectile
{
	private static float FireRate = 33f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<ChickenCannon>();

	public ref float FreeShotLoaded => ref base.Projectile.ai[0];

	public ref float FramesTillNextShot => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 126;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		FramesTillNextShot--;
		bool shouldShoot = false;
		if (FramesTillNextShot <= 0f)
		{
			FramesTillNextShot = FireRate;
			shouldShoot = true;
		}
		bool canShoot = !player.CantUseHoldout() && (player.HasAmmo(player.HeldItem) || FreeShotLoaded > 0f);
		if ((base.Projectile.soundDelay <= 0) & canShoot)
		{
			base.Projectile.soundDelay = (int)FireRate;
			SoundEngine.PlaySound(in SoundID.Item61, base.Projectile.position);
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			int projType = ModContent.ProjectileType<ChickenRocket>();
			float speedMult2 = 14.5f;
			int dmg = player.GetWeaponDamage(player.HeldItem);
			float kBack = player.HeldItem.knockBack;
			if (canShoot)
			{
				Vector2 source = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
				Vector2 direction = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - source;
				if (player.gravDir == -1f)
				{
					direction.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - source.Y;
				}
				Vector2 speedMult3 = Vector2.Normalize(direction);
				float shootSpeed = player.HeldItem.shootSpeed * base.Projectile.scale;
				if (float.IsNaN(speedMult3.X) || float.IsNaN(speedMult3.Y))
				{
					speedMult3 = -Vector2.UnitY;
				}
				speedMult3 *= shootSpeed;
				if (speedMult3.X != base.Projectile.velocity.X || speedMult3.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = speedMult3 * 0.55f;
				if (shouldShoot)
				{
					if (FreeShotLoaded > 0f)
					{
						FreeShotLoaded--;
					}
					else
					{
						player.PickAmmo(player.HeldItem, out projType, out speedMult2, out dmg, out kBack, out var _);
					}
					projType = ModContent.ProjectileType<ChickenRocket>();
					kBack = player.GetWeaponKnockback(player.HeldItem, kBack);
					Vector2 velocity = Vector2.Normalize(base.Projectile.velocity) * speedMult2;
					if (float.IsNaN(velocity.X) || float.IsNaN(velocity.Y))
					{
						velocity = -Vector2.UnitY;
					}
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), source, velocity, projType, dmg, kBack, base.Projectile.owner);
				}
			}
			else if (!canShoot)
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		if (player.itemTime < 2)
		{
			player.itemTime = 2;
		}
		if (player.itemAnimation < 2)
		{
			player.itemAnimation = 2;
		}
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
