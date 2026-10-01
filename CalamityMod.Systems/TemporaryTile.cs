using Microsoft.Xna.Framework;

namespace CalamityMod.Systems;

public struct TemporaryTile
{
	public Point position;

	public int timeleft;

	public TemporaryTileManager manager;

	public TemporaryTile(Point pos, TemporaryTileManager manager_, int timeLeft = 60)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		position = pos;
		manager = manager_;
		timeleft = timeLeft;
	}
}
