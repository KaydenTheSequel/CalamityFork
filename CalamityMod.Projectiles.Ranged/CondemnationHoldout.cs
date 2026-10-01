using System;
using System.IO;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class CondemnationHoldout : BaseGunHoldoutProjectile
{
	private float storedVelocity = 1f;

	public const float velocityMultiplier = 1.2f;

	public bool homing;

	public bool playShootSound = true;

	public override int AssociatedItemID => ModContent.ItemType<Condemnation>();

	public override float MaxOffsetLengthFromArm => 25f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private ref float ArrowsLoaded => ref base.Projectile.ai[1];

	private ref float FramesToLoadNextArrow => ref base.Projectile.localAI[0];

	public override void KillHoldoutLogic()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Owner.CantUseHoldout())
		{
			return;
		}
		if (ArrowsLoaded <= 0f)
		{
			base.Projectile.Kill();
			return;
		}
		if (homing && playShootSound)
		{
			SoundStyle style = SoundID.DD2_BallistaTowerShot with
			{
				Pitch = 0.2f,
				MaxInstances = 2
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = SoundID.DD2_BallistaTowerShot with
			{
				Pitch = -0.4f,
				MaxInstances = 2
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			playShootSound = false;
		}
		ShootProjectiles(homing);
		ArrowsLoaded--;
		if (ArrowsLoaded == 0f && homing)
		{
			homing = false;
		}
	}

	public override void HoldoutAI()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.CantUseHoldout())
		{
			return;
		}
		if (FramesToLoadNextArrow == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
			FramesToLoadNextArrow = base.HeldItem.useAnimation;
		}
		if (ArrowsLoaded <= 0f || ArrowsLoaded >= (float)Condemnation.MaxLoadedArrows || !base.Owner.HasAmmo(base.Owner.HeldItem))
		{
			SpawnCannotLoadArrowsDust(GunTipPosition);
		}
		if (!base.Owner.HasAmmo(base.HeldItem))
		{
			return;
		}
		CurrentChargingFrames++;
		if (!(CurrentChargingFrames >= FramesToLoadNextArrow) || !(ArrowsLoaded < (float)Condemnation.MaxLoadedArrows))
		{
			return;
		}
		base.Owner.PickAmmo(base.HeldItem, out var _, out var shootSpeed, out var damage, out var knockback, out var _);
		base.Projectile.damage = damage;
		base.Projectile.knockBack = knockback;
		storedVelocity = shootSpeed * 1.2f;
		SpawnArrowLoadedDust();
		CurrentChargingFrames = 0f;
		ArrowsLoaded++;
		FramesToLoadNextArrow = MathHelper.Clamp(FramesToLoadNextArrow--, 1f, (float)base.HeldItem.useAnimation);
		SoundStyle style = SoundID.Item108 with
		{
			Volume = SoundID.Item108.Volume * 0.4f,
			Pitch = -0.3f + ArrowsLoaded * 0.1f
		};
		SoundEngine.PlaySound(in style);
		if (ArrowsLoaded >= (float)Condemnation.MaxLoadedArrows)
		{
			SoundStyle fire = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/BrimflameRecharge");
			for (int i = 0; i < 3; i++)
			{
				SoundEngine.PlaySound(fire with
				{
					Volume = 0.8f,
					Pitch = (float)i * 0.25f,
					MaxInstances = 3
				});
			}
			homing = true;
		}
	}

	public void SpawnArrowLoadedDust()
	{
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		if (ArrowsLoaded >= (float)Condemnation.MaxLoadedArrows - 1f)
		{
			for (int i = 0; i < 5; i++)
			{
				float angle = 4.712389f - (float)i * ((float)Math.PI * 2f) / 5f;
				float f = 4.712389f - (float)(i + 2) * ((float)Math.PI * 2f) / 5f;
				Vector2 start = angle.ToRotationVector2();
				Vector2 end = f.ToRotationVector2();
				for (int j = 0; j < 40; j++)
				{
					Dust dust = Dust.NewDustPerfect(GunTipPosition, 267);
					dust.scale = 2.5f;
					dust.velocity = Vector2.Lerp(start, end, (float)j / 40f) * 16f;
					dust.color = Color.Crimson;
					dust.noGravity = true;
				}
			}
		}
		else
		{
			for (int k = 0; k < 36; k++)
			{
				Dust dust2 = Dust.NewDustPerfect(GunTipPosition, 267);
				dust2.velocity = ((float)Math.PI * 2f * (float)k / 36f).ToRotationVector2() * 5f + base.Owner.velocity;
				dust2.scale = Main.rand.NextFloat(1f, 1.5f);
				dust2.color = Color.Violet;
				dust2.noGravity = true;
			}
		}
	}

	public void SpawnCannotLoadArrowsDust(Vector2 GunTipPosition)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust chargeMagic = Dust.NewDustPerfect(GunTipPosition + Main.rand.NextVector2Circular(20f, 20f), 267);
				chargeMagic.velocity = (GunTipPosition - chargeMagic.position) * 0.1f + base.Owner.velocity;
				chargeMagic.scale = Main.rand.NextFloat(1f, 1.5f);
				chargeMagic.color = base.Projectile.GetAlpha(Color.White);
				chargeMagic.noGravity = true;
			}
		}
	}

	public void ShootProjectiles(bool homing)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * storedVelocity;
			int ArrowType = (homing ? ModContent.ProjectileType<CondemnationArrowHoming>() : ModContent.ProjectileType<CondemnationArrow>());
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity, ArrowType, (int)((float)base.Projectile.damage * 1.35f), base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.rand.NextFloat(0.9f, 1f);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.direction == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI * (float)base.Owner.direction) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (homing)
		{
			for (int i = 0; i < 22; i++)
			{
				Color val = Color.Lerp(Color.Red, Color.White, (float)i * 0.01f);
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.6f;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 22f).ToRotationVector2() * 5f + Main.rand.NextVector2Circular(7f, 7f);
				Main.EntitySpriteDraw(texture, drawPosition + drawOffset, null, auraColor, drawRotation, rotationPoint, base.Projectile.scale * Main.rand.NextFloat(0.7f, 1.1f), flipSprite);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		return false;
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(FramesToLoadNextArrow);
		writer.Write(storedVelocity);
		writer.Write(homing);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		FramesToLoadNextArrow = reader.ReadSingle();
		storedVelocity = reader.ReadSingle();
		homing = reader.ReadBoolean();
	}
}
