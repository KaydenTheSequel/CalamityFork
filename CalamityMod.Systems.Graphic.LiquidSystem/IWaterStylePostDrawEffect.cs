using Terraria;

namespace CalamityMod.Systems.Graphic.LiquidSystem;

public interface IWaterStylePostDrawEffect
{
	void PostDrawEffect(in Tile tile, int x, int y);
}
