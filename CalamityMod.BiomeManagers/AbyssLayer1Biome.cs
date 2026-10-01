using CalamityMod.CalPlayer;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Systems;
using CalamityMod.Waters;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class AbyssLayer1Biome : ModBiome
{
	public override ModWaterStyle WaterStyle => SulphuricDepthsWater.Instance;

	public override int BiomeTorchItemType => ModContent.ItemType<CausticTorch>();

	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/AbyssLayer1Icon";

	public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer1";

	public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer1";

	public override int Music
	{
		get
		{
			if (CalamityPlayer.areThereAnyDamnBosses)
			{
				return Main.curMusic;
			}
			return CalamityMod.Instance.GetMusicFromMusicMod("AbyssLayer1") ?? 21;
		}
	}

	public static bool MeetsBaseAbyssRequirement(Player player, out int playerYTileCoords)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Point point = player.Center.ToTileCoordinates();
		int maxTilesX = Main.maxTilesX;
		_ = Main.maxTilesY;
		int genLimit = maxTilesX / 2;
		int abyssChasmX = (Abyss.AtLeftSideOfWorld ? (genLimit - (genLimit - 135) + 35) : (genLimit + (genLimit - 135) - 35));
		bool abyssPosX = false;
		if (Abyss.AtLeftSideOfWorld)
		{
			if (point.X < abyssChasmX + 140)
			{
				abyssPosX = true;
			}
		}
		else if (point.X > abyssChasmX - 140)
		{
			abyssPosX = true;
		}
		playerYTileCoords = point.Y;
		if (WeakReferenceSupport.InAnySubworld())
		{
			return false;
		}
		int abyssStartHeight = (Main.remixWorld ? SulphurousSea.YStart : ((SulphurousSea.YStart + (int)Main.worldSurface) / 2 + 90));
		if (Main.remixWorld)
		{
			if ((!player.lavaWet && !player.honeyWet) & abyssPosX)
			{
				return playerYTileCoords < abyssStartHeight;
			}
			return false;
		}
		if (((!player.lavaWet && !player.honeyWet) & abyssPosX) && playerYTileCoords >= abyssStartHeight)
		{
			return playerYTileCoords <= Main.UnderworldLayer;
		}
		return false;
	}

	public override bool IsBiomeActive(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Point point = player.Center.ToTileCoordinates();
		int abyssStartHeight = (Main.remixWorld ? SulphurousSea.YStart : ((SulphurousSea.YStart + (int)Main.worldSurface) / 2 + 90));
		if (Main.remixWorld)
		{
			if (MeetsBaseAbyssRequirement(player, out var _) && point.Y < abyssStartHeight && BiomeTileCounterSystem.Layer1Tiles >= 200 && !player.Calamity().ZoneAbyssLayer2 && !player.Calamity().ZoneAbyssLayer3)
			{
				return !player.Calamity().ZoneAbyssLayer4;
			}
			return false;
		}
		if (MeetsBaseAbyssRequirement(player, out var _) && point.Y >= abyssStartHeight && BiomeTileCounterSystem.Layer1Tiles >= 200 && !player.Calamity().ZoneAbyssLayer2 && !player.Calamity().ZoneAbyssLayer3)
		{
			return !player.Calamity().ZoneAbyssLayer4;
		}
		return false;
	}
}
