using System;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AnomalysNanogunPlasmaBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/Magic/YharimsCrystalBeam";

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/AnomalysNanogunPlasmaBeam", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => LaserBeginTexture;

	public override Texture2D LaserEndTexture => LaserBeginTexture;

	public override float MaxScale => 1f;

	public override float Lifetime => 12f;

	public override float MaxLaserLength => 5500f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.OrangeRed;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.scale = MaxScale;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 12;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		Main.projFrames[base.Type] = 18;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.AI();
	}

	public override void ExtraBehavior()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 12)
		{
			Vector2 beamVector = base.Projectile.velocity;
			float beamLength = DetermineLaserLength_CollideWithTiles();
			int dustCount = Main.rand.Next(10, 30);
			for (int i = 0; i < dustCount; i++)
			{
				float dustProgressAlongBeam = beamLength * Main.rand.NextFloat(0f, 0.8f);
				Dust.NewDustPerfect(base.Projectile.Center + dustProgressAlongBeam * beamVector + beamVector.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-6f, 6f) * base.Projectile.scale, 187, beamVector * Main.rand.NextFloat(5f, 26f), 0, Color.OrangeRed, 2.2f).noGravity = true;
			}
			for (int step = 10; (float)step < base.LaserLength; step += 100)
			{
				for (int j = 0; j < 2; j++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + MathHelper.WrapAngle(base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (float)step, Vector2.Zero, Color.Red, "CalamityMod/Particles/SmallBloomRing", new Vector2(0.4f, 1.1f), MathHelper.WrapAngle(base.Projectile.rotation + (float)Math.PI / 2f), 0.1f, 1f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + MathHelper.WrapAngle(base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (float)step, Vector2.Zero, Color.White, "CalamityMod/Particles/SmallBloomRing", new Vector2(0.4f, 1.1f), MathHelper.WrapAngle(base.Projectile.rotation + (float)Math.PI / 2f), 0.1f, 0.9f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
		}
		else
		{
			base.Projectile.frameCounter++;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 8; i++)
		{
			Dust.NewDustPerfect(target.Center, 218, (base.Projectile.velocity * 30f).RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f)).noGravity = true;
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void DetermineScale()
	{
		base.Projectile.scale = 1f;
	}

	public override float DetermineLaserLength()
	{
		return DetermineLaserLength_CollideWithTiles();
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.LaserLength, base.Projectile.width + 16, DelegateMethods.CutTiles);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		int frame = base.Projectile.frameCounter / 2;
		DrawPlasmaBeam(Color.White, 1f, frame + 12, frame + 6, frame);
		return false;
	}

	private void DrawPlasmaBeam(Color beamColor, float scale, int startFrame = 0, int middleFrame = 0, int endFrame = 0)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, startFrame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, middleFrame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, endFrame);
		Main.EntitySpriteDraw(LaserBeginTexture, base.Projectile.Center - Main.screenPosition, startFrameArea, beamColor, MathHelper.WrapAngle(base.Projectile.rotation + (float)Math.PI), startFrameArea.Size() / 2f, scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength;
		laserBodyLength -= (float)(startFrameArea.Height / 2 + endFrameArea.Height) * scale;
		Vector2 centerOnLaser = base.Projectile.Center;
		centerOnLaser += base.Projectile.velocity * scale * (float)startFrameArea.Height / 2f;
		if (laserBodyLength > 0f)
		{
			float laserOffset = (float)middleFrameArea.Height * scale;
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, middleFrameArea, beamColor, base.Projectile.rotation, (float)LaserMiddleTexture.Width * 0.5f * Vector2.UnitX, scale, (SpriteEffects)0);
				incrementalBodyLength += laserOffset;
				centerOnLaser += base.Projectile.velocity * laserOffset;
			}
		}
		if (Math.Abs(base.LaserLength - DetermineLaserLength()) < 30f)
		{
			Vector2 laserEndCenter = centerOnLaser - Main.screenPosition;
			Main.EntitySpriteDraw(LaserEndTexture, laserEndCenter, endFrameArea, beamColor, MathHelper.WrapAngle(base.Projectile.rotation + (float)Math.PI), endFrameArea.Size() / 2f, scale, (SpriteEffects)0);
		}
	}
}
