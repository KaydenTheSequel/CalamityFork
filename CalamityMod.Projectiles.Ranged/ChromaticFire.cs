using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChromaticFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public static int Lifetime => 96;

	public ref float ColorType => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 52);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 5;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		Color effectcolor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.DeepSkyBlue, 
			1 => Color.MediumSpringGreen, 
			2 => Color.DarkOrange, 
			_ => Color.Violet, 
		});
		Time++;
		ColorType += 0.02f;
		if (!(Time >= 1f))
		{
			return;
		}
		base.Projectile.scale = 1.8f * Utils.GetLerpValue(5f, 30f, Time, clamped: true);
		if (Time == 1f)
		{
			for (int i = 0; i < 12; i++)
			{
				float rotMulti = Main.rand.NextFloat(0.7f, 1.1f);
				int dustType = (Main.rand.NextBool() ? 66 : 247);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType);
				dust.scale = Main.rand.NextFloat(1.8f, 2.5f) - rotMulti;
				dust.noGravity = true;
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5f * rotMulti) * Main.rand.NextFloat(0.5f, 1.8f) * rotMulti;
				dust.alpha = Main.rand.Next(90, 150);
				dust.color = effectcolor;
			}
		}
		if (Time > 9f)
		{
			float dustArea = Main.rand.NextFloat(0.1f, 1.7f);
			int dustType2 = (Main.rand.NextBool() ? 66 : 247);
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(9f, 9f) + base.Projectile.velocity * Main.rand.NextFloat(-1.8f, 1.8f), dustType2);
			dust2.scale = (1.8f - dustArea) * 0.65f;
			dust2.noGravity = true;
			dust2.velocity = Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * dustArea;
			dust2.alpha = Main.rand.Next(90, 150);
			dust2.color = effectcolor;
		}
		float smokeRot = MathHelper.ToRadians(3f);
		Color smokeColor = Main.hslToRgb(0.5f * (ColorType % 1f) + 0.5f * Utils.GetLerpValue(30f, Lifetime, Time, clamped: true) * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.7f);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, smokeColor, 12, base.Projectile.scale * Main.rand.NextFloat(0.6f, 1.2f), 0.45f, smokeRot, glowing: true, 0f, required: true));
		if (Main.rand.NextBool(5))
		{
			Color glowColor = Color.Lerp(smokeColor, Color.White, 0.3f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, glowColor, 9, base.Projectile.scale * Main.rand.NextFloat(0.4f, 0.7f), 0.2f, smokeRot, glowing: true, 0.005f));
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref smokeColor)).ToVector3() * base.Projectile.scale * 0.3f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 52f * base.Projectile.scale * 0.5f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 1200);
	}
}
