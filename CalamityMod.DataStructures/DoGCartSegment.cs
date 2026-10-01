using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.DataStructures;

public class DoGCartSegment
{
	public Vector2 Center;

	public float Rotation;

	public int OldDirection;

	public void Update(Player player, Vector2 aheadPosition, float idealRotation)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		int direction = (player.velocity.SafeNormalize(Vector2.UnitX * (float)player.direction).X > 0f).ToDirectionInt();
		if (player.velocity.X == 0f)
		{
			direction = player.direction;
		}
		if (OldDirection != direction)
		{
			if (OldDirection != 0)
			{
				Center = player.Center - Center + player.Center;
			}
			OldDirection = direction;
		}
		Vector2 offsetDirection = (aheadPosition - Center).SafeNormalize(Vector2.Zero);
		offsetDirection = offsetDirection.ToRotation().AngleTowards(idealRotation, 0.2f).ToRotationVector2();
		Rotation = offsetDirection.ToRotation();
		Center = aheadPosition - offsetDirection * 20f;
	}
}
