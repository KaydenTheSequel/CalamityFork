using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeLargeBasaltWall : MultiVariantModWall
{
	public override string Texture => "CalamityMod/Walls/LargeBasaltWall";

	public override void SetStaticDefaults()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = false;
		AddMapEntry(new Color(65, 64, 68));
	}

	public override void RandomUpdate(int i, int j)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		DrainWater(i - 1, j - 1);
		DrainWater(i - 1, j);
		DrainWater(i - 1, j + 1);
		DrainWater(i, j - 1);
		DrainWater(i, j);
		DrainWater(i, j + 1);
		DrainWater(i + 1, j - 1);
		DrainWater(i + 1, j);
		DrainWater(i + 1, j + 1);
		Dust obj = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 16, 16, 31, 0f, -1.9069767f, 195, new Color(255, 255, 255))];
		obj.noGravity = false;
		obj.fadeIn = 1.4209301f;
		static void DrainWater(int x, int y)
		{
			ref LiquidData liquidData = ref Main.tile[x, y].Get<LiquidData>();
			if (liquidData.LiquidType == 0 && liquidData.Amount != 0)
			{
				liquidData.Amount = 0;
				WorldGen.SquareTileFrame(x, y);
				if (Main.dedServ)
				{
					NetMessage.sendWater(x, y);
				}
			}
		}
	}

	public override void PlaceInWorld(int i, int j, Item item)
	{
		Tile t = Main.tile[i, j];
		if (t.LiquidType == 0 && j < Main.maxTilesY - 205)
		{
			t.LiquidAmount = 0;
			WorldGen.SquareTileFrame(i, j);
		}
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 36, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PopulateWallVariant(int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 4 * 468;
		frameYOffset = j % 4 * 180;
	}

	public override bool Drop(int i, int j, ref int type)
	{
		return false;
	}
}
