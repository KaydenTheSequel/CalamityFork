using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ElysianArrowProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/ElysianArrow";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.extraUpdates = 2;
		base.Projectile.aiStyle = 1;
	}

	public override void AI()
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		_ = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[0] > 5f)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 262 : 87, -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.7f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.4f, 1.1f);
			dust.alpha = 235;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		NPC target = base.Projectile.Center.ClosestNPCAt(2000f);
		Vector2 targetPosition = target?.Center ?? base.Projectile.Center;
		Vector2 spawnSpot = (target?.Center ?? base.Projectile.Center) + new Vector2(Main.rand.NextFloat(-450f, 450f), Main.rand.NextFloat(-750f, -950f));
		Vector2 velocity = ((target != null) ? CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(spawnSpot, target, 20f, ElysianArrowRain.MaxUpdate) : ((targetPosition - spawnSpot).SafeNormalize(Vector2.UnitX) * 20f));
		if (num < 1400f)
		{
			int Dusts = 8;
			float radians = (float)Math.PI * 2f / (float)Dusts;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
			for (int i = 0; i < Dusts; i++)
			{
				Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)i) * 3.5f;
				Dust.NewDustPerfect(spawnSpot, Main.rand.NextBool() ? 262 : 87, dustVelocity, 0, default(Color), 0.9f).noGravity = true;
				Dust.NewDustPerfect(spawnSpot, Main.rand.NextBool() ? 262 : 87, dustVelocity * 0.6f, 0, default(Color), 1.2f).noGravity = true;
			}
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, velocity, ModContent.ProjectileType<ElysianArrowRain>(), (int)((float)base.Projectile.damage * 0.15f), 0f, base.Projectile.owner);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");
		style.Volume = 0.4f;
		style.Pitch = 0.4f;
		SoundEngine.PlaySound(in style, base.Projectile.position);
		if (Main.dedServ)
		{
			return;
		}
		for (int k = 0; k < 7; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 262 : 87, base.Projectile.velocity.RotatedByRandom(0.4) * Main.rand.NextFloat(0.5f, 1.5f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 1.25f);
			dust.alpha = 235;
			if (Main.rand.NextBool())
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 303, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.8f, 1.5f);
				dust2.alpha = 70;
			}
		}
		for (int j = 0; j < 2; j++)
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 278, base.Projectile.velocity.RotatedByRandom(0.4) * Main.rand.NextFloat(0.5f, 1.5f));
			dust3.noGravity = false;
			dust3.scale = Main.rand.NextFloat(0.85f, 1f);
			dust3.color = (Main.rand.NextBool() ? Color.Orange : Color.Khaki);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}
}
