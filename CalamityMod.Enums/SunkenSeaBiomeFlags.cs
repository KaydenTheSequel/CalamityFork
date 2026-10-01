using System;

namespace CalamityMod.Enums;

[Flags]
public enum SunkenSeaBiomeFlags : byte
{
	None = 0,
	UndergroundDesert = 1,
	TimelessShores = 2,
	RadiantReefs = 4,
	PolypForest = 8,
	GleamingBurrows = 0x10,
	BasaltGully = 0x20,
	ClamDen = 0x40
}
