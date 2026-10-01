using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RedirectingFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float BurstIntensity => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (Time >= 18f)
		{
			NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1100f, ignoreTiles: false);
			if (potentialTarget != null)
			{
				HomeInOnTarget(potentialTarget);
			}
			float accelerationFactor = MathHelper.SmoothStep(1.025f, 1.015f, Utils.GetLerpValue(6f, 24f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true));
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 24f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= accelerationFactor;
			}
		}
		EmitDust();
		Time++;
	}

	public void HomeInOnTarget(NPC target)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		float oldSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		float delayFactor = Utils.GetLerpValue(20f, 35f, Time, clamped: true);
		float homeSpeed = MathHelper.Lerp(0f, 0.075f, delayFactor);
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(target.Center) * 16f, homeSpeed);
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * oldSpeed;
		Projectile projectile = base.Projectile;
		projectile.position += (target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * MathHelper.Lerp(25f, 1f, Utils.GetLerpValue(100f, 360f, base.Projectile.Distance(target.Center), clamped: true)) * delayFactor;
	}

	public Dust CreateDustInstance()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		int dustType = 264;
		Color brimstoneColor = Color.Lerp(Color.Red, Color.DarkViolet, (float)Math.Sin((float)base.Projectile.identity * 3f + Time / 12f) * 0.7f + 0.3f);
		Color dustColor = Color.Lerp(Color.Orange, Color.Yellow, Main.rand.NextFloat());
		dustColor = Color.Lerp(dustColor, brimstoneColor, (float)Math.Pow(BurstIntensity, 2.0));
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType);
		dust.color = dustColor;
		dust.noGravity = true;
		dust.noLight = true;
		return dust;
	}

	public void EmitDust()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust dust = CreateDustInstance();
				dust.velocity = ((float)Math.PI * 2f * (float)i / 5f).ToRotationVector2();
				dust.position += base.Projectile.velocity;
				dust.scale = MathHelper.Lerp(1f, 1.5f, BurstIntensity) * Main.rand.NextFloat(0.7f, 1.3f);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f) * BurstIntensity, 264);
				dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
				dust.color = Color.Lerp(Color.Orange, Color.Yellow, Main.rand.NextFloat(0.67f));
				dust.scale = MathHelper.Lerp(1f, 1.6f, BurstIntensity);
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}
}
