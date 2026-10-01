using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "NavystoneWallSafe" })]
public class NavystoneWall : ModWall, IVisibleThroughWater, ILoadable
{
	public override string Texture => "CalamityMod/Walls/NavystoneWall";

	int IVisibleThroughWater.WaterMapEntry { get; set; }

	public override void SetStaticDefaults()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		base.DustType = 96;
		this.AddMapEntryWithWaterVisibility(new Color(0, 50, 50));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
