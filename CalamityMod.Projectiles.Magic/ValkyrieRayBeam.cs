using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ValkyrieRayBeam : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 24;

	private const int BeamDustID = 73;

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

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
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
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.type != ModContent.ProjectileType<ValkyrieRayBeam>())
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
		Color beamColor = GetBeamColor();
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

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (base.Projectile.Center.X < target.Center.X).ToDirectionInt();
	}

	private Color GetBeamColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Color c = ValkyrieRay.LightColor;
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
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
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
		Color beamColor = GetBeamColor();
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
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 laserEndPos = base.Projectile.Center + beamVector * (base.Projectile.ai[0] - 14.5f * base.Projectile.scale);
		for (int i = 0; i < 2; i++)
		{
			float f = base.Projectile.rotation + (Main.rand.NextBool() ? 1f : (-1f)) * ((float)Math.PI / 2f);
			float dustStartDist = Main.rand.NextFloat(1f, 1.8f);
			Vector2 dustVel = f.ToRotationVector2() * dustStartDist;
			int d = Dust.NewDust(laserEndPos, 0, 0, 73, dustVel.X, dustVel.Y, 0, beamColor);
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
			int d2 = Dust.NewDust(laserEndPos + dustOffset - Vector2.One * 4f, 8, 8, 73, 0f, 0f, 100, beamColor, 1.2f);
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

	public ValkyrieRayBeam()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		beamVector = Vector2.Zero;
		base._002Ector();
	}
}
