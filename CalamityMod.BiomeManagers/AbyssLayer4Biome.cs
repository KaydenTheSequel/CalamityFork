using CalamityMod.CalPlayer;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using CalamityMod.Waters;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class AbyssLayer4Biome : ModBiome
{
	public override int Music
	{
		get
		{
			if (CalamityPlayer.areThereAnyDamnBosses)
			{
				return Main.curMusic;
			}
			return CalamityMod.Instance.GetMusicFromMusicMod("AbyssLayer4") ?? 36;
		}
	}

	public override ModWaterStyle WaterStyle => VoidWater.Instance;

	public override int BiomeTorchItemType => ModContent.ItemType<VoidTorch>();

	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/AbyssLayer4Icon";

	public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer4";

	public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer4";

	public override bool IsBiomeActive(Player player)
	{
		if (Main.remixWorld)
		{
			if (AbyssLayer1Biome.MeetsBaseAbyssRequirement(player, out var playerYTileCoords) && BiomeTileCounterSystem.Layer4Tiles >= 200)
			{
				return playerYTileCoords <= SulphurousSea.YStart - (int)((float)Main.UnderworldLayer * 0.6f);
			}
			return false;
		}
		if (AbyssLayer1Biome.MeetsBaseAbyssRequirement(player, out var playerYTileCoords2) && BiomeTileCounterSystem.Layer4Tiles >= 200)
		{
			return (double)playerYTileCoords2 > Main.rockLayer + (double)Main.maxTilesY * 0.268;
		}
		return false;
	}
}
