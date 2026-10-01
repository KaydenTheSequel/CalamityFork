using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class ShellstoneWall : ModWall, IVisibleThroughWater, ILoadable
{
	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		base.DustType = 24;
		this.AddMapEntryWithWaterVisibility(new Color(74, 71, 84));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
