using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VisceraBeam : ModProjectile, ILocalizedModType, IModType
{
	private int storedPenetrate;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 9;
		base.Projectile.height = 9;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		storedPenetrate = (base.Projectile.penetrate = 7);
		base.Projectile.MaxUpdates = 100;
		base.Projectile.timeLeft = 900;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		for (int i = 0; i <= 15; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool() ? 60 : 5));
			dust.position = base.Projectile.Center;
			dust.scale = Main.rand.NextFloat(0.8f, 1.3f);
			dust.velocity = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.1f, 0.9f);
			dust.noGravity = true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Laceration>(), 60);
		if (base.Projectile.ai[1] > 0f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<VisceraBoom>(), (int)((float)base.Projectile.damage * 0.75f), base.Projectile.knockBack * 4f, base.Projectile.owner, 0f, base.Projectile.ai[1]);
		}
		if (base.Projectile.ai[2] < 1f)
		{
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_OnHit(target), base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(0.5) * Main.rand.NextFloat(3f, 5f), ModContent.ProjectileType<BloodstoneHealOrb>(), 5, 0f, base.Projectile.owner);
			}
			base.Projectile.ai[2]++;
		}
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.penetrate = 1;
		}
		if (base.Projectile.ai[1] == 0f && base.Projectile.penetrate != storedPenetrate)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfLargeHit", 3);
			style.Volume = 0.7f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i <= 6; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool() ? 60 : 5));
				dust.scale = Main.rand.NextFloat(0.7f, 1.4f);
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.8f, 1.9f);
				dust.noGravity = true;
			}
			storedPenetrate = base.Projectile.penetrate;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] == 16f)
		{
			if (base.Projectile.ai[1] > 0f)
			{
				for (int j = 0; j <= 25; j++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool() ? 60 : 5));
					dust2.scale = Main.rand.NextFloat(0.9f, 1.9f);
					dust2.velocity = base.Projectile.velocity.RotatedByRandom(0.6) * Main.rand.NextFloat(1.8f, 2.9f);
					dust2.noGravity = true;
				}
			}
			else
			{
				for (int k = 0; k <= 10; k++)
				{
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool() ? 60 : 5));
					dust3.scale = Main.rand.NextFloat(0.7f, 1.4f);
					dust3.velocity = base.Projectile.velocity.RotatedByRandom(0.6) * Main.rand.NextFloat(0.8f, 1.9f);
					dust3.noGravity = true;
				}
			}
		}
		if (base.Projectile.localAI[0] > 16f)
		{
			int bloody = Dust.NewDust(base.Projectile.Center, 1, 1, (!ChildSafety.Disabled) ? 16 : 5);
			Main.dust[bloody].position = base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
			Main.dust[bloody].scale = Main.rand.NextFloat(0.3f, 0.8f);
			Main.dust[bloody].velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.6f);
			Main.dust[bloody].noGravity = true;
			if (base.Projectile.localAI[0] % 3f == 0f && targetDist < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center - base.Projectile.velocity * 0.5f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 7, 0.8f, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 0.5f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 4, 0.65f, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
