using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.CalPlayer.Dashes;

public class CounterScarfDash : PlayerDashEffect
{
	public new static string ID { get; private set; }

	public override DashCollisionType CollisionType => DashCollisionType.NoCollision;

	public override bool IsOmnidirectional => false;

	public override void Load()
	{
		ID = DashID;
	}

	public override float CalculateDashSpeed(Player player)
	{
		if (!player.Calamity().evasionScarf)
		{
			return 15f;
		}
		return 19f;
	}

	public override void OnDashEffects(Player player)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 20; d++)
		{
			Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, 235, 0f, 0f, 100, default(Color), 2f);
			dust.position += Main.rand.NextVector2Square(-5f, 5f);
			dust.velocity *= 0.2f;
			dust.scale *= Main.rand.NextFloat(1f, 1.2f);
			dust.shader = GameShaders.Armor.GetSecondaryShader(player.cNeck, player);
		}
	}

	public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 2; k++)
		{
			int dustSpawnHeight = 8;
			float dustSpawnTop = player.Bottom.Y - 4f;
			if (player.velocity.Y != 0f)
			{
				dustSpawnHeight = 16;
				dustSpawnTop = player.Center.Y - 8f;
			}
			Dust dust = Dust.NewDustDirect(new Vector2(player.position.X, dustSpawnTop), player.width, dustSpawnHeight, 235, 0f, 0f, 100, default(Color), 1.4f);
			dust.velocity *= 0.1f;
			dust.scale *= Main.rand.NextFloat(1f, 1.2f);
			dust.shader = GameShaders.Armor.GetSecondaryShader(player.cNeck, player);
		}
	}
}
