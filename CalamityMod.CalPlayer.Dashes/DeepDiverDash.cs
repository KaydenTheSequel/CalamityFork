using System;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class DeepDiverDash : PlayerDashEffect
{
	public int Time;

	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.ShieldSlam;

	public override bool IsOmnidirectional => false;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 20f;
	}

	public override void OnDashEffects(Player player)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Time = 0;
		for (int m = 0; m < 3; m++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(player.Center - player.velocity, -player.velocity * (0.08f * (float)m), affectedByGravity: false, 25, 4f - 0.5f * (float)m, (Main.rand.NextBool() ? Color.Aqua : Color.DodgerBlue) * 0.4f));
		}
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		for (int m = 0; m < 5; m++)
		{
			Vector2 dustVel3 = -player.velocity.RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * Main.rand.NextFloat(0.03f, 0.2f);
			Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.7f, Main.rand.NextBool(7) ? 278 : 80, dustVel3);
			if (dust.type == 278)
			{
				dust.scale = 1.2f;
				dust.velocity = Vector2.Zero;
				dust.noGravity = false;
				dust.color = (Main.rand.NextBool() ? Color.Aqua : Color.DodgerBlue);
			}
			else
			{
				dust.scale = Main.rand.NextFloat(0.6f, 1.8f);
				dust.alpha = 125;
				dust.noGravity = true;
			}
			dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
		}
		if (Time % 2 == 0)
		{
			Vector2 dustVel4 = -player.velocity.RotatedBy(0.05f + MathHelper.Clamp((float)Time * 0.03f, 0f, 0.55f)) * 0.75f;
			Vector2 dustVel5 = -player.velocity.RotatedBy(-0.05f - MathHelper.Clamp((float)Time * 0.03f, 0f, 0.55f)) * 0.75f;
			GeneralParticleHandler.SpawnParticle(new PointParticle(player.Center + new Vector2(0f, (float)(-15 * player.direction)) + dustVel4, dustVel4, affectedByGravity: false, 8, 1.4f, (Main.rand.NextBool() ? Color.Aqua : Color.DodgerBlue) * 0.5f));
			GeneralParticleHandler.SpawnParticle(new PointParticle(player.Center + new Vector2(0f, (float)(15 * player.direction)) + dustVel5, dustVel5, affectedByGravity: false, 8, 1.4f, (Main.rand.NextBool() ? Color.Aqua : Color.DodgerBlue) * 0.5f));
		}
		player.velocity.X *= 0.967f;
		dashSpeed = 25f;
	}

	public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfSmallHit3");
		style.Pitch = 0.7f;
		style.Volume = 0.4f;
		SoundEngine.PlaySound(in style, player.Center);
		for (int i = 0; i <= 6; i++)
		{
			Dust dust = Dust.NewDustPerfect(player.Center, Main.rand.NextBool() ? 278 : 132, player.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.5f, 1f) + new Vector2(0f, -3f));
			if (dust.type == 278)
			{
				dust.scale = 1.2f;
				dust.color = (Main.rand.NextBool() ? Color.Aqua : Color.DodgerBlue);
			}
			else
			{
				dust.scale = 0.9f;
			}
			dust.noGravity = false;
			dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
		}
		int hitDirection = player.direction;
		if (player.velocity.X != 0f)
		{
			hitDirection = Math.Sign(player.velocity.X);
		}
		hitContext.HitDirection = hitDirection;
		hitContext.PlayerImmunityFrames = 16;
		hitContext.damageClass = DamageClass.Melee;
		hitContext.BaseDamage = 35;
		hitContext.BaseKnockback = 0.2f;
	}
}
