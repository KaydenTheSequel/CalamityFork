using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using CalamityMod.Waters;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class AstralInfectionBiome : ModBiome
{
	public override ModWaterStyle WaterStyle => AstralWater.Instance;

	public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
	{
		get
		{
			if (Main.LocalPlayer.ZoneSnow)
			{
				return ModContent.Find<ModSurfaceBackgroundStyle>("CalamityMod/AstralSnowSurfaceBGStyle");
			}
			if (Main.LocalPlayer.ZoneDesert && !Main.LocalPlayer.ZoneSnow)
			{
				return ModContent.Find<ModSurfaceBackgroundStyle>("CalamityMod/AstralDesertSurfaceBGStyle");
			}
			return ModContent.Find<ModSurfaceBackgroundStyle>("CalamityMod/AstralSurfaceBGStyle");
		}
	}

	public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
	{
		get
		{
			_ = Main.LocalPlayer.ZoneSnow;
			return ModContent.Find<ModUndergroundBackgroundStyle>("CalamityMod/AstralUndergroundBGStyle");
		}
	}

	public override int BiomeTorchItemType => ModContent.ItemType<AstralTorch>();

	public override SceneEffectPriority Priority
	{
		get
		{
			if (Main.LocalPlayer.ZoneDesert && Sandstorm.Happening && !Main.LocalPlayer.ZoneSnow && !Main.slimeRain && !Main.eclipse)
			{
				return SceneEffectPriority.Environment;
			}
			return SceneEffectPriority.BiomeHigh;
		}
	}

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/AbovegroundAstralBiomeIcon";

	public override string BackgroundPath
	{
		get
		{
			if (Main.LocalPlayer.ZoneDesert && !Main.LocalPlayer.ZoneSnow)
			{
				return "CalamityMod/Backgrounds/MapBackgrounds/AstralBG";
			}
			if (!Main.LocalPlayer.ZoneDirtLayerHeight && !Main.LocalPlayer.ZoneRockLayerHeight)
			{
				_ = Main.LocalPlayer.ZoneUnderworldHeight;
			}
			return "CalamityMod/Backgrounds/MapBackgrounds/AstralBG";
		}
	}

	public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/AstralBG";

	public override int Music
	{
		get
		{
			if (Main.LocalPlayer.ZoneDirtLayerHeight || Main.LocalPlayer.ZoneRockLayerHeight || Main.LocalPlayer.ZoneUnderworldHeight)
			{
				return CalamityMod.Instance.GetMusicFromMusicMod("AstralInfectionUnderground") ?? 15;
			}
			return CalamityMod.Instance.GetMusicFromMusicMod("AstralInfection") ?? 15;
		}
	}

	public override bool IsBiomeActive(Player player)
	{
		if (!player.ZoneDungeon)
		{
			return BiomeTileCounterSystem.AstralTiles > 950;
		}
		return false;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:Astral", isActive);
		if (Main.LocalPlayer.ZoneDirtLayerHeight || Main.LocalPlayer.ZoneRockLayerHeight || Main.LocalPlayer.ZoneUnderworldHeight)
		{
			player.ManageSpecialBiomeVisuals("CalamityMod:Astral", isActive);
		}
	}
}
