using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
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

public class MagnaCannonHoldout : BaseGunHoldoutProjectile
{
	public SlotId MagnaChargeSlot;

	public static int FramesPerLoad = 9;

	public static int MaxLoadableShots = 20;

	public static float BulletSpeed = 12f;

	public int time;

	public override int AssociatedItemID => ModContent.ItemType<MagnaCannon>();

	public override float MaxOffsetLengthFromArm => 28f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private ref float ShotsLoaded => ref base.Projectile.ai[1];

	private ref float ShootTimer => ref base.Projectile.ai[2];

	private bool FullyCharged => CurrentChargingFrames >= (float)MagnaCannon.FullChargeFrames;

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
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(MagnaChargeSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = base.Projectile.Center;
		}
		Color newColor;
		if (base.Owner.CantUseHoldout())
		{
			base.KeepRefreshingLifetime = false;
			if (ShotsLoaded > 0f)
			{
				base.Projectile.timeLeft = MagnaCannon.AftershotCooldownFrames;
				ShootTimer--;
			}
			if (ShootTimer <= 0f)
			{
				ChargeSound?.Stop();
				SoundEngine.PlaySound(in MagnaCannon.Fire, base.Projectile.position);
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * BulletSpeed;
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity.RotatedByRandom(MathHelper.ToRadians(9f)), ModContent.ProjectileType<MagnaShot>(), base.Projectile.damage, base.Projectile.knockBack * (float)((!FullyCharged) ? 1 : 3), base.Projectile.owner);
				}
				for (int i = 0; i <= 3; i++)
				{
					Vector2 gunTipPosition = GunTipPosition;
					int type = ModContent.DustType<SquashDust>();
					Vector2? velocity = shootVelocity.RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.2f, 0.9f);
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(gunTipPosition, type, velocity, 0, newColor, Main.rand.NextFloat(1.3f, 1.6f));
					dust.noGravity = false;
					dust.color = (Main.rand.NextBool(3) ? Color.DodgerBlue : Color.RoyalBlue);
					dust.fadeIn = 0.3f;
				}
				base.OffsetLengthFromArm -= 5f;
				ShotsLoaded--;
				ShootTimer = (FullyCharged ? 4f : 5f);
			}
		}
		else
		{
			if (ShotsLoaded < (float)MaxLoadableShots && CurrentChargingFrames % (float)FramesPerLoad == 0f)
			{
				ShotsLoaded++;
			}
			CurrentChargingFrames++;
			if (FullyCharged)
			{
				ShotsLoaded = MaxLoadableShots;
				if (CurrentChargingFrames == (float)MagnaCannon.FullChargeFrames)
				{
					MagnaChargeSlot = SoundEngine.PlaySound(in MagnaCannon.ChargeFull, base.Projectile.Center);
				}
				else if ((CurrentChargingFrames - (float)MagnaCannon.FullChargeFrames - (float)MagnaCannon.ChargeFullSoundFrames) % (float)MagnaCannon.ChargeLoopSoundFrames == 0f)
				{
					MagnaChargeSlot = SoundEngine.PlaySound(in MagnaCannon.ChargeLoop, base.Projectile.Center);
				}
			}
			else if (CurrentChargingFrames == 10f)
			{
				MagnaChargeSlot = SoundEngine.PlaySound(in MagnaCannon.ChargeStart, base.Projectile.Center);
			}
			if (CurrentChargingFrames >= 10f)
			{
				float orbScale = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)MagnaCannon.FullChargeFrames) / 200f;
				Vector2 gunTipPosition2 = GunTipPosition;
				newColor = Color.DodgerBlue;
				Lighting.AddLight(gunTipPosition2, ((Color)(ref newColor)).ToVector3() * orbScale);
			}
			if (CurrentChargingFrames == (float)MagnaCannon.FullChargeFrames)
			{
				for (int j = 0; j < 36; j++)
				{
					Vector2 gunTipPosition3 = GunTipPosition;
					int type2 = ModContent.DustType<SquashDust>();
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(gunTipPosition3, type2, null, 0, newColor);
					dust2.velocity = ((float)Math.PI * 2f * (float)j / 36f).ToRotationVector2() * Main.rand.NextFloat(6f, 7.5f);
					dust2.scale = Main.rand.NextFloat(2f, 2.5f);
					dust2.noGravity = true;
					dust2.color = (Main.rand.NextBool(3) ? Color.Cyan : Color.RoyalBlue);
					dust2.fadeIn = 1f;
				}
			}
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(MagnaChargeSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = value.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (!base.Owner.CantUseHoldout())
		{
			float rumble = MathHelper.Clamp(CurrentChargingFrames, 0f, (float)MagnaCannon.FullChargeFrames);
			drawPosition += Main.rand.NextVector2Circular(rumble / 43f, rumble / 43f);
		}
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Main.EntitySpriteDraw(value, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		float chargeScale = (base.KeepRefreshingLifetime ? Utils.GetLerpValue(0f, MagnaCannon.FullChargeFrames, CurrentChargingFrames, clamped: true) : 0f);
		for (int i = 0; i < 3; i++)
		{
			Texture2D value2 = tex2.Value;
			Vector2 position = GunTipPosition - Main.screenPosition;
			Color val = Color.Lerp(FullyCharged ? Color.DodgerBlue : Color.RoyalBlue, Color.White, (float)i * 0.25f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, position, null, val * 0.8f, Main.rand.NextFloat(-5f, 5f), tex2.Size() * 0.5f, new Vector2(1.35f, 1f) * base.Projectile.scale * chargeScale * (1f - 0.27f * (float)i) * 0.25f * ((chargeScale >= 1f && CurrentChargingFrames <= (float)(MagnaCannon.FullChargeFrames + 3)) ? 1.75f : (FullyCharged ? 1.4f : 1f)), (SpriteEffects)0);
		}
		return false;
	}
}
