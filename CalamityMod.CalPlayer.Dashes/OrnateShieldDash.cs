using System;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class OrnateShieldDash : PlayerDashEffect
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
		if (base.DashTimeAdjustedForStartup <= 12)
		{
			for (int d = 0; d < 3; d++)
			{
				Dust iceDashDust = Dust.NewDustPerfect(player.Center + new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-15f, 15f)) - player.velocity * 1.7f, Main.rand.NextBool(8) ? 223 : 180, -player.velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.6f, 0.8f));
				iceDashDust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				iceDashDust.noGravity = true;
				iceDashDust.fadeIn = 0.5f;
				if (iceDashDust.type == 180)
				{
					iceDashDust.scale = Main.rand.NextFloat(1.6f, 2.2f);
				}
			}
		}
		dashSpeed = 12.5f;
	}

	public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
		if (base.DashTimeAdjustedForStartup <= 12)
		{
			int hitDirection = player.direction;
			if (player.velocity.X != 0f)
			{
				hitDirection = Math.Sign(player.velocity.X);
			}
			hitContext.HitDirection = hitDirection;
			hitContext.PlayerImmunityFrames = 12;
			hitContext.damageClass = DamageClass.Melee;
			hitContext.BaseDamage = 50;
			hitContext.BaseKnockback = 3f;
			npc.AddBuff(324, 180);
		}
	}
}
