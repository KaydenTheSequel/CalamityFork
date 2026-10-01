using System.Diagnostics;
using Terraria;

namespace CalamityMod.Schematics;

[DebuggerDisplay("Tile ID = {TileType}, Wall ID = {WallType}")]
public struct SchematicMetaTile
{
	internal ushort TileType;

	internal ushort WallType;

	internal byte LiquidAmount;

	internal byte LiquidType;

	internal TileWallBrightnessInvisibilityData? brightnessInvisibility;

	internal TileWallWireStateData wallWireState;

	public bool keepTile;

	public bool keepWall;

	public SchematicMetaTile(Tile t)
	{
		TileType = t.TileType;
		WallType = t.WallType;
		LiquidAmount = t.LiquidAmount;
		LiquidType = (byte)t.LiquidType;
		TileWallBrightnessInvisibilityData twbid = t.Get<TileWallBrightnessInvisibilityData>();
		brightnessInvisibility = new TileWallBrightnessInvisibilityData
		{
			IsTileInvisible = twbid.IsTileInvisible,
			IsWallInvisible = twbid.IsWallInvisible,
			IsTileFullbright = twbid.IsTileFullbright,
			IsWallFullbright = twbid.IsWallFullbright
		};
		TileWallWireStateData twwsd = t.Get<TileWallWireStateData>();
		wallWireState = new TileWallWireStateData
		{
			HasTile = twwsd.HasTile,
			IsActuated = twwsd.IsActuated,
			HasActuator = twwsd.HasActuator,
			TileColor = twwsd.TileColor,
			WallColor = twwsd.WallColor,
			TileFrameNumber = twwsd.TileFrameNumber,
			WallFrameNumber = twwsd.WallFrameNumber,
			WallFrameX = twwsd.WallFrameX,
			WallFrameY = twwsd.WallFrameY,
			IsHalfBlock = twwsd.IsHalfBlock,
			Slope = twwsd.Slope,
			WireData = twwsd.WireData,
			TileFrameX = t.TileFrameX,
			TileFrameY = t.TileFrameY
		};
		keepTile = false;
		keepWall = false;
	}

