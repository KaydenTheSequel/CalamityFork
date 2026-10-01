using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BloodfireBulletProj : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 1200;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 12;
		base.Projectile.timeLeft = 1200;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 0.75f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (base.Projectile.localAI[0] == 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.7f;
		}
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Vector2 center = base.Projectile.Center;
		Color newColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f);
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] > 6f))
		{
			return;
		}
		if (Main.rand.NextBool(3))
		{
			Vector2 center2 = base.Projectile.Center;
			int type = ((!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool(3) ? 130 : 60));
			Vector2? velocity = -base.Projectile.velocity.RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(0.01f, 0.3f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.9f);
			if (dust.type == 130)
			{
				dust.scale = Main.rand.NextFloat(0.35f, 0.55f);
			}
		}
		if (targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity, -base.Projectile.velocity * 0.01f, affectedByGravity: false, 4, 0.4f, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Firebrick));
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 100);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] > 6f)
		{
			CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red);
		}
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= OnHitEffect(Main.player[base.Projectile.owner], target);
	}

	private float OnHitEffect(Player owner, NPC target)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		owner.lifeRegenTime += 3f;
		float lifeRegenTimeContribution = Utils.GetLerpValue(0f, 3600f, owner.lifeRegenTime, clamped: true) * 0.1f;
		float finalDamageBoost = 1f + lifeRegenTimeContribution;
		if (lifeRegenTimeContribution == 0.1f)
		{
			for (int k = 0; k < 3; k++)
			{
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(6.5f, 6.5f), 100.0) * Main.rand.NextFloat(0.8f, 1.2f), Main.rand.Next(8, 11), Main.rand.NextFloat(0.7f, 0.9f), (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red));
				int dustType = ModContent.DustType<DiamondDust>();
				float velMulti = Main.rand.NextFloat(0.1f, 0.75f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType, (-base.Projectile.velocity * 4f).RotatedByRandom(0.4) * velMulti);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.55f, 0.65f);
				dust.color = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Firebrick);
				dust.noLightEmittence = true;
				dust.noLight = true;
				dust.fadeIn = 15f;
			}
		}
		return finalDamageBoost;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(1f, 3f), affectedByGravity: false, Main.rand.Next(5, 8), Main.rand.NextFloat(0.4f, 0.6f), (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red));
		}
		for (int i = 0; i < 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool(3) ? 130 : 60), Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.9f);
			if (dust.type == 130)
			{
				dust.scale = Main.rand.NextFloat(0.35f, 0.55f);
			}
		}
	}
}
