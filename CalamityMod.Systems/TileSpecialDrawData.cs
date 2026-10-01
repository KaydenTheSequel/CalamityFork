using Terraria;

namespace CalamityMod.Systems;

public struct TileSpecialDrawData : ITileData
{
	private byte Data;

	public bool HasBlendMergeData
	{
		readonly get
		{
			return TileDataPacking.GetBit(Data, 0);
		}
		set
		{
			Data = (byte)TileDataPacking.SetBit(value, Data, 0);
		}
	}

	public bool HasSpecialPoint
	{
		readonly get
		{
			return TileDataPacking.GetBit(Data, 1);
		}
		set
		{
			Data = (byte)TileDataPacking.SetBit(value, Data, 1);
		}
	}

	public bool Flag0
	{
		readonly get
		{
			return TileDataPacking.GetBit(Data, 4);
		}
		set
		{
			Data = (byte)TileDataPacking.SetBit(value, Data, 4);
		}
	}

	public bool Flag1
	{
		readonly get
		{
			return TileDataPacking.GetBit(Data, 5);
		}
		set
		{
			Data = (byte)TileDataPacking.SetBit(value, Data, 5);
		}
	}

	public bool Flag2
	{
		readonly get
		{
			return TileDataPacking.GetBit(Data, 6);
		}
		set
		{
			Data = (byte)TileDataPacking.SetBit(value, Data, 6);
		}
	}

	public bool Flag3
	{
		readonly get
		{
			return TileDataPacking.GetBit(Data, 7);
		}
		set
		{
			Data = (byte)TileDataPacking.SetBit(value, Data, 7);
		}
	}

	public readonly void GetFlags(out bool flag0, out bool flag1, out bool flag2, out bool flag3)
	{
		flag0 = Flag0;
		flag1 = Flag1;
		flag2 = Flag2;
		flag3 = Flag3;
	}

	public void SetFlags(bool flag0, bool flag1, bool flag2, bool flag3)
	{
		Flag0 = flag0;
		Flag1 = flag1;
		Flag2 = flag2;
		Flag3 = flag3;
	}
}
