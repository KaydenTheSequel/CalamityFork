using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class UrsaSlash : ModProjectile, ILocalizedModType, IModType
{
	private static float radius = 85f;

	public bool visuals = true;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = (int)radius);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 20;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		if (visuals)
		{
			bool visible = Owner.Calamity().ursaSergeantVisual;
			Vector2 slashDir = Utils.RotatedByRandom(new Vector2(13f, 13f), 100.0);
			Vector2 slashPos1 = base.Projectile.Center + slashDir.RotatedBy(MathHelper.ToRadians(90f) * 1.25f);
			Vector2 slashPos2 = base.Projectile.Center + slashDir.RotatedBy(MathHelper.ToRadians(-90f) * 1.25f);
			Owner.SetScreenshake(4f);
			for (int i = 0; i < 3; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - slashDir * 6f, slashDir * 0.65f, affectedByGravity: false, 19, 0.085f * (1f - (float)i * 0.25f), Color.Coral * (visible ? 1f : 0.3f), new Vector2(1.9f, 1f), quickShrink: true, glow: false, 0.7f));
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(slashPos1 - slashDir * 6f, slashDir.RotatedBy(0.05999999865889549) * 0.65f, affectedByGravity: false, 19, 0.067f * (1f - (float)i * 0.25f), Color.DarkTurquoise * (visible ? 1f : 0.3f), new Vector2(1.9f, 1f), quickShrink: true, glow: false, 0.7f));
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(slashPos2 - slashDir * 6f, slashDir.RotatedBy(-0.05999999865889549) * 0.65f, affectedByGravity: false, 19, 0.067f * (1f - (float)i * 0.25f), Color.DarkTurquoise * (visible ? 1f : 0.3f), new Vector2(1.9f, 1f), quickShrink: true, glow: false, 0.7f));
			}
			for (int j = 0; j <= 9; j++)
			{
				int dustStyle = ModContent.DustType<SquashDust>();
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center - slashDir * 6f, dustStyle);
				dust.scale = Main.rand.NextFloat(0.8f, 1.7f) * (visible ? 1f : 0.3f) * (Main.rand.NextBool(5) ? 1.5f : 1f);
				dust.velocity = slashDir.RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.3f, 2f);
				dust.noGravity = true;
				dust.color = Color.Coral;
				dust.fadeIn = 0.5f;
				if (!visible)
				{
					dust.noLight = true;
					dust.noLightEmittence = true;
				}
				Dust dust2 = Dust.NewDustPerfect(slashPos1 - slashDir * 6f, dustStyle);
				dust2.scale = Main.rand.NextFloat(0.8f, 1.7f) * (visible ? 1f : 0.3f) * (Main.rand.NextBool(5) ? 1.5f : 1f);
				dust2.velocity = slashDir.RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.3f, 2f);
				dust2.noGravity = true;
				dust2.color = Color.DarkTurquoise;
				dust2.fadeIn = 0.5f;
				if (!visible)
				{
					dust2.noLight = true;
					dust2.noLightEmittence = true;
				}
				Dust dust3 = Dust.NewDustPerfect(slashPos2 - slashDir * 6f, dustStyle);
				dust3.scale = Main.rand.NextFloat(0.8f, 1.7f) * (visible ? 1f : 0.3f) * (Main.rand.NextBool(5) ? 1.5f : 1f);
				dust3.velocity = slashDir.RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.3f, 2f);
				dust3.noGravity = true;
				dust3.color = Color.DarkTurquoise;
				dust3.fadeIn = 0.5f;
				if (!visible)
				{
					dust3.noLight = true;
					dust3.noLightEmittence = true;
				}
			}
			if (visible)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AstralSlash", 3);
				style.Volume = 0.65f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfLargeHit", 3);
				style.Volume = 0.85f;
				style.Pitch = Main.rand.NextFloat(0.4f, 0.5f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		visuals = false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.7f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}

	public override bool? CanDamage()
	{
		return base.CanDamage();
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
