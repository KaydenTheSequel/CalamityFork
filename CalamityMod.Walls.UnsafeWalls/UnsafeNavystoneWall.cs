using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeNavystoneWall : ModWall, IVisibleThroughWater, ILoadable
{
	public override string Texture => "CalamityMod/Walls/NavystoneWall";

	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 96;
		this.AddMapEntryWithWaterVisibility(new Color(16, 45, 48));
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
