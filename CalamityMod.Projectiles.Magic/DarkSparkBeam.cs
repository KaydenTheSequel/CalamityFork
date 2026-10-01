using System;
using System.IO;
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
public class DarkSparkBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Magic/YharimsCrystalBeam";

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		Vector2? chargeUpCenter = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (base.Projectile.type != ModContent.ProjectileType<DarkSparkBeam>() || !Main.projectile[(int)base.Projectile.ai[1]].active || Main.projectile[(int)base.Projectile.ai[1]].type != ModContent.ProjectileType<DarkSparkPrism>())
		{
			base.Projectile.Kill();
			return;
		}
		float laserPosition = (float)(int)base.Projectile.ai[0] - 2.5f;
		Vector2 aimDirection = Vector2.Normalize(Main.projectile[(int)base.Projectile.ai[1]].velocity);
		Projectile projectile2 = Main.projectile[(int)base.Projectile.ai[1]];
		Vector2 projDirection = Vector2.Zero;
		Color color = GetBeamColor((int)projectile2.ai[0], (int)base.Projectile.ai[0]);
		base.Projectile.Opacity = 1f;
		float laserTimer;
		float yOffset;
		float laserRotationSpeed;
		float scaleFactor6;
		if (projectile2.ai[0] < 360f)
		{
			laserTimer = projectile2.ai[0] / 720f;
			yOffset = 6f + projectile2.ai[0] / 360f * 7f;
			laserRotationSpeed = ((!(projectile2.ai[0] < 240f)) ? (3f + 5f * ((projectile2.ai[0] - 240f) / 120f)) : 1.75f);
			scaleFactor6 = -2f - projectile2.ai[0] / 360f * 5f;
		}
		else
		{
			laserTimer = 0.5f;
			laserRotationSpeed = 10.875f;
			yOffset = 13f;
			scaleFactor6 = -7f;
		}
		float laserDirection = (projectile2.ai[0] + laserPosition * laserRotationSpeed) / (laserRotationSpeed * 6f) * ((float)Math.PI * 2f);
		float laserNormalize = Vector2.UnitY.RotatedBy(laserDirection).Y * ((float)Math.PI / 6f) * laserTimer * 0.33f;
		projDirection = (Vector2.UnitY.RotatedBy(laserDirection) * new Vector2(4f, yOffset)).RotatedBy(projectile2.velocity.ToRotation());
		base.Projectile.position = projectile2.Center + aimDirection * 16f - base.Projectile.Size / 2f + new Vector2(0f, 0f - Main.projectile[(int)base.Projectile.ai[1]].gfxOffY);
		Projectile projectile3 = base.Projectile;
		projectile3.position += projectile2.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
		Projectile projectile4 = base.Projectile;
		projectile4.position += projDirection;
		base.Projectile.velocity = Vector2.Normalize(projectile2.velocity).RotatedBy(laserNormalize);
		base.Projectile.scale = 1.5f * (1.5f - laserTimer);
		float amount = projectile2.ai[0] / 600f;
		if (amount > 1f)
		{
			amount = 1f;
		}
		base.Projectile.damage = (int)((float)projectile2.damage * MathHelper.Lerp(0.25f, 2.2f, amount));
		if (projectile2.ai[0] >= 360f)
		{
			chargeUpCenter = projectile2.Center;
		}
		if (!Collision.CanHitLine(Main.player[base.Projectile.owner].Center, 0, 0, projectile2.Center, 0, 0))
		{
			chargeUpCenter = Main.player[base.Projectile.owner].Center;
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		float laserRotate = base.Projectile.velocity.ToRotation();
		base.Projectile.rotation = laserRotate - (float)Math.PI / 2f;
		base.Projectile.velocity = laserRotate.ToRotationVector2();
		Vector2 samplingPoint = base.Projectile.Center;
		if (chargeUpCenter.HasValue)
		{
			samplingPoint = chargeUpCenter.Value;
		}
		float[] array3 = new float[2];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, 0f * base.Projectile.scale, 2400f, array3);
		float beamSeparation = 0f;
		for (int i = 0; i < array3.Length; i++)
		{
			beamSeparation += array3[i];
		}
		beamSeparation /= 2f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], beamSeparation, 0.75f);
		if (!(Math.Abs(base.Projectile.localAI[1] - beamSeparation) < 100f) || !(base.Projectile.scale > 0.15f))
		{
			return;
		}
		Vector2 dustSpawn = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14.5f * base.Projectile.scale);
		Vector2 randomRotate = default(Vector2);
		for (int j = 0; j < 2; j++)
		{
			float dustRotate = base.Projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float dustRandom = (float)Main.rand.NextDouble() * 0.8f + 1f;
			((Vector2)(ref randomRotate))._002Ector((float)Math.Cos(dustRotate) * dustRandom, (float)Math.Sin(dustRotate) * dustRandom);
			int rainbowDust = Dust.NewDust(dustSpawn, 0, 0, 267, randomRotate.X, randomRotate.Y, 0, color, 3.3f);
			Main.dust[rainbowDust].color = color;
			Main.dust[rainbowDust].scale = 1.2f;
			if (base.Projectile.scale > 1f)
			{
				Dust obj = Main.dust[rainbowDust];
				obj.velocity *= base.Projectile.scale;
				Main.dust[rainbowDust].scale *= base.Projectile.scale;
			}
			Main.dust[rainbowDust].noGravity = true;
			if (base.Projectile.scale != 1.4f)
			{
				Dust dust = DustExtensions.BetterCloneDust(rainbowDust);
				dust.color = color;
				dust.scale /= 2f;
				dust.noGravity = true;
			}
			Main.dust[rainbowDust].color = color;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 extraDustSpawn = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.Projectile.width;
			int extraRainbows = Dust.NewDust(dustSpawn + extraDustSpawn - Vector2.One * 4f, 8, 8, 267, 0f, 0f, 100, color, 5f);
			Dust obj2 = Main.dust[extraRainbows];
			obj2.velocity *= 0.5f;
			Main.dust[extraRainbows].noGravity = true;
			Main.dust[extraRainbows].velocity.Y = 0f - Math.Abs(Main.dust[extraRainbows].velocity.Y);
		}
		DelegateMethods.v3_1 = ((Color)(ref color)).ToVector3() * 0.3f;
		Vector2 size = default(Vector2);
		((Vector2)(ref size))._002Ector(((Vector2)(ref base.Projectile.velocity)).Length() * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale);
		float shaderLength = base.Projectile.velocity.ToRotation();
		if (!Main.dedServ)
		{
			((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(base.Projectile.position + Utils.RotatedBy(new Vector2(size.X * 0.5f, 0f), (double)shaderLength, default(Vector2)), color, size, RippleShape.Square, shaderLength);
		}
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float drawArea = base.Projectile.localAI[1];
		Projectile projectile2 = Main.projectile[(int)base.Projectile.ai[1]];
		Color beamColor = GetBeamColor((int)projectile2.ai[0], (int)base.Projectile.ai[0]);
		Vector2 drawStart = base.Projectile.Center.Floor();
		drawStart += base.Projectile.velocity * base.Projectile.scale * 10.5f;
		drawArea -= base.Projectile.scale * 14.5f * base.Projectile.scale;
		Vector2 drawScale = default(Vector2);
		((Vector2)(ref drawScale))._002Ector(base.Projectile.scale);
		DelegateMethods.f_1 = 1f;
		DelegateMethods.c_1 = beamColor * 0.75f * base.Projectile.Opacity;
		_ = ref base.Projectile.oldPos[0];
		_ = new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Utils.DrawLaser(Main.spriteBatch, tex, drawStart - Main.screenPosition, drawStart + base.Projectile.velocity * drawArea - Main.screenPosition, drawScale, DelegateMethods.RainbowLaserDraw);
		DelegateMethods.c_1 = beamColor * 0.75f * base.Projectile.Opacity;
		Utils.DrawLaser(Main.spriteBatch, tex, drawStart - Main.screenPosition, drawStart + base.Projectile.velocity * drawArea - Main.screenPosition, drawScale / 2f, DelegateMethods.RainbowLaserDraw);
		return false;
	}

	public Color GetBeamColor(int timer, int type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		Color color = Color.Black;
		if ((float)timer < 360f)
		{
			float brightness = MathHelper.Clamp((float)timer / 180f, 0f, 1f);
			color = Color.Lerp(Color.Black, Color.White, brightness);
		}
		else
		{
			float hue = 0f;
			hue = type switch
			{
				1 => 1f / 12f, 
				2 => 1f / 6f, 
				3 => 1f / 3f, 
				4 => 7f / 12f, 
				5 => 0.75f, 
				_ => 0f, 
			};
			float lightness = 1f - MathHelper.Clamp(((float)timer - 360f) / 240f, 0f, 1f) * 0.4f;
			color = Main.hslToRgb(hue, 1f, lightness);
		}
		((Color)(ref color)).A = 127;
		return color;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
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
		float useless = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 22f * base.Projectile.scale, ref useless))
		{
			return true;
		}
		return false;
	}
}
