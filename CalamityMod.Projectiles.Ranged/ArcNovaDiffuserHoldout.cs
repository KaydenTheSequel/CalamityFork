using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
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

namespace CalamityMod.Projectiles.Ranged;

public class ArcNovaDiffuserHoldout : BaseGunHoldoutProjectile
{
	public static int FramesPerLoad = 13;

	public static int MaxLoadableShots = 15;

	public static float BulletSpeed = 12f;

	public SlotId NovaChargeSlot;

	public Vector2 effectSpot;

	public float effectScale;

	public Vector2 effectSpot2;

	public float effectScale2;

	public Vector2 effectSpot3;

	public float effectScale3;

	public int time;

	public override int AssociatedItemID => ModContent.ItemType<ArcNovaDiffuser>();

	public override float MaxOffsetLengthFromArm => 24f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public override float RecoilResolveSpeed
	{
		get
		{
			if (!base.KeepRefreshingLifetime && !(CurrentChargingFrames < (float)ArcNovaDiffuser.Charge2Frames))
			{
				return 0.04f;
			}
			return 0.4f;
		}
	}

	public ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	public ref float ShotsLoaded => ref base.Projectile.ai[1];

	public ref float ShootRecoilTimer => ref base.Projectile.ai[2];

	public bool ChargeLV1 => CurrentChargingFrames >= (float)ArcNovaDiffuser.Charge1Frames;

