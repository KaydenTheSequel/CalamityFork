using System;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.DraedonStructures;
using CalamityMod.Tiles.Ores;
using CalamityMod.Tiles.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class BiomeTileCounterSystem : ModSystem
{
	public static int BrimstoneCragTiles;

	public static int SulphurTiles;

	public static int AstralTiles;

	public static int SunkenSeaTiles;

	public static int SunkenSeaShoresTiles;

	public static int SunkenSeaPolypTiles;

	public static int SunkenSeaReefsTiles;

	public static int SunkenSeaBurrowsTiles;

	public static int SunkenSeaBasaltTiles;

	public static int ArsenalLabTiles;

	public static int AbyssTiles;

	public static int UndergroundTiles;

	public static int Layer1Tiles;

	public static int Layer2Tiles;

	public static int Layer3Tiles;

	public static int Layer4Tiles;

	public override void ResetNearbyTileEffects()
	{
		BrimstoneCragTiles = 0;
		AstralTiles = 0;
		SunkenSeaTiles = 0;
		SunkenSeaShoresTiles = 0;
		SunkenSeaPolypTiles = 0;
		SunkenSeaReefsTiles = 0;
		SunkenSeaBurrowsTiles = 0;
		SunkenSeaBasaltTiles = 0;
		SulphurTiles = 0;
		AbyssTiles = 0;
		ArsenalLabTiles = 0;
		UndergroundTiles = 0;
		Layer1Tiles = 0;
		Layer2Tiles = 0;
		Layer3Tiles = 0;
		Layer4Tiles = 0;
	}

	public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
	{
		BrimstoneCragTiles = tileCounts[ModContent.TileType<InfernalSuevite>()] + tileCounts[ModContent.TileType<BrimstoneSlag>()];
		SunkenSeaTiles = tileCounts[ModContent.TileType<EutrophicSand>()] + tileCounts[ModContent.TileType<HardenedEutrophicSand>()] + tileCounts[ModContent.TileType<Navystone>()] + tileCounts[ModContent.TileType<Limestone>()] + tileCounts[ModContent.TileType<PolypSand>()] + tileCounts[ModContent.TileType<Dunesand>()] + tileCounts[ModContent.TileType<Runestone>()] + tileCounts[ModContent.TileType<Shellstone>()] + tileCounts[ModContent.TileType<MossyStone>()];
		SunkenSeaShoresTiles = tileCounts[ModContent.TileType<Runestone>()] + tileCounts[ModContent.TileType<Dunesand>()] + tileCounts[ModContent.TileType<AridSoil>()];
		SunkenSeaPolypTiles = tileCounts[ModContent.TileType<Limestone>()] + tileCounts[ModContent.TileType<PolypSand>()] + tileCounts[ModContent.TileType<ScarletSeaGrassTile>()];
		SunkenSeaReefsTiles = tileCounts[ModContent.TileType<Shellstone>()] + tileCounts[ModContent.TileType<EutrophicSand>()] + tileCounts[ModContent.TileType<YellowCoral>()] + tileCounts[ModContent.TileType<OrangeCoral>()] + tileCounts[ModContent.TileType<CyanCoral>()] + tileCounts[ModContent.TileType<LimeCoral>()] + tileCounts[ModContent.TileType<MagentaCoral>()];
		SunkenSeaBurrowsTiles = tileCounts[ModContent.TileType<Navystone>()] + tileCounts[ModContent.TileType<HardenedEutrophicSand>()] + tileCounts[ModContent.TileType<WhitePearlPile>()] + tileCounts[ModContent.TileType<BlackPearlPile>()] + tileCounts[ModContent.TileType<PinkPearlPile>()] + tileCounts[ModContent.TileType<SeaPrism>()] + tileCounts[ModContent.TileType<MossyStone>()];
		SunkenSeaBasaltTiles = tileCounts[ModContent.TileType<Basalt>()] + tileCounts[ModContent.TileType<VolcanicSand>()];
		AbyssTiles = tileCounts[ModContent.TileType<AbyssGravel>()] + tileCounts[ModContent.TileType<Voidstone>()];
		SulphurTiles = tileCounts[ModContent.TileType<SulphurousSand>()] + tileCounts[ModContent.TileType<SulphurousSandstone>()] + tileCounts[ModContent.TileType<HardenedSulphurousSandstone>()];
		ArsenalLabTiles = tileCounts[ModContent.TileType<LaboratoryPanels>()] + tileCounts[ModContent.TileType<LaboratoryPlating>()] + tileCounts[ModContent.TileType<HazardChevronPanels>()];
		UndergroundTiles = tileCounts[1];
		Layer1Tiles = tileCounts[ModContent.TileType<SulphurousShale>()];
		Layer2Tiles = tileCounts[ModContent.TileType<AbyssGravel>()] + tileCounts[ModContent.TileType<PlantyMush>()];
		Layer3Tiles = tileCounts[ModContent.TileType<PyreMantle>()];
		Layer4Tiles = tileCounts[ModContent.TileType<Voidstone>()];
		int astralDesertTiles = tileCounts[ModContent.TileType<AstralSand>()] + tileCounts[ModContent.TileType<AstralSandstone>()] + tileCounts[ModContent.TileType<HardenedAstralSand>()] + tileCounts[ModContent.TileType<CelestialRemains>()];
		int astralSnowTiles = tileCounts[ModContent.TileType<AstralIce>()] + tileCounts[ModContent.TileType<AstralSnow>()];
		Main.SceneMetrics.SandTileCount += astralDesertTiles;
		Main.SceneMetrics.SnowTileCount += astralSnowTiles;
		AstralTiles = astralDesertTiles + astralSnowTiles + tileCounts[ModContent.TileType<AstralDirt>()] + tileCounts[ModContent.TileType<AstralStone>()] + tileCounts[ModContent.TileType<AstralGrass>()] + tileCounts[ModContent.TileType<AstralOre>()] + tileCounts[ModContent.TileType<NovaeSlag>()] + tileCounts[ModContent.TileType<AstralClay>()];
	}
}
