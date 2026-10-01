using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Enums;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class AsgardianAegisDash : PlayerDashEffect
{
	public int Time;

	public bool PostHit;

	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.ShieldSlam;

	public override bool IsOmnidirectional => false;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 23.3f;
	}

	public override void OnDashEffects(Player player)
	{
		Time = 0;
		PostHit = false;
	}

	public override void DashStartupEffects(Player player)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		player.velocity *= 0.9f;
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		Time += 2;
		MathHelper.Lerp(0f, 1f, Utils.GetLerpValue(2f, 2.5f, Time, clamped: true));
		if (base.DashTimeAdjustedForStartup <= 18)
		{
			for (int d = 0; d < 2; d++)
			{
				Dust hFlameDust = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.2f, Main.rand.NextBool(8) ? 180 : 295, -player.velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(1.8f, 2.8f));
				hFlameDust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				hFlameDust.noGravity = hFlameDust.type != 180;
				hFlameDust.fadeIn = 0.5f;
				if (hFlameDust.type == 180)
				{
					hFlameDust.scale = Main.rand.NextFloat(0.8f, 1.2f);
					hFlameDust.velocity += new Vector2(0f, -2.5f) * Main.rand.NextFloat(0.8f, 1.2f);
				}
				Dust dust = Dust.NewDustPerfect(player.Center + Main.rand.NextVector2Circular(6f, 6f) - player.velocity * 2f, 92);
				dust.velocity = -player.velocity * Main.rand.NextFloat(0.6f, 1.4f);
				dust.scale = Main.rand.NextFloat(0.9f, 1.4f);
				dust.noGravity = true;
				if (d < 1)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.2f, -player.velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.1f, 0.8f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(1.15f, 1.25f), Main.rand.NextBool() ? Color.Fuchsia : Color.Cyan, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.4f, 0.5f)));
				}
			}
		}
		dashSpeed = 16f;
	}

	public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (base.DashTimeAdjustedForStartup <= 18)
		{
			if (!PostHit)
			{
				player.SetScreenshake(5f);
				PostHit = true;
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(npc.Center, Vector2.Zero, Color.Aqua, new Vector2(2f, 2f), 0f, 0.1f, 0.85f, 36));
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(npc.Center, Vector2.Zero, Color.Magenta, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.65f, 26));
			int hitDirection = player.direction;
			if (player.velocity.X != 0f)
			{
				hitDirection = Math.Sign(player.velocity.X);
			}
			hitContext.HitDirection = hitDirection;
			hitContext.PlayerImmunityFrames = 12;
			hitContext.damageClass = DamageClass.Melee;
			hitContext.BaseDamage = 1000;
			hitContext.BaseKnockback = 15f;
			int explosionDamage = (int)player.GetBestClassDamage().ApplyTo(300f);
			Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<CosmicDashExplosion>(), explosionDamage, 20f, Main.myPlayer, 3f);
			npc.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300);
		}
	}
}
