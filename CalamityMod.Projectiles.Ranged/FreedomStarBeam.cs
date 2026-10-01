using System;
using System.IO;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FreedomStarBeam : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 840;

	private const int TimeToReachMaxSize = 480;

	private const int TimeToShrink = 60;

	private const float MaxBeamScale = 3f;

	private const int DustType = 226;

	private const int PierceLimit = 50;

	private const float MaxBeamLength = 1200f;

	private const float BeamTileCollisionWidth = 1f;

	private const float BeamHitboxCollisionWidth = 15f;

	private const int NumSamplePoints = 3;

	private const float BeamLengthChangeFactor = 0.5f;

	private const float OuterBeamOpacityMultiplier = 0.82f;

	private const float InnerBeamOpacityMultiplier = 0.2f;

	private const float MaxBeamBrightness = 0.75f;

	private const float MainDustBeamEndOffset = 14.5f;

	private const float SidewaysDustBeamEndOffset = 4f;

	private const float BeamRenderTileOffset = 10.5f;

	private const float BeamLengthReductionFactor = 14.5f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.timeLeft = 840;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (Main.projectile[(int)base.Projectile.ai[1]].active && Main.projectile[(int)base.Projectile.ai[1]].type == ModContent.ProjectileType<FreedomStarHoldout>())
		{
			Vector2 value27 = Vector2.Normalize(Main.projectile[(int)base.Projectile.ai[1]].velocity);
			base.Projectile.position = Main.projectile[(int)base.Projectile.ai[1]].Center + value27 * 16f - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + new Vector2(0f, 0f - Main.projectile[(int)base.Projectile.ai[1]].gfxOffY);
			base.Projectile.velocity = Vector2.Normalize(Main.projectile[(int)base.Projectile.ai[1]].velocity);
			if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
			{
				base.Projectile.velocity = -Vector2.UnitY;
			}
			float rotation = base.Projectile.velocity.ToRotation();
			base.Projectile.rotation = rotation - (float)Math.PI / 2f;
			base.Projectile.velocity = rotation.ToRotationVector2();
			bool pierceCapped = base.Projectile.numHits >= 50;
			if (base.Projectile.timeLeft > 360 && !pierceCapped)
			{
				base.Projectile.localAI[0]++;
			}
			else if ((base.Projectile.timeLeft < 60) | pierceCapped)
			{
				base.Projectile.localAI[0] -= 8f;
			}
			if (base.Projectile.localAI[1] == 0f && !pierceCapped)
			{
				base.Projectile.localAI[1] = base.Projectile.damage;
			}
			float power = MathHelper.Clamp(base.Projectile.localAI[0] / 480f, 0.1f, 1f);
			base.Projectile.scale = 3f * power;
			base.Projectile.damage = (int)MathHelper.Lerp(base.Projectile.localAI[1], base.Projectile.localAI[1] * 3f, power);
			if ((power <= 0.1f) & pierceCapped)
			{
				base.Projectile.Kill();
			}
			float[] laserScanResults = new float[3];
			float scanWidth = ((base.Projectile.scale < 1f) ? 1f : base.Projectile.scale);
			Collision.LaserScan(base.Projectile.Center, base.Projectile.velocity, 1f * scanWidth, 1200f, laserScanResults);
			float avg = 0f;
			for (int i = 0; i < laserScanResults.Length; i++)
			{
				avg += laserScanResults[i];
			}
			avg /= 3f;
			base.Projectile.ai[0] = MathHelper.Lerp(base.Projectile.ai[0], avg, 0.5f);
			Vector2 beamDims = default(Vector2);
			((Vector2)(ref beamDims))._002Ector(((Vector2)(ref base.Projectile.velocity)).Length() * base.Projectile.ai[0], (float)base.Projectile.width * base.Projectile.scale);
			Color beamColor = Color.Cyan;
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
			Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.ai[0], beamDims.Y, DelegateMethods.CastLight);
			if (base.Projectile.timeLeft == 840)
			{
				int dustCount = Main.rand.Next(10, 30);
				for (int j = 0; j < dustCount; j++)
				{
					float dustProgressAlongBeam = base.Projectile.ai[0] * Main.rand.NextFloat(0f, 0.7f);
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustProgressAlongBeam * base.Projectile.velocity + base.Projectile.velocity.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-6f, 6f) * base.Projectile.scale, 226, base.Projectile.velocity * Main.rand.NextFloat(2f, 16f), 0, beamColor);
					dust.color = beamColor;
					dust.noGravity = true;
					dust.scale = 0.7f;
				}
			}
		}
		else
		{
			base.Projectile.Kill();
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.ai[0], 15f * base.Projectile.scale, ref _);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 explosionOffset = Utils.RotatedByRandom(new Vector2((float)base.Projectile.width, (float)base.Projectile.height), 6.2831854820251465);
			SoundEngine.PlaySound(in SoundID.Item14, target.Center + explosionOffset);
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center + explosionOffset, Vector2.Zero, 645, base.Projectile.damage / 5, base.Projectile.knockBack, base.Projectile.owner, 0f, -1f);
			Main.projectile[proj].DamageType = DamageClass.Ranged;
			Main.projectile[proj].scale = base.Projectile.scale * 0.7f;
			Main.projectile[proj].netUpdate = true;
			if (base.Projectile.numHits >= 50)
			{
				base.Projectile.localAI[1] *= 0.8f;
			}
		}
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
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float beamLength = base.Projectile.ai[0];
		Vector2 val = base.Projectile.Center.Floor() + base.Projectile.velocity * base.Projectile.scale * 10.5f;
		Vector2 scaleVec = default(Vector2);
		((Vector2)(ref scaleVec))._002Ector(base.Projectile.scale);
		beamLength -= 14.5f * base.Projectile.scale;
		DelegateMethods.f_1 = 1f;
		Vector2 beamStartPos = val - Main.screenPosition;
		Vector2 beamEndPos = beamStartPos + base.Projectile.velocity * beamLength;
		Utils.LaserLineFraming llf = DelegateMethods.RainbowLaserDraw;
		Color beamColor = Color.Cyan;
		DelegateMethods.c_1 = beamColor * 0.82f * base.Projectile.Opacity;
		Utils.DrawLaser(Main.spriteBatch, tex, beamStartPos, beamEndPos, scaleVec, llf);
		for (int i = 0; i < 5; i++)
		{
			beamColor = Color.Lerp(beamColor, Color.White, 0.4f);
			scaleVec *= 0.85f - (float)i * 0.15f;
			DelegateMethods.c_1 = beamColor * 0.2f * base.Projectile.Opacity;
			Utils.DrawLaser(Main.spriteBatch, tex, beamStartPos, beamEndPos, scaleVec, llf);
		}
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
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
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
		Vector2 laserEndPos = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.ai[0] - 14.5f * base.Projectile.scale);
		for (int i = 0; i < 2; i++)
		{
			float f = base.Projectile.rotation + (Main.rand.NextBool() ? 1f : (-1f)) * ((float)Math.PI / 2f);
			float dustStartDist = Main.rand.NextFloat(1f, 1.8f);
			Vector2 dustVel = f.ToRotationVector2() * dustStartDist;
			int d = Dust.NewDust(laserEndPos, 0, 0, 226, dustVel.X, dustVel.Y, 0, beamColor);
			Main.dust[d].color = beamColor;
			Main.dust[d].noGravity = true;
			Main.dust[d].scale = 0.7f;
			if (base.Projectile.scale > 1f)
			{
				Dust obj = Main.dust[d];
				obj.velocity *= base.Projectile.scale;
				Main.dust[d].scale *= base.Projectile.scale;
			}
			if (base.Projectile.scale != 3f)
			{
				DustExtensions.BetterCloneDust(d).scale /= 2f;
			}
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 dustOffset = base.Projectile.velocity.RotatedBy(1.5707963705062866) * (Main.rand.NextFloat() - 0.5f) * (float)base.Projectile.width;
			int d2 = Dust.NewDust(laserEndPos + dustOffset - Vector2.One * 4f, 8, 8, 226, 0f, 0f, 100, beamColor, 1.2f);
			Dust obj2 = Main.dust[d2];
			obj2.velocity *= 0.5f;
			Main.dust[d2].velocity.Y = 0f - Math.Abs(Main.dust[d2].velocity.Y);
		}
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
		Vector2 beamEndPos = center + base.Projectile.velocity * base.Projectile.ai[0];
		Utils.PlotTileLine(center, beamEndPos, (float)base.Projectile.width * base.Projectile.scale, cut);
	}
}
