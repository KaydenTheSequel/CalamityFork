using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace CalamityMod;

public class LavaTileRunner : TileRunner
{
	public LavaTileRunner(Vector2 pos, Vector2 speed, Point16 hRange, Point16 vRange, double strength, int steps, ushort type, bool addTile, bool overRide)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector(pos, speed, hRange, vRange, strength, steps, type, addTile, overRide);
	}

	public override void ChangeTile(Tile tile)
	{
		tile.HasTile = false;
		tile.LiquidType = 1;
		tile.LiquidAmount = byte.MaxValue;
	}
}
