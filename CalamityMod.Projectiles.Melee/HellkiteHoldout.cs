using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class HellkiteHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int storedUseAnim;

	public int swingCount;

	public int pierceReduction;

	public bool chargedSwing;

	public int chargeTimer;

	public int chargeTimerMax = 240;

	public bool playSwingSound = true;

	public SlotId AudSlot;

	public override int AssignedItemID => ModContent.ItemType<Hellkite>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Hellkite>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Hellkite";

	public override float HitboxOutset => 100f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(185f, 185f) * base.Projectile.scale;
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-3f, 124f);
		}
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void WhenSpawned()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		CanHit = false;
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1f;
		base.Projectile.ai[1] = -1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnim = Owner.itemAnimationMax;
		storedUseAnim = useAnim;
		chargeTimerMax = useAnim * 5;
		if (mousePos.X < Owner.Center.X)
		{
			Owner.direction = -1;
		}
		else
		{
			Owner.direction = 1;
		}
		FlipAsSword = Owner.direction == -1;
	}

	public override void UseStyle()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1315: Unknown result type (might be due to invalid IL or missing references)
		//IL_132b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_1332: Unknown result type (might be due to invalid IL or missing references)
		//IL_1337: Unknown result type (might be due to invalid IL or missing references)
		//IL_133f: Unknown result type (might be due to invalid IL or missing references)
		//IL_134e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1367: Unknown result type (might be due to invalid IL or missing references)
		//IL_136d: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_137c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1381: Unknown result type (might be due to invalid IL or missing references)
		//IL_138b: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1097: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_1107: Unknown result type (might be due to invalid IL or missing references)
		//IL_1120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_115c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1166: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_120d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1226: Unknown result type (might be due to invalid IL or missing references)
		//IL_122c: Unknown result type (might be due to invalid IL or missing references)
		//IL_122d: Unknown result type (might be due to invalid IL or missing references)
		//IL_123b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1240: Unknown result type (might be due to invalid IL or missing references)
		//IL_124a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1260: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e84: Unknown result type (might be due to invalid IL or missing references)
		AnimationProgress = Animation % (float)(chargedSwing ? ((int)((float)storedUseAnim * 0.7f)) : storedUseAnim);
		if (Owner.Calamity().mouseRight)
		{
			base.Projectile.ai[2] = 5f;
		}
		DrawUnconditionally = false;
		bool num = Owner == null || !Owner.active || Owner.dead || (base.Projectile.ai[2] == 0f && !Owner.channel) || (base.Projectile.ai[2] == 5f && !Owner.Calamity().mouseRight) || Owner.CCed || Owner.noItems;
		if (CanHit || postSwing)
		{
			mousePos = Owner.Center - aimVel;
		}
		else
		{
			mousePos = Owner.Calamity().mouseWorld;
		}
		if (CanHit)
		{
			fadeIn = MathHelper.Lerp(fadeIn, 1f, (chargedSwing ? 1.5f : 1f) * 0.23f * Owner.GetAttackSpeed<MeleeDamageClass>());
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.3f);
		}
		if (chargeTimer > 0)
		{
			fadeIn = Utils.Remap(chargeTimer, 0f, chargeTimerMax, 0f, 1f);
		}
		if (num)
		{
			chargeTimer = 0;
			if (base.Projectile.ai[2] == 5f)
			{
				Owner.itemAnimation = Owner.itemAnimationMax;
				base.Projectile.timeLeft = Owner.itemAnimation;
			}
			base.Projectile.ai[2] = 0f;
		}
		if (!doSwing)
		{
			playSwingSound = true;
			mousePos = Owner.Calamity().mouseWorld;
			aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			CanHit = false;
			if (mousePos.X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			FlipAsSword = Owner.direction == -1;
			Vector2 bladePos = default(Vector2);
			((Vector2)(ref bladePos))._002Ector(60f, 0f);
			Vector2 particlePos = Owner.Center + bladePos.RotatedBy(base.FinalRotation + MathHelper.ToRadians(-45f));
			if (base.Projectile.ai[2] == 5f)
			{
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction), 0.05f);
				float rotationValue = 45f + 25f * Utils.GetLerpValue(0f, chargeTimerMax, chargeTimer, clamped: true) * (float)(FlipAsSword ? 1 : (-1)) * (0f - base.Projectile.ai[1]);
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(rotationValue), 0.3f);
				Animation = 0f;
				Owner.itemAnimation++;
				base.Projectile.timeLeft++;
				if (chargeTimer < chargeTimerMax && !chargedSwing)
				{
					chargeTimer++;
				}
				Vector2 particleVel = (Owner.Center - particlePos).SafeNormalize(Vector2.UnitX) * -15f;
				particlePos += Main.rand.NextVector2Circular(20f, 20f);
				Dust dust = Dust.NewDustPerfect(particlePos, 267, particleVel * Main.rand.NextFloat(0.2f, 1f));
				dust.scale = Main.rand.NextFloat(0.65f, 1.15f) * fadeIn;
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(particlePos, particleVel * Main.rand.NextFloat(0.2f, 1.5f), affectedByGravity: false, 22, Main.rand.NextFloat(0.2f, 0.4f) * fadeIn, Main.rand.NextBool(3) ? Color.Red : Color.OrangeRed));
				if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
				{
					ChargeSound.Position = base.Projectile.Center;
					ChargeSound.Pitch = Utils.Remap(chargeTimer, 0f, chargeTimerMax, -0.4f, 0f);
					ChargeSound.Volume = Utils.Remap(chargeTimer, 0f, chargeTimerMax, 0f, 0.5f) * 100f;
				}
				else if (!chargedSwing)
				{
					SoundStyle style = Hellkite.ChargeSound with
					{
						Volume = 0.01f,
						Pitch = 0f,
						IsLooped = true
					};
					AudSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if (chargeTimer == chargeTimerMax)
			{
				particlePos = Owner.Center + bladePos.RotatedBy(base.FinalRotation + MathHelper.ToRadians(-45f));
				SoundStyle style = Hellkite.FullChargeSound with
				{
					Volume = 0.9f,
					PitchVariance = 0.2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				chargedSwing = true;
				useAnim = storedUseAnim / 3;
				chargeTimer++;
				for (int i = 0; i < 20; i++)
				{
					GeneralParticleHandler.SpawnParticle(new LineParticle(particlePos, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.5f, 1f), affectedByGravity: false, 30, Main.rand.NextFloat(0.3f, 0.8f), Main.rand.NextBool(3) ? Color.Red : Color.OrangeRed));
					Dust dust2 = Dust.NewDustPerfect(particlePos, 267, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.5f, 1f));
					dust2.scale = Main.rand.NextFloat(0.65f, 1.15f) * fadeIn;
					dust2.noGravity = true;
					dust2.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
				}
			}
			if (chargeTimer == 0)
			{
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					base.Projectile.localNPCImmunity[j] = 0;
				}
				base.Projectile.numHits = 0;
				pierceReduction = 0;
				doSwing = true;
			}
		}
		else if (chargeTimer == 0)
		{
			if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound2))
			{
				ChargeSound2?.Stop();
			}
			if (!CanHit && !postSwing)
			{
				if (mousePos.X < Owner.Center.X)
				{
					Owner.direction = -1;
				}
				else
				{
					Owner.direction = 1;
				}
			}
			else if ((Owner.Center - aimVel).X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(45f), 0.1f);
			if (AnimationProgress < (float)useAnim / 1.5f)
			{
				if (base.Projectile.ai[2] == 5f && !chargedSwing)
				{
					doSwing = false;
				}
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					base.Projectile.scale = 1f;
					Animation = 0f;
					doSwing = false;
					chargeTimer = 0;
					chargedSwing = false;
					useAnim = storedUseAnim;
					base.Projectile.ai[1] = 0f - base.Projectile.ai[1];
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction * (1f + Utils.GetLerpValue((float)useAnim * 0.35f, (float)useAnim * 0.6f, Animation, clamped: true) * 0.5f)), 0.2f);
				FlipAsSword = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX).X > 0f;
			}
			else
			{
				float time = AnimationProgress - (float)(useAnim / 3);
				float timeMax = useAnim - useAnim / 3;
				if (time >= (float)(int)(timeMax * (chargedSwing ? 0.2f : 0.4f)) && playSwingSound)
				{
					if (!chargedSwing)
					{
						SoundStyle style = Hellkite.SwingSound with
						{
							Volume = 0.8f,
							PitchVariance = 0.25f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					else
					{
						SoundStyle style = Hellkite.SwingSound with
						{
							Volume = 0.9f,
							Pitch = 0.2f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = Hellkite.SwingSoundBig with
						{
							Volume = 1f,
							Pitch = 0f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					swingCount++;
					playSwingSound = false;
				}
				if (time > (float)(int)(timeMax * (chargedSwing ? 0.2f : 0.4f)) && time < (float)(int)(timeMax * (chargedSwing ? 0.9f : 0.7f)))
				{
					CanHit = true;
					Vector2 particleVel2 = Utils.RotatedBy(new Vector2(0f, 10f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
					Vector2 particlePos2 = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, 170), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
					if (chargedSwing)
					{
						for (int k = 0; k < 3; k++)
						{
							particleVel2 = (new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction) * Main.rand.NextFloat(0.3f, 1f)).RotatedBy(base.FinalRotation + MathHelper.ToRadians(-45f));
							particlePos2 = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, 170), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
							GeneralParticleHandler.SpawnParticle(new AltSparkParticle(particlePos2, -particleVel2.RotatedByRandom(0.4000000059604645), affectedByGravity: false, 24, Main.rand.NextFloat(0.3f, 0.7f), Main.rand.NextBool(3) ? Color.OrangeRed : Color.DarkRed));
							GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(particlePos2, -particleVel2.RotatedByRandom(0.4000000059604645), Main.rand.NextBool(4) ? Color.Red : Color.DarkRed, 23, Main.rand.NextFloat(0.5f, 1f), 0.65f));
							GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(particlePos2, -particleVel2.RotatedByRandom(0.4000000059604645) * 2f, Main.rand.NextBool(4) ? Color.Crimson : Color.Red, 23, Main.rand.NextFloat(0.5f, 1f), 0.65f, 0f, glowing: true));
						}
					}
					else
					{
						GeneralParticleHandler.SpawnParticle(new AltSparkParticle(particlePos2, -particleVel2.RotatedByRandom(0.20000000298023224), affectedByGravity: false, 24, Main.rand.NextFloat(0.3f, 0.7f), Main.rand.NextBool(3) ? Color.OrangeRed : Color.DarkRed));
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(particlePos2, -particleVel2.RotatedByRandom(0.20000000298023224) * 2f, Main.rand.NextBool(4) ? Color.Red : Color.DarkRed, 23, Main.rand.NextFloat(0.5f, 1f), 0.65f));
					}
				}
				else
				{
					CanHit = false;
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(150f * base.Projectile.ai[1] * (float)Owner.direction, 120f * (0f - base.Projectile.ai[1]) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
				if (time < (float)(int)(timeMax * 0.9f))
				{
					postSwing = true;
				}
				if (CanHit)
				{
					if (chargedSwing)
					{
						for (int l = 0; l < 6; l++)
						{
							float randRot = Main.rand.NextFloat(-10f, -45f);
							Vector2 dustVel = Utils.RotatedBy(new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2));
							GeneralParticleHandler.SpawnParticle(new PointParticle(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2)).RotatedByRandom(0.4000000059604645), -dustVel * Main.rand.NextFloat(0.4f, 0.7f), affectedByGravity: false, Main.rand.Next(15, 19), Main.rand.NextFloat(0.7f, 1.2f), (Main.rand.NextBool(4) ? Color.Orange : Color.OrangeRed) * 0.8f));
						}
						for (int m = 0; m < 4; m++)
						{
							float randRot2 = Main.rand.NextFloat(-30f, -60f);
							Vector2 dustVel2 = Utils.RotatedBy(new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot2)), default(Vector2));
							Dust dust3 = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), 278, dustVel2 * Main.rand.NextFloat(0.3f, 0.9f));
							dust3.scale = Main.rand.NextFloat(0.65f, 0.95f);
							dust3.noGravity = true;
							dust3.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
						}
					}
					else
					{
						for (int n = 0; n < 3; n++)
						{
							float randRot3 = Main.rand.NextFloat(-30f, -60f);
							Vector2 dustVel3 = Utils.RotatedBy(new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot3)), default(Vector2));
							Dust dust4 = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), 278, dustVel3 * Main.rand.NextFloat(0.1f, 0.5f));
							dust4.scale = Main.rand.NextFloat(0.55f, 0.85f);
							dust4.noGravity = true;
							dust4.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
						}
					}
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && pierceReduction > 0)
		{
			pierceReduction--;
		}
		if (!chargedSwing)
		{
			if (base.Projectile.numHits == 0)
			{
				SoundStyle style = Hellkite.HitSoundSmall with
				{
					Volume = 0.85f,
					PitchVariance = 0.25f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Owner.SetScreenshake(4.5f);
			}
			for (int i = 0; i < MathHelper.Clamp(8 - base.Projectile.numHits * 2, 2, 8); i++)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -20f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: false, 40, Main.rand.NextFloat(0.3f, 1f), Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed));
				if (Main.rand.NextBool())
				{
					GeneralParticleHandler.SpawnParticle(new AltLineParticle(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -20f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: false, 40, Main.rand.NextFloat(0.3f, 1f), Color.DarkRed));
				}
			}
		}
		else if (base.Projectile.numHits == 0)
		{
			SoundStyle style = Hellkite.HitSoundBig with
			{
				Volume = 1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Owner.SetScreenshake(8.5f);
			if (!CalamityClientConfig.Instance.Photosensitivity)
			{
				for (int j = 0; j < 3; j++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 2f * (float)(j + 1), 1f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1f * (float)(j + 1), 0.5f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
			for (int k = 0; k < 2; k++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center, (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -25f * (float)((k != 0) ? 1 : (-1)), affectedByGravity: false, 12, 0.08f, Color.OrangeRed, new Vector2(3f, 0.8f), quickShrink: true));
			}
			for (int l = 0; l < 15; l++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -40f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: true, 40, Main.rand.NextFloat(0.5f, 1.2f), Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed));
				if (Main.rand.NextBool())
				{
					GeneralParticleHandler.SpawnParticle(new AltSparkParticle(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -40f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: true, 40, Main.rand.NextFloat(0.5f, 1.2f), Color.DarkRed));
				}
				Dust dust = Dust.NewDustPerfect(target.Center, 278, Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.2f, 1f));
				dust.scale = Main.rand.NextFloat(0.55f, 0.85f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
			}
		}
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, chargedSwing ? 24 : 19, ignoreKBImmune: true);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (chargedSwing)
		{
			modifiers.SetCrit();
		}
		float critDamage = Math.Min(Owner.GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		float minMult = 0.25f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(pierceReduction, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= (chargedSwing ? (2.9f + critDamage) : 1f) * damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		if ((useAnim > 0 || DrawUnconditionally) && (Owner.ItemAnimationActive || Owner.Calamity().mouseRight))
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			Asset<Texture2D> glowTex = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/HellkiteGlow", (AssetRequestMode)2);
			ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2);
			Asset<Texture2D> swoosh = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearRagged", (AssetRequestMode)2);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			SpriteEffects sEffects = (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects));
			Color val;
			for (int i = 0; i < 25; i++)
			{
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/HellkiteGhost", (AssetRequestMode)2).Value;
				val = Color.OrangeRed;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.15f * fadeIn;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * (float)((chargeTimer > 0) ? 4 : 7) * fadeIn;
				Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY), centerTexture.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			if (swingCount > 0 && base.Projectile.ai[2] != 5f && !playSwingSound)
			{
				Texture2D value = swoosh.Value;
				Vector2 position = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
				val = Color.Lerp(Color.DarkRed, Color.OrangeRed, 0.25f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value, position, null, val * fadeIn * 0.9f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 == 0) ? (-80) : 80)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, base.Projectile.scale * 2.35f / 4f, (SpriteEffects)(swingCount % 2 == 0));
			}
			Main.EntitySpriteDraw(tex.Value, generalDrawPos, tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, sEffects);
			if (chargeTimer > 0)
			{
				for (int j = 0; j < 5; j++)
				{
					val = Color.Lerp(Color.DarkRed, Color.Red, Utils.GetLerpValue(0f, 5f, j)) * 0.4f * fadeIn;
					((Color)(ref val)).A = 0;
					Color auraColor2 = val;
					Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)j / 7f + Main.GlobalTimeWrappedHourly * 17f).ToRotationVector2();
					rotationalDrawOffset *= MathHelper.Lerp(3f, 5.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 13f) * 0.5f);
					Main.EntitySpriteDraw(glowTex.Value, base.Projectile.Center - Main.screenPosition + rotationalDrawOffset + new Vector2(0f, Owner.gfxOffY), glowTex.Value.Frame(1, FrameCount, 0, Frame), auraColor2, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, sEffects);
				}
			}
			Main.EntitySpriteDraw(glowTex.Value, generalDrawPos, glowTex.Frame(1, FrameCount, 0, Frame), (chargeTimer > 0) ? Color.Lerp(Color.White, Color.Gold, fadeIn) : Color.White, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)glowTex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, sEffects);
		}
		return false;
	}

	public override void ResetStyle()
	{
	}
}
