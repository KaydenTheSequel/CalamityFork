using System;
using System.Collections.Generic;
using CalamityMod.Schematics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class ShimmerShrine
{
	public static void PlaceShimmerShrine(StructureMap structures)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		string mapKey = "Shimmer Shrine Key";
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		int placementPositionX = (int)GenVars.shimmerPosition.X;
		int placementPositionY = (int)Main.worldSurface - 300;
		int offset = 28;
		for (; !Main.tile[placementPositionX, placementPositionY].HasTile; placementPositionY++)
		{
		}
		Point placementPoint = default(Point);
		((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY + offset);
		SchematicAnchor anchorType = SchematicAnchor.Center;
		bool place = true;
		SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, placementPoint, anchorType, ref place, FillShimmerShrineChest);
		CalamityUtils.AddProtectedStructure(CalamityUtils.GetSchematicProtectionArea(schematic, placementPoint, anchorType), 30);
	}

	public static void FillShimmerShrineChest(Chest chest, int Type, bool place)
	{
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(52, 1),
			new ChestItem(WorldGen.genRand.NextBool() ? 29 : 109, 1),
			new ChestItem((GenVars.gold == 8) ? 19 : 706, WorldGen.genRand.Next(5, 16)),
			new ChestItem(4345, WorldGen.genRand.Next(3, 5)),
			new ChestItem(188, WorldGen.genRand.Next(5, 11)),
			new ChestItem(4479, WorldGen.genRand.Next(1, 3)),
			new ChestItem(73, WorldGen.genRand.Next(2, 5))
		};
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}
}
