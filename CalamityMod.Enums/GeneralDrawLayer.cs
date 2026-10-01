using System;

namespace CalamityMod.Enums;

[Flags]
public enum GeneralDrawLayer
{
	BeforeAllTiles = 1,
	BeforeSolidTiles = 2,
	BeforeNPCs = 4,
	AfterNPCs = 8,
	BeforeProjectiles = 0x10,
	AfterProjectiles = 0x20,
	AfterPlayers = 0x40,
	AfterDusts = 0x80,
	AfterEverything = 0x100
}
