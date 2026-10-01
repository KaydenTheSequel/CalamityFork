using System;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class AsgardsValorDash : PlayerDashEffect
{
	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.ShieldSlam;

	public override bool IsOmnidirectional => false;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 16.9f;
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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		dashSpeed = 12.5f;
		if (base.DashTimeAdjustedForStartup > 14)
		{
			return;
		}
		for (int d = 0; d < 3; d++)
		{
			Dust holyFireDashDust = Dust.NewDustDirect(player.position + Vector2.UnitY * 4f, player.width, player.height - 8, Main.rand.NextBool() ? 296 : 158, 0f, 0f, 0, default(Color), 1.2f);
			holyFireDashDust.velocity = -player.velocity * Main.rand.NextFloat(0.1f, 0.75f);
			holyFireDashDust.scale *= Main.rand.NextFloat(2f, 2.4f);
			holyFireDashDust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
			holyFireDashDust.noGravity = true;
			if (Main.rand.NextBool())
			{
				holyFireDashDust.fadeIn = 0.1f;
			}
		}
		Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.7f, 222, -player.velocity * Main.rand.NextFloat(0.15f, 0.4f), 0, default(Color), 0.5f);
		dust.noGravity = false;
		dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
	}

	public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		if (base.DashTimeAdjustedForStartup <= 14)
		{
			int hitDirection = player.direction;
			if (player.velocity.X != 0f)
			{
				hitDirection = Math.Sign(player.velocity.X);
			}
			hitContext.HitDirection = hitDirection;
			hitContext.PlayerImmunityFrames = 12;
			hitContext.damageClass = DamageClass.Melee;
			hitContext.BaseDamage = 200;
			hitContext.BaseKnockback = 9f;
			int Dusts = 12;
			float radians = (float)Math.PI * 2f / (float)Dusts;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
			for (int k = 0; k < Dusts; k++)
			{
				Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k);
				Dust dust = Dust.NewDustPerfect(npc.Center, 296, velocity * 3f, 0, default(Color), 2.5f);
				dust.noGravity = true;
				dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				Dust dust2 = Dust.NewDustPerfect(npc.Center, 158, velocity * 5f, 0, default(Color), 2.2f);
				dust2.noGravity = true;
				dust2.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				dust2.color = Color.Salmon;
				Dust dust3 = Dust.NewDustPerfect(npc.Center, 169, velocity * 7f, 0, default(Color), 1.9f);
				dust3.noGravity = true;
				dust3.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				dust3.color = Color.SandyBrown;
			}
			for (int i = 0; i < 5; i++)
			{
				Dust dust4 = Dust.NewDustPerfect(npc.Center, 222, Utils.RotatedByRandom(new Vector2(0f, -3.5f), 0.699999988079071) * Main.rand.NextFloat(0.8f, 1.4f), 0, default(Color), 1.2f);
				dust4.noGravity = false;
				dust4.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
			}
			SoundEngine.PlaySound(SoundID.Item62 with
			{
				Volume = 0.6f,
				PitchVariance = 0.3f
			}, npc.Center);
		}
	}
}
