using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DragoonBigBolt : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float colorValue;

	public float sizeMult = 1f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 75;
		base.Projectile.timeLeft = 200;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		colorValue = MathHelper.Lerp(colorValue, 50f, 0.025f);
		Color usedColor = Color.Lerp(Color.Cyan, Color.Orchid, Utils.GetLerpValue(0f, 50f, colorValue));
		if (time == 0)
		{
			colorValue += 30f;
			sizeMult = base.Projectile.ai[1];
		}
		float num = Vector2.Distance(obj.Center, base.Projectile.Center);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (num < 1400f && base.Projectile.timeLeft > 5)
		{
			Vector2 pos = base.Projectile.Center;
			if (base.Projectile.timeLeft % 4 == 0)
			{
				if (time < 120)
				{
					float velMult = ((base.Projectile.ai[1] == 0.5f) ? 0.2f : (3f * sizeMult));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, base.Projectile.velocity * 1.2f * velMult, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 11, 0.15f * sizeMult, usedColor, new Vector2(2f, 0.8f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 1f));
					sizeMult *= 0.97f;
				}
				GeneralParticleHandler.SpawnParticle(new BoltParticle(pos, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 30, 0.6f, usedColor, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.3f));
			}
			if (Main.rand.NextBool(35))
			{
				GeneralParticleHandler.SpawnParticle(new BoltParticle(pos, base.Projectile.velocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.9f), affectedByGravity: false, 23, Main.rand.NextFloat(0.2f, 0.25f), usedColor, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.3f));
			}
			if (Main.rand.NextBool(10))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, base.Projectile.velocity * Main.rand.NextFloat(-0.4f, 0.4f), "CalamityMod/Particles/DrainLineBloom", affectedByGravity: false, 80, Main.rand.NextFloat(1.2f, 1.3f) * sizeMult, usedColor, new Vector2(1f, 4f), useAddativeBlend: true, glowCenter: true));
			}
			if (time % 5 == 0)
			{
				Dust dust = Dust.NewDustPerfect(pos, 278, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.5f, 1f), 0, default(Color), Main.rand.NextFloat(0.45f, 0.6f));
				dust.noGravity = true;
				dust.color = usedColor;
			}
		}
		if (base.Projectile.ai[1] == 0.5f && base.Projectile.timeLeft == 1)
		{
			for (int i = 0; i < 3; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Cyan, "CalamityMod/Particles/BloomCircle", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.1f, 1.48f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.1f, 0.925f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.15f;
		int hitsToMinMult = 3;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(144, 300);
		if (base.Projectile.ai[1] == 0.5f)
		{
			return;
		}
		_ = Main.player[base.Projectile.owner];
		if (base.Projectile.numHits == 0)
		{
			base.Projectile.timeLeft = 5;
			base.Projectile.velocity = Vector2.Zero;
			float fxScale = 3f;
			Vector2 pos = target.Center;
			for (int i = 0; i < (int)(7f * fxScale); i++)
			{
				GeneralParticleHandler.SpawnParticle(new BoltParticle(pos, (new Vector2(4f, 4f) * fxScale).RotatedByRandom(100.0) * Main.rand.NextFloat(0.3f, 1.9f), affectedByGravity: true, 13, Main.rand.NextFloat(0.1f, 0.15f) * fxScale, Main.rand.NextBool(5) ? Color.Cyan : Color.Orchid, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.7f));
				Dust dust = Dust.NewDustPerfect(pos, ModContent.DustType<LightDust>(), (new Vector2(5f, 5f) * fxScale).RotatedByRandom(100.0) * Main.rand.NextFloat(0.5f, 1f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.55f) * fxScale);
				dust.noGravity = !Main.rand.NextBool(3);
				dust.color = (Main.rand.NextBool(5) ? Color.Cyan : Color.Orchid);
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(pos, Vector2.Zero, Color.Cyan, "CalamityMod/Particles/HighResFoggyCircleHardEdge", new Vector2(1f, 1f), 0f, 0f, 0.0815f * fxScale, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(pos, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/BloomCircle", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 1.38f * fxScale, 0.5f * fxScale, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(pos, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.925f * fxScale, 0.2f * fxScale, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, 20f, ignoreKBImmune: true);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		float size = 45f * sizeMult * (float)((base.Projectile.numHits <= 0) ? 1 : 6);
		Player Owner = Main.player[base.Projectile.owner];
		if (time <= 1 && base.Projectile.ai[1] != 0.5f)
		{
			float _ = float.NaN;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, Owner.Center, size, ref _);
		}
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, size, targetHitbox);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
