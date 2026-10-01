using Microsoft.Xna.Framework;

namespace CalamityMod.Systems;

public abstract class TemporaryTileManager
{
	public abstract int[] ManagedTypes { get; }

	public virtual void UpdateEffect(TemporaryTile tile)
	{
	}

	public abstract TemporaryTile Setup(Point pos);

	public virtual void EndEffect(TemporaryTile tile)
	{
	}
}