	public void ApplyTo(int x, int y, SchematicMetaTile original)
	{
		Tile tile;
		if (!keepTile && !keepWall)
		{
			tile = Main.tile[x, y];
			tile.TileType = TileType;
			tile = Main.tile[x, y];
			tile.WallType = WallType;
			tile = Main.tile[x, y];
			tile.LiquidAmount = LiquidAmount;
			tile = Main.tile[x, y];
			tile.Get<LiquidData>().LiquidType = LiquidType;
			tile = Main.tile[x, y];
			ref TileWallBrightnessInvisibilityData targetWBIState = ref tile.Get<TileWallBrightnessInvisibilityData>();
			if (brightnessInvisibility.HasValue)
			{
				TileWallBrightnessInvisibilityData biActual = brightnessInvisibility.Value;
				targetWBIState.IsTileInvisible = biActual.IsTileInvisible;
				targetWBIState.IsWallInvisible = biActual.IsWallInvisible;
				targetWBIState.IsTileFullbright = biActual.IsTileFullbright;
				targetWBIState.IsWallFullbright = biActual.IsWallFullbright;
			}
			tile = Main.tile[x, y];
			ref TileWallWireStateData reference = ref tile.Get<TileWallWireStateData>();
			CalamitySchematicIO.AssignWallWireState(ref reference, wallWireState.NonFrameBits);
			reference.TileFrameX = wallWireState.TileFrameX;
			reference.TileFrameY = wallWireState.TileFrameY;
		}
		else if (keepTile && keepWall)
		{
			tile = Main.tile[x, y];
			tile.TileType = original.TileType;
			tile = Main.tile[x, y];
			tile.WallType = original.WallType;
			tile = Main.tile[x, y];
			tile.LiquidAmount = original.LiquidAmount;
			tile = Main.tile[x, y];
			tile.Get<LiquidData>().LiquidType = original.LiquidType;
			tile = Main.tile[x, y];
			ref TileWallBrightnessInvisibilityData reference2 = ref tile.Get<TileWallBrightnessInvisibilityData>();
			TileWallBrightnessInvisibilityData biOriginal = original.brightnessInvisibility.Value;
			reference2.IsTileInvisible = biOriginal.IsTileInvisible;
			reference2.IsWallInvisible = biOriginal.IsWallInvisible;
			reference2.IsTileFullbright = biOriginal.IsTileFullbright;
			reference2.IsWallFullbright = biOriginal.IsWallFullbright;
			tile = Main.tile[x, y];
			ref TileWallWireStateData reference3 = ref tile.Get<TileWallWireStateData>();
			CalamitySchematicIO.AssignWallWireState(ref reference3, original.wallWireState.NonFrameBits);
			reference3.TileFrameX = original.wallWireState.TileFrameX;
			reference3.TileFrameY = original.wallWireState.TileFrameY;
		}
		else if (keepWall)
		{
			tile = Main.tile[x, y];
			tile.TileType = TileType;
			tile = Main.tile[x, y];
			tile.WallType = original.WallType;
			tile = Main.tile[x, y];
			tile.LiquidAmount = LiquidAmount;
			tile = Main.tile[x, y];
			tile.Get<LiquidData>().LiquidType = LiquidType;
			tile = Main.tile[x, y];
			ref TileWallBrightnessInvisibilityData targetWBIState2 = ref tile.Get<TileWallBrightnessInvisibilityData>();
			if (brightnessInvisibility.HasValue)
			{
				TileWallBrightnessInvisibilityData biActual2 = brightnessInvisibility.Value;
				targetWBIState2.IsTileInvisible = biActual2.IsTileInvisible;
				targetWBIState2.IsWallInvisible = biActual2.IsWallInvisible;
				targetWBIState2.IsTileFullbright = biActual2.IsTileFullbright;
				targetWBIState2.IsWallFullbright = biActual2.IsWallFullbright;
			}
			tile = Main.tile[x, y];
			ref TileWallWireStateData reference4 = ref tile.Get<TileWallWireStateData>();
			CalamitySchematicIO.AssignWallWireState(ref reference4, wallWireState.NonFrameBits);
			reference4.TileFrameX = wallWireState.TileFrameX;
			reference4.TileFrameY = wallWireState.TileFrameY;
			reference4.WallColor = original.wallWireState.WallColor;
			reference4.WallFrameNumber = original.wallWireState.WallFrameNumber;
			reference4.WallFrameX = original.wallWireState.WallFrameX;
			reference4.WallFrameY = original.wallWireState.WallFrameY;
			TileWallBrightnessInvisibilityData biOriginal2 = original.brightnessInvisibility.Value;
			targetWBIState2.IsWallInvisible = biOriginal2.IsWallInvisible;
			targetWBIState2.IsWallFullbright = biOriginal2.IsWallFullbright;
		}
		else if (keepTile)
		{
			tile = Main.tile[x, y];
			tile.TileType = original.TileType;
			tile = Main.tile[x, y];
			tile.WallType = WallType;
			tile = Main.tile[x, y];
			tile.LiquidAmount = original.LiquidAmount;
			tile = Main.tile[x, y];
			tile.Get<LiquidData>().LiquidType = original.LiquidType;
			tile = Main.tile[x, y];
			ref TileWallBrightnessInvisibilityData targetWBIState3 = ref tile.Get<TileWallBrightnessInvisibilityData>();
			TileWallBrightnessInvisibilityData biOriginal3 = original.brightnessInvisibility.Value;
			targetWBIState3.IsTileInvisible = biOriginal3.IsTileInvisible;
			targetWBIState3.IsWallInvisible = biOriginal3.IsWallInvisible;
			targetWBIState3.IsTileFullbright = biOriginal3.IsTileFullbright;
			targetWBIState3.IsWallFullbright = biOriginal3.IsWallFullbright;
			tile = Main.tile[x, y];
			ref TileWallWireStateData reference5 = ref tile.Get<TileWallWireStateData>();
			CalamitySchematicIO.AssignWallWireState(ref reference5, original.wallWireState.NonFrameBits);
			reference5.TileFrameX = original.wallWireState.TileFrameX;
			reference5.TileFrameY = original.wallWireState.TileFrameY;
			reference5.WallColor = wallWireState.WallColor;
			reference5.WallFrameNumber = wallWireState.WallFrameNumber;
			reference5.WallFrameX = wallWireState.WallFrameX;
			reference5.WallFrameY = wallWireState.WallFrameY;
			if (brightnessInvisibility.HasValue)
			{
				TileWallBrightnessInvisibilityData biActual3 = brightnessInvisibility.Value;
				targetWBIState3.IsWallInvisible = biActual3.IsWallInvisible;
				targetWBIState3.IsWallFullbright = biActual3.IsWallFullbright;
			}
		}
	}
}
