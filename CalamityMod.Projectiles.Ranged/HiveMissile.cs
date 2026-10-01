using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HiveMissile : ModProjectile, ILocalizedModType, IModType
{
	public bool HasHit;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketID => ref base.Projectile.ai[0];

	public ref float ProjectileSpeed => ref base.Projectile.ai[1];

	public ref float Time => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 25;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 100;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.wet && (RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f))
		{
			HasHit = true;
			base.Projectile.Kill();
		}
		Time++;
		Color newColor;
		if (base.Projectile.timeLeft <= 50)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.965f;
			if (Time % 5f == 0f)
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f) - base.Projectile.velocity * 3.5f;
				int type = (Main.rand.NextBool(3) ? TheHiveHoldout.DustEffectsID : 303);
				Vector2? velocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.4f, 0.9f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(0.5f, 0.9f));
				dust.noGravity = false;
				if (dust.type != TheHiveHoldout.DustEffectsID)
				{
					dust.color = (Main.rand.NextBool(3) ? TheHiveHoldout.EffectsColor : TheHiveHoldout.StaticEffectsColor);
				}
			}
		}
		if (Main.dedServ)
		{
			return;
		}
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 40f, 0f, 0f, 255f);
		if (Time % 3f == 0f)
		{
			Vector2 center = base.Projectile.Center;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			int type2 = (Main.rand.NextBool() ? 303 : TheHiveHoldout.DustEffectsID);
			float scale = Main.rand.NextFloat(0.3f, 0.6f);
			newColor = default(Color);
			Dust trailDust = Dust.NewDustDirect(center, width, height, type2, 0f, 0f, 0, newColor, scale);
			trailDust.noGravity = true;
			trailDust.noLight = true;
			trailDust.noLightEmittence = true;
			trailDust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f);
			if (trailDust.type != TheHiveHoldout.DustEffectsID)
			{
				trailDust.color = (Main.rand.NextBool(3) ? TheHiveHoldout.EffectsColor : TheHiveHoldout.StaticEffectsColor);
			}
		}
		if (Time > 5f)
		{
			float sizeBonus = ((base.Projectile.timeLeft < 30) ? (Time * 0.003f) : 0f);
			Color smokeColor = Color.Lerp(Color.Black, Color.Lime, 0.25f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center - base.Projectile.velocity * 2f, -base.Projectile.velocity.RotatedByRandom(sizeBonus) * Main.rand.NextFloat(0.2f, 0.6f), smokeColor * 0.65f, 6, Main.rand.NextFloat(0.3f, 0.45f) + sizeBonus, 0.23f - sizeBonus * 0.3f, Main.rand.NextFloat(-0.2f, 0.2f)));
			if (Main.rand.NextBool())
			{
				Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f) - base.Projectile.velocity * 2.5f;
				Vector2? velocity2 = -base.Projectile.velocity.RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.05f, 0.4f);
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(position2, 303, velocity2, 0, newColor, Main.rand.NextFloat(0.8f, 1.4f));
				dust2.noGravity = false;
				dust2.color = Color.Black;
				dust2.alpha = Main.rand.Next(90, 221);
			}
		}
		Vector2 center2 = base.Projectile.Center;
		newColor = TheHiveHoldout.StaticEffectsColor;
		Lighting.AddLight(center2, ((Color)(ref newColor)).ToVector3() * 0.7f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		HasHit = true;
		target.AddBuff(ModContent.BuffType<Plague>(), 90);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		HasHit = true;
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		if (!HasHit)
		{
			return;
		}
		CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
		rocketBehaviorInfo.clusterProjectileID = 0;
		rocketBehaviorInfo.destructiveClusterProjectileID = 0;
		CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
		bool isClusterRocket = RocketID == 4445f || RocketID == 4446f;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PlagueBoom", 4);
		style.Volume = 0.5f;
		style.Pitch = -0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int blastRadius = (int)((float)base.Projectile.RocketBehavior(info) * 0.7f);
		base.Projectile.ExpandHitboxBy((float)blastRadius);
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		base.Projectile.penetrate = -1;
		base.Projectile.Damage();
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, TheHiveHoldout.StaticEffectsColor * 0.8f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), (float)base.Projectile.width / 22815f, (float)base.Projectile.width / 2275f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int k = 0; k < 15; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 303, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
			dust.scale = Main.rand.NextFloat(0.75f, 0.95f);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? TheHiveHoldout.EffectsColor : TheHiveHoldout.StaticEffectsColor);
		}
		float projAmount = (isClusterRocket ? 3f : 2f);
		if (Main.player[base.Projectile.owner].strongBees && Main.rand.NextBool())
		{
			projAmount++;
		}
		for (int i = 0; (float)i < projAmount; i++)
		{
			int BEES = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Utils.RotatedByRandom(new Vector2(4f, 10f), 4.0) * Main.rand.NextFloat(0.2f, 0.8f), ModContent.ProjectileType<BasicPlagueBee>(), (int)((float)base.Projectile.damage * (isClusterRocket ? 0.2f : 0.3f)), 0f, base.Projectile.owner, 0f, 0f, isClusterRocket ? 2f : 1f);
			if (BEES.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[BEES].penetrate = 1;
				Main.projectile[BEES].DamageType = DamageClass.Ranged;
			}
		}
	}
}
