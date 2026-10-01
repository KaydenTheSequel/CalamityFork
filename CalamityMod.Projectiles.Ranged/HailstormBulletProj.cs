using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HailstormBulletProj : ModProjectile, ILocalizedModType, IModType
{
	public float rotIncrease;

	public bool rotDirection;

	public Vector2 startVelocity;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 18;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.timeLeft = 400;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 15;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10.5f;
			startVelocity = base.Projectile.velocity;
			rotDirection = Main.rand.NextBool();
			Projectile projectile = base.Projectile;
			projectile.velocity *= Main.rand.NextFloat(0.97f, 1.03f);
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 150f && base.Projectile.localAI[0] % 15f == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + base.Projectile.velocity * 18.5f, base.Projectile.velocity, affectedByGravity: false, 15, MathHelper.Clamp(-5f + base.Projectile.localAI[0] * 0.03f, 0f, 0.53f), Color.SkyBlue));
		}
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 5.5f, Main.rand.NextBool(3) ? 135 : 279, (-base.Projectile.velocity * 0.4f).RotatedBy(rotIncrease));
		dust.scale = 0.75f;
		dust.noGravity = true;
		rotIncrease += 0.1f * (float)((!rotDirection) ? 1 : (-1));
		Projectile projectile2 = base.Projectile;
		projectile2.velocity *= 0.984f;
		if (base.Projectile.localAI[0] > 100f && base.Projectile.localAI[0] < 300f && base.Projectile.localAI[0] % 9f == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, base.Projectile.velocity * 0.01f, affectedByGravity: false, 17, 0.6f, Color.SkyBlue));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		target.AddBuff(324, 120);
		if (hit.Crit)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenPhaseTransitionCrack");
			style.Volume = 0.35f;
			style.Pitch = 1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (!target.boss)
			{
				target.AddBuff(ModContent.BuffType<GlacialState>(), 120);
			}
			int points = 6;
			float radians = (float)Math.PI * 2f / (float)points;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
			Vector2 addedPlacement = startVelocity;
			float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
			for (int k = 0; k < points; k++)
			{
				Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.45f * rotRando);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity * 4.5f + addedPlacement, velocity * 7f, "CalamityMod/Particles/GlowBlade", affectedByGravity: false, 6, 0.025f, Color.SkyBlue * 0.9f, new Vector2(1.5f, 0.6f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.9f, 0.8f, 0.3f));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.CritDamage += 0.85f;
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] > 15f)
		{
			CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, lightColor);
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] < 399f)
		{
			SoundStyle style = SoundID.Item27 with
			{
				Volume = 0.3f,
				Pitch = 0.8f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int k = 0; k < 11; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.1f, 1.2f));
				dust.scale = Main.rand.NextFloat(0.9f, 1.5f);
				dust.noGravity = true;
			}
		}
	}
}
