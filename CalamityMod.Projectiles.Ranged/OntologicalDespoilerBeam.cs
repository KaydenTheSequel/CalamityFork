using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class OntologicalDespoilerBeam : ModProjectile, ILocalizedModType, IModType
{
	public Color baseColor;

	public bool fading;

	public int sineDir;

	public float fadeMultiplier;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 80;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		bool inRange = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center) < 1400f;
		if (sineDir == 0)
		{
			sineDir = (Main.rand.NextBool() ? 1 : (-1));
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (time <= 5f)
		{
			base.Projectile.scale = 0.1f;
		}
		else if (base.Projectile.scale < 1f && !fading)
		{
			base.Projectile.scale += 0.07f;
		}
		else if (fading)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.992f;
			base.Projectile.scale *= 0.992f;
		}
		if (fading)
		{
			fadeMultiplier -= 0.007f;
		}
		if ((time > 11f) & inRange)
		{
			if (fadeMultiplier > 0.01f)
			{
				for (int i = -1 * sineDir; (sineDir == 1) ? (i <= 1) : (i >= -1); i += 2 * sineDir)
				{
					float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.25f / (float)Math.PI);
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 35f * (float)i * fadeMultiplier, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f * Main.rand.NextFloat(0.6f, 0.8f), "CalamityMod/Particles/Light", affectedByGravity: false, 45, 0.4f * fadeMultiplier * MathHelper.Lerp(Math.Abs(sine), 0.55f, 0.6f), (i == 1) ? Color.White : Color.Black, new Vector2(1f, 1f), i == 1, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0f, 1f, 1f, flipHorizontal: false, noShrink: true));
				}
			}
			if (Main.rand.NextBool(4) && !fading)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), base.Projectile.velocity.RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(2.3f, 5.8f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.3f, 2.15f);
				dust.color = baseColor;
			}
			if (Main.rand.NextBool(12) && !fading)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity * 8f, base.Projectile.velocity * Main.rand.NextFloat(0.5f, 4f), "CalamityMod/Particles/DrainLine", affectedByGravity: false, 80, 4.5f * base.Projectile.scale, Color.Black, new Vector2(0.4f, 3f), useAddativeBlend: false));
			}
			if (time % (float)((!fading) ? 1 : 2) == 0f)
			{
				for (int j = 0; j < 2; j++)
				{
					bool glow = j == 0;
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity * 8f, -base.Projectile.velocity * 0.1f, glow ? "CalamityMod/Particles/VoidBeamGlow" : "CalamityMod/Particles/VoidBeam", affectedByGravity: false, 23, 2.3f * base.Projectile.scale, glow ? baseColor : Color.Black, Vector2.One, glow, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
				}
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 30; i++)
		{
			float dustPower = Main.rand.NextFloat(0.2f, 1f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), (base.Projectile.velocity * 15f * (dustPower * dustPower)).RotatedByRandom(1f - dustPower * dustPower) * Main.rand.NextFloat(0.9f, 1f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(2.15f, 3.45f) * dustPower;
			dust.color = baseColor;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278, (base.Projectile.velocity * 25f * (dustPower * dustPower)).RotatedByRandom(1f - dustPower * dustPower) * Main.rand.NextFloat(0.9f, 1f));
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(1.35f, 1.75f) * dustPower;
			dust2.color = baseColor;
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(base.Projectile.Center, (base.Projectile.velocity * 15f * (dustPower * dustPower)).RotatedByRandom(1f - dustPower * dustPower) * Main.rand.NextFloat(0.9f, 1f), affectedByGravity: false, Main.rand.Next(30, 39), Main.rand.NextFloat(2.6f, 3.3f) * dustPower, Color.Black));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.5f, 1.35f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int j = 0; j < 6; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 1.05f - (float)j * 0.1f, 0.4f, 25 - j * 2, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		fading = true;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.5f;
		base.Projectile.scale = 1.3f;
		SoundStyle style;
		for (int k = 0; k < 3; k++)
		{
			style = new SoundStyle("CalamityMod/Sounds/Item/OntologicalDespoilerLargeImpact");
			style.Volume = 0.65f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		style = new SoundStyle("CalamityMod/Sounds/Item/MeldExplosion");
		style.Volume = 1f;
		style.Pitch = Main.rand.NextFloat(-0.5f, -0.6f);
		style.MaxInstances = -1;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SetCrit();
		float critDamage = Math.Min(Main.player[base.Projectile.owner].GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		modifiers.SourceDamage *= 1f + critDamage;
		Vector2 launchVel = base.Projectile.velocity;
		float launchPower = 60f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits >= 1)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public OntologicalDespoilerBeam()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		baseColor = Color.White;
		fadeMultiplier = 1f;
		base._002Ector();
	}
}
