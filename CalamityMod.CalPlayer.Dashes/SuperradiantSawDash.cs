using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.CalPlayer.Dashes;

public class SuperradiantSawDash : PlayerDashEffect
{
	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.NoCollision;

	public override bool IsOmnidirectional => true;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		return 36f;
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDustPerfect(player.Center + Main.rand.NextVector2Unit() * 12f, 66, -player.velocity * 0.2f, 150, Color.Lime, 1.2f).noGravity = true;
		Vector2 sparkVel = player.velocity.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(-3f, -6f);
		Color sparkColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.25f);
		float scale = Main.rand.NextFloat(1.2f, 2f);
		GeneralParticleHandler.SpawnParticle(new CritSpark(player.Center + Main.rand.NextVector2Unit() * 12f, sparkVel, Color.White, sparkColor, scale, 24, 0.5f, scale * 2f));
		player.maxFallSpeed = 50f;
		dashSpeed = 24f;
		runSpeedDecelerationFactor = 0.8f;
		player.Calamity().SpeedBlasterDashStarted = false;
		player.Calamity().sBlasterDashActivated = false;
	}
}
