using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeLimestoneWall : ModWall, IVisibleThroughWater, ILoadable
{
	public override string Texture => "CalamityMod/Walls/LimestoneWall";

	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = false;
		base.DustType = 22;
		AddMapEntry(new Color(125, 85, 61));
		this.AddMapEntryWithWaterVisibility(new Color(78, 76, 127));
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
