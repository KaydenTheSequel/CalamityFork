using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class LuminitePlanet : Planetoid
{
	public static void GenerateLuminitePlanetoids()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		if (GenVars.structures == null)
		{
			GenVars.structures = new StructureMap();
		}
		WorldGenConfiguration config = WorldGenConfiguration.FromEmbeddedPath("Terraria.GameContent.WorldBuilding.Configuration.json");
		int totalPlanetoidsToGenerate = Main.maxTilesX / 1200 + 2;
		Point planetoidOrigin = default(Point);
		for (int i = 0; i < totalPlanetoidsToGenerate; i++)
		{
			for (int tries = 0; tries < 15000; tries++)
			{
				((Point)(ref planetoidOrigin))._002Ector(WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.15), (int)((double)Main.maxTilesX * 0.85)), WorldGen.genRand.Next(75, 125));
				if (WorldGen.genRand.NextBool(2))
				{
					if (config.CreateBiome<LuminitePlanet>().Place(planetoidOrigin, GenVars.structures))
					{
						break;
					}
				}
				else if (config.CreateBiome<LuminitePlanet2>().Place(planetoidOrigin, GenVars.structures))
				{
					break;
				}
			}
		}
	}

	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		int radius = GenBase._random.Next(14, 18);
		if (!CheckIfPlaceable(origin, radius, structures))
		{
			return false;
		}
		PlacePlanet(origin, radius);
		return base.Place(origin, structures);
	}

	public void PlacePlanet(Point origin, int radius)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		Point offsetedOrigin = default(Point);
		for (int i = 0; i < 6; i++)
		{
			Vector2 offset = WorldGen.genRand.NextVector2Circular(7f, 7f);
			((Point)(ref offsetedOrigin))._002Ector((int)((float)origin.X + offset.X), (int)((float)origin.Y + offset.Y));
			WorldUtils.Gen(offsetedOrigin, new Shapes.Circle(radius), Actions.Chain(new Actions.PlaceTile((ushort)ModContent.TileType<ExodiumOre>())));
			WorldUtils.Gen(offsetedOrigin, new Shapes.Circle(radius - 4), Actions.Chain(new Actions.PlaceWall(1)));
			WorldUtils.Gen(origin, new Shapes.Circle(radius - 3), Actions.Chain(new Modifiers.Blotches(), new Actions.SetTile(408, setSelfFrames: true)));
		}
		WorldUtils.Gen(origin, new Shapes.Circle(radius + 16), Actions.Chain(new Actions.Smooth()));
		if (Main.dedServ)
		{
			NetMessage.SendTileSquare(-1, origin.X - radius - 16, origin.Y - radius - 16, radius * 2 + 16, radius * 2 + 16);
		}
	}
}
