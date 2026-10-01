using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FirestormCannonHoldout : BaseGunHoldoutProjectile
{
	public static readonly SoundStyle WarningSound = new SoundStyle("CalamityMod/Sounds/Item/FireImplosion")
	{
		Volume = 0.75f
	};

	public static readonly SoundStyle OverheatSound = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashSteamRelease");

	public SlotId WarningSlot;

	private bool displayOverheat;

	private const int WarningTime = 380;

	public override int AssociatedItemID => ModContent.ItemType<FirestormCannon>();

	public override float MaxOffsetLengthFromArm => 16f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.35f;
		}
	}

	public ref float Timer => ref base.Projectile.ai[0];

	private int BuiltHeat => (base.Owner.HeldItem.ModItem as FirestormCannon).BuiltUpHeat;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void KillHoldoutLogic()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.CantUseHoldout(needsToHold: false) || base.HeldItem.type != base.Owner.HeldItem.type || (base.Owner.Calamity().mouseRight && !displayOverheat) || (BuiltHeat == 0 && !Main.mouseLeft))
		{
			if (SoundEngine.TryGetActiveSound(WarningSlot, out ActiveSound warn))
			{
				warn.Stop();
			}
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		if (Main.mouseLeft && base.Owner.Calamity().flareGunOverheat == 0)
		{
			Timer++;
		}
		else
		{
			Timer = 0f;
		}
		if (Timer >= 30f)
		{
			(base.Owner.HeldItem.ModItem as FirestormCannon).BuiltUpHeat++;
			if (BuiltHeat >= 480)
			{
				WarningSlot = SoundEngine.PlaySound(in OverheatSound, base.Owner.Center);
				for (int e = 0; e < 7; e++)
				{
					Vector2 dustVel = -base.Projectile.rotation.ToRotationVector2().RotatedByRandom(0.4712389409542084) * Main.rand.NextFloat(3.8f, 5.5f);
					Dust.NewDustPerfect(base.Projectile.Center, 127, dustVel, 0, default(Color), 1.5f).noGravity = true;
				}
				base.Owner.Hurt(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.FlareGunOverheat").ToNetworkText(base.Owner.name)), 20, 1, pvp: false, quiet: false, -1, dodgeable: false, 0f, 1f, 0f);
				base.Owner.AddBuff(24, 180);
				base.Owner.Calamity().flareGunOverheat = 180;
				displayOverheat = true;
				return;
			}
			if (BuiltHeat == 380)
			{
				WarningSlot = SoundEngine.PlaySound(in WarningSound, base.Owner.Center);
			}
			float firingLerp = Utils.GetLerpValue(0f, 90f, Timer - 30f, clamped: true);
			int firingFrequency = (int)MathHelper.Lerp((float)base.HeldItem.useTime, (float)(base.HeldItem.useTime / 2), firingLerp);
			if (Timer % (float)firingFrequency == 0f)
			{
				SoundStyle style = SoundID.Item11 with
				{
					Volume = 0.8f
				};
				SoundEngine.PlaySound(in style, base.Owner.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					base.Owner.PickAmmo(base.HeldItem, out var flareType, out var _, out var _, out var _, out var _, Main.rand.Next(100) >= 70);
					Vector2 velocity = base.Projectile.velocity.RotatedByRandom((float)Math.PI / 25f * (1f + firingLerp * 0.5f)) * base.Owner.HeldItem.shootSpeed;
					Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, velocity, flareType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 1f);
					projectile.penetrate = 3;
					projectile.MaxUpdates = 2;
					projectile.timeLeft = 600;
					projectile.DamageType = DamageClass.Ranged;
					projectile.usesIDStaticNPCImmunity = false;
					projectile.usesLocalNPCImmunity = true;
					projectile.localNPCHitCooldown = 60;
				}
			}
		}
		if (BuiltHeat < 380)
		{
			displayOverheat = false;
		}
		if (displayOverheat && Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, -Vector2.UnitY * 7.5f, Color.DarkGray, 25, 0.4f, 0.75f));
		}
		if (SoundEngine.TryGetActiveSound(WarningSlot, out ActiveSound warning) && warning.IsPlaying)
		{
			warning.Position = base.Projectile.Center;
		}
	}

	public override bool? CanDamage()
	{
		return BuiltHeat >= 120 && base.Owner.Calamity().flareGunOverheat == 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 300);
		(base.Owner.HeldItem.ModItem as FirestormCannon).BuiltUpHeat -= 6;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = value.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Color tintColor = (displayOverheat ? Color.Black : ((BuiltHeat >= 380) ? Color.Lerp(Color.Tomato, Color.White, MathF.Abs(MathF.Sin((float)base.Owner.miscCounter * (float)Math.PI / 30f))) : Color.Tomato));
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(Utils.GetLerpValue(0f, 380f, BuiltHeat, clamped: true) * 0.45f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(tintColor);
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		Main.EntitySpriteDraw(value, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
