using Microsoft.Xna.Framework;

namespace CalamityMod;

public struct RicoshotTarget
{
	public Vector2 pos;

	public RicoshotTargetType type;

	public int entityID;

	public bool IsValid => type != RicoshotTargetType.None;

	public RicoshotTarget()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		pos = -Vector2.One;
		type = RicoshotTargetType.None;
		entityID = -1;
	}
}
