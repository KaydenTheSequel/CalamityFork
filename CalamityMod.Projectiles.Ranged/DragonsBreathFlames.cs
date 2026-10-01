using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DragonsBreathFlames : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public float beamWidth = 1.1f;

	public bool beamsize;

	public float bbbBONUSbeamSizeWOAHH = 50f;

	public bool postHit;

	public int beamWeldBloomReduction;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref int audioCooldown => ref Main.player[base.Projectile.owner].Calamity().DragonsBreathAudioCooldown;

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 6;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 500;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		Vector3 Light = ((base.Projectile.ai[0] == 1f) ? new Vector3(0.255f, 0.08f, 0.08f) : new Vector3(0.255f, 0.06f, 0f));
		Lighting.AddLight(base.Projectile.Center, Light * (float)((base.Projectile.ai[0] == 1f) ? 5 : 3));
		if (Time == 0)
		{
			bbbBONUSbeamSizeWOAHH += base.Projectile.ai[2];
			beamWidth = base.Projectile.ai[1];
			if (base.Projectile.ai[0] == 1f)
			{
				base.Projectile.penetrate = 1;
			}
		}
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		float sine = MathHelper.Clamp(Math.Abs((float)Math.Sin(((float)Time + base.Projectile.ai[1]) * 0.575f / (float)Math.PI)), 0.6f + (float)Time * 0.0015f, 1f);
		beamWidth = sine;
		if (base.Projectile.ai[0] == 0f)
		{
			if (Time == 0)
			{
				for (int i = 0; i <= 7; i++)
				{
					int DustType1 = 259;
					Vector2 dustyVelocity = base.Projectile.velocity * 4f;
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? DustType1 : 174);
					dust.scale = ((dust.type == DustType1) ? Main.rand.NextFloat(0.9f, 1.9f) : Main.rand.NextFloat(0.8f, 1.7f));
					dust.velocity = dustyVelocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.3f, 0.8f);
					dust.noGravity = true;
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? DustType1 : 174);
					dust2.scale = ((dust.type == DustType1) ? Main.rand.NextFloat(0.85f, 1.8f) : Main.rand.NextFloat(0.75f, 1.6f));
					dust2.velocity = dustyVelocity.RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(0.8f, 2.1f);
					dust2.noGravity = true;
				}
				for (int j = 0; j <= 5; j++)
				{
					Vector2 smokeVel = base.Projectile.velocity * 4.2f;
					float rotationAngle = Main.rand.NextFloat(-0.15f, 0.15f);
					smokeVel = smokeVel.RotatedBy(rotationAngle) * Main.rand.NextFloat(0.45f, 1.3f);
					float smokeScale = Main.rand.NextFloat(0.4f, 1.2f);
					GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(base.Projectile.Center, smokeVel, Color.DimGray, Main.rand.NextBool() ? Color.SlateGray : Color.Black, smokeScale, 100f));
				}
			}
			if (Time % 9 == 0 && (float)Time > 14f && targetDist < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(1f + (float)Time * 0.15f, 1f + (float)Time * 0.15f), -base.Projectile.velocity * 0.5f, affectedByGravity: false, 15, Main.rand.NextFloat(0.4f, 0.7f), Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed));
			}
			if (targetDist < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 10, 0.1f + (float)Time * 0.0015f, Color.OrangeRed * 0.5f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 10, 0.04f + (float)Time * 0.0015f, Color.Lerp(Color.Orange, Color.White, 0.25f) * 0.5f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
			}
			if (Time < 120)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.01f;
			}
		}
		if (base.Projectile.ai[0] == 1f)
		{
			if (bbbBONUSbeamSizeWOAHH > 0f)
			{
				bbbBONUSbeamSizeWOAHH -= 0.65f;
			}
			base.Projectile.extraUpdates = 20;
			if (Time > 0)
			{
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), Main.rand.NextBool(3) ? 162 : 259);
				dust3.scale = Main.rand.NextFloat(0.3f, 0.5f);
				dust3.noGravity = true;
				if (Main.rand.NextBool(6))
				{
					dust3.velocity = new Vector2(0f, Main.rand.NextFloat(-1f, -13f));
				}
				if (targetDist < 1400f)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 8, (0.2f + bbbBONUSbeamSizeWOAHH * 0.0015f) * beamWidth, Color.OrangeRed, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.6f));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 8, (0.1f + bbbBONUSbeamSizeWOAHH * 0.0015f) * beamWidth, Color.Lerp(Color.Orange, Color.White, 0.3f), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.6f));
				}
			}
			if (Time == 0)
			{
				for (int k = 0; k <= 3; k++)
				{
					Vector2 spinninpoint = base.Projectile.velocity * 3f;
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(scale: Main.rand.NextFloat(0.006f, 0.008f) * 2f, velocity: spinninpoint.RotatedByRandom(0.8500000238418579) * Main.rand.NextFloat(0.4f, 0.95f), relativePosition: base.Projectile.Center, affectedByGravity: false, lifetime: 8, color: Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed, squash: new Vector2(2f, 1f), quickShrink: true, glow: false, shrinkSpeed: 1.3f));
					float sparkScale2 = Main.rand.NextFloat(0.01f, 0.017f) * 2f;
					Vector2 sparkvelocity2 = spinninpoint.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(1.1f, 3.1f);
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, sparkvelocity2, affectedByGravity: false, 8, sparkScale2, Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed, new Vector2(2f, 1f), quickShrink: true, glow: false, 1.3f));
				}
			}
		}
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			if (beamWeldBloomReduction < 3)
			{
				if (audioCooldown == 0)
				{
					SoundEngine.PlaySound(in DragonsBreath.WeldingBurn, base.Projectile.Center);
					audioCooldown = 12;
				}
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 7, Main.rand.NextFloat(0.6f, 0.7f), Color.OrangeRed, Vector2.One));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 7, Main.rand.NextFloat(0.4f, 0.5f), Color.OrangeRed, Vector2.One, useAddativeBlend: true, glowCenter: true));
				if (!postHit)
				{
					for (int i = 0; i < 2; i++)
					{
						Vector2 SpawnPosition = target.Center + Main.rand.NextVector2Circular(target.width, target.height) * 0.04f;
						Vector2 spinninpoint = (base.Projectile.Center - SpawnPosition).SafeNormalize(Vector2.UnitY);
						int sparkLifetime = Main.rand.Next(22, 36);
						float sparkScale = Main.rand.NextFloat(0.8f, 1.3f);
						Color sparkColor = (Main.rand.NextBool(4) ? Color.OrangeRed : Color.Orange);
						Vector2 sparkVelocity = spinninpoint.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(12f, 25f);
						GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
					}
					for (int j = 0; j <= 3; j++)
					{
						Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, 278, base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(2.5f, 8.5f), 0, default(Color), Main.rand.NextFloat(0.5f, 1.1f));
						dust.noGravity = true;
						dust.color = (Main.rand.NextBool() ? Color.Orange : Color.OrangeRed);
					}
					for (int k = 0; k <= 3; k++)
					{
						Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, Main.rand.NextBool(3) ? 293 : 174, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.5f, 3.5f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.3f)).noGravity = true;
						Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, Main.rand.NextBool(3) ? 293 : 174, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(5f)) * Main.rand.NextFloat(0.8f, 5.8f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.3f)).noGravity = true;
					}
					postHit = true;
				}
				beamWeldBloomReduction++;
			}
			float sine = MathHelper.Clamp(Math.Abs((float)Math.Sin(((float)Time + base.Projectile.ai[1]) * 0.575f / (float)Math.PI)), 0.2f + (float)Time * 0.0022f, 1f);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.Lerp(Color.Orange, Color.OrangeRed, sine) * 0.7f, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(0.2f, 1.2f), 2.5f * Main.rand.NextFloat(0.9f, 1.2f), 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DragonsBreathBurst>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			target.AddBuff(ModContent.BuffType<Dragonfire>(), 420);
		}
		else if (!postHit)
		{
			for (int l = 0; l <= 5; l++)
			{
				Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, Main.rand.NextBool(3) ? 293 : 174, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.5f, 3.5f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.3f)).noGravity = true;
				Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, Main.rand.NextBool(3) ? 293 : 174, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(5f)) * Main.rand.NextFloat(0.8f, 5.8f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.3f)).noGravity = true;
			}
			for (int m = 0; m <= 2; m++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, 278, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.5f, 3.5f), 0, default(Color), Main.rand.NextFloat(0.8f, 1.5f));
				dust2.noGravity = true;
				dust2.color = (Main.rand.NextBool() ? Color.Orange : Color.OrangeRed);
			}
			postHit = true;
		}
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 1200);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (Time <= 1)
		{
			float _ = float.NaN;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, Owner.Center, base.Projectile.width, ref _);
		}
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (base.Projectile.ai[1] == 1f) ? 4f : ((float)base.Projectile.width + (float)Time * 0.1f), targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}
}
