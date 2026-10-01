using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using CalamityMod.Waters;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class SulphurousSeaBiome : ModBiome
{
	public override ModWaterStyle WaterStyle
	{
		get
		{
			if (!Main.zenithWorld)
			{
				return SulphuricWater.Instance;
			}
			return PissWater.Instance;
		}
	}

	public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
	{
		get
		{
			if (!Main.zenithWorld)
			{
				return ModContent.Find<ModSurfaceBackgroundStyle>("CalamityMod/SulphurSeaSurfaceBGStyle");
			}
			return ModContent.Find<ModSurfaceBackgroundStyle>("CalamityMod/PissSeaSurfaceBGStyle");
		}
	}

	public override int BiomeTorchItemType => ModContent.ItemType<SulphurousTorch>();

	public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/SulphurousSeaIcon";

	public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/SulphurBG";

	public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/SulphurBG";

	public override int Music
	{
		get
		{
			int music = Main.curMusic;
			if (!CalamityPlayer.areThereAnyDamnBosses)
			{
				bool acidRainEventIsOngoing = AcidRainEvent.AcidRainEventIsOngoing;
				bool normalRain = Main.cloudAlpha > 0f;
				music = (acidRainEventIsOngoing ? (DownedBossSystem.downedPolterghast ? (CalamityMod.Instance.GetMusicFromMusicMod("AcidRainTier3") ?? 52) : (CalamityMod.Instance.GetMusicFromMusicMod("AcidRainTier1") ?? 41)) : ((!normalRain) ? ((!Main.dayTime) ? (CalamityMod.Instance.GetMusicFromMusicMod("SulphurousSeaNight") ?? 21) : (CalamityMod.Instance.GetMusicFromMusicMod("SulphurousSeaDay") ?? 21)) : (CalamityMod.Instance.GetMusicFromMusicMod("SulphurousSeaRain") ?? 21)));
			}
			return music;
		}
	}

	public override bool IsBiomeActive(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		Point point = player.Center.ToTileCoordinates();
		if (BiomeTileCounterSystem.SulphurTiles < 300)
		{
			if (IsInBiomePosition(point))
			{
				return !player.Calamity().ZoneAbyss;
			}
			return false;
		}
		return true;
	}

	public static bool IsInBiomePosition(Point tilePos)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		bool sulphurPosX = false;
		if (Abyss.AtLeftSideOfWorld)
		{
			if (tilePos.X < 435)
			{
				sulphurPosX = true;
			}
		}
		else if (tilePos.X > Main.maxTilesX - 435)
		{
			sulphurPosX = true;
		}
		if (Main.remixWorld)
		{
			if ((tilePos.Y > SulphurousSea.YStart && tilePos.Y < Main.UnderworldLayer) & sulphurPosX)
			{
				return !WeakReferenceSupport.InAnySubworld();
			}
			return false;
		}
		if (((double)tilePos.Y < Main.rockLayer - (double)(Main.maxTilesY / 13)) & sulphurPosX)
		{
			return !WeakReferenceSupport.InAnySubworld();
		}
		return false;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		string biomeName = "CalamityMod:SulphurSea";
		if (SkyManager.Instance[biomeName] != null && isActive != SkyManager.Instance[biomeName].IsActive())
		{
			if (isActive)
			{
				SkyManager.Instance.Activate(biomeName, default(Vector2));
			}
			else
			{
				SkyManager.Instance.Deactivate(biomeName);
			}
		}
	}
}
