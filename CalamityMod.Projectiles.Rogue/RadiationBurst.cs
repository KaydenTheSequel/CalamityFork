using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RadiationBurst : ModProjectile, ILocalizedModType, IModType
{
	public NPC targetedNPC;

	public int time;

	public int boomTime = 50;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = boomTime + 2;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (time < boomTime)
		{
			float fade = Utils.GetLerpValue(boomTime + 3, 0f, time);
			float numberOfDusts = 2f;
			float rotFactor = 360f / numberOfDusts;
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				MathHelper.ToRadians((float)i * rotFactor);
				Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 250f, 0.04f);
				velOffset *= Main.rand.NextFloat(25f, 45f) * fade;
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, affectedByGravity: false, (int)(14f - 5f * fade), Main.rand.NextFloat(1.1f, 1.25f) - 0.5f * fade, Color.Chartreuse));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velOffset * 2.5f, 278, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
				dust.noGravity = true;
				dust.color = Color.Chartreuse;
			}
		}
		if (time == boomTime && targetDist < 1400f)
		{
			for (int j = 0; j <= 30; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 278 : 75, Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.3f));
				dust2.noGravity = true;
				if (dust2.type == 278)
				{
					dust2.noGravity = false;
					dust2.color = Color.Chartreuse;
				}
				else
				{
					dust2.scale *= 1.5f;
				}
			}
			for (int k = 0; k < 3; k++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 18f, base.Projectile.velocity, Color.Chartreuse * 0.7f, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.25f + (float)k * 0.11f, 18 - k * 3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Chartreuse, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 1.5f, 0.5f, 32, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 1f, 0.2f, 32, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			Owner.SetScreenshake(3.5f);
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return time >= boomTime && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 350f, targetHitbox);
	}
}
