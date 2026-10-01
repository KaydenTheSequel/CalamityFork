using System;
using System.IO;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NanoblackPiercingStrike : ModProjectile, ILocalizedModType, IModType
{
	internal const float MaxBeamLength = 1800f;

	private const float BeamTileCollisionWidth = 1f;

	private const float BeamHitboxCollisionWidth = 40f;

	private const int NumSamplePoints = 9;

	private const float BeamLightBrightness = 0.5f;

	internal const float ProgressiveState_Initial = 0f;

	internal const float ProgressiveState_BeamCalculated = 1f;

	internal const float ProgressiveState_HasDealtDamage = 2f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	internal Vector2 TargetPos
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(base.Projectile.ai[1], base.Projectile.ai[2]);
		}
	}

	internal ref float VisibleBeamLength => ref base.Projectile.localAI[0];

	internal ref float ProgressiveState => ref base.Projectile.localAI[1];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 6;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(VisibleBeamLength);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		VisibleBeamLength = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0)
		{
			ProgressiveState = 2f;
		}
		Vector2 fakeVel = (TargetPos - base.Projectile.Center).SafeNormalize(-Vector2.UnitY);
		if (ProgressiveState == 0f)
		{
			Vector2 center = base.Projectile.Center;
			float[] laserScanResults = new float[9];
			Collision.LaserScan(center, fakeVel, 1f, 1800f, laserScanResults);
			float averageSampledLength = 0f;
			for (int i = 0; i < laserScanResults.Length; i++)
			{
				averageSampledLength += laserScanResults[i];
			}
			averageSampledLength /= 9f;
			VisibleBeamLength = averageSampledLength;
			float distToTarget = base.Projectile.Center.Distance(TargetPos);
			float minOvershoot = 224f;
			float maxOvershoot = 320f;
			if (VisibleBeamLength < distToTarget || VisibleBeamLength > distToTarget + maxOvershoot)
			{
				float randomOvershoot = Main.rand.NextFloat(minOvershoot, maxOvershoot);
				VisibleBeamLength = distToTarget + randomOvershoot;
			}
			ProgressiveState = 1f;
		}
		Vector2 beamDims = default(Vector2);
		((Vector2)(ref beamDims))._002Ector(VisibleBeamLength, (float)base.Projectile.width * base.Projectile.scale);
		PiercingStrikeEffects();
		if (Main.netMode != 2)
		{
			WaterShaderData obj = (WaterShaderData)Filters.Scene["WaterDistortion"].GetShader();
			float waveSine = 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 20f);
			Vector2 ripplePos = base.Projectile.position + Utils.RotatedBy(new Vector2(beamDims.X * 0.5f, 0f), (double)base.Projectile.rotation, default(Vector2));
			Color waveData = new Color(0.5f, 0.1f * (float)Math.Sign(waveSine) + 0.5f, 0f, 1f) * Math.Abs(waveSine);
			obj.QueueRipple(ripplePos, waveData, beamDims, RippleShape.Square, base.Projectile.rotation);
		}
		Vector2 lightDest = base.Projectile.Center + fakeVel * VisibleBeamLength;
		Color piercingStrikeColor = NanoblackReaper.PiercingStrikeColor;
		DelegateMethods.v3_1 = ((Color)(ref piercingStrikeColor)).ToVector3() * 0.5f;
		Utils.PlotTileLine(base.Projectile.Center, lightDest, beamDims.Y, DelegateMethods.CastLight);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = float.NaN;
		Vector2 fakeVel = (TargetPos - base.Projectile.Center).SafeNormalize(-Vector2.UnitY);
		Vector2 endPoint = base.Projectile.Center + fakeVel * VisibleBeamLength;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, endPoint, 40f * base.Projectile.scale, ref _);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (ProgressiveState != 2f)
		{
			return null;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.penetrate++;
	}

	private void PiercingStrikeEffects()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		Vector2 fakeVel = (TargetPos - base.Projectile.Center).SafeNormalize(-Vector2.UnitY);
		Vector2 endPoint = base.Projectile.Center + fakeVel * VisibleBeamLength;
		int iter = 5 - base.Projectile.timeLeft;
		float startLerp = (float)iter * 0.15f;
		float endLerp = 0.6f + (float)iter * 0.15f;
		Vector2 start = Vector2.Lerp(base.Projectile.Center, endPoint, startLerp);
		Vector2 segmentEnd = Vector2.Lerp(base.Projectile.Center, endPoint, endLerp);
		float particleSpeed = Main.rand.NextFloat(34f, 40f);
		Vector2 particleVel = fakeVel.RotatedByRandom(0.10471975803375244) * particleSpeed;
		int lifetime = 5;
		float xScale = 0.017f;
		float xShrink = 0.66f;
		GeneralParticleHandler.SpawnParticle(new StaticGlowLine(start, segmentEnd, particleVel, lifetime, xScale, xShrink, NanoblackReaper.PiercingStrikeColor));
	}
}
