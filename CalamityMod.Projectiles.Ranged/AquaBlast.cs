using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AquaBlast : ModProjectile, ILocalizedModType, IModType
{
	public int spreadDust;

	public static int Lifetime = 600;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.extraUpdates = 2;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 21)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 19)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.frame = base.Projectile.frameCounter / 4 % Main.projFrames[base.Type];
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.AliceBlue;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f);
		if (base.Projectile.timeLeft <= Lifetime - 7)
		{
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity / 0.18f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 5, 1.9f, Color.SeaGreen, fadeIn: true));
			}
		}
		if (Main.rand.NextBool(2))
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(1f, 1f), 411);
			gore.timeLeft = 6 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 0.8f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
		if (Main.rand.NextBool())
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(6 + spreadDust, 6 + spreadDust);
			int type = ((!Main.rand.NextBool(5)) ? 278 : 267);
			Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.35f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(0.4f, 0.6f));
			dust.noGravity = true;
			dust.color = ((!Main.rand.NextBool(5)) ? Color.Aquamarine : Color.Aqua);
			if (dust.type == 278)
			{
				dust.scale *= 0.7f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 60);
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Vector2 smokeVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 5f;
		int smokeAmount = Main.rand.Next(8, 13);
		for (int i = 0; i < smokeAmount; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeVel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(1.2f, 2f), Color.White, Main.rand.Next(40, 61), Main.rand.NextFloat(0.2f, 0.4f), 0.3f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.ShimmerWeak1 with
		{
			Pitch = 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 5; i++)
		{
			int bloodLifetime = Main.rand.Next(22, 25);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = Color.Lerp(Color.SkyBlue, Color.Aquamarine, Main.rand.NextFloat());
			bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
			if (Main.rand.NextBool(20))
			{
				bloodScale *= 2f;
			}
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 2.25f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit() * 2f * randomSpeedMultiplier;
			bloodVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Aquamarine * 0.8f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.05f, 22, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Aqua * 0.8f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.04f, 22, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DeepSkyBlue, "CalamityMod/Particles/DetailedExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.2f, 22, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, 0.6f, 11, produceLight: false));
	}
}
