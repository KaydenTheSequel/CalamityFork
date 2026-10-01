using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AtaraxiaBoom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.1f, 0.45f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			int flowerPetalCount = 6;
			float thetaDelta = Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0).ToRotation();
			float weaveDistanceMin = 0.5f;
			float weaveDistanceOutwardMax = 10f;
			float weaveDistanceInner = 0.5f;
			for (float theta = 0f; theta < (float)Math.PI * 2f; theta += 0.05f)
			{
				float colorRando = Main.rand.NextFloat(0f, 1f);
				Vector2 velocity = theta.ToRotationVector2() * (weaveDistanceMin + (float)(Math.Sin(thetaDelta + theta * (float)flowerPetalCount) + 0.5 + (double)weaveDistanceInner) * weaveDistanceOutwardMax);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267, velocity);
				dust.noGravity = true;
				dust.scale = 1.15f;
				dust.color = Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando);
			}
			for (int k = 0; k < 5; k++)
			{
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.6f, 1.2f);
				float colorRando2 = Main.rand.NextFloat(0f, 1f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velocity2, velocity2, affectedByGravity: true, 50, Main.rand.NextFloat(0.7f, 0.95f), Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando2)));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.88f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (base.Projectile.ai[0] == 1f) ? 130 : 200, targetHitbox);
	}
}
