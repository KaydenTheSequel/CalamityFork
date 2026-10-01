using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Walls;
using CalamityMod.Walls.UnsafeWalls;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class CalamityTileSets
{
	public static SetFactory Factory = new SetFactory(TileLoader.TileCount, "CalamityMod/TileID", Search);

	public static IdDictionary Search = IdDictionary.Create<TileID, int>();

	public static bool[] CanBeReplacedByAbyssGeneration = Factory.CreateBoolSet();

	public static bool[] DrawBlendMergeAfterSolidTile = Factory.CreateBoolSet(ModContent.TileType<SeaPrism>());

	public static bool[] IsAbyssWall = Factory.CreateBoolSet(ModContent.WallType<UnsafeSulphurousShaleWall>(), ModContent.WallType<UnsafeAbyssGravelWall>(), ModContent.WallType<PyreMantleWall>(), ModContent.WallType<UnsafeVoidstoneWall>(), ModContent.WallType<HardenedSulphurousSandstoneWall>(), ModContent.WallType<UnsafeSulphurousSandstoneWall>());
}
