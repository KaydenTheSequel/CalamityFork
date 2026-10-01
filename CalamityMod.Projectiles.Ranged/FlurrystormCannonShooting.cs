using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlurrystormCannonShooting : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<FlurrystormCannon>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 68;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 2)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.ai[0]++;
		int fireRate = 0;
		if (base.Projectile.ai[0] >= 60f)
		{
			fireRate++;
		}
		if (base.Projectile.ai[0] >= 120f)
		{
			fireRate++;
		}
		if (base.Projectile.ai[0] >= 180f)
		{
			fireRate++;
		}
		if (base.Projectile.ai[0] >= 240f)
		{
			fireRate++;
		}
		if (base.Projectile.ai[0] >= 300f)
		{
			fireRate++;
		}
		if (base.Projectile.ai[0] >= 360f)
		{
			fireRate++;
		}
		int initialRate = 26;
		int fireRateMult = 3;
		base.Projectile.ai[1]--;
		bool shouldShoot = false;
		if (base.Projectile.ai[1] <= 0f)
		{
			base.Projectile.ai[1] = initialRate - fireRateMult * fireRate;
			shouldShoot = true;
		}
		bool canShoot = !player.CantUseHoldout() && player.HasAmmo(player.HeldItem);
		if (base.Projectile.localAI[0] > 0f)
		{
			base.Projectile.localAI[0]--;
		}
		if ((base.Projectile.soundDelay <= 0) & canShoot)
		{
			base.Projectile.soundDelay = initialRate - fireRateMult * fireRate;
			if (base.Projectile.ai[0] != 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item11, base.Projectile.position);
			}
			base.Projectile.localAI[0] = 12f;
		}
		if (shouldShoot && Main.myPlayer == base.Projectile.owner)
		{
			int projType = 166;
			float speedMult2 = 14f;
			int dmg = player.GetWeaponDamage(player.HeldItem);
			float kBack = player.HeldItem.knockBack;
			if (canShoot)
			{
				player.PickAmmo(player.HeldItem, out projType, out speedMult2, out dmg, out kBack, out var _);
				kBack = player.GetWeaponKnockback(player.HeldItem, kBack);
				float shootSpeed = player.HeldItem.shootSpeed * base.Projectile.scale;
				Vector2 source = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
				Vector2 direction = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - source;
				if (player.gravDir == -1f)
				{
					direction.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - source.Y;
				}
				Vector2 speedMult3 = Vector2.Normalize(direction);
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
				Vector2 snowballVel = Vector2.Normalize(base.Projectile.velocity) * speedMult2 * (0.6f + Main.rand.NextFloat(0f, 0.15f));
				if (float.IsNaN(snowballVel.X) || float.IsNaN(snowballVel.Y))
				{
					snowballVel = -Vector2.UnitY;
				}
				Vector2 sourceS = source + Utils.RandomVector2(Main.rand, -5f, 5f);
				snowballVel.X += Main.rand.NextFloat(-2f, 2f);
				snowballVel.Y += Main.rand.NextFloat(-2f, 2f);
				int snowball = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), sourceS, snowballVel, projType, dmg, kBack, base.Projectile.owner);
				if (snowball.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[snowball].noDropItem = true;
					Main.projectile[snowball].DamageType = DamageClass.Ranged;
					Main.projectile[snowball].extraUpdates += Main.rand.Next(0, 2);
				}
				if (Main.rand.NextBool(5))
				{
					Vector2 chunkVel = Vector2.Normalize(base.Projectile.velocity) * speedMult2 * (0.6f + Main.rand.NextFloat() * 0.8f);
					if (float.IsNaN(chunkVel.X) || float.IsNaN(chunkVel.Y))
					{
						chunkVel = -Vector2.UnitY;
					}
					Vector2 sourceC = source + Utils.RandomVector2(Main.rand, -15f, 15f);
					int iceChunk = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), sourceC, chunkVel, ModContent.ProjectileType<FlurrystormIceChunk>(), (int)((double)dmg * 1.5), (int)((double)kBack * 1.5), base.Projectile.owner, 0f, chunkVel.Y);
					Main.projectile[iceChunk].extraUpdates += fireRate / 2;
				}
			}
			else
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
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
