using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Walls;

public class FloralWall : MultiVariantModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(23, 39, 48));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 128, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 60, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PopulateWallVariant(int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int xPos = i % 2;
		int yPos = j % 3;
		switch (xPos)
		{
		case 0:
			switch (yPos)
			{
			case 0:
				frameXOffset = 0;
				frameYOffset = 0;
				break;
			case 1:
				frameXOffset = 0;
				frameYOffset = 1;
				break;
			case 2:
				frameXOffset = 0;
				frameYOffset = 2;
				break;
			}
			break;
		case 1:
			switch (yPos)
			{
			case 0:
				frameXOffset = 1;
				frameYOffset = 0;
				break;
			case 1:
				frameXOffset = 1;
				frameYOffset = 1;
				break;
			case 2:
				frameXOffset = 1;
				frameYOffset = 2;
				break;
			}
			break;
		}
		frameXOffset *= 468;
		frameYOffset *= 180;
	}
}
