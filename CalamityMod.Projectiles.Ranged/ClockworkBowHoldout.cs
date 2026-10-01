using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ClockworkBowHoldout : ModProjectile
{
	private float storedVelocity = 1f;

	private float angularSpread = MathHelper.ToRadians(16f);

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<ClockworkBow>();

	private Player Owner => Main.player[base.Projectile.owner];

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private ref float LoadedBolts => ref base.Projectile.ai[1];

	private ref float FramesToLoadBolt => ref base.Projectile.localAI[0];

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/ClockworkBow";

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 96;
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
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		Vector2 tipPosition = armPosition + base.Projectile.velocity * (float)base.Projectile.width * 0.5f;
		if (Owner.CantUseHoldout())
		{
			if (LoadedBolts <= 0f)
			{
				base.Projectile.Kill();
				return;
			}
			UnloadBolts(tipPosition);
		}
		else
		{
			if (FramesToLoadBolt == 0f)
			{
				FramesToLoadBolt = Owner.HeldItem.useAnimation;
			}
			if (Owner.HasAmmo(Owner.HeldItem))
			{
				CurrentChargingFrames++;
				if ((double)(CurrentChargingFrames - FramesToLoadBolt / 2f) <= 0.01)
				{
					SoundEngine.PlaySound(in SoundID.Item17);
				}
				if (CurrentChargingFrames >= FramesToLoadBolt && LoadedBolts < 6f)
				{
					Item heldItem = Owner.HeldItem;
					Owner.PickAmmo(heldItem, out var _, out var shootSpeed, out var damage, out var knockback, out var _);
					base.Projectile.damage = damage;
					base.Projectile.knockBack = knockback;
					storedVelocity = shootSpeed;
					CurrentChargingFrames = 0f;
					LoadedBolts++;
					if (LoadedBolts % 2f == 0f)
					{
						CombatText.NewText(Owner.Hitbox, new Color(155, 255, 255), CalamityUtils.GetTextValue("Misc.ClockworkTock"), dramatic: true);
					}
					else
					{
						CombatText.NewText(Owner.Hitbox, new Color(255, 200, 100), CalamityUtils.GetTextValue("Misc.ClockworkTick"), dramatic: true);
					}
					FramesToLoadBolt *= 0.95f;
					if (LoadedBolts >= 6f)
					{
						SoundEngine.PlaySound(in SoundID.Item23);
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
			}
		}
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
	}

	public void UnloadBolts(Vector2 tipPosition)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (LoadedBolts == 6f)
		{
			for (int i = 0; (float)i < LoadedBolts; i++)
			{
				float increment = angularSpread * (LoadedBolts - 1f) / 2f;
				float spreadForThisProjectile = MathHelper.Lerp(0f - increment, increment, (float)i / (LoadedBolts - 1f));
				ShootProjectiles(tipPosition, spreadForThisProjectile);
			}
			SoundEngine.PlaySound(in SoundID.Item38);
		}
		else if (LoadedBolts != 0f)
		{
			for (int j = 0; (float)j < LoadedBolts + 1f; j++)
			{
				if ((float)j != LoadedBolts)
				{
					float increment2 = angularSpread * (LoadedBolts - 1f + MathHelper.Clamp(CurrentChargingFrames / FramesToLoadBolt * 2f, 0f, 1f)) / 2f;
					float spreadForThisProjectile2 = MathHelper.Lerp(0f - increment2, increment2, (float)j / MathHelper.Lerp(LoadedBolts - 1f, LoadedBolts, MathHelper.Clamp(CurrentChargingFrames * 2f / FramesToLoadBolt, 0f, 1f)));
					ShootProjectiles(tipPosition, spreadForThisProjectile2);
				}
			}
			SoundEngine.PlaySound(in SoundID.Item38);
		}
		FramesToLoadBolt = Owner.HeldItem.useAnimation;
		LoadedBolts = 0f;
	}

	public void ShootProjectiles(Vector2 tipPosition, float projectileRotation)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(projectileRotation) * storedVelocity;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, shootVelocity, ModContent.ProjectileType<PrecisionBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	private void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			float interpolant = Utils.GetLerpValue(5f, 25f, Owner.Distance(Owner.ClampedMouseWorld()), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, Owner.SafeDirectionTo(Main.MouseWorld), interpolant);
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
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
		base.Projectile.timeLeft = 3;
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
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		float loops = LoadedBolts + 1f;
		if (LoadedBolts == 6f)
		{
			loops = LoadedBolts;
		}
		Vector2 PointingTo = default(Vector2);
		for (int i = 0; (float)i < loops; i++)
		{
			float Shift = 0f;
			float BoltAngle;
			if (LoadedBolts == 0f)
			{
				BoltAngle = 0f;
			}
			else if (LoadedBolts == 6f)
			{
				float increment = angularSpread * (LoadedBolts - 1f) / 2f;
				BoltAngle = MathHelper.Lerp(0f - increment, increment, (float)i / (LoadedBolts - 1f));
			}
			else
			{
				float increment2 = angularSpread * (LoadedBolts - 1f + MathHelper.Clamp(CurrentChargingFrames * 2f / FramesToLoadBolt, 0f, 1f)) / 2f;
				BoltAngle = MathHelper.Lerp(0f - increment2, increment2, (float)i / MathHelper.Lerp(LoadedBolts - 1f, LoadedBolts, MathHelper.Clamp(CurrentChargingFrames * 2f / FramesToLoadBolt, 0f, 1f)));
			}
			if ((float)i == LoadedBolts)
			{
				Shift = 1f - CurrentChargingFrames / FramesToLoadBolt;
			}
			if (((float)i == LoadedBolts - 1f || LoadedBolts == 6f) && Owner.HasAmmo(Owner.HeldItem))
			{
				Main.spriteBatch.EnterShaderRegion();
				GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f - MathHelper.Clamp(CurrentChargingFrames * 2f / FramesToLoadBolt, 0f, 1f));
				GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - LoadedBolts / 6f, 0.8f, 0.85f));
				GameShaders.Misc["CalamityMod:BasicTint"].Apply();
			}
			Color Transparency = Color.White * (1f - Shift);
			Texture2D BoltTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/PrecisionBolt", (AssetRequestMode)2).Value;
			((Vector2)(ref PointingTo))._002Ector((float)Math.Cos(base.Projectile.rotation + BoltAngle), (float)Math.Sin(base.Projectile.rotation + BoltAngle));
			Vector2 ShiftDown = PointingTo.RotatedBy(-1.5707963705062866);
			float FlipFactor = ((Owner.direction < 0) ? ((float)Math.PI) : 0f);
			Vector2 drawPosition = Owner.Center + PointingTo.RotatedBy(FlipFactor) * (20f + Shift * 40f) - ShiftDown.RotatedBy(FlipFactor) * (float)(BoltTexture.Width / 2) - Main.screenPosition;
			Main.EntitySpriteDraw(BoltTexture, drawPosition, null, Transparency, base.Projectile.rotation + BoltAngle + (float)Math.PI / 2f + FlipFactor, BoltTexture.Size(), 1f, (SpriteEffects)0);
			if (((float)i == LoadedBolts - 1f || LoadedBolts == 6f) && Owner.HasAmmo(Owner.HeldItem))
			{
				Main.spriteBatch.ExitShaderRegion();
			}
		}
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
