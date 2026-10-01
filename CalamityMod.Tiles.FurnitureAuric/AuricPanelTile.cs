using CalamityMod.Systems;
using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAuric;

public class AuricPanelTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = AuricOre.MineSound;
		base.MineResist = 3f;
		base.DustType = 55;
		AddMapEntry(new Color(213, 138, 69));
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
