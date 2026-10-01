using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class HeartPlanet : Planetoid
{
	private ushort[] mossTypes = new ushort[4] { 182, 179, 183, 181 };

	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		int radius = GenBase._random.Next(6, 10);
		if (!CheckIfPlaceable(origin, radius, structures))
		{
			return false;
		}
		PlacePlanet(origin, radius);
		return base.Place(origin, structures);
	}

	public void PlacePlanet(Point origin, int radius)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		ShapeData mainArea = new ShapeData();
		WorldUtils.Gen(origin, new Shapes.Circle(radius), Actions.Chain(new Modifiers.Blotches(), new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(56), new Actions.PlaceTile(1).Output(mainArea)));
		ushort gemType = GenBase._random.Next(new ushort[2] { 67, 66 });
		WorldGen.TileRunner(origin.X, origin.Y, GenBase._random.NextFloat(6f, 9f), GenBase._random.Next(8, 18), gemType);
		ushort mossType = GenBase._random.Next(mossTypes);
		WorldUtils.Gen(origin, new ModShapes.OuterOutline(mainArea), Actions.Chain(new Actions.SetTile(mossType, setSelfFrames: true, setNeighborFrames: false), new Modifiers.Conditions(new CustomConditions.RandomChance(2f)), new Actions.Smooth(applyToNeighbors: true), new Actions.SetFrames(frameNeighbors: true)));
		ShapeData room = new ShapeData();
		int width = GenBase._random.Next(3, 5) * 2;
		int height = GenBase._random.Next(5, 8);
		Point roomTopLeft = default(Point);
		((Point)(ref roomTopLeft))._002Ector(origin.X - width / 2, origin.Y - height / 2);
		bool gold = GenBase._random.NextBool();
		ushort tile = (ushort)(Main.getGoodWorld ? 76 : (gold ? 45 : 177));
		ushort wall = (ushort)(Main.getGoodWorld ? 177 : (gold ? 10 : 47));
		WorldUtils.Gen(roomTopLeft, new Shapes.Rectangle(width, height), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(wall).Output(room)));
		WorldUtils.Gen(roomTopLeft, new ModShapes.InnerOutline(room), Actions.Chain(new Actions.PlaceTile(tile)));
		WorldGen.AddLifeCrystal(origin.X, origin.Y + 2);
	}
}
