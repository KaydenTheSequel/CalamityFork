using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.BaseTiles;

public abstract class DoubleDirectionalTile : ModTile
{
	public enum TileDirection : byte
	{
		Up,
		Down,
		Left,
		Right,
		UpLeft,
		UpRight,
		DownLeft,
		DownRight,
		Center
	}

	public static readonly TileDirection?[] BroadTopLeft = new TileDirection?[3]
	{
		TileDirection.Left,
		TileDirection.UpLeft,
		TileDirection.Up
	};

	public static readonly TileDirection?[] BroadTopRight = new TileDirection?[3]
	{
		TileDirection.Right,
		TileDirection.UpRight,
		TileDirection.Up
	};

	public static readonly TileDirection?[] BroadBottomLeft = new TileDirection?[3]
	{
		TileDirection.Left,
		TileDirection.DownLeft,
		TileDirection.Down
	};

	public static readonly TileDirection?[] BroadBottomRight = new TileDirection?[3]
	{
		TileDirection.Right,
		TileDirection.DownRight,
		TileDirection.Down
	};

	public static readonly TileDirection?[] BroadLeft = new TileDirection?[3]
	{
		TileDirection.Left,
		TileDirection.UpLeft,
		TileDirection.DownLeft
	};

	public static readonly TileDirection?[] BroadRight = new TileDirection?[3]
	{
		TileDirection.Right,
		TileDirection.UpRight,
		TileDirection.DownRight
	};

	public static readonly TileDirection?[] BroadTop = new TileDirection?[3]
	{
		TileDirection.UpLeft,
		TileDirection.Up,
		TileDirection.UpRight
	};

	public static readonly TileDirection?[] BroadBottom = new TileDirection?[3]
	{
		TileDirection.DownRight,
		TileDirection.Down,
		TileDirection.DownLeft
	};

	public static readonly TileDirection?[,] WhereDoIPoint = new TileDirection?[5, 13]
	{
		{
			TileDirection.Left,
			TileDirection.Up,
			TileDirection.Up,
			TileDirection.Up,
			TileDirection.Right,
			null,
			null,
			null,
			null,
			null,
			TileDirection.Left,
			TileDirection.Right,
			null
		},
		{
			TileDirection.Left,
			TileDirection.Center,
			TileDirection.Center,
			TileDirection.Center,
			TileDirection.Right,
			null,
			TileDirection.Up,
			TileDirection.Up,
			TileDirection.Up,
			null,
			TileDirection.Left,
			TileDirection.Right,
			null
		},
		{
			TileDirection.Left,
			TileDirection.Down,
			TileDirection.Down,
			TileDirection.Down,
			TileDirection.Right,
			null,
			TileDirection.Down,
			TileDirection.Down,
			TileDirection.Down,
			null,
			TileDirection.Left,
			TileDirection.Right,
			null
		},
		{
			TileDirection.UpLeft,
			TileDirection.UpRight,
			TileDirection.UpLeft,
			TileDirection.UpRight,
			TileDirection.UpLeft,
			TileDirection.UpRight,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		},
		{
			TileDirection.DownLeft,
			TileDirection.DownRight,
			TileDirection.DownLeft,
			TileDirection.DownRight,
			TileDirection.DownLeft,
			TileDirection.DownRight,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		}
	};

	public static readonly int?[,] WhatVariantAmI = new int?[5, 13]
	{
		{
			1, 1, 2, 3, 1, 1, 1, 2, 3, 1,
			1, 1, 1
		},
		{
			2, 1, 2, 3, 2, 2, 1, 2, 3, 2,
			2, 2, 2
		},
		{
			3, 1, 2, 3, 3, 3, 1, 2, 3, 3,
			3, 3, 3
		},
		{
			1, 1, 2, 2, 3, 3, 1, 2, 3, 1,
			2, 3, null
		},
		{
			1, 1, 2, 2, 3, 3, 1, 2, 3, null,
			null, null, null
		}
	};

	public virtual bool TransitionHardRequired => false;

	public TileDirection? GiveDirection(int typeToConnectTo, int i, int j)
	{
		Tile tile = CalamityUtils.ParanoidTileRetrieval(i, j);
		if (tile.TileType != typeToConnectTo)
		{
			return null;
		}
		int slotY = tile.TileFrameX / 18;
		int slotX = tile.TileFrameY / 18;
		if (slotX >= 5 || slotY >= 13)
		{
			return null;
		}
		return WhereDoIPoint[slotX, slotY];
	}

