using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class NavyslateWall : ModWall, IVisibleThroughWater, ILoadable
{
	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 96;
		this.AddMapEntryWithWaterVisibility(new Color(11, 40, 43));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