	public bool ChargeLV2 => CurrentChargingFrames >= (float)ArcNovaDiffuser.Charge2Frames;

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
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(NovaChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		if (base.Owner.CantUseHoldout())
		{
			base.KeepRefreshingLifetime = false;
			if (ChargeLV2)
			{
				if (ShotsLoaded > 0f)
				{
					base.Projectile.timeLeft = ArcNovaDiffuser.AftershotCooldownFrames * 2;
					ChargeSound?.Stop();
					SoundEngine.PlaySound(in ArcNovaDiffuser.BigShot, base.Projectile.Center);
					Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
					int charge2Damage = (int)((float)base.Projectile.damage * 9f);
					float charge2KB = base.Projectile.knockBack * 3f;
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity, ModContent.ProjectileType<NovaChargedShot>(), charge2Damage, charge2KB, base.Projectile.owner);
					}
					base.Owner.SetScreenshake(6.5f);
					for (int i = 0; i <= 45; i++)
					{
						Dust dust = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, ModContent.DustType<SquashDust>(), shootVelocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.6f, 1.9f), 0, default(Color), Main.rand.NextFloat(2f, 3.3f));
						dust.noGravity = true;
						dust.color = (Main.rand.NextBool(3) ? Color.Lime : ArcNovaDiffuser.mainColor);
						dust.fadeIn = 2.5f;
						if (Main.rand.NextBool(4))
						{
							dust.scale = Main.rand.NextFloat(0.8f, 0.95f);
							dust.fadeIn = -0.85f;
							dust.velocity /= 2f;
						}
					}
					for (int j = 0; j < 2; j++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, base.Projectile.velocity * 25f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 16, 0.95f, ArcNovaDiffuser.mainColor, new Vector2(1.8f, 0.8f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.8f, 1f, 0.9f));
					}
					GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, base.Projectile.velocity * 2f, "CalamityMod/Particles/BloomRing", affectedByGravity: false, 12, 0.7f, ArcNovaDiffuser.mainColor, new Vector2(0.5f, 1.5f), useAddativeBlend: true, glowCenter: false, -(float)Math.PI / 2f, fadeIn: false, affectedByLight: false, 0f, 1f, 1f, flipHorizontal: false, noShrink: true));
					ShotsLoaded = 0f;
					ShootRecoilTimer = (base.Projectile.timeLeft = 55);
					base.OffsetLengthFromArm -= 25f;
				}
				else if (ShootRecoilTimer > 0f)
				{
					ShootRecoilTimer -= 2f;
				}
			}
			else if (ShotsLoaded > 0f)
			{
				base.Projectile.timeLeft = ArcNovaDiffuser.AftershotCooldownFrames;
				ShootRecoilTimer -= (ChargeLV1 ? 2.3f : 2f);
				if (ShootRecoilTimer <= 0f)
				{
					ChargeSound?.Stop();
					SoundEngine.PlaySound(in ArcNovaDiffuser.SmallShot, base.Projectile.Center);
					Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
					Vector2 fireVec = shootVelocity2.RotatedByRandom(MathHelper.ToRadians(2f));
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, fireVec * (ChargeLV1 ? 1f : 0.7f), ModContent.ProjectileType<NovaShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					}
					for (int k = 0; k <= 4; k++)
					{
						Dust dust2 = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, ModContent.DustType<SquashDust>(), shootVelocity2.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(1.2f, 1.6f), 0, default(Color), Main.rand.NextFloat(0.8f, 1.8f));
						dust2.noGravity = true;
						dust2.color = (Main.rand.NextBool(3) ? Color.Lime : ArcNovaDiffuser.mainColor);
						dust2.fadeIn = 1.5f;
						if (Main.rand.NextBool(6))
						{
							dust2.scale = Main.rand.NextFloat(0.4f, 0.5f);
							dust2.fadeIn = -0.85f;
							dust2.velocity /= 2f;
						}
					}
					ShotsLoaded--;
					ShootRecoilTimer = (int)MathHelper.Lerp(33f, 13f, Math.Min(CurrentChargingFrames / (float)ArcNovaDiffuser.Charge1Frames, 1f));
					base.OffsetLengthFromArm -= 6f;
				}
			}
			else if (ShootRecoilTimer > 0f)
			{
				ShootRecoilTimer -= 2f;
			}
		}
		else
		{
			if (ShotsLoaded < (float)MaxLoadableShots && CurrentChargingFrames % (float)FramesPerLoad == 0f)
			{
				ShotsLoaded++;
			}
			if (ChargeLV1)
			{
				CurrentChargingFrames += 2f;
			}
			else
			{
				CurrentChargingFrames++;
			}
			if (ChargeLV1)
			{
				if (CurrentChargingFrames == (float)ArcNovaDiffuser.Charge2Frames)
				{
					SoundEngine.PlaySound(in ArcNovaDiffuser.ChargeLV2, base.Projectile.Center);
				}
				else if (CurrentChargingFrames == (float)ArcNovaDiffuser.Charge1Frames)
				{
					SoundEngine.PlaySound(in ArcNovaDiffuser.ChargeLV1, base.Projectile.Center);
					ShotsLoaded = MaxLoadableShots;
				}
				if ((CurrentChargingFrames - (float)ArcNovaDiffuser.Charge1Frames) % (float)(ArcNovaDiffuser.ChargeLoopSoundFrames * 2) == 0f)
				{
					NovaChargeSlot = SoundEngine.PlaySound(in ArcNovaDiffuser.ChargeLoop, base.Projectile.Center);
				}
			}
			else if (CurrentChargingFrames == 10f)
			{
				NovaChargeSlot = SoundEngine.PlaySound(in ArcNovaDiffuser.ChargeStart, base.Projectile.Center);
			}
			if (CurrentChargingFrames >= 10f)
			{
				float strength = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)ArcNovaDiffuser.Charge2Frames) / 45f;
				Vector3 DustLight = default(Vector3);
				((Vector3)(ref DustLight))._002Ector(0f, 0.255f, 0f);
				Lighting.AddLight(GunTipPosition, DustLight * strength);
			}
			if (CurrentChargingFrames == (float)ArcNovaDiffuser.Charge1Frames)
			{
				for (int l = 0; l < 25; l++)
				{
					Dust dust3 = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<SquashDust>());
					dust3.velocity = ((float)Math.PI * 2f * (float)l / 25f).ToRotationVector2() * Main.rand.NextFloat(8f, 9.5f);
					dust3.scale = Main.rand.NextFloat(1.2f, 1.5f);
					dust3.noGravity = true;
					dust3.color = (Main.rand.NextBool(3) ? Color.Lime : ArcNovaDiffuser.mainColor);
					dust3.fadeIn = 1f;
				}
			}
			if (CurrentChargingFrames == (float)ArcNovaDiffuser.Charge2Frames)
			{
				for (int m = 0; m < 35; m++)
				{
					Dust dust4 = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<SquashDust>());
					dust4.velocity = ((float)Math.PI * 2f * (float)m / 35f).ToRotationVector2() * Main.rand.NextFloat(11f, 13.5f);
					dust4.scale = Main.rand.NextFloat(1.7f, 1.9f);
					dust4.noGravity = true;
					dust4.color = (Main.rand.NextBool(3) ? Color.Lime : ArcNovaDiffuser.mainColor);
					dust4.fadeIn = 1f;
				}
			}
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(NovaChargeSlot, out ActiveSound ChargeSound))
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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 tipDrawPosition = GunTipPosition - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Color color = ArcNovaDiffuser.mainColor;
		float chargeScale = (base.KeepRefreshingLifetime ? Math.Min(Utils.GetLerpValue(0f, ArcNovaDiffuser.Charge1Frames, CurrentChargingFrames), 2f) : 0f);
		if (!base.Owner.CantUseHoldout())
		{
			float rumble = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)ArcNovaDiffuser.Charge2Frames);
			drawPosition += Main.rand.NextVector2Circular(rumble / 70f, rumble / 70f);
		}
		float sine = (float)Math.Sin((float)time * 0.15f / (float)Math.PI);
		Color val;
		for (int i = 0; i < 10; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * (2f + sine * 0.3f);
			Vector2 position = drawPosition + drawOffset * chargeScale;
			val = color;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(texture, position, null, val * Math.Min(chargeScale, 1f) * 0.3f, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		}
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Asset<Texture2D> tex3 = ModContent.Request<Texture2D>("CalamityMod/Particles/ForwardSmear", (AssetRequestMode)2);
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		for (int j = 0; j < 4; j++)
		{
			Vector2 place = Vector2.One.RotateRandom(6.2831854820251465) * Main.rand.NextFloat(1f, 5.5f) * Math.Min(chargeScale, 1f);
			Vector2 vel = base.Projectile.velocity.RotatedByRandom(0.30000001192092896);
			Texture2D value = tex3.Value;
			Vector2 position2 = tipDrawPosition + place - Vector2.Lerp(place, -base.Projectile.velocity, 0.9f) * Main.rand.NextFloat(15f, 35f) + base.Projectile.velocity * -8f * (6f - chargeScale * 2f);
			val = (Main.rand.NextBool(3) ? Color.Lime : color);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position2, null, val * 0.8f * Math.Min(chargeScale, 1f), vel.ToRotation() - (float)Math.PI / 2f, new Vector2((float)(tex3.Width() / 2), (float)tex3.Height()), new Vector2(0.25f * chargeScale, 1.7f + (Main.rand.NextBool(4) ? 1.8f : 0f)) * base.Projectile.scale * 0.05f * Math.Min(chargeScale, 1f), (SpriteEffects)0);
		}
		for (int k = 0; k < 3; k++)
		{
			Texture2D value2 = tex2.Value;
			val = Color.Lerp(color, Color.White, (float)k * 0.25f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, tipDrawPosition, null, val * 0.8f, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1.35f, 1f) * base.Projectile.scale * chargeScale * (1f - 0.27f * (float)k) * 0.23f, (SpriteEffects)0);
		}
		if (CurrentChargingFrames >= (float)ArcNovaDiffuser.Charge2Frames)
		{
			for (int y = 0; y < 3; y++)
			{
				Vector2 angleVel = ((float)Math.PI * 2f * (float)y / 3f).ToRotationVector2().RotatedBy((float)time * 0.18f);
				Vector2 drawOffset2 = Utils.RotatedBy(new Vector2(angleVel.X * 0.7f, angleVel.Y * 1.2f), (double)base.Projectile.rotation, default(Vector2)) * chargeScale * 12f;
				for (int l = 0; l < 2; l++)
				{
					Texture2D value3 = tex2.Value;
					Vector2 position3 = tipDrawPosition + drawOffset2;
					val = Color.Lerp(color, Color.White, (float)l);
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value3, position3, null, val * 0.8f, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1f, 1f) * base.Projectile.scale * chargeScale * (1f - 0.25f * (float)l) * 0.085f * (float)((!(CurrentChargingFrames <= (float)(ArcNovaDiffuser.Charge2Frames + 3))) ? 1 : 3), (SpriteEffects)0);
				}
			}
		}
		return false;
	}
}
