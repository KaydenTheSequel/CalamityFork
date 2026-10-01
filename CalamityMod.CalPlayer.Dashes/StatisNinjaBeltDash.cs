using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class StatisNinjaBeltDash : PlayerDashEffect
{
	public int Time;

	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.NoCollision;

	public override bool IsOmnidirectional => false;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 48f;
	}

	public override void OnDashEffects(Player player)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		Time = 0;
		for (int i = 0; i < 15; i++)
		{
			Vector2 intenededVel = ((float)Math.PI * 2f * (float)i / 15f).ToRotationVector2() * 1.5f;
			Vector2 fxVel = Utils.RotatedBy(new Vector2(intenededVel.X, intenededVel.Y * 2.3f), (double)player.velocity.ToRotation(), default(Vector2));
			Vector2 position = player.Center + fxVel.RotatedBy(player.velocity.ToRotation()) + player.velocity.SafeNormalize(Vector2.UnitX) * 60f;
			Color dustColor = (Main.rand.NextBool() ? Color.Purple : Color.DarkSlateBlue);
			int dustStyle = ModContent.DustType<SquashDust>();
			Dust dust = Dust.NewDustPerfect(position, dustStyle, (fxVel - player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(4f, 5f)) * Main.rand.NextFloat(1.2f, 1.5f));
			dust.scale = Main.rand.NextFloat(1.6f, 2.3f) * 1.8f;
			dust.color = dustColor;
			dust.noGravity = true;
			dust.fadeIn = 1.5f;
		}
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		for (int i = 0; i < 2; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(player.Center + Main.rand.NextVector2Circular(15f, 15f), -player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(5f, 9f), Main.rand.NextBool() ? Color.Purple : Color.DarkSlateBlue, 16, Main.rand.NextFloat(0.7f, 0.9f), 0.55f, 0.2f * Main.rand.NextFloatDirection(), glowing: true));
		}
		int dustStyle = ModContent.DustType<SquashDust>();
		Dust dust = Dust.NewDustPerfect(player.Center + Main.rand.NextVector2Circular(15f, 15f), dustStyle, player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(5f, 9f));
		dust.scale = Main.rand.NextFloat(0.9f, 1.7f);
		dust.color = (Main.rand.NextBool() ? Color.Purple : Color.DarkSlateBlue);
		dust.noGravity = true;
		dust.fadeIn = 0.5f;
		player.velocity.X *= 0.93f;
		if (player.velocity.X > 140f)
		{
			player.velocity.X = 140f;
		}
		if (player.velocity.X < -140f)
		{
			player.velocity.X = -140f;
		}
		if (Time > 8)
		{
			player.velocity.X *= 0.5f;
		}
	}
}
