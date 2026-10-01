using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public static class CustomConditions
{
	public class RandomChance : GenCondition
	{
		private float oneInThisValue;

		public RandomChance(float chance)
		{
			oneInThisValue = chance;
		}

		protected override bool CheckValidity(int x, int y)
		{
			return GenBase._random.NextFloat(oneInThisValue) <= 1f;
		}
	}

	public class IsWater : GenCondition
	{
		protected override bool CheckValidity(int x, int y)
		{
			Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
			if (tile.LiquidAmount >= 200)
			{
				return tile.LiquidType == 0;
			}
			return false;
		}
	}

	public class SolidOrPlatform : GenCondition
	{
		protected override bool CheckValidity(int x, int y)
		{
			if (!TileID.Sets.Platforms[CalamityUtils.ParanoidTileRetrieval(x, y).TileType])
			{
				return WorldGen.SolidTile(x, y);
			}
			return true;
		}
	}

	public class IsNotTouchingAir : GenCondition
	{
		private bool _useDiagonals;

		public IsNotTouchingAir(bool diagonals)
		{
			_useDiagonals = diagonals;
		}

		protected override bool CheckValidity(int x, int y)
		{
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 1; j <= y + 1; j++)
				{
					if ((j == y || i == x || _useDiagonals) && WorldGen.InWorld(i, j) && !GenBase._tiles[i, j].HasTile)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
