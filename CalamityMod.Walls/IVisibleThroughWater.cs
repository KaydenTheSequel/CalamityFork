using Terraria.ModLoader;

namespace CalamityMod.Walls;

public interface IVisibleThroughWater : ILoadable
{
	int WaterMapEntry { get; set; }
}
