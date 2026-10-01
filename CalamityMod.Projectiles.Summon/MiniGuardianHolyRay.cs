using System;
using System.IO;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianHolyRay : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/ProvidenceHolyRay";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 3;
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		Player owner = Main.player[base.Projectile.owner];
		if (owner.active && !owner.dead)
		{
			Vector2 fireFrom = default(Vector2);
			((Vector2)(ref fireFrom))._002Ector(owner.Center.X, owner.Center.Y);
			base.Projectile.position = fireFrom - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
			base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		}
		else
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		float num801 = 0.66f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 150f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / 150f) * 10f * num801;
		if (base.Projectile.scale > num801)
		{
			base.Projectile.scale = num801;
		}
		float num804 = base.Projectile.velocity.ToRotation();
		num804 += base.Projectile.ai[0];
		base.Projectile.rotation = num804 - (float)Math.PI / 2f;
		base.Projectile.velocity = num804.ToRotationVector2();
		float num805 = 3f;
		float num806 = base.Projectile.width;
		Vector2 samplingPoint = base.Projectile.Center;
		if (vector78.HasValue)
		{
			samplingPoint = vector78.Value;
		}
		float[] array3 = new float[(int)num805];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, num806 * base.Projectile.scale, 2400f, array3);
		float num807 = 0f;
		for (int i = 0; i < array3.Length; i++)
		{
			num807 += array3[i];
		}
		num807 /= num805;
		num807 = 2400f;
		int dustType = ProvUtils.GetDustID(!Main.dayTime);
		float amount = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], num807, amount);
		Vector2 vector79 = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14f);
		Vector2 vector80 = default(Vector2);
		for (int j = 0; j < 2; j++)
		{
			float num810 = base.Projectile.velocity.ToRotation() + (Main.rand.NextBool() ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
			((Vector2)(ref vector80))._002Ector((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
			int num812 = Dust.NewDust(vector79, 0, 0, dustType, vector80.X, vector80.Y);
			Main.dust[num812].noGravity = true;
			Main.dust[num812].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value29 = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.Projectile.width;
			int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, dustType, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[num813];
			obj.velocity *= 0.5f;
			Main.dust[num813].velocity.Y = 0f - Math.Abs(Main.dust[num813].velocity.Y);
		}
		DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		bool dayTime = Main.dayTime;
		Texture2D texture2D19 = (dayTime ? ModContent.Request<Texture2D>(Texture, (AssetRequestMode)1).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ProvidenceHolyRayNight", (AssetRequestMode)1).Value);
		Texture2D texture2D20 = (dayTime ? ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayMid", (AssetRequestMode)1).Value : ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayMidNight", (AssetRequestMode)1).Value);
		Texture2D texture2D21 = (dayTime ? ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayEnd", (AssetRequestMode)1).Value : ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayEndNight", (AssetRequestMode)1).Value);
		float num223 = base.Projectile.localAI[1];
		Color color44 = ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, 0) * 0.9f;
		Vector2 vector = base.Projectile.Center - Main.screenPosition;
		Main.spriteBatch.Draw(texture2D19, vector, (Rectangle?)null, color44, base.Projectile.rotation, texture2D19.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
		num223 -= (float)(texture2D19.Height / 2 + texture2D21.Height) * base.Projectile.scale;
		Vector2 value20 = base.Projectile.Center;
		value20 += base.Projectile.velocity * base.Projectile.scale * (float)texture2D19.Height / 2f;
		if (num223 > 0f)
		{
			float num224 = 0f;
			Rectangle rectangle7 = default(Rectangle);
			((Rectangle)(ref rectangle7))._002Ector(0, 36 * (base.Projectile.timeLeft / 3 % 4), texture2D20.Width, 36);
			while (num224 + 1f < num223)
			{
				if (num223 - num224 < (float)rectangle7.Height)
				{
					rectangle7.Height = (int)(num223 - num224);
				}
				Main.spriteBatch.Draw(texture2D20, value20 - Main.screenPosition, (Rectangle?)rectangle7, color44, base.Projectile.rotation, new Vector2((float)(rectangle7.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0, 0f);
				num224 += (float)rectangle7.Height * base.Projectile.scale;
				value20 += base.Projectile.velocity * (float)rectangle7.Height * base.Projectile.scale;
				rectangle7.Y += 36;
				if (rectangle7.Y + rectangle7.Height > texture2D20.Height)
				{
					rectangle7.Y = 0;
				}
			}
		}
		Vector2 vector2 = value20 - Main.screenPosition;
		Main.spriteBatch.Draw(texture2D21, vector2, (Rectangle?)null, color44, base.Projectile.rotation, texture2D21.Frame().Top(), base.Projectile.scale, (SpriteEffects)0, 0f);
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
		float num6 = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 22f * base.Projectile.scale, ref num6))
		{
			return true;
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.scale >= 0.5f;
	}
}
