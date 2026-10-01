using System;
using System.IO;
using CalamityMod.Items.Weapons.Ranged;
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

namespace CalamityMod.Projectiles.Ranged;

public class AdamantiteAcceleratorHoldout : BaseGunHoldoutProjectile
{
	private SlotId ChargeupSoundSlot;

	public CalamityUtils.CurveSegment bounceAway = new CalamityUtils.CurveSegment(CalamityUtils.SineOutEasing, 0f, 0.5f, 0.5f);

	public CalamityUtils.CurveSegment moveBack = new CalamityUtils.CurveSegment(CalamityUtils.SineInEasing, 0.56f, 1f, -1f);

	public CalamityUtils.CurveSegment bounceBack = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.7f, 0f, -0.35f);

	public CalamityUtils.CurveSegment unsquish = new CalamityUtils.CurveSegment(CalamityUtils.SineOutEasing, 0f, 1f, -1f);

	public CalamityUtils.CurveSegment oversquish = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.7f, 0f, -0.6f);

	public override int AssociatedItemID => ModContent.ItemType<AdamantiteParticleAccelerator>();

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			return base.Owner.MountedCenter + base.Projectile.rotation.ToRotationVector2() * 70f + (Vector2.UnitY * -12f * (float)base.Owner.direction).RotatedBy(base.Projectile.rotation);
		}
	}

	public override float MaxOffsetLengthFromArm => base.MaxOffsetLengthFromArm;

	public override float OffsetXUpwards => base.OffsetXUpwards;

	public override float OffsetXDownwards => base.OffsetXDownwards;

	public override float BaseOffsetY => base.BaseOffsetY;

	public override float OffsetYUpwards => base.OffsetYUpwards;

	public override float OffsetYDownwards => base.OffsetYDownwards;

	public ref float ChargeTimer => ref base.Projectile.ai[0];

	public ref float DelayTimer => ref base.Projectile.ai[1];

	public ref float ChargeRate => ref base.Projectile.localAI[0];

	public ref float BounceBackPower => ref base.Projectile.localAI[1];

	internal float RecoilDisplacement => CalamityUtils.PiecewiseAnimation(1f - BounceBackPower, bounceAway, moveBack, bounceBack);

	internal float RecoilSquish => CalamityUtils.PiecewiseAnimation(1f - MathHelper.Clamp(BounceBackPower * 2f, 0f, 1f), unsquish, oversquish);

	public override void KillHoldoutLogic()
	{
		int maxTime = 44;
		if (ChargeTimer > (float)maxTime)
		{
			if (!base.Owner.CantUseHoldout())
			{
				ResetToStart();
			}
			else
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void HoldoutAI()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		if (ChargeRate == 0f)
		{
			ChargeRate = 44f / (float)base.HeldItem.useTime;
		}
		ActiveSound soundOut;
		if (ChargeTimer == 0f)
		{
			SoundStyle style = SoundID.DD2_WitherBeastAuraPulse with
			{
				Volume = SoundID.DD2_WitherBeastAuraPulse.Volume * 1.6f
			};
			ChargeupSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else if (SoundEngine.TryGetActiveSound(ChargeupSoundSlot, out soundOut) && soundOut.IsPlaying)
		{
			soundOut.Sound.Pitch = ChargeTimer * 2f;
			soundOut.Position = base.Projectile.Center;
		}
		if (BounceBackPower > 0f)
		{
			BounceBackPower -= 0.07f;
		}
		ChargeTimer += ChargeRate;
		if (!(MathHelper.Clamp(base.Projectile.ai[0] / 28f, 0f, 1f) >= 1f))
		{
			return;
		}
		if (DelayTimer == 0f)
		{
			FiringEffects(new Color(235, 40, 121));
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.rotation.ToRotationVector2(), ModContent.ProjectileType<AdamAcceleratorBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 120f).Center = GunTipPosition;
			}
		}
		else if (DelayTimer == 8f)
		{
			FiringEffects(new Color(49, 161, 246));
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.rotation.ToRotationVector2(), ModContent.ProjectileType<AdamAcceleratorBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, -120f).Center = GunTipPosition;
			}
		}
		DelayTimer++;
	}

	private void FiringEffects(Color color)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item92, GunTipPosition);
		SoundEngine.PlaySound(in SoundID.Item60, GunTipPosition);
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition + base.Projectile.rotation.ToRotationVector2() * 5f, Vector2.Zero, color, new Vector2(0.5f, 1f), base.Projectile.rotation, 0.05f, 0.34f + Main.rand.NextFloat(0.3f), 30));
		base.Owner.SetScreenshake(1f);
		BounceBackPower = 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D gun = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/AdamantiteParticleAccelerator", (AssetRequestMode)2).Value;
		SpriteEffects flip = (SpriteEffects)(base.Owner.direction < 0);
		float drawAngle = base.Projectile.rotation + ((base.Owner.direction < 0) ? ((float)Math.PI) : 0f);
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((base.Owner.direction < 0) ? ((float)gun.Width - 33f) : 33f, 33f);
		Vector2 drawOffset = base.Owner.MountedCenter + base.Projectile.rotation.ToRotationVector2() * (10f - RecoilDisplacement * 5f) - Main.screenPosition;
		Vector2 scale = new Vector2(1f - 0.05f * RecoilSquish, 1f + 0.05f * RecoilSquish) * base.Projectile.scale;
		Main.EntitySpriteDraw(gun, drawOffset, null, lightColor, drawAngle, drawOrigin, scale, flip);
		return false;
	}

	public override bool PreKill(int timeLeft)
	{
		if (!base.Owner.CantUseHoldout())
		{
			ResetToStart();
			return false;
		}
		return true;
	}

	public void ResetToStart()
	{
		ChargeTimer = 0f;
		DelayTimer = 0f;
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(ChargeRate);
		writer.Write(BounceBackPower);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		ChargeRate = reader.ReadSingle();
		BounceBackPower = reader.ReadSingle();
	}
}
