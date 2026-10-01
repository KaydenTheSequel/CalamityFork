using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class BrimstoneCragsBiome : ModBiome
{
	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("BrimstoneCrags") ?? 2;

	public override int BiomeTorchItemType => ModContent.ItemType<GloomTorch>();

	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/BrimstoneCragsIcon";

	public override string BackgroundPath => "Terraria/Images/MapBG3";

	public override bool IsBiomeActive(Player player)
	{
		if (BiomeTileCounterSystem.BrimstoneCragTiles > 500)
		{
			return player.ZoneUnderworldHeight;
		}
		return false;
	}
}
