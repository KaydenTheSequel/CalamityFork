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

public class OpalStrikerHoldout : BaseGunHoldoutProjectile
{
	public SlotId OpalChargeSlot;

	public static float ChargedDamageMult = 5f;

	public static float ChargedKBMult = 3f;

	public static float BulletSpeed = 12f;

	public int time;

	public override int AssociatedItemID => ModContent.ItemType<OpalStriker>();

	public override float MaxOffsetLengthFromArm => 25f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private bool FullyCharged => CurrentChargingFrames >= (float)OpalStriker.FullChargeFrames;

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
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(OpalChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		Color newColor;
		if (base.Owner.CantUseHoldout())
		{
			base.KeepRefreshingLifetime = false;
			if (base.Projectile.ai[1] != 1f)
			{
				base.Projectile.timeLeft = OpalStriker.AftershotCooldownFrames;
				ChargeSound?.Stop();
				SoundEngine.PlaySound(FullyCharged ? OpalStriker.ChargedFire : OpalStriker.Fire, base.Projectile.Center);
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
				if (FullyCharged)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity, ModContent.ProjectileType<OpalChargedStrike>(), (int)((float)base.Projectile.damage * ChargedDamageMult), base.Projectile.knockBack * ChargedKBMult, base.Projectile.owner);
					for (int i = 0; i <= 35; i++)
					{
						Vector2 gunTipPosition = GunTipPosition;
						int type = ModContent.DustType<SquashDust>();
						Vector2? velocity = shootVelocity.RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(1.6f, 2.9f);
						newColor = default(Color);
						Dust dust = Dust.NewDustPerfect(gunTipPosition, type, velocity, 0, newColor, Main.rand.NextFloat(1.6f, 2.5f));
						dust.noGravity = true;
						dust.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
						dust.fadeIn = 2.5f;
						if (Main.rand.NextBool(4))
						{
							dust.scale = Main.rand.NextFloat(0.8f, 0.95f);
							dust.fadeIn = -0.85f;
							dust.velocity /= 2f;
						}
					}
					base.OffsetLengthFromArm -= 25f;
					base.Owner.SetScreenshake(5f);
					for (int j = 0; j < 2; j++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(GunTipPosition, base.Projectile.velocity * 18f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 18, 0.65f, Color.OrangeRed, new Vector2(1.2f, 0.8f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.6f, 1f, 0.9f));
					}
				}
				else
				{
					for (int k = 0; k <= 5; k++)
					{
						Vector2 gunTipPosition2 = GunTipPosition;
						int type2 = ModContent.DustType<SquashDust>();
						Vector2? velocity2 = shootVelocity.RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.6f, 1.9f);
						newColor = default(Color);
						Dust dust2 = Dust.NewDustPerfect(gunTipPosition2, type2, velocity2, 0, newColor, Main.rand.NextFloat(1.2f, 2f));
						dust2.noGravity = true;
						dust2.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
						dust2.fadeIn = 1.5f;
					}
					base.OffsetLengthFromArm -= 5f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity, ModContent.ProjectileType<OpalStrike>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				base.Projectile.ai[1] = 1f;
			}
		}
		else
		{
			CurrentChargingFrames++;
			if (FullyCharged)
			{
				if ((CurrentChargingFrames - (float)OpalStriker.FullChargeFrames) % (float)OpalStriker.ChargeLoopSoundFrames == 0f)
				{
					OpalChargeSlot = SoundEngine.PlaySound(in OpalStriker.ChargeLoop, base.Projectile.Center);
				}
			}
			else if (CurrentChargingFrames == 10f)
			{
				OpalChargeSlot = SoundEngine.PlaySound(in OpalStriker.Charge, base.Projectile.Center);
			}
			if (CurrentChargingFrames >= 10f)
			{
				float orbScale = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)OpalStriker.FullChargeFrames) / 200f;
				Vector2 gunTipPosition3 = GunTipPosition;
				newColor = Color.Orange;
				Lighting.AddLight(gunTipPosition3, ((Color)(ref newColor)).ToVector3() * orbScale);
			}
			if (CurrentChargingFrames == (float)OpalStriker.FullChargeFrames)
			{
				SoundStyle style = OpalStriker.Fire with
				{
					Pitch = 0.6f,
					Volume = 0.7f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int l = 0; l < 36; l++)
				{
					Vector2 gunTipPosition4 = GunTipPosition;
					int type3 = ModContent.DustType<SquashDust>();
					newColor = default(Color);
					Dust dust3 = Dust.NewDustPerfect(gunTipPosition4, type3, null, 0, newColor);
					dust3.velocity = ((float)Math.PI * 2f * (float)l / 36f).ToRotationVector2() * Main.rand.NextFloat(7f, 8.5f);
					dust3.scale = Main.rand.NextFloat(1f, 1.5f);
					dust3.noGravity = true;
					dust3.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
					dust3.fadeIn = 1f;
				}
			}
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(OpalChargeSlot, out ActiveSound ChargeSound))
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
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		float chargeScale = (base.KeepRefreshingLifetime ? Utils.GetLerpValue(0f, OpalStriker.FullChargeFrames, CurrentChargingFrames, clamped: true) : 0f);
		if (!base.Owner.CantUseHoldout())
		{
			float rumble = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)OpalStriker.FullChargeFrames);
			drawPosition += Main.rand.NextVector2Circular(rumble / 30f, rumble / 30f);
		}
		float sine = (float)Math.Sin((float)time * 0.25f / (float)Math.PI);
		Color val;
		for (int i = 0; i < 10; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * (3f + sine * 0.3f);
			Vector2 position = drawPosition + drawOffset * chargeScale;
			val = Color.OrangeRed;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(texture, position, null, val * Math.Min(chargeScale, 1f) * 0.4f, drawRotation + ((base.Owner.gravDir == -1f) ? ((float)Math.PI) : 0f), rotationPoint, base.Projectile.scale, flipSprite);
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		SpriteEffects flipSpriteGlow = (SpriteEffects)0;
		if (base.Owner.gravDir == -1f)
		{
			if (base.Projectile.spriteDirection == -1)
			{
				flipSpriteGlow = (SpriteEffects)2;
			}
		}
		else
		{
			rotationPoint.Y = (float)texture.Height - rotationPoint.Y;
			if (base.Projectile.spriteDirection == 1)
			{
				flipSpriteGlow = (SpriteEffects)2;
			}
		}
		for (int j = 0; j < 3; j++)
		{
			Texture2D value = tex2.Value;
			Vector2 position2 = GunTipPosition - Main.screenPosition;
			val = Color.Lerp(FullyCharged ? Color.OrangeRed : Color.Orange, Color.White, (float)j * 0.25f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position2, null, val * 0.8f, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1.35f, 1f) * base.Projectile.scale * chargeScale * (1f - 0.27f * (float)j) * 0.3f * ((chargeScale >= 1f && CurrentChargingFrames <= (float)(OpalStriker.FullChargeFrames + 3)) ? 1.75f : (FullyCharged ? 1.4f : 1f)), flipSpriteGlow);
		}
		return false;
	}
}
