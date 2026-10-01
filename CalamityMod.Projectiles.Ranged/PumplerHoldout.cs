using System;
using System.IO;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PumplerHoldout : ModProjectile
{
	private float angularSpread = MathHelper.ToRadians(15f);

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Pumpler>();

	private Player Owner => Main.player[base.Projectile.owner];

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private ref float PumpkinsCharge => ref base.Projectile.ai[1];

	private ref float FramesToLoadNextPumpkin => ref base.Projectile.localAI[0];

	private ref float Overfilled => ref base.Projectile.localAI[1];

	private bool IsOverfilled => Overfilled >= 0.5f;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 9;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		Vector2 tipPosition = armPosition + base.Projectile.velocity * (float)base.Projectile.width * 0.5f;
		if (Owner.CantUseHoldout() || IsOverfilled)
		{
			if (PumpkinsCharge <= 0f && !IsOverfilled)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= 10)
			{
				base.Projectile.frame++;
				if (base.Projectile.frame >= Main.projFrames[base.Type] - 1)
				{
					base.Projectile.frame = 0;
				}
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame == 0 && IsOverfilled)
			{
				Overfilled = 0f;
			}
			if (PumpkinsCharge >= 1f)
			{
				UnloadChamber(tipPosition);
			}
		}
		else
		{
			if (FramesToLoadNextPumpkin == 0f)
			{
				FramesToLoadNextPumpkin = Owner.HeldItem.useAnimation;
			}
			CurrentChargingFrames++;
			if (CurrentChargingFrames >= FramesToLoadNextPumpkin && PumpkinsCharge < 5f)
			{
				base.Projectile.frame = base.Projectile.frame + 1;
				CurrentChargingFrames = 0f;
				PumpkinsCharge++;
				FramesToLoadNextPumpkin *= 0.85f;
				if (PumpkinsCharge >= 5f)
				{
					SoundEngine.PlaySound(in SoundID.Item69);
				}
				else
				{
					SoundStyle style = SoundID.Item108 with
					{
						Volume = SoundID.Item108.Volume * 0.3f
					};
					SoundEngine.PlaySound(in style);
				}
			}
			if (CurrentChargingFrames >= 160f)
			{
				Overfilled = 1f;
			}
		}
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
	}

	public void SmokeBurst(Vector2 tipPosition)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; (float)i < 2f * PumpkinsCharge; i++)
			{
				SmallSmokeParticle smallSmokeParticle = new SmallSmokeParticle(tipPosition + Main.rand.NextVector2Circular(10f, 10f), Vector2.Zero, Color.Orange, new Color(40, 40, 40), Main.rand.NextFloat(0.3f, 0.8f), 145 - Main.rand.Next(50));
				smallSmokeParticle.Velocity = (smallSmokeParticle.Position - Owner.Center) * 0.3f + Owner.velocity;
				GeneralParticleHandler.SpawnParticle(smallSmokeParticle);
			}
		}
	}

	public void UnloadChamber(Vector2 tipPosition)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (PumpkinsCharge == 1f)
		{
			ShootProjectiles(tipPosition, 0f);
		}
		else
		{
			for (int i = 0; (float)i < PumpkinsCharge; i++)
			{
				float spreadForThisProjectile = MathHelper.Lerp(0f - angularSpread, angularSpread, (float)i / (PumpkinsCharge - 1f));
				ShootProjectiles(tipPosition, spreadForThisProjectile);
			}
		}
		SoundEngine.PlaySound(in SoundID.Item96);
		SmokeBurst(tipPosition);
		FramesToLoadNextPumpkin = Owner.HeldItem.useAnimation;
		PumpkinsCharge = 0f;
		if (!IsOverfilled)
		{
			Overfilled = 1f;
		}
		base.Projectile.frame = 6;
	}

	public void ShootProjectiles(Vector2 tipPosition, float projectileRotation)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Item heldItem = Owner.HeldItem;
			int projectileType = ModContent.ProjectileType<PumplerGrenade>();
			int PumpkinDamage = ((heldItem != null) ? Owner.GetWeaponDamage(heldItem) : 0);
			float shootSpeed = heldItem.shootSpeed * 1.5f;
			float knockback = Owner.GetWeaponKnockback(heldItem, heldItem.knockBack);
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(projectileRotation) * shootSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, shootVelocity, projectileType, PumpkinDamage, knockback, base.Projectile.owner);
		}
	}

	private void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			float interpolant = Utils.GetLerpValue(5f, 25f, Owner.Distance(Main.MouseWorld), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, Owner.SafeDirectionTo(Main.MouseWorld), interpolant);
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 30f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 35f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		int oldDirection = base.Projectile.spriteDirection;
		if (oldDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		base.Projectile.direction = (base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		if (base.Projectile.spriteDirection != oldDirection)
		{
			base.Projectile.rotation -= (float)Math.PI;
		}
		base.Projectile.timeLeft = 2;
	}

	private void ManipulatePlayerVariables()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		if (PumpkinsCharge > 0f && !IsOverfilled)
		{
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(MathHelper.Clamp(1f - 0.2f * CurrentChargingFrames - 0.1f * (5f - PumpkinsCharge), 0f, 1f));
		}
		else
		{
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(0f);
		}
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - 0.04f * (PumpkinsCharge - 1f), 0.8f, 0.5f));
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frameRectangle = TextureAssets.Projectile[base.Type].Value.Frame(1, 9, 0, base.Projectile.frame);
		Main.EntitySpriteDraw(TextureAssets.Projectile[base.Type].Value, drawPosition, frameRectangle, lightColor, base.Projectile.rotation, frameRectangle.Size() * 0.5f, 1f, (SpriteEffects)(base.Projectile.direction == -1));
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(IsOverfilled);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Overfilled = (reader.ReadBoolean() ? 1f : 0f);
	}
}
