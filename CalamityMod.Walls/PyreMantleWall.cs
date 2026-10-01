using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class PyreMantleWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 33;
		AddMapEntry(new Color(33, 30, 30));
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Main.tile[i, j].LiquidAmount == 0 && j < Main.maxTilesY - 205)
		{
			Main.tile[i, j].Get<LiquidData>().LiquidType = 0;
			Main.tile[i, j].LiquidAmount = byte.MaxValue;
			WorldGen.SquareTileFrame(i, j);
			if (Main.dedServ)
			{
				NetMessage.sendWater(i, j);
			}
		}
	}

	public override void KillWall(int i, int j, ref bool fail)
	{
		fail = true;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
