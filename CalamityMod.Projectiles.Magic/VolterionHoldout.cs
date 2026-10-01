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

public class VolterionHoldout : BaseGunHoldoutProjectile
{
	public int Time;

	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/VolterionFire")
	{
		Volume = 0.35f
	};

	public SlotId FireSoundSlot;

	public static Asset<Texture2D> MuzzleFlash;

	public override int AssociatedItemID => ModContent.ItemType<Volterion>();

	public override float MaxOffsetLengthFromArm => 64f;

	public override float BaseOffsetY => -16f;

	public override float OffsetXUpwards => -16f;

	public override float OffsetXDownwards => 12f;

	public override float OffsetYUpwards => 6f;

	public override float OffsetYDownwards => 20f;

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
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.3f;
		}
	}

	public override string Texture => "CalamityMod/Projectiles/Magic/VolterionHoldout";

	public ref float FlashTimer => ref base.Projectile.ai[0];

	public override void Load()
	{
		MuzzleFlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/VolterionHoldoutFlash", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 15;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void KillHoldoutLogic()
	{
		if (base.HeldItem.type != base.Owner.HeldItem.type || base.Owner.dead || !base.Owner.active)
		{
			base.Projectile.Kill();
			base.Projectile.netUpdate = true;
		}
	}

	public override void HoldoutAI()
	{
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.damage = ((base.HeldItem != null) ? base.Owner.GetWeaponDamage(base.HeldItem) : 0);
		if (FlashTimer > 0f)
		{
			FlashTimer -= 0.5f;
		}
		if (base.Projectile.frame == 14)
		{
			base.Projectile.frameCounter++;
			if ((float)base.Projectile.frameCounter >= MathHelper.Clamp((float)(base.Owner.HeldItem.useAnimation - 42), 0f, (float)base.Owner.HeldItem.useAnimation))
			{
				if (base.Owner.CantUseHoldout() || !base.Owner.CheckMana(base.Owner.HeldItem))
				{
					base.Projectile.Kill();
				}
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
			}
		}
		else if (base.Projectile.frame == 0)
		{
			if (base.Projectile.frameCounter != 1)
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter >= 3)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				return;
			}
			base.Projectile.frameCounter++;
			if (base.Owner.CheckMana(base.Owner.HeldItem, -1, pay: true))
			{
				FlashTimer = 4f;
				FireSoundSlot = SoundEngine.PlaySound(in FireSound, GunTipPosition);
				base.Owner.SetScreenshake(3f);
				Vector2 offsetPos = GunTipPosition - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 18f;
				Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.HeldItem.shootSpeed;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), offsetPos, velocity, ModContent.ProjectileType<VolterionShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				for (int i = 0; i < 8; i++)
				{
					float scale = Main.rand.NextFloat(0.6f, 1.25f);
					Vector2 randVelocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(8f, 12f);
					GeneralParticleHandler.SpawnParticle(new PointParticle(offsetPos, randVelocity, affectedByGravity: false, 15, scale, new Color(51, 197, 255)));
				}
			}
		}
		else if (base.Projectile.frame > 0)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= 3)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
		}
		if (SoundEngine.TryGetActiveSound(FireSoundSlot, out ActiveSound currentSound) && currentSound.IsPlaying)
		{
			currentSound.Position = GunTipPosition;
		}
	}

	public override bool? CanDamage()
	{
		return FlashTimer > 0f;
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
		return CalamityUtils.CircularHitboxCollision(GunTipPosition + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)MuzzleFlash.Width() * 0.4f, 64f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 2)
		{
			return false;
		}
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		float scale = base.Projectile.scale * base.Owner.gravDir;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, frame.Size() * 0.5f, scale, flipSprite);
		if (FlashTimer > 0f)
		{
			Texture2D value2 = MuzzleFlash.Value;
			Vector2 flashPos = GunTipPosition + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)MuzzleFlash.Width() * 0.5f - Main.screenPosition;
			Rectangle flashFrame = value2.Frame(1, 4, 0, (int)(4f - FlashTimer));
			Main.EntitySpriteDraw(value2, flashPos, flashFrame, Color.White, drawRotation, flashFrame.Size() * 0.5f, scale, flipSprite);
		}
		return false;
	}
}
