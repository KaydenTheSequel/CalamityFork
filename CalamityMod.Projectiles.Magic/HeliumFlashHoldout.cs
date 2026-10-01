using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

internal class HeliumFlashHoldout : BaseGunHoldoutProjectile
{
	public SlotId HeliumChargeSlot;

	public static float BulletSpeed = 15f;

	public int time;

	public int starcoreFrameCounter;

	public int starcoreFrame;

	public override int AssociatedItemID => ModContent.ItemType<HeliumFlash>();

	public override float MaxOffsetLengthFromArm => 60f;

	public override float BaseOffsetY => 0f;

	public override float RecoilResolveSpeed => 0.4f;

	public override string Texture => "CalamityMod/Projectiles/Magic/HeliumFlashEmpty";

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 26f;
		}
	}

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private bool FullyCharged => CurrentChargingFrames >= (float)HeliumFlash.FullChargeFrames;

	public override void KillHoldoutLogic()
	{
		if (base.Owner.CantUseHoldout(needsToHold: false) || base.HeldItem.type != base.Owner.HeldItem.type)
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e55: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(HeliumChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		starcoreFrameCounter++;
		if (starcoreFrameCounter > ((!FullyCharged) ? 1 : 0))
		{
			starcoreFrame++;
			starcoreFrameCounter = 0;
		}
		if (starcoreFrame >= 6)
		{
			starcoreFrame = 0;
		}
		Color newColor;
		if (base.Owner.CantUseHoldout())
		{
			base.KeepRefreshingLifetime = false;
			if (base.Projectile.ai[1] != 1f)
			{
				base.Projectile.timeLeft = (FullyCharged ? (HeliumFlash.AftershotCooldownFrames * 3) : ((int)((float)HeliumFlash.AftershotCooldownFrames * 1.5f)));
				ChargeSound?.Stop();
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
				if (FullyCharged)
				{
					base.Projectile.ai[2] = 1f;
					base.OffsetLengthFromArm -= 25f;
					SoundEngine.PlaySound(in HeliumFlash.ChargeFire, base.Projectile.Center);
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity, ModContent.ProjectileType<VolatileStarcore>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					}
					GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.05f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, Vector2.Zero, Color.Red, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.08f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					for (int i = 0; i < 17; i++)
					{
						Vector2 gunTipPosition = GunTipPosition;
						newColor = default(Color);
						Dust dust = Dust.NewDustPerfect(gunTipPosition, 278, null, 0, newColor);
						dust.velocity = base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(5f, 25f);
						dust.scale = Main.rand.NextFloat(0.65f, 0.95f);
						dust.noGravity = true;
						dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.OrangeRed, 0.7f);
					}
					Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition, shootDirection * 18f, affectedByGravity: false, 6, 0.057f, Color.OrangeRed, new Vector2(1.7f, 0.8f), quickShrink: true));
					for (int j = 0; j <= 18; j++)
					{
						Vector2 spinninpoint = shootVelocity / 2f;
						GeneralParticleHandler.SpawnParticle(new LineParticle(scale: Main.rand.NextFloat(0.3f, 0.8f), velocity: spinninpoint.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(0.5f, 0.7f), relativePosition: GunTipPosition, affectedByGravity: false, lifetime: 40, color: Main.rand.NextBool() ? Color.Red : Color.DarkRed));
						float sparkScale2 = Main.rand.NextFloat(0.4f, 1f);
						Vector2 sparkvelocity2 = spinninpoint.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.9f, 1.6f);
						GeneralParticleHandler.SpawnParticle(new LineParticle(GunTipPosition, sparkvelocity2, affectedByGravity: false, 40, sparkScale2, Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed));
					}
				}
				else
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashDudFire");
					style.Volume = 0.4f;
					style.Pitch = 0.3f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					for (int k = 0; k < 12; k++)
					{
						Vector2 gunTipPosition2 = GunTipPosition;
						Vector2? velocity = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 3.5f);
						newColor = default(Color);
						Dust dust2 = Dust.NewDustPerfect(gunTipPosition2, 278, velocity, 0, newColor);
						dust2.scale = Main.rand.NextFloat(0.55f, 0.9f);
						dust2.noGravity = false;
						dust2.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.OrangeRed, 0.7f);
					}
				}
				base.Projectile.ai[1] = 1f;
			}
		}
		else
		{
			if (base.Projectile.ai[1] != 1f)
			{
				CurrentChargingFrames++;
			}
			if (FullyCharged)
			{
				if ((CurrentChargingFrames - (float)HeliumFlash.FullChargeFrames) % (float)HeliumFlash.ChargeLoopSoundFrames == 0f)
				{
					HeliumChargeSlot = SoundEngine.PlaySound(in HeliumFlash.ChargeLoop, base.Projectile.Center);
				}
				if (Main.rand.NextBool())
				{
					Vector2 dustVel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(3f, 5f);
					Vector2 position = GunTipPosition + dustVel;
					Vector2? velocity2 = dustVel * 0.15f;
					newColor = default(Color);
					Dust dust3 = Dust.NewDustPerfect(position, 278, velocity2, 0, newColor);
					dust3.scale = Main.rand.NextFloat(0.35f, 0.7f);
					dust3.noGravity = true;
					dust3.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.OrangeRed, 0.7f);
				}
			}
			else if (CurrentChargingFrames == 10f)
			{
				HeliumChargeSlot = SoundEngine.PlaySound(in HeliumFlash.Charge, base.Projectile.Center);
			}
			if (CurrentChargingFrames >= 10f && !FullyCharged)
			{
				GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(base.Owner, Main.rand.NextFloat(0.06f + CurrentChargingFrames / 180f, 0.08f + CurrentChargingFrames / 180f), Main.rand.NextVector2CircularEdge(2.5f, 2.5f) * Main.rand.NextFloat(0.3f * CurrentChargingFrames, 0.3f * CurrentChargingFrames), 0f, Color.Red, Color.Orange, 7, GunTipPosition));
			}
			if (CurrentChargingFrames == (float)HeliumFlash.FullChargeFrames)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashReady");
				style.Volume = 1f;
				style.Pitch = 0f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int l = 0; l < 18; l++)
				{
					Vector2 dustVel2 = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 5f);
					Vector2 position2 = GunTipPosition + dustVel2;
					Vector2? velocity3 = dustVel2 * 0.7f;
					newColor = default(Color);
					Dust dust4 = Dust.NewDustPerfect(position2, 278, velocity3, 0, newColor);
					dust4.scale = Main.rand.NextFloat(0.45f, 0.9f);
					dust4.noGravity = true;
					dust4.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.OrangeRed, 0.7f);
				}
			}
		}
		if (base.Projectile.ai[1] == 1f)
		{
			if (base.Projectile.ai[2] == 1f && base.Projectile.timeLeft == (int)((float)HeliumFlash.AftershotCooldownFrames * 1.4f))
			{
				base.OffsetLengthFromArm += 8f;
			}
			if (base.Projectile.ai[2] == 1f && base.Projectile.timeLeft == (int)((float)HeliumFlash.AftershotCooldownFrames * 1.1f))
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashSteamRelease");
				style.Volume = 0.6f;
				style.Pitch = 0f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int b = 0; b < 12; b++)
				{
					Vector2 smokeVel1 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.ToRadians(84f)) * 14f;
					Vector2 smokeVel2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.ToRadians(48f)) * 14f;
					Vector2 smokeVel3 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.ToRadians(125f)) * 14f;
					for (int m = 0; m < 2; m++)
					{
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition + smokeVel1 * 2f, smokeVel1 * Main.rand.NextFloat(0.2f, 0.7f), Color.Lerp(Color.SlateGray, Color.Orange, Main.rand.NextFloat(0f, 0.4f)), Main.rand.Next(15, 36), Main.rand.NextFloat(0.08f, 0.45f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition + smokeVel2 * 2f, smokeVel2 * Main.rand.NextFloat(0.2f, 0.7f), Color.Lerp(Color.SlateGray, Color.Orange, Main.rand.NextFloat(0f, 0.4f)), Main.rand.Next(15, 36), Main.rand.NextFloat(0.08f, 0.45f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition + smokeVel3 * 2f, smokeVel3 * Main.rand.NextFloat(0.2f, 0.7f), Color.Lerp(Color.SlateGray, Color.Orange, Main.rand.NextFloat(0f, 0.4f)), Main.rand.Next(15, 36), Main.rand.NextFloat(0.08f, 0.45f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
						Vector2 position3 = GunTipPosition + smokeVel1 * 2f;
						Vector2? velocity4 = smokeVel1.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.03f, 0.3f);
						newColor = default(Color);
						Dust dust5 = Dust.NewDustPerfect(position3, 303, velocity4, 180, newColor, Main.rand.NextFloat(0.3f, 1.1f));
						dust5.noGravity = false;
						dust5.color = Color.Lerp(Color.SlateGray, Color.Orange, Main.rand.NextFloat(0f, 0.4f));
						Vector2 position4 = GunTipPosition + smokeVel2 * 2f;
						Vector2? velocity5 = smokeVel2.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.03f, 0.3f);
						newColor = default(Color);
						Dust dust6 = Dust.NewDustPerfect(position4, 303, velocity5, 180, newColor, Main.rand.NextFloat(0.3f, 1.1f));
						dust6.noGravity = false;
						dust6.color = Color.Lerp(Color.SlateGray, Color.Orange, Main.rand.NextFloat(0f, 0.4f));
						Vector2 position5 = GunTipPosition + smokeVel3 * 2f;
						Vector2? velocity6 = smokeVel3.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.03f, 0.3f);
						newColor = default(Color);
						Dust dust7 = Dust.NewDustPerfect(position5, 303, velocity6, 180, newColor, Main.rand.NextFloat(0.3f, 1.1f));
						dust7.noGravity = false;
						dust7.color = Color.Lerp(Color.SlateGray, Color.Orange, Main.rand.NextFloat(0f, 0.4f));
						if (m == 0)
						{
							smokeVel1 *= -1f;
							smokeVel2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.ToRadians(-125f)) * 14f;
							smokeVel3 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.ToRadians(-48f)) * 14f;
						}
					}
				}
			}
			CurrentChargingFrames *= (FullyCharged ? 0f : 0.9f);
		}
		Vector2 gunTipPosition3 = GunTipPosition;
		newColor = Color.OrangeRed;
		Lighting.AddLight(gunTipPosition3, ((Color)(ref newColor)).ToVector3() * 1.5f * Utils.GetLerpValue(0f, HeliumFlash.FullChargeFrames, CurrentChargingFrames, clamped: true));
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(HeliumChargeSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI / 2f * (float)base.Owner.direction) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (!base.Owner.CantUseHoldout() && !FullyCharged)
		{
			float rumble = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)HeliumFlash.FullChargeFrames);
			drawPosition += Main.rand.NextVector2Circular(rumble / 25f, rumble / 25f);
		}
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/VolatileStarcore", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, 6, 0, starcoreFrame);
		Vector2 origin = frame.Size() * 0.5f;
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float bonusScale = 1f;
		if (CurrentChargingFrames >= (float)HeliumFlash.FullChargeFrames)
		{
			bonusScale = MathHelper.Clamp(2f * Utils.GetLerpValue((float)HeliumFlash.FullChargeFrames * 1.2f, HeliumFlash.FullChargeFrames, CurrentChargingFrames, clamped: true), 1f, 3f);
		}
		float randSize = Main.rand.NextFloat(0.8f, 1.2f);
		Vector2 position = GunTipPosition - Main.screenPosition;
		Color val = Color.OrangeRed;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.5f * Utils.GetLerpValue(0f, HeliumFlash.FullChargeFrames, CurrentChargingFrames, clamped: true) * randSize * bonusScale, (SpriteEffects)0);
		Vector2 position2 = GunTipPosition - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position2, null, val * Utils.GetLerpValue(0f, HeliumFlash.FullChargeFrames, CurrentChargingFrames, clamped: true) * 0.75f, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.25f * Utils.GetLerpValue(0f, HeliumFlash.FullChargeFrames, CurrentChargingFrames, clamped: true) * randSize * bonusScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation + MathHelper.ToRadians(45f * (float)base.Projectile.spriteDirection), rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Main.EntitySpriteDraw(value, GunTipPosition - Main.screenPosition, frame, Color.White, 0f, origin, base.Projectile.scale * 0.5f * Utils.GetLerpValue(0f, HeliumFlash.FullChargeFrames, CurrentChargingFrames, clamped: true), (SpriteEffects)0);
		return false;
	}
}
