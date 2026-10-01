using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PolarStar : ModProjectile, ILocalizedModType, IModType
{
	private static float HitboxSize = 30f;

	public int dustTypeWhite = 91;

	public int tileBounces;

	public float DustScaleMultiplier = 1f;

	public bool DoSlowdown = true;

	public Vector2 StartVelocity;

	public Color EffectsColor;

	public int DustEffectsID;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/PolarStar";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 60;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = true;
		base.Projectile.ArmorPenetration = 15;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (Time == 1f)
		{
			StartVelocity = base.Projectile.velocity;
			float num = base.Projectile.ai[1];
			if (num != 2f)
			{
				if (num == 1f)
				{
					DustEffectsID = ModContent.DustType<AstralBlue>();
					EffectsColor = Color.Turquoise;
					DustScaleMultiplier = 1f;
				}
				else
				{
					DustEffectsID = 223;
					EffectsColor = Color.Violet;
					DustScaleMultiplier = 0.4f;
				}
			}
			else
			{
				DustEffectsID = ModContent.DustType<AstralOrange>();
				EffectsColor = Color.Coral;
				DustScaleMultiplier = 1f;
			}
			for (int i = 0; i <= 10; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(5) ? dustTypeWhite : DustEffectsID, base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.3f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.85f, 1.4f) * DustScaleMultiplier;
			}
			for (int j = 0; j <= 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.4f), Main.rand.NextFloat(0.2f, 0.6f), EffectsColor, Main.rand.Next(40, 51), 0.25f, 2f));
			}
		}
		if (Time % 3f == 0f)
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f), Main.rand.NextBool(5) ? dustTypeWhite : DustEffectsID);
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.5f, 1.2f) * DustScaleMultiplier;
			dust2.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.01f, 0.045f);
		}
		if (Time > 4f && DoSlowdown)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity * 3f, -base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 6, 0.04f, EffectsColor * 0.8f, new Vector2(0.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.4f));
		}
		if (base.Projectile.timeLeft <= 2 && DoSlowdown)
		{
			base.Projectile.timeLeft = 7;
			base.Projectile.velocity = Vector2.Zero;
			DoSlowdown = false;
		}
		if (!DoSlowdown)
		{
			base.Projectile.extraUpdates = 0;
			base.Projectile.alpha = 255;
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref EffectsColor)).ToVector3() * 1f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (DoSlowdown)
		{
			base.Projectile.timeLeft = 7;
			base.Projectile.velocity = Vector2.Zero;
			DoSlowdown = false;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		int points = 5;
		float radians = (float)Math.PI * 2f / (float)points;
		Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
		Vector2 addedPlacement = StartVelocity * Main.rand.NextFloat(0.1f, 1.5f);
		for (int k = 0; k < points; k++)
		{
			Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity * 7.5f + addedPlacement, velocity * 2.5f, "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 13, 0.02f, EffectsColor * 0.8f, new Vector2(2.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
		}
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits <= 1)
		{
			return null;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, HitboxSize, targetHitbox);
	}
}
