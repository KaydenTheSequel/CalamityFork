using System;
using System.Collections.Generic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PhotovisceratorHoldout : ModProjectile
{
	public Color sparkColor;

	public Color sparkColorSmooth;

	public int Time;

	public SlotId PhotoUseSound;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Photoviscerator>();

	public Player Owner => Main.player[base.Projectile.owner];

	public bool OwnerCanShoot
	{
		get
		{
			if ((Owner.channel || Owner.Calamity().mouseRight) && !Owner.noItems)
			{
				return !Owner.CCed;
			}
			return false;
		}
	}

	public ref float ShootTimer => ref base.Projectile.ai[0];

	public ref float ForcedLifespan => ref base.Projectile.ai[1];

	public ref int PhotoTimer => ref Main.player[base.Projectile.owner].Calamity().PhotoTimer;

	public override void SetDefaults()
	{
		base.Projectile.width = 170;
		base.Projectile.height = 66;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (PhotoTimer > 0)
		{
			PhotoTimer--;
		}
		if (Time == 1)
		{
			base.Projectile.alpha = 255;
		}
		else
		{
			base.Projectile.alpha = 0;
		}
		sparkColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		});
		List<Color> eColors = new List<Color>
		{
			Color.OrangeRed,
			Color.MediumTurquoise,
			Color.Orange,
			Color.LawnGreen
		};
		float rate = Main.GlobalTimeWrappedHourly * 8f;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		sparkColorSmooth = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (Owner.Calamity().mouseRight && SoundEngine.TryGetActiveSound(PhotoUseSound, out ActiveSound Sound2))
		{
			Sound2?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(PhotoUseSound, out ActiveSound Sound3) && Sound3.IsPlaying)
		{
			Sound3.Position = base.Projectile.Center;
		}
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
		Color energyColor = Color.Orange;
		Vector2 flamePosition = armPosition + base.Projectile.velocity * (float)base.Projectile.width * 0.12f;
		Vector2 verticalOffset = Vector2.UnitY.RotatedBy(base.Projectile.rotation);
		if (Math.Cos(base.Projectile.rotation) < 0.0)
		{
			verticalOffset *= -1f;
		}
		if (Main.rand.NextBool(4))
		{
			Vector2 flameAngle = -Vector2.UnitY.RotatedBy(base.Projectile.rotation + MathHelper.ToRadians(Main.rand.NextFloat(270f, 300f) * (float)base.Projectile.spriteDirection));
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(flamePosition - verticalOffset * 26f, flameAngle * Main.rand.NextFloat(0.8f, 3.6f), 0.25f, energyColor, 20));
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(armPosition - verticalOffset * 22f, flameAngle * Main.rand.NextFloat(0.7f, 3.2f), 0.2f, energyColor, 12));
		}
		Lighting.AddLight(armPosition + base.Projectile.velocity * 42f - verticalOffset * 10f, 0.8f, 0.8f, 0.8f);
		Lighting.AddLight(armPosition + base.Projectile.velocity * 96f + verticalOffset * 6f, 0.4f, 0.4f, 0.4f);
		if (!OwnerCanShoot)
		{
			ForcedLifespan--;
			if (ForcedLifespan <= 0f)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			if (base.Projectile.owner != Main.myPlayer)
			{
				return;
			}
			if (!Owner.HasAmmo(Owner.HeldItem))
			{
				base.Projectile.Kill();
			}
			else if (Owner.Calamity().mouseRight && !Owner.channel)
			{
				if (ShootTimer < 0f)
				{
					RightClickAttack(armPosition, verticalOffset);
				}
				ShootTimer--;
			}
			else if (!Owner.Calamity().mouseRight && Owner.channel)
			{
				ShootTimer++;
				LeftClickAttack(armPosition, verticalOffset);
			}
		}
	}

	public void LeftClickAttack(Vector2 armPosition, Vector2 verticalOffset)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 1 || Time % 55 == 0)
		{
			PhotoUseSound = SoundEngine.PlaySound(in Photoviscerator.UseSound, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(PhotoUseSound, out ActiveSound Sound3))
		{
			Sound3.Pitch = 0f - (float)PhotoTimer * 0.002f;
		}
		Owner.PickAmmo(Owner.HeldItem, out var _, out var shootSpeed, out var damage, out var knockback, out var _, Main.rand.Next(100) < Photoviscerator.AmmoSavedPercent);
		IEntitySource source = base.Projectile.GetSource_FromThis();
		Vector2 position = armPosition + base.Projectile.velocity * 55f - verticalOffset * 10f;
		Vector2 velocity = base.Projectile.velocity * shootSpeed;
		if (PhotoTimer == 1)
		{
			for (int i = 0; i < 30; i++)
			{
				sparkColor = (Color)(Main.rand.Next(4) switch
				{
					0 => Color.Red, 
					1 => Color.MediumTurquoise, 
					2 => Color.Orange, 
					_ => Color.LawnGreen, 
				});
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(position, (base.Projectile.velocity * 3f).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.3f, 1.6f), 0.9f, sparkColorSmooth, 60));
			}
			SoundStyle style = DeadSunsWind.ShootSound with
			{
				Volume = 1.9f
			};
			SoundEngine.PlaySound(in style, Owner.MountedCenter);
		}
		Dust dust = Dust.NewDustPerfect(position, 263, (base.Projectile.velocity * 10f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.6f));
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(1.3f, 1.8f) - (float)PhotoTimer * 0.02f;
		dust.color = sparkColorSmooth;
		Dust dust2 = Dust.NewDustPerfect(position, 263, (base.Projectile.velocity * 15f).RotatedByRandom(0.25) * Main.rand.NextFloat(0.3f, 1.6f));
		dust2.noGravity = true;
		dust2.scale = Main.rand.NextFloat(1.3f, 1.8f) - (float)PhotoTimer * 0.02f;
		dust2.color = sparkColorSmooth;
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position + Main.rand.NextVector2Circular(5f, 5f), (base.Projectile.velocity * 15f).RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(0.5f, 2.1f), sparkColorSmooth, Color.White, Main.rand.NextFloat(1.8f, 2.9f) - (float)PhotoTimer * 0.026f, 160f, Main.rand.NextFloat(-3f, 3f)));
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position + Main.rand.NextVector2Circular(5f, 5f), (base.Projectile.velocity * 18f).RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(1.8f, 3.1f), sparkColorSmooth, Color.White, Main.rand.NextFloat(0.8f, 1.9f) - (float)PhotoTimer * 0.02f, 160f, Main.rand.NextFloat(-3f, 3f)));
		}
		Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.004999999888241291), ModContent.ProjectileType<ExoFire>(), (int)((float)damage * (1f - (float)PhotoTimer / 59.9f)), knockback, base.Projectile.owner, Main.rand.NextFloat(0f, 3f));
		if (ShootTimer >= (float)(Owner.HeldItem.useTime * 10) && PhotoTimer == 0)
		{
			ShootTimer = 0f;
			for (int j = 0; j < 2; j++)
			{
				Vector2 bombPos = armPosition + base.Projectile.velocity * 108f + verticalOffset * 6f;
				int yDirection = (j == 0).ToDirectionInt();
				Vector2 bombVel = velocity.RotatedBy(0.2f * (float)yDirection);
				Projectile.NewProjectile(source, bombPos, bombVel, ModContent.ProjectileType<ExoLight>(), damage, knockback, base.Projectile.owner, yDirection);
			}
			SoundStyle style = HalleysInferno.Hit with
			{
				Volume = 0.5f
			};
			SoundEngine.PlaySound(in style, Owner.MountedCenter);
		}
	}

	public void RightClickAttack(Vector2 armPosition, Vector2 verticalOffset)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		ShootTimer = (float)(Photoviscerator.RightClickCooldown * Owner.HeldItem.useTime) / (float)Photoviscerator.LightBombCooldown - 1f;
		ForcedLifespan = ShootTimer;
		Owner.PickAmmo(Owner.HeldItem, out var _, out var shootSpeed, out var damage, out var knockback, out var _);
		IEntitySource source = base.Projectile.GetSource_FromThis();
		Vector2 position = armPosition + base.Projectile.velocity * 55f - verticalOffset * 10f;
		Vector2 velocity = base.Projectile.velocity * shootSpeed * Photoviscerator.RightClickVelocityMult;
		for (int i = 0; i <= 15; i++)
		{
			sparkColor = (Color)(Main.rand.Next(4) switch
			{
				0 => Color.Red, 
				1 => Color.MediumTurquoise, 
				2 => Color.Orange, 
				_ => Color.LawnGreen, 
			});
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(position, (base.Projectile.velocity * 10f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.6f), sparkColor, new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.2f, 0.35f), 0f, 40));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(position, (base.Projectile.velocity * 10f).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(0.8f, 3.1f), sparkColor, new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.2f, 0.35f), 0f, 40));
			Dust dust = Dust.NewDustPerfect(position, 263, (base.Projectile.velocity * 10f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.6f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.3f, 1.8f);
			dust.color = sparkColor;
		}
		SoundStyle style = HalleysInferno.ShootSound with
		{
			Volume = 0.4f
		};
		SoundEngine.PlaySound(in style, Owner.MountedCenter);
		int rightClickDamage = (int)(0.7f * (float)damage);
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ExoFlareCluster>(), rightClickDamage, knockback, base.Projectile.owner);
	}

	public void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			float interpolant = Utils.GetLerpValue(5f, 90f, base.Projectile.Distance(Main.MouseWorld), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(Main.MouseWorld), interpolant);
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 28f;
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

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(PhotoUseSound, out ActiveSound Sound))
		{
			Sound?.Stop();
		}
		PhotoTimer = 90;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(85f, 33f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/PhotovisceratorHoldoutGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
	}
}
