using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class AcidRainBiome : ModBiome
{
	public override string BestiaryIcon => "CalamityMod/BiomeManagers/AcidRainIcon";

	public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/SulphurBG";

	public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
}