	public int GiveVariant(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int slotY = tile.TileFrameX / 18;
		int slotX = tile.TileFrameY / 18;
		return WhatVariantAmI[slotX, slotY].Value;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		if (GiveDirection(type, i, j) != TileDirection.Center)
		{
			return;
		}
		TileDirection? TopLeft = GiveDirection(type, i - 1, j - 1);
		TileDirection? Top = GiveDirection(type, i, j - 1);
		TileDirection? TopRight = GiveDirection(type, i + 1, j - 1);
		TileDirection? Left = GiveDirection(type, i - 1, j);
		TileDirection? Right = GiveDirection(type, i + 1, j);
		TileDirection? BottomLeft = GiveDirection(type, i - 1, j + 1);
		TileDirection? Bottom = GiveDirection(type, i, j + 1);
		TileDirection? BottomRight = GiveDirection(type, i + 1, j + 1);
		if (Top == TileDirection.Up && Bottom == TileDirection.Center)
		{
			frameXOffset = -18 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if (Top == TileDirection.Center && Bottom == TileDirection.Down)
		{
			frameXOffset = -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if (Left == TileDirection.Left && Right == TileDirection.Center)
		{
			frameXOffset = 18 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if (Left == TileDirection.Center && Right == TileDirection.Right)
		{
			frameXOffset = 36 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if ((Top == TileDirection.Up && Left == TileDirection.Left && Bottom == TileDirection.Center && Right == TileDirection.Center) || ((Top == TileDirection.Left || Top == TileDirection.UpLeft) && (Left == TileDirection.Up || Left == TileDirection.UpLeft) && (BottomRight == TileDirection.Center || BroadBottomRight.Contains(BottomRight))))
		{
			frameXOffset = 54 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if ((Top == TileDirection.Up && Right == TileDirection.Right && Bottom == TileDirection.Center && Left == TileDirection.Center) || ((Top == TileDirection.Right || Top == TileDirection.UpRight) && (Right == TileDirection.Up || Right == TileDirection.UpRight) && (BottomLeft == TileDirection.Center || BroadBottomLeft.Contains(BottomLeft))))
		{
			frameXOffset = 72 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if ((Bottom == TileDirection.Down && Left == TileDirection.Left && Top == TileDirection.Center && Right == TileDirection.Center) || ((Bottom == TileDirection.Left || Bottom == TileDirection.DownLeft) && (Left == TileDirection.Down || Left == TileDirection.DownLeft) && (TopRight == TileDirection.Center || BroadTopRight.Contains(TopRight))))
		{
			frameXOffset = 90 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if ((Bottom == TileDirection.Down && Right == TileDirection.Right && Top == TileDirection.Center && Left == TileDirection.Center) || ((Bottom == TileDirection.Right || Bottom == TileDirection.DownRight) && (Right == TileDirection.Down || Right == TileDirection.DownRight) && (TopLeft == TileDirection.Center || BroadTopLeft.Contains(TopLeft))))
		{
			frameXOffset = 108 + -18 * (GiveVariant(i, j) - 1);
			frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
		}
		if (TransitionHardRequired)
		{
			if (BroadLeft.Contains(Left) && BroadRight.Contains(Right) && Top == TileDirection.Center && Bottom == TileDirection.Center)
			{
				frameXOffset = -18;
				frameYOffset = 126;
			}
			if (BroadTop.Contains(Top) && BroadBottom.Contains(Bottom) && Left == TileDirection.Center && Right == TileDirection.Center)
			{
				frameXOffset = -18;
				frameYOffset = 144;
			}
			if (BroadTop.Contains(Top) && BroadBottom.Contains(Bottom) && BroadLeft.Contains(Left) && Right == TileDirection.Center)
			{
				frameXOffset = 126 + -18 * (GiveVariant(i, j) - 1);
				frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
			}
			if (BroadTop.Contains(Top) && BroadBottom.Contains(Bottom) && BroadRight.Contains(Right) && Left == TileDirection.Center)
			{
				frameXOffset = 144 + -18 * (GiveVariant(i, j) - 1);
				frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
			}
			if (BroadLeft.Contains(Left) && BroadRight.Contains(Right) && BroadTop.Contains(Top) && Bottom == TileDirection.Center)
			{
				frameXOffset = 162 + -18 * (GiveVariant(i, j) - 1);
				frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
			}
			if (BroadLeft.Contains(Left) && BroadRight.Contains(Right) && BroadBottom.Contains(Bottom) && Top == TileDirection.Center)
			{
				frameXOffset = 180 + -18 * (GiveVariant(i, j) - 1);
				frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
			}
			if (Left != TileDirection.Center && Right != TileDirection.Center && Top != TileDirection.Center && Bottom != TileDirection.Center)
			{
				frameXOffset = 198 + -18 * (GiveVariant(i, j) - 1);
				frameYOffset = 72 + 18 * (GiveVariant(i, j) - 1);
			}
		}
	}
}
