using CalamityMod.Enums;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;

namespace CalamityMod.CalPlayer.Dashes;

public class SpeedBlasterDash : PlayerDashEffect
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
		return 30f;
	}

	public override void OnDashEffects(Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SpeedBlaster.Dash, player.Center);
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
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDustPerfect(player.Center + Main.rand.NextVector2Unit() * 12f, 66, -player.velocity * 0.2f, 150, Color.Aqua, 1.2f).noGravity = true;
		Vector2 sparkVel = player.velocity.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(-3f, -6f);
		Color sparkColor = SpeedBlasterShot.GetColor(Main.rand.Next(5));
		float scale = Main.rand.NextFloat(1f, 1.6f);
		GeneralParticleHandler.SpawnParticle(new CritSpark(player.Center + Main.rand.NextVector2Unit() * 12f, sparkVel, Color.White, sparkColor, scale, 15, 0.5f, scale * 2f));
		player.maxFallSpeed = 50f;
		dashSpeed = 20f;
		runSpeedDecelerationFactor = 0.8f;
		player.Calamity().SpeedBlasterDashStarted = false;
		player.Calamity().sBlasterDashActivated = false;
	}
}
