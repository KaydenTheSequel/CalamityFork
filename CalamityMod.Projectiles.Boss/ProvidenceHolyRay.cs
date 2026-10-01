using System;
using System.IO;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ProvidenceHolyRay : ModProjectile, ILocalizedModType, IModType
{
	private float ExtraWidth = 1f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		ExtraWidth = base.Projectile.ai[2];
		bool scissorLasers = CalamityWorld.revenge || !ProvUtils.StandardAI();
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (Main.npc[(int)base.Projectile.ai[1]].active && (Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<Providence>() || Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<ProfanedGuardianCommander>()))
		{
			Vector2 laserOffset = (Vector2)((Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<ProfanedGuardianCommander>()) ? new Vector2(40f * (float)Main.npc[(int)base.Projectile.ai[1]].direction, 20f) : (Vector2.UnitY * 32f));
			Vector2 fireFrom = new Vector2(Main.npc[(int)base.Projectile.ai[1]].Center.X, Main.npc[(int)base.Projectile.ai[1]].Center.Y) + laserOffset;
			base.Projectile.position = fireFrom - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		float projScale = ((Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<ProfanedGuardianCommander>()) ? 0.66f : 1f);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= (scissorLasers ? 100f : 180f))
		{
			base.Projectile.Kill();
			return;
		}
		if (projScale == 1f)
		{
			ProvUtils.ApplyGFBDamage(base.Projectile, 240, 20);
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / (scissorLasers ? 100f : 180f)) * 10f * projScale;
		if (base.Projectile.scale > projScale)
		{
			base.Projectile.scale = projScale;
		}
		float projVelRotation = base.Projectile.velocity.ToRotation();
		projVelRotation += base.Projectile.ai[0];
		base.Projectile.rotation = projVelRotation - (float)Math.PI / 2f;
		base.Projectile.velocity = projVelRotation.ToRotationVector2();
		float projWidth = base.Projectile.width;
		Vector2 samplingPoint = base.Projectile.Center;
		if (vector78.HasValue)
		{
			samplingPoint = vector78.Value;
		}
		float[] array3 = new float[3];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, projWidth * base.Projectile.scale, 2400f, array3);
		float rayLength = 0f;
		for (int i = 0; i < array3.Length; i++)
		{
			rayLength += array3[i];
		}
		rayLength /= 3f;
		if (!Collision.CanHitLine(Main.npc[(int)base.Projectile.ai[1]].Center, 1, 1, Main.player[Main.npc[(int)base.Projectile.ai[1]].target].Center, 1, 1))
		{
			rayLength = 2400f;
		}
		int dustType = ProvUtils.GetDustID();
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], rayLength, 0.5f);
		Vector2 dustRotation = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14f);
		Vector2 finalDustVel = default(Vector2);
		for (int j = 0; j < 2; j++)
		{
			float randDustDirection = base.Projectile.velocity.ToRotation() + (Main.rand.NextBool() ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float randDustVel = (float)Main.rand.NextDouble() * 2f + 2f;
			((Vector2)(ref finalDustVel))._002Ector((float)Math.Cos(randDustDirection) * randDustVel, (float)Math.Sin(randDustDirection) * randDustVel);
			int holyDust = Dust.NewDust(dustRotation, 0, 0, dustType, finalDustVel.X, finalDustVel.Y);
			Main.dust[holyDust].noGravity = true;
			Main.dust[holyDust].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 extraDustRotate = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.Projectile.width;
			int extraHolyDust = Dust.NewDust(dustRotation + extraDustRotate - Vector2.One * 4f, 8, 8, dustType, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[extraHolyDust];
			obj.velocity *= 0.5f;
			Main.dust[extraHolyDust].velocity.Y = 0f - Math.Abs(Main.dust[extraHolyDust].velocity.Y);
		}
		DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
		for (float i2 = 0f; i2 < base.Projectile.localAI[1]; i2 += Main.rand.NextFloat(15f, 30f))
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(30f * ExtraWidth), 0f), (double)Main.rand.NextFloat((float)Math.PI * 2f), default(Vector2)) + base.Projectile.velocity * i2, base.Projectile.velocity * 5f, affectedByGravity: false, 5, Main.rand.NextFloat(0.5f, 1.5f), ProvUtils.GetProjectileColor(255)));
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(lightColor);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(base.Projectile.scale * ExtraWidth, base.Projectile.scale);
		bool num = ProvUtils.StandardAI();
		Texture2D texture2D19 = (num ? ModContent.Request<Texture2D>(Texture, (AssetRequestMode)1).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ProvidenceHolyRayNight", (AssetRequestMode)1).Value);
		Texture2D texture2D20 = (num ? ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayMid", (AssetRequestMode)1).Value : ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayMidNight", (AssetRequestMode)1).Value);
		Texture2D texture2D21 = (num ? ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayEnd", (AssetRequestMode)1).Value : ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayEndNight", (AssetRequestMode)1).Value);
		float rayDrawLength = base.Projectile.localAI[1];
		Color baseColor = ProvUtils.GetProjectileColor(lightColor);
		Vector2 vector = base.Projectile.Center - Main.screenPosition;
		Main.spriteBatch.Draw(texture2D19, vector, (Rectangle?)null, baseColor, base.Projectile.rotation, texture2D19.Size() / 2f, scale, (SpriteEffects)0, 0f);
		rayDrawLength -= (float)(texture2D19.Height / 2 + texture2D21.Height) * base.Projectile.scale;
		Vector2 projCenter = base.Projectile.Center;
		projCenter += base.Projectile.velocity * base.Projectile.scale * (float)texture2D19.Height / 2f;
		Texture2D GlowBallTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D GlowRingTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2).Value;
		base.Projectile.localAI[2]++;
		Color glowColor = ProvUtils.GetProjectileColor(0);
		Main.EntitySpriteDraw(GlowBallTexture, base.Projectile.Center - Main.screenPosition, GlowBallTexture.Frame(), glowColor, 0f, GlowBallTexture.Frame().Center(), base.Projectile.scale / 1.5f * ExtraWidth, (SpriteEffects)0);
		Main.EntitySpriteDraw(GlowRingTexture, base.Projectile.Center - Main.screenPosition, GlowRingTexture.Frame(), glowColor, 0f, GlowRingTexture.Frame().Center(), base.Projectile.scale / MathHelper.Lerp(1.5f, 1f, (float)Math.Sin(base.Projectile.localAI[2] / 20f) * 0.5f) * ExtraWidth, (SpriteEffects)0);
		if (rayDrawLength > 0f)
		{
			float raySegment = 0f;
			Rectangle drawRectangle = default(Rectangle);
			((Rectangle)(ref drawRectangle))._002Ector(0, 36 * (base.Projectile.timeLeft / 3 % 4), texture2D20.Width, 36);
			while (raySegment + 1f < rayDrawLength)
			{
				if (rayDrawLength - raySegment < (float)drawRectangle.Height)
				{
					drawRectangle.Height = (int)(rayDrawLength - raySegment);
				}
				Main.spriteBatch.Draw(texture2D20, projCenter - Main.screenPosition, (Rectangle?)drawRectangle, baseColor, base.Projectile.rotation, new Vector2((float)(drawRectangle.Width / 2), 0f), scale, (SpriteEffects)0, 0f);
				raySegment += (float)drawRectangle.Height * base.Projectile.scale;
				projCenter += base.Projectile.velocity * (float)drawRectangle.Height * base.Projectile.scale;
				drawRectangle.Y += 36;
				if (drawRectangle.Y + drawRectangle.Height > texture2D20.Height)
				{
					drawRectangle.Y = 0;
				}
			}
		}
		Vector2 vector2 = projCenter - Main.screenPosition;
		Main.spriteBatch.Draw(texture2D21, vector2, (Rectangle?)null, baseColor, base.Projectile.rotation, texture2D21.Frame().Top(), scale, (SpriteEffects)0, 0f);
		return false;
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		targetHitbox.Width = (int)((float)targetHitbox.Width * ExtraWidth);
		targetHitbox.Height = (int)((float)targetHitbox.Height * ExtraWidth);
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 240);
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.scale >= 0.5f;
	}
}
