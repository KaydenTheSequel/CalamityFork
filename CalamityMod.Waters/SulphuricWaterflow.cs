using CalamityMod.Systems.Graphic.LiquidSystem;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class SulphuricWaterflow : ModWaterfallStyle, IWaterfallStyleModifyColor
{
	public void ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor)
	{
		WaterStyleCommon.ModifySulphuricWaterColor(x, y, ref liquidColor, isSlope: false);
	}

	void IWaterfallStyleModifyColor.ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor)
	{
		ModifyColor(in tile, x, y, ref liquidColor);
	}
}
