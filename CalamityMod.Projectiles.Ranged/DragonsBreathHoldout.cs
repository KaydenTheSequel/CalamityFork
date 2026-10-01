using System;
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

public class DragonsBreathHoldout : BaseGunHoldoutProjectile
{
	public int fireSpeed = 1;

	public bool firingBeam;

	public int weldingTimer;

	public SlotId WeldSoundSlot;

	public bool hasLaunchedMag;

	public float fade;

	public override int AssociatedItemID => ModContent.ItemType<DragonsBreath>();

	public override float MaxOffsetLengthFromArm => 60f;

	public override float OffsetXUpwards => -10f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 5f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - base.Projectile.velocity.RotatedBy(0.6f * (float)base.Projectile.direction) * 10f;
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public ref float fireTimer => ref base.Projectile.ai[1];

	public override void HoldoutAI()
	{
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		fade = MathHelper.Lerp(fade, 0f, 0.25f);
		if (Time == 0f)
		{
			weldingTimer = base.Owner.itemAnimationMax * 6;
		}
		if (firingBeam)
		{
			if (weldingTimer > 0)
			{
				if (SoundEngine.TryGetActiveSound(WeldSoundSlot, out ActiveSound WeldSound) && WeldSound.IsPlaying)
				{
					WeldSound.Position = base.Projectile.Center;
				}
				if (base.Owner.Calamity().DragonsBreathAudioCooldown2 == 0)
				{
					base.Owner.Calamity().DragonsBreathAudioCooldown2 = 30;
					WeldSoundSlot = SoundEngine.PlaySound(in DragonsBreath.WeldingShoot, base.Projectile.Center);
				}
				if (weldingTimer % 2 == 0 && Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f, ModContent.ProjectileType<DragonsBreathFlames>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner, 1f, weldingTimer, (Time % 15f < 10f) ? 18 : 0);
				}
				weldingTimer--;
				if (Time % 3f == 0f)
				{
					base.OffsetLengthFromArm = 54f;
				}
			}
			else
			{
				Time = -1f;
				firingBeam = false;
				fireSpeed = 1;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
				style.PitchVariance = 0.15f;
				style.Volume = 0.75f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 6f).RotatedBy(-2.3f * (float)base.Projectile.direction), ModContent.ProjectileType<DragonsBreathMag>(), 1, base.Projectile.knockBack, base.Projectile.owner, 1f);
				}
				hasLaunchedMag = true;
			}
		}
		else
		{
			if (fireTimer >= (float)base.Owner.itemTimeMax && Main.myPlayer == base.Projectile.owner)
			{
				base.Owner.PickAmmo(base.Owner.HeldItem, out var _, out var shootSpeed, out var damage, out var knockback, out var _);
				for (int i = 0; i < 3; i++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.15f * (float)i * 0.5f) * shootSpeed * i switch
					{
						1 => 0.7f, 
						0 => 0.5f, 
						_ => 1f, 
					} * Main.rand.NextFloat(0.8f, 1f), ModContent.ProjectileType<DragonsBreathFlames>(), damage, knockback, base.Projectile.owner);
				}
				SoundEngine.PlaySound(in DragonsBreath.FireballSound, base.Projectile.Center);
				hasLaunchedMag = false;
				fade = 1f;
				base.OffsetLengthFromArm = 52f;
				fireSpeed++;
				fireTimer = 0f;
				if (fireSpeed == 20)
				{
					base.OffsetLengthFromArm = 32f;
					firingBeam = true;
					SoundEngine.PlaySound(in DragonsBreath.WeldingStart, base.Projectile.Center);
				}
			}
			fireTimer += 1f + (float)fireSpeed * 0.15f;
		}
		Time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 2f)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		float rotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 origin = texture.Size() * 0.5f;
		SpriteEffects effects = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Color val;
		if (firingBeam)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 position2 = position + Main.rand.NextVector2Circular(10f, 10f);
				val = Color.Orange;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(texture, position2, null, val * 0.6f, rotation, origin, base.Projectile.scale * base.Owner.gravDir, effects);
			}
		}
		Main.EntitySpriteDraw(texture, position, null, base.Projectile.GetAlpha(lightColor), rotation + (hasLaunchedMag ? (-0.5f * Utils.GetLerpValue((float)base.Owner.itemAnimationMax * 0.2f, 0f, fireTimer, clamped: true) * (float)base.Projectile.direction) : 0f), origin, base.Projectile.scale * base.Owner.gravDir, effects);
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 drawPosition = GunTipPosition - Main.screenPosition + base.Projectile.velocity * 3f;
		_ = base.Projectile.rotation;
		_ = base.Projectile.spriteDirection;
		_ = -1;
		_ = texture.Size() * 0.5f;
		val = Color.OrangeRed;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, drawPosition, null, val * fade, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.4f, (SpriteEffects)0);
		val = Color.Orange;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, drawPosition, null, val * 0.75f * fade, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.25f, (SpriteEffects)0);
		return false;
	}
}
