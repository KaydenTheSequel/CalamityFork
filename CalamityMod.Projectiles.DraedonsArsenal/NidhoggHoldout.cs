using System;
using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
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

public class NidhoggHoldout : BaseGunHoldoutProjectile
{
	public float offsetBase = 30f;

	public SlotId SoundSlot;

	public int time;

	public float lightSine;

	private float charge;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override int AssociatedItemID => ModContent.ItemType<Nidhogg>();

	public override float MaxOffsetLengthFromArm => offsetBase;

	public override float RecoilResolveSpeed
	{
		get
		{
			if (!(shootingTimer < 0f))
			{
				return 0.1f;
			}
			return 0.02f;
		}
	}

	public override float OffsetXUpwards => 0f;

	public override float BaseOffsetY => 0f;

	public override float OffsetYDownwards => 0f;

	public override float WeaponTurnSpeed
	{
		get
		{
			if (!charging)
			{
				return 0.4f;
			}
			return 0.75f;
		}
	}

	public bool charging => base.Projectile.ai[2] == 5f;

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public ref float chargeTimer => ref base.Projectile.ai[1];

	public int fireRate => base.Owner.HeldItem.useAnimation;

	public int chargeRate => 222;

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
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound audio2) && audio2.IsPlaying)
		{
			audio2?.Stop();
		}
	}

	public override void HoldoutAI()
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
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
			if (charge > 0f)
			{
				charge = 1f - (float)Math.Pow(Utils.GetLerpValue(-50f, -20f, shootingTimer, clamped: true), 8.0);
			}
			shootingTimer++;
			if (charge == 0f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit2");
				style.Volume = 0.5f;
				style.Pitch = Main.rand.NextFloat(0.3f, 0.4f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				charge -= 0.001f;
			}
			if (shootingTimer == 0f)
			{
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
				Shoot(isBig: false);
			}
			else
			{
				Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalGaussColor)).ToVector3() * 2f * MathHelper.Lerp(Math.Abs(lightSine), 0.5f, 0.3f));
				if (chargeTimer == 0f)
				{
					SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/NidhoggCharge");
					SoundStyle style = sound with
					{
						Volume = 0.9f
					};
					SoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (chargeTimer == (float)chargeRate)
				{
					Shoot(isBig: true);
				}
				else
				{
					chargeTimer++;
				}
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
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		if (shootingTimer >= 0f)
		{
			charge = (float)Math.Pow(chargeTimer / (float)chargeRate, 3.0);
		}
	}

	public void Shoot(bool isBig)
	{
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		if (isBig)
		{
			Player owner = base.Owner;
			owner.velocity += -base.Projectile.velocity * 12f;
			base.OffsetLengthFromArm -= 15f;
			StopSounds();
			base.Owner.Calamity().arsenalCooldown = 540;
			base.Owner.AddCooldown(ArsenalPower.ID, 540);
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int chargeDamage = base.Projectile.damage * 33;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity * 6f, ModContent.ProjectileType<NidhoggRailgunBigShot>(), chargeDamage, 0f, base.Projectile.owner);
			}
			SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/NidhoggBigShot");
			for (int i = 0; i < 2; i++)
			{
				SoundEngine.PlaySound(sound with
				{
					Volume = 0.9f,
					MaxInstances = 2,
					Pitch = ((i == 0) ? (-0.4f) : 0f)
				}, base.Projectile.Center);
			}
			base.Owner.SetScreenshake(8f);
			for (int j = 0; j < 40; j++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, Main.rand.NextBool(3) ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalGaussDust, (shootVelocity * 25f).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.3f, 0.9f), 0, default(Color), Main.rand.NextFloat(1.2f, 1.9f));
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalGaussColor;
			}
			for (float i2 = 0.6f; i2 <= 1f; i2 += 0.4f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * (20f - 10f * i2), "CalamityMod/Particles/GlowSquareFading", affectedByGravity: false, 19, 0.9f * i2, ArsenalEffects.ArsenalGaussColor, new Vector2(1.2f, 0.7f), useAddativeBlend: true, glowCenter: false, (float)Math.PI, fadeIn: false, affectedByLight: false, -0.1f));
			}
			shootingTimer = -50f;
			chargeTimer = 0f;
			base.Projectile.ai[2] = 0f;
		}
		else
		{
			base.OffsetLengthFromArm -= 5f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/NidhoggFire");
			style.Volume = 0.6f;
			style.Pitch = Main.rand.NextFloat(0.5f, 0.65f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 6f;
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity2, ModContent.ProjectileType<NidhoggRailgunBlast>(), base.Projectile.damage, 0f, base.Projectile.owner);
			}
			for (int k = 0; k < 9; k++)
			{
				Dust dust2 = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, Main.rand.NextBool(3) ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalGaussDust, (shootVelocity2 * 4f).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.7f, 1.1f));
				dust2.noGravity = true;
				dust2.color = ArsenalEffects.ArsenalGaussColor;
			}
			shootingTimer = fireRate;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D topJaw = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/NidhoggTop", (AssetRequestMode)2).Value;
		Texture2D bottomJaw = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/NidhoggBottom", (AssetRequestMode)2).Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/NidhoggGlow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 jawVec = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy((float)Math.PI / 2f * (float)base.Projectile.direction);
		Main.rand.NextFloat(0.9f, 1f);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.direction == -1) ? ((float)Math.PI) : 0f);
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.direction == -1);
		Texture2D aimTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineThick", (AssetRequestMode)2).Value;
		Texture2D ringTex = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSquareFading", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Texture2D centerTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Color val;
		if (charging)
		{
			drawPosition += base.Projectile.velocity * Main.rand.NextFloat(-6f * charge, 6f * charge);
			for (int i = 1; i <= 5; i++)
			{
				float sine = (float)Math.Sin((charge * 60f + (float)time * 0.15f + MathHelper.ToRadians(216f) * (float)i) / (float)Math.PI);
				float angle = MathHelper.ToRadians(36f) * sine * Math.Max(1f - charge * 1.06f, 0f);
				val = ArsenalEffects.ArsenalGaussColor;
				((Color)(ref val)).A = 0;
				Color lineColor = val * Math.Abs(sine) * Math.Min(chargeTimer / ((float)chargeRate * 0.3f), 1f) * 0.2f;
				Main.EntitySpriteDraw(aimTex, base.Projectile.Center - Main.screenPosition + base.Projectile.velocity * 10f, null, lineColor, base.Projectile.rotation + angle - (float)Math.PI / 2f, new Vector2(aimTex.Size().X * 0.5f, 0f), new Vector2(0.2f - Math.Abs(sine) * 0.12f, 8f) * 0.07f, (SpriteEffects)2);
			}
			for (int j = 0; j < 3; j++)
			{
				Vector2 position = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f;
				val = Color.Lerp(ArsenalEffects.ArsenalGaussColor, Color.White, (float)j * 0.3f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(centerTex, position, null, val, drawRotation, centerTex.Size() * 0.5f, new Vector2(2.1f, 0.9f) * (0.04f + 0.1f * (float)Math.Pow(charge, 3.0) - 0.02f * (float)j), flipSprite);
			}
		}
		Vector2 topJawPlace = drawPosition - jawVec * (5f + 10f * (float)Math.Pow(charge, 3.0));
		Vector2 bottomJawPlace = drawPosition + jawVec * (4f + 10f * (float)Math.Pow(charge, 3.0)) + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 6f;
		Vector2 glowPlace = topJawPlace + jawVec;
		for (int k = 0; k < 14; k++)
		{
			val = ArsenalEffects.ArsenalGaussColor;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.25f * charge;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)k / 14f).ToRotationVector2() * 3f * charge;
			Main.EntitySpriteDraw(bottomJaw, bottomJawPlace + drawOffset, null, auraColor, drawRotation, bottomJaw.Size() * 0.5f, base.Projectile.scale, flipSprite);
			Main.EntitySpriteDraw(topJaw, topJawPlace + drawOffset, null, auraColor, drawRotation, topJaw.Size() * 0.5f, base.Projectile.scale, flipSprite);
		}
		Main.EntitySpriteDraw(bottomJaw, bottomJawPlace, null, drawColor, drawRotation, bottomJaw.Size() * 0.5f, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(topJaw, topJawPlace, null, drawColor, drawRotation, topJaw.Size() * 0.5f, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glowTexture, glowPlace, null, Color.White, drawRotation, glowTexture.Size() * 0.5f, base.Projectile.scale, flipSprite);
		if (charging)
		{
			for (int l = 1; l <= 3; l++)
			{
				lightSine = (float)Math.Sin((charge * 100f + (float)time * 0.15f + MathHelper.ToRadians(360f) * (float)l) / (float)Math.PI);
				MathHelper.ToRadians(36f);
				_ = lightSine;
				_ = charge;
				Vector2 posOffset = base.Projectile.velocity * 20f * lightSine;
				val = ArsenalEffects.ArsenalGaussColor;
				((Color)(ref val)).A = 0;
				Color lineColor2 = val * Math.Min(chargeTimer / ((float)chargeRate * 0.3f), 1f) * 0.7f;
				Main.EntitySpriteDraw(ringTex, base.Projectile.Center - Main.screenPosition + posOffset, null, lineColor2, base.Projectile.rotation + (float)Math.PI / 2f, ringTex.Size() * 0.5f, new Vector2(1f + 0.5f * (float)Math.Pow(charge, 3.0), 1f) * (0.12f + (float)l * 0.025f), (SpriteEffects)2);
			}
		}
		return false;
	}
}
