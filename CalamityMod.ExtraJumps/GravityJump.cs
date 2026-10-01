using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.ExtraJumps;

public class GravityJump : ExtraJump
{
	public override Position GetDefaultPosition()
	{
		return ExtraJump.BeforeBottleJumps;
	}

	public override float GetDurationMultiplier(Player player)
	{
		return 3f;
	}

	public override void UpdateHorizontalSpeeds(Player player)
	{
		player.runAcceleration *= 2f;
		player.maxRunSpeed *= 4f;
	}

	public override void OnStarted(Player player, ref bool playSound)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		playSound = true;
		if (player.wingsLogic <= 0)
		{
			player.velocity.Y *= (player.slowFall ? 2f : 2.75f);
		}
		else
		{
			player.velocity.Y *= 1.7f;
		}
		player.StopExtraJumpInProgress();
		Color color = default(Color);
		Vector2 stretch = default(Vector2);
		for (int i = 0; i < 3; i++)
		{
			Vector2 center = player.Center;
			Vector2 velocity = Vector2.UnitY * (0f - (2.5f + 2f * (float)i)) * player.gravDir;
			int lifetime = 120;
			float scale = MathHelper.Lerp(0.05f, 0.1f, (float)i / 3f);
			((Color)(ref color))._002Ector(94, 229, 163);
			((Vector2)(ref stretch))._002Ector(0.5f, 1.5f);
			float shrink = -0.3f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(center, velocity, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, lifetime, scale, color, stretch, useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, shrink));
		}
	}

	public override void ShowVisuals(Player player)
	{
		player.StopExtraJumpInProgress();
	}
}
