using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class ExoskeletonGaussNukeCannon : ExoskeletonCannon
{
	public int NukeRebuildTime => ShootRate - 45;

	public ref float RebuildTimer => ref base.Projectile.localAI[0];

	public override int ShootRate => 240;

	public override float ShootSpeed => 16f;

	public override Vector2 OwnerRestingOffset
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return ExoskeletonCannon.HoverOffsetTable[base.HoverOffsetIndex];
		}
	}

	public override void ClampFirstLimbRotation(ref double limbRotation)
	{
		limbRotation = ExoskeletonCannon.RotationalClampTable[base.HoverOffsetIndex];
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
	}

	public override void ShootAtTarget(NPC target, Vector2 shootDirection)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = CommonCalamitySounds.LargeWeaponFireSound with
		{
			Volume = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		RebuildTimer = 1f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			int nukeID = ModContent.ProjectileType<MinionGaussNuke>();
			Vector2 laserVelocity = shootDirection * ShootSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, laserVelocity, nukeID, (int)((float)base.Projectile.damage * 1f), 0f, base.Projectile.owner);
		}
	}

	public override void PostAI()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % 6;
		if (RebuildTimer >= 1f)
		{
			RebuildTimer++;
			base.Projectile.frame += 6;
		}
		if (RebuildTimer >= (float)NukeRebuildTime)
		{
			RebuildTimer = 0f;
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonGaussNukeCannonGlowmask", (AssetRequestMode)2).Value;
		Texture2D stabilizer = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonGaussNukeStabilizer", (AssetRequestMode)2).Value;
		Texture2D stabilizer2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonGaussNukeStabilizerBottom", (AssetRequestMode)2).Value;
		Texture2D crystal = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonGaussNukeCrystal", (AssetRequestMode)2).Value;
		Texture2D core = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonGaussNukeCore", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		float stabilizerRotationInterpolant = (float)Math.Pow(Utils.GetLerpValue((float)NukeRebuildTime * 0.75f, (float)NukeRebuildTime * 0.92f, RebuildTimer, clamped: true), 1.7);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 crystalOrigin = crystal.Size() * new Vector2((base.Projectile.spriteDirection == 1) ? 1f : 0f, 0.5f);
		Vector2 stabilizerOrigin = stabilizer.Size() * new Vector2(0.5f, 1f);
		Vector2 coreOrigin = core.Size() * new Vector2((base.Projectile.spriteDirection == 1) ? 0f : 1f, 0.5f);
		Vector2 perpendicularStabilizerOffset = (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * base.Projectile.scale * MathHelper.Lerp(18f, 10f, stabilizerRotationInterpolant);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		float rotation = base.Projectile.rotation;
		if (base.Projectile.spriteDirection == -1)
		{
			rotation += (float)Math.PI;
		}
		DrawLimbs();
		if (RebuildTimer >= 1f)
		{
			float stabilizerDirection = rotation + (float)Math.PI;
			if (base.Projectile.spriteDirection == -1)
			{
				stabilizerDirection += (float)Math.PI;
			}
			Vector2 aimDirection = base.Projectile.rotation.ToRotationVector2();
			float crystalOffsetInterpolant = Utils.GetLerpValue(0f, (float)NukeRebuildTime * 0.5f, RebuildTimer, clamped: true);
			Vector2 crystalOffset = aimDirection * crystalOffsetInterpolant * base.Projectile.scale * ((float)crystal.Width - 15f);
			Main.EntitySpriteDraw(crystal, drawPosition + crystalOffset, null, base.Projectile.GetAlpha(lightColor), rotation, crystalOrigin, base.Projectile.scale, direction);
			float stabilizerOffsetInterpolant = Utils.GetLerpValue((float)NukeRebuildTime * 0.5f, (float)NukeRebuildTime * 0.75f, RebuildTimer, clamped: true);
			float stabilizerOffsetMagnitude = MathHelper.Lerp((float)(-stabilizer.Width) * 0.32f, (float)stabilizer.Width * 0.4f - 8f, stabilizerOffsetInterpolant) * base.Projectile.scale;
			float leftStabilizerRotation = stabilizerDirection + (float)Math.PI - (1f - stabilizerRotationInterpolant) * 0.42f;
			float rightStabilizerRotation = stabilizerDirection + (1f - stabilizerRotationInterpolant) * 0.42f;
			Vector2 stabilizerOffset = aimDirection * stabilizerOffsetMagnitude;
			Color stabilizerColor = base.Projectile.GetAlpha(lightColor) * (float)Math.Pow(stabilizerOffsetInterpolant, 1.61);
			Vector2 leftStabilizerPosition = drawPosition + stabilizerOffset - perpendicularStabilizerOffset * 0.9f;
			Vector2 rightStabilizerPosition = drawPosition + stabilizerOffset + perpendicularStabilizerOffset;
			SpriteEffects leftStabilizerDirection = direction;
			SpriteEffects rightStabilizerDirection = (SpriteEffects)(direction ^ 1);
			if (base.Projectile.spriteDirection == -1)
			{
				leftStabilizerDirection = (SpriteEffects)(leftStabilizerDirection ^ 1);
				rightStabilizerDirection = (SpriteEffects)(rightStabilizerDirection ^ 1);
				Utils.Swap(ref stabilizer, ref stabilizer2);
			}
			Main.EntitySpriteDraw(stabilizer, leftStabilizerPosition, null, stabilizerColor, leftStabilizerRotation, stabilizerOrigin, base.Projectile.scale, leftStabilizerDirection);
			Main.EntitySpriteDraw(stabilizer2, rightStabilizerPosition, null, stabilizerColor, rightStabilizerRotation, stabilizerOrigin, base.Projectile.scale, rightStabilizerDirection);
			Vector2 coreDrawPosition = drawPosition + aimDirection * base.Projectile.scale * -12f;
			Main.EntitySpriteDraw(core, coreDrawPosition, null, base.Projectile.GetAlpha(lightColor) * stabilizerRotationInterpolant, rotation, coreOrigin, base.Projectile.scale, direction);
		}
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, base.Projectile.GetAlpha(Color.White), rotation, origin, base.Projectile.scale, direction);
		return false;
	}
}
