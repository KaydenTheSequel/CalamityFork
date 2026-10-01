using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AstralGodRay : ModProjectile, ILocalizedModType, IModType
{
	private const float LaserLength = 80f;

	private const float LaserLengthChangeRate = 2f;

	private const float WaveTheta = 0.09f;

	private const int WaveTwistFrames = 9;

	public new string LocalizationCategory => "Projectiles.Boss";

	private ref float WaveFrameState => ref base.Projectile.localAI[1];

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		if (Vector2.Distance(new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]), base.Projectile.Center) < 80f)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		float waveSign = ((WaveFrameState < 0f) ? (-1f) : 1f);
		if (Math.Abs(WaveFrameState) < 1f)
		{
			float dirToUse = ((WaveFrameState != 0f) ? waveSign : (Main.rand.NextBool() ? (-1f) : 1f));
			waveSign = 0f - dirToUse;
			WaveFrameState = dirToUse * 9f * 0.5f;
			float iterRotation = base.Projectile.velocity.ToRotation();
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				base.Projectile.oldRot[i] = iterRotation;
				iterRotation += waveSign * 0.09f;
			}
		}
		else if (Math.Abs(WaveFrameState) > 9f)
		{
			WaveFrameState = 0f - waveSign;
		}
		else
		{
			WaveFrameState += waveSign;
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(waveSign * 0.09f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.localAI[0] += 10f;
		if (base.Projectile.localAI[0] > 80f)
		{
			base.Projectile.localAI[0] = 80f;
			return;
		}
		base.Projectile.localAI[0] -= 2f;
		if (base.Projectile.localAI[0] <= 0f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (WaveFrameState < 0f)
		{
			return new Color(255, 100, 0, base.Projectile.alpha);
		}
		return new Color(0, 255, 200, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.DrawBeam(80f, 2f, lightColor, null, curve: true);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		int dustID = ((WaveFrameState < 0f) ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
		int dustAmt = 4;
		Vector2 dustPos = base.Projectile.Center - base.Projectile.velocity / 2f;
		Vector2 dustVel = base.Projectile.velocity / 4f;
		for (int i = 0; i < dustAmt; i++)
		{
			Dust dust = Dust.NewDustDirect(dustPos, 0, 0, dustID, 0f, 0f, 0, default(Color), 1.5f);
			dust.velocity += dustVel;
			dust.velocity *= Main.rand.NextFloat(0.4f, 1f);
			dust.noGravity = true;
		}
	}
}
