using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeNavyslateWall : ModWall, IVisibleThroughWater, ILoadable
{
	public override string Texture => "CalamityMod/Walls/NavyslateWall";

	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = false;
		base.DustType = 96;
		this.AddMapEntryWithWaterVisibility(new Color(11, 40, 43));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool Drop(int i, int j, ref int type)
	{
		return false;
	}
}
