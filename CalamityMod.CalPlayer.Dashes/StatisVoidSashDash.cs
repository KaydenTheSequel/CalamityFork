using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public class StatisVoidSashDash : PlayerDashEffect
{
	public int Time;

	public Vector2 aimVel;

	public bool strongVisuals = true;

	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.NoCollision;

	public override bool IsOmnidirectional => false;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 64f;
	}

	public override void OnDashEffects(Player player)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		strongVisuals = player.Calamity().voidSashVisuals;
		Time = 0;
		aimVel = player.velocity;
		if (strongVisuals)
		{
			SoundStyle style = StatisVoidSash.VoidDash with
			{
				Volume = 0.7f,
				Pitch = Main.rand.NextFloat(-0.15f, 0.15f)
			};
			SoundEngine.PlaySound(in style, player.Center);
			for (int i = 0; i < 40; i++)
			{
				Vector2 intenededVel = ((float)Math.PI * 2f * (float)i / 40f).ToRotationVector2() * 3f;
				Vector2 fxVel = Utils.RotatedBy(new Vector2(intenededVel.X, intenededVel.Y * 2.3f), (double)player.velocity.ToRotation(), default(Vector2));
				SignusMetaball.SpawnParticle(player.Center + fxVel.RotatedBy(player.velocity.ToRotation()) + player.velocity.SafeNormalize(Vector2.UnitX) * 60f, fxVel - player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(6f, 9f), 60f * Main.rand.NextFloat(0.7f, 1f), 60, new Vector2(1.8f, 0.9f), 0.06f);
			}
		}
		for (int j = 0; j < 30; j++)
		{
			Vector2 intenededVel2 = ((float)Math.PI * 2f * (float)j / 30f).ToRotationVector2() * 3f;
			Vector2 fxVel2 = Utils.RotatedBy(new Vector2(intenededVel2.X, intenededVel2.Y * 2f), (double)player.velocity.ToRotation(), default(Vector2));
			Vector2 position = player.Center + fxVel2.RotatedBy(player.velocity.ToRotation()) + player.velocity.SafeNormalize(Vector2.UnitX) * 60f;
			Color dustColor = (Main.rand.NextBool(3) ? Color.Indigo : Color.DarkOrchid);
			int dustStyle = ModContent.DustType<SquashDust>();
			Dust dust = Dust.NewDustPerfect(position, dustStyle, (fxVel2 - player.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(6f, 9f)) * Main.rand.NextFloat(1.2f, 1.5f));
			dust.scale = Main.rand.NextFloat(1.6f, 2.3f) * 2.3f;
			dust.color = dustColor;
			dust.noGravity = true;
			dust.fadeIn = 1.5f;
		}
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		aimVel = Vector2.Lerp(aimVel, player.velocity, 0.2f);
		float fxFade = Utils.GetLerpValue(5f, 15f, Math.Abs(aimVel.X), clamped: true);
		Vector2 position = player.Center - aimVel * 2f + Main.rand.NextVector2Circular(10f, 20f);
		Color trailColor = (Main.rand.NextBool(3) ? Color.Indigo : Color.DarkOrchid);
		int dustStyle = ModContent.DustType<SquashDust>();
		Dust dust = Dust.NewDustPerfect(position, dustStyle, aimVel.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(9f, 15f));
		dust.scale = Main.rand.NextFloat(0.9f, 1.7f);
		dust.color = trailColor;
		dust.noGravity = true;
		dust.fadeIn = 0.5f;
		if (strongVisuals)
		{
			SignusMetaball.SpawnParticle(player.Center + Main.rand.NextVector2Circular(20f, 20f), -aimVel.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(5f, 15f), 120f * Main.rand.NextFloat(0.7f, 1f), 60, new Vector2(0.8f, 1.2f), 0.08f);
			int dir = MathF.Sign(aimVel.X);
			Vector2 safeVel = aimVel.SafeNormalize(Vector2.UnitX);
			Math.Max(fxFade, 0.5f);
			for (int i = -1; i <= 1; i += 2)
			{
				Vector2 fxVelocity = safeVel.RotatedBy((float)dir * MathHelper.ToRadians(75f) * (float)(-i)) - safeVel * 3f;
				SignusMetaball.SpawnParticle(player.Center + (safeVel * 35f).RotatedBy(-0.2f * (float)dir * (float)i) * 1.5f, fxVelocity * Main.rand.NextFloat(5f, 8f), 50f * Main.rand.NextFloat(0.7f, 1f), 8, new Vector2(0.8f, 1.2f), 0.18f);
			}
		}
		player.velocity.X *= 0.95f;
		if (player.velocity.X > 140f)
		{
			player.velocity.X = 140f;
		}
		if (player.velocity.X < -140f)
		{
			player.velocity.X = -140f;
		}
		if (Time > 10)
		{
			player.velocity.X *= 0.5f;
		}
	}
}
