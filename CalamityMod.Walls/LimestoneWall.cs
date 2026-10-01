using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class LimestoneWall : ModWall, IVisibleThroughWater, ILoadable
{
	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 22;
		AddMapEntry(new Color(125, 85, 61));
		this.AddMapEntryWithWaterVisibility(new Color(78, 76, 127));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
