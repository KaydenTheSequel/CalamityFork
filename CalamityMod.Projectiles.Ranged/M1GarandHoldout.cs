using System;
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

public class M1GarandHoldout : BaseGunHoldoutProjectile
{
	private float muzzleFlashTimer;

	private float flashVariant;

	private int pingAnimationTimer;

	private const int PingAnimationDuration = 120;

	private bool shouldPing;

	public static Asset<Texture2D> MuzzleFlash;

	public static Asset<Texture2D> HoldoutGlow;

	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumBlunderbussFire")
	{
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle PingSound = new SoundStyle("CalamityMod/Sounds/Item/M1GarandPing");

	public static readonly SoundStyle ReloadSound = new SoundStyle("CalamityMod/Sounds/Item/M1GarandReload");

	private SlotId reloadSoundSlot;

	public override int AssociatedItemID => ModContent.ItemType<M1Garand>();

	public ref float CurrentState => ref base.Projectile.ai[0];

	public ref float reloadTimeLeft => ref base.Projectile.ai[1];

	public ref float globalTimer => ref base.Projectile.ai[2];

	public override float RecoilResolveSpeed => 0.13f;

	public override float MaxOffsetLengthFromArm => 28f;

	public override float BaseOffsetY => -4f;

	public override float OffsetXUpwards => -6f;

	public override float OffsetXDownwards => 0f;

	public override float OffsetYUpwards => 0f;

	public override float OffsetYDownwards => 0f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + new Vector2(1.5f, -2.5f) + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.425f;
		}
	}

	public override string Texture => "CalamityMod/Projectiles/Ranged/M1GarandHoldout";

	public override void Load()
	{
		MuzzleFlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/M1GarandMuzzleFlash", (AssetRequestMode)1);
		HoldoutGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/M1GarandHoldoutGlow", (AssetRequestMode)1);
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		base.Projectile.frame = Main.projFrames[base.Type] - 1;
	}

	public override void KillHoldoutLogic()
	{
		if (CurrentState == 0f && base.HeldItem.type != base.Owner.HeldItem.type)
		{
			base.Projectile.Kill();
			base.Projectile.netUpdate = true;
		}
	}

	public override void HoldoutAI()
	{
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		globalTimer++;
		if (muzzleFlashTimer > 0f)
		{
			muzzleFlashTimer--;
		}
		if (shouldPing)
		{
			pingAnimationTimer += 3;
		}
		while (true)
		{
			float currentState = CurrentState;
			if (currentState != 0f)
			{
				if (currentState != 1f)
				{
					break;
				}
				reloadTimeLeft--;
				base.Owner.channel = true;
				if (reloadTimeLeft <= 120f && reloadTimeLeft > 75f)
				{
					base.Projectile.rotation += 0.15f * (float)base.Projectile.spriteDirection;
				}
				else if (reloadTimeLeft < 75f && reloadTimeLeft >= 64f)
				{
					float progress = (75f - reloadTimeLeft) / 11f;
					float lerpedOffset = MathHelper.Lerp(0.15f, 0f, progress);
					base.Projectile.rotation += lerpedOffset * (float)base.Projectile.spriteDirection;
				}
				else if (reloadTimeLeft == 39f)
				{
					base.OffsetLengthFromArm -= 2f;
				}
				if (reloadTimeLeft > 40f)
				{
					base.Projectile.frame = 3;
					base.Projectile.frameCounter = 0;
				}
				else if (base.Projectile.frame < Main.projFrames[base.Type] - 1)
				{
					base.Projectile.frameCounter++;
					if (base.Projectile.frameCounter >= 3)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
				}
				else
				{
					base.Projectile.frame = Main.projFrames[base.Type] - 1;
				}
				if (reloadTimeLeft == 110f)
				{
					reloadSoundSlot = SoundEngine.PlaySound(in ReloadSound, base.Projectile.Center);
				}
				if (SoundEngine.TryGetActiveSound(reloadSoundSlot, out ActiveSound sound) && sound.IsPlaying)
				{
					sound.Position = base.Projectile.Center;
				}
				if (reloadTimeLeft <= 0f)
				{
					if (SoundEngine.TryGetActiveSound(reloadSoundSlot, out ActiveSound soundToStop) && soundToStop.IsPlaying)
					{
						soundToStop.Stop();
					}
					base.Owner.Calamity().garandShots += 8;
					globalTimer = 0f;
					base.Projectile.Kill();
					base.Projectile.netUpdate = true;
				}
				break;
			}
			if (base.Projectile.frame == 5)
			{
				base.Projectile.frameCounter++;
				if ((float)base.Projectile.frameCounter >= MathHelper.Clamp((float)(base.Owner.HeldItem.useAnimation - 18), 0f, (float)base.Owner.HeldItem.useAnimation))
				{
					if (base.Owner.CantUseHoldout())
					{
						base.Projectile.Kill();
						break;
					}
					if (base.Owner.Calamity().garandShots != 0)
					{
						base.Projectile.frame = 0;
						base.Projectile.frameCounter = 0;
						break;
					}
					CurrentState = 1f;
					reloadTimeLeft = 120f;
					continue;
				}
				break;
			}
			if (base.Projectile.frame == 0)
			{
				if (base.Owner.Calamity().garandShots == 0 && globalTimer < 3f)
				{
					CurrentState = 1f;
					reloadTimeLeft = 120f;
					continue;
				}
				if (base.Projectile.frameCounter != 1)
				{
					base.Projectile.frameCounter++;
					if (base.Projectile.frameCounter >= 3)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					break;
				}
				base.Projectile.frameCounter++;
				if (base.Owner.Calamity().garandShots <= 0)
				{
					break;
				}
				muzzleFlashTimer = 2f;
				flashVariant = Main.rand.Next(3);
				SoundEngine.PlaySound(in FireSound, GunTipPosition);
				if (base.Owner.Calamity().garandShots == 1)
				{
					SoundEngine.PlaySound(in PingSound, GunTipPosition);
					shouldPing = true;
				}
				base.OffsetLengthFromArm -= 3.5f;
				base.Owner.Calamity().garandShots--;
				Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.HeldItem.shootSpeed;
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity.RotatedBy(Main.rand.NextFloat(2.5f, 2.65f) * (float)(-base.Projectile.direction)) * 0.5f, ModContent.ProjectileType<M1GarandBulletCasing>(), 0, 0f, base.Projectile.owner);
					if (base.Owner.Calamity().garandShots == 0)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity.RotatedBy(Main.rand.NextFloat(1.175f, 1.25f) * (float)(-base.Projectile.direction)) * 0.285f, ModContent.ProjectileType<M1GarandEmptyClip>(), 0, 0f, base.Projectile.owner);
						GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.PaleGoldenrod, Color.Gold * 0.25f, 1.25f, 5, base.Projectile.velocity.ToRotation() + Main.rand.NextFloat(-0.15f, 0.15f)));
					}
				}
				for (int i = 0; i < 8; i++)
				{
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 7f * Main.rand.NextFloat(0.4f, 1.5f), Color.Gray, Main.rand.Next(30, 50), Main.rand.NextFloat(0.22f, 0.44f), 0.35f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 4f, Color.Gray * 0.5f, new Vector2(0.5f, 1f), base.Projectile.velocity.ToRotation(), 0.1f, 0.475f, 18));
				if (Main.myPlayer == base.Projectile.owner)
				{
					Vector2 offsetPos = GunTipPosition - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 18f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), offsetPos, velocity, ModContent.ProjectileType<M1GarandShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				break;
			}
			if (base.Projectile.frame > 0)
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter >= 3)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
			}
			break;
		}
	}

	public override bool? CanDamage()
	{
		return muzzleFlashTimer > 0f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(GunTipPosition + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)MuzzleFlash.Width() * 0.5f, 64f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		if (globalTimer < 3f)
		{
			return false;
		}
		int chosenFrame = Math.Clamp(base.Projectile.frame, 0, Main.projFrames[base.Type] - 1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, chosenFrame);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		float scale = base.Projectile.scale * base.Owner.gravDir;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (CurrentState != 1f && base.Owner.Calamity().garandShots == 0 && base.Projectile.frame >= 3)
		{
			frame = texture.Frame(1, Main.projFrames[base.Type], 0, 3);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, frame.Size() * 0.5f, scale, flipSprite);
		if (muzzleFlashTimer > 0f)
		{
			Texture2D value = MuzzleFlash.Value;
			int flashFrameY = (int)flashVariant;
			Vector2 flashPos = GunTipPosition + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)MuzzleFlash.Width() * 0.5f - Main.screenPosition;
			Rectangle flashFrame = value.Frame(1, 3, 0, flashFrameY);
			Main.EntitySpriteDraw(value, flashPos, flashFrame, Color.White, drawRotation, flashFrame.Size() * 0.5f, scale * 0.8f, flipSprite);
		}
		if (shouldPing)
		{
			if (pingAnimationTimer <= 120)
			{
				Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/M1GarandPingText", (AssetRequestMode)2).Value;
				Vector2 origin = value2.Size() * 0.5f;
				Vector2 finalPosition = base.Owner.Center + new Vector2(40f * (float)(-base.Owner.direction), -40f * base.Owner.gravDir);
				float progress = (float)pingAnimationTimer / 120f;
				Vector2 pingDrawPosition;
				if (pingAnimationTimer < 15)
				{
					float popOutProgress = (float)pingAnimationTimer / 15f;
					pingDrawPosition = Vector2.Lerp(base.Owner.Center, finalPosition, popOutProgress);
				}
				else
				{
					pingDrawPosition = finalPosition;
				}
				pingDrawPosition -= Main.screenPosition;
				float stretchFactorY = 0f;
				if (progress < 0.25f)
				{
					float stretchProgress = progress / 0.25f;
					stretchFactorY = MathHelper.Lerp(-1.4f, 0.5f, (float)Math.Sin(stretchProgress * ((float)Math.PI / 2f)));
				}
				else if (progress < 0.5f)
				{
					float squashProgress = (progress - 0.25f) / 0.3f;
					stretchFactorY = MathHelper.Lerp(0.5f, -0.25f, squashProgress * squashProgress);
				}
				else if (progress < 0.8f)
				{
					float stretchProgress2 = (progress - 0.6f) / 0.2f;
					stretchFactorY = MathHelper.Lerp(-0.25f, 0f, stretchProgress2 * stretchProgress2);
				}
				else
				{
					stretchFactorY = 0f;
				}
				Color drawColor = Color.White;
				int fadeDuration = 30;
				int fadeStartFrame = 120 - fadeDuration;
				if (pingAnimationTimer >= fadeStartFrame)
				{
					float fadeProgress = (float)(pingAnimationTimer - fadeStartFrame) / (float)fadeDuration;
					drawColor = Color.White * MathHelper.Lerp(1f, 0f, fadeProgress);
				}
				float basePingDrawScale = 1f;
				float stretchScaleY = 1f + stretchFactorY;
				float stretchScaleX = 1f - stretchFactorY * 0.5f;
				Vector2 finalPingScale = default(Vector2);
				((Vector2)(ref finalPingScale))._002Ector(basePingDrawScale * stretchScaleX, basePingDrawScale * stretchScaleY);
				Main.EntitySpriteDraw(value2, pingDrawPosition, null, drawColor, ((base.Owner.direction == 1) ? (-0.25f) : 0.25f) + ((base.Owner.gravDir == -1f) ? ((float)Math.PI * (float)base.Owner.direction) : 0f), origin, finalPingScale, (SpriteEffects)(base.Owner.gravDir == -1f));
			}
			else
			{
				shouldPing = false;
				pingAnimationTimer = 0;
			}
		}
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentState != 1f && !(globalTimer < 3f))
		{
			int chosenFrame = Math.Clamp(base.Projectile.frame, 0, Main.projFrames[base.Type] - 1);
			if (globalTimer > 2f && globalTimer < 6f)
			{
				chosenFrame = 0;
			}
			Rectangle frame = TextureAssets.Projectile[base.Type].Value.Frame(1, Main.projFrames[base.Type], 0, chosenFrame);
			Main.EntitySpriteDraw(HoldoutGlow.Value, base.Projectile.Center - Main.screenPosition, rotation: base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f), origin: frame.Size() * 0.5f, scale: base.Projectile.scale * base.Owner.gravDir, effects: (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f), sourceRectangle: frame, color: Color.White);
		}
	}
}
