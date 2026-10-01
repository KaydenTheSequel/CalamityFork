using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.ExtraJumps;

public class StatigelJump : ExtraJump
{
	public override Position GetDefaultPosition()
	{
		return new Before(ExtraJump.BlizzardInABottle);
	}

	public override float GetDurationMultiplier(Player player)
	{
		return 1.25f;
	}

	public override void UpdateHorizontalSpeeds(Player player)
	{
		player.runAcceleration *= 3f;
		player.maxRunSpeed *= 1.75f;
	}

	public override void OnStarted(Player player, ref bool playSound)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		playSound = true;
		int offset = player.height;
		if (player.gravDir == -1f)
		{
			offset = 0;
		}
		for (int i = 0; i < 35; i++)
		{
			Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y + (float)offset), Main.rand.NextBool() ? 243 : 56, Utils.RotatedByRandom(new Vector2(0f - player.velocity.X, 15f), MathHelper.ToRadians(50f)) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(1.2f, 1.9f)).noGravity = true;
		}
		for (int j = 0; j < 20; j++)
		{
			Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y + (float)offset), Main.rand.NextBool() ? 242 : 135, Utils.RotatedByRandom(new Vector2(0f - player.velocity.X, 15f), MathHelper.ToRadians(50f)) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(1.2f, 1.9f)).noGravity = true;
		}
	}

	public override void ShowVisuals(Player player)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Vector2 position = player.Calamity().RandomDebuffVisualSpot + new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
			Vector2 pulseVelocity = new Vector2(Main.rand.NextFloat(-1f, 1f) - player.velocity.X * 0.5f, Main.rand.NextFloat(4f, 7f)) * Main.rand.NextFloat(0.2f, 1f);
			GeneralParticleHandler.SpawnParticle(new GenericBloom(position, pulseVelocity, Main.rand.NextBool() ? Color.DarkTurquoise : Color.Orchid, 0.055f, 8));
			Dust dust = Dust.NewDustPerfect(position, Main.rand.NextBool() ? 243 : 56, pulseVelocity, 100, default(Color), Main.rand.NextFloat(0.6f, 0.9f));
			dust.noGravity = false;
			dust.alpha = 190;
			Dust.NewDustPerfect(position, Main.rand.NextBool() ? 242 : 135, pulseVelocity * 0.9f, 100, default(Color), Main.rand.NextFloat(1.2f, 1.9f)).noGravity = true;
		}
	}
}
