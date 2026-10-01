using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PhasedGodRay : ModProjectile, ILocalizedModType, IModType
{
	private const float LaserLength = 80f;

	private const float LaserLengthChangeRate = 2f;

	private const float WaveTheta = 0.09f;

	private const int WaveTwistFrames = 9;

	public new string LocalizationCategory => "Projectiles.Magic";

	private ref float WaveFrameState => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = 280;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		float waveSign = ((WaveFrameState < 0f) ? (-1f) : 1f);
		if (Math.Abs(WaveFrameState) < 1f)
		{
			float dirToUse = ((WaveFrameState != 0f) ? waveSign : (Main.rand.NextBool() ? (-1f) : 1f));
			waveSign = 0f - dirToUse;
			WaveFrameState = dirToUse * 9f * 0.5f;
			float iterRotation = base.Projectile.velocity.ToRotation();
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				base.Projectile.oldRot[i] = iterRotation;
				iterRotation += waveSign * 0.09f;
			}
		}
		else if (Math.Abs(WaveFrameState) > 9f)
		{
			WaveFrameState = 0f - waveSign;
		}
		else
		{
			WaveFrameState += waveSign;
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(waveSign * 0.09f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Lighting.AddLight(base.Projectile.Center, 0.87f, 0.65f, 0.1725f);
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.localAI[0] += 10f;
			if (base.Projectile.localAI[0] > 80f)
			{
				base.Projectile.localAI[0] = 80f;
			}
		}
		else
		{
			base.Projectile.localAI[0] -= 2f;
			if (base.Projectile.localAI[0] <= 0f)
			{
				base.Projectile.Kill();
			}
		}
		float fadeInLerp = Utils.GetLerpValue(280f, 265f, base.Projectile.timeLeft, clamped: true);
		Color beamColor = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, fadeInLerp);
		Color subColor = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, Main.rand.NextFloat());
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 7, 0.65f - 0.3f * fadeInLerp, beamColor * 0.8f, new Vector2(1.1f - 0.4f * fadeInLerp, 0.8f + 0.6f * fadeInLerp), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.6f));
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 5, 0.35f - 0.15f * fadeInLerp, Color.Lerp(beamColor, Color.White, 0.6f), new Vector2(0.7f, 1.4f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.6f));
		if (base.Projectile.timeLeft == 280)
		{
			for (int j = 0; j < 3; j++)
			{
				subColor = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, Main.rand.NextFloat());
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity * 2f, base.Projectile.velocity.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.5f, 0.7f), "CalamityMod/Particles/SemiCircularSmearVerticalBlank", affectedByGravity: false, 10, 0.45f, subColor, new Vector2(0.8f, 1.2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>());
				dust.scale = Main.rand.NextFloat(0.6f, 1.4f);
				dust.noGravity = true;
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.3f, 0.8f);
				dust.color = subColor;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		for (int i = 0; i < 2; i++)
		{
			Color color = AetherfluxCannon.accentColor;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 20, Main.rand.NextFloat(0.15f, 0.25f) * 3f, color, new Vector2(1f, 1f)));
		}
		base.Projectile.timeLeft = 280;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldShoot");
		style.Volume = 0.4f;
		style.Pitch = Main.rand.NextFloat(0.5f, 0.65f);
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (base.Projectile.damage > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.865f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return new Color(222, 166, 44, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			Color subColor = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, Main.rand.NextFloat());
			float rot = Main.rand.NextFloat(-0.1f, 0.1f);
			Vector2 startVel = (base.Projectile.velocity * 0.1f).RotatedBy(rot);
			Vector2 endVel = startVel.RotatedBy(rot * 5f) * 20f;
			GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center, startVel, endVel, "CalamityMod/Particles/BloomCircle", Main.rand.Next(15, 21), Main.rand.NextFloat(0.015f, 0.025f) * 15f, subColor, new Vector2(1.1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.4f, 0.015f));
		}
	}
}
