using System;
using System.Collections.Generic;
using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

[PierceResistException(false)]
public class CountermeasureMittHoldout : BaseGunHoldoutProjectile
{
	public float offsetBase;

	public int time;

	public float rotSpeed;

	public float f1r;

	public float f1x;

	public float f1d = -0.95f;

	public SlotId AudSlot1;

	public float f2r;

	public float f2x;

	public float f2d = -0.95f;

	public SlotId AudSlot2;

	public float f3r;

	public float f3x;

	public float f3d = -0.95f;

	public SlotId AudSlot3;

	public float f4r;

	public float f4x;

	public float f4d = -0.95f;

	public SlotId AudSlot4;

	public float f5r;

	public float f5x;

	public float f5d = -0.95f;

	public SlotId AudSlot5;

	public int palmAnimTime = 20;

	public int cooldownGiven = 400;

	public int attackTime = 100;

	public int attackCycles;

	public bool failedManaCheck;

	public bool canHit;

	public float alteredRotationRot = 0.7f;

	public int hitTimer;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override int AssociatedItemID => ModContent.ItemType<CountermeasureMitt>();

	public override float MaxOffsetLengthFromArm => offsetBase;

	public override float RecoilResolveSpeed => 0.4f;

	public override float OffsetXUpwards => 0f;

	public override float BaseOffsetY => 0f;

	public override float OffsetYDownwards => 0f;

	public override float WeaponTurnSpeed => 0.5f;

	public bool forcePalm => base.Projectile.ai[2] != 0f;

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public ref float fireRate => ref base.Projectile.ai[1];

	public float f1l => 0.02f * rotSpeed;

	public float f2l => 0.016f * rotSpeed;

	public float f3l => 0.012f * rotSpeed;

	public float f4l => 0.008f * rotSpeed;

	public float f5l => 0.004f * rotSpeed;

	public float alteredRotation => base.Projectile.rotation + f2r * alteredRotationRot * (float)(-base.Projectile.direction);

