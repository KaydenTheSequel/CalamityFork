using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class TundraFlameBlossomsOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float TimerForCharging => ref base.Projectile.ai[0];

	public ref float TypeOfFlowerOrb => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 360;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		TypeOfFlowerOrb = Main.rand.Next(2);
		TypeOfFlowerOrb = ((TypeOfFlowerOrb == 0f) ? 6 : 113);
		SpawnDust();
		DustTrail();
		TargetNPC();
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3());
		base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.velocity.X);
	}

	public void SpawnDust()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 45; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / 45f * (float)i).ToRotationVector2() * 4f;
				Dust.NewDustPerfect(base.Projectile.Center + velocity * 3.5f, (int)TypeOfFlowerOrb, velocity, 0, default(Color), 1.25f).noGravity = true;
			}
		}
		base.Projectile.localAI[0] = 1f;
	}

	public void DustTrail()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (int)TypeOfFlowerOrb);
		dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
		dust.noGravity = true;
		dust.scale = 2f;
		base.Projectile.netUpdate = true;
	}

	public void TargetNPC()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1700f, Owner);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, (potentialTarget.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * 30f, TimerForCharging);
			TimerForCharging += 0.001f;
			TimerForCharging = ((TimerForCharging > 1f) ? 1f : TimerForCharging);
		}
		base.Projectile.netUpdate = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		DustFlowerOnHit();
		target.AddBuff(323, 240);
		target.AddBuff(324, 240);
	}

	public void DustFlowerOnHit()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		int flowerPetalCount = Main.rand.Next(3, 6);
		float thetaDelta = base.Projectile.velocity.ToRotation();
		float weaveDistanceMin = 2f;
		float weaveDistanceOutwardMax = 3f;
		float weaveDistanceInner = 0.5f;
		for (float theta = 0f; theta < (float)Math.PI * 2f; theta += 0.05f)
		{
			Vector2 velocity = theta.ToRotationVector2() * (weaveDistanceMin + (float)(Math.Sin(thetaDelta + theta * (float)flowerPetalCount) + 0.5 + (double)weaveDistanceInner) * weaveDistanceOutwardMax);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (int)TypeOfFlowerOrb, velocity, 0, default(Color), 1.25f);
			dust.noGravity = true;
			dust.scale = 1.35f;
		}
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
