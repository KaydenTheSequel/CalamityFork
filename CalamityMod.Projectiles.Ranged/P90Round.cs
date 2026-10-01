using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class P90Round : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 35;
		base.Projectile.height = 35;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 5;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 30;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.SolidCollision(base.Projectile.Center, 5, 5))
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.timeLeft == 598)
		{
			for (int i = 0; i <= 3; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 303 : 244, (base.Projectile.velocity * Main.rand.NextFloat(0.2f, 1.1f)).RotatedByRandom(0.20000000298023224));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.8f, 1.4f);
			}
		}
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Vector3 DustLight = default(Vector3);
		((Vector3)(ref DustLight))._002Ector(0.19f, 0.19f, 0.19f);
		Lighting.AddLight(base.Projectile.Center, DustLight * 2f);
		if (num < 1400f && base.Projectile.timeLeft < 596 && base.Projectile.timeLeft % 2 == 0)
		{
			int positionVariation = ((base.Projectile.timeLeft < 565) ? 25 : ((base.Projectile.timeLeft < 585) ? 12 : 5));
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center - base.Projectile.velocity * 0.75f + Main.rand.NextVector2Circular(positionVariation, positionVariation), -base.Projectile.velocity * Main.rand.NextFloat(0.003f, 0.001f), affectedByGravity: false, 4, 1.45f, Color.Chocolate));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center + base.Projectile.velocity.RotatedByRandom(0.30000001192092896), Vector2.Zero, Color.White, Color.Chocolate, Main.rand.NextFloat(0.7f, 1.5f), Main.rand.Next(9, 17), Main.rand.NextFloat(-0.01f, 0.01f), 2.5f));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 4; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 1.5f + Main.rand.NextVector2Circular(9f, 9f), Main.rand.NextBool(3) ? 303 : 244, (-base.Projectile.velocity * Main.rand.NextFloat(0.2f, 3f)).RotatedByRandom(MathHelper.ToRadians(20f)) * Main.rand.NextFloat(0.1f, 0.8f));
			dust.noGravity = true;
			dust.scale = ((dust.type == 244) ? Main.rand.NextFloat(1.8f, 2.5f) : Main.rand.NextFloat(1.4f, 1.8f));
			dust.fadeIn = ((dust.type == 244) ? 1.2f : 0f);
		}
	}
}
