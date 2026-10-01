using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class HolyStarDamage : ModProjectile, ILocalizedModType, IModType
{
	private bool started;

	public int time;

	public bool reachedMaxDamage;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public override void SetDefaults()
	{
		base.Projectile.localAI[1] = Main.rand.NextFloat(30f);
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 200;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		Lighting.AddLight(base.Projectile.Center, 0.45f, 0.35f, 0f);
		if (!started)
		{
			Color cl = Color.Goldenrod;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, cl, "CalamityMod/Particles/BlastCone", new Vector2(Main.rand.NextFloat(2f, 4f), 1.5f), Vector2.Zero.AngleTo(base.Projectile.velocity), 1f * base.Projectile.scale, 0f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			started = true;
		}
		if (base.Projectile.ai[0] < 240f)
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.timeLeft < 160)
			{
				base.Projectile.timeLeft = 160;
			}
		}
		if (base.Projectile.ai[1] == 5f)
		{
			if (base.Projectile.ai[2] != -1f && time > 20)
			{
				NPC targeted = ((base.Projectile.ai[2] == -1f) ? null : Main.npc[(int)base.Projectile.ai[2]]);
				if (targeted != null && (targeted.life <= 0 || !targeted.CanBeChasedBy(base.Projectile)))
				{
					targeted = null;
				}
				else
				{
					base.Projectile.timeLeft++;
				}
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted ?? base.Projectile.Center.ClosestNPCAt(800f), ignoreTiles: true, 0.5f, 12f, 0.975f);
			}
			if ((double)base.Projectile.scale < 1.15)
			{
				base.Projectile.scale += 0.004f;
			}
			if ((double)base.Projectile.scale >= 1.15 && !reachedMaxDamage)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.3f, 8, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				reachedMaxDamage = true;
			}
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		base.Projectile.localAI[1] += ((Vector2)(ref base.Projectile.velocity)).Length() / 20f;
		Color col = ((base.Projectile.ai[1] == 5f) ? Color.Lerp(Color.Goldenrod, Color.Orchid, Utils.GetLerpValue(0.85f, 1f, base.Projectile.scale)) : Color.Goldenrod);
		GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.8f, affectedByGravity: false, 5, 0.06f * base.Projectile.scale, col * 0.7f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 1.5f));
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 240);
		if (base.Projectile.ai[1] == 5f)
		{
			modifiers.SourceDamage *= base.Projectile.scale;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		float lerpMult = MathHelper.Lerp(0.5f, 1.5f, Math.Abs((float)Math.Sin(base.Projectile.localAI[1] / 10f)));
		Texture2D value = TextureAssets.Projectile[base.Projectile.type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Color val = Color.Goldenrod;
		((Color)(ref val)).A = 150;
		Color baseColor = val;
		val = Color.Khaki;
		((Color)(ref val)).A = 150;
		Color baseColor2 = val;
		baseColor *= lerpMult;
		baseColor2 *= lerpMult;
		Vector2 origin = value.Size() / 2f;
		Vector2 scale = new Vector2(0.5f, 1.5f) * lerpMult;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		base.Projectile.rotation += MathHelper.ToRadians(lerpMult * 4f);
		float upRight = (float)Math.PI / 4f;
		float up = (float)Math.PI / 2f;
		float upLeft = (float)Math.PI * 3f / 4f;
		float left = (float)Math.PI;
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, upLeft + base.Projectile.rotation, origin, scale * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, upRight - base.Projectile.rotation, origin, scale * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, upLeft + base.Projectile.rotation, origin, scale * 0.6f * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, upRight - base.Projectile.rotation, origin, scale * 0.6f * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, up + base.Projectile.rotation, origin, scale * 0.6f * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, left - base.Projectile.rotation, origin, scale * 0.6f * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, up + base.Projectile.rotation, origin, scale * 0.36f * base.Projectile.scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, left - base.Projectile.rotation, origin, scale * 0.36f * base.Projectile.scale, spriteEffects);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		int dustType = ModContent.DustType<LightDust>();
		for (int i = 0; i < 7; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType, ((new Vector2(7f, 7f) * base.Projectile.scale).RotatedByRandom(100.0) + (reachedMaxDamage ? Vector2.Zero : base.Projectile.velocity)) * Main.rand.NextFloat(0.2f, 1f) * (float)((!reachedMaxDamage) ? 1 : 2));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.95f, 1.45f) * base.Projectile.scale;
			dust.color = (Main.rand.NextBool(4) ? (reachedMaxDamage ? Color.Orchid : Color.Khaki) : Color.Goldenrod);
			dust.noLightEmittence = true;
		}
		if (reachedMaxDamage)
		{
			SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
			{
				Volume = 0.7f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Goldenrod, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 1.5f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int j = 0; j < 4; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.3f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(1.15f, 1.3f), Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f)), new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.3f, 0.4f)));
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BurningHolyBlast>(), (int)((float)base.Projectile.damage * 1.2f), base.Projectile.knockBack, base.Projectile.owner, 0.75f).ArmorPenetration = 30;
			}
		}
	}

	public override bool? CanDamage()
	{
		if (time <= 15)
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 50f, targetHitbox);
	}
}
