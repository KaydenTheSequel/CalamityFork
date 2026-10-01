using CalamityMod.CalPlayer;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using CalamityMod.Waters;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class AbyssLayer2Biome : ModBiome
{
	public override int Music
	{
		get
		{
			if (CalamityPlayer.areThereAnyDamnBosses)
			{
				return Main.curMusic;
			}
			return CalamityMod.Instance.GetMusicFromMusicMod("AbyssLayer2") ?? 36;
		}
	}

	public override ModWaterStyle WaterStyle => UpperAbyssWater.Instance;

	public override int BiomeTorchItemType => ModContent.ItemType<KelpTorch>();

	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/AbyssLayer2Icon";

	public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer23";

	public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer23";

	public override bool IsBiomeActive(Player player)
	{
		if (Main.remixWorld)
		{
			if (AbyssLayer1Biome.MeetsBaseAbyssRequirement(player, out var playerYTileCoords) && BiomeTileCounterSystem.Layer2Tiles >= 200 && playerYTileCoords <= SulphurousSea.YStart - (int)((float)Main.UnderworldLayer * 0.2f))
			{
				return playerYTileCoords > SulphurousSea.YStart - (int)((float)Main.UnderworldLayer * 0.4f);
			}
			return false;
		}
		if (AbyssLayer1Biome.MeetsBaseAbyssRequirement(player, out var playerYTileCoords2) && BiomeTileCounterSystem.Layer2Tiles >= 200 && (double)playerYTileCoords2 > Main.rockLayer - 10.0)
		{
			return (double)playerYTileCoords2 <= Main.rockLayer + (double)Main.maxTilesY * 0.143;
		}
		return false;
	}
}
