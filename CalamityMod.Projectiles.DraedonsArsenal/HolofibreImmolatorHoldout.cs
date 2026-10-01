using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class HolofibreImmolatorHoldout : BaseGunHoldoutProjectile
{
	public static float BulletSpeed = 12f;

	public SlotId ChargeSlot;

	public int time;

	public int chargeMax;

	public int downtime;

	public static int spamDelayMax = 8;

	public int spamDelay = spamDelayMax;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override int AssociatedItemID => ModContent.ItemType<HolofibreImmolator>();

	public override float MaxOffsetLengthFromArm => 15f;

	public override float OffsetXUpwards => -0f;

	public override float BaseOffsetY => -0f;

	public override float OffsetYDownwards => 0f;

	public override float WeaponTurnSpeed => 0.45f;

	public ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	public ref float ShotsLoaded => ref base.Projectile.ai[1];

	public bool ChargeLV1 => CurrentChargingFrames >= (float)chargeMax;

	public override void KillHoldoutLogic()
	{
		if (base.HeldItem.type != base.Owner.HeldItem.type)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.OffsetLengthFromArm = 15f;
		Player Owner = Main.player[base.Projectile.owner];
		chargeMax = (int)((float)Owner.itemAnimationMax * 1.5f);
	}

	public override void HoldoutAI()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentChargingFrames == 0f)
		{
			SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/ImmolatorCharge");
			SoundStyle style = sound with
			{
				Volume = 0.7f,
				IsLooped = true
			};
			ChargeSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		base.Owner.HeldItem.Calamity();
		Vector2 mountedCenter = base.Owner.MountedCenter;
		_ = base.Owner.Calamity().mouseWorld - mountedCenter;
		if (SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		if (ChargeLV1 && spamDelay > 0)
		{
			spamDelay--;
		}
		bool autoSpam = base.Owner.Calamity().mouseRight && !Main.mouseLeft;
		bool chargeSpam = base.Owner.Calamity().mouseRight && Main.mouseLeft && ChargeLV1 && spamDelay == 0;
		if ((!Main.mouseLeft | autoSpam | chargeSpam) && downtime == 0 && time >= 1)
		{
			base.KeepRefreshingLifetime = false;
			base.Projectile.timeLeft = (base.Owner.Calamity().mouseRight ? (base.Owner.itemAnimationMax * 2) : base.Owner.itemAnimationMax);
			downtime = base.Owner.itemAnimationMax;
			if (base.Owner.Calamity().mouseRight && !Main.mouseLeft)
			{
				CurrentChargingFrames = 0f;
			}
			spamDelay = spamDelayMax;
			if (ChargeLV1)
			{
				ChargeSound?.Stop();
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ImmolatorFire");
				style.Volume = 0.7f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
				if (Main.myPlayer == base.Projectile.owner)
				{
					int charge2Damage = (int)((float)base.Projectile.damage * 4.5f);
					float charge2KB = base.Projectile.knockBack * 3f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity * 2f, ModContent.ProjectileType<ImmolationArrow>(), charge2Damage, charge2KB, base.Projectile.owner);
				}
				for (int i = 0; i <= 25; i++)
				{
					Dust dust = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, ArsenalEffects.ArsenalPlasmaDust, shootVelocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.2f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.5f, 1.3f));
					dust.noGravity = true;
					dust.color = ArsenalEffects.ArsenalPlasmaColor;
				}
				CurrentChargingFrames = 0f;
			}
			else
			{
				ChargeSound?.Stop();
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ImmolatorFire");
				style.Volume = 0.7f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
				if (Main.myPlayer == base.Projectile.owner)
				{
					for (int j = 0; j < 3; j++)
					{
						Vector2 fireVec = shootVelocity2.RotatedBy(j switch
						{
							2 => -0.4f, 
							0 => 0.4f, 
							_ => 0f, 
						} * Utils.GetLerpValue((float)chargeMax * 1.1f, 0f, CurrentChargingFrames, clamped: true));
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, fireVec * MathHelper.Clamp(2f * Utils.GetLerpValue(0f, chargeMax, CurrentChargingFrames, clamped: true), 0.5f, 2f), ModContent.ProjectileType<ImmolationSpray>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					}
				}
				for (int k = 0; k <= 4; k++)
				{
					Dust dust2 = Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 15f, ArsenalEffects.ArsenalPlasmaDust, shootVelocity2.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.9f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.8f, 1.3f));
					dust2.noGravity = false;
					dust2.color = ArsenalEffects.ArsenalPlasmaColor;
					dust2.fadeIn = 2f;
				}
				CurrentChargingFrames = 0f;
			}
		}
		else if (downtime == 0)
		{
			CurrentChargingFrames++;
			base.Projectile.timeLeft = base.Owner.itemAnimationMax;
			if (CurrentChargingFrames >= 10f)
			{
				float strength = Utils.GetLerpValue(0f, chargeMax, CurrentChargingFrames, clamped: true);
				Vector3 DustLight = ((Color)(ref ArsenalEffects.ArsenalPlasmaColor)).ToVector3();
				Lighting.AddLight(GunTipPosition, DustLight * strength);
			}
			if (CurrentChargingFrames == (float)chargeMax)
			{
				ChargeSound?.Stop();
				for (int l = 0; l < 20; l++)
				{
					Dust dust3 = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>());
					dust3.velocity = ((float)Math.PI * 2f * (float)l / 20f).ToRotationVector2() * 4f + base.Owner.velocity * 0.5f;
					dust3.scale = Main.rand.NextFloat(0.6f, 0.8f);
					dust3.noGravity = false;
					dust3.color = ArsenalEffects.ArsenalPlasmaColor;
				}
				SoundStyle sound2 = new SoundStyle("CalamityMod/Sounds/Item/ImmolatorChargeLoop");
				ChargeSlot = SoundEngine.PlaySound(sound2 with
				{
					Volume = 0.7f,
					IsLooped = true
				}, base.Projectile.Center);
			}
			if (CurrentChargingFrames > (float)chargeMax && Main.rand.NextBool())
			{
				Dust dust4 = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<SquashDust>());
				dust4.velocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(5f, 9f);
				dust4.scale = Main.rand.NextFloat(0.7f, 0.85f);
				dust4.noGravity = true;
				dust4.color = ArsenalEffects.ArsenalPlasmaColor;
				dust4.fadeIn = 0.5f;
			}
		}
		if ((downtime == 1 && !base.Owner.Calamity().mouseRight) || base.Owner.dead)
		{
			ChargeSound?.Stop();
			base.Projectile.Kill();
		}
		if (downtime > 0)
		{
			downtime--;
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 10f;
		float fade = Utils.GetLerpValue(chargeMax / 3, chargeMax, CurrentChargingFrames, clamped: true);
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/HolofibreImmolator", (AssetRequestMode)2).Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/HolofibreImmolatorGlow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = value.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D pointTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glowTexture, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		if (CurrentChargingFrames > 0f && downtime == 0)
		{
			float randSize = Main.rand.NextFloat(0.8f, 1.2f);
			Vector2 position = tipPosition - Main.screenPosition;
			Color val = ArsenalEffects.ArsenalPlasmaColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.25f * Utils.GetLerpValue(0f, chargeMax, CurrentChargingFrames, clamped: true) * randSize, (SpriteEffects)0);
			Vector2 position2 = tipPosition - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position2, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.15f * Utils.GetLerpValue(0f, chargeMax, CurrentChargingFrames, clamped: true) * randSize, (SpriteEffects)0);
			for (int i = 0; i < 3; i++)
			{
				Vector2 position3 = tipPosition - Main.screenPosition + base.Projectile.velocity * 15f * fade;
				val = Color.Lerp(Color.White, ArsenalEffects.ArsenalPlasmaColor, (float)i * 0.5f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(pointTexture, position3, null, val * fade * 0.7f, base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(90f)).ToRotation(), pointTexture.Size() * 0.5f, new Vector2(0.7f - 0.2f * (float)i, 0.7f + 0.3f * (float)i) * 0.035f * fade * randSize * ((CurrentChargingFrames == (float)chargeMax) ? 1.5f : 1f) * (0.6f + 0.2f * (float)i), flipSprite);
			}
		}
		return false;
	}
}
