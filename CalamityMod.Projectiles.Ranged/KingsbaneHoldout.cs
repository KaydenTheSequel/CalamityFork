using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class KingsbaneHoldout : ModProjectile
{
	public int Time;

	public int revTimer;

	public int framesBetweenShots;

	public bool fullRev;

	public int fullRevShots = 50;

	public int windupAnim = 11;

	public int soundTimer;

	public bool discharging;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Kingsbane>();

	public override string Texture => "CalamityMod/Projectiles/Ranged/KingsbaneWindUp";

	private Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 112;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void AI()
	{
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time == 3)
		{
			base.Projectile.alpha = 0;
		}
		if (Time % 2 == 0)
		{
			soundTimer++;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > windupAnim && !Owner.CantUseHoldout())
		{
			if (base.Projectile.frame == 1 && Time < 85)
			{
				base.Projectile.frame = 0;
			}
			else
			{
				base.Projectile.frame++;
			}
			if (windupAnim > 0)
			{
				windupAnim--;
			}
			base.Projectile.frameCounter = 0;
		}
		else if (Owner.CantUseHoldout())
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 2;
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		Vector2 tipPosition = armPosition + base.Projectile.velocity * (float)base.Projectile.width * 0.85f + new Vector2(0f, 3.8f);
		Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 15f;
		int bulletAMMO = 14;
		Owner.PickAmmo(Owner.HeldItem, out bulletAMMO, out var _, out var _, out var _, out var _);
		if (Owner.CantUseHoldout() || discharging)
		{
			discharging = true;
			if (fullRev && fullRevShots > 0)
			{
				base.Projectile.timeLeft = 2;
				Dust dust = Dust.NewDustPerfect(tipPosition - base.Projectile.velocity * 68f, 87, base.Projectile.velocity.RotatedBy(8.6f * Main.rand.NextFloat(0.975f, 1.025f) * (float)(-base.Projectile.direction)) * Main.rand.NextFloat(5.5f, 7f) + Owner.velocity * 0.5f);
				dust.noGravity = false;
				dust.scale = Main.rand.NextFloat(0.8f, 0.9f);
				Dust dust2 = Dust.NewDustPerfect(tipPosition - base.Projectile.velocity * 5f, Main.rand.NextBool(4) ? 169 : 162, (base.Projectile.velocity * Main.rand.NextFloat(4f, 15.5f)).RotatedByRandom(0.30000001192092896));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(1.3f, 2.2f);
				Owner.SetScreenshake(1.85f);
				Player owner = Owner;
				owner.velocity += -base.Projectile.velocity * (float)fullRevShots * (Main.zenithWorld ? 0.028f : 0.013f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition + base.Projectile.velocity * 5f + Main.rand.NextVector2Circular(7f, 7f), shootVelocity.RotatedByRandom(MathHelper.ToRadians(4f)), ModContent.ProjectileType<AuricBullet>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				SoundStyle fire = new SoundStyle("CalamityMod/Sounds/Item/GunShotSmall");
				if (fullRevShots % 2 == 0)
				{
					SoundStyle style = fire with
					{
						Volume = 0.7f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				Owner.channel = true;
				fullRevShots--;
			}
		}
		else
		{
			if (Time < 90 && soundTimer > windupAnim + 2)
			{
				SoundStyle style = SoundID.Item23 with
				{
					Pitch = (float)(8 - windupAnim) * 0.15f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				soundTimer = 0;
			}
			base.Projectile.timeLeft = 2;
			if (Time > 90)
			{
				fullRev = true;
				if (framesBetweenShots == 0)
				{
					Dust dust3 = Dust.NewDustPerfect(tipPosition - base.Projectile.velocity * 68f, 87, base.Projectile.velocity.RotatedBy(8.6f * Main.rand.NextFloat(0.985f, 1.015f) * (float)(-base.Projectile.direction)) * Main.rand.NextFloat(4f, 5f) + Owner.velocity * 0.5f);
					dust3.noGravity = false;
					dust3.scale = Main.rand.NextFloat(0.8f, 0.9f);
					for (int i = 0; i <= 2; i++)
					{
						Dust dust4 = Dust.NewDustPerfect(tipPosition - base.Projectile.velocity * 6f, Main.rand.NextBool(3) ? 263 : 247, (base.Projectile.velocity * Main.rand.NextFloat(4f, 15.5f)).RotatedByRandom(0.20000000298023224));
						dust4.noGravity = true;
						dust4.scale = Main.rand.NextFloat(0.9f, 1.6f);
					}
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, shootVelocity.RotatedByRandom(MathHelper.ToRadians(1.5f)), bulletAMMO, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/GunShotMid");
					style.Volume = 0.4f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					framesBetweenShots = 3;
				}
				if (framesBetweenShots > 0)
				{
					framesBetweenShots--;
				}
			}
		}
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
	}

	private void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Utils.GetLerpValue(0f, 55f, Owner.Distance(Main.MouseWorld), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(Main.MouseWorld), discharging ? 0.2f : 0.45f).SafeNormalize(Vector2.UnitY);
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		base.Projectile.Center = armPosition + base.Projectile.velocity * MathHelper.Clamp(47f - (float)(framesBetweenShots * 2), 0f, 47f) + new Vector2(0f, 5f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (Owner.CantUseHoldout() || discharging)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += Main.rand.NextVector2Circular(4.5f, 4.5f);
		}
	}

	private void ManipulatePlayerVariables()
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
