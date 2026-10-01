using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.MarniteArchitect;

public class MarniteArchitectPlayer : ModPlayer
{
	public bool setEquipped;

	public bool mounted;

	public SlotId liftDroningSoundSlot;

	public override void ResetEffects()
	{
		setEquipped = false;
		mounted = false;
	}

	public override void UpdateDead()
	{
		setEquipped = false;
		mounted = false;
	}

	public override void PostUpdateMiscEffects()
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (!setEquipped && base.Player.mount.Type == ModContent.MountType<MarniteLift>() && base.Player.mount.Active)
		{
			base.Player.mount.Dismount(base.Player);
		}
		ActiveSound soundPlaying2;
		if (mounted)
		{
			if (!SoundEngine.TryGetActiveSound(liftDroningSoundSlot, out ActiveSound soundPlaying))
			{
				liftDroningSoundSlot = SoundEngine.PlaySound(in MarniteArchitectHeadgear.LiftHummSound, base.Player.Center);
				return;
			}
			soundPlaying.Position = base.Player.Center;
			float distanceToGround = RaycastGround(centerOnly: true).Y;
			soundPlaying.Volume = 1f - distanceToGround / MarniteArchitectHeadgear.MaxLiftHeight * 0.3f;
		}
		else if (SoundEngine.TryGetActiveSound(liftDroningSoundSlot, out soundPlaying2))
		{
			soundPlaying2.Stop();
			liftDroningSoundSlot = SlotId.Invalid;
		}
	}

	public Vector2 RaycastGround(bool centerOnly = false)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		float closestDistance = MarniteArchitectHeadgear.MaxLiftHeight + 1f;
		if (centerOnly)
		{
			Vector2 basePosition = base.Player.Bottom;
			for (float j = 0f; j < closestDistance; j += 8f)
			{
				Point tileToCheck = (basePosition + Vector2.UnitY * j).ToSafeTileCoordinates();
				if (Main.tile[tileToCheck].IsTileSolidGround())
				{
					closestDistance = j;
					break;
				}
			}
			return Vector2.UnitY * closestDistance;
		}
		for (float i = 0f; i < (float)base.Player.width; i += (float)base.Player.width / 6f)
		{
			Vector2 basePosition2 = base.Player.BottomLeft + Vector2.UnitX * i;
			for (float j2 = 0f; j2 < closestDistance; j2 += 8f)
			{
				Point tileToCheck2 = (basePosition2 + Vector2.UnitY * j2).ToSafeTileCoordinates();
				if (Main.tile[tileToCheck2].IsTileSolidGround())
				{
					closestDistance = j2;
					break;
				}
			}
		}
		return Vector2.UnitY * closestDistance;
	}

	public float BestLiftDistanceFromGround()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 closestGround = RaycastGround();
		if (closestGround.Y > MarniteArchitectHeadgear.MaxLiftHeight)
		{
			return -1f;
		}
		return closestGround.Y;
	}

	public override void PreUpdateMovement()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (!mounted)
		{
			return;
		}
		float distanceToGround = BestLiftDistanceFromGround();
		if (!(distanceToGround >= 0f))
		{
			return;
		}
		float newVelocity;
		if (base.Player.controlUp || base.Player.controlJump)
		{
			newVelocity = 0f - MarniteArchitectHeadgear.LiftRaiseSpeed;
			if (distanceToGround + MarniteArchitectHeadgear.LiftRaiseSpeed > MarniteArchitectHeadgear.MaxLiftHeight - 3f)
			{
				newVelocity = (MarniteArchitectHeadgear.MaxLiftHeight - 3f - distanceToGround) * -1f;
			}
			base.Player.fallStart = (int)(base.Player.Center.Y / 16f);
		}
		else if (base.Player.controlDown)
		{
			newVelocity = MarniteArchitectHeadgear.LiftRaiseSpeed;
			if (Collision.TileCollision(base.Player.position, new Vector2(base.Player.velocity.X, newVelocity), base.Player.width, base.Player.height, fallThrough: true, fall2: false, (int)base.Player.gravDir).Y == 0f)
			{
				newVelocity = 0.5f;
			}
		}
		else
		{
			newVelocity = 0f;
		}
		if (Math.Abs(base.Player.velocity.Y) < 4f)
		{
			base.Player.velocity.Y = newVelocity;
			base.Player.fallStart = (int)(base.Player.Center.Y / 16f);
		}
		else
		{
			base.Player.velocity.Y = MathHelper.Lerp(base.Player.velocity.Y, newVelocity, 0.12f);
		}
	}
}
