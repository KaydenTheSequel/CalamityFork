using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CausticEdgeProjectile : ModProjectile, ILocalizedModType, IModType
{
	private const float ColorAlternateTime = 30f;

	private const int TimeLeft = 600;

	private const int MaxAlpha = 128;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.alpha = 128;
	}

	public override void AI()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[2]++;
		if (base.Projectile.ai[2] >= 30f)
		{
			base.Projectile.ai[2] = -30f;
		}
		Vector3 val = new Vector3(0.9f, 0f, 0.9f);
		Vector3 colorTwo = default(Vector3);
		((Vector3)(ref colorTwo))._002Ector(0.6f, 1.2f, 0f);
		float colorScalar = Math.Abs(base.Projectile.ai[2]) / 30f;
		Vector3 lightColor = Vector3.Lerp(val, colorTwo, colorScalar);
		Lighting.AddLight(base.Projectile.Center, lightColor.X, lightColor.Y, lightColor.Z);
		if (base.Projectile.localAI[1] > 7f)
		{
			float dustScale = 1.25f * base.Projectile.scale;
			int maxRandom = 10;
			int chanceOfGreenDust = (int)Math.Round(MathHelper.Lerp(0f, (float)maxRandom, colorScalar));
			int dustType = ((Main.rand.Next(maxRandom) < chanceOfGreenDust) ? 74 : 171);
			int dust = Dust.NewDust(new Vector2(base.Projectile.position.X - base.Projectile.velocity.X + 2f, base.Projectile.position.Y + 2f - base.Projectile.velocity.Y), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= -0.25f;
			if (dustType == 171)
			{
				Main.dust[dust].fadeIn = 1.5f;
			}
			dustType = ((Main.rand.Next(maxRandom) < chanceOfGreenDust) ? 74 : 171);
			dust = Dust.NewDust(new Vector2(base.Projectile.position.X - base.Projectile.velocity.X + 2f, base.Projectile.position.Y + 2f - base.Projectile.velocity.Y), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj2 = Main.dust[dust];
			obj2.velocity *= -0.25f;
			Dust obj3 = Main.dust[dust];
			obj3.position -= base.Projectile.velocity * 0.5f;
			if (dustType == 171)
			{
				Main.dust[dust].fadeIn = 1.5f;
			}
		}
		if (base.Projectile.localAI[1] < 15f)
		{
			base.Projectile.localAI[1]++;
		}
		else if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale -= 0.02f;
			base.Projectile.alpha += 15;
			if (base.Projectile.alpha >= 123)
			{
				base.Projectile.alpha = 128;
				base.Projectile.localAI[0] = 1f;
			}
		}
		else if (base.Projectile.localAI[0] == 1f)
		{
			base.Projectile.scale += 0.02f;
			base.Projectile.alpha -= 15;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				base.Projectile.localAI[0] = 0f;
			}
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item8, base.Projectile.Center);
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = new Vector3(100f, 0f, 100f);
		Vector3 colorTwo = default(Vector3);
		((Vector3)(ref colorTwo))._002Ector(67f, 133f, 0f);
		Vector3 newLightColor = Vector3.Lerp(val, colorTwo, Math.Abs(base.Projectile.ai[2]) / 30f);
		return new Color((int)newLightColor.X, (int)newLightColor.Y, (int)newLightColor.Z, 0);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.5f;
			SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(70, 60);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.5f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int i = 4; i < 31; i++)
		{
			float oldXVel = base.Projectile.oldVelocity.X * (30f / (float)i);
			float oldYVel = base.Projectile.oldVelocity.Y * (30f / (float)i);
			float dustScale = 1.8f * base.Projectile.scale;
			int dustType = (Main.rand.NextBool() ? 74 : 171);
			int dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel * 0.5f, base.Projectile.oldPosition.Y - oldYVel * 0.5f), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			if (dustType == 171)
			{
				Main.dust[dust].fadeIn = 1.5f;
			}
			dustScale = 1.4f * base.Projectile.scale;
			dustType = (Main.rand.NextBool() ? 74 : 171);
			dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel * 0.5f, base.Projectile.oldPosition.Y - oldYVel * 0.5f), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.1f;
			if (dustType == 171)
			{
				Main.dust[dust].fadeIn = 1.5f;
			}
		}
	}
}
