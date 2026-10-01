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

internal class VitriolicViperHoldout : BaseGunHoldoutProjectile
{
	public int FirstChargeFrames;

	public int FullyChargedFrames;

	public SlotId ChargeSlot;

	public static float BulletSpeed = 15f;

	public int time;

	public float chargePower;

	public float overchargePower;

	public Color bColor;

	public float vortexRotation;

	public override int AssociatedItemID => ModContent.ItemType<VitriolicViper>();

	public override float MaxOffsetLengthFromArm => 40f;

	public override float BaseOffsetY => 0f;

	public override float RecoilResolveSpeed => 0.4f;

	public override string Texture => "CalamityMod/Items/Weapons/Magic/VitriolicViper";

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
			return base.GunTipPosition - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * -20f;
		}
	}

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private ref float CurrentOverchargeFrames => ref base.Projectile.ai[2];

	private bool FirstCharge => CurrentChargingFrames >= (float)FirstChargeFrames;

	private bool FullCharge => CurrentOverchargeFrames >= (float)FullyChargedFrames;

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
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(ChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		chargePower = Utils.GetLerpValue(0f, FirstChargeFrames, CurrentChargingFrames, clamped: true) * (FullCharge ? 1.4f : 1f);
		if (CurrentOverchargeFrames < (float)FullyChargedFrames)
		{
			overchargePower = MathHelper.Clamp(Utils.GetLerpValue(FullyChargedFrames * 2, 0f, CurrentOverchargeFrames, clamped: true), 0.5f, 1f);
		}
		else
		{
			overchargePower = MathHelper.Lerp(overchargePower, 1f, 0.09f);
		}
		vortexRotation += 0.4f * chargePower;
		if (base.Owner.CantUseHoldout())
		{
			base.KeepRefreshingLifetime = false;
			if (base.Projectile.ai[1] != 1f)
			{
				base.Projectile.timeLeft = 30;
				ChargeSound?.Stop();
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
				if (FullCharge)
				{
					base.OffsetLengthFromArm -= 25f;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ViperSpit");
					style.Pitch = -0.3f + Main.rand.NextFloat(-0.1f, 0.1f) + (FirstCharge ? 0.2f : 0f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					style = new SoundStyle("CalamityMod/Sounds/Item/MeldShoot");
					style.Pitch = -0.3f;
					style.Volume = 0.5f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					base.Owner.SetScreenshake(4f);
					if (Main.myPlayer == base.Projectile.owner)
					{
						for (int i = 0; i < 33; i++)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, (shootVelocity * 2f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.8f, 1.2f), ModContent.ProjectileType<VitriolicViperFang>(), base.Projectile.damage / 10, base.Projectile.knockBack, base.Projectile.owner);
						}
					}
					GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, Vector2.Zero, bColor, "CalamityMod/Particles/HighResFoggyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.05f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					for (int j = 0; j < 17; j++)
					{
						Dust dust = Dust.NewDustPerfect(GunTipPosition, 278);
						dust.velocity = base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(5f, 25f);
						dust.scale = Main.rand.NextFloat(0.45f, 0.75f);
						dust.noGravity = true;
						dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Green : bColor, 0.7f);
					}
					base.Projectile.velocity.SafeNormalize(Vector2.Zero);
					for (int k = 0; k <= 18; k++)
					{
						Vector2 spinninpoint = shootVelocity / 2f;
						GeneralParticleHandler.SpawnParticle(new LineParticle(scale: Main.rand.NextFloat(0.3f, 0.8f), velocity: spinninpoint.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(0.5f, 0.7f), relativePosition: GunTipPosition, affectedByGravity: false, lifetime: 40, color: Main.rand.NextBool() ? bColor : Color.Green));
						float sparkScale2 = Main.rand.NextFloat(0.4f, 1f);
						Vector2 sparkvelocity2 = spinninpoint.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.9f, 1.6f);
						GeneralParticleHandler.SpawnParticle(new LineParticle(GunTipPosition, sparkvelocity2, affectedByGravity: false, 40, sparkScale2, Main.rand.NextBool() ? bColor : Color.Green));
					}
				}
				else
				{
					base.OffsetLengthFromArm -= 15f;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ViperSpit");
					style.Pitch = chargePower * 0.3f + Main.rand.NextFloat(-0.1f, 0.1f);
					style.Volume = 0.5f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity * MathHelper.Clamp(chargePower, 0.3f, 1f), ModContent.ProjectileType<VitriolicViperSpit>(), (int)((float)base.Projectile.damage * MathHelper.Clamp(chargePower, 0.3f, 1f)), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, chargePower).extraUpdates = (int)Utils.Remap(chargePower, 0f, 1f, 2f, 13f);
					}
					for (int l = 0; l < 17; l++)
					{
						Dust dust2 = Dust.NewDustPerfect(GunTipPosition, 278);
						dust2.velocity = base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(5f, 25f);
						dust2.scale = Main.rand.NextFloat(0.55f, 0.75f) * chargePower + 0.1f;
						dust2.noGravity = true;
						dust2.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Green : bColor, 0.7f);
					}
					Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition, shootDirection * 18f, affectedByGravity: false, 6, 0.057f * chargePower, bColor, new Vector2(1.7f, 0.8f), quickShrink: true));
				}
				base.Projectile.ai[1] = 1f;
			}
		}
		else
		{
			if (base.Projectile.ai[1] != 1f)
			{
				if (!FirstCharge)
				{
					CurrentChargingFrames++;
				}
				else
				{
					CurrentOverchargeFrames++;
				}
			}
			if (CurrentChargingFrames >= 10f)
			{
				if (FirstCharge)
				{
					for (int m = 0; m < 2; m++)
					{
						Dust dust3 = Dust.NewDustPerfect(GunTipPosition + ((float)m * (float)Math.PI + vortexRotation * 0.35f + (float)Math.PI / 2f).ToRotationVector2() * 20f * chargePower * overchargePower, 75, ((float)m * (float)Math.PI + vortexRotation * 0.35f * (float)Math.Sign(((Vector2)(ref base.Projectile.velocity)).Length())).ToRotationVector2() * 4f);
						dust3.noGravity = true;
						dust3.scale = Main.rand.NextFloat(0.85f, 0.9f);
					}
				}
				else
				{
					Dust dust4 = Dust.NewDustPerfect(GunTipPosition, 75, Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.2f, 1f) * chargePower * overchargePower);
					dust4.noGravity = true;
					dust4.scale = Main.rand.NextFloat(0.6f, 0.7f);
				}
			}
			if (CurrentChargingFrames == (float)FirstChargeFrames && CurrentOverchargeFrames == 0f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/NuclearTerrorHit");
				style.Volume = 1f;
				style.Pitch = -0.2f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int n = 0; n < 18; n++)
				{
					Vector2 dustVel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 5f);
					Dust dust5 = Dust.NewDustPerfect(GunTipPosition + dustVel, 278, dustVel * 0.7f);
					dust5.scale = Main.rand.NextFloat(0.45f, 0.9f);
					dust5.noGravity = true;
					dust5.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? bColor : Color.Green, 0.7f);
				}
			}
			if (CurrentOverchargeFrames == (float)FullyChargedFrames)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/NuclearTerrorHit");
				style.Volume = 1f;
				style.Pitch = 0.2f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int num = 0; num < 12; num++)
				{
					Dust dust6 = Dust.NewDustPerfect(GunTipPosition, 278, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(2f, 5.5f));
					dust6.scale = Main.rand.NextFloat(0.75f, 1.1f);
					dust6.noGravity = false;
					dust6.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Green : bColor, 0.7f);
				}
			}
		}
		if (base.Projectile.ai[1] == 1f)
		{
			CurrentChargingFrames = 0f;
			CurrentOverchargeFrames = 0f;
		}
		Lighting.AddLight(GunTipPosition, ((Color)(ref bColor)).ToVector3() * 1.5f * chargePower);
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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI / 2f * (float)base.Owner.direction) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleVortex", (AssetRequestMode)2).Value;
		Color val;
		for (int i = 0; i < 3; i++)
		{
			Vector2 position = GunTipPosition - Main.screenPosition;
			val = Color.Chartreuse;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position, null, val * 0.4f, vortexRotation * ((float)i * 0.3f), rechargeTexture.Size() * 0.5f, (0.07f + (float)i * 0.015f) * chargePower * overchargePower, (SpriteEffects)0);
		}
		Vector2 position2 = GunTipPosition - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position2, null, val * chargePower, vortexRotation, rechargeTexture.Size() * 0.5f, 0.25f * chargePower * 0.14f * overchargePower, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation + MathHelper.ToRadians(45f * (float)base.Projectile.spriteDirection), rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		return false;
	}

	public VitriolicViperHoldout()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		FirstChargeFrames = 30;
		FullyChargedFrames = 30;
		bColor = Color.Chartreuse;
		base._002Ector();
	}
}
