using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PristineFire : ModProjectile, ILocalizedModType, IModType
{
	public float smokeOpa = 0.25f;

	public Vector2 beamPos;

	public Vector2 beamPos2;

	public bool hasIgnited;

	public int boomTime = PristineFury.boomTime;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Particles/MediumMist";

	public ref float time => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.timeLeft = 120;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		float mult = Utils.GetLerpValue(140f, 75f, base.Projectile.timeLeft, clamped: true);
		Vector2 center = base.Projectile.Center;
		Color val = Color.Lerp(Color.Red, Color.Goldenrod, mult);
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3() * 0.7f);
		if (base.Projectile.timeLeft < 116)
		{
			float sine = (float)Math.Sin(time * 0.65f / (float)Math.PI);
			beamPos = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * (30f * mult * -1f) * Utils.GetLerpValue(116f, 108f, base.Projectile.timeLeft, clamped: true);
			beamPos2 = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * (30f * mult) * Utils.GetLerpValue(116f, 108f, base.Projectile.timeLeft, clamped: true);
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark((i == 0) ? beamPos : beamPos2, base.Projectile.velocity * 0.1f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 6, 0.065f * mult + 0.01f, Color.Lerp(Color.Red, Color.Goldenrod, mult), new Vector2(1f, 2.5f)));
			}
		}
		if (!hasIgnited)
		{
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile projectile = Main.projectile[x];
				if (Vector2.Distance(base.Projectile.Center, projectile.Center) <= 100f && projectile.active && projectile.type == ModContent.ProjectileType<PristineSecondary>() && projectile.Opacity > 0.7f)
				{
					if (projectile.ai[2] == 0f)
					{
						projectile.ai[2] = boomTime;
						hasIgnited = true;
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceBurn");
						style.Volume = 1f;
						style.Pitch = Main.rand.NextFloat(0.5f, 0.6f);
						style.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					else
					{
						hasIgnited = true;
					}
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		time--;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 240);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(50);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		for (int i = 0; i < 4; i++)
		{
			Vector2 val = ((i < 2) ? beamPos : beamPos2);
			Vector2 lineVel = (val.DirectionFrom(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 40f) * 3f).RotatedByRandom(0.1599999964237213) * Main.rand.NextFloat(0.4f, 2.5f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(val, lineVel, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 11, 0.09f, Main.rand.NextBool() ? Color.Orange : Color.DarkOrange, new Vector2(2f, 1.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1.1f));
		}
		for (int j = 0; j <= 3; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 169 : 158, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.2f, 1.5f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.2f));
			dust.noGravity = true;
			dust.fadeIn = 0.5f;
		}
		for (int k = 0; k < 7; k++)
		{
			float velMulti = Main.rand.NextFloat(0.1f, 1.8f);
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(32f, 32f);
			Vector2 smokeVel = Vector2.UnitY * Main.rand.NextFloat(-12f, -8f) * velMulti;
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position, smokeVel, Main.rand.NextBool() ? Color.Orange : Color.DarkOrange, Color.Black, Main.rand.NextFloat(0.7f, 1.9f) - velMulti, 225 - Main.rand.Next(60), 0.1f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
