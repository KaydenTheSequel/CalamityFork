using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class WildfireBloomFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public static int Lifetime => 96;

	public bool BlueFire => base.Projectile.ai[0] == 1f;

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 4;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time == 1f)
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f), (base.Projectile.velocity * 1f).RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(0.2f, 2.1f), Color.Lime, Color.Turquoise, Main.rand.NextFloat(0.8f, 1.9f), 180f, Main.rand.NextFloat(-3f, 3f)));
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f), (base.Projectile.velocity * 2f).RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.2f, 2.1f), Color.Lime, Color.Turquoise, Main.rand.NextFloat(0.4f, 1.1f), 180f, Main.rand.NextFloat(-3f, 3f)));
			for (int i = 0; i < 9; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(5) ? 135 : 107);
				dust.noGravity = true;
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.3f, 2.9f);
				dust.scale = Main.rand.NextFloat(1.3f, 2.1f);
			}
		}
		if (!(Time >= 1f))
		{
			return;
		}
		base.Projectile.scale = 1.8f * Utils.GetLerpValue(6f, 30f, Time, clamped: true);
		float smokeRot = MathHelper.ToRadians(3f);
		float colorValue = CalamityUtils.Convert01To010(Utils.GetLerpValue(30f, Lifetime, Time, clamped: true));
		Color smokeColor = Color.Lerp(Color.Lime, Color.Turquoise, colorValue);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, smokeColor, 18, base.Projectile.scale * Main.rand.NextFloat(0.6f, 1.2f), 0.4f, smokeRot, glowing: true, 0f, required: true));
		if (Time > 4f)
		{
			for (int j = 0; j < 2; j++)
			{
				float dustArea = Main.rand.NextFloat(0.1f, 1.7f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(9f, 9f) + base.Projectile.velocity * Main.rand.NextFloat(-1.8f, 1.8f), Main.rand.NextBool(5) ? 135 : 107);
				dust2.noGravity = true;
				dust2.velocity = Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * dustArea;
				dust2.scale = (1.8f - dustArea) * 0.65f;
			}
		}
		if (Main.rand.NextBool(5))
		{
			Color glowColor = Color.Gold;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, glowColor, 9, base.Projectile.scale * Main.rand.NextFloat(0.4f, 0.7f), 0.2f, smokeRot, glowing: true, 0.005f));
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref smokeColor)).ToVector3() * base.Projectile.scale * 0.3f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 52f * base.Projectile.scale * 0.5f, targetHitbox);
	}
}
