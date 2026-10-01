using Microsoft.Xna.Framework;

namespace CalamityMod.Physics;

public readonly record struct RopeSettings(bool StartIsFixed, bool EndIsFixed, bool RespondToEntityMovement, bool RespondToWind, Vector2? TileColliderArea, float Mass = 1f)
{
	public RopeSettings()
		: this(StartIsFixed: false, EndIsFixed: false, RespondToEntityMovement: false, RespondToWind: false, null)
	{
	}
}
