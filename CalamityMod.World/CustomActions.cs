using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public static class CustomActions
{
	public class SolidScanner : GenAction
	{
		private int _count;

		public int GetCount()
		{
			return _count;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			Tile tile = GenBase._tiles[x, y];
			if (tile.HasTile && Main.tileSolid[tile.TileType])
			{
				_count++;
			}
			return UnitApply(origin, x, y, args);
		}
	}

	public class PlaceTree : GenAction
	{
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			return WorldGen.GrowTree(x, y);
		}
	}

	public class SetPaint : GenAction
	{
		private readonly byte _paintID;

		public SetPaint(byte paintID)
		{
			_paintID = paintID;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			if (!WorldGen.InWorld(x, y))
			{
				return false;
			}
			WorldGen.paintTile(x, y, _paintID);
			return UnitApply(origin, x, y, args);
		}
	}

	public class JungleGrass : GenAction
	{
		private bool _tryMushrooms;

		public JungleGrass(bool mush)
		{
			_tryMushrooms = mush;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			if (GenBase._tiles[x, y].HasTile || GenBase._tiles[x, y - 1].HasTile)
			{
				return false;
			}
			if (GenBase._tiles[x, y + 1].TileType == 60)
			{
				WorldGen.PlaceTile(x, y, GenBase._random.Next(new ushort[2] { 61, 74 }), mute: true);
			}
			else if (_tryMushrooms && GenBase._tiles[x, y + 1].TileType == 70)
			{
				WorldGen.PlaceTile(x, y, 71);
			}
			return UnitApply(origin, x, y, args);
		}
	}

	public class RandomFrom : GenAction
	{
		private GenAction[] _actions;

		private float[] _weights;

		public RandomFrom(GenAction[] actions, float[] weights)
		{
			_actions = actions;
			_weights = new float[weights.Length];
			for (int i = 0; i < weights.Length; i++)
			{
				if (i == 0)
				{
					_weights[0] = weights[0];
				}
				else
				{
					_weights[i] = _weights[i - 1] + weights[i];
				}
			}
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			float number = GenBase._random.NextFloat(0f, 1f);
			int index;
			for (index = 0; number > _weights[index]; index++)
			{
			}
			return _actions[index].Apply(origin, x, y, args);
		}
	}

	public class DistanceFromOrigin : GenAction
	{
		private float _distance;

		private bool _greaterThan;

		public DistanceFromOrigin(bool greater, float distance)
		{
			_greaterThan = greater;
			_distance = distance;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			float distance = Vector2.Distance(origin.ToWorldCoordinates(), new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
			if (_greaterThan && distance > _distance)
			{
				return UnitApply(origin, x, y, args);
			}
			if (distance < _distance)
			{
				return UnitApply(origin, x, y, args);
			}
			return false;
		}
	}
}
