using System;
using CalamityMod.Cooldowns;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PhalanxSurgeHoldout : BaseGunHoldoutProjectile
{
	public float offsetBase = 30f;

	public SlotId ChargeSlot;

	public SlotId StartSlot;

	public bool fullyCharged;

	public bool doingChargeAttack;

	public bool startChargeLoop = true;

	public int time;

	private float charge;

	public bool didDash;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override int AssociatedItemID => ModContent.ItemType<PhalanxSurge>();

	public override float MaxOffsetLengthFromArm
	{
		get
		{
			if (!doingChargeAttack)
			{
				return offsetBase;
			}
			return 100f;
		}
	}

	public override float RecoilResolveSpeed
	{
		get
		{
			if (!doingChargeAttack)
			{
				if (!(shootingTimer < 0f))
				{
					return 0.1f;
				}
				return 0.2f;
			}
			return 2f;
		}
	}

	public override float OffsetXUpwards => 0f;

	public override float BaseOffsetY => 0f;

	public override float OffsetYDownwards => 0f;

	public override float WeaponTurnSpeed
	{
		get
		{
			if (!(shootingTimer < 0f))
			{
				return 0.35f;
			}
			return 0f;
		}
	}

	public bool charging => base.Projectile.ai[2] == 5f;

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public ref float chargeTimer => ref base.Projectile.ai[1];

	public int fireRate => base.Owner.HeldItem.useAnimation;

	public int chargeRate => 65;

	public override void KillHoldoutLogic()
	{
		if (base.Owner.dead)
		{
			StopSounds();
			base.Projectile.Kill();
		}
	}

	public void StopSounds()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(StartSlot, out ActiveSound audio) && audio.IsPlaying)
		{
			audio?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound audio2) && audio2.IsPlaying)
		{
			audio2?.Stop();
		}
	}

	public override void HoldoutAI()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			base.OffsetLengthFromArm = offsetBase;
			if (!charging)
			{
				shootingTimer = fireRate;
			}
		}
		if (shootingTimer < 0f)
		{
			if (base.Projectile.localAI[0] > 0f)
			{
				base.Owner.velocity = -base.Projectile.velocity * (didDash ? 25f : (5f + 10f * base.Projectile.localAI[0]));
				didDash = false;
				base.Projectile.localAI[0] = 0f;
			}
			if (didDash)
			{
				base.Owner.velocity = base.Projectile.velocity * 25f;
				if (!Collision.SolidCollision(GunTipPosition, 10, 10))
				{
					Player owner = base.Owner;
					owner.Center += base.Projectile.velocity * 40f;
					Projectile projectile = base.Projectile;
					projectile.Center += base.Projectile.velocity * 40f;
				}
				float fxScale = Utils.GetLerpValue(10f, 25f, ((Vector2)(ref base.Owner.velocity)).Length(), clamped: true);
				for (int i = 0; i < 3; i++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Owner.Center + Main.rand.NextVector2Circular(20f, 20f), -base.Owner.velocity * Main.rand.NextFloat(0.4f, 1.2f), "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 12, Main.rand.NextFloat(0.02f, 0.03f) * fxScale, ArsenalEffects.ArsenalLaserColor * 0.8f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1f * fxScale));
				}
			}
			base.Owner.direction = base.Projectile.direction;
			shootingTimer++;
			if (shootingTimer == 0f)
			{
				didDash = false;
				shootingTimer = fireRate;
			}
			return;
		}
		if (base.HeldItem.type != base.Owner.HeldItem.type)
		{
			StopSounds();
			base.Projectile.Kill();
			return;
		}
		if (!charging)
		{
			StopSounds();
		}
		if (shootingTimer == 0f)
		{
			if (!charging || base.Owner.Calamity().arsenalCooldown > 0)
			{
				if (base.Owner.Calamity().mouseRight)
				{
					base.Projectile.ai[2] = 5f;
					shootingTimer = 1f;
					return;
				}
				Shoot(isSkewer: false);
			}
			else if (base.Owner.Calamity().mouseRight && !doingChargeAttack)
			{
				for (int j = 0; j < 4; j++)
				{
					Vector2 place = GunTipPosition + Main.rand.NextVector2Circular(5f, 5f) + base.Projectile.velocity * 12f;
					int dir = (Main.rand.NextBool() ? 1 : (-1));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(place, -base.Projectile.velocity.RotatedBy(0.42f * (float)dir).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(8f, 30f), "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 6, 0.015f * charge, ArsenalEffects.ArsenalLaserColor, new Vector2(1f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1.4f));
					if (j % 2 == 0)
					{
						Dust dust = Dust.NewDustPerfect(place, ArsenalEffects.ArsenalLaserDust, -base.Projectile.velocity.RotatedBy(0.42f * (float)dir).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(4f, 20f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.9f) * charge);
						dust.noGravity = true;
						dust.color = ArsenalEffects.ArsenalLaserColor;
						dust.alpha = 100;
					}
				}
				bool num = SoundEngine.TryGetActiveSound(StartSlot, out ActiveSound audio) && audio != null;
				if (chargeTimer == (float)chargeRate && (!SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound audio3) || !audio3.IsPlaying))
				{
					SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/PhalanxSurgeChargeLoop");
					SoundStyle style = sound with
					{
						Volume = 0.7f,
						IsLooped = true
					};
					ChargeSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if ((!num || !audio.IsPlaying) && startChargeLoop)
				{
					SoundStyle sound2 = new SoundStyle("CalamityMod/Sounds/Item/PhalanxSurgeCharge");
					SoundStyle style = sound2 with
					{
						Volume = 0.9f
					};
					StartSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
					startChargeLoop = false;
				}
				if (chargeTimer == (float)chargeRate)
				{
					SoundStyle sound3 = new SoundStyle("CalamityMod/Sounds/Item/PhalanxSurgeChargeMax");
					SoundStyle style = sound3 with
					{
						Volume = 0.8f
					};
					StartSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
					fullyCharged = true;
					chargeTimer = 0f;
					base.OffsetLengthFromArm -= 7f;
				}
				if (!fullyCharged)
				{
					chargeTimer++;
				}
			}
			else if (fullyCharged)
			{
				doingChargeAttack = true;
				int chargeAnimMax = (int)((float)chargeRate * 0.2f);
				float completion = chargeTimer / (float)chargeAnimMax;
				if (chargeTimer <= (float)chargeAnimMax)
				{
					float inMin = 10f;
					float outMax = 60f;
					float inOutPoint = 0.5f;
					if (completion >= inOutPoint)
					{
						float completionLerp = (float)Math.Pow(Utils.GetLerpValue(inOutPoint, 1f, completion, clamped: true), 5.0);
						base.OffsetLengthFromArm = MathHelper.Lerp(inMin, outMax, completionLerp);
					}
					else
					{
						float completionLerp2 = (float)Math.Pow(Utils.GetLerpValue(0f, inOutPoint, completion, clamped: true), 3.0);
						base.OffsetLengthFromArm = MathHelper.Lerp(offsetBase, inMin, completionLerp2);
					}
				}
				if (chargeTimer == (float)chargeAnimMax)
				{
					Shoot(isSkewer: true);
					fullyCharged = false;
					startChargeLoop = true;
					doingChargeAttack = false;
					chargeTimer = 0f;
					base.Projectile.ai[2] = 0f;
				}
				chargeTimer++;
			}
			else
			{
				shootingTimer = (int)((float)(-fireRate) * 0.5f);
				chargeTimer = 0f;
				startChargeLoop = true;
				base.Projectile.ai[2] = 0f;
			}
		}
		else if (!base.Owner.Calamity().mouseRight && !Main.mouseLeft && shootingTimer > 0f)
		{
			StopSounds();
			base.Projectile.Kill();
		}
		if (shootingTimer > 0f)
		{
			shootingTimer--;
		}
		time++;
		if (SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		if (SoundEngine.TryGetActiveSound(StartSlot, out ActiveSound StartSound) && StartSound.IsPlaying)
		{
			StartSound.Position = base.Projectile.Center;
		}
		charge = (float)(fullyCharged ? 1.0 : Math.Pow(chargeTimer / (float)chargeRate, 4.0));
	}

	public void Shoot(bool isSkewer)
	{
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style;
		if (isSkewer)
		{
			if (Main.mouseLeft)
			{
				style = new SoundStyle("CalamityMod/Sounds/Item/LauncherHeavyShot");
				style.Volume = 0.9f;
				style.Pitch = 0.4f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Owner.Center, base.Projectile.velocity * 7f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 26, 0.09f, ArsenalEffects.ArsenalLaserColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.1f));
				didDash = true;
			}
			StopSounds();
			base.Owner.Calamity().arsenalCooldown = 300;
			base.Owner.AddCooldown(ArsenalPower.ID, 300);
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int chargeDamage = base.Projectile.damage * (35 + (didDash ? 10 : 0));
				float chargeKB = base.Projectile.knockBack * 3f;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity * 2f, ModContent.ProjectileType<PhalanxSurgeLance>(), chargeDamage, chargeKB, base.Projectile.owner, 0f, 0f, didDash ? 5 : 0).timeLeft = (didDash ? 25 : 10);
			}
			style = new SoundStyle("CalamityMod/Sounds/Item/PhalanxSurgeChargeShoot");
			style.Volume = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Owner.SetScreenshake(6f);
			float mult = ((!didDash) ? 1 : 2);
			for (int i = 0; (float)i < 20f * mult; i++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, ArsenalEffects.ArsenalLaserDust, (shootVelocity * 20f).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.2f, 1.2f) * mult, 0, default(Color), Main.rand.NextFloat(0.5f, 1.3f) * mult);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalLaserColor;
				dust.alpha = 100;
			}
			shootingTimer = (didDash ? (-25) : (-10));
			return;
		}
		base.OffsetLengthFromArm -= 10f;
		style = new SoundStyle("CalamityMod/Sounds/Item/PhalanxSurgeShoot");
		style.Volume = 0.7f;
		style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 6f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			float angle1 = MathHelper.ToRadians(2f);
			int projType = ModContent.ProjectileType<PhalanxSurgeLaser>();
			for (int j = 0; j < 2; j++)
			{
				Vector2 corVel1 = shootVelocity2.RotatedBy(angle1 * 1.25f) * 0.8f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center - shootVelocity2 * 2f, corVel1, projType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 1f);
				Vector2 corVel2 = shootVelocity2.RotatedBy(angle1 * 0.75f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center - shootVelocity2 * 10f, corVel2, projType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0.65f);
				angle1 *= -1f;
			}
			float angle2 = MathHelper.ToRadians(5f);
			for (int k = 0; k < 2; k++)
			{
				Vector2 corVel3 = shootVelocity2.RotatedBy(angle2) * 0.9f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center - shootVelocity2 * 4f, corVel3, projType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0.8f);
				angle2 *= -1f;
			}
		}
		for (int l = 0; l < 9; l++)
		{
			Dust dust2 = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, ArsenalEffects.ArsenalLaserDust, (shootVelocity2 * 5f).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.5f, 0.8f));
			dust2.noGravity = true;
			dust2.color = ArsenalEffects.ArsenalLaserColor;
			dust2.alpha = 100;
		}
		shootingTimer = fireRate;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(StartSlot, out ActiveSound StartSound))
		{
			StartSound?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/PhalanxSurge", (AssetRequestMode)2).Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/PhalanxSurgeGlow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, -3f);
		float randSize = Main.rand.NextFloat(0.9f, 1f);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.direction == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.direction == -1);
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/ArchSmear", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Color val;
		if (charging)
		{
			float rumble = (fullyCharged ? 0f : charge);
			drawPosition += Main.rand.NextVector2Circular(5f * rumble, 5f * rumble);
			for (int i = 0; i < 8; i++)
			{
				float thin = 1f - (float)i * 0.15f;
				float fat = (1f + (float)i * 0.3f) * charge;
				Vector2 position = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity * 10f;
				val = ArsenalEffects.ArsenalLaserColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(rechargeTexture, position, null, val * 0.35f * charge, base.Projectile.rotation + (float)Math.PI / 2f, rechargeTexture.Size() * 0.5f, new Vector2(thin, fat) * 0.3f * randSize, (SpriteEffects)0);
				Vector2 position2 = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity * 10f;
				val = Color.White;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(rechargeTexture, position2, null, val * 0.25f * charge, base.Projectile.rotation + (float)Math.PI / 2f, rechargeTexture.Size() * 0.5f, new Vector2(thin, fat) * 0.2f * randSize, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		if (charging)
		{
			for (int j = 0; j < 4; j++)
			{
				val = Color.White;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.6f * charge;
				Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)j / 7f + Main.GlobalTimeWrappedHourly * 48f).ToRotationVector2() * charge;
				rotationalDrawOffset *= 3f;
				Main.EntitySpriteDraw(glowTexture, drawPosition + rotationalDrawOffset, null, auraColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
			}
		}
		Main.EntitySpriteDraw(glowTexture, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		return false;
	}
}
