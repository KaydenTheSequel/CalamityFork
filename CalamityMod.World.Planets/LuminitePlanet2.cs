using System;
using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class LuminitePlanet2 : Planetoid
{
	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		int radius = GenBase._random.Next(16, 21);
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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		Point offsetedOrigin = default(Point);
		for (int i = 0; i < 6; i++)
		{
			Vector2 offset = WorldGen.genRand.NextVector2Circular(7f, 7f);
			((Point)(ref offsetedOrigin))._002Ector((int)((float)origin.X + offset.X), (int)((float)origin.Y + offset.Y));
			WorldUtils.Gen(offsetedOrigin, new Shapes.Circle(radius), Actions.Chain(new Actions.PlaceTile((ushort)ModContent.TileType<ExodiumOre>()), new Actions.SetFrames()));
			WorldUtils.Gen(offsetedOrigin, new Shapes.Circle(radius - 4), Actions.Chain(new Actions.PlaceWall(1)));
			WorldUtils.Gen(origin, new Shapes.Circle(radius - 2), Actions.Chain(new Modifiers.Blotches(), new Actions.SetTile(408, setSelfFrames: true), new Actions.SetFrames()));
		}
		float outwardTentacleReach = (float)radius + 18f;
		float initialRotationalOffset = WorldGen.genRand.NextFloat((float)Math.PI * 2f);
		Vector2[] initialTendrilDirections = (Vector2[])(object)new Vector2[5];
		for (int j = 0; j < initialTendrilDirections.Length; j++)
		{
			Vector2[] veinDirections = (Vector2[])(object)new Vector2[4];
			veinDirections[0] = ((float)Math.PI * 2f * (float)j / (float)initialTendrilDirections.Length + initialRotationalOffset).ToRotationVector2();
			veinDirections[0] = veinDirections[0].RotatedBy(WorldGen.genRand.NextFloat(-0.19f, 0.19f));
			for (int k = 1; k < veinDirections.Length; k++)
			{
				veinDirections[k] = veinDirections[k - 1].RotatedBy(WorldGen.genRand.NextFloat(-0.87f, 0.87f));
			}
			for (int l = 0; l <= 18; l++)
			{
				float completionRatio = (float)l / 18f;
				Vector2 offset2 = Vector2.CatmullRom(veinDirections[0], veinDirections[1], veinDirections[2], veinDirections[3], (float)l / 18f);
				offset2 *= completionRatio * outwardTentacleReach;
				Point origin2 = new Point((int)((float)origin.X + offset2.X), (int)((float)origin.Y + offset2.Y));
				int strength = (int)(4f - completionRatio * 4f) + 1;
				WorldUtils.Gen(origin2, new Shapes.Circle(strength), Actions.Chain(new Actions.SetTile((ushort)ModContent.TileType<ExodiumOre>(), setSelfFrames: true)));
			}
		}
		WorldUtils.Gen(origin, new Shapes.Circle(radius + 36), Actions.Chain(new Actions.Smooth()));
		if (Main.dedServ)
		{
			NetMessage.SendTileSquare(-1, origin.X - radius - 36, origin.Y - radius - 36, radius * 2 + 36, radius * 2 + 36);
		}
	}
}
