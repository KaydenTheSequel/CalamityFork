using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
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

[PierceResistException(false)]
public class YharimsCrystalBeam : ModProjectile, ILocalizedModType, IModType
{
	private const float PiBeamDivisor = (float)Math.PI / 6f;

	private const float MaxDamageMultiplier = 3f;

	private const float BeamPosOffset = 16f;

	private const float MaxBeamScale = 1.8f;

	private const float MaxBeamLength = 2400f;

	private const float BeamTileCollisionWidth = 1f;

	private const float BeamHitboxCollisionWidth = 22f;

	private const int NumSamplePoints = 3;

	private const float BeamLengthChangeFactor = 0.75f;

	private const float VisualEffectThreshold = 0.1f;

	private const float OuterBeamOpacityMultiplier = 0.75f;

	private const float InnerBeamOpacityMultiplier = 0.1f;

	private const float BeamLightBrightness = 0.75f;

	private const float MainDustBeamEndOffset = 14.5f;

	private const float SidewaysDustBeamEndOffset = 4f;

	private const float BeamRenderTileOffset = 10.5f;

	private const float BeamLengthReductionFactor = 14.5f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		Projectile hostCrystal = Main.projectile[(int)base.Projectile.ai[1]];
		if (base.Projectile.type != ModContent.ProjectileType<YharimsCrystalBeam>() || !hostCrystal.active || hostCrystal.type != ModContent.ProjectileType<YharimsCrystalPrism>())
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 hostCrystalDir = Vector2.Normalize(hostCrystal.velocity);
		float chargeRatio = MathHelper.Clamp(hostCrystal.ai[0] / 180f, 0f, 1f);
		base.Projectile.damage = (int)((float)hostCrystal.damage * GetDamageMultiplier(chargeRatio));
		base.Projectile.friendly = hostCrystal.ai[0] > 30f;
		float beamIdOffset = (float)(int)base.Projectile.ai[0] - 3f + 0.5f;
		float beamSpread;
		float beamStartSidewaysOffset;
		float beamStartForwardsOffset;
		float spinRate;
		if (chargeRatio < 1f)
		{
			base.Projectile.scale = MathHelper.Lerp(0f, 1.8f, chargeRatio);
			beamSpread = MathHelper.Lerp(1.22f, 0f, chargeRatio);
			beamStartSidewaysOffset = MathHelper.Lerp(20f, 6f, chargeRatio);
			beamStartForwardsOffset = MathHelper.Lerp(-17f, -13f, chargeRatio);
			if (chargeRatio <= 0.66f)
			{
				float phaseRatio = chargeRatio * 1.5f;
				base.Projectile.Opacity = MathHelper.Lerp(0f, 0.4f, phaseRatio);
				spinRate = MathHelper.Lerp(20f, 16f, phaseRatio);
			}
			else
			{
				float phaseRatio2 = (chargeRatio - 0.66f) * 3f;
				base.Projectile.Opacity = MathHelper.Lerp(0.4f, 1f, phaseRatio2);
				spinRate = MathHelper.Lerp(16f, 6f, phaseRatio2);
			}
		}
		else
		{
			base.Projectile.scale = 1.8f;
			base.Projectile.Opacity = 1f;
			beamSpread = 0f;
			spinRate = 6f;
			beamStartSidewaysOffset = 6f;
			beamStartForwardsOffset = -13f;
		}
		float deviationAngle = (hostCrystal.ai[0] + beamIdOffset * spinRate) / (spinRate * 6f) * ((float)Math.PI * 2f);
		Vector2 val = Vector2.UnitY.RotatedBy(deviationAngle);
		float sinusoidYOffset = val.Y * ((float)Math.PI / 6f) * beamSpread;
		float hostCrystalAngle = hostCrystal.velocity.ToRotation();
		Vector2 yVec = default(Vector2);
		((Vector2)(ref yVec))._002Ector(4f, beamStartSidewaysOffset);
		Vector2 beamSpanVector = (val * yVec).RotatedBy(hostCrystalAngle);
		base.Projectile.Center = hostCrystal.Center;
		Projectile projectile = base.Projectile;
		projectile.position += hostCrystalDir * 16f + new Vector2(0f, 0f - hostCrystal.gfxOffY);
		Projectile projectile2 = base.Projectile;
		projectile2.position += hostCrystalDir * beamStartForwardsOffset;
		Projectile projectile3 = base.Projectile;
		projectile3.position += beamSpanVector;
		base.Projectile.velocity = hostCrystalDir.RotatedBy(sinusoidYOffset);
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 samplingPoint = base.Projectile.Center;
		if (hostCrystal.ai[0] >= 180f)
		{
			samplingPoint = hostCrystal.Center;
		}
		if (!Collision.CanHitLine(Main.player[base.Projectile.owner].Center, 0, 0, hostCrystal.Center, 0, 0))
		{
			samplingPoint = Main.player[base.Projectile.owner].Center;
		}
		float[] laserScanResults = new float[3];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, 1f * base.Projectile.scale, 2400f, laserScanResults);
		float avg = 0f;
		for (int i = 0; i < laserScanResults.Length; i++)
		{
			avg += laserScanResults[i];
		}
		avg /= 3f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], avg, 0.75f);
		Vector2 beamDims = default(Vector2);
		((Vector2)(ref beamDims))._002Ector(((Vector2)(ref base.Projectile.velocity)).Length() * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale);
		Color beamColor = GetBeamColor();
		if (chargeRatio >= 0.1f)
		{
			ProduceBeamDust(beamColor);
			if (!Main.dedServ)
			{
				WaterShaderData obj = (WaterShaderData)Filters.Scene["WaterDistortion"].GetShader();
				float waveSine = 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 20f);
				Vector2 ripplePos = base.Projectile.position + Utils.RotatedBy(new Vector2(beamDims.X * 0.5f, 0f), (double)base.Projectile.rotation, default(Vector2));
				Color waveData = new Color(0.5f, 0.1f * (float)Math.Sign(waveSine) + 0.5f, 0f, 1f) * Math.Abs(waveSine);
				obj.QueueRipple(ripplePos, waveData, beamDims, RippleShape.Square, base.Projectile.rotation);
			}
		}
		DelegateMethods.v3_1 = ((Color)(ref beamColor)).ToVector3() * 0.75f * chargeRatio;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], beamDims.Y, DelegateMethods.CastLight);
	}

	private float GetDamageMultiplier(float chargeRatio)
	{
		float f = chargeRatio * chargeRatio * chargeRatio;
		return MathHelper.Lerp(1f, 3f, f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 22f * base.Projectile.scale, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float beamLength = base.Projectile.localAI[1];
		Vector2 val = base.Projectile.Center.Floor() + base.Projectile.velocity * base.Projectile.scale * 10.5f;
		Vector2 scaleVec = default(Vector2);
		((Vector2)(ref scaleVec))._002Ector(base.Projectile.scale);
		beamLength -= 14.5f * base.Projectile.scale * base.Projectile.scale;
		DelegateMethods.f_1 = 1f;
		Vector2 beamStartPos = val - Main.screenPosition;
		Vector2 beamEndPos = beamStartPos + base.Projectile.velocity * beamLength;
		Utils.LaserLineFraming llf = DelegateMethods.RainbowLaserDraw;
		DelegateMethods.c_1 = GetBeamColor() * 0.75f * base.Projectile.Opacity;
		Utils.DrawLaser(Main.spriteBatch, tex, beamStartPos, beamEndPos, scaleVec, llf);
		scaleVec *= 0.5f;
		DelegateMethods.c_1 = Color.White * 0.1f * base.Projectile.Opacity;
		Utils.DrawLaser(Main.spriteBatch, tex, beamStartPos, beamEndPos, scaleVec, llf);
		return false;
	}

	private void ProduceBeamDust(Color beamColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		Vector2 laserEndPos = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14.5f * base.Projectile.scale);
		for (int i = 0; i < 2; i++)
		{
			float f = base.Projectile.rotation + (Main.rand.NextBool() ? 1f : (-1f)) * ((float)Math.PI / 2f);
			float dustStartDist = Main.rand.NextFloat(1f, 1.8f);
			Vector2 dustVel = f.ToRotationVector2() * dustStartDist;
			int d = Dust.NewDust(laserEndPos, 0, 0, 244, dustVel.X, dustVel.Y, 0, beamColor, 3.3f);
			Main.dust[d].color = beamColor;
			Main.dust[d].noGravity = true;
			Main.dust[d].scale = 1.2f;
			if (base.Projectile.scale > 1f)
			{
				Dust obj = Main.dust[d];
				obj.velocity *= base.Projectile.scale;
				Main.dust[d].scale *= base.Projectile.scale;
			}
			if (base.Projectile.scale != 1.8f)
			{
				DustExtensions.BetterCloneDust(d).scale /= 2f;
			}
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 dustOffset = base.Projectile.velocity.RotatedBy(1.5707963705062866) * (Main.rand.NextFloat() - 0.5f) * (float)base.Projectile.width;
			Vector2 position = laserEndPos + dustOffset - Vector2.One * 4f;
			int dustID = 244;
			int d2 = Dust.NewDust(position, 8, 8, dustID, 0f, 0f, 100, beamColor, 5f);
			Dust obj2 = Main.dust[d2];
			obj2.velocity *= 0.5f;
			Main.dust[d2].velocity.Y = 0f - Math.Abs(Main.dust[d2].velocity.Y);
		}
	}

	private Color GetBeamColor()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		float hue = GetHue(base.Projectile.ai[0]);
		float sat = 0.66f;
		Color c = Main.hslToRgb(hue, sat, 0.53f);
		((Color)(ref c)).A = 64;
		return c;
	}

	private float GetHue(float indexing)
	{
		string name = Main.player[base.Projectile.owner].name ?? "";
		if (Main.player[base.Projectile.owner].active)
		{
			switch (name)
			{
			case "Ziggums":
				return 2f;
			case "Poly":
				return 0.83f;
			case "Zach":
				return 1.5f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.1f;
			case "Grox the Great":
				return 1.27f;
			case "Jenosis":
				return 0.65f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.1f;
			case "DM DOKURO":
				return 0f;
			case "Phoenix":
			case "Uncle Danny":
				return 1.7f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.07f;
			case "Minecat":
				return 0.15f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.07f;
			case "Khaelis":
				return 1.15f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.18f;
			case "Purple Necromancer":
				return 1.7f + (float)Math.Cos(Main.time / 120.0 * Math.PI * 2.0) * 0.05f;
			case "gamagamer64":
				return 0.83f + (float)Math.Cos(Main.time / 120.0 * Math.PI * 2.0) * 0.03f;
			case "Svante":
				return 1.4f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.06f;
			case "Puff":
				return 0.31f + (float)Math.Cos(Main.time / 120.0 * Math.PI * 2.0) * 0.13f;
			case "Leviathan":
				return 1.9f + (float)Math.Cos(Main.time / 180.0 * Math.PI * 2.0) * 0.1f;
			case "Testdude":
				return Main.rand.NextFloat();
			}
		}
		return indexing / 6f % 0.12f;
	}

	public override void CutTiles()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.TileActionAttempt cut = DelegateMethods.CutTiles;
		Vector2 center = base.Projectile.Center;
		Vector2 beamEndPos = center + base.Projectile.velocity * base.Projectile.localAI[1];
		Utils.PlotTileLine(center, beamEndPos, (float)base.Projectile.width * base.Projectile.scale, cut);
	}
}
