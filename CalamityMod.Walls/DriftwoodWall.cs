using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class DriftwoodWall : ModWall, IVisibleThroughWater, ILoadable
{
	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		this.AddMapEntryWithWaterVisibility(new Color(69, 56, 58));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
