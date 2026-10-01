using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GreenWater : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeLeft = 300;

	public Vector2 storedVel;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ArmorPenetration = 40;
			if (base.Projectile.timeLeft == 300)
			{
				storedVel = base.Projectile.velocity;
				base.Projectile.velocity = -base.Projectile.velocity;
			}
			if (base.Projectile.timeLeft > 240)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.95f;
				base.Projectile.rotation = storedVel.ToRotation() + (float)Math.PI / 4f;
			}
			else
			{
				if (base.Projectile.timeLeft == 240)
				{
					base.Projectile.velocity = storedVel;
				}
				NPC target = Owner.Calamity().mouseWorld.ClosestNPCAt(500f);
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, target, ignoreTiles: false, 0.3f, 10f, 0.98f);
				if (Main.rand.NextBool(5))
				{
					GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3f + Main.rand.NextVector2Circular(6f, 6f), -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.6f), affectedByGravity: true, 13, Main.rand.NextFloat(0.55f, 0.8f), Color.DarkRed * 0.8f, AddativeBlend: false, needed: false, GlowCenter: false));
				}
			}
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			NPC target2 = base.Projectile.Center.ClosestNPCAt(155f);
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, target2, ignoreTiles: false, 0.15f, 9f, 0.98f, 0.95f, accelerate: true);
		}
		else
		{
			base.Projectile.scale = 1.2f;
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 8f;
			float fade = Utils.GetLerpValue(255f, 0f, base.Projectile.alpha);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + offset, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 7, 0.7f, Color.Aqua * 0.6f * fade));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - offset, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 7, 0.7f, Color.Aqua * 0.6f * fade));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3f + Main.rand.NextVector2Circular(6f, 6f), 102, (-base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 9f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.8f), 180, default(Color), Main.rand.NextFloat(0.8f, 1.4f));
			dust.noGravity = true;
			dust.alpha = MathHelper.Clamp(base.Projectile.alpha, 0, 180);
			if (base.Projectile.timeLeft > 130)
			{
				base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 150f, 130f, 255f, 0f);
			}
		}
		if (Main.rand.NextBool())
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3f + Main.rand.NextVector2Circular(6f, 6f), 5, (-base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(0.8f, 1.4f));
			dust2.noGravity = true;
			dust2.alpha = MathHelper.Clamp(base.Projectile.alpha, 0, 100);
		}
		if (base.Projectile.timeLeft <= 60)
		{
			base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 0f, 60f, 255f, 0f);
		}
		if (base.Projectile.timeLeft > 280)
		{
			base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 300f, 280f, 255f, 0f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 2f)
		{
			target.AddBuff(103, 300);
			target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 300);
		}
		if (base.Projectile.ai[0] == 0f)
		{
			target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
		}
		for (int i = 0; i <= 4; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, 5, (base.Projectile.velocity * 2.5f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.8f)).noGravity = false;
		}
		for (int j = 0; j <= 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center, (base.Projectile.velocity * 4.5f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.1f, 0.8f) + new Vector2(0f, -2f), affectedByGravity: true, 20, 0.5f, Color.DarkRed * 0.7f));
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfSmallHit", 3);
		style.Volume = 0.5f;
		style.Pitch = Main.rand.NextFloat(-0.2f, -0.3f);
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.25f;
		int hitsToMinMult = 6;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}
}
