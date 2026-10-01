using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.FurnitureMonolith;
using CalamityMod.Tiles.Ores;
using CalamityMod.Walls;
using CalamityMod.Walls.UnsafeWalls;
using Terraria.ModLoader;

namespace CalamityMod.World;

public class AstralConversion : ModBiomeConversion
{
	public static int GrassType;

	public static int DirtType;

	public static int StoneType;

	public static int SandType;

	public static int SandstoneType;

	public static int HardenedSandType;

	public static int SnowType;

	public static int IceType;

	public static int ClayType;

	public static int SiltType;

	public static int FossilType;

	public static int WoodType;

	public static int OreType;

	public static int GrassWallType;

	public static int DirtWallType;

	public static int StoneWallType;

	public static int SnowWallType;

	public static int IceWallType;

	public static int SandstoneWallType;

	public static int HardenedSandWallType;

	public static int FossilWallType;

	public static int WoodWallType;

	public override void PostSetupContent()
	{
		GrassType = ModContent.TileType<AstralGrass>();
		DirtType = ModContent.TileType<AstralDirt>();
		StoneType = ModContent.TileType<AstralStone>();
		SnowType = ModContent.TileType<AstralSnow>();
		IceType = ModContent.TileType<AstralIce>();
		SandType = ModContent.TileType<AstralSand>();
		SandstoneType = ModContent.TileType<AstralSandstone>();
		HardenedSandType = ModContent.TileType<HardenedAstralSand>();
		ClayType = ModContent.TileType<AstralClay>();
		SiltType = ModContent.TileType<NovaeSlag>();
		FossilType = ModContent.TileType<CelestialRemains>();
		WoodType = ModContent.TileType<AstralMonolith>();
		OreType = ModContent.TileType<AstralOre>();
		DirtWallType = ModContent.WallType<UnsafeAstralDirtWall>();
		GrassWallType = ModContent.WallType<UnsafeAstralGrassWall>();
		StoneWallType = ModContent.WallType<UnsafeAstralStoneWall>();
		SnowWallType = ModContent.WallType<UnsafeAstralSnowWall>();
		IceWallType = ModContent.WallType<UnsafeAstralIceWall>();
		SandstoneWallType = ModContent.WallType<UnsafeAstralSandstoneWall>();
		HardenedSandWallType = ModContent.WallType<UnsafeHardenedAstralSandWall>();
		FossilWallType = ModContent.WallType<CelestialRemainsWall>();
		WoodWallType = ModContent.WallType<AstralMonolithWall>();
		TileLoader.RegisterSimpleConversion(2, base.Type, GrassType);
		TileLoader.RegisterSimpleConversion(0, base.Type, DirtType);
		TileLoader.RegisterSimpleConversion(1, base.Type, StoneType);
		TileLoader.RegisterSimpleConversion(147, base.Type, SnowType);
		TileLoader.RegisterSimpleConversion(161, base.Type, IceType);
		TileLoader.RegisterSimpleConversion(53, base.Type, SandType);
		TileLoader.RegisterSimpleConversion(396, base.Type, SandstoneType);
		TileLoader.RegisterSimpleConversion(397, base.Type, HardenedSandType);
		TileLoader.RegisterSimpleConversion(40, base.Type, ClayType);
		TileLoader.RegisterSimpleConversion(123, base.Type, SiltType);
		TileLoader.RegisterSimpleConversion(404, base.Type, FossilType);
		TileLoader.RegisterSimpleConversion(191, base.Type, WoodType);
		TileLoader.RegisterSimpleConversion(37, base.Type, OreType);
		WallLoader.RegisterSimpleConversion(63, base.Type, GrassWallType);
		WallLoader.RegisterSimpleConversion(2, base.Type, DirtWallType);
		WallLoader.RegisterSimpleConversion(1, base.Type, StoneWallType);
		WallLoader.RegisterSimpleConversion(40, base.Type, SnowWallType);
		WallLoader.RegisterSimpleConversion(71, base.Type, IceWallType);
		WallLoader.RegisterSimpleConversion(187, base.Type, SandstoneWallType);
		WallLoader.RegisterSimpleConversion(216, base.Type, HardenedSandWallType);
		WallLoader.RegisterSimpleConversion(223, base.Type, FossilWallType);
		WallLoader.RegisterSimpleConversion(244, base.Type, WoodWallType);
		TileLoader.RegisterConversion(DirtType, 1, 0);
		TileLoader.RegisterConversion(DirtType, 4, 0);
		TileLoader.RegisterConversion(DirtType, 2, 0);
		TileLoader.RegisterConversion(SnowType, 1, 147);
		TileLoader.RegisterConversion(SnowType, 4, 147);
		TileLoader.RegisterConversion(SnowType, 2, 147);
	}
}
