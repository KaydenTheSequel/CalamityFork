using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AdamAcceleratorBeam : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 24;

	private const float MaxBeamScale = 1.2f;

	private const float MaxBeamLength = 2400f;

	private const float BeamTileCollisionWidth = 1f;

	private const float BeamHitboxCollisionWidth = 15f;

	private const int NumSamplePoints = 3;

	private const float BeamLengthChangeFactor = 0.75f;

	private const float OuterBeamOpacityMultiplier = 0.82f;

	private const float InnerBeamOpacityMultiplier = 0.2f;

	private const float MaxBeamBrightness = 0.75f;

	private const float MainDustBeamEndOffset = 14.5f;

	private const float SidewaysDustBeamEndOffset = 4f;

	private const float BeamRenderTileOffset = 10.5f;

	private const float BeamLengthReductionFactor = 14.5f;

	private Vector2 beamVector;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public int DustType
	{
		get
		{
			if (!(polarity < 0f))
			{
				return 73;
			}
			return 226;
		}
	}

	public ref float polarity => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 24;
	}

	public override void AI()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.type != ModContent.ProjectileType<AdamAcceleratorBeam>())
		{
			base.Projectile.Kill();
			return;
		}
		if (base.Projectile.velocity != Vector2.Zero)
		{
			beamVector = Vector2.Normalize(base.Projectile.velocity);
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			base.Projectile.velocity = Vector2.Zero;
		}
		float power = (float)base.Projectile.timeLeft / 24f;
		base.Projectile.scale = 1.2f * power;
		float[] laserScanResults = new float[3];
		float scanWidth = ((base.Projectile.scale < 1f) ? 1f : base.Projectile.scale);
		Collision.LaserScan(base.Projectile.Center, beamVector, 1f * scanWidth, 2400f, laserScanResults);
		float avg = 0f;
		for (int i = 0; i < laserScanResults.Length; i++)
		{
			avg += laserScanResults[i];
		}
		avg /= 3f;
		base.Projectile.ai[0] = MathHelper.Lerp(base.Projectile.ai[0], avg, 0.75f);
		Vector2 beamDims = default(Vector2);
		((Vector2)(ref beamDims))._002Ector(((Vector2)(ref beamVector)).Length() * base.Projectile.ai[0], (float)base.Projectile.width * base.Projectile.scale);
		Color beamColor = GetBeamColor(polarity);
		ProduceBeamDust(beamColor);
		if (!Main.dedServ)
		{
			WaterShaderData obj = (WaterShaderData)Filters.Scene["WaterDistortion"].GetShader();
			float waveSine = 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 20f);
			Vector2 ripplePos = base.Projectile.position + Utils.RotatedBy(new Vector2(beamDims.X * 0.5f, 0f), (double)base.Projectile.rotation, default(Vector2));
			Color waveData = new Color(0.5f, 0.1f * (float)Math.Sign(waveSine) + 0.5f, 0f, 1f) * Math.Abs(waveSine);
			obj.QueueRipple(ripplePos, waveData, beamDims, RippleShape.Square, base.Projectile.rotation);
		}
		DelegateMethods.v3_1 = ((Color)(ref beamColor)).ToVector3() * power * 0.75f;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + beamVector * base.Projectile.ai[0], beamDims.Y, DelegateMethods.CastLight);
		if (base.Projectile.timeLeft == 24)
		{
			int dustCount = Main.rand.Next(10, 30);
			for (int j = 0; j < dustCount; j++)
			{
				float dustProgressAlongBeam = base.Projectile.ai[0] * Main.rand.NextFloat(0f, 0.7f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustProgressAlongBeam * beamVector + beamVector.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-6f, 6f) * base.Projectile.scale, DustType, beamVector * Main.rand.NextFloat(2f, 16f), 0, beamColor);
				dust.color = beamColor;
				dust.noGravity = true;
				dust.scale = 0.7f;
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + beamVector * base.Projectile.ai[0], 15f * base.Projectile.scale, ref _);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.GetGlobalNPC<CalamityPolarityNPC>().applyPolarity(polarity, target);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (base.Projectile.Center.X < target.Center.X).ToDirectionInt();
		float targetPolarity = target.GetGlobalNPC<CalamityPolarityNPC>().CurPolarity;
		if (polarity * targetPolarity < 0f)
		{
			modifiers.SourceDamage *= 1.2f;
			for (int i = 0; i < 4; i++)
			{
				Color sparkColor = AdamantiteParticleAccelerator.LightColors[(polarity < 0f) ? 1u : 0u];
				Vector2 sparkSpeed = Owner.DirectionTo(target.Center).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)) * Main.rand.NextFloat(8f, 17f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, sparkSpeed, Color.White, sparkColor, 0.7f + Main.rand.NextFloat(0f, 0.6f), 30, 0.4f, 0.6f));
			}
		}
	}

	private Color GetBeamColor(float polarity)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		int index = ((!(polarity > 0f)) ? 1 : 0);
		Color c = AdamantiteParticleAccelerator.LightColors[index];
		((Color)(ref c)).A = 64;
		return c;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		if (beamVector == Vector2.Zero || base.Projectile.velocity != Vector2.Zero)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float beamLength = base.Projectile.ai[0];
		Vector2 val = base.Projectile.Center.Floor() + beamVector * base.Projectile.scale * 10.5f;
		Vector2 scaleVec = default(Vector2);
		((Vector2)(ref scaleVec))._002Ector(base.Projectile.scale);
		beamLength -= 14.5f * base.Projectile.scale * base.Projectile.scale;
		DelegateMethods.f_1 = 1f;
		Vector2 beamStartPos = val - Main.screenPosition;
		Vector2 beamEndPos = beamStartPos + beamVector * beamLength;
		Utils.LaserLineFraming llf = DelegateMethods.RainbowLaserDraw;
		Color beamColor = GetBeamColor(polarity);
		DelegateMethods.c_1 = beamColor * 0.82f * base.Projectile.Opacity;
		Utils.DrawLaser(Main.spriteBatch, tex, beamStartPos, beamEndPos, scaleVec, llf);
		for (int i = 0; i < 5; i++)
		{
			beamColor = Color.Lerp(beamColor, Color.White, 0.4f);
			scaleVec *= 0.85f;
			DelegateMethods.c_1 = beamColor * 0.2f * base.Projectile.Opacity;
			Utils.DrawLaser(Main.spriteBatch, tex, beamStartPos, beamEndPos, scaleVec, llf);
		}
		return false;
	}

	private void ProduceBeamDust(Color beamColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 laserEndPos = base.Projectile.Center + beamVector * (base.Projectile.ai[0] - 14.5f * base.Projectile.scale);
		for (int i = 0; i < 2; i++)
		{
			float f = base.Projectile.rotation + (Main.rand.NextBool() ? 1f : (-1f)) * ((float)Math.PI / 2f);
			float dustStartDist = Main.rand.NextFloat(1f, 1.8f);
			Vector2 dustVel = f.ToRotationVector2() * dustStartDist;
			int d = Dust.NewDust(laserEndPos, 0, 0, DustType, dustVel.X, dustVel.Y, 0, beamColor);
			Main.dust[d].color = beamColor;
			Main.dust[d].noGravity = true;
			Main.dust[d].scale = 0.7f;
			if (base.Projectile.scale > 1f)
			{
				Dust obj = Main.dust[d];
				obj.velocity *= base.Projectile.scale;
				Main.dust[d].scale *= base.Projectile.scale;
			}
			if (base.Projectile.scale != 1.2f)
			{
				DustExtensions.BetterCloneDust(d).scale /= 2f;
			}
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 dustOffset = beamVector.RotatedBy(1.5707963705062866) * (Main.rand.NextFloat() - 0.5f) * (float)base.Projectile.width;
			int d2 = Dust.NewDust(laserEndPos + dustOffset - Vector2.One * 4f, 8, 8, DustType, 0f, 0f, 100, beamColor, 1.2f);
			Dust obj2 = Main.dust[d2];
			obj2.velocity *= 0.5f;
			Main.dust[d2].velocity.Y = 0f - Math.Abs(Main.dust[d2].velocity.Y);
		}
	}

	public override void CutTiles()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.TileActionAttempt cut = DelegateMethods.CutTiles;
		Vector2 center = base.Projectile.Center;
		Vector2 beamEndPos = center + beamVector * base.Projectile.ai[0];
		Utils.PlotTileLine(center, beamEndPos, (float)base.Projectile.width * base.Projectile.scale, cut);
	}

	public AdamAcceleratorBeam()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		beamVector = Vector2.Zero;
		base._002Ector();
	}
}
