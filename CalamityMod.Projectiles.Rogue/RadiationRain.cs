using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RadiationRain : ModProjectile, ILocalizedModType, IModType
{
	public NPC targetedNPC;

	public int time;

	public Vector2 spawnSpot;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 10;
		base.Projectile.timeLeft = 200;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30 * base.Projectile.MaxUpdates;
		base.Projectile.ArmorPenetration = 30;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (time == 0)
		{
			float orbScale = 0.6f * Main.rand.NextFloat(0.8f, 1.1f);
			Color smokeColor = Color.Lerp(Color.DimGray, Color.DarkGreen, Main.rand.NextFloat(0.2f, 0.6f));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, smokeColor * 0.7f, "CalamityMod/ExtraTextures/GreyscaleVortex", new Vector2(1f, 1f), base.Projectile.ai[2] * 0.45f, orbScale, orbScale * 1.1f, 12, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			CustomPulse customPulse = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Chartreuse, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.8f, 0.4f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			GeneralParticleHandler.SpawnParticle(customPulse);
			customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
			CustomPulse customPulse2 = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.4f, 0.2f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			GeneralParticleHandler.SpawnParticle(customPulse2);
			customPulse2.DrawLayer = GeneralDrawLayer.AfterEverything;
			for (int i = 0; i < 2; i++)
			{
				int dir = ((i == 0) ? 1 : (-1));
				GlowSparkParticle glowSparkParticle = new GlowSparkParticle(base.Projectile.Center + new Vector2((float)(20 * dir), 0f), new Vector2((float)(10 * dir), 0f), affectedByGravity: false, 12, 0.087f, Color.Chartreuse, new Vector2(1.7f, 0.8f), quickShrink: true, glow: true, 0.8f);
				GeneralParticleHandler.SpawnParticle(glowSparkParticle);
				glowSparkParticle.DrawLayer = GeneralDrawLayer.AfterEverything;
			}
			for (int j = 0; j < 3; j++)
			{
				smokeColor = Color.Lerp(Color.DimGray, Color.DarkGreen, Main.rand.NextFloat(0.2f, 0.6f));
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(27f, 27f), 100.0) * Main.rand.NextFloat(0.2f, 1f), smokeColor, Main.rand.Next(25, 41), Main.rand.NextFloat(0.7f, 1.3f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool()));
			}
			spawnSpot = base.Projectile.Center;
			targetedNPC = base.Projectile.Center.ClosestNPCAt(1200f);
			if (targetedNPC != null)
			{
				base.Projectile.velocity = (targetedNPC.Center - base.Projectile.Center + targetedNPC.velocity * 1.5f).SafeNormalize(Vector2.UnitX) * 8f;
			}
			else
			{
				base.Projectile.velocity = (Owner.ClampedMouseWorld() - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 8f;
			}
		}
		if (targetDist < 1400f && time > 5)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f), base.Projectile.velocity * Main.rand.NextFloat(1.5f, 5f), affectedByGravity: false, 2, Main.rand.NextFloat(0.04f, 0.06f), Color.Lerp(Color.Green, Color.Chartreuse, Main.rand.NextFloat(0.2f, 1f)), new Vector2(0.2f * (3f * Utils.GetLerpValue(40f, 0f, time, clamped: true) + 1f), 1.5f), quickShrink: true, glow: false, 0.3f));
			if (time % 6 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new AltLineParticle(base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f), base.Projectile.velocity * Main.rand.NextFloat(4.5f, 9f), affectedByGravity: false, 12, Main.rand.NextFloat(0.9f, 1.1f) * (2f * Utils.GetLerpValue(40f, 0f, time, clamped: true) + 1f), Color.Lerp(Color.Green, Color.Chartreuse, Main.rand.NextFloat(0.2f, 1f))));
			}
			if (time % 4 == 0)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f), 267);
				dust.velocity = base.Projectile.velocity * Main.rand.NextFloat(0.5f, 3f);
				dust.scale = Main.rand.NextFloat(0.45f, 0.75f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Green : Color.Chartreuse, 0.7f);
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
		}
		if (time % 10 * base.Projectile.MaxUpdates == 0)
		{
			Owner.SetScreenshake(1.5f);
		}
		if (base.Projectile.ai[2] > 0f && time == 30)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, base.Projectile.velocity, ModContent.ProjectileType<RadiationRain>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] - 1f);
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 60);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
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
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 60f, targetHitbox);
	}
}