	public Vector2 palmBlastPos
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			return base.Owner.Center + alteredRotation.ToRotationVector2() * 55f * base.Projectile.scale;
		}
	}

	public int palmManaCost => base.HeldItem.mana * 30;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 55;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void KillHoldoutLogic()
	{
	}

	public override bool? CanDamage()
	{
		return null;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (!canHit)
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		if (!canHit)
		{
			return false;
		}
		float realRot = alteredRotation;
		Vector2 handPos = base.Owner.GetBackHandPosition(Player.CompositeArmStretchAmount.None, base.Owner.compositeBackArm.rotation) + (base.Owner.compositeBackArm.rotation + (float)Math.PI / 2f).ToRotationVector2() * 9f;
		float beamLength = 2000f * base.Projectile.scale;
		float beamThickness = 20f * base.Projectile.scale;
		float fingerLength = 6f * base.Projectile.scale;
		float _ = float.NaN;
		bool colCheck = false;
		for (int t = 0; t < 5; t++)
		{
			float rot = t switch
			{
				1 => f4r * (float)base.Projectile.direction, 
				2 => f3r * (float)base.Projectile.direction, 
				3 => f2r * (float)base.Projectile.direction, 
				4 => f1r * (float)base.Projectile.direction, 
				_ => (0f - f5r) * 0.3f * (float)base.Projectile.direction, 
			};
			float num = t switch
			{
				1 => f4x, 
				2 => f3x, 
				3 => f2x, 
				4 => f1x, 
				_ => f5x, 
			};
			Vector2 basePosition = handPos + alteredRotation.ToRotationVector2() * ((t == 0) ? 2.5f : 3.3f) * base.Projectile.scale + (alteredRotation + rot).ToRotationVector2() * fingerLength + (alteredRotation + (float)Math.PI / 2f * (float)(-base.Projectile.direction)).ToRotationVector2() * (float)((t == 0) ? (-3) : 3) * base.Projectile.scale;
			if (num > 0.2f)
			{
				colCheck = Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), basePosition, basePosition + (realRot + rot).ToRotationVector2() * (fingerLength + beamLength), beamThickness, ref _);
			}
			if (colCheck)
			{
				t = 5;
			}
		}
		return colCheck;
	}

	public void EndSounds()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(AudSlot1, out ActiveSound s1))
		{
			s1?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(AudSlot2, out ActiveSound s2))
		{
			s2?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(AudSlot3, out ActiveSound s3))
		{
			s3?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(AudSlot4, out ActiveSound s4))
		{
			s4?.Stop();
		}
		if (SoundEngine.TryGetActiveSound(AudSlot5, out ActiveSound s5))
		{
			s5?.Stop();
		}
	}

	public override void HoldoutAI()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e25: Unknown result type (might be due to invalid IL or missing references)
		if (hitTimer > 0)
		{
			hitTimer--;
		}
		if (base.Owner.dead)
		{
			EndSounds();
			base.Projectile.Kill();
			return;
		}
		base.Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, alteredRotation + (float)Math.PI / 2f * (float)(-base.Projectile.direction) + ((base.Owner.direction == -1) ? ((float)Math.PI) : 0f));
		Vector2 handPos = base.Owner.GetBackHandPosition(Player.CompositeArmStretchAmount.None, base.Owner.compositeBackArm.rotation) + (base.Owner.compositeBackArm.rotation + (float)Math.PI / 2f).ToRotationVector2() * 4f;
		float armRotation = (base.Owner.Center.DirectionTo(handPos).ToRotation() - (float)Math.PI / 2f) * base.Owner.gravDir + ((base.Owner.gravDir == -1f) ? ((float)Math.PI) : 0f);
		base.Owner.SetCompositeArmFront(enabled: true, base.FrontArmStretch, armRotation + base.ExtraFrontArmRotation * (float)base.Projectile.direction);
		if (time == 0 && forcePalm)
		{
			rotSpeed = 7f;
			alteredRotationRot = 0f;
		}
		if (time == 1)
		{
			rotSpeed = 0f;
		}
		if (forcePalm)
		{
			alteredRotationRot = MathHelper.Lerp(alteredRotationRot, 0f, 0.1f);
		}
		else if (shootingTimer > -10f)
		{
			alteredRotationRot = MathHelper.Lerp(alteredRotationRot, 0.7f, 0.1f);
		}
		float fxPower = 5f;
		if (!forcePalm)
		{
			if (shootingTimer == 10f)
			{
				f1d *= -1f;
			}
			if (shootingTimer == 20f)
			{
				f2d *= -1f;
			}
			if (shootingTimer == 30f)
			{
				f3d *= -1f;
			}
			if (shootingTimer == 40f)
			{
				f4d *= -1f;
			}
			if (shootingTimer == 50f)
			{
				f5d *= -1f;
			}
			if (attackCycles == 0)
			{
				if (shootingTimer == 10f)
				{
					f1x = fxPower;
				}
				if (shootingTimer == 20f)
				{
					f2x = fxPower;
				}
				if (shootingTimer == 30f)
				{
					f3x = fxPower;
				}
				if (shootingTimer == 40f)
				{
					f4x = fxPower;
				}
				if (shootingTimer == 50f)
				{
					f5x = fxPower;
				}
			}
		}
		f1r = MathHelper.Lerp(f1r, f1d, f1l * rotSpeed);
		f2r = MathHelper.Lerp(f2r, f2d * 0.775f, f2l * rotSpeed);
		f3r = MathHelper.Lerp(f3r, f3d * 0.55f, f3l * rotSpeed);
		f4r = MathHelper.Lerp(f4r, f4d * 0.325f, f4l * rotSpeed);
		f5r = MathHelper.Lerp(f5r, f5d * 2.3f - 2f, f5l * 4f * rotSpeed);
		float goalValue = (canHit ? 1 : 0);
		float lerpPower = (forcePalm ? 0.35f : (canHit ? 0.2f : 0.25f));
		f1x = MathHelper.Lerp(f1x, (f1x > 0.05f) ? goalValue : 0f, lerpPower);
		f2x = MathHelper.Lerp(f2x, (f2x > 0.05f) ? goalValue : 0f, lerpPower);
		f3x = MathHelper.Lerp(f3x, (f3x > 0.05f) ? goalValue : 0f, lerpPower);
		f4x = MathHelper.Lerp(f4x, (f4x > 0.05f) ? goalValue : 0f, lerpPower);
		f5x = MathHelper.Lerp(f5x, (f5x > 0.05f) ? goalValue : 0f, lerpPower);
		float sine = (float)Math.Sin((float)time * 0.2f / (float)Math.PI) * 0.1f;
		float maxVol = 0.4f;
		float minPitch = -0.8f;
		if (SoundEngine.TryGetActiveSound(AudSlot1, out ActiveSound f1s) && f1s.IsPlaying)
		{
			f1s.Position = base.Projectile.Center;
			f1s.Pitch = Utils.Remap(f1x, 0.7f, 1f, 0f + minPitch, 0f) + sine;
			f1s.Volume = Utils.Remap(f1x, 0f, 1f, 0f, maxVol) * 100f;
		}
		else if (f1x > 1f)
		{
			SoundStyle beam = new SoundStyle("CalamityMod/Sounds/Item/MittWelding/Weld1");
			SoundStyle style = beam with
			{
				Volume = 0.01f,
				Pitch = 0f,
				IsLooped = true
			};
			AudSlot1 = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(AudSlot2, out ActiveSound f2s) && f2s.IsPlaying)
		{
			f2s.Position = base.Projectile.Center;
			f2s.Pitch = Utils.Remap(f2x, 0.7f, 1f, 0.2f + minPitch, 0.2f) + sine;
			f2s.Volume = Utils.Remap(f2x, 0f, 1f, 0f, maxVol) * 100f;
		}
		else if (f2x > 1f)
		{
			SoundStyle beam2 = new SoundStyle("CalamityMod/Sounds/Item/MittWelding/Weld2");
			SoundStyle style = beam2 with
			{
				Volume = 0.01f,
				Pitch = 0f,
				IsLooped = true
			};
			AudSlot2 = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(AudSlot3, out ActiveSound f3s) && f3s.IsPlaying)
		{
			f3s.Position = base.Projectile.Center;
			f3s.Pitch = Utils.Remap(f3x, 0.7f, 1f, 0.4f + minPitch, 0.4f) + sine;
			f3s.Volume = Utils.Remap(f3x, 0f, 1f, 0f, maxVol) * 100f;
		}
		else if (f3x > 1f)
		{
			SoundStyle beam3 = new SoundStyle("CalamityMod/Sounds/Item/MittWelding/Weld3");
			SoundStyle style = beam3 with
			{
				Volume = 0.01f,
				Pitch = 0f,
				IsLooped = true
			};
			AudSlot3 = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(AudSlot4, out ActiveSound f4s) && f4s.IsPlaying)
		{
			f4s.Position = base.Projectile.Center;
			f4s.Pitch = Utils.Remap(f4x, 0.7f, 1f, 0.6f + minPitch, 0.6f) + sine;
			f4s.Volume = Utils.Remap(f4x, 0f, 1f, 0f, maxVol) * 100f;
		}
		else if (f4x > 1f)
		{
			SoundStyle beam4 = new SoundStyle("CalamityMod/Sounds/Item/MittWelding/Weld4");
			SoundStyle style = beam4 with
			{
				Volume = 0.01f,
				Pitch = 0f,
				IsLooped = true
			};
			AudSlot4 = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(AudSlot5, out ActiveSound f5s) && f5s.IsPlaying)
		{
			f5s.Position = base.Projectile.Center;
			f5s.Pitch = Utils.Remap(f5x, 0.7f, 1f, 0.8f + minPitch, 0.8f) + sine;
			f5s.Volume = Utils.Remap(f5x, 0f, 1f, 0f, maxVol) * 100f;
		}
		else if (f5x > 1f)
		{
			SoundStyle beam5 = new SoundStyle("CalamityMod/Sounds/Item/MittWelding/Weld5");
			SoundStyle style = beam5 with
			{
				Volume = 0.01f,
				Pitch = 0f,
				IsLooped = true
			};
			AudSlot5 = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		Vector2 center = base.Projectile.Center;
		Color val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, 0.3f);
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3() * MathHelper.Clamp((f1x + f2x + f3x + f4x + f5x - 4f) * 2f, 0f, 0.7f));
		base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 1f, 0.1f);
		if (time == 0)
		{
			if (!forcePalm)
			{
				shootingTimer = -40f;
			}
			if (base.Projectile.ai[2] != 0f)
			{
				base.Projectile.ai[2] = palmAnimTime;
			}
			fireRate = attackTime;
			base.OffsetLengthFromArm = offsetBase;
		}
		if (shootingTimer < 0f)
		{
			canHit = false;
			if (rotSpeed > 0f)
			{
				rotSpeed -= 0.05f;
			}
			if (shootingTimer == -1f || forcePalm)
			{
				if ((!Main.mouseLeft && !forcePalm) || failedManaCheck)
				{
					EndSounds();
					base.Projectile.Kill();
					return;
				}
				attackCycles = 0;
				fireRate = attackTime;
			}
		}
		if (base.Owner.Calamity().mouseRight && base.Owner.Calamity().arsenalCooldown <= 0 && !forcePalm && !failedManaCheck)
		{
			base.Projectile.ai[2] = 5f;
			shootingTimer = 0f;
		}
		if (forcePalm)
		{
			if (!base.Owner.CheckMana(base.HeldItem, palmManaCost))
			{
				failedManaCheck = true;
				SoundStyle style = SoundID.MaxMana with
				{
					Pitch = -0.5f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.ai[2] = 0f;
				return;
			}
			rotSpeed = 5f;
			f1d = 0f - Math.Abs(f1d);
			f2d = 0f - Math.Abs(f2d);
			f3d = 0f - Math.Abs(f3d);
			f4d = 0f - Math.Abs(f4d);
			f5d = 0f - Math.Abs(f5d);
			if (shootingTimer <= (float)palmAnimTime)
			{
				if (shootingTimer == 1f)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MittReadyPalm");
					style.Volume = 1f;
					style.Pitch = 0f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (shootingTimer == (float)(int)((float)palmAnimTime * 0.8f))
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MittThrust");
					style.Volume = 1f;
					style.Pitch = 0f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (shootingTimer == (float)palmAnimTime)
				{
					Shoot();
					base.Projectile.ai[2] = 0f;
					shootingTimer = -30f;
					rotSpeed = 0f;
				}
				Vector2 position = palmBlastPos;
				val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, 0.3f);
				Lighting.AddLight(position, ((Color)(ref val)).ToVector3() * (float)Math.Pow(Utils.GetLerpValue(0f, palmAnimTime, shootingTimer), 2.0));
			}
			if (shootingTimer > (float)palmAnimTime * 0.8f)
			{
				base.Projectile.scale *= 1.25f;
			}
			canHit = false;
		}
		else if (shootingTimer >= 0f)
		{
			if (!Main.mouseLeft)
			{
				shootingTimer = fireRate;
			}
			if (shootingTimer < fireRate && !failedManaCheck)
			{
				canHit = true;
				if (time % 3 == 0)
				{
					if (base.Owner.CheckMana(base.HeldItem, -1, pay: true))
					{
						if (rotSpeed < 1.1f)
						{
							rotSpeed += 0.1f;
						}
					}
					else
					{
						SoundStyle style = SoundID.MaxMana with
						{
							Pitch = -0.5f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						failedManaCheck = true;
					}
				}
			}
			else if (attackCycles < 1 && !failedManaCheck)
			{
				attackCycles++;
				shootingTimer = 0f;
			}
			else
			{
				shootingTimer = -30f;
			}
		}
		time++;
		shootingTimer++;
	}

	public void Shoot()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		bool hitAnything = false;
		bool oneFx = true;
		float damageMult = 25f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.dontTakeDamage || !(npc.Center.Distance(palmBlastPos) <= (float)(80 + Math.Max(npc.width / 2, npc.height / 2))))
			{
				continue;
			}
			hitAnything = true;
			Vector2 pos = Vector2.Lerp(npc.Center, base.Projectile.Center, 0.35f);
			Vector2 vel = Vector2.Lerp(base.Projectile.Center.DirectionTo(npc.Center), alteredRotation.ToRotationVector2(), 0.5f);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, vel, ModContent.ProjectileType<CountermeasurePalmBlast>(), (int)((float)base.Projectile.damage * damageMult), 0f, base.Projectile.owner, 0f, npc.whoAmI);
			}
			if (oneFx)
			{
				for (int i = 0; i < 25; i++)
				{
					float size = Main.rand.NextFloat(0.4f, 1f);
					Dust dust = Dust.NewDustPerfect(npc.Center + Main.rand.NextVector2Circular(10f, 10f), ModContent.DustType<SquashDust>(), -vel.SafeNormalize(Vector2.UnitX).RotatedBy(0.95f * (float)(Main.rand.NextBool() ? 1 : (-1))) * Main.rand.NextFloat(4f, 18f) * size, 0, default(Color), Main.rand.NextFloat(1.1f, 1.9f) + (1f - size));
					dust.noGravity = true;
					dust.color = ArsenalEffects.ArsenalLaserColor;
					dust.fadeIn = 0.7f;
				}
				for (int j = 0; j < 25; j++)
				{
					Dust dust2 = Dust.NewDustPerfect(pos + Main.rand.NextVector2Circular(20f, 20f), ArsenalEffects.ArsenalLaserDust, vel.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(10f, 45f), 0, default(Color), Main.rand.NextFloat(1f, 1.8f));
					dust2.noGravity = true;
					dust2.color = ArsenalEffects.ArsenalLaserColor;
					dust2.alpha = 100;
					dust2.fadeIn = 20f;
					if (j % 2 == 0)
					{
						Dust dust3 = Dust.NewDustPerfect(pos + Main.rand.NextVector2Circular(20f, 20f), ArsenalEffects.ArsenalLaserDust, vel.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(40f, 65f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.1f));
						dust3.noGravity = true;
						dust3.color = ArsenalEffects.ArsenalLaserColor;
						dust3.alpha = 100;
						dust3.fadeIn = 10f;
					}
				}
				for (int k = 0; k < 2; k++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, vel.SafeNormalize(Vector2.UnitX) * 2f, "CalamityMod/Particles/BloomRing", affectedByGravity: false, 20, 1f, ArsenalEffects.ArsenalLaserColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, vel.SafeNormalize(Vector2.UnitX) * 18f, "CalamityMod/Particles/BloomRing", affectedByGravity: false, 18, 0.8f, ArsenalEffects.ArsenalLaserColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, vel.SafeNormalize(Vector2.UnitX) * 34f, "CalamityMod/Particles/BloomRing", affectedByGravity: false, 16, 0.6f, ArsenalEffects.ArsenalLaserColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
				}
				GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, vel.SafeNormalize(Vector2.UnitX) * 8f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 22, 0.7f, ArsenalEffects.ArsenalLaserColor, new Vector2(1f, 8f), useAddativeBlend: true, glowCenter: true, (float)Math.PI / 2f, fadeIn: false, affectedByLight: false, -0.5f));
				oneFx = false;
			}
			damageMult -= 6f;
			if (damageMult < 1f)
			{
				damageMult = 1f;
			}
		}
		if (hitAnything && base.Owner.CheckMana(base.HeldItem, palmManaCost, pay: true))
		{
			base.Owner.Calamity().arsenalCooldown = cooldownGiven;
			base.Owner.AddCooldown(ArsenalPower.ID, cooldownGiven);
			SoundStyle aud = new SoundStyle("CalamityMod/Sounds/Item/MittHit");
			for (int l = 0; l < 2; l++)
			{
				SoundEngine.PlaySound(aud with
				{
					Volume = 0.8f,
					Pitch = 0f,
					MaxInstances = 2
				}, base.Projectile.Center);
			}
			base.Owner.SetScreenshake(8f);
			return;
		}
		base.Owner.Calamity().arsenalCooldown = cooldownGiven / 10;
		base.Owner.AddCooldown(ArsenalPower.ID, cooldownGiven / 10);
		for (int m = 0; m < 15; m++)
		{
			Dust dust4 = Dust.NewDustPerfect(palmBlastPos, ModContent.DustType<SquashDust>(), base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(4f, 7f), 0, default(Color), Main.rand.NextFloat(0.7f, 0.85f));
			dust4.noGravity = false;
			dust4.color = ArsenalEffects.ArsenalLaserColor;
		}
		for (int n = 0; n < 2; n++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(palmBlastPos, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, 0.5f, ArsenalEffects.ArsenalLaserColor, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true));
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MittFail");
		style.Volume = 1f;
		style.Pitch = 0f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (hitTimer == 0)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustPerfect(target.Center, ArsenalEffects.ArsenalLaserDust, base.Owner.Center.DirectionTo(target.Center).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(9f, 13f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.1f));
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalLaserColor;
				dust.alpha = 100;
				dust.fadeIn = 10f;
			}
			hitTimer = base.Projectile.localNPCHitCooldown;
		}
		else
		{
			Dust dust2 = Dust.NewDustPerfect(target.Center, ArsenalEffects.ArsenalLaserDust, base.Owner.Center.DirectionTo(target.Center).RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(6f, 9f), 0, default(Color), Main.rand.NextFloat(0.3f, 0.7f));
			dust2.noGravity = true;
			dust2.color = ArsenalEffects.ArsenalLaserColor;
			dust2.alpha = 100;
			dust2.fadeIn = 10f;
			modifiers.SourceDamage *= 0.2f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 handPos = base.Owner.GetBackHandPosition(Player.CompositeArmStretchAmount.None, base.Owner.compositeBackArm.rotation) + (base.Owner.compositeBackArm.rotation + (float)Math.PI / 2f).ToRotationVector2() * 9f * base.Projectile.scale;
		base.Projectile.Center = handPos;
		if (time < 2)
		{
			return false;
		}
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/CountermeasureMittHoldout", (AssetRequestMode)2).Value;
		Texture2D finger = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/CountermeasureMittFinger2", (AssetRequestMode)2).Value;
		Texture2D laser = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineFade", (AssetRequestMode)2).Value;
		Texture2D laser2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineThick", (AssetRequestMode)2).Value;
		Texture2D bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D diamond = ModContent.Request<Texture2D>("CalamityMod/Particles/SquareRotated", (AssetRequestMode)2).Value;
		Vector2 drawPosition = handPos - Main.screenPosition;
		Vector2 vel = alteredRotation.ToRotationVector2();
		float randSize = Main.rand.NextFloat(0.8f, 1.3f);
		float fadeIn = (float)Math.Pow(Utils.GetLerpValue(0f, palmAnimTime, shootingTimer), 2.0);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = alteredRotation + ((base.Projectile.direction == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = value.Size() * 0.5f;
		_ = base.Projectile.direction;
		_ = -1;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.direction == -1);
		float thrustCuttoff = (float)palmAnimTime * 0.8f;
		float pullBack = MathHelper.Lerp(0.2f, 1f, (float)Math.Pow(Utils.GetLerpValue(thrustCuttoff, 0f, shootingTimer, clamped: true), 4.0));
		float pushOut = MathHelper.Lerp(0.2f, 1.5f, (float)Math.Pow(Utils.GetLerpValue(thrustCuttoff, palmAnimTime, shootingTimer, clamped: true), 5.0));
		float thrustMult = ((!forcePalm) ? 1f : ((shootingTimer <= thrustCuttoff) ? pullBack : pushOut));
		Vector2 thrustAddition = vel * 10f * thrustMult - vel * 10f;
		Main.EntitySpriteDraw(value, drawPosition + thrustAddition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Vector2 fingerOrgin = default(Vector2);
		Color val;
		for (int i = 0; i < 5; i++)
		{
			float fingerLength = 3f * base.Projectile.scale;
			Vector2 fingerScale = new Vector2(1f, 1f) * base.Projectile.scale;
			((Vector2)(ref fingerOrgin))._002Ector(0f, (float)(finger.Height / 2));
			float rot = i switch
			{
				1 => f4r * (float)base.Projectile.direction, 
				2 => f3r * (float)base.Projectile.direction, 
				3 => f2r * (float)base.Projectile.direction, 
				4 => f1r * (float)base.Projectile.direction, 
				_ => (0f - f5r) * 0.3f * (float)base.Projectile.direction, 
			};
			float fx = i switch
			{
				1 => f4x, 
				2 => f3x, 
				3 => f2x, 
				4 => f1x, 
				_ => f5x, 
			};
			Color fingerColor = Color.Lerp(drawColor, Color.Black, (float)(4 - i) * 0.2f);
			if (i == 0)
			{
				fingerScale = new Vector2(0.9f, 1f) * base.Projectile.scale;
				fingerLength = 1f * base.Projectile.scale;
				fingerColor = drawColor;
			}
			Vector2 basePosition = handPos + vel * ((i == 0) ? 2.5f : 3.3f) * base.Projectile.scale + thrustAddition + (alteredRotation + rot).ToRotationVector2() * fingerLength + (alteredRotation + (float)Math.PI / 2f * (float)(-base.Projectile.direction)).ToRotationVector2() * (float)((i == 0) ? (-3) : 3) * base.Projectile.scale;
			Main.EntitySpriteDraw(finger, basePosition - Main.screenPosition, null, fingerColor, alteredRotation + rot, fingerOrgin, fingerScale, (SpriteEffects)0);
			Vector2 velocity = (alteredRotation + rot).ToRotationVector2();
			float laserLength = 2.5f;
			Vector2 position = basePosition - Main.screenPosition + velocity * 542f * laserLength * base.Projectile.scale;
			val = ArsenalEffects.ArsenalLaserColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(laser, position, null, val * fx, velocity.ToRotation() + (float)Math.PI / 2f, laser.Size() / 2f, new Vector2(2.5f * randSize * fx, 55f * laserLength) * base.Projectile.scale * 0.01f, (SpriteEffects)2);
			Vector2 position2 = basePosition - Main.screenPosition + velocity * 542f * laserLength * base.Projectile.scale;
			val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, 0.3f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(laser2, position2, null, val * fx, velocity.ToRotation() + (float)Math.PI / 2f, laser2.Size() / 2f, new Vector2(0.4f * Math.Min(fx, 1f), 55f * laserLength) * base.Projectile.scale * 0.01f, (SpriteEffects)2);
			for (int y = 0; y < 2; y++)
			{
				Vector2 position3 = basePosition - Main.screenPosition + velocity * 7f * base.Projectile.scale;
				val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, (float)y);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(bloom, position3, null, val * (fx - 1f), velocity.ToRotation() + (float)Math.PI / 2f, bloom.Size() / 2f, new Vector2(2f + 0.05f * (fx + 25f), 1f) * base.Projectile.scale * MathHelper.Lerp(fx, 1f, 0.7f) * (0.03f - 0.01f * (float)y), (SpriteEffects)2);
			}
		}
		if (forcePalm)
		{
			for (int j = 0; j < 8; j++)
			{
				Vector2 position4 = palmBlastPos - Main.screenPosition + thrustAddition;
				val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, (float)j * 0.1f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(bloom, position4, null, val * fadeIn * 0.25f, alteredRotation + Main.rand.NextFloat(-4f, 4f), bloom.Size() / 2f, new Vector2(0.4f, 1.8f) * base.Projectile.scale * (0.3f - 0.02f * (float)j) * randSize, (SpriteEffects)2);
			}
			Vector2 position5 = palmBlastPos - Main.screenPosition + thrustAddition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(diamond, position5, null, val * fadeIn, 0f, diamond.Size() / 2f, new Vector2(1.7f * Main.rand.NextFloat(1f, 1.6f) * (float)Math.Pow(fadeIn, 3.0), 0.06f) * base.Projectile.scale * 0.3f, (SpriteEffects)2);
			Vector2 position6 = palmBlastPos - Main.screenPosition + thrustAddition;
			val = ArsenalEffects.ArsenalLaserColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(bloom, position6, null, val * fadeIn * 0.35f, 0f, bloom.Size() / 2f, new Vector2(1.8f, 0.5f) * base.Projectile.scale * 0.4f * randSize, (SpriteEffects)2);
		}
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}
}
