using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace CalamityMod;

public class TileRunner
{
	public Vector2 pos;

	public Vector2 speed;

	public Point16 hRange;

	public Point16 vRange;

	public double strength;

	public double str;

	public int steps;

	public int stepsLeft;

	public ushort type;

	public bool addTile;

	public bool overRide;

	public TileRunner(Vector2 pos, Vector2 speed, Point16 hRange, Point16 vRange, double strength, int steps, ushort type, bool addTile, bool overRide)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		this.pos = pos;
		if (speed.X == 0f && speed.Y == 0f)
		{
			this.speed = new Vector2((float)WorldGen.genRand.Next(hRange.X, hRange.Y + 1) * 0.1f, (float)WorldGen.genRand.Next(vRange.X, vRange.Y + 1) * 0.1f);
		}
		else
		{
			this.speed = speed;
		}
		this.hRange = hRange;
		this.vRange = vRange;
		this.strength = strength;
		str = strength;
		this.steps = steps;
		stepsLeft = steps;
		this.type = type;
		this.addTile = addTile;
		this.overRide = overRide;
	}

	public void Start()
	{
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		while (str > 0.0 && stepsLeft > 0)
		{
			str = strength * (double)stepsLeft / (double)steps;
			int num = (int)Math.Max((double)pos.X - str * 0.5, 1.0);
			int b = (int)Math.Min((double)pos.X + str * 0.5, Main.maxTilesX - 1);
			int c = (int)Math.Max((double)pos.Y - str * 0.5, 1.0);
			int d = (int)Math.Min((double)pos.Y + str * 0.5, Main.maxTilesY - 1);
			for (int i = num; i < b; i++)
			{
				for (int j = c; j < d; j++)
				{
					if (!((double)(Math.Abs((float)i - pos.X) + Math.Abs((float)j - pos.Y)) >= strength * StrengthRange()))
					{
						ChangeTile(Main.tile[i, j]);
					}
				}
			}
			str += 50.0;
			while (str > 50.0)
			{
				pos += speed;
				stepsLeft--;
				str -= 50.0;
				speed.X += (float)WorldGen.genRand.Next(hRange.X, hRange.Y + 1) * 0.05f;
				speed.Y += (float)WorldGen.genRand.Next(vRange.X, vRange.Y + 1) * 0.05f;
			}
			speed = Vector2.Clamp(speed, new Vector2(-1f, -1f), new Vector2(1f, 1f));
		}
	}

	public virtual void ChangeTile(Tile tile)
	{
		if (!addTile)
		{
			tile.HasTile = false;
		}
		else
		{
			tile.TileType = type;
		}
	}

	public virtual double StrengthRange()
	{
		return 0.5 + (double)WorldGen.genRand.Next(-10, 11) * 0.0075;
	}
}
