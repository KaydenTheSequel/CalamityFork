using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class Sandstream : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public bool PostHit;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time == 1)
		{
			for (int i = 0; i <= 10; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 313, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.2f, 1.2f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.7f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.7f, 2.3f);
			}
		}
		if (PostHit)
		{
			for (int j = 0; j < 4; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 216 : 32);
				dust2.noGravity = true;
				dust2.velocity = Utils.RotatedByRandom(new Vector2(0.5f, 0.5f), 100.0);
				dust2.scale = Main.rand.NextFloat(0.3f, 1.3f);
			}
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 313);
			dust3.noGravity = true;
			dust3.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.5f);
			dust3.scale = Main.rand.NextFloat(0.3f, 1.3f);
			base.Projectile.penetrate = 1;
			int falloffTime = 15;
			if (Time > falloffTime)
			{
				base.Projectile.velocity.X *= 0.9711f;
			}
			if (base.Projectile.velocity.Y < 15f && Time > falloffTime)
			{
				base.Projectile.velocity.Y += 0.16f;
			}
			if (base.Projectile.velocity.Y < 5f)
			{
				base.Projectile.velocity.Y *= 0.98f;
			}
			base.Projectile.extraUpdates = 3;
		}
		else
		{
			if (Main.rand.NextBool(5))
			{
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, (-base.Projectile.velocity * 0.2f).RotatedByRandom(0.20000000298023224), Color.Peru, Color.PeachPuff, MathHelper.Clamp(Main.rand.NextFloat(1.1f, 1.5f) - (float)Time * 0.02f, 0.5f, 2f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
			}
			for (int k = 0; k < 4; k++)
			{
				float DustArea = MathHelper.Clamp(3f - (float)Time * 0.03f, 1f, 3f);
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(DustArea, DustArea), Main.rand.NextBool(3) ? 216 : 32);
				dust4.noGravity = true;
				dust4.velocity = Utils.RotatedByRandom(new Vector2(0.5f, 0.5f), 100.0) + base.Projectile.velocity * 0.3f;
				dust4.scale = MathHelper.Clamp(Main.rand.NextFloat(1.4f, 1.9f) - (float)Time * 0.01f, 0.9f, 1.5f);
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust5 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 216 : 32);
				dust5.noGravity = false;
				dust5.velocity = new Vector2(0f, Main.rand.NextFloat(1f, 4f));
				dust5.scale = Main.rand.NextFloat(0.3f, 0.5f);
				dust5.fadeIn = 0.7f;
			}
			base.Projectile.velocity.Y += 0.035f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (!PostHit)
		{
			Time = 2;
		}
		PostHit = true;
		base.Projectile.timeLeft = 300;
		float numberOfDusts = 6f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0.5f, 1.5f), 0f), (double)(rot * Main.rand.NextFloat(1.1f, 9.1f)), default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0.5f, 1.5f), 0f), (double)(rot * Main.rand.NextFloat(1.1f, 9.1f)), default(Vector2));
			Dust dust = Dust.NewDustPerfect(target.Center + offset, Main.rand.NextBool(3) ? 288 : 207, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = false;
			dust.velocity = velOffset;
			dust.scale = Main.rand.NextFloat(0.5f, 0.8f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SandstreamScepterExplosion>(), base.Projectile.damage / 3, base.Projectile.knockBack * 4f, base.Projectile.owner);
		float numberOfDusts = 20f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(1.5f, 5.5f), 0f), (double)(rot * Main.rand.NextFloat(3.1f, 9.1f)), default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(1.5f, 5.5f), 0f), (double)(rot * Main.rand.NextFloat(3.1f, 9.1f)), default(Vector2));
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + offset, velOffset * Main.rand.NextFloat(1.5f, 3f), Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.9f, 1.2f), 160f, Main.rand.NextFloat(0.03f, -0.03f)));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, Main.rand.NextBool() ? 288 : 207, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = false;
			dust.velocity = velOffset;
			dust.scale = Main.rand.NextFloat(1.2f, 1.6f);
		}
	}
}
