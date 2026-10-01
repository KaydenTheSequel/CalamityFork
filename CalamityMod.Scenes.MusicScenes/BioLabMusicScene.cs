using System;
using CalamityMod.Systems;
using CalamityMod.Walls;
using CalamityMod.Walls.DraedonStructures;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Scenes.MusicScenes;

public class BioLabMusicScene : ModSceneEffect
{
	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("BioLab") ?? (-1);

	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override float GetWeight(Player player)
	{
		return 0.6f;
	}

	public override bool IsSceneEffectActive(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Tile backWall = Framing.GetTileSafely((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
		Vector2 playerPosition = player.Center;
		float num = Vector2.DistanceSquared(CalamityWorld.SunkenSeaLabCenter, playerPosition);
		float planetoidLabDistance = Vector2.DistanceSquared(CalamityWorld.PlanetoidLabCenter, playerPosition);
		float jungleLabDistance = Vector2.DistanceSquared(CalamityWorld.JungleLabCenter, playerPosition);
		float hellLabDistance = Vector2.DistanceSquared(CalamityWorld.HellLabCenter, playerPosition);
		float iceLabDistance = Vector2.DistanceSquared(CalamityWorld.IceLabCenter, playerPosition);
		float cavernLabDistance = Vector2.DistanceSquared(CalamityWorld.CavernLabCenter, playerPosition);
		double labRadius = Math.Pow(1280.0, 2.0);
		bool behindLabWall = backWall.WallType == 153 || backWall.WallType == 335 || backWall.WallType == 146 || backWall.WallType == 156 || backWall.WallType == 184 || backWall.WallType == 5 || backWall.WallType == 23 || backWall.WallType == 231 || backWall.WallType == 137 || backWall.WallType == 341 || backWall.WallType == 232 || backWall.WallType == 20 || backWall.WallType == 164 || backWall.WallType == 165 || backWall.WallType == 11 || backWall.WallType == 147 || backWall.WallType == 167 || backWall.WallType == 166 || backWall.WallType == 136 || backWall.WallType == ModContent.WallType<CinderplateWall>() || backWall.WallType == ModContent.WallType<ElumplateWall>() || backWall.WallType == ModContent.WallType<EutrophicGlassWall>() || backWall.WallType == ModContent.WallType<HavocplateWall>() || backWall.WallType == ModContent.WallType<HazardChevronWall>() || backWall.WallType == ModContent.WallType<LaboratoryPanelWall>() || backWall.WallType == ModContent.WallType<LaboratoryPlateBeam>() || backWall.WallType == ModContent.WallType<LaboratoryPlatePillar>() || backWall.WallType == ModContent.WallType<LaboratoryPlatingWall>() || backWall.WallType == ModContent.WallType<NavyplateWall>() || backWall.WallType == ModContent.WallType<OnyxplateWall>() || backWall.WallType == ModContent.WallType<PlagueContainmentCellsWall>() || backWall.WallType == ModContent.WallType<PlaguedPlateWall>() || backWall.WallType == ModContent.WallType<RustedPlatePillar>() || backWall.WallType == ModContent.WallType<RustedPlatingWall>() || backWall.WallType == ModContent.WallType<ShellstoneSlabWall>();
		bool nearBioLabPoint = (double)num <= labRadius || (double)planetoidLabDistance <= labRadius || (double)jungleLabDistance <= labRadius || (double)hellLabDistance <= labRadius || (double)iceLabDistance <= labRadius || (double)cavernLabDistance <= labRadius;
		return (BiomeTileCounterSystem.ArsenalLabTiles > 150) & behindLabWall & nearBioLabPoint;
	}
}
