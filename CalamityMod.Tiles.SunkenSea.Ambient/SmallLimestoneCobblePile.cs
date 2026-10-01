using Terraria.ID;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SmallLimestoneCobblePile : SmallLimestoneCobblePileBase
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		TileID.Sets.BreakableWhenPlacing[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileObjectData.GetTileData(base.Type, 0).LavaDeath = false;
	}
}
