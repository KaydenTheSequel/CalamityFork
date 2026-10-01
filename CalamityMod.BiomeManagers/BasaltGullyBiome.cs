using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using CalamityMod.Waters;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class BasaltGullyBiome : ModBiome
{
	public override ModWaterStyle WaterStyle => SunkenSeaBurrowsWater.Instance;

	public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.Find<ModUndergroundBackgroundStyle>("CalamityMod/SunkenSeaBGStyle");

	public override int BiomeTorchItemType => ModContent.ItemType<NavyPrismTorch>();

	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("SunkenSea") ?? 43;

	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/SunkenSeaIcon";

	public override string BackgroundPath => "Terraria/Images/MapBG3";

	public override string MapBackground => "Terraria/Images/MapBG3";

	public override bool IsBiomeActive(Player player)
	{
		bool MoreThanShores = BiomeTileCounterSystem.SunkenSeaBasaltTiles > BiomeTileCounterSystem.SunkenSeaShoresTiles;
		bool MoreThanReefs = BiomeTileCounterSystem.SunkenSeaBasaltTiles > BiomeTileCounterSystem.SunkenSeaReefsTiles;
		bool MoreThanPolyp = BiomeTileCounterSystem.SunkenSeaBasaltTiles > BiomeTileCounterSystem.SunkenSeaPolypTiles;
		bool MoreThanBurrows = BiomeTileCounterSystem.SunkenSeaBasaltTiles > BiomeTileCounterSystem.SunkenSeaBurrowsTiles;
		_ = BiomeTileCounterSystem.SunkenSeaShoresTiles;
		_ = BiomeTileCounterSystem.UndergroundTiles;
		if (BiomeTileCounterSystem.SunkenSeaBasaltTiles > 2500)
		{
			return MoreThanShores | MoreThanReefs | MoreThanPolyp | MoreThanBurrows;
		}
		return false;
	}
}
