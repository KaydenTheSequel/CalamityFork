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

public class ElysianAegisDash : PlayerDashEffect
{
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
		return 21.5f;
	}

	public override void OnDashEffects(Player player)
	{
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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		if (base.DashTimeAdjustedForStartup <= 16)
		{
			for (int d = 0; d < 4; d++)
			{
				Dust hFlameDust = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.2f, Main.rand.NextBool(8) ? 222 : 162, -player.velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(1.8f, 2.8f));
				hFlameDust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				hFlameDust.noGravity = hFlameDust.type != 222;
				hFlameDust.fadeIn = 0.5f;
				if (hFlameDust.type == 222)
				{
					hFlameDust.scale = Main.rand.NextFloat(0.8f, 1.2f);
					hFlameDust.velocity += new Vector2(0f, -2.5f) * Main.rand.NextFloat(0.8f, 1.2f);
				}
				if (hFlameDust.type == 180)
				{
					hFlameDust.scale = Main.rand.NextFloat(1.6f, 2.2f);
				}
				Dust dust = Dust.NewDustPerfect(player.Center + Main.rand.NextVector2Circular(6f, 6f) - player.velocity * 2f, 228);
				dust.velocity = -player.velocity * Main.rand.NextFloat(0.6f, 1.4f);
				dust.scale = Main.rand.NextFloat(0.9f, 1.4f);
				dust.noGravity = true;
				if (d < 1)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.2f, -player.velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.1f, 0.8f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(1.15f, 1.25f), Main.rand.NextBool(4) ? Color.Khaki : Color.Orange, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.4f, 0.5f)));
				}
			}
		}
		dashSpeed = 14f;
	}

	public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		if (base.DashTimeAdjustedForStartup <= 16)
		{
			if (!PostHit)
			{
				player.SetScreenshake(3.5f);
				PostHit = true;
			}
			float particleScale = Main.rand.NextFloat(0.45f, 0.55f);
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(npc.Center, Vector2.Zero, Color.Gray * 0.6f, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, particleScale + 0.07f, 20, UseAdditiveBlend: false));
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(npc.Center, Vector2.Zero, Color.Orange, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, particleScale, 20));
			int hitDirection = player.direction;
			if (player.velocity.X != 0f)
			{
				hitDirection = Math.Sign(player.velocity.X);
			}
			hitContext.HitDirection = hitDirection;
			hitContext.PlayerImmunityFrames = 12;
			hitContext.damageClass = DamageClass.Melee;
			hitContext.BaseDamage = 500;
			hitContext.BaseKnockback = 12f;
			int supremeExplosionDamage = (int)player.GetBestClassDamage().ApplyTo(100f);
			Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<HolyExplosionSupreme>(), supremeExplosionDamage, 15f, Main.myPlayer, 1f);
			npc.AddBuff(ModContent.BuffType<HolyFlames>(), 300);
		}
	}
}
